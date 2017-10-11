using Bachelor.SerializeData.PlanetInfo;

namespace Bachelor.Managers.XML
{
    public class PlanetContainer : XMLObjectContainer<PlanetData>
    {
        public PlanetContainer(string folderPath, string fileName, bool resourceFile = true) : base(folderPath, fileName, resourceFile)
        {
        }

        public Planet FindPlanet(int findId)
        {
            return m_ObjectData.PlanetList.Find(x => x.Id == findId);
        }
    }
}