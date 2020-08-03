using System.Threading.Tasks;
using Domain.Entities.Contact;

namespace Application.Repositories.Contact
{
    public interface IContactRepository: IRepository<ContactEntity>
    {

    }
}