using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Robworld.PsPublicLibrary.Mvvm
{
    /// <summary>
    /// Represents the base of all ViewModels
    /// </summary>
    public class RwViewModelBase : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Methods
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 
        #endregion
    }
}
