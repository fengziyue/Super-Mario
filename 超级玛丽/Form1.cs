using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Timers;




namespace 超级玛丽
{
    public partial class Form1 : Form
    {
        int mm = 0;
        int mario_step = 1;
        int step_count = 1;
        mario mario1=new mario();
        map map1=new map();
        int middle = 400;
        private Image block_image;//初始化砖块
        private Image mario_image;//初始化马里奥
        private Image mario1_image;//初始化马里奥
        private Image mario2_image;//初始化马里奥
        private Image mario3_image;//初始化马里奥
        private Image mario_jump_image;//初始化马里奥
        private Image floatblock_image;//初始化浮砖
        private Image cloud_image;//初始化云
        private Image plant1_image;//初始化植被1
        private Image pipe_image;//初始化水管

        public Form1()
        {
            InitializeComponent();
            this.KeyDown += new KeyEventHandler(this.Form1_KeyDown);//订阅按下
            this.KeyUp += new KeyEventHandler(this.Form1_KeyUp);//订阅抬起
            System.Timers.Timer mytimer = new System.Timers.Timer(20);//计时器
            mytimer.Elapsed += new ElapsedEventHandler(act);

            SetStyle(ControlStyles.Opaque, true);
            block_image = new Bitmap("block.jpg");//初始化砖块
            mario_image = new Bitmap("mario.png");//初始化马里奥
            mario1_image = new Bitmap("mario1.png");//初始化马里奥
            mario2_image = new Bitmap("mario2.png");//初始化马里奥
            mario3_image = new Bitmap("mario3.png");//初始化马里奥
            mario_jump_image = new Bitmap("mario_jump.png");//初始化马里奥
            floatblock_image = new Bitmap("floatblock.jpg");//初始化浮砖
            cloud_image = new Bitmap("cloud.png");//初始化云
            plant1_image = new Bitmap("plant1.png");//初始化植被1
            pipe_image = new Bitmap("pipe.png");//初始化水管
            mytimer.Start();


        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)//按下的操作
        {
            if (e.KeyCode == Keys.Left)
            {
                mario1.left = 1;
            }
            if (e.KeyCode == Keys.Right)
            {
                mario1.right = 1;
            }
            if(e.KeyCode==Keys.Up)
            {
                mario1.up = 1;
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)//抬起的操作
        {
            if (e.KeyCode == Keys.Left)
            {
                mario1.left = 0;
            }
            if (e.KeyCode == Keys.Right)
            {
                mario1.right = 0;
            }
        }
        void act(object sourse, ElapsedEventArgs e)//每秒执行50次
        {
            if (mm == 1)//
            {
                //MessageBox.Show("haha");//
            }//
            else
            {
                mm = 1;//

                mario1.move();

                if (mario1.marioo[0] >= middle + 100)//修改屏幕中间值
                {
                    middle = mario1.marioo[0] - 100;
                }
                if (mario1.marioo[0] <= middle - 400)
                {
                    mario1.marioo[0] = middle - 400;
                }
                Graphics ee = this.CreateGraphics();//双倍缓冲
                Graphics displayGraphics = ee;//双倍缓冲
                Image ii = new Bitmap(ClientRectangle.Width, ClientRectangle.Height);
                //Graphics gg = this.CreateGraphics();//创建Graphics对象
                Graphics g = Graphics.FromImage(ii);
                g.FillRectangle(Brushes.DeepSkyBlue, new Rectangle(0, 0, 800, 600));//画天空蓝

                for (int i = 0; map1.block[i, 0] <= middle + 400; i++)
                {
                    if (map1.block[i, 0] >= middle - 500)
                    {
                        g.DrawImage(block_image, new Rectangle(map1.block[i, 0] - middle + 400, 500, 50, 50));//画地面
                    }
                }
                for (int i = 0; map1.floatblock[i, 0] < middle + 400; i++)
                {
                    if (map1.floatblock[i, 0] >= middle - 500)
                    {
                        g.DrawImage(floatblock_image, new Rectangle(map1.floatblock[i, 0] - middle + 400, 300, 50, 50));//画浮砖
                    }
                }
                for (int i = 0; map1.cloud[i, 0] < middle + 400; i++)
                {
                    if (map1.cloud[i, 0] >= middle - 550)
                    {
                        g.DrawImage(cloud_image, new Rectangle(map1.cloud[i, 0] - middle + 400, map1.cloud[i,1], 150,100 ));//画云
                    }
                }
                for (int i = 0; map1.plant1[i, 0] < middle + 400; i++)
                {
                    if (map1.plant1[i, 0] >= middle - 800)
                    {
                        g.DrawImage(plant1_image, new Rectangle(map1.plant1[i, 0] - middle + 400, 350, 300, 150));//画植被1
                    }
                }
                for (int i = 0; map1.pipe[i, 0] < middle + 400; i++)
                {
                    if (map1.pipe[i, 0] >= middle - 800)
                    {
                        g.DrawImage(pipe_image, new Rectangle(map1.pipe[i, 0] - middle + 400, 300, 100, 200));//画水管
                    }
                }
                if (mario1.marioo[1]!=400&& mario1.marioo[1] != 200)
                {
                    g.DrawImage(mario_jump_image, new Rectangle(mario1.marioo[0] - middle + 400, mario1.marioo[1], 50, 100));//画马里奥
                }
                else
                {


                    if (mario1.left == 0 && mario1.right == 0)
                    {
                        g.DrawImage(mario_image, new Rectangle(mario1.marioo[0] - middle + 400, mario1.marioo[1], 50, 100));//画马里奥
                    }
                    else
                    {
                        if (step_count == 31)
                            step_count = 1;
                        if (step_count <= 10)
                        {
                            g.DrawImage(mario1_image, new Rectangle(mario1.marioo[0] - middle + 400, mario1.marioo[1], 50, 100));//画马里奥
                            step_count++;
                        }
                        if (step_count > 10 && step_count <= 20)
                        {
                            g.DrawImage(mario2_image, new Rectangle(mario1.marioo[0] - middle + 400, mario1.marioo[1], 50, 100));//画马里奥
                            step_count++;
                        }
                        if (step_count > 20)
                        {
                            g.DrawImage(mario3_image, new Rectangle(mario1.marioo[0] - middle + 400, mario1.marioo[1], 50, 100));//画马里奥
                            step_count++;
                        }
                    }
                }
                displayGraphics.DrawImage(ii, ClientRectangle);
                g.Dispose();
                ii.Dispose();
                ee.Dispose();
                mm = 0;//
            }
        }
    }




