﻿using Arch.EntityFrameworkCore.UnitOfWork;

namespace MyToDo.api.Context.Repository
{
    public class UserRepository : Repository<User>, IRepository<User>
    {
        public UserRepository(MyToDoContext dbContext) : base(dbContext)
        {
        }
    }
}
