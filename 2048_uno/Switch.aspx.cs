using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

public partial class Switch : System.Web.UI.Page
{
    public static string x = "", score, highestscore;
    protected void Page_Load(object sender, EventArgs e)
    {
        Display_in_switch();
    }
    protected void NewGame_click(object sender, EventArgs e)
    {
        Response.Redirect("2048.aspx");
    }
    protected void Display_in_switch() // Display all of the strings in the 2048.aspx page which were displayed to the user
    {
        int sum = 0;
        string [,] array = new string[4, 4];
        for(int i = 0; i < array.GetLength(0); i++) // Retrive all of the values from sessions to array
        {
            for(int j = 0; j < array.GetLength(1); j++)
            {               
                array[i, j] = ""+Session["x" + sum];
                sum++;
            }
        }
        score = ""+Session["score"];
        sum = 0;
        string c;
        x = "<b><table border ='4px solid Black' align='center' style=font-size:40px;width:400px;text-align:center;height:320px;border-collapse:separate;border-radius:6px;>";
        for (int i = 0; i < array.GetLength(0); i++)
        {
            x += "<tr style=font-size:40px;width:200px;text-align:center;height:80px>";
            for (int j = 0; j < array.GetLength(1); j++)
            {
                c = Draw(array[i, j]);
                x += "<td style=width:25%;background-color:" + c + ">";
                x+= array[i, j];
                x += "</td>";
            }
            x += "</tr>";
        }
        x += "<table></b>";
        string highscore_string = "Select * from Tablex";
        DataTable dt = MyAdoHelper.ExecuteSelect(highscore_string);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                highestscore = "" + dt.Rows[i][j];
            }
        }
    }  
    public string Draw(string number_index) // Choose color based on the index value from the array (*Works for 17 colors - highest number possible is 131072 in the base 2^17*)
    {
        String c = "AliceBlue";
        if (number_index == "2")
        {
            c = "PeachPuff";
        }
        else if (number_index == "4")
        {
            c = "Yellow";
        }
        else if (number_index == "8")
        {
            c = "LightSeaGreen";
        }
        else if (number_index == "16")
        {
            c = "LightSkyBlue";
        }
        else if (number_index == "32")
        {
            c = "DodgerBlue";
        }
        else if (number_index == "64")
        {
            c = "MediumOrchid";
        }
        else if (number_index == "128")
        {
            c = "DeepPink";
        }
        else if (number_index == "256")
        {
            c = "Red";
        }
        else if (number_index == "512")
        {
            c = "DarkOrange";
        }
        else if (number_index == "1024")
        {
            c = "Tomato";
        }
        return c;
    }
}