using System.Windows.Input;

namespace ToDoList.App
{
	public class CustomCheckbox : CheckBox
	{
		public static readonly BindableProperty CheckedCommandProperty = BindableProperty.Create(nameof(CheckedCommand), typeof(ICommand), typeof(CustomCheckbox), null);

		public ICommand CheckedCommand
		{
			get => (ICommand)GetValue(CheckedCommandProperty);
			set => SetValue(CheckedCommandProperty, value);
		}

		public static readonly BindableProperty CheckedCommandParameterProperty = BindableProperty.Create(nameof(CheckedCommandParameter), typeof(object), typeof(CustomCheckbox), null);

		public object CheckedCommandParameter
		{
			get => GetValue(CheckedCommandParameterProperty);
			set => SetValue(CheckedCommandParameterProperty, value);
		}
		public CustomCheckbox()
		{
			this.CheckedChanged += CustomCheckbox_CheckedChanged;
		}

		private void CustomCheckbox_CheckedChanged(object? sender, CheckedChangedEventArgs e)
		{
			CheckedCommand?.Execute(CheckedCommandParameter);
		}
	}
}
