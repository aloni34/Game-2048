using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Threading.Tasks;
using System.Threading;
using System.Data;

public partial class _2048 : System.Web.UI.Page
{
    public static Game game = new Game();
    public static cell c = new cell();
    public static string x, score, str = "0", test = "";
    public Directions direct = new Directions();
    public Tuple<int, int[,], Boolean> aTuple;


    protected void Page_Load(object sender, EventArgs e) // played all the time when the user load the page, however we use this page only when the user logged to the page in the first time
    {
        if (!IsPostBack) // First time the user opens the page the following code will happen
        {
            Create();
            High_score();
        }
    }
    protected void Up_click(object sender, EventArgs e) // Play in the case the user pressed the up button
    {
        Failed();
        Direction_function("↑");
        x = game.Display();
        score = game.get_score().ToString();
    }
    protected void Right_click(object sender, EventArgs e) // Play in the case the user pressed the right button
    {
        Failed();
        Direction_function("→");
        x = game.Display();
        score = game.get_score().ToString();
    }
    protected void Down_click(object sender, EventArgs e) // Play in the case the user pressed the down button
    {
        Failed();
        Direction_function("↓");
        x = game.Display();
        score = game.get_score().ToString();
    }
    protected void Left_click(object sender, EventArgs e) // Play in the case the user pressed the left button
    {
        Failed();
        Direction_function("←");
        x = game.Display();
        score = game.get_score().ToString();
    }
    protected void Restart_click(object sender, EventArgs e) // Play the game from zero
    {
        Create();
    }
    protected void Random_function(int length) // Take the array from game and make a random number in the array where there is 0 and return back the array to game
    {
        for(int i = 0; i < length; i++)
        {
            game.set_array(c.Create_random_number(game.get_array()));
        }
    }
    protected void Direction_function(string move) // Choose which function to play based on the string which is sent, then takes high score from the data base and check if the user failed
    {
        aTuple = direct.On_move(game.get_score(), game.get_array(), move);
        game.set_score(aTuple.Item1);
        if (aTuple.Item3)
        {
            Random_function(1);
            High_score();
            Failed();
        }
    }
    protected void Create()
    {
        ResetValues();
        Random_function(2); // Create 2 numbers inside the array in game
        x = game.Display();
        score = game.get_score().ToString();
    }
    protected void ResetValues() // Reset all the values in any objects (at the moment only in game)
    {
        game.Reset_Values();
    }  
   
