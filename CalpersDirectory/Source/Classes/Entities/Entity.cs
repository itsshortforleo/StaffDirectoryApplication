using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Source.Classes.Entities
{
    public class Entity
    {
        [Key]
        public int EntityKey { get; set; }

        public int EntityTypeKey { get; set; }
        public int? EntitySubtypeKey { get; set; }

        public int? PictureKey { get; set; }
        public int? EmployeeClassificationKey { get; set; }
        public int? GridLocationKey { get; set; }
        public int? AccessDoorKey { get; set; }
        public int? JobTitleKey { get; set; }
        public int? UnitKey { get; set; }
        public DateTime EffectiveBeginDt { get; set; }
        public DateTime? EffectiveEndDt { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedDt { get; set; }
        public bool IsEntityCompletelyPrivate { get; set; }

        public virtual ICollection<Name> Names { get; set; }
        public virtual ICollection<Phone> Phones { get; set; }
        public virtual EntityType EntityType { get; set; }
        public virtual EntitySubtype EntitySubtype { get; set; }
        public virtual Picture Picture { get; set; }
        public virtual EmployeeClassification EmployeeClassification { get; set; }
        public virtual ICollection<Email> Emails { get; set; }
        public virtual ICollection<EntityIdentifier> EntityIdentifiers { get; set; }
        public virtual JobTitle JobTitle { get; set; }
        public virtual GridLocation GridLocation { get; set; }
        public virtual AccessDoor AccessDoor { get; set; }
        public virtual Unit Unit { get; set; }
    }
}