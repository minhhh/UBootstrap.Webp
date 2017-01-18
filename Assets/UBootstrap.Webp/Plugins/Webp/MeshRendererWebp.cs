using UnityEngine;
using System.Collections;

using WebP;

namespace UBootstrap.Webp
{
    [RequireComponent (typeof(MeshRenderer))]
    public class MeshRendererWebp : WebpRendererBase
    {
        private MeshRenderer _meshRenderer = null;

        protected MeshRenderer Renderer {
            get {
                if (this._meshRenderer == null) {
                    this._meshRenderer = this.GetComponent<MeshRenderer> ();
                }
                return this._meshRenderer;
            }
        }

        public override Texture2D BaseTexture {
            get {
                if (Renderer.sharedMaterial != null && Renderer.sharedMaterial.mainTexture != null) {
                    if (!(Renderer.sharedMaterial.mainTexture is Texture2D)) {
                        Debug.LogError (string.Format ("MeshRenderer.sharedMaterial.mainTexture is not Texture2D at [{0}]", this.gameObject.name));
                    }

                    return Renderer.sharedMaterial.mainTexture as Texture2D;
                }

                return null;
            }
        }

        protected override void SetWebpTextureFromShared (Texture2D sharedWebpTexture)
        {
            if (sharedWebpTexture != null) {
                MaterialPropertyBlock.Clear ();
                this.Renderer.GetPropertyBlock (MaterialPropertyBlock);
                MaterialPropertyBlock.SetTexture (MainTexPropertyNameId, sharedWebpTexture);
                this.Renderer.SetPropertyBlock (MaterialPropertyBlock);
            } else {
                MaterialPropertyBlock.Clear ();
                this.Renderer.SetPropertyBlock (MaterialPropertyBlock);
            }
        }
    }
}