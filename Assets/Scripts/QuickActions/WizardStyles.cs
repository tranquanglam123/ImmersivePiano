using UnityEditor;
using UnityEngine;

namespace ImmerisvePiano.Interaction.Editor
{
    internal class WizardStyles
    {
        internal readonly GUIContent ErrorIcon =
            EditorGUIUtility.IconContent("console.erroricon.sml");

        internal readonly GUIContent InfoIcon =
            EditorGUIUtility.IconContent("console.infoicon.sml");

        internal readonly GUIContent WarningIcon =
            EditorGUIUtility.IconContent("console.warnicon.sml");

        internal readonly GUIStyle ListLabel = new GUIStyle("TV Selection")
        {
            border = new RectOffset(0, 0, 0, 0),
            padding = new RectOffset(5, 5, 5, 3),
            margin = new RectOffset(4, 4, 4, 5)
        };

        internal readonly GUIStyle FixIcon = new GUIStyle(GUIStyle.none)
        {
            margin = new RectOffset(0, 2, 1, 0),
            stretchWidth = false,
            stretchHeight = false,
            fixedWidth = 24f,
            fixedHeight = 24f,
            alignment = TextAnchor.MiddleRight,
        };

        internal readonly GUIStyle FixButton = new GUIStyle(EditorStyles.miniButton)
        {
            margin = new RectOffset(0, 0, 2, 2),
            stretchWidth = false,
            fixedWidth = 48f,
        };

        internal readonly GUIStyle MessageButton = new GUIStyle(GUI.skin.button)
        {
            stretchWidth = true,
            stretchHeight = true,
            fixedWidth = 82f,
        };

        internal readonly GUIStyle FixAllButton = new GUIStyle(EditorStyles.miniButton)
        {
            margin = new RectOffset(0, 6, 4, 2),
            stretchWidth = false,
            fixedWidth = 64f,
        };

        internal readonly GUIStyle ResetButton = new GUIStyle(EditorStyles.miniButton)
        {
            margin = new RectOffset(0, 0, 2, 2),
            stretchWidth = false,
            fixedWidth = 64f,
        };

        internal readonly GUIStyle ResetAllButton = new GUIStyle(EditorStyles.miniButton)
        {
            margin = new RectOffset(0, 6, 4, 2),
            stretchWidth = false,
            fixedWidth = 82f,
        };

        internal readonly GUIStyle Foldout = new GUIStyle(EditorStyles.foldoutHeader)
        {
            margin = new RectOffset(0, 0, 0, 0),
            padding = new RectOffset(16, 5, 5, 5),
            fixedHeight = 26.0f
        };

        internal readonly GUIStyle FoldoutHorizontal = new GUIStyle(EditorStyles.label)
        {
            fixedHeight = 26.0f
        };

        internal readonly GUIStyle List = new GUIStyle(EditorStyles.helpBox)
        {
            margin = new RectOffset(3, 3, 3, 3),
            padding = new RectOffset(3, 3, 3, 3)
        };

        internal readonly GUIStyle ButtonArea = new GUIStyle(GUIStyle.none)
        {
            fixedHeight = 32f,
        };

        internal readonly GUIStyle TargetLabel = new GUIStyle(GUIStyle.none)
        {
            margin = new RectOffset(3, 3, 3, 3),
        };

        internal readonly GUIStyle WizardField = new GUIStyle(EditorStyles.label)
        {
        };

        internal readonly GUIStyle WizardFieldTooltip = new GUIStyle(EditorStyles.label)
        {
            richText = true,
            wordWrap = true,
            fontSize = (int)(GUI.skin.label.fontSize * 0.9f),
            padding = new RectOffset(3, 3, 3, 5),
        };
    }
}
