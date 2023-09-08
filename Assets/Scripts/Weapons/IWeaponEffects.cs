using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public interface IWeaponEffects
{
    void PlayShootEffect();

    void PlayReloadEffect(TextMeshProUGUI textMeshProUGUI);
}