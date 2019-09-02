using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
/// <summary>
/// 为了便于解析测温区域的数据 现在只支持 一条测温线L1  四个测温点S2 S3 S4 S5 三个测温区域 A6 A7 A8 以上
/// </summary>
namespace InfraredAnalyze
{
    public partial class FrmMeasureTemperConfig : Form
    {
        public FrmMeasureTemperConfig()
        {
            InitializeComponent();
            ucPbx1 = (UCPbx)FromHandle(StaticClass.tempIntPtr);
            pnlMonitor.Controls.Add(ucPbx1);
        }

        UCPbx ucPbx1;
        private void FrmMeasureTemperConfig_Load(object sender, EventArgs e)
        {
            if (StaticClass.tempConnect < 0|| StaticClass.tempMonitor < 0)
            {
                MessageBox.Show("连接异常！请重新连接！");
                foreach (Control control in this.Controls)
                {
                    control.Enabled = false;
                }
                return;
            }
            Get_Area_Param();
        }

        SqlCreate sqlCreate = new SqlCreate();
        List<DMSDK.temperSpot> list_All_Spot;
        DMSDK.temperSpot Struct_temperSpot;

        List<DMSDK.temperArea> list_All_Area;
        DMSDK.temperArea Struct_temperArea;

        List<DMSDK.temperLine> list_All_Line;
        DMSDK.temperLine Struct_temperLine;

