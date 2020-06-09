using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleGame1_10ICT
{
    public partial class FrmSimpleGame : Form
    {
        int x;  //x is the position on the x axis from the upper left corner 
        int y;  //y is the position on the y axis from the upper left corner 
        int dx;  //change in x position
        int dy; //change in y position
        int score;//keep a count of the number of times the ball hits the paddle
        int lives; //number of lives 
        bool left, right;
        int x1 = 260;
        public FrmSimpleGame()
        {
            InitializeComponent();
        }

        private void FrmSimpleGame_Load(object sender, EventArgs e)
        {
            //Loads the ball on the screen at the upper left corner of the window 
            x = 1;	//x coordinate of the ball starting point
            y = 175;	//y coordinate of the ball starting point 
            dx = 5;	//Speed of the ball in the x direction is 5
            dy = 5;	//Speed of the ball in the y direction is 5
            score = 0; //The initial score is 0 
            lives = 3;	//The initial number of lives is 3

        }

        private void FrmSimpleGame_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left) { left = true; }
            if (e.KeyData == Keys.Right) { right = true; }
        }

        private void FrmSimpleGame_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Left) { left = false; }
            if (e.KeyData == Keys.Right) { right = false; }

        }

        private void TmrPaddle_Tick(object sender, EventArgs e)
        {
            //if right arrow pressed and paddle has not passed 
            //the right side of the form
            if (right && (x1 + 50 < this.ClientSize.Width))
            {
                x1 += 5;//add 5 to x1
                //moves the paddle to the right 5
                imgPaddle.Location = new Point(x1, this.ClientSize.Height - 30);
            }
           
  		// if left arrow key pressed and paddle has not passed
           	//the left side of form
            if (left && (x1 > 0))
            {
                //move paddle 5 to the left
                x1 -= 5;
                imgPaddle.Location = new Point(x1, this.ClientSize.Height - 30);
    }

}

private void TmrBall_Tick(object sender, EventArgs e)
        {
            // move the ball's location to a new point 
            x = x + dx;//add to the x value
            y = y + dy;//add to the y value
            imgBall.Location = new Point(x, y);
           

            if (x < 0) //if ball on the left side
            {
                dx = -dx;// change x direction
            }
            if (x + 10 > this.ClientSize.Width)//ball on right side
            {
                dx = -dx;// change x direction
            }
            if (y < 0) //if ball at the top of the form
            {
                dy = -dy;// change y direction
            }
            //if the ball hits the bottom of the form 
            if (y > this.ClientSize.Height - 10)//ball at the bottom
            {
                // lose a life
                lives -= 1;
                //convert the integer lives to a string and display in lblLives
                lblLives.Text = lives.ToString();

                if (lives == 0)
                {
                    lblGameOver.Text = "Game Over";//display Game Over
                    TmrBall.Stop();//stop the timer
                }
                // reposition the ball if game not over
                x = 1;
                y = 175;
           
            }
            //check for ball hitting paddle
            if (imgBall.Bounds.IntersectsWith(imgPaddle.Bounds))
            {
                score += 1; //add 1 to the score
                            //convert the score to a string so we can display it in lblScore
                lblScore.Text = score.ToString();
                if (score >= 5)
                {
                    TmrBall.Interval = 10;
                }

                dy = -dy;//change the direction of the ball
            }
            //Check for ball hitting a block
            foreach(Control x in this.Controls)
            {
                if(x is PictureBox && x.Tag == "block")
                {
                    if (imgBall.Bounds.IntersectsWith(x.Bounds))
                    {
                        this.Controls.Remove(x);
                        dy = -dy;
                        score++;
                    }
                }
            }
        }
    }
}
