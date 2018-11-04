using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
      
        public static void CreateXMLfile(string path)
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
            result = xdoc.Element("Posts").Elements("Post").Where(x => x.Element("TrangThai").Value == "0").First();
            return result;
         
        }

        public static void Insert(string path,string content,string pathImage)
        {
            XDocument xdoc = XDocument.Load(path);

            xdoc.Element("Posts").Add(
               new XElement("Post", new XAttribute("id", 1),
                       new XElement("TrangThai", "0"),
                       new XElement("Content", content),
                       new XElement("PathMedia", pathImage)
                   ));

            xdoc.Save(path);
        }

        public static void Delete(string path)
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
    }
}
