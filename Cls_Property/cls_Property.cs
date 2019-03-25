﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Net;
using System.IO;
using System.Text;

namespace Property_cls
{
    public class cls_Property
    {
        SqlConnection conn = new SqlConnection();//ConfigurationManager.ConnectionStrings["ConStr"].ToString());

        //properties
        public string MLSID
        {
            get;
            set;
        }
        public string Name
        {
            get;
            set;
        }
        public string FirstName
        {
            get;
            set;
        }
        public string LastName
        {
            get;
            set;
        }
        public string date
        {
            get;
            set;
        }
        public string comments
        {
            get;
            set;
        }
        public string Link
        {
            get;
            set;
        }
        public int PageID
        {
            get;
            set;
        }
        public string PageName
        {
            get;
            set;
        }
        public string metatag
        {
            get;
            set;
        }
        public string metadiscription
        {
            get;
            set;
        }
        public string PageTitle
        {
            get;
            set;
        }
        public string PageDescription
        {
            get;
            set;
        }
        public string PageType
        {
            get;
            set;
        }
        public string SubMenuPageName
        {
            get;
            set;
        }
        public string CreatedBy
        {
            get;
            set;
        }
        public Boolean IncludeInMenu
        {
            get;
            set;
        }
        public Boolean IncludeInSubMenu
        {
            get;
            set;
        }
        public int SubMenuPageID
        {
            get;
            set;
        }
        public int DisplayIndex
        {
            get;
            set;
        }
        public string PageLocation
        {
            get;
            set;
        }
        public string ImageTitle
        {
            get;
            set;
        }
        public string ImageUrl
        {
            get;
            set;
        }
        public string ImageDesc
        {
            get;
            set;
        }
        public int Insert_PageBlog()
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_InsertPageBlog";
                cmd.Parameters.AddWithValue("@PageName", PageName);
                cmd.Parameters.AddWithValue("@MetaTag",metatag);
                cmd.Parameters.AddWithValue("@MetaDiscription", metadiscription);
                cmd.Parameters.AddWithValue("@PageTitle", PageTitle);
                cmd.Parameters.AddWithValue("@PageDescription", PageDescription);
                cmd.Parameters.AddWithValue("@PageType", PageType);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@IncludeInMenu", IncludeInMenu);
                cmd.Parameters.AddWithValue("@IncludeInSubMenu", IncludeInSubMenu);
                cmd.Parameters.AddWithValue("@SubMenuPageID", SubMenuPageID);
                cmd.Parameters.AddWithValue("@DisplayIndex", DisplayIndex);
                cmd.Parameters.AddWithValue("@SubMenuPageName", SubMenuPageName);
                cmd.Parameters.AddWithValue("@PageLocation", PageLocation);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public int Update_PageBlog()
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_UpdatePageBlog";
                cmd.Parameters.AddWithValue("@PageID", PageID);
                cmd.Parameters.AddWithValue("@PageName", PageName);
                cmd.Parameters.AddWithValue("@PageTitle", PageTitle);
                cmd.Parameters.AddWithValue("@PageDescription", PageDescription);
                cmd.Parameters.AddWithValue("@PageType", PageType);
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@IncludeInMenu", IncludeInMenu);
                cmd.Parameters.AddWithValue("@IncludeInSubMenu", IncludeInSubMenu);
                cmd.Parameters.AddWithValue("@SubMenuPageID", SubMenuPageID);
                cmd.Parameters.AddWithValue("@DisplayIndex", DisplayIndex);
                cmd.Parameters.AddWithValue("@SubMenuPageName", SubMenuPageName);
                cmd.Parameters.AddWithValue("@PageLocation", PageLocation);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public int Delete_PageBlog()
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_DeletePageBlog";
                cmd.Parameters.AddWithValue("@PageID", PageID);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }


        //preconstruction functions

        public int InsertDreamHouse(string Title, string filename, string Description, string Address, string Price)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_InsertPreCons";
                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.Parameters.AddWithValue("@ImageUrl", filename);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Price", Price);


