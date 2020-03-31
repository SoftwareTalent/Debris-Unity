using UnityEngine;
using System.Collections;

namespace Com.Debris.CoreSolution
{
    public class DestroyGemEffect : MonoBehaviour
    {
        // Constructor
        private DestroyGemEffect() { }
        
        // Animation event
        public void DestroyEffect()
        {
            Destroy(this.gameObject);
        }

        // Animation event
        public void TurnOffBonusEffect()
        {
            this.transform.parent.parent.gameObject.SetActive(false);
        }
    }
}
