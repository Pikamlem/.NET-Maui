using SQLite;

namespace SQLforPika
{
	public class Person
	{
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        

	}
}