using AngleSharp.Svg.Samples.SilkyNvg;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenGL;
using Silk.NET.Windowing;
using SilkyNvg;
using SilkyNvg.Graphics;
using SilkyNvg.Paths;
using SilkyNvg.Rendering.OpenGL;
using SilkyNvg.Text;

namespace OpenGL_Example
{
    public class Program
    {

        private static GL gl;
        private static Nvg nvg;

        private static IWindow window;

        private static void KeyDown(IKeyboard _, Key key, int _2)
        {
            Console.WriteLine(key);
        }

        private static void Load()
        {
            IInputContext input = window.CreateInput();
            foreach (IKeyboard keyboard in input.Keyboards)
            {
                keyboard.KeyDown += KeyDown;
            }

            gl = window.CreateOpenGL();

            OpenGLRenderer nvgRenderer = new(CreateFlags.StencilStrokes | CreateFlags.Debug, gl);
            nvg = Nvg.Create(nvgRenderer);

            SVGManager.Init(nvg, new RenderDimensions()
            {
                RenderWidth = 1250d,
                RenderHeight = 720,
                FontSize = 0
            });
        }

        private static void Render(double _)
        {
            Vector2D<float> winSize = window.Size.As<float>();
            Vector2D<float> fbSize = window.FramebufferSize.As<float>();

            float pxRatio = fbSize.X / winSize.X;

            SVGManager.UpdateDimensions(winSize.X, winSize.Y);

            gl.Viewport(0, 0, (uint)fbSize.X, (uint)fbSize.Y);
            gl.ClearColor(0.3f, 0.3f, 0.32f, 1.0f);
            gl.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit | ClearBufferMask.StencilBufferBit);

            nvg.BeginFrame(winSize.As<float>(), pxRatio);

            nvg.StrokeColour(Colour.Azure);
            nvg.Stroke();

            SVGManager.Render(nvg);

            nvg.EndFrame();
        }

        private static void Close()
        {
            nvg.Dispose();
            gl.Dispose();
        }

        static void Main()
        {
            WindowOptions windowOptions = WindowOptions.Default;
            windowOptions.FramesPerSecond = -1;
            windowOptions.ShouldSwapAutomatically = true;
            windowOptions.Size = new Vector2D<int>(1000, 600);
            windowOptions.Title = "SilkyNvg";
            windowOptions.VSync = false;
            windowOptions.PreferredDepthBufferBits = 24;
            windowOptions.PreferredStencilBufferBits = 8;

            window = Window.Create(windowOptions);
            window.Load += Load;
            window.Render += Render;
            window.Closing += Close;
            window.Run();

            window.Dispose();
        }

    }
}