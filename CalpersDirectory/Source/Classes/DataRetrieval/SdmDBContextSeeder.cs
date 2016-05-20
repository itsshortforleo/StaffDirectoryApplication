using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic.FileIO;
using Source.Classes.Entities;
using Source.Framework;

namespace Source.Classes.DataRetrieval
{
    public class SdmDbContextSeeder : DropCreateDatabaseIfModelChanges<SdmDbContext>
    {
        protected override void Seed(SdmDbContext context)
        {
            String path = HttpContext.Current.Server.MapPath(Constants.DATA_FROM_CSV);
            DataTable dt = ReadCsvAndConvertToDataTable(path);
            DataColumnCollection columns = dt.Columns;



            foreach (DataRow row in dt.Rows)
            {
                // Save Reference Datatables first. Which ones are they?
                // EmployeeClassification, JobTitle, Building, Division, Unit, etc.
                SaveRefDataToDb(columns, row);

                var entity = SaveEntityDataToDb(columns, row);

                // Is this entity an employee or manager
                entity = SetEntitySubtypeForEntity(dt, entity);

                context.Entities.Add(entity);

            }
            base.Seed(context);
        }

        // Check if entity is manager
        private Entity SetEntitySubtypeForEntity(DataTable dt, Entity entity)
        {
            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {
                    // Get code for manager subtype
                    String managerEntitySubtypeCode = "MANAGER";
                    var managerEntitySubtypeObject = dbc.EntitySubtypes.Where(x => x.Code == managerEntitySubtypeCode);
                    var filteredManagerEntitySubtype = managerEntitySubtypeObject.ToArray().First();

                    // Get code for employee subtype
                    String employeeEntitySubtypeCode = "EMPLOYEE";
                    var employeeEntitySubtypeObject = dbc.EntitySubtypes.Where(x => x.Code == employeeEntitySubtypeCode);
                    var filteredEmployeeEntitySubtype = employeeEntitySubtypeObject.ToArray().First();

                    // Get code for managerid identifiertype
                    String managerIdentifierTypeCode = "HRMSID";
                    var hrmsIdIdentifierTypeObject = dbc.IdentifierTypes.Where(x => x.Code == managerIdentifierTypeCode);
                    var filteredHrmsIdIdentifierType = hrmsIdIdentifierTypeObject.ToArray().First();

                    var entityHrmsIdValue =
                        entity.EntityIdentifiers.Where(
                            x => x.IdentifierTypeKey == filteredHrmsIdIdentifierType.IdentifierTypeKey);
                    var filteredEntityHrmsIdValue = entityHrmsIdValue.ToArray().First();

                    foreach (DataRow row in dt.Rows)
                    {
                        String currentManagerId = row.Field<String>("manager_ID");

                        // if from the list of all manager ids there exists one that equals your own ID, then you're a manager
                        if (currentManagerId.Equals(filteredEntityHrmsIdValue.IdentifierValue))
                        {
                            entity.EntitySubtypeKey = filteredManagerEntitySubtype.EntitySubtypeKey;
                        }
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
            return entity;
        }

        private Entity SaveEntityDataToDb(DataColumnCollection columns, DataRow row)
        {
            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {
                    String rowEmpClassCode = row.Field<String>("empl_classification");
                    var empClassObject = dbc.EmployeeClassifications.Where(x => x.Code == rowEmpClassCode);
                    var filteredEmpClass = empClassObject.ToArray().First();

                    String rowGridLocValue = row.Field<String>("grid_location");
                    var gridLocObject = dbc.GridLocations.Where(x => x.GridLocationValue == rowGridLocValue);
                    var filteredGridLoc = gridLocObject.ToArray().First();

                    String rowAccessDoorCode = row.Field<String>("access_door");
                    var filteredAccessDoor = new AccessDoor();
                    if (!String.IsNullOrEmpty(rowAccessDoorCode))
                    {
                        var accessDoorObject = dbc.AccessDoors.Where(x => x.Code == rowAccessDoorCode);
                        filteredAccessDoor = accessDoorObject.ToArray().First();
                    }

                    String rowJobTitleCode = row.Field<String>("job_title");
                    var jobTitleObject = dbc.JobTitles.Where(x => x.Code == rowJobTitleCode);
                    var filteredJobTitle = jobTitleObject.ToArray().First();

                    String rowUnitValue = row.Field<String>("unit");
                    var unitObject = dbc.Units.Where(x => x.UnitValue == rowUnitValue);
                    var filteredUnit = unitObject.ToArray().First();

                    String deskPhoneCode = "DESKPHN";
                    var deskPhoneTypeObject = dbc.PhoneTypes.Where(x => x.Code == deskPhoneCode);
                    var filteredDeskPhoneType = deskPhoneTypeObject.ToArray().First();

                    String mobilePhoneCode = "MOBILEPHN";
                    var mobilePhoneTypeObject = dbc.PhoneTypes.Where(x => x.Code == mobilePhoneCode);
                    var filteredMobilePhoneType = mobilePhoneTypeObject.ToArray().First();

                    String completeDeskPhoneNumber = row.Field<String>("desk_phone");
                    String completeMobilePhoneNumber = row.Field<String>("mobile_phone");

                    String deskPhoneAreaCode = string.Empty;
                    String deskPhoneNumber = string.Empty;
                    String mobilePhoneNumber = string.Empty;
                    String mobilePhoneAreaCode = string.Empty;
                    if (completeDeskPhoneNumber.Length == 11)
                    {
                        deskPhoneAreaCode = completeDeskPhoneNumber.Substring(1, 3);
                        deskPhoneNumber = completeDeskPhoneNumber.Substring(4, 7);
                    }
                    if (completeMobilePhoneNumber.Length == 11)
                    {
                        mobilePhoneAreaCode = completeMobilePhoneNumber.Substring(1, 3);
                        mobilePhoneNumber = completeMobilePhoneNumber.Substring(4, 7);
                    }
                    String entityTypeCode = "INDIVIDUAL";
                    var entityTypeObject = dbc.EntityTypes.Where(x => x.Code == entityTypeCode);
                    var filteredEntityType = entityTypeObject.ToArray().First();

                    String emailTypeColumnName = "email";
                    var emailTypeObject = dbc.EmailTypes.Where(x => x.Code == emailTypeColumnName);
                    var filteredEmailType = emailTypeObject.ToArray().First();

                    String hrmsidCode = "HRMSID";
                    var hrmsidIdentifierTypeObject = dbc.IdentifierTypes.Where(x => x.Code == hrmsidCode);
                    var filteredHrmsidIdentifierType = hrmsidIdentifierTypeObject.ToArray().First();

                    String manageridCode = "MANAGERID";
                    var manageridIdentifierTypeObject = dbc.IdentifierTypes.Where(x => x.Code == manageridCode);
                    var filteredManageridIdentifierType = manageridIdentifierTypeObject.ToArray().First();

                    // Get code for employee subtype
                    String employeeEntitySubtypeCode = "EMPLOYEE";
                    var employeeEntitySubtypeObject = dbc.EntitySubtypes.Where(x => x.Code == employeeEntitySubtypeCode);
                    var filteredEmployeeEntitySubtype = employeeEntitySubtypeObject.ToArray().First();

                    Entity entity = new Entity()
                    { 
                        EntityTypeKey = filteredEntityType.EntityTypeKey,
                        EntitySubtypeKey = filteredEmployeeEntitySubtype.EntitySubtypeKey,
                        EmployeeClassificationKey = filteredEmpClass.EmployeeClassificationKey,
                        GridLocationKey = filteredGridLoc.GridLocationKey,
                        JobTitleKey = filteredJobTitle.JobTitleKey,
                        UnitKey = filteredUnit.UnitKey,
                        EffectiveBeginDt = DateTime.Now,
                        EffectiveEndDt = null,
                        CreatedBy = 0,
                        CreatedDt = DateTime.Now,
                        UpdatedBy = 0,
                        UpdatedDt = DateTime.Now,
                        Names = new List<Name>()
                        {
                            new Name()
                            {
                                FirstName = row.Field<String>("first_name"),
                                LastName = row.Field<String>("last_name"),
                                CreatedBy = 0,
                                CreatedDt = DateTime.Now,
                                UpdatedBy = 0,
                                UpdatedDt = DateTime.Now
                            }
                        },
                        Phones = new List<Phone>()
                        {
                            new Phone()
                            {
                                PhoneTypeKey = filteredDeskPhoneType.PhoneTypeKey,
                                AreaCode = deskPhoneAreaCode,
                                PhoneNumber = deskPhoneNumber,
                                ForeignFlag = false,
                                CreatedBy = 0,
                                CreatedDt = DateTime.Now,
                                UpdatedBy = 0,
                                UpdatedDt = DateTime.Now
                            },
                            new Phone()
                            {
                                PhoneTypeKey = filteredMobilePhoneType.PhoneTypeKey,
                                AreaCode = mobilePhoneAreaCode,
                                PhoneNumber = mobilePhoneNumber,
                                ForeignFlag = false,
                                CreatedBy = 0,
                                CreatedDt = DateTime.Now,
                                UpdatedBy = 0,
                                UpdatedDt = DateTime.Now
                            }
                        },
                        Emails = new List<Email>()
                        {
                            new Email()
                            {
                                EmailTypeKey = filteredEmailType.EmailTypeKey,
                                EmailAddress = row.Field<String>("email"),
                                CreatedBy = 0,
                                CreatedDt = DateTime.Now,
                                UpdatedBy = 0,
                                UpdatedDt = DateTime.Now
                            }
                        },
                        EntityIdentifiers = new List<EntityIdentifier>()
                        {
                            new EntityIdentifier()
                            {
                                IdentifierTypeKey = filteredHrmsidIdentifierType.IdentifierTypeKey,
                                IdentifierValue = row.Field<String>("HRMS_ID"),
                                CreatedBy = 0,
                                CreatedDt = DateTime.Now,
                                UpdatedBy = 0,
                                UpdatedDt = DateTime.Now
                            },
                            new EntityIdentifier()
                            {
                                IdentifierTypeKey = filteredManageridIdentifierType.IdentifierTypeKey,
                                IdentifierValue = row.Field<String>("manager_ID"),
                                CreatedBy = 0,
                                CreatedDt = DateTime.Now,
                                UpdatedBy = 0,
                                UpdatedDt = DateTime.Now
                            }
                        }
                    };
                    if (!String.IsNullOrEmpty(rowAccessDoorCode))
                    {
                        entity.AccessDoor.AccessDoorKey = filteredAccessDoor.AccessDoorKey;
                    }
                    return entity;
                };
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }         
        }

        private static void SaveRefDataToDb(DataColumnCollection columns, DataRow row)
        {
            SaveEntityTypesAndSubtypes();

            if (columns.Contains("empl_classification"))
            {
                AddEmployeeClassificationRefDataToDb(row);
            }
            if (columns.Contains("job_title"))
            {
                AddJobTitleRefDataToDb(row);
            }
            if (columns.Contains("building"))
            {
                AddBuildingRefDataToDb(row);
            }
            if (columns.Contains("floor"))
            {
                AddFloorRefDataToDb(row);
            }
            if (columns.Contains("division"))
            {
                AddDivisionRefDataToDb(row);
            }
            if (columns.Contains("unit"))
            {
                AddUnitRefDataToDb(row);
            }
            if (columns.Contains("desk_phone"))
            {
                AddDeskPhoneRefDataToDb();
            }
            if (columns.Contains("mobile_phone"))
            {
                AddMobilePhoneRefDataToDb();
            }
            if (columns.Contains("HRMS_ID"))
            {
                AddHrmsidRefDataToDb();
            }
            if (columns.Contains("manager_ID"))
            {
                AddManagerIdRefDataToDb();
            }
            if (columns.Contains("access_door"))
            {
                AddAccessDoorRefDataToDb(row);
            }
            if (columns.Contains("email"))
            {
                AddEmailTypeRefDataToDb();
            }
            if (columns.Contains("grid_location"))
            {
                AddGridLocationRefDataToDb(row);
            }
        }

        private static void SaveEntityTypesAndSubtypes()
        {


            EntityType indEntityType = new EntityType()
            {
                Code = "INDIVIDUAL",
                DescShort = "Individual",
                DescLong = "Individual",
                ActiveFlag = true,
                CreatedDt = DateTime.Now,
                CreatedBy = 0,
                UpdatedDt = DateTime.Now,
                UpdatedBy = 0,
                EntitySubtypes = new List<EntitySubtype>()
                {
                    new EntitySubtype()
                    {
                        Code = "EMPLOYEE",
                        DescShort = "Employee",
                        DescLong = "Employee",
                        ActiveFlag = true,
                        CreatedDt = DateTime.Now,
                        CreatedBy = 0,
                        UpdatedDt = DateTime.Now,
                        UpdatedBy = 0
                    },
                    new EntitySubtype()
                    {
                        Code = "MANAGER",
                        DescShort = "Manager",
                        DescLong = "Manager",
                        ActiveFlag = true,
                        CreatedDt = DateTime.Now,
                        CreatedBy = 0,
                        UpdatedDt = DateTime.Now,
                        UpdatedBy = 0
                    }
                }
            };
            EntityType resEntityType = new EntityType()
            {
                Code = "RESOURCE",
                DescShort = "Resource",
                DescLong = "Resource",
                ActiveFlag = true,
                CreatedDt = DateTime.Now,
                CreatedBy = 0,
                UpdatedDt = DateTime.Now,
                UpdatedBy = 0,
                EntitySubtypes = new List<EntitySubtype>()
                {
                    new EntitySubtype()
                    {
                        Code = "CONFROOM",
                        DescShort = "Conference Room",
                        DescLong = "Conference Room",
                        ActiveFlag = true,
                        CreatedDt = DateTime.Now,
                        CreatedBy = 0,
                        UpdatedDt = DateTime.Now,
                        UpdatedBy = 0
                    },
                    new EntitySubtype()
                    {
                        Code = "RESTROOM",
                        DescShort = "Rest Room",
                        DescLong = "Rest Room",
                        ActiveFlag = true,
                        CreatedDt = DateTime.Now,
                        CreatedBy = 0,
                        UpdatedDt = DateTime.Now,
                        UpdatedBy = 0
                    }
                }
            };
        
           
            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {
                    // if this record already exists in db, dont save it (this is a reference table)
                    if (dbc.EntityTypes.Any(o => o.Code == indEntityType.Code))
                    {
                        //match! todo: fix this if else
                    }
                    else
                    {
                        dbc.EntityTypes.Add(indEntityType);
                        dbc.SaveChanges();
                    }
                    if (dbc.EntityTypes.Any(o => o.Code == resEntityType.Code))
                    {
                        //match! todo: fix this if else
                    }
                    else
                    {
                        dbc.EntityTypes.Add(resEntityType);
                        dbc.SaveChanges();
                    }

                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private static void AddEmployeeClassificationRefDataToDb(DataRow row)
        {
            EmployeeClassification ec = new EmployeeClassification()
            {
                Code = row.Field<String>("empl_classification"),
                DescShort = row.Field<String>("empl_classification"),
                DescLong = row.Field<String>("empl_classification"),
                ActiveFlag = true,
                CreatedDt = DateTime.Now,
                CreatedBy = 0,
                UpdatedDt = DateTime.Now,
                UpdatedBy = 0
            };
            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {
                    // if this record already exists in db, dont save it (this is a reference table)
                    if (dbc.EmployeeClassifications.Any(o => o.Code == ec.Code))
                    {
                        //match! todo: fix this if else
                    }
                    else
                    {
                        dbc.EmployeeClassifications.Add(ec);
                        dbc.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private static void AddJobTitleRefDataToDb(DataRow row)
        {
            JobTitle jt = new JobTitle()
            {
                Code = row.Field<String>("job_title"),
                DescShort = row.Field<String>("job_title"),
                DescLong = row.Field<String>("job_title"),
                ActiveFlag = true,
                CreatedDt = DateTime.Now,
                CreatedBy = 0,
                UpdatedDt = DateTime.Now,
                UpdatedBy = 0
            };

            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {
                    // if this record already exists in db, dont save it (this is a reference table)
                    if (dbc.JobTitles.Any(o => o.Code == jt.Code))
                    {
                        //match! todo: fix this if else
                    }
                    else
                    {
                        dbc.JobTitles.Add(jt);
                        dbc.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private static void AddBuildingRefDataToDb(DataRow row)
        {
            Building bd = new Building()
            {
                Code = row.Field<String>("building"),
                DescShort = row.Field<String>("building"),
                DescLong = row.Field<String>("building"),
                ActiveFlag = true,
                CreatedDt = DateTime.Now,
                CreatedBy = 0,
                UpdatedDt = DateTime.Now,
                UpdatedBy = 0
            };

            if (bd.Code.Equals("LPW"))
            {
                bd.DescShort = "Lincoln Plaza West";
                bd.DescLong = "Lincoln Plaza West";
            }
            else if (bd.Code.Equals("LPE"))
            {
                bd.DescShort = "Lincoln Plaza East";
                bd.DescLong = "Lincoln Plaza East";
            }
            else if (bd.Code.Equals("LPN"))
            {
                bd.DescShort = "Lincoln Plaza North";
                bd.DescLong = "Lincoln Plaza North";
            }
            else if (bd.Code.Equals("LPS"))
            {
                bd.DescShort = "Lincoln Plaza South";
                bd.DescLong = "Lincoln Plaza South";
            }

            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {
                    // if this record already exists in db, dont save it (this is a reference table)
                    if (dbc.Buildings.Any(o => o.Code == bd.Code))
                    {
                        //match! todo: fix this if else
                    }
                    else
                    {
                        dbc.Buildings.Add(bd);
                        dbc.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private static void AddFloorRefDataToDb(DataRow row)
        {
            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {
                    String rowBuildingCode = row.Field<String>("building");
                    var buildingObject = dbc.Buildings.Where(x => x.Code == rowBuildingCode);
                    var filteredBuilding = buildingObject.ToArray().First();

                    Floor flr = new Floor()
                    {
                        FloorValue = row.Field<String>("floor"),
                        DescShort = row.Field<String>("floor"),
                        DescLong = row.Field<String>("floor"),
                        CreatedDt = DateTime.Now,
                        CreatedBy = 0,
                        UpdatedDt = DateTime.Now,
                        UpdatedBy = 0,
                        BuildingKey = filteredBuilding.BuildingKey
                    };

                    // if this record already exists in db, dont save it (this is a reference table)
                    if (dbc.Floors.Any(o => o.FloorValue == flr.FloorValue))
                    {
                        //match! todo: fix this if else
                    }
                    else
                    {
                        dbc.Floors.Add(flr);
                        dbc.SaveChanges();
                    }
                };
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private static void AddDivisionRefDataToDb(DataRow row)
        {
            Division dv = new Division()
            {

                Code = row.Field<String>("division"),
                DescShort = row.Field<String>("division"),
                DescLong = row.Field<String>("division"),
                ActiveFlag = true,
                CreatedDt = DateTime.Now,
                CreatedBy = 0,
                UpdatedDt = DateTime.Now,
                UpdatedBy = 0
            };

            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {
                    // if this record already exists in db, dont save it (this is a reference table)
                    if (dbc.Divisions.Any(o => o.Code == dv.Code))
                    {
                        //match! todo: fix this if else
                    }
                    else
                    {
                        dbc.Divisions.Add(dv);
                        dbc.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private static void AddAccessDoorRefDataToDb(DataRow row)
        {
            String acdCode = row.Field<String>("access_door");
            if (String.IsNullOrEmpty(acdCode))
            {
                return;
            }
            AccessDoor acd = new AccessDoor()
            {

                Code = acdCode,
                DescShort = acdCode,
                DescLong = acdCode,
                ActiveFlag = true,
                CreatedDt = DateTime.Now,
                CreatedBy = 0,
                UpdatedDt = DateTime.Now,
                UpdatedBy = 0
            };

            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {
                    // if this record already exists in db, dont save it (this is a reference table)
                    if (dbc.AccessDoors.Any(o => o.Code == acd.Code))
                    {
                        //match! todo: fix this if else
                    }
                    else
                    {
                        dbc.AccessDoors.Add(acd);
                        dbc.SaveChanges();
                    }
                }
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private static void AddUnitRefDataToDb(DataRow row)
        {
            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {
                    String rowDivisionCode = row.Field<String>("division");
                    var divisionObject = dbc.Divisions.Where(x => x.Code == rowDivisionCode);
                    var filteredDivision = divisionObject.ToArray().First();

                    Unit un = new Unit()
                    {
                        UnitValue = row.Field<String>("unit"),
                        DescShort = row.Field<String>("unit"),
                        DescLong = row.Field<String>("unit"),
                        CreatedDt = DateTime.Now,
                        CreatedBy = 0,
                        UpdatedDt = DateTime.Now,
                        UpdatedBy = 0,
                        DivisionKey = filteredDivision.DivisionKey
                    };

                    // if this record already exists in db, dont save it (this is a reference table)
                    if (dbc.Units.Any(o => o.UnitValue == un.UnitValue))
                    {
                        //match! todo: fix this if else
                    }
                    else
                    {
                        dbc.Units.Add(un);
                        dbc.SaveChanges();
                    }
                };
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private static void AddGridLocationRefDataToDb(DataRow row)
        {
            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {
                    String rowBuildingCode = row.Field<String>("building");

                    var floorObject = dbc.Floors.Where(x => x.Building.Code == rowBuildingCode);
                    var filteredFloor = floorObject.ToArray().First();

                    GridLocation grl = new GridLocation()
                    {
                        GridLocationValue = row.Field<String>("grid_location"),
                        CreatedDt = DateTime.Now,
                        CreatedBy = 0,
                        UpdatedDt = DateTime.Now,
                        UpdatedBy = 0,
                        Floor = filteredFloor
                    };

                    // if this record already exists in db, dont save it (this is a reference table)
                    if (dbc.GridLocations.Any(o => o.GridLocationValue == grl.GridLocationValue))
                    {
                        //match! todo: fix this if else
                    }
                    else
                    {
                        dbc.GridLocations.Add(grl);
                        dbc.SaveChanges();
                    }
                };
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private static void AddDeskPhoneRefDataToDb()
        {
            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {
                    PhoneType pht = new PhoneType()
                    {
                        Code = "DESKPHN",
                        DescShort = "Desk Phone",
                        DescLong = "Desk Phone",
                        ActiveFlag = true,
                        CreatedDt = DateTime.Now,
                        CreatedBy = 0,
                        UpdatedDt = DateTime.Now,
                        UpdatedBy = 0
                    };

                    // if this record already exists in db, dont save it (this is a reference table)
                    if (dbc.PhoneTypes.Any(o => o.Code == pht.Code))
                    {
                        //match! todo: fix this if else
                    }
                    else
                    {
                        dbc.PhoneTypes.Add(pht);
                        dbc.SaveChanges();
                    }
                };
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private static void AddMobilePhoneRefDataToDb()
        {
            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {
                    PhoneType pht = new PhoneType()
                    {
                        Code = "MOBILEPHN",
                        DescShort = "Mobile Phone",
                        DescLong = "Mobile Phone",
                        ActiveFlag = true,
                        CreatedDt = DateTime.Now,
                        CreatedBy = 0,
                        UpdatedDt = DateTime.Now,
                        UpdatedBy = 0
                    };

                    // if this record already exists in db, dont save it (this is a reference table)
                    if (dbc.PhoneTypes.Any(o => o.Code == pht.Code))
                    {
                        //match! todo: fix this if else
                    }
                    else
                    {
                        dbc.PhoneTypes.Add(pht);
                        dbc.SaveChanges();
                    }
                };
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private static void AddHrmsidRefDataToDb()
        {
            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {
                    IdentifierType hrms = new IdentifierType()
                    {
                        Code = "HRMSID",
                        DescShort = "HRMS ID",
                        DescLong = "HRMS ID",
                        ActiveFlag = true,
                        CreatedDt = DateTime.Now,
                        CreatedBy = 0,
                        UpdatedDt = DateTime.Now,
                        UpdatedBy = 0
                    };

                    // if this record already exists in db, dont save it (this is a reference table)
                    if (dbc.IdentifierTypes.Any(o => o.Code == hrms.Code))
                    {
                        //match! todo: fix this if else
                    }
                    else
                    {
                        dbc.IdentifierTypes.Add(hrms);
                        dbc.SaveChanges();
                    }
                };
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private static void AddManagerIdRefDataToDb()
        {
            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {
                    IdentifierType mngrid = new IdentifierType()
                    {
                        Code = "MANAGERID",
                        DescShort = "Manager ID",
                        DescLong = "Manager ID",
                        ActiveFlag = true,
                        CreatedDt = DateTime.Now,
                        CreatedBy = 0,
                        UpdatedDt = DateTime.Now,
                        UpdatedBy = 0
                    };

                    // if this record already exists in db, dont save it (this is a reference table)
                    if (dbc.IdentifierTypes.Any(o => o.Code == mngrid.Code))
                    {
                        //match! todo: fix this if else
                    }
                    else
                    {
                        dbc.IdentifierTypes.Add(mngrid);
                        dbc.SaveChanges();
                    }
                };
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        private static void AddEmailTypeRefDataToDb()
        {
            try
            {
                using (SdmDbContext dbc = new SdmDbContext())
                {

                    EmailType emt = new EmailType()
                    {
                        Code = "email",
                        DescShort = "Office Email",
                        DescLong = "Office Email",
                        ActiveFlag = true,
                        CreatedDt = DateTime.Now,
                        CreatedBy = 0,
                        UpdatedDt = DateTime.Now,
                        UpdatedBy = 0
                    };

                    // if this record already exists in db, dont save it (this is a reference table)
                    if (dbc.EmailTypes.Any(o => o.Code == emt.Code))
                    {
                        //match! todo: fix this if else
                    }
                    else
                    {
                        dbc.EmailTypes.Add(emt);
                        dbc.SaveChanges();
                    }
                };
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Value: \"{1}\", Error: \"{2}\"",
                            ve.PropertyName,
                            eve.Entry.CurrentValues.GetValue<object>(ve.PropertyName),
                            ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        public static DataTable ReadCsvAndConvertToDataTable(String path)
        {
            
            StreamReader oStreamReader = new StreamReader(path);
            DataTable oDataTable = new DataTable();
            int rowCount = 0;
            String[] columnNames = null;
            String[] oStreamDataValues = null;

            while (!oStreamReader.EndOfStream)
            {
                string oStreamRowData = oStreamReader.ReadLine().Trim();

                if (oStreamRowData.Length > 0)
                {
                    oStreamDataValues = oStreamRowData.Split(',');
                    if (rowCount == 0)
                    {
                        rowCount = 1;
                        columnNames = oStreamDataValues;
                        foreach (String csvHeader in columnNames)
                        {
                            DataColumn oDataColumn = new DataColumn(csvHeader.ToUpper(),
                                typeof(String));
                            oDataColumn.DefaultValue = String.Empty;
                            oDataTable.Columns.Add(oDataColumn);
                        }
                    }
                    else
                    {
                        DataRow oDataRow = oDataTable.NewRow();
                        for (int i = 0; i < columnNames.Length; i++)
                        {
                            oDataRow[columnNames[i]] = oStreamDataValues[i] == null
                                ? String.Empty
                                : oStreamDataValues[i].ToString();
                        }
                        oDataTable.Rows.Add(oDataRow);
                    }
                }
            }
            oStreamReader.Close();
            oStreamReader.Dispose();
            return oDataTable;
        }
    }
}