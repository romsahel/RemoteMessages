using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Reflection;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Media;

namespace RemoteMessages
{
    /// <TODO>
    /// - Correct drafts (not saving correctly when sent message ; sometimes disappearing)
    /// - Add sound notifications (correct 'em)
    /// - Add embedded image viewer
    /// - Smiley shortcut (menu to autoreplace smileys) or most recent/used menu
    /// - 
    /// </TODO>

    public partial class MainForm : Form
    {
        #region Variables
        private string previousConversation;
        private HtmlElement previousSelectedContact;

        private bool isPreviousF11;
        private bool isPreviousF1;
        private bool isPreviousF2;
        private bool isPreviousAltDown;
        private bool isPreviousCtrlDown;
        private bool isPreviousMouse;

        private Timer timerReplacing, timerUnfocusing, timerCheckNew, timerTimeOut;
        private Dictionary<string, string> drafts, shortcuts;
        private bool isPreviousF12;
        private string url, port;
        private bool isReplacing, isAutoUpdate, isUnfocusing;
        private bool closeToTray, minimizeToTray, escapeToTray;
        private bool showBalloon, showFlash;
        private int delayReplacing = -1, delayUnfocusing = -1;
        private int delayBalloon = -1, flashCount = -1;
        private string deviceName;
        private bool documentCompleted;
        private bool justUnfocused;
        private bool isExiting;
        private bool fakeClick;
        private bool exceptionRaised;
        private bool loggedIn;

        private bool isGhostMode;
        private string password;
        private string hotkey;
        private bool loginDisplayed = false;

        private Process ahkProcess;

        public static string appFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Remote Client\";
        #endregion

        private const string VERSION = "3.2.15";
        private bool aboutDisplayed;
        private bool isDrafting;
        private bool sendFocused = false;

        private NotificationForm notification;
        private SoundPlayer ringtone;
        private HtmlElement previousFirstContact;
        private bool soundEnabled;
        private int soundIndex = -1;

        public MainForm()
        {
            checkUpdate();

            InitializeComponent();

            Size screenSize = Screen.PrimaryScreen.WorkingArea.Size;
            if (screenSize.Width < this.Width)
                this.Width = Screen.PrimaryScreen.WorkingArea.Size.Width - 100;
            if (screenSize.Height < this.Height)
                this.Height = Screen.PrimaryScreen.WorkingArea.Size.Height - 200;


            exceptionRaised = false;
            isPreviousF11 = false;
            isPreviousF1 = false;
            isPreviousAltDown = false;
            isPreviousCtrlDown = false;
            isExiting = false;
            isPreviousMouse = false;
            justUnfocused = false;
            drafts = new Dictionary<string, string>();
            shortcuts = new Dictionary<string, string>();
            documentCompleted = false;
            aboutDisplayed = false;

            loggedIn = false;

            notify.Visible = true;
            notify.MouseUp += new MouseEventHandler(ShowMeToggle);
            notify.ContextMenuStrip = contextMenu;

            notification = new NotificationForm();
            notification.Click += new EventHandler(notification_Click);
            notification.Hide();

            this.webBrowser1.ScriptErrorsSuppressed = true;
            this.webBrowser1.DocumentTitleChanged += new EventHandler(DocumentTitleChanged);
        }

