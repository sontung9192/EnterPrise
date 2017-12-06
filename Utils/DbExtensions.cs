using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

using System.ComponentModel;
using System.Globalization;

namespace Utils
{
    public static class DbExtensions
    {
        public static List<T> ToList<T>(this IDataReader rdr) where T : new()
        {
            var result = new List<T>();
            Type iType = typeof(T);
            try
            {
                if (rdr != null)
                {
                    //cache property info so we're not banging on reflection in a long loop
                    PropertyInfo[] cachedProps = new PropertyInfo[rdr.FieldCount];
                    FieldInfo[] cachedFields = new FieldInfo[rdr.FieldCount];

                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        string pName = rdr.GetName(i);
                        PropertyInfo pi = iType.GetProperty(pName);
                        if (pi != null)
                        {
                            cachedProps[i] = pi;
                        }
                        else
                        {
                            FieldInfo fi = iType.GetField(pName);
                            cachedFields[i] = fi;
                        }
                    }
                    //set the values        
                    //PropertyInfo prop;
                    //FieldInfo field;

                    while (rdr.Read())
                    {
                        T item = new T();
                        rdr.Load(ref item);
                        result.Add(item);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(iType.FullName, ex);
                //Log.Error("TOLIST ::: " + iType.FullName, ex);
            }
            finally
            {
                //Close reader , then connection will release and return to pooling 
                if (rdr != null && !rdr.IsClosed)
                    rdr.Close();
            }
            return result;
        }

        public static List<T> ToList<T>(this IDataReader rdr, out int totalrecords) where T : new()
        {
            totalrecords = -1;
            var result = new List<T>();
            Type iType = typeof(T);
            try
            {
                if (rdr != null)
                {
                    //cache property info so we're not banging on reflection in a long loop
                    PropertyInfo[] cachedProps = new PropertyInfo[rdr.FieldCount];
                    FieldInfo[] cachedFields = new FieldInfo[rdr.FieldCount];

                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        string pName = rdr.GetName(i);
                        PropertyInfo pi = iType.GetProperty(pName);
                        if (pi != null)
                        {
                            cachedProps[i] = pi;
                        }
                        else
                        {
                            FieldInfo fi = iType.GetField(pName);
                            cachedFields[i] = fi;
                        }
                    }
                    //set the values        
                    //PropertyInfo prop;
                    //FieldInfo field;

                    while (rdr.Read())
                    {
                        T item = new T();
                        rdr.Load(ref item);
                        result.Add(item);
                    }

                    if (rdr.NextResult() && rdr.Read())
                    {
                        totalrecords = rdr.GetInt32(0);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(iType.FullName, ex);
                //Log.Error("TOLIST ::: " + iType.FullName, ex);
            }
            finally
            {
                //Close reader , then connection will release and return to pooling 
                if (rdr != null && !rdr.IsClosed)
                    rdr.Close();
            }
            return result;
        }

        public static T SingleOrDefault<T>(this IDataReader rdr) where T : new()
        {
            if(rdr == null || rdr.IsClosed)
            {
                return default(T);
            }

            var result = new List<T>();
            Type iType = typeof(T);
            try
            {
                if (rdr != null)
                {
                    //cache property info so we're not banging on reflection in a long loop
                    PropertyInfo[] cachedProps = new PropertyInfo[rdr.FieldCount];
                    FieldInfo[] cachedFields = new FieldInfo[rdr.FieldCount];

                    for (int i = 0; i < rdr.FieldCount; i++)
                    {
                        string pName = rdr.GetName(i);
                        PropertyInfo pi = iType.GetProperty(pName);
                        if (pi != null)
                        {
                            cachedProps[i] = pi;
                        }
                        else
                        {
                            FieldInfo fi = iType.GetField(pName);
                            cachedFields[i] = fi;
                        }
                    }
                    //set the values        
                    //PropertyInfo prop;
                    //FieldInfo field;
                    while (rdr.Read())
                    {
                        T item = new T();
                        rdr.Load(ref item);
                        result.Add(item);
                    }

                    //Close reader , then connection will release and return to pooling 
                    //rdr.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(iType.FullName, ex);
            }
            finally
            {
                //Close reader , then connection will release and return to pooling 
                if (rdr != null && !rdr.IsClosed)
                    rdr.Close();
            }
            if (result.Count > 0)
                return result.FirstOrDefault();
            return default(T);
        }

        public static T TryParse<T>(string inValue)
        {
            TypeConverter converter =
                TypeDescriptor.GetConverter(typeof(T));

            return (T)converter.ConvertFromString(null,
                CultureInfo.InvariantCulture, inValue);
        }

        internal static void Load<T>(this IDataReader rdr, ref T item) where T : new()
        {
            Type iType = typeof(T);

            if (iType.IsPrimitive)
            {
                object obj = rdr.GetValue(0);
                if (obj != null)
                {
                    item = TryParse<T>(Convert.ToString(obj));
                }

                return;
            }

            try
            {
                //cache property info so we're not banging on reflection in a long loop
                PropertyInfo[] cachedProps = new PropertyInfo[rdr.FieldCount];
                FieldInfo[] cachedFields = new FieldInfo[rdr.FieldCount];
                #region get properties
                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    string pName = rdr.GetName(i);
                    PropertyInfo pi = iType.GetProperty(pName);
                    if (pi != null)
                    {
                        cachedProps[i] = pi;
                    }
                    else
                    {
                        FieldInfo fi = iType.GetField(pName);
                        cachedFields[i] = fi;
                    }
                }
                #endregion get properties
                //set the values        
                PropertyInfo prop;
                FieldInfo field;
                #region get value

                for (int i = 0; i < rdr.FieldCount; i++)
                {
                    prop = cachedProps[i];
                    field = cachedFields[i];
                    if (prop != null && !DBNull.Value.Equals(rdr.GetValue(i)))
                    {
                        try
                        {
                            prop.SetValue(item, rdr.GetValue(i), null);
                        }
                        catch (Exception ex1) {
                            throw new Exception(string.Format("{0}:{1}:{2}:{3}:{4}", iType.FullName, prop.Name, field != null ? field.Name : string.Empty, ex1.Message, ex1.StackTrace));
                        }
                    }

                    else if (field != null && !DBNull.Value.Equals(rdr.GetValue(i)))
                    {
                        try
                        {
                            object value = rdr.GetValue(i);
                            Type t = value.GetType();
                            if (t == typeof(SByte))
                            {
                                bool setting = value.ToString() == "0";
                                field.SetValue(item, setting);
                            }
                            else
                            {
                                //Type toFieldType = field.FieldType;
                                //value.ChangeTypeTo(toFieldType);
                                field.SetValue(item, value);
                            }
                        }
                        catch (Exception ex1) {
                            throw new Exception(string.Format("{0}:{1}:{2}", iType.FullName, prop.Name, field != null ? field.Name : string.Empty), ex1);
                        }
                    }
                }

                #endregion get value
            }
            catch (Exception ex)
            {
                throw new Exception(iType.FullName, ex);
            }
            //finally
            //{
            //    //Close reader , then connection will release and return to pooling 
            //    if (rdr != null && !rdr.IsClosed)
            //        rdr.Close();
            //}
        }
    }
}
