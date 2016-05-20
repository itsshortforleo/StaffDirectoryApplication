using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using Source.Classes.Entities;

namespace Source.Classes.DataRetrieval
{
    public class EntityDataAction
    {
        public List<Entity> GetEntities()
        {
            SdmDbContext entityDbContext = new SdmDbContext();
            return entityDbContext.Entities.ToList();
        }

        public DataTable ConvertEntitiesForDisplay()
        {
            List<Entity> entities = GetEntities();
            DataTable dt = new DataTable();
            dt.Columns.Add("Key");
            dt.Columns.Add("Name");
            foreach (Entity entity in entities)
            {
                DataRow dr = dt.NewRow();
                dr["Key"] = entity.EntityKey;

                String name = string.Empty;
                name = entity.Names.FirstOrDefault().FirstName + " " + entity.Names.FirstOrDefault().LastName;
                dr["Name"] = name;

                dt.Rows.Add(dr);
            }
            return dt;
        }

        //public DataTable SearchEntities(String filter)
        //{
        //    var keywords = new String[] { "Car", "Yellow" }.ToList();
        //    List<Entity> entities = GetEntities();
        //    DataTable dt = new DataTable();

        //    var p = db.Posts.Where(q => keywords.Any(k => q.Title.Contains(k)));


        //    var p = db.Posts.Where(q => keywords.Any(k => q.Title.Contains(k)));

        //}

        public DataTable SearchEntitiesByParamenter(String sqlTextParameter)
        {
            string constr = ConfigurationManager.ConnectionStrings["SdmDbContext"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.CommandText = "SELECT Entities.EntityKey, Names.FirstName, Names.LastName FROM Entities join Names on entities.EntityKey = names.EntityKey WHERE Names.FirstName LIKE '%' + @Name +  '%' OR Names.LastName LIKE '%' + @Name +  '%'";

                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@Name", sqlTextParameter.Trim());
                    DataTable dt = new DataTable();
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);

                        return dt;
                        //gvStaffSearch.DataSource = dt;
                        //gvStaffSearch.DataBind();
                    }
                }
            }
        }

        public Entity GetEntityDetailsByKey(int entityKey)
        {
            var entityDbContext = new SdmDbContext();
            // Query for all entities that match the passed entityKey                
            IQueryable<Entity> entity = from e in entityDbContext.Entities
                where e.EntityKey.Equals(entityKey)
                select e;
            var singleEntity = entity.SingleOrDefault();

            return singleEntity;


        }
    }
}