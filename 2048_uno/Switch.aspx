<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Switch.aspx.cs" Inherits="Switch" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Css/my.css" type="text/css"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <center>
            <div style="background-color:cadetblue">
            <div style="height:50px;">
                <img src="Images/2048game.png" width:"50%;" height:"100%;""/> 
            </div>
            <br /><br /><br />  <br /><br />                                 
        </center>
         <table border="0" style="width:100%;background-color:deepskyblue">
            <tr>
                <td style="width:33%;text-align:center"> 
                    <asp:ImageButton ID="ImageButton1" runat="server" value="reset" style="font-size:24px; color:red;" onclick="NewGame_click" AccessKey="8" ImageUrl="/Images/restart.png"/>
                </td>
                   
                 <td style="width:17%">  
                     <p style="font-size:20px;" class="board">
                         <strong>
                             Highest Score: <span style="text-align:left;color:red"><%= highestscore %></span>
                         </strong>                         
                    </p>  
                 </td>
                <td style="width:17%">
                    <p style="font-size:20px;" class="board">
                         <strong>
                             Score:<span style="text-align:left;color:aliceblue">  <%= score %> </span>
                         </strong>                         
                    </p>  
                </td>
                
                <td style="width:33%">
                    <strong>This game was made by passion, please relax and enjoy</strong>
                </td>
            </tr>
        </table>
        </div>
        <br /><br />
        <center>
            <%=  x %>
            <h1 style="color:red">GAME OVER</h1>
            <asp:ImageButton ID="ImageButton2" runat="server" value="reset" style="font-size:24px; color:red;" onclick="NewGame_click" AccessKey="8" ImageUrl="/Images/restart.png"/>
        </center>
        
    </form>
</body>
</html>
