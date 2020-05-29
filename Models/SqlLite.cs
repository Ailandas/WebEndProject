using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace WebEndProject.Models
{
    public static class SqlLite
    {
        public static void InsertToDatabase(string Category, string Word)
        {
            try
            {

                string Path = HttpContext.Current.Server.MapPath("~");
                Path = Path + "\\Database.db";
                //string DataSource = @"Data Source=" + Path + "\\Database\\Database.db;";
                using (SqliteConnection m_dbConnection = new SqliteConnection(@"Data Source=Database.db;"))
                {
                    m_dbConnection.Open();
                    using (SqliteCommand cmdNew = new SqliteCommand($"INSERT INTO Categories (ID, Category, Word) VALUES (@ID , @Category, @Word)", m_dbConnection))
                    {
                        cmdNew.Parameters.AddWithValue("@ID", 0);
                        cmdNew.Parameters.AddWithValue("@Category", Category);
                        cmdNew.Parameters.AddWithValue("@Word", Word);
                        cmdNew.ExecuteNonQuery();
                    }
                    m_dbConnection.Close();
                }
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc.Message);
            }
        }
        public static string GetCurrentDirectory()
        {
            var enviroment = System.Environment.CurrentDirectory;
            string projectDirectory = Directory.GetParent(enviroment).Parent.FullName;

            return projectDirectory;
        }
    }
}