using CommunityToolkit.Mvvm.ComponentModel;

namespace ToDoList.App
{
	public partial class ToDoItem : ObservableObject
	{
		public int ID { get; set; }
		public string DisplayDate => TaskDate.ToString("dd-MM-yyyy HH:mm:ss");

		[ObservableProperty]
		private string? _title;

		[ObservableProperty]
		private string? _description;

		[ObservableProperty]
		private DateTime _taskDate;

		[ObservableProperty]
		private bool _isChecked;
	}
}
