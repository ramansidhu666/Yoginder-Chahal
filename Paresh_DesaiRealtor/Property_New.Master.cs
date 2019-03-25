﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Property_cls;
using System.Data.SqlClient;
using System.Web.UI.HtmlControls;
using System.Configuration;

namespace Property
{
    public partial class Property_New : System.Web.UI.MasterPage
    {
        #region Global

        cls_Property clsobj = new cls_Property();

        #endregion Global
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindMenusList();
                string PgNam = "";
                System.Data.SqlClient.SqlConnection conn;
                conn = new System.Data.SqlClient.SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                conn.Open();
                System.Data.SqlClient.SqlCommand cmd;
                cmd = new System.Data.SqlClient.SqlCommand();
                cmd.Connection = conn;
                int pageid = 0;
                if (Session["pageid"] == null)
                    pageid = 17;
                else
                    pageid = Convert.ToInt32(Session["pageid"].ToString());
                cmd.CommandText = "select * from tbl_PageBlogs where id=" + pageid;
                System.Data.SqlClient.SqlDataReader dr;
                dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    HtmlMeta meta = new HtmlMeta();
                    meta.Name = "Meta Tag";
                    meta.Content = dr["MetaTag"].ToString();
                    Page.Header.Controls.Add(meta);
                    HtmlMeta meta1 = new HtmlMeta();
                    meta1.Name = "MetaDiscription";
                    meta1.Content = dr["MetaDiscription"].ToString();
                    Page.Header.Controls.Add(meta1);
                    Page.Title = dr["pagetitle"].ToString();
                    //PgNam = dr["PageTitle"].ToString();
                }
            }  
        }

        private void BindMenusList()
        {
            StringBuilder StrMenu = new StringBuilder();
            DataTable dt = new DataTable();
            DataTable dtSubmenu = new DataTable();
            dt = clsobj.GetMenuList();



            if (dt.Rows.Count > 0)
            {
                string PageName = dt.Rows[0]["PageName"].ToString();
                StrMenu.Append("<a class='toggleMenu' href='#'></a>");
                StrMenu.Append("<ul class='nav'>");
                StrMenu.Append("<li class='test' style='background:none;'><a href='../Default.aspx' title='Home' class='active'>Home</a></li>");
               
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    clsobj.PageID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    dtSubmenu = clsobj.GetSubMenuBy_PageID();
                    //check if it has submenu 
                    if (dtSubmenu.Rows.Count > 0)
                    {
                        StrMenu.Append("<li><a href=../Default.aspx>" + dt.Rows[i]["PageName"] + "</a>");//</li>
                        StrMenu.Append("<ul>");
                        for (int j = 0; j < dtSubmenu.Rows.Count; j++)
                        {
                            StrMenu.Append("<li><a href='../StaticPages.aspx?PageID=" + dtSubmenu.Rows[j]["id"] + "' title='" + dtSubmenu.Rows[j]["PageName"] + "'>" + dtSubmenu.Rows[j]["PageName"] + "</a> </li>");
                        }
                        StrMenu.Append("</ul>");
                        StrMenu.Append("</li>");
                    }
                    else
                    {
                        StrMenu.Append("<li><a href='../StaticPages.aspx?PageID=" + dt.Rows[i]["id"] + "' title='" + dt.Rows[i]["PageName"] + "'>" + dt.Rows[i]["PageName"] + "</a>");//</li>
                    }
                }
                DataTable exclusive_dt = clsobj.GetExclusiveListing();

                StrMenu.Append("<li><a href=#>My Listing</a>");//</li>
                if (exclusive_dt.Rows.Count > 0)
                {
                    StrMenu.Append("<ul >");

                    for (int j = 0; j < exclusive_dt.Rows.Count; j++)
                    {
                        StrMenu.Append("<li><a href='../ExclusiveDetailPage.aspx?Id=" + exclusive_dt.Rows[j]["Id"] + "' title='Home'>" + exclusive_dt.Rows[j]["Title"] + "</a></li>");
                    }
                    StrMenu.Append("</ul>");
                }
                StrMenu.Append("<li class='test' style='background:none;'><a href='Admin/Adminlogin.aspx' title='About Us'>Login</a></li>");

                //StrMenu.Append("<li>");
                //StrMenu.Append("<a href='../Calculators.aspx' title='Calculators'>Calculators</a>");
                //StrMenu.Append("</li>");

                StrMenu.Append("<li>");
                StrMenu.Append("<a href='../About.aspx' title='About Us'>About Us</a>");
                StrMenu.Append("</li>");
                StrMenu.Append("<li class='test' style='background:none;'><a href='Free-home-evaluation.aspx' title='About Us'>Home Evaluation</a></li>");
                StrMenu.Append("</ul>");


            }


            // string str = @" <ul class='nav'><li style='background:none;' class='test'><a class='active' title='Home' href='../Default.aspx'>Home</a></li><li class=''><a title='Featured Properties' href='../FeatureListing.aspx'>Featured Properties</a></li><li class=''><a href='../Default.aspx' class='parent'>Seller</a><ul><li><a title='Seller Information' href='../StaticPages.aspx?PageID=22'>Seller Information</a> </li><li><a title='NeighbourHood Sold Report' href='../StaticPages.aspx?PageID=24'>NeighbourHood Sold Report</a> </li></ul></li><li class=''><a href='../Default.aspx' class='parent'>Buyer</a><ul><li><a title='Buying a House ' href='../StaticPages.aspx?PageID=25'>Buying a House </a> </li><li><a title='Choosing a Mortgage Term' href='../StaticPages.aspx?PageID=26'>Choosing a Mortgage Term</a> </li><li><a title='Glossary of terms' href='../StaticPages.aspx?PageID=27'>Glossary of terms</a> </li><li><a title='Helpful Moving Tips' href='../StaticPages.aspx?PageID=28'>Helpful Moving Tips</a> </li><li><a title='Mortgage applicaiton tips ' href='../StaticPages.aspx?PageID=29'>Mortgage applicaiton tips </a> </li><li><a title='Mortgage application steps ' href='../StaticPages.aspx?PageID=30'>Mortgage application steps </a> </li><li><a title='Mortgage Glossary' href='../StaticPages.aspx?PageID=31'>Mortgage Glossary</a> </li><li><a title='Renting VS Buying, Which is better' href='../StaticPages.aspx?PageID=32'>Renting VS Buying, Which is better</a> </li><li><a title='Save your money everyday' href='../StaticPages.aspx?PageID=33'>Save your money everyday</a> </li><li><a title='Selecting and working with a Realtor' href='../StaticPages.aspx?PageID=34'>Selecting and working with a Realtor</a> </li><li><a title='The cost of home buyer' href='../StaticPages.aspx?PageID=35'>The cost of home buyer</a> </li><li><a title='The elements of an offer' href='../StaticPages.aspx?PageID=36'>The elements of an offer</a> </li><li><a title='Tips for choosing a realtor' href='../StaticPages.aspx?PageID=37'>Tips for choosing a realtor</a> </li><li><a title='Types of Mortgages' href='../StaticPages.aspx?PageID=38'>Types of Mortgages</a> </li><li><a title='Understanding market conditions' href='../StaticPages.aspx?PageID=39'>Understanding market conditions</a> </li><li><a title='What is a mortgage' href='../StaticPages.aspx?PageID=40'>What is a mortgage</a> </li><li><a title='Why do I need home inspection' href='../StaticPages.aspx?PageID=41'>Why do I need home inspection</a> </li><li><a title='Why do you want to move' href='../StaticPages.aspx?PageID=42'>Why do you want to move</a> </li></ul></li><li class=''><a href='../Default.aspx' class='parent'>Links</a><ul><li class=''><a title='Useful Search Tools' href='../StaticPages.aspx?PageID=43'>Useful Search Tools</a> </li><li><a title='Real Estate and Housings' href='../StaticPages.aspx?PageID=44'>Real Estate and Housings</a> </li><li><a title='Utilities' href='../StaticPages.aspx?PageID=45'>Utilities</a> </li><li><a title='School Boards, Universities, Colleges' href='../StaticPages.aspx?PageID=46'>School Boards, Universities, Colleges</a> </li><li><a title='Banks and Financial Institutions' href='../StaticPages.aspx?PageID=47'>Banks and Financial Institutions</a> </li><li><a title='Transportation' href='../StaticPages.aspx?PageID=48'>Transportation</a> </li><li><a title='Hospitals and Health Centres' href='../StaticPages.aspx?PageID=49'>Hospitals and Health Centres</a> </li><li><a title='Government' href='../StaticPages.aspx?PageID=50'>Government</a> </li></ul></li><li style='background:none;' class='test'><a title='About Us' href='Admin/Adminlogin.aspx'>Login</a></li><li class=''><a title='Calculators' href='../Calculators.aspx'>Calculators</a></li><li class=''><a title='About Us' href='../About.aspx'>About Us</a></li><li style='background:none;' class='test'><a title='About Us' href='Free-home-evaluation.aspx'>Home Evaluation</a></li></ul>";

            //dynamicmenus.Text = str;

            // dynamicmenus.Text = StrMenu.ToString();
            //Div_menu_bg.InnerHtml = StrMenu.ToString();

        }
        protected void btnsubmit_Click(object sender, EventArgs e)
        {
            //Session["username"] = txtname.Text;
            //Session["userphone"] = txtphone.Text;
            //Session["useremail"] = txtemail.Text;
            //myModal.Visible = false;      
        }
    }
}