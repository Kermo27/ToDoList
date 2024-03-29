﻿using ToDoList.App.Services;

namespace ToDoList.App
{
	public partial class MainPage : ContentPage
	{
		int count = 0;
		private IUserData _userData;

		public MainPage(IUserData userData)
		{
			InitializeComponent();
			_userData = userData;
		}

		private void OnCounterClicked(object sender, EventArgs e)
		{
			count++;

			if (count == 1)
				CounterBtn.Text = $"Clicked {count} time";
			else
				CounterBtn.Text = $"Clicked {count} times";

			SemanticScreenReader.Announce(CounterBtn.Text);
		}

		private void GoToClicked(object sender, EventArgs e)
		{
			Shell.Current.GoToAsync(nameof(ToDo));
		}

		private void DeleteJson(object sender, EventArgs e)
		{
			_userData.DeleteUserData();
		}
	}

}
