namespace Createx.Core.Entities;

public class OrderDetail
{
    public int Id { get; set; }

    public int Quantity { get; set; }

    public int ProductId { get; set; }

    public int OrderId { get; set; }

    public double Price { get; set; }

    public virtual Order Order { get; set; } = null!;


}