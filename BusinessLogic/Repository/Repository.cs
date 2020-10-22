using Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Repository
{
    public class Repository<T>
    {
        protected ApplicationDbContext db = new ApplicationDbContext();

        public List<string> FieldProperty = new List<string> {
            "System.Boolean",
            "System.String",
            "System.Int16",
            "System.Int32",
            "System.Int64",
            "System.int",
            "System.Double",
            "System.DateTime",
            "System.DateTimeOffset"
        };

        public bool ValidateTypeProperty(string property)
        {
            bool Valid = false;

            try
            {
                foreach (string p in FieldProperty)
                {
                    if (p.Equals(property))
                    {
                        Valid = true;
                        break;
                    }
                }
            }
            catch
            {
                Valid = false;
            }
            return Valid;
        }

        public T MapOut(T original, T update)
        {

            try
            {

                if (original == null)
                {
                    throw new NullReferenceException("This object is null.");
                }
                else
                {
                    foreach (PropertyInfo pi in original.GetType().GetProperties().Where(p => !p.GetGetMethod().GetParameters().Any()))
                    {
                        try
                        {
                            if (pi.Name != "Id")
                            {

                                if (ValidateTypeProperty(pi.PropertyType.FullName.ToString()))
                                {
                                    original.GetType().GetProperty(pi.Name).SetValue(original, pi.GetValue(update, null), null);
                                }
                            }

                        }
                        catch (Exception ex)
                        {
                            System.Diagnostics.Debug.WriteLine(ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                original = default;
                System.Diagnostics.Debug.WriteLine(ex.Message);
            }

            return original;
        }
    }
}
