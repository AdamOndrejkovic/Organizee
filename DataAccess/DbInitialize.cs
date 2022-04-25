namespace DataAccess
{
    public class DbInitialize
    {
        public static void InitData(OrganizeeDbContext dbContext)
        {
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();
        }

    }
}