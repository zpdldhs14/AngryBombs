public interface IPlayerState
{
    void EnterState(playerMovement player);
    void UpdateState(playerMovement player);
    void ExitState(playerMovement player);
}