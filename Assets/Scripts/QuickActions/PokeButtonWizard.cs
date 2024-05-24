using Oculus.Interaction;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ImmersivePiano.Interaction.Editor.QuickActions
{
    internal class PokeButtonWizard : QuickActionsWizard
    {
        private const string MENU_NAME = MENU_FOLDER +
            "Add Poke Interaction to White Button";

        [MenuItem(MENU_NAME, priority = 201)]
        private static void OpenWizard()
        {
            ShowWindow<PokeButtonWizard>(Selection.gameObjects[0]);
        }

        [MenuItem(MENU_NAME, true)]
        static bool Validate()
        {
            return Selection.gameObjects.Length == 1;
        }

        #region Fields

        [SerializeField]
        [DeviceType, WizardSetting]
        [InspectorName("Add Required Interactor(s)")]
        [Tooltip("The interactors required for the new interactable will be " +
            "added for the device types selected here, if not already present.")]
        private DeviceTypes _deviceTypes = DeviceTypes.All;

        [SerializeField]
        [Tooltip("The mesh and material to make button Poke interactable.")]
        [WizardDependency(ReadOnly = true,
            FindMethod = nameof(FindMeshNMat),
            FixMethod = nameof(FixMeshNMat))]
        private MeshFilter _meshFilter;
        private Mesh _mesh;
        private MeshRenderer _meshRenderer;
        private Material _material;


        #endregion Fields

        private void FindMeshNMat()
        {
            _meshFilter = Target.GetComponent<MeshFilter>();
            _meshRenderer = Target.GetComponent<MeshRenderer>();
        }

        private void FixMeshNMat()
        {
            _meshFilter = AddComponent<MeshFilter>(Target);
            _meshRenderer = AddComponent<MeshRenderer>(Target);
        }

        protected override void Create()
        {
            GameObject obj = Templates.CreateFromTemplate(
                Target.transform, Templates.PokeKeyNoteW);
            if (obj.transform.parent.gameObject.GetComponent<Key>() == null)
            {
                obj.transform.parent.gameObject.AddComponent<Key>();
            }


            InteractorUtils.AddInteractorsToRig(
                InteractorTypes.Poke, _deviceTypes);
        }

        protected override IEnumerable<MessageData> GetMessages()
        {
            var result = Enumerable.Empty<MessageData>();

            //result = result.Concat(Messages
            //    .MissingPointableCanvasModule<PokeInteractor>());

            //if (Target.GetComponent<MeshFilter>() == null || Target.GetComponent<MeshRenderer>() == null)
            //{
            //    result = result.Append(new MessageData(MessageType.Error,
            //        "The target object must have MeshFilter and MeshRenderer attached."));
            //}
            //else if (_meshFilter != null && _meshRenderer != null && _meshFilter.mesh == null || _meshRenderer.material == null)
            //{
            //    result = result.Append(new MessageData(MessageType.Error,
            //        "The object should have a mesh and a material",
            //        new ButtonData("Fix", () => _meshFilter.mesh = GetCorrectMesh(GetAllMeshes(Target), Target))));
            //}
            return result;
        }
        private Mesh GetCorrectMesh(Mesh[] All, GameObject target)
        {
            var result = new Mesh();
            foreach (var mesh in All)
            {
                if (mesh.name == target.name)
                {
                    result = mesh;
                    break;
                }
            }
            return result;
        }
    }
}
