using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        public bool Running=false;
        public Transform CameraPosition;
        
        [SerializeField]
        private Animator _lightAnimator;

        
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        
        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }
        private void FixedUpdate()
        {
            if (Running)
            {
                var h = CrossPlatformInputManager.GetAxis("Horizontal");
                m_Character.Move(h, false, false);
            }

        }
        
        public void GlowFinal()
        {
            _lightAnimator.gameObject.SetActive(true);
            //_lightAnimator.SetBool("action",true);
        }
    }
}
