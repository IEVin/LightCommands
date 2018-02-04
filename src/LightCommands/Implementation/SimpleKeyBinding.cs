using System.Windows.Controls.Primitives;
using System.Windows.Input;

namespace LightCommands.Implementation
{
	// Skip hotkey in TextBox
	public class SimpleKeyBinding : KeyBinding
	{
		public override InputGesture Gesture
		{
			get { return base.Gesture as KeyGesture; }
			set
			{
				if ( value is SimpleKeyGesture )
				{
					base.Gesture = value;
					return;
				}
				base.Gesture = new SimpleKeyGesture( (KeyGesture)value );
			}
		}
	}

	public class SimpleKeyGesture : KeyGesture
	{
		private readonly KeyGesture gesture;

		public SimpleKeyGesture( KeyGesture gesture )
			: base( Key.None )
		{
			this.gesture = gesture;
		}

		public override bool Matches( object o, InputEventArgs e )
		{
			// skip TextBox input
			if ( e.OriginalSource is TextBoxBase )
				return false;

			return gesture.Matches( o, e );
		}
	}
}