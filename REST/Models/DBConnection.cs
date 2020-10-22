using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MySql.Data;
using System.Collections;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Net.Http.Headers;


namespace REST.Models
{
    /// <summary>
    /// Clase utilizada para conectarse a la base de datos y crear una interfaz para su uso.
    /// </summary>
    public class DBConnection
    {
        private MySql.Data.MySqlClient.MySqlConnection connection;
        private int orderNumber=0;
        private string userId = "root";
        private string password = "1234567890";
        private string dbName = "elchinogranjerotest";

        /// <summary>
        /// Constructor de la clase donde se establecen las credenciales para la conexión para la base de datos.
        /// </summary>
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

        /// <summary>
        /// Método utilizado para obtener de la base de datos una solicitud de afiliación de un productor determinado segun cédula.
        /// </summary>
        /// <param name="id">Identificador utilizado para obtener una solicitud específica.</param>
        /// <returns>Retorna una solicitud de afiliación de un productor o null si no existe una solicitud asociada al identificador.</returns>
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

        /// <summary>
        /// Método utilizado para obtener de la base de datos todas las solicitudes de afiliación, de productores, existentes.
        /// </summary>
        /// <returns>Retorna un ArrayList que contiene todas las solicitudes de afiliación, de productores, existentes.</returns>
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

        /// <summary>
        /// Método utilizado para obtener todos los productores que se encuentran registrados en la base de datos.
        /// </summary>
        /// <returns>Retorna un ArrayList que contiene todos los productores que se encuentran registrados en la base de datos</returns>
        public ArrayList getAllProducers()
        {
            ArrayList producers = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;

            String sqlString = "SELECT * FROM productores";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);

            sqlReader = cmd.ExecuteReader();
            while (sqlReader.Read())
            {
                Producer form = new Producer(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetString(3), sqlReader.GetString(4), sqlReader.GetString(5), sqlReader.GetString(6), sqlReader.GetInt32(7),sqlReader.GetString(8), sqlReader.GetInt32(9),sqlReader.GetInt32(10),sqlReader.GetString(11), sqlReader.GetString(12), sqlReader.GetString(13),sqlReader.GetString(15));
                producers.Add(form);
            }
            return producers;
        }

        public ArrayList getProducersByLocation(string province, string canton, string district)
        {
            ArrayList producers = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;

            String sqlString = "SELECT * FROM productores WHERE Provincia='"+province+"' AND Canton='"+canton+"' AND Distrito='"+district+"'";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);

            sqlReader = cmd.ExecuteReader();
            while (sqlReader.Read())
            {
                Producer form = new Producer(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetString(3), sqlReader.GetString(4), sqlReader.GetString(5), sqlReader.GetString(6), sqlReader.GetInt32(7), sqlReader.GetString(8), sqlReader.GetInt32(9), sqlReader.GetInt32(10), sqlReader.GetString(11), sqlReader.GetString(12), sqlReader.GetString(13),sqlReader.GetString(15));
                producers.Add(form);
            }

