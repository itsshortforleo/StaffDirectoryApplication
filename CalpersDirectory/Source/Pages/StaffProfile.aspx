<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="StaffProfile.aspx.cs" Inherits="Source.Pages.StaffProfile" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <meta charset="utf-8">
    <meta http-equiv="Content-Type" content="text/html">
    <title>Calpers Staff Profile</title>
    <meta name="author" content="Leonardo Lopez">
    <link rel="shortcut icon" href="../Images/calpers.ico"/>
    <link rel="stylesheet" type="text/css" media="all" href="../Styles/styles.css">
    <script type="text/javascript" src="../js/jquery/jquery.min.js"></script>
    
    
    	<!-- demo -->
<%--	<link href="../Keyboard/docs/css/bootstrap.min.css" rel="stylesheet">
	<link href="../Keyboard/docs/css/font-awesome.min.css" rel="stylesheet">
	<link href="../Keyboard/docs/css/demo.css" rel="stylesheet">--%>

	<!-- jQuery & jQuery UI + theme (required) -->
	<link href="../Keyboard/docs/css/jquery-ui.min.css" rel="stylesheet">
	<script src="../Keyboard/docs/js/jquery-latest.min.js"></script>
	<script src="../Keyboard/docs/js/jquery-ui.min.js"></script>
	<script src="../Keyboard/docs/js/bootstrap.min.js"></script>

	<!-- keyboard widget css & script (required) -->
	<link href="../Keyboard/css/keyboard.css" rel="stylesheet">
	<script src="../Keyboard/dist/js/jquery.keyboard.js"></script>

	<!-- keyboard extensions (optional) -->
<%--	<script src="../Keyboard/dist/js/jquery.mousewheel.js"></script>--%>
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

