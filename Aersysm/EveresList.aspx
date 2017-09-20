<%@ Page Title="" Language="C#" MasterPageFile="~/adminSite.Master" AutoEventWireup="true" CodeBehind="EveresList.aspx.cs" Inherits="Services.admin.EveresList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <asp:GridView ID="GridView1" runat="server"  CellPadding="4" Width="920px" AutoGenerateColumns="False"
        ForeColor="#333333" GridLines="None" OnRowDataBound="GridView1_RowDataBound"     >
        <AlternatingRowStyle BackColor="White" />
        <Columns>
          
             <asp:BoundField DataField="EveresId" HeaderText="EveresId" SortExpression="EveresId"> </asp:BoundField>
             <asp:BoundField DataField="EveresName" HeaderText="EveresName" SortExpression="EveresName"> </asp:BoundField>
             <asp:BoundField DataField="HappenedDate" HeaderText="HappenedDate" SortExpression="HappenedDate"> </asp:BoundField>
             <asp:BoundField DataField="SendtoDate" HeaderText="SendtoDate" SortExpression="SendtoDate"> </asp:BoundField>
             <asp:BoundField DataField="HospId" HeaderText="HospId" SortExpression="HospId"> </asp:BoundField>
             <asp:BoundField DataField="HospDepId" HeaderText="HospDepId" SortExpression="HospDepId"> </asp:BoundField>
             <asp:BoundField DataField="ExamineState" HeaderText="HospDepId" SortExpression="ExamineState"> </asp:BoundField>
           

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
