using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Test.ViewModels.Base
{
    public abstract class ViewModel : INotifyPropertyChanged
    {
        private bool _disposed;
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected virtual bool Set<T>(ref T field, T value, [CallerMemberName] string PropertyName = null)
        {
            if (Equals(field, value)) return false;
            field = value;
            OnPropertyChanged(PropertyName);
            return true;
        }

        public void Desponse()
        {
            Disponse(true);
        }

        protected virtual void Disponse(bool disponsig)
        {
            if (!disponsig || _disposed) return;
            _disposed = true;
            GC.SuppressFinalize(this);
        }
    }
}