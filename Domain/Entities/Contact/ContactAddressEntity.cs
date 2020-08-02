using Domain.Common;

namespace Domain.Entities.Contact
{
    public class ContactAddressEntity : AuditableEntity
    {
        public int Address_ID { get; set; }
        public string Address_Name { get; set; }
        public string Description { get; set; }
        public string Address_1 { get; set; }
        public string Address_2 { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string CountryCode { get; set; }
    }
}