    protected void Failed()
    {
        Boolean check = false;
        int[,] array = game.get_array();
        for (int i = 0; i < array.GetLength(0); i++) // Check if there is a 0 value in one of the indexes in the array   
        {
            for (int j = 0; j < array.GetLength(1); j++)
            {
                if (array[i, j] == 0)
                {
                    check = true;
                }
            }
        }
        if(!check) // If none of the indexes contains 0
         {
           for(int i = 1; i < array.GetLength(0) - 1; i++) // Check the inside of the array (checks rows 1-2 and colmuns 1-2)
           {
              for(int j = 1; j < array.GetLength(1) - 1; j++)
              {
                 if(array[i,j] == array[i+1, j] || array[i, j] == array[i-1, j] || array[i, j] == array[i, j+1] || array[i, j] == array[i, j-1])
                 {
                      check = true;
                 }
              }
           }
         }  
        if (!check)
        {
            for(int i = 1; i < 3 && !check; i++) // Check the center of the first row, last row, first colmun and last colmun
            {
                if(array[0,i] == array[0, i+1] || array[0, i] == array[0, i-1] || array[0, i] == array[1, i]) // check index[0,1], index[0,2]
                {
                    check = true;
                }
                else if((array[3, i] == array[3, i + 1] || array[3, i] == array[3, i - 1] || array[3, i] == array[2, i]) && !check) // check index[3,1], index[3,2]
                {
                    check = true;
                }
                else if ((array[i, 0] == array[i+1, 0] || array[i, 0] == array[i-1, 0] || array[i, 0] == array[i, 1]) && !check) // check index[1,0], index[2,0]
                {
                    check = true;
                }
                else if ((array[i, 3] == array[i + 1, 3] || array[i, 1] == array[i - 1, 3] || array[i, 3] == array[i, 2]) && !check) // check index[1,3], index[2,]
                {
                    check = true;
                }
            }
            // Check the corners
            if ((array[0, 0] == array[0, 1] || array[0, 0] == array[1, 0]) && !check) // Check index[0,0]
            {
                check = true;
            }
            else if ((array[0, 3] == array[0, 2] || array[0, 3] == array[1, 3]) && !check) // Check index[0,3]
            {
                check = true;
            }
            else if ((array[3, 0] == array[3, 1] || array[3, 0] == array[2, 0]) && !check) // Check index[3,0]
            {
                check = true;
            }
            else if ((array[3, 3] == array[3, 2] || array[3, 3] == array[2, 3]) && !check) // Check index[3,3]
            {
                check = true;
            }
        }      
        if (!check)
        {
            int sum = 0;
            for (int i = 0; i < array.GetLength(0); i++) // Make 16 sesions as the number of indexes in the array
            {
                for (int j = 0; j < array.GetLength(1); j++)
                {
                    Session["x" + sum] = array[i,j];
                    sum++;
                }
            }                     
            Session["score"] = score;
            Response.Redirect("Switch.aspx");
        }
        
    }
    protected void High_score() // Retrive from database the highest score and place it in the variable test. It also checks if the active score is higher and if its higher it places the active score inside the data base
    {
        string highscore_string = "Select * from Tablex";
        DataTable dt = MyAdoHelper.ExecuteSelect(highscore_string);
        for (int i = 0; i < dt.Rows.Count; i++)
        {
            for (int j = 0; j < dt.Columns.Count; j++)
            {
                test = "" + dt.Rows[i][j];
            }
        }
        int score_ = Convert.ToInt32(test);
        if (score_ < game.get_score())
        {
            test = ""+ game.get_score();
            highscore_string = "UPDATE Tablex set score='" + game.get_score() + "' where Id='" + "player" + "'";
            MyAdoHelper.DoQuery(highscore_string);
        }
    }













 //  ------------------------------------------------- Class 'Game' -------------------------------------------------------

    public class Game
    {
            int score;
            int[,] array;
            string x;
            public Game()
            {
                this.score = 0;
                this.array = new int[4, 4];
                this.x = "";
            }
            public int[,] get_array()
            {
                return this.array;
            }
            public int get_score()
            {
              return this.score;
            }
            public void set_array(int[,] array_1)
            {
                this.array = array_1;
            }
            public void set_score(int score_1)
            {
                this.score = score_1;
            }
            public void Reset_Values() // Reset all of the values in the class Game
            {
                this.score = 0;            
                this.x = "";
                for(int i = 0; i < array.GetLength(0); i++)
                {
                    for(int j = 0; j < array.GetLength(1); j++)
                    {
                        array[i, j] = 0;
                    }
                }
            }
            public string Display() // Create a string which displays the game of 2048 to the user
            {
                string c;
                this.x = "<b><table border ='4px solid Black' align='center' style=font-size:40px;width:400px;text-align:center;height:320px;border-collapse:separate;border-radius:6px;>";
                for (int i = 0; i < array.GetLength(0); i++)
                {
                    this.x += "<tr style=font-size:40px;width:200px;text-align:center;height:80px>";
                    for (int j = 0; j < array.GetLength(1); j++)
                    {
                        c = Draw(array[i, j]);
                        this.x += "<td style=width:25%;background-color:" + c + ">";
                        if (array[i, j] != 0)
                        {
                            this.x += array[i, j].ToString();
                        }
                        this.x += "</td>";
                    }
                    this.x += "</tr>";
                }
                this.x += "<table></b>";
                return x;
            }
            public string Draw(int number_index) // Choose color based on the index value from the array (*Works for 17 colors - highest number possible is 131072 in the base 2^17*)
            {
                String c = "AliceBlue";
                if (number_index == 2)
                {
                    c = "PeachPuff";
                }
                else if (number_index == 4)
                {
                    c = "Yellow";
                }
                else if (number_index == 8)
                {
                    c = "LightSeaGreen";
                }
                else if (number_index == 16)
                {
                    c = "LightSkyBlue";
                }
                else if (number_index == 32)
                {
                    c = "DodgerBlue";
                }
                else if (number_index == 64)
                {
                    c = "MediumOrchid";
                }
                else if (number_index == 128)
                {
                    c = "DeepPink";
                }
                else if (number_index == 256)
                {
                    c = "Red";
                }
                else if (number_index == 512)
                {
                    c = "DarkOrange";
                }
                else if (number_index == 1024)
                {
                    c = "Tomato";
                }
                else if (number_index == 2048)
                {
                    c = "greenyellow";
                }
                else if (number_index == 4096)
                {
                    c = "springgreen";
                }
                else if (number_index == 8192)
                {
                    c = "limegreen";
                }
                else if (number_index == 16384)
                {
                    c = "cyan";
                }
                else if (number_index == 32768)
                {
                    c = "lightskyblue";
                }
                else if (number_index == 65536)
                {
                    c = "dodgerblue";
                }
                else if (number_index == 131072)
                {
                    c = "blue";
                }
            return c;
            }
    }
    //  ------------------------------------------------- Class 'cell' -------------------------------------------------------

