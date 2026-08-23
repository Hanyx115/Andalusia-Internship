using TaskA5.Repo.Interfaces;

namespace TaskA5.Repo
{
    public class Taskrepo : ITaskRepo
    {
        private readonly List<Task> _tasks = new();
        public List<Task> GetAll()
        {
            return _tasks;
            
        }
    }
}
