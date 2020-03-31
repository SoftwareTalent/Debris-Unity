using UnityEngine;
using System.Collections;

namespace Com.Debris.CoreSolution
{
    /// <summary>
    /// Show up layer name on the background
    /// </summary>
    public class SetLayerFloorName : MonoBehaviour
    {
        [SerializeField]
        private Renderer m_rendererTextMesh;

        [SerializeField]
        private string m_layerName;

        public TextMesh textMesh;

        // Constructor
        private SetLayerFloorName() { }

        // Behaviour messages
        void Start()
        {
            m_rendererTextMesh.sortingLayerName = m_layerName;
            m_rendererTextMesh.sortingOrder = 1;

            textMesh.text = "floor " + FloorGenerator.instance.HighestFloor;
        }
    }
}
