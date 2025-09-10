using UnityEngine;

public class PlayingFootsteps : MonoBehaviour
{
   public void PlaySound()
    {
        SoundManager.PlaySound(SoundManager.SoundType.FOOTSTEPS, 0.5f);
    }
}
