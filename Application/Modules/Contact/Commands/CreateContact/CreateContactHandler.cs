using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Identity;
using Application.Repositories.Contact;
using AutoMapper;
using Domain.Entities.Contact;
using MediatR;

namespace Application.Modules.Contact.Commands.CreateContact
{
    public class CreateContactHandler : IRequestHandler<CreateContactCommand,int>
    {
        private readonly IMapper _mapper;
        private readonly IContactRepository _contactRepository;
        private readonly ICurrentUser _currentUser;

        public CreateContactHandler(IMapper mapper, IContactRepository contactRepository, ICurrentUser currentUser)
        {
            _mapper = mapper;
            _contactRepository = contactRepository;
            _currentUser = currentUser;
        }

        public async Task<int> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<CreateContactCommand, ContactEntity>(request);
            entity.CreatedBy = _currentUser.UserId;
            entity.LastModifiedBy = _currentUser.UserId;
            entity.Created = DateTime.UtcNow;
            entity.LastModified = DateTime.UtcNow;

            entity = await _contactRepository.Insert(entity);

            return entity.Contact_ID;
        }
    }
}