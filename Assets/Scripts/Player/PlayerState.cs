using System.Collections;
using UnityEngine;

public abstract class PlayerState {
    protected readonly PlayerSystem _system;

    public PlayerState(PlayerSystem system) {
        _system = system;
    }


    // Starts as the player enters the state.
    public virtual IEnumerator Start() {
        yield break;
    }

    // Updates every frame as long as the state is current.
    public virtual IEnumerator Process() {
        yield break;
    }

    // Starts as the player exits the state.
    public virtual IEnumerator End() {
        yield break;
    }

    // Executes when the game is paused
    public virtual IEnumerator Pause() {
        yield break;
    }

    // Executes when the game is resumed
    public virtual IEnumerator Resume() {
        yield break;
    }
}