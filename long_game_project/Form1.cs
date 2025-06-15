using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
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
    class advanced_img_actor
    {
        public Bitmap img;
        public Rectangle src;
        public Rectangle dst;

    }
    class cimghero
    {
        public int x, y, walk_frame_index = 0, shot_frame_index, dead_frame_index = 0, jump_frame_index = 0, idle_frame_index = 0;
        public bool is_move_right = true,
        is_touched_laser = false;
        public int hero_health = 5, display_flag = 0;
        public List<Bitmap> walk_r_imges = new List<Bitmap>();
        public List<Bitmap> walk_l_imges = new List<Bitmap>();
        public List<Bitmap> shot_r_imges = new List<Bitmap>();
        public List<Bitmap> shot_l_imges = new List<Bitmap>();
        public List<Bitmap> dead_imges = new List<Bitmap>();
        public List<Bitmap> jump_right_imges = new List<Bitmap>();
        public List<Bitmap> jump_left_imges = new List<Bitmap>();
        public List<Bitmap> idle_imges = new List<Bitmap>();
        public List<Bitmap> health_bar = new List<Bitmap>();
        public List<Bitmap> game_over_frames = new List<Bitmap>();
        public bool is_dead = false;
        public bool show_game_over = false;
        public int game_over_frame_index = 0;
        public int death_animation_delay = 0;  // Add delay counter for death animation
        public int H;
        public int W;


    }
    class cimgactor// used in arrows 
    {
        public int x, y, old_y, xd = -1, dy = -1;
        public Bitmap img;
        public bool is_moving_right;
        public bool is_falling = false;
        public int fall_speed = 0;
    }
    class cenemyactor // for  enemy
    {

        public int x, y, wf = 0, dir = 10, dead_frame_index = 0, revive_waiting_time, freez;
        public List<Bitmap> run_right = new List<Bitmap>();
        public List<Bitmap> run_left = new List<Bitmap>();
        public List<Bitmap> dead = new List<Bitmap>();
        public List<Bitmap> idle = new List<Bitmap>();
        public List<Bitmap> attack_right = new List<Bitmap>();
        public List<Bitmap> attack_left = new List<Bitmap>();
        public List<Bitmap> hurt = new List<Bitmap>();
        public bool is_move_right = true, is_dead = false, is_touched_hero;
        public int attack_frame_index = 0;
        public bool is_attacking = false;
        public int attack_cooldown = 0;
        public int ATTACK_RANGE = 50;
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
        public bool is_touched_hero = false;
    }

    public class MUCImgActor
    {
        public int x, y, xd = 1, xy = 1, f = 0;
        public List<Bitmap> imgs = new List<Bitmap>();
        public int iF = 0;
    }
    public partial class Form1 : Form
    {
        cimghero hero = new cimghero();
        Bitmap off;
        int ct_tick, rightflag = 0, leftflag = 0, upflag = 0, downflag = 0, spaceflag = 0,
            f_flag = 0, f_scroll_R = -1, f_scroll_L = -1, f_scroll_UP = -1, f_scroll_DOWN = -1, A, B, F_hit_blok = -1, F_lo = 0, F_g
            , F_wolf = -1, f_laser_yz=1, py_laser2=600;
        int stair_flag = 0;
        bool gravity = true;


        bool isjump = false;
        int jump_count = 0;
        Timer tt = new Timer();
        List<cimgactor> arrows = new List<cimgactor>();
        cenemyactor python = new cenemyactor();
        cenemyactor wolf = new cenemyactor();
        cenemyactor dragon = new cenemyactor();
        int dead_flag = 0, hero_dead_waiting_time = 0;
        cadvancedimgactor pnn = new cadvancedimgactor();
        int scrollpositionx = 0;


        List<cadvancedimgactor> Background_le1 = new List<cadvancedimgactor>();
        List<MUCImgActor> lava_le1 = new List<MUCImgActor>();
        List<cimgactor> block_le1 = new List<cimgactor>();
        List<cimgactor> chick_point = new List<cimgactor>();
        List<cimgactor> block_up_of_lava = new List<cimgactor>();
        List<cimgactor> door_le1 = new List<cimgactor>();
        List<cimgactor> linedoor_le1 = new List<cimgactor>();
        List<cimgactor> stair_le1 = new List<cimgactor>();
        List<cimgactor> trav_le1 = new List<cimgactor>();

        List<cimgactor> python_block_le1 = new List<cimgactor>();
        List<cimgactor> dragon_block_le1 = new List<cimgactor>();
        List<cimgactor> wolf_block_le1 = new List<cimgactor>();
        List<cimgactor> laser_enemy_block_le1 = new List<cimgactor>();
        List<cimgactor> laser_block_le1 = new List<cimgactor>();
        cenemyactor laser_enemy = new cenemyactor();
        cadvancedimgactor laser_line;

       
        List<cadvancedimgactor> intro_frames = new List<cadvancedimgactor>();
        List<cadvancedimgactor> you_win_frames = new List<cadvancedimgactor>();
        int current_intro_frame = 0;
        int current_win_frame = 0;
        bool is_intro_active = true;
        bool is_win_active = false;
        
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
        ///
        void creat_Background_le1()
        {
            cadvancedimgactor pnn = new cadvancedimgactor();
            pnn.img = new Bitmap("background/Background_le1.png");
            pnn.recdst = new Rectangle(0, 0, Width, Height);
            pnn.recsrc = new Rectangle(0, 0, pnn.img.Width, pnn.img.Height);
            Background_le1.Add(pnn);
        }
        void creat_block_le1()
        {
            int yy = 0;
            int xx = 0;
            int x = 0;

            ///ard
            for (int i = 0; i < 1; i++)
            {
                cimgactor pnn = new cimgactor();
                pnn.img = new Bitmap("block/tile1.png");
                pnn.x = 0 + xx;
                pnn.y = ClientSize.Height - 70;
                block_le1.Add(pnn);
                for (int o = 0; o < 20; o++)
                {
                    cimgactor pnnr1 = new cimgactor();
                    pnnr1.img = new Bitmap("block/tile2.png");
                    pnnr1.x = (50 + x) + xx;
                    pnnr1.y = ClientSize.Height - yy - 70;
                    block_le1.Add(pnnr1);
                    x += pnnr1.img.Width;
                }
                cimgactor pnn2 = new cimgactor();
                pnn2.img = new Bitmap("block/tile3.png");
                pnn2.x = (50 + x) + xx;
                pnn2.y = ClientSize.Height - yy - 70;
                block_le1.Add(pnn2);

            }
            yy = 0;
            xx = 1200;
            x = 0;
            // block above lava 
            for (int i = 0; i < 3; i++)
            {
                x = 0;
                if (i != 0)
                {
            
                    cimgactor pnn = new cimgactor();
                    pnn.img = new Bitmap("block/tile1.png");
                    pnn.x = 0 + xx;
                    pnn.y = ClientSize.Height - yy - 40;
                    block_up_of_lava.Add(pnn);
            
                    cimgactor pnnr1 = new cimgactor();
                    pnnr1.img = new Bitmap("block/tile2.png");
                    pnnr1.x = (50 + x) + xx;
                    pnnr1.y = ClientSize.Height - yy - 40;
                    block_up_of_lava.Add(pnnr1);
                    x += pnnr1.img.Width;
            
                    cimgactor pnn2 = new cimgactor();
                    pnn2.img = new Bitmap("block/tile3.png");
                    pnn2.x = (50 + x) + xx;
                    pnn2.y = ClientSize.Height - yy - 40;
                    block_up_of_lava.Add(pnn2);
                }
                yy += 100;
                xx += 200;
            }
            for (int i = 0; i < 2; i++)
            {
                yy -= 100;
                xx += 70;
                x = 0;
                cimgactor pnn = new cimgactor();
                pnn.img = new Bitmap("block/tile1.png");
                pnn.x = 50 + xx;
                pnn.y = ClientSize.Height - yy - 70;
                block_up_of_lava.Add(pnn);


                cimgactor pnnr1 = new cimgactor();
                pnnr1.img = new Bitmap("block/tile2.png");
                pnnr1.x = (0 + x) + xx;
                pnnr1.y = ClientSize.Height - yy - 70;
                block_up_of_lava.Add(pnnr1);
                x += pnnr1.img.Width;



                cimgactor pnn3 = new cimgactor();
                pnn3.img = new Bitmap("block/tile3.png");
                pnn3.x = (50 + x) + xx;
                pnn3.y = ClientSize.Height - yy - 70;
                block_up_of_lava.Add(pnn3);
                xx += 150;
            }

            yy = 0;
            xx = 2320;
            x = 0;
            ///ard
            for (int i = 0; i < 1; i++)
            {
                cimgactor pnn = new cimgactor();
                pnn.img = new Bitmap("block/tile1.png");
                pnn.x = 0 + xx;
                pnn.y = ClientSize.Height - yy - 70;
                wolf_block_le1.Add(pnn);
                for (int o = 0; o < 21; o++)
                {
                    cimgactor pnnr1 = new cimgactor();
                    pnnr1.img = new Bitmap("block/tile2.png");
                    pnnr1.x = (50 + x) + xx;
                    pnnr1.y = ClientSize.Height - yy - 70;
                    wolf_block_le1.Add(pnnr1);
                    x += pnnr1.img.Width;
                }
                cimgactor pnn2 = new cimgactor();
                pnn2.img = new Bitmap("block/tile3.png");
                pnn2.x = (50 + x) + xx;
                pnn2.y = ClientSize.Height - yy - 70;
                wolf_block_le1.Add(pnn2);

            }

            //////fok elsylm
            yy = 0;
            xx = 3000;
            x = 0;
            ///ard
            for (int i = 0; i < 1; i++)
            {
                cimgactor pnn = new cimgactor();
                pnn.img = new Bitmap("block/tile1.png");
                pnn.x = 0 + xx;
                pnn.y = ClientSize.Height / 2;
                block_le1.Add(pnn);
                for (int o = 0; o < 10; o++)
                {
                    cimgactor pnnr1 = new cimgactor();
                    pnnr1.img = new Bitmap("block/tile2.png");
                    pnnr1.x = (50 + x) + xx;
                    pnnr1.y = ClientSize.Height / 2 - yy;
                    block_le1.Add(pnnr1);
                    x += pnnr1.img.Width;
                }
                cimgactor pnn2 = new cimgactor();
                pnn2.img = new Bitmap("block/tile3.png");
                pnn2.x = (50 + x) + xx;
                pnn2.y = ClientSize.Height / 2 - yy;
                block_le1.Add(pnn2);

            }
            //bolkfok
            yy = 0;
            xx = 400;
            x = 0;
            ///ard




            for (int i = 0; i < 1; i++)
            {
                cimgactor pnn = new cimgactor();
                pnn.img = new Bitmap("block/tile1.png");
                pnn.x = 0 + xx;
                pnn.y = ClientSize.Height - yy - 70 - 300;
                dragon_block_le1.Add(pnn);
                for (int o = 0; o < 10; o++)
                {
                    cimgactor pnnr1 = new cimgactor();
                    pnnr1.img = new Bitmap("block/tile2.png");
                    pnnr1.x = (50 + x) + xx;
                    pnnr1.y = ClientSize.Height - yy - 70 - 300;
                    dragon_block_le1.Add(pnnr1);
                    x += pnnr1.img.Width;
                }
                cimgactor pnn2 = new cimgactor();
                pnn2.img = new Bitmap("block/tile3.png");
                pnn2.x = (50 + x) + xx;
                pnn2.y = ClientSize.Height - yy - 70 - 300;
                dragon_block_le1.Add(pnn2);

            }

            xx += 200;
            for (int i = 0; i < 1; i++)
            {
                //cimgactor pnn = new cimgactor();
                //pnn.img = new Bitmap("block/tile1.png");
                //pnn.x = 0 + xx+50;
                //pnn.y = ClientSize.Height - yy - 70 - 500;
                //block_le1.Add(pnn);
                for (int o = 0; o < 10; o++)
                {
                    cimgactor pnnr1 = new cimgactor();
                    pnnr1.img = new Bitmap("block/tile2.png");
                    pnnr1.x = (50 + x) + xx;
                    pnnr1.y = ClientSize.Height - yy - 70 - 500;
                    laser_enemy_block_le1.Add(pnnr1);
                    x += pnnr1.img.Width;
                }
                cimgactor pnn2 = new cimgactor();
                pnn2.img = new Bitmap("block/tile3.png");
                pnn2.x = (50 + x) + xx;
                pnn2.y = ClientSize.Height - yy - 70 - 500;
                laser_enemy_block_le1.Add(pnn2);
            }
            /////////////////////////////////////
            xx += 200;
            for (int i = 0; i < 1; i++)
            {


                for (int o = 0; o < 8; o++)
                {
                    cimgactor pnnr1 = new cimgactor();
                    pnnr1.img = new Bitmap("block/tile2.png");
                    pnnr1.x = (50 + x) + xx;
                    pnnr1.y = ClientSize.Height - yy - 70 - 500;
                    python_block_le1.Add(pnnr1);
                    x += pnnr1.img.Width;
                }

                cimgactor pnn2 = new cimgactor();
                pnn2.img = new Bitmap("block/tile3.png");
                pnn2.x = (50 + x) + xx;
                B = 850;
                A = 700;
                pnn2.y = ClientSize.Height - yy - 70 - 500;
                python_block_le1.Add(pnn2);

            }
            foreach (var item in block_up_of_lava)
            {
                item.old_y = item.y;
            }
        }
        void creat_door_le1()
        {
            int x = 0, xx = 0, yy = 0;

            x = 0;
            cimgactor pnn = new cimgactor();
            pnn.img = new Bitmap("block/tile1.png");
            pnn.x = 0 + xx;
            pnn.y = ClientSize.Height - yy - 70 - 400;
            block_le1.Add(pnn);

            for (int i = 0; i < 2; i++)
            {
                cimgactor pnnr1 = new cimgactor();
                pnnr1.img = new Bitmap("block/tile2.png");
                pnnr1.x = (50 + x) + xx;
                pnnr1.y = ClientSize.Height - yy - 70 - 400;
                block_le1.Add(pnnr1);
                x += pnnr1.img.Width;
            }
            cimgactor pnn2 = new cimgactor();
            pnn2.img = new Bitmap("block/tile3.png");
            pnn2.x = (50 + x) + xx;
            pnn2.y = ClientSize.Height - yy - 70 - 400;
            block_le1.Add(pnn2);
            yy += 100;
            xx += 200;

            cimgactor pnndo = new cimgactor();
            pnndo.img = new Bitmap("door/door_le1.png");
            pnndo.x = 0;
            pnndo.y = ClientSize.Height - yy - 70 - 420;
            door_le1.Add(pnndo);


        }
        void creat_trav_le1()
        {

            cimgactor pnn = new cimgactor();
            pnn.img = new Bitmap("block/tile1.png");
            pnn.x = 2800;
            pnn.y = ClientSize.Height / 2;
            trav_le1.Add(pnn);

            cimgactor pnnr1 = new cimgactor();
            pnnr1.img = new Bitmap("block/tile2.png");
            pnnr1.x = 2850;
            pnnr1.y = ClientSize.Height / 2;
            trav_le1.Add(pnnr1);

            cimgactor pnn2 = new cimgactor();
            pnn2.img = new Bitmap("block/tile3.png");
            pnn2.x = 2900;
            pnn2.y = ClientSize.Height / 2;
            trav_le1.Add(pnn2);

        }
        void creat_stair_le1()
        {
            int yy = 0;

            cimgactor pnn = new cimgactor();
            pnn.img = new Bitmap("stair/stair1.png");
            pnn.x = 3500;
            pnn.y = ClientSize.Height / 2;
            stair_le1.Add(pnn);
            for (int i = 0; i < 9; i++)
            {
                yy += pnn.img.Height;
                cimgactor pnn1 = new cimgactor();
                pnn1.img = new Bitmap("stair/stair2.png");
                pnn1.x = 3500;
                pnn1.y = ClientSize.Height / 2 + yy;
                stair_le1.Add(pnn1);
            }
        }
        void creat_lava_le1()
        {
            //lava_tile5

            MUCImgActor pnn = new MUCImgActor();
            pnn.imgs = new List<Bitmap>();
            Bitmap pnnimg = new Bitmap("lava/lava_tile5.png");
            pnn.imgs.Add(pnnimg);
            pnnimg = new Bitmap("lava/lava_tile8.png");
            pnn.imgs.Add(pnnimg);
            pnn.x = 1350;
            pnn.y = ClientSize.Height - 70;
            lava_le1.Add(pnn);
            int xx = 0;
            for (int i = 0; i < 18; i++)
            {
                xx += 50;
                MUCImgActor pnn3 = new MUCImgActor();
                pnn3.imgs = new List<Bitmap>();
                Bitmap pnnimg3 = new Bitmap("lava/lava_tile6.png");
                pnn3.imgs.Add(pnnimg3);
                pnnimg3 = new Bitmap("lava/lava_tile9.png");
                pnn3.imgs.Add(pnnimg3);
                pnn3.x = 1350 + xx;
                pnn3.y = ClientSize.Height - 70;
                lava_le1.Add(pnn3);
            }
            xx += 50;
            MUCImgActor pnn2 = new MUCImgActor();
            pnn2.imgs = new List<Bitmap>();
            Bitmap pnnimg2 = new Bitmap("lava/lava_tile7.png");
            pnn2.imgs.Add(pnnimg2);
            pnnimg2 = new Bitmap("lava/lava_tile10.png");
            pnn2.imgs.Add(pnnimg2);
            pnn2.x = 1350 + xx;
            pnn2.y = ClientSize.Height - 70;
            lava_le1.Add(pnn2);
            //MUCImgActor pnnf = new MUCImgActor();
            //pnn.imgs = new List<Bitmap>();
            //for (int i = 0; i < 8; i++)
            //{
            //    Bitmap pnnimgf = new Bitmap("lava_tile6.png");
            //    pnnimgf.MakeTransparent(pnnimgf.GetPixel(0, 0));
            //    pnnf.imgs.Add(pnnimgf);
            //    pnnf.x = 50 + xx;
            //    pnnf.y = 600;
            //}
            //lava.Add(pnnf);
        }
        void creat_chick_point_le1()
        {
            int yy = 0;
            int xx = 0;
            int x = 0;


            cimgactor pnn = new cimgactor();
            pnn.img = new Bitmap("marker_statue/marker_statue3.png");
            pnn.x = 1250;
            pnn.y = ClientSize.Height - 70 - 70;
            chick_point.Add(pnn);


             pnn = new cimgactor();
            pnn.img = new Bitmap("marker_statue/marker_statue3.png");
            pnn.x = 3050;
            pnn.y = ClientSize.Height /2 -75;
            chick_point.Add(pnn);
        }
        void laser_blo_le1()
        {
            cimgactor pnnr1 = new cimgactor();
            pnnr1.img = new Bitmap("lasir/00_lasir.png");
            pnnr1.x = 450 ;
            pnnr1.y = ClientSize.Height  - 70 - 280;
            laser_block_le1.Add(pnnr1);

            pnnr1 = new cimgactor();
            pnnr1.img = new Bitmap("lasir/00_lasir.png");
            pnnr1.x = 750;
            pnnr1.y = ClientSize.Height - 70 - 280;
            laser_block_le1.Add(pnnr1);

            pnnr1 = new cimgactor();
            pnnr1.img = new Bitmap("lasir/00_lasir.png");
            pnnr1.x = 1050;
            pnnr1.y = ClientSize.Height - 70 - 280;
            laser_block_le1.Add(pnnr1);

        }
        void scllor()
        {
            if (hero.is_dead) return;
            if (leftflag == 1)
            {
                if (f_scroll_L == 1)
                {
                    for (int i = 0; i < block_le1.Count; i++)
                    {
                        block_le1[i].x += 20;
                    }
                    for (int i = 0; i < block_up_of_lava.Count; i++)
                    {
                        block_up_of_lava[i].x += 20;
                    }
                    for (int i = 0; i < door_le1.Count; i++)
                    {
                        door_le1[i].x += 20;
                    }
                    for (int i = 0; i < linedoor_le1.Count; i++)
                    {
                        linedoor_le1[i].x += 20;
                    }
                    for (int i = 0; i < stair_le1.Count; i++)
                    {
                        stair_le1[i].x += 20;
                    }
                    for (int i = 0; i < lava_le1.Count; i++)
                    {
                        lava_le1[i].x += 20;
                    }
                    for (int i = 0; i < trav_le1.Count; i++)
                    {
                        trav_le1[i].x += 20;
                    }

                    for (int i = 0; i < python_block_le1.Count; i++)
                    {
                        python_block_le1[i].x += 20;
                    }


                    for (int i = 0; i < wolf_block_le1.Count; i++)
                    {
                        wolf_block_le1[i].x += 20;
                    }
                    for (int i = 0; i < dragon_block_le1.Count; i++)
                    {
                        dragon_block_le1[i].x += 20;
                    }
                    for (int i = 0; i < laser_enemy_block_le1.Count; i++)
                    {
                        laser_enemy_block_le1[i].x += 20;
                    }
                    for (int i = 0; i < chick_point.Count; i++)
                    {
                        chick_point[i].x += 20;
                    }

                    for (int i = 0; i < laser_block_le1.Count; i++)
                    {
                        laser_block_le1[i].x += 20;
                    }

                    // Only move wolf if it's within visible area
                    if (wolf.x >= -100 && wolf.x <= this.ClientSize.Width + 100)
                    {
                        wolf.x += 20;
                    }
                    // Only move dragon if it's within its boundaries
                    if (dragon.x >= dragon_block_le1[0].x && dragon.x <= dragon_block_le1[dragon_block_le1.Count - 1].x + dragon_block_le1[dragon_block_le1.Count - 1].img.Width)
                    {
                        dragon.x += 20;
                    }
                    if (python.x >= -100 && python.x <= this.ClientSize.Width + 100)
                    {
                        python.x += 20;
                    }
                    laser_enemy.x += 20;
                    laser_line.recdst.X += 20;

                }
            }
            if (rightflag == 1)
            {
                laser_enemy.x -= 20;
                if (f_scroll_R == 1)
                {
                    for (int i = 0; i < block_le1.Count; i++)
                    {
                        block_le1[i].x -= 20;
                    }
                    for (int i = 0; i < block_up_of_lava.Count; i++)
                    {
                        block_up_of_lava[i].x -= 20;
                    }
                    for (int i = 0; i < door_le1.Count; i++)
                    {
                        door_le1[i].x -= 20;
                    }
                    for (int i = 0; i < linedoor_le1.Count; i++)
                    {
                        linedoor_le1[i].x -= 20;
                    }
                    for (int i = 0; i < stair_le1.Count; i++)
                    {
                        stair_le1[i].x -= 20;
                    }
                    for (int i = 0; i < lava_le1.Count; i++)
                    {
                        lava_le1[i].x -= 20;
                    }
                    for (int i = 0; i < trav_le1.Count; i++)
                    {
                        trav_le1[i].x -= 20;
                    }
                    for (int i = 0; i < python_block_le1.Count; i++)
                    {
                        python_block_le1[i].x -= 20;
                    }
                    for (int i = 0; i < chick_point.Count; i++)
                    {
                        chick_point[i].x -= 20;
                    }

                    for (int i = 0; i < laser_block_le1.Count; i++)
                    {
                        laser_block_le1[i].x -= 20;
                    }
                    for (int i = 0; i < wolf_block_le1.Count; i++)
                    {
                        wolf_block_le1[i].x -= 20;
                    }
                    for (int i = 0; i < dragon_block_le1.Count; i++)
                    {
                        dragon_block_le1[i].x -= 20;
                    }
                    for (int i = 0; i < laser_enemy_block_le1.Count; i++)
                    {
                        laser_enemy_block_le1[i].x -= 20;
                    }
                    // Only move wolf if it's within visible area
                    if (wolf.x >= -100 && wolf.x <= this.ClientSize.Width + 100)
                    {
                        wolf.x -= 20;
                    }
                    // Only move dragon if it's within its boundaries
                    if (dragon.x >= dragon_block_le1[0].x && dragon.x <= dragon_block_le1[dragon_block_le1.Count - 1].x + dragon_block_le1[dragon_block_le1.Count - 1].img.Width)
                    {
                        dragon.x -= 20;
                    }
                    if (python.x >= -100 && python.x <= this.ClientSize.Width + 100)
                    {
                        python.x -= 20;
                    }
                    
                    laser_line.recdst.X -= 20;
                }
            }
        }
        int gravity_function()
        {
            int groundY = ClientSize.Height - hero.H;

            // block_up_of_lava
            foreach (var block in block_up_of_lava)
            {
                if (block.y >= hero.y + hero.H)
                {
                    if (hero.x + hero.W > block.x && hero.x < block.x + block.img.Width)
                    {
                        int newGround = block.y - hero.H;
                        if (newGround < groundY)
                        {
                            groundY = newGround;
                        }
                    }
                }
            }

            // block_le1
            foreach (var block in block_le1)
            {
                if (block.y >= hero.y + hero.H)
                {
                    if (hero.x + hero.W > block.x && hero.x < block.x + block.img.Width)
                    {
                        int newGround = block.y - hero.H;
                        if (newGround < groundY)
                        {
                            groundY = newGround;
                        }
                    }
                }
            }

            // wolf_block_le1
            foreach (var block in wolf_block_le1)
            {
                if (block.y >= hero.y + hero.H)
                {
                    if (hero.x + hero.W > block.x && hero.x < block.x + block.img.Width)
                    {
                        int newGround = block.y - hero.H;
                        if (newGround < groundY)
                        {
                            groundY = newGround;
                        }
                    }
                }
            }

            //  trav_le1
            foreach (var block in trav_le1)
            {
                if (block.y >= hero.y + hero.H)
                {
                    if (hero.x + hero.W > block.x && hero.x < block.x + block.img.Width)
                    {
                        int newGround = block.y - hero.H;
                        if (newGround < groundY)
                        {
                            groundY = newGround;
                        }
                    }
                }
            }
            
            foreach (var block in python_block_le1)
            {
                if (block.y >= hero.y + hero.H)
                {
                    if (hero.x + hero.W > block.x && hero.x < block.x + block.img.Width)
                    {
                        int newGround = block.y - hero.H;
                        if (newGround < groundY)
                        {
                            groundY = newGround;
                        }
                    }
                }
            }
            
            foreach (var block in laser_enemy_block_le1)
            {
                if (block.y >= hero.y + hero.H)
                {
                    if (hero.x + hero.W > block.x && hero.x < block.x + block.img.Width)
                    {
                        int newGround = block.y - hero.H;
                        if (newGround < groundY)
                        {
                            groundY = newGround;
                        }
                    }
                }
            }
            foreach (var block in dragon_block_le1)
            {
                if (block.y >= hero.y + hero.H)
                {
                    if (hero.x + hero.W > block.x && hero.x < block.x + block.img.Width)
                    {
                        int newGround = block.y - hero.H;
                        if (newGround < groundY)
                        {
                            groundY = newGround;
                        }
                    }
                }
            }
            return groundY;
        }
        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            this.Text = (e.X + "," + e.Y).ToString();
        }
        void make_block_fall()
        {
            for (int i = 0; i < block_up_of_lava.Count; i++)
            {
                if (block_up_of_lava[i].is_falling)
                {
                    block_up_of_lava[i].fall_speed += 1; // Increase fall speed (gravity)
                    block_up_of_lava[i].y += block_up_of_lava[i].fall_speed;


                }
            }

            // Check if hero is standing on block_up_of_lava
            foreach (var block in block_up_of_lava)
            {
                if (!block.is_falling &&
                    hero.y + hero.H >= block.y &&
                    hero.y + hero.H <= block.y + 5 && // Small threshold to detect standing
                    hero.x + hero.W > block.x &&
                    hero.x < block.x + block.img.Width)
                {
                    block.is_falling = true;
                    block.fall_speed = 0;
                }
            }
        }
        void check_hero_touching_lava()
        {
            foreach (var lava in lava_le1)
            {
                if (hero.x + hero.W > lava.x &&
                    hero.x < lava.x + lava.imgs[0].Width &&
                    hero.y + hero.H > lava.y &&
                    hero.y < lava.y + lava.imgs[0].Height)
                {
                    hero.hero_health = 0;
                    if (!hero.is_dead)
                    {
                        hero.is_dead = true;
                        hero.display_flag = 3;  // Set to death animation
                        hero.dead_frame_index = 0;
                    }
                    break;
                }
            }
        }
        void game_over_animation()
        {
            if (hero.show_game_over)
            {
                hero.game_over_frame_index++;
                if (hero.game_over_frame_index >= hero.game_over_frames.Count)
                {
                    hero.game_over_frame_index = 0;  // Loop the animation
                }

            }
        }
        void stair()
        {
            bool is_on_stair = false;

            foreach (var s in stair_le1)
            {
                if (hero.x + hero.W > s.x && hero.x < s.x + s.img.Width)
                {
                    gravity = false;

                    if (stair_flag == 1)
                    {
                        hero.y -= 5;


                        if (hero.y < stair_le1[0].y - hero.H)
                        {
                            hero.y = stair_le1[0].y - hero.H;
                        }
                    }
                    else if (stair_flag == 2)
                    {
                        hero.y += 5;


                        if (hero.y > stair_le1[stair_le1.Count - 2].y - hero.H)
                        {
                            hero.y = wolf_block_le1[0].y - hero.H;
                        }
                    }
                
                }
                else
                    gravity = true;
            }


        }
        private void Tt_Tick(object sender, EventArgs e)
        {
            if (is_intro_active || is_win_active)
            {
                drawdb();
                return;
            }

            // Check if hero reached the door
            if (!is_win_active && door_le1.Count > 0)
            {
                if (hero.x + hero.W > door_le1[0].x && 
                    hero.x < door_le1[0].x + door_le1[0].img.Width &&
                    hero.y + hero.H > door_le1[0].y && 
                    hero.y < door_le1[0].y + door_le1[0].img.Height)
                {
                    is_win_active = true;
                    current_win_frame = 0;
                    return;
                }
            }

            if (python_block_le1[0].x >= 700 && python_block_le1[0].x <= 720)
            {
                F_lo = 1;
            }
            if (F_lo != 1)
            {
                python.x = python_block_le1[0].x;
                wolf.x = wolf_block_le1[wolf_block_le1.Count / 2].x;
            }


            if (wolf_block_le1[0].x >= 700 && wolf_block_le1[0].x <= 720)
            {
                F_wolf = 0;
            }
            if (F_wolf != -1)
            {
                F_wolf = -1;
            }

            if (gravity)
            {

                int groundY = gravity_function();

                if (hero.y < groundY - 10)
                {
                    hero.y += 10;
                }
                else
                {
                    hero.y = groundY;
                }
            }


            //check_hero_touching_lava();
            // Update game over animation
            game_over_animation();
            stair();
            check_laser_enemey();
            hit_leser();
            laser();
            trav();
            frem_lava();
            shot();
            jump();
            if_idle();
            movehero();
            move_wolf();
            movearrow();
            move_python();
            move_dragon();
            make_block_fall();
            move_laser_enemies();
            enemy_hit_from_arrow(wolf);
            //enemy_hit_from_arrow(laser_enemy
            enemy_hit_from_arrow(python);
            enemy_hit_from_arrow(dragon);
            //check_hero_health_if_touch(wolf);
            check_hero_health_if_touch(python);
            check_hero_health_if_touch(dragon);
            //this.Text = hero.hero_health.ToString();
            Text = "HELL GAME";
            this.Text = hero.x.ToString();
            scllor();
            drawdb();
            ct_tick++;
        }
        void check_hero_health_if_touch(cenemyactor enemy)
        {
            if (hero.hero_health <= 0)
            {
                if (!hero.is_dead)  // Only set these values when hero first dies
                {
                    hero.is_dead = true;
                    hero.display_flag = 3;  // Set to death animation
                    hero.dead_frame_index = 0;
                }
            }
            int Y = python.y;
            int H = python.y + python.run_right[python.wf].Height;
            bool is_touching = false;

            if (hero.x + hero.walk_r_imges[hero.walk_frame_index].Width > python.x &&
                hero.x < python.x + python.run_right[python.wf].Width &&
                !python.is_dead &&
                hero.y <= Y &&
                hero.y + hero.walk_r_imges[hero.walk_frame_index].Height >= H - 17)
            {
                is_touching = true;
            }

            if (is_touching && !enemy.is_touched_hero && !hero.is_dead)
            {
                hero.hero_health--;
                enemy.is_touched_hero = true;
            }
            else if (!is_touching)
            {
                enemy.is_touched_hero = false;
            }
        }
        void laser()
        {
            if (f_laser_yz == 1 && py_laser2 <= ClientSize.Height - 70)
            {
                py_laser2 += 15;
            }
            //what zhwr
            if (ct_tick % 20 == 0 && py_laser2 >= ClientSize.Height - 70)
            {
                f_laser_yz = -1;
                py_laser2 = 500;
            }
            //what ela5tfa
            if (ct_tick % 30 == 0 && py_laser2 == 500)
            {
                f_laser_yz = 1;
            }
        }
        void hit_leser()
        {
            foreach (var item in laser_block_le1)
            {
                int x = item.x;
                int w = item.x + item.img.Width;
                int y = item.y;
                if (f_laser_yz == 1)
                {
                    if (hero.x >= x - 50 && hero.x <= w-50 && hero.y <= py_laser2&& hero.y>y)
                    {
                        hero.hero_health--;
                        hero.is_touched_laser = true;
                    }
                    else
                    {
                        hero.is_touched_laser = false;  
                    }
                }
            }
        }

        void trav()
        {
            int trav_speed = 5;

            for (int i = 0; i < trav_le1.Count; i++)
            {
                cimgactor trav = trav_le1[i];

                if (trav.dy == -1)
                {
                    if (trav.y >= ClientSize.Height / 2 - 150)
                    {
                        if (hero.x + hero.W > trav.x && hero.x < trav.x + trav.img.Width &&
                            hero.y + hero.H == trav.y)
                        {
                            hero.y -= trav_speed;
                        }

                        trav.y -= trav_speed;
                    }
                    else
                    {
                        trav.dy = 1;
                    }
                }

                if (trav.dy == 1)
                {
                    if (trav.y <= ClientSize.Height / 2)
                    {
                        if (hero.x + hero.W > trav.x && hero.x < trav.x + trav.img.Width &&
                            hero.y + hero.H == trav.y)
                        {
                            hero.y += trav_speed;
                        }

                        trav.y += trav_speed;
                    }
                    else
                    {
                        trav.dy = -1;
                    }
                }
            }
        }
        void frem_lava()
        {
            for (int i = 0; i < lava_le1.Count; i++)
            {
                if (lava_le1[i].iF < 1)
                {
                    lava_le1[i].iF++;
                }
                else
                {
                    lava_le1[i].iF = 0;
                }
            }
        }
        void move_python()
        {
            if (python.is_dead) return;
            python.wf++;
            if (python.wf > 5) python.wf = 0;

            python.x += python.dir;

            if (python.x + python.run_right[python.wf].Width > python_block_le1[python_block_le1.Count - 1].x + 50)
            {
                python.is_move_right = false;
                python.dir = -5;

            }
            if (python.x < python_block_le1[0].x)
            {
                python.is_move_right = true;
                python.dir = 5;

            }



        }
        void check_laser_enemey()
        {
            bool is_touching = false;
            if (laser_line.recdst.X + laser_line.recdst.Width > hero.x && laser_line.recdst.X + laser_line.recdst.Width < hero.x + hero.H && hero.y < laser_block_le1[0].y - hero.H)
            {
                is_touching = true;

            }

            if (is_touching && !laser_line.is_touched_hero)
            {
                hero.hero_health--;
                laser_line.is_touched_hero = true;
            }
            else if (!is_touching)
            {
                laser_line.is_touched_hero = false;
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
            if (hero.is_dead)
            {
                // Show death animation
                if (hero.dead_frame_index < hero.dead_imges.Count)
                {
                    g2.DrawImage(hero.dead_imges[hero.dead_frame_index], hero.x, hero.y);

                    // Add delay between frames
                    if (hero.death_animation_delay < 2)  // Adjust this number to change delay length
                    {
                        hero.death_animation_delay++;
                    }
                    else
                    {
                        hero.dead_frame_index++;  // Only increment frame after delay
                        hero.death_animation_delay = 0;  // Reset delay counter

                        // When death animation finishes, show game over
                        if (hero.dead_frame_index >= hero.dead_imges.Count)
                        {
                            hero.show_game_over = true;
                            hero.game_over_frame_index = 0;
                        }
                    }
                }
            }
            else
            {
                // Normal hero display code
                Bitmap hero_current_image;
                switch (hero.display_flag)
                {
                    case -1:
                        hero_current_image = hero.idle_imges[hero.idle_frame_index];
                        break;
                    case 0:
                        if (hero.is_move_right)
                        {
                            hero_current_image = hero.walk_r_imges[hero.walk_frame_index];
                        }
                        else
                        {
                            hero_current_image = hero.walk_l_imges[hero.walk_frame_index];
                        }
                        break;
                    case 1:
                        if (hero.is_move_right)
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
        }
        void if_idle()
        {
            if (rightflag == 0 && leftflag == 0 && upflag == 0 && downflag == 0 && f_flag == 0 && spaceflag == 0 && !isjump)
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
            if (hero.is_dead) return;

            cimghero h = hero;

            if (hero.y > 0)
            {
                if (upflag == 1 && jump_count == 0)
                {
                    hero.display_flag = 2;
                    isjump = true;
                    f_scroll_UP = 1;
                }
                else
                {
                    f_scroll_UP = -1;
                }
            }
            else
            {
                f_scroll_UP = -1;
            }

            if (hero.y < this.ClientSize.Height - 190)
            {
                //if (downflag == 1)
                //{
                //    h.y += 10;
                //    f_scroll_DOWN = 1;
                //}
                //else
                //{
                //    f_scroll_DOWN = -1;
                //}
            }
            else
            {
                f_scroll_DOWN = -1;
            }

            if (hero.x > door_le1[0].x)
            {
                if (leftflag == 1)
                {
                    f_scroll_L = 1;
                    hero.display_flag = 0;
                    h.x -= 10;
                    hero.walk_frame_index--;
                    if (hero.walk_frame_index < 0) hero.walk_frame_index = 7;
                }
                else
                {
                    f_scroll_L = -1;
                }
            }
            else
            {
                f_scroll_L = -1;
            }
            if (hero.x < stair_le1[0].x + 150)
            {
                if (rightflag == 1)
                {
                    f_scroll_R = 1;
                    hero.display_flag = 0;
                    h.x += 10;
                    hero.walk_frame_index++;
                    if (hero.walk_frame_index > 7) hero.walk_frame_index = 0;
                }
            }
            else
            {
                f_scroll_R = -1;
            }
            // shot

        }
        void jump()
        {
            if (isjump)
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
            else if (jump_count > 0)
            {
                int groundY = gravity_function();

                if (hero.y < groundY)
                {
                    hero.y += 20;
                    jump_count--;
                    if (hero.y > groundY) hero.y = groundY;
                }
                else
                {
                    jump_count = 0;
                }
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
                    stair_flag = 0;
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
                case Keys.U:
                    stair_flag = 0;
                    break;
            }
        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (is_intro_active)
            {
                if (e.KeyCode == Keys.Enter)
                {
                    is_intro_active = false;
                    intro_frames.Clear();
                    return;
                }
            }

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
                    stair_flag = 2;
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
                case Keys.U:
                    stair_flag = 1;
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

                if (hero.x + hero.walk_r_imges[hero.walk_frame_index].Width > wolf.x &&
                    hero.x < wolf.x + wolf.run_right[wolf.wf].Width && wolf.is_dead == false)
                {
                    if (hero.y + hero.walk_r_imges[hero.walk_frame_index].Height > wolf.y &&
                   hero.y + hero.walk_r_imges[hero.walk_frame_index].Height <= wolf.y + wolf.run_right[wolf.wf].Height)
                    {
                        wolf.is_attacking = true;
                        hero.hero_health--;
                        wolf.attack_frame_index = 0;
                        //hero.x = chick_point[0].x - 50;
                        //hero.y = chick_point[0].y-150;
                        //f_scroll_L = 1;
                        //leftflag = 1;
                    }
                }
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
            // Only move if hero is past last lava block
            else if (hero.x > lava_le1[lava_le1.Count-1].x)
            {
                // Then check if within detection range
                if (distance_to_hero <= wolf.DETECTION_RANGE && !wolf.is_in_cooldown && wolf.x > wolf_block_le1[0].x)
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
                else
                {
                    // Stay in place if out of detection range
                    wolf.wf++;
                    if (wolf.wf >= wolf.idle.Count)
                    {
                        wolf.wf = 0;
                    }
                }
            }
            // If hero is before last lava block, stay in place
            else
            {
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
            int abs;
            if (hero.x - wolf.x > 0)
            {
                abs = hero.x - wolf.x;
            }
            else
            {
                abs = hero.x - wolf.x;
                abs *= -1;
            }
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
            
            else if (wolf.is_in_cooldown || abs > wolf.DETECTION_RANGE)
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

            // Get the patrol boundaries from block_le1
            int leftBoundary = dragon_block_le1[0].x;
            int rightBoundary = dragon_block_le1[dragon_block_le1.Count - 1].x + dragon_block_le1[0].img.Width;

            // Only move dragon if it's within its boundaries
            if (dragon.x >= leftBoundary && dragon.x <= rightBoundary)
            {
                dragon.x += dragon.dir;
            }

            // Check boundaries and reverse direction if needed
            if (dragon.x < leftBoundary)
            {
                dragon.x = leftBoundary;  // Set to exact boundary
                dragon.is_move_right = true;
                dragon.dir = 10;
            }

            if (dragon.x + dragon.run_right[dragon.wf].Width > rightBoundary)
            {
                dragon.x = rightBoundary - dragon.run_right[dragon.wf].Width;  // Set to exact boundary
                dragon.is_move_right = false;
                dragon.dir = -10;
            }
        }

        void dragon_display(Graphics g2)
        {

            if (hero.x < 400 || (hero.y < 500 && hero.x < 500))
            {
                Bitmap dragon_current_image;

                if (dragon.is_dead)
                {
                    // Handle death animation if you have one
                    return;
                }
                //else if (dragon.is_attacking)
                //{
                //    if (dragon.is_move_right)
                //    {
                //        dragon_current_image = dragon.attack_right[dragon.attack_frame_index];
                //    }
                //    else
                //    {
                //        dragon_current_image = dragon.attack_left[dragon.attack_frame_index];
                //    }
                //}
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
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            // Load intro frames
            Bitmap temp;
           
            // Load you win frames
            for (int i = 1; i <= 100; i++)
            {
                cadvancedimgactor pnn = new cadvancedimgactor();
                string number = i.ToString();
                if (i < 10)
                    number = "00" + number;
                else if (i < 100)
                    number = "0" + number;
               
                string filename = "you win/ezgif-frame-" + number + ".png";
                Bitmap frame = new Bitmap(filename);
                pnn.img = frame;
                pnn.recdst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
                pnn.recsrc = new Rectangle(0, 0, frame.Width, frame.Height);
                you_win_frames.Add(pnn);
            }

            // Load intro frames
            for (int i = 1; i <= 188; i++)
            {
                cadvancedimgactor pnn = new cadvancedimgactor();
                string number = i.ToString();
                if (i < 10)
                    number = "00" + number;
                else if (i < 100)
                    number = "0" + number;
               

                string filename = "intro/ezgif-frame-" + number + ".png";

                Bitmap frame = new Bitmap(filename);
                pnn.img = frame;
                pnn.recdst = new Rectangle(0, 0, this.ClientSize.Width, this.ClientSize.Height);
                pnn.recsrc = new Rectangle(0, 0, frame.Width, frame.Height);
                intro_frames.Add(pnn);
            }


            creat_Background_le1();
            creat_lava_le1();
            creat_block_le1();
            creat_chick_point_le1();
            creat_door_le1();
            creat_stair_le1();
            creat_trav_le1();
            laser_blo_le1();
            dragon_block_le1.RemoveAt(2);
            dragon_block_le1.RemoveAt(2);
            dragon_block_le1.RemoveAt(2);


            dragon_block_le1.RemoveAt(4);
            dragon_block_le1.RemoveAt(4);
            dragon_block_le1.RemoveAt(4);
            // walk 
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            for (int i = 0; i < 8; i++)
            {
                temp = new Bitmap("walk/" + i + ".png");
                hero.walk_r_imges.Add(temp);
                temp = new Bitmap("walk_left/" + i + ".png");

                hero.H = temp.Height;

                hero.W = temp.Width;

                hero.walk_l_imges.Add(temp);

            }
            hero.x = 0; //this.ClientSize.Width - hero.walk_r_imges[0].Width;
            hero.y = this.ClientSize.Height - 70 - hero.H;

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

                python.x = python_block_le1[0].x;
                python.y = python_block_le1[0].y - python.run_right[0].Height + 17;
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

            //  wolf position - start near lava
            wolf.x = wolf_block_le1[wolf_block_le1.Count / 2].x;
            wolf.y = this.ClientSize.Height - 70 - wolf.idle[0].Height + 15;
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
            dragon.x = dragon_block_le1[0].x;  // Start at the left boundary
            dragon.y = dragon_block_le1[0].img.Height -100;
            dragon.is_move_right = true;
            dragon.dir = 10;
            // creat laser character
            for (int i = 0; i < 6; i++)
            {
                temp = new Bitmap("laser load/load laser" + (i + 1) + ".png");
                laser_enemy.idle.Add(temp);
            }
            laser_enemy.x = laser_enemy_block_le1[0].x;
            laser_enemy.health = 10;
            laser_enemy.y = laser_enemy_block_le1[0].y -170;  // Position above first block with 50px gap
            laser_enemy.wf = 0;  // Initialize animation frame
            laser_enemy.fly_count = 0;  // Use fly_count as delay counter

            // Load health bar images
            for (int i = 0; i < 5; i++)
            {
                temp = new Bitmap("health bar/0" + i + "_0.png");
                hero.health_bar.Add(temp);
            }

            // Load game over frames
            for (int i = 0; i < 95; i++)  // Assuming there are 5 game over frames
            {
                temp = new Bitmap("game over frames/game-over-game-" + i + ".png");
                hero.game_over_frames.Add(temp);
            }
            //laser line
            
            laser_line = new cadvancedimgactor();
            laser_line.img = new Bitmap("laser load/laser line.png");
            laser_line.recsrc = new Rectangle(0, 0, laser_line.img.Width, laser_line.img.Height);
            laser_line.recdst = new Rectangle(laser_enemy.x + laser_enemy.idle[0].Width,
                                         laser_enemy.y + laser_enemy.idle[0].Height / 2 - 35,
                                         0, // Start with 0 width
                                         laser_line.img.Height);
        }
        void drawscene(Graphics g2)
        {
            if (is_intro_active)
            {
                // Draw intro frame
                if (current_intro_frame < intro_frames.Count)
                {
                    // Center the intro frame
                    g2.DrawImage(intro_frames[current_intro_frame].img, intro_frames[current_intro_frame].recdst, intro_frames[current_intro_frame].recsrc,GraphicsUnit.Pixel);

                  

                    // Loop back to last frame
                    current_intro_frame++;
                    if (current_intro_frame >= intro_frames.Count)
                    {
                        current_intro_frame = intro_frames.Count - 1;
                    }
                }
                return;
            }

            if (is_win_active)
            {
                // Draw win frame
                if (current_win_frame < you_win_frames.Count)
                {
                    g2.DrawImage(you_win_frames[current_win_frame].img, you_win_frames[current_win_frame].recdst, you_win_frames[current_win_frame].recsrc, GraphicsUnit.Pixel);
                    current_win_frame++;
                    if (current_win_frame >= you_win_frames.Count)
                    {
                        current_win_frame = you_win_frames.Count - 1;
                    }
                }
                return;
            }

            g2.Clear(Color.White);

            // Draw health bar in top-left corner


            for (int i = 0; i < Background_le1.Count; i++)
            {
                g2.DrawImage(Background_le1[i].img, Background_le1[i].recdst, Background_le1[i].recsrc, GraphicsUnit.Pixel);
            }

            for (int i = 0; i < block_le1.Count; i++)
            {
                g2.DrawImage(block_le1[i].img, block_le1[i].x, block_le1[i].y);
            }
            for (int i = 0; i < block_up_of_lava.Count; i++)
            {
                g2.DrawImage(block_up_of_lava[i].img, block_up_of_lava[i].x, block_up_of_lava[i].y);
            }
            for (int i = 0; i < door_le1.Count; i++)
            {
                g2.DrawImage(door_le1[i].img, door_le1[i].x, door_le1[i].y);
            }
            for (int i = 0; i < linedoor_le1.Count; i++)
            {
                g2.DrawImage(linedoor_le1[i].img, linedoor_le1[i].x, linedoor_le1[i].y);
            }
            for (int i = 0; i < stair_le1.Count; i++)
            {
                g2.DrawImage(stair_le1[i].img, stair_le1[i].x, stair_le1[i].y);
            }
            for (int i = 0; i < lava_le1.Count; i++)
            {
                MUCImgActor pT = lava_le1[i];
                g2.DrawImage(pT.imgs[pT.iF], pT.x, pT.y);
            }
            for (int i = 0; i < trav_le1.Count; i++)
            {
                g2.DrawImage(trav_le1[i].img, trav_le1[i].x, trav_le1[i].y);
            }
            // Draw wolf blocks
            for (int i = 0; i < wolf_block_le1.Count; i++)
            {
                g2.DrawImage(wolf_block_le1[i].img, wolf_block_le1[i].x, wolf_block_le1[i].y);
            }
            // Draw dragon blocks
            for (int i = 0; i < dragon_block_le1.Count; i++)
            {
                g2.DrawImage(dragon_block_le1[i].img, dragon_block_le1[i].x, dragon_block_le1[i].y);
            }
            // Draw python blocks
            for (int i = 0; i < python_block_le1.Count; i++)
            {
                g2.DrawImage(python_block_le1[i].img, python_block_le1[i].x, python_block_le1[i].y);
            }
            for (int i = 0; i < laser_enemy_block_le1.Count; i++)
            {
                g2.DrawImage(laser_enemy_block_le1[i].img, laser_enemy_block_le1[i].x, laser_enemy_block_le1[i].y);
            }

            for (int i = 0; i < laser_block_le1.Count; i++)
            {
                Pen p = new Pen(Color.Red);

                g2.DrawLine(p, laser_block_le1[i].x+25, laser_block_le1[i].y+20, laser_block_le1[i].x+25, py_laser2);

                g2.DrawImage(laser_block_le1[i].img, laser_block_le1[i].x, laser_block_le1[i].y);
            }
            hero_display(g2);
            wolf_display(g2);
            dragon_display(g2);
            arrow_display(g2);
            python_display(g2);
            laser_display(g2);
            if (hero.hero_health > 0 && hero.hero_health <= 5)
            {
                g2.DrawImage(hero.health_bar[hero.hero_health - 1], 20, 20);
            }

            // Draw game over screen if hero is dead
            if (hero.show_game_over)
            {
                // Draw current game over frame in center of screen
                int x = (ClientSize.Width - hero.game_over_frames[0].Width) / 2;
                int y = (ClientSize.Height - hero.game_over_frames[0].Height) / 2;
                g2.DrawImage(hero.game_over_frames[hero.game_over_frame_index], x, y);
            }
            for (int i = 0; i < chick_point.Count; i++)
            {
                g2.DrawImage(chick_point[i].img, chick_point[i].x, chick_point[i].y);
            }

        }

        void move_laser_enemies()
        {
            if (laser_enemy.is_dead) return;

            // Update animation frame
            if (laser_enemy.fly_count > 0)
            {
                laser_enemy.fly_count--;
            }
            else
            {
                if (laser_enemy.freez == 1)
                {
                    // If frozen, stay on last frame
                    laser_enemy.wf = 5;
                    // When delay is complete, reset to frame 0 and unfreeze
                    if (laser_enemy.fly_count == 0)
                    {
                        laser_enemy.wf = 0;
                        laser_enemy.freez = 0;
                        // Reset laser line when animation resets
                        laser_line.recdst.Width = 0;
                    }
                }
                else
                {
                    if (laser_enemy.wf < laser_enemy.idle.Count - 1)
                    {
                        laser_enemy.freez = 0;
                        laser_enemy.wf++;
                    }
                    else
                    {
                        // When reaching last frame, start delay and freeze
                        laser_enemy.fly_count = 30;  
                        laser_enemy.freez = 1;
                        // Start laser line animation
                        laser_line.recdst.Width = 0;
                        laser_line.recdst.Height = laser_line.recsrc.Height;
                        laser_line.recdst.Y = laser_enemy.y + laser_enemy.idle[0].Height / 2 - 35;
                    }
                }
            }

            // Update laser line width when enemy is frozen
            if (laser_enemy.freez == 1)
            {
                laser_line.recdst.Width += 20; // Increase width over time
                laser_line.recdst.Height -= 2;
                laser_line.recdst.Y += 1;

                if (laser_line.recdst.Width > 1000) // Maximum width
                {
                    laser_line.recdst.Width = 1000;
                }
            }
        }

        void laser_display(Graphics g2)
        {
            if (!laser_enemy.is_dead)
            {
                g2.DrawImage(laser_enemy.idle[laser_enemy.wf], laser_enemy.x, laser_enemy.y);

                // Draw laser line when enemy is frozen
                if (laser_enemy.freez == 1)
                {
                    g2.DrawImage(laser_line.img, laser_line.recdst, laser_line.recsrc, GraphicsUnit.Pixel);
                }
            }
        }




    }

}


