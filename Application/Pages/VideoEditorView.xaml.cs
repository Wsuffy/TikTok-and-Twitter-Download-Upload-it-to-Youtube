using System;
using System.Windows;
using Application.Pages.VideoEditor.Contracts;

namespace Application.Pages;

public partial class VideoEditorView : Window
{
    private readonly IVideoEditorModel _model;
    
    public VideoEditorView(IVideoEditorModel model)
    {
        _model = model;
        InitializeComponent();
    }

    private void PrivateVideo_Checked(object sender, EventArgs e)
    {
        _model.ChangePrivateVideoStatus();
    }

    private void Close(object sender, RoutedEventArgs e)
    {
        this.Hide();
    }

    private void Submit(object sender, RoutedEventArgs e)
    {
        _model.Submit(TittleText.Text, DescriptionText.Text);
    }

    public void SetPath(string path)
    {
        _model.SetVideoPath(path);
    }
}