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
		private bool _isNameValid;

		private readonly IUserData _userData;

		public WelcomeViewModel(IUserData userData)
		{
			_userData = userData;
		}

		[RelayCommand]
		private async Task SaveName()
		{
			if (!IsNameValid)
			{
				await Shell.Current.DisplayAlert("Error", "Invalid name", "OK");
				return;
			}

			_userData.SaveName(Name);
			await Shell.Current.GoToAsync(nameof(MainPage));
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
