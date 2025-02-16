using System;
using Animations;
using UnityEngine;
using Zenject;

namespace Other.Chest
{
    public class ChestView : MonoBehaviour
    {
        [SerializeField] private GameObject toolTip;
        [SerializeField] private ChestAnimator animator;

        private bool isPlayerNear = false;
        private bool isChestOpen = false; // Для відстеження стану скрині
        private ChestViewModel _viewModel;

        [Inject]
        public void Initialize(ChestViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        private void Update()
        {
            if (isPlayerNear && Input.GetKeyDown(KeyCode.E))
            {
                if (isChestOpen)
                {
                    CloseChest();
                }
                else
                {
                    OpenChest();
                }
            }
        }

        private void OpenChest()
        {
            animator.OpenChestAnimation();
            isChestOpen = true;
        }

        private void CloseChest()
        {
            animator.CloseChestAnimation();
            isChestOpen = false;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                toolTip.SetActive(true);
                isPlayerNear = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                toolTip.SetActive(false);
                isPlayerNear = false;

                if (isChestOpen) // Закривати скриню, якщо гравець пішов
                {
                    CloseChest();
                }
            }
        }
    }
}