using System.Runtime.CompilerServices;
using System.Text.Json;
using System.Windows.Input;
using ToDoList.App.Model;

namespace ToDoList.App.ViewModel
{
	public class WelcomeViewModel : INotifyPropertyChanged
	{
		private string _name;
		public string Name
		{
			get => _name;
			set
			{
				_name = value;
				OnPropertyChanged();
			}
		}

		public ICommand SaveNameCommand => new Command(() => SaveName());

		public event PropertyChangedEventHandler PropertyChanged;

		private bool _errorLabelVisibility;
		public bool ErrorLabelVisibility
		{
			get => _errorLabelVisibility;
			set
			{
				_errorLabelVisibility = value;
				OnPropertyChanged();
			}
		}

		protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}

		private async void SaveName()
		{
			var user = new User { Name = Name };
			if (user.Name != null)
			{
				string jsonString = JsonSerializer.Serialize(user);
				File.WriteAllText("UserData.json", jsonString);
				// Nie mam zielonego pojęcia dlaczego to działa zamiast await Shell.Current.GoToAsync(nameof(MainPage));
				Application.Current.MainPage = new AppShell();
			}
			else
			{
				ErrorLabelVisibility = true;
			}
		}
	}
}
