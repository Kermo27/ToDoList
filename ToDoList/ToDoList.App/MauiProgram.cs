using Microsoft.Extensions.Logging;
using ToDoList.App.ViewModel;

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
#endif
			builder.Services.AddSingleton<ToDoItemDatabase>();
			builder.Services.AddSingleton<ToDoItemDetailsViewModel>();
			builder.Services.AddSingleton<ToDoItemDetails>();
			builder.Services.AddSingleton<ToDo>();
			builder.Services.AddSingleton<ToDoViewModel>();
			return builder.Build();
		}
	}
}
