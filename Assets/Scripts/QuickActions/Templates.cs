using System;
using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.HandGrab;
using UnityEditor;
using UnityEngine;

using Object = UnityEngine.Object;

namespace ImmersivePiano.Interaction.Editor.QuickActions
{
    internal class Template
    {
        /// <summary>
        /// The instantiated prefab will be given this name
        /// </summary>
        public readonly string DisplayName;

        /// <summary>
        /// The GUID of the prefab asset
        /// </summary>
        public readonly string AssetGUID;

        /// <param name="displayName">The instantiated GameObject will be given this name.
        /// Does not need to correspond to the prefab asset name.</param>
        /// <param name="assetGUID">The GUID of the prefab asset.</param>
        public Template(string displayName, string assetGUID)
        {
            DisplayName = displayName;
            AssetGUID = assetGUID;
        }
    }

    internal static class Templates
    {
        public static event Action<Template, GameObject> WhenObjectCreated = delegate { };

        #region Interactables

       
        public static readonly Template PokeKeyNoteW =
            new Template(
                "KeyNoteW",
                "68406b978d3381b45b350a0d0c5e83f2");

        public static readonly Template PokeKeyNoteB =
            new Template(
                "KeyNoteB",
                "60dd2595c12496745806f86c7849bc63");

        #endregion Interactables

        #region Interactors

        public static readonly Template HandGrabInteractor =
            new Template(
                "HandGrabInteractor",
                "f0a90b2d303e7744fa8c9d3c6e2418a4");

        public static readonly Template HandPokeInteractor =
            new Template(
                "PokeInteractor",
                "abe5a2b766edc96438786a6785a2f74b");

        public static readonly Template HandRayInteractor =
            new Template(
                "RayInteractor",
                "a6df867c95b07224498cb3ea2d410ce5");

        public static readonly Template DistanceHandGrabInteractor =
            new Template(
                "DistanceHandGrabInteractor",
                "7ea5ce61c81c5ba40a697e2642e80c83");

        public static readonly Template ControllerPokeInteractor =
            new Template(
                "PokeInteractor",
                "ef9bd966f1a997b4cb9eef15b0620b24");

        public static readonly Template ControllerRayInteractor =
            new Template(
                "RayInteractor",
                "074f70ff54d0c6d489aaeba17f4bc66d");

        public static readonly Template ControllerGrabInteractor =
            new Template(
                "GrabInteractor",
                "069b845e75891f04bb2e512a8ebf3b78");

        public static readonly Template ControllerDistanceGrabInteractor =
            new Template(
                "DistanceGrabInteractor",
                "d9ef0d4c78b4bfd409cb884dfe1524d6");

        private static Dictionary<Type, Template> _handInteractorTemplates = new()
        {
            [typeof(HandGrabInteractor)] = HandGrabInteractor,
            [typeof(PokeInteractor)] = HandPokeInteractor,
            [typeof(RayInteractor)] = HandRayInteractor,
            [typeof(DistanceHandGrabInteractor)] = DistanceHandGrabInteractor,

        };

        private static Dictionary<Type, Template> _controllerInteractorTemplates = new()
        {
            [typeof(GrabInteractor)] = ControllerGrabInteractor,
            [typeof(PokeInteractor)] = ControllerPokeInteractor,
            [typeof(RayInteractor)] = ControllerRayInteractor,
            [typeof(DistanceGrabInteractor)] = ControllerDistanceGrabInteractor,
        };

        /// <summary>
        /// Gets the <see cref="Template"/> for a Hand interactor type
        /// </summary>
        public static bool TryGetHandInteractorTemplate(Type type, out Template template)
        {
            return _handInteractorTemplates.TryGetValue(type, out template);
        }

        /// <summary>
        /// Gets the <see cref="Template"/> for a Controller interactor type
        /// </summary>
        public static bool TryGetControllerInteractorTemplate(Type type, out Template template)
        {
            return _controllerInteractorTemplates.TryGetValue(type, out template);
        }

        #endregion Interactors

        /// <summary>
        /// Add an interactable prefab to a GameObject and register it in the Undo stack.
        /// Also registers with the cleanup list, to be optionally removed
        /// when the user cancels out of the wizard.
        /// </summary>
        /// <param name="parent">The Transform the prefab will be instantiated under</param>
        /// <param name="template">The <see cref="Template"/>to be instantiated</param>
        /// <returns>The GameObject at the root of the prefab.</returns>
        public static GameObject CreateFromTemplate(Transform parent, Template template)
        {
            GameObject result = Object.Instantiate(AssetDatabase.LoadMainAssetAtPath(
                AssetDatabase.GUIDToAssetPath(template.AssetGUID))) as GameObject;
            result.name = template.DisplayName;
            result.transform.SetParent(parent?.transform, false);
            Undo.RegisterCreatedObjectUndo(result, "Add " + template.DisplayName);
            WhenObjectCreated.Invoke(template, result);
            ChangeMeshInInstance(parent.gameObject, result);
            return result;
        }

        public static GameObject ChangeMeshInInstance(GameObject orin, GameObject res)
        {
            MeshFilter meshfilter = orin.GetComponent<MeshFilter>();
            res.GetComponentInChildren<MeshFilter>().mesh = meshfilter.sharedMesh;
            MeshFilter.DestroyImmediate(meshfilter);
            MeshRenderer.DestroyImmediate(orin.GetComponent<MeshRenderer>());
            //orin.GetComponent<MeshRenderer>().enabled = false;
            CenterSurface(res);
            return res;
        }
        public static void CenterSurface(GameObject target)
        {
            Transform button = target.transform.Find("Button");
            Transform surface = button.Find("Surface");
            Bounds bounds = target.GetComponentInChildren<MeshFilter>().sharedMesh.bounds;
            surface.localPosition = bounds.center;
        }
    }
}
