using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.IO;
using System.Linq;
using System.Web;

namespace WebEndProject.Models
{
    public static class SqlLite
    {
        public static string kelias = System.Web.HttpContext.Current.Request.PhysicalApplicationPath + "\\Database.db;";
        public static void InsertToDatabase(string Category, string Word)
        {
             try
             {


            

            using (SQLiteConnection m_dbConnection = new SQLiteConnection("Data Source=" + kelias))
                {
                   

                    int ID=GetLatestID(); //Paima naujausia ID ir padidina
                    ID++;

                        string sql1 = $"insert into Categories (ID,Category,Word) VALUES (@ID, @Category, @Word)";
                        SQLiteCommand command1 = new SQLiteCommand(sql1, m_dbConnection);
                        command1.Parameters.AddWithValue("@ID", ID);
                        command1.Parameters.AddWithValue("@Category", Category);
                        command1.Parameters.AddWithValue("@Word", Word);
                        m_dbConnection.Open();
                        command1.ExecuteNonQuery();
                        m_dbConnection.Close();
                   
                   
                }

            }
            catch (Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        public static bool CheckIfCategoryEgzists(string Category)
        {
            string data = "";
            bool Egzists = false;
            using (SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=" + kelias))
            {

                m_dbConnection.Open();
                using (SQLiteCommand command3 = new SQLiteCommand($"SELECT Category FROM Categories", m_dbConnection))
                {

                    SQLiteDataReader reader3 = command3.ExecuteReader();
                    while (reader3.Read())
                    {
                        
                        data = Convert.ToString(reader3[0]);
                        if (data == Category)
                        {
                            Egzists = true;
                            break;
                            
                        }


                    }
                }
                m_dbConnection.Close();
                
            }
            return Egzists;
        }
        public static int GetLatestID() //Grazina paskutini ID is duomenu bazes
        {
            string data = "";
            using (SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=" + kelias))
            {

                m_dbConnection.Open();
                using (SQLiteCommand command3 = new SQLiteCommand($"SELECT ID FROM Categories", m_dbConnection))
                {

                    SQLiteDataReader reader3 = command3.ExecuteReader();
                    while (reader3.Read())
                    {
                        data = Convert.ToString(reader3[0]);


                    }
                }
                m_dbConnection.Close();
            }
            return Convert.ToInt32(data);
        }
        public static string getCategory()
        {
            
            string data = "";
            using (SQLiteConnection m_dbConnection = new SQLiteConnection(@"Data Source=" + kelias))
            {
                
                m_dbConnection.Open();
                using (SQLiteCommand command3 = new SQLiteCommand($"SELECT * FROM Categories", m_dbConnection))
                {

                    SQLiteDataReader reader3 = command3.ExecuteReader();
                    while (reader3.Read())
                    {
                        data = Convert.ToString(reader3[1]);
                       

                    }
                }
                m_dbConnection.Close();
            }
            return data;
        }
        public static string GetCurrentDirectory()
        {
            var enviroment = System.Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.FullName;

            return projectDirectory;
        }
    }
}