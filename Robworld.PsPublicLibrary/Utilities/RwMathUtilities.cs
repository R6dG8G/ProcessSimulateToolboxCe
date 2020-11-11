using System;
using Tecnomatix.Engineering;

namespace Robworld.PsPublicLibrary.Utilities
{
    /// <summary>
    /// Provides static mathematical methods 
    /// </summary>
    public static class RwMathUtilities
    {
        /// <summary>
        /// Returns the absolute value of a double
        /// </summary>
        /// <param name="value">The input value</param>
        /// <returns>The absolute value of the input parameter</returns>
        public static double Abs(double value)
        {
            return Math.Abs(value);
        }

        /// <summary>
        /// Calculate the length of a 3D vector
        /// </summary>
        /// <param name="x">The x value</param>
        /// <param name="y">The y value</param>
        /// <param name="z">The z value</param>
        /// <returns>The length of the vector</returns>
        public static double Length(double x, double y, double z)
        {
            double xp = Math.Pow(x, 2.0);
            double yp = Math.Pow(y, 2.0);
            double zp = Math.Pow(z, 2.0);
            return Math.Sqrt(xp + yp + zp);
        }

        /// <summary>
        /// Converts an angle from radians to degrees
        /// </summary>
        /// <param name="radians">Value in radians</param>
        /// <returns>The value in degrees</returns>
        public static double Rad2Deg(double radians)
        {
            return (radians / Math.PI) * 180.0;
        }

        /// <summary>
        /// Converts an angle from degrees to radians
        /// </summary>
        /// <param name="degrees">Value in degrees</param>
        /// <returns>The value in radians</returns>
        public static double Deg2Rad(double degrees)
        {
            return (degrees / 180.0) * Math.PI;
        }

        /// <summary>
        /// Rounds a double value to the nearest integral
        /// </summary>
        /// <param name="value">The value that has to be rounded</param>
        /// <returns></returns>
        public static double Round(double value)
        {
            return Math.Round(value, 3);
        }

        /// <summary>
        /// Create a TxTransformation object in RPY representation
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="z">The z coordinate</param>
        /// <param name="rx">The rx rotation</param>
        /// <param name="ry">The ry rotation</param>
        /// <param name="rz">The rz rotation</param>
        /// <param name="convertToRadians">Convert the rotation values to radians</param>
        /// <returns>The TxTransformation object</returns>
        public static TxTransformation CreateRpyTransformation(double x, double y, double z, double rx, double ry, double rz, bool convertToRadians)
        {
            TxVector translation = CreateVector(x, y, z, false);
            TxVector rotation = CreateVector(rx, ry, rz, true);
            return new TxTransformation(translation, rotation, TxTransformation.TxRotationType.RPY_XYZ);
        }

        /// <summary>
        /// Create a TxVector object
        /// </summary>
        /// <param name="v1">The first value</param>
        /// <param name="v2">The second value</param>
        /// <param name="v3">The third value</param>
        /// <param name="convertToRadians">Convert the values to radians</param>
        /// <returns></returns>
        public static TxVector CreateVector(double v1, double v2, double v3, bool convertToRadians)
        {
            TxVector vector;
            if(convertToRadians)
            {
                vector = new TxVector(Deg2Rad(v1), Deg2Rad(v2), Deg2Rad(v3));
            }
            else
            {
                vector = new TxVector(v1, v2, v3);
            }
            return vector;
        }
    }
}
