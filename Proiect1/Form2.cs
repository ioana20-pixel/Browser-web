using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;
using System.Data.SqlClient;

namespace Proiect1
{
    public partial class Form2 : Form
    {
        
        public Form2()
        {
            InitializeComponent();
            this.Icon = new Icon("forbidden-icon-10065-Windows.ico");

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.Text = "Ban Keywords";
            if (!File.Exists("MyDatabase.sqlite"))
            {
                SQLiteConnection.CreateFile("MyDatabase.sqlite");
            }
            var _conn = new SQLiteConnection("Data Source = MyDatabase.sqlite; Version = 3; ");
            _conn.Open();
            string stmt = "CREATE TABLE IF NOT EXISTS Keywords(id INTEGER PRIMARY KEY AUTOINCREMENT, kw TEXT)";
            SQLiteCommand cmd = new SQLiteCommand(stmt, _conn);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to create table! ERROR:" +
                ex.ToString());
                return;
            }
            _conn.Close();
        }

        private void kwButton_Click(object sender, EventArgs e)
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
                return;
            }
            string keywords = kwAddText.Text;
            string statement = "INSERT INTO Keywords (kw) VALUES ($keywords)";
            SQLiteCommand cmd2 = new SQLiteCommand(statement, _conn);
            cmd2.Parameters.AddWithValue("$keywords", keywords);
            try
            {
                cmd2.ExecuteNonQuery();
                MessageBox.Show("Keyword " + keywords + " added to DB");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to insert in table! ERROR:" +
                ex.ToString());
                return;
            }
            _conn.Close();
        }

        private void comboBox1_Click(object sender, EventArgs e)
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
                return;
            }
            try
            {
                comboBox1.DisplayMember = "kw";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter("SELECT kw FROM Keywords", _conn);
                DataTable dt = new DataTable();
                adapter.Fill(dt);
                comboBox1.DataSource = dt;
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Unable to execute query! ERROR:" +
                ex.ToString());
                return;
            }
            _conn.Close();
        }
    }
}
