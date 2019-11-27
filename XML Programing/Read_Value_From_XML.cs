using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Testing
{
    class Read_Value_From_XML
    {
        static void Main(string[] args)
        {            
            XmlDocument doc = new XmlDocument();
            doc.Load(System.IO.Path.Combine(Environment.CurrentDirectory, "TestDocument.xml"));            

            XmlNodeList xmlList = doc.SelectNodes("/InputDocument/DeclarationList/DeclarationHeader/Reference[@RefCode='MWB' or @RefCode='TRV' or @RefCode='CAR']");
            foreach (XmlNode xmlNode in xmlList)
            {                
                Console.WriteLine(xmlNode.InnerText);
            }
        }        
    }
}
