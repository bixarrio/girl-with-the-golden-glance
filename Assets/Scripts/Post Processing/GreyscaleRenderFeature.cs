using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class GreyscaleRenderFeature : ScriptableRendererFeature
{
    #region Properties and Fields

    [SerializeField] Material _material;

    private CustomRenderPass _renderPass;

    #endregion

    #region Override Methods
    public override void Create()
    {
        _renderPass = new CustomRenderPass(_material);
        _renderPass.renderPassEvent = RenderPassEvent.AfterRendering;
    }

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        _renderPass.Source = renderer.cameraColorTarget;
        renderer.EnqueuePass(_renderPass);
    }

    #endregion

    #region Classes

    class CustomRenderPass : ScriptableRenderPass
    {
        #region Properties and Fields

        private readonly Material _greyscaleMaterial;
        private RenderTargetHandle _tempRenderTarget;

        public RenderTargetIdentifier Source { get; set; }

        #endregion

        #region ctor

        public CustomRenderPass(Material greyscaleMaterial)
        {
            _greyscaleMaterial = greyscaleMaterial;
            _tempRenderTarget.Init("_tempColorTexture");
        }

        #endregion

        #region Override Methods

        public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var commandBuffer = CommandBufferPool.Get();
            commandBuffer.GetTemporaryRT(_tempRenderTarget.id, renderingData.cameraData.cameraTargetDescriptor);
            Blit(commandBuffer, Source, _tempRenderTarget.Identifier(), _greyscaleMaterial);
            Blit(commandBuffer, _tempRenderTarget.Identifier(), Source);
            context.ExecuteCommandBuffer(commandBuffer);
            CommandBufferPool.Release(commandBuffer);
        }

        #endregion
    }

    #endregion
}
