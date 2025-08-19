using Facts;
using StatusEffects;
using UnityEngine;

public class IceCard : ThrowCard{
    private readonly IceCardInfoSO _info;
    public IceCard(IceCardInfoSO cardInfo, int position) : base(cardInfo, position) {
    }

    protected override void OnThrowHit(RaycastHit? hit) {
        if (!hit.HasValue) {
            RegisterUse();
            return;
        }

        var rhit = hit.Value;
        
        if (rhit.collider.gameObject.TryGetComponent(out StatusEffectsHandler status)) {
            Events.ON_PLAYER_USE_FREEZE.Raise(Unit.Default);
            status.AddEffect(StatusEffectsType.FREEZE, 8f);
        }
        RegisterUse();
    }
}