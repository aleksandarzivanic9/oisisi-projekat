namespace Core.Serialization
{
    public class Storage<T> where T : ISerializable, new()
    {
        private readonly string _filePath;
        private readonly Serializer<T> _serializer = new Serializer<T>();

        public Storage(string fileName)
        {
            // Gradimo putanju pomocu Path.DirectorySeparatorChar po specifikaciji
            string dataDir = $"..{Path.DirectorySeparatorChar}Core{Path.DirectorySeparatorChar}Data";
            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }
            _filePath = $"{dataDir}{Path.DirectorySeparatorChar}{fileName}";
        }

        public List<T> Load()
        {
            if (!File.Exists(_filePath))
            {
                File.Create(_filePath).Close();
                return new List<T>();
            }

            IEnumerable<string> lines = File.ReadLines(_filePath);
            return _serializer.FromCSV(lines);
        }

        public void Save(List<T> objects)
        {
            string serializedObjects = _serializer.ToCSV(objects);
            File.WriteAllText(_filePath, serializedObjects);
        }
    }
}