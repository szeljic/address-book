using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Windows.Forms;
using System.Timers;
using System.Diagnostics;
using System.IO;

namespace AddressBook
{
    class DBConnection
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        public DBConnection()
        {
            Initialize();
        }

        private void Initialize()
        {
            server = @"localhost";
            database = @"addressbook";
            uid = @"root";
            password = @"stralezelja";
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        private bool OpenConnection()
        {
            try
            {
                connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        MessageBox.Show("Cannot connect to server. Contact administrator.");
                        break;
                    case 1045:
                        MessageBox.Show("Invalid username/password, please try again.");
                        break;
                }
                return false;
            }

        }

        private bool CloseConnection()
        {
            try
            {
                connection.Clone();
                return true;
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }

        public void Insert(Person person, string table)
        {
            string query = @"INSERT INTO " + table + "(fullName, address, phone, email) VALUES ('" + person.getFullName() + "', '" + person.getAddress() + "', '" + person.getPhone() + "', '" + person.getEmail() + "')";

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void Update(string oldPersonsFullName, Person person, string table)
        {
            string query = @"UPDATE " + table + " SET fullName='" + person.getFullName() + "', " + "address='" + person.getAddress() + "', " + "phone='" + person.getPhone() + "', " + "email='" + person.getEmail() + "'" +  " WHERE fullName='" + oldPersonsFullName + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand();
                cmd.CommandText = query;
                cmd.Connection = connection;
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }

        public void Delete(string fullName, string table)
        {
            string query = @"DELETE FROM " + table + " WHERE fullName='" + fullName + "'";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }

        }

        public List<Person> Select(string columns, string tabel, string where)
        {
            string query = @"SELECT " +  columns + " FROM " + tabel;
            if (where != null)
            {
                query += " WHERE " + where;
            }

            List<Person> list = new List<Person>();

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader data = cmd.ExecuteReader();
                if (data.HasRows)
                {
                    while (data.Read())
                    {
                        list.Add(new Person(data.GetString(1), data.GetString(2), data.GetString(3), data.GetString(4)));
                    }
                }
                data.Close();
                this.CloseConnection();
                return list;
            }
            else
            {
                return list;
            }
            
        }



        public int Count()
        {
            string query = @"SELECT COUNT(*) FROM addressbook";
            int count = -1;

            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                count = int.Parse(cmd.ExecuteScalar() + "");
                this.CloseConnection();
                return count;
            }
            else
            {
                return count;
            }
        }

        public void Backup()
        {
            try
            {
                DateTime dateTime = DateTime.Now;
                int year = dateTime.Year;
                int month = dateTime.Month;
                int day = dateTime.Day;
                int hour = dateTime.Hour;
                int minute = dateTime.Minute;
                int second = dateTime.Second;
                int millisecond = dateTime.Millisecond;

                string path;
                path = "MySqlBackups\\" + year + "-" + month + "-" + day + "-" + hour + "-" + minute + "-" + second + "-" + millisecond + ".sql";
                StreamWriter file = new StreamWriter(path);

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysqldump";
                psi.RedirectStandardInput = false;
                psi.RedirectStandardOutput = true;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);

                string output;
                output = process.StandardOutput.ReadToEnd();
                file.WriteLine(output);
                process.WaitForExit();
                file.Close();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error. Unable to backup. " + ex.Message);
            }
        }

        public void Restore()
        {
            try
            {
                string path;
                path = "MySqlBackup.sql";
                StreamReader file = new StreamReader(path);
                string input = file.ReadToEnd();
                file.Close();

                ProcessStartInfo psi = new ProcessStartInfo();
                psi.FileName = "mysql";
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.Arguments = string.Format(@"-u{0} -p{1} -h{2} {3}", uid, password, server, database);
                psi.UseShellExecute = false;

                Process process = Process.Start(psi);
                process.StandardInput.WriteLine(input);
                process.StandardInput.Close();
                process.WaitForExit();
                process.Close();
            }
            catch (IOException ex)
            {
                MessageBox.Show("Error. Unable to restore. " + ex.Message);
            }

        }
    }
}
