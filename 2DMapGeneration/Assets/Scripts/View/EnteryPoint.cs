using UnityEngine;
using Controller;

namespace View
{
    public class EnteryPoint : MonoBehaviour
    {
        [SerializeField] private LevelGeneratorView _levelGeneratorView;

        private void Awake()
        {
            var controller = new LevelGeneratorController(_levelGeneratorView);
            controller.Awake();
        }
    }   
}
