using Robworld.PsPublicLibrary.Mvvm;
using Robworld.PsPublicLibrary.Utilities;
using System;
using System.Collections;
using Tecnomatix.Engineering;
using Tecnomatix.Engineering.Olp.OLP_Utilities;

namespace Robworld.PsViewers.RoboticOperations
{
    internal class RoboticLocationViewModel : RwViewModelBase
    {
        #region Fields
        private readonly TxObjectBase location;
        private bool isFailure;
        private string pathName;
        private string reachabilityResult;
        private string axisLimitsResult;
        private string collisionStatusResult;
        private double deltaRotationalAxisLimit;
        private double deltaLinearAxisLimit;
        private double deltaA5Singularity;
        private double deltaA23Singularity;
        private double deltaA1Singularity;
        private ArrayList lowerLimits;
        private ArrayList upperLimits;
        private ArrayList currentJointValues;
        #endregion

        #region Properties
        public bool IsProcessLocation { get; }

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
            IsProcessLocation = (location is TxWeldLocationOperation) ? true : false;
            PathName = pathName;
            PathId = pathId;
            deltaRotationalAxisLimit = 3.0;
            deltaLinearAxisLimit = 50.0;
            deltaA1Singularity = 20.0;
            deltaA23Singularity = 10.0;
            deltaA5Singularity = 5.0;
        }
        #endregion

        #region Methods
        public void CheckForAxisLimits()
        {
            AxisLimitsResult = string.Empty;
            isFailure = true;
            if (location is ITxRoboticLocationOperation roboticLocation)
            {
                if(roboticLocation.ParentRoboticOperation.Robot is ITxRobot robot)
                {
                    TxRobotInverseData inverseData = new TxRobotInverseData
                    {
                        Destination = (location as ITxLocatableObject).AbsoluteLocation
                    };
                    if (robot.DoesInverseExist(inverseData))
                    {
                        currentJointValues = robot.GetPoseAtLocation(roboticLocation).JointValues;
                        GetRobotAxisLimits(robot);
                        if (currentJointValues.Count > 0 && currentJointValues.Count == lowerLimits.Count && currentJointValues.Count == upperLimits.Count)
                        {
                            CheckAxisLimits();
                            CheckSingularities(robot);
                            isFailure = false;
                        }
                    }
                    else
                    {
                        AxisLimitsResult = "No inverse!";
                        isFailure = false;
                    }
                }
            }
            if(isFailure && string.IsNullOrEmpty(AxisLimitsResult))
            {
                AxisLimitsResult = "Failure!";
            }
            else if(!isFailure && string.IsNullOrEmpty(AxisLimitsResult))
            {
                AxisLimitsResult = "Ok!";
            }
        }

        private void CheckSingularities(ITxRobot robot)
        {
            CheckA1Singularity(robot);
            CheckA23Singularity(robot);
            CheckA5Singularity();
        }

        private void CheckA1Singularity(ITxRobot irobot)
        {
            TxTransformation currentWorkingFrame = TxApplication.ActiveDocument.WorkingFrame;
            TxRobot robot = irobot as TxRobot;
            if(robot != null)
            {
                if (location is ITxLocatableObject locateableLocation)
                {
                    TxApplication.ActiveDocument.WorkingFrame = robot.AbsoluteLocation;
                    double locationXvalue = locateableLocation.LocationRelativeToWorkingFrame.Translation.X;
                    double locationYvalue = locateableLocation.LocationRelativeToWorkingFrame.Translation.Y;
                    double xyValue = RwMathUtilities.Length(locationXvalue, locationYvalue, 0.0);
                    if (RwMathUtilities.Abs(xyValue) < deltaA1Singularity)
                    {
                        AxisLimitsResult = string.IsNullOrEmpty(AxisLimitsResult)
                            ? $"A1 Singularity: {xyValue:F1}"
                            : string.Join("; ", AxisLimitsResult, $"A1 Singularity: {xyValue:F1}");
                    }
                    TxApplication.ActiveDocument.WorkingFrame = currentWorkingFrame;
                }
            }
        }

