using UnityEngine;
using UnityEngine.SceneManagement;
using Game.Management;

namespace Game.Button
{
    public class MenuButton : MonoBehaviour
    {
        public void StartSingleGame()
        {
            SceneManager.LoadScene(Scenes.GAMEPLAY);
        }
    }
}
