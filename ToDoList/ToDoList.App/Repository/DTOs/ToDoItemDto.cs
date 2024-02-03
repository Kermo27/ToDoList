using SQLite;

namespace ToDoList.App
{
	public class ToDoItemDto
	{
		[PrimaryKey, AutoIncrement]
		public int Id { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public DateTime TaskDate { get; set; }
		public bool IsChecked { get; set; }
		public string ImagePath { get; set; }
	}
}
