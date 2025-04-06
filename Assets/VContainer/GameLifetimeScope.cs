using Assets.Input;
using VContainer;
using VContainer.Unity;

public sealed class GameLifetimeScope : LifetimeScope
{
    protected override void Configure(IContainerBuilder builder)
    {
        RegisterInput(builder);
    }

    private void RegisterInput(IContainerBuilder builder)
    {
        builder.RegisterEntryPoint<InputHandler>().
            AsSelf();
    }
}