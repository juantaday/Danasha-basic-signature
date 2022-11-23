using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Domain.Helpers
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


        private static string GetPropertyNull(string typenull)
        {
            if (typenull.Contains("System.DateTime"))
            {
                return "System.DateTime";
            }
            else if (typenull.Contains("System.Int32"))
            {
                return "System.Int32";
            }
            else
            {
                return "";
            }

        }


        public static bool ContainColumn(this DataTable table, string columnName)
        {
            DataColumnCollection columns = table.Columns;
            if (columns.Contains(columnName))
            {
                return true;
            }

            return false;
        }


        //public static List<T> ToList<T>(this IEnumerable<T> source) where T : class
        //{
        //    if (source == null)
        //    {
        //        throw new Exception("No hay origen de datos");
        //    }
        //    return new List<T>(source);
        //}

        public static List<T> ConvertDataTable<T>(DataTable dt)
        {
            List<T> data = new List<T>();
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
                        obj.GetType().GetProperty(p.Name)
                             .SetValue(obj, dr[column.ColumnName]);
                    }
                }
            }
            return obj;
        }




    }
}
