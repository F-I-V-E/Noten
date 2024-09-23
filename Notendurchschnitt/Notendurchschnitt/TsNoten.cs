using System.Collections;
using System.IO;
using Newtonsoft.Json;

namespace Notendurchschnitt
{
    class TsNoten : IEnumerable<TsNote>
    {
        [JsonRequired]
        [System.Text.Json.Serialization.JsonPropertyName("Noten")]
        private List<TsNote> liNoten;
        [JsonIgnore]
        public int Anzahl { get => liNoten.Count; }
        public double Durchschnitt { get => liNoten.Count == 0 ? liNoten.Sum(note => note.Note * note.Gewicht) / liNoten.Sum(note => note.Gewicht) : throw new Exception("keine Noten angegeben"); }
        public TsNoten() { liNoten = []; }
        public TsNote Lesen(int index) =>liNoten[index];
        public void Hinzufuegen(double note, double gewicht)
        {
            if (Anzahl < 8)
                liNoten.Add(new TsNote(note,gewicht));
            else
                throw new Exception("Zu viele Noten!");
        }
        public void Loeschen(int index) { liNoten.RemoveAt(index); }
        public IEnumerator<TsNote> GetEnumerator() => liNoten.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public TsNote this[int index] { get => Lesen(index); }
        public void ExportJSON(string path) { File.WriteAllText(path, JsonConvert.SerializeObject(liNoten, Formatting.Indented)); }
        public void ImportJSON(string path) { liNoten = JsonConvert.DeserializeObject<List<TsNote>>(File.ReadAllText(path)); }
    }
}
