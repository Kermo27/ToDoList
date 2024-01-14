using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ToDoList.App
{
	public class ToDoItem : INotifyPropertyChanged
	{
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
				if(_description == value) return;

				_description = value;
				OnPropertyChanged();
			}
		}

		public event PropertyChangedEventHandler? PropertyChanged;

		private void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}
	}
}
