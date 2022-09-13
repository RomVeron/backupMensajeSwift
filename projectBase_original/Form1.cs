using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using Oracle.DataAccess;
using System.Xml;
using System.IO;
using System.Xml.Serialization;

namespace f2
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// la capa de negocios
        /// </summary>
        private businessLayer capaNegocios = new businessLayer();
        private XmlDocument xDoc = new XmlDocument();
        private OpenFileDialog op = new OpenFileDialog();
        //private DirectoryInfo dir;
        private DialogResult result = new DialogResult();
        private FolderBrowserDialog folder = new FolderBrowserDialog();
        private FileInfo[] files;
        private DirectoryInfo[] dires;


        //private DirectoryInfo[] dires = ne
        
        /// <summary>
        /// constructor del formulario
        /// </summary>
        public Form1()
        {
            InitializeComponent();
            //seteos standart de clase
            iniciosVarios();
            //establecer la version del archivo
            f2.objFinanasys2 laSesion = new objFinanasys2();
            laSesion.version = "AAAAABBBBCCCCDDD00009";
            this.Tag = (Object)laSesion;
        }         

        /// <summary>
        /// ocurre al cargar el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        /// <summary>
        /// Ocurre antes de cerrar el formulario
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sePuedeCerrar)
            {
                this.DialogResult = MessageBox.Show("Cerrar "+this.Text.Trim()+" ?",
                    this.nombreAplicacion,
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);
                if (this.DialogResult != DialogResult.Yes)
                    e.Cancel = true;
            }
            else { e.Cancel = true; }            
        }

        /// <summary>
        /// Ocurre cuando el formulario de muestra
        /// por primera vez
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Shown(object sender, EventArgs e)
        {
            esProduccion = false;
            if (esProduccion)
            {
                //recupero los parametros recibidos desde finansys            
                f2.objFinanasys2 laSesion = (f2.objFinanasys2)this.Tag;
                usuarioOracle = laSesion.usuario;
                passOracle = laSesion.userPassword;
                parametros = laSesion.parametros;
            }
            else
            {
                //aqui podemos colocar los datos para trabajar en un sandBox
                usuarioOracle = "RSA";
                passOracle = "12345678";
                parametros = "";
            }
            
            //aqui se estable la capa de negocios
            capaNegocios = new businessLayer(usuarioOracle, passOracle, "DATOS_148");

        }

        #region SETEOS DE BASE          
        /// <summary>
        /// estable si el form esta en produccion
        /// </summary>
        private bool esProduccion = false;
        /// <summary>
        /// el usuario de oracle que viene de finansys
        /// </summary>
        private string usuarioOracle = "";
        /// <summary>
        /// el password de oracle
        /// </summary>
        private string passOracle = "";
        /// <summary>
        /// los parametro recibidos desde finansys
        /// </summary>
        private string parametros = "";
        /// <summary>
        /// determina si se puede cerrar el formulario
        /// </summary>
        private bool sePuedeCerrar = false;
        /// <summary>
        /// el nombre de la aplicacion
        /// </summary>
        private string nombreAplicacion = "Finansys 2";
        /// <summary>
        /// seteos de formularios
        /// </summary>
        private void iniciosVarios()
        {
            //Inicializaciones de base
            this.Icon = projectBase.Properties.Resources.pyg;
            this.Text = "Finansys 2";
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
        }
        #endregion        

        private void btnCaptura_Click(object sender, EventArgs e)
        {
            //ejemplo2();
            if (chReemplazo.Checked && chReubicar.Checked)
            {
                MessageBox.Show("Solo puede seleccionar una opcion o ninguna!");
            }
            else
            {
                if (chReemplazo.Checked)
                {
                    //OpenFileDialog op = new OpenFileDialog();
                    op.Multiselect = true;
                    op.Filter = "Archivo|*.*";
                    //op.InitialDirectory = Application.StartupPath;
                    op.InitialDirectory = @"C:\Users\ronaldo.arroyo.BANCONTINENTAL\Desktop\pedido en proceso\0a lectura de xml intl\MT_01.xml";
                    //XmlDocument xDoc = new XmlDocument();
                    if (op.ShowDialog(this) != DialogResult.OK)
                    {
                        MessageBox.Show("DEBE SELECCIONAR UN ARCHIVO XML", "Fin@nsys2");
                    }
                    else
                    {
                        CheckForIllegalCrossThreadCalls = false;
                        if (!chReemplazo.Checked)
                            backgroundWorker1.RunWorkerAsync();
                        else
                            backgroundWorker2.RunWorkerAsync();
                    }
                }
                else 
                {
                    //result = folder.ShowDialog();
                    if (folder.ShowDialog(this) != DialogResult.OK)
                    {
                        MessageBox.Show("DEBE SELECCIONAR UN DIRECTORIO", "Fin@nsys2");
                    }
                    else
                    {
                        CheckForIllegalCrossThreadCalls = false;
                        backgroundWorker3.RunWorkerAsync();
                    }
                }
            }

            #region coment
            //while (reader.Read())
            //{
            //    switch (reader.NodeType)
            //    {
            //        case XmlNodeType.Element: // The node is an element.
            //            Console.Write("<" + reader.Name);
            //            while (reader.MoveToNextAttribute()) // Read the attributes.
            //                Console.Write(" " + reader.Name + "='" + reader.Value + "'");
            //            Console.WriteLine(">");
            //            mensaje = mensaje + "<" + reader.Name + " " + reader.Name + "='" + reader.Value + "'" + ">";
            //            break;
            //        case XmlNodeType.Text: //Display the text in each element.
            //            Console.WriteLine(reader.Value);
            //            mensaje = mensaje + reader.Value;
            //            break;
            //        case XmlNodeType.EndElement: //Display the end of the element.
            //            Console.Write("</" + reader.Name);
            //            Console.WriteLine(">");
            //            mensaje = mensaje + "</" + reader.Name + ">";
            //            break;
            //    }
            //    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Message")
            //    {
            //        getMessage(mensaje);
            //        mensaje = "";
            //    }
            //}
            #endregion
        }

        private void getMessage(string message)
        {

            XmlWriter writer = null;
            XmlWriterSettings settings = null;
            string nombreArchivo = "";
            try
            {
                // Create an XmlWriterSettings object with the correct options. 
                settings = new XmlWriterSettings();
                settings.Indent = true;
                settings.IndentChars = ("\t");
                settings.OmitXmlDeclaration = true;
                settings.Encoding = Encoding.UTF8;
                settings.CheckCharacters = true;
                settings.ConformanceLevel = ConformanceLevel.Auto;

                // Create the XmlWriter object and write some content.
                nombreArchivo = DateTime.Now.Ticks.ToString()+".xml";
                writer = XmlWriter.Create(nombreArchivo, settings);
                writer.WriteStartElement("xml");
                writer.WriteAttributeString("version", "1.0");
                //writer.WriteElementString("item", "tesing");
                writer.WriteString(message);
                writer.WriteEndElement();
                writer.Flush();
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }

            EditorialResponse(@"C:\Users\ronaldo.arroyo.BANCONTINENTAL\Desktop\INTL\0a lectura de xml intl\projectBase_original\bin\Debug\" + nombreArchivo, "&gt;", ">", "&lt;", "<", @"C:\Users\ronaldo.arroyo.BANCONTINENTAL\Desktop\INTL\0a lectura de xml intl\2004-2010\2005\c\" + nombreArchivo);
        
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            long cantFiles = op.FileNames.LongLength;
            for (int f = 0; f < cantFiles; f++)
            {

                xDoc.Load(op.FileNames[f]);
                XmlNodeList xMensaje = xDoc.GetElementsByTagName("Message"); //GetElementsByTagName("Message");
                writeMessage("Procesando mensajes: " + xMensaje.Count);
                continue;
                XmlDocument doc = new XmlDocument(xDoc.NameTable);
                XmlTextReader reader = new XmlTextReader(op.FileNames[f]);

                XmlNodeList tipoMensaje = xDoc.GetElementsByTagName("Type");

                #region progressBar
                //dt = capaNegocios.Operaciones(nroOperacion);

                writeMessage("Procesando mensajes: " + xMensaje.Count);
                int cantRegistros = xMensaje.Count;//dt.Rows.Count;
                int i = 0;
                //lblProcesados.Text = i.ToString().PadLeft(5, '0') + "/" + cantRegistros.ToString().PadLeft(5, '0');

                // Display the ProgressBar control.
                progressBar1.Visible = true;
                // Set Minimum to 1 to represent the first file being copied.
                if (cantRegistros > 1) progressBar1.Minimum = 1;
                else progressBar1.Minimum = 0;
                // Set Maximum to the total number of files to copy.
                if (cantRegistros > 1) progressBar1.Maximum = cantRegistros - 1;
                else progressBar1.Maximum = cantRegistros;
                // Set the initial value of the ProgressBar.
                if (cantRegistros > 1) progressBar1.Value = 1;
                else progressBar1.Value = 0;
                // Set the Step property to a value of 1 to represent each file being copied.
                progressBar1.Step = 1;
                #endregion

                string mensaje = "";
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element: // The node is an element.
                            if (reader.IsEmptyElement)
                                mensaje = mensaje + "<" + reader.Name + "/>\r\n";
                            else
                                if (reader.Name != "Messages")
                                    if (reader.Name == "Message")
                                        mensaje = mensaje + "<" + reader.Name + " xmlns=\"urn:swift:saa:xsd:messaging\" xmlns:SwSec=\"urn:swift:saa:xsd:messaging\" xmlns:Sw=\"urn:swift:saa:xsd:messaging\" xmlns:SwInt=\"urn:swift:saa:xsd:messaging\"" + ">\r\n";
                                    else
                                        mensaje = mensaje + "<" + reader.Name + ">\r\n";
                            break;
                        case XmlNodeType.Text: //Display the text in each element.
                            mensaje = mensaje + reader.Value + "\r\n";
                            break;
                        case XmlNodeType.EndElement: //Display the end of the element.
                            if (reader.Name != "Messages")
                                mensaje = mensaje + "</" + reader.Name + ">\r\n";
                            break;
                    }
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Message")
                    {
                        //doc.LoadXml(mensaje);
                        getMessage(mensaje);
                        //writeMessage(mensaje);
                        i++;
                        //writeMessage("Mensajes procesados: " + i + "/" + xMensaje.Count + ". Archivo: " + f + "/" + cantFiles);
                        progressBar1.PerformStep();
                        System.Threading.Thread.Sleep(1);
                        mensaje = "";
                    }
                }
            }
        }

        private void writeMessage(string mensaje)
        {
            //textBox1.Text += textBox1.Text + mensaje + "\r\n";
            //Verificamos el tama�o del monitor de swift            
            if (textBox1.Text.Length > 10000 && mensaje.Contains("--------------"))
            {
                //Vaciamos el monitor de citi
                textBox1.Clear();
            }
            //Mostramos el mensaje en la pantalla
            if (mensaje.Contains("--------------"))
                textBox1.AppendText("\r\n" + mensaje);
            else
                textBox1.AppendText("\r\n" + DateTime.Parse(DateTime.Now.ToString()) + " " + mensaje);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            writeMessage("Proceso completado!");
        }

        /// <summary>
        /// proceso para reemplazar texto
        /// </summary>
        /// <param name="fileName">Ruta del archivo a modificar</param>
        /// <param name="word">Texto para modificar</param>
        /// <param name="replacement">Texto para reemplazar</param>
        /// <param name="saveFileName">Ruta del archivo para guardar</param>
        public void EditorialResponse(string fileName, string word1, string replacement1, string word2, string replacement2, string saveFileName)
        {
            //StreamReader reader = new StreamReader(directory + fileName);
            StreamReader reader = new StreamReader(fileName);
            string input = reader.ReadToEnd();

            using (StreamWriter writer = new StreamWriter(saveFileName, true))
            {
                {
                    string output = input.Replace(word1, replacement1);
                    output = output.Replace(word2, replacement2);
                    writer.Write(output);
                }
                writer.Close();
                writer.Dispose();
            }
            reader.Close();
            reader.Dispose();

            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }

        }


        private void ejemplo()
        {
            XmlTextReader reader = null;

            try
            {

                // Create the string containing the XML to read.
                String xmlFrag = "<book>" +
                               "<title>Pride And Prejudice</title>" +
                               "<author>" +
                               "<first-name>Jane</first-name>" +
                               "<last-name>Austen</last-name>" +
                               "</author>" +
                               "<curr:price>19.95</curr:price>" +
                               "<misc>&h;</misc>" +
                               "</book>";

                // Create an XmlNamespaceManager to resolve namespaces.
                NameTable nt = new NameTable();
                XmlNamespaceManager nsmgr = new XmlNamespaceManager(nt);
                nsmgr.AddNamespace(String.Empty, "urn:samples"); //default namespace
                nsmgr.AddNamespace("curr", "urn:samples:dollar");

                // Create an XmlParserContext.  The XmlParserContext contains all the information
                // required to parse the XML fragment, including the entity information and the
                // XmlNamespaceManager to use for namespace resolution.
                XmlParserContext context;
                String subset = "<!ENTITY h 'hardcover'>";
                context = new XmlParserContext(nt, nsmgr, "book", null, null, subset, null, null, XmlSpace.None);

                // Create the reader.
                reader = new XmlTextReader(xmlFrag, XmlNodeType.Element, context);


                // Parse the file and display the node values.
                while (reader.Read())
                {
                    if (reader.HasValue)
                        Console.WriteLine("{0} [{1}] = {2}", reader.NodeType, reader.Name, reader.Value);
                    else
                        Console.WriteLine("{0} [{1}]", reader.NodeType, reader.Name);
                }
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private void ejemplo2() {
            XmlTextReader reader = null;

            try
            {

                // Load the reader with the data file and ignore all white space nodes.
                reader = new XmlTextReader(@"C:\Users\ronaldo.arroyo.BANCONTINENTAL\Desktop\INTL\BCNAPYPA messages extraction\2016\BCNA_2016.xml");
                reader.WhitespaceHandling = WhitespaceHandling.None;

                // Parse the file and display each of the nodes.
                while (reader.Read())
                {
                    switch (reader.NodeType)
                    {
                        case XmlNodeType.Element:
                            Console.Write("<{0}>", reader.Name);
                            break;
                        case XmlNodeType.Text:
                            Console.Write(reader.Value);
                            break;
                        case XmlNodeType.CDATA:
                            Console.Write("<![CDATA[{0}]]>", reader.Value);
                            break;
                        case XmlNodeType.ProcessingInstruction:
                            Console.Write("<?{0} {1}?>", reader.Name, reader.Value);
                            break;
                        case XmlNodeType.Comment:
                            Console.Write("<!--{0}-->", reader.Value);
                            break;
                        case XmlNodeType.XmlDeclaration:
                            Console.Write("<?xml version='1.0'?>");
                            break;
                        case XmlNodeType.Document:
                            break;
                        case XmlNodeType.DocumentType:
                            Console.Write("<!DOCTYPE {0} [{1}]", reader.Name, reader.Value);
                            break;
                        case XmlNodeType.EntityReference:
                            Console.Write(reader.Name);
                            break;
                        case XmlNodeType.EndElement:
                            Console.Write("</{0}>", reader.Name);
                            break;
                    }
                }
            }

            finally
            {
                if (reader != null)
                    reader.Close();
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            long cantFiles = op.FileNames.LongLength;
            for (int f = 0; f < cantFiles; f++)
            {
                EditorialResponse(op.FileNames[f], "&gt;", ">", "&lt;", "<", "xmlns=\"urn:swift:saa:xsd:messaging\"", "xmlns=\"urn:swift:saa:xsd:messaging\" xmlns:SwSec=\"urn:swift:saa:xsd:messaging\" xmlns:Sw=\"urn:swift:saa:xsd:messaging\" xmlns:SwInt=\"urn:swift:saa:xsd:messaging\"", "&#xD;", "", @"C:\Users\ronaldo.arroyo.BANCONTINENTAL\Desktop\INTL\0a lectura de xml intl\2004-2010\2005\2005_" + f + ".xml");
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            writeMessage("Reemplazo completado!");
        }



        public void EditorialResponse(string fileName, 
                                      string word1, string replacement1,
                                      string word2, string replacement2,
                                      string word3, string replacement3,
                                      string word4, string replacement4,
                                      string saveFileName)
        {
            //StreamReader reader = new StreamReader(directory + fileName);
            StreamReader reader = new StreamReader(fileName);
            string input = reader.ReadToEnd();

            using (StreamWriter writer = new StreamWriter(saveFileName, true))
            {
                {
                    string output = input.Replace(word1, replacement1);
                    output = output.Replace(word2, replacement2);
                    output = output.Replace(word3, replacement3);
                    output = output.Replace(word4, replacement4);
                    writer.Write(output);
                }
                writer.Close();
                writer.Dispose();
            }
            reader.Close();
            reader.Dispose();

        }

        private void backgroundWorker3_DoWork(object sender, DoWorkEventArgs e)
        {
            /* 
             * <MessageIdentifier> fin.103 </MessageIdentifier>
             * <SenderSwiftAddress> BCNAPYPAAXXX </SenderSwiftAddress>
             * <ReceiverSwiftAddress> MRMDUS33XXXX </ReceiverSwiftAddress>
             * <TransactionReference> TPO 17571212 </TransactionReference>
             */
            DirectoryInfo dir = new DirectoryInfo(folder.SelectedPath.ToString());
            dires = dir.GetDirectories();

            if (dires.Length > 0)
            {
                //giramos por los directorios
                for (int i = 0; i < dires.Length; i++)
                {
                    //giramos por los archivos del directorio seleccionado
                    files = dires[i].GetFiles("*.xml");
                    moverTodo(files);
                    writeMessage(dires[i].Name + " terminado!");
                }
            }
            else
            {
                //si no hay directorios internos verificamos lo xml de la carpeta seleccionada
                files = dir.GetFiles("*.xml");
                moverTodo(files);
                writeMessage(dir.Name + " terminado!");
            }

        }

        private string reemplazoNombreCaracEsp(string texto)
        {
            string aux = texto.Replace("\r","");
            aux = aux.Replace("\n","");
            aux = aux.Replace(@"\", "-");
            aux = aux.Replace('/', '-');
            aux = aux.Replace(':', '-');
            aux = aux.Replace('*', '-');
            aux = aux.Replace('?', '-');
            aux = aux.Replace('"', '-');
            aux = aux.Replace('<', '-');
            aux = aux.Replace('>', '-');
            aux = aux.Replace('|', '-');
            return aux.Trim();
        }

        private string reemplazoNombreCaracEsp_v2(string texto)
        {
            string aux = texto.Replace("\r", "");
            aux = aux.Replace("\n", "");
            return aux.Trim();
        }

        private void moverTodo(FileInfo[] files)
        {
            for (int f = 0; f < files.Length; f++)
            {
                xDoc.Load(files[f].FullName);
                string _messageIdentifier = xDoc.GetElementsByTagName("MessageIdentifier").Item(0).InnerText;
                string _senderSwiftAddress = xDoc.GetElementsByTagName("SenderSwiftAddress").Item(0).InnerText;
                string _receiverSwiftAddress = xDoc.GetElementsByTagName("ReceiverSwiftAddress").Item(0).InnerText;
                string _transactionReference;
                try { _transactionReference = xDoc.GetElementsByTagName("TransactionReference").Item(0).InnerText; }
                catch { _transactionReference = ""; }

                string _relatedTransactionReference;
                try { _relatedTransactionReference = "(" + xDoc.GetElementsByTagName("RelatedTransactionReference").Item(0).InnerText + ")"; }
                catch { _relatedTransactionReference = ""; }

                DateTime dateCreation = DateTime.Parse(reemplazoNombreCaracEsp_v2(xDoc.GetElementsByTagName("CreationDate").Item(0).InnerText).Substring(0,10));

                

                _messageIdentifier = reemplazoNombreCaracEsp(_messageIdentifier);
                _senderSwiftAddress = reemplazoNombreCaracEsp(_senderSwiftAddress);
                _receiverSwiftAddress = reemplazoNombreCaracEsp(_receiverSwiftAddress);
                _transactionReference = reemplazoNombreCaracEsp(_transactionReference);
                _relatedTransactionReference = reemplazoNombreCaracEsp(_relatedTransactionReference);

                string _enviadoRecibido = "";

                if (_senderSwiftAddress.Contains("BCNAPYPA"))
                    _enviadoRecibido = "ENVIADOS";
                else
                    _enviadoRecibido = "RECIBIDOS";

                DirectoryInfo dirAux = new DirectoryInfo(files[f].DirectoryName + @"\" + dateCreation.Month + @"\" + dateCreation.Day + @"\" + _enviadoRecibido + @"\" + _messageIdentifier);
                if (!dirAux.Exists)
                    dirAux.Create();


                string sourceFile = files[f].FullName;
                string destinationFile = dirAux.FullName + @"\" + dateCreation.Ticks + "_" + _transactionReference + _relatedTransactionReference + "_" + DateTime.Now.Ticks + ".xml";

                // To move a file or folder to a new location:
                System.IO.File.Move(sourceFile, destinationFile);
                System.Threading.Thread.Sleep(1);
            }
        }

        private void backgroundWorker3_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            writeMessage("Proceso completado!");
        }

    }
}