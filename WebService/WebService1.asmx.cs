using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Xml;
using System.Xml.Schema;

namespace WebService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {       

        [WebMethod(CacheDuration = 30)]
        public int PostNotify(XmlDocument xml)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(System.IO.Path.Combine(Environment.CurrentDirectory, "Webservice_Payload.xml"));
            
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.ValidationType = ValidationType.Schema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessInlineSchema;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ProcessSchemaLocation;
            settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
            settings.ValidationEventHandler += new ValidationEventHandler(ValidationCallBack);
           
            XmlReader reader = XmlReader.Create("inlineSchema.xml", settings);            
            while (reader.Read())
            {
                return 0;
            }

            XmlNodeList nodelist = doc.SelectNodes("//InputDocument/DeclarationList/Declaration[@Command<>'DEFAULT']");
            foreach (XmlNode xmlNode in nodelist)
            {
                return -1;
            }

            XmlNodeList nodes = doc.GetElementsByTagName("SiteID");
            foreach (XmlNode node in nodes)
            {
                if (node.InnerText.ToString() != "DUB")
                {
                    return -2;
                }
            }

            return 0;
        }
       
        private static void ValidationCallBack(object sender, ValidationEventArgs args)
        {
            if (args.Severity == XmlSeverityType.Warning)
                Console.WriteLine("\tWarning: Matching schema not found.  No validation occurred." + args.Message);
            else
                Console.WriteLine("\tValidation error: " + args.Message);

        }
    }
}
