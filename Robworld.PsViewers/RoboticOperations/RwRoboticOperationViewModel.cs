using Robworld.PsPublicLibrary.Mvvm;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;
using Tecnomatix.Engineering;

namespace Robworld.PsViewers.RoboticOperations
{
    public class RwRoboticOperationViewModel : RwViewModelBase
    {
        #region Fields
        private static readonly Random random = new Random();
        private ObservableCollection<RoboticLocationViewModel> locationOperations;
        #endregion

        #region Properties
        public ICollectionView LocationOperations { get; set; }

        public ICommand AddSelectedPathsToViewerCommand { get; private set; }

        public ICommand AddPathsFromPathEditorToViewerCommand { get; private set; }

        public ICommand RemovePathsFromViewerCommand { get; private set; }

        public ICommand ReachabilityCommand { get; private set; }

        public ICommand AxisLimitsCommand { get; private set; }

        public ICommand CollisionStatusCommand { get; private set; }

        public ICommand BindToExcelCommand { get; private set; }

        public ICommand ShowOptionsCommand { get; private set; }
        #endregion

        #region Constructors
        public RwRoboticOperationViewModel()
        {
            locationOperations = new ObservableCollection<RoboticLocationViewModel>();
            AddSelectedPathsToViewerCommand = new RwActionCommand(OnAddSelectedPathsToViewerExecuted, OnAddSelectedPathsToViewerCanExecute);
            AddPathsFromPathEditorToViewerCommand = new RwActionCommand(OnAddPathsFromPathEditorToViewerExecuted, OnAddPathsFromPathEditorToViewerCanExecute);
            RemovePathsFromViewerCommand = new RwActionCommand(OnRemovePathsFromViewerExecuted, OnRemovePathsFromViewerCanExecute);
            ReachabilityCommand = new RwActionCommand(OnReachabilityExecuted, OnReachabilityCanExecute);
            AxisLimitsCommand = new RwActionCommand(OnAxisLimitsExecuted, OnAxisLimitsCanExecute);
            CollisionStatusCommand = new RwActionCommand(OnCollisionStatusExecuted, OnCollisionStatusCanExecute);
            BindToExcelCommand = new RwActionCommand(OnBindToExcelExecuted, OnBindToExcelCanExecute);
            ShowOptionsCommand = new RwActionCommand(OnShowOptionsExecuted, OnShowOptionsCanExecute);
            CreateView();
        }
        #endregion

        #region Methods
        private void CreateView()
        {
            LocationOperations = CollectionViewSource.GetDefaultView(locationOperations);
            using (LocationOperations.DeferRefresh())
            {
                LocationOperations.GroupDescriptions.Clear();
                LocationOperations.GroupDescriptions.Add(new PropertyGroupDescription("PathId"));
            }
        }

        private void OnAddSelectedPathsToViewerExecuted(object obj)
        {
            TxObjectList selectedOperations = TxApplication.ActiveSelection.GetFilteredItems(new TxTypeFilter(typeof(ITxRoboticOperation)));
            if (selectedOperations == null || selectedOperations.Count == 0) return;
            foreach (ITxCompoundOperation operation in selectedOperations)
            {
                TxObjectList locations = operation.GetAllDescendants(new TxTypeFilter(typeof(ITxRoboticLocationOperation)));
                foreach (TxObjectBase location in locations)
                {
                    locationOperations.Add(new RoboticLocationViewModel(location, operation.Name, operation.Id));
                }
            }
        }

        private void OnAddPathsFromPathEditorToViewerExecuted(object obj)
        {
            throw new NotImplementedException();
        }

        private void OnRemovePathsFromViewerExecuted(object obj)
        {
            throw new NotImplementedException();
        }

        private void OnReachabilityExecuted(object obj)
        {
            foreach (RoboticLocationViewModel location in locationOperations)
            {
                int distance = random.Next(0, 200);
                location.ReachabilityResult = (distance < 100) ? "nOk" : "Ok";
            }
        }

        private void OnAxisLimitsExecuted(object obj)
        {
            foreach (RoboticLocationViewModel location in locationOperations)
            {
                if (location.IsProcessLocation)
                {
                    location.CheckForAxisLimits();
                }


                //double axisValue = -360 + random.NextDouble() * 720;
                //location.AxisLimitsResult = (Math.Abs(axisValue) <= 5.0 || Math.Abs(axisValue) >= 355.0) ? "nOk" : "Ok";
            }
        }

        private void OnCollisionStatusExecuted(object obj)
        {
            foreach (RoboticLocationViewModel location in locationOperations)
            {
                if (location.IsProcessLocation)
                {
                    location.CheckForCollisions();
                }
            }
        }

        private void OnBindToExcelExecuted(object obj)
        {
            throw new NotImplementedException();
        }

        private void OnShowOptionsExecuted(object obj)
        {
            throw new NotImplementedException();
        }

        private bool OnAddSelectedPathsToViewerCanExecute(object arg)
        {
            return true;
        }

        private bool OnAddPathsFromPathEditorToViewerCanExecute(object arg)
        {
            return false;
        }

        private bool OnRemovePathsFromViewerCanExecute(object arg)
        {
            return false;
        }

        private bool OnReachabilityCanExecute(object arg)
        {
            return true;
        }

        private bool OnAxisLimitsCanExecute(object arg)
        {
            return true;
        }

        private bool OnCollisionStatusCanExecute(object arg)
        {
            return true;
        }

        private bool OnBindToExcelCanExecute(object arg)
        {
            return false;
        }

        private bool OnShowOptionsCanExecute(object arg)
        {
            return false;
        }
        #endregion
    }
}
