using System;
using System.Windows.Forms;
using System.Xml;

namespace libComunes.CapaDatos
{
    public class clsParametros
    {
        #region "Atributos"
            private string strArchivoXML;
            private string strServidor;
            private string strBaseDatos;
            private string strUsuario;
            private string strClave;
            private string strSeguridadIntegrada;
            private string strCadenaConexion;
            private string strError;
        #endregion

        #region "Propiedades"
            public string CadenaConexion
            {
                get
                {
                    return strCadenaConexion;
                }
            }

            public string Error
            {
                get
                {
                    return strError;
                }
            }
        #endregion

        #region "Metodos"
            public bool GenerarCadena()
            {
                //El primer paso es abrir el archivo de conexion
                //Capturamos el nombre de la aplicación, con base en la ruta donde se ejecuta el proyecto
                string strNombreAplicacion = AppDomain.CurrentDomain.BaseDirectory;
                //Se eliminan los dos  ùltimos caracteres
                strNombreAplicacion = strNombreAplicacion.Substring(1, strNombreAplicacion.Length - 2);
                //Se captura el nombre borrándole el directorio de origen de la ruta
                strNombreAplicacion = strNombreAplicacion.Substring(strNombreAplicacion.LastIndexOf('\\') 
                    + 1, strNombreAplicacion.Length - (strNombreAplicacion.LastIndexOf('\\') + 1));

                //Defin el nombre del archivo, la ruta donde se ubica es donde ejecuta el proyecto
                //se le agrega la palabra CON, para indicar que es el archivo con la conexion
                strArchivoXML = Application.StartupPath + "\\CON_" + strNombreAplicacion + ".xml";
                //strArchivoXML = @"C:\RutasXML\CON_" + strNombreAplicacion + ".xml";
                //Abrimos el archivo xml
                try
                {
                    //Declaramos el objeto documento
                    XmlDocument objDocumento = new XmlDocument();
                    //Abrimos el archivo
                    objDocumento.Load(strArchivoXML);

                    //Leemos la información
                    XmlNode objNodo;
                    //Leemos el servidor
                    objNodo = objDocumento.SelectSingleNode("//SERVIDOR");
                    strServidor = objNodo.InnerText;

                    //Leemos la base de datos
                    objNodo = objDocumento.SelectSingleNode("//BASE_DATOS");
                    strBaseDatos = objNodo.InnerText;

                    //Leemos el usuario
                    objNodo = objDocumento.SelectSingleNode("//USUARIO");
                    strUsuario = objNodo.InnerText;

                    //Leemos la clave
                    objNodo = objDocumento.SelectSingleNode("//CLAVE");
                    strClave = objNodo.InnerText;

                    //Leemos la seguridad integrada
                    objNodo = objDocumento.SelectSingleNode("//SEGURIDAD_INTEGRADA");
                    strSeguridadIntegrada = objNodo.InnerText;

                    //Con todos los datos generamos la cadena de conexion
                    //Hay dos cadenas de conexion, una cuando la seguridad es integrada y otra
                    //cuando la seguridad es mixta en el SQL Server, es decir, utilizo la seguridad de
                    //SQL Server
                    if (strSeguridadIntegrada.ToUpper() == "SI")
                    {
                        //La cadena de conexion es con seguridad integrada
                        strCadenaConexion = "Data Source=" + strServidor + "; Initial Catalog=" + strBaseDatos +
                                                       "; Integrated Security=SSPI;";
                    }
                    else
                    {
                        strCadenaConexion = "Data Source=" + strServidor + ";Initial Catalog=" + strBaseDatos +
                                                      ";User Id=" + strUsuario + ";Pwd=" + strClave + ";";
                    }

                    // Al terminar, se libera memoria de los objetos xml y node y retorna verdadero
                    objNodo = null;
                    objDocumento = null;
                    return true;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return false;
                }
            }
        #endregion
    }
}
