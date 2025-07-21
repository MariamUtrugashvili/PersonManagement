using PersonManagement.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonManagement.Domain.Entities
{
    public class RelatedPerson : BaseEntity
    {
        public int PersonId { get; private set; }
        public int RelatedToPersonId { get; private set; }
        public RelationType RelationType { get; private set; }

        protected RelatedPerson() { } // EF Core

        private RelatedPerson(int personId, int relatedToPersonId, RelationType relationType)
        {
            if (personId == relatedToPersonId)
                throw new ArgumentException("Person cannot be related to themselves.");

            PersonId = personId;
            RelatedToPersonId = relatedToPersonId;
            RelationType = relationType;
        }

        public static RelatedPerson Create(int personId, int relatedToPersonId, RelationType relationType)
        {
            return new RelatedPerson(personId, relatedToPersonId, relationType);
        }

        public void ChangeRelationType(RelationType newRelationType)
        {
            RelationType = newRelationType;
            SetUpdated();
        }
    }
}