            sqlReader.Close();
            return producers;


        }

        /// <summary>
        /// Método utilizado para obtener un productor determinado asociado a un identificador específico, registrado en la base de datos.
        /// </summary>
        /// <param name="id">Identificador único asociado a un productor.</param>
        /// <returns>Retorna un productor o null si no existe un productor asociado al identificador.</returns>
        public Producer getProducer(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM productores WHERE Cedula=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return new Producer(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetString(3), sqlReader.GetString(4), sqlReader.GetString(5), sqlReader.GetString(6), sqlReader.GetInt32(7), sqlReader.GetString(8), sqlReader.GetInt32(9), sqlReader.GetInt32(10), sqlReader.GetString(11), sqlReader.GetString(12), sqlReader.GetString(13),sqlReader.GetString(15));
            }
            return null;
        }

        /// <summary>
        /// Método utilizado para actualizar, en la base de datos, los datos de un productor específico asociado a un indentificador dado.
        /// </summary>
        /// <param name="id">Identificador único asociado a un productor.</param>
        /// <param name="producer">Productor al cual se le requiere actualizar los datos.</param>
        /// <returns>Retorna un código númerico que permite identificar el estado de la operación.</returns>
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
                        sqlString = "UPDATE productores SET Cedula=" + producer.cedula.ToString() + ",Nombre='" + producer.name + "',Apellidos='" + producer.lastName + "',Provincia='" + producer.province + "',Canton='" + producer.canton + "',Distrito='" + producer.district + "',Direccion='" + producer.address + "',Telefono=" + producer.phoneN.ToString() + ",Fecha_Nacimiento='" + producer.birthDate+ "',Num_Sinpe=" + producer.sinpeN.ToString() + ",Calificacion=" + producer.calification + ",Lugares_Entrega='" + producer.deliveryPlaces + "',nombreNegocio='" + producer.businessName + "',Password='" + producer.getPassword() + "' WHERE Cedula=" + id.ToString();
                        cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                        cmd.ExecuteNonQuery();
                        return 200;
                    }
                    sqlReader.Close();
                    sqlString = "UPDATE productores SET Cedula=" + producer.cedula.ToString() + ",Nombre='" + producer.name + "',Apellidos='" + producer.lastName + "',Provincia='" + producer.province + "',Canton='" + producer.canton + "',Distrito='" + producer.district + "',Direccion='" + producer.address + "',Telefono=" + producer.phoneN.ToString() + ",Fecha_Nacimiento='" + producer.birthDate+ "',Num_Sinpe=" + producer.sinpeN.ToString() + ",Calificacion=" + producer.calification + ",Lugares_Entrega='" + producer.deliveryPlaces + "',nombreNegocio='" + producer.businessName + "',Password='" + producer.getPassword() + "' WHERE Cedula=" + id.ToString();
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
                    sqlString = "UPDATE productores SET Cedula=" + producer.cedula.ToString() + ",Nombre='" + producer.name + "',Apellidos='" + producer.lastName + "',Provincia='" + producer.province + "',Canton='" + producer.canton + "',Distrito='" + producer.district + "',Direccion='" + producer.address + "',Telefono=" + producer.phoneN.ToString() + ",Fecha_Nacimiento='" + producer.birthDate + "',Num_Sinpe=" + producer.sinpeN.ToString() + ",Calificacion=" + producer.calification + ",Lugares_Entrega='" + producer.deliveryPlaces + "',nombreNegocio='" + producer.businessName + "',Password='" + producer.getPassword() + "' WHERE Cedula=" + id.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    cmd.ExecuteNonQuery();
                    return 200;
                }
                return 200;
            }
            return 404;
        }
        /// <summary>
        /// Método para eliminar el productor asociado a un identificador dado, de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único</param>
        /// <returns>String que permite reconocer el estado de la operación o null en caso de que no exista el productor</returns>
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

        /// <summary>
        /// Método para crear producto en la base de datos.
        /// </summary>
        /// <param name="product">Producto el cual se requiere crear en la base de datos.</param>
        /// <returns>Retorna un string que permite identificar el estado de la operación.</returns>
        public string createProduct(Product product)
        {
            string sqlString = "INSERT INTO productos (ID,Nombre,Categoria,Productor,Foto,Precio,Modo_Venta,Disponibilidad,Ganancias) VALUES (" + product.id.ToString() + ",'" + product.name + "','" + product.category + "'," + product.producer.ToString() + ",'" + product.image + "'," + product.cost.ToString() + ",'" + product.saleMode + "'," + product.inStock.ToString() + "," + product.quantity.ToString()+")";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            cmd.ExecuteNonQuery();
            return "OK";
        }

        /// <summary>
        /// Método para obtener el producto, asociado a un identificador dado, de la base de datos.
        /// </summary>
        /// <param name="id">Identificador único.</param>
        /// <returns>Retorna un producto.</returns>
        public Product getProduct(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM productos WHERE ID=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return new Product(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetInt32(3), sqlReader.GetString(4), sqlReader.GetFloat(5), sqlReader.GetString(6), sqlReader.GetFloat(7), sqlReader.GetInt32(8));
            }
            return null;
        }

        /// <summary>
        /// Método utilizado para actualizar, en la base de datos, los datos de un producto específico asociado a un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único.</param>
        /// <param name="product">Producto al cual se le requiere actualizar los datos.</param>
        /// <returns>Retorna un string que permite verificar el estado de la operación.</returns>
        public string updateProduct(int id, Product product)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM productos WHERE ID=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                sqlReader.Close();
                sqlString = "UPDATE productos SET ID=" + product.id.ToString() + ",Nombre='" + product.name + ",Categoria=" + product.category + ",Productor=" + product.producer.ToString() + ",Foto=" + product.image + ",Precio=" + product.cost.ToString() + ",Modo_Venta=" + product.saleMode + ",Disponibilidad=" + product.inStock.ToString() + ",Ganancias=" + product.quantity.ToString() + " WHERE Cedula=" + id.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                cmd.ExecuteNonQuery();
                return "200";
            }
            return "404";
        }

        /// <summary>
        /// Método para eliminar un producto de la base de datos según identificador dado.
        /// </summary>
        /// <param name="id">Identificador único.</param>
        /// <returns>Retorna un string para verificar el estado de la operación.</returns>
        public string deleteProduct(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM productos WHERE ID='" + id.ToString() + "'";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                sqlReader.Close();
                sqlString = "DELETE FROM productos WHERE ID=" + id.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                cmd.ExecuteNonQuery();
                return "200";
            }
            return "404";
        }

        /// <summary>
        /// Método para obtener todos los productos de un productor dado según una cédula dada.
        /// </summary>
        /// <param name="cedula">Identificador único.</param>
        /// <returns>Retorna un ArrayList de productos.</returns>
        public ArrayList getProducerAllProducts(int cedula)
        {
            ArrayList products = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;

            String sqlString = "SELECT * FROM productos WHERE Productor=" + cedula.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);

            sqlReader = cmd.ExecuteReader();
            while (sqlReader.Read())
            {
                Product list = new Product(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetInt32(3), sqlReader.GetString(4), sqlReader.GetFloat(5), sqlReader.GetString(6), sqlReader.GetFloat(7), sqlReader.GetInt32(8));
                products.Add(list);
            }
            sqlReader.Close();
            return products;
        }

        /// <summary>
        /// Método para obtener el top 10 de productos más vendidos de un producto dado según una cédula dada.
        /// </summary>
        /// <param name="cedula">Identificador único.</param>
        /// <returns>Retorna un ArrayList con los 10 productos más vendidos de un productor dado.</returns>
        public ArrayList getProducerTop10SoldProducts(int cedula)
        {
            ArrayList products = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;

            String sqlString = "SELECT* FROM productos WHERE Productor=" + cedula.ToString() + " ORDER BY Vendidos ASC LIMIT 10 ";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);

            sqlReader = cmd.ExecuteReader();
            while (sqlReader.Read())
            {
                Product list = new Product(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetInt32(3), sqlReader.GetString(4), sqlReader.GetFloat(5), sqlReader.GetString(6), sqlReader.GetFloat(7), sqlReader.GetInt32(8));
                products.Add(list);
            }
            return products;
        }

        /// <summary>
        /// Método para obtener el top 10 general de productos más vendidos.
        /// </summary>
        /// <returns>Retorna un ArrayList con los 10 productos más vendidos.</returns>
        public ArrayList getTop10SoldProducts()
        {
            ArrayList products = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;

            String sqlString = "SELECT * FROM productos ORDER BY Vendidos ASC LIMIT 10";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);

            sqlReader = cmd.ExecuteReader();
            while (sqlReader.Read())
            {
                Product list = new Product(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetInt32(3), sqlReader.GetString(4), sqlReader.GetFloat(5), sqlReader.GetString(6), sqlReader.GetFloat(7), sqlReader.GetInt32(8));
                products.Add(list);
            }
            return products;
        }

        /// <summary>
        /// Método para obtener el top 10 general de productos que más ganancias generan.
        /// </summary>
        /// <returns>Retorna un ArrayList con los 10 productos que más ganancias generan.</returns>
        public ArrayList getTop10MostProfitableProducts()
        {
            ArrayList products = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;

            String sqlString = "SELECT * FROM productos ORDER BY Ganancias ASC LIMIT 10";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);

            sqlReader = cmd.ExecuteReader();
            while (sqlReader.Read())
            {
                Product list = new Product(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetInt32(3), sqlReader.GetString(4), sqlReader.GetFloat(5), sqlReader.GetString(6), sqlReader.GetFloat(7), sqlReader.GetInt32(8));
                products.Add(list);
            }
            return products;
        }

        /// <summary>
        /// Método para almacenar una afiliación de productor en la base de datos.
        /// </summary>
        /// <param name="form">Afiliación por guardar</param>
        /// <returns>Retorna un string para verificar el estado de la operación.</returns>
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

        /// <summary>
        /// Método utilizado para actualizar, en la base de datos, los datos de una afiliación de producto específico asociado a un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único.</param>
        /// <param name="statusComment">Comentario almacenado.</param>
        /// <returns>Retorna un string para verificar el estado de la operación.</returns>
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

        /// <summary>
        /// Método para obtener todas las categorías existentes en la base de datos.
        /// </summary>
        /// <returns>Retorna un ArrayList que contiene todas las categorías.</returns>
        public ArrayList getAllCategories()
        {
            ArrayList categories = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;

            String sqlString = "SELECT * FROM categorias";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);

            sqlReader = cmd.ExecuteReader();
            while (sqlReader.Read())
            {
                Category category = new Category(sqlReader.GetInt32(0),sqlReader.GetString(1));
                categories.Add(category);
            }
            return categories;
        }

        /// <summary>
        /// Método para obtener una categoría específica según un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único.</param>
        /// <returns>Retorna una categoría.</returns>
        public Category getCategory(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM categorias WHERE ID=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return new Category(sqlReader.GetInt32(0), sqlReader.GetString(1));
            }
            return null;
        }

        /// <summary>
        /// Método para crear categoría en la base de datos.
        /// </summary>
        /// <param name="category">Categoría la cual se creará en la base de datos.</param>
        /// <returns>Retorna un código numérico para reconocer el estado de la operación.</returns>
        public int createCategory(Category category)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM categorias WHERE ID=" + category.ID;
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return 409;
            }
            sqlReader.Close();
            sqlString = "SELECT * FROM categorias WHERE Nombre='" + category.name+"'";
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return 409;
            }

            sqlReader.Close();
            sqlString = "INSERT INTO categorias (ID,Nombre) VALUES (" + category.ID.ToString() + ",'" + category.name + "')";
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            cmd.ExecuteNonQuery();
            return 200;
        }

        /// <summary>
        /// Método para actualizar, en la base de datos, los datos de una categoría específica asociada a un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único.</param>
        /// <param name="category">Categoría con los datos actualizados</param>
        /// <returns></returns>
        public int updateCategory(int id, Category category)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM categorias WHERE ID=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                int actualID = sqlReader.GetInt32(0);
                string actualName = sqlReader.GetString(1);

                if (category.ID != id)
                {
                    sqlReader.Close();
                    sqlString = "SELECT * FROM categorias WHERE ID=" + category.ID.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    sqlReader = cmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        return 409;
                    }
                   

                    if (!actualName.Equals(category.name))
                    {
                        sqlReader.Close();
                        sqlString = "SELECT * FROM categorias WHERE Nombre=" + category.name;
                        cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                        sqlReader = cmd.ExecuteReader();
                        if (sqlReader.Read())
                        {
                            return 409;
                        }

                        sqlReader.Close();
                        sqlString = "UPDATE categorias SET ID=" + category.ID.ToString() + ",Nombre='" + category.name + "' WHERE ID=" + id.ToString();
                        cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                        cmd.ExecuteNonQuery();
                        return 200;
                    }
                    sqlReader.Close();
                    sqlString = "UPDATE categorias SET ID=" + category.ID.ToString() + " WHERE ID=" + id.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    cmd.ExecuteNonQuery();
                    return 200;

                }
                if(!actualName.Equals(category.name))
                {
                    sqlReader.Close();
                    sqlString = "SELECT * FROM categorias WHERE Nombre='" + category.name+"'";
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    sqlReader = cmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        return 409;
                    }

                    sqlReader.Close();
                    sqlString = "UPDATE categorias SET Nombre='" + category.name + "' WHERE ID=" + id.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    cmd.ExecuteNonQuery();
                    return 200;
                }
                return 200;
            }
            return 404;
        }

        /// <summary>
        /// Método para eliminar una categoría asociada a un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único.</param>
        /// <returns>Retorna código númerico para verificar el estado de la operación.</returns>
        public int deleteCategory(int id)
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

        /// <summary>
        /// Método para obtener todos los clientes almacenados en la base de datos.
        /// </summary>
        /// <returns>Retorna un ArrayList con todos los clientes.</returns>
        public ArrayList getAllClients()
        {
            ArrayList clients = new ArrayList();

            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;

            String sqlString = "SELECT * FROM clientes";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);

            sqlReader = cmd.ExecuteReader();
            while (sqlReader.Read())
            {
                Client category = new Client(sqlReader.GetInt32(0), sqlReader.GetString(1),sqlReader.GetString(2),sqlReader.GetString(3),sqlReader.GetString(4),sqlReader.GetString(5),sqlReader.GetString(6),sqlReader.GetInt32(7),sqlReader.GetString(8),sqlReader.GetString(9),sqlReader.GetString(10));
                clients.Add(category);
            }
            return clients;
        }

        /// <summary>
        /// Método para obtener un cliente específico según un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único.</param>
        /// <returns>Retorna un cliente asociado al identificador dado.</returns>
        public Client getClient(int id)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM clientes WHERE Cedula=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return new Client(sqlReader.GetInt32(0), sqlReader.GetString(1), sqlReader.GetString(2), sqlReader.GetString(3), sqlReader.GetString(4), sqlReader.GetString(5), sqlReader.GetString(6), sqlReader.GetInt32(7), sqlReader.GetString(8), sqlReader.GetString(9), sqlReader.GetString(10));
            }
            return null;
        }

        /// <summary>
        /// Método para crear un cliente en la base de datos.
        /// </summary>
        /// <param name="client">Cliente por crear.</param>
        /// <returns>Retorna un código númerico para verificar el estado de la operación.</returns>
        public int createClient(Client client)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM clientes WHERE Cedula=" + client.cedula.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return 409;
            }

            sqlReader.Close();
            sqlString = "SELECT * FROM clientes WHERE Usuario='" + client.userName+"'";
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                return 409;
            }

            ArrayList cryptoComponents = sha256PasswordHasher(client.getPassword());

            sqlReader.Close();
            sqlString = "INSERT INTO clientes (Cedula,Nombre,Apellidos,Provincia,Canton,Distrito,Direccion,Telefono,Fecha_Nacimiento,Usuario,Password,Salt) VALUES (" + client.cedula.ToString() + ",'" + client.name +"','"+client.lastName+ "','"+client.province+ "','"+client.canton+ "','"+client.district+ "','"+client.address+ "',"+client.phoneN.ToString()+ ",'"+client.birthDate+"','"+client.userName+"','"+cryptoComponents[0]+"','"+cryptoComponents[1]+"')";
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            cmd.ExecuteNonQuery();

            sqlReader.Close();
            sqlString = "INSERT INTO tokens (Usuario,Token,Tipo) VALUES (" + client.cedula.ToString() + ",'"+Convert.ToBase64String(Guid.NewGuid().ToByteArray())+"','clientes')";
            cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            cmd.ExecuteNonQuery();


            return 200;
        }

        /// <summary>
        /// Método para actualizar, en la base de datos, los datos de un cliente específico asociado a un identificador dado.
        /// </summary>
        /// <param name="id">Identificador único.</param>
        /// <param name="client">Cliente con datos actualizados.</param>
        /// <returns></returns>
        public int updateClient(int id, Client client)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM clientes WHERE Cedula=" + id.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                
                if (client.cedula != id)
                {
                    sqlReader.Close();
                    sqlString = "SELECT * FROM clientes WHERE Cedula=" + client.cedula.ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    sqlReader = cmd.ExecuteReader();
                    if (sqlReader.Read())
                    {
                        return 409;
                    }
                }
                sqlReader.Close();
                sqlString = "UPDATE clientes SET Cedula=" + client.cedula.ToString() + ",Nombre='" + client.name + "',Apellidos='" + client.lastName + "',Provincia='" + client.province + "',Canton='" + client.canton + "',Distrito='" + client.district + "',Direccion='" + client.address + "',Telefono=" + client.phoneN.ToString() + ",Fecha_Nacimiento='" + client.birthDate + "',Usuario='" + client.userName + "',Password='" + client.getPassword() + "' WHERE Cedula=" + id.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                cmd.ExecuteNonQuery();
                return 200;
            }
            return 404;
        }

        /// <summary>
        /// Método para obtener un cliente según un token y un nombre de usuario dado.
        /// </summary>
        /// <param name="token">Token único dado.</param>
        /// <param name="userName">Nombre de usuario dado.</param>
        /// <returns>Retorna cliente asociado al token y al nombre de usuario.</returns>
        public Client getClientbyUserName(string token, string userName)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM tokens WHERE Token='" + token+"'";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                sqlString = "SELECT * FROM clientes WHERE Cedula=" +sqlReader.GetInt32(0).ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                sqlReader.Close();
                sqlReader = cmd.ExecuteReader();
                sqlReader.Read();
                if(sqlReader.GetString(9).Equals(userName))
                {
                    Client c=new Client(sqlReader.GetInt32(0), sqlReader.GetString(1),sqlReader.GetString(2),sqlReader.GetString(3),sqlReader.GetString(4),sqlReader.GetString(5),sqlReader.GetString(6),sqlReader.GetInt32(7),sqlReader.GetString(8),sqlReader.GetString(9),null);

                    ArrayList notifications = new ArrayList();

                    sqlString = "SELECT * FROM notificaciones WHERE Cliente="+sqlReader.GetInt32(0).ToString();
                    sqlReader.Close();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    sqlReader = cmd.ExecuteReader();
                    while(sqlReader.Read())
                    {
                        notifications.Add(new Notification(sqlReader.GetInt32(0),sqlReader.GetInt32(1),sqlReader.GetInt32(2),sqlReader.GetString(3)));
                    }
                    c.addNotifications(notifications);
                    return c;
                }

            }
            return null;

        }

        /// <summary>
        /// Método para eliminar un cliente según un token dado.
        /// </summary>
        /// <param name="token">Token único dado.</param>
        /// <returns>Retorna un código númerico.</returns>
        public int deleteClient(string token)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            string sqlString = "SELECT * FROM tokens WHERE Token='" + token+"' AND Tipo='clientes'";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                string cedula = sqlReader.GetInt32(0).ToString();

                sqlString = "DELETE FROM clientes WHERE Cedula="+cedula;
                sqlReader.Close();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                cmd.ExecuteNonQuery();

                sqlString = "DELETE FROM tokens WHERE Usuario=" + cedula+" AND Tipo='clientes'";
                sqlReader.Close();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                cmd.ExecuteNonQuery();


                return 200;
            }
            return 404;
        }

        /// <summary>
        /// Método para crear un pedido en la base de datos.
        /// </summary>
        /// <param name="order">Pedido por ser creado en las base de datos.</param>
        /// <returns>Retorna un código númerico para verificar el estado de la operación.</returns>
        public int createOrder(Order order)
        {
            orderNumberGenerator();
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            String sqlString = "SELECT * FROM tokens WHERE Token='" + order.token + "'AND Tipo='clientes' AND Usuario=" + order.clientID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                foreach(List<int> tuple in order.productIds)
                {
                    sqlReader.Close();
                    sqlString = "SELECT * FROM productos WHERE ID=" + tuple[0].ToString();
                    cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                    sqlReader=cmd.ExecuteReader();
                    
                    if(sqlReader.Read())
                    {
                        sqlString = "INSERT INTO pedidos (ID,Cliente,Productor,ID_Producto,Comprobante,Direccion,Estado,Cantidad) VALUES (" +this.orderNumber.ToString()+ "," + order.clientID.ToString() + "," +sqlReader.GetInt32(3)+ "," + tuple[0] + ",'" + order.invoice + "','" + order.address + "','PENDIENTE'," +tuple[1]+ ")";
                        sqlReader.Close();
                        cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        return 409;
                    }
                    sqlReader.Close();
                }
                return 200;
            }
            return 409;
        }

        /// <summary>
        /// Método para obtener un token de un usuario.
        /// </summary>
        /// <param name="userName">Nombre de usuario.</param>
        /// <param name="password">Contraseña.</param>
        /// <param name="type">Tipo de usuario.</param>
        /// <returns>Retorna el token del usuario dado.</returns>
        public string getToken(string userName,string password,string type)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            String sqlString = "";
            if (type.Equals("cliente"))
            {
                sqlString = "SELECT * FROM clientes WHERE Usuario='" + userName + "'";
            }
            else if (type.Equals("productor"))
            {
                sqlString = "SELECT * FROM productores WHERE Cedula=" + userName;
            }
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {

                if (type.Equals("cliente"))
                {
                    if (passwordVerifier(password, sqlReader.GetString(10), sqlReader.GetString(11)))
                    {
                        string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                        sqlString = "UPDATE tokens SET Token='" + token + "' WHERE Usuario=" + sqlReader.GetInt32(0).ToString() + " AND Tipo='clientes'";
                        sqlReader.Close();
                        cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                        cmd.ExecuteNonQuery();

                        return token;
                    }
                    return "409";
                }
                else if(type.Equals("productor"))
                {
                    if (passwordVerifier(password, sqlReader.GetString(13), sqlReader.GetString(14)))
                    {
                        string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                        sqlString = "UPDATE tokens SET Token='" + token + "' WHERE Usuario=" + sqlReader.GetInt32(0).ToString() + " AND Tipo='productores'";
                        sqlReader.Close();
                        cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                        cmd.ExecuteNonQuery();

                        return token;
                    }
                    return "409";
                }
            }
            return "409";
        }


        /// <summary>
        /// Método para cerrar sesión.
        /// </summary>
        /// <param name="credentials">Credenciales de usuario.</param>
        /// <returns>Retorna el estado de la petición.</returns>
        public bool logOut(SignOutRequest credentials)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            String sqlString = "SELECT * FROM tokens WHERE Token='" + credentials.token+"' AND Tipo='"+credentials.type+"'";
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                string token = Convert.ToBase64String(Guid.NewGuid().ToByteArray());

                sqlReader.Close();
                sqlString = "UPDATE tokens SET Token='" + token + "' WHERE Token='" + credentials.token+"' AND Tipo='"+credentials.type+"'";
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                cmd.ExecuteNonQuery();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Método para encriptar contraseña. 
        /// </summary>
        /// <param name="input">Contraseña por encriptar.</param>
        /// <returns>Retorna un ArrayList con la contraseña encriptada.</returns>
        public ArrayList sha256PasswordHasher(string input)
        {
            ArrayList cryptoComponents = new ArrayList();
            var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
            var buff = new byte[10];
            rng.GetBytes(buff);

            string salt = Convert.ToBase64String(buff);

            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
            System.Security.Cryptography.SHA256Managed sha256string = new System.Security.Cryptography.SHA256Managed();

            byte[] hash = sha256string.ComputeHash(bytes);

            cryptoComponents.Add(Convert.ToBase64String(hash));
            cryptoComponents.Add(salt);

            return cryptoComponents;
        }

        /// <summary>
        /// Método para verificar contraseña.
        /// </summary>
        /// <param name="password">Contraseña ingresada por el usuario.</param>
        /// <param name="hashedPassword">Contraseña encriptada.</param>
        /// <param name="salt">Salt para encriptación.</param>
        /// <returns>Retorna un booleano dependiendo de si las contraseñas son iguales.</returns>
        public bool passwordVerifier(string password,string hashedPassword,string salt)
        {
            byte[] bytes = System.Text.Encoding.UTF8.GetBytes(password + salt);
            System.Security.Cryptography.SHA256Managed sha256string = new System.Security.Cryptography.SHA256Managed();

            byte[] hash = sha256string.ComputeHash(bytes);


            return hashedPassword.Equals(Convert.ToBase64String(hash));
        }

        /// <summary>
        /// Método para asignar a una lista de productores sus respectivos productos.
        /// </summary>
        /// <param name="producers">Lista de productores.</param>
        /// <returns>Retorna los productores con sus productos ya asignados.</returns>
        public ArrayList productAsigner(ArrayList producers)
        {
            foreach (Producer p in producers)
            {
                p.products = getProducerAllProducts(p.cedula);
            }
            return producers;
        }

        /// <summary>
        /// Método para procesar la calificación de un productor.
        /// </summary>
        /// <param name="rating">Calificación.</param>
        /// <param name="producerID">Identificador del productor.</param>
        /// <param name="notificationID">Identificador de la notificación asociada</param>
        /// <returns>Retorna un código numérico que permite verificar el estado de la operación.</returns>
        public int postRating(int rating, int producerID, int notificationID)
        {
            MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
            String sqlString = "SELECT * FROM productores WHERE Cedula=" + producerID.ToString();
            MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
            sqlReader = cmd.ExecuteReader();
            if (sqlReader.Read())
            {
                sqlString = "UPDATE productores SET Calificacion=" +((sqlReader.GetInt32(10)+rating)/2).ToString()+ " WHERE Cedula="+producerID.ToString();
                sqlReader.Close();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                cmd.ExecuteNonQuery();

                sqlString = "DELETE FROM notificaciones WHERE ID="+notificationID.ToString();
                cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                cmd.ExecuteNonQuery();


                return 200;
            }
            return 409;
        }

        /// <summary>
        /// Método que asigna un número identificador a un pedido.
        /// </summary>
        public void orderNumberGenerator()
        {
            int number =this.orderNumber;
            while (true)
            {
                MySql.Data.MySqlClient.MySqlDataReader sqlReader = null;
                String sqlString = "SELECT * FROM pedidos WHERE ID="+number.ToString();
                MySql.Data.MySqlClient.MySqlCommand cmd = new MySql.Data.MySqlClient.MySqlCommand(sqlString, connection);
                sqlReader = cmd.ExecuteReader();
                if(!sqlReader.Read())
                {
                    sqlReader.Close();
                    break;
                }
                number++;
                sqlReader.Close();
            }
            this.orderNumber= number;
        }
    }
}