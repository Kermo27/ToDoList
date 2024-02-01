namespace ToDoList.App
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(ToDo), typeof(ToDo));
			Routing.RegisterRoute(nameof(ToDoItemDetails), typeof(ToDoItemDetails));
			Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
		}
	}
}
