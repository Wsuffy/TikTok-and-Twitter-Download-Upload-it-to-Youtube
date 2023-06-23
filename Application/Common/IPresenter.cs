namespace Application.Common;

public interface IPresenter<in TView> where TView : IView
{
    void SetView(TView view);
}