public interface IGameObserver
{
    public void OnGameNotify(IsometricGameState isoGameState);
    public void OnSideScrollGameNotify(SideScrollGameState sidescrollGameState);
}
