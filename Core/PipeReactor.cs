using System;
using Unity;

namespace PipeReactor.Core;

public static class PipeReactor
{
    static PipeReactor()
    {
        OnResourcesLoading += ResourcesLoader.LoadTextures;

        OnResourcesLoading?.Invoke();
    }

    public static UnityContainer Container { get; private set; } = new();

    public static event Action OnResourcesLoading;
}