                cmd.Parameters.Add("@newID", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
                result = System.Convert.ToInt32(cmd.Parameters["@newID"].Value);
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public int InsertDreamHouseImage(int DreamHouseId, string filename)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into [tbl_PreConstructionImages](DreamHouseId,ImageUrl) values(@DreamHouseId,@ImageUrl)";
                cmd.Parameters.AddWithValue("@DreamHouseId", DreamHouseId);
                cmd.Parameters.AddWithValue("@ImageUrl", filename);

                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public DataTable GetDreamHouseList()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "usp_GetAllPreConstruction";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                //dt.TableName = "tbl_Virtual";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
        public DataTable GetDreamHouse()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "usp_GetAllPreConstruction";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                //dt.TableName = "tbl_Virtual";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public DataTable GetDreamHouseForSlider(int DreamHouseId)
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "select * from tbl_PreConstructionImages where DreamHouseId=" + DreamHouseId;
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                //dt.TableName = "tbl_Virtual";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
        public DataTable GetDreamHouseDetail(int DreamHouseId)
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "select * from tbl_PreConstruction where Id=" + DreamHouseId;
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                //dt.TableName = "tbl_Virtual";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public int DeleteDreamHouse(int id)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from tbl_PreConstruction where id=@id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public DataTable GetDreamHousebyID(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "select * from tbl_PreConstruction where id=" + id;
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "tbl_DreamHouse";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
        //end


        public int Insert_ContactUS(string firstname, string lastname, string emailid, string phoneno, string message)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddContactUS";
                cmd.Parameters.AddWithValue("@firstname", firstname);
                cmd.Parameters.AddWithValue("@lastname", lastname);
                cmd.Parameters.AddWithValue("@emailid", emailid);
                cmd.Parameters.AddWithValue("@phonenumber", phoneno);
                cmd.Parameters.AddWithValue("@message", message);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public DataTable GetAllBlogs()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "usp_GetAllBlog";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "BlogList";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public DataTable GetPageBlogs_ByID()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd = conn.CreateCommand();
                cmd.CommandText = "usp_GetPageBlogsByID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ID", PageID);
                reader = cmd.ExecuteReader();
                dt.Load(reader);
                dt.TableName = "PageBlog";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public DataTable GetUserInfo()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                };
                string str = "usp_UserInfoSelectAll";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
        public DataTable GetAllPages()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "usp_GetAllPages";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "PageList";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public DataTable GetSiteSettings()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "usp_SelectSiteSettings";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public DataTable GetListBrokerage()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ServiceDataBase"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "GetListBrokerage";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }


        public DataTable GetMenuList()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "usp_GetMenuList";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public DataTable GetSubMenuBy_PageID()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd = conn.CreateCommand();
                cmd.CommandText = "usp_GetSubMenuListBy_PageID";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@SubMenuPageID", PageID);
                reader = cmd.ExecuteReader();
                dt.Load(reader);
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public int Insert_Favourite(int UserId, string MLSID)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_InsertFavourite";
                cmd.Parameters.AddWithValue("@UserID", UserId);
                cmd.Parameters.AddWithValue("@MLSID", MLSID);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public DataTable SelectFavourite(string MLSID)
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_SelectFavourite";
                cmd.Parameters.AddWithValue("@MLSID", MLSID);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                reader = cmd.ExecuteReader();
                dt.Load(reader);
                dt.TableName = "Favourite";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }


        public DataTable GetScheduledAppointments()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand();
                SqlDataReader reader;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_GetAppointment";
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                reader = cmd.ExecuteReader();
                dt.Load(reader);
                dt.TableName = "Appointment";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }


        public int Insert_FeatureProperty()
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_CreateFeatureProperty";
                cmd.Parameters.AddWithValue("@MLSID",MLSID);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public int Insert_VirtualLink()
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_CreateVirtualLink";
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@Link", Link);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public int Insert_Testimonial()
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_CreateTestimonial";
                cmd.Parameters.AddWithValue("@FirstName", FirstName);
                cmd.Parameters.AddWithValue("@LastName", LastName);
                cmd.Parameters.AddWithValue("@Date", date);
                cmd.Parameters.AddWithValue("@comments", comments);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public DataTable GetFeturedIDs()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "usp_GetFeatureIDs";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "Tbl_Featured";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }


        public DataTable GetFeturedIDsTop3()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "usp_GetFeatureIDsTop3";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                if(dt.Rows.Count > 0)
                {
                    dt.TableName = "Tbl_Featured";
                }
             
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
        public DataTable GetAllFeturedIDs()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "usp_GetAllFeatureIDs";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "Tbl_Featured";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
        public DataTable GetUserInfoByID(int id)
        {
            DataTable dt = new DataTable();
            try
            {


                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                };
                string str = "select * from registration where id=" + id + ";";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public DataTable GetAllBanner()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "select * from tblBanner order by itemorder asc;";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "tblBanner";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public DataTable GetCurrentFlyer()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "select * from tblCurrentFlyer;";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "tblCurrentFlyer";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
        public DataTable GetFeaturedProperties()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "sp_Features";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "tbl_Featured";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }


        public DataTable GetFeaturedPropertiesByMlsId(string mls)
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_FeaturesPropertiesbyMlsId";
                cmd.Parameters.AddWithValue("@mls", mls);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(dt);
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
     

        public DataTable GetVirtualTour()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "sp_Virtual";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "tbl_Virtual";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public DataTable GetTestimonial()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "sp_Testimonial";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "tbl_Virtual";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public DataTable GetCurrentFlyers()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "select * from [tblCurrentFlyer]";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "tblCurrentFlyer";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public DataTable GetTestimonials()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "sp_Testimonials";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "tbl_Testimonials";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public int InsertBanners(string name,string filename,int itemorder)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into tblBanner(Name,FileName,ItemOrder) values(@Name,@FileName,@ItemOrder)";
                cmd.Parameters.AddWithValue("@Name", name);
                cmd.Parameters.AddWithValue("@FileName", filename);
                cmd.Parameters.AddWithValue("@ItemOrder", itemorder);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public int InsertcurrentFlyer(string filename)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into tblCurrentFlyer(FileName) values(@FileName)";
              
                cmd.Parameters.AddWithValue("@FileName", filename);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }
        public int DeleteBanners(int id)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from tblBanner where id=@id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public int Deletecurrentflyer(int id)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from tblCurrentFlyer where id=@id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public DataTable GetBannerbyID(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "select * from tblBanner where id="+id;
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "tblBanner";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public DataTable GetCurrentFlyerID(int id)
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "select * from tblCurrentFlyer where id=" + id;
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "tblBanner";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public int Insert_PreConstructionCondos(string name, string emailid, string phoneno, string priceRange, string city)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddPreConstructionCondos";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@emailid", emailid);
                cmd.Parameters.AddWithValue("@phonenumber", phoneno);
                cmd.Parameters.AddWithValue("@pricerange", priceRange);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public int Insert_CommBuyingSelling(string name, string emailid, string phoneno, string priceRange, string city)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddCommBuyingSelling";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@emailid", emailid);
                cmd.Parameters.AddWithValue("@phonenumber", phoneno);
                cmd.Parameters.AddWithValue("@pricerange", priceRange);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public int Insert_AddPowerOfSale(string name, string emailid, string phoneno, string priceRange, string city)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddPowerOfSale";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@emailid", emailid);
                cmd.Parameters.AddWithValue("@phonenumber", phoneno);
                cmd.Parameters.AddWithValue("@pricerange", priceRange);
                cmd.Parameters.AddWithValue("@city", city);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }
        public DataTable GetTopNFeturedProperties(String NumberOfProperty)
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_GetFeatureIDsTopN";
                cmd.Parameters.AddWithValue("@NumberOfProperties", NumberOfProperty);
                cmd.Connection = conn;
                SqlDataReader reader = cmd.ExecuteReader();
                dt.Load(reader);
                dt.TableName = "Tbl_Featured";
            }

            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
        public int Insert_ListingsInInbox(string Name, string PropertyType, string Email, string PhoneNumber, string Radius)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddSearchedProperty";
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@PropertyType", PropertyType);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                cmd.Parameters.AddWithValue("@Radius", Radius);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }
        public int Insert_SearchedProperty(string Name, string PropertyType, string Email, string PhoneNumber, int Radius)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddSearchedProperty";
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@PropertyType", PropertyType);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                cmd.Parameters.AddWithValue("@Radius", Radius);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public int Insert_Home_worth(string Name, string lastname, string PhoneNumber, string email , string selling)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddHomeWorth";
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@LastName", lastname);
                cmd.Parameters.AddWithValue("@PhoneNumber", PhoneNumber);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@selling", selling);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }


       
        public int Insert_DreamHouse()
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_InsertDreamHouse";
                cmd.Parameters.AddWithValue("@Title", ImageTitle);
                cmd.Parameters.AddWithValue("@Description", ImageDesc);
                cmd.Parameters.AddWithValue("@PageType", PageType);

                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }
    
   

        protected DataTable GetAdminClients()
        {
            DataTable dt = new DataTable();
            try
            {
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "select * from [AdminClient]";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "AdminClient";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
            }
            return dt;
        }
        public int InsertAdminClient(string Name, string DOB, string Email, string PhoneNo, string Address, string Source, string status, string Gender, string Photopath, string Remarks)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_InsertAdminClient";
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@DOB", DOB);
                cmd.Parameters.AddWithValue("@PhoneNo", PhoneNo);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@Photopath", Photopath);
                cmd.Parameters.AddWithValue("@Remarks", Remarks);
                cmd.Parameters.AddWithValue("@IsEmailSend", false);
                cmd.Parameters.Add("@newID", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
                result = System.Convert.ToInt32(cmd.Parameters["@newID"].Value);
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public string GetURL()
        {
            Boolean IsLive = Convert.ToBoolean(ConfigurationManager.AppSettings["IsLive"].ToString());
            string URL = "";
            if (IsLive)
            {
                URL = ConfigurationManager.AppSettings["LiveURL"].ToString();
            }
            else
            {
                URL = ConfigurationManager.AppSettings["LocalURL"].ToString();
            }
            return URL;
        }

public DataTable GetExclusiveListing()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "usp_GetAllExclusiveListing";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                //dt.TableName = "tbl_Virtual";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }

        public DataTable GetExclusiveDetail(int DreamHouseId)
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "select * from ExclusiveListing where Id=" + DreamHouseId;
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                //dt.TableName = "tbl_Virtual";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
        public DataTable GetExclusiveForSlider(int DreamHouseId)
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "select * from ExclusiveListingImages where DreamHouseId=" + DreamHouseId;
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                //dt.TableName = "tbl_Virtual";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
        public int DeleteExclusive(int id)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from ExclusiveListing where id=@id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }
        public int InsertExclusiveData(string Title, string filename, string Description, string Address, string Price, string Status, string Type, string Bed, string Bath, string Tax, string Mls, string Area)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_InsertExclusiveListing";
                cmd.Parameters.AddWithValue("@Title", Title);
                cmd.Parameters.AddWithValue("@ImageUrl", filename);
                cmd.Parameters.AddWithValue("@Description", Description);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Price", Price);
                cmd.Parameters.AddWithValue("@Mls", Mls);
                cmd.Parameters.AddWithValue("@Status", Status);
                cmd.Parameters.AddWithValue("@Bed", Bed);
                cmd.Parameters.AddWithValue("@Bath", Bath);
                cmd.Parameters.AddWithValue("@Type", Type);
                cmd.Parameters.AddWithValue("@Tax", Tax);
                cmd.Parameters.AddWithValue("@Area", Area);


                cmd.Parameters.Add("@newID", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
                result = System.Convert.ToInt32(cmd.Parameters["@newID"].Value);
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public int InsertExclusiveImage(int DreamHouseId, string filename)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "Insert into [ExclusiveListingImages](DreamHouseId,ImageUrl) values(@DreamHouseId,@ImageUrl)";
                cmd.Parameters.AddWithValue("@DreamHouseId", DreamHouseId);
                cmd.Parameters.AddWithValue("@ImageUrl", filename);

                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }

        public int InsertNewsLetter(string Name, string Source, string FirstImage, string SecondImage)
        {
            DateTime? fwd_date = null;
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_InsertNewsLetter";
                cmd.Parameters.AddWithValue("@NewsLetterName", Name);
                cmd.Parameters.AddWithValue("@Image", FirstImage);
                cmd.Parameters.AddWithValue("@second_Image", SecondImage);
                cmd.Parameters.AddWithValue("@OrderNo", Source);
                cmd.Parameters.AddWithValue("@fwd_date", fwd_date).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@IsActive", false);

                cmd.Parameters.Add("@newID", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
                result = System.Convert.ToInt32(cmd.Parameters["@newID"].Value);
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }



        public int InsertGereetings(string Name, string Source, string FirstImage, string SecondImage)
        {
            DateTime? fwd_date = null;
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_InsertGereetings";
                cmd.Parameters.AddWithValue("@GreetingsName", Name);
                cmd.Parameters.AddWithValue("@Image", FirstImage);
                cmd.Parameters.AddWithValue("@second_Image", SecondImage);
                cmd.Parameters.AddWithValue("@OrderNo", Source);
                cmd.Parameters.AddWithValue("@fwd_date", fwd_date).Value = DBNull.Value;
                cmd.Parameters.AddWithValue("@IsActive", false);

                cmd.Parameters.Add("@newID", SqlDbType.Int).Direction = ParameterDirection.Output;

                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
                result = System.Convert.ToInt32(cmd.Parameters["@newID"].Value);
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }
       public int UpdateAdminClient(int ClientId, string Name, string DOB, string Email, string PhoneNo, string Address, string Source, string status, string Gender, string Photopath, string Remarks)
        {
            int result = 0;



            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_UpdateAdminClient";
                cmd.Parameters.AddWithValue("@ID", ClientId);
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@DOB", DOB);
                cmd.Parameters.AddWithValue("@PhoneNo", PhoneNo);
                cmd.Parameters.AddWithValue("@Address", Address);
                cmd.Parameters.AddWithValue("@Email", Email);
                cmd.Parameters.AddWithValue("@Source", Source);
                cmd.Parameters.AddWithValue("@Status", status);
                cmd.Parameters.AddWithValue("@Gender", Gender);
                cmd.Parameters.AddWithValue("@Photopath", Photopath);
                cmd.Parameters.AddWithValue("@Remarks", Remarks);
                cmd.Parameters.AddWithValue("@IsEmailSend", false);


                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }
        public DataTable GetdataTable(string qry)
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
            DataTable dt = new DataTable();
            SqlDataAdapter NewsLetter_Adp = new SqlDataAdapter(qry, conn);
            NewsLetter_Adp.Fill(dt);

            return dt;

        }
        #region Video tag functionality
        public DataTable GetVideoList()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "select Id, Name,URL from tblFiles";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                //dt.TableName = "tbl_Virtual";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
        public DataTable GetVideoBanner()
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                string str = "select * from tblFiles where RunFirst=1";
                SqlDataAdapter adp = new SqlDataAdapter(str, conn);
                adp.Fill(dt);
                dt.TableName = "tblFiles";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return dt;
        }
        public int InsertPropertyVideo(string Name, string ContentType, byte[] data, string YoutubeLink)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "insert into tblFiles(Name, ContentType,RunFirst,URL) values (@Name, @ContentType,@RunFirst,@YoutubeLink)";
                cmd.Parameters.AddWithValue("@Name", Name);
                cmd.Parameters.AddWithValue("@ContentType", "video/mp4");
                cmd.Parameters.AddWithValue("@RunFirst", 0);
                cmd.Parameters.AddWithValue("@YoutubeLink", YoutubeLink);


                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }
        public void ChangeVideo(int Id)
        {
            DataTable dt = new DataTable();
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }

                string str = "update tblFiles set RunFirst=0";
                SqlDataAdapter adp1 = new SqlDataAdapter(str, conn);
                adp1.Fill(dt);

                string str1 = "update tblFiles set RunFirst=1 where Id=" + Id;
                SqlDataAdapter adp2 = new SqlDataAdapter(str1, conn);
                adp2.Fill(dt);
                dt.TableName = "tblFiles";
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }

        }
        public int DeleteVideo(int id)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "delete from tblFiles where id=@id;";
                cmd.Parameters.AddWithValue("@id", id);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }
        #endregion

        public string ExecuteNonQuery(string QStr)
        {
            string ErrorMessage = "";
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
            SqlCommand cmd = null;
            try
            {

                if (conn.State == ConnectionState.Open)
                {
                    conn.Close();
                }
                conn.Open();
                cmd = new SqlCommand(QStr, conn);
                cmd.CommandType = CommandType.Text;
                cmd.ExecuteNonQuery();
                conn.Close();

                ErrorMessage = "success";
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("The DELETE statement conflicted with the REFERENCE constraint"))
                {
                    ErrorMessage = "FK";
                }
            }
            finally
            {
                if (cmd != null)
                {
                    cmd.Dispose();
                }
                if (conn != null)
                {
                    conn.Close();
                }

            }

            return ErrorMessage;
        }
        public int Insert_UserInfo(string name, string emailid, string phone)
        {
            int result = 0;
            try
            {
                conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConStr"].ConnectionString.ToString());
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "usp_AddUserInfo";
                cmd.Parameters.AddWithValue("@name", name);
                cmd.Parameters.AddWithValue("@emailid", emailid);
                cmd.Parameters.AddWithValue("@phone", phone);
                cmd.Connection = conn;
                if (conn.State == ConnectionState.Closed)
                {
                    conn.Open();
                }
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            { }
            finally
            {
                conn.Close();
                conn.Dispose();
            }
            return result;
        }
        public string GetAddressFromLatLong(double Latitute, double Longitute)
        {
            string Address = "";
            try
            {
                string url = "https://maps.googleapis.com/maps/api/geocode/xml?latlng={0},{1}&Key={2}&sensor=false";
                url = string.Format(url, Latitute, Longitute, "AIzaSyDxw4-H4Y1ig9brfwS9Qwt8wCNE96ueVhE");
                WebRequest request = WebRequest.Create(url);
                using (WebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                    {
                        DataSet dsResult = new DataSet();
                        dsResult.ReadXml(reader);
                        Address = dsResult.Tables["result"].Rows[0]["formatted_address"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                string ErrorMsg = ex.Message.ToString();

            }
            return Address;
        }
    }

}