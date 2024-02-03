namespace ToDoList.App.Services
{
	public interface IUserData
	{
		bool SaveName(string name);
		bool NameExists();
		string GetName();
		bool DeleteUserData();
	}
}
