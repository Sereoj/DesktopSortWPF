namespace DesktopSort.UI.ViewModels
{
    public interface IApplicationContentView
    {
        string Name { get; }

        bool IsLoading { get; set; }

        void Init();
    }
}