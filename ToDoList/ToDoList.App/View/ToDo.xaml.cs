using ToDoList.App.ViewModel;

namespace ToDoList.App;

public partial class ToDo : ContentPage
{
	public ToDo(ToDoViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();

		(BindingContext as ToDoViewModel)?.OnAppearing();
	}
}