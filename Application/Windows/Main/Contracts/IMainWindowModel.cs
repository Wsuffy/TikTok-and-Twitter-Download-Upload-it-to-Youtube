using System;
using System.Threading.Tasks;
using Uploader.Core;
using User.Core;

namespace Application.Windows.Main.Contracts;

public interface IMainWindowModel
{
    void SetUserSecret(UserSecretEntity secretEntity);
    void SetCurrentPath(string path);
}