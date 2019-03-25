<%@ Page Title="" Language="C#" MasterPageFile="~/PropertyNew.Master" AutoEventWireup="true"
    CodeBehind="ContactUs.aspx.cs" Inherits="Property.ContactUs" %>

<%@ Register TagName="ContactInfo" TagPrefix="uc" Src="~/Controls/ContactInfo.ascx" %>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="wrapper_new">
          <div class="row landing_page_p_pge">
        <div class="col-md-6 col-sm-6">
            <div class="contact_in_left">
                <uc:ContactInfo ID="ContactInfo" runat="Server"></uc:ContactInfo>
            </div>

        </div>
        <div class="col-md-6 col-sm-6">
            <div class="contact_in_right">
               <iframe src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d2882.2352395249277!2d-79.71648568408366!3d43.747211579118215!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x882b3d0a5285dc29%3A0xd36285823ecd7eda!2sHomelife+Miracle+Realty+Ltd.!5e0!3m2!1sen!2sin!4v1553337251963" width="100%" frameborder="0" style="border:0" allowfullscreen></iframe>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
