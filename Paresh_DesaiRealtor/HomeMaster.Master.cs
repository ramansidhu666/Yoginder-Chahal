using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using Property_cls;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Mail;

namespace Property
{
    public partial class HomeMaster : System.Web.UI.MasterPage
    {
        #region Global
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ToString());
        cls_Property clsobj = new cls_Property();

        #endregion Global
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["FirstName"] = null;
                BindMenusList();
                SiteSetting();
                bindBnanners();
                GetImages();
                GetFeaturedProperties();
                //GetTestimonials();
            }
        }
        //protected void addre_submit_Click(object sender, EventArgs e)
        //{
        //    string s = search.Value;
        //    Response.Redirect("~/Review_home_worth.aspx?address=" + search.Value, false);
        //}
        void GetFeaturedProperties()
        {
            try
            {
                DataTable dt = new DataTable();
                dt = clsobj.GetTopNFeturedProperties("4");


                Repeater1.DataSource = dt;

                Repeater1.DataBind();
            }
            catch (Exception ex)
            {
                throw ex;

            }
            finally
            {

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
                StrMenu.Append("<li class='test'><a href='../Home.aspx' title='Home'>Home</a></li>");
                
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    clsobj.PageID = Convert.ToInt32(dt.Rows[i]["ID"]);
                    dtSubmenu = clsobj.GetSubMenuBy_PageID();
                    //check if it has submenu 
                    if (dtSubmenu.Rows.Count > 0)
                    {
                        StrMenu.Append("<li><a href=#>" + dt.Rows[i]["PageName"] + "</a>");//</li>
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

                StrMenu.Append("<li class='test' style='background:none;'><a href='Calculators.aspx' title='Calculators'>Calculators</a></li>");
                StrMenu.Append("<li style='background:none;'><a href='RealEstateNews.aspx' title='Real Estate News'>Real Estate News</a></li>");
                StrMenu.Append("<li style='background:none;'><a href='Home_worth.aspx' title='Home Evaluation'>Home Evaluation</a></li>");
                StrMenu.Append("<li class='test' style='background:none;'><a href='ContactUs.aspx' title='Contact Us'>Contact Us</a></li>");
                StrMenu.Append("</ul>"); ;


            }


            dynamicmenus.Text = StrMenu.ToString();

        }
        public void GetImages()
        {
            Property1.MLSDataWebServiceSoapClient mlsClient = new Property1.MLSDataWebServiceSoapClient();
            //DataTable dt = mlsClient.GetImageByMLSID(Convert.ToString(Request.QueryString["MLSID"]));

            DataTable dt = clsobj.GetDreamHouseList();
            if (dt.Rows.Count > 0)
            {
                rptImages.DataSource = dt;
                rptImages.DataBind();
                //sliderDiv.Visible = true;
                //sliderImageEmpty.Visible = false;
            }
            else
            {
                //sliderDiv.Visible = false;
                //sliderImageEmpty.Visible = true;
            }

            mlsClient = null;
        }
        protected void SiteSetting()
        {
            try
            {
                DataTable dt = clsobj.GetSiteSettings();
                DataTable dt1 = clsobj.GetUserInfo();
                if (dt.Rows.Count > 0)
                {
                   lblemailid.Text = Convert.ToString(dt.Rows[0]["Email"]);
                    siteTitle.Text = Convert.ToString(dt.Rows[0]["Title"]);
                   lblemail.Text = Convert.ToString(dt.Rows[0]["Email"]);
                    //lblmobile2.Text = Convert.ToString(dt.Rows[0]["Mobile"]);
                    lblmobile.Text = Convert.ToString(dt.Rows[0]["Mobile"]);
                    //lblfax.Text = Convert.ToString(dt.Rows[0]["Fax"]);
                    //lblemail.Text = Convert.ToString(dt.Rows[0]["Email"]);
                    if (dt1.Rows.Count > 0)
                    {
                        //lblBrkrOneName.Text = Convert.ToString(dt1.Rows[0]["FirstName"]) + " " + Convert.ToString(dt1.Rows[0]["LastName"]);
                    }
                  
                    byte[] favimage = (byte[])dt.Rows[0]["Favicon.ico"];
                    if (favimage.Length > 0)
                    {
                        Session["MyFavicon"] = favimage;
                        favicon.Visible = true;
                        favicon.Href = "~/ShowFavicon.aspx";
                    }
                    else
                    {
                        favicon.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                cls_Property clsp = new cls_Property();
               // clsp.Insert_ContactUS(txtFirstName.Text, txtLastName.Text, txtEmail.Text, txtPhoneno.Text, txtMessage.Text + "<br/>" + HttpContext.Current.Request.Url.AbsoluteUri);
                //int indextilldel = Request.Url.AbsoluteUri.IndexOf("Posting");

                string UserEmailId = ConfigurationManager.AppSettings["RegFromMailAddress"].ToString();
                string ToEmailId = ConfigurationManager.AppSettings["ToEmailID"].ToString();
                SendMailToAdmin(UserEmailId);
               

                //txtFirstName.Text = "";
                //txtLastName.Text = "";
                //txtEmail.Text = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Congrats!", "alert('Your E-mail has been sent sucessfully to admin - Thank You');", true);
                return;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Failure", "alert('Your message failed to send, please try again.');", true);
                return;
                //If the message failed at some point, let the user know
                //lblResult.Text = "Your message failed to send, please try again.";
            }
        }

        public void SendMailToAdmin(string UserEmailId)
        {

            MailMessage mail = new MailMessage();


            string ToEmailID = ConfigurationManager.AppSettings["ToEmailID"].ToString(); //From Email & To Email are same for admin
            //string ToEmailPassword = ConfigurationManager.AppSettings["ToEmailPassword"].ToString();
            string FromEmailID = ConfigurationManager.AppSettings["RegFromMailAddress"].ToString();
            string FromEmailPassword = ConfigurationManager.AppSettings["RegPassword"].ToString();


            string _Host = ConfigurationManager.AppSettings["SmtpServer"].ToString();
            int _Port = Convert.ToInt32(ConfigurationManager.AppSettings["Port"].ToString());
            Boolean _UseDefaultCredentials = false;
            Boolean _EnableSsl = true;

            mail.To.Add(ToEmailID);
            mail.From = new MailAddress(FromEmailID);
            mail.Subject = "Free Real Estate Reports";
            string body = "";
           // body = "<p>Person Name : " + txtFirstName.Text + " " + txtLastName.Text + "</p>";
            //body = body + "<p>Email ID : " + txtEmail.Text + "</p>";
            mail.Body = body;

            mail.IsBodyHtml = true;
            SmtpClient smtp = new SmtpClient();
            smtp.Host = _Host;
            smtp.Port = _Port;
            smtp.UseDefaultCredentials = _UseDefaultCredentials;
            smtp.Credentials = new System.Net.NetworkCredential
            (FromEmailID, FromEmailPassword);// Enter senders User name and password
            smtp.EnableSsl = _EnableSsl;
            smtp.Send(mail);
        }
        protected void bindBnanners()
        {
            StringBuilder html = new StringBuilder();
            DataTable dt = clsobj.GetAllBanner();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Images;
                Images = "/admin/uploadfiles/" + dt.Rows[i]["FileName"].ToString();
                if (Images != "")
                {
                    html.AppendLine("<img src='" + Images + "'  data-thumb='" + Images + "'  alt='banner" + i + "' />");
                }
            }
            //ltrImgsf.Text = html.ToString();
        }
    }
}