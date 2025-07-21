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

        protected RelatedPerson() { } //For EF

        public RelatedPerson(int personId, int relatedToPersonId, RelationType relationType)
        {
            PersonId = personId;
            RelatedToPersonId = relatedToPersonId;
            RelationType = relationType;
        }
    }
}
