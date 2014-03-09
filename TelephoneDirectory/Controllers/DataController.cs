namespace TelephoneDirectory.Controllers
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using TelephoneDirectory.Models;

    public class DataController : ApiController
    {
        #region Public Methods and Operators

        public async void Delete(int id)
        {
            using (DataContext context = new DataContext())
            {
                TelephoneEntry entity = await context.TelephoneEntries.FirstOrDefaultAsync(t => t.Id == id);
                if (entity != null)
                {
                    context.Entry(entity).State = EntityState.Deleted;
                }

                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<TelephoneEntry>> Get()
        {
            using (DataContext context = new DataContext())
            {
                return await context.TelephoneEntries.ToListAsync();
            }
        }

        public async Task<TelephoneEntry> Get(int id)
        {
            using (DataContext context = new DataContext())
            {
                return await context.TelephoneEntries.FirstOrDefaultAsync(t => t.Id == id);
            }
        }

        public async Task<List<TelephoneEntry>> Get(string query)
        {
            using (DataContext context = new DataContext())
            {
                return await context.TelephoneEntries.Where(t => string.Equals(t.FirstName, query) || string.Equals(t.LastName, query)).ToListAsync();
            }
        }

        public async Task<List<TelephoneEntry>> Get(string firstName, string lastName)
        {
            using (DataContext context = new DataContext())
            {
                return await context.TelephoneEntries.Where(t => string.Equals(t.FirstName, firstName) && string.Equals(t.LastName, lastName)).ToListAsync();
            }
        }

        public async Task<int> Post([FromBody]TelephoneEntry telephoneEntry)
        {
            using (DataContext context = new DataContext())
            {
                if (telephoneEntry.Id == 0)
                {
                    context.Entry(telephoneEntry).State = EntityState.Added;
                }
                else
                {
                    context.Entry(telephoneEntry).State = EntityState.Modified;
                }

                await context.SaveChangesAsync();
                return telephoneEntry.Id;
            }
        }

        #endregion
    }
}