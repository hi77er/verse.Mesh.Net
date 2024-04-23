namespace verse.Mesh.Net.InventoryService.Carts;

public record CartRecord(Guid Id, Guid UserId, IEnumerable<CartItemRecord> CartItems);
