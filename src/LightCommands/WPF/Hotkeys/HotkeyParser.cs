using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace LightCommands.WPF.Hotkeys
{
	public static class HotkeyParser
	{
		private static readonly KeyConverter keyConv = new KeyConverter();
		private static readonly Dictionary<Key, string> keyToStirng;
		private static readonly Dictionary<string, Key> stringToKey;

	    static HotkeyParser()
	    {
	        var spec = new[]
	                   {
	                       new {Key = Key.Tab, Name = "Tab"},
	                       new {Key = Key.Enter, Name = "Enter"},
	                       new {Key = Key.Oem3, Name = "~"},
	                       new {Key = Key.Oem5, Name = "\\"},
	                       new {Key = Key.OemQuestion, Name = "/"},
	                       new {Key = Key.OemComma, Name = ","},
	                       new {Key = Key.OemPeriod, Name = "."},
	                       new {Key = Key.OemMinus, Name = "-"},
	                       new {Key = Key.OemPlus, Name = "+"},
	                       new {Key = Key.OemOpenBrackets, Name = "["},
	                       new {Key = Key.OemCloseBrackets, Name = "]"},
	                       new {Key = Key.Oem1, Name = ";"},
	                       new {Key = Key.OemQuotes, Name = "'"},
	                       new {Key = Key.PageDown, Name = "PageDown"},
	                       new {Key = Key.PageUp, Name = "PageUp"},
	                       new {Key = Key.Add, Name = "Num+"},
	                       new {Key = Key.Subtract, Name = "Num-"},
	                       new {Key = Key.Divide, Name = "Num/"},
	                       new {Key = Key.Multiply, Name = "Num*"},
	                       new {Key = Key.Decimal, Name = "Num."},
	                   };

	        keyToStirng = spec.ToDictionary(x => x.Key, x => x.Name);
	        stringToKey = spec.ToDictionary(x => x.Name, x => x.Key);
	    }


	    public static string ToString( Key key, ModifierKeys modificator )
		{
			var sb = new StringBuilder();
			if ( (modificator & ModifierKeys.Control) == ModifierKeys.Control )
				sb.Append( "Ctrl+" );
			if ( (modificator & ModifierKeys.Alt) == ModifierKeys.Alt )
				sb.Append( "Alt+" );
			if ( (modificator & ModifierKeys.Shift) == ModifierKeys.Shift )
				sb.Append( "Shift+" );

			return sb.Append( GetString( key ) ).ToString();
		}

		public static HotKeyParams ToHotkeyParams( string key, object parameters = null )
		{
			if ( string.IsNullOrWhiteSpace( key ) )
				return new HotKeyParams();

			var modifier = Enumerable.Empty<string>();


            // skip symbol '+' if it is single
			var lastSepInd = key.LastIndexOf( '+', Math.Max( 0, key.Length - 2 ) );	
			if ( lastSepInd > -1 )
			{
				modifier = key.Remove( lastSepInd ).Split( '+' );
				key = key.Substring( lastSepInd + 1 );
			}

			var modifVal = modifier.Aggregate( ModifierKeys.None, ( res, x ) => res | GetModifier( x ) );
			var keyVal = GetKey( key );

			return new HotKeyParams( keyVal, modifVal, parameters );
		}


	    private static ModifierKeys GetModifier(string key)
	    {
	        switch (key)
	        {
	            case "Ctrl":
	                return ModifierKeys.Control;
	            case "Alt":
	                return ModifierKeys.Alt;
	            case "Shift":
	                return ModifierKeys.Shift;
	            default:
	                return ModifierKeys.None;
	        }
	    }

	    private static Key GetKey( string key )
		{
			if ( key.Length == 1 && '0' <= key[0] && key[0] <= '9' )
				return key[0] - '0' + Key.D0;
			if ( key.Length == 4 && key.StartsWith( "Num" ) && '0' <= key[3] && key[3] <= '9' )
				return key[3] - '0' + Key.NumPad0;

			Key res;
			return stringToKey.TryGetValue( key, out res )
							 ? res
							 : (Key)keyConv.ConvertFromString( key );
		}

		private static string GetString( Key key )
		{
			if ( Key.D0 <= key && key <= Key.D9 )
				return (key - Key.D0).ToString();
			if ( Key.NumPad0 <= key && key <= Key.NumPad9 )
				return "Num" + (key - Key.NumPad0);

			string res;
			return keyToStirng.TryGetValue( key, out res )
							 ? res
							 : keyConv.ConvertToString( key );
		}
	}
}