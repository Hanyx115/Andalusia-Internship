using TaskAP.Model;
using TaskAP.Repo;
using TaskAP.Service.Interface;


namespace TaskAP.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo _productRepo;

        public ProductService(IProductRepo productRepo)
        {
            _productRepo = productRepo;
        }

        public IEnumerable<Product> GetAll()
        {
            return _productRepo.GetAll();
        }

        public Product? GetById(int id)
        {
            return _productRepo.GetById(id);
        }

        public Product Create(Product product)
        {
            return _productRepo.Create(product);
        }

        public Product? Update(int id, Product product)
        {
            return _productRepo.Update(id, product);
        }

        public bool Delete(int id)
        {
            return _productRepo.Delete(id);
        }

        public Product? UpdateName(int id, string name)
        {
            return _productRepo.UpdateName(id, name);
        }
    }
}

