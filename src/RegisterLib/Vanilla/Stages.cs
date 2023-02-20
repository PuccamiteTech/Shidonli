using I18N;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RegisterLib
{
	public class Stages : UserControl
	{
		internal Storyboard s_2;

		internal Storyboard s_3;

		internal Storyboard s_4;

		internal Grid LayoutRoot;

		internal Path path1;

		internal Path path;

		internal Path path2;

		internal Path path3;

		internal TextBlock textBlock1;

		internal TextBlock textBlock;

		internal Path path5;

		internal Path path4;

		internal TextBlock textBlock2;

		internal Path path7;

		internal Path path6;

		internal TextBlock textBlock3;

		private bool _contentLoaded;

		public Stages()
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
			Application.LoadComponent(this, new Uri("/RegisterLib;component/Stages.xaml", UriKind.Relative));
			this.s_2 = (Storyboard)base.FindName("s_2");
			this.s_3 = (Storyboard)base.FindName("s_3");
			this.s_4 = (Storyboard)base.FindName("s_4");
			this.LayoutRoot = (Grid)base.FindName("LayoutRoot");
			this.path1 = (Path)base.FindName("path1");
			this.path = (Path)base.FindName("path");
			this.path2 = (Path)base.FindName("path2");
			this.path3 = (Path)base.FindName("path3");
			this.textBlock1 = (TextBlock)base.FindName("textBlock1");
			this.textBlock = (TextBlock)base.FindName("textBlock");
			this.path5 = (Path)base.FindName("path5");
			this.path4 = (Path)base.FindName("path4");
			this.textBlock2 = (TextBlock)base.FindName("textBlock2");
			this.path7 = (Path)base.FindName("path7");
			this.path6 = (Path)base.FindName("path6");
			this.textBlock3 = (TextBlock)base.FindName("textBlock3");
		}
	}
}