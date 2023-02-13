using System.Collections;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerState_Jump : PlayerState {
    public PlayerState_Jump(PlayerSystem system) : base(system) { }

    public override IEnumerator Start() {
        _system.ePlayerState = EPlayerState.Player_Jump;
        yield break;
    }

    public override IEnumerator Process() {
        // PlayerState_Fall condition & transition
        if (_system.characterController.velocity.y < 0) {
            _system.SetState(new PlayerState_Fall(_system));
            yield break;
        }

        yield break;
    }
}
