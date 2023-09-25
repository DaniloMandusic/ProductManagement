using Infrastructure.Interfaces;
using Infrastructure.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Data.JSON;

public class ProductServiceJSON: IProductService
{
    public List<Product> getProducts()
    {
        string jsonFilePath = "C:\\Users\\danilo\\RiderProjects\\Products\\Data\\JSON\\Files\\data.json";
        
        if (System.IO.File.Exists(jsonFilePath))
        {
            string jsonData = System.IO.File.ReadAllText(jsonFilePath);
            
            JObject jsonObject = JObject.Parse(jsonData);
            JToken productsArray = jsonObject["products"];
            string jsonArrayString = productsArray.ToString();

            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(jsonArrayString);

            return products;
        }
        else
        {
            List<Product> list = new List<Product>();
            return list; // return empty list
        }
    }

    public void addProduct(Product newProduct)
    {
        string jsonFilePath = "C:\\Users\\danilo\\RiderProjects\\Products\\Data\\JSON\\Files\\data.json";
        string jsonData = System.IO.File.ReadAllText(jsonFilePath);

        JObject jsonObject = JObject.Parse(jsonData);
        
        JArray existingProductsArray = (JArray)jsonObject["products"];
        
        int newProductId = existingProductsArray
            .Select(p => (int)p["id"])
            .DefaultIfEmpty(0)
            .Max() + 1;
        
        JObject newProductObject = new JObject
        {
            { "id", newProductId },
            { "name", newProduct.name },
            { "price", newProduct.price },
            {"description", newProduct.description},
            {"categoryId", newProduct.categoryId},
            {"manufacturerId", newProduct.manufacturerId},
            {"supplierId", newProduct.supplierId},
        };
        
        existingProductsArray.Add(newProductObject);
        
        string updatedJsonData = jsonObject.ToString(Formatting.Indented);
        
        System.IO.File.WriteAllText(jsonFilePath, updatedJsonData);
    }
    
    public void deleteProduct(Product product)
    {
        throw new NotImplementedException();
    }
    
    public void editProduct(Product editedProduct)
    {
        string jsonFilePath = "C:\\Users\\danilo\\RiderProjects\\Products\\Data\\JSON\\Files\\data.json";
        string jsonData = System.IO.File.ReadAllText(jsonFilePath);
        
        JObject jsonObject = JObject.Parse(jsonData);
        
        JArray productsArray = (JArray)jsonObject["products"];
        
        int index = productsArray
            .Select((p, i) => new { Product = p, Index = i })
            .FirstOrDefault(item => (int)item.Product["id"] == editedProduct.id)?.Index ?? -1;

        if (index != -1)
        {
            JObject productObject = (JObject)productsArray[index];
            productObject["name"] = editedProduct.name;
            productObject["price"] = editedProduct.price;
            productObject["description"] = editedProduct.description;
            productObject["categoryId"] = editedProduct.categoryId;
            productObject["manufacturerId"] = editedProduct.manufacturerId;
            productObject["supplierId"] = editedProduct.supplierId;
            
            string updatedJsonData = jsonObject.ToString(Formatting.Indented);
            
            System.IO.File.WriteAllText(jsonFilePath, updatedJsonData);
        }
        else
        {
            return;
        }
    }

    public Product getProductById(int id)
    {
        try
        {
            string jsonFilePath = "C:\\Users\\danilo\\RiderProjects\\Products\\Data\\JSON\\Files\\data.json";
            
            string jsonData = System.IO.File.ReadAllText(jsonFilePath);
            JObject jsonObject = JObject.Parse(jsonData);
            
            JArray productsArray = (JArray)jsonObject["products"];
            
            JObject productObject = productsArray
                .Select(p => (JObject)p)
                .FirstOrDefault(p => (int)p["id"] == id);

            if (productObject != null)
            {
                Product product = productObject.ToObject<Product>();
                return product;
            }
            else
            {
                return null;
            }
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}