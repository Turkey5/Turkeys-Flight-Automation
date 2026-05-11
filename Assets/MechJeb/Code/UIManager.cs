using UnityEngine;
using SpaceWarp2.API.Mods;
using System.Drawing;
using System.Numerics;

namespace MechJeb
{
    public class UIManager
    {
        private bool _windowVisible = true;
        private Rect _windowRect = new Rect(10, 10, 300, 400);
        private UnityEngine.Vector2 _scrollPosition = UnityEngine.Vector2.zero;
        private GUIStyle _windowStyle;
        private GUIStyle _labelStyle;
        private GUIStyle _buttonStyle;

        public UIManager()
        {
            InitializeStyles();
        }
        
        private void InitializeStyles()
        {
            _windowStyle = new GUIStyle(GUI.skin.window);
            _labelStyle = new GUIStyle(GUI.skin.label);
            _buttonStyle = new GUIStyle(GUI.skin.button);
        }

        public void DrawWindow()
        {
            if (!_windowVisible)
                return;
            _windowRect = GUILayout.Window(
                "MechJeb_Main".GetHashCode(),
                _windowRect,
                DrawWindowContents,
                "MechJeb"
            );
        }

        public void DrawWindowContents(int windowID)
        {
            GUILayout.BeginVertical();
            GUILayout.Label("MechJeb - Autopilot", new GUIStyle(GUI.skin.label));
            GUILayout.Label("MechJeb - Autopilot", new GUIStyle(GUI.skin.label));

        }

        private void DrawManeuverPlanning()
        {
        }
        public void Toggle()
        {
            _windowVisible = !_windowVisible;
        }
    }
}