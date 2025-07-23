using MediatR;
using PersonManagement.Application.Caching;
using PersonManagement.Application.Constants;
using PersonManagement.Application.Exceptions;
using PersonManagement.Domain.Repositories;
using System.Threading;
using System.Threading.Tasks;

namespace PersonManagement.Application.Persons.Queries.GetPersonById
{
    public class GetPersonByIdQueryHandler : IRequestHandler<GetPersonByIdQuery, GetPersonByIdResponse>
    {
        private readonly IPersonRepository _personRepository;
        private readonly ICacheService _cacheService;

        private const int cacheExpiryInMinutes = 10;

        public GetPersonByIdQueryHandler(IPersonRepository personRepository, ICacheService cacheService)
        {
            _personRepository = personRepository;
            _cacheService = cacheService;
        }

        public async Task<GetPersonByIdResponse> Handle(GetPersonByIdQuery request, CancellationToken cancellationToken)
        {
            var cacheKey = CacheConstants.GetPersonCacheKey(request.Id);

            var cachedResult = await _cacheService.GetAsync<GetPersonByIdResponse>(cacheKey);
            if (cachedResult != null)
                return cachedResult;

            var person = await _personRepository.GetByIdNoTrackingAsync(
                id: request.Id,
                cancellationToken: cancellationToken,
                includeRelatedPersons: true,
                includePhoneNumbers: true) ?? throw new PersonNotFoundException(request.Id);

            var response = new GetPersonByIdResponse
            {
                Id = person.Id,
                FirstName = person.FirstName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth,
                Gender = person.Gender,
                PersonalNumber = person.PersonalNumber,
                PhoneNumbers = person.PhoneNumbers
                        .Select(p => new PhoneNumberResponse
                        {
                            Id = p.Id,
                            Number = p.Number,
                            PhoneNumberType = p.PhoneNumberType
                        }).ToList(),
                RelatedPersons = person.RelatedPersons
                    .Select(rp => new RelatedPersonResponse
                    {
                        Id = rp.RelatedToPerson.Id,
                        FirstName = rp.RelatedToPerson.FirstName,
                        LastName = rp.RelatedToPerson.LastName,
                        DateOfBirth = rp.RelatedToPerson.DateOfBirth,
                        Gender = rp.RelatedToPerson.Gender,
                        PersonalNumber = rp.RelatedToPerson.PersonalNumber,
                        PhoneNumbers = rp.RelatedToPerson.PhoneNumbers
                                        .Select(pn => new PhoneNumberResponse
                                        {
                                            Id = pn.Id,
                                            Number = pn.Number,
                                            PhoneNumberType = pn.PhoneNumberType
                                        }).ToList()
                    }).ToList()
            };
            
            await _cacheService.SetAsync(cacheKey, response, TimeSpan.FromMinutes(cacheExpiryInMinutes));
            return response;
        }
    }
}