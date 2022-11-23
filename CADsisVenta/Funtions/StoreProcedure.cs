using CADsisVenta.Class;
using CADsisVenta.Data.Entyties;
using CADsisVenta.Data.Models;
using CADsisVenta.DataSetSystemTableAdapters;
using CADsisVenta.Helpers;
using DomainSQLite.Helpers;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CADsisVenta.Funtions
{
    public class StoreProcedure
    {
        #region Atrinbutes
        private static string keyHas = "sadsf325#@@@$@#485yy//*==9||*8";
        #endregion

        #region Methods

        public async static Task<List<PurchaseSaleMonth>> GetUtilitiesxMes(int fromYear, int fromMonth, int toYear, int toMonth)
        {

            using (var cmd = new SqlComandExec())
            {

                var result = new List<PurchaseSaleMonth>();

                var sql = @"SELECT  dt.Years,dt.Months, sum(Utilidades) as Valores from 
                            (select   year(fechaDesde)  as Years,  month(fechaDesde) as Months, (dt.Prec_Venta - (dt.Cantidad * dt.Prec_Compra)) as Utilidades
                            FROM FacturaVenta as f
	                        inner join FacturaVentaDetail as dt on f.idFactVenta = dt.idFacturaVenta
                            WHERE
                                fechaDesde >= CAST(LTRIM(@desdea) + RIGHT('00' + LTRIM(@desdem), 2) + '01' AS datetime)
                                AND fechaDesde<DATEADD([month],
                                DATEDIFF([month], '19000101', LTRIM(@hastaa) + RIGHT('00' + LTRIM(@hastam), 2) + '01') +1, '19000101')) as dt
                            group by dt.Years, dt.Months";


                cmd.ParameterCollection = new SqlParameter[] {
                    new SqlParameter
                    {
                        ParameterName ="@desdea",
                        SqlDbType =SqlDbType.Int,
                        Value =fromYear
                    },new SqlParameter
                    {
                        ParameterName ="@desdem",
                        SqlDbType =SqlDbType.Int,
                        Value =fromMonth
                    }, new SqlParameter
                    {
                        ParameterName ="@hastaa",
                        SqlDbType =SqlDbType.Int,
                        Value =toYear
                    }, new SqlParameter
                    {
                        ParameterName ="@hastam",
                        SqlDbType =SqlDbType.Int,
                        Value =toMonth
                    }
                };

                var dt = await cmd.RetornaTablaAsync(sql);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        result.Add(new PurchaseSaleMonth
                        {
                            Months = row.Field<int>("Months"),
                            Years = row.Field<int>("Years"),
                            Valores = row.Field<decimal>("Valores"),
                            TypeMovents = "Utilidad"
                        });
                    }

                }

                return result.OrderByDescending(x => x.Years).OrderByDescending(x => x.Months).ToList();
            }
        }

        public async static Task<DataTable > GetSalesWithDiscount()
        {

            using (var cmd = new SqlComandExec())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                return await cmd.RetornaTablaAsync("GetSalesWithDiscount");
                    
            }    

         }

        public async static Task<DataTable> GetSalesWithOperation(int operation)
        {

            using (var cmd = new SqlComandExec())
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ParameterCollection = new SqlParameter[] {
                    new SqlParameter {
                        ParameterName ="@IdOperation",
                        SqlDbType = SqlDbType.Int,  
                        Value = operation   
                    }
                };

                return await cmd.RetornaTablaAsync("GetSalesWithOperation");

            }

        }

     
        private static string FirstSQLExceteForIva()
        {
            return @"

                            alter table productos
                            disable trigger [trg_Productos_update];

                            select top(1) @Identity=Id  from ProductoImpuesto
                            where IdProducto =@IdProducto;
                            IF EXISTS (select 1 from ProductoImpuesto where Id=@Identity) 
                            BEGIN
                               Update  ProductoImpuesto set CODIGO_IMPUESTO = @Codigo
                               where Id = @Identity;
                            END
                            ELSE
                            BEGIN
                               insert ProductoImpuesto (CODIGO_IMPUESTO, IdProducto)
                               values (@Codigo, @IdProducto);
                               set @Identity = SCOPE_IDENTITY();
                            END

                            update Productos set DefaultTaxCode = @Codigo, ivaPorcentaje = @ivaPorcentaje
                            where idProducto = @IdProducto;

                            Delete from ProductoImpuesto
                            where IdProducto = @IdProducto and Id<> @Identity;

                            alter table productos
                            enable trigger [trg_Productos_update];

                             ";

        }


        public async static Task<List<SaleMonth>> GetVentaTipoXMes(int fromYear, int fromMonth, int toYear, int toMonth)
        {

            using (var cmd = new SqlComandExec())
            {

                var result = new List<SaleMonth>();

                var sql = @"Select idTypoDocument as TipoDoc,  dt.Years,dt.Months, sum(Base00Iva) as Base0, sum(Base12Iva) as BaseIva ,
                            sum(Iva) as Iva, sum(Total) as Total
                            from 
                                (select f.idTypoDocument,  year(fechaDesde)  as Years,  month(fechaDesde) as Months,
	                            Base00Iva , Base12Iva, Iva, Total
                                FROM FacturaVenta as f
                            WHERE
                                fechaDesde >= CAST(LTRIM(@desdea) + RIGHT('00' + LTRIM(@desdem), 2) + '01' AS datetime)
                                AND fechaDesde<DATEADD([month],
                                DATEDIFF([month], '19000101', LTRIM(@hastaa) + RIGHT('00' + LTRIM(@hastam), 2) + '01') +1, '19000101')) as dt
                            group by dt.idTypoDocument, dt.Years, dt.Months";


                cmd.ParameterCollection = new SqlParameter[] {
                    new SqlParameter
                    {
                        ParameterName ="@desdea",
                        SqlDbType =SqlDbType.Int,
                        Value =fromYear
                    },new SqlParameter
                    {
                        ParameterName ="@desdem",
                        SqlDbType =SqlDbType.Int,
                        Value =fromMonth
                    }, new SqlParameter
                    {
                        ParameterName ="@hastaa",
                        SqlDbType =SqlDbType.Int,
                        Value =toYear
                    }, new SqlParameter
                    {
                        ParameterName ="@hastam",
                        SqlDbType =SqlDbType.Int,
                        Value =toMonth
                    }
                };

                var dt = await cmd.RetornaTablaAsync(sql);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        result.Add(new SaleMonth
                        {
                            Months = row.Field<int>("Months"),
                            Years = row.Field<int>("Years"),
                            TipoDoc = row.Field<int>("TipoDoc"),
                            Base0 = row.Field<decimal>("Base0"),
                            BaseIva = row.Field<decimal>("BaseIva"),
                            Iva = row.Field<decimal>("Iva"),
                            Total = row.Field<decimal>("Total")
                        });
                    }

                }

                return result.OrderByDescending(x => x.Years).OrderByDescending(x => x.Months).ToList();
            }
        }


        public async static Task<List<ItemWeraHouse>> GetListWareHuose() {
            using (var cmd = new SqlComandExec())
            {

                var result = new List<ItemWeraHouse>();

                var sql = @"select b.idBodega, (tb.Nom_typoBodega + ' | '+  b.Nom_Bodega ) as Nom_Bodega
                        from Bodegas as  b
                        inner join TypoBodega as tb on tb.idTypoBodega = b.TypoBodega";


              
                var dt = await cmd.RetornaTablaAsync(sql);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        result.Add(new ItemWeraHouse
                        {
                            idBodega = row.Field<int>("idBodega"),
                            Nom_Bodega = row.Field<string >("Nom_Bodega")
                        });
                    }

                }

                return result.OrderBy(x => x.idBodega).ToList();
            }

        }

        public async static Task<List<ItemMenu>> GetListMenuActivated(string CodUser)
        {
            using (var cmd = new SqlComandExec())
            {

                var result = new List<ItemMenu>();

                var sql = @"SELECT  P.idPermiso, P.DropDownName, p.MenuStripName
                            FROM   dbo.UsuarioGrupo AS U 
                            INNER JOIN dbo.Grupo AS G ON U.idGrupo = G.idGrupo 
                            INNER JOIN dbo.GrupoPermiso AS GU ON G.idGrupo = GU.idGrupo 
                            INNER JOIN dbo.Permisos AS P ON GU.idPermiso = P.idPermiso 
                            WHERE  (U.[login] =@CodUser)";


                cmd.ParameterCollection = new SqlParameter[] {
                    new SqlParameter
                    {
                        ParameterName ="@CodUser",
                        SqlDbType =SqlDbType.Char,
                        Value =CodUser
                    }
                };

                var dt = await cmd.RetornaTablaAsync(sql);
                if (dt != null)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        result.Add(new ItemMenu
                        {
                            idPermiso = row.Field<int>("idPermiso"),
                            DropDownName = row.Field<string>("DropDownName"),
                            MenuStripName = row.Field<string>("MenuStripName")
                        });
                    }

                }

                return result;
            }

        }

        public static ResponseTerminal isTerminalHabil(string codUser, string codTerminal, int idTerminal = 0)
        {
            try
            {
                string sql;
                using (CajaStadoTableAdapter atap = new CajaStadoTableAdapter())
                {
                    DataTable dt = new DataTable();
                    dt = atap.GetDataByHabilUserWithIdTerminal(codUser, idTerminal);
                    if (!(dt.Rows.Count == 0))
                    {
                        return new ResponseTerminal { Success = true, DataDb = dt, IDterminal = dt.Rows[0].Field<int>("idCajaStado") };
                    }
                    dt = atap.GetDataByHabilAllUserOnIdTerminal(idTerminal);
                    if (!(dt.Rows.Count == 0))
                    {
                        return new ResponseTerminal { Success = true, DataDb = dt, IDterminal = dt.Rows[0].Field<int>("idCajaStado") };
                    }
                    dt = null;
                    sql = "Este terminal no tiene estado de operación activa." + Constants.vbNewLine;
                    sql = sql + "Solicítelo al administrador de terminales.";
                    Interaction.MsgBox(sql, MsgBoxStyle.Exclamation, "Importante");
                    return new ResponseTerminal { Success = false, DataDb = null };
                }

            }
            catch (Exception ex)
            {
                Interaction.MsgBox(ex.Message, MsgBoxStyle.Critical, "Error");
                return new ResponseTerminal { Success = false, DataDb = null };
            }
        }
        /// <summary>
        /// Devuel tres datos
        /// 1 codigo de barra
        /// 2 id de presentacion
        /// 3 codigo de producto
        /// </summary>
        /// <param name="idProduct"></param>
        /// <returns></returns>
        public async static Task<Tuple<string ,int, string  >> GetBarCode(int idProduct)
        {
            string sql = @"select  top(1) pp.Barcode,pp.idPresentacion , pp.codProducto
            from ProductoPresentacion  as  pp
            where pp.idProducto =@idProducto";

            using (var cmd = new SqlComandExec())
            {
                cmd.ParameterCollection = new SqlParameter[] {
                    new SqlParameter
                    {
                        ParameterName ="@idProducto",
                        SqlDbType =SqlDbType.Int,
                        Value =idProduct
                    }
                };

                var dt = await cmd.RetornaTablaAsync(sql);
                if (dt != null && dt.Rows.Count == 1)
                {
                    return Tuple.Create  (dt.Rows[0].Field<string>("Barcode"), 
                        dt.Rows[0].Field<int>("idPresentacion"),
                        dt.Rows[0].Field<string >("codProducto"));
                }
                else
                {
                    return Tuple .Create (string.Empty ,-1, string.Empty );
                }
            }

        }
        public async static Task<decimal> GetMontoMaximoCliente(int idcliente)
        {
            string  sql = @"select c.monto_Max
	                from Clientes as c
	                where c.idCliente = @IdCliente and  c.credito = 1;";
            using (var cmd = new SqlComandExec()) {
                cmd.ParameterCollection = new SqlParameter[] {
                    new SqlParameter
                    {
                        ParameterName ="@IdCliente",
                        SqlDbType =SqlDbType.Int,
                        Value =idcliente
                    }
                };

                var dt = await cmd.RetornaTablaAsync(sql);
                if (dt != null && dt.Rows.Count == 1)
                {
                    return dt.Rows[0].Field<decimal>("monto_Max");
                }
                else {
                    return 0; 
                }
            }

        }



        public static async  Task<bool > DeleteBarCode(int id_Producto)
        {
            var sql = @"update pp set pp.Barcode =@Barcode
            from ProductoPresentacion as pp
            where idProducto = @idProduct;";

            using (var cmd = new CADsisVenta.Funtions.SqlComandExec())
            {
                cmd.ParameterCollection = new SqlParameter[]
                {
                    new SqlParameter
                    {
                        ParameterName= "@idProduct",
                        SqlDbType = SqlDbType.Int,
                        Value = id_Producto
                    },
                     new SqlParameter
                    {
                        ParameterName= "@Barcode",
                        SqlDbType = SqlDbType.VarChar,
                        Value = DBNull.Value 
                    }
                };

                return await cmd.ExecuteComandAsync(sql);
            }

        }

        public async static Task<DataTable> GetElectronicInvoice(DateTime dateStar, DateTime dateEnd, bool filterDate  )
        {
          
            using (var cnn = new SqlConnection(DomainSQLite.Setting.Configuration.ConectionString)) 
            {
                await cnn.OpenAsync();
                using (var cmd = new SqlCommand("GetElectronicInvoice", cnn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    var parameters = new SqlParameter[] {
                        new SqlParameter {
                            ParameterName = "@DateStar",
                            Value = dateStar.Date,
                            SqlDbType = SqlDbType.DateTime
                        },
                        new SqlParameter {
                            ParameterName = "@DateEnd",
                            Value = dateEnd.Date,
                            SqlDbType = SqlDbType.DateTime
                        },
                        new SqlParameter {
                            ParameterName = "@FilterDate",
                            Value = filterDate,
                            SqlDbType = SqlDbType.Bit
                        } 
                    
                    };

                     cmd.Parameters.AddRange(parameters);
                
                    cmd.ExecuteNonQuery();
                    var dt = new DataTable();  
                    using (var tab = new SqlDataAdapter(cmd))
                    {
                        tab.Fill(dt);
                        return dt;

                    }

                };

          }

         }

        public async static Task<DataTable> GetElectronicInvoiceDeatil(DateTime dateStar, DateTime dateEnd, bool filterDate)
        {
            using (var cnn = new SqlConnection(DomainSQLite.Setting.Configuration.ConectionString))
            {
                await cnn.OpenAsync();

                using (var cmd = new SqlCommand("GetElectronicInvoiceDetail",cnn))
                {

                    cmd.CommandType = CommandType.StoredProcedure;

                      var parameters = new SqlParameter[] {
                            new SqlParameter {
                                ParameterName = "@DateStar",
                                Value = dateStar.Date,
                                SqlDbType = SqlDbType.DateTime
                            },
                            new SqlParameter {
                                ParameterName = "@DateEnd",
                                Value = dateEnd.Date,
                                SqlDbType = SqlDbType.DateTime
                            },
                            new SqlParameter {
                                ParameterName = "@FilterDate",
                                Value = filterDate,
                                SqlDbType = SqlDbType.Bit
                            }
                        };

                    cmd.Parameters.AddRange(parameters);

                    cmd.ExecuteNonQuery();

                    var dt = new DataTable();
 
                    using (var tab = new SqlDataAdapter(cmd))
                        tab.Fill(dt);

                    return dt;

                }

            }
         

        }


    }

        #endregion

}
