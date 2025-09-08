using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    public Slider staminaBarSlider;

    public void GiveFullStamina(float stamina)
    {
        staminaBarSlider.maxValue = stamina;
        staminaBarSlider.value = stamina;
    }

    public void SetStamina(float stamina)
    {
        staminaBarSlider.value = stamina;
    }

}
