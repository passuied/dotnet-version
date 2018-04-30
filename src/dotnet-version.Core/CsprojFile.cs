using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace DotNet.Versioning.Core
{
    public class CsprojFile 
    {
        private readonly string _path;
        private readonly XDocument _xmlDoc;

        public CsprojFile(string path)
        {
            this._path = path;
            this._xmlDoc = XDocument.Load(path);

            if (_xmlDoc.Root.Name != "Project" || _xmlDoc.Root.Attribute("Sdk") == null || _xmlDoc.Root.Attribute("Sdk").Value != "Microsoft.NET.Sdk")
            {
                throw new ArgumentException($"Invalid Csproj file '{path}'. File should be a valid Microsoft.NET.Sdk file.");
            }
        }

        private static readonly string _assemblyVersionXName = "AssemblyVersion";
        private static readonly string _propertyGroupXPath = "//Project/PropertyGroup";
        private static readonly string _assemblyVersionXPath = $"{_propertyGroupXPath}/{_assemblyVersionXName}";
        public string AssemblyVersion
        {
            get
            {
                var assemblyVersionElt = _xmlDoc.XPathSelectElement(_assemblyVersionXPath);
                if (assemblyVersionElt != null)
                {
                    return assemblyVersionElt.Value;
                }
                else
                {
                    return "1.0.0";
                }
            }
            set
            {
                var assemblyVersionElt = _xmlDoc.XPathSelectElement(_assemblyVersionXPath);
                if (assemblyVersionElt != null)
                {
                    assemblyVersionElt.Value = value;
                }
                else
                {
                    var propertyGroupElt = _xmlDoc.XPathSelectElement(_propertyGroupXPath);
                    if (propertyGroupElt != null)
                    {
                        propertyGroupElt.Add(new XElement(_assemblyVersionXName, value));
                    }
                }
            }
        }

        public void Save()
        {
            _xmlDoc.Save(_path);
        }


    }
}
