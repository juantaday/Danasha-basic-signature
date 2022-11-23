using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CADsisVenta.Statics
{
    public class PropertyCopier<TParent, TChild> where TParent : class
                                            where TChild : class
    {
        public static void Copy(TParent parent, TChild child, string[] notMapped)
        {


            if (notMapped != null)
                notMapped = new List<string>(notMapped) { "Id" }.ToArray();
            else
                notMapped = new List<string>() { }.ToArray();


            var parentProperties = parent.GetType().GetProperties().Where(p => !notMapped.Contains(p.Name)).ToArray();


            var childProperties = child.GetType().GetProperties();


            foreach (PropertyInfo propertyInfo in child.GetType().GetProperties())
            {
                if ((Array.IndexOf(notMapped, propertyInfo.Name)) == -1 && propertyInfo.PropertyType.IsSerializable)
                {
                    child.GetType().GetProperty(propertyInfo.Name)
                   .SetValue(child, propertyInfo.GetValue(parent));
                }
            } 

            //foreach (var parentProperty in parentProperties)
            //{

            //    if (parentProperty.PropertyType.IsSerializable)
            //    {

            //        foreach (var childProperty in childProperties)
            //        {
            //            if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
            //            {
            //                childProperty.SetValue(child, parentProperty.GetValue(parent));
            //                break;
            //            }
            //        }
            //    }

   
            //}

        }

       
    }
}
