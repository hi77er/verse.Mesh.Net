using Moq;
using verse.Mesh.Net.Core.CartAggregate;
using verse.Mesh.Net.Core.ProductAggregate;
using verse.Mesh.Net.Infrastructure.Data.MemCache;
using verse.Mesh.Net.UseCases.Carts;
using verse.Mesh.Net.UseCases.Carts.Create;
using verse.Mesh.Net.UseCases.Carts.Get;
using verse.Mesh.Net.UseCases.Products;
using verse.Mesh.Net.UseCases.Products.Get;

namespace verse.Mesh.Net.UnitTests.UseCases;

public class CartUseCasesTests
{
  private readonly Guid _testCartId = Guid.NewGuid();
  private readonly Guid _testUserId = Guid.NewGuid();
  private readonly Guid _testProductId = Guid.NewGuid();
  private readonly string _testProductName = "Test Product Name";
  private readonly decimal _testProductPrice = 1.5m;

  private readonly Mock<IDistributedCacheAdapter> _cacheService = new Mock<IDistributedCacheAdapter>();

  private GetCartByUserHandler _getCartByUserIdhandler;
  private CreateCartHandler _createCartHandler;
  private GetProductByIdHandler _getProductByIdhandler;

  public CartUseCasesTests()
  {
    _getCartByUserIdhandler = new GetCartByUserHandler(_cacheService.Object);
    _createCartHandler = new CreateCartHandler(_cacheService.Object);
    _getProductByIdhandler = new GetProductByIdHandler(_cacheService.Object);
  }

  [SetUp]
  public void Setup() { }

  [Test]
  public async Task GetCartByUserId_Success()
  {
    var _testCart = new Cart(_testUserId, new List<CartItem>() { })
    {
      Id = _testCartId
    };
    var _testCartDTO = new CartDTO(_testCartId, _testUserId, new List<CartItemDTO>() { });

    var testUserIdStr = _testUserId.ToString();
    _cacheService.Setup(x => x.GetItemAsync<Cart>(testUserIdStr)).ReturnsAsync(_testCart);

    var cToken = CancellationToken.None;
    var request = new GetCartByUserQuery(_testUserId);

    var result = await _getCartByUserIdhandler.Handle(request, cToken);

    Assert.IsNotNull(result);
    Assert.That(result, Is.TypeOf<Ardalis.Result.Result<CartDTO>>());
    Assert.That(_testCartId, Is.EqualTo(result.Value.Id));
    Assert.That(_testUserId, Is.EqualTo(result.Value.UserId));
  }

  [Test]
  public async Task CreateCart_Success()
  {
    var cToken = CancellationToken.None;
    var command = new CreateCartCommand(_testUserId, new List<CreateCartItemDTO>() { });
    var commandResult = await _createCartHandler.Handle(command, cToken);

    Assert.IsNotNull(commandResult?.Value);
    Assert.That(commandResult, Is.TypeOf<Ardalis.Result.Result<Guid>>());
    Assert.That(commandResult?.Value, Is.Not.Empty);
  }

  [Test]
  public async Task GetProductById_Success()
  {
    var _testProduct = new Product(_testProductName, _testProductPrice)
    {
      Id = _testProductId
    };
    var _testProductDTO = new ProductDTO(_testProductId, _testProductName, _testProductPrice);

    var testProductIdStr = _testProductId.ToString();
    _cacheService.Setup(x => x.GetItemAsync<Product>(testProductIdStr)).ReturnsAsync(_testProduct);

    var request = new GetProductByIdQuery(_testProductId);

    var cToken = CancellationToken.None;
    var result = await _getProductByIdhandler.Handle(request, cToken);

    Assert.IsNotNull(result);
    Assert.That(result, Is.TypeOf<Ardalis.Result.Result<ProductDTO>>());
    Assert.That(_testProductId, Is.EqualTo(result.Value.Id));
    Assert.That(_testProductName, Is.EqualTo(result.Value.Name));
    Assert.That(_testProductPrice, Is.EqualTo(result.Value.Price));
  }

}
