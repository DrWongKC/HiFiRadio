using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HiFiRadio
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<RadioStation> GetRadioStationList()
        {
            var list = new List<RadioStation>();
            
            using (TextFieldParser parser = new TextFieldParser(@"c:\temp\ListOfRadioStations.csv"))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                while (!parser.EndOfData)
                {
                    //Processing row
                    string[] fields = parser.ReadFields();
                    List<String> arrayList = new List<string>();
                    foreach (string field in fields)
                    {
                        arrayList.Add(field);
                    }
                    for (int i=0; i<(arrayList.Count/2); i+=2)
                    {
                        list.Add(new RadioStation() { StationName = arrayList[i], StationUrl = arrayList[i+1] });
                    }
                }
            }
            return list;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var radioStations = GetRadioStationList();

            // Clear the list view
            listView1.Items.Clear();

            foreach (var station in radioStations)
            {
                var row = new string[] { station.StationName, station.StationUrl };
                var lvi = new ListViewItem(row);
                // We add the whole object to the Tag property if we want
                // Later to display details about the selected item
                lvi.Tag = station; // We need to cast to station when we get the value
                // Add the item to the listView1
                listView1.Items.Add(lvi);
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var selectedItem = (RadioStation)listView1.SelectedItems[0].Tag;

            if (selectedItem != null)
            {
                axWindowsMediaPlayer1.URL = selectedItem.StationUrl;
            }
        }
    }
}
