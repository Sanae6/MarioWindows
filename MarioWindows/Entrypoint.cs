using System;
using System.Drawing;
using System.IO;
using System.Reflection;
using Windows.Win32.Foundation;
using MarioWindows.Structs;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Mathematics;
using OpenTK.Windowing.Common;
using OpenTK.Windowing.Desktop;
using OpenTK.Windowing.GraphicsLibraryFramework;

namespace MarioWindows {
    class Entrypoint : GameWindow {
        public static int MarioProgram;
        public static int MarioTexture;
        public MarioGl Mario;

        static unsafe void Main(string[] args) {
            // To create a new window, create a class that extends GameWindow, then call Run() on it.
            using Entrypoint window = new Entrypoint(GameWindowSettings.Default, new NativeWindowSettings {
                Size = new Vector2i(800, 600),
                Title = "Mario",
                // This is needed to run on macos
                Flags = ContextFlags.ForwardCompatible,
            });
            // HWND hwnd = (HWND) GLFW.GetWin32Window(window.WindowPtr);
            // Graphics gfx = Graphics.FromHwnd(hwnd);

            // PInvoke.SetWindowLong(hwnd, WINDOW_LONG_PTR_INDEX.GWL_EXSTYLE, (int) (WINDOW_EX_STYLE.WS_EX_LAYERED | WINDOW_EX_STYLE.WS_EX_TOPMOST));
            window.Run();
        }

        public Entrypoint(GameWindowSettings gameWindowSettings, NativeWindowSettings nativeWindowSettings) : base(gameWindowSettings, nativeWindowSettings) { }

        private string ReadResource(string location) {
            using Stream stream = Assembly.GetExecutingAssembly().GetManifestResourceStream($"MarioWindows.{location}")!;
            using StreamReader reader = new StreamReader(stream);
            return reader.ReadToEnd();
        }

        protected override unsafe void OnLoad() {
            {
                MarioProgram = GL.CreateProgram();
                int vertShader = GL.CreateShader(ShaderType.VertexShader);
                int fragShader = GL.CreateShader(ShaderType.FragmentShader);
                GL.ShaderSource(vertShader, ReadResource("mario.vert"));
                GL.ShaderSource(fragShader, ReadResource("mario.frag"));
                GL.CompileShader(vertShader);
                GL.CompileShader(fragShader);
                GL.AttachShader(MarioProgram, vertShader);
                GL.AttachShader(MarioProgram, fragShader);
                GL.LinkProgram(MarioProgram);
                GL.DetachShader(MarioProgram, vertShader);
                GL.DetachShader(MarioProgram, fragShader);
            }

            Sm64.sm64_global_terminate();
            byte[] textureData = new byte[4 * 704 * 64];
            Sm64.sm64_global_init(File.ReadAllBytes("baserom.us.z64"), textureData);
            Sm64.sm64_static_surfaces_load(Surface.CreateSurfaceList(ConstantSurfaces.Vertices), (uint) ConstantSurfaces.Vertices.Length);
            
            MarioTexture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, MarioTexture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (uint) TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (uint) TextureWrapMode.ClampToEdge);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int) TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int) TextureMinFilter.Linear);
            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, 704, 64, 0, PixelFormat.Rgba, PixelType.UnsignedByte, textureData);
            Mario = new MarioGl(new Vector3i(50, 1000, 50));
        }

        protected override void OnUpdateFrame(FrameEventArgs e) {
            if (KeyboardState.IsKeyDown(Keys.Escape)) Close();
            
            Console.WriteLine("Update");
            Mario.Update();

            base.OnUpdateFrame(e);
        }

        public Vector3 CameraPos;

        protected override void OnRenderFrame(FrameEventArgs args) {
            GL.ClearColor(Color.Lime);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            Matrix4 projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45.0f), ClientSize.X / (float) ClientSize.Y, 100.0f, 20000.0f),
                    view = Matrix4.LookAt(CameraPos, Mario.Position, new Vector3(0,1,0));
            

            Mario.Render(view, projection);

            SwapBuffers();
            base.OnRenderFrame(args);
        }
    }
}