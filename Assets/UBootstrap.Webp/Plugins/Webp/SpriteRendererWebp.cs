using UnityEngine;
using System.Collections;
using System;

namespace UBootstrap.Webp
{
    [RequireComponent (typeof(SpriteRenderer))]
    public class SpriteRendererWebp : WebpRendererBase
    {
        public Rect uvRect = new Rect (0, 0, 1, 1);
        private Rect oldUVRect = new Rect (0, 0, 1, 1);
        public Vector2 pivot = new Vector2 (0.5f, 0.5f);
        private Vector2 oldPivot = new Vector2 (0.5f, 0.5f);

        private SpriteRenderer spriteRenderer = null;

        protected SpriteRenderer SpriteRenderer {
            get {
                if (this.spriteRenderer == null) {
                    this.spriteRenderer = this.GetComponent<SpriteRenderer> ();
                }
                return this.spriteRenderer;
            }
        }

        public override Texture2D BaseTexture {
            get {
                if (SpriteRenderer.sprite != null) {
                    return SpriteRenderer.sprite.texture;
                }

                return null;
            }
        }

        protected override void SetWebpTextureFromShared (Texture2D sharedWebpTexture)
        {
            if (sharedWebpTexture != null) {
                try {
                    var sprite = Sprite.Create (sharedWebpTexture, new Rect (uvRect.x * sharedWebpTexture.width, uvRect.y * sharedWebpTexture.height, uvRect.width * sharedWebpTexture.width, uvRect.height * sharedWebpTexture.height), pivot);
                    SpriteRenderer.sprite = sprite;
                    oldUVRect = uvRect;
                    oldPivot = pivot;
                } catch (Exception e) {
                    Debug.LogError ("Error occur! Might be invalid uvrect " + e.Message);
                }
            } else {
                SpriteRenderer.sprite = null;
            }
        }

        protected override void OnValidate ()
        {
            base.OnValidate ();

            var baseTexture = BaseTexture;
            if ((oldUVRect != uvRect || oldPivot != pivot) && baseTexture != null && IsWebpTexture(baseTexture)) {
                SetWebpTextureFromShared (BaseTexture);
            }
        }
    }
}