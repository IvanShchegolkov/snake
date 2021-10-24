using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Змейка
{
    public partial class Form1 : Form
    {
        private int rI, rJ;
        private PictureBox fruit;
        private PictureBox[] snake = new PictureBox[400];
        private Label labelScore;
        private int dirX;
        private int dirY;
        private int _weigth = 900;
        private int _heigth = 800;
        private int _sizeofsides = 40;
        private int score = 0;
        public Form1()
        {
            InitializeComponent();
            this.Text = "Snake";
            this.Width = _weigth;
            this.Height = _heigth;
            dirX = 1;
            dirY = 0;
            labelScore = new Label();
            labelScore.Text = "Score: 0";
            labelScore.Location = new Point(810, 10);
            this.Controls.Add(labelScore);
            snake[0] = new PictureBox();
            snake[0].Location = new Point(201, 201);
            snake[0].Size = new Size(_sizeofsides-1, _sizeofsides-1);
            snake[0].BackColor = Color.Red;
            this.Controls.Add(snake[0]);
            fruit = new PictureBox();
            fruit.BackColor = Color.Yellow;
            fruit.Size = new Size(_sizeofsides, _sizeofsides);
            _generateMap();
            _generateFruit();
            timer1.Tick += new EventHandler(_update);
            timer1.Interval = 200;
            timer1.Start();
            this.KeyDown += new KeyEventHandler(OKP);
        }



        private void _generateFruit()
        {
            Random r = new Random();
            rI = r.Next(0, _heigth-_sizeofsides);
            int tempI = rI % _sizeofsides;
            rI -= tempI;
            rJ = r.Next(0, _heigth - _sizeofsides);
            int tempJ = rJ % _sizeofsides;
            rJ -= tempJ;
            rI++;
            rJ++;
            fruit.Location = new Point(rI, rJ);
            this.Controls.Add(fruit);
        }


        private void _checkBorders()
        {
            if (snake[0].Location.X < 0)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelScore.Text = "Score" + score;
                dirX = 1;
            }
            if (snake[0].Location.X > _heigth)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelScore.Text = "Score" + score;
                dirX = -1;
            }
            if (snake[0].Location.Y < 0)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelScore.Text = "Score" + score;
                dirY = 1;
            }
            if (snake[0].Location.Y > _heigth)
            {
                for (int _i = 1; _i <= score; _i++)
                {
                    this.Controls.Remove(snake[_i]);
                }
                score = 0;
                labelScore.Text = "Score" + score;
                dirY = -1;
            }
        }


        private void _eatItself()
        {
            for(int _i = 1; _i < score; _i++)
            {
                if (snake[0].Location == snake[_i].Location)
                {
                    for(int _j = _i; _j <= score; _j++)
                    {
                        this.Controls.Remove(snake[_j]);
                    }
                    score = score - (score - _i + 1);
                }
            }
        }


        private void _eatFruit()
        {
            if (snake[0].Location.X == rI && snake[0].Location.Y == rJ)
            {
                labelScore.Text = "score: " + ++score;
                snake[score] = new PictureBox();
                snake[score].Location = new Point(snake[score - 1].Location.X + 40 * dirX, snake[score - 1].Location.Y-40*dirY);
                snake[score].Size = new Size(_sizeofsides, _sizeofsides);
                snake[score].BackColor = Color.Red;
                this.Controls.Add(snake[score]);
                _generateFruit();
            }
        }


        private void _generateMap()
        {
            for(int i = 0; i < _weigth / _sizeofsides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(0, _sizeofsides * i);
                pic.Size = new Size(_weigth - 100, 1);
                this.Controls.Add(pic);
            }
            for (int i = 0; i <= _heigth / _sizeofsides; i++)
            {
                PictureBox pic = new PictureBox();
                pic.BackColor = Color.Black;
                pic.Location = new Point(_sizeofsides * i, 0);
                pic.Size = new Size(1,_weigth);
                this.Controls.Add(pic);
            }
        }



        private void _moveSnake()
        {
            for(int i = score; i >= 1; i--)
            {
                snake[i].Location = snake[i - 1].Location;
            }
            snake [0].Location = new Point(snake[0].Location.X + dirX * (_sizeofsides), snake[0].Location.Y + dirY * (_sizeofsides));
            _eatItself();
        }


        private void _update(Object myObject, EventArgs eventsArgs)
        {
            _checkBorders();
            _eatFruit();
            _moveSnake();
            //Cube.Location = new Point(Cube.Location.X + dirX * _sizeofsides, Cube.Location.Y + dirY * _sizeofsides);
        }


        private void OKP(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode.ToString())
            {
                case "Right":
                    dirX = 1;
                    dirY = 0;
                    break;
                case "Left":
                    dirX = -1;
                    dirY = 0;
                    break;
                case "Up":
                    dirY = -1;
                    dirX = 0;
                    break;
                case "Down":
                    dirY = 1;
                    dirX = 0;
                    break;
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
