using DG.Tweening;
using UnityEngine;

namespace Animations
{
    public class ChestAnimator : MonoBehaviour
    {
        [SerializeField] private Transform lid;
        [SerializeField] private float openAngle = 70f;
        [SerializeField] private float animationDuration = 1.0f;

        public void OpenChestAnimation()
        {
            lid.DOLocalRotate(new Vector3(openAngle, 0, 0), animationDuration)
                .SetEase(Ease.OutBack);
        }

        public void CloseChestAnimation()
        {
            lid.DOLocalRotate(Vector3.zero, animationDuration)
                .SetEase(Ease.InBack);
        }
    }
}