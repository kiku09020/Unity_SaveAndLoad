using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace scene_2 {
    public class TrailToggle : MonoBehaviour {
        [SerializeField] GameManager gameManager;
        [SerializeField] Toggle toggle;

		public void Changed()
        {
            gameManager.trail.enabled = toggle.isOn;
        }
    }
}