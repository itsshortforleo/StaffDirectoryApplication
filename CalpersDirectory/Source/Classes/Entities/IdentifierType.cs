﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Source.Classes.Entities
{
    public class IdentifierType
    {
        [Key]
        public int IdentifierTypeKey { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(40)]
        public string Code { get; set; }
        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(40)]
        public string DescShort { get; set; }

        [Required]
        [Column(TypeName = "NVARCHAR")]
        [StringLength(256)]
        public string DescLong { get; set; }
        public bool ActiveFlag { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime CreatedDt { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime UpdatedDt { get; set; }

        public virtual ICollection<EntityIdentifier> EntityIdentifiers { get; set; }

    }
}