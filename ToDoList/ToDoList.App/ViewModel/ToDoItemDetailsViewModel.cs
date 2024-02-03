using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using SQLitePCL;

namespace ToDoList.App
{
	[QueryProperty(nameof(Item), nameof(Item))]
	public partial class ToDoItemDetailsViewModel : ObservableObject
	{
		private readonly ToDoItemDatabase _database;

		[ObservableProperty]
		private ToDoItem _item;

		public ToDoItemDetailsViewModel(ToDoItemDatabase database)
		{
			_database = database;
		}

		[RelayCommand]
		private async Task UpdateItem()
		{
			Item.TaskDate = DateTime.Now;

			await _database.SaveItemAsync(new ToDoItemDto
			{
				Id = Item.ID,
				Title = Item.Title,
				Description = Item.Description,
				TaskDate = Item.TaskDate,
				IsChecked = Item.IsChecked,
				ImagePath = Item.ImagePath
			});

			await Shell.Current.GoToAsync("..");
		}

		[RelayCommand]
		private async Task DeleteItem()
		{
			await _database.DeleteItemAsync(Item.ID);
			await Shell.Current.GoToAsync("..");
		}

		[RelayCommand]
		private async Task TakePhoto()
		{
			FileResult photo = null;

			if (MediaPicker.Default.IsCaptureSupported)
			{
				photo = await MediaPicker.Default.CapturePhotoAsync();
			}

			if (photo != null)
			{
				string localFilePath = Path.Combine(FileSystem.AppDataDirectory, photo.FileName);

				using Stream sourceStream = await photo.OpenReadAsync();
				using FileStream localFileStream = File.OpenWrite(localFilePath);

				await sourceStream.CopyToAsync(localFileStream);
				await _database.SaveItemImagePathAsync(Item.ID, photo.FileName);
				
				Item.ImagePath = localFilePath;
			}
		}

		[RelayCommand]
		private async Task PickPhoto()
		{
			FileResult pickedPhoto = await MediaPicker.Default.PickPhotoAsync();

			string uniqueFileName = $"photo_{DateTime.Now:yyyyMMdd_HHmmss}.jpg";

			string destinationPath = Path.Combine(FileSystem.AppDataDirectory, uniqueFileName);
			
			File.Copy(pickedPhoto.FullPath, destinationPath);
			
			await _database.SaveItemImagePathAsync(Item.ID, uniqueFileName);

			Item.ImagePath = destinationPath;
		}
	}
}
