using TaskAP.Model;

namespace TaskAP.Repo
{ 
    public interface IProductRepo
    {
        IEnumerable<Product> GetAll();

        Product? GetById(int id);

        Product Create(Product product);

        Product? Update(int id, Product product);

        bool Delete(int id);

        Product? UpdateName(int id, string name);
    }
}
