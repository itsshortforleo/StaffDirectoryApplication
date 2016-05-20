using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using Source.Classes.DataRetrieval;
using Source.Classes.Entities;

namespace Source.Pages
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //EntityDataAction eda = new EntityDataAction();
            //DataTable entityList = eda.ConvertEntitiesForDisplay();
            //GridView1.DataSource = entityList;
            //GridView1.DataBind();

            //private DataTable ConvertEntitiesForDisplay()
            if (!this.IsPostBack)
            {
                this.BindGrid();
            }
        }

        protected void Search(object sender, EventArgs e)
        {
            this.BindGrid();
        }

        private void BindGrid()
        {
            EntityDataAction eda = new EntityDataAction();
            DataTable entityList = eda.SearchEntitiesByParamenter(keyboard.Text);

            gvStaffSearch.DataSource = entityList;
            gvStaffSearch.DataBind();

     
        }

        protected void OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvStaffSearch.PageIndex = e.NewPageIndex;
            this.BindGrid();
        }

        protected void OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                e.Row.Cells[0].Text = Regex.Replace(e.Row.Cells[0].Text, keyboard.Text.Trim(), delegate (Match match)
                {
                    return string.Format("<span style = 'background-color:#D9EDF7'>{0}</span>", match.Value);
                }, RegexOptions.IgnoreCase);
            }
        }

        /// <summary>
        ///     Handles the Click event of the Edit Threshold hyperlink control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.EventArgs" /> instance containing the event data.</param>
        protected void lnkVisitProfile_Click(object sender, EventArgs e)
        {
            var entityKey = ((LinkButton)sender).CommandArgument;

            Response.Redirect("~/Pages/StaffProfile.aspx?EntityKey=" + entityKey);
        }
    }
}