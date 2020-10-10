﻿using System;
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
            string credentials = "server=127.0.0.1;uid=" + userId + ";pwd=" + password + ";database=" + dbName;
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


        public AffilliationForm getAffilliationForm(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM afiliaciones WHERE Cedula=" +id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return new AffilliationForm(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetString(12), sqlReader.GetString(3), sqlReader.GetString(4), sqlReader.GetString(5), sqlReader.GetString(6), sqlReader.GetInt32(7), sqlReader.GetDateTime(8), sqlReader.GetInt32(9), sqlReader.GetString(10), sqlReader.GetString(11), sqlReader.GetString(13));;
            }
            return null;
        }
        public ArrayList getAllAffilliationForms()
        {
            ArrayList forms=new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;

            String sqlString = "SELECT * FROM afiliaciones";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);

            sqlReader = cmd.ExecuteReader();
            while (sqlReader.Read())
            {
                AffilliationForm form = new AffilliationForm(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetString(12), sqlReader.GetString(3), sqlReader.GetString(4), sqlReader.GetString(5), sqlReader.GetString(6), sqlReader.GetInt32(7), sqlReader.GetDateTime(8), sqlReader.GetInt32(9), sqlReader.GetString(10), sqlReader.GetString(11), sqlReader.GetString(13));
                forms.Add(form);
            }
            return forms;
        }

        public ArrayList getAllProducers()
        {
            ArrayList producers = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;

            String sqlString = "SELECT * FROM productores";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);

            sqlReader = cmd.ExecuteReader();
            while (sqlReader.Read())
            {
                Producer form = new Producer(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetString(3), sqlReader.GetString(4), sqlReader.GetString(5), sqlReader.GetString(6), sqlReader.GetInt32(7),sqlReader.GetDateTime(8), sqlReader.GetInt32(9),sqlReader.GetInt32(10),sqlReader.GetString(11), sqlReader.GetString(12), sqlReader.GetString(13));
                producers.Add(form);
            }
            return producers;
        }
        public Producer getProducer(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM productores WHERE Cedula=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return new Producer(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetString(3), sqlReader.GetString(4), sqlReader.GetString(5), sqlReader.GetString(6), sqlReader.GetInt32(7), sqlReader.GetDateTime(8), sqlReader.GetInt32(9), sqlReader.GetInt32(10), sqlReader.GetString(11), sqlReader.GetString(12), sqlReader.GetString(13));
            }
            return null;
        }

        public int updateProducer(int id,Producer producer)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM productores WHERE Cedula=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                int  actualCedula = id;
                string actualBusinessName = sqlReader.GetString(12);
                if (producer.cedula != id)
                {
                    sqlReader.Close();
                    sqlString = "SELECT * FROM productores WHERE Cedula=" + producer.cedula.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    sqlReader = cmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        return 409;
                    }
                    
                    if (!actualBusinessName.Equals(producer.businessName))
                    {
                        sqlReader.Close();
                        sqlString = "SELECT * FROM productores WHERE nombreNegocio=" + producer.businessName;
                        cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                        sqlReader = cmd.ExecuteReader();
                        if (sqlReader.Read())
                        {
                            return 409;
                        }

                        sqlReader.Close();
                        sqlString = "UPDATE productores SET Cedula=" + producer.cedula.ToString() + ",Nombre='" + producer.name + "',Apellidos='" + producer.lastName + "',Provincia='" + producer.province + "',Canton='" + producer.canton + "',Distrito='" + producer.district + "',Direccion='" + producer.address + "',Telefono=" + producer.phoneN.ToString() + ",Fecha_Nacimiento='" + producer.birthDate.ToString("yyyy-MM-dd HH:mm:ss") + "',Num_Sinpe=" + producer.sinpeN.ToString() + ",Calificacion=" + producer.calification + ",Lugares_Entrega='" + producer.deliveryPlaces + "',nombreNegocio='" + producer.businessName + "',Password='" + producer.password + "' WHERE Cedula=" + id.ToString();
                        cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                        cmd.ExecuteNonQuery();
                        return 200;
                    }
                    sqlReader.Close();
                    sqlString = "UPDATE productores SET Cedula=" + producer.cedula.ToString() + ",Nombre='" + producer.name + "',Apellidos='" + producer.lastName + "',Provincia='" + producer.province + "',Canton='" + producer.canton + "',Distrito='" + producer.district + "',Direccion='" + producer.address + "',Telefono=" + producer.phoneN.ToString() + ",Fecha_Nacimiento='" + producer.birthDate.ToString("yyyy-MM-dd HH:mm:ss") + "',Num_Sinpe=" + producer.sinpeN.ToString() + ",Calificacion=" + producer.calification + ",Lugares_Entrega='" + producer.deliveryPlaces + "',nombreNegocio='" + producer.businessName + "',Password='" + producer.password + "' WHERE Cedula=" + id.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    cmd.ExecuteNonQuery();
                    return 200;

                }
                if (actualBusinessName!= producer.businessName)
                {
                    sqlReader.Close();
                    sqlString = "SELECT * FROM productores WHERE nombreNegocio=" + producer.businessName;
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    sqlReader = cmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        return 409;
                    }

                    sqlReader.Close();
                    sqlString = "UPDATE productores SET Cedula=" + producer.cedula.ToString() + ",Nombre='" + producer.name + "',Apellidos='" + producer.lastName + "',Provincia='" + producer.province + "',Canton='" + producer.canton + "',Distrito='" + producer.district + "',Direccion='" + producer.address + "',Telefono=" + producer.phoneN.ToString() + ",Fecha_Nacimiento='" + producer.birthDate.ToString("yyyy-MM-dd HH:mm:ss") + "',Num_Sinpe=" + producer.sinpeN.ToString() + ",Calificacion=" + producer.calification + ",Lugares_Entrega='" + producer.deliveryPlaces + "',nombreNegocio='" + producer.businessName + "',Password='" + producer.password + "' WHERE Cedula=" + id.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    cmd.ExecuteNonQuery();
                    return 200;
                }
                return 200;
            }
            return 404;
        }
        public string deleteProducer(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM productores WHERE Cedula=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                sqlReader.Close();
                sqlString = "DELETE FROM productores WHERE cedula=" + id.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                cmd.ExecuteNonQuery();
                return "200";
            }
            return "404";
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

                    sqlString = "INSERT INTO tokens (Usuario,Token,Tipo) VALUES (" + form.cedula.ToString()+",'"+token+"','productores')";
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
        public ArrayList getAllCategories()
        {
            ArrayList categories = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;

            String sqlString = "SELECT * FROM categorias";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);

            sqlReader = cmd.ExecuteReader();
            while (sqlReader.Read())
            {
                Categorie categorie = new Categorie(sqlReader.GetInt32(0),sqlReader.GetString(1));
                categories.Add(categorie);
            }
            return categories;
        }
        public Categorie getCategorie(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM categorias WHERE ID=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return new Categorie(sqlReader.GetInt32(0), sqlReader.GetString(1));
            }
            return null;
        }

        public int createCategorie(Categorie categorie)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM categorias WHERE ID=" + categorie.ID;
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return 409;
            }
            sqlReader.Close();
            sqlString = "SELECT * FROM categorias WHERE Nombre='" + categorie.name+"'";
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return 409;
            }

            sqlReader.Close();
            sqlString = "INSERT INTO categorias (ID,Nombre) VALUES (" + categorie.ID.ToString() + ",'" + categorie.name + "')";
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            cmd.ExecuteNonQuery();
            return 200;
        }

        public int updateCategorie(int id, Categorie categorie)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM categorias WHERE ID=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                int actualID = sqlReader.GetInt32(0);
                string actualName = sqlReader.GetString(1);

                if (categorie.ID != id)
                {
                    sqlReader.Close();
                    sqlString = "SELECT * FROM categorias WHERE ID=" + categorie.ID.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    sqlReader = cmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        return 409;
                    }
                   

                    if (!actualName.Equals(categorie.name))
                    {
                        sqlReader.Close();
                        sqlString = "SELECT * FROM categorias WHERE Nombre=" + categorie.name;
                        cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                        sqlReader = cmd.ExecuteReader();
                        if (sqlReader.Read())
                        {
                            return 409;
                        }

                        sqlReader.Close();
                        sqlString = "UPDATE categorias SET ID=" + categorie.ID.ToString() + ",Nombre='" + categorie.name + "' WHERE ID=" + id.ToString();
                        cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                        cmd.ExecuteNonQuery();
                        return 200;
                    }
                    sqlReader.Close();
                    sqlString = "UPDATE categorias SET ID=" + categorie.ID.ToString() + " WHERE ID=" + id.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    cmd.ExecuteNonQuery();
                    return 200;

                }
                if(!actualName.Equals(categorie.name))
                {
                    sqlReader.Close();
                    sqlString = "SELECT * FROM categorias WHERE Nombre='" + categorie.name+"'";
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    sqlReader = cmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        return 409;
                    }

                    sqlReader.Close();
                    sqlString = "UPDATE categorias SET Nombre='" + categorie.name + "' WHERE ID=" + id.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    cmd.ExecuteNonQuery();
                    return 200;
                }
                return 200;
            }
            return 404;
        }
        public int deleteCategorie(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM categorias WHERE ID=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                sqlReader.Close();
                sqlString = "DELETE FROM categorias WHERE ID=" + id.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                cmd.ExecuteNonQuery();
                return 200;
            }
            return 404;
        }
        public string getToken(SignInRequest credentials)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            String sqlString = "SELECT * FROM "+credentials.type+" WHERE cedula=" + credentials.ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                if (sqlReader.GetString(13)==credentials.password)
                {
                    string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                    sqlReader.Close();
                    sqlString = "UPDATE tokens SET Token='" + token + "' WHERE Usuario=" + credentials.ID.ToString() + " AND Tipo='" + credentials.type + "'";
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    cmd.ExecuteNonQuery();

                    return token;
                }
                return "409";
            }
            return "409";
        }
        public bool logOut(SignOutRequest credentials)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            String sqlString = "SELECT * FROM "+credentials.type+" WHERE cedula=" + credentials.ID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                sqlReader.Close();
                sqlString = "UPDATE tokens SET Token='" + token + "' WHERE Usuario=" + credentials.ID.ToString()+" AND Tipo='"+credentials.type+"'";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            return false;
        }
    }
}