    public class cell
    {
        public int Random_Generator() // Create value of 2 or 4 based on random (2 is more common than 4)
        {
            Random rnd = new Random();
            int number = rnd.Next(0, 11);
            if (number > 7)
            {
                number = 4;
            }
            else
            {
                number = 2;
            }
            return number;
        }
        public int[,] Create_random_number(int[,] array) // Create a number and choose place to place it inside the array
        {
            int z2 = Random_Generator();
            int x1 = Random_place();
            int x2 = Random_place();
            while (array[x1, x2] != 0)
            {
                x1 = Random_place();
                x2 = Random_place();
            }
            array[x1, x2] = z2;
            return array;
        }
        public int Random_place() // Create index for the position of the array
        {
            Random rnd = new Random();
            int number = rnd.Next(0, 4);
            return number;
        }
    }
    //  ------------------------------------------------- Class 'Directions' -------------------------------------------------------

    public class Directions
    {
        public Tuple<int, int[,], Boolean> On_move(int score, int[,] array, string side) // Choose the correct function where to move based on the button which was pressed
        {          
            Boolean check = false;
            Tuple<int, int[,], Boolean> result = new Tuple<int, int[,], Boolean>(score, array, check);
            if (side == "↑")
            {
                result = on_up(score, array);
            }
            else if(side == "←")
            {
                result = on_left(score, array);
            }
            else if (side == "↓")
            {
                result = on_down(score, array);
            }
            else if(side == "→")
            {
                result = on_right(score, array);
            }
            return result;
        }
        public Tuple<int, int[,], Boolean> on_up(int score, int[,] array) // Check for equal values which can be combined and if possible it is moving values as up as possible
        {
            Boolean check = false; // Check if any move has been made
            for (int z = 0; z < 4; z++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    for (int j = 3; j > 0; j--)
                    {
                        if (array[j, i] != 0 && array[j - 1, i] == 0)
                        {
                            array[j - 1, i] = array[j, i];
                            array[j, i] = 0;
                            check = true;
                        }
                    }
                }
            }
            for (int i = 0; i <= 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    if (array[j + 1, i] == array[j, i] && array[j, i] != 0)
                    {
                        array[j, i] = array[j, i] + array[j + 1, i];
                        score += array[j, i];
                        array[j + 1, i] = 0;

                        check = true;
                    }
                }
            }
            for (int z = 0; z < 4; z++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    for (int j = 3; j > 0; j--)
                    {
                        if (array[j, i] != 0 && array[j - 1, i] == 0)
                        {
                            array[j - 1, i] = array[j, i];
                            array[j, i] = 0;
                            check = true;
                        }
                    }
                }
            }

            Tuple<int, int[,], Boolean> aTuple = new Tuple<int, int[,], Boolean>(score, array, check);
            return aTuple;
        }
        public Tuple<int, int[,], Boolean> on_down(int score, int[,] array) // Check for equal values which can be combined and if possible it is moving values as down as possible
        {
            Boolean check = false; // Check if any move has been made
            for (int z = 0; z < 4; z++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (array[j, i] != 0 && array[j + 1, i] == 0)
                        {
                            array[j + 1, i] = array[j, i];
                            array[j, i] = 0;
                            check = true;
                        }
                    }
                }
            }
            for (int i = 0; i <= 3; i++)
            {

                for (int j = 3; j > 0; j--)
                {
                    if (array[j - 1, i] == array[j, i] && array[j, i] != 0)
                    {
                        array[j, i] = array[j, i] + array[j - 1, i];
                        score += array[j, i];
                        array[j - 1, i] = 0;

                        check = true;
                    }
                }
            }
            for (int z = 0; z < 4; z++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (array[j, i] != 0 && array[j + 1, i] == 0)
                        {
                            array[j + 1, i] = array[j, i];
                            array[j, i] = 0;
                            check = true;
                        }
                    }
                }
            }
            Tuple<int, int[,], Boolean> aTuple = new Tuple<int, int[,], Boolean>(score, array, check);
            return aTuple;
        }
        public Tuple<int, int[,], Boolean> on_right(int score, int[,] array) // Check for equal values which can be combined and if possible it is moving values as right as possible
        {
            Boolean check = false; // Check if any move has been made
            for (int z = 0; z < 4; z++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (array[i, j] != 0 && array[i, j + 1] == 0)
                        {
                            array[i, j + 1] = array[i, j];
                            array[i, j] = 0;
                            check = true;
                        }
                    }
                }
            }
            for (int i = 0; i <= 3; i++)
            {

                for (int j = 3; j > 0; j--)
                {
                    if (array[i, j - 1] == array[i, j] && array[i, j] != 0)
                    {
                        array[i, j] = array[i, j] + array[i, j - 1];
                        score += array[i, j];
                        array[i, j - 1] = 0;

                        check = true;
                    }
                }
            }
            for (int z = 0; z < 4; z++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    for (int j = 0; j < 3; j++)
                    {
                        if (array[i, j] != 0 && array[i, j + 1] == 0)
                        {
                            array[i, j + 1] = array[i, j];
                            array[i, j] = 0;
                            check = true;
                        }
                    }
                }
            }
            Tuple<int, int[,], Boolean> aTuple = new Tuple<int, int[,], Boolean>(score, array, check);
            return aTuple;
        }
        public Tuple<int, int[,], Boolean> on_left(int score, int[,] array) // Check for equal values which can be combined and if possible it is moving values as left as possible
        {
            Boolean check = false; // Check if any move has been made
            for (int z = 0; z < 4; z++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    for (int j = 3; j > 0; j--)
                    {
                        if (array[i, j] != 0 && array[i, j - 1] == 0)
                        {
                            array[i, j - 1] = array[i, j];
                            array[i, j] = 0;
                            check = true;
                        }
                    }
                }
            }
            for (int i = 0; i <= 3; i++)
            {

                for (int j = 0; j < 3; j++)
                {
                    if (array[i, j + 1] == array[i, j] && array[i, j] != 0)
                    {
                        array[i, j] = array[i, j] + array[i, j + 1];
                        score += array[i, j];
                        array[i, j + 1] = 0;

                        check = true;
                    }
                }
            }
            for (int z = 0; z < 4; z++)
            {
                for (int i = 0; i <= 3; i++)
                {
                    for (int j = 3; j > 0; j--)
                    {
                        if (array[i, j] != 0 && array[i, j - 1] == 0)
                        {
                            array[i, j - 1] = array[i, j];
                            array[i, j] = 0;
                            check = true;
                        }
                    }
                }
            }
            Tuple<int, int[,], Boolean> aTuple = new Tuple<int, int[,], Boolean>(score, array, check);
            return aTuple;
        }
    }
}