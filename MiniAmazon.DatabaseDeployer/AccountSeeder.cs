using DomainDrivenDatabaseDeployer;
using MiniAmazon.Domain.Entities;
using NHibernate;

namespace MiniAmazon.DatabaseDeployer
{
    public class AccountSeeder : IDataSeeder
    {
        private readonly ISession _session;

        public AccountSeeder(ISession session)
        {
            _session = session;
        }

        public void Seed()
        {
            var account = new Account
                {
                    Name = "Carlos Luis",
                    Email = "carlosluis_lopez@yahoo.com",
                    Password = "123",
                    Age = 24,
                    Genre = "M",
                    Status = true,
                };

            _session.Save(account);
        }
    }
}