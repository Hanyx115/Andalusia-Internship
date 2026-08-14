using WebApplication1.Model;
using WebApplication1.Repo.Interface;
using WebApplication1.Service.Interface;

namespace WebApplication1.Service
{
    public class BookService: IBookservice
    {
        private readonly IBooksrepo _bookRepo;
        public BookService(IBooksrepo bookRepo)
        {
            _bookRepo = bookRepo;
        }

        public void AddBook(Books book)
        {
            _bookRepo.AddBook(book);
        }

        public void DeleteBook(int id)
        {
            _bookRepo.DeleteBook(id);
        }

        public Books GetBookById(int id)
        {
            return _bookRepo.GetBookById(id);
        }

        public IEnumerable<Books> ListAllBooks()
        {
            return _bookRepo.ListAllBooks();
        }
    }
}
