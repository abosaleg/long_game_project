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
        public List<Bitmap>shot_r_imges = new List<Bitmap>();
        public List<Bitmap>shot_l_imges = new List<Bitmap>();
        public List<Bitmap>dead_imges = new List<Bitmap>();
        public List<Bitmap>jump_right_imges = new List<Bitmap>();
        public List<Bitmap>jump_left_imges = new List<Bitmap>();
        public List<Bitmap>idle_imges = new List<Bitmap>();



    }
    class cimgactor// used in arrows 
    {
        public int x, y, oldx;
        public Bitmap img;
        public bool is_moving_right;
    }
    class cenemyactor // for  enemy
    {

        public int x, y, wf=0, dir=10, dead_frame_index=0, revive_waiting_time;
        public List<Bitmap> run_right = new List<Bitmap>();
        public List<Bitmap> run_left = new List<Bitmap>();
        public List<Bitmap> dead = new List<Bitmap>();
        public List<Bitmap> idle = new List<Bitmap>();
        public List<Bitmap> attack_right = new List<Bitmap>();
        public List<Bitmap> attack_left = new List<Bitmap>();
        public List<Bitmap> hurt = new List<Bitmap>();
        public bool is_move_right = true, is_dead=false, is_touched_hero;
        public int attack_frame_index = 0;
        public bool is_attacking = false;
        public int attack_cooldown = 0;
        public int ATTACK_RANGE = 100;
        public int DETECTION_RANGE = 600;
        public bool is_in_cooldown = false;
        public int health = 5;
        public bool is_hurt = false;
        public int hurt_frame_index = 0;
        public int fly_count = 0;  // Counter for fly animation cycles

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
        cenemyactor wolf = new cenemyactor();  
        cenemyactor dragon = new cenemyactor();
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
            move_dragon();
            move_wolf();
            move_python();
            enemy_hit_from_arrow(python);
            enemy_hit_from_arrow(wolf);
            enemy_hit_from_arrow(dragon);
            check_hero_health_if_touch(python);
            check_hero_health_if_touch(wolf);
            check_hero_health_if_touch(dragon);
            this.Text=hero.hero_health.ToString();
            
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
            }
            else if (python.is_hurt)
            {
                python_current_image = python.hurt[python.hurt_frame_index];
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
                    if(hero.is_move_right)
                    {
                        hero_current_image = hero.shot_r_imges[hero.shot_frame_index];
                    }
                    else
                    {
                        hero_current_image = hero.shot_l_imges[hero.shot_frame_index];
                    }
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
                hero.y -= 20;
                
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
                hero.y += 20;
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
                    // Store the direction when the arrow is created
                    pnn.is_moving_right = hero.is_move_right;
                    
                    if (pnn.is_moving_right)
                    {
                        pnn.img = new Bitmap("arrow right.png");
                        pnn.x = hero.x + hero.shot_r_imges[hero.shot_frame_index].Width;
                    }
                    else
                    {
                        pnn.img = new Bitmap("arrow left.png");
                        pnn.x = hero.x - hero.shot_r_imges[hero.shot_frame_index].Width;
                    }
                    pnn.y = hero.y + (hero.shot_r_imges[hero.shot_frame_index].Height / 2) - 20;
                    arrows.Add(pnn);
                }
            }
        }
        void movearrow()
        {
            for (int i = 0; i < arrows.Count; i++)
            {
                // Move arrow based on its stored direction
                if (arrows[i].is_moving_right)
                {
                    arrows[i].x += 20;
                    if (arrows[i].x + arrows[i].img.Width > this.ClientSize.Width)
                    {
                        arrows.RemoveAt(i);
                        i--;
                    }
                }
                else
                {
                    arrows[i].x -= 20;
                    if (arrows[i].x < 0)
                    {
                        arrows.RemoveAt(i);
                        i--;
                    }
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
       
        void enemy_hit_from_arrow(cenemyactor enemy)
        {
            if (arrows.Count > 0)
            {
                for (int i = 0; i < arrows.Count; i++)
                {
                    if (arrows[i].x > enemy.x && arrows[i].x < enemy.x + enemy.run_right[enemy.wf].Width)
                    {
                        if (arrows[i].y > enemy.y && arrows[i].y < enemy.y + enemy.run_right[enemy.wf].Height && !enemy.is_dead)
                        {
                            arrows.RemoveAt(i);
                            i--;
                            enemy.health--;
                            enemy.is_hurt = true;
                            enemy.hurt_frame_index = 0;
                            
                            // Put enemy in cooldown when hit
                            enemy.is_in_cooldown = true;
                            enemy.attack_cooldown = 15; // Adjust this value to change cooldown duration
                            
                            if (enemy.health <= 0)
                            {
                                enemy.is_dead = true;
                                enemy.dir = 0;
                                enemy.dead_frame_index = 0;
                            }
                            break;
                        }
                    }
                }
            }

            // Handle hurt animation
            if (enemy.is_hurt)
            {
                enemy.hurt_frame_index++;
                if (enemy.hurt_frame_index >= enemy.hurt.Count)
                {
                    enemy.is_hurt = false;
                    enemy.hurt_frame_index = 0;
                }
            }

            if (enemy.is_dead)
            {
                enemy.dead_frame_index++;
                if (enemy.dead_frame_index >= enemy.dead.Count)
                {
                    enemy.dead_frame_index = enemy.dead.Count - 1;
                }
            }
        }

      
            void creatbackground()
            {
               
                pnn.img = new Bitmap("2.jpg");
                pnn.recdst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
                pnn.recsrc = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
            }



        

        void move_wolf()
        {
            if (wolf.is_dead) return;
            
            int distance_to_hero;
            // Calculate distance to hero
            if (hero.x - wolf.x < 0)
            {
                distance_to_hero = (hero.x - wolf.x) * -1;
            }
            else
            {
                distance_to_hero = hero.x - wolf.x;
            }

            // Handle cooldown state
            if (wolf.is_in_cooldown)
            {
                wolf.attack_cooldown--;
                if (wolf.attack_cooldown <= 0)
                {
                    wolf.is_in_cooldown = false;
                }
                return;
            }

            // Attack range check (100 units)
            if (distance_to_hero <= wolf.ATTACK_RANGE && !wolf.is_attacking && !wolf.is_in_cooldown)
            {
                wolf.is_attacking = true;
                hero.hero_health--; 
                wolf.attack_frame_index = 0;
            }

            // Handle attack animation
            if (wolf.is_attacking)
            {
                wolf.attack_frame_index++;
                if (wolf.attack_frame_index >= wolf.attack_right.Count)
                {
                    wolf.is_attacking = false;
                    wolf.is_in_cooldown = true;
                    wolf.attack_cooldown = 20;
                }
            }
            // Detection range check (300 units)
            else if (distance_to_hero <= wolf.DETECTION_RANGE && !wolf.is_in_cooldown)
            {
                // Move towards hero
                wolf.wf++;
                if (wolf.wf > 5) wolf.wf = 0;

                if (hero.x < wolf.x)
                {
                    wolf.is_move_right = false;
                    wolf.x -= 5;
                }
                else
                {
                    wolf.is_move_right = true;
                    wolf.x += 5;
                }
            }
            // If out of detection range, stay idle
            else
            {
                // Update idle animation
                wolf.wf++;
                if (wolf.wf >= wolf.idle.Count)
                {
                    wolf.wf = 0;
                }
            }
        }

        void wolf_display(Graphics g2)
        {
            Bitmap wolf_current_image;

            if (wolf.is_dead)
            {
                if (wolf.dead_frame_index >= wolf.dead.Count)
                {
                    wolf.dead_frame_index = wolf.dead.Count - 1;
                }
                wolf_current_image = wolf.dead[wolf.dead_frame_index];
                if (wolf.dead_frame_index < wolf.dead.Count - 1)
                {
                    wolf.dead_frame_index++;
                }
            }
            else if (wolf.is_attacking)
            {
                if (wolf.is_move_right)
                {
                    wolf_current_image = wolf.attack_right[wolf.attack_frame_index];
                }
                else
                {
                    wolf_current_image = wolf.attack_left[wolf.attack_frame_index];
                }
            }
            else if (wolf.is_in_cooldown || Math.Abs(hero.x - wolf.x) > wolf.DETECTION_RANGE)
            {
                // Show idle animation when in cooldown or out of detection range
                wolf_current_image = wolf.idle[wolf.wf];
                wolf.wf++;
                if (wolf.wf >= wolf.idle.Count)
                {
                    wolf.wf = 0;
                }
            }
            else
            {
                if (wolf.is_move_right)
                {
                    wolf_current_image = wolf.run_right[wolf.wf];
                }
                else
                {
                    wolf_current_image = wolf.run_left[wolf.wf];
                }
            }

            g2.DrawImage(wolf_current_image, wolf.x, wolf.y);
        }

        void move_dragon()
        {
            if (dragon.is_dead) return;

            // Handle flame animation
            if (dragon.is_attacking)
            {
                dragon.attack_frame_index++;
                if (dragon.attack_frame_index >= dragon.attack_right.Count)
                {
                    dragon.is_attacking = false;
                    dragon.wf = 0;  // Reset flying animation when flame ends
                    dragon.fly_count = 0;  // Reset fly count
                }
            }
            else
            {
                // Update flying animation
                dragon.wf++;
                if (dragon.wf >= dragon.run_right.Count)
                {
                    dragon.wf = 0;
                    dragon.fly_count++;  // Increment fly count
                    
                    // After completing two fly animation cycles, start flame animation
                    if (dragon.fly_count >= 5)
                    {
                        dragon.is_attacking = true;
                        dragon.attack_frame_index = 0;
                    }
                }
            }

            // Move dragon (now moves even while attacking)
            dragon.x += dragon.dir;

            if (dragon.x < 0)
            {
                dragon.is_move_right = true;
                dragon.dir = 10;
            }

            if (dragon.x + dragon.run_right[dragon.wf].Width > ClientSize.Width)
            {
                dragon.is_move_right = false;
                dragon.dir = -10;
            }
        }

        void dragon_display(Graphics g2)
        {
            Bitmap dragon_current_image;

            if (dragon.is_dead)
            {
                // Handle death animation if you have one
                return;
            }
            else if (dragon.is_attacking)
            {
                if (dragon.is_move_right)
                {
                    dragon_current_image = dragon.attack_right[dragon.attack_frame_index];
                }
                else
                {
                    dragon_current_image = dragon.attack_left[dragon.attack_frame_index];
                }
            }
            else
            {
                if (dragon.is_move_right)
                {
                    dragon_current_image = dragon.run_right[dragon.wf];
                }
                else
                {
                    dragon_current_image = dragon.run_left[dragon.wf];
                }
            }

            g2.DrawImage(dragon_current_image, dragon.x, dragon.y);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // walk 
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            Bitmap temp;
            for (int i = 0; i < 8; i++)
            {
                temp = new Bitmap("walk/" + i + ".png");
                hero.walk_r_imges.Add(temp);
                temp = new Bitmap("walk_left/" + i + ".png");


                hero.walk_l_imges.Add(temp);

            }
            hero.x = this.ClientSize.Width - hero.walk_r_imges[0].Width;
            hero.y = 445;
            // shot
            for (int i = 0; i < 11; i++)
            {
                temp = new Bitmap("shot right/" + (i) + ".png");
                hero.shot_r_imges.Add(temp);
                temp = new Bitmap("shot left/" + (i) + ".png");
                hero.shot_l_imges.Add(temp);
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
                if (i < 3)
                {
                    temp = new Bitmap(i + ".png");
                    python.dead.Add(temp);
                }
                // Load hurt animation
                if (i < 2)
                {

                    temp = new Bitmap("python Hurt/0" + i + "_hurt.png");
                    python.hurt.Add(temp);
                }

                python.x = 200;
                python.y = 445;
            }

            // Load wolf images
            for (int i = 0; i < 6; i++)
            {
                //00_Attack_3
                wolf.idle.Add(new Bitmap("wolf idle/0" + i + "_Idle.png"));
                wolf.run_right.Add(new Bitmap("wolf run right/0" + i + "_Run.png"));
                if (i <= 4)
                    wolf.attack_right.Add(new Bitmap("wolf attak right/0" + i + "_Attack_3.png"));
            }

            // Load wolf dead animation
            for (int i = 0; i < 2; i++)
            {
                wolf.dead.Add(new Bitmap("wolfdead/0" + i + "_wolfDead.png"));
            }

            // Create left-facing versions of run and attack animations
            for (int i = 0; i < wolf.run_right.Count; i++)
            {
                wolf.run_left.Add(new Bitmap("wolf Run left/0" + i + "_.png"));
            }

            for (int i = 0; i < wolf.attack_right.Count; i++)
            {
                wolf.attack_left.Add(new Bitmap("wolf attak left/0" + i + "_0.png"));
            }

            //  wolf position
            wolf.x = 300;
            wolf.y = 445;
            wolf.is_move_right = true;

            // Load dragon images
            for (int i = 0; i < 6; i++)
            {
                temp = new Bitmap("dragon fly right/0" + i + "_0.png");
                dragon.run_right.Add(temp);
                temp = new Bitmap("dragon fly left/0" + i + "_0.png");
                dragon.run_left.Add(temp);
                temp = new Bitmap("dragon flame right/0" + i + "_1.png");
                dragon.attack_right.Add(temp);
                temp = new Bitmap("dragon flame left/0" + i + "_2.png");
                dragon.attack_left.Add(temp);
            }

            // Set dragon initial position
            dragon.x = 400;
            dragon.y = 200;
            dragon.is_move_right = true;
            dragon.dir = 10;
        }
            void drawscene(Graphics g2)
        {
            g2.Clear(Color.White);

            hero_display(g2);
            wolf_display(g2);
            dragon_display(g2);
            arrow_display(g2);
            python_display(g2);
        }
    }

}
