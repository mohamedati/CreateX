using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Createx.Core.Entities;

public partial class Product
{
    public int Id { get; set; }


    [StringLength(50)]
    public string NameAr { get; set; } = null!;


    [StringLength(50)]
    public string? NameEn { get; set; }

    public int? ParentProductId { get; set; }

    public int StoreId { get; set; }

    
    public double SalePrice { get; set; }

    public double PurchasePrice { get; set; }

    public string ProductId { get; set; } = null!;

    public string Barcode { get; set; } = null!;

    public string? ExtraBarcode { get; set; }

    public int TypeId { get; set; }



    public byte[] Photo { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public bool IsPublish { get; set; }
    
    public virtual Product? ParentProduct { get; set; }
    public virtual ICollection<Product> ChildProducts { get; set; } = new List<Product>();
    public virtual Store Store { get; set; } = null!;
    public virtual ProductType Type { get; set; } = null!;
    public virtual ICollection<ProductDetail> ProductDetails { get; set; } = new List<ProductDetail>();
}
