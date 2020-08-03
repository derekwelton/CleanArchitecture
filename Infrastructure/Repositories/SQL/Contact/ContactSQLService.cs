using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Application.Repositories.Contact;
using Dapper;
using Domain;
using Domain.Entities.Contact;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repositories.SQL.Contact
{
    public class ContactSQLService : IContactRepository
    {
        private readonly SQLService _service;
        private readonly IConfiguration _config;
        private readonly string _connectionString;

        public ContactSQLService(SQLService service, IConfiguration config)
        {
            _service = service;
            _config = config;
            _connectionString = _config["ConnectionStrings:Master"];
        }


        public async Task<ContactEntity> Insert(ContactEntity entity)
        {
            var p = new DynamicParameters();
            p.Add("@ID", 0, DbType.Int32, ParameterDirection.Output);
            p.Add("@FirstName", entity.FirstName);
            p.Add("@LastName", entity.LastName);
            p.Add("@Email", entity.Email);
            p.Add("@Notes", entity.Notes);
            p.Add("@Type", entity.Type);

            await _service.SaveDataAsync("spContact_Insert", p);

            entity.Contact_ID = p.Get<int>(@"ID");

            return entity;
        }
        public async Task<ContactEntity> Update(ContactEntity entity)
        {
            var p = new DynamicParameters();
            p.Add("@ID", entity.Contact_ID);
            p.Add("@FirstName", entity.FirstName);
            p.Add("@LastName", entity.LastName);
            p.Add("@Email", entity.Email);
            p.Add("@Notes", entity.Notes);
            p.Add("@Type", entity.Type);

            await _service.SaveDataAsync("spContact_Update", p);

            entity.Contact_ID = p.Get<int>(@"ID");

            return entity;
        }
        public async Task<ContactEntity> GetByID(object id)
        {
            var p = new DynamicParameters();
            p.Add("@ID", id);

            var output = await _service.LoadDataAsync<ContactEntity, dynamic>("spContact_GetID", p);

            return output?.FirstOrDefault();
        }
        public async Task<ContactEntity> GetByIDWithAddressesAndPhoneNumbers(object id)
        {
            var p = new DynamicParameters();
            p.Add("@ID", id);

            var output = await LoadContactWithAddressAndPhoneNumbersDataAsync<dynamic, string>("spContact_GetByIDWithAddressesAndPhoneNumbers", p, "ContactAddress_ID,ContactPhoneNumber_ID");

            return output?.FirstOrDefault();
        }
        public async Task<IEnumerable<ContactEntity>> Get(string firstName = null, string lastName = null, string email = null, ContactType? type = null)
        {
            var p = new DynamicParameters();
            p.Add("@FirstName", firstName);
            p.Add("@LastName", lastName);
            p.Add("@Email", email);
            p.Add("@Type", type);

            var output = await _service.LoadDataAsync<ContactEntity, dynamic>("spContact_Get", p);

            return output?.ToList();
        }
        public async Task<IEnumerable<ContactEntity>> GetWithAddressesAndPhoneNumbers(string firstName = null, string lastName = null, string email = null, ContactType? type = null)
        {
            var p = new DynamicParameters();
            p.Add("@FirstName", firstName);
            p.Add("@LastName", lastName);
            p.Add("@Email", email);
            p.Add("@Type", type);

            var output = await LoadContactWithAddressAndPhoneNumbersDataAsync<dynamic, string>("spContact_GetWithAddressesAndPhoneNumbers", p, "ContactAddress_ID,ContactPhoneNumber_ID");

            return output?.ToList();
        }
        public async Task Delete(ContactEntity entity)
        {
            var p = new DynamicParameters();
            p.Add("@ID",entity.Contact_ID);

            await _service.DeleteDataAsync("spContact_Delete", p);
        }
        public async Task Delete(object id)
        {
            var p = new DynamicParameters();
            p.Add("@ID", id);

            await _service.DeleteDataAsync("spContact_Delete", p);
        }



        private async Task<IEnumerable<ContactEntity>> LoadContactWithAddressAndPhoneNumbersDataAsync<P, T>(string storedProcedure, P parameters,T extra)
        {
            string splitOn = "";
            if (extra is string)
            {
                splitOn = extra as string;
                var lookup = new Dictionary<int, ContactEntity>();
                using (IDbConnection connection = new SqlConnection(_connectionString))
                {
                    var rows = await connection.QueryAsync<ContactEntity, ContactAddressEntity, ContactPhoneNumberEntity, ContactEntity>(storedProcedure, (parent, address, phoneNumber) =>
                    {
                        ContactEntity entity;

                        if (!lookup.TryGetValue(parent.Contact_ID, out entity))
                        {
                            entity = parent;
                            lookup.Add(entity.Contact_ID, entity);
                        }

                        if (address != null) entity.Addresses.Add(address);
                        if (phoneNumber != null) entity.PhoneNumbers.Add(phoneNumber);

                        return entity;
                    }, parameters, splitOn: splitOn, commandType: CommandType.StoredProcedure);

                    return rows.Distinct();
                }
            }
            else
            {
                throw new ArgumentException($"{extra} is not a string. Requires to be a string to be used as a split on value");
            }

        }
    }
}