        private void checkUpdate(bool manual = false)
        {
            try
            {
                // Create web client.
                WebClient client = new WebClient();
                client.Proxy = null;
                // Download string.
                string changelog = client.DownloadString(@"http://aerr.github.io/RemoteMessages/VERSION.txt");
                changelog = changelog.Split(new string[] { "---___---___---" }, StringSplitOptions.None)[0];
                string value = changelog.Split('\n')[0];
                if (Int32.Parse(value.Replace(".", "")) > Int32.Parse(VERSION.Replace(".", "")))
                {
                    DialogResult result = MessageBox.Show("Current version: " + VERSION + "\nAn update is available, would you like to download it?\n\nChanges:\n" + changelog,
                                 "An update is available!",
                                 MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Warning,
                                 MessageBoxDefaultButton.Button2);

                    if (result == System.Windows.Forms.DialogResult.Yes)
                    {
                        client.DownloadFile(@"http://aerr.github.io/RemoteMessages/downloads/setup_RemoteMessages.exe", appFolder + "setup_update.exe");

                        Process p = new Process();
                        p.StartInfo.FileName = appFolder + "update.bat";
                        p.StartInfo.Arguments = "/B";
                        p.StartInfo.WorkingDirectory = appFolder;
                        p.Start();

                        try
                        {
                            ahkProcess.Kill();
                            ahkProcess.Close();
                        }
                        catch { }

                        Environment.Exit(1);
                    }
                }
                else if (manual)
                {
                    MessageBox.Show("Your already have the latest version of the application.", "No updates available.", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch
            {
                if (manual)
                    MessageBox.Show("Error while contacting the server. Please, try again later.", "Can't reach the server!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Displays the options in a new window
        /// </summary>
        private void displayOptions()
        {
            isUnfocusing = false;
            using (PreferencesForm form2 = new PreferencesForm(new bool[] { closeToTray, minimizeToTray, escapeToTray },
                new bool[] { showBalloon, showFlash },
                delayBalloon, flashCount,
                isAutoUpdate, isReplacing, isUnfocusing, delayReplacing, delayUnfocusing, deviceName, url,
                isGhostMode, password, hotkey, isDrafting, soundEnabled, getVolume(), soundIndex,
                VERSION))
            {
                DialogResult res = form2.ShowDialog();
                if (res == System.Windows.Forms.DialogResult.OK)
                {
                    bool[] bBackgrounds = form2.getBackgrounderOptions();
                    bool[] bNotifs = form2.getNotifOptions();
                    int[] sMoreNotifs = form2.getNotifMoreOptions();

                    closeToTray = bBackgrounds[0];
                    minimizeToTray = bBackgrounds[1];
                    escapeToTray = bBackgrounds[2];

                    isDrafting = form2.getDraftActivated();


                    showBalloon = bNotifs[0];
                    showFlash = bNotifs[1];

                    flashCount = sMoreNotifs[0];
                    delayBalloon = sMoreNotifs[1];


                    bool mustRefresh = (isAutoUpdate != form2.getAutoIPActivated())
                        || (isAutoUpdate && deviceName != form2.getDeviceName())
                        || (!isAutoUpdate && url != form2.getDeviceName())
                        || port != form2.getPort();

                    soundEnabled = form2.getSoundActivated();
                    ChangeVolume(form2.getSoundVolume());

                    isAutoUpdate = form2.getAutoIPActivated();
                    isReplacing = form2.getReplacementActivated();
                    isUnfocusing = form2.getUnfocusActivated();

                    delayReplacing = form2.getReplacementDelay();
                    delayUnfocusing = form2.getUnfocusDelay();

                    isGhostMode = form2.getGhostModeActivated();
                    password = form2.getPassword();

                    if (soundIndex != form2.getSoundIndex())
                    {
                        soundIndex = form2.getSoundIndex();
                        ChangeRingtone();
                    }

                    if (form2.getHotkey() != hotkey)
                    {
                        hotkey = form2.getHotkey();
                        try
                        {
                            ahkProcess.Kill();
                            ahkProcess.Close();
                        }
                        catch { }
                        ahkProcess = Process.Start(appFolder + "remotemessages_script.exe", '"' + hotkey + '"');
                    }

                    port = form2.getPort();

                    if (isAutoUpdate)
                    {
                        deviceName = form2.getDeviceName();
                        string[] temp = url.Split(':');
                        url = temp[0] + ':' + temp[1] + ':' + port;
                    }
                    else
                        url = "http://" + form2.getDeviceName() + ":" + port;

                    saveConfig();

                    if (timerUnfocusing != null)
                        timerUnfocusing.Interval = delayUnfocusing;

                    if (timerReplacing != null)
                        timerReplacing.Interval = delayReplacing;

                    if (mustRefresh)
                    {
                        progressBar1.Visible = true;
                        if (isAutoUpdate)
                            FindNewIP();
                        else
                            DisplayPage();
                    }
                }
                else if (res == System.Windows.Forms.DialogResult.Abort)
                {
                    isExiting = true;
                    this.Close();
                }
            }
        }

        private static int getVolume()
        {
            uint soundVolume;
            // At this point, CurrVol gets assigned the volume
            Native.waveOutGetVolume(IntPtr.Zero, out soundVolume);
            // Calculate the volume
            ushort CalcVol = (ushort)(soundVolume & 0x0000ffff);
            // Get the volume on a scale of 1 to 10 (to fit the trackbar)
            int currVolume = CalcVol / (ushort.MaxValue / 100);
            return currVolume;
        }

        private bool loadConfig()
        {
            loadShortcutsFromFile();

            try
            {
                using (StreamReader reader = new StreamReader(appFolder + "remote.cfg"))
                {
                    url = reader.ReadLine();
                    port = reader.ReadLine();

                    closeToTray = Boolean.Parse(reader.ReadLine());
                    minimizeToTray = Boolean.Parse(reader.ReadLine());
                    escapeToTray = Boolean.Parse(reader.ReadLine());

                    isDrafting = Boolean.Parse(reader.ReadLine());

                    showBalloon = Boolean.Parse(reader.ReadLine());
                    delayBalloon = Int32.Parse(reader.ReadLine());
                    showFlash = Boolean.Parse(reader.ReadLine());
                    flashCount = Int32.Parse(reader.ReadLine());

                    soundEnabled = Boolean.Parse(reader.ReadLine());
                    ChangeVolume(Int32.Parse(reader.ReadLine()));

                    isAutoUpdate = Boolean.Parse(reader.ReadLine());
                    isReplacing = Boolean.Parse(reader.ReadLine());
                    isUnfocusing = Boolean.Parse(reader.ReadLine());

                    deviceName = reader.ReadLine();

                    delayReplacing = Int32.Parse(reader.ReadLine());

                    delayUnfocusing = Int32.Parse(reader.ReadLine());

                    isGhostMode = Boolean.Parse(reader.ReadLine());
                    password = reader.ReadLine();
                    hotkey = reader.ReadLine();
                    soundIndex = Int32.Parse(reader.ReadLine());
                    if (reader.EndOfStream)
                    {
                        reader.Close();
                        reader.Dispose();
                        return configNotFound();
                    }
                }
                return true;
            }
            catch
            {
                return configNotFound();
            }
        }

        private static void ChangeVolume(int vol)
        {

            // Calculate the volume that's being set. BTW: this is a trackbar!
            int NewVolume = ((ushort.MaxValue / 100) * vol);
            // Set the same volume for both the left and the right channels
            uint NewVolumeAllChannels = (((uint)NewVolume & 0x0000ffff) | ((uint)NewVolume << 16));
            // Set the volume
            Native.waveOutSetVolume(IntPtr.Zero, NewVolumeAllChannels);
        }

        private bool configNotFound()
        {
            if (url == null)
                url = "http://192.168.1.1:333";
            if (deviceName == null)
                deviceName = "yourDevicesName";
            if (port == null)
                port = "333";

            closeToTray = true;
            minimizeToTray = true;
            escapeToTray = true;

            isDrafting = true;

            showBalloon = true;
            if (delayBalloon == -1)
                delayBalloon = 3000;

            showFlash = true;
            if (flashCount == -1)
                flashCount = 6;

            isAutoUpdate = true;
            isReplacing = true;
            isUnfocusing = true;

            if (delayReplacing == -1)
                delayReplacing = 500;

            if (delayUnfocusing == -1)
                delayUnfocusing = 5000;

            isGhostMode = false;
            if (password == null)
                password = "";
            if (hotkey == null)
                hotkey = "!H";
            if (soundIndex == -1)
                soundIndex = 0;

            raiseException("No configuration has been found.\nIf this is the first time you use Remote Client, this is perfectly normal. Click the 'Options' button to display the preferences to enter your device's name or IP address and configure the application.", null);
            return false;
        }
        private void saveConfig()
        {
            using (StreamWriter writer = new StreamWriter(appFolder + "remote.cfg"))
            {
                writer.WriteLine(url);
                writer.WriteLine(port);

                writer.WriteLine(closeToTray);
                writer.WriteLine(minimizeToTray);
                writer.WriteLine(escapeToTray);

                writer.WriteLine(isDrafting);

                writer.WriteLine(showBalloon);
                writer.WriteLine(delayBalloon);
                writer.WriteLine(showFlash);
                writer.WriteLine(flashCount);

                writer.WriteLine(soundEnabled);
                writer.WriteLine(getVolume());

                writer.WriteLine(isAutoUpdate);
                writer.WriteLine(isReplacing);
                writer.WriteLine(isUnfocusing);

                writer.WriteLine(deviceName);

                writer.WriteLine(delayReplacing);

                writer.WriteLine(delayUnfocusing);

                writer.WriteLine(isGhostMode);
                writer.WriteLine(password);
                writer.WriteLine(hotkey);

                writer.WriteLine(soundIndex);

                writer.WriteLine();
            }
        }

        ///<summary>
        ///Called when a key is down
        ///</summary>
        private void webBrowser1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (!this.webBrowser1.IsWebBrowserContextMenuEnabled)
                this.webBrowser1.IsWebBrowserContextMenuEnabled = true;

            if (e.KeyCode == Keys.F10)
            {
                SmileyButton_Click(sender, null);
            }
            if (e.KeyCode == Keys.Escape)
            {
                if (documentCompleted && !exceptionRaised)
                {
                    webBrowser1.Document.GetElementById("editor").RemoveFocus();
                }
                if (webBrowser1.Focused && escapeToTray && ((!documentCompleted && !exceptionRaised) || getCurrentContactElement() == null))
                    Form1_FormClosing(sender, null);
            }

            if (e.KeyCode == Keys.F1 && !isPreviousF1)
            {
                displayOptions();
                isPreviousF1 = true;
            }
            else
                isPreviousF1 = false;

            if (e.KeyCode == Keys.F2 && !isPreviousF2)
            {
                checkUpdate(true);
                isPreviousF2 = true;
            }
            else
                isPreviousF2 = false;

            if (e.KeyCode == Keys.F11 && !isPreviousF11)
            {
                if (FormBorderStyle == System.Windows.Forms.FormBorderStyle.None)
                    FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
                else
                {
                    FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
                    Width = 960;
                    Height = 1080;
                    Location = new Point(1920 - Width, 0);
                }
                isPreviousF11 = true;
            }
            else
                isPreviousF11 = false;

            if (e.KeyCode == Keys.F12 && !isPreviousF12)
            {
                FindNewIP();
                isPreviousF12 = true;
            }
            else
                isPreviousF12 = false;

            if (documentCompleted && !exceptionRaised)
            {
                if (e.Modifiers == Keys.Alt && e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9 && !isPreviousAltDown)
                {
                    if (documentCompleted && !exceptionRaised)
                        webBrowser1.Document.GetElementById("editor").RemoveFocus();

                    ConversationChanged();
                    isPreviousAltDown = true;
                }
                else
                    isPreviousAltDown = false;

                if (sendFocused && e.KeyCode == Keys.Enter)
                {
                    emptyCurrentDraft();
                }

                if (e.Modifiers == Keys.Control && !isPreviousCtrlDown)
                {
                    if (e.KeyCode == Keys.Enter)
                    {
                        webBrowser1.Document.GetElementById("send").InvokeMember("click");
                    }
                    else if (e.KeyCode == Keys.E)
                    {
                        webBrowser1.Document.Body.Children[3].Children[0].Children[0].Children[4].Children[2].Focus();

                        isPreviousCtrlDown = true;
                    }
                }
                else
                    isPreviousCtrlDown = false;
            }
        }

        #region Conversation change (saving/loading draft, emoji)
        ///<summary>
        /// This function is called when user clicks the conversation's list.
        ///</summary>
        private void ConversationsList_MouseDown(object sender, HtmlElementEventArgs e)
        {
            if (!fakeClick && e.MouseButtonsPressed == System.Windows.Forms.MouseButtons.Left && !isPreviousMouse)
            {
                ConversationChanged();
                isPreviousMouse = true;
            }
            else
            {
                fakeClick = false;
                isPreviousMouse = false;
            }
        }
        ///<summary>
        /// Waits for the conversation to be loaded and displayed before replacing the smileys.
        ///</summary>
        private void ConversationChanged(bool justChanging = true, bool sending = false)
        {
            if (timerReplacing != null)
            {
                timerReplacing.Interval = delayReplacing;
                if (sending)
                    timerReplacing.Interval += 4000;

                if (delayReplacing == 0)
                    ConversationChangedTimer(null, null);
                else
                    timerReplacing.Start();
            }
        }

        private void ConversationChangedTimer(object sender, EventArgs e)
        {
            if (isReplacing)
                fromStringToEmoji(webBrowser1.Document.Body.Children[1]);

            isPreviousMouse = false;
            timerReplacing.Stop();
        }
        ///<summary>
        /// Replaces the smileys by their matching emoticons in messages 
        ///</summary>
        private bool fromStringToEmoji(HtmlElement element)
        {
            if (element.InnerHtml != null)
            {
                string removedByStringBuilder = new StringBuilder(element.InnerHtml)
        .Replace(" :)", " <img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAKDy1ZxSQAAAk9JREFUSA2UUU9I03EcHdSt1NbWSEZQUtBBd8g001FQpzpYgYFGRUnUKaGQ6CRhERRlikUEleChTMKFoZTVnM0iwqkRUw8ZgzIqS8WcbLJ+r/e+0Fi1jA6Pzz7v8/7sy88GwDYfvh5fumKiZtm26VOOc1O1jq7pWtdzM7mL130+/1/DRw/bs6ZOOq7M1rusuVtrkAiUwXp9FtZIg5naxesunfTpitIWjFU7y6ONyyfm2kuASAvwJQhMviT6UsBdPO/SSS/f7yV/FESqs1ZFL7njiZ4y4GMnMO4nnjAsDcTrTp308smfWvJLwbtjjsKZi9kxK7AdeN8KjN0BPnD+C9JRL5/8yvlZkiyw2WwLZuqyw4n2XOBtPZ9++f9Bn/zKUZ5KkgUvDtk9seac7+jdArw5DYym4gxLU3f+Njv5VJ189CtHeckCti0cr3EOWp389wO7gXAVcRQYrsLn3krUHVmNsI/fZIj8EHlO7eJ1l87o5aNfOcpTrnlB267FrtlGtwX/OiBUCgxWUFiBBOeF/U5c3ZeJ8+VLkHi1k3ypmdrF6y6d9MYX2gHlKE+5pqDjwKK8WNNKIMBvE/QSW4GeTYh2F6NhTybunnDj2kE7Jv1FQJ/XTO3idZdOejylT37mKE+5puBxZcbGeHMO8LAA6CB8fAnx7f56BJs8sAY2I/KoBJ+6NvCFXjO1i9ddOuNpo0/+BwVQnnJNwb29GcXxG3yBzwO05AHX1wI3idZcWN35wLN8WKEiWP18YaiQwUQ/XyNed+qMXr7b/M0c5Sn3BwAAAP//otUgQAAAAo5JREFUnZFNSNNxHMb/amy4zFjzLZ26N3VzmURBt+jWi0TRIejl0CXq0iWCbh6yg3oI3MEhvYP0IhWWFK5Exkoz2SGnrmnO1zxkZBQK/tnL0/P9t4mGdUh4fPb7vnye335TACg3jhl2q94S4FEFcNcGeMqAVqrdAvgqgQEX8MENhHb81hBdzv2sS1/mZN5TDtx2aBy1tQTCVSTgQq1SstxclEAH4fetQFspcK8MocZCxPxcGKoCwoRFUvpIH6VYX35tR9SznRdjQJuZ+wx7aIPwhKsFKIpiiFza5kuygS4CnzCk04rFl1ZcO2KAr6kIcwEHVAYkxl1YCTsx3WtH19UCNBzdjKVuO3cI7qA6HUg+sEF4wk0HZB536evUO6VxvOIz+fm139DfV2Ex5IT33FY01GWj+fQWXD+bi6aTOWg4mI1bF41YHq/W5hDgfC/VXYEVcoTHgEwtQJ6Jf6ZofX5fvJO3CMoT8I3HqEk34p9r8CNajWGfDcGnFoR77FiaciMxy/4EFaGGqUEXZF84wtO48i8VkHF5X86J7y3FCwjwzSP8IadrgBn6HH2evrAT+FoLfKHP8Sy9KeoTFWZAoBKyLxwGZKwLSIUY95izDv9sK15CH0NGuThJ+JQEUbNrJOFSj1Jy+7eVkD3ZJ9y4evH0h7SzadhVlHVoprEwpD62IDngBEYIGJPbEjbN24vLmeDkOydkTuZlT/bTLPHV32BdUVHyHAWbDrw4b/R+u2meV59bYwl/BRKDTiSDTs3lrD6zxqQvczJPeN5azl8DtIai6LlQTu3fW6Y7034qt2Xkiqlnoj6/X1zOUpd+ak7/J/yfAelhLusoE2WhXJQ75XKWui49u5Fv+EQbDf5v7ReotfKVSG7lIQAAAABJRU5ErkJggg==\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(":-)", "<img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAKDy1ZxSQAAAk9JREFUSA2UUU9I03EcHdSt1NbWSEZQUtBBd8g001FQpzpYgYFGRUnUKaGQ6CRhERRlikUEleChTMKFoZTVnM0iwqkRUw8ZgzIqS8WcbLJ+r/e+0Fi1jA6Pzz7v8/7sy88GwDYfvh5fumKiZtm26VOOc1O1jq7pWtdzM7mL130+/1/DRw/bs6ZOOq7M1rusuVtrkAiUwXp9FtZIg5naxesunfTpitIWjFU7y6ONyyfm2kuASAvwJQhMviT6UsBdPO/SSS/f7yV/FESqs1ZFL7njiZ4y4GMnMO4nnjAsDcTrTp308smfWvJLwbtjjsKZi9kxK7AdeN8KjN0BPnD+C9JRL5/8yvlZkiyw2WwLZuqyw4n2XOBtPZ9++f9Bn/zKUZ5KkgUvDtk9seac7+jdArw5DYym4gxLU3f+Njv5VJ189CtHeckCti0cr3EOWp389wO7gXAVcRQYrsLn3krUHVmNsI/fZIj8EHlO7eJ1l87o5aNfOcpTrnlB267FrtlGtwX/OiBUCgxWUFiBBOeF/U5c3ZeJ8+VLkHi1k3ypmdrF6y6d9MYX2gHlKE+5pqDjwKK8WNNKIMBvE/QSW4GeTYh2F6NhTybunnDj2kE7Jv1FQJ/XTO3idZdOejylT37mKE+5puBxZcbGeHMO8LAA6CB8fAnx7f56BJs8sAY2I/KoBJ+6NvCFXjO1i9ddOuNpo0/+BwVQnnJNwb29GcXxG3yBzwO05AHX1wI3idZcWN35wLN8WKEiWP18YaiQwUQ/XyNed+qMXr7b/M0c5Sn3BwAAAP//otUgQAAAAo5JREFUnZFNSNNxHMb/amy4zFjzLZ26N3VzmURBt+jWi0TRIejl0CXq0iWCbh6yg3oI3MEhvYP0IhWWFK5Exkoz2SGnrmnO1zxkZBQK/tnL0/P9t4mGdUh4fPb7vnye335TACg3jhl2q94S4FEFcNcGeMqAVqrdAvgqgQEX8MENhHb81hBdzv2sS1/mZN5TDtx2aBy1tQTCVSTgQq1SstxclEAH4fetQFspcK8MocZCxPxcGKoCwoRFUvpIH6VYX35tR9SznRdjQJuZ+wx7aIPwhKsFKIpiiFza5kuygS4CnzCk04rFl1ZcO2KAr6kIcwEHVAYkxl1YCTsx3WtH19UCNBzdjKVuO3cI7qA6HUg+sEF4wk0HZB536evUO6VxvOIz+fm139DfV2Ex5IT33FY01GWj+fQWXD+bi6aTOWg4mI1bF41YHq/W5hDgfC/VXYEVcoTHgEwtQJ6Jf6ZofX5fvJO3CMoT8I3HqEk34p9r8CNajWGfDcGnFoR77FiaciMxy/4EFaGGqUEXZF84wtO48i8VkHF5X86J7y3FCwjwzSP8IadrgBn6HH2evrAT+FoLfKHP8Sy9KeoTFWZAoBKyLxwGZKwLSIUY95izDv9sK15CH0NGuThJ+JQEUbNrJOFSj1Jy+7eVkD3ZJ9y4evH0h7SzadhVlHVoprEwpD62IDngBEYIGJPbEjbN24vLmeDkOydkTuZlT/bTLPHV32BdUVHyHAWbDrw4b/R+u2meV59bYwl/BRKDTiSDTs3lrD6zxqQvczJPeN5azl8DtIai6LlQTu3fW6Y7034qt2Xkiqlnoj6/X1zOUpd+ak7/J/yfAelhLusoE2WhXJQ75XKWui49u5Fv+EQbDf5v7ReotfKVSG7lIQAAAABJRU5ErkJggg==\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" :D", " <img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAKX0YylNAAAAmNJREFUSA18U11IU3EcHdRTJWvNRmJB9kEv1UtiWfZBPUQ99AEGkT1IhG8LNIkeYomGEFEpzkdBrEYWVCySCIuJmrVNV0nmQ4pUVqCZyJRtrJ3O+buNVcuHw//+zu983Hu3awFgWQg/qlasmXKtPDRTY786XWt/NlPreGlOzuK1X8j/3/CRCpt1+qK9ea7BkYh5NiLuK0VisB6J4UZzahavvXTSZyvKWjBenXtytmnVVMy7Cxi7C0x2Az/9RDADnMVzL5308v1d8k/BWLW1YPZmfjTeVQp87wAmXhDPGZYF4rWnTnr55M8s+aPgc6W9KHw9L5LwHQa+3APG24FvPCcf8rwPfOV1CpoNn9RRL5/8ykmVpAssFsui8I2893HvZmC0gY/uJprxqdeFS8dsGHpaleTEu80sXnvpjJ4++ZWjPJWkC16dtW2NtK37hZ79wMc6YIQYvQLvtQNwly1Be812zim+zszitZfO6OWjXznKSxewbfGEK/dNooN3HzoBDJ0jnMAHJ1rPb8CtiuVoca4FhpM8T83itZfO6OWjXznKU655ggfHlznmmvIT8G2j4CgwWDaPd6dw58J6BtnQVrkaeMtd6Ig5NYvXHtSlPfIzR3nKNQVPypduibTyDnt2AIE9wMBBIMhXFdyNzsZNaCm34nE9gzgjUGJOzeK1N7z08snPHOUp1xR0nskpjnoY0FsMvGZACv4SxEL74LlcgHBw73y4CgjN4rUHdWmPvMxRnnJNwaPTOTujt/kE3YVAH3/MANFPBPlvCxTxy+UHN8Dr/gxwNrw0gvR+oo/XXYVQnnJ/AwAA//+HnG0nAAAC4ElEQVSdUmtIk1EY/tTQWFGuzW1eMs2muzg1CqI/ZZBEN4iKoAtFEGiFmLVcIdU0b9AF9E9mNyKxIv2Rc4q2iWa2TbIkdf7wVqOVm5oQgg63PZ13bKJh/eiD93u+97zP5XC+wwHgHhzgbZp5shZoVwCmFOiuRcJYGo0xfRJcH5WY61fBbVXBw9DLitDdl4y5XhVc3Up8q5eiSRuJjrux8JpUQJsCM49jQL4cBWSlctHTFRKP15gIWBSoyV2NR6UaVBZko/DUdlzZtx7qDCHyMlYgbyfPh+oMEa7ul6Lo9A5UFatRpc2GoUzE9HLAkAjyI19fAMdxPKtG0OxuSgC65BhtiMOt/DNobTVg5IsNYw4nJiacmHTaMemwY4Lh+LgT38ccGBweRUtzI4pzD+NnuxQwy+FpTAD5kW8gIPhgctje2eexbnTKgEEVSg4tR2Z6MC6c3IrK2xoYm+sw0N+D0cEBWHu70aJ7gYrSXOQcTcPZ9CDcOc4DBpKBDhnIh/xYQLAvgI6JPYJhbUSnWxcPfFLAbpKhJischstCNFwS4FUOH8/Oh+PpuXBUM6xjvf6iAEY2f5mzBpNMQzp3fTzIh/x8vvTyBwSpt608MlUR5URnEjCSAptJjqb8CHQUiGEukcCyoMxFEry9IYY+XwQHmQ+lsN0nYqo8ykk+LCBoUYA/hL85JmTPr/tR076QPiXcP1LRVZsAHQt5o5Wg9aYELQx1hWL06NiZszl6lcC7RJCO9MycP7/xwEcA2ZC3URKy+2uZ+LOrNg5eM/snVgU8w+xK2lIxa0tjmOLr0a9g11IG4hGfdKQPeBHO/4NFixwn3CBatkufyb83+TDG7qqPn/O0SeG1JMH7QQaPhRXrXa/j52hOPOIzc+FCn78G+AYcF8YE61ilb4kNPVF9bFV5n0ZgGLoe8Z6QelqnuZ8X9qf5PwMCZCYOZSVgFcdKzkrpR+ppPTTAXQqXPKKliP+79htYSNpsLVVr9AAAAABJRU5ErkJggg==\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" :d", " <img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAKX0YylNAAAAmNJREFUSA18U11IU3EcHdRTJWvNRmJB9kEv1UtiWfZBPUQ99AEGkT1IhG8LNIkeYomGEFEpzkdBrEYWVCySCIuJmrVNV0nmQ4pUVqCZyJRtrJ3O+buNVcuHw//+zu983Hu3awFgWQg/qlasmXKtPDRTY786XWt/NlPreGlOzuK1X8j/3/CRCpt1+qK9ea7BkYh5NiLuK0VisB6J4UZzahavvXTSZyvKWjBenXtytmnVVMy7Cxi7C0x2Az/9RDADnMVzL5308v1d8k/BWLW1YPZmfjTeVQp87wAmXhDPGZYF4rWnTnr55M8s+aPgc6W9KHw9L5LwHQa+3APG24FvPCcf8rwPfOV1CpoNn9RRL5/8ykmVpAssFsui8I2893HvZmC0gY/uJprxqdeFS8dsGHpaleTEu80sXnvpjJ4++ZWjPJWkC16dtW2NtK37hZ79wMc6YIQYvQLvtQNwly1Be812zim+zszitZfO6OWjXznKSxewbfGEK/dNooN3HzoBDJ0jnMAHJ1rPb8CtiuVoca4FhpM8T83itZfO6OWjXznKU655ggfHlznmmvIT8G2j4CgwWDaPd6dw58J6BtnQVrkaeMtd6Ig5NYvXHtSlPfIzR3nKNQVPypduibTyDnt2AIE9wMBBIMhXFdyNzsZNaCm34nE9gzgjUGJOzeK1N7z08snPHOUp1xR0nskpjnoY0FsMvGZACv4SxEL74LlcgHBw73y4CgjN4rUHdWmPvMxRnnJNwaPTOTujt/kE3YVAH3/MANFPBPlvCxTxy+UHN8Dr/gxwNrw0gvR+oo/XXYVQnnJ/AwAA//+HnG0nAAAC4ElEQVSdUmtIk1EY/tTQWFGuzW1eMs2muzg1CqI/ZZBEN4iKoAtFEGiFmLVcIdU0b9AF9E9mNyKxIv2Rc4q2iWa2TbIkdf7wVqOVm5oQgg63PZ13bKJh/eiD93u+97zP5XC+wwHgHhzgbZp5shZoVwCmFOiuRcJYGo0xfRJcH5WY61fBbVXBw9DLitDdl4y5XhVc3Up8q5eiSRuJjrux8JpUQJsCM49jQL4cBWSlctHTFRKP15gIWBSoyV2NR6UaVBZko/DUdlzZtx7qDCHyMlYgbyfPh+oMEa7ul6Lo9A5UFatRpc2GoUzE9HLAkAjyI19fAMdxPKtG0OxuSgC65BhtiMOt/DNobTVg5IsNYw4nJiacmHTaMemwY4Lh+LgT38ccGBweRUtzI4pzD+NnuxQwy+FpTAD5kW8gIPhgctje2eexbnTKgEEVSg4tR2Z6MC6c3IrK2xoYm+sw0N+D0cEBWHu70aJ7gYrSXOQcTcPZ9CDcOc4DBpKBDhnIh/xYQLAvgI6JPYJhbUSnWxcPfFLAbpKhJischstCNFwS4FUOH8/Oh+PpuXBUM6xjvf6iAEY2f5mzBpNMQzp3fTzIh/x8vvTyBwSpt608MlUR5URnEjCSAptJjqb8CHQUiGEukcCyoMxFEry9IYY+XwQHmQ+lsN0nYqo8ykk+LCBoUYA/hL85JmTPr/tR076QPiXcP1LRVZsAHQt5o5Wg9aYELQx1hWL06NiZszl6lcC7RJCO9MycP7/xwEcA2ZC3URKy+2uZ+LOrNg5eM/snVgU8w+xK2lIxa0tjmOLr0a9g11IG4hGfdKQPeBHO/4NFixwn3CBatkufyb83+TDG7qqPn/O0SeG1JMH7QQaPhRXrXa/j52hOPOIzc+FCn78G+AYcF8YE61ilb4kNPVF9bFV5n0ZgGLoe8Z6QelqnuZ8X9qf5PwMCZCYOZSVgFcdKzkrpR+ppPTTAXQqXPKKliP+79htYSNpsLVVr9AAAAABJRU5ErkJggg==\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(":-D", "<img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAKX0YylNAAAAmNJREFUSA18U11IU3EcHdRTJWvNRmJB9kEv1UtiWfZBPUQ99AEGkT1IhG8LNIkeYomGEFEpzkdBrEYWVCySCIuJmrVNV0nmQ4pUVqCZyJRtrJ3O+buNVcuHw//+zu983Hu3awFgWQg/qlasmXKtPDRTY786XWt/NlPreGlOzuK1X8j/3/CRCpt1+qK9ea7BkYh5NiLuK0VisB6J4UZzahavvXTSZyvKWjBenXtytmnVVMy7Cxi7C0x2Az/9RDADnMVzL5308v1d8k/BWLW1YPZmfjTeVQp87wAmXhDPGZYF4rWnTnr55M8s+aPgc6W9KHw9L5LwHQa+3APG24FvPCcf8rwPfOV1CpoNn9RRL5/8ykmVpAssFsui8I2893HvZmC0gY/uJprxqdeFS8dsGHpaleTEu80sXnvpjJ4++ZWjPJWkC16dtW2NtK37hZ79wMc6YIQYvQLvtQNwly1Be812zim+zszitZfO6OWjXznKSxewbfGEK/dNooN3HzoBDJ0jnMAHJ1rPb8CtiuVoca4FhpM8T83itZfO6OWjXznKU655ggfHlznmmvIT8G2j4CgwWDaPd6dw58J6BtnQVrkaeMtd6Ig5NYvXHtSlPfIzR3nKNQVPypduibTyDnt2AIE9wMBBIMhXFdyNzsZNaCm34nE9gzgjUGJOzeK1N7z08snPHOUp1xR0nskpjnoY0FsMvGZACv4SxEL74LlcgHBw73y4CgjN4rUHdWmPvMxRnnJNwaPTOTujt/kE3YVAH3/MANFPBPlvCxTxy+UHN8Dr/gxwNrw0gvR+oo/XXYVQnnJ/AwAA//+HnG0nAAAC4ElEQVSdUmtIk1EY/tTQWFGuzW1eMs2muzg1CqI/ZZBEN4iKoAtFEGiFmLVcIdU0b9AF9E9mNyKxIv2Rc4q2iWa2TbIkdf7wVqOVm5oQgg63PZ13bKJh/eiD93u+97zP5XC+wwHgHhzgbZp5shZoVwCmFOiuRcJYGo0xfRJcH5WY61fBbVXBw9DLitDdl4y5XhVc3Up8q5eiSRuJjrux8JpUQJsCM49jQL4cBWSlctHTFRKP15gIWBSoyV2NR6UaVBZko/DUdlzZtx7qDCHyMlYgbyfPh+oMEa7ul6Lo9A5UFatRpc2GoUzE9HLAkAjyI19fAMdxPKtG0OxuSgC65BhtiMOt/DNobTVg5IsNYw4nJiacmHTaMemwY4Lh+LgT38ccGBweRUtzI4pzD+NnuxQwy+FpTAD5kW8gIPhgctje2eexbnTKgEEVSg4tR2Z6MC6c3IrK2xoYm+sw0N+D0cEBWHu70aJ7gYrSXOQcTcPZ9CDcOc4DBpKBDhnIh/xYQLAvgI6JPYJhbUSnWxcPfFLAbpKhJischstCNFwS4FUOH8/Oh+PpuXBUM6xjvf6iAEY2f5mzBpNMQzp3fTzIh/x8vvTyBwSpt608MlUR5URnEjCSAptJjqb8CHQUiGEukcCyoMxFEry9IYY+XwQHmQ+lsN0nYqo8ykk+LCBoUYA/hL85JmTPr/tR076QPiXcP1LRVZsAHQt5o5Wg9aYELQx1hWL06NiZszl6lcC7RJCO9MycP7/xwEcA2ZC3URKy+2uZ+LOrNg5eM/snVgU8w+xK2lIxa0tjmOLr0a9g11IG4hGfdKQPeBHO/4NFixwn3CBatkufyb83+TDG7qqPn/O0SeG1JMH7QQaPhRXrXa/j52hOPOIzc+FCn78G+AYcF8YE61ilb4kNPVF9bFV5n0ZgGLoe8Z6QelqnuZ8X9qf5PwMCZCYOZSVgFcdKzkrpR+ppPTTAXQqXPKKliP+79htYSNpsLVVr9AAAAABJRU5ErkJggg==\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" :p", " <img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAJQ1IzyJwAAAhxJREFUSA2cU09IU3Ecf3jwUqgbq0FlB8lOSYENKSshoYOYdQhi1SW6RKWFBHqeGFGnUJBFpy5t6EGJkkjZYctVQ+daDDsIHXIYmg6h0Za9T5/Pmz7SLSkPH77v9/38+423ZwAwtkK6y7F/qWdXS8bnfJjpdY2t3N8d1dRZe/Fb+f8avtTlqPzW7fRnB9zID9ViNeKFOfMI5uwTa+qsvXjppC9VVLJg/q7rUta/Z/nn6Ckg/RxYfg9kpoqhPXnppJdvc0lRwdfuyprsQHXOnKB2IQIshjjHOMeLYe3FRyC9fPL/WbKh4Msd57HvfXt/mOE23mwYmAty/gMs3TDkk1856yV2gWEYZSv9+z6tjtYBn/1EP9H3H5DeD/mVozyV2AXxG1VH8oEDvzDRDMz2Er5tgD76laM8u0Bt8z5XynzN2ycuAql24lYJdHC3jlI8fQn+25ijPOVav2DEu8OdfVxtIlwPTJ8HkpeBD1cYxpliYfICED8HTJ0lWgtTZ+3FS5ekXj75maM85VoFoWs7D+We8eVHjwOxkww4w4LTmAkcxu3mcqRfsTjWuIYT9rP24qWTHpP0yc8c5SnXKgjfrGjIDx4sFLxjgDDdhAfeKgSvO/DyHsvj/CZiCl8Dz9qLlw6JJuAtOYEFylOuVfDiakVDLsiQN7xp9GgBcQ8+jtThaacbcyG+r0kPQc6Gx9qLlw7U295IPXKBGij3NwAAAP//OeTfIwAAAqJJREFUlVJfSFNxGL1GQbhyuWbz7/ZgOt3VzR4kosdIxHzKemnRUwpKBCJCQn8o6iGhaRgSjOnaHpybUWZ/1Iptipu2xJyRq7AslQjEl8gpW/f0favJWj7UhcP5fd/vfOfs270CAMFp3KFbs6mBURHwlQJ+4hc6YIrwuQwIEs9QL44gnaeLCb/vWcf6cRGSrwTwimA/9hU4oL5USP92KzsseYqAAAlfEr8nk1k6v9ESk1koAfGame9DpGN9gOYmiiF5tGA/9o0FCIKwNdCo6IoOFf4KmBXRfjwNrsbdeHojC+7uXHju5sH/UIPAEw18Axq4+3Lx3JKD4VYVnGfTYaqRA28phAKigwVgP/aNBfAWexWCuNajWYWfftVHA8w1MvRWp6KrYjtMOhlseZnozFHiXKYCnbl7YFfnwSSmwUr3jspUdJ/aSQElkMZ0YB/2Y9+NAErbNnNeaY88yAdeiQh/0sPeoMDQGTkcVfuBk1cJF7FibCK+TLgEx5FyDNbL4WrJwPqSgd6ZiEh/PtiH/f4I4KJalBkWWlVT0gj9r3N6rC4a4GxKh+NYJdByj9CL7y1WYgehDz1HK+BsVmD9C30I7/SQvEXgefZhv78CKHVLWYZQsNyWNc9iTNOWKwYs9NUCd6YA2zii1jHiCcA6iSXXaeCrPrax5C7Cclv2PM+zz6YB8WbxLkEz0aw0h23qH9FRLT501wH3Z4FHryENBIHHdO4PYc5SiyhtyzrW81zcI84b7yDeYKYnhaC+UiU7sdiWORm6dnhZah8BLD5IZi9gHoN0cwTc53vWsZ7nEn1iXsmN5JqGlATD8EGjPVrbAanhNiJ1HXhGNff5Pnkmsd50g0QBn+lJUQmC7IJm34HrheWHmLnmfrI2uf6ngOSh/6l/App9Gw4d/70NAAAAAElFTkSuQmCC\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" :P", " <img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAJQ1IzyJwAAAhxJREFUSA2cU09IU3Ecf3jwUqgbq0FlB8lOSYENKSshoYOYdQhi1SW6RKWFBHqeGFGnUJBFpy5t6EGJkkjZYctVQ+daDDsIHXIYmg6h0Za9T5/Pmz7SLSkPH77v9/38+423ZwAwtkK6y7F/qWdXS8bnfJjpdY2t3N8d1dRZe/Fb+f8avtTlqPzW7fRnB9zID9ViNeKFOfMI5uwTa+qsvXjppC9VVLJg/q7rUta/Z/nn6Ckg/RxYfg9kpoqhPXnppJdvc0lRwdfuyprsQHXOnKB2IQIshjjHOMeLYe3FRyC9fPL/WbKh4Msd57HvfXt/mOE23mwYmAty/gMs3TDkk1856yV2gWEYZSv9+z6tjtYBn/1EP9H3H5DeD/mVozyV2AXxG1VH8oEDvzDRDMz2Er5tgD76laM8u0Bt8z5XynzN2ycuAql24lYJdHC3jlI8fQn+25ijPOVav2DEu8OdfVxtIlwPTJ8HkpeBD1cYxpliYfICED8HTJ0lWgtTZ+3FS5ekXj75maM85VoFoWs7D+We8eVHjwOxkww4w4LTmAkcxu3mcqRfsTjWuIYT9rP24qWTHpP0yc8c5SnXKgjfrGjIDx4sFLxjgDDdhAfeKgSvO/DyHsvj/CZiCl8Dz9qLlw6JJuAtOYEFylOuVfDiakVDLsiQN7xp9GgBcQ8+jtThaacbcyG+r0kPQc6Gx9qLlw7U295IPXKBGij3NwAAAP//OeTfIwAAAqJJREFUlVJfSFNxGL1GQbhyuWbz7/ZgOt3VzR4kosdIxHzKemnRUwpKBCJCQn8o6iGhaRgSjOnaHpybUWZ/1Iptipu2xJyRq7AslQjEl8gpW/f0favJWj7UhcP5fd/vfOfs270CAMFp3KFbs6mBURHwlQJ+4hc6YIrwuQwIEs9QL44gnaeLCb/vWcf6cRGSrwTwimA/9hU4oL5USP92KzsseYqAAAlfEr8nk1k6v9ESk1koAfGame9DpGN9gOYmiiF5tGA/9o0FCIKwNdCo6IoOFf4KmBXRfjwNrsbdeHojC+7uXHju5sH/UIPAEw18Axq4+3Lx3JKD4VYVnGfTYaqRA28phAKigwVgP/aNBfAWexWCuNajWYWfftVHA8w1MvRWp6KrYjtMOhlseZnozFHiXKYCnbl7YFfnwSSmwUr3jspUdJ/aSQElkMZ0YB/2Y9+NAErbNnNeaY88yAdeiQh/0sPeoMDQGTkcVfuBk1cJF7FibCK+TLgEx5FyDNbL4WrJwPqSgd6ZiEh/PtiH/f4I4KJalBkWWlVT0gj9r3N6rC4a4GxKh+NYJdByj9CL7y1WYgehDz1HK+BsVmD9C30I7/SQvEXgefZhv78CKHVLWYZQsNyWNc9iTNOWKwYs9NUCd6YA2zii1jHiCcA6iSXXaeCrPrax5C7Cclv2PM+zz6YB8WbxLkEz0aw0h23qH9FRLT501wH3Z4FHryENBIHHdO4PYc5SiyhtyzrW81zcI84b7yDeYKYnhaC+UiU7sdiWORm6dnhZah8BLD5IZi9gHoN0cwTc53vWsZ7nEn1iXsmN5JqGlATD8EGjPVrbAanhNiJ1HXhGNff5Pnkmsd50g0QBn+lJUQmC7IJm34HrheWHmLnmfrI2uf6ngOSh/6l/App9Gw4d/70NAAAAAElFTkSuQmCC\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" :o", " <img src=\"data:image/png;base64,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\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" :O", " <img src=\"data:image/png;base64,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\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" :/", " <img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAJfRDPvtgAAAitJREFUSA2cUl1IU3EcvfQWDMVuWkgrkuFL9lbrQyPqJTAEe9KoXiojCoVCiKCXSIP1IkjZ9Dko7QN9MUgimEvZppJm+qIihBFYK0eNbcx7Ouefd1itBT0cfvzO73zc7V4LgFUIn69u8sZvlNYm2uzASrv9IhEoGzGTu3jdC/n/Gj5/oaQ4fs3uSnZtQeaJD9nhBjgzHXDmeszULl536aTPV5S3YKl1c2OyuzyeGawBPgwAX6LAygTwdR20i+ddOunl+73kj4LF1uKd3+9709lwA7AcAj694nzJmQeG1z0E6eWTf33JLwXvr9j+b53lKSdUyyfrB5Z6Ofv+DaPrh3zyK8ctyRVYlrUh0bltNvu8ClgMEnf/A0HIrxzlqSRXELtYUpV+5FvF66PA/G3i1k8scC60EWt7QZ4++pWjvFyB2j7etKedod3AJP/7mRaiGZhtQWrqMgJNPixHzpnd5bWL1106w8tHv3KUp1zzC56d8JQlg14Hw3uAN/XA21PEaWD6DO5d2oE79RvRcXYrMMXbRJ2Z2sXrLp3Ryyc/c5SnXFMQbvLsSj+sAEYPALFDDDkGjB0BxqvxtL0CwZMePLi+nTxvsWoztYvXXTqjl09+5ihPuaZgpLnIn3lcycNBIMJv30W0Bs7kYaTmjmP1Hd9NjLc1aBevO6jLeeRljvKUawqGzhftT/f5+IL4dY3uIzgjRFRzL0OJMWKcuwvt4nWXTh4XYT+UN8jcHwAAAP//HnMFcQAAAlpJREFUjVNLaFNRFHwV+8FYhVZDRFNba5smaSMILlyoSy2oW1Fwo3tdt4gLxapUwZWCC/8bJRuDqFHBULVtkn4SG9uQapv+BDdFULExn3HmkReUNtDFMPczZ+bc++4zABj+4zbf0qPtwLt2oN8HDJAjXmCYPEKOEXGOx4hEERrHuT7iAYbIEc5ZV+jvAPq8WHrYAPkaCujZZ2z+eXNLvhBqA8IsiJJHiBiRIGbbkU158OyqA9eP1aL7QA0uHbYhcNGOTMzFoKI+Qh50AyEX5CdfM8AwjMqJrvpg7nUrzRkwTOEoQfOF9ztx+2wdLp+ohf+CHaEHTkQDjQg/bUTw7jY86XUgawUMsSbsRv5VK+QnXzNApzi9p2b/kr8pZ3YQY8iYG5mkB+dObcDXJLv6vQuY5/GneR1fiM/UTBLice5Tj1FiwA35yE++pQCmrZ3tsfflgi3ARxpM8k7TBK8Hc0XMky1oXWFTCuM4SfCb5F62QD7y+y9Akyud6zq/39r6zTxFit2miVlijrCMLVboDKEmpJN+wAPVy0d+ywKYuuaIu3L3rzvOH+bHnuCLmmLhNDFDKMyCTLWu/U8M4dWoTvXyWTHATDSMiqOuKt9cryP+J9CEgp7hOA0mZcjANCHWnMaFqBfSSa86mldY5stOYG2og707qjrenqm7sXjfuZB50ZzNf2hDnv9Egf+EWPPM8+bs4j3ngnTS/9t5ycsarMTqhmg46Ko+9PjkxmuJrvo3qfObBsWaa1370q1Ur7XSKyonMEU8EU2qCRuxvsial+66XP2qAsoVr2b9L3IDHqCbRNM8AAAAAElFTkSuQmCC\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(":-/", "<img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAJfRDPvtgAAAitJREFUSA2cUl1IU3EcvfQWDMVuWkgrkuFL9lbrQyPqJTAEe9KoXiojCoVCiKCXSIP1IkjZ9Dko7QN9MUgimEvZppJm+qIihBFYK0eNbcx7Ouefd1itBT0cfvzO73zc7V4LgFUIn69u8sZvlNYm2uzASrv9IhEoGzGTu3jdC/n/Gj5/oaQ4fs3uSnZtQeaJD9nhBjgzHXDmeszULl536aTPV5S3YKl1c2OyuzyeGawBPgwAX6LAygTwdR20i+ddOunl+73kj4LF1uKd3+9709lwA7AcAj694nzJmQeG1z0E6eWTf33JLwXvr9j+b53lKSdUyyfrB5Z6Ofv+DaPrh3zyK8ctyRVYlrUh0bltNvu8ClgMEnf/A0HIrxzlqSRXELtYUpV+5FvF66PA/G3i1k8scC60EWt7QZ4++pWjvFyB2j7etKedod3AJP/7mRaiGZhtQWrqMgJNPixHzpnd5bWL1106w8tHv3KUp1zzC56d8JQlg14Hw3uAN/XA21PEaWD6DO5d2oE79RvRcXYrMMXbRJ2Z2sXrLp3Ryyc/c5SnXFMQbvLsSj+sAEYPALFDDDkGjB0BxqvxtL0CwZMePLi+nTxvsWoztYvXXTqjl09+5ihPuaZgpLnIn3lcycNBIMJv30W0Bs7kYaTmjmP1Hd9NjLc1aBevO6jLeeRljvKUawqGzhftT/f5+IL4dY3uIzgjRFRzL0OJMWKcuwvt4nWXTh4XYT+UN8jcHwAAAP//HnMFcQAAAlpJREFUjVNLaFNRFHwV+8FYhVZDRFNba5smaSMILlyoSy2oW1Fwo3tdt4gLxapUwZWCC/8bJRuDqFHBULVtkn4SG9uQapv+BDdFULExn3HmkReUNtDFMPczZ+bc++4zABj+4zbf0qPtwLt2oN8HDJAjXmCYPEKOEXGOx4hEERrHuT7iAYbIEc5ZV+jvAPq8WHrYAPkaCujZZ2z+eXNLvhBqA8IsiJJHiBiRIGbbkU158OyqA9eP1aL7QA0uHbYhcNGOTMzFoKI+Qh50AyEX5CdfM8AwjMqJrvpg7nUrzRkwTOEoQfOF9ztx+2wdLp+ohf+CHaEHTkQDjQg/bUTw7jY86XUgawUMsSbsRv5VK+QnXzNApzi9p2b/kr8pZ3YQY8iYG5mkB+dObcDXJLv6vQuY5/GneR1fiM/UTBLice5Tj1FiwA35yE++pQCmrZ3tsfflgi3ARxpM8k7TBK8Hc0XMky1oXWFTCuM4SfCb5F62QD7y+y9Akyud6zq/39r6zTxFit2miVlijrCMLVboDKEmpJN+wAPVy0d+ywKYuuaIu3L3rzvOH+bHnuCLmmLhNDFDKMyCTLWu/U8M4dWoTvXyWTHATDSMiqOuKt9cryP+J9CEgp7hOA0mZcjANCHWnMaFqBfSSa86mldY5stOYG2og707qjrenqm7sXjfuZB50ZzNf2hDnv9Egf+EWPPM8+bs4j3ngnTS/9t5ycsarMTqhmg46Ko+9PjkxmuJrvo3qfObBsWaa1370q1Ur7XSKyonMEU8EU2qCRuxvsial+66XP2qAsoVr2b9L3IDHqCbRNM8AAAAAElFTkSuQmCC\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" ;)", " <img src=\"data:image/png;base64,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\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(";-)", "<img src=\"data:image/png;base64,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\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" :(", " <img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAJc3Tq+DAAAAihJREFUSA2Ukk1IVHEUxR/UrgaxsaFBAzPcWQRJMuZAtKtFKmi0cCGV7ZQKF7WRMChaaIoVQZuQkHQRIeWiL8avpsYpTSlcqAhlCJYNojIjz3c65zXv8crJaHG487/3nN+ZgTEAGJvp+8Uduxebdh5buuK/kWj2P1tqDry2J9/a675Z/q/w6XPZWYlL/turbQFrrasQZqQK1sQ1WJPt9tRbe93lkz9TUcaCucacUysduxbXeg8Dsw+Bb4PAjxgV94hv7XmXT37l/izZUDDbmLVn5WZuyuyvAub7gIVX1EvCMkh73emTXznlvSW/FXy+4D+03BJMWpHjwJceYK4b+Mr5L8lHv3LKi+OUuAWGYWxZbg1+NHuLgJk2/vRb/y/mlBdHPJW4BW/OZu9PdhasY+goMHUVmE5rxvPZ2Xmn964c8+KI5xawbetCU86Y1cdvP1oNfGoAJhtgTp3H9bpCzA+f4a5+g7TXXT757Rzz4ognrv0LHlVuD6x25FqIHGRBORLRagzcLUN7XR6e3wvDGj8JjFVSFbyf+DX51l53+eRXTnlxxBPXLnhau21f8n4+EA0h3nkAl8t9uFMfxPiTUgbCwAj/rrbKOB2ld7zLJ79yyosjnrh2wYvTvlCqay8wHAJiYZgTRwgiOEaY5EIzfHY9zH1g7i1z5Ignrl3wuMZXmnqQDwwWs70E5hBnvIRg/tuk99Qo3+84HemtveMZSeei3PUXQzxxfwIAAP//cp4ZhgAAAnRJREFUnVJfSFNxFL5qKAwzxjaTucxh3a7bVKKgt+gtqpeyPw/Re74Wwt7Kxyh70JfKpB4Meuklo39gYOjaVpahSxbptlyBWGKUoNPdfX3nt3tFY/XQ4Nu555zvfN+5v9/VAGi3jzv2Ld/dAbwMANFWjN5sxLPuevwYNpCLBzE3pKPvohsr70LAREjFvgtuVZe+8IQvc4VoCzAcwPIdH0RXE4OONq1+qbfOLLzQgVgA5vsABsIeXD25FddPb0P3qRoM9XOBZPM6JJe69IU3EK6FOc4FY+RwIdETXWWgaZpjKux6nn/aBMRJeGtgNRFANqIjydrc2B4gw+0/UcAGc6lLPzuqY3WSvTeGMjCfNEH0RNc2KG8PVR1bud+QR4QkvgEmaTQlG1uiGcZMsIg0Y8qqSz9BnvDHiREDoiN6NChXBnJM/LlmujyR/CN/0eAjN04RaWI2hF9TBr7E9SJiusqVYYpm0+QkCR5RftAP0RE9pSt/lkFZ58HqM4u93nlEeCRikOGFzbagkG1Bd4cTl09Uo6u9WsVrzM0sObJAijwxGNGx2OOdFx0alG0ysEyc+30VR3/e8i4pkw/WcJoC39uAhQ34xmepzxCTfAveg8zJPMWd64vbD3Zk07G3ruLI5yvbJ3IP+NnFeCcJCqgjo1i6tbix5BQuRPkpkyd8mZN5W0vi+h1sKmqae1ftlsOPzztvLPT7vuYG/Wvm8G6Yrw0UxgwVJc899K9JX3jCp7h7o85fDVRD06o4sJM4dKCh8ty9szU9ibBraPqS55VEyaUufYtX9af4Pw1sMocrCRfRSDQTQStKLvVKm1sqljyiUsT/rf0GRBP7A2M6IZEAAAAASUVORK5CYII=\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(":-(", "<img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAJc3Tq+DAAAAihJREFUSA2Ukk1IVHEUxR/UrgaxsaFBAzPcWQRJMuZAtKtFKmi0cCGV7ZQKF7WRMChaaIoVQZuQkHQRIeWiL8avpsYpTSlcqAhlCJYNojIjz3c65zXv8crJaHG487/3nN+ZgTEAGJvp+8Uduxebdh5buuK/kWj2P1tqDry2J9/a675Z/q/w6XPZWYlL/turbQFrrasQZqQK1sQ1WJPt9tRbe93lkz9TUcaCucacUysduxbXeg8Dsw+Bb4PAjxgV94hv7XmXT37l/izZUDDbmLVn5WZuyuyvAub7gIVX1EvCMkh73emTXznlvSW/FXy+4D+03BJMWpHjwJceYK4b+Mr5L8lHv3LKi+OUuAWGYWxZbg1+NHuLgJk2/vRb/y/mlBdHPJW4BW/OZu9PdhasY+goMHUVmE5rxvPZ2Xmn964c8+KI5xawbetCU86Y1cdvP1oNfGoAJhtgTp3H9bpCzA+f4a5+g7TXXT757Rzz4ognrv0LHlVuD6x25FqIHGRBORLRagzcLUN7XR6e3wvDGj8JjFVSFbyf+DX51l53+eRXTnlxxBPXLnhau21f8n4+EA0h3nkAl8t9uFMfxPiTUgbCwAj/rrbKOB2ld7zLJ79yyosjnrh2wYvTvlCqay8wHAJiYZgTRwgiOEaY5EIzfHY9zH1g7i1z5Ignrl3wuMZXmnqQDwwWs70E5hBnvIRg/tuk99Qo3+84HemtveMZSeei3PUXQzxxfwIAAP//cp4ZhgAAAnRJREFUnVJfSFNxFL5qKAwzxjaTucxh3a7bVKKgt+gtqpeyPw/Re74Wwt7Kxyh70JfKpB4Meuklo39gYOjaVpahSxbptlyBWGKUoNPdfX3nt3tFY/XQ4Nu555zvfN+5v9/VAGi3jzv2Ld/dAbwMANFWjN5sxLPuevwYNpCLBzE3pKPvohsr70LAREjFvgtuVZe+8IQvc4VoCzAcwPIdH0RXE4OONq1+qbfOLLzQgVgA5vsABsIeXD25FddPb0P3qRoM9XOBZPM6JJe69IU3EK6FOc4FY+RwIdETXWWgaZpjKux6nn/aBMRJeGtgNRFANqIjydrc2B4gw+0/UcAGc6lLPzuqY3WSvTeGMjCfNEH0RNc2KG8PVR1bud+QR4QkvgEmaTQlG1uiGcZMsIg0Y8qqSz9BnvDHiREDoiN6NChXBnJM/LlmujyR/CN/0eAjN04RaWI2hF9TBr7E9SJiusqVYYpm0+QkCR5RftAP0RE9pSt/lkFZ58HqM4u93nlEeCRikOGFzbagkG1Bd4cTl09Uo6u9WsVrzM0sObJAijwxGNGx2OOdFx0alG0ysEyc+30VR3/e8i4pkw/WcJoC39uAhQ34xmepzxCTfAveg8zJPMWd64vbD3Zk07G3ruLI5yvbJ3IP+NnFeCcJCqgjo1i6tbix5BQuRPkpkyd8mZN5W0vi+h1sKmqae1ftlsOPzztvLPT7vuYG/Wvm8G6Yrw0UxgwVJc899K9JX3jCp7h7o85fDVRD06o4sJM4dKCh8ty9szU9ibBraPqS55VEyaUufYtX9af4Pw1sMocrCRfRSDQTQStKLvVKm1sqljyiUsT/rf0GRBP7A2M6IZEAAAAASUVORK5CYII=\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" ;(", " <img src=\"data:image/png;base64,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\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(";-(", "<img src=\"data:image/png;base64,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\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" :'(", " <img src=\"data:image/png;base64,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\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" :x", " <img src=\"data:image/png;base64,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\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" :@", " <img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAKcRl58vAAAAmhJREFUSA2EUk1Ik3Ec/rfNiWPWNJsZtZFOsXSHkMw+hDoEUV1KS6mMllrKnNk0FjGmdKhtRplIH14qIT0JHrrUJSKILiUIHaIggi5BOGg2975ze/o/r/Q2y+zw8Of3fL6DCQBiJfjLxKZgmTgQconw1fJVTyVe8eVNnvpK+X+W+1xi9aVSw8hQpchM1ubh9ckavA+14OO1c9rLmzx1+uhfbmjZgYBLHB/ZKmafHXTgy70gYo8jiE9EMTcRwdx4WHt5k6dOH/3M/Tny10DQKTYPVxuVN6drEBu/gfjDASQe9CP5SGIsC/ImT50++pljPntkycDFClE7XGWYnz7iwPfRAH7c7UHyfg+UbIwuvanTRz9zzLPn14g+IIQw3qk2vXtZn4vYzfOYv+VBcsgD5fb/QR/9zDHPHvZxRB/oKhXuqR3m9IdDFiQGT0G53gAl3LiISCPUKHEsC1KTvO6RfuaYZw/79AG5ZhrcYno7vT8P345aoQTroQ7shXqlDmqvG+oFF1SfE4p3A1RvyeIrb42nTp/0M8c8e9jHXu0XtBQL+9g2U+bTYQviJ/KheNYg1VaIxFkb0h0FWOgsQKqzEAve39BuyVOnj37mmGcP+9irDXRtFO4ndWZ8bbAieUaWttkwuc+CJrsBlytyMNMsuV470LdeB2/y1Omjnznm2cM+9moDfU6x8/luM2abrFA9NmTk16a71yLlX4fP7UUIVeUiut0C9Dt18CZPnT76mWOePexjrzbQ7RS7XuyR/57mfKjtNsBXBPiLgUAJZlrtaHUY0VGeAzVcCUjw5U2eOn2aX+aYZw/72PsTAAD//6LElusAAAJ2SURBVJ1RS0hUYRT+fczDq6NMd8psqHFGHdCptBnzkRExLSJd2KYiIyiGyjAmJnqToy2EiKCJ0qygECMhEIoWtiha9Ni0chExEO0CcTMJgc1Yfp3vbyYyphZdOPfcc873+P9zFQB12KOCzzdZkdrrwMIRJ3B8OXCmCuhfg+vhCpz3WzC4sQy41ijRpDNr9jknTuOFRz51qEddRYOwqdyPWyzfZ3c5kOkVg5OVQloNXPTi1WkvzvmKMRHxArdagdsSklmzzzlxGi888qlDPepqA6WUMbyueOpjt4H5CA1WAgMeYKgOiyONSN4M4euDzcC9dmCsQ2fW7HNOnMYLj3zqUI+6OYPCVqfqfLut5Nvn/XLlmNxgoBq45AcS64EbQeBuG+bk5MmhJszfadO17nNOHPHCI5861BODQm3ANcljjgftr993lSLdawIXZK882dUAMBrC2B43+mqsiPmtOOqz4Mkhriz0c06c4MkjnzrU07p8ZQ0KdrqLdj/tsM/M9lRg8YSsadADXKnH9KlaxEJlmIzWYqo/gId9PkQ3GPgQl5PLnDjiySOfOmJQsMQga+L0G6rzxZaSLwRnjq3Qu02Pyi3uy5q4joTsnHmiGenhBj0njnjyyBdx+ZHZg+c+clmGRo2hdjxqt02/63IgdcBEJlqJxbOrgLhHTiurkcyafc6JI5488nNazL/+wZKmUq6qUrX98lrLyMuw8SnZXb4w07MMqYMm5iIunVmzzzlxxIu463edvxrogVI2IXgktjaUF+2L11kS4822Z5Mt9jfMrNnnPIuz/Sn+T4McWMhWCVOiWqJeIpDNrNm35rD5ct4V5QP+b+8H4ZknUAbbKeQAAAAASUVORK5CYII=DYbPh+b7widXLgtXGvVjbutJ+G67G5zchav/Wz+f4YHDtuzQjVWQ6QhB7G7TsS9JUgMXURi9Jo5NYvXXjrp0xWlLXhbvaA0cjU3FGvdALx7CHx5Doz7ga8p0Cyee+mkl+/vkhkFY9VZiyYaHdG4rwT42Al8esqznWcaGF77Tkgvn/ypJX8UBKss17f63B/wFgPfO4BQC34Gm3jTR7xp80yQN3vqjJ4++ZUzXZIssNlsc8L1ecOTnpUY8NTgRFEOzpTm4aZ7H/o9buDNZWDs0m9wFq+9dNLLJ79ylKeSZEFvhX15tMk5OdG1BQfyLQx21PKfP+C/v86bXwBGzwKBFGgWrz110ssnv3KUlyxQ2/vT1mCibQUwUI548BQwfBQYqvx/UG989CtHeco1X3B/V0Z25IojAe8aoG8HS/YSZSwpB17vAV7tJr9zauffPnVqFq+9dNLLJz9zlKdcU+A7lLEsensx0FMA9G4E/EUUb0Xg3mocL56LD+1ryRcS61NQaHjtpZPe+ORnjvKUawq6KzNdsTtLuGDIM759oX8zzpfNw60jdjw+x5f3chPDyU+Ds3jtpZM+6WWO8pRrCtoOZuZHm51AF19XzzqCZ18BvDeWonG/HSMtq3g7ci9SwFm89tJJb3zy+lxQXitzfwEAAP//6wSk5AAAAmBJREFUjZJPSJNxHMbfRWo0oT9bUcSWZjq31y0KPHSoY+WhTkF0sEv3CC/hpW4SFdSp6GBkeQk8SIfKCjIj57R0q+Z02dSZUAjDSmG2P0/P87aNQgceHr6/P8/38/x+v/c1ABg9Z+2BdPde4E0TMBgAgqwhHxDl+GszMM55hPpQkMZh7mtd+/KFTKsvP+gHBkykH7ohrqGAjiPGjqXbu3P5/sa/4BHWUSrsIZR13AvEqElCpwmaKMyjrNqXT/5haohr/R6IJ64VYBhGRazd0Zd90QCMEPKexjFKzYJMUjMmXne70dnmxHBvDZCgT4Hj9IQpBbyjQl7knjdAPHGtAN3ifPOmo+me2qx1Al3/I6GxAuSLDz8IPLffwLVjlThTY8NSks8zVdjXIeQfo4JeiCOeuKUApm1MduwcyPbV8+R8hjgBCWqammvCYtyHu63V6G3bhpun7fg5y3XeCglqiuMJKmIi+6we4oj3X4AmV1s2tyze2fPdukWcH2uGSlJzfmQWAui85EDXxa14cMWJ3Dd+WCtEQfTIH/RB/eKItyqAqRtOeisOLd9z/bL+ohghCTZOU7NU6gCwfBBYYBVU69qPMoRPoz71i7NmgJVoGLZTnsrA3PVdkd+Pa5Ef1Yck4LOADCxKc4LzIybkk199hNuK8FU3KG7oBIf3VfpfXdh+K9Xlml95WpfJvW1EbtREPmxaVfOVJ3WZ1H3XvHzy/3vyEqs4WKvqNJT7uKfqxKPWLTc+tTtexi87h1Q117r25VurX2ulv6icwTLxRoRUUXaqulA1L711uf51BZRrXs/6HzhUFPsbI56EAAAAAElFTkSuQmCC\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" :*)", " <img src=\"data:image/png;base64,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\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" :*", " <img src=\"data:image/png;base64,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\" alt=\"1F60A\" class=\"emoji\">")
        .Replace("&lt;3", "<img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAMZwpc6UgAAAuVJREFUSA10k1tIk2Ecxv/uUzTdbE6HZ20eMFOQDkQHiroRNKnsqqICCxI6XHhdYEUGHtLmMsNITechj0lphoc0XR6wUhwpKzc3nc7NGXnT5dP7Dvc1Ai9+/J/vfZ7n/fPxbQSAPCkgCi1SSEoey+lNsUJSXBBAoZ6+p75HJC1W0IOSIOoqDqKKIl+K9/S5Fi+/TuRTLqO12uhATJcVYqGrAzMVpdAmR0AdQOulgaRwl4nISy2lpVcJSkxXamDq+4C5xjo07Y1DuZQ2C/0pQsxywcsaKdm+FhXAOTuLjZFROIc+wjk8BMf4GAys/FJOv9mlqRUKiq6SkU3/VA2HjucG4RzoY3MIzqkpzGnr8ExGm+UKihLfQONPGa+TwuAYG4O9ox3OznZsdHaw2cGe2+DofY/JGznQBgtvG4JJ059+CPaeHpfHM/+yrXAMDqA78zgqd1KuuOBFiKTZUFWJ1foa2Gurse6Bg2l7TTVs2npMZp2E7lgaVhvqXTnueWZ5d7W2Bub2NtSE+3eLC+rC/CaMFRpY1U9gU5dhjWHfgmsOP19hvtWlS11n/+d4hvvmqudoipXrxAXacN9RQ/5dWO7nw8qwbcMqO+ds5/PuEsP46CFaYmQj4oIGJWm+XMiGKe82zHk3scywbrHC5na4M3zyDu+a8m5Bn3sNjWFCq7igOYTO9ycpYci5iMWrV2DJuYylLZbZdGNlmuN+5tOd4x3eNeRcwqf98dAq6Y64oIn99LrCJdCp/KE/kgrj6XQsnsuCJfsUls5mYHkbuGfJzoSZZU1nMvD9xD6MJ+7EO3ZXrYJSxAX8j9MYKqkeiBQwHiNgdpcP5hL8YNgthyktAosHVDAfTob5aAosDK4XD8YxLxI/98gxn7gDepUPJmMFDEcJaAnzmmghEsQFXLQoSdoTKfzSRQv4FuuNOZU3DHHeWIgTYFRJYOIw7YJpfmZUCfjBMvMsO8M6n1m3N1L4w75pIr+T8xcAAP//mA452QAAAZJJREFUtVLNLgNRFD6ZM4l0QwiZtjOj9ZNY2HgBsbVg7QWsvYCX8ACeoEpQbaJNG4REVFXiN6RV2qXEpgmRcJ2PjozJTNXC4ss999zzfd+5515SSpGDZERbzJqsjmOszuO6uhnW1d2IrmqCew+Qw9mt1FwM6aoknLzFajWiLTt6WL/FsUkQccqghV2bVVkMroVYFRGIPQjqLSB2DFBzKrX7wklFaClhUSjQoGUS2ja5cTjI6kyIFekQYvXRLzRaK3IVAbo/ku5zFj9m+qjbLY74xw2cwwxRV9biIoiXIgATjOjzBmKAGLkrOSvFRdzk2lYP9Tp89+prgILNMI3nolzFbGGCUTnvgBi5ckxXBYufMmF9yi3qjgMNUJQeoPCOzc2SCKFbPCiAuZ9Ibs/mV6mZcAt647YGKMZNsqbWwLjw8EBRxlIwteZGhKa9gt79rwYgrNs0mTf5eV8e/kAg3/EtFaU5r5jfviMDEBP9NJaO8ovgfc2gGT8xv1zHBiAnDZpdMWjeTygo9yeDIJF2+X83+ADzLJ7bhuzPOQAAAABJRU5ErkJggg==\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" --'", " <img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAI16VRn8AAAAgFJREFUSA2ckkFI03Ecxf94FsX+uYO0QzG6pLecpWnUJREEOxnY0cSLQuEl8JJa4MWDRGoQXZTKJKyDoRJhTsX9pxSFXRKFMAJ15ajhxvy/3vu5/7CaRh4eX37f73uft00tANZB2rxxxB9tL6yJddndW7ftiVi3b9ZMvrXX/aD8vvDlpoL86E37XrzPh+RIAKnperhLPXA/3TdTb+11l0/+bEVZC9bajl6JDxRFk2PngC/PgW9hYGsR+L5HemvPu3zyK/dnyV8Fq235x3/2+ROpUD2wPgVsvOZ8xZlFZq/7FORXTvm9Jb8VfL5uB3/0Fm27b2r4yUaBtSecw/+W8Y1COeXF8UoyBZZl5cR6j31MvSwGVvqB1bv/L+aUF0c8lWQKnOaC4sTjwA5mLgLLd6jOQ4g55sURL1Ogtq+37A/uZAnwjr/9UivVcggxx7w44olrvsGzy7m+eL/fxfRp4G0d8L6BuorBziCGOnY12FGKpFMLLNaaqbd3G6JPfpNTnhzxxDUFoWu5pxKPTgBzZwGnkpBLQOQCZl5UY/xBOVWB8YeVSIX5b+tUmKm32fMeok9+k1OeHPHENQWzLXnB5NOTPJQD84R4ilQxeB5YSMtRQVreTnf5vIwmOeKJawomG/POJIYD/APxq86VUZzzVDitCOdCFmnveZTxFApCvDFyfwEAAP//PqCYXgAAAitJREFUjZM9TJNRGIUvRH4iJCSCRGJaAgTaUuhgwqBRB2OiDLo7OBh3dgZdNMREBibdDEYWEyYHBTWRYKRQoLQilBTSllZCXBD/kgLtdzzng9YS2sjw5L33vu97zv35PgPAjN6q8aVHmoGPXcCUD/AzBrzAPGOQMUTCHH8+QONQ5z7Kz5EA19hnTXUDk16kXzghXSODgUvm9O8nTVlrwg3MsHGWMUhCZJEse4CVAiIciyWivOpUHyDTXJtwQXrStQ2MMRWR/vrxzLsOitNgnoULbjzsc+Lenab/8oB1tsEc+2Y8yL7tgPSkaxvoFHd7qi+nR1sy9g50/EXuZPMi8P0KsE1+iqv/+MG5cmLzwn79Anv8HkhHetLNG9DtRHKgcTIz3s675p1GeacxEifrJEW+FpDkOM66GFnjeIWEvciMtUM60jtkoMmj3pO920/PfrNPEeVjJUiSpEihuMYylHFCMK96fyfULx3pHTGga/kNT8W5P88cv+zHjvCLirExTtaJzHJIVOvKL9GEV6M+9UunqIHtaEzZTVelL/X4THj3VQusIN9jmQKrEqRhgihqTmFr1gvVqV59FC/LiR85QS6hHZxvrez+0HdqaOu5Y2PnTdte9pMbWX7zFv8JRc13XrftbQ07NlSn+sKd57Vyg2JRuyHOa66q6y9v1w1+6a9/H73fMK2oudaVV12xfq3lv6JSBXYRT0SRKlJDag+i5vm7LtV/LINSzcdZ/wtpOyUfNaJEKQAAAABJRU5ErkJggg==\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" :s", " <img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAI16VRn8AAAAgFJREFUSA2ckkFI03Ecxf94FsX+uYO0QzG6pLecpWnUJREEOxnY0cSLQuEl8JJa4MWDRGoQXZTKJKyDoRJhTsX9pxSFXRKFMAJ15ajhxvy/3vu5/7CaRh4eX37f73uft00tANZB2rxxxB9tL6yJddndW7ftiVi3b9ZMvrXX/aD8vvDlpoL86E37XrzPh+RIAKnperhLPXA/3TdTb+11l0/+bEVZC9bajl6JDxRFk2PngC/PgW9hYGsR+L5HemvPu3zyK/dnyV8Fq235x3/2+ROpUD2wPgVsvOZ8xZlFZq/7FORXTvm9Jb8VfL5uB3/0Fm27b2r4yUaBtSecw/+W8Y1COeXF8UoyBZZl5cR6j31MvSwGVvqB1bv/L+aUF0c8lWQKnOaC4sTjwA5mLgLLd6jOQ4g55sURL1Ogtq+37A/uZAnwjr/9UivVcggxx7w44olrvsGzy7m+eL/fxfRp4G0d8L6BuorBziCGOnY12FGKpFMLLNaaqbd3G6JPfpNTnhzxxDUFoWu5pxKPTgBzZwGnkpBLQOQCZl5UY/xBOVWB8YeVSIX5b+tUmKm32fMeok9+k1OeHPHENQWzLXnB5NOTPJQD84R4ilQxeB5YSMtRQVreTnf5vIwmOeKJawomG/POJIYD/APxq86VUZzzVDitCOdCFmnveZTxFApCvDFyfwEAAP//PqCYXgAAAitJREFUjZM9TJNRGIUvRH4iJCSCRGJaAgTaUuhgwqBRB2OiDLo7OBh3dgZdNMREBibdDEYWEyYHBTWRYKRQoLQilBTSllZCXBD/kgLtdzzng9YS2sjw5L33vu97zv35PgPAjN6q8aVHmoGPXcCUD/AzBrzAPGOQMUTCHH8+QONQ5z7Kz5EA19hnTXUDk16kXzghXSODgUvm9O8nTVlrwg3MsHGWMUhCZJEse4CVAiIciyWivOpUHyDTXJtwQXrStQ2MMRWR/vrxzLsOitNgnoULbjzsc+Lenab/8oB1tsEc+2Y8yL7tgPSkaxvoFHd7qi+nR1sy9g50/EXuZPMi8P0KsE1+iqv/+MG5cmLzwn79Anv8HkhHetLNG9DtRHKgcTIz3s675p1GeacxEifrJEW+FpDkOM66GFnjeIWEvciMtUM60jtkoMmj3pO920/PfrNPEeVjJUiSpEihuMYylHFCMK96fyfULx3pHTGga/kNT8W5P88cv+zHjvCLirExTtaJzHJIVOvKL9GEV6M+9UunqIHtaEzZTVelL/X4THj3VQusIN9jmQKrEqRhgihqTmFr1gvVqV59FC/LiR85QS6hHZxvrez+0HdqaOu5Y2PnTdte9pMbWX7zFv8JRc13XrftbQ07NlSn+sKd57Vyg2JRuyHOa66q6y9v1w1+6a9/H73fMK2oudaVV12xfq3lv6JSBXYRT0SRKlJDag+i5vm7LtV/LINSzcdZ/wtpOyUfNaJEKQAAAABJRU5ErkJggg==\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(" :S", " <img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAKT1uFhLQAAAl9JREFUSA2EUl1Ik2EY/aDu0obOVjKCfuhu2UVutYyCECKL0WJQQRch0V1B4kVhiDgILCrDhIIuRML+IERISiqUaZtt6lazH0hZlJFMbYgb+2R+p+e85li17OLw8Jzn/PDyfRoAbTlM1xSvn6lfc2C2wdyUaDT3zDZaXqkpO3nel/P/M3zsdJEpcd7cmmq2GPMdW5Dp9cB4ewnGhxtqcifPO3XU5yvKWzBRW3Is2bJuZr6rAojdB6Z8wI/XglAOZCcvd+qop+/Pkr8KYrWmjcnrVj3T5wG+dwPxl4IXEpYH5HkXHfX00Z9b8lvBl3Nmx9zV0rTRWwV8fQhMPAC+yfwfqBM9ffQzZ6kkW6Bp2oq5a6WjmS4bMN4sT78JfG5FInoF6U+/dnI5IM87dYoXH/3MYR5LsgWBU0Vl6fZNC+jfB4x5kRn3YmrkAmpcVsSH6hRHPhfkeaeOenUTP3OYly2QtpXx+pKw8dSGlN8Nf3sVvJ5iNBwxIfnxIvD+DPAuD4TnnTrq6aOfOcxjrnrBY3eBJdViNeDbjtiz/XjUZMdw5yEkItXAm6NARD542C04DIy4Fid38nKnjnr66GcO85irCp6cXLU13bYB6N8JBPcA0YMYuFWGyR4HFkK7ASIov2yQcwmyC887ddTTp/ySwzzmqoLn1YVOvWMzMOAEBiVgsAL6aCW6b29DnasAw23y4SN7c8JFIzt53qnTo5VAQErplxzmMVcVdJ4o3KXflRf4ygH/DoH8ZQE7jLATwXs2XD6+GnfOrkUmJPyQQ03u5Hk3wvJyv33RR29fOZjH3J8AAAD//+OKqOAAAAK/SURBVJ1SXUhTcRS/aigOic3rVJbpxky3u4+Igt6iDygqqOihB4lKKBKiIgmlB8Mn6aVIS0akpZlp0Yvri8rAkvn97ZqKH0vRCimsGMzbPn6dc90kw3pocPa755zf+f3u/9y/AEC4fUi12X93PfBWAtrsQLsV6LIAvVYEeiyoPSPCka9BYNgGDFHNbYPjhEapcx89zKeguXAbcVok+O9kgHUFNijYKKzzVaSHwm9ygA4y6TYBfSbIgyaUHkyC86oOi14SGjEDoxSEi5OSUi89kAR5YImPLsIO6jfngPVYVzEQBEHlKRZfBl8YgU4i9Jrg68xFY1k6PK+NkElsyKlHeIpMxiUF3U8NCHglpd9YlgZfey6dZMkg9NwI1mPdqEHsYWvC/sWGzCBcRBogEY8E+ROta0JCZ30mCncmYuZdNvDRiuGmLFzYkQh3kx4Yk/Bzzo7Qezp5P71cqwmsw3pkEKsY8JroJ06Wal3BJwbFoO+RAYW7VHhQkoqbx9SoPZmMa3lq1BSJuE7I+Y3jajSUaFG4OxFTr8i8X0LQaQDrsJ6iy38Rg5iL25KOLFTo5uHKxeduM6rPaVGdr8bDIi1mac81BRpU08etOytipseM+vMpqKL+vaI0fB+kE7TmYKFcN886ZBCzwiBiotmSEbfvxy2dj038I3ZUnkrGFw/t/oMN38atqLukhX+WVkf5HJlU0knkUcpJnOd4nsQ1yy8efYgiNVWb0uP2Tl9JG5If6xHiG8X7HaNr6LUhPE1ihErutiBEN4d5zOc5no9qMS5/gxVFQUjJTl2z59lpjeNrVcac7DQEQi0bFLEw3RQW5VxuMgS4zzzmk3jK7zp/NVAagpBAA1kU27dmxh+9n7e23F0sNk9c1rYxcs517kd4CX+K/9MgSqbheAqRQk9hprBEkHOux0e5q+GqK1qN+L+1X+nB3zFFOOt8AAAAAElFTkSuQmCC\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(":-S", "<img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAKT1uFhLQAAAl9JREFUSA2EUl1Ik2EY/aDu0obOVjKCfuhu2UVutYyCECKL0WJQQRch0V1B4kVhiDgILCrDhIIuRML+IERISiqUaZtt6lazH0hZlJFMbYgb+2R+p+e85li17OLw8Jzn/PDyfRoAbTlM1xSvn6lfc2C2wdyUaDT3zDZaXqkpO3nel/P/M3zsdJEpcd7cmmq2GPMdW5Dp9cB4ewnGhxtqcifPO3XU5yvKWzBRW3Is2bJuZr6rAojdB6Z8wI/XglAOZCcvd+qop+/Pkr8KYrWmjcnrVj3T5wG+dwPxl4IXEpYH5HkXHfX00Z9b8lvBl3Nmx9zV0rTRWwV8fQhMPAC+yfwfqBM9ffQzZ6kkW6Bp2oq5a6WjmS4bMN4sT78JfG5FInoF6U+/dnI5IM87dYoXH/3MYR5LsgWBU0Vl6fZNC+jfB4x5kRn3YmrkAmpcVsSH6hRHPhfkeaeOenUTP3OYly2QtpXx+pKw8dSGlN8Nf3sVvJ5iNBwxIfnxIvD+DPAuD4TnnTrq6aOfOcxjrnrBY3eBJdViNeDbjtiz/XjUZMdw5yEkItXAm6NARD542C04DIy4Fid38nKnjnr66GcO85irCp6cXLU13bYB6N8JBPcA0YMYuFWGyR4HFkK7ASIov2yQcwmyC887ddTTp/ySwzzmqoLn1YVOvWMzMOAEBiVgsAL6aCW6b29DnasAw23y4SN7c8JFIzt53qnTo5VAQErplxzmMVcVdJ4o3KXflRf4ygH/DoH8ZQE7jLATwXs2XD6+GnfOrkUmJPyQQ03u5Hk3wvJyv33RR29fOZjH3J8AAAD//+OKqOAAAAK/SURBVJ1SXUhTcRS/aigOic3rVJbpxky3u4+Igt6iDygqqOihB4lKKBKiIgmlB8Mn6aVIS0akpZlp0Yvri8rAkvn97ZqKH0vRCimsGMzbPn6dc90kw3pocPa755zf+f3u/9y/AEC4fUi12X93PfBWAtrsQLsV6LIAvVYEeiyoPSPCka9BYNgGDFHNbYPjhEapcx89zKeguXAbcVok+O9kgHUFNijYKKzzVaSHwm9ygA4y6TYBfSbIgyaUHkyC86oOi14SGjEDoxSEi5OSUi89kAR5YImPLsIO6jfngPVYVzEQBEHlKRZfBl8YgU4i9Jrg68xFY1k6PK+NkElsyKlHeIpMxiUF3U8NCHglpd9YlgZfey6dZMkg9NwI1mPdqEHsYWvC/sWGzCBcRBogEY8E+ROta0JCZ30mCncmYuZdNvDRiuGmLFzYkQh3kx4Yk/Bzzo7Qezp5P71cqwmsw3pkEKsY8JroJ06Wal3BJwbFoO+RAYW7VHhQkoqbx9SoPZmMa3lq1BSJuE7I+Y3jajSUaFG4OxFTr8i8X0LQaQDrsJ6iy38Rg5iL25KOLFTo5uHKxeduM6rPaVGdr8bDIi1mac81BRpU08etOytipseM+vMpqKL+vaI0fB+kE7TmYKFcN886ZBCzwiBiotmSEbfvxy2dj038I3ZUnkrGFw/t/oMN38atqLukhX+WVkf5HJlU0knkUcpJnOd4nsQ1yy8efYgiNVWb0uP2Tl9JG5If6xHiG8X7HaNr6LUhPE1ihErutiBEN4d5zOc5no9qMS5/gxVFQUjJTl2z59lpjeNrVcac7DQEQi0bFLEw3RQW5VxuMgS4zzzmk3jK7zp/NVAagpBAA1kU27dmxh+9n7e23F0sNk9c1rYxcs517kd4CX+K/9MgSqbheAqRQk9hprBEkHOux0e5q+GqK1qN+L+1X+nB3zFFOOt8AAAAAElFTkSuQmCC\" alt=\"1F60A\" class=\"emoji\">")
        .Replace(":-s", "<img src=\"data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAABgAAAAYCAYAAADgdz34AAAAHGlET1QAAAACAAAAAAAAAAwAAAAoAAAADAAAAAwAAAKT1uFhLQAAAl9JREFUSA2EUl1Ik2EY/aDu0obOVjKCfuhu2UVutYyCECKL0WJQQRch0V1B4kVhiDgILCrDhIIuRML+IERISiqUaZtt6lazH0hZlJFMbYgb+2R+p+e85li17OLw8Jzn/PDyfRoAbTlM1xSvn6lfc2C2wdyUaDT3zDZaXqkpO3nel/P/M3zsdJEpcd7cmmq2GPMdW5Dp9cB4ewnGhxtqcifPO3XU5yvKWzBRW3Is2bJuZr6rAojdB6Z8wI/XglAOZCcvd+qop+/Pkr8KYrWmjcnrVj3T5wG+dwPxl4IXEpYH5HkXHfX00Z9b8lvBl3Nmx9zV0rTRWwV8fQhMPAC+yfwfqBM9ffQzZ6kkW6Bp2oq5a6WjmS4bMN4sT78JfG5FInoF6U+/dnI5IM87dYoXH/3MYR5LsgWBU0Vl6fZNC+jfB4x5kRn3YmrkAmpcVsSH6hRHPhfkeaeOenUTP3OYly2QtpXx+pKw8dSGlN8Nf3sVvJ5iNBwxIfnxIvD+DPAuD4TnnTrq6aOfOcxjrnrBY3eBJdViNeDbjtiz/XjUZMdw5yEkItXAm6NARD542C04DIy4Fid38nKnjnr66GcO85irCp6cXLU13bYB6N8JBPcA0YMYuFWGyR4HFkK7ASIov2yQcwmyC887ddTTp/ySwzzmqoLn1YVOvWMzMOAEBiVgsAL6aCW6b29DnasAw23y4SN7c8JFIzt53qnTo5VAQErplxzmMVcVdJ4o3KXflRf4ygH/DoH8ZQE7jLATwXs2XD6+GnfOrkUmJPyQQ03u5Hk3wvJyv33RR29fOZjH3J8AAAD//+OKqOAAAAK/SURBVJ1SXUhTcRS/aigOic3rVJbpxky3u4+Igt6iDygqqOihB4lKKBKiIgmlB8Mn6aVIS0akpZlp0Yvri8rAkvn97ZqKH0vRCimsGMzbPn6dc90kw3pocPa755zf+f3u/9y/AEC4fUi12X93PfBWAtrsQLsV6LIAvVYEeiyoPSPCka9BYNgGDFHNbYPjhEapcx89zKeguXAbcVok+O9kgHUFNijYKKzzVaSHwm9ygA4y6TYBfSbIgyaUHkyC86oOi14SGjEDoxSEi5OSUi89kAR5YImPLsIO6jfngPVYVzEQBEHlKRZfBl8YgU4i9Jrg68xFY1k6PK+NkElsyKlHeIpMxiUF3U8NCHglpd9YlgZfey6dZMkg9NwI1mPdqEHsYWvC/sWGzCBcRBogEY8E+ROta0JCZ30mCncmYuZdNvDRiuGmLFzYkQh3kx4Yk/Bzzo7Qezp5P71cqwmsw3pkEKsY8JroJ06Wal3BJwbFoO+RAYW7VHhQkoqbx9SoPZmMa3lq1BSJuE7I+Y3jajSUaFG4OxFTr8i8X0LQaQDrsJ6iy38Rg5iL25KOLFTo5uHKxeduM6rPaVGdr8bDIi1mac81BRpU08etOytipseM+vMpqKL+vaI0fB+kE7TmYKFcN886ZBCzwiBiotmSEbfvxy2dj038I3ZUnkrGFw/t/oMN38atqLukhX+WVkf5HJlU0knkUcpJnOd4nsQ1yy8efYgiNVWb0uP2Tl9JG5If6xHiG8X7HaNr6LUhPE1ihErutiBEN4d5zOc5no9qMS5/gxVFQUjJTl2z59lpjeNrVcac7DQEQi0bFLEw3RQW5VxuMgS4zzzmk3jK7zp/NVAagpBAA1kU27dmxh+9n7e23F0sNk9c1rYxcs517kd4CX+K/9MgSqbheAqRQk9hprBEkHOux0e5q+GqK1qN+L+1X+nB3zFFOOt8AAAAAElFTkSuQmCC\" alt=\"1F60A\" class=\"emoji\">")
        .ToString();

                if (element.InnerHtml != removedByStringBuilder)
                {
                    element.InnerHtml = removedByStringBuilder;
                    return true;
                }
                else
                    return false;
            }
            return false;
        }
        #endregion

        private void Conversations_OnNewMessage(object sender, EventArgs e)
        {
            timerCheckNew.Enabled = false;

            // We check if the window is not focused
            if (!justUnfocused && !this.Focused && !webBrowser1.Focused && notify.Visible &&
                // And if First contact has changed
                previousFirstContact != webBrowser1.Document.GetElementById("conversations").FirstChild)
            {
                // We check if no conversations are selected
                if (getCurrentContactElement() == null || getCurrentContactElement() != webBrowser1.Document.GetElementById("conversations").FirstChild)
                {
                    NotifyMe(webBrowser1.Document.GetElementById("conversations"));
                }
                // If there is a selection, change occurred in conversation
                // So we check if it's not some of the user's pending/unsent message
                else if (previousConversation != webBrowser1.Document.Body.Children[1].InnerHtml)
                {
                    HtmlElementCollection children = this.webBrowser1.Document.GetElementById("messages").Children;
                    if (!children[children.Count - 1].OuterHtml.Contains("data-name=\"Me\""))
                        NotifyMe(webBrowser1.Document.GetElementById("conversations"));
                }
            }
            previousFirstContact = webBrowser1.Document.GetElementById("conversations").FirstChild;
            previousConversation = webBrowser1.Document.Body.Children[1].InnerHtml;

            justUnfocused = false;
            timerCheckNew.Start();
        }

        private void NotifyMe(HtmlElement list)
        {
            if (showFlash)
                Native.Flash(this, (uint)flashCount);

            string name = (list.Children[0].InnerText).Split('×')[0];

            notify.Icon = RemoteMessages.Properties.Resources.xxsmall_favicon_notif;

            if (soundEnabled && !notification.Visible)
            {
                ringtone.Stop();
                ringtone.Play();
            }

            if (showBalloon)
            {
                Native.ShowInactiveTopmost(notification);
                notification.ShowNotification(name, "You just received a new message!", delayBalloon, list.Children[0].InnerHtml);
            }
        }

        void notification_Click(object sender, EventArgs e)
        {
            ShowMe();
            int X = webBrowser1.Document.Body.Children[1].OffsetRectangle.Width + 80;
            int Y = webBrowser1.Document.Body.Children[0].OffsetRectangle.Height + 20;
            DoMouseClick(X, Y);
        }

        #region Form modification (Focus, Size, Closing, Title)
        private void Form1_Shown(object sender, EventArgs e)
        {
            timerTimeOut = new Timer();
            timerTimeOut.Interval = 30000;
            timerTimeOut.Tick += new EventHandler(raiseException);

            using (StreamWriter w = File.AppendText(appFolder + "drafts")) { }
            using (StreamWriter w = File.AppendText(appFolder + "customemoji.cfg")) { }

            bool successful = loadConfig();
            ChangeRingtone();
            ahkProcess = Process.Start(appFolder + "remotemessages_script.exe", '"' + hotkey + '"');
            if (successful)
            {
                if (isAutoUpdate)
                    FindNewIP();
                else
                    DisplayPage();
            }
            loadDraftFromFile();
        }

        private void ChangeRingtone()
        {
            switch (soundIndex)
            {
                case 0:
                    ringtone = new SoundPlayer(RemoteMessages.Properties.Resources.ringtone_3notes);
                    break;
                case 1:
                    ringtone = new SoundPlayer(RemoteMessages.Properties.Resources.ringtone_Calypso);
                    break;
                default:
                    ringtone = new SoundPlayer(RemoteMessages.Properties.Resources.ringtone_3notes);
                    break;
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            notify.Text = "Remote Messages\nClick to Show/Hide";
            notify.Icon = RemoteMessages.Properties.Resources.xxsmall_favicon;

            if (timerCheckNew != null)
                timerCheckNew.Stop();

            if (timerUnfocusing != null)
                timerUnfocusing.Stop();

            webBrowser1.Document.Focus();

            if (previousSelectedContact != null && getCurrentContactElement() == null)
            {
                Rectangle curr = previousSelectedContact.OffsetRectangle;
                int X = webBrowser1.Document.Body.Children[1].OffsetRectangle.Width + 80;
                int Y = webBrowser1.Document.Body.Children[0].OffsetRectangle.Height + 20;
                Y += curr.Y;
                DoMouseClick(X, Y);
                ConversationChanged(false);
            }
        }
        private void Form1_Deactivate(object sender, EventArgs e)
        {
            notify.Text = "Remote Messages\nNo new activity";

            if (documentCompleted && !exceptionRaised)
            {
                previousSelectedContact = getCurrentContactElement();
                previousConversation = webBrowser1.Document.Body.Children[1].InnerHtml;

                if (findCurrentContactName() != "" && isUnfocusing)
                {
                    if (delayUnfocusing == 0)
                        sendEsc(null, null);
                    else
                        timerUnfocusing.Start();
                }

                timerCheckNew.Start();
                previousFirstContact = webBrowser1.Document.GetElementById("conversations").FirstChild;
            }
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized && minimizeToTray)
                Form1_FormClosing(sender, null);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if ((!isExiting && closeToTray) || e == null)
            {
                if (documentCompleted && !exceptionRaised)
                {
                    previousConversation = webBrowser1.Document.Body.Children[1].InnerHtml;
                }
                if (e != null)
                    e.Cancel = true;

                this.Hide();
            }
            else // True exiting
            {
                try
                {
                    ahkProcess.Kill();
                    ahkProcess.Close();
                }
                catch { }
                saveDraftToFile();
            }
        }

        private void DocumentTitleChanged(object sender, EventArgs e)
        {
            // We check if title has really changed
            if (webBrowser1.DocumentTitle != this.Text)
            {
                if (webBrowser1.DocumentTitle != "")
                {
                    this.notify.Text = webBrowser1.DocumentTitle;
                    this.Text = webBrowser1.DocumentTitle;
                }
                else
                {
                    this.notify.Text = "Remote Messages";
                    this.Text = "Remote Messages";
                }
            }
        }
        #endregion

        #region At launch
        private void raiseException(object sender, EventArgs e)
        {
            if (!exceptionRaised)
            {
                timerTimeOut.Stop();
                webBrowser1.Stop();

                progressBar1.Visible = false;
                string msg = "Your device cannot be found.\nPlease click the 'Options' button and check if you have not mistyped your devices' name.\nIf not, check your wifi connection (both on your device and on your computer) and then click 'Retry'.\nClicking Abort will close the application.";

                if (sender != null && e == null)
                    msg = (string)sender;


                using (ErrorForm error = new ErrorForm(msg, (sender != null && e == null)))
                {
                    switch (error.ShowDialog())
                    {
                        case System.Windows.Forms.DialogResult.OK:
                            displayOptions();
                            break;
                        case System.Windows.Forms.DialogResult.Retry:
                            if (isAutoUpdate)
                                FindNewIP();
                            else
                                DisplayPage();
                            break;
                        case System.Windows.Forms.DialogResult.Abort:
                            isExiting = true;
                            this.Close();
                            break;
                        default:
                            displayOptions();
                            break;
                    }
                }
                exceptionRaised = true;
            }
        }
        private void DisplayPage()
        {
            label1.Visible = false;
            documentCompleted = false;
            exceptionRaised = false;

            timerTimeOut.Stop();

            progressBar1.Value = 75;

            HttpWebRequest wrq = (HttpWebRequest)WebRequest.Create(url);
            wrq.Proxy = null;
            HttpWebResponse wrs = null;

            try
            {
                wrs = (HttpWebResponse)wrq.GetResponse();

                if (wrs.StatusCode == HttpStatusCode.OK)
                {
                    webBrowser1.Navigate(url);
                    loggedIn = true;
                }
            }
            catch (System.Net.WebException protocolError)
            {
                if (((HttpWebResponse)protocolError.Response).StatusCode == HttpStatusCode.Unauthorized)
                {
                    using (LoginForm login = new LoginForm())
                    {
                        DialogResult res = login.ShowDialog();
                        if (res == System.Windows.Forms.DialogResult.OK)
                        {
                            webBrowser1.Navigate(String.Format(@"http://{0}:{1}@{2}", login.getUsername(), login.getPassword(), url.Substring(7)));
                            loggedIn = false;
                        }
                        else
                        {
                            label1.Visible = true;
                            progressBar1.Visible = false;
                            exceptionRaised = true;
                        }
                    }
                }
            }

            timerTimeOut.Start();
        }
        ///<summary>
        /// Finds IP of the device and displays the RemoteMessages webpage
        ///</summary>
        private void FindNewIP()
        {
            progressBar1.Value = 0;
            progressBar1.Visible = true;
            Cursor.Current = Cursors.WaitCursor;

            // Start the child process.
            Process p = new Process();
            progressBar1.Value += 10;
            // Redirect the output stream of the child process.
            p.StartInfo.UseShellExecute = false;
            p.StartInfo.RedirectStandardOutput = true;
            p.StartInfo.RedirectStandardInput = true;
            p.StartInfo.CreateNoWindow = true;
            progressBar1.Value += 10;
            p.StartInfo.FileName = "cmd";
            progressBar1.Value += 10;
            p.Start();
            progressBar1.Value += 10;
            p.StandardInput.WriteLine("ping -t " + deviceName + " -n 1\nexit");
            progressBar1.Value += 15;
            string output = p.StandardOutput.ReadToEnd();
            try
            {
                string ip = (output.Split(new string[] { deviceName + ".lan [" }, StringSplitOptions.RemoveEmptyEntries)[1]).Split(']')[0];
                progressBar1.Value += 15;
                url = "http://" + ip + ":" + port;
                UseWaitCursor = false;
                Cursor.Current = Cursors.Default;
                DisplayPage();
            }
            catch
            {
                raiseException(null, null);
            }
        }
        ///<summary>
        /// When page is fully loaded
        ///</summary>
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (!webBrowser1.Document.Domain.Contains("exception"))
            {
                if (!loggedIn)
                {
                    webBrowser1.Navigate(url);
                    exceptionRaised = false;
                    progressBar1.Visible = false;
                }
                timerTimeOut.Stop();
                if (loggedIn && !documentCompleted)
                {
                    documentCompleted = true;
                    webBrowser1.ScrollBarsEnabled = false;

                    string button = "<div title=\"Use this button to convert your text smileys to emoji\" id=\"smiley-button\" class=\"button\" type=\"button\" style=\" height: 18px;  background-repeat: no-repeat;  background-position: center;  width: 26px;  min-width: 26px;  background-image: url(/themes/iOS/emoji.png); background-color: #F6F6F6;  border-color: #B7B7B7;\"></div>";
                    webBrowser1.Document.GetElementById("send").OuterHtml += button;
                    webBrowser1.Document.GetElementById("smiley-button").Click += new HtmlElementEventHandler(SmileyButton_Click);

                    webBrowser1.Document.GetElementById("emoji-pane").MouseUp += new HtmlElementEventHandler(EmojiPane_Click);

                    webBrowser1.Document.GetElementById("conversations").MouseDown += new HtmlElementEventHandler(ConversationsList_MouseDown);

                    webBrowser1.Document.GetElementById("editor").Focusing += new HtmlElementEventHandler(Editor_Focusing);
                    webBrowser1.Document.GetElementById("editor").LosingFocus += new HtmlElementEventHandler(Editor_LosingFocus);

                    webBrowser1.Document.GetElementById("send").MouseDown += new HtmlElementEventHandler(Send_MouseUp);
                    webBrowser1.Document.GetElementById("send").Focusing += new HtmlElementEventHandler(Send_Focusing);
                    webBrowser1.Document.GetElementById("send").LosingFocus += new HtmlElementEventHandler(Send_LosingFocus);

                    progressBar1.Visible = false;

                    timerUnfocusing = new Timer();
                    timerUnfocusing.Interval = delayUnfocusing;
                    timerUnfocusing.Tick += new EventHandler(sendEsc);

                    timerReplacing = new Timer();
                    timerReplacing.Interval = delayReplacing;
                    timerReplacing.Tick += new EventHandler(ConversationChangedTimer);

                    timerCheckNew = new Timer();
                    timerCheckNew.Interval = 500;
                    timerCheckNew.Tick += new EventHandler(Conversations_OnNewMessage);
                    timerCheckNew.Enabled = false;

                    if (!Focused && !webBrowser1.Focused)
                        Form1_Deactivate(null, null);

                    url = webBrowser1.Url.ToString();
                }
                loggedIn = true;
            }
            else
            {
                raiseException(null, null);
            }
        }

        void EmojiPane_Click(object sender, HtmlElementEventArgs e)
        {
            if (e.MouseButtonsPressed == System.Windows.Forms.MouseButtons.Right)
            {
                bool currentUnfocusing = isUnfocusing;
                isUnfocusing = false;

                this.webBrowser1.IsWebBrowserContextMenuEnabled = false;

                HtmlElement el = webBrowser1.Document.GetElementFromPoint(PointToClient(Cursor.Position));
                string currentEmoji = el.OuterHtml;
                string txt = "";
                shortcuts.TryGetValue(currentEmoji, out txt);

                using (CustomEmoji form = new CustomEmoji(currentEmoji, txt))
                {
                    DialogResult res = form.ShowDialog();
                    if (res == System.Windows.Forms.DialogResult.OK)
                    {
                        if (!form.getAlreadyExists())
                            shortcuts.Add(currentEmoji, form.getShortcut());
                        else
                            shortcuts[currentEmoji] = form.getShortcut();
                    }
                }
                isUnfocusing = currentUnfocusing;
            }
        }

        void SmileyButton_Click(object sender, HtmlElementEventArgs e)
        {
            List<string> emojiList = new List<string>(shortcuts.Keys);

            foreach (string s in emojiList)
            {
                if (webBrowser1.Document.GetElementById("editor").InnerHtml != null)
                    webBrowser1.Document.GetElementById("editor").InnerHtml = webBrowser1.Document.GetElementById("editor").InnerHtml.Replace(shortcuts[s], s);
            }

            webBrowser1.Document.GetElementById("editor").Focus();
            SendKeys.Send("{PGDN}");
        }

        private Point getOffset(HtmlElement el)
        {
            //get element pos
            int xPos = el.OffsetRectangle.Left;
            int yPos = el.OffsetRectangle.Top;

            //get the parents pos
            HtmlElement tempEl = el.OffsetParent;
            while (tempEl != null)
            {
                xPos += tempEl.OffsetRectangle.Left;
                yPos += tempEl.OffsetRectangle.Top;

                tempEl = tempEl.OffsetParent;
            }

            return (new Point(xPos, yPos));
        }

        private void Send_MouseUp(object sender, HtmlElementEventArgs e)
        {
            emptyCurrentDraft();
        }

        private void Send_Focusing(object sender, HtmlElementEventArgs e)
        {
            sendFocused = true;
        }

        private void Send_LosingFocus(object sender, HtmlElementEventArgs e)
        {
            sendFocused = false;
        }


        private void Editor_Focusing(object sender, HtmlElementEventArgs e)
        {
            loadDraft();
        }

        private void Editor_LosingFocus(object sender, HtmlElementEventArgs e)
        {
            saveDraft();
        }

        #endregion

        #region Draft management
        /// <summary>
        /// Save content of drafts var to a file
        /// </summary>
        private void saveDraftToFile()
        {
            if (isDrafting)
            {
                if (documentCompleted && !exceptionRaised)
                    saveDraft();
                List<string> contactList = new List<string>(drafts.Keys);
                using (StreamWriter writer = new StreamWriter(appFolder + "drafts"))
                {
                    foreach (string s in contactList)
                    {
                        if (drafts[s] != "" && drafts[s] != null)
                            writer.WriteLine("/|\\" + s + "||--||" + drafts[s]);
                    }
                }
            }
        }
        /// <summary>
        /// Loads from file to drafts var
        /// </summary>
        private void loadDraftFromFile()
        {
            if (isDrafting)
            {
                string previous = "";
                using (StreamReader reader = new StreamReader(appFolder + "drafts"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        if (line != "")
                        {
                            if (line.StartsWith("/|\\"))
                            {
                                string[] parts = line.Substring(3).Split(new string[] { "||--||" }, StringSplitOptions.RemoveEmptyEntries);
                                drafts.Add(parts[0], parts[1]);
                                previous = parts[0];
                            }
                            else
                            {
                                if (previous.Trim() != "")
                                    drafts[previous] += "\n" + line;
                            }
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Finds current draft and saves it into drafts var
        /// </summary>
        private void saveDraft()
        {
            if (isDrafting)
            {
                string currentContact = findCurrentContactName();
                if (currentContact != "")
                {
                    string currentDraft = webBrowser1.Document.GetElementById("editor").InnerHtml;

                    // Finds to which contact it belongs to
                    if (!drafts.ContainsKey(currentContact))
                        drafts.Add(currentContact, currentDraft);
                    else
                        drafts[currentContact] = currentDraft;
                }
            }
        }
        /// <summary>
        /// Finds current contact and loads the matching draft
        /// </summary>
        private void loadDraft()
        {
            if (isDrafting)
            {
                string contact = findCurrentContactName();
                if (drafts.ContainsKey(contact))
                {
                    if (webBrowser1.Document.GetElementById("editor").InnerHtml != drafts[contact])
                        webBrowser1.Document.GetElementById("editor").InnerHtml = drafts[contact];
                }
            }
        }

        private void emptyCurrentDraft()
        {
            if (isDrafting)
            {
                string contact = findCurrentContactName();
                if (drafts.ContainsKey(contact))
                    drafts[contact] = "";
            }
        }
        #endregion

        #region Basic Html manipulations methods
        /// <summary>
        /// Returns as a string the current contact name (followed by a 'x')
        /// </summary>
        private string findCurrentContactName()
        {
            foreach (HtmlElement i in webBrowser1.Document.GetElementById("conversations").Children)
            {
                if (i.InnerHtml.Contains("selected"))
                {
                    return i.InnerText;
                }
            }
            return "";
        }
        /// <summary>
        /// Returns as an HtmlElement the current contact
        /// </summary>
        private HtmlElement getCurrentContactElement()
        {
            if (documentCompleted)
            {
                foreach (HtmlElement i in webBrowser1.Document.GetElementById("conversations").Children)
                {
                    if (i.InnerHtml.Contains("selected"))
                    {
                        return i;
                    }
                }
            }
            return null;
        }
        #endregion

        #region Notify Icon
        private void contextShowHide_Click(object sender, EventArgs e)
        {
            ShowMeToggle(sender, null);
        }

        private void contextExit_Click(object sender, EventArgs e)
        {
            isExiting = true;
            this.Close();
        }

        private void contextOptions_Click(object sender, EventArgs e)
        {
            displayOptions();
        }


        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!aboutDisplayed)
            {
                bool currentUnfocusing = isUnfocusing;
                using (AboutForm about = new AboutForm(VERSION))
                {
                    aboutDisplayed = true;
                    about.ShowDialog();
                    aboutDisplayed = false;
                }
                isUnfocusing = false;
                isUnfocusing = currentUnfocusing;
            }
        }
        #endregion

        #region Simulated inputs
        /// <summary>
        /// Simulates a mouse click at X;Y
        /// </summary>
        public void DoMouseClick(int X, int Y)
        {
            Point prevPos = Cursor.Position;
            //Call the imported function with the cursor's current position
            Cursor.Position = PointToScreen(new Point(X, Y));
            fakeClick = true;
            Native.mouse_event(Native.MOUSEEVENTF_LEFTDOWN | Native.MOUSEEVENTF_LEFTUP, (uint)X, (uint)Y, 0, 0);
            Cursor.Position = prevPos;
        }

        /// <summary>
        /// Simulates a keypress of Escape
        /// </summary>
        private void sendEsc(object sender, EventArgs e)
        {
            if (!webBrowser1.Document.Focused && getCurrentContactElement() != null
                && !previousConversation.Contains("pending") && !previousConversation.Contains("unsent"))
            {
                timerUnfocusing.Stop();
                Native.PostMessage(webBrowser1.Handle, Native.WM_KEYDOWN, (IntPtr)Keys.Escape, IntPtr.Zero);
                justUnfocused = true;
            }
        }
        #endregion

        #region Methods allowing the one instance only SHOWME + ghostmode
        ///<summary>
        /// This method is called when another instance of this app is launched
        ///</summary>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == Native.WM_SHOWME)
            {
                ShowMe(Native.WM_SHOWME);
            }
            else if (m.Msg == Native.WM_SHOWME_AHK && !loginDisplayed)
            {
                ShowMe(Native.WM_SHOWME_AHK);
            }
            base.WndProc(ref m);
        }
        ///<summary>
        /// This method is called when another instance of this app is launched
        ///</summary>
        private void ShowMe(object sender = null, EventArgs e = null)
        {
            if (sender != null && sender.GetType().Name == "Int32" && (int)sender == Native.WM_SHOWME_AHK)
            {
                if (isGhostMode)
                {
                    if (notify.Visible)
                    {
                        if (this.Focused || this.webBrowser1.Focused)
                        {
                            notify.Visible = false;
                            this.Hide();
                        }
                        else
                            this.Show();
                    }
                    else
                    {
                        bool loop;
                        loginDisplayed = true;
                        using (LoginForm login = new LoginForm(true))
                        {
                            do
                            {
                                loop = false;
                                login.Activate();
                                DialogResult res = login.ShowDialog();
                                if (res == System.Windows.Forms.DialogResult.OK)
                                {
                                    if (login.getGhostModePassword() == password)
                                    {
                                        notify.Visible = true;
                                        this.Show();
                                    }
                                    else
                                        loop = true;
                                }
                                login.setPasswordClear();
                            } while (loop);
                        }
                        loginDisplayed = false;
                    }
                }
                else
                    ShowMeToggle(sender, null);
            }
            else
                this.Show();
        }
        ///<summary>
        /// This method is called when another instance of this app is launched
        ///</summary>
        private void ShowMeToggle(object sender, MouseEventArgs e)
        {
            if (e == null || e.Button == System.Windows.Forms.MouseButtons.Left)
            {


                if ((!this.Visible) || (sender.GetType().Name == "Int32" && (int)sender == Native.WM_SHOWME_AHK && (!this.Focused && !this.webBrowser1.Focused)))
                    this.Show();
                else
                    Form1_FormClosing(sender, null);
            }
        }
        #endregion


        private void loadShortcutsFromFile()
        {
            using (StreamReader reader = new StreamReader(appFolder + "customemoji.cfg"))
            {
                while (!reader.EndOfStream)
                {
                    string line = reader.ReadLine();
                    if (line != "")
                    {
                        string[] parts = line.Split(new string[] { "||--||" }, StringSplitOptions.RemoveEmptyEntries);
                        shortcuts.Add(parts[0], parts[1]);
                    }
                }
            }
        }


        public new void Show()
        {
            if (WindowState == FormWindowState.Minimized)
                WindowState = FormWindowState.Normal;
            // make our form jump to the top of everything
            TopMost = true;
            base.Show();
            TopMost = false;
            this.Activate();
        }
    }
}
