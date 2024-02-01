namespace ToDoList.App
{
	public partial class App : Application
	{
		public App()
		{
			InitializeComponent();

			MainPage = new AppShell();

			//if (File.Exists("UserData.json"))
			//{
			//	var jsonString = File.ReadAllText("UserData.json");
			//	var user = JsonSerializer.Deserialize<User>(jsonString);

			//	if (!string.IsNullOrEmpty(user.Name))
			//	{
			//		MainPage = new AppShell();
			//	}
			//}

			//if (MainPage == null)
			//{
			//	MainPage = new WelcomePage();
			//}
		}
	}
}
