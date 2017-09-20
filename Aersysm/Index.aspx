<%@ Page Title="" Language="C#" MasterPageFile="~/adminSite.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="Services.admin.Index" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <br/>

    <asp:LinkButton ID="btn1" runat="server" OnClick="btn1_Click" >上报统计</asp:LinkButton>

    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

    <asp:LinkButton ID="btn2" runat="server" OnClick="btn2_Click" >按医院统计</asp:LinkButton>

    <br/>

    <br/><br/>

    <br/>

     <asp:GridView ID="GridView1" runat="server"  CellPadding="4" Width="920px"
        ForeColor="#333333" GridLines="None" OnRowDataBound="GridView1_RowDataBound"     >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
          

        </Columns>
       
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
       
    </asp:GridView>
</asp:Content>
