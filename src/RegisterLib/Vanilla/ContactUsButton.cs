using AnimalsWorldClassLibrary1.Utils;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;

namespace RegisterLib
{
	public class ContactUsButton : UserControl
	{
		internal Storyboard s_MO;

		internal Grid LayoutRoot;

		internal Canvas r_ContactUs;

		internal TextBlock t18_ContactUs;

		private bool _contentLoaded;

		public ContactUsButton()
		{
			this.InitializeComponent();
			base.MouseEnter += new MouseEventHandler(this.ContactUsButton_MouseEnter);
			base.MouseLeave += new MouseEventHandler(this.ContactUsButton_MouseLeave);
			base.MouseLeftButtonDown += new MouseButtonEventHandler(this.ContactUsButton_MouseLeftButtonDown);
		}

		private void ContactUsButton_MouseEnter(object sender, MouseEventArgs e)
		{
			this.s_MO.Begin();
		}

		private void ContactUsButton_MouseLeave(object sender, MouseEventArgs e)
		{
			this.s_MO.Stop();
		}

		private void ContactUsButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			HTMLUtils.OpenPopup("pages/Mailer.aspx", "Contact Us");
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("/RegisterLib;component/ContactUsButton.xaml", UriKind.Relative));
			this.s_MO = (Storyboard)base.FindName("s_MO");
			this.LayoutRoot = (Grid)base.FindName("LayoutRoot");
			this.r_ContactUs = (Canvas)base.FindName("r_ContactUs");
			this.t18_ContactUs = (TextBlock)base.FindName("t18_ContactUs");
		}
	}
}