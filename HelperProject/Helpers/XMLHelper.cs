using System.IO;
using System.Xml.Serialization;

namespace HelperProject.Helpers
{
    public class XMLHelper<T>
    {
        XmlSerializer mySerializer;
        string ClassName;
        string BaseDirectory;

        public XMLHelper()
        {
            mySerializer = new XmlSerializer(typeof(T));
            BaseDirectory = "";
        }

        public XMLHelper(string myBaseDirectory)
        {
            mySerializer = new XmlSerializer(typeof(T));
            BaseDirectory = myBaseDirectory;
        }

        public string GetFullFilePath(T myObj)
        {
            ClassName = myObj.GetType().Name;
            var fullPath = BaseDirectory + @"\" + ClassName + ".xml";
            DirectoryHelper.CreatDirectoryIfNotExists(BaseDirectory);
            return fullPath;
        }

        public void Save(T myObj)
        {
            TextWriter myWriter = new StreamWriter(GetFullFilePath(myObj));
            mySerializer.Serialize(myWriter, myObj);
            myWriter.Close();
        }

        public T Load(T myObj)
        {
            XmlSerializer mySerializer = new XmlSerializer(typeof(T));
            TextReader myReader = new StreamReader(GetFullFilePath(myObj));
            T NewObject = (T)mySerializer.Deserialize(myReader);
            myReader.Close();
            return NewObject;
        }
    }
}