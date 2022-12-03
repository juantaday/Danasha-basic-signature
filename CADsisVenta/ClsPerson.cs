using CADsisVenta.DataSetPersonTableAdapters;
using DomainSQLite.Setting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CADsisVenta
{
    public class ClsPerson
    {

        private static PersonasTableAdapter Person_TableAdapter = new PersonasTableAdapter();
        private static PersonaSectorTableAdapter PersonSector_TableAdapter = new PersonaSectorTableAdapter();
        private static PersonaBySectorTableAdapter personaBySector_TableAdapter = new PersonaBySectorTableAdapter();
        public static int InsertPerson(string apellidos, string nombre, string Ruc_Ci, 
            string Direccion, string telefono, string mail,
            global::System.Nullable<global::System.DateTime> fech_Naci,
            bool genero, string nota, byte[] foto, string telef_casa,
            string telef_ofic, bool SendMail)
        {


            using (var cmd = new CADsisVenta.Funtions.SqlComandExec ()) 
            {
               
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                object _date;
                if (fech_Naci != null)
                    _date = fech_Naci.Value;
                else
                    _date = DBNull.Value;

                object _foto;
                if (foto != null)
                    _foto = foto;
                else
                    _foto = DBNull.Value;

                cmd.ParameterCollection = new System.Data.SqlClient.SqlParameter[]
                        {
                        new System.Data.SqlClient.SqlParameter {
                             ParameterName ="@Apellidos",
                             SqlDbType = System.Data.SqlDbType.VarChar ,
                             Value =apellidos
                        },
                         new System.Data.SqlClient.SqlParameter {
                             ParameterName ="@Nombre",
                             SqlDbType = System.Data.SqlDbType.VarChar ,
                             Value =nombre
                        },
                         new System.Data.SqlClient.SqlParameter {
                             ParameterName ="@Ruc_Ci",
                             SqlDbType = System.Data.SqlDbType.VarChar ,
                             Value =Ruc_Ci
                        } ,
                         new System.Data.SqlClient.SqlParameter {
                             ParameterName ="@Direccion",
                             SqlDbType = System.Data.SqlDbType.VarChar ,
                             Value =  Direccion
                        },
                         new System.Data.SqlClient.SqlParameter {
                             ParameterName ="@telefono",
                             SqlDbType = System.Data.SqlDbType.VarChar ,
                             Value =  telefono
                        },
                         new System.Data.SqlClient.SqlParameter {
                             ParameterName ="@mail",
                             SqlDbType = System.Data.SqlDbType.VarChar ,
                             Value =  mail
                        },
                         new System.Data.SqlClient.SqlParameter {
                             ParameterName ="@fech_Naci",
                             SqlDbType = System.Data.SqlDbType.Date ,
                             Value =  _date
                        },
                         new System.Data.SqlClient.SqlParameter {
                             ParameterName ="@genero",
                             SqlDbType = System.Data.SqlDbType.Bit ,
                             Value =  genero
                        },
                         new System.Data.SqlClient.SqlParameter {
                             ParameterName ="@nota",
                             SqlDbType = System.Data.SqlDbType.VarChar ,
                             Value =  nota
                        },
                         new System.Data.SqlClient.SqlParameter {
                             ParameterName ="@foto",
                             SqlDbType = System.Data.SqlDbType.Binary ,
                             Value =  _foto
                        },
                         new System.Data.SqlClient.SqlParameter {
                             ParameterName ="@telef_casa",
                             SqlDbType = System.Data.SqlDbType.VarChar ,
                             Value =  telef_casa
                        },
                         new System.Data.SqlClient.SqlParameter {
                             ParameterName ="@telef_ofic",
                             SqlDbType = System.Data.SqlDbType.VarChar ,
                             Value =  telef_ofic
                        },new System.Data.SqlClient.SqlParameter {
                             ParameterName ="@sendEmail",
                             SqlDbType = System.Data.SqlDbType.Bit ,
                             Value =  SendMail
                        }
                        };


                var dt = cmd.RetornaTabla("[dbo].[InsertPerson]");
                if (dt != null && dt.Rows.Count == 1)
                {
                    var identity = dt.Rows[0][0];
                    int result = 0;
                    int.TryParse(identity.ToString(), out result);
                    return result;
                }
                else
                    return 0;

            }


        }
        public static int UpdatePerson(int idPerson_Original, string apellidos, string nombre, 
            string Ruc_Ci, string Direccion, string telefono, string mail, 
            global::System.Nullable<global::System.DateTime> fech_Naci, bool genero,
            string nota, byte[] foto, string telef_casa, string telef_ofic, bool senMail)
        {
            Person_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);


           var result = Person_TableAdapter.UpdatePerson(apellidos, nombre, Ruc_Ci, Direccion, telefono, 
                mail, fech_Naci, genero, nota, foto, telef_casa, telef_ofic,
                idPerson_Original,senMail);

            return (int)result;

        }
        public static int DeletePerson(int idPerson_Original)
        {
            return Person_TableAdapter.DeletePerson(idPerson_Original);
        }
        public static int UpdatePersonZona(int idPerson, int idSector)
        {
            personaBySector_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);


            int? idSec = (int)personaBySector_TableAdapter.ScalarIdSectorByIdPersona(idPerson);
            if (idSec > 0)
            {
                return PersonSector_TableAdapter.UpdatePersonSector(idSector, idPerson);
            }
            else
            {
                return PersonSector_TableAdapter.InsertPersonSector(idPerson, idSector);
            }
        }
        public static int getPersonIdSector(int idPerson)
        {

            personaBySector_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);


            int? idSec = (int)personaBySector_TableAdapter.ScalarIdSectorByIdPersona(idPerson);
            if (idSec > 0)
            {
                return (int)idSec;
            }
            else
            {
                return 0;
            }
        }
        public static CADsisVenta.DataSetPerson.PersonasDataTable getDataLikePerson(
            string param1, string param2, string param3)
        {

            return Person_TableAdapter.GetDataLikePerson(param1, param2, param3);
        }
        public static bool IsPersonRegister(string textConsult)
        {
            Person_TableAdapter.Connection = new System.Data.SqlClient.SqlConnection(Configuration.ConectionString);

            int? idSec = (int)Person_TableAdapter.ScalarIsPersonRegister(textConsult);
            if (idSec == 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

    }
}
