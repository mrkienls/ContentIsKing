using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

/*
 *
 * https://docs.microsoft.com/en-us/dotnet/csharp/programming-guide/concepts/linq/basic-queries-linq-to-xml
     */

/*
 Luu:  tenpost | trang thai(da post/ chua post) | duong dan file anh
 to XML
 Dung LINQ de truy van
 */
namespace ContentIsKing.DatabaseXML
{
    static public class MainDatabase
    {
      
         static void CreateXMLfile(string path)
        {
            XDocument xmlDocument = new XDocument(
               new XDeclaration("1.0", "utf-8", "yes"),

               new XComment("LINQ To XML Demo"),

               new XElement("Posts",
                   new XElement("Post", new XAttribute("id", 1),
                       new XElement("TrangThai", "0"),
                       new XElement("Content", "xx"),
                       new XElement("PathMedia", "Mumbai")
                   ),
                              new XElement("Post", new XAttribute("id", 1),
                       new XElement("TrangThai", "0"),
                       new XElement("Content", "yy"),
                       new XElement("PathMedia", "Mumbai")
                   ), new XElement("Post", new XAttribute("id", 1),
                       new XElement("TrangThai", "0"),
                       new XElement("Content", "zz"),
                       new XElement("PathMedia", "Mumbai")
                   ), new XElement("Post", new XAttribute("id", 1),
                       new XElement("TrangThai", "0"),
                       new XElement("Content", "mm"),
                       new XElement("PathMedia", "Mumbai")
                   )
                       ));

            xmlDocument.Save(path);
        }



      

         public static XElement readXML(string path)
        {
            XElement result = null;
            XDocument xdoc = XDocument.Load(path);
            try { 
            result = xdoc.Element("Posts").Elements("Post").Where(x => x.Element("TrangThai").Value == "0").First();
            }
            catch { }
            

            return result;
         
        }


        // neu content da co trong file thi return true
        static bool content_exists(string path, string content)
        {
            XDocument xdoc = XDocument.Load(path);
            XElement result = null;
            try
            {
                result = xdoc.Element("Posts").Elements("Post").Where(x => x.Element("Content").Value == content).First();
            } catch
            {

            }
            

            if (result != null) return true;
            else  return false;
        }

         static void Insert(string path,string content,string pathImage)
        {
            string id = DateTime.Now.ToString("yyyyMMddTHHmmss.fff") ;

            XDocument xdoc = XDocument.Load(path);

            xdoc.Element("Posts").Add(
               new XElement("Post", new XAttribute("id", id),
                       new XElement("TrangThai", "0"),
                       new XElement("Content", content),
                       new XElement("PathMedia", pathImage)
                   ));

            xdoc.Save(path);
        }

         static void Delete(string path)
        {
            XDocument xdoc = XDocument.Load(path);
            xdoc.Element("Posts").Elements("Post").Where(x => x.Element("Content").Value == "Kien").Remove();
            xdoc.Save(path);
        }

         public static void Update(string path, string Content, string trangThai)
        {
            XDocument xdoc = XDocument.Load(path);
            xdoc.Element("Posts").Elements("Post").Where(x => x.Element("Content").Value == Content).SingleOrDefault().SetElementValue("TrangThai", trangThai);
           xdoc.Save(path);
       }

         public static void saveXML(string path,string content, string pathMedia)
        {
            XElement node = readXML(path);
          if (!content_exists(path,content)) Insert(path, content, pathMedia);
     //       var content_dbXml = node.Element("Content").Value;
          //  if (content!=content_dbXml) Insert(path,content,pathMedia);
 
        }
    }
}
