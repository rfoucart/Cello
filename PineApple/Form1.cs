using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace PineApple
{
    public partial class Form1 : Form
    {
        const int numberOfDays = 500;
        private Mission mission;
        private List<Button> listButtonPanel;
        public Form1()
        {
            InitializeComponent();
            //mission = new Mission("wasabi",numberOfDays);
            
            globalPanel.Controls.Remove(globalPanel.GetControlFromPosition(0, 0));

           

            // Création du paneau de boutons
            for(int i = 1 ; i <= 5 ; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Button cmd = new Button();
                    cmd.Text = string.Format("{0}", (i-1)*10+j);
                    cmd.Image = new Bitmap(Image.FromFile("mol.png"), new Size(20, 20)); ;
                    cmd.Bounds = button3.Bounds;
                    cmd.ImageAlign = ContentAlignment.MiddleRight;
                    cmd.TextAlign = ContentAlignment.MiddleLeft;
                    cmd.FlatStyle = FlatStyle.Flat;
                    cmd.BackColor = Color.LightBlue;
                    cmd.Margin = new Padding(0, 0, 0, 0);
                    globalPanel.Controls.Add(cmd, i-1, j-1);
                }
            }
            //mission.newAstronaute("jean-pierre");
            //mission.newAstronaute("jean-guy");
            //mission.newAstronaute("pierre-jean");

            //mission.newLocation("petaouchnok",150,200);
            //this.WriteMissionXML();

            this.ReadMissionXML();

            tbl();
        }
        private void tbl()
        {
            
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnCount = 154; // <<<-------
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.FixedSize;//.AddColumns;
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                ColumnStyle cs = new ColumnStyle(SizeType.Percent, 100f / (float)(tableLayoutPanel1.ColumnCount));
                tableLayoutPanel1.ColumnStyles.Add(cs);
            }
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute,31));
            //tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Absolute, 30));
            //tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 30));
            for( int x=0; x<25; x++)
                    {
                        Label Text2 = new Label();
                        
                        Text2.Text = x+"H";
                        Text2.BackColor = Color.LightCyan;
                        Text2.Margin = new Padding(0, 0, 0, 0);
                        Text2.Height = 30;
                        Text2.AutoSize = false;
                        tableLayoutPanel1.Controls.Add(Text2, x*6, 0);
                        tableLayoutPanel1.SetColumnSpan(Text2, 6);
                        
                        
                        /*Button cmd = new Button();
                        cmd.Text = string.Format("{0}H", x);
                        cmd.Margin = new Padding(0, 0, 0, 0);//Finally, add the control to the correct location in the table
                        panel1.Controls.Add(cmd, x, 0);
                        panel1.SetColumnSpan(cmd, 6);*/
                        
                    }
            tableLayoutPanel1.ResumeLayout();
        }
        private void WriteMissionXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("Missions");
            xmlDoc.AppendChild(rootNode);

            mission.WriteXML(xmlDoc, rootNode);

            xmlDoc.Save("./mission.xml");
        }
        private void ReadMissionXML()
        {

            XmlDocument doc = new XmlDocument();
            doc.Load("mission.xml");
            var name = doc.SelectSingleNode("/Missions/Mission/Name");
            var astronautes = doc.SelectNodes("/Missions/Mission/Astronautes/Astronaute");
            var refastro = doc.SelectSingleNode("/Missions/Mission/refNumberAstronaute");
            var locations = doc.SelectNodes("/Missions/Mission/Locations/Location");
            var reflocation = doc.SelectSingleNode("/Missions/Mission/refNumberLoc");

            //Create the mission
            mission = new Mission(name.InnerText, numberOfDays);

            //Add the astronautes
            foreach(XmlNode astronaute in astronautes)
            {
                mission.newAstronaute(astronaute["Name"].InnerText, int.Parse(astronaute["Number"].InnerText));
            }

            //Add the ref number to the class
            Astronaute.setRefNumber(int.Parse(refastro.InnerText));

            foreach (XmlNode location in locations)
            {
                mission.newLocation(location["Name"].InnerText, int.Parse(location["POSX"].InnerText), int.Parse(location["POSY"].InnerText), int.Parse(location["Number"].InnerText));
            }

            //Add the ref number to the class
            PineApple.Location.setRefNumber(int.Parse(reflocation.InnerText));
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void menuToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void flowLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
