using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace REST.Models
{
    public class DBConnection
    {
        private MySql.Data.MySqlClient.MySqlConnection connection;
        private string userId = "root";
        private string password = "1234567890";
        private string dbName = "elchinogranjero";
        public DBConnection()
        {
            string credentials = "server=127.0.0.1;uid="+userId+";pwd="+password+";database="+dbName;
            try
            {
                this.connection = new MySql.Data.MySqlClient.MySqlConnection();
                connection.ConnectionString = credentials;
                connection.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                Debug.WriteLine(ex);
            }
        }
        public string saveAffiliationForm(AffilliationForm form)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM afiliaciones WHERE nombreNegocio='" + form.businessName + "'";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if(sqlReader.Read())
            {
                return "Ya existe una solicitud de afiliacion para el nombre de negocio indicado!";
            }

            sqlReader.Close();
            sqlString = "SELECT * FROM productores WHERE nombreNegocio='" + form.businessName + "'";
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return "El nombre del negocio que desea registrar ya existe!";
            }

            sqlReader.Close();
            sqlString = "SELECT * FROM productores WHERE Cedula=" + form.cedula;
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return "Ya existe un negocio asociado a este numero de cedula!";
            }

            sqlReader.Close();
            sqlString = "SELECT * FROM afiliaciones WHERE Cedula=" + form.cedula;
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return "Ya existe una solocitud de afiliacion asociada a este numero de cedula!";
            }

            sqlReader.Close();
            sqlString = "INSERT INTO afiliaciones (Cedula,Nombre,Apellidos,nombreNegocio,Provincia,Canton,Distrito,Direccion,Telefono,Fecha_Nacimiento,Num_Sinpe,Comentario,Estado,Password) VALUES ("+form.cedula.ToString()+",'"+form.name+"','"+form.lastName+"','"+form.businessName+"','"+form.province+"','"+form.canton+"','"+form.district+"','"+form.address+"',"+form.phoneN.ToString()+",'"+form.birthDate.ToString("yyyy-MM-dd HH:mm:ss")+"',"+form.sinpeN.ToString()+",'"+form.comment+"','"+form.status+"','"+form.password+"')";
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            cmd.ExecuteNonQuery();
            return "OK";
        }

        public string updateAffiliationForm(int id,string statusComment)
        {
            String[] elements = statusComment.Split(':');
            string status = elements[0];
            string comment = elements[1];


            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            String sqlString = "SELECT * FROM afiliaciones WHERE cedula=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                if (status.Equals("READED"))
                {
                    sqlReader.Close();
                    sqlString = "DELETE FROM afiliaciones WHERE cedula=" + id.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    cmd.ExecuteNonQuery();
                }
                else if(status.Equals("ACCEPTED"))
                {
                    sqlReader.Close();
                    sqlString = "SELECT * FROM afiliaciones WHERE cedula=" + id.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    sqlReader = cmd.ExecuteReader();
                    sqlReader.Read();
                    AffilliationForm form = new AffilliationForm(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetString(12), sqlReader.GetString(3), sqlReader.GetString(4), sqlReader.GetString(5), sqlReader.GetString(6), sqlReader.GetInt32(7), sqlReader.GetDateTime(8), sqlReader.GetInt32(9), sqlReader.GetString(10), sqlReader.GetString(11),sqlReader.GetString(13));

                    sqlReader.Close();
                    sqlString = "DELETE FROM afiliaciones WHERE cedula=" + id.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    cmd.ExecuteNonQuery();

                    sqlString = "INSERT INTO productores (Cedula,Nombre,Apellidos,nombreNegocio,Provincia,Canton,Distrito,Direccion,Telefono,Fecha_Nacimiento,Num_Sinpe,Lugares_Entrega,Calificacion,Password) VALUES (" + form.cedula.ToString() + ",'" + form.name + "','" + form.lastName + "','" + form.businessName + "','" + form.province + "','" + form.canton + "','" + form.district + "','" + form.address + "'," + form.phoneN.ToString() + ",'" + form.birthDate.ToString("yyyy-MM-dd HH:mm:ss") + "'," + form.sinpeN.ToString() + ",'',5,'"+form.password+"')";
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    cmd.ExecuteNonQuery();

                    string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                    sqlString = "INSERT INTO tokens (Productor,Token) VALUES (" + form.cedula.ToString()+",'"+token+"')";
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    cmd.ExecuteNonQuery();

                }
                else
                {
                    sqlReader.Close();
                    sqlString = "UPDATE afiliaciones SET Comentario='" + comment + "',Estado='"+status+"' WHERE cedula=" + id.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    cmd.ExecuteNonQuery();
                }
                return "200";

            }
            return "404";
        }
        public string getToken(string credentials)
        {
            String[] elements = credentials.Split(':');
            string id = elements[0];
            string password = elements[1];

            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            String sqlString = "SELECT * FROM productores WHERE cedula=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                if (sqlReader.GetString(13) == password)
                {
                    string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                    sqlReader.Close();
                    sqlString = "UPDATE tokens SET Token='" +token+ "' WHERE Productor="+id.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    cmd.ExecuteNonQuery();

                    return "200:" + token;
                }
                return "409:";
            }
            return "409:";
        }
        public bool logOut(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            String sqlString = "SELECT * FROM productores WHERE cedula=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                sqlReader.Close();
                sqlString = "UPDATE tokens SET Token='" + token + "' WHERE Productor=" + id.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            return false;
        }
    }
}