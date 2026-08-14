using WebApplication1.Model;
using WebApplication1.Repo.Interface;

namespace WebApplication1
{
    public class ConflictException
    {
        private readonly IBooksrepo _bookrepo;

        public Books GetById(int id)
        {
            var task = _bookrepo.GetBookById(id);

            if (task == null)
            {
                throw new KeyNotFoundException(
                    $"Task with ID {id} was not found.");
            }

            return task;
        }
    }
}
