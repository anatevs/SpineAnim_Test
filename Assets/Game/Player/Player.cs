using Assets.Input;
using UnityEngine;
using VContainer;

namespace GameCore
{
    public class Player : MonoBehaviour
    {
        [SerializeField]
        private bool _isSighting;

        [SerializeField]
        private SightPresenter _sightPresenter;

        private InputHandler _input;

        [Inject]
        private void Construct(InputHandler input)
        {
            _input = input;
        }


    }
}