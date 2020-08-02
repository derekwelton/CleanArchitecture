using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities.Contact
{
    public class ContactEntity : AuditableEntity
    {
        public int Contact_ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Notes { get; set; }
        public ContactType Type { get; set; }
        public IList<ContactAddressEntity> Addresses { get; private set; } = new List<ContactAddressEntity>();
        public IList<ContactPhoneNumberEntity> PhoneNumbers { get; private set; } = new List<ContactPhoneNumberEntity>();
    }
}