using AnimalsWorldClassLibrary1.Framework;
using I18N;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RegisterLib
{
	public class ChooseName : UserControl
	{
		internal Storyboard s_MONext;

		internal Storyboard s_2;

		internal Storyboard s_3;

		internal Storyboard s_4;

		internal Storyboard s_MOBack;

		internal Storyboard s_Heb;

		internal Storyboard s_Promotion;

		internal Canvas LayoutRoot;

		internal Path path4;

		internal Path path5;

		internal Canvas r_Name;

		internal TextBlock t18_ChooseUserName;

		internal TextBox r_Username;

		internal TextBlock t18_ChoosePassword;

		internal PasswordBox r_Password;

		internal PasswordBox r_ReEnterPassword;

		internal TextBlock t18_reEnterPassword;

		internal Canvas r_Email;

		internal TextBlock t18_EmailAddress;

		internal Canvas canvas2;

		internal TextBlock t18_Iagree;

		internal Path path17;

		internal CheckBox r_IagreeTerms;

		internal Canvas canvas1;

		internal TextBlock t18_TermsOfUse;

		internal TextBox r_EmailAddress;

		internal Canvas r_Activate;

		internal TextBlock t18_Hi;

		internal TextBlock t18_Before_you_play;

		internal TextBlock r_EmailAddressRep;

		internal TextBlock t18_EmailAddress_Copy3;

		internal TextBlock t18_EmailAddress_Copy;

		internal TextBlock t18_AnActivation;

		internal Canvas canvas;

		internal Path path6;

		internal Path path7;

		internal Path path8;

		internal Canvas r_NextButton;

		internal Path path;

		internal Path path1;

		internal TextBlock t18_OK;

		internal TextBlock t18_Next;

		internal Canvas r_BackButton;

		internal Path path2;

		internal Path path3;

		internal TextBlock t18_Back;

		internal Viewbox viewbox;

		internal Path path9;

		internal Viewbox viewbox1;

		internal Path path10;

		internal Path path11;

		internal Path path13;

		internal Path path14;

		internal Path path12;

		internal Viewbox viewbox2;

		internal Path path15;

		internal Path path16;

		internal TextBlock t18_It_sFree;

		internal Image image;

		private bool _contentLoaded;

		public ChooseName()
		{
			this.InitializeComponent();
			SetStrings.Instatnce().SetControl(this);
			this.r_NextButton.MouseEnter += new MouseEventHandler(this.r_NextButton_MouseEnter);
			this.r_NextButton.MouseLeave += new MouseEventHandler(this.r_NextButton_MouseLeave);
			this.r_BackButton.MouseEnter += new MouseEventHandler(this.r_BackButton_MouseEnter);
			this.r_BackButton.MouseLeave += new MouseEventHandler(this.r_BackButton_MouseLeave);
			this.t18_TermsOfUse.MouseLeftButtonDown += new MouseButtonEventHandler(this.t18_TermsOfUse_MouseLeftButtonDown);
			if (I18NGlobal.CurLang == EnumLang.HEBREW)
			{
				this.s_Heb.Begin();
			}
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("/RegisterLib;component/ChooseName.xaml", UriKind.Relative));
			this.s_MONext = (Storyboard)base.FindName("s_MONext");
			this.s_2 = (Storyboard)base.FindName("s_2");
			this.s_3 = (Storyboard)base.FindName("s_3");
			this.s_4 = (Storyboard)base.FindName("s_4");
			this.s_MOBack = (Storyboard)base.FindName("s_MOBack");
			this.s_Heb = (Storyboard)base.FindName("s_Heb");
			this.s_Promotion = (Storyboard)base.FindName("s_Promotion");
			this.LayoutRoot = (Canvas)base.FindName("LayoutRoot");
			this.path4 = (Path)base.FindName("path4");
			this.path5 = (Path)base.FindName("path5");
			this.r_Name = (Canvas)base.FindName("r_Name");
			this.t18_ChooseUserName = (TextBlock)base.FindName("t18_ChooseUserName");
			this.r_Username = (TextBox)base.FindName("r_Username");
			this.t18_ChoosePassword = (TextBlock)base.FindName("t18_ChoosePassword");
			this.r_Password = (PasswordBox)base.FindName("r_Password");
			this.r_ReEnterPassword = (PasswordBox)base.FindName("r_ReEnterPassword");
			this.t18_reEnterPassword = (TextBlock)base.FindName("t18_reEnterPassword");
			this.r_Email = (Canvas)base.FindName("r_Email");
			this.t18_EmailAddress = (TextBlock)base.FindName("t18_EmailAddress");
			this.canvas2 = (Canvas)base.FindName("canvas2");
			this.t18_Iagree = (TextBlock)base.FindName("t18_Iagree");
			this.path17 = (Path)base.FindName("path17");
			this.r_IagreeTerms = (CheckBox)base.FindName("r_IagreeTerms");
			this.canvas1 = (Canvas)base.FindName("canvas1");
			this.t18_TermsOfUse = (TextBlock)base.FindName("t18_TermsOfUse");
			this.r_EmailAddress = (TextBox)base.FindName("r_EmailAddress");
			this.r_Activate = (Canvas)base.FindName("r_Activate");
			this.t18_Hi = (TextBlock)base.FindName("t18_Hi");
			this.t18_Before_you_play = (TextBlock)base.FindName("t18_Before_you_play");
			this.r_EmailAddressRep = (TextBlock)base.FindName("r_EmailAddressRep");
			this.t18_EmailAddress_Copy3 = (TextBlock)base.FindName("t18_EmailAddress_Copy3");
			this.t18_EmailAddress_Copy = (TextBlock)base.FindName("t18_EmailAddress_Copy");
			this.t18_AnActivation = (TextBlock)base.FindName("t18_AnActivation");
			this.canvas = (Canvas)base.FindName("canvas");
			this.path6 = (Path)base.FindName("path6");
			this.path7 = (Path)base.FindName("path7");
			this.path8 = (Path)base.FindName("path8");
			this.r_NextButton = (Canvas)base.FindName("r_NextButton");
			this.path = (Path)base.FindName("path");
			this.path1 = (Path)base.FindName("path1");
			this.t18_OK = (TextBlock)base.FindName("t18_OK");
			this.t18_Next = (TextBlock)base.FindName("t18_Next");
			this.r_BackButton = (Canvas)base.FindName("r_BackButton");
			this.path2 = (Path)base.FindName("path2");
			this.path3 = (Path)base.FindName("path3");
			this.t18_Back = (TextBlock)base.FindName("t18_Back");
			this.viewbox = (Viewbox)base.FindName("viewbox");
			this.path9 = (Path)base.FindName("path9");
			this.viewbox1 = (Viewbox)base.FindName("viewbox1");
			this.path10 = (Path)base.FindName("path10");
			this.path11 = (Path)base.FindName("path11");
			this.path13 = (Path)base.FindName("path13");
			this.path14 = (Path)base.FindName("path14");
			this.path12 = (Path)base.FindName("path12");
			this.viewbox2 = (Viewbox)base.FindName("viewbox2");
			this.path15 = (Path)base.FindName("path15");
			this.path16 = (Path)base.FindName("path16");
			this.t18_It_sFree = (TextBlock)base.FindName("t18_It_sFree");
			this.image = (Image)base.FindName("image");
		}

		private void r_BackButton_MouseEnter(object sender, MouseEventArgs e)
		{
			this.s_MOBack.Begin();
		}

		private void r_BackButton_MouseLeave(object sender, MouseEventArgs e)
		{
			this.s_MOBack.Stop();
		}

		private void r_NextButton_MouseEnter(object sender, MouseEventArgs e)
		{
			this.s_MONext.Begin();
		}

		private void r_NextButton_MouseLeave(object sender, MouseEventArgs e)
		{
			this.s_MONext.Stop();
		}

		private void t18_TermsOfUse_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			string absoluteUri = HtmlPage.Document.DocumentUri.AbsoluteUri;
			string str = string.Concat(absoluteUri.Substring(0, absoluteUri.LastIndexOf("/")), "/Pages/TermsOfUse.aspx?lang=", Global.CurLanguage);
			HtmlWindow window = HtmlPage.Window;
			object[] objArray = new object[] { str };
			window.Invoke("showModalPopUp", objArray);
		}
	}
}