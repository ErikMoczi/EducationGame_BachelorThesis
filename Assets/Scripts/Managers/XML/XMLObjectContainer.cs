using System.IO;
using System.Runtime.Serialization;
using UnityEngine;
using Bachelor.Utilities;
using System;

namespace Bachelor.Managers.XML
{
    public class XMLObjectContainer<T> : ObjectContainer<T> where T : ObjectData
    {
        private static readonly string m_ResourcesPath = Path.Combine(Application.dataPath, "Resources");

        public XMLObjectContainer(
            string filePath,
            string fileName,
            bool resourceFile = false
        ) : base(
            resourceFile ? TryLoadFromResources(filePath, fileName) : LoadFromFile(filePath, fileName)
        )
        {
        }

        private static T TryLoadFromResources(string folderPath, string fileName)
        {
#if !UNITY_EDITOR
            try
            {
                return LoadFromFile(Path.Combine(m_ResourcesPath, folderPath), fileName);
            }  
            catch
            {
                return LoadFromResources(folderPath, fileName);
            }
#else
            return LoadFromResources(folderPath, fileName);
#endif
        }

        private static T LoadFromResources(string folderPath, string fileName)
        {
            var serializer = new DataContractSerializer(typeof(T));
            var asset = Resources.Load<TextAsset>(Path.Combine(folderPath, Path.GetFileNameWithoutExtension(fileName).ToString()));

            if (asset == null)
            {
                throw new ArgumentException("Can't find resource");
            }

            using (var stream = new MemoryStream(asset.bytes))
            {
                return (T)serializer.ReadObject(stream);
            }
        }

        private static T LoadFromFile(string folderPath, string fileName)
        {
            var serializer = new DataContractSerializer(typeof(T));
            using (var stream = new FileStream(Path.Combine(folderPath, fileName), FileMode.Open))
            {
                return (T)serializer.ReadObject(stream);
            }
        }
    }
}