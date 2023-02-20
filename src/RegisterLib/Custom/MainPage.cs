using AnimalsWorldClassLibrary1.Framework;
using I18N;
using Interfaces;
using LoginLib.LogServiceReference;
using ShidonniSCCommon;
using System;
using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;

namespace RegisterLib
{
	public class MainPage : UserControl, IShidonni
	{
		private RegisterPage _registerScreenControl;

		private static int _exceptionCount;

		internal Grid LayoutRoot;

		private bool _contentLoaded;

		static MainPage()
		{
		}

		public MainPage()
		{
			this.InitializeComponent();
		}

		public bool Init(Canvas canvas, DBAuthenticationInfo info, EnumLang lang, bool noMailMode, string aff, string newOldPostfix)
		{
			this._registerScreenControl = new RegisterPage(canvas, Application.Current.RootVisual, lang, aff, EnumApplicationMode.SHIDONNI, newOldPostfix);
			return true;
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("/RegisterLib;component/MainPage.xaml", UriKind.Relative));
			this.LayoutRoot = (Grid)base.FindName("LayoutRoot");
		}

		public void OnAppException(string errMsg, Exception e)
		{
			MainPage.OnAppException2(errMsg, e);
		}

		public static void OnAppException2(string errMsg, Exception e)
		{
			MainPage._exceptionCount++;
			if (MainPage._exceptionCount > 2)
			{
				return;
			}
			if (errMsg == null)
			{
				errMsg = "";
			}
			string str = "";
			try
			{
				str = string.Concat(" StackTrace: ", e.StackTrace);
			}
			catch (Exception exception)
			{
			}
			try
			{
				string absoluteUri = HtmlPage.Document.DocumentUri.AbsoluteUri;
				if (absoluteUri.Contains("?"))
				{
					absoluteUri = absoluteUri.Substring(0, absoluteUri.IndexOf("?"));
				}
				string str1 = absoluteUri.Substring(0, absoluteUri.LastIndexOf("/"));
				Binding basicHttpBinding = new BasicHttpBinding();
				EndpointAddress endpointAddress = new EndpointAddress(string.Concat(str1, "/LogService.asmx"));
				LogServiceSoapClient logServiceSoapClient = new LogServiceSoapClient(basicHttpBinding, endpointAddress);
				logServiceSoapClient.WriteExeptionSLogAsync(RegisterPage._id, Global.CurrentAffiliate, errMsg, str);
			}
			catch
			{
			}
		}
	}
}