namespace ToDoList.App;

public partial class ToDoItemDetails : ContentPage
{
	public ToDoItemDetails(ToDoItem selectedItem)
	{
		InitializeComponent();

		BindingContext = selectedItem;
	}
}