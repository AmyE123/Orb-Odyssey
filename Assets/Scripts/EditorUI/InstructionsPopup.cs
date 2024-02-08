namespace CT6RIGPR
{
    using UnityEditor;
    using UnityEngine;

    /// <summary>
    /// An instructions popup for any tools we may have.
    /// </summary>
    public class InstructionsPopup : EditorWindow
    {
        private string _instructions;
        private string _url;

        /// <summary>
        /// Shows the instructions window
        /// </summary>
        /// <param name="instructions">What instructions you want in the window</param>
        public static void ShowWindow(string instructions)
        {
            InstructionsPopup window = GetWindow<InstructionsPopup>("Instructions", true);
            window._instructions = instructions;
            window.ParseURL();
            window.Show();
        }

        private void OnGUI()
        {
            SetHelpBoxInfo();
        }

        private void SetHelpBoxInfo()
        {
            GUILayout.Label("How to Use", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox(_instructions, MessageType.Info);

            if (!string.IsNullOrEmpty(_url))
            {
                if (GUILayout.Button(_url, EditorStyles.linkLabel))
                {
                    Application.OpenURL(_url);
                }
            }
        }

        private void ParseURL()
        {
            int startIndex = _instructions.IndexOf("[");
            int endIndex = _instructions.IndexOf("]", startIndex);
            if (startIndex != -1 && endIndex != -1)
            {
                _url = _instructions.Substring(startIndex + 1, endIndex - startIndex - 1);
                _instructions = _instructions.Remove(startIndex, endIndex - startIndex + 1);
            }
        }
    }
}