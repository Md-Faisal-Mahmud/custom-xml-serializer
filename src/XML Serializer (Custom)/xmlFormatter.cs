 using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace JSON_Serializer__Custom_;

public static class xmlFormatter
{
    public static string Convert(object obj, bool first = true)
    {
        StringBuilder json = new StringBuilder();
        Type type = obj.GetType();

        if (first)
        {
            json.Append($"<{type.Name}>");
        }

        if (obj == null)
        {
            json.Append($"<{type.Name}></{type.Name}>");
        }

        var fieldInfo = type.GetFields();
        var propertyInfo = type.GetProperties();

        #region  Field section
        foreach (var field in fieldInfo)                   //==============================================================================================================================
        {
            if (field.FieldType == typeof(object))
            #region object datatype: field
            {
                var value = field.GetValue(obj);

                //json.Append($"\"{property.Name}\":");

                if (value is null)
                {
                    json.Append($"<{field.Name}></{field.Name}>");
                }
                else if (value is string || value is char)
                {
                    if (field.FieldType == typeof(char) && (char)field.GetValue(obj) == '\0')
                    {
                        json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                    }
                    else
                    {
                        json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>\n");
                    }
                }
                else if (value is int || value is sbyte || value is byte || value is short || value is ushort || value is uint || value is long || value is ulong)
                {
                    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                }
                else if (value is double)
                {
                    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                    //double withPoint = (double)field.GetValue(obj);
                    //double withoutPoint = Math.Truncate(withPoint);
                    //if (withPoint == withoutPoint)
                    //{
                    //    json.Append($"<{field.Name}>{string.Format("{0:0.0}", field.GetValue(obj))}</{field.Name}>");
                    //}
                    //else
                    //{
                    //    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                    //}
                }
                else if (value is decimal)
                {
                    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                    //decimal withPoint = (decimal)field.GetValue(obj);
                    //decimal withoutPoint = Math.Truncate(withPoint);
                    //if (withPoint == withoutPoint)
                    //{
                    //    json.Append($"<{field.Name}>{string.Format("{0:0.0}", field.GetValue(obj))}</{field.Name}>");
                    //}
                    //else
                    //{
                    //    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                    //}
                }
                else if (value is float)
                {
                    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                    //float withPoint = (float)field.GetValue(obj);
                    //float withoutPoint = (float)Math.Truncate(withPoint);
                    //if (withPoint == withoutPoint)
                    //{
                    //    json.Append($"<{field.Name}>{string.Format("{0:0.0}", field.GetValue(obj))}</{field.Name}>");
                    //}
                    //else
                    //{
                    //    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                    //}
                }
                else
                {
                    json.Append($"<{field.Name}></{field.Name}>");
                }

            }
            #endregion
            else if (field.GetValue(obj) == null)                                                    /// null
            {
                json.Append($"<{field.Name}></{field.Name}>");
            }
            else if (field.FieldType == typeof(char))                                             ///  char
            {
                if ((char)field.GetValue(obj) == '\0')
                {
                    json.Append($"<{field.Name}></{field.Name}>");
                }
                else
                    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>\n");
            }
            else if (field.FieldType == typeof(string) || field.FieldType == typeof(char))         /// string
            {
                if (field.FieldType == typeof(char) && (char)field.GetValue(obj) == '\0')
                {
                    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                }
                else
                {
                    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>\n");
                }
            }
            #region  --->                                                                       ///   List<int> (primitive)  + List<string> 
            else if (field.FieldType.IsGenericType &&
                field.FieldType.GetGenericTypeDefinition() == typeof(List<>) &&
                (field.FieldType.GetGenericArguments()[0].IsPrimitive ||
                field.FieldType.GetGenericArguments()[0] == typeof(string) ||
                field.FieldType.GetGenericArguments()[0] == typeof(decimal)))
            {
                var list = field.GetValue(obj) as IList;
                json.Append($"<{field.Name}>");
                string naming = string.Empty;
                var type1 = field.FieldType.GetGenericArguments()[0];
                if (type1 == typeof(int))
                {
                    naming = "int";
                }
                else if (type1 == typeof(uint))
                {
                    naming = "uint";
                }
                else if (type1 == typeof(bool))
                {
                    naming = "boolean";
                }
                else if (type1 == typeof(char))
                {
                    naming = "char";
                }
                else if (type1 == typeof(byte))
                {
                    naming = "byte";
                }
                else if (type1 == typeof(sbyte))
                {
                    naming = "sbyte";
                }

                else if (type1 == typeof(short))
                {
                    naming = "short";
                }
                else if (type1 == typeof(ushort))
                {
                    naming = "unsignedShort";
                }
                else if (type1 == typeof(long))
                {
                    naming = "long";

                }
                else if (type1 == typeof(ulong))
                {
                    naming = "ulong";
                }
                else if (type1 == typeof(string))
                {
                    naming = "string";
                }
                else if (type1 == typeof(float))
                {
                    naming = "float";
                }
                else if (type1 == typeof(double))
                {
                    naming = "double";
                }
                else if (type1 == typeof(decimal))
                {
                    naming = "decimal";
                }
                else
                {
                    naming = type1.Name.ToString();
                }
                foreach (var item in list)
                {
                    Type t = item.GetType();
                    if (naming == "boolean")
                    {
                        json.Append($"<{naming}>{item.ToString().ToLower()}</{naming}>");
                    }
                    else
                    {
                        json.Append($"<{naming}>{item}</{naming}>");
                    }
                }
                json.Append($"</{field.Name}>");
            }
            #endregion
            else if (field.FieldType.IsArray && (!field.FieldType.GetElementType().IsPrimitive) &&                       ///    array
                    (field.FieldType != typeof(string[])) &&
                    (field.FieldType != typeof(decimal[])))
            {
                var array = field.GetValue(obj) as Array;
                json.Append($"<{field.Name}>");
                for (int j = 0; j < array.Length; j++)
                {
                    Type t = array.GetValue(j).GetType();
                    json.Append($"<{t.Name}>");
                    json.Append(Convert(array.GetValue(j), false));
                    json.Append($"</{t.Name}>");
                }
                json.Append($"</{field.Name}>");
            }
            else if (field.FieldType.IsGenericType)                                           ///    generic List<>
            {
                //var listType = property.PropertyType.GetGenericArguments()[0];
                //var list = (property.GetValue(obj) as IEnumerable<dynamic>).ToList();
                var list = (field.GetValue(obj) as IEnumerable).OfType<object>().ToList();

                json.Append($"<{field.Name}>");
                foreach (var item in list)
                {
                    Type t = item.GetType();
                    json.Append($"<{t.Name}>");
                    json.Append(Convert(item, false));
                    json.Append($"</{t.Name}>");
                }
                json.Append($"</{field.Name}>");
            }
            else if (field.FieldType.IsClass && field.FieldType != typeof(string))                ///    class + int[] etc    Array
            {
                #region  --->                                                                       ///  array  of primitives

                if (field.FieldType.IsArray && (field.FieldType.GetElementType().IsPrimitive ||
                    field.FieldType.GetElementType() == typeof(string) ||
                    field.FieldType.GetElementType() == typeof(decimal)))
                {
                    var list = field.GetValue(obj) as Array;
                    json.Append($"<{field.Name}>");
                    string naming = string.Empty;
                    var type1 = field.FieldType;
                    if (type1 == typeof(int[]))
                    {
                        naming = "int";
                    }
                    else if (type1 == typeof(char[]))
                    {
                        naming = "char";
                    }
                    else if (type1 == typeof(uint[]))
                    {
                        naming = "uint";
                    }
                    else if (type1 == typeof(bool[]))
                    {
                        naming = "boolean";
                    }
                    else if (type1 == typeof(byte[]))
                    {
                        naming = "byte";
                    }
                    else if (type1 == typeof(sbyte[]))
                    {
                        naming = "sbyte";
                    }
                    else if (type1 == typeof(short[]))
                    {
                        naming = "short";
                    }
                    else if (type1 == typeof(ushort[]))
                    {
                        naming = "unsignedShort";
                    }
                    else if (type1 == typeof(long[]))
                    {
                        naming = "long";
                    }
                    else if (type1 == typeof(ulong[]))
                    {
                        naming = "ulong";
                    }
                    else if (type1 == typeof(string[]))
                    {
                        naming = "string";
                    }
                    else if (type1 == typeof(float[]))
                    {
                        naming = "float";
                    }
                    else if (type1 == typeof(double[]))
                    {
                        naming = "double";
                    }
                    else if (type1 == typeof(decimal[]))
                    {
                        naming = "decimal";
                    }
                    else
                    {
                        naming = type1.Name.ToString();
                    }
                    foreach (var item in list)
                    {
                        Type t = item.GetType();
                        if (naming == "boolean")
                        {
                            json.Append($"<{naming}>{item.ToString().ToLower()}</{naming}>");
                        }
                        else
                        {
                            json.Append($"<{naming}>{item}</{naming}>");
                        }
                    }

                    json.Append($"</{field.Name}>");
                }
                #endregion
                else
                {
                    var value = field.GetValue(obj);
                    json.Append($"<{field.Name}>");
                    json.Append($"{Convert(value, false)}");
                    json.Append($"</{field.Name}>");
                }
            }
            else if (field.FieldType == typeof(DateTime))                                      ///  DateTime  com.
            {
                //json.Append($"<{field.Name}>{((DateTime)field.GetValue(obj)).ToString("yyyy-MM-ddTHH:mm:ss")}</{field.Name}>\n");
                json.Append($"<{field.Name}>{(DateTime)field.GetValue(obj)}</{field.Name}>\n");
            }
            else if (field.FieldType == typeof(bool))                                         /// boolen
            {
                json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>\n");
            }
            else if (field.FieldType.IsEnum)                                                  /// Enum
            {
                var val = field.GetValue(obj);
                json.Append($"<{field.Name}>{val}</{field.Name}>\n");
            }
            ///   int, sbyte, byte,short, ushort, uint, long, ulong
            else if (field.FieldType == typeof(int) || field.FieldType == typeof(sbyte) || field.FieldType == typeof(byte) || field.FieldType == typeof(short) || field.FieldType == typeof(ushort) || field.FieldType == typeof(uint) || field.FieldType == typeof(long) || field.FieldType == typeof(ulong))
            {
                json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
            }
            else if (field.FieldType == typeof(double))                                        /// double .com
            {
                json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                #region Formatting
                //double withPoint = (double)field.GetValue(obj);
                //double withoutPoint = Math.Truncate(withPoint);
                //if (withPoint == withoutPoint)
                //{ 
                //    json.Append($"<{field.Name}>{string.Format("{0:0.0}", field.GetValue(obj))}</{field.Name}>");
                //}
                //else
                //{
                //    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                //}
                #endregion

            }
            else if (field.FieldType == typeof(decimal))                                     /// decimal
            {
                json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                #region Fomatting
                //decimal withPoint = (decimal)field.GetValue(obj);
                //decimal withoutPoint = Math.Truncate(withPoint);
                //if (withPoint == withoutPoint)
                //{
                //    json.Append($"<{field.Name}>{string.Format("{0:0.0}", field.GetValue(obj))}</{field.Name}>");
                //}
                //else
                //{
                //    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                //}
                #endregion

            }

            else if (field.FieldType == typeof(float))                                       /// float
            {
                json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                #region Fomatting
                //float withPoint = (float)field.GetValue(obj);
                //float withoutPoint = (float)Math.Truncate(withPoint);
                //if (withPoint == withoutPoint)
                //{
                //    json.Append($"<{field.Name}>{string.Format("{0:0.0}", field.GetValue(obj))}</{field.Name}>");
                //}
                //else
                //{
                //    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                //}
                #endregion
            }
            else if (field.FieldType == typeof(Guid))                                        /// Guid
            {
                var x = field.GetValue(obj);
                if (x is string)
                {
                    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");

                }
                else
                {
                    json.Append($"<{field.Name}>{field.GetValue(obj).ToString()}</{field.Name}>");
                }
            }
            else if (field.FieldType.IsValueType && !field.FieldType.IsPrimitive)            /// custom struct
            {
                var value = field.GetValue(obj);
                json.Append($"<{field.Name}>");
                json.Append($"{Convert(value, false)}");
                json.Append($"</{field.Name}>");
            }
            else
            {
            }
        }
        #endregion
        #region Property   section
        foreach (var property in propertyInfo)                   //==============================================================================================================================
        {
            if (property.PropertyType == typeof(object))
            #region object datatype: field
            {
                var value = property.GetValue(obj);

                //json.Append($"\"{property.Name}\":");
                if (value is null)
                {
                    json.Append($"<{property.Name}></{property.Name}>");
                }
                else if (value is string || value is char)
                {
                    if (property.PropertyType == typeof(char) && (char)property.GetValue(obj) == '\0')
                    {
                        json.Append($"<{property.Name}>{property.GetValue(obj)}</{property.Name}>");
                    }
                    else
                    {
                        json.Append($"<{property.Name}>{property.GetValue(obj)}</{property.Name}>\n");
                    }
                }
                else if (value is int || value is sbyte || value is byte || value is short || value is ushort || value is uint || value is long || value is ulong)
                {
                    json.Append($"<{property.Name}>{property.GetValue(obj)}</{property.Name}>");
                }
                else if (value is double)
                {
                    json.Append($"<{property.Name}>{property.GetValue(obj)}</{property.Name}>");
                    //double withPoint = (double)field.GetValue(obj);
                    //double withoutPoint = Math.Truncate(withPoint);
                    //if (withPoint == withoutPoint)
                    //{
                    //    json.Append($"<{field.Name}>{string.Format("{0:0.0}", field.GetValue(obj))}</{field.Name}>");
                    //}
                    //else
                    //{
                    //    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                    //}
                }
                else if (value is decimal)
                {
                    json.Append($"<{property.Name}>{property.GetValue(obj)}</{property.Name}>");
                    //decimal withPoint = (decimal)field.GetValue(obj);
                    //decimal withoutPoint = Math.Truncate(withPoint);
                    //if (withPoint == withoutPoint)
                    //{
                    //    json.Append($"<{field.Name}>{string.Format("{0:0.0}", field.GetValue(obj))}</{field.Name}>");
                    //}
                    //else
                    //{
                    //    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                    //}
                }
                else if (value is float)
                {
                    json.Append($"<{property.Name}>{property.GetValue(obj)}</{property.Name}>");
                    //float withPoint = (float)field.GetValue(obj);
                    //float withoutPoint = (float)Math.Truncate(withPoint);
                    //if (withPoint == withoutPoint)
                    //{
                    //    json.Append($"<{field.Name}>{string.Format("{0:0.0}", field.GetValue(obj))}</{field.Name}>");
                    //}
                    //else
                    //{
                    //    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                    //}
                }
                else
                {
                    json.Append($"<{property.Name}></{property.Name}>");
                }
            }
            #endregion
            else if (property.GetValue(obj) == null)                                                    /// null
            {
                json.Append($"<{property.Name}></{property.Name}>");
            }
            else if (property.PropertyType == typeof(char))                                             ///  char
            {
                if ((char)property.GetValue(obj) == '\0')
                {
                    json.Append($"<{property.Name}></{property.Name}>");
                }
                else
                    json.Append($"<{property.Name}>{property.GetValue(obj)}</{property.Name}>\n");
            }
            else if (property.PropertyType == typeof(string) || property.PropertyType == typeof(char))         /// string
            {
                if (property.PropertyType == typeof(char) && (char)property.GetValue(obj) == '\0')
                {
                    json.Append($"<{property.Name}>{property.GetValue(obj)}</{property.Name}>");
                }
                else
                {
                    json.Append($"<{property.Name}>{property.GetValue(obj)}</{property.Name}>\n");
                }
            }
            #region  --->                                                                       ///   List<int> (primitive)  + List<string> 
            else if (property.PropertyType.IsGenericType &&
                property.PropertyType.GetGenericTypeDefinition() == typeof(List<>) &&
                (property.PropertyType.GetGenericArguments()[0].IsPrimitive ||
                property.PropertyType.GetGenericArguments()[0] == typeof(string) ||
                property.PropertyType.GetGenericArguments()[0] == typeof(decimal)))
            {
                var list = property.GetValue(obj) as IList;
                json.Append($"<{property.Name}>");
                string naming = string.Empty;
                var type1 = property.PropertyType.GetGenericArguments()[0];
                if (type1 == typeof(int))
                {
                    naming = "int";
                }
                else if (type1 == typeof(uint))
                {
                    naming = "uint";
                }
                else if (type1 == typeof(bool))
                {
                    naming = "boolean";
                }
                else if (type1 == typeof(char))
                {
                    naming = "char";
                }
                else if (type1 == typeof(byte))
                {
                    naming = "byte";
                }
                else if (type1 == typeof(sbyte))
                {
                    naming = "sbyte";
                }
                else if (type1 == typeof(short))
                {
                    naming = "short";
                }
                else if (type1 == typeof(ushort))
                {
                    naming = "unsignedShort";
                }
                else if (type1 == typeof(long))
                {
                    naming = "long";

                }
                else if (type1 == typeof(ulong))
                {
                    naming = "ulong";
                }
                else if (type1 == typeof(string))
                {
                    naming = "string";
                }
                else if (type1 == typeof(float))
                {
                    naming = "float";
                }
                else if (type1 == typeof(double))
                {
                    naming = "double";
                }
                else if (type1 == typeof(decimal))
                {
                    naming = "decimal";
                }
                else
                {
                    naming = type1.Name.ToString();
                }
                foreach (var item in list)
                {
                    Type t = item.GetType();
                    if (naming == "boolean")
                    {
                        json.Append($"<{naming}>{item.ToString().ToLower()}</{naming}>");
                    }
                    else
                    {
                        json.Append($"<{naming}>{item}</{naming}>");
                    }
                }
                json.Append($"</{property.Name}>");
            }
            #endregion
            else if (property.PropertyType.IsArray && (!property.PropertyType.GetElementType().IsPrimitive) &&                       ///    array
                    (property.PropertyType != typeof(string[])) &&
                    (property.PropertyType != typeof(decimal[])))
            {
                var array = property.GetValue(obj) as Array;
                json.Append($"<{property.Name}>");
                for (int j = 0; j < array.Length; j++)
                {
                    Type t = array.GetValue(j).GetType();
                    json.Append($"<{t.Name}>");
                    json.Append(Convert(array.GetValue(j), false));
                    json.Append($"</{t.Name}>");
                }
                json.Append($"</{property.Name}>");
            }
            else if (property.PropertyType.IsGenericType)                                           ///    generic List<>
            {
                //var listType = property.PropertyType.GetGenericArguments()[0];
                //var list = (property.GetValue(obj) as IEnumerable<dynamic>).ToList();


                var list = (property.GetValue(obj) as IEnumerable).OfType<object>().ToList();

                json.Append($"<{property.Name}>");
                foreach (var item in list)
                {
                    Type t = item.GetType();
                    json.Append($"<{t.Name}>");
                    json.Append(Convert(item, false));
                    json.Append($"</{t.Name}>");
                }
                json.Append($"</{property.Name}>");
            }
            else if (property.PropertyType.IsClass && property.PropertyType != typeof(string))                ///    class + int[] etc    Array
            {
                #region  --->                                                                       ///  array  of primitives
                if (property.PropertyType.IsArray && (property.PropertyType.GetElementType().IsPrimitive ||
                    property.PropertyType.GetElementType() == typeof(string) ||
                    property.PropertyType.GetElementType() == typeof(decimal)))
                {
                    var list = property.GetValue(obj) as Array;
                    json.Append($"<{property.Name}>");
                    string naming = string.Empty;
                    var type1 = property.PropertyType;
                    if (type1 == typeof(int[]))
                    {
                        naming = "int";
                    }
                    else if (type1 == typeof(char[]))
                    {
                        naming = "char";
                    }
                    else if (type1 == typeof(uint[]))
                    {
                        naming = "uint";
                    }
                    else if (type1 == typeof(bool[]))
                    {
                        naming = "boolean";
                    }
                    else if (type1 == typeof(byte[]))
                    {
                        naming = "byte";
                    }
                    else if (type1 == typeof(sbyte[]))
                    {
                        naming = "sbyte";
                    }
                    else if (type1 == typeof(short[]))
                    {
                        naming = "short";
                    }
                    else if (type1 == typeof(ushort[]))
                    {
                        naming = "unsignedShort";
                    }
                    else if (type1 == typeof(long[]))
                    {
                        naming = "long";
                    }
                    else if (type1 == typeof(ulong[]))
                    {
                        naming = "ulong";
                    }
                    else if (type1 == typeof(string[]))
                    {
                        naming = "string";
                    }
                    else if (type1 == typeof(float[]))
                    {
                        naming = "float";
                    }
                    else if (type1 == typeof(double[]))
                    {
                        naming = "double";
                    }
                    else if (type1 == typeof(decimal[]))
                    {
                        naming = "decimal";
                    }
                    else
                    {
                        naming = type1.Name.ToString();
                    }
                    foreach (var item in list)
                    {
                        Type t = item.GetType();
                        if (naming == "boolean")
                        {
                            json.Append($"<{naming}>{item.ToString().ToLower()}</{naming}>");
                        }
                        else
                        {
                            json.Append($"<{naming}>{item}</{naming}>");
                        }
                    }
                    json.Append($"</{property.Name}>");
                }
                #endregion
                else
                {
                    var value = property.GetValue(obj);
                    json.Append($"<{property.Name}>");
                    json.Append($"{Convert(value, false)}");
                    json.Append($"</{property.Name}>");
                }
            }
            else if (property.PropertyType == typeof(DateTime))                                      ///  DateTime  com.
            {
                //json.Append($"<{field.Name}>{((DateTime)field.GetValue(obj)).ToString("yyyy-MM-ddTHH:mm:ss")}</{field.Name}>\n");
                json.Append($"<{property.Name}>{(DateTime)property.GetValue(obj)}</{property.Name}>\n");
            }
            else if (property.PropertyType == typeof(bool))                                         /// boolen
            {
                json.Append($"<{property.Name}>{property.GetValue(obj)}</{property.Name}>\n");
            }
            else if (property.PropertyType.IsEnum)                                                  /// Enum
            {
                var val = property.GetValue(obj);
                json.Append($"<{property.Name}>{val}</{property.Name}>\n");
            }
            ///   int, sbyte, byte,short, ushort, uint, long, ulong
            else if (property.PropertyType == typeof(int) || property.PropertyType == typeof(sbyte) || property.PropertyType == typeof(byte) || property.PropertyType == typeof(short) || property.PropertyType == typeof(ushort) || property.PropertyType == typeof(uint) || property.PropertyType == typeof(long) || property.PropertyType == typeof(ulong))
            {
                json.Append($"<{property.Name}>{property.GetValue(obj)}</{property.Name}>");
            }
            else if (property.PropertyType == typeof(double))                                        /// double .com
            {
                json.Append($"<{property.Name}>{property.GetValue(obj)}</{property.Name}>");
                #region Formatting
                //double withPoint = (double)field.GetValue(obj);
                //double withoutPoint = Math.Truncate(withPoint);
                //if (withPoint == withoutPoint)
                //{ 
                //    json.Append($"<{field.Name}>{string.Format("{0:0.0}", field.GetValue(obj))}</{field.Name}>");
                //}
                //else
                //{
                //    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                //}
                #endregion
            }
            else if (property.PropertyType == typeof(decimal))                                     /// decimal
            {
                json.Append($"<{property.Name}>{property.GetValue(obj)}</{property.Name}>");
                #region Fomatting
                //decimal withPoint = (decimal)field.GetValue(obj);
                //decimal withoutPoint = Math.Truncate(withPoint);
                //if (withPoint == withoutPoint)
                //{
                //    json.Append($"<{field.Name}>{string.Format("{0:0.0}", field.GetValue(obj))}</{field.Name}>");
                //}
                //else
                //{
                //    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                //}
                #endregion
            }
            else if (property.PropertyType == typeof(float))                                       /// float
            {
                json.Append($"<{property.Name}>{property.GetValue(obj)}</{property.Name}>");
                #region Fomatting
                //float withPoint = (float)field.GetValue(obj);
                //float withoutPoint = (float)Math.Truncate(withPoint);
                //if (withPoint == withoutPoint)
                //{
                //    json.Append($"<{field.Name}>{string.Format("{0:0.0}", field.GetValue(obj))}</{field.Name}>");
                //}
                //else
                //{
                //    json.Append($"<{field.Name}>{field.GetValue(obj)}</{field.Name}>");
                //}
                #endregion
            }
            else if (property.PropertyType == typeof(Guid))                                        /// Guid
            {
                var x = property.GetValue(obj);
                if (x is string)
                {
                    json.Append($"<{property.Name}>{property.GetValue(obj)}</{property.Name}>");

                }
                else
                {
                    json.Append($"<{property.Name}>{property.GetValue(obj).ToString()}</{property.Name}>");
                }
            }

            else if (property.PropertyType.IsValueType && !property.PropertyType.IsPrimitive)            /// custom struct
            {
                var value = property.GetValue(obj);
                json.Append($"<{property.Name}>");
                json.Append($"{Convert(value, false)}");
                json.Append($"</{property.Name}>");
            }
            else
            {
            }
        }
        #endregion
        if (first)
        {
            json.Append($"</{type.Name}>");
        }

        return json.ToString();
    }
}