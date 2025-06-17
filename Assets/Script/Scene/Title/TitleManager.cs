using UnityEngine;

public class TitleManager : MonoBehaviour
{
    public void OnClick_GameStart()
    {
        Debug.Log("SceneLoader에게 게임 씬 로드를 요청합니다.");
        // 싱글톤 인스턴스를 통해 SceneLoader의 함수를 바로 호출
        SceneLoader.Instance.LoadScene("VillageScene");
    }

    public void OnClick_GameExit()
    {
        Debug.Log("SceneLoader에게 게임 종료를 요청합니다.");
        SceneLoader.Instance.QuitGame();
    }
}