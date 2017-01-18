using UnityEngine;
using System.Collections;
using UnityEngine.UI;

using WebP;

namespace UBootstrap.Webp
{
    [RequireComponent (typeof(RawImage))]
    public class UIRawImageWebp : WebpRendererBase
    {
        private RawImage rawImage = null;

        private RawImage RawImage {
            get {
                if (this.rawImage == null) {
                    this.rawImage = this.GetComponent<RawImage> ();
                } 
                return this.rawImage;
            }
        }


        public override Texture2D BaseTexture {
            get {
                if (RawImage.texture != null) {
                    return RawImage.texture as Texture2D;
                }

                return null;
            }
        }

        protected override void SetWebpTextureFromShared (Texture2D sharedWebpTexture)
        {
            if (sharedWebpTexture != null) {
                RawImage.texture = sharedWebpTexture;
            } else {
                RawImage.texture = null;
            }
        }
    }
}