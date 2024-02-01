using ToDoList.App.ViewModel;

namespace ToDoList.App.View;

public partial class WelcomePage : ContentPage
{
	public WelcomePage()
	{
		InitializeComponent();
		BindingContext = new WelcomeViewModel();
	}

	protected override void OnAppearing()
	{
		base.OnAppearing();
		((WelcomeViewModel)BindingContext).OnAppearing();
	}
}