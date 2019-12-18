using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Checkers
{
    public partial class Form1 : Form
    {
        private static AI firstAI;
        private static AI secondAI;

        private static bool isFirstPlayer = true;
        private static bool GUI = true;
        private static bool ifAlphaBeta = true;

        private static Board board;
        private static Move move;
        private static int firstDepth = 1, secondDepth =1;

        private static bool gameStopped = false;
        private static List<Move> playerMovements = new List<Move>();
        private static List<Move> playerBeatings = new List<Move>();

        private static int drawCounter = 60;
        private static int draw1=12, draw2=12;

        // Player controls
        private static bool moveDone = false;
        private static bool pawnChosen = false;
        private static int pawnX;
        private static int pawnY;
        private static int moveX;
        private static int moveY;

        private static PictureBox[,] fields = new PictureBox[8,8];



        public bool checkDraw()
        {
            if (board.CountPlr1() < draw1 || board.CountPlr2() < draw2)
            {
                draw1 = board.CountPlr1();
                draw2 = board.CountPlr2();
                drawCounter = 60;
            }
            else
            {
                drawCounter -= 1;
            }
            if (drawCounter == 0)
            {
                return true;
            }
            return false;
        }
        public void gameStart()
        {

            if(radioButton1.Checked)
                isFirstPlayer = true;
            if (radioButton2.Checked)
                isFirstPlayer = false;
            gameStopped = false;
            drawCounter = 60;
            draw1 = 12; draw2 = 12;
            board = new Board();
            //Console.WriteLine("First PC depth: ");
            secondDepth = Int32.Parse(textBox2.Text);
            firstDepth = Int32.Parse(textBox1.Text);
            firstAI = new AI(firstDepth, ifAlphaBeta, Int32.Parse(textBox4.Text));
            drawCounter = 60;
            //Console.WriteLine("Second PC depth ");
            secondAI = new AI(secondDepth, ifAlphaBeta, Int32.Parse(textBox5.Text));
            showBoard(board.checkPawns());
        }
        public void gameEnd()
        {
            if (board.CountPlr1() == board.CountPlr2())
            {
                Console.WriteLine("Draw");
                MessageBox.Show("Draw!", "Game result",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (board.CountPlr1() > board.CountPlr2())
            {
                Console.WriteLine("Player 1 won!");
                MessageBox.Show("Player 1 won!", "Game result",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else if (board.CountPlr2() > board.CountPlr1())
            {
                Console.WriteLine("Player 2 won!");
                MessageBox.Show("Player 2 won!", "Game result",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            showBoard(board.checkPawns());
        }
     
        //GUI  
        public Form1()
        {
            InitializeComponent();
            board = new Board();
            showBoard(board.checkPawns());
            textBox1.Text = "1";
            textBox2.Text = "1";
            textBox3.Text = "1";
            textBox4.Text = "1";
            textBox5.Text = "1";
            fields[0, 1] = f1; fields[0, 3] = f3; fields[0, 5] = f5; fields[0, 7] = f7;
            fields[1, 0] = f8; fields[1, 2] = f10; fields[1, 4] = f12; fields[1, 6] = f14;
            fields[2, 1] = f17; fields[2, 3] = f19; fields[2, 5] = f21; fields[2, 7] = f23;
            fields[3, 0] = f24; fields[3, 2] = f26; fields[3, 4] = f28; fields[3, 6] = f30;
            fields[4, 1] = f33; fields[4, 3] = f35; fields[4, 5] = f37; fields[4, 7] = f39;
            fields[5, 0] = f40; fields[5, 2] = f42; fields[5, 4] = f44; fields[5, 6] = f46;
            fields[6, 1] = f49; fields[6, 3] = f51; fields[6, 5] = f53; fields[6, 7] = f55;
            fields[7, 0] = f56; fields[7, 2] = f58; fields[7, 4] = f60; fields[7, 6] = f62;
	    }

        public void showBoard(int[,] board)
        {
            if (board[0, 1] == 1) f1.BackgroundImage = www.BackgroundImage; else if (board[0, 1] == 2) f1.BackgroundImage = bbb.BackgroundImage; else if (board[0, 1] == 3) f1.BackgroundImage = wwww.BackgroundImage; else if (board[0, 1] == 4) f1.BackgroundImage = bbbb.BackgroundImage; else f1.BackgroundImage = bf.BackgroundImage;
            if (board[0, 3] == 1) f3.BackgroundImage = www.BackgroundImage; else if (board[0, 3] == 2) f3.BackgroundImage = bbb.BackgroundImage; else if (board[0, 3] == 3) f3.BackgroundImage = wwww.BackgroundImage; else if (board[0, 3] == 4) f3.BackgroundImage = bbbb.BackgroundImage; else f3.BackgroundImage = bf.BackgroundImage;
            if (board[0, 5] == 1) f5.BackgroundImage = www.BackgroundImage; else if (board[0, 5] == 2) f5.BackgroundImage = bbb.BackgroundImage; else if (board[0, 5] == 3) f5.BackgroundImage = wwww.BackgroundImage; else if (board[0, 5] == 4) f5.BackgroundImage = bbbb.BackgroundImage; else f5.BackgroundImage = bf.BackgroundImage;
            if (board[0, 7] == 1) f7.BackgroundImage = www.BackgroundImage; else if (board[0, 7] == 2) f7.BackgroundImage = bbb.BackgroundImage; else if (board[0, 7] == 3) f7.BackgroundImage = wwww.BackgroundImage; else if (board[0, 7] == 4) f7.BackgroundImage = bbbb.BackgroundImage; else f7.BackgroundImage = bf.BackgroundImage;

            if (board[1, 0] == 1) f8.BackgroundImage = www.BackgroundImage; else if (board[1, 0] == 2) f8.BackgroundImage = bbb.BackgroundImage; else if (board[1, 0] == 3) f8.BackgroundImage = wwww.BackgroundImage; else if (board[1, 0] == 4) f8.BackgroundImage = bbbb.BackgroundImage; else f8.BackgroundImage = bf.BackgroundImage;
            if (board[1, 2] == 1) f10.BackgroundImage = www.BackgroundImage; else if (board[1, 2] == 2) f10.BackgroundImage = bbb.BackgroundImage; else if (board[1, 2] == 3) f10.BackgroundImage = wwww.BackgroundImage; else if (board[1, 2] == 4) f10.BackgroundImage = bbbb.BackgroundImage; else f10.BackgroundImage = bf.BackgroundImage;
            if (board[1, 4] == 1) f12.BackgroundImage = www.BackgroundImage; else if (board[1, 4] == 2) f12.BackgroundImage = bbb.BackgroundImage; else if (board[1, 4] == 3) f12.BackgroundImage = wwww.BackgroundImage; else if (board[1, 4] == 4) f12.BackgroundImage = bbbb.BackgroundImage; else f12.BackgroundImage = bf.BackgroundImage;
            if (board[1, 6] == 1) f14.BackgroundImage = www.BackgroundImage; else if (board[1, 6] == 2) f14.BackgroundImage = bbb.BackgroundImage; else if (board[1, 6] == 3) f14.BackgroundImage = wwww.BackgroundImage; else if (board[1, 6] == 4) f14.BackgroundImage = bbbb.BackgroundImage; else f14.BackgroundImage = bf.BackgroundImage;

            if (board[2, 1] == 1) f17.BackgroundImage = www.BackgroundImage; else if (board[2, 1] == 2) f17.BackgroundImage = bbb.BackgroundImage; else if (board[2, 1] == 3) f17.BackgroundImage = wwww.BackgroundImage; else if (board[2, 1] == 4) f17.BackgroundImage = bbbb.BackgroundImage; else f17.BackgroundImage = bf.BackgroundImage;
            if (board[2, 3] == 1) f19.BackgroundImage = www.BackgroundImage; else if (board[2, 3] == 2) f19.BackgroundImage = bbb.BackgroundImage; else if (board[2, 3] == 3) f19.BackgroundImage = wwww.BackgroundImage; else if (board[2, 3] == 4) f19.BackgroundImage = bbbb.BackgroundImage; else f19.BackgroundImage = bf.BackgroundImage;
            if (board[2, 5] == 1) f21.BackgroundImage = www.BackgroundImage; else if (board[2, 5] == 2) f21.BackgroundImage = bbb.BackgroundImage; else if (board[2, 5] == 3) f21.BackgroundImage = wwww.BackgroundImage; else if (board[2, 5] == 4) f21.BackgroundImage = bbbb.BackgroundImage; else f21.BackgroundImage = bf.BackgroundImage;
            if (board[2, 7] == 1) f23.BackgroundImage = www.BackgroundImage; else if (board[2, 7] == 2) f23.BackgroundImage = bbb.BackgroundImage; else if (board[2, 7] == 3) f23.BackgroundImage = wwww.BackgroundImage; else if (board[2, 7] == 4) f23.BackgroundImage = bbbb.BackgroundImage; else f23.BackgroundImage = bf.BackgroundImage;

            if (board[3, 0] == 1) f24.BackgroundImage = www.BackgroundImage; else if (board[3, 0] == 2) f24.BackgroundImage = bbb.BackgroundImage; else if (board[3, 0] == 3) f24.BackgroundImage = wwww.BackgroundImage; else if (board[3, 0] == 4) f24.BackgroundImage = bbbb.BackgroundImage; else f24.BackgroundImage = bf.BackgroundImage;
            if (board[3, 2] == 1) f26.BackgroundImage = www.BackgroundImage; else if (board[3, 2] == 2) f26.BackgroundImage = bbb.BackgroundImage; else if (board[3, 2] == 3) f26.BackgroundImage = wwww.BackgroundImage; else if (board[3, 2] == 4) f26.BackgroundImage = bbbb.BackgroundImage; else f26.BackgroundImage = bf.BackgroundImage;
            if (board[3, 4] == 1) f28.BackgroundImage = www.BackgroundImage; else if (board[3, 4] == 2) f28.BackgroundImage = bbb.BackgroundImage; else if (board[3, 4] == 3) f28.BackgroundImage = wwww.BackgroundImage; else if (board[3, 4] == 4) f28.BackgroundImage = bbbb.BackgroundImage; else f28.BackgroundImage = bf.BackgroundImage;
            if (board[3, 6] == 1) f30.BackgroundImage = www.BackgroundImage; else if (board[3, 6] == 2) f30.BackgroundImage = bbb.BackgroundImage; else if (board[3, 6] == 3) f30.BackgroundImage = wwww.BackgroundImage; else if (board[3, 6] == 4) f30.BackgroundImage = bbbb.BackgroundImage; else f30.BackgroundImage = bf.BackgroundImage;

            if (board[4, 1] == 1) f33.BackgroundImage = www.BackgroundImage; else if (board[4, 1] == 2) f33.BackgroundImage = bbb.BackgroundImage; else if (board[4, 1] == 3) f33.BackgroundImage = wwww.BackgroundImage; else if (board[4, 1] == 4) f33.BackgroundImage = bbbb.BackgroundImage; else f33.BackgroundImage = bf.BackgroundImage;
            if (board[4, 3] == 1) f35.BackgroundImage = www.BackgroundImage; else if (board[4, 3] == 2) f35.BackgroundImage = bbb.BackgroundImage; else if (board[4, 3] == 3) f35.BackgroundImage = wwww.BackgroundImage; else if (board[4, 3] == 4) f35.BackgroundImage = bbbb.BackgroundImage; else f35.BackgroundImage = bf.BackgroundImage;
            if (board[4, 5] == 1) f37.BackgroundImage = www.BackgroundImage; else if (board[4, 5] == 2) f37.BackgroundImage = bbb.BackgroundImage; else if (board[4, 5] == 3) f37.BackgroundImage = wwww.BackgroundImage; else if (board[4, 5] == 4) f37.BackgroundImage = bbbb.BackgroundImage; else f37.BackgroundImage = bf.BackgroundImage;
            if (board[4, 7] == 1) f39.BackgroundImage = www.BackgroundImage; else if (board[4, 7] == 2) f39.BackgroundImage = bbb.BackgroundImage; else if (board[4, 7] == 3) f39.BackgroundImage = wwww.BackgroundImage; else if (board[4, 7] == 4) f39.BackgroundImage = bbbb.BackgroundImage; else f39.BackgroundImage = bf.BackgroundImage;

            if (board[5, 0] == 1) f40.BackgroundImage = www.BackgroundImage; else if (board[5, 0] == 2) f40.BackgroundImage = bbb.BackgroundImage; else if (board[5, 0] == 3) f40.BackgroundImage = wwww.BackgroundImage; else if (board[5, 0] == 4) f40.BackgroundImage = bbbb.BackgroundImage; else f40.BackgroundImage = bf.BackgroundImage;
            if (board[5, 2] == 1) f42.BackgroundImage = www.BackgroundImage; else if (board[5, 2] == 2) f42.BackgroundImage = bbb.BackgroundImage; else if (board[5, 2] == 3) f42.BackgroundImage = wwww.BackgroundImage; else if (board[5, 2] == 4) f42.BackgroundImage = bbbb.BackgroundImage; else f42.BackgroundImage = bf.BackgroundImage;
            if (board[5, 4] == 1) f44.BackgroundImage = www.BackgroundImage; else if (board[5, 4] == 2) f44.BackgroundImage = bbb.BackgroundImage; else if (board[5, 4] == 3) f44.BackgroundImage = wwww.BackgroundImage; else if (board[5, 4] == 4) f44.BackgroundImage = bbbb.BackgroundImage; else f44.BackgroundImage = bf.BackgroundImage;
            if (board[5, 6] == 1) f46.BackgroundImage = www.BackgroundImage; else if (board[5, 6] == 2) f46.BackgroundImage = bbb.BackgroundImage; else if (board[5, 6] == 3) f46.BackgroundImage = wwww.BackgroundImage; else if (board[5, 6] == 4) f46.BackgroundImage = bbbb.BackgroundImage; else f46.BackgroundImage = bf.BackgroundImage;

            if (board[6, 1] == 1) f49.BackgroundImage = www.BackgroundImage; else if (board[6, 1] == 2) f49.BackgroundImage = bbb.BackgroundImage; else if (board[6, 1] == 3) f49.BackgroundImage = wwww.BackgroundImage; else if (board[6, 1] == 4) f49.BackgroundImage = bbbb.BackgroundImage; else f49.BackgroundImage = bf.BackgroundImage;
            if (board[6, 3] == 1) f51.BackgroundImage = www.BackgroundImage; else if (board[6, 3] == 2) f51.BackgroundImage = bbb.BackgroundImage; else if (board[6, 3] == 3) f51.BackgroundImage = wwww.BackgroundImage; else if (board[6, 3] == 4) f51.BackgroundImage = bbbb.BackgroundImage; else f51.BackgroundImage = bf.BackgroundImage;
            if (board[6, 5] == 1) f53.BackgroundImage = www.BackgroundImage; else if (board[6, 5] == 2) f53.BackgroundImage = bbb.BackgroundImage; else if (board[6, 5] == 3) f53.BackgroundImage = wwww.BackgroundImage; else if (board[6, 5] == 4) f53.BackgroundImage = bbbb.BackgroundImage; else f53.BackgroundImage = bf.BackgroundImage;
            if (board[6, 7] == 1) f55.BackgroundImage = www.BackgroundImage; else if (board[6, 7] == 2) f55.BackgroundImage = bbb.BackgroundImage; else if (board[6, 7] == 3) f55.BackgroundImage = wwww.BackgroundImage; else if (board[6, 7] == 4) f55.BackgroundImage = bbbb.BackgroundImage; else f55.BackgroundImage = bf.BackgroundImage;

            if (board[7, 0] == 1) f56.BackgroundImage = www.BackgroundImage; else if (board[7, 0] == 2) f56.BackgroundImage = bbb.BackgroundImage; else if (board[7, 0] == 3) f56.BackgroundImage = wwww.BackgroundImage; else if (board[7, 0] == 4) f56.BackgroundImage = bbbb.BackgroundImage; else f56.BackgroundImage = bf.BackgroundImage;
            if (board[7, 2] == 1) f58.BackgroundImage = www.BackgroundImage; else if (board[7, 2] == 2) f58.BackgroundImage = bbb.BackgroundImage; else if (board[7, 2] == 3) f58.BackgroundImage = wwww.BackgroundImage; else if (board[7, 2] == 4) f58.BackgroundImage = bbbb.BackgroundImage; else f58.BackgroundImage = bf.BackgroundImage;
            if (board[7, 4] == 1) f60.BackgroundImage = www.BackgroundImage; else if (board[7, 4] == 2) f60.BackgroundImage = bbb.BackgroundImage; else if (board[7, 4] == 3) f60.BackgroundImage = wwww.BackgroundImage; else if (board[7, 4] == 4) f60.BackgroundImage = bbbb.BackgroundImage; else f60.BackgroundImage = bf.BackgroundImage;
            if (board[7, 6] == 1) f62.BackgroundImage = www.BackgroundImage; else if (board[7, 6] == 2) f62.BackgroundImage = bbb.BackgroundImage; else if (board[7, 6] == 3) f62.BackgroundImage = wwww.BackgroundImage; else if (board[7, 6] == 4) f62.BackgroundImage = bbbb.BackgroundImage; else f62.BackgroundImage = bf.BackgroundImage;
            f1.Refresh(); f3.Refresh(); f5.Refresh(); f7.Refresh(); f8.Refresh(); f10.Refresh(); f12.Refresh(); f14.Refresh(); f17.Refresh(); f19.Refresh(); f21.Refresh(); f23.Refresh(); f24.Refresh(); f26.Refresh(); f28.Refresh(); f30.Refresh(); f33.Refresh(); f35.Refresh(); f37.Refresh(); f39.Refresh(); f40.Refresh(); f42.Refresh();
            f44.Refresh(); f46.Refresh(); f49.Refresh(); f51.Refresh(); f53.Refresh(); f55.Refresh(); f56.Refresh(); f58.Refresh(); f60.Refresh(); f62.Refresh();
           // Form1.ActiveForm.Refresh();
        }       

        //PC VS PC BUTTON
        private void button1_Click(object sender, EventArgs e)
        {
            //DateTime start = DateTime.Now;
            gameStart();
            while (board.endOfGame() < 0 && !checkDraw() && !gameStopped)
            {
                if (isFirstPlayer)
                {     
                    move = firstAI.choseMove(board, isFirstPlayer); 
                    board.makeMove(move);
                }
                else
                { 
                    move = secondAI.choseMove(board, isFirstPlayer); 
                    board.makeMove(move);
                }
                isFirstPlayer = !isFirstPlayer;
                if(GUI)
                showBoard(board.checkPawns());
               // board.showBoard();
                if(GUI)
                Application.DoEvents();
            }
            gameEnd();
           // Console.WriteLine(firstAI.numberOfNodes);
           // TimeSpan elapsedTime = DateTime.Now - start;
           // Console.WriteLine("Czas rozgrywki " +elapsedTime.TotalMilliseconds);
        }

        //STOP BUTTON
        private void button8_Click(object sender, EventArgs e)
        {
            gameStopped = true;
            board = new Board();
            showBoard(board.checkPawns());
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }

        // PC VS PLAYER
        private void button2_Click(object sender, EventArgs e)
        {
            gameStart();
            firstDepth = Int32.Parse(textBox3.Text);
            firstAI = new AI(firstDepth, ifAlphaBeta, Int32.Parse(textBox4.Text));

              while (board.endOfGame() < 0 && !checkDraw() && !gameStopped)
            {
                playerMovements.Clear();
                playerBeatings.Clear();
                if (isFirstPlayer)
                {
                    board.checkBeatings(true, playerBeatings);
                    board.getMovements(true, playerMovements);
                    while (!moveDone)
                    {                   
                        Application.DoEvents();
                    }
                    moveDone = false;   
                }
                else
                {
                    if (firstDepth < 11)
                    Thread.Sleep(500);
                    move = firstAI.choseMove(board, isFirstPlayer);
                    board.makeMove(move);
                }
                isFirstPlayer = !isFirstPlayer;
                showBoard(board.checkPawns());
                //board.showBoard();
            }
            gameEnd();
        }

        private void playerControl(int x, int y)
        {
            int[,] b = board.checkPawns();
            if (b[x, y] == 1 || b[x, y] == 3 ||b[x,y] == 2 || b[x, y]==4)
            {
                pawnX = x;
                pawnY = y;
                pawnChosen = true;
                for (int xx = 0; xx < 8; xx++)
                {
                    for (int yy = 0; yy < 8; yy++)
                    {
                        if (fields[xx,yy]!=null)
                            fields[xx, yy].Image = unsigned.Image;
                    }
                }
                fields[x, y].Image = signed.Image;
                foreach (Move move in playerMovements)
                {
                    if (move.getPrevX() == x && move.getPrevY() == y)
                        fields[move.getNewX(), move.getNewY()].Image = signed.Image;
                }
                foreach (Move move in playerBeatings)
                {
                     if (move.getPrevX() == x && move.getPrevY() == y)
                     fields[move.getNewX(), move.getNewY()].Image = signed.Image;
                }
            }
            else if(pawnChosen)
            {
                moveX = x;
                moveY = y;
                foreach (Move move in playerMovements)
                {
                    if (move.getPrevX() == pawnX && move.getPrevY() == pawnY && move.getNewX() == moveX && move.getNewY() == moveY && playerBeatings.Count() == 0)
                    {
                        moveDone = true;
                        pawnChosen = false;
                        board.makeMove(move);
                        for (int xx = 0; xx < 8; xx++)
                        {
                            for (int yy = 0; yy < 8; yy++)
                            {
                                if (fields[xx, yy] != null)
                                    fields[xx, yy].Image = unsigned.Image;
                            }
                        }
                    }
                }
                foreach (Move move in playerBeatings)
                {
                    if (move.getPrevX() == pawnX && move.getPrevY() == pawnY && move.getNewX() == moveX && move.getNewY() == moveY && !moveDone)
                    {
                        moveDone = true;
                        pawnChosen = false;
                        board.makeMove(move);
                        for (int xx = 0; xx < 8; xx++)
                        {
                            for (int yy = 0; yy < 8; yy++)
                            {
                                if (fields[xx, yy] != null)
                                    fields[xx, yy].Image = unsigned.Image;
                            }
                        }
                    }
                }
            }
            label4.Text = "Player pawn: x= " + pawnX + ", y= " + pawnY + ", move: x= " + moveX + ", y= " + moveY;
        }

        // Player moves on board
        private void f1_Click(object sender, EventArgs e)
        {
            playerControl(0, 1);
        }
        private void f3_Click(object sender, EventArgs e)
        {
            playerControl(0,3);
        }
        private void f5_Click(object sender, EventArgs e)
        {
            playerControl(0,5);
        }
        private void f7_Click(object sender, EventArgs e)
        {
            playerControl(0,7);
        }
        private void f8_Click(object sender, EventArgs e)
        {
            playerControl(1,0);
        }
        private void f10_Click(object sender, EventArgs e)
        {
            playerControl(1,2);
        }
        private void f12_Click(object sender, EventArgs e)
        {
            playerControl(1,4);
        }
        private void f14_Click(object sender, EventArgs e)
        {
            playerControl(1,6);
        }
        private void f17_Click(object sender, EventArgs e)
        {
            playerControl(2,1);
        }
        private void f19_Click(object sender, EventArgs e)
        {
            playerControl(2,3);
        }
        private void f21_Click(object sender, EventArgs e)
        {
            playerControl(2,5);
        }
        private void f23_Click(object sender, EventArgs e)
        {
            playerControl(2,7);
        }
        private void f24_Click(object sender, EventArgs e)
        {
            playerControl(3,0);
        }
        private void f26_Click(object sender, EventArgs e)
        {
            playerControl(3,2);
        }
        private void f28_Click(object sender, EventArgs e)
        {
            playerControl(3,4);
        }
        private void f30_Click(object sender, EventArgs e)
        {
            playerControl(3,6);
        }
        private void f33_Click(object sender, EventArgs e)
        {
            playerControl(4,1);
        }
        private void f35_Click(object sender, EventArgs e)
        {
            playerControl(4,3);
        }
        private void f37_Click(object sender, EventArgs e)
        {
            playerControl(4,5);
        }
        private void f39_Click(object sender, EventArgs e)
        {
            playerControl(4,7);
        }
        private void f40_Click(object sender, EventArgs e)
        {
            playerControl(5,0);
        }
        private void f42_Click(object sender, EventArgs e)
        {
            playerControl(5,2);
        }
        private void f44_Click(object sender, EventArgs e)
        {
            playerControl(5,4);
        }
        private void f46_Click(object sender, EventArgs e)
        {
            playerControl(5,6);
        }
        private void f49_Click(object sender, EventArgs e)
        {
            playerControl(6,1);
        }
        private void f51_Click(object sender, EventArgs e)
        {
            playerControl(6,3);
        }
        private void f53_Click(object sender, EventArgs e)
        {
            playerControl(6,5);
        }
        private void f55_Click(object sender, EventArgs e)
        {
            playerControl(6,7);
        }
        private void f56_Click(object sender, EventArgs e)
        {
            playerControl(7,0);
        }
        private void f58_Click(object sender, EventArgs e)
        {
            playerControl(7,2);
        }
        private void f60_Click(object sender, EventArgs e)
        {
            playerControl(7,4);
        }
        private void f62_Click(object sender, EventArgs e)
        {
            playerControl(7,6);
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void label10_Click_1(object sender, EventArgs e)
        {

        }

        private void wwww_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            gameStart();

            while (board.endOfGame() < 0 && !checkDraw() && !gameStopped)
            {
                playerMovements.Clear();
                playerBeatings.Clear();
                if (isFirstPlayer)
                {
                    board.checkBeatings(true, playerBeatings);
                    board.getMovements(true, playerMovements);
                    while (!moveDone)
                    {
                        Application.DoEvents();
                    }
                    moveDone = false;
                }
                else
                {
                    board.checkBeatings(false, playerBeatings);
                    board.getMovements(false, playerMovements);
                    while (!moveDone)
                    {
                        Application.DoEvents();
                    }
                    moveDone = false;
                }
                isFirstPlayer = !isFirstPlayer;
                showBoard(board.checkPawns());
            }
            gameEnd();
        }
    }
}
