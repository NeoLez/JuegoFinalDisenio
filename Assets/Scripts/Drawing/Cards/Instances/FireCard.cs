using Facts;
using StatusEffects;
using UnityEngine;

public class FireCard : ThrowCard {
    private FireCardInfoSO _info;
    public FireCard(FireCardInfoSO cardInfo, int position) : base(cardInfo, position) {
    }

    protected override void OnThrowHit(RaycastHit? hit) {
        if (!hit.HasValue) {
            RegisterUse();
            return;
        }

        var rhit = hit.Value;
        
        if (rhit.collider.gameObject.TryGetComponent(out StatusEffectsHandler status)) {
            Events.ON_PLAYER_USE_FIRE.Raise(Unit.Default);
            status.AddEffect(StatusEffectsType.BURN, 4f);
        }
        RegisterUse();
    }
}