<%@ Page Language="C#" AutoEventWireup="true" CodeFile="2048.aspx.cs" Inherits="_2048" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="Css/my.css" type="text/css"/>
    <title></title>
    <script type="text/javascript">           
    </script>
</head>
<body>
    <form id="form1" runat="server" action="2048.aspx">      
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
                    <asp:ImageButton ID="ImageButton1" runat="server" value="reset" style="font-size:24px; color:red;" onclick="Restart_click" AccessKey="8" ImageUrl="/Images/restart.png"/>
                </td>
                   
                 <td style="width:17%">  
                     <p style="font-size:20px;" class="board">
                         <strong>
                             Highest Score: <span style="text-align:left;color:red"><%=test %> </span> 
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
        </center>
        
        


       <center>
        <table style="text-align:center; width:16%;height:12%; background-color:cornsilk" border="0">
            <tr>
                <td>                 
                </td>
                <td>
                    <asp:ImageButton ID="Up" runat="server" value="↑" style="font-size:24px; color:red;" onclick="Up_click"  AccessKey="8"  ImageUrl="/Images/2.png"/>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td style="text-align:right; width:43%">
                    <asp:ImageButton ID="Left" runat="server" value="←" style="font-size:24px; color:red;" onclick="Left_click" AccessKey="4" ImageUrl="/Images/4.png"/>
                </td>
                <td style="width:14%">
                     <img src="Images/Playgame.png" />
                </td>
                <td style="text-align:left; width:43%">
                    <asp:ImageButton ID="Right" runat="server" value="→" style="font-size:24px; color:red;" onclick="Right_click" AccessKey="6" ImageUrl="/Images/1.png"/>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td>
                    <asp:ImageButton ID="Down" runat="server" value="↓" style="font-size:24px; color:red;" onclick="Down_click" AccessKey="2" ImageUrl="/Images/3.png"/>
                </td>
                <td>
                </td>
            </tr>
        </table>
        </center>
    
     
       
        
    </form>
   
     
</body>
</html>
