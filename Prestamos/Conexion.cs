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
               //no = this.dataReader.GetInt32(this.dataReader.GetOrdinal("ID_PRODUCTO"));

            //   conexionSQL.mensaje = no.ToString();
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
                            //rows number of record got updated
                            
                    
                }
                catch (SqlException ex)
                {

                    return false;
                    //Log exception
                    //Display Error message
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