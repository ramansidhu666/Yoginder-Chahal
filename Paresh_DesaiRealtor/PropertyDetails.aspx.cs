﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Property_cls;
using System.Net;
using System.Configuration;
using System.Net.Mail;

namespace Property
{
    public partial class PropertyDetails : System.Web.UI.Page
    {

        #region PageLoad
        cls_Property clsobj = new cls_Property();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                int userCount = Convert.ToInt32(HttpContext.Current.Session["MySession"]);

                if (userCount >0 && Request.Cookies["UserEmail"] == null)
                {
                    //btnModal_Clicked(sender, e);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
                }
                HttpContext.Current.Session["MySession"] = userCount + 1;
            }
            Session["PropertySearchType"] = Request.QueryString["PropertyType"];
            Session["Municipality"] = null;
            if (Page.IsPostBack == false)
            {
                //Session["PropertySearchType"] = Request.QueryString["PropertyType"].ToString();
                //if (Session["PropertySearchType"].ToString().Contains("Residential"))
                //{
                //    GetImages();
                //}
                //else if (Session["PropertySearchType"].ToString().Contains("Commercial"))
                //{
                //    GetCommercialImages();
                //}
                //else if (Session["PropertySearchType"].ToString().Contains("Condo"))
                //{
                //    GetCondoImages();
                //}
                //else
                //{
                //    Response.Write("Invalid MLS#");
                //}
                //GetPropertyDetails();
            }
        }
        #endregion Page Load

        protected void btnSaveUserInfo_Clicked(object sender, EventArgs e)
        {
            string objuser;
            if (txtName.Value == "" && txtEmailID.Value == "" && txtPhone.Value == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Error!", "alert('Fill all the details');", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal();", true);
            }
            else
            {
                cls_Property clsp = new cls_Property();
                var obj = clsp.Insert_UserInfo(txtName.Value, txtEmailID.Value, txtPhone.Value);
                string UserEmailId = ConfigurationManager.AppSettings["RegFromMailAddress"].ToString();
                string ToEmailId = ConfigurationManager.AppSettings["ToEmailID"].ToString();

                SendMailToAdmin(UserEmailId);
                if (obj != null)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Congrts!", "alert('Your E-mail has been sent sucessfully - Thank You');", true);
                    objuser = "Success";
                    //HttpCookie UserEmailcookies = new HttpCookie("UserEmail");
                    Response.Cookies["UserEmail"].Value = txtEmailID.Value;
                    //UserEmailcookies.Value=Email;
                }
                else
                {
                    objuser = "Failure";
                }
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
            mail.Subject = "New user registered";
            string body = "";
            body = "<p>Person Name : " + txtName.Value + "</p>";
            body = body + "<p>Email ID : " + txtEmailID.Value + "</p>";
            body = body + "<p>Phone Number : " + txtPhone.Value + "</p>";
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
        private string CheckNullOrEmptyvalue(string pValue)
        {
            string pval1 = "";
            if (pValue == "null" || pValue == "")
                pval1 = "";
            else
                pval1 = pValue;
            return pval1;
        }
        void GetPropertyDetails()
        {
            try
            {
                Property1.MLSDataWebServiceSoapClient mlsClient = new Property1.MLSDataWebServiceSoapClient();

                DataTable dt = new DataTable();
                if (Session["PropertySearchType"].ToString().Contains("Residential") || Session["PropertySearchType"].ToString().Contains("IDXImagesResidential"))
                {
                    dt = mlsClient.GetResidentialProperties(Convert.ToString(Request.QueryString["MLSID"]), "0", "0", "0", "0", "0", "0");
                }
                else if (Session["PropertySearchType"].ToString().Contains("Commercial") || Session["PropertySearchType"].ToString().Contains("IDXImagesCommercial"))
                {
                    dt = mlsClient.GetAllCommercialProperties(Request.QueryString["MLSID"].ToString(), "0", "0", "0", "0", "0");
                }
                else if (Session["PropertySearchType"].ToString().Contains("Condo") || Session["PropertySearchType"].ToString().Contains("IDXImagesCondo"))
                {
                    dt = mlsClient.GetProperties_Condo(Convert.ToString(Request.QueryString["MLSID"]), "0", "0", "0", "0", "0", "0");
                }
                try
                {
                    lblroom.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Rooms"]));
                lblMLS.Text = Convert.ToString(dt.Rows[0]["MLS"]);
                lblbed.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Bedrooms"]));
                //lblApxAge1.Text = Convert.ToString(dt.Rows[0]["ApproxAge"]) != "null" && Convert.ToString(dt.Rows[0]["ApproxAge"]) != "" ? Convert.ToString(dt.Rows[0]["ApproxAge"]) : "";
                //lblSubTypeofhome.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["typeown1out"]));
                //lblHeat1.Text = Convert.ToString(dt.Rows[0]["HeatType"]) != "null" && Convert.ToString(dt.Rows[0]["HeatType"]) != "" ? Convert.ToString(dt.Rows[0]["HeatType"]) : "";
                //lblBasement1.Text = Convert.ToString(dt.Rows[0]["Basement1"]) != "null" && Convert.ToString(dt.Rows[0]["Basement1"]) != "" ? Convert.ToString(dt.Rows[0]["Basement1"]) : "";
                //lblSewers1.Text = Convert.ToString(dt.Rows[0]["Sewers"]) != "null" && Convert.ToString(dt.Rows[0]["Sewers"]) != "" ? Convert.ToString(dt.Rows[0]["Sewers"]) : "";
                //lblGarageTypes1.Text = Convert.ToString(dt.Rows[0]["GarageType"]) != "null" && Convert.ToString(dt.Rows[0]["GarageType"]) != "" ? Convert.ToString(dt.Rows[0]["GarageType"]) : "";
                //lblParking1.Text = Convert.ToString(dt.Rows[0]["ParkingSpaces"]) != "null" && Convert.ToString(dt.Rows[0]["ParkingSpaces"]) != "" ? Convert.ToString(dt.Rows[0]["ParkingSpaces"]) : "";
                //lblKitchen.Text = (Convert.ToString(dt.Rows[0]["Kitchens"]) != "null" && (Convert.ToString(dt.Rows[0]["Kitchens"]) != "") ? (Convert.ToString(dt.Rows[0]["Kitchens"])) : "") + (Convert.ToString(dt.Rows[0]["KitchensPlus"]) != "null" && (Convert.ToString(dt.Rows[0]["KitchensPlus"]) != "") ? ("+" + Convert.ToString(dt.Rows[0]["KitchensPlus"])) : "");
                //lblfamilyrm.Text = Convert.ToString(dt.Rows[0]["FamilyRoom"]) != "null" && (Convert.ToString(dt.Rows[0]["FamilyRoom"]) != "") ? (Convert.ToString(dt.Rows[0]["FamilyRoom"])) : "";
                //lblbasement.Text = (Convert.ToString(dt.Rows[0]["Basement1"]) != "null" && (Convert.ToString(dt.Rows[0]["Basement1"]) != "") ? (Convert.ToString(dt.Rows[0]["Basement1"])) : "") + " " + (Convert.ToString(dt.Rows[0]["Basement2"]) != "null" && (Convert.ToString(dt.Rows[0]["Basement2"]) != "") ? (Convert.ToString(dt.Rows[0]["Basement2"])) : "");
                //lblExterior.Text = Convert.ToString(dt.Rows[0]["Exterior1"]) != "null" && Convert.ToString(dt.Rows[0]["Exterior1"]) != "" ? Convert.ToString(dt.Rows[0]["Exterior1"]) : "";
                //lblGarageType.Text = (Convert.ToString(dt.Rows[0]["GarageType"]) != "null" && Convert.ToString(dt.Rows[0]["GarageType"]) != "" ? Convert.ToString(dt.Rows[0]["GarageType"]) : "") + (Convert.ToString(dt.Rows[0]["GarageParkSpaces"]) != "" && Convert.ToString(dt.Rows[0]["GarageParkSpaces"]) != "null" ? ("/" + Convert.ToString(dt.Rows[0]["GarageParkSpaces"])) : "");
               // lblParking.Text = Convert.ToString(dt.Rows[0]["ParkingSpaces"]) != "null" && Convert.ToString(dt.Rows[0]["ParkingSpaces"]) != "" ? Convert.ToString(dt.Rows[0]["ParkingSpaces"]) : "";
                //lblPool.Text = "<b>Pets Permitted:</b>" + (Convert.ToString(dt.Rows[0]["PetsPermitted"]) != "null" && Convert.ToString(dt.Rows[0]["PetsPermitted"]) != "" ? Convert.ToString(dt.Rows[0]["PetsPermitted"]) : "");
                //lblWater.Text = Convert.ToString(dt.Rows[0]["WaterIncluded"]) != "null" && Convert.ToString(dt.Rows[0]["WaterIncluded"]) != "" ? Convert.ToString(dt.Rows[0]["WaterIncluded"]) : "";
                //lblSewers.Text = "<b>Bldg Insur Incl:</b>" + (Convert.ToString(dt.Rows[0]["BuildingInsuranceIncluded"]) != "" && Convert.ToString(dt.Rows[0]["WaterIncluded"]) != "null" ? Convert.ToString(dt.Rows[0]["WaterIncluded"]) : "");
                //lblSpecificDesignation.Text = Convert.ToString(dt.Rows[0]["SpecialDesignation1"]) != "" && Convert.ToString(dt.Rows[0]["SpecialDesignation1"]) != "null" ? Convert.ToString(dt.Rows[0]["SpecialDesignation1"]) : "";
                //LblParkingInc.Text = Convert.ToString(dt.Rows[0]["ParkingIncluded"]) != "" && Convert.ToString(dt.Rows[0]["ParkingIncluded"]) != "null" ? Convert.ToString(dt.Rows[0]["ParkingIncluded"]) : "";
                //lblBalcony.Text = Convert.ToString(dt.Rows[0]["Balcony"]) != "" && Convert.ToString(dt.Rows[0]["Balcony"]) != "null" ? Convert.ToString(dt.Rows[0]["Balcony"]) : "";
                Session["Address"] = (CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["address"])) + ", " + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Municipality"])) + " , " + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["PostalCode"])));
                lblAddressBar1.Text = (CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["address"])) + ", " + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Municipality"])) + ", " + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["PostalCode"])) + " , " + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["province"])));
                lblPropertyDescriptionnew.Text = Convert.ToString(dt.Rows[0]["remarksforclients"]);
                }
                catch (Exception ex)
                { 
                }
                string extras;
                if (dt.Rows[0]["extras"].ToString().Length > 5)
                    extras = "<b style='float:left; width:80px;'>Extras :</b>" + "<div style='margin:0 0 0 96px;'>" + Convert.ToString(dt.Rows[0]["extras"]) + "</div>";
                else
                    extras = "";

                //lblCommunity.Text = Convert.ToString(dt.Rows[0]["Community"]);

                // lblprovince.Text = CheckNullOrEmptyvalue(dt.Rows[0]["Municipality"].ToString());

                try
                {
                  //  lblStorey.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Style"]));
                }
                catch
                {
                    //lblStorey.Text = "";
                }

               // lblSubTypeofhome.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["typeown1out"]));
                string frontONNsew = "";
                try
                {
                    frontONNsew = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["FrontingOnNSEW"]));
                }
                catch
                { }
                //if (frontONNsew.ToString() == "E")
                //    lblfronting.Text = "<b>Fronting On: </b>" + "East";
                //else if (frontONNsew.ToString() == "W")
                //    lblfronting.Text = "<b>Fronting On: </b>" + "West";
                //else if (frontONNsew.ToString() == "N")
                //    lblfronting.Text = "<b>Fronting On: </b>" + "North";
                //else if (frontONNsew.ToString() == "S")
                //    lblfronting.Text = "<b>Fronting On: </b>" + "South";
                try
                {
                    //lbltype.Text = Convert.ToString(dt.Rows[0]["TypeOwn1Out"]) + " " + Convert.ToString(dt.Rows[0]["Style"]);
                }
                catch
                {
                    //lbltype.Text = Convert.ToString(dt.Rows[0]["TypeOwn1Out"]);
                }
                lblgarage.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["GarageType"]));
                lblgarage1.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["GarageType"]));
                try
                {
                    lblprice.Text = "$" + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["ListPrice"]));
                    lbltax.Text = dt.Rows[0]["TotalTaxes"].ToString();
                    lblgarage.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["GarageType"]));
                    lblbasement.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Basement1"]));
                    lblarea.Text = CheckNullOrEmptyvalue(dt.Rows[0]["area"].ToString());
                    lblcity.Text = CheckNullOrEmptyvalue(dt.Rows[0]["Municipality"].ToString());
                    lblcommunity.Text = Convert.ToString(dt.Rows[0]["Community"]);
                    lbltype.Text = Convert.ToString(dt.Rows[0]["TypeOwn1Out"]) + " " + Convert.ToString(dt.Rows[0]["Style"]);
                    lblbed.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Bedrooms"]));
                    lblBedroom.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Bedrooms"]));
                    lblbath.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Washrooms"]));
                    lblbath1.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Washrooms"]));
                    Session["Address"] = (CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["address"])) + ", " + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Municipality"])) + " , " + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["PostalCode"])));
                    lblAddressBar1.Text = (CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["address"])) + ", " + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Municipality"])) + ", " + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["PostalCode"])) + " , " + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["province"])));
                    lblPropertyDescriptionnew.Text = Convert.ToString(dt.Rows[0]["remarksforclients"]);
                    if (dt.Rows[0]["extras"].ToString().Length > 5)
                        extras = "<b style='float:left; width:80px;'>Extras :</b>" + "<div style='margin:0 0 0 96px;'>" + Convert.ToString(dt.Rows[0]["extras"]) + "</div>";
                    else
                        extras = "";
                    try
                    {
                        //lblStorey.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Style"]));
                    }
                    catch
                    {
                        lbltype.Text = Convert.ToString(dt.Rows[0]["TypeOwn1Out"]);
                       // lblStorey.Text = "";
                    }
               
                    try
                    {
                       // lblWater1.Text = Convert.ToString(dt.Rows[0]["water"]) != "null" && Convert.ToString(dt.Rows[0]["water"]) != "" ? Convert.ToString(dt.Rows[0]["water"]) : "";
                        //frontONNsew = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["FrontingOnNSEW"]));
                    }
                    catch
                    { }
                    //if (frontONNsew.ToString() == "E")
                    //    lblfronting.Text = "Fronting On:" + "East";
                    //else if (frontONNsew.ToString() == "W")
                    //    lblfronting.Text = "Fronting On: " + "West";
                    //else if (frontONNsew.ToString() == "N")
                    //    lblfronting.Text = "Fronting On:" + "North";
                    //else if (frontONNsew.ToString() == "S")
                    //    lblfronting.Text = "Fronting On:" + "South";

                    lblgarage.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["GarageType"]));
                    try
                    {

                        lblbed.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Bedrooms"]));
                        lblBedroom.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Bedrooms"]));
                        lblbath.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Washrooms"]));
                        //lblWashRooms.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Washrooms"]));
                        //lblDirCrossSt.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["DirectionsCrossStreets"]));

                        //lblKitchen.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Kitchens"])) + "+" + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["KitchensPlus"]));
                        //lblfamilyrm.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["FamilyRoom"]));
                        //lblExterior.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Exterior1"]));

                    }
                    catch
                    {
                        lblbed.Text = CheckNullOrEmptyvalue(dt.Rows[0]["totalarea"].ToString()) + " " + CheckNullOrEmptyvalue(dt.Rows[0]["totalareacode"].ToString());
                        lblbath.Text = CheckNullOrEmptyvalue(dt.Rows[0]["Water"].ToString());
                    }
                    lblremarks.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Remarksforclients"]));
                }
                catch
                {
                    //lblbasement122.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Basement1"]));
                    //lblAreaLabel.Text = "Office Area";
                    //lblAreaValue.Text = CheckNullOrEmptyvalue(dt.Rows[0]["OfficeAptarea"].ToString()) + " " + CheckNullOrEmptyvalue(dt.Rows[0]["officeaptareacode"].ToString());
                    //lblBedLabel.Text = "Total Area";
                    //lblbed.Text = CheckNullOrEmptyvalue(dt.Rows[0]["totalarea"].ToString()) + " " + CheckNullOrEmptyvalue(dt.Rows[0]["totalareacode"].ToString());
                    //lblBathLabel.Text = "Water";
                    lblbath.Text = CheckNullOrEmptyvalue(dt.Rows[0]["Water"].ToString());
                }

              //  lblMLS1.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["MLS"]));
                //lbltx.Text = dt.Rows[0]["TotalTaxes"].ToString();
                // lblBasement.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Basement1"]));
                // lblGarageType.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["GarageType"]));
                // lblParking.Text = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["ParkingSpaces"]));
                int NoOfRoom = Convert.ToInt32("0" + lblroom.Text);
                DataTable dtRooms = new DataTable();
                dtRooms.Columns.Add("Room", typeof(string));
                dtRooms.Columns.Add("Level", typeof(string));
                dtRooms.Columns.Add("RoomDim", typeof(string));
                dtRooms.Columns.Add("RoomDesc", typeof(string));

                for (int i = 0; i < NoOfRoom; i++)
                {
                    int RowIndex = i + 1;
                    string vRoom = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Room" + RowIndex + ""]));
                    string vLevel = CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Level" + RowIndex + ""])) != "" ? Convert.ToString(dt.Rows[0]["Level" + RowIndex + ""]) : "0";
                    string vRoomDim = (CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Length"])) != "" ? (Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Length"])) : "0") + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Width"]) != "" ? ("x" + Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Width"])) : "");// Convert.ToString(dt.Rows[0]["Room1Length"]) + "x" + Convert.ToString(dt.Rows[0]["Room1Width"]);
                    string vRoomDesc = (CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Desc1"])) != "" ? (Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Desc1"])) : "----") + CheckNullOrEmptyvalue(Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Desc2"]) != "" ? ("," + Convert.ToString(dt.Rows[0]["Room" + RowIndex + "Desc2"])) : "");

                    DataRow dr = dtRooms.NewRow();
                    dr["Room"] = vRoom;
                    dr["Level"] = vLevel;
                    dr["RoomDim"] = vRoomDim;
                    dr["RoomDesc"] = vRoomDesc;
                    dtRooms.Rows.Add(dr);
                    LVroom.DataSource = dtRooms;
                    LVroom.DataBind();
                }
            }

            catch (Exception ex)
            { }
        }
        #region Residential Methods

        void GetImages()
        {
            Property1.MLSDataWebServiceSoapClient mlsClient = new Property1.MLSDataWebServiceSoapClient();
            DataTable dt = mlsClient.GetImageByMLSID(Convert.ToString(Request.QueryString["MLSID"]));

            if (dt.Rows.Count > 0)
            {
                img1.ImageUrl = Convert.ToString(dt.Rows[0]["MLSImage"]);
                img2.ImageUrl = Convert.ToString(dt.Rows[0]["MLSImage"]);
                Image1.ImageUrl = Convert.ToString(dt.Rows[1]["MLSImage"]);
                Image2.ImageUrl = Convert.ToString(dt.Rows[2]["MLSImage"]);
                Image3.ImageUrl = Convert.ToString(dt.Rows[3]["MLSImage"]);
                Image4.ImageUrl = Convert.ToString(dt.Rows[4]["MLSImage"]);
                Image5.ImageUrl = Convert.ToString(dt.Rows[5]["MLSImage"]);
            }
            else
            {
              
            }

            mlsClient = null;
        }
       
   

        #endregion Residential Methods
        #region Commercial Methods
        void GetCommercialImages()
        {
            try
            {
                Property1.MLSDataWebServiceSoapClient mlsClient = new Property1.MLSDataWebServiceSoapClient();
                DataTable dt = mlsClient.GetCommercialPropertiesByMLSID(Convert.ToString(Request.QueryString["MLSID"]));

                if (dt.Rows.Count > 0)
                {
                    img1.ImageUrl = Convert.ToString(dt.Rows[0]["MLSImage"]);
                    img2.ImageUrl = Convert.ToString(dt.Rows[0]["MLSImage"]);
                    Image1.ImageUrl = Convert.ToString(dt.Rows[1]["MLSImage"]);
                    Image2.ImageUrl = Convert.ToString(dt.Rows[2]["MLSImage"]);
                    Image3.ImageUrl = Convert.ToString(dt.Rows[3]["MLSImage"]);
                    Image4.ImageUrl = Convert.ToString(dt.Rows[4]["MLSImage"]);
                    Image5.ImageUrl = Convert.ToString(dt.Rows[5]["MLSImage"]);
                }
                else
                {

                }

                mlsClient = null;
            }
            catch (Exception ex)
            { }
        }

        #endregion Commercial Methods

        #region Condo Methods

        void GetCondoImages()
        {
            Property1.MLSDataWebServiceSoapClient mlsClient = new Property1.MLSDataWebServiceSoapClient();
            DataTable dt = mlsClient.GetPropertiesByMLSID_Condo(Convert.ToString(Request.QueryString["MLSID"]));

            if (dt.Rows.Count > 0)
            {
                img1.ImageUrl = Convert.ToString(dt.Rows[0]["MLSImage"]);
                img2.ImageUrl = Convert.ToString(dt.Rows[0]["MLSImage"]);
                Image1.ImageUrl = Convert.ToString(dt.Rows[1]["MLSImage"]);
                Image2.ImageUrl = Convert.ToString(dt.Rows[2]["MLSImage"]);
                Image3.ImageUrl = Convert.ToString(dt.Rows[3]["MLSImage"]);
                Image4.ImageUrl = Convert.ToString(dt.Rows[4]["MLSImage"]);
                Image5.ImageUrl = Convert.ToString(dt.Rows[5]["MLSImage"]);
            }
            else
            {
             
            }

            mlsClient = null;
        }

        #endregion Condo Methods
     
    }
}
