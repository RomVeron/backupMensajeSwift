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
        public readonly string[] ticketSb = new string[2];
        public string[] idInstanciaSb = new string[2];
        readonly HistoricoMsjSwift_4.sb.servicebus elSb = new HistoricoMsjSwift_4.sb.servicebus();
        private string[] noise = new string[2];
        private string[] ticketResult = new string[2];

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

        public void registroHistoricoLogEnvioSwift(string cuenta,
                                                   string estado,
                                                   string nombre_archivo_swift,
                                                   string path,
                                                   string referencia,
                                                   string sender,
                                                   string receiber,
                                                   string inout,
                                                   string mt,
                                                   DateTime fecha)
        {

            OracleConnection conexion = new OracleConnection(this.cadenaDeConeccion);
            OracleCommand comando = new OracleCommand("wilson1.pkg_catastro_swift.sp_reg_hist_logs_envio_swift", conexion);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.Add("pi_cuenta", cuenta);
            comando.Parameters.Add("pi_estado", estado);
            comando.Parameters.Add("pi_nomarchswift", nombre_archivo_swift);
            comando.Parameters.Add("pi_path", path);
            comando.Parameters.Add("pi_referencia", referencia);
            comando.Parameters.Add("pi_sender", sender);
            comando.Parameters.Add("pi_receiver", receiber);
            comando.Parameters.Add("pi_inout", inout);
            comando.Parameters.Add("pi_mt", mt);
            comando.Parameters.Add("pi_fecha", fecha);

            try
            {
                comando.Connection.Open();
                comando.ExecuteNonQuery();
                comando.Connection.Close();
            }
            catch (OracleException ex)
            {
                //MessageError(ex);
                //MessageBox.Show(ex.Errors.ToString() + "  " + ex.ToString());
            }
            finally
            {
                conexion.Close();
            }
        }


        /// <summary>
        /// Método que obtiene el número de manual del programa.
        /// </summary>
        /// <returns></returns>
        public string sp_dt_param_menu()
        {
            /*wilson1.pkg_carta_referencia.sp_dt_param_menu(v_menu => :v_menu);*/
            try
            {
                var losParametros = new HistoricoMsjSwift_4.sb.elParametro[1];
                losParametros[0] = new HistoricoMsjSwift_4.sb.elParametro() { nombre = "v_menu", esOut = true, tipoDato = HistoricoMsjSwift_4.sb.tipoDeDatos.Varchar2 };
                generar();

                var elVuelto = elSb.intOracle("wilson1.pkg_carta_referencia.sp_dt_param_menu", losParametros, elTicket(), laInstanacia(), elNoise());
                return elVuelto.resultado.ToString();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Generar token de conexion sl SB.
        /// </summary>
        private void generar() { generar(0); }
        /// <summary>
        /// Generar token de conexion sl SB.
        /// </summary>
        /// <param name="posicion"></param>
        private void generar(int posicion)
        {
            noise[posicion] = DateTime.Now.Ticks.ToString();
            ticketResult[posicion] = utiles.GetMD5(ticketSb[posicion] + noise[posicion]);
        }
        /// <summary>
        /// Gevuelve ticket de conexion actual al SB.
        /// </summary>
        /// <returns></returns>
        private string elTicket() { return elTicket(0); }
        /// Devuelve ticket de conexion actual al SB.
        private string elTicket(int posicion)
        {
            return ticketResult[posicion];
        }
        /// <summary>
        /// La semilla para ocultar el ticket de sesion en la comunicacion.
        /// </summary>
        /// <returns></returns>
        private string elNoise() { return elNoise(0); }
        /// <summary>
        /// La semilla para ocultar el ticket de sesion en la comunicacion
        /// </summary>
        /// <param name="posicion"></param>
        /// <returns></returns>
        private string elNoise(int posicion)
        {
            return noise[posicion];
        }
        /// <summary>
        /// Devuelve la instancia de conexion a la BD.
        /// </summary>
        /// <returns></returns>
        private string laInstanacia() { return laInstanacia(0); }
        /// <summary>
        /// Devuelve la instancia de conexion a la BD.
        /// </summary>
        /// <param name="posicion"></param>
        /// <returns></returns>
        private string laInstanacia(int posicion)
        {
            return idInstanciaSb[posicion];
        }

        #endregion
    }
}