        private void CheckA23Singularity(ITxRobot irobot)
        {
            TxRobot robot = irobot as TxRobot;
            if (robot != null)
            {
                double? jointA3HomeValue = null;
                foreach (TxPose pose in robot.PoseList)
                {
                    if (pose.Name.Equals("HOME"))
                    {
                        TxPoseData poseDataHome = pose.PoseData;
                        robot.CurrentPose = poseDataHome;
                        jointA3HomeValue = (double)pose.Device.CurrentPose.JointValues[2];
                        break;
                    }
                }
                if(jointA3HomeValue != null)
                {
                    double a3SingularityValue = RwMathUtilities.Rad2Deg((double)jointA3HomeValue) - 90.0;
                    double a3CurrentValue = RwMathUtilities.Rad2Deg((double)currentJointValues[2]);
                    if (RwMathUtilities.Abs(a3CurrentValue - a3SingularityValue) < deltaA23Singularity)
                    {
                        AxisLimitsResult = string.IsNullOrEmpty(AxisLimitsResult)
                            ? $"A23 Singularity: {a3CurrentValue:F1}"
                            : string.Join("; ", AxisLimitsResult, $"A23 Singularity: {a3CurrentValue:F1}");
                    }
                }
            }
        }

        private void CheckA5Singularity()
        {
            double a5Value = RwMathUtilities.Rad2Deg((double)currentJointValues[4]);
            if (RwMathUtilities.Abs(a5Value) < deltaA5Singularity)
            {
                AxisLimitsResult = string.IsNullOrEmpty(AxisLimitsResult) 
                    ? $"A5 Singularity: {a5Value:F1}" 
                    : string.Join("; ", AxisLimitsResult, $"A5 Singularity: {a5Value:F1}");
            }
        }

        private void CheckAxisLimits()
        {
            for (int i = 0; i < currentJointValues.Count; i++)
            {
                double upperValue = RwMathUtilities.Abs(RwMathUtilities.Rad2Deg((double)upperLimits[i]-(double)currentJointValues[i]));
                double lowerValue = RwMathUtilities.Abs(RwMathUtilities.Rad2Deg((double)lowerLimits[i] - (double)currentJointValues[i]));
                if (upperValue < deltaRotationalAxisLimit)
                {
                    AxisLimitsResult = string.IsNullOrEmpty(AxisLimitsResult) 
                        ? $"A{i}: {upperValue:F1}"
                        : string.Join("; ", AxisLimitsResult, $"A{i}: {upperValue:F1}");
                }
                if (lowerValue < deltaRotationalAxisLimit)
                {
                    AxisLimitsResult = string.IsNullOrEmpty(AxisLimitsResult)
                        ? $"A{i}: {lowerValue:F1}"
                        : string.Join("; ", AxisLimitsResult, $"A{i}: {lowerValue:F1}");
                }
            }
        }

        private void GetRobotAxisLimits(ITxRobot robot)
        {
            lowerLimits = new ArrayList();
            upperLimits = new ArrayList();

            if (robot as TxRobot is ITxDevice robotAsDevice)
            {
                foreach (TxJoint joint in robotAsDevice.DrivingJoints)
                {
                    if(joint.HardLimits is TxJointConstantHardLimits constantLimits)
                    {
                        lowerLimits.Add(constantLimits.LowerLimit);
                        upperLimits.Add(constantLimits.UpperLimit);
                    }
                    else if (joint.HardLimits is TxJointVariableHardLimits variableLimits)
                    {
                        lowerLimits.Add(variableLimits.LimitsData.Limits[0].JointLowerLimitValue);
                        upperLimits.Add(variableLimits.LimitsData.Limits[0].JointUpperLimitValue);
                    }
                    else
                    {
                        lowerLimits.Add(joint.LowerSoftLimit);
                        upperLimits.Add(joint.UpperSoftLimit);
                    }
                }
            }
        }

        public void CheckForCollisions()
        {
            if (JumpRobotToLocation())
            {
                TxCollisionQueryParams collisionParameters = new TxCollisionQueryParams
                {
                    Mode = TxCollisionQueryParams.TxCollisionQueryMode.DefinedPairs
                };
                TxCollisionQueryContext context = TxApplication.ActiveDocument.CollisionRoot.GlobalQueryContext;
                CollisionStatusResult = (TxApplication.ActiveDocument.CollisionRoot.HasCollidingObjects(collisionParameters, context)) ? "nOk" : "Ok";
            }
        }

        private bool JumpRobotToLocation()
        {
            bool isSuccess = false;
            if (location is ITxRoboticLocationOperation roboticLocation)
            {
                TxRobot robot = roboticLocation.ParentRoboticOperation.Robot as TxRobot;
                if (robot != null)
                {
                    TxOlpRobotFollowMode txOlpRobotFollowMode = new TxOlpRobotFollowMode(robot);
                    isSuccess = txOlpRobotFollowMode.JumpRobotToLocation(roboticLocation);
                }
            }
            return isSuccess;
        }
        #endregion
    }
}