using Infrastructure.Models;

namespace Infrastructure.Interfaces;

public interface IProductService
{
    List<Product> getProducts();
    void addProduct(Product product);
    void deleteProduct(Product product);
    void editProduct(Product product); 
    Product getProductById(int id);
}