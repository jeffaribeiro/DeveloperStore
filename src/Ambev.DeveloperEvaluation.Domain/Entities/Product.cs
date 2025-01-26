using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Domain.Entities;

public class Product : BaseEntity
{
    /// <summary>
    /// The title of the product
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// The price of the product
    /// </summary>
    public decimal Price { get; set; }

    /// <summary>
    /// The description of the product
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// The category of the product
    /// </summary>
    public string Category { get; set; } = string.Empty;

    /// <summary>
    /// The image URL of the product
    /// </summary>
    public string Image { get; set; } = string.Empty;

    /// <summary>
    /// The rating details of the product
    /// </summary>
    public Rating Rating { get; set; } = new Rating();
}
