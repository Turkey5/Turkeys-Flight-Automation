using KSP.Sim.impl;
using KSP.Sim;
using UnityEngine;
using SpaceWarp2.API.Mods;
using KSP.Game;

namespace MechJeb
{
    public class OrbitCalulator
    {
        public double CalculateHohmannTransferDeltaV(PatchedConicsOrbit currentOrbit, double targetAltitude)
        {
            if (currentOrbit == null)
                return 0;
            CelestialBodyComponent body = currentOrbit.referenceBody;
            if (body == null)
                return 0;
            double r1 = currentOrbit.semiMajorAxis;
            double r2 = targetAltitude + body.radius;
            double GM = body.Mass * 6.67430e-11; //gravitational parameter
            double v1 = System.Math.Sqrt(GM / r1);
            double vTransfer1 = System.Math.Sqrt(GM * (2 / r1 - 2 / (r1 + r2)));
            double vTransfer2 = System.Math.Sqrt(GM * (2 / r2 - 2 / (r1 + r2)));
            double v2 = System.Math.Sqrt(GM / r2);
            double dv1 = System.Math.Abs(vTransfer1 - v1);
            double dv2 = System.Math.Abs(v2 - vTransfer2);
            return dv1 + dv2;
        }

        public double CalculateHohmannTransferTime(PatchedConicsOrbit currentOrbit, double targetAltitude)
        {
            if (currentOrbit == null)
                return 0;
 
            CelestialBodyComponent body = currentOrbit.referenceBody;
            if (body == null)
                return 0;
 
            double r1 = currentOrbit.semiMajorAxis;
            double r2 = targetAltitude + body.radius;
            double a_transfer = (r1 + r2) / 2;
 
            double GM = body.Mass * 6.67430e-11;
            double transferPeriod = 2 * System.Math.PI * System.Math.Sqrt(a_transfer * a_transfer * a_transfer / GM);
 
            return transferPeriod / 2; // Half the orbital period of transfer ellipse
        }
 
        /// <summary>
        /// Calculate delta-v for circularization burn
        /// </summary>
        public double CalculateCircularizationDeltaV(PatchedConicsOrbit currentOrbit)
        {
            if (currentOrbit == null)
                return 0;
 
            CelestialBodyComponent body = currentOrbit.referenceBody;
            if (body == null)
                return 0;
 
            double r = currentOrbit.Apoapsis + body.radius;
            double GM = body.Mass * 6.67430e-11;
 
            double vCurrent = currentOrbit.GetOrbitalVelocityAtUTZup(GameManager.Instance.Game.UniverseModel.UniverseTime).magnitude;
            double vCircular = System.Math.Sqrt(GM / r);
 
            return System.Math.Abs(vCircular - vCurrent);
        }
 
        /// <summary>
        /// Get orbital velocity at a given altitude
        /// </summary>
        public double GetOrbitalVelocityAtAltitude(CelestialBodyComponent body, double altitude)
        {
            if (body == null)
                return 0;
 
            double r = altitude + body.radius;
            double GM = body.Mass * 6.67430e-11;
 
            return System.Math.Sqrt(GM / r);
        }
 
        /// <summary>
        /// Get escape velocity at a given altitude
        /// </summary>
        public double GetEscapeVelocityAtAltitude(CelestialBodyComponent body, double altitude)
        {
            if (body == null)
                return 0;
 
            double r = altitude + body.radius;
            double GM = body.Mass * 6.67430e-11;
 
            return System.Math.Sqrt(2 * GM / r);
        }
 
        /// <summary>
        /// Convert a delta-v vector to burn directions (prograde, normal, radial)
        /// </summary>
        public Vector3d ConvertDVToBurnVec(PatchedConicsOrbit orbit, Vector3d deltaV, double UT)
        {
            if (orbit == null)
                return Vector3d.zero;
 
            Vector3d burnVec = Vector3d.zero;
            burnVec.x = Vector3d.Dot(deltaV, orbit.RadialPlus(UT));
            burnVec.y = Vector3d.Dot(deltaV, orbit.NormalPlus(UT));
            burnVec.z = Vector3d.Dot(deltaV, -1 * orbit.Retrograde(UT));
            return burnVec;
        }
        public Vector3d ConvertBurnVecToDV(PatchedConicsOrbit orbit, Vector3d burnVec, double UT)
        {
            if (orbit == null)
                return Vector3d.zero;
            return burnVec.x * orbit.RadialPlus(UT) + 
                   burnVec.y * orbit.NormalPlus(UT) - 
                   burnVec.z * orbit.Retrograde(UT);
        }
        public double GetOrbitalPeriod(PatchedConicsOrbit orbit)
        {
            if (orbit == null)
                return 0;
 
            return orbit.period;
        }
        public double GetMeanMotion(PatchedConicsOrbit orbit)
        {
            if (orbit == null)
                return 0;
            return (2 * System.Math.PI) / orbit.period;
        }
    }
}