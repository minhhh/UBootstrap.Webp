using UnityEditor;
using UnityEngine;
using System.Collections;

namespace UBootstrap.Webp
{
    public static class WebpRendererBaseEditor
    {
        public static void OnInspectorGUI (Editor editor, WebpRendererBase webpRendererBase)
        {
            if (webpRendererBase == null) {
                Debug.LogError ("webpRendererBase is null");
                return;
            }

            editor.DrawDefaultInspector ();
        }
    }

    [CustomEditor (typeof(MeshRendererWebp))]
    public class MeshRendererWebpEditor : Editor
    {
        public override void OnInspectorGUI ()
        {
            WebpRendererBaseEditor.OnInspectorGUI (this, target as WebpRendererBase);
        }
    }

    [CustomEditor (typeof(SpriteRendererWebp))]
    public class SpriteRendererWebpEditor : Editor
    {
        public override void OnInspectorGUI ()
        {
            WebpRendererBaseEditor.OnInspectorGUI (this, target as WebpRendererBase);
        }
    }

    [CustomEditor (typeof(UIImageWebp))]
    public class UIImageWebpEditor : Editor
    {
        public override void OnInspectorGUI ()
        {
            WebpRendererBaseEditor.OnInspectorGUI (this, target as WebpRendererBase);
        }
    }

    [CustomEditor (typeof(UIRawImageWebp))]
    public class UIRawImageWebpEditor : Editor
    {
        public override void OnInspectorGUI ()
        {
            WebpRendererBaseEditor.OnInspectorGUI (this, target as WebpRendererBase);
        }
    }
}