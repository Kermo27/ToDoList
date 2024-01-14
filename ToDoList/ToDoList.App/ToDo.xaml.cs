using System.Collections.ObjectModel;
using System.Windows.Input;

namespace ToDoList.App;

public partial class ToDo : ContentPage
{
	public ObservableCollection<ToDoItem> ToDoItems { get; set; } = new ObservableCollection<ToDoItem>();
	public ICommand DeleteItemCommand { get; set; }
	public ICommand UpdateItemCommand { get; set; }

	public string Title { get; set; }
	public string Description { get; set; }

	public ToDo()
	{
		InitializeComponent();
		BindingContext = this;
		DeleteItemCommand = new Command<ToDoItem>(DeleteItem);
		UpdateItemCommand = new Command<ToDoItem>(UpdateItem);
	}

	private void AddItem(object sender, EventArgs e)
	{
		ToDoItem toDoItemModel = new ToDoItem();
		toDoItemModel.Title = Title;
		toDoItemModel.Description = Description;

		ToDoItems.Add(toDoItemModel);
	}

	private void DeleteItem(ToDoItem item)
	{
		ToDoItems.Remove(item);
	}

	private void UpdateItem(ToDoItem item)
	{
		item.Title = Title;
		item.Description = Description;
	}
}