using System.Threading;
using System.Threading.Tasks;
using Domain.Entities.Contact;
using MediatR;

namespace Application.Contact.Commands.CreateContact
{
    public class CreateContactHandler : IRequestHandler<CreateContactCommand,int>
    {
        public async Task<int> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var entity = new ContactEntity();

            entity.FirstName = request.FirstName;
            entity.LastName = request.LastName;

            //create an interface that acts like db access, follow similiar pattern you did in DRP
            return 1;
            //watch nick chapsas video on his method of doing this
        }
    }
}