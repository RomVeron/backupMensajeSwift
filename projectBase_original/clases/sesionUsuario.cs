using System;
using System.Collections.Generic;
using System.Text;

namespace f2
{
    /// <summary>
    /// usuario de transpaso de datos de finansys 2
    /// </summary>
    public class objFinanasys2
    {
        /// <summary>
        /// contructor
        /// </summary>
        public objFinanasys2()
        {
            _parametros = "";
            _userPassword = "";
            _usuario = "";
            _version = "";
        }
        private string _usuario;
        private string _userPassword;
        private string _parametros;
        private string _version;
        /// <summary>
        /// usuario de oracle desde VFP
        /// </summary>
        public string usuario { get { return this._usuario; } set { this._usuario = value; } }
        /// <summary>
        /// password de oracle desde VFP
        /// </summary>
        public string userPassword { get { return this._userPassword; } set { this._userPassword = value; } }
        /// <summary>
        /// parametros a pasar a aplicacion c# de VFP
        /// </summary>
        public string parametros { get { return this._parametros; } set { this._parametros = value; } }
        /// <summary>
        /// la version del ensamblado
        /// </summary>
        public string version { get { return this._version; } set { this._version = value; } }
    }
}
