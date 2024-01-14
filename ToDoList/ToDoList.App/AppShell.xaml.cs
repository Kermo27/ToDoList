using Microsoft.Maui.Controls;

namespace ToDoList.App
{
	public partial class AppShell : Shell
	{
		public AppShell()
		{
			InitializeComponent();

			Routing.RegisterRoute(nameof(ToDo), typeof(ToDo));
		}
	}
}
