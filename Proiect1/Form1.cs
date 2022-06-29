using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Proiect1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
            this.Icon = new Icon("download.ico");

        }
        private bool checkURL(string s)
        {
            if (!File.Exists("MyDatabase.sqlite"))
            {
                SQLiteConnection.CreateFile("MyDatabase.sqlite");
            }
            var _conn = new SQLiteConnection("Data Source = MyDatabase.sqlite; Version = 3; ");
            _conn.Open();
            string stmt = "CREATE TABLE IF NOT EXISTS Keywords(id INTEGER PRIMARY KEY AUTOINCREMENT,kw TEXT)";
            SQLiteCommand cmd = new SQLiteCommand(stmt, _conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to create table! ERROR:" +
                ex.ToString());
                return false;
            }
            SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT kw FROM Keywords", _conn);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            s = s.ToLower();
            var stringArr = dt.Rows.Cast<DataRow>()
                                .Select(row => row[0].ToString())
                                .ToArray();
            var kws = (from key in stringArr
                       where s.Any(val => s.Contains(key))
                       select key);
            foreach(string st in kws)
            {
                return true;
            }
            return false;
        }

        private void homeButton_Click(object sender, EventArgs e)
        {
            webBrowser1.Navigate("www.google.ro");
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoBack();
        }

        private void forwardButton_Click(object sender, EventArgs e)
        {
            webBrowser1.GoForward();
        }

        private void goButton_Click(object sender, EventArgs e)
        {
            if (!checkURL(searchBox.Text))
            {
                webBrowser1.Navigate(searchBox.Text);
            }
            else
                MessageBox.Show("Keyword blocat!");
        }

        private void searchBox_Click(object sender, EventArgs e)
        {
        }
        private void searchBox_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !checkURL(searchBox.Text))
            {
                webBrowser1.Navigate(searchBox.Text);
            }
            else if(e.KeyCode == Keys.Enter && checkURL(searchBox.Text))
                MessageBox.Show("Keyword blocat!");
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        private void webBrowser1_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            searchBox.Text = "" + webBrowser1.Url;
        }

    }
}
