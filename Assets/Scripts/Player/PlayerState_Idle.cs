using System.Collections;
using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class PlayerState_Idle : PlayerState {
    public PlayerState_Idle(PlayerSystem system) : base(system) { }

    public override IEnumerator Start() {
        _system.ePlayerState = EPlayerState.Player_Idle;
        yield break;
    }

    public override IEnumerator Process() {
        // PlayerState_Jump condition & transition
        if (Input.GetButtonDown("Jump")) {
            _system.SetState(new PlayerState_Jump(_system));
            yield break;
        }

        // PlayerState_Fall condition & transition
        if (!_system.characterController.isGrounded && _system.characterController.velocity.y < 0) {
            _system.SetState(new PlayerState_Fall(_system));
            yield break;
        }

        // PlayerState_Walk condition & transition
        if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            _system.SetState(new PlayerState_Walk(_system));
            yield break;
        }

        yield break;
    }

    public override IEnumerator Pause() {
        yield break;
    }

    public override IEnumerator Resume() {
        yield break;
    }
}
