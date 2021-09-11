
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts.Managers
{
    public class MainMenu : MonoBehaviour
    {
        public enum Scenes
        {
            MainMenu,
            Game
        }
        // Start is called before the first frame update
        public void OnPlayClicked()
        {
            SceneManager.LoadScene((int)Scenes.Game);
        }
    }
}