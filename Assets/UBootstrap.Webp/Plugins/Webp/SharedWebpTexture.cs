using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using WebP;

namespace UBootstrap.Webp
{
    public static class SharedWebpTexture
    {
        class SharedWebpTextureEntry
        {
            public string assetId;
            public int refCount;
            public Texture2D texture;
        }

        [System.NonSerialized]
        private static List<SharedWebpTextureEntry> sharedWebpTextures = new List<SharedWebpTextureEntry> ();

        static SharedWebpTextureEntry GetSharedWebpTextureEntry (string assetId)
        {
            SharedWebpTextureEntry entry;
            for (int i = 0; i < sharedWebpTextures.Count; i++) {
                entry = sharedWebpTextures [i];
                if (entry.assetId == assetId) {
                    return entry;
                }
            }
            return null;
        }

        public static Texture2D RefSharedWebpTexture (TextAsset webpAsset)
        {
            if (webpAsset == null) {
                return null;
            }

            Texture2D sharedTexture = null;
            string sharedTextureKey = webpAsset.GetInstanceID ().ToString ();
            SharedWebpTextureEntry sharedWebpTextureEntry = GetSharedWebpTextureEntry (sharedTextureKey);

            if (sharedWebpTextureEntry != null) {
                // The texture might be already GCed
                if (sharedWebpTextureEntry.texture == null) {
                    sharedWebpTextures.Remove (sharedWebpTextureEntry);
                    sharedWebpTextureEntry = null;
                }
            }
            
            if (sharedWebpTextureEntry != null) {
                sharedWebpTextureEntry.refCount++;
                sharedTexture = sharedWebpTextureEntry.texture;
            } else {
                Status lError;
                sharedTexture = Texture2DExt.CreateTexture2DFromWebP (webpAsset.bytes, false, true, out lError, Texture2DExt.ScaleBy (1f));

                if (lError != Status.SUCCESS) {
//                    Debug.LogError ("SharedWebpTexture::RefSharedAlphaMaterial Webp Load Error : " + lError.ToString ());
                    return null;
                }

                sharedWebpTextureEntry = new SharedWebpTextureEntry () {
                    assetId = sharedTextureKey,
                    texture = sharedTexture,
                    refCount = 1
                };

                sharedWebpTextures.Add (sharedWebpTextureEntry);
            }

            return sharedTexture;
        }

        public static void UnrefSharedWebpTexture (Texture2D sharedTexture)
        {
            if (sharedTexture == null) {
                return;
            }

            for (int i = 0; i < sharedWebpTextures.Count; i++) {
                SharedWebpTextureEntry sharedWebpTextureEntry = sharedWebpTextures [i];
                if (sharedWebpTextureEntry == null) {
                    continue;
                }

                if (sharedWebpTextureEntry.texture == sharedTexture) {
                    sharedWebpTextureEntry.refCount--;
                    if (sharedWebpTextureEntry.refCount <= 0) {
                        sharedWebpTextures.Remove (sharedWebpTextureEntry);
                    }
                    return;
                }
            }
        }

        public static bool ContainSharedTexture (Texture2D sharedTexture)
        {
            if (sharedTexture == null) {
                return false;
            }

            for (int i = 0; i < sharedWebpTextures.Count; i++) {
                SharedWebpTextureEntry sharedWebpTextureEntry = sharedWebpTextures [i];
                if (sharedWebpTextureEntry == null) {
                    continue;
                }

                if (sharedWebpTextureEntry.texture == sharedTexture) {
                    return true;
                }
            }

            return false;
        }

        #if UNITY_EDITOR || DEVELOPMENT_BUILD
        public static void DumpDebugStats ()
        {
            Debug.Log ("SharedWebpTexture, key count : " + sharedWebpTextures.Count);
            foreach (SharedWebpTextureEntry sharedWebpTextureEntry in sharedWebpTextures) {
                if (sharedWebpTextureEntry.texture != null) {
                    Debug.Log (string.Format ("{0}: {1}, refCount [{2}]", sharedWebpTextureEntry.assetId, sharedWebpTextureEntry.texture.name, sharedWebpTextureEntry.refCount));
                }
            }
        } 
        #endif
    }
}