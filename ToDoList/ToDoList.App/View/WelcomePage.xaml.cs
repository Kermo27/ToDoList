using ToDoList.App.ViewModel;

namespace ToDoList.App.View;

public partial class WelcomePage : ContentPage
{
	public WelcomePage(WelcomeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		((WelcomeViewModel)BindingContext).OnAppearing();
	}
}