using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Oracle.DataAccess;

namespace f2
{
    /// <summary>
    /// aqui va todo el codigo del developer
    /// que relaciona formulario con el mundo exterior
    /// </summary>
    class businessLayer
    {
        public DataTable dstipo() 
        {
            Oracle.DataAccess.Client.OracleConnection cone = new OracleConnection(this.cadenaDeConeccion);
            Oracle.DataAccess.Client.OracleDataAdapter adap = new OracleDataAdapter("wilson1.pkg_ganado.sp_nombre_ganado", cone);
            adap.SelectCommand.CommandType = CommandType.StoredProcedure;
            adap.SelectCommand.Parameters.Add("PO_CURSOR", OracleDbType.RefCursor);
            adap.SelectCommand.Parameters["PO_CURSOR"].Direction = ParameterDirection.Output;
            DataTable vuelto = new DataTable();
            try
            {
                adap.Fill(vuelto);
            }
            catch { throw; }
            return vuelto;
        }

        #region  standar
        /// <summary>
        /// cadena de coneccion del ensamblado
        /// </summary>
        //private string cadenaCone = "User Id=AAA111;Password=BBB222;Data Source=CCC333;Min Pool Size=10;Connection Lifetime=120;Connection Timeout=60;Incr Pool Size=5; Decr Pool Size=2";
        private string cadenaCone = "User Id=AAA111;Password=BBB222;Data Source=CCC333;Pooling = false;Connection Lifetime=5;Connection Timeout=60";
        /// <summary>
        /// devuelve la cadena de conexion para oracle
        /// </summary>
        public string cadenaDeConeccion { get { return this.cadenaCone; } }
        /// <summary>
        /// constructor simple
        /// </summary>
        public businessLayer(){}
        /// <summary>
        /// contruye la clase de negocios 
        /// con datos de la BD
        /// </summary>
        /// <param name="usuario">usuario de oracle</param>
        /// <param name="passWord">password de oracle</param>
        /// <param name="dataSource">el TND a donde apuntar</param>
        public businessLayer(string usuario, string passWord, string dataSource)
        {
            cadenaCone = cadenaCone.Replace("AAA111", usuario).Replace("BBB222", passWord).Replace("CCC333", dataSource);
        }
        #endregion

    }
}
