using KSP.Sim.impl;
using KSP.Sim;
using SpaceWarp2.API.Mods;
using Castle.Components.DictionaryAdapter.Xml;
using Redux.WorldVis;
using UnityEngine.UI.Extensions;
using KSP.Game;
using System.Security.Policy;
using System.Runtime.Serialization;

namespace MechJeb
{
    public class VesselManager
    {
        private VesselComponent _activeVessel;
        public VesselComponent ActiveVessel
        {
            get => _activeVessel;
            private set => _activeVessel = value;
        }
        public OrbiterComponent ActiveOrbiter
        {
            get
            {
                if (ActiveVessel == null)
                    return null;
                return ActiveVessel.Orbiter;
            }
        }

        public PatchedConicsOrbit ActiveOrbit
        {
            get
            {
                var orbiter = ActiveOrbiter;
                if (orbiter == null)
                    return null;
                return orbiter.PatchedConicsOrbit;
            }
        }

        public PatchedConicSolver PatchedConicSolver
        {
            get
            {
                var orbiter = ActiveOrbiter;
                if (orbiter == null)
                    return null;
                return orbiter.PatchedConicSolver;
            }
        }

        public CelestialBodyComponent ReferenceBody
        {
            get
            {
                var orbit = ActiveOrbit;
                if (orbit == null)
                    return null;
                return orbit.referenceBody;
            }
        }

        public bool HasActiveVessel => ActiveVessel != null;

        public bool IsInOrbit
        {
            get
            {
                if (!HasActiveVessel)
                    return false;
                var orbit = ActiveOrbit;
                return orbit != null && orbit.eccentricity < 1.0;
            }
        }

        public double GetAltitude()
        {
            var orbit = ActiveOrbit;
            if (orbit == null)
                return 0;
            var refBody = ReferenceBody;
            if (refBody == null)
                return 0;
            var position = orbit.GetRelativePositionAtUT(GameManager.Instance.Game.UniverseModel.UniverseTime);
            var distance = position.magnitude;
            return distance - refBody.radius;
        }

        public double GetOrbitalVelocity()
        {
            var orbit = ActiveOrbit;
            if (orbit == null)
                return 0;
            double UT = GameManager.Instance.Game.UniverseModel.UniverseTime;
            var velocity = orbit.GetOrbitalVelocityAtUTZup(UT);
            return velocity.magnitude;
        }

        public double GetTimeToPeriapsis()
        {
            var orbit = ActiveOrbit;
            if (orbit == null)
                return 0;
            double UT = GameManager.Instance.Game.UniverseModel.UniverseTime;
            double timeToEvent = orbit.GetUTforTrueAnomaly(0, UT);

            return timeToEvent - UT;
        }

        public double GetTimeToApoapsis()
        {
            var orbit = ActiveOrbit;
            if (orbit == null)
                return 0;
            double UT = GameManager.Instance.Game.UniverseModel.UniverseTime;
            double trueAnomalyAtApoapsis = System.Math.PI;
            double timeToEvent = orbit.GetUTforTrueAnomaly(trueAnomalyAtApoapsis, UT);

            return timeToEvent - UT;
        }

        public void LogVesselInfo()
        {
            if (!HasActiveVessel)
            {
                //put log stuff here eventually
            }
        }
    }
}