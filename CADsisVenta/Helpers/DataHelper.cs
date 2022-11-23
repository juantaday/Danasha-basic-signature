using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CADsisVenta.Helpers
{
    public static class DataHelper
    {
        public static DataTable ToDataTable<T>(IList<T> lista) where T : class, new()
        {

            DataTable table = new DataTable();

            T entidad = new T();
            PropertyInfo[] pInfo = entidad.GetType().GetProperties();

            foreach (PropertyInfo prop in entidad.GetType().GetProperties())
            {

                string inField = "System.";

                if (prop.PropertyType.FullName.IndexOf(inField) != -1 && !prop.PropertyType.FullName.Contains("Collection"))
                {
                    table.Columns.Add(new DataColumn(prop.Name, Type.GetType(prop.PropertyType.FullName)));
                }
            }


            if (table.Columns.Count > 0 && lista != null)
            {
                foreach (T t in lista)
                {
                    DataRow row = table.NewRow();
                    foreach (DataColumn column in table.Columns)
                    {

                        row[column.ColumnName] = t.GetType().GetProperty(column.ColumnName).GetValue(t, null);
                    }
                    table.Rows.Add(row);
                }
            }

            return table;
        }


        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();

            if (dt == null)
                return data;


            foreach (DataRow row in dt.Rows)
            {
                T item = GetItem<T>(row);
                data.Add(item);
            }

            return data;

        }
        private static T GetItem<T>(DataRow dr)
        {
            Type temp = typeof(T);
            T obj = Activator.CreateInstance<T>();

            foreach (DataColumn column in dr.Table.Columns)
            {
                if (dr[column.ColumnName] != null && !string.IsNullOrEmpty(dr[column.ColumnName].ToString()))
                {
                    var p = obj.GetType().GetProperties().FirstOrDefault(x => x.Name == column.ColumnName);
                    if (p != null)

                    {

                        //Convert.ChangeType does not handle conversion to nullable types
                        //if the property type is nullable, we need to get the underlying type of the property
                        var targetType = IsNullableType(p.PropertyType) ? Nullable.GetUnderlyingType(p.PropertyType) : p.PropertyType;


                        try
                        {
                            if (p.PropertyType.IsEnum)
                                p.SetValue(obj, dr[column.ColumnName] is DBNull ? (object)null : Enum.Parse(targetType, Convert.ToString(dr[column.ColumnName])));
                            else
                                p.SetValue(obj, dr[column.ColumnName] is DBNull ? (object)null : Convert.ChangeType(dr[column.ColumnName], targetType));
                        }
                        catch (Exception ex)
                        {
                            //Logging.CustomLogging(loggingAreasType: LoggingAreasType.Class, loggingType: LoggingType.Error, className: CurrentClassName, methodName: MethodBase.GetCurrentMethod().Name, stackTrace: "There's some problem in converting model property name: " + PropertyName + ", model property type: " + targetType.ToString() + ", data row value: " + (dr[PropertyName] is DBNull ? string.Empty : Convert.ToString(dr[PropertyName])) + " | " + ex.StackTrace);
                            throw new Exception(ex.Message + "\n" + ex.StackTrace, ex.InnerException);
                        }


                    }
                }
            }
            return obj;
        }

        private static bool IsNullableType(Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition().Equals(typeof(Nullable<>));
        }

    }
}
