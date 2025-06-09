using IncidentCreator.Application.Dtos;
using IncidentCreator.Application.Interfaces;
using IncidentCreator.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace IncidentCreator.Application.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IncidentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> CreateIncidentProcessAsync(IncidentCreationRequest request)
        {
            var account = await _unitOfWork.Accounts.FindAsync(a => a.Name == request.AccountName);
            if (account == null)
            {
                return null;
            }

            var contact = await _unitOfWork.Contacts.FindAsync(c => c.Email == request.ContactEmail);

            if (contact != null)
            {
                contact.FirstName = request.ContactFirstName;
                contact.LastName = request.ContactLastName;
                
                if (contact.AccountId != account.Id)
                {
                    contact.AccountId = account.Id;
                }
                _unitOfWork.Contacts.Update(contact);
            }
            else
            {
                contact = new Contact
                {
                    FirstName = request.ContactFirstName,
                    LastName = request.ContactLastName,
                    Email = request.ContactEmail,
                    AccountId = account.Id
                };
                await _unitOfWork.Contacts.AddAsync(contact);
            }

            var incident = new Incident
            {
                IncidentName = $"INC-{Guid.NewGuid()}",
                Description = request.IncidentDescription,
                Account = account
            };

            await _unitOfWork.Incidents.AddAsync(incident);

            await _unitOfWork.CompleteAsync();

            return incident.IncidentName;
        }
    }
}