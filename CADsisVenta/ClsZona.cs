using CADsisVenta.DataSetZonasTableAdapters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DomainSQLite.Setting;

namespace CADsisVenta
{
    public class ClsZona
    {
        private static PaisTableAdapter Pais_TableAdapter = new PaisTableAdapter();
        private static ProvinciaTableAdapter Provincia_TableAdapter = new ProvinciaTableAdapter();
        private static CantonTableAdapter Canton_TableAdapter = new CantonTableAdapter();
        private static ParroquiaTableAdapter Parroquia_TableAdapter = new ParroquiaTableAdapter();
        private static SectorTableAdapter Sector_TableAdapter = new SectorTableAdapter();
        private static ParroquiaByidSectorTableAdapter ParroByIdSector = new ParroquiaByidSectorTableAdapter();
        public static CADsisVenta.DataSetZonas.ProvinciaDataTable getProvincia(int idPais)
        {
            Provincia_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Provincia_TableAdapter.GetDataSelect(idPais);
        }
        public static CADsisVenta.DataSetZonas.ProvinciaDataTable getProvinciaSelect(int idPais)
        {
            Provincia_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Provincia_TableAdapter.GetDataSelect(idPais);
        }
        public static CADsisVenta.DataSetZonas.CantonDataTable getCanton(int idProvincia)
        {
            Canton_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);


            return Canton_TableAdapter.GetData(idProvincia);
        }
        public static CADsisVenta.DataSetZonas.ParroquiaDataTable getParroquia(int idCanton)
        {
            Parroquia_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Parroquia_TableAdapter.GetData(idCanton);
        }
        public static CADsisVenta.DataSetZonas.SectorDataTable getSector(int idParroquia)
        {
            Sector_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Sector_TableAdapter.GetData(idParroquia);
        }
        public static int InsertProvincia(string Nom_Provincia, int idPais, decimal latitud, decimal longitud)
        {
            Provincia_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Provincia_TableAdapter.InsertProvincia(Nom_Provincia, idPais, latitud, longitud);
        }
        public static int InsertPais(string Nom_Provincia)
        {
            Pais_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Pais_TableAdapter.Insert(Nom_Provincia, 0, 0);
        }
        public static int UpdatePais(int idPais, string Nom_Provincia)
        {
            Pais_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Pais_TableAdapter.UpdatePais(Nom_Provincia, 0, 0, idPais);
        }
        public static int InsertSector(int idParroquia, string Nom_Sector, decimal latitud, decimal longitud)
        {
            Sector_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Sector_TableAdapter.InsertSector(idParroquia, Nom_Sector, latitud, longitud);
        }
        public static int UpdateProvincia(int id_Origival_idProvincia, string Nom_Provincia, decimal latitud, decimal longitud)
        {
            Provincia_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Provincia_TableAdapter.UpdateProvincia(Nom_Provincia, latitud, longitud, id_Origival_idProvincia);
        }
        public static int DeleteProvincia(int id_Origival_idProvincia)
        {
            Provincia_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Provincia_TableAdapter.DeleteProvincia(id_Origival_idProvincia);
        }
        public static int DeletePais(int id_Origival_idPais)
        {
            Pais_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Pais_TableAdapter.DeletePais(id_Origival_idPais);
        }
        public static int DeleteSector(int id_OriginalSector)
        {
            Sector_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Sector_TableAdapter.DeleteSector(id_OriginalSector);
        }
        public static int DeleteParroquia(int id_OriginalParroquia)
        {
            Parroquia_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Parroquia_TableAdapter.DeleteParroquia(id_OriginalParroquia);
        }
        public static int InsertCanton(int idProvincia, string Nom_canton, decimal latitud, decimal longitud)
        {
            Canton_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Canton_TableAdapter.InsertCanton(idProvincia, Nom_canton, latitud, longitud);
        }
        public static int InsertParroquia(int idCanton, string Nom_Parroquia, decimal latitud, decimal longitud)
        {
            Parroquia_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Parroquia_TableAdapter.InsertParroquia(idCanton, Nom_Parroquia, latitud, longitud);
        }
        public static int UpdateCanton(int orig_idCanton, int idProvincia, string Nom_canton, decimal latitud, decimal longitud)
        {
            Canton_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Canton_TableAdapter.UpdateCanton(idProvincia, Nom_canton, latitud, longitud, orig_idCanton);
        }
        public static int UpdateParroquia(int IDoriginal_Parroquia, int idCanton, string Nom_Parroquia, decimal latitud, decimal longitud)
        {
            Parroquia_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Parroquia_TableAdapter.UpdateParroquia(Nom_Parroquia, latitud, longitud, IDoriginal_Parroquia);
        }
        public static int UpdateSector(int IDoriginal_Sector, int idParroquia, string Nom_Sector, decimal latitud, decimal longitud)
        {
            Sector_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Sector_TableAdapter.UpdateSector(idParroquia, Nom_Sector, latitud, longitud, IDoriginal_Sector);
        }
        public static int DeleteCanton(int orig_idCanton)
        {
            Canton_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Canton_TableAdapter.DeleteCanton(orig_idCanton);
        }
        //para incluir 0
        public static CADsisVenta.DataSetZonas.ProvinciaDataTable getProvinciaAll(int idPais)
        {
            Provincia_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Provincia_TableAdapter.GetDataAll(idPais);
        }
        public static CADsisVenta.DataSetZonas.CantonDataTable getCantonAll(int idProvincia)
        {
            Canton_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Canton_TableAdapter.GetDataAll(idProvincia);
        }
        public static CADsisVenta.DataSetZonas.ParroquiaDataTable getParroquiaAll(int idCanton)
        {
            Parroquia_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Parroquia_TableAdapter.GetDataAll(idCanton);
        }
        public static CADsisVenta.DataSetZonas.SectorDataTable getSectorAll(int idParroquia)
        {
            Sector_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Sector_TableAdapter.GetDataAll(idParroquia);
        }
        public static CADsisVenta.DataSetZonas.PaisDataTable getPaisAll()
        {
            Pais_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return Pais_TableAdapter.GetDataAll();
        }
        // para rellenar
        public static CADsisVenta.DataSetZonas.ParroquiaByidSectorDataTable getParroquiaByidSector(int idSector)
        {
            ParroByIdSector.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return ParroByIdSector.GetDataParroquiaByIdSector(idSector);
        }
        public static int getIdParroquiaByIdSector(int idSector)
        {
            ParroByIdSector.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return (int)ParroByIdSector.ScalarIdParroquiaByIdSector(idSector);
        }
        public static int getIdCantonByIdParroquia(int idParroquia)
        {
            ParroByIdSector.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return (int)ParroByIdSector.ScalarIdCantonByIdParroquia(idParroquia);
        }
        public static int getIdProvinByIdCanton(int idCanton)
        {
            ParroByIdSector.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return (int)ParroByIdSector.ScalarIdProvinciaByIdCanton(idCanton);
        }
        public static int getIdPaisByIdProvin(int idProvin)
        {
            ParroByIdSector.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            return (int)ParroByIdSector.ScalarIdPaisByIdProvincia(idProvin);
        }
    }
}
