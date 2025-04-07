using Assets.Input;
using GameCore;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public sealed class GameLifetimeScope : LifetimeScope
{
    [Header("UI views")]
    [SerializeField]
    private SightView _sightView;

    protected override void Configure(IContainerBuilder builder)
    {
        RegisterInput(builder);

        RegisterUI(builder);
    }

    private void RegisterInput(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<InputHandler>()
            .AsSelf();
    }

    private void RegisterUI(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<SightPresenter>()
            .WithParameter(_sightView)
            .AsSelf();
    }
}