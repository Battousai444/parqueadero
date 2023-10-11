using System;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using libComunes.CapaDatos;

namespace libComunes.CapaObjetos
{
    public class clsCombos
    {
        public clsCombos()
        {
            oParametro = new SqlParameter();
            oCommand = new SqlCommand();
            NombreTabla = "tblCombo";
        }
        ~clsCombos()
        {
            if (oParametro != null) oParametro = null;
            if (oCommand != null) oCommand = null;
        }
        #region "Atributos"
        private string strError;
        private SqlParameter oParametro;
        //private ComboBox cboGenerico;
        #endregion
        #region "Propiedades"
        public DropDownList cboGenericoWeb { get; set; }
        public string NombreTabla { get; set; }
        public string SQL { private get; set; }
        public string ColumnaTexto { get; set; }
        public string ColumnaValor { get; set; }
        public bool StoredProcedure { get; set; }
        public string Error { get; private set; }
        private SqlCommand oCommand;
        #endregion
        #region "Metodos"
        public bool LlenarComboWeb()
        {
            if (Validar())
            {
                if (cboGenericoWeb == null)
                {
                    strError = "No definió el combo";
                    return false;
                }
                clsConexion objConexionBD = new clsConexion();

                objConexionBD.NombreTabla = NombreTabla;
                objConexionBD.StoredProcedure = StoredProcedure;
                objConexionBD.SQL = SQL;
                objConexionBD.oCommand = oCommand;
                if (objConexionBD.LlenarDataSet())
                {
                    cboGenericoWeb.DataSource = objConexionBD.DATASET.Tables[NombreTabla];
                    cboGenericoWeb.DataTextField = ColumnaTexto;
                    cboGenericoWeb.DataValueField = ColumnaValor;
                    //objcboGenericoWeb.DataTextField = objConexionBD.DATASET.Tables[strNombreTabla].Columns[1].ColumnName;
                    //objcboGenericoWeb.DataValueField = objConexionBD.DATASET.Tables[strNombreTabla].Columns[0].ColumnName;
                    cboGenericoWeb.DataBind();
                    objConexionBD.CerrarConexion();
                    objConexionBD = null;
                    return true;
                }
                else
                {
                    strError = objConexionBD.Error;

                    objConexionBD.CerrarConexion();
                    objConexionBD = null;
                    return false;
                }
            }
            else
            {
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
                strError = ex.Message;
                return (false);
            }
        }

        //public bool LlenarCombo()
        //{
        //    if (Validar())
        //    {

        //        clsConexion objConexionBD = new clsConexion();

        //        objConexionBD.NombreTabla = strNombreTabla;
        //        objConexionBD.SQL = strSQL;
        //        if (objConexionBD.LlenarDataSet())
        //        {
        //            cboGenerico.DataSource = objConexionBD.DATASET.Tables[strNombreTabla];
        //            cboGenerico.DisplayMember   = strColumnaTexto;
        //            cboGenerico.ValueMember  = strColumnaValor;

        //            objConexionBD.CerrarConexion();
        //            objConexionBD = null;
        //            return true;
        //        }
        //        else
        //        {
        //            strError = objConexionBD.Error;

        //            objConexionBD.CerrarConexion();
        //            objConexionBD = null;
        //            return false;
        //        }
        //    }
        //    else
        //    {
        //        return false;
        //    }
        //}
        #endregion
        #region "Metodos Privados"
        private bool Validar()
        {
            if (SQL == "")
            {
                strError = "No definio la instruccion SQL";
                return false;
            }
            if (string.IsNullOrEmpty(ColumnaTexto))
            {
                strError = "No definio el nombre de la columna para el texto del combobox";
                return false;
            }

            if (string.IsNullOrEmpty(ColumnaValor))
            {
                strError = "No definio el nombre de la columna para el valor del combobox";
                return false;
            }
            if (string.IsNullOrEmpty(NombreTabla))
            {
                NombreTabla = "Tabla";
            }
            return true;
        }
        #endregion
    }
}