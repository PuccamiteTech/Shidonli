using AnimalsWorldClassLibrary1;
using AnimalsWorldClassLibrary1.Animal;
using AnimalsWorldClassLibrary1.Assets;
using AnimalsWorldClassLibrary1.AuthenticationSrv;
using AnimalsWorldClassLibrary1.Editor;
using AnimalsWorldClassLibrary1.Framework;
using AnimalsWorldClassLibrary1.Games;
using AnimalsWorldClassLibrary1.Gifts;
using AnimalsWorldClassLibrary1.GoogleAnalytics;
using AnimalsWorldClassLibrary1.MailSrv;
using AnimalsWorldClassLibrary1.MusicEffects;
using AnimalsWorldClassLibrary1.ServerAPI;
using AnimalsWorldClassLibrary1.Toys;
using AnimalsWorldClassLibrary1.World.Common;
using I18N;
using LoginLib.LogServiceReference;
using ShidonniSCCommon;
using ShidonniSCCommon.LoadXap;
using ShidonniSCCommon.Utils;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.Windows;
using System.Windows.Browser;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace RegisterLib
{
	public class RegisterPage : UserControl
	{
		private static RegisterPage _thMainScreen;

		private IEditor _editFrameControl;

		private DispatcherTimer _timer;

		private BasicWorldControl4 _world;

		private object _loadUserData;

		private object _loadUserData2;

		private InkAnimal _animal;

		private PolyLinesPt _polylines;

		private Queue<MovementType> _qmt = new Queue<MovementType>();

		private AuthenticateServiceSoapClient _authenticationProxy;

		private ShidonniSCCommon.DBAuthenticationInfo _info;

		private static DateTime MIN_DATE;

		private bool _nextEnabled = true;

		private int _stage = 1;

		private string _oldName = "";

		private string _name = "";

		private string _password = "";

		private string _rePassword = "";

		private string _oldEmail = "";

		private string _email = "";

		private string _authenticationHost = "";

		private string _mailService;

		private bool _agreeChecked;

        private bool _doAttemptLoad;

		private string _oldNewPostfix = "";

		internal static string _id;

		private int _abPromotion;

		private long _worldId;

		private DateTime _time = DateTime.Now;

		internal Canvas LayoutRoot;

		internal Canvas r_SkyPos;

		internal Canvas r_Pet;

		internal Stages r_Stages;

		internal ChooseName r_ChooseName;

		internal PetTalk r_PetTalk;

		internal LowerLeftButtons r_Menu;

		internal ContactUsButton r_ContactUs;

		internal Canvas r_SignUp;

		internal Canvas r_Games;

		internal Canvas r_background;

		internal Canvas r_PlayQuest;

		internal Canvas r_PlayEggCatcher;

		internal Canvas r_PlayJumpTo4;

		internal Canvas r_GamePos;

		private bool _contentLoaded;

		private Canvas EditorCanvas
		{
			get
			{
				return Global.MainCanvas;
			}
		}

		private bool PlayWorld
		{
			get
			{
				if (this._world == null)
				{
					return false;
				}
				return this._world.Play;
			}
			set
			{
				if (this._world == null)
				{
					return;
				}
				this._world.Play = value;
				if (value)
				{
					MusicPlayer.Instance().Play(this.WorldMusicFile);
					return;
				}
				MusicPlayer.Instance().StopFile(this.WorldMusicFile);
			}
		}

		private string WorldMusicFile
		{
			get
			{
				return "sky.wma";
			}
		}

		static RegisterPage()
		{
			RegisterPage._thMainScreen = null;
			RegisterPage.MIN_DATE = new DateTime(1974, 10, 4);
			RegisterPage._id = "";
		}

		public RegisterPage(Canvas mainCanvas, UIElement kbEvents, EnumLang lang, string currentAffiliate, EnumApplicationMode appMode, string oldNewPostfix)
		{
			this.InitializeComponent();
			SetStrings.Instatnce().SetControl(this);
			string str = "not really required";
			Global.CurrentAffiliate = currentAffiliate;
			Global.DownloadUrl = str;
			Global.CurLanguage = lang;
			Global.CurApplicationMode = appMode;
			Global.RegisterState = true;
			this._oldNewPostfix = oldNewPostfix;
			RegisterPage._id = DateTime.Now.ToString("mmssffff");
			RegisterPage._thMainScreen = this;
			RegisterPage.InitGame(mainCanvas, kbEvents);
			this.InitWorld();
			EditorToolsList.Instance().InitDefault();
			if (HtmlPage.Window.GetProperty("registerFormLoad") != null)
			{
				try
				{
					HtmlPage.Window.CreateInstance("registerFormLoad", null);
				}
				catch (Exception exception)
				{
				}
			}
			this.InitProxy();
			this._timer = new DispatcherTimer()
			{
				Interval = TimeSpan.FromSeconds(6)
			};
			this.r_ChooseName.r_NextButton.MouseLeftButtonDown += new MouseButtonEventHandler(this.r_NextButton_MouseLeftButtonDown);
			this.r_ChooseName.r_BackButton.MouseLeftButtonDown += new MouseButtonEventHandler(this.r_BackButton_MouseLeftButtonDown);
			this.r_ChooseName.r_IagreeTerms.Checked += new RoutedEventHandler(this.r_IagreeTerms_Checked);
			this.r_ChooseName.r_IagreeTerms.Unchecked += new RoutedEventHandler(this.r_IagreeTerms_Unchecked);
			this.r_PlayQuest.MouseLeftButtonDown += new MouseButtonEventHandler(this.r_PlayQuest_MouseLeftButtonDown);
			this.r_PlayEggCatcher.MouseLeftButtonDown += new MouseButtonEventHandler(this.r_PlayEggCatcher_MouseLeftButtonDown);
			this.r_PlayJumpTo4.MouseLeftButtonDown += new MouseButtonEventHandler(this.r_PlayJumpTo4_MouseLeftButtonDown);
			this.r_SignUp.MouseLeftButtonDown += new MouseButtonEventHandler(this.r_SignUp_MouseLeftButtonDown);
			this.EditAnimal(null);
			this.r_Menu.ShowButtons(true, false);
			LogEvents.Init("NEW_", false);
			this.WriteRegisterStateLog(RegisterPage._id, Global.CurrentAffiliate, "stage1");
			LogEvents.LogRegistration(string.Concat("start", this._oldNewPostfix), Global.CurrentAffiliate);
		}

		private void _authenticationProxy_CheckUserNameExistCompleted(object sender, CheckUserNameExistCompletedEventArgs e)
		{
			try
			{
				if (this._animal.Name.ToUpper() == "STAGEPASS" || !e.Result)
				{
					this.GoToStage3();
				}
				else if (Global.CurLanguage != EnumLang.HEBREW)
				{
					this.Alert("Sorry but this username is already taken. \nPlease select other name");
				}
				else
				{
					this.Alert("שם המשתמש תפוס\n אנא בחר שם אחר");
				}
			}
			catch
			{
				if (Global.CurLanguage != EnumLang.HEBREW)
				{
					this.Alert("Sorry but this username is already taken. \nPlease select other name");
				}
				else
				{
					this.Alert("שם המשתמש תפוס\n אנא בחר שם אחר");
				}
			}
		}

		private void _authenticationProxy_RegisterCompleted(object sender, RegisterCompletedEventArgs e)
		{
			AnimalsWorldClassLibrary1.AuthenticationSrv.DBAuthenticationInfo result = e.Result;
			if (result.Username.StartsWith("Error: "))
			{
				this.Alert(result.Username.Substring(7));
				return;
			}
			this._info = new ShidonniSCCommon.DBAuthenticationInfo()
			{
				Id = result.Id,
				Username = result.Username,
				ServerIP = result.ServerIP,
				LastEntrance = result.LastEntrance,
				Password = result.Password
			};
			this.InitServerMgr();
			if (HtmlPage.Window.GetProperty("registerFormComplete") != null)
			{
				try
				{
					HtmlPage.Window.CreateInstance("registerFormComplete", null);
				}
				catch (Exception exception)
				{
				}
			}
			this.SendActivationMail();
			this.SetAffiliation(this._info.Id, this._info.ServerUtc);
			long id = this._info.Id;
			this.WriteRegisterLog(id.ToString(), Global.CurrentAffiliate, "");
		}

		private void _editFrameControl_OnCloseAnimal(object sender, bool ok)
		{
			if (this._editFrameControl == null)
			{
				return;
			}
			try
			{
				this._editFrameControl.iOnClose -= new iEditorOnCloseDelegate(this._editFrameControl_OnCloseAnimal);
			}
			catch (Exception exception)
			{
			}
			try
			{
				if (ok)
				{
					this._polylines = this._editFrameControl.iPolyLines;
				}
				if (this._polylines != null)
				{
					this.AddAnimalToWorld();
				}
				this._timer.Tick += new EventHandler(this._timer_Tick);
				this._timer.Start();
				this._stage++;
				this.WriteRegisterStateLog(RegisterPage._id, Global.CurrentAffiliate, "stage2");
				LogEvents.LogRegistration(string.Concat("goto_stage2", this._oldNewPostfix), Global.CurrentAffiliate);
			}
			catch (Exception exception2)
			{
				Exception exception1 = exception2;
				MainPage.OnAppException2(exception1.Message, exception1);
			}
			this.EditorCanvas.Children.Remove(this._editFrameControl.iEditorUi);
			this._editFrameControl = null;
			this.PlayWorld = true;

            ParseCheats();
		}

        private void ParseCheats()
        {
            string name = this._animal.Name.ToUpper();

            if (name.StartsWith("GAME"))
            {
                if (name.Contains("01"))
                {
                    OpenGame(EnumGameType.BEJEWELED);
                }
                else if (name.Contains("02"))
                {
                    OpenGame(EnumGameType.BREAK_OUT);
                }
                else if (name.Contains("03"))
                {
                    OpenGame(EnumGameType.BRICKS);
                }
                else if (name.Contains("04"))
                {
                    OpenGame(EnumGameType.BUBBLE_SHOOTER);
                }
                else if (name.Contains("05"))
                {
                    OpenGame(EnumGameType.CONNECT_FOUR);
                }
                else if (name.Contains("06"))
                {
                    OpenGame(EnumGameType.CRAZY_TAXI);
                }
                else if (name.Contains("07"))
                {
                    OpenGame(EnumGameType.EGG_CATCHER);
                }
                else if (name.Contains("08"))
                {
                    OpenGame(EnumGameType.GOBLINS);
                }
                else if (name.Contains("09"))
                {
                    OpenGame(EnumGameType.GOBLINS_1);
                }
                else if (name.Contains("10"))
                {
                    OpenGame(EnumGameType.HIVE);
                }
                else if (name.Contains("11"))
                {
                    OpenGame(EnumGameType.ISLAND);
                }
                else if (name.Contains("12"))
                {
                    OpenGame(EnumGameType.MEMORY);
                }
                else if (name.Contains("13"))
                {
                    OpenGame(EnumGameType.PIXOS);
                }
                else if (name.Contains("14"))
                {
                    OpenGame(EnumGameType.PUZZLE);
                }
                else if (name.Contains("15"))
                {
                    OpenGame(EnumGameType.QUEST);
                }
                else if (name.Contains("16"))
                {
                    OpenGame(EnumGameType.RESTAURANT);
                }
                else if (name.Contains("17"))
                {
                    OpenGame(EnumGameType.ROPES_AND_LADDERS);
                }
                else if (name.Contains("18"))
                {
                    OpenGame(EnumGameType.SEA_GUARD);
                }
                else if (name.Contains("19"))
                {
                    OpenGame(EnumGameType.SIMON);
                }
                else if (name.Contains("20"))
                {
                    OpenGame(EnumGameType.SNAKE);
                }
                else if (name.Contains("21"))
                {
                    OpenGame(EnumGameType.SPACE);
                }
                else if (name.Contains("22"))
                {
                    OpenGame(EnumGameType.TRAFFIC_JAM);
                }
                else if (name.Contains("23"))
                {
                    OpenGame(EnumGameType.XONIX);
                }
            }
            else if (name.StartsWith("CAFF"))
            {
                if (name.Contains("01"))
                {
                    Global.CurrentAffiliate = String.Empty;
                }
                else if (name.Contains("02"))
                {
                    Global.CurrentAffiliate = "KA2";
                }
            }

            if (name.Contains("SAVE"))
            {
                SavePolylines();
            }

            if (name.Contains("LOAD"))
            {
                _doAttemptLoad = true;
                Alert("Enter the design's data in the user name field, then press next.");
            }
            else
            {
                _doAttemptLoad = false;
            }

            if (name.Contains("GLOW"))
            {
                this._animal.DoGlow();
            }

            if (name.Contains("POOF"))
            {
                this._animal.DoMagic();
            }

            if (name.Contains("DUPE"))
            {
                this._world.Add(this._animal.Clone());
            }
        }

        private void SavePolylines()
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                using (BinaryWriter writer = new BinaryWriter(memoryStream))
                {
                    this._polylines.Write(writer);
                    writer.Flush();
                    this._animal.Name = Convert.ToBase64String(memoryStream.ToArray());
                    Alert("Go back, and copy the animal's name to save the design.");
                }
            }
        }

        private void LoadPolyLines(string base64)
        {
            this._polylines.Clear();

            if (base64.Length == 0 || base64.Contains(" ") || base64.Length % 4 != 0)
            {
                Alert("The design's data is invalid.");
                return;
            }

            using (MemoryStream memoryStream = new MemoryStream(Convert.FromBase64String(base64)))
            {
                using (BinaryReader reader = new BinaryReader(memoryStream))
                {
                    if (this._polylines.Read(reader))
                    {
                        Alert("The design's data loaded. Go back.");
                    }
                    else
                    {
                        Alert("The design's data failed to load.");
                    }
                }
            }
        }

		private void _timer_Tick(object sender, EventArgs e)
		{
			if (Global.CurrentAffiliate != "KA2")
			{
				this.StartRegistration();
			}
			else
			{
				this.r_Games.Visibility = System.Windows.Visibility.Visible;
				this.r_SignUp.Visibility = System.Windows.Visibility.Visible;
			}
			if (this._animal != null && this._animal._Brain != null)
			{
				this._animal._Brain.CheckForMusicPlayer = false;
				if (this._animal._Brain._currentOp != null)
				{
					this._animal._Brain._currentOp.Done();
				}
				this._animal._Brain._currentOp = new DanceAtParty(this._animal._transform4, DateTime.Now + TimeSpan.FromMinutes(15), this._animal, false);
			}
			this.r_PetTalk.s_2.Begin();
			this._timer.Stop();
			this._timer.Tick -= new EventHandler(this._timer_Tick);
		}

		private void ABPromotion()
		{
			try
			{
				if (Global.CurrentAffiliate == "KA2")
				{
					this._abPromotion = 1;
					if (DateTime.Now.Minute % 2 != 0)
					{
						this.r_ChooseName.s_Promotion.Begin();
						this._abPromotion = 2;
					}
				}
			}
			catch
			{
			}
		}

		private void AddAnimalToWorld()
		{
			this._animal = new InkAnimal();
			this._animal.SetPolyAnimal(this._polylines);
			this._animal.MyWorld = this._world;
			this._animal.Name = this._editFrameControl.iEditName;
			this._animal.Info.SoundFamily = this._editFrameControl.iAnimalSoundFamily;
			this._animal.SetPosition(new Point(0, 0));
			this._animal.SetDirection(Transform4.DIRECTION.Left);
			this._animal._Brain._currentOp = new Hop(this._animal._transform4);
			this._animal._talk.LastTalked = DateTime.Now - TimeSpan.FromMinutes(10);
			this._animal._talk.SayHi();
			this._animal.AnimalType = this._editFrameControl.iAnimalType;
			this._qmt.Enqueue(MovementType.HOP);
			this._qmt.Enqueue(MovementType.WALK_SHORT);
			this._qmt.Enqueue(MovementType.TURN_180);
			this._qmt.Enqueue(MovementType.WAG);
			this._animal._Brain._currentOp = new MoveSequence(this._animal._Brain, this._animal._transform4, this._qmt, this._animal, this._animal._Brain.YLevel);
			this._animal.MyWorld = this._world;
			this._world.Add(this._animal);
			this._animal.RescaleAnimal();
			PlayEffect.PlayEffectFile("next4.wma", 1);
		}

		public void AddNewWorld()
		{
			DBWorld dBWorld = new DBWorld()
			{
				StarOrder = Worlds.Count + 1
			};
			this._worldId = ServerMgr.Instance().InsertWorld(dBWorld);
		}

		private void Alert(string text)
		{
			AlertControl.Alert(text);
		}

		private void CheckUserNameExists()
		{
			this._authenticationProxy.CheckUserNameExistAsync(this._name);
		}

		private void Close()
		{
			HtmlPage.Window.Navigate(HtmlPage.Document.DocumentUri);
		}

		private void CreateUser()
		{
			AnimalsWorldClassLibrary1.AuthenticationSrv.ArrayOfString arrayOfString = new AnimalsWorldClassLibrary1.AuthenticationSrv.ArrayOfString()
			{
				this._name,
				this._password,
				this._email
			};
			this._authenticationProxy.RegisterAsync(arrayOfString);
		}

		private void DisableNextButton()
		{
			bool? isChecked = this.r_ChooseName.r_IagreeTerms.IsChecked;
			if ((isChecked.GetValueOrDefault() ? false : isChecked.HasValue))
			{
				this.r_ChooseName.r_NextButton.Opacity = 0.5;
				this.r_ChooseName.r_NextButton.MouseLeftButtonDown -= new MouseButtonEventHandler(this.r_NextButton_MouseLeftButtonDown);
				this._nextEnabled = false;
			}
		}

		public void EditAnimal(InkAnimal a)
		{
			if (this._editFrameControl == null)
			{
				this.LoadEditor(a, null);
				return;
			}
			this._editFrameControl.iInitEditor(EnumEditorType.ANIMAL);
			if (a != null)
			{
				this._editFrameControl.iUserData = a;
				this._editFrameControl.iEditName = a.Name;
				this._editFrameControl.iSetInitialPoints(a.MyGraphicObject.OrgPolyLines.Clone(), true);
				this._editFrameControl.iAnimalType = a.AnimalType;
				this._editFrameControl.iAnimalSoundFamily = a.Info.SoundFamily;
			}
			this._editFrameControl.iUserData2 = "register";
			this.EditorCanvas.Children.Add(this._editFrameControl.iEditorUi);
			this._editFrameControl.iOnClose += new iEditorOnCloseDelegate(this._editFrameControl_OnCloseAnimal);
			this._editFrameControl.iSetPosition(0, 0, this.EditorCanvas.Width, this.EditorCanvas.Height);
			this._editFrameControl.iFade = true;
			this.PlayWorld = false;
		}

		private void EnableNextButton()
		{
			if (!this._nextEnabled)
			{
				this.r_ChooseName.r_NextButton.Opacity = 1;
				this.r_ChooseName.r_NextButton.MouseLeftButtonDown += new MouseButtonEventHandler(this.r_NextButton_MouseLeftButtonDown);
				this._nextEnabled = true;
			}
		}

		private void Game_OnClose(object sender, EnumGameType t, GiftsList gifts, int pts)
		{
			GameManager.Instance().OnClose -= new GameManager.OnCloseDelegate(this.Game_OnClose);
			if (this._animal != null && this._animal._Brain != null)
			{
				this._animal._Brain.Paused = false;
			}
			int totalSeconds = (int)(DateTime.Now - this._time).TotalSeconds;
			string str = RegisterPage._id;
			string currentAffiliate = Global.CurrentAffiliate;
			object[] objArray = new object[] { "exit ", t.ToString(), " time: ", totalSeconds, "s" };
			this.WriteRegisterStateLog(str, currentAffiliate, string.Concat(objArray));
		}

		private void Game_OnLoad(object sender, IGame game)
		{
			GameManager.Instance().OnLoad -= new GameManager.OnLoadDelegate(this.Game_OnLoad);
			GameManager.Instance().OnClose += new GameManager.OnCloseDelegate(this.Game_OnClose);
		}

		private void GoToStage3()
		{
			this.r_ChooseName.r_BackButton.Visibility = System.Windows.Visibility.Visible;
			this.r_ChooseName.s_2.Stop();
			this.r_Stages.s_2.Stop();
			this.r_PetTalk.s_2.Stop();
			this.r_ChooseName.s_3.Begin();
			this.r_Stages.s_3.Begin();
			this.r_PetTalk.s_3.Begin();
			this._stage++;
			this.WriteRegisterStateLog(RegisterPage._id, Global.CurrentAffiliate, "stage3");
			LogEvents.LogRegistration(string.Concat("goto_stage3", this._oldNewPostfix), Global.CurrentAffiliate);
		}

		private void GoToStage4()
		{
			this.r_Games.Visibility = System.Windows.Visibility.Collapsed;
			PlayEffect.PlayEffectFile("next4.wma", 1);
			this._email = this.r_ChooseName.r_EmailAddress.Text;
			if (this.ValidateEmail())
			{
				this.r_ChooseName.s_3.Stop();
				this.r_Stages.s_3.Stop();
				this.r_PetTalk.s_3.Stop();
				this.r_ChooseName.r_EmailAddressRep.Text = this._email;
				this.r_ChooseName.s_4.Begin();
				this.r_Stages.s_4.Begin();
				this._stage++;
				if (this._name != this._oldName)
				{
					this.CreateUser();
					this._oldName = this._name;
					this._oldEmail = this._email;
				}
				else if (this._email != this._oldEmail)
				{
					this.SendActivationMail();
					this._oldEmail = this._email;
				}
				IsoStore.PutSetting("RegisteredTime", DateTime.UtcNow.ToString());
				this.WriteRegisterStateLog(RegisterPage._id, Global.CurrentAffiliate, "stage4");
				LogEvents.LogRegistration(string.Concat("complete", this._oldNewPostfix), Global.CurrentAffiliate);
				this.PlayWorld = false;
			}
		}

		private void InitEditor(IEditor editor)
		{
			if (editor == null)
			{
				return;
			}
			this._editFrameControl = editor;
			this.EditAnimal(this._loadUserData as InkAnimal);
		}

		public static void InitGame(Canvas mainCanvas, UIElement kbEvents)
		{
			GameLoop.Start();
			Global.MainCanvas = mainCanvas;
			Global.MainKbEvents = kbEvents;
			Global.MainControl = RegisterPage._thMainScreen;
			Global.GameCanvas = RegisterPage._thMainScreen.r_GamePos;
			Global._modalFrameCanvas = new Canvas()
			{
				Width = Global.MainCanvas.Width,
				Height = Global.MainCanvas.Height,
				Background = new SolidColorBrush(Colors.White),
				Opacity = 0.4
			};
		}

		[DebuggerNonUserCode]
		public void InitializeComponent()
		{
			if (this._contentLoaded)
			{
				return;
			}
			this._contentLoaded = true;
			Application.LoadComponent(this, new Uri("/RegisterLib;component/RegisterPage.xaml", UriKind.Relative));
			this.LayoutRoot = (Canvas)base.FindName("LayoutRoot");
			this.r_SkyPos = (Canvas)base.FindName("r_SkyPos");
			this.r_Pet = (Canvas)base.FindName("r_Pet");
			this.r_Stages = (Stages)base.FindName("r_Stages");
			this.r_ChooseName = (ChooseName)base.FindName("r_ChooseName");
			this.r_PetTalk = (PetTalk)base.FindName("r_PetTalk");
			this.r_Menu = (LowerLeftButtons)base.FindName("r_Menu");
			this.r_ContactUs = (ContactUsButton)base.FindName("r_ContactUs");
			this.r_SignUp = (Canvas)base.FindName("r_SignUp");
			this.r_Games = (Canvas)base.FindName("r_Games");
			this.r_background = (Canvas)base.FindName("r_background");
			this.r_PlayQuest = (Canvas)base.FindName("r_PlayQuest");
			this.r_PlayEggCatcher = (Canvas)base.FindName("r_PlayEggCatcher");
			this.r_PlayJumpTo4 = (Canvas)base.FindName("r_PlayJumpTo4");
			this.r_GamePos = (Canvas)base.FindName("r_GamePos");
		}

		private void InitProxy()
		{
			Binding basicHttpBinding = new BasicHttpBinding();
			this._authenticationHost = ServerUtil.AuthenticationServerInfo._serverUrl;
			this._mailService = ServerUtil.UserMailingService;
			YPMgr.Instance().InitServiceProxy(this._authenticationHost);
			EndpointAddress endpointAddress = new EndpointAddress(string.Concat(this._authenticationHost, "/AuthenticateService.asmx"));
			this._authenticationProxy = new AuthenticateServiceSoapClient(basicHttpBinding, endpointAddress);
			this._authenticationProxy.CheckUserNameExistCompleted += new EventHandler<CheckUserNameExistCompletedEventArgs>(this._authenticationProxy_CheckUserNameExistCompleted);
			this._authenticationProxy.RegisterCompleted += new EventHandler<RegisterCompletedEventArgs>(this._authenticationProxy_RegisterCompleted);
		}

		private void InitServerMgr()
		{
			UserMgr.Instance().OnDBUserInfoEndLoad += new UserMgr.OnDBUserInfoEndLoadDelegate(this.RegisterPage_OnDBUserInfoEndLoad);
			ServerMgr.Instance().OnDBSkyEndLoad += new ServerMgr.OnDBSkyEndLoadDelegate(this.RegisterPage_OnDBSkyEndLoad);
			YPMgr.Instance().InitServiceProxy(this._authenticationHost);
			ServerMgr.Instance().Start(this._info);
			ServerMgr.Instance().OnDBWorldLoad += new ServerMgr.onDBWorldLoadDelegate(this.RegisterPage_OnDBWorldLoad);
		}

		private void InitWorld()
		{
			Rect rect = new Rect(0, 0, this.r_Pet.Width, this.r_Pet.Height);
			this._world = new BasicWorldControl4(this, rect);
			this.r_Pet.Children.Add(this._world);
			this._world.Play = true;
		}

		private void LoadEditor(object userData, object userData2)
		{
			this._loadUserData = userData;
			this._loadUserData2 = userData2;
			LoadXap loadXap = new LoadXap();
			loadXap.OnLoadObject += new LoadXap.OnLoadObjectDelegate(this.loadXap_OnLoadObject);
			loadXap.LoadObject("EditorLib.Xap", "EditorLib.dll", "AnimalsWorldClassLibrary1.Editor.EditorFrameControl", Global.MainCanvas, PleaseWaitControl.EnumWaitType.EDITOR);
		}

		private void loadXap_OnLoadObject(LoadXap sender, object obj)
		{
			this.InitEditor(obj as IEditor);
			try
			{
				sender.OnLoadObject -= new LoadXap.OnLoadObjectDelegate(this.loadXap_OnLoadObject);
			}
			catch (Exception exception)
			{
			}
		}

		public bool OpenGame(EnumGameType t)
		{
			bool flag;
			try
			{
				this._animal._Brain.Paused = true;
				this._time = DateTime.Now;
				this.WriteRegisterStateLog(RegisterPage._id, Global.CurrentAffiliate, t.ToString());
				this.StartRegistration();
				GameManager.Instance().OnLoad += new GameManager.OnLoadDelegate(this.Game_OnLoad);
				GameManager.Instance().InitGame(t, this._animal);
				flag = true;
			}
			catch
			{
				flag = false;
			}
			return flag;
		}

		private void r_BackButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			switch (this._stage)
			{
				case 2:
				{
					this._world.Remove(this._animal);
					this.r_ChooseName.Visibility = System.Windows.Visibility.Collapsed;
					this.r_Games.Visibility = System.Windows.Visibility.Collapsed;
					this.r_Stages.s_2.Stop();
					this.r_ChooseName.s_2.Stop();
					this.r_PetTalk.s_2.Stop();
					this.EditAnimal(this._animal);
					LogEvents.LogRegistration(string.Concat("back_to_stage1", this._oldNewPostfix), Global.CurrentAffiliate);
					break;
				}
				case 3:
				{
					this.r_ChooseName.s_3.Stop();
					this.r_Stages.s_3.Stop();
					this.r_PetTalk.s_3.Stop();
					this.r_ChooseName.s_2.Begin();
					this.r_Stages.s_2.Begin();
					this.r_PetTalk.s_2.Begin();
					LogEvents.LogRegistration(string.Concat("back_to_stage2", this._oldNewPostfix), Global.CurrentAffiliate);
					break;
				}
				case 4:
				{
					this.r_ChooseName.s_4.Stop();
					this.r_Stages.s_4.Stop();
					this.r_PetTalk.s_4.Stop();
					this.r_ChooseName.s_3.Begin();
					this.r_Stages.s_3.Begin();
					this.r_PetTalk.s_3.Begin();
					LogEvents.LogRegistration(string.Concat("back_to_stage3", this._oldNewPostfix), Global.CurrentAffiliate);
					break;
				}
			}
			this._stage--;
		}

		private void r_IagreeTerms_Checked(object sender, RoutedEventArgs e)
		{
			this._agreeChecked = true;
		}

		private void r_IagreeTerms_Unchecked(object sender, RoutedEventArgs e)
		{
			this._agreeChecked = false;
		}

		private void r_NextButton_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			switch (this._stage)
			{
				case 2:
				{
					this._name = this.r_ChooseName.r_Username.Text;
					this._password = this.r_ChooseName.r_Password.Password;
					this._rePassword = this.r_ChooseName.r_ReEnterPassword.Password;
					if (this._name != null && (this._name != "") & (this._name == this._oldName))
					{
						this.GoToStage3();
						return;
					}
                    if (_doAttemptLoad)
                    {
                        LoadPolyLines(this._name.Trim());
                    }
					if (!this.ValidateUsernameAndPassword())
					{
						break;
					}

					this.CheckUserNameExists();
					return;
				}
				case 3:
				{
					if (this._agreeChecked)
					{
						this.GoToStage4();
						return;
					}
					if (Global.CurLanguage == EnumLang.HEBREW)
					{
						this.Alert(".כדי להצטרף לשידוני עליך לאשר את תנאי השימוש");
						return;
					}
					this.Alert("You must agree to the Shidonni Terms of Use to create Shidonni account.");
					return;
				}
				case 4:
				{
					this.Close();
					break;
				}
				default:
				{
					return;
				}
			}
		}

		private void r_PlayEggCatcher_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.OpenGame(EnumGameType.EGG_CATCHER);
		}

		private void r_PlayJumpTo4_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.OpenGame(EnumGameType.CONNECT_FOUR);
		}

		private void r_PlayQuest_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.OpenGame(EnumGameType.QUEST);
		}

		private void r_SignUp_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
		{
			this.WriteRegisterStateLog(RegisterPage._id, Global.CurrentAffiliate, "signup");
			this.StartRegistration();
		}

		private void RegisterPage_OnDBSkyEndLoad(object sender)
		{
			this.AddNewWorld();
			this._animal.SaveInDB(this._worldId);
		}

		private void RegisterPage_OnDBUserInfoEndLoad(object sender, byte[] userImage)
		{
			ServerMgr.Instance().LoadDBWorldsInfo();
		}

		private void RegisterPage_OnDBWorldLoad(object sender, DBWorld world)
		{
			BasicWorldControl basicWorldControl = new BasicWorldControl(world.WorldID, world.Int1, true)
			{
				_starOrder = world.StarOrder
			};
			basicWorldControl.SetLowRes(true);
			basicWorldControl.DBLoadingRegister();
		}

		public void SendActivationMail()
		{
			BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
			EndpointAddress endpointAddress = new EndpointAddress(string.Concat(this._mailService, "/MailService.asmx"));
			MailServiceSoapClient mailServiceSoapClient = new MailServiceSoapClient(basicHttpBinding, endpointAddress);
			ActivationMail activationMail = new ActivationMail(this._info.Id, this._email, this._name, this._password, Global.CurrentAffiliate, Global.CurLanguage);
			activationMail.CreateActivationMail();
			mailServiceSoapClient.SendMailAsync("info@shidonni.com", activationMail.Email, activationMail.Title, activationMail.Message);
		}

		private void SetAffiliation(long userId, DateTime serverUTCtime)
		{
			DateTime dateTime;
			AnimalsWorldClassLibrary1.AuthenticationSrv.DBUserAffiliate dBUserAffiliate = new AnimalsWorldClassLibrary1.AuthenticationSrv.DBUserAffiliate()
			{
				Id = userId,
				FirstTimeAffiliate = IsoStore.TryGetSetting("FirstTimeAffiliate")
			};
			if (DateTime.TryParse(IsoStore.TryGetSetting("FirstTimeDate"), out dateTime))
			{
				dBUserAffiliate.FirstTimeDate = dateTime;
			}
			if (dBUserAffiliate.FirstTimeDate < RegisterPage.MIN_DATE)
			{
				dBUserAffiliate.FirstTimeDate = RegisterPage.MIN_DATE;
			}
			dBUserAffiliate.RegistrationAffiliate = Global.CurrentAffiliate;
			dBUserAffiliate.RegistrationDate = serverUTCtime;
			this._authenticationProxy.SetAffiliateAsync(dBUserAffiliate);
		}

		private void SetEmail()
		{
			AnimalsWorldClassLibrary1.AuthenticationSrv.ArrayOfString arrayOfString = new AnimalsWorldClassLibrary1.AuthenticationSrv.ArrayOfString()
			{
				this._info.Id.ToString(),
				this._email
			};
			this._authenticationProxy.UpdateParentEmailAsync(arrayOfString);
		}

		private void StartRegistration()
		{
			if (this._stage == 2)
			{
				this.r_SignUp.Visibility = System.Windows.Visibility.Collapsed;
				this.r_ChooseName.Visibility = System.Windows.Visibility.Visible;
				this.r_Stages.s_2.Begin();
				this.r_ChooseName.s_2.Begin();
			}
		}

		private bool ValidateEmail()
		{
			if (this.r_ChooseName.r_EmailAddress.Text == null || "" == this.r_ChooseName.r_EmailAddress.Text)
			{
				if (Global.CurLanguage != EnumLang.HEBREW)
				{
					this.Alert("Please enter an email address.");
				}
				else
				{
					this.Alert("אנא הכנס כתובת דואר");
				}
				return false;
			}
			if (160 < this.r_ChooseName.r_EmailAddress.Text.Length)
			{
				if (Global.CurLanguage != EnumLang.HEBREW)
				{
					this.Alert("Parent email is too long.");
				}
				else
				{
					this.Alert("כתובת הדואר ארוכה מדי");
				}
				return false;
			}
			if (CommonHelper.IsEmailValid(this.r_ChooseName.r_EmailAddress.Text))
			{
				return true;
			}
			if (Global.CurLanguage != EnumLang.HEBREW)
			{
				this.Alert("There is a problem with your email. \nPlease re-enter a real email address.");
			}
			else
			{
				this.Alert("כתובת הדואר אינה נכונה\n אנא הכנס שנית");
			}
			return false;
		}

		private bool ValidateUsernameAndPassword()
		{
			if (this._password != this._rePassword)
			{
				if (Global.CurLanguage != EnumLang.HEBREW)
				{
					this.Alert("The passwords do not match.");
				}
				else
				{
					this.Alert("הסיסמאות אינן תואמות");
				}
				this.r_ChooseName.r_Password.Password = "";
				this.r_ChooseName.r_ReEnterPassword.Password = "";
				return false;
			}
			if (this._name == null || this._password == null || "" == this._name || "" == this._password)
			{
				if (Global.CurLanguage != EnumLang.HEBREW)
				{
					this.Alert("Please enter name and password.");
				}
				else
				{
					this.Alert("אנא הכנס שם משתמש וסיסמה");
				}
				return false;
			}
			if (160 < this._name.Length)
			{
				if (Global.CurLanguage != EnumLang.HEBREW)
				{
					this.Alert("Username is too long.");
				}
				else
				{
					this.Alert("שם משתמש ארוך מדי");
				}
				return false;
			}
			if (!CommonHelper.IsUserNameValid(this._name))
			{
				if (Global.CurLanguage != EnumLang.HEBREW)
				{
					this.Alert("Sorry but you can't use that name. \nUser name must be between 3 and 20 characters long and contain only letters, numbers and underscores.");
				}
				else
				{
					this.Alert("שם אינו מתאים. \nאנא הכנס שם באורך 3-20 תווים, הכוללים אותיות ומספרים בלבד");
				}
				return false;
			}
			if (20 < this._password.Length)
			{
				if (Global.CurLanguage != EnumLang.HEBREW)
				{
					this.Alert("Password is too long.");
				}
				else
				{
					this.Alert("הסיסמה ארוכה מדי");
				}
				return false;
			}
			if (this._password == this._name)
			{
				if (Global.CurLanguage != EnumLang.HEBREW)
				{
					this.Alert("Sorry but you can't use that password. \nPassword and user name can't be the same.");
				}
				else
				{
					this.Alert("הסיסמה אינה מתאימה. \nסיסמה חייבת להיות שונה משם המשתמש.");
				}
				return false;
			}
			if (CommonHelper.IsPasswordValid(this._password))
			{
				return true;
			}
			if (Global.CurLanguage != EnumLang.HEBREW)
			{
				this.Alert("Sorry but you can't use that password. \nPassword must be between 4 and 20 characters long and contain only letters, numbers and underscores.");
			}
			else
			{
				this.Alert("הסיסמה אינה מתאימה. \nאנא בחר סיסמה באורך 4-20 תווים, הכוללים אותיות ומספרים בלבד.");
			}
			return false;
		}

		private void WriteRegisterLog(string id, string af, string param)
		{
			if (id == null)
			{
				id = "";
			}
			if (af == null)
			{
				af = "";
			}
			if (param == null)
			{
				param = "";
			}
			try
			{
				string absoluteUri = HtmlPage.Document.DocumentUri.AbsoluteUri;
				if (absoluteUri.Contains("?"))
				{
					absoluteUri = absoluteUri.Substring(0, absoluteUri.IndexOf("?"));
				}
				string str = absoluteUri.Substring(0, absoluteUri.LastIndexOf("/"));
				Binding basicHttpBinding = new BasicHttpBinding();
				EndpointAddress endpointAddress = new EndpointAddress(string.Concat(str, "/LogService.asmx"));
				(new LogServiceSoapClient(basicHttpBinding, endpointAddress)).WriteRegisterLogAsync(id, af, param);
			}
			catch
			{
			}
		}

		private void WriteRegisterStateLog(string id, string af, string param)
		{
			if (id == null)
			{
				id = "";
			}
			if (af == null)
			{
				af = "";
			}
			if (param == null)
			{
				param = "";
			}
			try
			{
				string absoluteUri = HtmlPage.Document.DocumentUri.AbsoluteUri;
				if (absoluteUri.Contains("?"))
				{
					absoluteUri = absoluteUri.Substring(0, absoluteUri.IndexOf("?"));
				}
				string str = absoluteUri.Substring(0, absoluteUri.LastIndexOf("/"));
				Binding basicHttpBinding = new BasicHttpBinding();
				EndpointAddress endpointAddress = new EndpointAddress(string.Concat(str, "/LogService.asmx"));
				(new LogServiceSoapClient(basicHttpBinding, endpointAddress)).WriteRegisterStateLogAsync(id, af, param);
			}
			catch
			{
			}
		}
	}
}