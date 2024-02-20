namespace CT6RIGPR
{
    using UnityEditor;
    using UnityEngine;

#if UNITY_EDITOR
    /// <summary>
    /// A GUI for the editor representing the jump pad direction.
    /// </summary>
    [CustomEditor(typeof(JumpPad))]
    public class JumpPadEditor : Editor
    {
        void OnSceneGUI()
        {
            JumpPad jumpPad = (JumpPad)target;

            Handles.color = Color.green;
            Vector3 direction = jumpPad.transform.position + jumpPad.GetLaunchDirection() * 2; // Adjust the multiplier for length
            Handles.DrawLine(jumpPad.transform.position, direction);

            Handles.ConeHandleCap(0, direction, Quaternion.LookRotation(jumpPad.GetLaunchDirection()), 0.5f, EventType.Repaint);
        }
    }
#endif
}