namespace TelephoneDirectory.Models
{
    using System.Data.Entity;

    public class Initializer : DropCreateDatabaseAlways<DataContext>
    {
        #region Methods

        protected override void Seed(DataContext context)
        {
            context.TelephoneEntries.Add(new TelephoneEntry { FirstName = "Jon", LastName = "Preece", Number = "4444" });
        }

        #endregion
    }
}