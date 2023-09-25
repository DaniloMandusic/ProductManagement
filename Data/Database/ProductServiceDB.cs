using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using Infrastructure.Interfaces;
using Infrastructure.Models;
using Microsoft.EntityFrameworkCore.Sqlite;

namespace Data.Database;

public class ProductServiceDB: IProductService
{
    private readonly DatabaseContext _context;

    public ProductServiceDB(DatabaseContext context)
    {
        _context = context;
    }
    
    
    public List<Product> getProducts()
    {
        var data = _context.Products.ToList();
        
        return data;
    }

    public void addProduct(Product product)
    {
        List<Product> allProducts = getProducts();
        
        Product lastProduct = allProducts.Last();
        product.id = lastProduct.id + 1;
        
        _context.Products.Add(product);
        _context.SaveChanges();
    }

    public void deleteProduct(Product product)
    {
        throw new NotImplementedException();
    }

    public void editProduct(Product product)
    {
        Product oldProduct = getProductById(product.id);
        oldProduct.id = product.id;
        oldProduct.name = product.name;
        oldProduct.description = product.description;
        oldProduct.price = product.price;
        oldProduct.categoryId = product.categoryId;
        oldProduct.manufacturerId = product.manufacturerId;
        oldProduct.supplierId = product.supplierId;
        
        _context.SaveChanges();
    }

    public Product getProductById(int id)
    {
        Product product = _context.Products.FirstOrDefault(item => item.id == id);

        return product;
    }
}