using WebApplication1.Model;

namespace WebApplication1.Repo.Interface
{
    public interface IBooksrepo
    {
        IEnumerable<Books> ListAllBooks();
        Books GetBookById(int id);
        void AddBook(Books book);
        void DeleteBook(int id);
    }
}
