using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DoAn1.DAO
{
    class DataProvider
    {
        string connectionString = DoAn1.Properties.Settings.Default.QuanLyQuanAnConnectionString;

        private static DataProvider instance;

        public static DataProvider Instance
        {
            get { if (instance == null) instance = new DataProvider(); return DataProvider.instance; }
            private set { DataProvider.instance = value; }
        }

        public DataProvider() { }

        public SqlDataReader ExecuteQuery(string query, object[] parameter = null)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);

            if (parameter != null)
            {
                string[] listPara = query.Split(' ');
                int i = 0;
                foreach (string item in listPara)
                {
                    if (item.Contains('@'))
                    {
                        cmd.Parameters.AddWithValue(item, parameter[i]);
                        i++;
                    }
                }
            }

            SqlDataReader reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

            return reader;
        }


        public int ExecuteNoneQuery(string query, object[] paramete = null)
        {
            int data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);

                if (paramete != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            if (i < paramete.Length)
                            {
                                cmd.Parameters.AddWithValue(item, paramete[i]);
                                i++;
                            }
                            else
                            {
                                throw new ArgumentException("Không đủ tham số cho câu truy vấn SQL.");
                            }
                        }
                    }
                }


                data = cmd.ExecuteNonQuery();

                connection.Close();

            }

            return data;
        }

        public object ExecuteScalar(string query, object[] paramete = null)
        {
            object data = 0;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                SqlCommand cmd = new SqlCommand(query, connection);

                if (paramete != null)
                {
                    string[] listPara = query.Split(' ');
                    int i = 0;
                    foreach (string item in listPara)
                    {
                        if (item.Contains('@'))
                        {
                            if (i < paramete.Length)
                            {
                                cmd.Parameters.AddWithValue(item, paramete[i]);
                                i++;
                            }
                            else
                            {
                                throw new ArgumentException("Không đủ tham số cho câu truy vấn SQL.");
                            }
                        }
                    }
                }

                data = cmd.ExecuteScalar();

                connection.Close();

            }

            return data;
        }
    }
}
