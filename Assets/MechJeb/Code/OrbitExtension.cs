using KSP.Sim.impl;
 
namespace MechJeb
{
    public static class OrbitExtensionsHelper
    {
        public static Vector3d GetPrograde(PatchedConicsOrbit orbit, double UT)
        {
            if (orbit == null)
                return Vector3d.zero;
            var velocity = orbit.GetOrbitalVelocityAtUTZup(UT);
            if (velocity.magnitude < 0.0001)
                return Vector3d.zero;
            return velocity.normalized;
        }
        public static Vector3d GetRadialPlus(PatchedConicsOrbit orbit, double UT)
        {
            if (orbit == null)
                return Vector3d.zero;
            var position = orbit.GetRelativePositionAtUT(UT);
            if (position.magnitude < 0.0001)
                return Vector3d.zero;
            return position.normalized;
        }
        public static Vector3d GetNormalPlus(PatchedConicsOrbit orbit, double UT)
        {
            if (orbit == null)
                return Vector3d.zero;
            var position = orbit.GetRelativePositionAtUT(UT);
            var velocity = orbit.GetOrbitalVelocityAtUTZup(UT);
            Vector3d normal = Vector3d.Cross(velocity, position);
            if (normal.magnitude < 0.0001)
                return Vector3d.zero;
            return normal.normalized;
        }
        public static Vector3d Prograde(this PatchedConicsOrbit orbit, double UT)
        {
            return GetPrograde(orbit, UT);
        }
        public static Vector3d RadialPlus(this PatchedConicsOrbit orbit, double UT)
        {
            return GetRadialPlus(orbit, UT);
        }
        public static Vector3d NormalPlus(this PatchedConicsOrbit orbit, double UT)
        {
            return GetNormalPlus(orbit, UT);
        }
        public static Vector3d Retrograde(this PatchedConicsOrbit orbit, double UT)
        {
            return -GetPrograde(orbit, UT);
        }
        public static Vector3d RadialMinus(this PatchedConicsOrbit orbit, double UT)
        {
            return -GetRadialPlus(orbit, UT);
        }
        public static Vector3d NormalMinus(this PatchedConicsOrbit orbit, double UT)
        {
            return -GetNormalPlus(orbit, UT);
        }
        public static PatchedConicsOrbit UpdateFromStateVectors(
            this PatchedConicsOrbit orbit,
            Vector3d position,
            Vector3d velocity,
            CelestialBodyComponent referenceBody,
            double UT)
        {
            if (orbit == null || referenceBody == null)
                return orbit;
            KSP.Sim.Position kspPosition = new KSP.Sim.Position(
                referenceBody.coordinateSystem,
                position - referenceBody.Position.localPosition
            );
            KSP.Sim.Velocity kspVelocity = new KSP.Sim.Velocity(
                referenceBody.relativeToMotion,
                velocity
            );
            orbit.UpdateFromStateVectors(kspPosition, kspVelocity, referenceBody, UT);
 
            return orbit;
        }
        // e = v^2/2 - GM/r
        public static double GetOrbitalEnergy(PatchedConicsOrbit orbit, double UT)
        {
            if (orbit == null)
                return 0;
            var position = orbit.GetRelativePositionAtUT(UT);
            var velocity = orbit.GetOrbitalVelocityAtUTZup(UT);
            double r = position.magnitude;
            double v = velocity.magnitude;
            double GM = orbit.referenceBody.Mass * 6.67430e-11; // gravitational parameter
            return (v * v / 2.0) - (GM / r);
        }
        // L = r × v
        public static Vector3d GetOrbitalAngularMomentum(PatchedConicsOrbit orbit, double UT)
        {
            if (orbit == null)
                return Vector3d.zero;
            var position = orbit.GetRelativePositionAtUT(UT);
            var velocity = orbit.GetOrbitalVelocityAtUTZup(UT);
            return Vector3d.Cross(position, velocity);
        }
    }
}
 