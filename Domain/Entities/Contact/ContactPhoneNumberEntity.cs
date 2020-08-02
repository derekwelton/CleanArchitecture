using Domain.Common;

namespace Domain.Entities.Contact
{
    public class ContactPhoneNumberEntity : AuditableEntity
    {
        public int ContactPhoneNumber_ID { get; set; }
        public string PhoneNumber { get; set; }
        public string Extension { get; set; }
    }
}