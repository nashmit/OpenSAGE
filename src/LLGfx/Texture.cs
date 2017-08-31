﻿namespace LLGfx
{
    public sealed partial class Texture : GraphicsDeviceChild
    {
        public int MipMapCount { get; }

        public static Texture CreateTexture2D(
            GraphicsDevice graphicsDevice,
            ResourceUploadBatch uploadBatch,
            PixelFormat pixelFormat,
            int width,
            int height,
            TextureMipMapData[] mipMapData)
        {
            return new Texture(
                graphicsDevice,
                uploadBatch,
                pixelFormat,
                width,
                height,
                mipMapData);
        }

        private Texture(
            GraphicsDevice graphicsDevice,
            ResourceUploadBatch uploadBatch,
            PixelFormat pixelFormat,
            int width,
            int height,
            TextureMipMapData[] mipMapData)
            : base(graphicsDevice)
        {
            MipMapCount = mipMapData.Length;

            PlatformConstruct(
                graphicsDevice,
                uploadBatch,
                pixelFormat,
                width,
                height,
                mipMapData);
        }
    }

    public struct TextureMipMapData
    {
        public byte[] Data;
        public int BytesPerRow;
    }
}