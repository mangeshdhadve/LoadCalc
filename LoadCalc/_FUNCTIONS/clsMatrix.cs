using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.AutoCAD.Geometry;
using static System.Math;

namespace LoadCalc._FUNCTIONS
{
    /// <summary>
    /// Describes a 3d rotation with Yaw Pitch and Roll
    /// (i.e. a specific Euler angles called Tait-Ryan angles with z-y'-x" convention).
    /// </summary>
    public class clsMatrix
    {
        /// <summary>
        /// Get the transformation matrix.
        /// </summary>
        public Matrix3d Transform { get; private set; }

        /// <summary>
        /// Gets the rotation matrix about Z axis.
        /// </summary>
        public Matrix3d Yaw { get; private set; }

        /// <summary>
        /// Gets the rotation matrix about Y axis.
        /// </summary>
        public Matrix3d Pitch { get; private set; }

        /// <summary>
        /// Gets the rotation matrix about X axis.
        /// </summary>
        public Matrix3d Roll { get; private set; }

        /// <summary>
        /// Gets the rotation angle about Z axis.
        /// </summary>
        public double Alpha { get; private set; }

        /// <summary>
        /// Gets the rotation angle about Y axis.
        /// </summary>
        public double Beta { get; private set; }

        /// <summary>
        /// Gets the rotation angle about X axis.
        /// </summary>
        public double Gamma { get; private set; }

        /// <summary>
        /// Create a new intance of TaitBryan.
        /// </summary>
        /// <param name="xform">Transformation matrix.</param>
        public clsMatrix(Matrix3d xform)
        {
            try
            {

                if (!xform.IsUniscaledOrtho())
                    throw new ArgumentException("Non uniscaled ortho matrix.");

                Transform = xform;
                Beta = -Math.Asin(xform[2, 0] / xform.GetScale()) * -1;
                if (Abs(Beta - PI * 0.5) < 1e-7)
                {
                    Beta = PI * 0.5;
                    Alpha = Atan2(xform[1, 2], xform[1, 1]);
                    Gamma = 0.0;
                }
                else if (Abs(Beta + PI * 0.5) < 1e-7)
                {
                    Beta = -PI * 0.5;
                    Alpha = Atan2(-xform[1, 2], xform[1, 1]);
                    Gamma = 0.0;
                }
                else
                {
                    Alpha = Atan2(xform[1, 0], xform[0, 0]);
                    Gamma = Atan2(xform[2, 1], xform[2, 2]);
                }

                Yaw = Matrix3d.Rotation(Alpha, Vector3d.ZAxis, Point3d.Origin);
                Pitch = Matrix3d.Rotation(Beta, Vector3d.YAxis, Point3d.Origin);
                Roll = Matrix3d.Rotation(Gamma, Vector3d.XAxis, Point3d.Origin);

                Alpha = Math.Round(Alpha * 180.0 / Math.PI, 8);
                Beta = Math.Round(Beta * 180.0 / Math.PI, 8);
                Gamma = Math.Round(Gamma * 180.0 / Math.PI, 8);
            }
            catch (Exception)
            {
                Alpha = 999;
                Beta = 999;
                Gamma = 999;
            }
        }
    }
}
