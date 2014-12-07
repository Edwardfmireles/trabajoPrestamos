using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
using System.IO;

namespace Prestamos
{
    public class Conexion
    {

        public string query;
        protected int resultado;
        protected SqlConnection conn;
        protected SqlCommand comsql;
        public static string mensaje = String.Empty;
        protected SqlDataAdapter registros = null;
        protected SqlDataReader dataReader = null;

        public static SqlConnection ConectarBD;

        // CONSTRUCTOR DE LA CLASE CONEXION EL CUAL INICIA UNA CONEXION SQL
        public Conexion()
        {

            StreamReader objReader = new StreamReader("c:\\conexion.txt");

            string line = "";

            while (line != null)
            {
                line = objReader.ReadLine();
                break;
            }
            objReader.Close();

            Conexion.ConectarBD = new SqlConnection(line);

        }

        // METODO PARA OBTENER EL ULTIMO ID DE UNA TABLA
        public int obtenerUltimoId(string select, string from, string where)
        {

            if (select != String.Empty && select.Length > 4 && from != String.Empty && from.Length > 4)
            {
                this.query = "SELECT " + select + " FROM " + from;

                if (where != String.Empty)
                {
                    //this.query += " WHERE '" + where + "'";
                    this.query += where;
                }
            }

           int  no = 0;

           try 
           {
               Conexion.ConectarBD.Open();
               this.comsql = new SqlCommand(this.query, Conexion.ConectarBD);
               
               this.dataReader = this.comsql.ExecuteReader();

               while (this.dataReader.Read())
               {
                   no++;
               }
           }
          catch(SqlException s)
           {
               throw new Exception("EROR>>>>>" + s.Message);
           }
           finally
           {
               Conexion.ConectarBD.Close();
           }

           return no;

        }

        // METODO PARA INSERTAR DATOS EN LA BASE DE DATOS
        public bool insertar(string into, string values, string where)
        {

            if (into != String.Empty && into.Length > 4 && values != String.Empty && values.Length > 4)
            {
                this.query = "INSERT INTO " + into + " VALUES " + values;

                if (where != String.Empty && where.Length > 4)
                {
                    this.query += " WHERE " + where;
                }

            }

            Conexion.mensaje = this.query;
                
                try
                {
                    Conexion.ConectarBD.Open();
                    this.comsql = new SqlCommand(this.query, Conexion.ConectarBD);
                    
                    this.comsql.ExecuteNonQuery();

                    return true;

                }
                catch (SqlException e)
                {
                    Conexion.mensaje = "error al se "+e.Message;           
                    return false;
                }
                finally
                {
                 
                    Conexion.ConectarBD.Close();

                }

        }

        // METODO PARA ACTUALIZAR UNA COLUMNA EN LA BASE DE DATOS
        public bool actualizar(string tabla, string columna, string valor, string where)
        {
            if (tabla != String.Empty && tabla.Length > 4 && columna != String.Empty && columna.Length > 4 && valor != String.Empty && valor.Length > 0 && where != String.Empty && where.Length > 4)
            {
                this.query = "UPDATE " + tabla + " SET " + columna + "=" + valor + " WHERE " + where;

                try
                {

                    Conexion.ConectarBD.Open();
                    this.comsql = new SqlCommand(this.query, Conexion.ConectarBD);

                    Conexion.mensaje = this.query;

                    this.comsql.ExecuteNonQuery();
                    return true;
                    
                }
                catch (SqlException)
                {

                }
                finally
                {
                    Conexion.ConectarBD.Close();
                }

            }

            return false;
        }
        
    }
}