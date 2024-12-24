using UnityEngine;
using UnityEditor;

namespace Puppeteer.Editor
{
    [CustomPropertyDrawer(typeof(InputState))]
    public class InputStatePropertyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return EditorGUIUtility.singleLineHeight * 4.5f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            // Begin change check
            EditorGUI.BeginChangeCheck();

            // Begin property
            EditorGUI.BeginProperty(position, label, property);

            // Get the properties
            var moveDirection = property.FindPropertyRelative("moveDirection");
            var crouching = property.FindPropertyRelative("crouching");
            var sprinting = property.FindPropertyRelative("sprinting");
            var canJump = property.FindPropertyRelative("canJump");

            // Set the indent level
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Get the rects for the properties
            var labelRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
            var moveDirectionRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight, position.width, EditorGUIUtility.singleLineHeight);
            var crouchingRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 2, position.width, EditorGUIUtility.singleLineHeight);
            var sprintingRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 3, position.width, EditorGUIUtility.singleLineHeight);
            var canJumpRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight * 4, position.width, EditorGUIUtility.singleLineHeight);

            // Add bold label style
            GUIStyle boldLabel = new GUIStyle(GUI.skin.label);
            boldLabel.fontStyle = FontStyle.Bold;

            // Draw the properties
            EditorGUI.LabelField(labelRect, label, boldLabel);
            EditorGUI.PropertyField(moveDirectionRect, moveDirection);
            EditorGUI.PropertyField(crouchingRect, crouching);
            EditorGUI.PropertyField(sprintingRect, sprinting);
            EditorGUI.PropertyField(canJumpRect, canJump);

            // Reset indent level
            EditorGUI.indentLevel = indent;

            // End property
            EditorGUI.EndProperty();
        }
    }
}
