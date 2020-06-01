using Robworld.PsPublicLibrary.Mvvm;
using Robworld.PsPublicLibrary.Utilities;
using System.Collections;
using System.Windows.Input;
using Tecnomatix.Engineering;
using Tecnomatix.Engineering.Ui.WPF;

namespace Robworld.PsViewers.DeltaAxis
{
    /// <summary>
    /// View delta axis values of a robot on different locations
    /// </summary>
    public class RwDeltaAxisViewModel : RwViewModelBase
    {
        #region Fields
        private double axis1ValueLocation1;
        private double axis2ValueLocation1;
        private double axis3ValueLocation1;
        private double axis4ValueLocation1;
        private double axis5ValueLocation1;
        private double axis6ValueLocation1;
        private double axis1ValueLocation2;
        private double axis2ValueLocation2;
        private double axis3ValueLocation2;
        private double axis4ValueLocation2;
        private double axis5ValueLocation2;
        private double axis6ValueLocation2;
        private double axis1DeltaValue;
        private double axis2DeltaValue;
        private double axis3DeltaValue;
        private double axis4DeltaValue;
        private double axis5DeltaValue;
        private double axis6DeltaValue;
        #endregion

        #region Properties
        /// <summary>
        /// Get the caption for the first location
        /// </summary>
        public string CaptionLocation1 { get; }

        /// <summary>
        /// Get the caption for the second location
        /// </summary>
        public string CaptionLocation2 { get; }

