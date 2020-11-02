<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebFormFront.aspx.cs" Inherits="BIPIT_WebFront.WebFormFront" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <asp:GridView ID="GridView1" runat="server">
            </asp:GridView>
            <div style="overflow: hidden;">
    <div style="width: 1000%;">
        <div style="float: left; width: 230px; height: 100px;"><asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList></div>
        <div style="float: left; width: 230px; height: 100px;"> <asp:DropDownList ID="DropDownList2" runat="server">
            </asp:DropDownList></div>
        <div style="float: left; width: 250px; height: 250px;"><asp:Calendar ID="Calendar1" runat="server"></asp:Calendar></div>
                <div style="float: left; width: 250px; height: 250px;"><asp:Calendar ID="Calendar2" runat="server"></asp:Calendar></div>
                <div style="float: left; width: 250px; height: 250px;"><asp:Calendar ID="Calendar3" runat="server"></asp:Calendar></div>

    </div>
</div>
                        
        </div>
        <asp:Button ID="Button1" runat="server" Text="Добавить" OnClick="Button1_Click1"/>
    </form>
</body>
</html>
