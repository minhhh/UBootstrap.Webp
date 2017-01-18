using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using WebP;

namespace UBootstrap.Webp
{
    [RequireComponent (typeof(Image))]
    public class UIImageWebp : WebpRendererBase
    {
        public Rect uvRect = new Rect (0, 0, 1, 1);
        private Rect oldUVRect = new Rect (0, 0, 1, 1);

        private Image image = null;

        private Image Image {
            get {
                if (this.image == null) {
                    this.image = this.GetComponent<Image> ();
                } 
                return this.image;
            }
        }


        public override Texture2D BaseTexture {
            get {
                if (Image.sprite != null) {
                    return Image.sprite.texture;
                }

                return null;
            }
        }

        protected override void SetWebpTextureFromShared (Texture2D sharedWebpTexture)
        {
            if (sharedWebpTexture != null) {
                var sprite = Sprite.Create (sharedWebpTexture, new Rect (uvRect.x * sharedWebpTexture.width, uvRect.y * sharedWebpTexture.height, uvRect.width * sharedWebpTexture.width, uvRect.height * sharedWebpTexture.height), Vector2.zero);
                Image.sprite = sprite;
                oldUVRect = uvRect;
            } else {
                Image.sprite = null;
            }
        }

        protected override void OnValidate ()
        {
            base.OnValidate ();

            var baseTexture = BaseTexture;
            if ((oldUVRect != uvRect) && baseTexture != null && IsWebpTexture(baseTexture)) {
                SetWebpTextureFromShared (BaseTexture);
            }
        }
    }
}