using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace long_game_project
{
    class CAdvImgActor
    {
        Rectangle srcrec;
        Bitmap img;
        

    }

    class cimghero
    {
        public int x, y, walk_frame_index = 0,shot_frame_index,dead_frame_index=0,jump_frame_index=0,idle_frame_index=0;
        public bool is_move_right = true;
        public int hero_health = 1, display_flag=0;
        public List<Bitmap>walk_r_imges = new List<Bitmap>();
        public List<Bitmap>walk_l_imges = new List<Bitmap>();
        public List<Bitmap>shot_imges = new List<Bitmap>();
        public List<Bitmap>dead_imges = new List<Bitmap>();
        public List<Bitmap>jump_right_imges = new List<Bitmap>();
        public List<Bitmap>jump_left_imges = new List<Bitmap>();
        public List<Bitmap>idle_imges = new List<Bitmap>();



    }
    class cimgactor// used in arrows 
    {

        public int x, y, oldx;
        public Bitmap img;
    }
    class cenemyactor // for  enemy
    {

        public int x, y, wf=0,dir=-10, dead_frame_index=0,revive_waiting_time;

        public List<Bitmap> run_right = new List<Bitmap>();
        public List<Bitmap> run_left = new List<Bitmap>();
        public List<Bitmap> dead = new List<Bitmap>();
        public bool is_move_right = true, is_dead=false, is_touched_hero;

    }
    class cadvancedimgactor
    {
            public Bitmap img;
            public Rectangle recdst;
            public Rectangle recsrc;


    }
    public partial class Form1 : Form
    {
        cimghero hero = new cimghero();
        Bitmap off ;
        int rightflag = 0, leftflag = 0, upflag = 0, downflag = 0, spaceflag = 0, f_flag = 0;
        
        bool isjump = false;
        int jump_count=0;
        Timer tt = new Timer();
        List<cimgactor> arrows= new List<cimgactor>();
        cenemyactor python = new cenemyactor();
        int dead_flag = 0,hero_dead_waiting_time=0;
        cadvancedimgactor pnn = new cadvancedimgactor();
        int scrollpositionx = 0;





        public Form1()
        {
            Load += Form1_Load;
            Paint += Form1_Paint;
            KeyDown += Form1_KeyDown;
            KeyUp += Form1_KeyUp;
            tt.Tick += Tt_Tick;
            tt.Start();
            tt.Interval = 100;
            this.WindowState = FormWindowState.Maximized;
            MouseDown += Form1_MouseDown;
        }

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
           this.Text=(e.X+","+e.Y).ToString();
        }

        private void Tt_Tick(object sender, EventArgs e)
        {

            movehero();
            shot();
            movearrow();
            jump();
            if_idle();
            move_python();
            enemy_hit_from_arrow();
            check_hero_health_if_touch(python);
           this.Text=hero.hero_health.ToString();
            //check_if_hero_damage(python);
            drawdb();

        }
        void check_hero_health_if_touch(cenemyactor enemy)
        {
            bool is_touching = false;

            if (hero.x + hero.walk_r_imges[hero.walk_frame_index].Width > enemy.x &&
                hero.x < enemy.x + enemy.run_right[enemy.wf].Width && enemy.is_dead == false )
            {


                  if(hero.y + hero.walk_r_imges[hero.walk_frame_index].Height > enemy.y && hero.y < enemy.y + enemy.run_right[enemy.wf].Height)
                  {

                
                    is_touching = true;
                
                  }
                   
            }

            if (is_touching && !enemy.is_touched_hero)
            {
                hero.hero_health--;
                enemy.is_touched_hero = true;  
            }
            else if (!is_touching)
            {
                enemy.is_touched_hero = false; 
            }
        }

        //        void check_if_hero_damage( cenemyactor enemy)
//{
//            bool is_touching;
            
//            if (hero.x + hero.walk_r_imges[hero.walk_frame_index].Width > enemy.x &&
//                       hero.x < enemy.x + enemy.run_right[enemy.wf].Width&&jump_count==0)
//            {
//                is_touching = true;
                
//            }
//            else
//            {
//                is_touching= false;
//            }

//            if (is_touching && !enemy.is_dead)
//            {
//                if (dead_flag == 0)
//                {
//                    dead_flag = 1;
//                    hero.display_flag = 3;
//                    hero.dead_frame_index = 0;
//                    hero_dead_waiting_time = 0;
//                }
//            }

//    if (dead_flag == 1)
//    {
//        if (hero.dead_frame_index < hero.dead_imges.Count - 1)
//        {
//            hero_dead_waiting_time++;
           
//            if (hero_dead_waiting_time % 5 == 0) 
//            {
                     
//                        hero.dead_frame_index++;
//            }
//        }

//                else
//                {

//                    if (!is_touching)
//                    {
//                        hero.display_flag = 5;
//                        dead_flag = 0;
//                        hero.dead_frame_index = 0;
//                    }
//                }
//            }
//}

        void move_python()
        {
            if (python.is_dead) return;
            python.wf++;
            if (python.wf > 5) python.wf = 0;

            python.x += python.dir ;

            if (python.x < 0)
            {
                python.is_move_right = true;
                python.dir = 10;

            }

            if (python.x + python.run_right[python.wf].Width > ClientSize.Width)
            {
                python.is_move_right = false;
               python.dir = -10;
                
            }
           
           
        }
        void arrow_display(Graphics g2)
        {
            for (int i = 0; i < arrows.Count; i++)
            {
                g2.DrawImage(arrows[i].img, arrows[i].x, arrows[i].y);
            }
        }
        void python_display(Graphics g2)
        {
            Bitmap python_current_image;

            if (python.is_dead)
            {
                if (python.dead_frame_index >= python.dead.Count)
                {
                    python.dead_frame_index = python.dead.Count - 1;
                }

                python_current_image = python.dead[python.dead_frame_index];

                if (python.dead_frame_index < python.dead.Count - 1)
                {
                    python.dead_frame_index++;
                }
            }
            else
            {
                if (python.is_move_right)
                {
                    python_current_image = python.run_right[python.wf];
                }
                else
                {
                    python_current_image = python.run_left[python.wf];
                }
            }

            g2.DrawImage(python_current_image, python.x, python.y);
        }

        void hero_display(Graphics g2)
        {
            Bitmap hero_current_image;
            // hero display
            switch (hero.display_flag)
            {
                case -1:
                    hero_current_image = hero.idle_imges[hero.idle_frame_index];
                    break;
                case 0:
                    if(hero.is_move_right)
                    {
                        hero_current_image = hero.walk_r_imges[hero.walk_frame_index];
                    }
                    else
                    {
                        hero_current_image = hero.walk_l_imges[hero.walk_frame_index];
                    }
                    
                    break;
                case 1:
                    hero_current_image = hero.shot_imges[hero.shot_frame_index];
                    break;
                case 2:
                    if (hero.is_move_right)
                    {
                        hero_current_image = hero.jump_right_imges[hero.jump_frame_index];
                    }
                    else
                    {
                        hero_current_image = hero.jump_left_imges[hero.jump_frame_index];
                    }
                    break;
                case 3:
                    hero_current_image = hero.dead_imges[hero.dead_frame_index];
                    break;


                default:
                    hero_current_image = hero.idle_imges[0];
                    break;
            }
            g2.DrawImage(hero_current_image, hero.x, hero.y);

        }
        void if_idle()
        {
            if (rightflag == 0 && leftflag == 0 && upflag == 0 && downflag == 0 && f_flag == 0 && spaceflag == 0 && !isjump )
            {
                hero.display_flag = -1;
                hero.walk_frame_index = 0;
                //hero.jump_frame_index = 0;
                hero.idle_frame_index++;
                
                if (hero.idle_frame_index > 7)
                {
                    hero.idle_frame_index = 0;
                 
                }

            }

        }
        void movehero()
        {
            if (dead_flag == 1) return;

            cimghero h = hero;
            if (upflag == 1 &&jump_count==0)
            {
                hero.display_flag = 2;
                isjump = true;
            }
            if (downflag == 1)
            {
                h.y += 15;

            }
            if (leftflag == 1)
            {
                hero.display_flag = 0;
                h.x -= 15;
                hero.walk_frame_index--;
                if (hero.walk_frame_index < 0) hero.walk_frame_index = 7;
            }
            if (rightflag == 1)
            {
                hero.display_flag = 0;
                h.x += 15;
                hero.walk_frame_index++;
                if (hero.walk_frame_index > 7) hero.walk_frame_index = 0;
            }
            // shot
           
        }
        void jump()
        {
            if (isjump )
            {
                jump_count++;
                hero.y -= 10;
                
                hero.jump_frame_index++;
                hero.display_flag = 2;
                if (hero.jump_frame_index > 8)
                {
                    hero.jump_frame_index = 0;
                    hero.display_flag = 0;
                    isjump = false;
                    return;

                }
            }
            else if(jump_count>0)
            {
                hero.y += 10;
                jump_count--;
            }

        }
        void shot()
        {
            if (f_flag == 1)
            {

                hero.shot_frame_index++;
                hero.display_flag = 1;




                if (hero.shot_frame_index > 10)
                {
                    hero.shot_frame_index = 0;
                    hero.display_flag = 0;


               
                    cimgactor pnn = new cimgactor();
                    pnn.img = new Bitmap("arrow.png");
                    pnn.x = hero.x + hero.shot_imges[hero.shot_frame_index].Width;
                    pnn.y = hero.y + (hero.shot_imges[hero.shot_frame_index].Height / 2)-20;
                    arrows.Add(pnn);
                }
                
                
            }
        }
        void movearrow()
        {
            for (int i = 0; i < arrows.Count; i++)
            {

                arrows[i].x += 20;
                if (arrows[i].x+ arrows[i].img.Width > this.ClientSize.Width)
                {
                    arrows.RemoveAt(i);
                    i--;
                }
            }
        }
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    upflag = 0;

                    break;
                case Keys.A:
                    leftflag = 0;
                 
                    break;
                case Keys.S:
                    downflag = 0;
                    break;
                case Keys.D:
                    rightflag = 0;
                   
                    break;
                case Keys.Space:
                    spaceflag = 0;
                    break;
                case Keys.F:
                    f_flag = 0;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                    upflag = 1;

                    break;
                case Keys.A:
                    leftflag = 1;
                    hero.is_move_right = false;

                    break;
                case Keys.S:
                    downflag = 1;
                    break;
                case Keys.D:
                   hero.is_move_right = true;
                    rightflag = 1;
                    break;
                case Keys.Space:
                    spaceflag = 1;
                    break;
                case Keys.F:
                    f_flag = 1;
                    break;

            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawdb();
        }
        void drawdb()
        {
            Graphics g = CreateGraphics();
            Graphics g2 = Graphics.FromImage(off);
            drawscene(g2);
            g.DrawImage(off, 0, 0);


        }
        void drawscene(Graphics g2)
        {
            g2.Clear(Color.White);
            g2.DrawImage(pnn.img, pnn.recdst,pnn.recsrc,GraphicsUnit.Pixel);
           
            hero_display(g2);
            python_display(g2);
            arrow_display(g2);







           

           
            

           
        }
        void enemy_hit_from_arrow()
        {
            if (arrows.Count > 0)
            {
                for (int i = 0; i < arrows.Count; i++)
                {
                    if (arrows[i].x > python.x && arrows[i].x < python.x + python.run_right[python.wf].Width)
                    {
                        if (arrows[i].y > python.y && arrows[i].y < python.y + python.run_right[python.wf].Height)
                        {
                            arrows.RemoveAt(i);
                            i--;
                            python.is_dead = true;
                            python.dir = 0;
                            break;
                        }
                    }
                }
            }
            if (python.is_dead)
            {
                python.revive_waiting_time++;
                if( python.revive_waiting_time>50)
                {
                    python.revive_waiting_time = 0;
                    python.is_dead = false;
                    python.dir = 10;
                   
                   
                }
              

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // walk 
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            Bitmap temp;
            for (int i = 0; i < 8; i++)
            {
                 temp = new Bitmap("walk/"+i+".png");
                hero.walk_r_imges.Add(temp);
                 temp = new Bitmap("walk_left/" + i+".png");
             

                hero.walk_l_imges.Add(temp);

            }
                hero.x = 10;
                hero.y = 445 ;
            // shot
            for (int i = 0; i < 11; i++)
            {
                 temp = new Bitmap("shot/" + (i ) + ".png");
                hero.shot_imges.Add(temp);
            }
            // dead

            for (int i = 0; i < 5; i++)
            {
                 temp = new Bitmap("dead/" + (i) + ".png");
                hero.dead_imges.Add(temp);
            }
            // jump right 
            for (int i = 0; i < 9; i++)
            {
                 temp = new Bitmap("jump/0" + (i) + "_jump.png");
                hero.jump_right_imges.Add(temp);
            }
            // jump left
            for (int i = 0; i < 9; i++)
            {
                temp = new Bitmap("jump_left/0" + (i) + "_0.png");
                hero.jump_left_imges.Add(temp);
            }
            // idle
            for (int i = 0; i < 8; i++)
            {
                temp = new Bitmap("idle/0" + (i) + "_idle.png");
                hero.idle_imges.Add(temp);
            }
            // python
            
            for (int i = 0; i < 6; i++)
            {
                temp = new Bitmap("python/0" + (i) + "_R.png");
             python.run_right.Add(temp);
                temp = new Bitmap("python_left/0" + (i) + "_r_r.png");
             python.run_left.Add(temp);
                if (i<3)
                {

                temp = new Bitmap( i +".png");
                python.dead.Add(temp);
                }
               
                python.x = 200;
                python.y = 445;
                creatbackground();

            }


            void creatbackground()
            {
               
                pnn.img = new Bitmap("2.jpg");
                pnn.recdst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
                pnn.recsrc = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
            }



        }
    }
}
