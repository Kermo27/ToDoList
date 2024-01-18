using Microsoft.Extensions.Logging;

namespace ToDoList.App
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

#if DEBUG
			builder.Logging.AddDebug();
			//builder.Services.AddSingleton<TodoListPage>();
			//builder.Services.AddTransient<TodoItemPage>();
#endif

			return builder.Build();
		}
	}
}
