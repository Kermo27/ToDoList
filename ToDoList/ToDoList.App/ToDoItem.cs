using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ToDoList.App
{
	public class ToDoItem : INotifyPropertyChanged
	{
		public int ID { get; set; }
		public string DisplayDate => TaskDate.ToString("dd-MM-yyyy HH:mm:ss");

		private string _title;
		public string Title
		{
			get
			{
				return _title;
			}
			set
			{
				if (_title == value) return;

				_title = value;
				OnPropertyChanged();
			}
		}

		private string _description;
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				if (_description == value) return;

				_description = value;
				OnPropertyChanged();
			}
		}

		private DateTime _taskDate;
		public DateTime TaskDate
		{
			get
			{
				return _taskDate;
			}
			set
			{
				if (_taskDate == value) return;

				_taskDate = value;
				OnPropertyChanged();
				OnPropertyChanged(nameof(DisplayDate));
			}
		}

		private bool _isChecked;
		public bool IsChecked { get; set; }

		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
