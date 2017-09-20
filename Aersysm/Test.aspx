<%@ Page Title="" Language="C#" MasterPageFile="~/adminSite.Master" AutoEventWireup="true" CodeBehind="Test.aspx.cs" Inherits="Aersysm.Test" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

<a href="http://192.168.0.100:9400/Aers.svc/gethosps?rud=ru00000001">gethosps</a><br/><br/>


<a href="http://192.168.0.100:9400/Aers.svc/findnursnoevent?rud=ru00000002">findnursnoevent护理部未通过</a><br/><br/>
<a href="http://192.168.0.100:9400/Aers.svc/findnursevent?rud=ru00000002">findnursevent护理部已审核</a><br/><br/>
<a href="http://192.168.0.100:9400/Aers.svc/findnurswait?rud=ru00000002">findnurswait护理部审核中</a><br/><br/>
<a href="http://192.168.0.100:9400/Aers.svc/findnursreport?rud=ru00000002">findnursreport</a><br/><br/>
<a href="http://192.168.0.100:9400/Aers.svc/findnurswaitData?rud=ru00000002">findnurswaitData</a><br/><br/>

<a href="http://192.168.0.100:9400/Aers.svc/findndepnoevent?rud=ru00000001">findndepnoeven护士长未通过</a><br/><br/>
<a href="http://192.168.0.100:9400/Aers.svc/findndepevent?rud=ru00000001">findndepevent护士长已审核</a><br/><br/>
<a href="http://192.168.0.100:9400/Aers.svc/findndepwait?rud=ru00000001">findndepwait护士长审核中</a><br/><br/>
<a href="http://192.168.0.100:9400/Aers.svc/findndepreport?rud=ru00000001">findndepreport</a><br/><br/>

<a href="http://192.168.0.100:9400/Aers.svc/findyhinfo?eud=0000000561">findyhinfo</a><br/><br/>
<a href="http://192.168.0.100:9400/Aers.svc/statecode">statecode</a><br/><br/>

<a href="http://192.168.0.100:9400/Aers.svc/checkonekey?eud=ru00000001">checkonekey</a><br/><br/>

    
<a href="http://192.168.0.100:9400/Aers.svc/Gethospital">所有医院信息Gethospital</a><br/><br/>

    
<a href="http://192.168.0.100:9400/Aers.svc/getevecounts?rol=145&eud=ru00000002">护理部getevecounts</a><br/><br/>

<a href="http://192.168.0.100:9400/Aers.svc/getevecounts?rol=146&eud=ru00000001">护士长getevecounts</a><br/><br/>

<a href="http://192.168.0.100:9400/Aers.svc/getevecounts?rol=147&eud=ru00000003">省厅getevecounts</a><br/><br/>

<a href="http://192.168.0.100:9100/TrainService.svc/CourseFindOrderBySortField?FieldName=CourseHot&Number=4">护士学堂</a><br/><br/>
    
</asp:Content>
