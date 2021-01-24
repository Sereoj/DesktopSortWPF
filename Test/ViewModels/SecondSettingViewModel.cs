using Test.ViewModels.Base;

namespace Test.ViewModels
{
    internal class SecondSettingViewModel : ViewModel
    {
        private bool _isBackgound;
        public bool IsBackground { set =>Set(ref _isBackgound, value); get => _isBackgound; }


        public SecondSettingViewModel()
        {
            IsBackground = true;
        }

    }
}
