using Labirinter.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Labirinter
{
    public partial class FLabirinter : Form
    {

        Random random = new Random();

        public FLabirinter()
        {
            InitializeComponent();
        }

        static int moveLeft = 1;
        static int moveRight = 1;
        static int moveTop = 1;
        static int moveDown = 1;
        static int provWall = 0;
        static int wallDown = 0;
        static int wallTop = 0;
        static int wallLeft = 0;
        static int wallRight = 0;
        static bool isDie = false;
        static int[] direct = new int[100];
        static int[] speed = new int[100];
        static int[] spawn = new int[7];
        static Button[] players = new Button[10];
        static int i = 0;
        static int b = 0;
        static int y = 0;
        static int dieLeft;
        static int dieTop;
        static int dies = 0;
        static bool isInStore = false;
        static bool isInComputer = false;
        static int predTop = 5;
        static int predLeft = 5;
        static int scanStatus = 0;
        static bool mainComputer = false;
        static bool login = false;
        static bool password = false;
        static bool dieCheat = false;
        static bool doorCheat = false;
        static int key = 0;
        static SoundPlayer soundPlayer = new SoundPlayer();

        #region Load

        private void Form1_Load(object sender, EventArgs e)
        {
            players[0] = Player;
            players[1] = Player_1;
            players[2] = Player_2;
            players[3] = Player_3;
            players[4] = Player_4;
            players[5] = Player_5;
            players[6] = Player_6;
            players[7] = Player_7;
            players[8] = Player_8;
            players[9] = Player_9;

           

            for (int i = 0; i < direct.Length; i++)
            {
                direct[i] = 1;
            }

            lblDies.Text = "Dies: " + dies;

            speed[1] = 1;
            speed[9] = 5;
            speed[10] = 5;
            speed[11] = 5;
            speed[12] = 5;
            speed[13] = 3;
            speed[14] = 3;
            speed[15] = 3;
            speed[16] = 30;


            spawn[1] = 11;
            spawn[2] = 9;
            spawn[3] = 8;
            spawn[4] = 10;
            spawn[5] = 11;
            spawn[6] = 10;



            dieLeft = Player.Left;
            dieTop = Player.Top;
            Player.Left = dieLeft;
            Player.Top = dieTop;
        }


       

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 87)
            {
                timerTop.Enabled = true;
            }

            if (e.KeyValue == 83)
            {
                timerDown.Enabled = true;
            }

            if (e.KeyValue == 65)
            {
                timerLeft.Enabled = true;
            }

            if (e.KeyValue == 68)
            {
                timerRight.Enabled = true;
            }
            if (e.KeyValue == 192)
            {
                if (!btn_Cheat.Visible)
                {
                    btn_Cheat.Visible = true;
                    txtBox_Cheat.Visible = true;
                    txtBox_Cheat.BringToFront();
                }
                else
                {
                    btn_Cheat.Visible = false;
                    txtBox_Cheat.Visible = false;
                }
               
            }
        }

        private void btn_Cheat_Click(object sender, EventArgs e)
        {
            if (txtBox_Cheat.Text == "//exept.noDie();")
            {
                dieCheat = true;
                btn_Cheat.Visible = false;
                txtBox_Cheat.Visible = false;
                txtBox_Cheat.Text = "-";
            }
            else
            {
                if (txtBox_Cheat.Text == "//exept.withDie();")
                {
                    dieCheat = false;
                    btn_Cheat.Visible = false;
                    txtBox_Cheat.Visible = false;
                    txtBox_Cheat.Text = "-";
                }
                else
                {
                    if (txtBox_Cheat.Text == "//exept.openDoor();")
                    {
                        door_1.Width = 0;
                        door_2.Height = 0;
                        door_3.Width = 0;
                        door_4.Width = 0;
                        door_5.Width = 0;
                        doorCheat = true;

                        soundPlayer.Stream = Resources.Door_Open;
                        soundPlayer.Play();

                        btn_Cheat.Visible = false;
                        txtBox_Cheat.Visible = false;
                        txtBox_Cheat.Text = "-";
                    }
                    else
                    {
                        if (txtBox_Cheat.Text == "//exept.closeDoor();")
                        {
                            door_1.Width = 65;
                            door_2.Height = 80;
                            door_3.Width = 35;
                            door_4.Width = 30;
                            door_5.Width = 100;
                            doorCheat = false;

                            soundPlayer.Stream = Resources.Door_Open;
                            soundPlayer.Play();

                            btn_Cheat.Visible = false;
                            txtBox_Cheat.Visible = false;
                            txtBox_Cheat.Text = "-";
                        }
                        else
                        {
                            txtBox_Cheat.Text = "Wrong Cheat!";
                        }
                    }
                }
            }
        }
        

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 87)
            {
                timerTop.Enabled = false;
            }

            if (e.KeyValue == 83)
            {
                timerDown.Enabled = false;
            }

            if (e.KeyValue == 65)
            {
                timerLeft.Enabled = false;
            }

            if (e.KeyValue == 68)
            {
                timerRight.Enabled = false;
            }
        }

        #endregion



        #region Timers


        ///////// Таймеры ////////////////////////////////////////////////////////////////////////////////










        private void timerTop_Tick(object sender, EventArgs e)
        {
            Proverka();
            
            if (moveTop == 1)
            {
                Player.Top = Player.Top - 5;
            }

            Obnull();
        }

        private void timerDown_Tick(object sender, EventArgs e)
        {
            Proverka();
            
            if (moveDown == 1)
            {
                Player.Top = Player.Top + 5;
            }

          

            Obnull();
        }

        private void timerLeft_Tick(object sender, EventArgs e)
        {
            Proverka();

            if (moveLeft == 1)
            {
                Player.Left = Player.Left - 5;
            }

            Obnull();
        }

        private void timerRight_Tick(object sender, EventArgs e)
        {
            Proverka();
            
            if (moveRight == 1)
            {
                Player.Left = Player.Left + 5;
            }

            Obnull();
        }

        #endregion



        #region Walls/Moving


        /////////// Стены //////////////////////////////////////////////////////////////////////////








        private void Obnull()
        {
            moveDown = 1;
            moveLeft = 1;
            moveRight = 1;
            moveTop = 1;

        }
       
        private void Proverka()
        {
            FourWall(Player, wall_g1);
            FourWall(Player, wall_g2);
            FourWall(Player, wall_g3);
            FourWall(Player, wall_g4);

            ///////

            FourWall(Player, wall_1);
            FourWall(Player, wall_2);
            FourWall(Player, wall_3);
            FourWall(Player, wall_4);
            FourWall(Player, wall_5);
            FourWall(Player, wall_6);
            FourWall(Player, wall_7);
            FourWall(Player, wall_8);
            FourWall(Player, wall_9);
            FourWall(Player, wall_10);
            FourWall(Player, wall_11);
            FourWall(Player, wall_12);
            FourWall(Player, wall_13);
            FourWall(Player, wall_14);
            FourWall(Player, wall_15);
            FourWall(Player, wall_16);
            FourWall(Player, wall_17);
            FourWall(Player, wall_18);
            FourWall(Player, wall_19);
            FourWall(Player, wall_20);
            FourWall(Player, wall_21);
            FourWall(Player, wall_22);
            FourWall(Player, wall_23);
            FourWall(Player, wall_24);


            ///////


            FourWall(Player, glass_1);
            FourWall(Player, glass_2);
            FourWall(Player, glass_3);
            FourWall(Player, glass_4);
            FourWall(Player, glass_5);
            FourWall(Player, glass_6);
            FourWall(Player, glass_7);
            FourWall(Player, glass_8);
            FourWall(Player, glass_9);
            FourWall(Player, glass_10);
            FourWall(Player, glass_11);
            FourWall(Player, glass_12);
            FourWall(Player, glass_13);



            ///////


            FourWall(Player, pBox_Books1);
            FourWall(Player, pBox_Books2);
            FourWall(Player, pBox_Store);


            ///////


            if (door_1.Visible)
            {
                FourWall(Player, door_1);
            }
            if (door_2.Visible)
            {
                FourWall(Player, door_2);
            }
            if (door_3.Visible)
            {
                FourWall(Player, door_3);
            }
            if (door_4.Visible)
            {
                FourWall(Player, door_4);
            }
            if (door_5.Visible)
            {
                FourWall(Player, door_5);
            }


            if (pBar_Store.Value == 1)
            {
                soundPlayer.Stream = Resources.Book_Sound;
                soundPlayer.Play();
            }
            if (pBar_Computer.Value == 1)
            {
                soundPlayer.Stream = Resources.Pad_Clicking;
                soundPlayer.Play();

            }


            //////////////////////////////////


            ButtonClick(Player, btn_1, wall_9);
            ButtonClick(Player, btn_2, wall_10);
            ButtonClick(Player, btn_3, wall_11);
            ButtonClick(Player, btn_4, wall_14);
            ButtonClick(Player, btn_5, wall_14);
            ButtonClick(Player, btn_6, wall_17);


            //////////////////////////////////


            FourBrick(Player, blade_1);
            FourBrick(Player, blade_2);
            FourBrick(Player, blade_3);
            FourBrick(Player, blade_4);
            FourBrick(Player, blade_5);
            FourBrick(Player, blade_6);
            FourBrick(Player, blade_7);
            FourBrick(Player, blade_8);
            FourBrick(Player, blade_9);
            FourBrick(Player, blade_10);
            FourBrick(Player, blade_11);
            FourBrick(Player, blade_12);
            FourBrick(Player, blade_13);
            FourBrick(Player, blade_14);
            FourBrick(Player, blade_15);
            FourBrick(Player, blade_16);
            FourBrick(Player, blade_17);
            FourBrick(Player, blade_18);
            FourBrick(Player, blade_19);
            FourBrick(Player, blade_20);
            FourBrick(Player, blade_21);
            FourBrick(Player, blade_22);


            //////////////////////////////////


            FourChekPoint(Player, checkPoint1);
            FourChekPoint(Player, checkPoint2);
            FourChekPoint(Player, checkPoint3);
            FourChekPoint(Player, checkPoint4);

            FourChekPoint(Player, key_1);
            FourChekPoint(Player, key_2);

            FourChekPoint(Player, btn_Win);


            //////////////////////////////////



            IsInRoom(Player, pBox_Store, glass_7, pBox_Books1, wall_7, isInStore, pBar_Store);
            IsInRoom(Player, pBox_Pad, glass_9, pBox_Pad, wall_16, isInComputer, pBar_Computer);
            IsInRoom(Player, pBox_MainComputer, glass_2, pBox_MainComputer, wall_15, mainComputer , progressBarTrash);


            ///////////////

            PultPushing(Player, btn_Pult_1);
        }

        private void IsInRoom(Button pl, Button wllTop, Button wllDown, Button wllLeft, Button wllRight, bool bl, ProgressBar pBar)
        {
            if (pl.Left >= wllLeft.Left && pl.Left <= wllRight.Left && pl.Top >= wllTop.Top && pl.Top + pl.Height <= wllDown.Top)
            {
                bl = true;
                pBar.Visible = true;
            }
            else
            {
                bl = false;
                pBar.Visible = false;
                
            }

            if (bl)
            {
                string dwn = wllDown.Name;

                switch (dwn)
                {
                    case "glass_7":
                        InStore(Player);
                        break;
                    case "glass_9":
                        InComputer(Player);
                        break;
                    case "glass_2":

                        if (brick_15.Visible == false && scanStatus == 0)
                        {
                            brick_15.Visible = true;
                            brick_15.Top = 105;
                            door_4.Width = 30;
                            door_4.Visible = true;
                        }
                        else
                        {
                            if (scanStatus >= 4)
                            {
                                door_4.Width = 0;
                                txtBox_Login.Visible = true;
                                btn_Login.Visible = true;
                                txtBox_Password.Visible = true;
                                btn_Password.Visible = true;
                            }
                            
                        }
                        
                       
                        break;
                }
            }
            else
            {
                if (bl == mainComputer)
                {
                    txtBox_Login.Visible = false;
                    btn_Login.Visible = false;
                    txtBox_Password.Visible = false;
                    btn_Password.Visible = false;
                }
            }
         
            
        }


        private void InStore(Button pl)
        {
           
            if (pl. Left >= predLeft || pl.Top >= predTop || pl.Left <= predLeft || pl.Top <= predTop)
            {
                if (pBar_Store.Value != pBar_Store.Maximum)
                {
                    pBar_Store.Value++;
                }
                else
                {
                    pBar_Store.Visible = false;
                    btn_Question.Visible = true;
                    btn_Question.BringToFront();
                }
            }
            predLeft = pl.Left;
            predTop = pl.Top;
        }

        private void InComputer(Button pl)
        {

            if (pl.Left >= predLeft || pl.Top >= predTop || pl.Left <= predLeft || pl.Top <= predTop)
            {
                if (pBar_Computer.Value != pBar_Computer.Maximum )
                {
                    pBar_Computer.Value++;
                    
                }
                else
                {
                    if (door_1.Width != 0)
                    {
                        pBar_Computer.Visible = false;
                        txtBox_Answer.Visible = true;
                        btn_Answer.Visible = true;
                        txtBox_Answer.BringToFront();
                    }
                    pBar_Computer.Visible = false;
                }
            }
            predLeft = pl.Left;
            predTop = pl.Top;
        }


        private void PultPushing(Button pl, Button pult)
        {
            if (pult == btn_Pult_1)
            {
                if (pl.Top + pl.Height >= pult.Top && pl.Left <= pult.Left + pult.Width && pl.Left + pl.Width >= pult.Left && pl.Top + pl.Height <= pult.Top + pult.Height)
                {
                    pult.Top = 350;
                    pult.Height = 10;
                    pult.BackColor = Color.DarkSalmon;

                    if (door_2.Height != 0)
                    {
                    //    btn_Enter_Password.BringToFront();
                    //    btn_Enter_Login.BringToFront();
                    //    txtBox_Enter_Login.BringToFront();
                    //    txtBox_Enter_Password.BringToFront();

                        txtBox_Enter_Login.Visible = true;
                        btn_Enter_Login.Visible = true;
                        txtBox_Enter_Password.Visible = true;
                        btn_Enter_Password.Visible = true;
                    }
                    

                }
                FourWall(pl, pult);
            }
        }


        private void btn_Enter_Password_Click(object sender, EventArgs e)
        {
            if (txtBox_Enter_Password.Text == "2002")
            {
                btn_Enter_Password.BackColor = Color.LightGreen;
                password = true;
            }

            if (btn_Enter_Login.BackColor == Color.LightGreen)
            {
                if (login && password)
                {
                    SoundPlayer soundPlayer = new SoundPlayer();
                    soundPlayer.Stream = Resources.Door_Open;
                    soundPlayer.Play();


                    while (door_2.Height != 0)
                    {
                        door_2.Height--;
                    }
                }
                else
                {
                    Die(Player);
                }
            }
        }

        private void btn_Enter_Login_Click(object sender, EventArgs e)
        {

            if (txtBox_Enter_Login.Text == "Alex_Rod")
            {
                btn_Enter_Login.BackColor = Color.LightGreen;
                login = true;
            }

            if (btn_Enter_Password.BackColor == Color.LightGreen)
            {
                if (login && password)
                {
                    SoundPlayer soundPlayer = new SoundPlayer();
                    soundPlayer.Stream = Resources.Door_Open;
                    soundPlayer.Play();

                    txtBox_Enter_Login.Visible = false;
                    btn_Enter_Login.Visible = false;
                    txtBox_Enter_Password.Visible = false;
                    btn_Enter_Password.Visible = false;

                    while (door_2.Height != 0)
                    {
                        door_2.Height--;
                    }
                }
                else
                {
                    Die(Player);
                }
            }
        }

        private void btn_Answer_Click(object sender, EventArgs e)
        {
            btn_Answer.Visible = false;
            txtBox_Answer.Visible = false;

            if (txtBox_Answer.Text == "15")
            {
                SoundPlayer soundPlayer = new SoundPlayer();
                soundPlayer.Stream = Resources.Door_Open;
                soundPlayer.Play();

               

                while (door_1.Width != 0)
                {
                    door_1.Width--;
                }
            }
            else
            {
                Die(Player);
            }
        }


        private void FourWall(Button pl, Button wll)
        {
            ProvDown(pl, wll);
            ProvRight(pl, wll);
            ProvLeft(pl, wll);
            ProvTop(pl, wll);

            StopWall();
        }

        private void StopWall()
        {
            if (wallDown == 1)
            {
                moveDown = 0;
            }
            if (wallLeft == 1)
            {
                moveLeft = 0;
            }
            if (wallRight == 1)
            {
                moveRight = 0;
            }
            if (wallTop == 1)
            {
                moveTop = 0;
            }
        }







        private void ProvRight(Button pl, Button wll)
        {
            if (pl.Left + pl.Width >= wll.Left && pl.Top < wll.Height + wll.Top && pl.Top + pl.Height > wll.Top && pl.Left + wll.Width < wll.Left + wll.Width) // движение вправо внизу сверху  находится слева от стены
            {
                if (wll == wall_1)
                {
                    provWall = 1;
                }
                if (wll == wall_2)
                {
                    provWall = 2;
                }
                if (wll == wall_3)
                {
                    provWall = 3;
                }
                if (wll == wall_4)
                {
                    provWall = 4;
                }
                wallRight = 1;
            }
            else
            {
                wallRight = 0;
            }


        }

        private void ProvLeft(Button pl, Button wll)
        {
            if (pl.Left <= wll.Left + wll.Width && pl.Top < wll.Height + wll.Top && pl.Top + pl.Height > wll.Top && pl.Left + pl.Width > wll.Left) // движение влево внизу сверху  находится справа от стены
            {
                if (wll == wall_1)
                {
                    provWall = 1;
                }
                if (wll == wall_2)
                {
                    provWall = 2;
                }
                if (wll == wall_3)
                {
                    provWall = 3;
                }
                if (wll == wall_4)
                {
                    provWall = 4;
                }
                wallLeft = 1;
            }
            else
            {
                wallLeft = 0;
            }

        }

        private void ProvTop(Button pl, Button wll)
        {
            if (pl.Top <= wll.Top + wll.Height && pl.Left < wll.Width + wll.Left && wll.Left < pl.Left + pl.Width && pl.Top >= wll.Top) // движение вверх справа слева находится ниже стены
            {
                if (wll == wall_1)
                {
                    provWall = 1;
                }
                if (wll == wall_2)
                {
                    provWall = 2;
                }
                if (wll == wall_3)
                {
                    provWall = 3;
                }
                if (wll == wall_4)
                {
                    provWall = 4;
                }
                wallTop = 1;
            }
            else
            {
                wallTop = 0;
            }

        }

        private void ProvDown(Button pl, Button wll)
        {
            if (pl.Top + pl.Height >= wll.Top && pl.Left < wll.Width + wll.Left && wll.Left < pl.Left + pl.Width && pl.Top + pl.Height <= wll.Top) // движение вниз справа слева находится выше стены
            {
                if (wll == wall_1)
                {
                    provWall = 1;
                }
                if (wll == wall_2)
                {
                    provWall = 2;
                }
                if (wll == wall_3)
                {
                    provWall = 3;
                }
                if (wll == wall_4)
                {
                    provWall = 4;
                }

                wallDown = 1;
            }
            else
            {
                wallDown = 0;
            }


        }




        #endregion 



        #region Bricks/Lazers

        /////////// Кирпичи / Лазеры  ////////////////////////////////////////////////////////////////////////////////////////



        private void timerBrick_Tick(object sender, EventArgs e)
        {
            BrickMove(brick_1, wall_6, wall_g1);
            BrickMove(brick_2, wall_6, wall_g1);
            BrickMove(brick_3, wall_6, wall_g1);
            BrickMove(brick_4, wall_6, wall_g1);
            BrickMove(brick_5, wall_6, wall_g1);
            BrickMove(brick_6, wall_7, wall_9);
            BrickMove(brick_7, wall_7, wall_9);
            BrickMove(brick_8, wall_7, wall_9);
            BrickMove(brick_9, wall_7, wall_9);
            BrickMove(brick_10, wall_9, wall_10);
            BrickMove(brick_11, wall_9, wall_10);
            BrickMove(brick_12, wall_9, wall_10);
            BrickMove(brick_13, wall_9, wall_10);
            BrickMove(brick_14, wall_5, wall_7);
            BrickMove(brick_15, glass_3, wall_6);
            BrickMove(brick_16, blade_13, blade_10);
            BrickMove(brick_17, blade_11, blade_12);
            BrickMove(brick_18, blade_18, blade_16);
            BrickMove(brick_19, blade_17, blade_16);
            BrickMove(brick_20, wall_g2, wall_23);
            BrickMove(brick_21, wall_g2, wall_23);
            BrickMove(brick_22, wall_g2, wall_23);
            BrickMove(brick_23, wall_g2, wall_23);
            BrickMove(brick_24, wall_24, wall_g4);
            BrickMove(brick_25, wall_24, wall_g4);
            BrickMove(brick_26, wall_24, wall_g4);
            BrickMove(brick_27, wall_24, wall_g4);
            

            //////////////////////


            BrickMove(lzr_1, wall_10, wall_g4);
            BrickMove(lzr_2, wall_10, wall_g4);
            BrickMove(lzr_3, wall_10, wall_g4);
            BrickMove(lzr_4, wall_10, wall_g4);


            /////////////////////////////


            BulletMoving(bullet_1, wall_21, bullet_1_corpus.Top);
            BulletMoving(bullet_2, wall_21, bullet_2_corpus.Top);
            BulletMoving(bullet_3, wall_21, bullet_3_corpus.Top);
            BulletMoving(bullet_4, wall_21, bullet_4_corpus.Top);
            BulletMoving(bullet_5, wall_21, bullet_5_corpus.Top);
            BulletMoving(bullet_6, wall_22, bullet_6_corpus.Top);


            /////////////////////////////


            FourBrick(Player, brick_1);
            FourBrick(Player, brick_2);
            FourBrick(Player, brick_3);
            FourBrick(Player, brick_4);
            FourBrick(Player, brick_5);
            FourBrick(Player, brick_6);
            FourBrick(Player, brick_7);
            FourBrick(Player, brick_8);
            FourBrick(Player, brick_9);
            FourBrick(Player, brick_10);
            FourBrick(Player, brick_11);
            FourBrick(Player, brick_12);
            FourBrick(Player, brick_13);
            FourBrick(Player, brick_14);
            FourBrick(Player, brick_16);
            FourBrick(Player, brick_17);
            FourBrick(Player, brick_18);
            FourBrick(Player, brick_19);
            FourBrick(Player, brick_20);
            FourBrick(Player, brick_21);
            FourBrick(Player, brick_22);
            FourBrick(Player, brick_23);
            FourBrick(Player, brick_24);
            FourBrick(Player, brick_25);
            FourBrick(Player, brick_26);
            FourBrick(Player, brick_27);


            ///////////////////


            FourBrick(Player, bullet_1);
            FourBrick(Player, bullet_2);
            FourBrick(Player, bullet_3);
            FourBrick(Player, bullet_4);
            FourBrick(Player, bullet_5);
            FourBrick(Player, bullet_6);
            
            
            ///////////////////

            FourBrick(Player, lzr_1);
            FourBrick(Player, lzr_2);
            FourBrick(Player, lzr_3);
            FourBrick(Player, lzr_4);
            
        }
        

        private void BrickMove(Button br, Button wll, Button wll2)
        {
            ProvBrick(br);

            if (i <= 4 || i == 18 || i == 20 || i >= 23 && i <= 26)
            {
                if (direct[i] == 1)
                {
                    if (br.Top + br.Height + 1 <= wll.Top)
                    {
                        br.Top++;

                    }
                    else
                    {
                        direct[i] = 0;

                        if (i == 18 && brick_15.Visible == true)
                        {
                            scanStatus++;
                        }
                    }
                }

                if (direct[i] == 0)
                {
                    if (br.Top - 1 >= wll2.Top + wll2.Height)
                    {
                        br.Top--;
                    }
                    else
                    {
                        direct[i] = 1;

                        if (i == 18 && brick_15.Visible == true)
                        {
                            scanStatus++;
                            if (scanStatus >= 4 )
                            {
                                brick_15.Visible = false;

                                soundPlayer.Stream = Resources.Door_Open;
                                soundPlayer.Play();

                                txtBox_Login.Visible = true;
                                btn_Login.Visible = true;
                                txtBox_Password.Visible = true;
                                btn_Password.Visible = true;

                                while (door_4.Width != 0)
                                {
                                    door_4.Width--;
                                }
                            }
                           
                        }
                    }
                }

            }



            if (i >= 5 && i <= 8 || i == 19 || i == 21 || i == 22 || i >= 27 && i <= 30)
            {
                if (direct[i] == 1)
                {
                    if (br.Left + br.Width + 1 <= wll2.Left)
                    {
                        br.Left++;
                    }
                    else
                    {
                        direct[i] = 0;
                    }
                }

                if (direct[i] == 0)
                {
                    if (br.Left - 1 >= wll.Left + wll.Width)
                    {
                        br.Left--;
                    }
                    else
                    {
                        direct[i] = 1;
                    }
                }

            }

            speed[17] = 8;

            if (i >= 9 && i <= 12 || i == 17)
            {
                if (direct[i] == 1)
                {
                    if (br.Left + br.Width + 1 <= wll2.Left)
                    {
                        br.Left += speed[i];
                    }
                    else
                    {
                        direct[i] = 0;
                    }
                }

                if (direct[i] == 0)
                {
                    if (br.Left - 1 >= wll.Left + wll.Width)
                    {
                        br.Left -= speed[i];
                    }
                    else
                    {
                        direct[i] = 1;
                    }
                }

            }
            if (i >= 13 && i <= 16)
            {
                if (direct[i] == 1)
                {
                    if (br.Left + br.Width + 1 <= wll2.Left)
                    {
                        br.Width += speed[i];
                    }
                    else
                    {
                        direct[i] = 0;
                    }
                }

                if (direct[i] == 0)
                {
                    if (br.Left + br.Width - 1 >= wll.Left + wll.Width)
                    {
                        br.Width -= speed[i];
                    }
                    else
                    {
                        direct[i] = 1;
                    }
                }

            }

          

            
        }

        private void ProvBrick(Button br)
        {

            string num = (br).Name;
            switch (num)
            {
                case "brick_1":
                    i = 0;
                    break;
                case "brick_2":
                    i = 1;
                    break;
                case "brick_3":
                    i = 2;
                    break;
                case "brick_4":
                    i = 3;
                    break;
                case "brick_5":
                    i = 4;
                    break;
                case "brick_6":
                    i = 5;
                    break;
                case "brick_7":
                    i = 6;
                    break;
                case "brick_8":
                    i = 7;
                    break;
                case "brick_9":
                    i = 8;
                    break;
                case "brick_10":
                    i = 9;
                    break;
                case "brick_11":
                    i = 10;
                    break;
                case "brick_12":
                    i = 11;
                    break;
                case "brick_13":
                    i = 12;
                    break;
                case "brick_14":
                    i = 17;
                    break;
                case "brick_15":
                    i = 18;
                    break;
                case "brick_16":
                    i = 19;
                    break;
                case "brick_17":
                    i = 20;
                    break;
                case "brick_18":
                    i = 21;
                    break;
                case "brick_19":
                    i = 22;
                    break;
                case "brick_20":
                    i = 23;
                    break;
                case "brick_21":
                    i = 24;
                    break;
                case "brick_22":
                    i = 25;
                    break;
                case "brick_23":
                    i = 26;
                    break;
                case "brick_24":
                    i = 27;
                    break;
                case "brick_25":
                    i = 28;
                    break;
                case "brick_26":
                    i = 29;
                    break;
                case "brick_27":
                    i = 30;
                    break;


                ///////////////////////////////


                case "lzr_1":
                    i = 13;
                    break;
                case "lzr_2":
                    i = 14;
                    break;
                case "lzr_3":
                    i = 15;
                    break;
                case "lzr_4":
                    i = 16;
                    break;
            }
            
            
          
        }


        


        private void RightBrick(Button pl, Button br)
        {
            if (pl.Left + pl.Width > br.Left && pl.Top < br.Height + br.Top && pl.Top + pl.Height > br.Top && pl.Left + br.Width < br.Left + br.Width)
            {
                isDie = true;
                if (isDie)
                {
                    Die(pl);
                }

            }

        }

        private void LeftBrick(Button pl, Button br)
        {
            if (pl.Left < br.Left + br.Width && pl.Top < br.Height + br.Top && pl.Top + pl.Height > br.Top && pl.Left + pl.Width > br.Left)
            {
                isDie = true;
                if (isDie)
                {
                    Die(pl);
                }

            }

        }

        private void TopBrick(Button pl, Button br)
        {
            if (pl.Top < br.Top + br.Height && pl.Left < br.Width + br.Left && br.Left < pl.Left + pl.Width && pl.Top > br.Top)
            {
                isDie = true;
                if (isDie)
                {
                    Die(pl);
                }

            }

        }

        private void DownBrick(Button pl, Button br)
        {
            if (pl.Top + pl.Height > br.Top && pl.Left < br.Width + br.Left && br.Left < pl.Left + pl.Width && pl.Top + pl.Height < br.Top)
            {
                isDie = true;
                if (isDie)
                {
                    Die(pl);
                }

            }


        }
        
        private void FourBrick(Button pl, Button br)
        {
            RightBrick(pl, br);
            LeftBrick(pl, br);
            TopBrick(pl, br);
            DownBrick(pl, br);
        }


        #endregion



        #region Buttons

        ////////  Конпки   //////////////////////////////////////////////////////////////////////////////////////







        private void ButtonClick(Button pl, Button btn, Button wll)
        {
            
            if (btn.Left + btn.Width == wll.Left) // проверка стены справа
            {
                if (btn == btn_1)
                {
                    if (btn.BackColor != Color.Lime)
                    {
                        if (pl.Left + pl.Width >= btn.Left && pl.Top + pl.Height >= btn.Top && pl.Top <= btn.Top + btn.Height && pl.Left <= btn.Left + btn.Width) //слева сверху снизу слева
                        {
                            btn.Left = btn.Left + 5;
                            btn.Width = btn.Width - 5;
                            btn.BackColor = Color.Lime;

                            FourWall(pl, btn);

                            SoundPlayer soundPlayer = new SoundPlayer();
                            soundPlayer.Stream = Resources.Light_Sound;
                            soundPlayer.Play();

                            speed[9] = 2;
                            speed[10] = 2;
                            speed[11] = 2;
                            speed[12] = 2;
                        }
                    }
                    else
                    {
                        FourWall(pl, btn);
                    }
                }

                if(btn == btn_5)
                {
                    if (btn.BackColor != Color.Lime)
                    {
                        if (pl.Left + pl.Width >= btn.Left && pl.Top + pl.Height >= btn.Top && pl.Top <= btn.Top + btn.Height && pl.Left <= btn.Left + btn.Width) //слева сверху снизу слева
                        {
                            btn.Left = btn.Left + 5;
                            btn.Width = btn.Width - 5;
                            btn.BackColor = Color.Lime;

                            FourWall(pl, btn);

                            SoundPlayer soundPlayer = new SoundPlayer();
                            soundPlayer.Stream = Resources.Door_Open;
                            soundPlayer.Play();


                            while (door_4.Width != 0)
                            {
                                door_4.Width--;
                            }
                        }
                    }
                    else
                    {
                        FourWall(pl, btn);
                    }
                }
            }


            if (btn.Left == wll.Left + wll.Width) // проверка стены слева
            {
                if (btn == btn_2)
                {
                    if (btn.BackColor != Color.Lime)
                    {
                        if (pl.Left <= btn.Width + btn.Left && pl.Top + pl.Height >= btn.Top && pl.Top <= btn.Top + btn.Height && pl.Left + pl.Width >= btn.Left) //слева сверху снизу справа
                        {
                            btn.Width = btn.Width - 5;
                            btn.BackColor = Color.Lime;
                            pBox_electro1.Visible = false;
                            btn_3.BackColor = Color.OrangeRed;
                            lblSayBtn2.Visible = false;

                            FourWall(pl, btn);

                            SoundPlayer soundPlayer = new SoundPlayer();
                            soundPlayer.Stream = Resources.Electro_Sound;
                            soundPlayer.Play();
                        }
                    }
                    else
                    {
                        FourWall(pl, btn);
                    }
                }


                if (btn == btn_4)
                {
                    if (btn.BackColor != Color.Lime)
                    {
                        pBar_Store.Value = 0;
                        pBar_Store.Visible = false;
                        door_3.Visible = true;
                        door_3.Width =  35;
                        btn_Question.Visible = false;

                        if (pl.Left <= btn.Width + btn.Left && pl.Top + pl.Height >= btn.Top && pl.Top <= btn.Top + btn.Height && pl.Left + pl.Width >= btn.Left) //слева сверху снизу справа
                        {
                            btn.Width = btn.Width - 5;
                            btn.BackColor = Color.Lime;

                            SoundPlayer soundPlayer = new SoundPlayer();
                            soundPlayer.Stream = Resources.Door_Open;
                            soundPlayer.Play();

                           
                            while (door_3.Width != 0)
                            {
                                door_3.Width--;
                            }


                            FourWall(pl, btn);
                        }
                    }
                    else
                    {
                        FourWall(pl, btn);
                        
                    }


                }


                if (btn == btn_6)
                {
                    if (btn.BackColor != Color.LightGray)
                    {
                        if (btn.BackColor != Color.Lime)
                        {
                            if (pl.Left <= btn.Width + btn.Left && pl.Top + pl.Height >= btn.Top && pl.Top <= btn.Top + btn.Height && pl.Left + pl.Width >= btn.Left) //слева сверху снизу справа
                            {
                                btn.Width = btn.Width - 5;
                                btn.BackColor = Color.Lime;

                                SoundPlayer soundPlayer = new SoundPlayer();
                                soundPlayer.Stream = Resources.Door_Open;
                                soundPlayer.Play();

                                int j = 1;
                                while (door_5.Width != 0)
                                {
                                    if (j >= 1000000)
                                    {
                                        door_5.Left++;
                                        door_5.Width--;
                                    }
                                    else
                                    {
                                        j++;
                                    }
                                }


                                FourWall(pl, btn);
                            }

                        }
                        else
                        {
                            FourWall(pl, btn);
                        }
                    }
                    else
                    {
                        FourWall(pl, btn);
                    }
                }
            }


            if (btn.Top == wll.Top + wll.Height) // проверка стены сверху
            {
                if (btn == btn_3)
                {
                    if (btn.BackColor != Color.LightGray)
                    {
                        if (btn.BackColor != Color.Lime)
                        {
                            if (pl.Top <= btn.Height + btn.Top && pl.Left + pl.Width >= btn.Left && pl.Left <= btn.Left + btn.Width && pl.Top + pl.Height >= btn.Top) //сверху справа слева снизу
                            {
                                btn.Height = btn.Height - 5;
                                btn.BackColor = Color.Lime;
                                speed[16] = 8;

                                FourWall(pl, btn);

                                SoundPlayer soundPlayer = new SoundPlayer();
                                soundPlayer.Stream = Resources.Light_Sound;
                                soundPlayer.Play();
                            }
                        }
                        else
                        {
                            FourWall(pl, btn);
                        }
                    }
                    else
                    {
                        FourWall(pl, btn);
                    }
                }


               
            }


           
        }


        #endregion



        #region Light


        private void timerLight_Tick(object sender, EventArgs e)
        {
            LightMove(light_wall_1);
            LightMove(light_wall_2);
            LightMove(light_wall_3);
            LightMove(light_wall_4);
            LightMove(light_wall_5);
            LightMove(light_wall_6);
            LightMove(light_wall_7);
            LightMove(light_wall_8);
            LightMove(light_wall_9);
            LightMove(light_wall_10);
            LightMove(light_wall_11);
            LightMove(light_wall_12);


            /////////////////////////////


            FourLight(Player, light_wall_1);
            FourLight(Player, light_wall_2);
            FourLight(Player, light_wall_3);
            FourLight(Player, light_wall_4);
            FourLight(Player, light_wall_5);
            FourLight(Player, light_wall_6);
            FourLight(Player, light_wall_7);
            FourLight(Player, light_wall_8);
            FourLight(Player, light_wall_9);
            FourLight(Player, light_wall_10);
            FourLight(Player, light_wall_11);
            FourLight(Player, light_wall_12);
        }

        private void ProvLight(Button lt)
        {
            string num = (lt).Name;

            switch (num)
            {
                case "light_wall_1":
                    i = 17;
                    break;
                case "light_wall_2":
                    i = 18;
                    break;
                case "light_wall_3":
                    i = 19;
                    break;
                case "light_wall_4":
                    i = 20;
                    break;
                case "light_wall_5":
                    i = 21;
                    break;
                case "light_wall_6":
                    i = 22;
                    break;
                case "light_wall_7":
                    i = 23;
                    break;
                case "light_wall_8":
                    i = 24;
                    break;
                case "light_wall_9":
                    i = 25;
                    break;
                case "light_wall_10":
                    i = 26;
                    break;
                case "light_wall_11":
                    i = 27;
                    break;
                case "light_wall_12":
                    i = 28;
                    break;


            }
        }

        private void LightMove(Button lt)
        {
            ProvLight(lt);

            int k = 110;

            switch (i)
            {
                case 17:
                    Lighting(17, light_wall_1, k);
                    break;
                case 18:
                    Lighting(18, light_wall_2, k);
                    break;
                case 19:
                    Lighting(19, light_wall_3, k);
                    break;
                case 20:
                    Lighting(20, light_wall_4, k);
                    break;
                case 21:
                    Lighting(21, light_wall_5, k);
                    break;
                case 22:
                    Lighting(22, light_wall_6, k);
                    break;
                case 23:
                    Lighting(23, light_wall_7, k);
                    break;
                case 24:
                    Lighting(24, light_wall_8, k);
                    break;
                case 25:
                    Lighting(25, light_wall_9, k);
                    break;
                case 26:
                    Lighting(26, light_wall_10, 100);
                    break;
                case 27:
                    Lighting(27, light_wall_11, 150);
                    break;
                case 28:
                    Lighting(28, light_wall_12, 50);
                    break;
            }




        }

        private void Lighting(int i, Button lt_wall, int max)
        {
            speed[i]++;
            if (speed[i] >= max)
            {
                speed[i] = 0;

                if (lt_wall.Visible)
                {
                    lt_wall.Visible = false;
                }
                else
                {
                    lt_wall.Visible = true;
                }

            }
        }


        



        private void RightLight(Button pl, Button br)
        {
            if (pl.Left + pl.Width > br.Left && pl.Top < br.Height + br.Top && pl.Top + pl.Height > br.Top && pl.Left + br.Width < br.Left + br.Width && br.Visible == true)
            {
                isDie = true;
                if (isDie)
                {
                    Die(pl);
                }

            }

        }

        private void LeftLight(Button pl, Button br)
        {
            if (pl.Left < br.Left + br.Width && pl.Top < br.Height + br.Top && pl.Top + pl.Height > br.Top && pl.Left + pl.Width > br.Left && br.Visible == true)
            {
                isDie = true;
                if (isDie)
                {
                    Die(pl);
                }

            }

        }

        private void TopLight(Button pl, Button br)
        {
            if (pl.Top < br.Top + br.Height && pl.Left < br.Width + br.Left && br.Left < pl.Left + pl.Width && pl.Top > br.Top && br.Visible == true)
            {
                isDie = true;
                if (isDie)
                {
                    Die(pl);
                }

            }

        }

        private void DownLight(Button pl, Button br)
        {
            if (pl.Top + pl.Height > br.Top && pl.Left < br.Width + br.Left && br.Left < pl.Left + pl.Width && pl.Top + pl.Height < br.Top && br.Visible == true)
            {
                isDie = true;
                if (isDie)
                {
                    Die(pl);
                }

            }


        }



        private void FourLight(Button pl, Button br)
        {
            RightLight(pl, br);
            LeftLight(pl, br);
            TopLight(pl, br);
            DownLight(pl, br);
        }




        #endregion



        #region  Check Points

        private void Spawn(Button br)
        {
            if (br == checkPoint1)
            {
                dieLeft = 1175;
                dieTop = 290;
                checkPoint1.BackColor = Color.LightGreen;
            }
            if (br == checkPoint2)
            {
                dieLeft = 765;
                dieTop = 315;
                checkPoint2.BackColor = Color.LightGreen;
            }
            if (br == checkPoint3)
            {
                dieLeft = 375;
                dieTop = 315;
                checkPoint3.BackColor = Color.LightGreen;
            }
            if (br == checkPoint4)
            {
                dieLeft = 30;
                dieTop = 560;
                checkPoint4.BackColor = Color.LightGreen;
            }
            if ((br == key_1 || br == key_2) && br.BackColor != Color.LightGreen)
            {
                br.BackColor = Color.LightGreen;
                key++;

                if (key >= 2)
                {
                    SoundPlayer soundPlayer = new SoundPlayer();
                    soundPlayer.Stream = Resources.Electro_Sound;
                    soundPlayer.Play();

                    btn_6.BackColor = Color.OrangeRed;
                }
            }

            if (br == btn_Win)
            {
                btn_Win.BackColor = Color.LightGreen;
                dieCheat = true;

                SoundPlayer soundPlayer = new SoundPlayer();
                soundPlayer.Stream = Resources.mlg___big_boss_doctrine_remix__zaycev_net_;
                soundPlayer.Play();

                timer_Win.Enabled = true;
            }

        }

        private void timer_Win_Tick(object sender, EventArgs e)
        {
            timerDown.Enabled = false;
            timerTop.Enabled = false;
            timerRight.Enabled = false;
            timerLeft.Enabled = false;


            timerBrick.Enabled = false;
            timerLight.Enabled = false;



            for (int i = 0; i < players.Length; i++)
            {
                players[i].BringToFront();
                players[i].Left = random.Next(0, 1205);
                players[i].Top = random.Next(0, 635);
                players[i].BringToFront();
            }
        }

        private void RightChekPoint(Button pl, Button br)
        {
            if (pl.Left + pl.Width >= br.Left && pl.Top < br.Height + br.Top && pl.Top + pl.Height > br.Top && pl.Left + br.Width < br.Left + br.Width)
            {
                Spawn(br);
            }

        }

        private void LeftChekPoint(Button pl, Button br)
        {
            if (pl.Left <= br.Left + br.Width && pl.Top < br.Height + br.Top && pl.Top + pl.Height > br.Top && pl.Left + pl.Width > br.Left)
            {
                Spawn(br);
            }

        }

        private void TopChekPoint(Button pl, Button br)
        {
            if (pl.Top <= br.Top + br.Height && pl.Left < br.Width + br.Left && br.Left < pl.Left + pl.Width && pl.Top >= br.Top)
            {
                Spawn(br);
            }

        }

        private void DownChekPoint(Button pl, Button br)
        {
            if (pl.Top + pl.Height >= br.Top && pl.Left < br.Width + br.Left && br.Left < pl.Left + pl.Width && pl.Top + pl.Height <= br.Top)
            {
                Spawn(br);
            }


        }



        private void FourChekPoint(Button pl, Button br)
        {
            RightChekPoint(pl, br);
            LeftChekPoint(pl, br);
            TopChekPoint(pl, br);
            TopChekPoint(pl, br);
        }

       

        #endregion



        #region Bullets

        private void BulletMoving(Button bul, Button wll, int top)
        {
            ProvBullet(bul);

           
            if (bul.Left > 110)
            {
                bul.Left = bul.Left - spawn[b];
            }
            else
            {

                bul.Left = wll.Left - bul.Width;
                bul.Top = top;
            }
        }

        private void ProvBullet(Button bul)
        {

            string num = (bul).Name;
            switch (num)
            {
                case "bullet_1":
                    b = 1;
                    break;
                case "bullet_2":
                    b = 2;
                    break;
                case "bullet_3":
                    b = 3;
                    break;
                case "bullet_4":
                    b = 4;
                    break;
                case "bullet_5":
                    b = 5;
                    break;
                case "bullet_6":
                    b = 6;
                    break;
            }
            
        }

            #endregion



        #region Die

            private void Die(Button pl)
        {
            timeDie.Enabled = true;

            if (btn_Win.BackColor != Color.LightGreen)
            {
                pBlood.Left = 0;
                pBlood.Top = 0;
                pBlood.Visible = true;
            }
           

            txtBox_Answer.Visible = false;
            btn_Question.Visible = false;
            btn_Answer.Visible = false;

            txtBox_Login.Visible = false;
            btn_Login.Visible = false;
            txtBox_Password.Visible = false;
            btn_Password.Visible = false;
            btn_Enter_Login.BackColor = Color.White;
            btn_Enter_Login.BackColor = Color.White;

            txtBox_Enter_Login.Visible = false;
            btn_Enter_Login.Visible = false;
            txtBox_Enter_Password.Visible = false;
            btn_Enter_Password.Visible = false;


            txtBox_Answer.Text = "";
            pBar_Computer.Value = 0;
            scanStatus = 0;

            btn_4.Width = 15;
            btn_4.BackColor = Color.OrangeRed;
            btn_5.Width = 15;
            btn_5.Left = 560;
            btn_5.BackColor = Color.OrangeRed;
            txtBox_Answer.Text = "0";

            if (!doorCheat)
            {
                door_3.Width = 35;
                door_3.Visible = true;
                door_4.Width = 30;
                door_4.Visible = true;
                door_1.Width = 65;
                door_1.Visible = true;
                door_5.Width = 100;
                door_5.Visible = true;
            }

            btn_Pult_1.Top = 345;
            btn_Pult_1.Height = 15;
            btn_Pult_1.BackColor = Color.Tomato;


          
            pBlood.SendToBack();
            btn_4.SendToBack();
            btn4_corpus.SendToBack();
            btn_5.SendToBack();
            btn5_corpus.SendToBack();
            glass_1.SendToBack();
            blade_7.SendToBack();
            blade_8.SendToBack();
            wall_14.SendToBack();
            btn_6.SendToBack();
            btn6_corpus.SendToBack();
            light_wall_6.SendToBack();
            light_wall_7.SendToBack();
            light_wall_8.SendToBack();
            light_wall_9.SendToBack();
            light_wall_10.SendToBack();
            light_wall_11.SendToBack();
            light_wall_12.SendToBack();
            glass_4.SendToBack();
            glass_5.SendToBack();
            glass_6.SendToBack();
            glass_10.SendToBack();
            glass_11.SendToBack();
            glass_12.SendToBack();
            glass_13.SendToBack();
            door_1.SendToBack();
            door_2.SendToBack();
            btn_Pult_1.SendToBack();
            blade_10.SendToBack();
            blade_11.SendToBack();
            blade_21.SendToBack();
            blade_22.SendToBack();
            key_1.SendToBack();
            checkPoint2.SendToBack();
            checkPoint3.SendToBack();
            


            timerSchet.Enabled = true;

            

            dies++;
            lblDies.Text = "Dies: " + dies;

            if (!dieCheat)
            {
                pl.Left = dieLeft;
                pl.Top = dieTop;
                timerLeft.Enabled = false;
                timerRight.Enabled = false;
                timerTop.Enabled = false;
                timerDown.Enabled = false;
            }
            

            moveLeft = 1;
            moveRight = 1;
            moveTop = 1;
            moveDown = 1;
            provWall = 0;
            wallDown = 0;
            wallTop = 0;
            wallLeft = 0;
            wallRight = 0;
            isDie = false;


        }

        private void timeDie_Tick(object sender, EventArgs e)
        {
            if (!dieCheat)
            {
                SoundPlayer soundPlayer = new SoundPlayer();
                soundPlayer.Stream = Resources.Blood_Sound;
                soundPlayer.Play();
            }
           
            timeDie.Enabled = false;

        }

        private void timerSchet_Tick(object sender, EventArgs e)
        {
            y++;

            if (y >= 10)
            {
                y = 0;
                timerSchet.Enabled = false;
                pBlood.Visible = false;


                btn_4.BringToFront();
                btn4_corpus.BringToFront();
                btn_5.BringToFront();
                btn5_corpus.BringToFront();
                glass_1.BringToFront();
                blade_7.BringToFront();
                blade_8.BringToFront();
                btn_6.BringToFront();
                btn6_corpus.BringToFront();
                light_wall_6.BringToFront();
                light_wall_7.BringToFront();
                light_wall_8.BringToFront();
                light_wall_9.BringToFront();
                light_wall_10.BringToFront();
                light_wall_11.BringToFront();
                light_wall_12.BringToFront();
                glass_4.BringToFront();
                glass_5.BringToFront();
                glass_6.BringToFront();
                glass_10.BringToFront();
                glass_11.BringToFront();
                glass_12.BringToFront();
                glass_13.BringToFront();
                door_1.BringToFront();
                btn_Pult_1.BringToFront();
                blade_10.BringToFront();
                blade_11.BringToFront();
                blade_21.BringToFront();
                blade_22.BringToFront();
                key_1.BringToFront();
               



            }
        }














        #endregion



        
        #region Trash

        //////// Шлак //////////////////////////////////////////////////////////////////////////////////////






        /* private void StopRight(Button pl, Button wll)
         {
             if (pl.Left + pl.Width >= wll.Left && pl.Top < wll.Height + wll.Top && pl.Top + pl.Height > wll.Top && pl.Left + wll.Width < wll.Left + wll.Width) // движение вправо внизу сверху  находится слева от стены
             {
                 moveRight = 0;

                 pl.Left = wll.Left - pl.Width;
             }
             else
             {
                 moveRight = 1;
             }
         }

         private void StopLeft(Button pl, Button wll)
         {
             if (pl.Left <= wll.Left + wll.Width && pl.Top < wll.Height + wll.Top && pl.Top + pl.Height > wll.Top && pl.Left + pl.Width > wll.Left) // движение влево внизу сверху  находится справа от стены
             {
                 moveLeft = 0;

                 pl.Left = wll.Left + wll.Width;
             }
             else
             {
                 moveLeft = 1;
             }
         }

         private void StopTop(Button pl, Button wll)
         {
             if (pl.Top <= wll.Top + wll.Height && pl.Left < wll.Width + wll.Left && wll.Left < pl.Left + pl.Width && pl.Top >= wll.Top) // движение вверх справа слева находится ниже стены
             {
                 moveTop = 0;

                 pl.Top = wll.Top + wll.Height;
             }
             else
             {
                 moveTop = 1;
             }
         }

         private void StopDown(Button pl, Button wll)
         {
             if (pl.Top + pl.Height >= wll.Top && pl.Left < wll.Width + wll.Left && wll.Left < pl.Left + pl.Width && pl.Top + pl.Height <= wll.Top) // движение вниз справа слева находится выше стены
             {
                 moveDown = 0;

                 pl.Top = wll.Top - pl.Height;
             }
             else
             {
                 moveDown = 1;
             }
         }



         private void StopMoving()
         {
             if (provWall == 1)
             {
                 StopDown(Player, wall_1);
                 StopLeft(Player, wall_1);
                 StopRight(Player, wall_1);
                 StopTop(Player, wall_1);
             }
             if (provWall == 2)
             {
                 StopDown(Player, wall_2);
                 StopLeft(Player, wall_2);
                 StopRight(Player, wall_2);
                 StopTop(Player, wall_2);
             }
             if (provWall == 3)
             {
                 StopDown(Player, wall_3);
                 StopLeft(Player, wall_3);
                 StopRight(Player, wall_3);
                 StopTop(Player, wall_3);
             }
             if (provWall == 4)
             {
                 StopDown(Player, wall_4);
                 StopLeft(Player, wall_4);
                 StopRight(Player, wall_4);
                 StopTop(Player, wall_4);
             }




             /*
              Gran();


             if (moveRight == 0 && moveTop == 0)
             {
                 moveLeft = 1;
                 moveDown = 1;
             }
             if (moveRight == 0 && moveDown == 0)
             {
                 moveLeft = 1;
                 moveTop = 1;
             }
             if (moveLeft == 0 && moveTop == 0)
             {
                 moveRight = 1;
                 moveDown = 1;
             }
             if (moveLeft == 0 && moveDown == 0)
             {
                 moveRight = 1;
                 moveTop = 1;
             }

             if (moveRight == 0)
             {
                 moveLeft = 1;
                 moveTop = 1;
                 moveDown = 1;
             }
             if (moveLeft == 0)
             {
                 moveRight = 1;
                 moveTop = 1;
                 moveDown = 1;
             }
             if (moveDown == 0)
             {
                 moveRight = 1;
                 moveTop = 1;
                 moveLeft = 1;
             }
             if (moveTop == 0)
             {
                 moveRight = 1;
                 moveDown = 1;
                 moveLeft = 1;
             }

         } */

        #endregion

       
    }
}