    class map
    {
        public int[,] block = new int[200, 2];//地面
        public int[,] plant1= new int[11,2];//植被1
        public int[,] pipe = new int[11, 2];//水管
        public int[,] cloud = new int[11, 2];//云
        public int[,] floatblock = new int[100, 2];//空中的砖块
        public map()
        {
            for (int i = 0; i < 200; i++)//初始化第一张地图地面
            {
                if(40<i&&i<45)
                {

                }
                else
                {
                    block[i, 0] = i * 50;
                    block[i, 1] = 500;
                }
            }
            
            for (int i = 0; i < 10; i++)//初始化第一张地图空中砖块
            {
                floatblock[i, 0] = i * 50+900;
                floatblock[i, 1] = 300;
            }
            floatblock[10, 0] = 10000;
            for (int i = 0; i < 10; i++)//初始化第一张地图云
            {
                cloud[i, 0] = 10000 ;
                cloud[i, 1] = 50;
            }
            cloud[0, 0] =300 ;
            cloud[1, 0] = 700;
            cloud[1, 1] = 100;
            
            for (int i = 0; i < 10; i++)//初始化第一张地图植被1
            {
                plant1[1, 0] =100000;
                plant1[i, 1] = 350;
            }
            plant1[0, 0] = 100;
            for (int i = 0; i < 10; i++)//初始化第一张地图水管
            {
                pipe[1, 0] = 100000;
                pipe[i, 1] = 300;
            }
            pipe[0, 0] = 1600;


        }

    }





    class mario
    {
        public int[] marioo = new int[2];
        public int left = 0;
        public int right = 0;
        public int up = 0;
        public int fall = 0;

        public mario()
        {
            marioo[0] = 400;
            marioo[1] = 400;
            up = 0;
        }
        public void walk()
        {
            if (left == 1)//走路
            {
                marioo[0] = marioo[0] - 10;
                crash_right(marioo[0], marioo[1]);
            }
            if (right == 1)
            {
                marioo[0] = marioo[0] + 10;
                crash_left(marioo[0], marioo[1]);
            }
        }
        public int stand(int a,int b)
        {
            
            if (b >= 400 && b <= 500)
            {
                if (a > 2050 && a < 2250)//第一个缺口
                {
                    
                    return 0;
                }
                
                
                else
                {
                    marioo[1] = 400;
                    return 1;
                }
            }

            if (b >= 200 && b <= 250)
            {
                if (a > 900 && a < 1400)
                {
                    marioo[1] = 200;
                    return 1;

                }
                if (a > 1600 && a < 1700)//第一个水管
                {
                    marioo[1] = 200;
                    return 1;
                }
                else
                {
                    return 0;
                }
                
            }
            return 0;
        }
        public int head_crash(int a, int b)
        {
            b = b - 100;
            if (b >= 200 && b <= 250)
            {
                if (a >= 900 && a <= 1400)
                {
                    
                    return 1;
                }
                else
                {
                    return 0;
                }

            }
            return 0;
        }
        public int crash_left(int a, int b)//左
        {

            if (a >= 850 && a <= 900)//第一排浮砖左边
            {
                
                if (b > 200 && b < 350)
                {

                    marioo[0] = 850;
                    return 1;

                }
                

            }
            if (a >= 1550 && a <= 1600)//第一个水管左边
            {

                if (b > 200 && b <= 400)
                {

                    marioo[0] = 1550;
                    return 1;

                }
                else
                {

                    return 0;
                }

            }
            return 0;
        }
        public int crash_right(int a, int b)//右
        {
            if (a >= 1650 && a <= 1700)//第一个水管右边
            {
                if (b > 200 && b <= 400)
                {
                    marioo[0] = 1700;
                    return 1;
                    
                }
                else
                {

                    return 0;
                }

            }
            
            return 0;
        }
        

        public void jump()
        {
            fall = fall + 1;
            marioo[1] = marioo[1] + fall;
            
            if (head_crash(marioo[0], marioo[1]) == 1 || head_crash(marioo[0] + 50, marioo[1]) == 1)
            {
                fall = 0;
                marioo[1] = 350;
            }
            walk();
        }
        public void move()
        {
            
            if (stand(marioo[0],marioo[1]) == 1||stand(marioo[0]+50,marioo[1])==1)
            {
                if (up == 1)
                {
                    
                    fall = -25;
                    up = 0;
                    jump();
                }
                else
                {
                    walk();
                    fall = 0;
                }
            }
            else
                jump();
        }
    }
    
}

