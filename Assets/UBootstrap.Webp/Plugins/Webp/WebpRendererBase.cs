using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UBootstrap.Webp
{
    [ExecuteInEditMode]
    public abstract class WebpRendererBase : MonoBehaviour
    {
        public abstract Texture2D BaseTexture { get; }

        [SerializeField]
        private TextAsset webpAsset;

        public virtual TextAsset WebpAsset {
            get {
                return webpAsset;
            }
            set {
                webpAsset = value;
                UpdateWebpAsset (false);
            }
        }

        private int lastWebpAssetInstanceID = 0;

        private int WebpAssetInstanceID {
            get {
                if (this.webpAsset == null) {
                    return 0;
                }
                return this.webpAsset.GetInstanceID ();
            } 
        }

        private int mainTexPropertyNameId = 0;

        protected int MainTexPropertyNameId {
            get {
                if (this.mainTexPropertyNameId == 0) {
                    this.mainTexPropertyNameId = Shader.PropertyToID ("_MainTex");
                }
                return this.mainTexPropertyNameId;
            }
        }

        private MaterialPropertyBlock mpb;

        protected MaterialPropertyBlock MaterialPropertyBlock {
            get {
                if (this.mpb == null) {
                    this.mpb = new MaterialPropertyBlock ();
                }
                return this.mpb;
            }
        }

        private List<Texture2D> refSharedWebpTextures = new List<Texture2D> ();

        private bool IsWebpAssetChanged (bool isForceUpdate)
        {
            if (isForceUpdate) {
                this.lastWebpAssetInstanceID = this.WebpAssetInstanceID;
                return true;
            }

            if (this.lastWebpAssetInstanceID != this.WebpAssetInstanceID) {
                this.lastWebpAssetInstanceID = this.WebpAssetInstanceID;
                return true;
            }

            return false;
        }

        protected void UpdateWebpAsset (bool isForceUpdate)
        {
            if (!IsWebpAssetChanged (isForceUpdate)) {
                return;
            }

            Texture2D sharedWebpTexture = SharedWebpTexture.RefSharedWebpTexture (webpAsset);
            UnrefCurrentWebpTextureOnChange ();

            this.SetWebpTextureFromShared (sharedWebpTexture);
            if (sharedWebpTexture != null) {
                this.refSharedWebpTextures.Add (sharedWebpTexture);
            }
        }

        abstract protected void SetWebpTextureFromShared (Texture2D sharedWebpTexture);

        public static bool IsWebpTexture (Texture2D texture)
        {
            return SharedWebpTexture.ContainSharedTexture (texture);
        }

        protected void UnrefCurrentWebpTextureOnChange ()
        {
            if (Application.isPlaying) {
                for (int i = 0; i < refSharedWebpTextures.Count; i++) {
                    SharedWebpTexture.UnrefSharedWebpTexture (refSharedWebpTextures [i]);
                }

                refSharedWebpTextures.Clear ();
            }
        }

        protected virtual void OnValidate ()
        {
            UpdateWebpAsset (false);
        }

        protected virtual void Awake ()
        {
        }

        protected virtual void Start ()
        {
            if (Application.isPlaying) {
                this.UpdateWebpAsset (true);
            }
        }

        protected virtual void OnDestroy ()
        {
            for (int i = 0; i < refSharedWebpTextures.Count; i++) {
                SharedWebpTexture.UnrefSharedWebpTexture (refSharedWebpTextures [i]);
            }

            refSharedWebpTextures.Clear ();
        }
    }
}
