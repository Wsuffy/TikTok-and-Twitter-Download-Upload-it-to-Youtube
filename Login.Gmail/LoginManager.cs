using Login.Core;
using Microsoft.Playwright;

namespace Login.Gmail;

public class LoginManager : ILoginManager
{
    public async Task<IPage> LoginAsync(IPage currentPage, string login, string password)
    {
        const string youtubeMain = "https://www.youtube.com/";
        
        await currentPage.GetByText("Sign in").First.ClickAsync();
        await currentPage.Locator("//input[@type='email']").FillAsync(login);
        await currentPage.Locator(
                "//button[@class='VfPpkd-LgbsSe VfPpkd-LgbsSe-OWXEXe-k8QpJ VfPpkd-LgbsSe-OWXEXe-dgl2Hf nCP5yc AjY5Oe DuMIQc LQeN7 qIypjc TrZEUc lw1w4b']")
            .ClickAsync();
        await currentPage.Locator("//input[@type='password']").First.FillAsync(password);
        await currentPage.Locator(
                "//button[@class='VfPpkd-LgbsSe VfPpkd-LgbsSe-OWXEXe-k8QpJ VfPpkd-LgbsSe-OWXEXe-dgl2Hf nCP5yc AjY5Oe DuMIQc LQeN7 qIypjc TrZEUc lw1w4b']")
            .ClickAsync();
        await currentPage.WaitForURLAsync(youtubeMain);
        return currentPage;
    }
}