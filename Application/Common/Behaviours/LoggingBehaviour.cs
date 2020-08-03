using System.Threading;
using System.Threading.Tasks;
using Application.Common.Identity;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUser _currentUser;
        private readonly IIdentityRepository _identityRepository;

        public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUser currentUser, IIdentityRepository identityRepository)
        {
            _logger = logger;
            _currentUser = currentUser;
            _identityRepository = identityRepository;
        }

        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userId = _currentUser.UserId ?? string.Empty;
            string userName = string.Empty;

            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _identityRepository.FindByIdAsync(userId);
                userName = user.UserName;
            }

            _logger.LogInformation("CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}",
                requestName, userId, userName, request);
        }
    }
}