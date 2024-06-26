﻿namespace verse.Mesh.Net.Infrastructure.Models;

public record ProductRecord(Guid Id, string Name, decimal Price, string? Description = null);
