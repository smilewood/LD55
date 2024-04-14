using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace LD55
{
    public class rendererFeature : ScriptableRendererFeature
    {

        [System.Serializable]
        public class CustomPassSettings
        {
            public RenderPassEvent passEvent = RenderPassEvent.BeforeRenderingPostProcessing;
            public int screenHeight = 144;
        }
        [SerializeField] private CustomPassSettings customPassSettings;
        private PixelizePass customPass;
        public override void Create()
        {
            customPass = new PixelizePass(customPassSettings);
        }
        public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
#if UNITY_EDITOR
            if (renderingData.cameraData.isSceneViewCamera) return;
#endif
            renderer.EnqueuePass(customPass);
        }

    }
}
