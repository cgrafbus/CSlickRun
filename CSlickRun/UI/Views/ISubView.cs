namespace CSlickRun.UI.Views;

public interface ISubView
{
    public bool OnExit();

    public void OnEnter();

    public void OnLayoutChanged();
}