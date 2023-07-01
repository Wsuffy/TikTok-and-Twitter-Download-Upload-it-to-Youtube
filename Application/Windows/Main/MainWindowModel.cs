using System;
using System.Threading.Tasks;
using Application.Windows.Main.Contracts;
using Uploader.Core;
using User.Core;

namespace Application.Windows.Main;

public class MainWindowModel : IMainWindowModel, IMainWindowSignalBus
{
    public Action<string> SavePathChanged { get; set; }
    public Func<UserSecretEntity, Task> UserSecretChanged { get; set; }

    public string CurrentSavePath
    {
        get => _currentSavePath;
        set
        {
            _currentSavePath = value;
            SavePathChanged?.Invoke(value);
        }
    }

    private string _currentSavePath;
    
    

    public void SetUserSecret(UserSecretEntity userSecretEntity)
    {
        UserSecretChanged?.Invoke(userSecretEntity);
    }
    
    public void SetCurrentPath(string path)
    {
        CurrentSavePath = path;
    }

}