// Player state machine
public enum PlayerState
{
    Idle,
    Walking,
    Running,
    Jumping,
    Falling,
    Landing,
    WallSliding,
    WallJumping,
    Dashing,
    Attacking,
    Stagger,
    Interacting
}

// Player State Class
[System.Serializable]
public class PlayerStateMachine
{
    public PlayerStateMachine(PlayerState state)
    {
        this.state = state;
    }

    public PlayerState state { get; set; }

    public void ChangeState(PlayerState newState)
    {
        if (state != newState)
        {
            state = newState;
        }
    }
}