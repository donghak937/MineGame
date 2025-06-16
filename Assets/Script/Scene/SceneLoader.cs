using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    // 이 클래스의 인스턴스를 저장할 static 변수 (싱글톤)
    public static SceneLoader Instance { get; private set; }

    private void Awake()
    {
        // --- 싱글톤 패턴 구현 ---
        // 만약 Instance가 아직 할당되지 않았다면
        if (Instance == null)
        {
            // 이 인스턴스를 유일한 인스턴스로 설정
            Instance = this;
            // 씬이 전환되어도 이 게임 오브젝트를 파괴하지 않음
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            // 이미 Instance가 존재한다면, 새로 생긴 이 오브젝트는 중복이므로 파괴
            Destroy(gameObject);
        }
    }

    // 다른 스크립트에서 호출할 씬 로드 함수
    // 씬의 이름을 받아서 해당 씬을 로드함
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    // 게임 종료 함수도 여기에 두면 좋다
    public void QuitGame()
    {
        Application.Quit();

        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}