using SQLite;
namespace JokeList
{
    public class JokeItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Joke { get; set; }
        public string Punchline { get; set; }
        public bool Delete { get; set; }
    }
}