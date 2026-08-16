using TaskAP.Model;

namespace TaskAP.Repo
{
    public class ProductRepo : IProductRepo
    {
        private readonly List<Product> _products = new List<Product>();

        private int _nextId = 1;

        public IEnumerable<Product> GetAll()
        {
            return _products;
        }

        public Product? GetById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public Product Create(Product product)
        {
            product.Id = _nextId++;

            _products.Add(product);

            return product;
        }

        public Product? Update(int id, Product product)
        {
            var existingProduct = _products.FirstOrDefault(p => p.Id == id);

            if (existingProduct == null)
            {
                return null;
            }

            existingProduct.Name = product.Name;
            existingProduct.Price = product.Price;

            return existingProduct;
        }

        public bool Delete(int id)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return false;
            }

            _products.Remove(product);

            return true;
        }

        public Product? UpdateName(int id, string name)
        {
            var product = _products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return null;
            }

            product.Name = name;

            return product;
        }
    }
}
