using System;
using System.Runtime.Serialization;
using Source.Classes.Entities;

namespace Source.Classes.DTOs
{
    [Serializable, DataContract]
    public class ActiveDirectoryDto : DataTransferObjectBase
    {
        /// <summary>
        ///     Gets or sets the refund daily threshold key.
        /// </summary>
        /// <value>
        ///     The refund daily threshold key.
        /// </value>
        [DataMember]
        public int Key { get; set; }

        [DataMember]
        public EntityIdentifier Hrmsid { get; set; }

        [DataMember]
        public Name Name { get; set; }

        [DataMember]
        public String Photo { get; set; }

        [DataMember]
        public EmployeeClassification EmployeeClassification { get; set; }

        [DataMember]
        public JobTitle JobTitle { get; set; }

        [DataMember]
        public Phone DeskPhone { get; set; }

        [DataMember]
        public Phone MobilePhone { get; set; }

        [DataMember]
        public GridLocation GridLocation { get; set; }

        [DataMember]
        public Building Building { get; set; }

        [DataMember]
        public Floor Floor { get; set; }

        [DataMember]
        public Email Email { get; set; }

        [DataMember]
        public AccessDoor AccessDoor { get; set; }

        [DataMember]
        public Division Division { get; set; }

        [DataMember]
        public Unit Unit { get; set; }

        [DataMember]
        public EntityIdentifier ManagerId { get; set; }

    }
}