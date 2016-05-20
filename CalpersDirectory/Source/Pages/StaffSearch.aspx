<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffSearch.aspx.cs" Inherits="Source.Pages.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>CalPERS Directory and Map</title>

    <meta charset="utf-8"/>
    <meta name="copyright" content="2016"/>
    <meta name="geo.region" content="US-CA"/>
    <meta name="geo.placename" content="Sacramento"/>
    <meta name="description" content="For more than eight decades, CalPERS has built retirement and health security for state, school, and public agency members who invest their lifework in public service. Our pension fund serves more than 1.7 million members in the CalPERS retirement system and administers benefits for 1.4 million members and their families in our health program."/>
    <meta name="keywords" content="calpers, retirement, california, health, health security, public agency, members, state, school, state, health program, sacramento"/>

    <!--[if lt IE 9]>
        <script src="http://html5shiv.googlecode.com/svn/trunk/html5.js"></script>
    <![endif]-->

    <!--[if lt IE 7]> <div style='clear: both; height: 59px; padding: 0 0 0 15px; position: relative;'>
        <a href="http://www.microsoft.com/windows/internet-explorer/default.aspx?ocid=ie6_countdown_bannercode">
            <img src="http://www.theie6countdown.com/images/upgrade.jpg" border="0" height="42" width="820" alt=""/>
        </a>
    </div> <![endif]-->

    <link rel="shortcut icon" href="../Images/calpers.ico"/>
    <link rel="stylesheet" href="../Styles/master.css" media="screen"/>
    
    
    	<!-- demo -->
	<link href="../Keyboard/docs/css/bootstrap.min.css" rel="stylesheet">
	<link href="../Keyboard/docs/css/font-awesome.min.css" rel="stylesheet">
	<link href="../Keyboard/docs/css/demo.css" rel="stylesheet">

	<!-- jQuery & jQuery UI + theme (required) -->
	<link href="../Keyboard/docs/css/jquery-ui.min.css" rel="stylesheet">
	<script src="../Keyboard/docs/js/jquery-latest.min.js"></script>
	<script src="../Keyboard/docs/js/jquery-ui.min.js"></script>
	<script src="../Keyboard/docs/js/bootstrap.min.js"></script>

	<!-- keyboard widget css & script (required) -->
	<link href="../Keyboard/css/keyboard.css" rel="stylesheet">
	<script src="../Keyboard/dist/js/jquery.keyboard.js"></script>

	<!-- keyboard extensions (optional) -->
	<script src="../Keyboard/dist/js/jquery.mousewheel.js"></script>
	<!--
	<script src="../Keyboard/js/jquery.keyboard.extension-typing.js"></script>
	<script src="../Keyboard/js/jquery.keyboard.extension-autocomplete.js"></script>
	-->

	<!-- initialize keyboard (required) -->
	<script>
		$(function(){
			$('#keyboard').keyboard();
		});
	</script>
    
    

</head>
<body data-vide-bg="/video/background">
<div id="staffSearchGridview">

    <form runat="server">

        <%--<input id="text" type="text" placeholder="    Tap here to find someone..."/>--%>
        <div>
            &nbsp;
        </div>

        <div id="wrap">
            <!-- wrapper only needed to center the input -->

            <!-- keyboard input -->
<%--            <input id="keyboard1" type="text">--%>

        </div> <!-- End wrapper -->

        Search:
        <asp:TextBox ID="keyboard" runat="server" Width="154px"/>
        <asp:Button Text="Search" runat="server" OnClick="Search" Width="83px"/>
        <hr/>
        <asp:GridView ID="gvStaffSearch" runat="server" AutoGenerateColumns="False" AllowPaging="True" CellSpacing="12" BackColor="White"
                      OnRowDataBound="OnRowDataBound" OnPageIndexChanging="OnPageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White"/>
            <Columns>

                <asp:BoundField Visible="False" HeaderStyle-Width="150px" DataField="EntityKey" HeaderText="Entity Key" ItemStyle-CssClass="ContactName" HtmlEncode="false">
                    <HeaderStyle Width="150px"></HeaderStyle>
                </asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton 
                            ID="lnkEditThreshold" OnClick="lnkVisitProfile_Click" runat="server" Text="Profile" ToolTip="Profile" CommandArgument='<%# Eval("EntityKey") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>



                <asp:BoundField HeaderStyle-Width="150px" DataField="FirstName" HeaderText="First Name">
                    <HeaderStyle Width="150px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField HeaderStyle-Width="150px" DataField="LastName" HeaderText="Last Name">
                    <HeaderStyle Width="150px"></HeaderStyle>
                </asp:BoundField>
            </Columns>
            <EditRowStyle BackColor="#2461BF"/>
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"/>
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White"/>
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center"/>
            <RowStyle BackColor="#EFF3FB"/>
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333"/>
            <SortedAscendingCellStyle BackColor="#F5F7FB"/>
            <SortedAscendingHeaderStyle BackColor="#6D95E1"/>
            <SortedDescendingCellStyle BackColor="#E9EBEF"/>
            <SortedDescendingHeaderStyle BackColor="#4870BE"/>
        </asp:GridView>
    </form>


</div>
<asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="ConvertEntitiesForDisplay" TypeName="Source.Classes.DataRetrieval.EntityDataAction"></asp:ObjectDataSource>
</body>
</html>