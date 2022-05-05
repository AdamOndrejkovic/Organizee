using DataAccess.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccess
{
    public class OrganizeeDbContext: DbContext
    {
        public OrganizeeDbContext(DbContextOptions<OrganizeeDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        
        public virtual DbSet<UserEntity> Users { get; set; }
        public virtual DbSet<TodoItemEntity> Todos { get; set; }
    }
}