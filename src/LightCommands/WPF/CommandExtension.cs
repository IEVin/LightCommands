using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Markup;
using JetBrains.Annotations;

namespace LightCommands.WPF
{
	[MarkupExtensionReturnType( typeof(ICommand) )]
	public class CommandExtension: MarkupExtension
	{
		private readonly Enum en;

		public CommandExtension()
		{
			var isDesignMode = (bool)DesignerProperties.IsInDesignModeProperty.GetMetadata( typeof(DependencyObject) ).DefaultValue;
			if ( !isDesignMode )
				throw new ArgumentException( "Invalid argument. Please use format: Property=\"{m:Command {x:Static local:MyEnum.EnumValue}}\"" );
		}

		public CommandExtension( object en )
			: this() {}

		public CommandExtension( [NotNull] Enum en )
		{
		    this.en = en ?? throw new ArgumentNullException( nameof(en) );
		}

		public override object ProvideValue( IServiceProvider serviceProvider )
		{
			// en == null in designer mode
		    return en?.GetRoutedUICommand();
		}
	}
}