        /// <summary>
        /// Get or set the value of axis 1 at the first location
        /// </summary>
        public double Axis1ValueLocation1
        {
            get { return axis1ValueLocation1; }
            private set
            {
                if (axis1ValueLocation1 != value)
                {
                    axis1ValueLocation1 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the delta value of axis 1
        /// </summary>
        public double Axis1DeltaValue
        {
            get { return axis1DeltaValue; }
            private set
            {
                if (axis1DeltaValue != value)
                {
                    axis1DeltaValue = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the value of axis 1 at the second location
        /// </summary>
        public double Axis1ValueLocation2
        {
            get { return axis1ValueLocation2; }
            private set
            {
                if (axis1ValueLocation2 != value)
                {
                    axis1ValueLocation2 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the value of axis 2 at the first location
        /// </summary>
        public double Axis2ValueLocation1
        {
            get { return axis2ValueLocation1; }
            private set
            {
                if (axis2ValueLocation1 != value)
                {
                    axis2ValueLocation1 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the delta value of axis 2
        /// </summary>
        public double Axis2DeltaValue
        {
            get { return axis2DeltaValue; }
            private set
            {
                if (axis2DeltaValue != value)
                {
                    axis2DeltaValue = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the value of axis 2 at the second location
        /// </summary>
        public double Axis2ValueLocation2
        {
            get { return axis2ValueLocation2; }
            private set
            {
                if (axis2ValueLocation2 != value)
                {
                    axis2ValueLocation2 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the value of axis 3 at the first location
        /// </summary>
        public double Axis3ValueLocation1
        {
            get { return axis3ValueLocation1; }
            private set
            {
                if (axis3ValueLocation1 != value)
                {
                    axis3ValueLocation1 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the delta value of axis 3
        /// </summary>
        public double Axis3DeltaValue
        {
            get { return axis3DeltaValue; }
            private set
            {
                if (axis3DeltaValue != value)
                {
                    axis3DeltaValue = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the value of axis 3 at the second location
        /// </summary>
        public double Axis3ValueLocation2
        {
            get { return axis3ValueLocation2; }
            private set
            {
                if (axis3ValueLocation2 != value)
                {
                    axis3ValueLocation2 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the value of axis 4 at the first location
        /// </summary>
        public double Axis4ValueLocation1
        {
            get { return axis4ValueLocation1; }
            private set
            {
                if (axis4ValueLocation1 != value)
                {
                    axis4ValueLocation1 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the delta value of axis 4
        /// </summary>
        public double Axis4DeltaValue
        {
            get { return axis4DeltaValue; }
            private set
            {
                if (axis4DeltaValue != value)
                {
                    axis4DeltaValue = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the value of axis 4 at the second location
        /// </summary>
        public double Axis4ValueLocation2
        {
            get { return axis4ValueLocation2; }
            private set
            {
                if (axis4ValueLocation2 != value)
                {
                    axis4ValueLocation2 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the value of axis 5 at the first location
        /// </summary>
        public double Axis5ValueLocation1
        {
            get { return axis5ValueLocation1; }
            private set
            {
                if (axis5ValueLocation1 != value)
                {
                    axis5ValueLocation1 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the delta value of axis 5
        /// </summary>
        public double Axis5DeltaValue
        {
            get { return axis5DeltaValue; }
            private set
            {
                if (axis5DeltaValue != value)
                {
                    axis5DeltaValue = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the value of axis 5 at the second location
        /// </summary>
        public double Axis5ValueLocation2
        {
            get { return axis5ValueLocation2; }
            private set
            {
                if (axis5ValueLocation2 != value)
                {
                    axis5ValueLocation2 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the value of axis 6 at the first location
        /// </summary>
        public double Axis6ValueLocation1
        {
            get { return axis6ValueLocation1; }
            private set
            {
                if (axis6ValueLocation1 != value)
                {
                    axis6ValueLocation1 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the delta value of axis 6
        /// </summary>
        public double Axis6DeltaValue
        {
            get { return axis6DeltaValue; }
            private set
            {
                if (axis6DeltaValue != value)
                {
                    axis6DeltaValue = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Get or set the value of axis 6 at the second location
        /// </summary>
        public double Axis6ValueLocation2
        {
            get { return axis6ValueLocation2; }
            private set
            {
                if (axis6ValueLocation2 != value)
                {
                    axis6ValueLocation2 = value;
                    OnPropertyChanged();
                }
            }
        }

        /// <summary>
        /// Represents the event on picking a location
        /// </summary>
        public ICommand LocationPickedEvent { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Create an instance of the delta axis view model
        /// </summary>
        public RwDeltaAxisViewModel()
        {
            CaptionLocation1 = "Reference location 1:";
            CaptionLocation2 = "Reference location 2:";
            LocationPickedEvent = new RwActionCommand(OnLocationPickedExecuted, OnLocationPickedCanExecute);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Get the picked location and assign the joint values of the robot for that location
        /// </summary>
        /// <param name="obj">The TxObjectEditBox control in the view</param>
        private void OnLocationPickedExecuted(object obj)
        {
            if (obj is TxObjEditBoxControl control)
            {
                control.LoseFocus();
                if(control.Object is ITxRoboticLocationOperation location)
                {
                    string whichLocation = control.Tag.ToString();
                    ArrayList jointValues = GetRobotPoseDataFromLocation(location);
                    if (jointValues == null) return;
                    
                    if(whichLocation.Equals("1"))
                    {
                        AssignLocation1Values(jointValues);
                    }
                    else
                    {
                        AssignLocation2Values(jointValues);
                    }
                }
            }
        }

        /// <summary>
        /// Get the joint values of the robot assigned to a location
        /// </summary>
        /// <param name="location">The location from which to get the joint values</param>
        /// <returns>The joint values</returns>
        private ArrayList GetRobotPoseDataFromLocation(ITxRoboticLocationOperation location)
        {
            ITxRobot robot = location.ParentRoboticOperation.Robot;
            if (robot != null)
            {
                TxPoseData pose = robot.GetPoseAtLocation(location);
                if (pose != null)
                {
                    return pose.JointValues;
                }
            }
            return null;
        }

        /// <summary>
        /// Assign the joint values of the robot at the first location
        /// </summary>
        /// <param name="jointValues">The joint values of the robot in radians</param>
        private void AssignLocation1Values(ArrayList jointValues)
        {
            Axis1ValueLocation1 = RwMathUtilities.Rad2Deg((double)jointValues[0]);
            Axis2ValueLocation1 = RwMathUtilities.Rad2Deg((double)jointValues[1]);
            Axis3ValueLocation1 = RwMathUtilities.Rad2Deg((double)jointValues[2]);
            Axis4ValueLocation1 = RwMathUtilities.Rad2Deg((double)jointValues[3]);
            Axis5ValueLocation1 = RwMathUtilities.Rad2Deg((double)jointValues[4]);
            Axis6ValueLocation1 = RwMathUtilities.Rad2Deg((double)jointValues[5]);
            CalculateAxisDeltaValues();
        }

        /// <summary>
        /// Assign the joint values of the robot at the second location
        /// </summary>
        /// <param name="jointValues">The joint values of the robot in radians</param>
        private void AssignLocation2Values(ArrayList jointValues)
        {
            Axis1ValueLocation2 = RwMathUtilities.Rad2Deg((double)jointValues[0]);
            Axis2ValueLocation2 = RwMathUtilities.Rad2Deg((double)jointValues[1]);
            Axis3ValueLocation2 = RwMathUtilities.Rad2Deg((double)jointValues[2]);
            Axis4ValueLocation2 = RwMathUtilities.Rad2Deg((double)jointValues[3]);
            Axis5ValueLocation2 = RwMathUtilities.Rad2Deg((double)jointValues[4]);
            Axis6ValueLocation2 = RwMathUtilities.Rad2Deg((double)jointValues[5]);
            CalculateAxisDeltaValues();
        }

        /// <summary>
        /// Calculate the delta values for all axis
        /// </summary>
        private void CalculateAxisDeltaValues()
        {
            Axis1DeltaValue = Axis1ValueLocation1 - Axis1ValueLocation2;
            Axis2DeltaValue = Axis2ValueLocation1 - Axis2ValueLocation2;
            Axis3DeltaValue = Axis3ValueLocation1 - Axis3ValueLocation2;
            Axis4DeltaValue = Axis4ValueLocation1 - Axis4ValueLocation2;
            Axis5DeltaValue = Axis5ValueLocation1 - Axis5ValueLocation2;
            Axis6DeltaValue = Axis6ValueLocation1 - Axis6ValueLocation2;
        }

        /// <summary>
        /// Decide if the pick event for choosing a location can be enabled
        /// </summary>
        /// <param name="arg">Not in use</param>
        /// <returns>Returns always true</returns>
        private bool OnLocationPickedCanExecute(object arg)
        {
            return true;
        }
        #endregion
    }
}
