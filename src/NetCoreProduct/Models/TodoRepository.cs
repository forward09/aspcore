using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreProduct.Models
{
    public class TodoRepository : ITodoRepository
    {
        static ConcurrentDictionary<string, TodoItem> todoItems =
          new ConcurrentDictionary<string, TodoItem>();


        public TodoRepository()
        {
            Add(new TodoItem
            {
                Name = "Item1"
            });
        }

        public void Add(TodoItem item)
        {
            item.Key = Guid.NewGuid().ToString();
            todoItems[item.Key] = item;
        }

        public TodoItem Find(string key)
        {
            TodoItem item;
            todoItems.TryGetValue(key, out item);
            return item;
        }

        public IEnumerable<TodoItem> GetAll()
        {
            return todoItems.Values;
        }

        public TodoItem Remove(string key)
        {
            TodoItem item;

            todoItems.TryGetValue(key, out item);
            todoItems.TryRemove(key, out item);

            return item;
        }

        public void Update(TodoItem item)
        {
            todoItems[item.Key] = item;
        }
    }
}
