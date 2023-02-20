using I18N;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;

namespace RegisterLib
{
	public class PetTalk : UserControl
	{
		internal Storyboard s_2;

		internal Storyboard s_3;

		internal Storyboard s_4old;

		internal Storyboard s_4;

		internal Canvas LayoutRoot;

		internal Canvas canvas;

		internal TextBlock t18_SignUP;

		internal TextBlock t18_WithMe;

		internal TextBlock t18_EnterYour;

		internal TextBlock t18_Email;

		internal TextBlock t18_CheckYour;

		internal TextBlock t18_Email1;

		internal Canvas canvas1;

		internal TextBlock t18_Free_Copy1;

		internal TextBlock t18_Free_Copy;

		internal TextBlock t18_Free;

		internal TextBlock t18_ActivateYourAccount;

		internal TextBlock t18_CoolStuff;

		internal TextBlock t18_DrawWorld;

		internal TextBlock t18_DrawFood;

		internal TextBlock t18_DrawFood_Copy;

		private bool _contentLoaded;

		public PetTalk()
		{
			this.InitializeComponent();
			SetStrings.Instatnce().SetControl(this);
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("/RegisterLib;component/PetTalk.xaml", UriKind.Relative));
			this.s_2 = (Storyboard)base.FindName("s_2");
			this.s_3 = (Storyboard)base.FindName("s_3");
			this.s_4old = (Storyboard)base.FindName("s_4old");
			this.s_4 = (Storyboard)base.FindName("s_4");
			this.LayoutRoot = (Canvas)base.FindName("LayoutRoot");
			this.canvas = (Canvas)base.FindName("canvas");
			this.t18_SignUP = (TextBlock)base.FindName("t18_SignUP");
			this.t18_WithMe = (TextBlock)base.FindName("t18_WithMe");
			this.t18_EnterYour = (TextBlock)base.FindName("t18_EnterYour");
			this.t18_Email = (TextBlock)base.FindName("t18_Email");
			this.t18_CheckYour = (TextBlock)base.FindName("t18_CheckYour");
			this.t18_Email1 = (TextBlock)base.FindName("t18_Email1");
			this.canvas1 = (Canvas)base.FindName("canvas1");
			this.t18_Free_Copy1 = (TextBlock)base.FindName("t18_Free_Copy1");
			this.t18_Free_Copy = (TextBlock)base.FindName("t18_Free_Copy");
			this.t18_Free = (TextBlock)base.FindName("t18_Free");
			this.t18_ActivateYourAccount = (TextBlock)base.FindName("t18_ActivateYourAccount");
			this.t18_CoolStuff = (TextBlock)base.FindName("t18_CoolStuff");
			this.t18_DrawWorld = (TextBlock)base.FindName("t18_DrawWorld");
			this.t18_DrawFood = (TextBlock)base.FindName("t18_DrawFood");
			this.t18_DrawFood_Copy = (TextBlock)base.FindName("t18_DrawFood_Copy");
		}
	}
}