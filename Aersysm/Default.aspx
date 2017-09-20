<%@ Page Title="主页" Language="C#" MasterPageFile="~/LoginSite.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="CreditWeb._Default" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <link href="Styles/Site.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    
    <div class="accountInfo">
                <fieldset class="login"> 
     <legend>帐户信息</legend>
                   
    <p>
      
        用户名：<br/><asp:TextBox ID="txtname" runat="server" 
            Width="150px"></asp:TextBox>
      <br/>
        密&nbsp;&nbsp; 码：<br/>
        <asp:TextBox ID="txtpwd" runat="server" 
            TextMode="Password" Width="150px"></asp:TextBox>
        <br/><asp:Button ID="bntlogin" runat="server" Text="登录" 
            onclick="bntlogin_Click" />
      <br/>
<br/>

      <br/>
      </p></fieldset> </div>
</asp:Content>
