using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Threading;

namespace RegisterLib
{
	public class App : Application
	{
		private bool _contentLoaded;

		public App()
		{
			base.Startup += new StartupEventHandler(this.Application_Startup);
			base.Exit += new EventHandler(this.Application_Exit);
			base.UnhandledException += new EventHandler<ApplicationUnhandledExceptionEventArgs>(this.Application_UnhandledException);
			this.InitializeComponent();
		}

		private void Application_Exit(object sender, EventArgs e)
		{
		}

		private void Application_Startup(object sender, StartupEventArgs e)
		{
			base.RootVisual = new MainPage();
		}

		private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
		{
			if (!Debugger.IsAttached)
			{
				e.Handled = true;
				Deployment.Current.Dispatcher.BeginInvoke(() => this.ReportErrorToDOM(e));
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
			Application.LoadComponent(this, new Uri("/RegisterLib;component/App.xaml", UriKind.Relative));
		}

		private void ReportErrorToDOM(ApplicationUnhandledExceptionEventArgs e)
		{
			try
			{
				string str = string.Concat(e.ExceptionObject.Message, e.ExceptionObject.StackTrace);
				str = str.Replace('\"', '\'').Replace("\r\n", "\\n");
				HtmlPage.Window.Eval(string.Concat("throw new Error(\"Unhandled Error in Silverlight Application ", str, "\");"));
			}
			catch (Exception exception)
			{
			}
		}
	}
}