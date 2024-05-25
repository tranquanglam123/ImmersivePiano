using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using Component = UnityEngine.Component;
namespace ImmersivePiano.QuickActions
{
    public class PokeQuickActions : EditorWindow
    {
        [MenuItem("GameObject/ Poke Quick Actions")]
        public static void ShowWindow()
        {
            PokeQuickActions PQA = GetWindow<PokeQuickActions>();
            PQA.titleContent = new GUIContent("Poke Quick Actions Wizard");
        }

        public void CreateGUI()
        {
            //Each editor window contains a root VisualElement
            VisualElement root = rootVisualElement;

            //VisualElement objects can contain other VisualElements
            Label label = new Label("Hello World");
            root.Add(label);

            //Create Button
            Button button = new Button();
            button.name = "button";
            button.text = "First Button";
            root.Add(button);

            //Create toggle
            Toggle toggle = new Toggle();
            toggle.name = "toggle";
            toggle.label = "Toggle";
            root.Add(toggle);
        }
    }
}
