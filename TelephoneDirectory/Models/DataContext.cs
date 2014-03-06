namespace TelephoneDirectory.Models
{
    using System.Data.Entity;

    public class DataContext : DbContext
    {
        #region Public Properties

        public DbSet<TelephoneEntry> TelephoneEntries { get; set; }

        #endregion
    }
}