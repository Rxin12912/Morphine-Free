using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Morphine.Framework.Components
{
    public class ButtonCollider : MonoBehaviour
    {
        public Elements.ButtonInfo btn;
        public string relatedText;

        private void OnTriggerEnter(Collider collider)
        {
            if (Time.frameCount >= MenuBase.framePressCooldown + 20)
            {
                MenuBase.framePressCooldown = Time.frameCount;
                transform.localScale = new Vector3(transform.localScale.x / 3, transform.localScale.y, transform.localScale.z);
                GorillaTagger.Instance.StartVibration(false, 1f, .1f);
                GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, false, 1.0f);
                MenuBase.Toggle(btn);
            }
        }
    }
}
