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
    public int x, y, walk_frame_index = 0, shot_frame_index, dead_frame_index = 0, jump_frame_index = 0, idle_frame_index = 0;
    public bool is_move_right = true;
    public int hero_health = 1, display_flag = 0;
    public List<Bitmap> walk_r_imges = new List<Bitmap>();
    public List<Bitmap> walk_l_imges = new List<Bitmap>();
    public List<Bitmap> shot_r_imges = new List<Bitmap>();
    public List<Bitmap> shot_l_imges = new List<Bitmap>();
    public List<Bitmap> dead_imges = new List<Bitmap>();
    public List<Bitmap> jump_right_imges = new List<Bitmap>();
    public List<Bitmap> jump_left_imges = new List<Bitmap>();
    public List<Bitmap> idle_imges = new List<Bitmap>();
    public int H;
    public int W;


  }
  class cimgactor// used in arrows 
  {
    public int x, y, oldx, xd = -1, dy = -1;
    public Bitmap img;
    public bool is_moving_right;
  }
  class cenemyactor // for  enemy
  {

    public int x, y, wf = 0, dir = 10, dead_frame_index = 0, revive_waiting_time,freez;
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
    int rightflag = 0, leftflag = 0, upflag = 0, downflag = 0, spaceflag = 0,
        f_flag = 0, f_scroll_R = -1, f_scroll_L = -1, f_scroll_UP = -1, f_scroll_DOWN = -1, A, B, F_hit_blok = -1, F_lo = 0, F_g;


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
    List<cimgactor> block_down_le1 = new List<cimgactor>();
    List<cimgactor> door_le1 = new List<cimgactor>();
    List<cimgactor> linedoor_le1 = new List<cimgactor>();
    List<cimgactor> stair_le1 = new List<cimgactor>();
    List<cimgactor> trav_le1 = new List<cimgactor>();

    List<cimgactor> python_block_le1 = new List<cimgactor>();
    List<cimgactor> dragon_block_le1 = new List<cimgactor>();
    List<cimgactor> wolf_block_le1 = new List<cimgactor>();
    cenemyactor laser_enemy = new cenemyactor();

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
      //down rocik1
      for (int i = 0; i < 3; i++)
      {
        x = 0;
        if (i != 0)
        {

          cimgactor pnn = new cimgactor();
          pnn.img = new Bitmap("block/tile1.png");
          pnn.x = 0 + xx;
          pnn.y = ClientSize.Height - yy - 70;
          block_down_le1.Add(pnn);

          cimgactor pnnr1 = new cimgactor();
          pnnr1.img = new Bitmap("block/tile2.png");
          pnnr1.x = (50 + x) + xx;
          pnnr1.y = ClientSize.Height - yy - 70;
          block_down_le1.Add(pnnr1);
          x += pnnr1.img.Width;

          cimgactor pnn2 = new cimgactor();
          pnn2.img = new Bitmap("block/tile3.png");
          pnn2.x = (50 + x) + xx;
          pnn2.y = ClientSize.Height - yy - 70;
          block_down_le1.Add(pnn2);
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
        block_down_le1.Add(pnn);


        cimgactor pnnr1 = new cimgactor();
        pnnr1.img = new Bitmap("block/tile2.png");
        pnnr1.x = (0 + x) + xx;
        pnnr1.y = ClientSize.Height - yy - 70;
        block_down_le1.Add(pnnr1);
        x += pnnr1.img.Width;



        cimgactor pnn3 = new cimgactor();
        pnn3.img = new Bitmap("block/tile3.png");
        pnn3.x = (50 + x) + xx;
        pnn3.y = ClientSize.Height - yy - 70;
        block_down_le1.Add(pnn3);
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
          block_le1.Add(pnnr1);
          x += pnnr1.img.Width;
        }
        cimgactor pnn2 = new cimgactor();
        pnn2.img = new Bitmap("block/tile3.png");
        pnn2.x = (50 + x) + xx;
        pnn2.y = ClientSize.Height - yy - 70 - 500;
        block_le1.Add(pnn2);
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
        int count = block_le1.Count;
        count = block_le1.Count;
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
      for (int i = 0; i < 10; i++)
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
    void creat_dertion_le1()
    {
      int yy = 0;
      int xx = 0;
      int x = 0;

      cimgactor pnn = new cimgactor();
      pnn.img = new Bitmap("marker_statue/marker_statue3.png");
      pnn.x = 200;
      pnn.y = ClientSize.Height - 70 - 50;
      block_le1.Add(pnn);

      pnn = new cimgactor();
      pnn.img = new Bitmap("marker_statue/marker_statue3.png");
      pnn.x = 1250;
      pnn.y = ClientSize.Height - 70 - 50;
      block_le1.Add(pnn);

      pnn = new cimgactor();
      pnn.img = new Bitmap("marker_statue/marker_statue3.png");
      pnn.x = 1900;
      pnn.y = ClientSize.Height / 2;
      block_le1.Add(pnn);
    }
    void scllor()
    {
      if (leftflag == 1)
      {
        if (f_scroll_L == 1)
        {
          for (int i = 0; i < block_le1.Count; i++)
          {
            block_le1[i].x += 20;
          }
          for (int i = 0; i < block_down_le1.Count; i++)
          {
            block_down_le1[i].x += 20;
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


        }
      }
      if (rightflag == 1)
      {
        if (f_scroll_R == 1)
        {
          for (int i = 0; i < block_le1.Count; i++)
          {
            block_le1[i].x -= 20;
          }
          for (int i = 0; i < block_down_le1.Count; i++)
          {
            block_down_le1[i].x -= 20;
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


          for (int i = 0; i < wolf_block_le1.Count; i++)
          {
            wolf_block_le1[i].x -= 20;
          }
          for (int i = 0; i < dragon_block_le1.Count; i++)
          {
            dragon_block_le1[i].x -= 20;
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
        }
      }
      if (downflag == 1)
      {
        if (f_scroll_DOWN == 1)
        {
          for (int i = 0; i < block_le1.Count; i++)
          {
            block_le1[i].y -= 20;
          }
          for (int i = 0; i < block_down_le1.Count; i++)
          {
            block_down_le1[i].y -= 20;
          }
          for (int i = 0; i < door_le1.Count; i++)
          {
            door_le1[i].y -= 20;
          }
          for (int i = 0; i < linedoor_le1.Count; i++)
          {
            linedoor_le1[i].y -= 20;
          }
          for (int i = 0; i < stair_le1.Count; i++)
          {
            stair_le1[i].y -= 20;
          }
          for (int i = 0; i < lava_le1.Count; i++)
          {
            lava_le1[i].y -= 20;
          }
          for (int i = 0; i < trav_le1.Count; i++)
          {
            trav_le1[i].y -= 20;
          }
          // Only move wolf if it's within visible area
          if (wolf.x >= -100 && wolf.x <= this.ClientSize.Width + 100)
          {
            wolf.y -= 20;
          }
        }
      }
    }
    void hit_hero_ard()
    {

      foreach (var block in block_down_le1)
      {
        if (hero.y <= block.y + block.img.Height)
        {
          if (hero.x + hero.W > block.x && hero.x < block.x + block.img.Width)
          {
            //hero.y = block.y - hero.H;
            F_hit_blok = block.y - hero.H;

            break;
          }
        }
      }

      foreach (var block in block_le1)
      {
        if (hero.y <= block.y + block.img.Height)
        {
          if (hero.x + hero.W > block.x && hero.x < block.x + block.img.Width)
          {
            //hero.y = block.y - hero.H;
            F_hit_blok = block.y - hero.H;

            break;
          }
        }
      }

    }
    int get_ground_y()
    {
      int groundY = 9999999;

      foreach (var block in block_down_le1.Concat(block_le1))
      {
        bool isBelow = block.y >= hero.y + hero.H;
        bool isOverlapping = hero.x + hero.W > block.x && hero.x < block.x + block.img.Width;

        if (isBelow && isOverlapping)
        {
          int thisY = block.y - hero.H;
          if (thisY < groundY)
            groundY = thisY;

        }
      }

      return groundY;
    }

    ///
    private void Form1_MouseDown(object sender, MouseEventArgs e)
    {
      this.Text = (e.X + "," + e.Y).ToString();
    }

    private void Tt_Tick(object sender, EventArgs e)
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

      for (int i = 0; i < trav_le1.Count; i++)
      {
        if (trav_le1[i].dy == -1)
        {
          if (trav_le1[i].y >= 260)
          {
            trav_le1[i].y -= 5;
          }
          else
          {
            trav_le1[i].dy = 1;
          }
        }
        if (trav_le1[i].dy == 1)
        {
          if (trav_le1[i].y <= 510)
          {
            trav_le1[i].y += 5;
          }
          else
          {
            trav_le1[i].dy = -1;
          }
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
      int groundY = get_ground_y();

      if (hero.y < groundY - 10)
      {
        hero.y += 10;
      }
      else
      {
        hero.y = groundY;
      }

      movehero();
      shot();
      movearrow();
      jump();
      if_idle();
      move_dragon();
      move_wolf();
      move_python();
      move_laser_enemies();
      enemy_hit_from_arrow(python);
      enemy_hit_from_arrow(wolf);
      enemy_hit_from_arrow(dragon);
      check_hero_health_if_touch(python);
      check_hero_health_if_touch(wolf);
      check_hero_health_if_touch(dragon);
      //this.Text=hero.hero_health.ToString();
      this.Text = hero.x.ToString();
      scllor();
      drawdb();
    }
    void check_hero_health_if_touch(cenemyactor enemy)
    {
      bool is_touching = false;

      if (hero.x + hero.walk_r_imges[hero.walk_frame_index].Width > enemy.x &&
          hero.x < enemy.x + enemy.run_right[enemy.wf].Width && enemy.is_dead == false)
      {


        if (hero.y + hero.walk_r_imges[hero.walk_frame_index].Height > enemy.y && hero.y < enemy.y + enemy.run_right[enemy.wf].Height)
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
      if (dead_flag == 1) return;

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
        int groundY = get_ground_y();

        if (hero.y < groundY)
        {
          hero.y += 20;
          jump_count--;
          if (hero.y > groundY) hero.y = groundY; // للتأكد إنه يقف عليه تمامًا
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
      // If out of detection range, patrol between lava and stairs
      else
      {
        wolf.wf++;
        if (wolf.wf >= wolf.idle.Count)
        {
          wolf.wf = 0;
        }

        // Get current positions of lava and stairs
        int lavaStartX = wolf_block_le1[wolf_block_le1.Count - 1].x;
        int stairsX = wolf_block_le1[0].x;

        // Patrol movement
        if (wolf.is_move_right)
        {
          wolf.x += 3;
          if (wolf.x >= stairsX)
          {
            wolf.is_move_right = false;
          }
        }
        else
        {
          wolf.x -= 3;
          if (wolf.x <= lavaStartX)
          {
            wolf.is_move_right = true;
          }
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
    }
    private void Form1_Load(object sender, EventArgs e)
    {

      creat_Background_le1();
      creat_lava_le1();
      creat_block_le1();
      creat_dertion_le1();
      creat_door_le1();
      creat_stair_le1();
      creat_trav_le1();
      // walk 
      off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

      Bitmap temp;
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
      hero.y = this.ClientSize.Height - 60 - hero.H;

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
      wolf.x = lava_le1[0].x;
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
      dragon.y = dragon_block_le1[0].img.Height + 100;
      dragon.is_move_right = true;
      dragon.dir = 10;
      // creat laser character
      for (int i = 0; i < 6; i++)
      {
        temp = new Bitmap("laser load/load laser" + (i + 1) + ".png");
        laser_enemy.idle.Add(temp);
      }
      laser_enemy.x = 100;
      laser_enemy.health = 10;
      laser_enemy.y = ClientSize.Height - laser_enemy.idle[0].Height;
      laser_enemy.wf = 0;  // Initialize animation frame
      laser_enemy.fly_count = 0;  // Use fly_count as delay counter

    }
    void drawscene(Graphics g2)
    {
      g2.Clear(Color.White);
      for (int i = 0; i < Background_le1.Count; i++)
      {
        g2.DrawImage(Background_le1[i].img, Background_le1[i].recdst, Background_le1[i].recsrc, GraphicsUnit.Pixel);
      }

      for (int i = 0; i < block_le1.Count; i++)
      {
        g2.DrawImage(block_le1[i].img, block_le1[i].x, block_le1[i].y);
      }
      for (int i = 0; i < block_down_le1.Count; i++)
      {
        g2.DrawImage(block_down_le1[i].img, block_down_le1[i].x, block_down_le1[i].y);
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
      hero_display(g2);
      wolf_display(g2);
      dragon_display(g2);
      arrow_display(g2);
      python_display(g2);
      laser_display(g2);
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
            laser_enemy.fly_count = 10;
            laser_enemy.freez = 1;
          }
        }
      }
    }

    void laser_display(Graphics g2)
    {
      if (!laser_enemy.is_dead)
      {
        g2.DrawImage(laser_enemy.idle[laser_enemy.wf], laser_enemy.x, laser_enemy.y);
      }
    }



  }

}

