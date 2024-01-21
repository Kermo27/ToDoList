namespace ToDoList.App;

public partial class ToDoItemDetails : ContentPage
{
	public ToDoItemDetails(ToDoItemDetailsViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}