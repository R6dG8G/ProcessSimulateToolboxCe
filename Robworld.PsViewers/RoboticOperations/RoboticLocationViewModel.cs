using Robworld.PsPublicLibrary.Mvvm;
using Tecnomatix.Engineering;

namespace Robworld.PsViewers.RoboticOperations
{
    internal class RoboticLocationViewModel : RwViewModelBase
    {
        #region Fields
        private readonly TxObjectBase location;
        private string pathName;
        private string reachabilityResult;
        private string axisLimitsResult;
        private string collisionStatusResult;
        #endregion

        #region Properties
        public string PathId { get; }

        public string PathName
        {
            get { return pathName; }
            set
            {
                if (pathName != value)
                {
                    pathName = value;
                    OnPropertyChanged();
                }
            }
        }

        public string Name
        {
            get { return (location as ITxObject).Name; }
            set
            {
                if ((location as ITxObject).Name != value)
                {
                    (location as ITxObject).Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string ReachabilityResult
        {
            get { return reachabilityResult; }
            set
            {
                if (reachabilityResult != value)
                {
                    reachabilityResult = value;
                    OnPropertyChanged();
                }
            }
        }

        public string AxisLimitsResult
        {
            get { return axisLimitsResult; }
            set
            {
                if (axisLimitsResult != value)
                {
                    axisLimitsResult = value;
                    OnPropertyChanged();
                }
            }
        }

        public string CollisionStatusResult
        {
            get { return collisionStatusResult; }
            set
            {
                if (collisionStatusResult != value)
                {
                    collisionStatusResult = value;
                    OnPropertyChanged();
                }
            }
        }

        public double X { get { return (location as ITxLocatableObject).AbsoluteLocation.Translation.X; } }

        public double Y { get { return (location as ITxLocatableObject).AbsoluteLocation.Translation.Y; } }

        public double Z { get { return (location as ITxLocatableObject).AbsoluteLocation.Translation.Z; } }

        public double Rx { get { return (location as ITxLocatableObject).AbsoluteLocation.RotationRPY_XYZ.X; } }

        public double Ry { get { return (location as ITxLocatableObject).AbsoluteLocation.RotationRPY_XYZ.Y; } }

        public double Rz { get { return (location as ITxLocatableObject).AbsoluteLocation.RotationRPY_XYZ.Z; } }
        #endregion

        #region Constructors
        public RoboticLocationViewModel(TxObjectBase location, string pathName, string pathId)
        {
            this.location = location;
            PathName = pathName;
            PathId = pathId;
        }
        #endregion
    }
}