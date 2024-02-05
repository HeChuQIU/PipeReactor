using System.IO;
using System.Linq;
using Godot;
using Unity;

namespace PipeReactor.Core;

public static class ResourcesLoader
{
    static ResourcesLoader()
    {
        // PipeReactor.OnResourcesLoading += LoadTextures;
    }

    public const string ResourcePath = "res://Resource";

    public static string TexturesPath => Path.Combine(ResourcePath, "Texture");

    public static void LoadTextures()
    {
        var globalizePath = ProjectSettings.GlobalizePath(TexturesPath);
        Directory.EnumerateFiles(globalizePath, "*.png", SearchOption.AllDirectories)
            .Select(s =>
            {
                GD.Print(s);
                return (Image.LoadFromFile(s), Path.GetFileNameWithoutExtension(s));
            })
            .Select(i => (ImageTexture.CreateFromImage(i.Item1),i.Item2))
            .ToList().ForEach(i => PipeReactor.Container.RegisterInstance(i.Item2, i.Item1));
    }
}