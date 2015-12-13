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
        private int daySheet;
        public Form1()
        {
            InitializeComponent();
            listButtonPanel = new List<Button>(0);
            //mission = new Mission("wasabi",numberOfDays);


            this.ReadMissionXML();
            mission.ReadActivityXML();

            globalPanel.Controls.Remove(globalPanel.GetControlFromPosition(0, 0));
            daySheet=4;
            globalPanelInit();
            panelActu(4);
            
            //mission.newAstronaute("jean-pierre");
            //mission.newAstronaute("jean-guy");
            //mission.newAstronaute("pierre-jean");

            //mission.newLocation("petaouchnok",150,200);
            //mission.newLocation("Base", 700, 1000);
            //this.WriteMissionXML();
            
           
            
            //mission.defaultDay(1);
            //mission.WriteActivityXML();
            showDay(1);
            searchInit();
            updateDateNow();

            dayHeaderInit();
        }
        //Initialisation/remplissage du groupbox de recherche
        private void searchInit()
        {
            List<Type> listeGenericType = mission.getActivityTypes();
            
            int i = 0;
            Dictionary<string, string> GT = new Dictionary<string, string>();
            Dictionary<string, string> T = new Dictionary<string, string>();
            foreach (Type t in listeGenericType)
            {   
                GT.Add(i.ToString(), t.getGenericType());
                i++;
            }
            searchGTypeCombo.DataSource = new BindingSource(GT, null);
            searchGTypeCombo.DisplayMember = "Value";
            searchGTypeCombo.ValueMember = "Key";
            //string value = ((KeyValuePair<string, string>)searchGTypeCombo.SelectedItem).Value;
         
        }
        private void updateDateNow()
        {
            DateTime now = DateTime.Now;
            label29.Text = now.ToString();
            label30.Text = mission.earthToMarsDate(now).getDay().ToString();
        }
        private void globalPanelInit()
        {
            // Création du paneau de boutons
            for (int i = 1; i <= 5; i++)
            {
                for (int j = 1; j <= 10; j++)
                {
                    Button cmd = new Button();
                    cmd.Text = string.Format("{0}", (i - 1) * 10 + j);
                    cmd.Image = new Bitmap(Image.FromFile("mol.png"), new Size(20, 20)); ;
                    cmd.Bounds = button3.Bounds;
                    cmd.ImageAlign = ContentAlignment.MiddleRight;
                    cmd.TextAlign = ContentAlignment.MiddleLeft;
                    cmd.FlatStyle = FlatStyle.Flat;
                    cmd.BackColor = Color.LightBlue;
                    cmd.Margin = new Padding(0, 0, 0, 0);
                    globalPanel.Controls.Add(cmd, i - 1, j - 1);
                    listButtonPanel.Add(cmd);
                }
            }
        }
        //Affichage de la Bande des heures dans la partie jour
        private void dayHeaderInit()
        {
            
            tableLayoutPanel1.SuspendLayout();
            tableLayoutPanel1.Controls.Clear();
            tableLayoutPanel1.ColumnCount = 148; // <<<-------
            tableLayoutPanel1.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel1.RowCount = 3;
            tableLayoutPanel1.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;//TableLayoutPanelGrowStyle.FixedSize;//.AddColumns;
            tableLayoutPanel1.ColumnStyles.Clear();
            tableLayoutPanel1.RowStyles.Clear();
            for (int i = 0; i < tableLayoutPanel1.ColumnCount; i++)
            {
                ColumnStyle cs = new ColumnStyle(SizeType.Absolute, 3.5f);
                tableLayoutPanel1.ColumnStyles.Add(cs);
            }
            tableLayoutPanel1.RowStyles.Clear();
            tableLayoutPanel1.RowStyles.Add(new RowStyle(SizeType.Absolute,31));
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
                       
            }
            tableLayoutPanel1.ResumeLayout();
        }
        //renomme les boutons (de 50 en 50 ) 
        //probléme de vitesse pour l'instant
        public void panelActu(int i)
        {
            if(i>=1 && i<=10)
            {
               for(int j=0 ; j<50 ; j++)
               {
                   listButtonPanel[j].Text=string.Format("{0}",(i-1)*50+(j+1));
                   listButtonPanel[j].Click+= new EventHandler(button3_Click);
                   if ((i - 1) * 50 + (j + 1) == mission.getCurrentDay().getDay())
                   {
                       listButtonPanel[j].BackColor=Color.LightGreen;
                   }
                   else if ((i - 1) * 50 + (j + 1) > mission.getCurrentDay().getDay())
                   {
                       listButtonPanel[j].BackColor=Color.LightBlue;
                   }
                   else if ((i-1)*50+(j+1) < mission.getCurrentDay().getDay())
                   {
                       listButtonPanel[j].BackColor = Color.LightGray;
                   }
               }
            }
        }
        //Actualise la partie jour en fonction du jour selectionné sur la partie calendrier. 
        public void showDay(int day)
        {
            tableLayoutPanel2.SuspendLayout();
            tableLayoutPanel2.Controls.Clear();
            tableLayoutPanel2.ColumnCount = 156; // <<<-------
            tableLayoutPanel2.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            tableLayoutPanel2.RowCount = 0;
            tableLayoutPanel2.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;//.AddColumns;
            tableLayoutPanel2.ColumnStyles.Clear();
            tableLayoutPanel2.RowStyles.Clear();
            tableLayoutPanel2.AutoScrollMargin = new Size(0,0);
            for (int i = 0; i < tableLayoutPanel2.ColumnCount; i++)
            {
                ColumnStyle cs = new ColumnStyle(SizeType.Percent, 100f / (float)(tableLayoutPanel2.ColumnCount));
                tableLayoutPanel2.ColumnStyles.Add(cs);
            }
            List<Activity> ListOfActivities = mission.selectActivitiesByDay(day);
            tableLayoutPanel2.RowCount = mission.getAstronautes().Count;
            for (int i = 0; i < tableLayoutPanel2.RowCount;i++ )
            {
                tableLayoutPanel2.RowStyles.Add(new RowStyle(SizeType.Percent, 100 / tableLayoutPanel2.RowCount));
            }

            if (ListOfActivities.Count() != 0)
            {
                foreach (Activity a in ListOfActivities)
                {
                    List<Astronaute> astronautes = mission.getAstronautes();
                    for (int j = 0; j < mission.getAstronautes().Count; j++)
                    {
                        foreach (int astro in a.getAstronautes())
                        {
                            if (astro == mission.getAstronautes()[j].getNumber())
                            {
                                Button cmd2 = new Button();
                                cmd2.Margin = new Padding(0, 0, 0, 0);//Finally, add the control to the correct location in the table
                                cmd2.Click += new System.EventHandler(cmd2_Click);
                                cmd2.Tag = a;
                                cmd2.MaximumSize = new Size(500, 200);
                                cmd2.Dock = DockStyle.Fill;
                                tableLayoutPanel2.Controls.Add(cmd2, hoursToColumn(a.getStartDate().getHours(), a.getStartDate().getMinutes()), j);
                                int length = lengthToColumn(a.getStartDate(), a.getEndDate());
                                tableLayoutPanel2.SetColumnSpan(cmd2, length);
                            }
                        }
                    }
                }
            }
            tableLayoutPanel2.ResumeLayout();
        }
        //Fait correspondre temps et colonnes sur le tableau d'affichage des jours
        private int hoursToColumn(int hours, int minutes)
        {
            return 6 * hours + minutes / 10;
        }
        private int lengthToColumn(MDate d, MDate f)
        {
            double D = d.getHours()*60 + d.getMinutes();
            double F = f.getHours()*60 + f.getMinutes();
            double DF = F - D;
            int df=(int)(DF/10);
            return  df ;
        }
        //Ecrit la mission en xml
        private void WriteMissionXML()
        {
            XmlDocument xmlDoc = new XmlDocument();
            XmlNode rootNode = xmlDoc.CreateElement("Missions");
            xmlDoc.AppendChild(rootNode);

            mission.WriteXML(xmlDoc, rootNode);

            xmlDoc.Save("./mission.xml");
        }

        //Charge le fichier mission.xml
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
            foreach (Astronaute astro in mission.getAstronautes())
            {
                checkedListBox1.Items.Add(astro.getName());
            }
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
        //Previous/Next
        private void button1_Click(object sender, EventArgs e)
        {
            Button a = sender as Button;
            if(a.Text=="Previous")
            {
                if(daySheet>1)
                {   
                    daySheet--;
                    panelActu(daySheet);
                }
            }
            else if(a.Text=="Next")
            {
                if (daySheet < 10)
                {
                    daySheet++;
                    panelActu(daySheet);
                }
            }
        }

        //Affichage du jour aprés click sur le calendrier
        private void button3_Click(object sender, EventArgs e)
        {
            Button A = sender as Button;
            showDay(int.Parse(A.Text));
        }

        private void PictNanediVallis_MouseClick(object sender, MouseEventArgs e)
        {
            double xRatio = (double)PictNanediVallis.Width / PictNanediVallis.Image.Width;
            double yRatio = (double)PictNanediVallis.Height / PictNanediVallis.Image.Height;
            Point basePixel = new Point(e.X, e.Y);

            // Application des taux d'étirement/compression du ratio
            basePixel.X = (int)(basePixel.X / xRatio);
            basePixel.Y = (int)(basePixel.Y / yRatio);
            /* Le clic sur l'image redimensionnée renvoie désormais les coordonnées (arrondies)
             * de l'image en taille réelle. Les axes partent du coin haut gauche du picture box :
             * Pour les abcisses : gauche -> droite
             * Pour les ordonnées : haut -> bas
             */

            /* L'origine du repère est fixé au pixel (700,1000).
             * 1 pixel correspond à 5 mètres.
             * => Il faut décaler l'origine, inverser le sens de l'axe des ordonnées et mettre à l'échelle
             */
            int metersX = (basePixel.X - 700) * 5;
            int metersY = (basePixel.Y - 1000) * (-5);
            // Insertion dans les TextBoxes correspondantes
            textBox1.Text = metersX.ToString();
            textBox2.Text = metersY.ToString();
        }

        //Remplissage des champs lors du clic sur une activité
        private void cmd2_Click(object sender, EventArgs e)
        {
            //On réactive le bouton "new activity" s'il a été bloqué, et on bloque Save & Reset
            NewActivityButton.Enabled = true;
            SaveActivityButton.Enabled = false;
            ResetActivityButton.Enabled = false;

            // Récupération de l'activité liée au bouton
            Activity a = (Activity)(sender as Button).Tag;
            //Appel de la fonction de remplissage
            fillActivityPanel(a);
        }
        //rempli les champs juste grace au numero de l'activité  tout les cas 
        private void fillActivityPanel(Activity a)
        {
            //Remplissage des champs de l'activité
            comboBoxStartHour.SelectedIndex = a.getStartDate().getHours();
            comboBoxStartMinutes.SelectedIndex = a.getStartDate().getMinutes() / 10;
            comboBoxEndHour.SelectedIndex = a.getEndDate().getHours();
            comboBoxEndMinutes.SelectedIndex = a.getEndDate().getMinutes() / 10;
            richTextBox1.Text = a.getDescription();
            labelGenericType.Text = a.getGenericType().ToString();
            comboBoxType.SelectedItem = a.getType().ToString();
            for (int i = 1; i < checkedListBox1.Items.Count; i++)//On déselectionne tous les astronautes
                checkedListBox1.SetItemChecked(i, false);
            foreach (int numAstro in a.getAstronautes()) //Pour resélectionner les bons
                checkedListBox1.SetItemChecked(numAstro, true);
        }
        
        private void NewActivityButton_Click(object sender, EventArgs e)
        {
            groupBox1.Text = "New Activity"; // Changement du nom pour montrer qu'on crée une activité
            NewActivityButton.Enabled = false;
            comboBoxStartHour.Text = "";
            comboBoxStartMinutes.Text = "";
            comboBoxEndHour.Text = "";
            comboBoxEndMinutes.Text = "";
            richTextBox1.Text = "";
            labelGenericType.Text = "";
            comboBoxType.Text = "";
            for (int i = 1; i < checkedListBox1.Items.Count; i++)//On déselectionne tous les astronautes
                checkedListBox1.SetItemChecked(i, false);
            numericUpDown1.ResetText();
        }

        private void SaveActivityButton_Click(object sender, EventArgs e)
        {
            groupBox1.Name = "Activity";


            //mission.newActivity(richTextBox1.Text, )

            //Réinitialisation des boutons après sauvegarde
            NewActivityButton.Enabled = true;
            SaveActivityButton.Enabled = false;
            ResetActivityButton.Enabled = false;
        }
        private void ResetActivityButton_Click(object sender, EventArgs e)
        {
            groupBox1.Name = "Activity";

            //Réinitialisation des boutons après reset
            NewActivityButton.Enabled = true;
            SaveActivityButton.Enabled = false;
            ResetActivityButton.Enabled = false;
        }

        //Fonction recherche aprés click sur le bouton search du panel search.
        private void search(object sender, EventArgs e)
        {
            searchPanel.SuspendLayout();
            List<Mission.searchResult> results = new List<Mission.searchResult>(0);
            if(searchGTypeCombo.Enabled==true)
            {
                results = mission.searchByType(int.Parse(((KeyValuePair<string, string>)searchGTypeCombo.SelectedItem).Key), int.Parse(((KeyValuePair<string, string>)searchTypeCombo.SelectedItem).Key), Convert.ToInt32(numericUpDown2.Value),  Convert.ToInt32(numericUpDown3.Value));
            }
            else
            {
                results = mission.searchByKeyword(textBox4.Text, Convert.ToInt32(numericUpDown2.Value),  Convert.ToInt32(numericUpDown3.Value));
            }
            searchPanel.Controls.Clear();
            searchPanel.RowCount = results.Count; // <<<-------
            searchPanel.CellBorderStyle = TableLayoutPanelCellBorderStyle.Single;
            searchPanel.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;//.AddColumns;
            searchPanel.RowStyles.Clear();
            for (int i = 0; i < searchPanel.RowCount; i++)
            {
                RowStyle cs = new RowStyle(SizeType.Percent, 100f / (float)(searchPanel.RowCount));
                searchPanel.RowStyles.Add(cs);
            }
            if (results.Count() != 0)
            {
                int j = 0;
                foreach (Mission.searchResult a in results)
                {
                    Label lb = new Label();
                    lb.Text = a.types;
                    lb.Margin = new Padding(0, 0, 0, 0);//Finally, add the control to the correct location in the table
                    lb.Click += new System.EventHandler(clicOnSearchResult);
                    lb.Tag = a.a;
                    lb.Dock = DockStyle.Fill;
                    searchPanel.Controls.Add(lb, 0, j);
                    Label lb1 = new Label();
                    lb1.Text = a.startDate;
                    lb1.Margin = new Padding(0, 0, 0, 0);//Finally, add the control to the correct location in the table
                    lb1.Click += new System.EventHandler(clicOnSearchResult);
                    lb1.Tag = a.a;
                    lb1.Dock = DockStyle.Fill;
                    searchPanel.Controls.Add(lb1, 1, j);
                    Label lb2 = new Label();
                    lb2.Text = a.endDate;
                    lb2.Margin = new Padding(0, 0, 0, 0);//Finally, add the control to the correct location in the table
                    lb2.Click += new System.EventHandler(clicOnSearchResult);
                    lb2.Tag = a.a;
                    lb2.Dock = DockStyle.Fill;
                    searchPanel.Controls.Add(lb2, 2, j);
                    j++;
                }
            }
            searchPanel.ResumeLayout();

        }
        private void clicOnSearchResult(object sender, EventArgs e)
        {
            fillActivityPanel((sender as Label).Tag as Activity);
        }
        //Lorsque un type generique est selectionné on bloque le champ keyword
        // et on affiche la liste cohérente dans type
        private void searchGTypeCombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox a = sender as ComboBox;

            int key = int.Parse(((KeyValuePair<string, string>)a.SelectedItem).Key);

            List<Type> listeGenericType = mission.getActivityTypes();

            int i = 0;
            Dictionary<string, string> T = new Dictionary<string, string>();
            foreach (string t in listeGenericType[key].getTypes())
            {
                T.Add(i.ToString(), t);
                i++;
            }
            searchTypeCombo.DataSource = new BindingSource(T, null);
            searchTypeCombo.DisplayMember = "Value";
            searchTypeCombo.ValueMember = "Key";
            //string value = ((KeyValuePair<string, string>)searchGTypeCombo.SelectedItem).Value;

        }
        //Lorsque un keyword est tapé, on bloque les champs type et generic type.
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            TextBox t =sender as TextBox;

            if (t.Text.Length != 0)
            {
                searchGTypeCombo.Enabled = false;
                searchTypeCombo.Enabled = false;
            }
            else
            {
                searchGTypeCombo.Enabled = true;
                searchTypeCombo.Enabled = true;
            }
        }
        //Lorsque un premier sol de la période est selectionné, on débloque la selection du 2eme jour de la période.
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            numericUpDown3.Minimum = (sender as NumericUpDown).Value;
        }
    }
}