        private void Get_Area_Param()//从数据库中加载 测温目标的位置信息
        {
            try
            {
                list_All_Spot = new List<DMSDK.temperSpot>();
                list_All_Area = new List<DMSDK.temperArea>();
                list_All_Line = new List<DMSDK.temperLine>();
                list_All_Spot = sqlCreate.Select_All_Spot(StaticClass.Temper_CameraId, "S",StaticClass.DataBaseName);
                list_All_Area = sqlCreate.Select_All_Area(StaticClass.Temper_CameraId, "A", StaticClass.DataBaseName);
                list_All_Line = sqlCreate.Select_All_Line(StaticClass.Temper_CameraId, "L", StaticClass.DataBaseName);
                Struct_temperSpot = new DMSDK.temperSpot();
                Struct_temperArea = new DMSDK.temperArea();
                Struct_temperLine = new DMSDK.temperLine();
                if (list_All_Spot.Count > 0 && list_All_Area.Count > 0 && list_All_Line.Count > 0)
                {
                    for (int i = 0; i < list_All_Spot.Count; i++)
                    {
                        Struct_temperSpot = (DMSDK.temperSpot)list_All_Spot[i];
                        ((TextBox)(tabSpot.Controls.Find("tbxSpot_" + (i + 1) + "_X", false)[0])).Text = Struct_temperSpot.X1.ToString();
                        ((TextBox)(tabSpot.Controls.Find("tbxSpot_" + (i + 1) + "_Y", false)[0])).Text = Struct_temperSpot.Y1.ToString();
                        ((TextBox)(tabSpot.Controls.Find("tbxSpot_" + (i + 1) + "_Emiss", false)[0])).Text = Struct_temperSpot.Emiss.ToString();
                    }
                    for (int i = 0; i < list_All_Area.Count; i++)
                    {
                        Struct_temperArea = (DMSDK.temperArea)list_All_Area[i];
                        ((TextBox)(tabAreas.Controls.Find("tbxArea_" + (i + 1) + "_X1", false)[0])).Text = Struct_temperArea.X1.ToString();
                        ((TextBox)(tabAreas.Controls.Find("tbxArea_" + (i + 1) + "_Y1", false)[0])).Text = Struct_temperArea.Y1.ToString();
                        ((TextBox)(tabAreas.Controls.Find("tbxArea_" + (i + 1) + "_X2", false)[0])).Text = Struct_temperArea.X2.ToString();
                        ((TextBox)(tabAreas.Controls.Find("tbxArea_" + (i + 1) + "_Y2", false)[0])).Text = Struct_temperArea.Y2.ToString();
                        ((TextBox)(tabAreas.Controls.Find("tbxArea_" + (i + 1) + "_Emiss", false)[0])).Text = Struct_temperArea.Emiss.ToString();
                        ((ComboBox)(tabAreas.Controls.Find("cbxMeasureType_" + (i + 1) + "", false)[0])).SelectedIndex = Struct_temperArea.MeasureType;//cbxMeasureType_1
                    }
                    for (int i = 0; i < list_All_Line.Count; i++)
                    {
                        Struct_temperLine = (DMSDK.temperLine)list_All_Line[i];
                        ((TextBox)(tabLine.Controls.Find("tbxLine_" + (i + 1) + "_X1", false)[0])).Text = Struct_temperLine.X1.ToString();//
                        ((TextBox)(tabLine.Controls.Find("tbxLine_" + (i + 1) + "_Y1", false)[0])).Text = Struct_temperLine.Y1.ToString();//
                        ((TextBox)(tabLine.Controls.Find("tbxLine_" + (i + 1) + "_X2", false)[0])).Text = Struct_temperLine.X2.ToString();//
                        ((TextBox)(tabLine.Controls.Find("tbxLine_" + (i + 1) + "_Y2", false)[0])).Text = Struct_temperLine.Y2.ToString();//
                        ((TextBox)(tabLine.Controls.Find("tbxLine_" + (i + 1) + "_X3", false)[0])).Text = Struct_temperLine.X3.ToString();//
                        ((TextBox)(tabLine.Controls.Find("tbxLine_" + (i + 1) + "_Y3", false)[0])).Text = Struct_temperLine.Y3.ToString();//
                        ((TextBox)(tabLine.Controls.Find("tbxLine_" + (i + 1) + "_Emiss", false)[0])).Text = Struct_temperLine.Emiss.ToString();//
                    }
                }
                else
                {
                    MessageBox.Show("测温参数-数据库异常！请检查数据库！");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAdd_Line_1_Click(object sender, EventArgs e)
        {
            Set_Line(btnAdd_Line_1, tbxLine_1_X1, tbxLine_1_Y1, tbxLine_1_X2, tbxLine_1_Y2, tbxLine_1_X3, tbxLine_1_Y3, tbxLine_1_Emiss, btnClear_Line_1);
        }

        private void btnAdd_Spot_1_Click(object sender, EventArgs e)
        {
            Set_Spot(btnAdd_Spot_1, tbxSpot_1_X, tbxSpot_1_Y, tbxSpot_1_Emiss, btnClear_Spot_1);
        }

        private void btnAdd_Spot_2_Click(object sender, EventArgs e)
        {
            Set_Spot(btnAdd_Spot_2, tbxSpot_2_X, tbxSpot_2_Y, tbxSpot_2_Emiss, btnClear_Spot_2);
        }

        private void btnAdd_Spot_3_Click(object sender, EventArgs e)
        {
            Set_Spot(btnAdd_Spot_3, tbxSpot_3_X, tbxSpot_3_Y, tbxSpot_3_Emiss, btnClear_Spot_3);
        }

        private void btnAdd_Spot_4_Click(object sender, EventArgs e)
        {
            Set_Spot(btnAdd_Spot_4, tbxSpot_4_X, tbxSpot_4_Y, tbxSpot_4_Emiss, btnClear_Spot_4);
        }

        private void btnAdd_Area_1_Click(object sender, EventArgs e)
        {
            Set_Area(btnAdd_Area_1, tbxArea_1_X1, tbxArea_1_Y1, tbxArea_1_X2, tbxArea_1_Y2, tbxArea_1_Emiss, cbxMeasureType_1, btnClear_Area_1);

        }

        private void btnAdd_Area_2_Click(object sender, EventArgs e)
        {
            Set_Area(btnAdd_Area_2, tbxArea_2_X1, tbxArea_2_Y1, tbxArea_2_X2, tbxArea_2_Y2, tbxArea_2_Emiss, cbxMeasureType_2, btnClear_Area_2);

        }

        private void btnAdd_Area_3_Click(object sender, EventArgs e)
        {
            Set_Area(btnAdd_Area_3, tbxArea_3_X1, tbxArea_3_Y1, tbxArea_3_X2, tbxArea_3_Y2, tbxArea_3_Emiss, cbxMeasureType_3, btnClear_Area_3);

        }

        private void btnAdd_Area_4_Click(object sender, EventArgs e)
        {
            Set_Area(btnAdd_Area_4, tbxArea_4_X1, tbxArea_4_Y1, tbxArea_4_X2, tbxArea_4_Y2, tbxArea_4_Emiss, cbxMeasureType_4, btnClear_Area_4);

        }

        string type;
        bool IsSet_Spot = false;
        bool IsSet_Area = false;
        bool IsSet_Line = false;

        #region//添加pbx 存放截图
        PictureBox pbxBitmap;
        private void Create_Pbx()
        {
            pbxBitmap = new PictureBox();
            pbxBitmap.Height = ucPbx1.Height;
            pbxBitmap.Width = ucPbx1.Width;
            ucPbx1.Controls.Add(pbxBitmap);
            pbxBitmap.Parent = ucPbx1;
            pbxBitmap.MouseEnter += PbxBitmap_MouseEnter;
            pbxBitmap.MouseLeave += PbxBitmap_MouseLeave;
            pbxBitmap.MouseDown += PbxBitmap_MouseDown;
            pbxBitmap.MouseMove += PbxBitmap_MouseMove;
            pbxBitmap.MouseUp += PbxBitmap_MouseUp;
            pen = new Pen(Color.Green, 3);
            bitmapDraw = new Bitmap(pbxBitmap.Width, pbxBitmap.Height);
            graphicsDraw = Graphics.FromImage(bitmapDraw);
            bitmap = new Bitmap(ucPbx1.Width, ucPbx1.Height);
            graphics = Graphics.FromImage(bitmap);
            //Size size = new Size(320,240);
            // Rectangle rectangle = new Rectangle(pbxScreen.Location, size);
            graphics.CopyFromScreen(ucPbx1.PointToScreen(Point.Empty), Point.Empty, bitmap.Size);//截图 会被遮挡
            //pbxScreen.DrawToBitmap(bitmap, rectangle);
            pbxBitmap.BackgroundImage = bitmap;
        }
        #endregion

        bool IsPbxMouseDown = false;
        ToolTip toolTip = new ToolTip();
        Point point;
        Point[] points = new Point[2];
        Point[] pointss = new Point[8];

        public Point[] Draw_Cross(Point point)//计算十字架的点坐标
        {
            Point[] points = new Point[8];
            Point temp_Point1 = new Point(point.X - 35, point.Y);
            Point temp_Point2 = new Point(point.X - 5, point.Y);
            Point temp_Point3 = new Point(point.X + 5, point.Y);
            Point temp_Point4 = new Point(point.X + 35, point.Y);
            Point temp_Point5 = new Point(point.X, point.Y - 5);
            Point temp_Point6 = new Point(point.X, point.Y - 35);
            Point temp_Point7 = new Point(point.X, point.Y + 5);
            Point temp_Point8 = new Point(point.X, point.Y + 35);
            points[0] = temp_Point1;
            points[1] = temp_Point2;
            points[2] = temp_Point3;
            points[3] = temp_Point4;
            points[4] = temp_Point5;
            points[5] = temp_Point6;
            points[6] = temp_Point7;
            points[7] = temp_Point8;
            return points;
        }

        private void PbxBitmap_MouseDown(object sender, MouseEventArgs e)//鼠标按下事件
        {
            if (IsSet_Area || IsSet_Line || IsSet_Spot)
            {
                IsPbxMouseDown = true;
                points[0] = new Point(e.X, e.Y);
            }
            if (IsSet_Spot)
            {
                pointss = Draw_Cross(e.Location);//
                graphicsDraw.Clear(Color.Transparent);//
                for (int i = 0; i < pointss.Length; i = i + 2)
                {
                    graphicsDraw.DrawLine(pen, pointss[i], pointss[i + 1]);
                }
                pbxBitmap.Image = bitmapDraw;
                if (tbxSpot_1_X.Enabled)
                {
                    tbxSpot_1_X.Text = (e.X / 2).ToString();
                    tbxSpot_1_Y.Text = (e.Y / 2).ToString();
                }
                else if (tbxSpot_2_X.Enabled)
                {
                    tbxSpot_2_X.Text = (e.X / 2).ToString();
                    tbxSpot_2_Y.Text = (e.Y / 2).ToString();
                }
                else if (tbxSpot_3_X.Enabled)
                {
                    tbxSpot_3_X.Text = (e.X / 2).ToString();
                    tbxSpot_3_Y.Text = (e.Y / 2).ToString();
                }
                else if (tbxSpot_4_X.Enabled)
                {
                    tbxSpot_4_X.Text = (e.X / 2).ToString();
                    tbxSpot_4_Y.Text = (e.Y / 2).ToString();
                }
            }
        }

        private void PbxBitmap_MouseLeave(object sender, EventArgs e)//鼠标离开事件
        {
            this.Cursor = Cursors.Default;
        }

        private void PbxBitmap_MouseEnter(object sender, EventArgs e)//鼠标进入时间
        {
            if (IsSet_Area || IsSet_Spot || IsSet_Line)
            {
                this.Cursor = Cursors.Cross;
            }
        }

        private void PbxBitmap_MouseUp(object sender, MouseEventArgs e)//鼠标按下事件
        {
            IsPbxMouseDown = false;
        }

        private void PbxBitmap_MouseMove(object sender, MouseEventArgs e)//鼠标移动事件
        {
            if (this.Cursor == Cursors.Cross)
            {
                point = new Point(e.X, e.Y);
                toolTip.SetToolTip(pbxBitmap, "X:" + point.X / 2 + " " + "Y:" + point.Y / 2);
            }
            if (IsPbxMouseDown)
            {
                if (IsSet_Area)
                {
                    points[1] = new Point(e.X, e.Y);
                    graphicsDraw.Clear(Color.Transparent);
                    graphicsDraw.DrawRectangle(pen, points[0].X, points[0].Y, points[1].X - points[0].X, points[1].Y - points[0].Y);
                    pbxBitmap.Image = bitmapDraw;
                    if (tbxArea_1_X1.Enabled)
                    {
                        tbxArea_1_X1.Text = (points[0].X / 2).ToString();
                        tbxArea_1_Y1.Text = (points[0].Y / 2).ToString();
                        tbxArea_1_X2.Text = (points[1].X / 2).ToString();
                        tbxArea_1_Y2.Text = (points[1].Y / 2).ToString();
                    }
                    else if (tbxArea_2_X1.Enabled)
                    {
                        tbxArea_2_X1.Text = (points[0].X / 2).ToString();
                        tbxArea_2_Y1.Text = (points[0].Y / 2).ToString();
                        tbxArea_2_X2.Text = (points[1].X / 2).ToString();
                        tbxArea_2_Y2.Text = (points[1].Y / 2).ToString();
                    }
                    else if (tbxArea_3_X1.Enabled)
                    {
                        tbxArea_3_X1.Text = (points[0].X / 2).ToString();
                        tbxArea_3_Y1.Text = (points[0].Y / 2).ToString();
                        tbxArea_3_X2.Text = (points[1].X / 2).ToString();
                        tbxArea_3_Y2.Text = (points[1].Y / 2).ToString();
                    }
                    else if (tbxArea_4_X1.Enabled)
                    {
                        tbxArea_4_X1.Text = (points[0].X / 2).ToString();
                        tbxArea_4_Y1.Text = (points[0].Y / 2).ToString();
                        tbxArea_4_X2.Text = (points[1].X / 2).ToString();
                        tbxArea_4_Y2.Text = (points[1].Y / 2).ToString();
                    }
                }
                else if (IsSet_Line)
                {
                    points[1] = new Point(e.X, e.Y);
                    graphicsDraw.Clear(Color.Transparent);
                    graphicsDraw.DrawLine(pen, points[0].X, points[0].Y, points[1].X, points[1].Y);
                    pbxBitmap.Image = bitmapDraw;
                    if (tbxLine_1_X1.Enabled)
                    {
                        tbxLine_1_X1.Text = (points[0].X / 2).ToString();
                        tbxLine_1_Y1.Text = (points[0].Y / 2).ToString();
                        tbxLine_1_X2.Text = (points[1].X / 2).ToString();
                        tbxLine_1_Y2.Text = (points[1].Y / 2).ToString();
                        tbxLine_1_X3.Text = ((points[0].X + points[1].X) / 4).ToString();
                        tbxLine_1_Y3.Text = ((points[0].Y + points[1].Y) / 4).ToString();
                    }
                }
            }
        }

        #region//释放GDI+资源
        Graphics graphics;
        Bitmap bitmap;
        Graphics graphicsDraw;
        Bitmap bitmapDraw;
        Pen pen;
        private void Dispose_Graph()
        {
            pbxBitmap.Dispose();
            graphics.Dispose();
            bitmap.Dispose();
            pen.Dispose();
            graphicsDraw.Dispose();
            bitmapDraw.Dispose();
        }
        #endregion

        #region//设置测温点
        private void Set_Spot(Button btnAdd_Spot, TextBox tbxSpot_X, TextBox tbxSpot_Y, TextBox tbxSpot_Emiss, Button btnClear_Spot)
        {
            try
            {
                if (btnAdd_Spot.Text == "编辑")
                {
                    IsSet_Spot = true;
                    tbxSpot_X.Enabled = true;
                    tbxSpot_Y.Enabled = true;
                    tbxSpot_Emiss.Enabled = true;
                    btnAdd_Spot.Text = "确认";
                    btnClear_Spot.Text = "取消";
                    Create_Pbx();
                    foreach (Control control in pnlBtnSpot.Controls)
                    {
                        control.Enabled = false;
                    }
                    tabAreas.Enabled = false;
                    tabLine.Enabled = false;
                    btnAdd_Spot.Enabled = true;
                    btnClear_Spot.Enabled = true;
                }
                else if (btnAdd_Spot.Text == "确认")
                {
                    int x = Convert.ToInt32(tbxSpot_X.Text);
                    int y = Convert.ToInt32(tbxSpot_Y.Text);
                    int emiss = Convert.ToInt32(tbxSpot_1_Emiss.Text);
                    if (x <= 0 || x > 320 || y <= 0 || y > 240)
                    {
                        MessageBox.Show("请输入合适的坐标");
                        return;
                    }
                    if (tbxSpot_1_X.Enabled)
                    {
                        type = "S2";
                        DMSDK.DM_SetSpot(StaticClass.tempConnect, 2, x - 2, y - 2, emiss);
                        sqlCreate.Update_Spot(StaticClass.Temper_CameraId, type, x - 2, y - 2, emiss,StaticClass.DataBaseName);
                    }
                    else if (tbxSpot_2_X.Enabled)
                    {
                        type = "S3";
                        DMSDK.DM_SetSpot(StaticClass.tempConnect, 3, x - 2, y - 2, emiss);
                        sqlCreate.Update_Spot(StaticClass.Temper_CameraId, type, x - 2, y - 2, emiss, StaticClass.DataBaseName);
                    }
                    else if (tbxSpot_3_X.Enabled)
                    {
                        type = "S4";
                        DMSDK.DM_SetSpot(StaticClass.tempConnect, 4, x - 2, y - 2, emiss);
                        sqlCreate.Update_Spot(StaticClass.Temper_CameraId, type, x - 2, y - 2, emiss, StaticClass.DataBaseName);
                    }
                    else if (tbxSpot_4_X.Enabled)
                    {
                        type = "S5";
                        DMSDK.DM_SetSpot(StaticClass.tempConnect, 5, x - 2, y - 2, emiss);
                        sqlCreate.Update_Spot(StaticClass.Temper_CameraId, type, x - 2, y - 2, emiss, StaticClass.DataBaseName);
                    }
                    Cancel_SetSpot(type, btnAdd_Spot, tbxSpot_X, tbxSpot_Y, tbxSpot_Emiss, btnClear_Spot);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "设置测温点失败！");
            }
        }
        #endregion

        #region//设置测温区域
        private void Set_Area(Button btnAdd_Area, TextBox tbxArea_X1, TextBox tbxArea_Y1, TextBox tbxArea_X2, TextBox tbxArea_Y2, TextBox tbxArea_Emiss, ComboBox cbxMeasureType, Button btnClear_Area)//设置测温区域
        {
            if (btnAdd_Area.Text == "编辑") //C++中将画面绑定到控件上了 无法在画面中动态的绘制矩形  转而为 截图绘制。
            {
                IsSet_Area = true;
                tbxArea_X1.Enabled = true;
                tbxArea_Y1.Enabled = true;
                tbxArea_X2.Enabled = true;
                tbxArea_Y2.Enabled = true;
                tbxArea_Emiss.Enabled = true;
                cbxMeasureType.Enabled = true;
                btnAdd_Area.Text = "确认";
                btnClear_Area.Text = "取消";
                Create_Pbx();
                foreach (Control control in pnlBtnArea.Controls)
                {
                    control.Enabled = false;
                }
                btnAdd_Area.Enabled = true;
                btnClear_Area.Enabled = true;
                tabSpot.Enabled = false;
                tabLine.Enabled = false;
            }
            else if (btnAdd_Area.Text == "确认")
            {
                try
                {
                    if (cbxMeasureType.SelectedIndex == -1)
                    {
                        MessageBox.Show("请选择区域测温方式！");
                        return;
                    }
                    int x1 = Convert.ToInt32(tbxArea_X1.Text);
                    int y1 = Convert.ToInt32(tbxArea_Y1.Text);
                    int x2 = Convert.ToInt32(tbxArea_X2.Text);
                    int y2 = Convert.ToInt32(tbxArea_Y2.Text);
                    int emiss = Convert.ToInt32(tbxArea_Emiss.Text);
                    int messuretype = Convert.ToInt32(cbxMeasureType.SelectedIndex);
                    if (x1 < 0 || x1 > 320 || y1 < 0 || y1 > 240 || x2 < 0 || x2 > 320 || y2 < 0 || y2 > 240)
                    {
                        MessageBox.Show("请输入合适的坐标");
                        return;
                    }
                    if (tbxArea_1_X1.Enabled)
                    {
                        type = "A6";
                        DMSDK.DM_SetArea(StaticClass.tempConnect, 6, x1, y1, x2, y2, emiss, messuretype);
                        sqlCreate.Update_Area(StaticClass.Temper_CameraId, "A6", x1, y1, x2, y2, emiss, messuretype, StaticClass.DataBaseName);
                    }
                    else if (tbxArea_2_X1.Enabled)
                    {
                        type = "A7";
                        DMSDK.DM_SetArea(StaticClass.tempConnect, 7, x1, y1, x2, y2, emiss, messuretype);
                        sqlCreate.Update_Area(StaticClass.Temper_CameraId, "A7", x1, y1, x2, y2, emiss, messuretype, StaticClass.DataBaseName);
                    }
                    else if (tbxArea_3_X1.Enabled)
                    {
                        type = "A8";
                        DMSDK.DM_SetArea(StaticClass.tempConnect, 8, x1, y1, x2, y2, emiss, messuretype);
                        sqlCreate.Update_Area(StaticClass.Temper_CameraId, "A8", x1, y1, x2, y2, emiss, messuretype, StaticClass.DataBaseName);
                    }
                    else if (tbxArea_4_X1.Enabled)
                    {
                        type = "A9";
                        DMSDK.DM_SetArea(StaticClass.tempConnect, 9, x1, y1, x2, y2, emiss, messuretype);
                        sqlCreate.Update_Area(StaticClass.Temper_CameraId, "A9", x1, y1, x2, y2, emiss, messuretype, StaticClass.DataBaseName);
                    }
                    Cancel_SetArea(type, btnAdd_Area, tbxArea_X1, tbxArea_Y1, tbxArea_X2, tbxArea_Y2, tbxArea_Emiss, cbxMeasureType, btnClear_Area);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "设置测温区域失败！");
                }
            }
        }
        #endregion

        #region//设置测温线
        private void Set_Line(Button btnAdd_Line, TextBox tbxLine_X1, TextBox tbxLine_Y1, TextBox tbxLine_X2, TextBox tbxLine_Y2, TextBox tbxLine_X3, TextBox tbxLine_Y3, TextBox tbxLine_Emiss, Button btnClear_Line)
        {
            if (btnAdd_Line.Text == "编辑")
            {
                IsSet_Line = true;
                tbxLine_X1.Enabled = true;
                tbxLine_Y1.Enabled = true;
                tbxLine_X2.Enabled = true;
                tbxLine_Y2.Enabled = true;
                tbxLine_X3.Enabled = true;
                tbxLine_Y3.Enabled = true;
                tbxLine_Emiss.Enabled = true;
                btnAdd_Line.Text = "确认";
                btnClear_Line.Text = "取消";
                Create_Pbx();
                foreach (Control control in pnlBtnLine.Controls)
                {
                    control.Enabled = false;
                }
                btnAdd_Line.Enabled = true;
                btnClear_Line.Enabled = true;
                tabSpot.Enabled = false;
                tabAreas.Enabled = false;
            }
            else if (btnAdd_Line.Text == "确认")
            {
                try
                {
                    int x1 = Convert.ToInt32(tbxLine_X1.Text);
                    int y1 = Convert.ToInt32(tbxLine_Y1.Text);
                    int x2 = Convert.ToInt32(tbxLine_X2.Text);
                    int y2 = Convert.ToInt32(tbxLine_Y2.Text);
                    int x3 = Convert.ToInt32(tbxLine_X3.Text);
                    int y3 = Convert.ToInt32(tbxLine_Y3.Text);
                    int emiss = Convert.ToInt32(tbxLine_Emiss.Text);
                    if (x1 <= 0 || x1 > 320 || y1 <= 0 || y1 > 240 || x2 <= 0 || x2 > 320 || y2 <= 0 || y2 > 240)
                    {
                        MessageBox.Show("请输入合适的坐标");
                        return;
                    }
                    if (tbxLine_X1.Enabled)
                    {
                        type = "L1";
                        DMSDK.DM_SetLine(StaticClass.tempConnect, 1, x1, y1, x2, y2, ((x1 + x2) / 2) - 2, ((y1 + y2) / 2) - 4, emiss);
                        sqlCreate.Update_Line(StaticClass.Temper_CameraId, "L1", x1, y1, x2, y2, (x1 + x2) / 2, (y1 + y2) / 2, emiss, StaticClass.DataBaseName);
                    }
                    Cancel_SetLine(type, btnAdd_Line, tbxLine_X1, tbxLine_Y1, tbxLine_X2, tbxLine_Y2, tbxLine_X3, tbxLine_Y3, tbxLine_Emiss, btnClear_Line);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + "设置测温线失败！");
                }
            }
        }
        #endregion

        #region//取消事件
        private void Cancel_SetSpot(string type, Button btnAdd_Spot, TextBox tbxSpot_X, TextBox tbxSpot_Y, TextBox tbxSpot_Emiss, Button btnClear_Spot)//取消设置点
        {
            list_All_Spot = new List<DMSDK.temperSpot>();
            list_All_Spot = sqlCreate.Select_Spot(StaticClass.Temper_CameraId, type,StaticClass.DataBaseName);
            Struct_temperSpot = (DMSDK.temperSpot)list_All_Spot[0];
            tbxSpot_X.Text = Struct_temperSpot.X1.ToString();
            tbxSpot_Y.Text = Struct_temperSpot.Y1.ToString();
            tbxSpot_Emiss.Text = Struct_temperSpot.Emiss.ToString();//文本框数据还原
            foreach (Control control in pnlBtnSpot.Controls)//控件enable为true
            {
                control.Enabled = true;
            }
            IsSet_Spot = false;
            btnAdd_Spot.Text = "编辑";
            btnClear_Spot.Text = "清除";
            tabAreas.Enabled = true;//其余控件为false
            tabLine.Enabled = true;
            tbxSpot_X.Enabled = false;
            tbxSpot_Y.Enabled = false;
            tbxSpot_Emiss.Enabled = false;
            Dispose_Graph();
        }

        private void Cancel_SetArea(string type, Button btnAdd_Area, TextBox tbxArea_X1, TextBox tbxArea_Y1, TextBox tbxArea_X2, TextBox tbxArea_Y2, TextBox tbxArea_Emiss, ComboBox cbxMeasureType, Button btnClear_Area)//取消设置区域
        {
            list_All_Area = new List<DMSDK.temperArea>();
            list_All_Area = sqlCreate.Select_Area(StaticClass.Temper_CameraId, type,StaticClass.DataBaseName);
            Struct_temperArea = (DMSDK.temperArea)list_All_Area[0];
            tbxArea_X1.Text = Struct_temperArea.X1.ToString();
            tbxArea_Y1.Text = Struct_temperArea.Y1.ToString();
            tbxArea_X2.Text = Struct_temperArea.X2.ToString();
            tbxArea_Y2.Text = Struct_temperArea.Y2.ToString();
            tbxArea_Emiss.Text = Struct_temperArea.Emiss.ToString();
            cbxMeasureType.SelectedIndex = Struct_temperArea.MeasureType;
            foreach (Control control in pnlBtnArea.Controls)
            {
                control.Enabled = true;
            }
            IsSet_Area = false;
            btnAdd_Area.Text = "编辑";
            btnClear_Area.Text = "清除";
            tabSpot.Enabled = true;
            tabLine.Enabled = true;
            tbxArea_X1.Enabled = false;
            tbxArea_Y1.Enabled = false;
            tbxArea_X2.Enabled = false;
            tbxArea_Y2.Enabled = false;
            tbxArea_Emiss.Enabled = false;
            cbxMeasureType.Enabled = false;
            Dispose_Graph();
        }

        private void Cancel_SetLine(string type, Button btnAdd_Line, TextBox tbxLine_X1, TextBox tbxLine_Y1, TextBox tbxLine_X2, TextBox tbxLine_Y2, TextBox tbxLine_X3, TextBox tbxLine_Y3, TextBox tbxLine_Emiss, Button btnClear_Line)//取消设置线
        {
            list_All_Line = new List<DMSDK.temperLine>();
            list_All_Line = sqlCreate.Select_Line(StaticClass.Temper_CameraId, type, StaticClass.DataBaseName);
            Struct_temperLine = (DMSDK.temperLine)list_All_Line[0];
            tbxLine_X1.Text = Struct_temperLine.X1.ToString();
            tbxLine_Y1.Text = Struct_temperLine.Y1.ToString();
            tbxLine_X2.Text = Struct_temperLine.X2.ToString();
            tbxLine_Y2.Text = Struct_temperLine.Y2.ToString();
            tbxLine_X3.Text = Struct_temperLine.X3.ToString();
            tbxLine_Y3.Text = Struct_temperLine.Y3.ToString();
            tbxLine_Emiss.Text = Struct_temperLine.Emiss.ToString();
            foreach (Control control in pnlBtnLine.Controls)
            {
                control.Enabled = true;
            }
            IsSet_Line = false;
            btnAdd_Line.Text = "编辑";
            btnClear_Line.Text = "清除";
            tabSpot.Enabled = true;
            tabAreas.Enabled = true;
            tbxLine_X1.Enabled = false;
            tbxLine_Y1.Enabled = false;
            tbxLine_X2.Enabled = false;
            tbxLine_Y2.Enabled = false;
            tbxLine_X3.Enabled = false;
            tbxLine_Y3.Enabled = false;
            tbxLine_Emiss.Enabled = false;
            Dispose_Graph();
        }
        #endregion

        private void btnClear_Line_1_Click(object sender, EventArgs e)
        {
            if (btnClear_Line_1.Text == "清除")
            {
                DMSDK.DM_ClearLine(StaticClass.tempConnect, 1);
                sqlCreate.Delete_Line(StaticClass.Temper_CameraId, "L1",StaticClass.DataBaseName);
                tbxLine_1_X1.Text = "0";
                tbxLine_1_Y1.Text = "0";
                tbxLine_1_X2.Text = "0";
                tbxLine_1_Y2.Text = "0";
                tbxLine_1_X3.Text = "0";
                tbxLine_1_Y3.Text = "0";
            }
            else if (btnClear_Line_1.Text == "取消")
            {
                Cancel_SetLine("L1", btnAdd_Line_1, tbxLine_1_X1, tbxLine_1_Y1, tbxLine_1_X2, tbxLine_1_Y2, tbxLine_1_X3, tbxLine_1_Y3, tbxLine_1_Emiss, btnClear_Line_1);
            }
        }

        private void btnClear_Spot_1_Click(object sender, EventArgs e)
        {
            if (btnClear_Spot_1.Text == "清除")
            {
                DMSDK.DM_ClearSpot(StaticClass.tempConnect, 2);
                sqlCreate.Delete_Spot(StaticClass.Temper_CameraId, "S2", StaticClass.DataBaseName);
                tbxSpot_1_X.Text = "0";
                tbxSpot_1_Y.Text = "0";
            }
            else if (btnClear_Spot_1.Text == "取消")
            {
                Cancel_SetSpot("S2", btnAdd_Spot_1, tbxSpot_1_X, tbxSpot_1_Y, tbxSpot_1_Emiss, btnClear_Spot_1);
            }
        }

        private void btnClear_Spot_2_Click(object sender, EventArgs e)
        {
            if (btnClear_Spot_2.Text == "清除")
            {
                DMSDK.DM_ClearSpot(StaticClass.tempConnect, 3);
                sqlCreate.Delete_Spot(StaticClass.Temper_CameraId, "S3", StaticClass.DataBaseName);
                tbxSpot_2_X.Text = "0";
                tbxSpot_2_Y.Text = "0";
            }
            else if (btnClear_Spot_2.Text == "取消")
            {
                Cancel_SetSpot("S3", btnAdd_Spot_2, tbxSpot_2_X, tbxSpot_2_Y, tbxSpot_2_Emiss, btnClear_Spot_2);
            }
        }

        private void btnClear_Spot_3_Click(object sender, EventArgs e)
        {
            if (btnClear_Spot_3.Text == "清除")
            {
                DMSDK.DM_ClearSpot(StaticClass.tempConnect, 4);
                sqlCreate.Delete_Spot(StaticClass.Temper_CameraId, "S4", StaticClass.DataBaseName);
                tbxSpot_3_X.Text = "0";
                tbxSpot_3_Y.Text = "0";
            }
            else if (btnClear_Spot_3.Text == "取消")
            {
                Cancel_SetSpot("S4", btnAdd_Spot_3, tbxSpot_3_X, tbxSpot_3_Y, tbxSpot_3_Emiss, btnClear_Spot_3);
            }
        }

        private void btnClear_Spot_4_Click(object sender, EventArgs e)
        {
            if (btnClear_Spot_4.Text == "清除")
            {
                DMSDK.DM_ClearSpot(StaticClass.tempConnect, 5);
                sqlCreate.Delete_Spot(StaticClass.Temper_CameraId, "S5", StaticClass.DataBaseName);
                tbxSpot_4_X.Text = "0";
                tbxSpot_4_Y.Text = "0";
            }
            else if (btnClear_Spot_4.Text == "取消")
            {
                Cancel_SetSpot("S5", btnAdd_Spot_4, tbxSpot_4_X, tbxSpot_4_Y, tbxSpot_4_Emiss, btnClear_Spot_4);
            }
        }

        private void btnClear_Area_1_Click(object sender, EventArgs e)
        {
            if (btnClear_Area_1.Text == "清除")
            {
                DMSDK.DM_ClearArea(StaticClass.tempConnect, 6);
                sqlCreate.Delete_Area(StaticClass.Temper_CameraId, "A6", StaticClass.DataBaseName);
                tbxArea_1_X1.Text = "0";
                tbxArea_1_X2.Text = "0";
                tbxArea_1_Y1.Text = "0";
                tbxArea_1_Y2.Text = "0";
            }
            else if (btnClear_Area_1.Text == "取消")
            {
                Cancel_SetArea("A6", btnAdd_Area_1, tbxArea_1_X1, tbxArea_1_Y1, tbxArea_1_X2, tbxArea_1_Y2, tbxArea_1_Emiss, cbxMeasureType_1, btnClear_Area_1);
            }
        }

        private void btnClear_Area_2_Click(object sender, EventArgs e)
        {
            if (btnClear_Area_2.Text == "清除")
            {
                DMSDK.DM_ClearArea(StaticClass.tempConnect, 7);
                sqlCreate.Delete_Area(StaticClass.Temper_CameraId, "A7", StaticClass.DataBaseName);
                tbxArea_2_X1.Text = "0";
                tbxArea_2_X2.Text = "0";
                tbxArea_2_Y1.Text = "0";
                tbxArea_2_Y2.Text = "0";
            }
            else if (btnClear_Area_2.Text == "取消")
            {
                Cancel_SetArea("A7", btnAdd_Area_2, tbxArea_2_X1, tbxArea_2_Y1, tbxArea_2_X2, tbxArea_2_Y2, tbxArea_2_Emiss, cbxMeasureType_2, btnClear_Area_2);
            }
        }

        private void btnClear_Area_3_Click(object sender, EventArgs e)
        {
            if (btnClear_Area_3.Text == "清除")
            {
                DMSDK.DM_ClearArea(StaticClass.tempConnect, 8);
                sqlCreate.Delete_Area(StaticClass.Temper_CameraId, "A8", StaticClass.DataBaseName);
                tbxArea_3_X1.Text = "0";
                tbxArea_3_X2.Text = "0";
                tbxArea_3_Y1.Text = "0";
                tbxArea_3_Y2.Text = "0";
            }
            else if (btnClear_Area_3.Text == "取消")
            {
                Cancel_SetArea("A8", btnAdd_Area_3, tbxArea_3_X1, tbxArea_3_Y1, tbxArea_3_X2, tbxArea_3_Y2, tbxArea_3_Emiss, cbxMeasureType_3, btnClear_Area_3);
            }
        }

        private void btnClear_Area_4_Click(object sender, EventArgs e)
        {
            if (btnClear_Area_4.Text == "清除")
            {
                DMSDK.DM_ClearArea(StaticClass.tempConnect, 9);
                sqlCreate.Delete_Area(StaticClass.Temper_CameraId, "A9", StaticClass.DataBaseName);
                tbxArea_4_X1.Text = "0";
                tbxArea_4_X2.Text = "0";
                tbxArea_4_Y1.Text = "0";
                tbxArea_4_Y2.Text = "0";
            }
            else if (btnClear_Area_4.Text == "取消")
            {
                Cancel_SetArea("A9", btnAdd_Area_4, tbxArea_4_X1, tbxArea_4_Y1, tbxArea_4_X2, tbxArea_4_Y2, tbxArea_4_Emiss, cbxMeasureType_4, btnClear_Area_4);
            }
        }

        FrmIsRunning isRunning;
        
        private void btnClaerAll_Click(object sender, EventArgs e)
        {
            Thread thread = new Thread(showDialog);
            thread.Start();
            showIsRunning();
            thread.Abort();
        }

        private void showDialog()
        {
            isRunning = new FrmIsRunning();
            isRunning.ShowDialog();
        }

        private void showIsRunning()
        {
            DMSDK.DM_ClearAllArea(StaticClass.tempConnect);
            DMSDK.DM_ClearAllLine(StaticClass.tempConnect);
            DMSDK.DM_ClearAllSpot(StaticClass.tempConnect);
            sqlCreate.Delete_Line(StaticClass.Temper_CameraId, "L1", StaticClass.DataBaseName);
            sqlCreate.Delete_Spot(StaticClass.Temper_CameraId, "S2", StaticClass.DataBaseName);
            sqlCreate.Delete_Spot(StaticClass.Temper_CameraId, "S3", StaticClass.DataBaseName);
            sqlCreate.Delete_Spot(StaticClass.Temper_CameraId, "S4", StaticClass.DataBaseName);
            sqlCreate.Delete_Spot(StaticClass.Temper_CameraId, "S5", StaticClass.DataBaseName);
            sqlCreate.Delete_Area(StaticClass.Temper_CameraId, "A6", StaticClass.DataBaseName);
            sqlCreate.Delete_Area(StaticClass.Temper_CameraId, "A7", StaticClass.DataBaseName);
            sqlCreate.Delete_Area(StaticClass.Temper_CameraId, "A8", StaticClass.DataBaseName);
            sqlCreate.Delete_Area(StaticClass.Temper_CameraId, "A9", StaticClass.DataBaseName);
            Thread.Sleep(10000);
            Get_Area_Param();
        }

        private void btnDefaultArea_Click(object sender, EventArgs e)
        {
            btnClearAll.PerformClick();
            DMSDK.DM_SetArea(StaticClass.tempConnect, 6, 6, 2, 312, 236, 90, 0);
            sqlCreate.Update_Area(StaticClass.Temper_CameraId, "A6", 6, 2, 312, 236, 90, 0, StaticClass.DataBaseName);
            Get_Area_Param();
        }

        private void FrmMeasureTemperConfig_FormClosing(object sender, FormClosingEventArgs e)
        {
            
        }
       
    }

}
