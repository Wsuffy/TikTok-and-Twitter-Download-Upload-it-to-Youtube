using System.Windows;
using System.Windows.Forms;
using Application.Pages;
using Application.Pages.VideoEditor.Contracts;
using Application.Windows.Main.Contracts;
using Microsoft.WindowsAPICodePack.Dialogs;
using Uploader.Core;
using User.Core;

namespace Application.Windows.Main
{
    public partial class MainWindowView : Window
    {
        private readonly IMainWindowModel _model;
        private readonly IVideoEditorSignalBus _videoEditorBus;
        private readonly IVideoEditorModel _pageModel;

        public MainWindowView(IMainWindowModel model, IVideoEditorSignalBus videoEditorBus, IVideoEditorModel pageModel)
        {
            _model = model;
            _videoEditorBus = videoEditorBus;
            _pageModel = pageModel;
            InitializeComponent();
        }


        private void BrowseFolder_Click(object sender, RoutedEventArgs routedEventArgs)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                directoryPath.Text = dialog.FileName;
                _model.SetCurrentPath(dialog.FileName);
            }
        }


        private void DownloadTikTok(object sender, RoutedEventArgs e)
        {
            _videoEditorBus.DownloadTikTokEvent?.Invoke(tikTokTextBox.Text);
        }

        private void DownloadTwitter(object sender, RoutedEventArgs e)
        {
            _videoEditorBus.DownloadTwitterEvent?.Invoke(twitterTextBox.Text);
        }

        private void ManualUpload(object sender, RoutedEventArgs routedEventArgs)
        {
            _pageModel.Submit(TittleText.Text, DescriptionText.Text);
        }

        private void DownloadAndUploadTikTok(object sender, RoutedEventArgs e)
        {
            _videoEditorBus.DownloadTikTokEvent?.Invoke(tikTokTextBox.Text);
            
            _pageModel.Submit(TittleText.Text, DescriptionText.Text);
        }

        private void DownloadAndUploadTwitter(object sender, RoutedEventArgs e)
        {
            _videoEditorBus.DownloadTwitterEvent?.Invoke(twitterTextBox.Text);
            _pageModel.Submit(TittleText.Text, DescriptionText.Text);
        }

        private void BrowseForVideo(object sender, RoutedEventArgs e)
        {
            var openFileDialog = new OpenFileDialog();
            openFileDialog.Title = "Select File";
            openFileDialog.ShowDialog();

            if (openFileDialog.FileName != "")
            {
                ManualTextBox.Text = openFileDialog.FileName;
                _pageModel.SetVideoPath(ManualTextBox.Text);
            }
        }

        private void SetUserInfo_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UserURL.Text) || string.IsNullOrEmpty(LoginTextBox.Text) ||
                string.IsNullOrEmpty(PasswordTextBox.Text))
                return;

            var userSecret = new UserSecretEntity
            {
                ShortUrl = UserURL.Text,
                Login = LoginTextBox.Text,
                Password = PasswordTextBox.Text
            };

            _model.SetUserSecret(userSecret);
        }
    }
}