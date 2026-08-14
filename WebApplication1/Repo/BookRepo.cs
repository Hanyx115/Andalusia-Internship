using System.Security.Cryptography.X509Certificates;
using WebApplication1.Model;
using WebApplication1.Repo.Interface;

namespace WebApplication1.Repo
{
    public class BookRepo : IBooksrepo
    {
        public List<Books> books = new List<Books>();

        public void AddBook(Books book)
        {
            books.Add(book);
        }
        
        public IEnumerable<Books> ListAllBooks()
        {
            return books;
        }
        public Books GetBookById(int id)
        {
            return books.FirstOrDefault(b => b.Id == id);
        }

        public void DeleteBook(int id)
        {
            var book = GetBookById(id);
            if (book != null)
            {
                books.Remove(book);
            }
            else
            {
                throw new Exception($"Book with ID {id} not found.");
            }
        }
    }
}
