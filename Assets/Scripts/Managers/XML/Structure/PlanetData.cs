using System.Collections.Generic;
using System.Runtime.Serialization;
using Bachelor.Utilities;

namespace Bachelor.SerializeData.PlanetInfo
{
    [DataContract(Name = "SolarSystem", Namespace = "")]
    public class PlanetData : ObjectData
    {
        [DataMember(Name = "Planets")]
        private List<Planet> m_PlanetList;

        public List<Planet> PlanetList
        {
            get
            {
                return m_PlanetList;
            }
        }
    }

    [DataContract(Namespace = "")]
    public class Planet
    {
        [DataMember]
        public int Id { get; private set; }

        [DataMember]
        public string Name { get; private set; }

        [DataMember(Name = "Texts")]
        public Texts TextsLine { get; private set; }
    }

    [CollectionDataContract(Name = "Texts", ItemName = "Line", Namespace = "")]
    public class Texts : List<string>
    {
        public List<string> TextsList { get; private set; }
    }
}