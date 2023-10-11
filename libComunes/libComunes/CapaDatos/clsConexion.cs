using System;
using System.Data.SqlClient;
using System.Data.OleDb;
using System.Data.Odbc;
using System.Data;

namespace libComunes.CapaDatos
{
    public class clsConexion
    {
        #region "Constructor"
        public clsConexion()
        {
            //Creamos el constructor de la clase conexión, donde creamos
            //las instancias de los objetos
            objConexionDB = new SqlConnection();
            oCommand = new SqlCommand();
            objAdapter = new SqlDataAdapter();
            objDataSet = new DataSet();
            oParametro = new SqlParameter();
            strSQL = "";
            strNombreTabla = "TablaGenerica";
        }
        #endregion
        #region "Destructor"
        //El destructor se invoca cuando se destruye el objeto
        ~clsConexion()
        {
            //Elimina el dataset
            if (objDataSet != null)
            {
                //Elimina el dataset
                objDataSet = null;
            }
            //Cuando destruyen el objeto invoca el mètodo de cerrar conexion
            CerrarConexion();
        }
        #endregion
        #region "Atributos"
        private SqlConnection objConexionDB;
        private SqlDataReader objReader;
        private SqlDataAdapter objAdapter;
        private SqlParameter oParametro;
        private string strNombreTabla;
        private DataSet objDataSet;
        private string strCadenaConexion;
        private string strSQL;
        private string strError;
        #endregion
        #region "Propiedades"
        public bool StoredProcedure { get; set; }
        public SqlCommand oCommand { get; set; }
        public DataSet DATASET
        {
            get { return objDataSet; }
        }
        public SqlDataReader Reader
        {
            get
            {
                return objReader;
            }
        }
        public string NombreTabla
        {
            get { return strNombreTabla; }
            set { strNombreTabla = value; }
        }
        public string SQL
        {
            get
            {
                return strSQL;
            }
            set
            {
                strSQL = value;
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
        public bool EjecutarSentencia()
        {
            //Validamos si el SQL existe
            if (strSQL == "")
            {
                //No definieron el sql, se debe abortar el proceso
                strError = "No definió la instrucción SQL";
                return false;
            }

            //Ejecuta las instrucciones sql de insercion, actualización y borrado a la base de 
            //datos, es decir, aquellas que no retornan datos
            //El primer paso es abrir la base de datos
            if (AbrirConexion())
            {
                //Le asigna al comando las propiedades
                try
                {
                    if (StoredProcedure)
                    {
                        oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    }
                    else
                    {
                        oCommand.CommandType = System.Data.CommandType.Text;
                    }
                    oCommand.Connection = objConexionDB;
                    oCommand.CommandText = strSQL;

                    //Ejecuta la sentencia, cierra la conexión y retorna verdadero
                    oCommand.ExecuteNonQuery();
                    CerrarConexion();
                    return true;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool Consultar()
        {
            //Validamos si el SQL existe
            if (strSQL == "")
            {
                //No definieron el sql, se debe abortar el proceso
                strError = "No definió la instrucción SQL";
                return false;
            }
            //Ejecuta las instrucciones sql de insercion, actualización y borrado a la base de 
            //datos, es decir, aquellas que no retornan datos
            //El primer paso es abrir la base de datos
            if (AbrirConexion())
            {
                //Le asigna al comando las propiedades
                try
                {
                    if (StoredProcedure)
                    {
                        oCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    }
                    else
                    {
                        oCommand.CommandType = System.Data.CommandType.Text;
                    }
                    oCommand.Connection = objConexionDB;
                    oCommand.CommandText = strSQL;

                    //Ejecuta la sentencia, almacena los datos en un reader, 
                    //cierra la conexión y retorna verdadero
                    objReader = oCommand.ExecuteReader();
                    return true;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        private bool GenerarCadenaConexion()
        {
            //Creamos la instancia del objeto parametros
            clsParametros objParametros = new clsParametros();

            if (objParametros.GenerarCadena())
            {
                strCadenaConexion = objParametros.CadenaConexion;
                objParametros = null;
                return true;
            }
            else
            {
                strError = objParametros.Error;
                objParametros = null;
                return false;
            }
        }
        private bool AbrirConexion()
        {
            //Validamos si la conexión está abierta
            if (objConexionDB.State == System.Data.ConnectionState.Open)
            {
                return true;
            }
            //Abre la conexión a la base de datos
            //Crea la cadena de conexión
            if (GenerarCadenaConexion())
            {
                //Asigno la cadena de conexión y abro la conexión a la base de datos
                objConexionDB.ConnectionString = strCadenaConexion;
                try
                {
                    objConexionDB.Open();
                    return true;
                }
                catch (Exception ex)
                {
                    //Captura si hubo un error al tratar de abrir la base de datos
                    strError = ex.Message;
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public void CerrarConexion()
        {
            try
            {
                //Cierra la conexión si està abierta y si existe
                if (objConexionDB != null)
                {
                    //La conexión existe, reviso que no estè cerrada
                    if (objConexionDB.State != System.Data.ConnectionState.Closed)
                    {
                        objConexionDB.Close();
                    }
                    //Libero memoria
                    objConexionDB = null;
                }

                //Elimina el command
                if (oCommand != null)
                {
                    oCommand = null;
                }

                //Libera el dataadapter
                if (objAdapter != null)
                {
                    objAdapter = null;
                }

                return;
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return;
            }
        }
        public bool LlenarDataSet()
        {
            //Primero validamos que exista el SQL
            if (strSQL == "")
            {
                strError = "No definió el SQL";
                return false;
            }
            //Se asegura que la tabla tenga datos
            if (strNombreTabla == "")
            {
                strNombreTabla = "TablaGenerica";
            }
            //Abre la conexión a la base de datos
            if (AbrirConexion())
            {
                try
                {
                    //Define los paràmetros del objeto command
                    oCommand.Connection = objConexionDB;
                    oCommand.CommandText = strSQL;
                    if (StoredProcedure)
                    {
                        oCommand.CommandType = CommandType.StoredProcedure;
                    }
                    else
                    {
                        oCommand.CommandType = CommandType.Text;
                    }

                    //Defino las propiedades del objeto adapter y lleno el dataset
                    objAdapter.SelectCommand = oCommand;
                    //Con el método fill se llena el dataset
                    objAdapter.Fill(objDataSet, strNombreTabla);

                    //Cierro la conexión
                    CerrarConexion();
                    return true;
                }
                catch (Exception ex)
                {
                    strError = ex.Message;
                    CerrarConexion();
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        public bool AgregarParametro(string sNombreParametro, SqlDbType TipoDato, Int32 Tamano, object Valor)
        {
            try
            {
                oParametro.ParameterName = sNombreParametro;
                oParametro.SqlDbType = TipoDato;
                oParametro.Value = Valor;
                oParametro.Size = Tamano;
                oCommand.Parameters.Add(oParametro);
                oParametro = new SqlParameter();
                return (true);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return (false);
            }
        }
        public bool AgregarParametro(string NombreParametro, object Valor)
        {
            try
            {
                oParametro.ParameterName = NombreParametro;
                oParametro.Value = Valor;
                oCommand.Parameters.Add(oParametro);
                oParametro = new SqlParameter();
                return (true);
            }
            catch (Exception ex)
            {
                strError = ex.Message;
                return (false);
            }
        }
    #endregion
    }
}
