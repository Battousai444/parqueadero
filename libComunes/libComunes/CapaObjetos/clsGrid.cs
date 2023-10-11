using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using libComunes.CapaDatos;

namespace libComunes.CapaObjetos
{
    public class clsGrid
    {
        #region "Constructor"
        public clsGrid()
        {
            NombreTabla = "Tabla_Grid";
            oParametro = new SqlParameter();
            oCommand = new SqlCommand();
        }
        ~clsGrid()
        {
            if (oParametro != null) oParametro = null;
            if (oCommand != null) oCommand = null;
        }
        #endregion

        #region "Atributos"
        private SqlCommand oCommand;
        private SqlParameter oParametro;
        #endregion

        #region "Propiedades"
        public GridView grdGenerico { get; set; }
        public string NombreTabla { get; set; }
        public string SQL { get; set; }
        public string Error { get; private set; }
        #endregion

        #region "Metodos Publicos"
        public bool LlenarGridWeb()
        {
            if (grdGenerico == null)
            {
                Error = "No ha definido el grid que se va a llenar";
                return false;
            }
            if (string.IsNullOrEmpty(SQL))
            {
                Error = "Debe definir una instrucción SQL";
                return false;
            }

            clsConexion objConexionBd = new clsConexion();
            if (string.IsNullOrEmpty(NombreTabla))
            {
                NombreTabla = "Tabla";
            }
            objConexionBd.NombreTabla = NombreTabla;
            objConexionBd.SQL = SQL;
            objConexionBd.oCommand = oCommand;

            if (objConexionBd.LlenarDataSet())
            {
                grdGenerico.DataSource = objConexionBd.DATASET.Tables[NombreTabla];
                grdGenerico.DataBind();
                objConexionBd.CerrarConexion();
                objConexionBd = null;
                return true;
            }
            else
            {
                Error = objConexionBd.Error;
                objConexionBd.CerrarConexion();
                objConexionBd = null;
                return false;
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
                Error = ex.Message;
                return (false);
            }
        }
        #endregion
    }
}