<body>
<form id="form1" runat="server">

    <div id="topbar">
        <a href="StaffSearch.aspx">Back to Search</a>
    </div>

    <div id="w">
        <div id="content" class="clearfix">
            <div id="userphoto">
                <img src="../Images/avatar.png" alt="default avatar">
            </div>
            <h1>Staff Profile</h1>

            <nav id="profiletabs">
                <ul class="clearfix">
                    <li>
                        <a href="#bio" class="sel">Contact Info</a>
                    </li>
                    <li>
                        <a href="#settings">Settings</a>
                    </li>
                    <li>
                        <a href="#activity">Extra Button 1</a>
                    </li>
                    <li>
                        <a href="#friends">Extra Button 2</a>
                    </li>
                </ul>
            </nav>

            <section id="bio">
                <asp:Panel class="corner" runat="server">

                    <table style="width: 100%" >
                        <tr>
                            <td class="label-align" >
                                <asp:Label CssClass="label" ID="lblName" runat="server" Text="Name:"></asp:Label>
                            </td>
                            <td class="data-align">
                                <asp:Label ID="lblNameValue" runat="server" Text="Sample Name"></asp:Label>
                            </td>
                            <td class="label-align" >
                                <asp:Label CssClass="label" ID="lblBuilding" runat="server" Text="Building:"></asp:Label>
                            </td>
                            <td class="data-align" >
                                <asp:Label ID="lblBuildingValue" runat="server" Text="Lincoln Plaza North"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label-align" >
                                <asp:Label CssClass="label" ID="lblJobTitle" runat="server" Text="Job Title:"></asp:Label>
                            </td>
                            <td class="data-align">
                                <asp:Label ID="lblJobTitleValue" runat="server" Text="Sample Job Title"></asp:Label>
                            </td>
                            <td class="label-align" >
                                <asp:Label CssClass="label" ID="lblFloor" runat="server" Text="Floor:"></asp:Label>
                            </td>
                            <td class="data-align">
                                <asp:Label ID="lblFloorValue" runat="server" Text="3"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label-align" >
                                <asp:Label CssClass="label" ID="lblDeskPhone" runat="server" Text="Desk Phone:"></asp:Label>
                            </td>
                            <td class="data-align">
                                <asp:Label ID="lblDeskPhoneValue" runat="server" Text="111-222-3333"></asp:Label>
                            </td>
                            <td class="label-align" >
                                <asp:Label CssClass="label" ID="lblUnit" runat="server" Text="Unit:"></asp:Label>
                            </td>
                            <td class="data-align">
                                <asp:Label ID="lblUnitValue" runat="server" Text="Sample Unit"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label-align" >
                                <asp:Label CssClass="label" ID="lblMobilePhone" runat="server" Text="Mobile Phone:"></asp:Label>
                            </td>
                            <td class="data-align">
                                <asp:Label ID="lblMobilePhoneValue" runat="server" Text="222-333-4444"></asp:Label>
                            </td>
                            <td class="label-align" >
                                <asp:Label CssClass="label" ID="lblManager" runat="server" Text="Manager:"></asp:Label>
                            </td>
                            <td class="data-align">
                                <asp:Label ID="lblManagerValue" runat="server" Text="Manager Name"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="label-align" >
                                <asp:Label CssClass="label" ID="lblEmail" runat="server" Text="Email:"></asp:Label>
                            </td>
                            <td class="data-align">
                                <asp:Label ID="lblEmailValue" runat="server" Text="sample@calpers.gov"></asp:Label>
                            </td>
                        </tr>
                    </table>
                            <asp:TextBox ID="keyboard" runat="server" Width="154px" Text="Hey, I'm here. Come get me!"/>
                       <asp:Button ID="SendSMS" runat="server" Text="Send Text Message!" OnClick="BtnSendTextMessage" CommandArgument="test string"/>
                     
                </asp:Panel>

            </section>

            <section id="activity" class="hidden">
                <p>Most recent actions:</p>

                <p class="activity">@10:15PM - Submitted a news article</p>

                <p class="activity">@9:50PM - Submitted a news article</p>

                <p class="activity">@8:15PM - Posted a comment</p>

                <p class="activity">
                    @4:30PM - Added <strong>someusername</strong> as a friend
                </p>

                <p class="activity">@12:30PM - Submitted a news article</p>
            </section>

            <section id="friends" class="hidden">
                <p>Friends list:</p>

                <ul id="friendslist" class="clearfix">
                    <li>
                        <a href="#"><img src="../Images/avatar.png" width="22" height="22"> Username</a>
                    </li>
                    <li>
                        <a href="#"><img src="../Images/avatar.png" width="22" height="22"> SomeGuy123</a>
                    </li>
                    <li>
                        <a href="#"><img src="../Images/avatar.png" width="22" height="22"> PurpleGiraffe</a>
                    </li>
                </ul>
            </section>

            <section id="settings" class="hidden">
                <p>Edit your user settings:</p>

                <p class="setting">
                    <span>E-mail Address <img src="../Images/edit.png" alt="*Edit*"> </span> lolno@gmail.com

                </p>

                <p class="setting">
                    <span>Language <img src="../Images/edit.png" alt="*Edit*"></span> English(US)
                </p>

                <p class="setting">
                    <span>Profile Status <img src="../Images/edit.png" alt="*Edit*"></span> Public
                </p>

                <p class="setting">
                    <span>Update Frequency <img src="../Images/edit.png" alt="*Edit*"></span> Weekly
                </p>

                <p class="setting">
                    <span>Connected Accounts <img src="../Images/edit.png" alt="*Edit*"></span> None
                </p>
            </section>
        </div><!-- @end #content -->
    </div><!-- @end #w -->
    <script type="text/javascript">
        $(function() {
            $('#profiletabs ul li a')
                .on('click',
                    function(e) {
                        e.preventDefault();
                        var newcontent = $(this).attr('href');

                        $('#profiletabs ul li a').removeClass('sel');
                        $(this).addClass('sel');

                        $('#content section')
                            .each(function() {
                                if (!$(this).hasClass('hidden')) {
                                    $(this).addClass('hidden');
                                }
                            });

                        $(newcontent).removeClass('hidden');
                    });
        });
    </script>
</form>

</body>
</html>