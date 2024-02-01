using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ToDoList.App.Services;

namespace ToDoList.App.ViewModel
{
	public partial class WelcomeViewModel : ObservableObject
	{
		[ObservableProperty]
		private string _name;

		[ObservableProperty]
		private bool _errorLabelVisibility;

		private readonly IUserData _userData;

		public WelcomeViewModel(IUserData userData)
		{
			_userData = userData;
		}

		[RelayCommand]
		private Task SaveName()
		{
			if (!string.IsNullOrEmpty(Name))
			{
				_userData.SaveName(Name);
				return Shell.Current.GoToAsync(nameof(MainPage));
			}
			else
			{
				ErrorLabelVisibility = true;
				return Task.CompletedTask;
			}
		}

		public void OnAppearing()
		{
			if (_userData.NameExists())
			{
				Shell.Current.GoToAsync(nameof(MainPage));
			}
		}
	}
}
