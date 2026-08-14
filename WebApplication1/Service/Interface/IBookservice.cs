using WebApplication1.Model;

namespace WebApplication1.Service.Interface
{
    public interface IBookservice
    {
        IEnumerable<Books> ListAllBooks();
        Books GetBookById(int id);
        void AddBook(Books book);
        void DeleteBook(int id);
    }
}
