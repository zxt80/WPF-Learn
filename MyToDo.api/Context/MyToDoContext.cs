using Microsoft.EntityFrameworkCore;

namespace MyToDo.api.Context
{
    public class MyToDoContext:DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<Memo> Memos { get; set; }

        public MyToDoContext(DbContextOptions<MyToDoContext> options):base(options)
        {

        }
    }
}
