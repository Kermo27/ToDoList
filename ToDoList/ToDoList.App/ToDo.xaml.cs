using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ToDoList.App;

public partial class ToDo : ContentPage
{
	public ObservableCollection<string> ToDoItems { get; set; } = new ObservableCollection<string>();
	public ICommand DeleteItemCommand { get; set; }
	public ICommand UpdateItemCommand { get; set; }

	public ToDo()
	{
		InitializeComponent();
		BindingContext = this;
		DeleteItemCommand = new Command<string>(DeleteItem);
		UpdateItemCommand = new Command<string>(UpdateItem);
	}

	private void AddItem(object sender, EventArgs e)
	{
		string toDoItem = ToDoItem.Text;

		ToDoItems.Add(toDoItem);
	}

	private void DeleteItem(string item)
	{
		ToDoItems.Remove(item);
	}

	private void UpdateItem(string item)
	{
		string toDoItemUpdate = ToDoItem.Text;

		ToDoItems.Remove(item);

		ToDoItems.Add(ToDoItem.Text);
	}
}