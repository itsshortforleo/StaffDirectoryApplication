using System.Data.Entity;
using Source.Classes.Entities;

namespace Source.Classes.DataRetrieval
{
    public class SdmDbContext : DbContext
    {
        public virtual DbSet<Entity> Entities { get; set; }
        public virtual DbSet<EntityType> EntityTypes { get; set; }
        public virtual DbSet<Name> Names { get; set; }
        public virtual DbSet<EntityIdentifier> EntityIdentifiers { get; set; }
        public virtual DbSet<IdentifierType> IdentifierTypes { get; set; }
        public virtual DbSet<Phone> Phones { get; set; }
        public virtual DbSet<PhoneType> PhoneTypes { get; set; }
        public virtual DbSet<Picture> Pictures { get; set; }
        public virtual DbSet<EmployeeClassification> EmployeeClassifications { get; set; }
        public virtual DbSet<JobTitle> JobTitles { get; set; }
        public virtual DbSet<GridLocation> GridLocations { get; set; }
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<AccessDoor> AccessDoors { get; set; }
        public virtual DbSet<Floor> Floors { get; set; }
        public virtual DbSet<Email> Emails { get; set; }
        public virtual DbSet<Division> Divisions { get; set; }
        public virtual DbSet<Unit> Units { get; set; }
        public virtual DbSet<EntitySubtype> EntitySubtypes { get; set; }
        public virtual DbSet<EmailType> EmailTypes { get; set; }
    }
}