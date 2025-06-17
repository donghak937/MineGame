using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

// 이 스크립트는 유니티 에디터가 시작될 때마다 초기화됨
[InitializeOnLoad]
public class EditorStartSceneLoader
{
    // static 생성자. 딱 한 번만 호출됨.
    static EditorStartSceneLoader()
    {
        // 에디터의 플레이 모드 상태가 바뀔 때마다 OnPlayModeStateChanged 함수를 호출하도록 등록
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private static void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        // 플레이 버튼을 누른 직후 (EnteringPlayMode)에만 작동
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            // 현재 열려있는 씬이 TitleScene이 아니라면
            if (SceneManager.GetActiveScene().name != "TitleScene")
            {
                // TitleScene을 강제로 로드
                EditorSceneManager.LoadScene("TitleScene");
            }
        }
    }
}