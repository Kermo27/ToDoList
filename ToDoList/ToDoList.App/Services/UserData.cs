using System.Text.Json;
using ToDoList.App.Extensions;
using ToDoList.App.Model;

namespace ToDoList.App.Services
{
	public class UserData : IUserData
	{
		private readonly string _userDataPath = Path.Combine(FileSystem.AppDataDirectory, "UserData.json");
		private User? _user;
		public string GetName()
		{
			if (_user != null)
			{
				return _user.Name;
			}

			if (File.Exists(_userDataPath))
			{
				var jsonString = File.ReadAllText(_userDataPath);
				var _user = JsonSerializer.Deserialize<User>(jsonString);

				if (_user != null)
				{
					return _user.Name;
				}
			}
			return string.Empty;
		}

		public bool NameExists()
		{
			return !string.IsNullOrEmpty(GetName());
		}

		public bool SaveName(string name)
		{
			if (!name.IsNullOrEmpty())
			{
				if (_user == null)
				{
					_user = new User();
				}

				_user.Name = name;
				string jsonString = JsonSerializer.Serialize(_user);
				File.WriteAllText(_userDataPath, jsonString);
				return true;
			}

			return false;
		}
	}
}
