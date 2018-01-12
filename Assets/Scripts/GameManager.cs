using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum PHASE
    {
        PREPARE,        // 準備
        FALL,           // 落下
        CAMERA_MOVE,    // カメラ移動
    };

    // 次の動物出現Yオフセット
    const float NEXT_ANIMAL_OFFSET = 5.0f;

    [SerializeField, HeaderAttribute("動物")]
    GameObject animalPrefab = null;

    [SerializeField, HeaderAttribute("動物マネージャ")]
    AnimalManager animalManager = null;
    [SerializeField, HeaderAttribute("カメラコントローラ")]
    CameraController cameraController = null;

    // 落下中の動物
    GameObject fallingAnimal = null;
    // フェーズ
    PHASE phase = PHASE.FALL;

    public void OnRotationStay()
    {
        // 落下準備中以外は無効
        if (phase != PHASE.PREPARE) return;
        if (fallingAnimal == null) return;

        fallingAnimal.transform.Rotate(Vector3.forward, -0.5f);

    }

    // Use this for initialization
    void Start () {

        StartCoroutine("Game");
	}

    IEnumerator Game()
    {
        yield return new WaitForSeconds(1.0f);

        while (true)
        {
            // 動物発生
            phase = PHASE.PREPARE;
            CreateAnimal();

            yield return new WaitForSeconds(5.0f);

            // 落下開始
            phase = PHASE.FALL;
            FallAnimal();

            // 動物が止まるまで待つ
            yield return new WaitUntil(DidStopFalling);

            animalManager.AddAnimal(fallingAnimal);
            fallingAnimal = null;

            // カメラスクロール開始
            phase = PHASE.CAMERA_MOVE;
            float maxYPos = animalManager.GetMaxYPos();
            cameraController.StartMove(new Vector3(0, maxYPos, 0));
            // カメラスクロールが終わるまで待つ
            yield return new WaitWhile(cameraController.IsMoving);
        }
    }

    void CreateAnimal()
    {
        Vector3 pos = Vector3.zero;
        pos.y = cameraController.transform.position.y + NEXT_ANIMAL_OFFSET;

        fallingAnimal = animalManager.CreateAnimal(pos);
    }

    void FallAnimal()
    {
        if (fallingAnimal == null) return;

        // 剛体がついていない
        Rigidbody2D rigidbody = fallingAnimal.GetComponent<Rigidbody2D>();
        if (rigidbody == null) return;

        rigidbody.gravityScale = 1.0f;
    }

    bool DidStopFalling()
    {
        // 落下中の動物がいない
        if (fallingAnimal == null) return true;

        // 剛体がついていない
        Rigidbody2D rigidbody = fallingAnimal.GetComponent<Rigidbody2D>();
        if (rigidbody == null) return true;

        // スリープに入った
        if (rigidbody.IsSleeping()) return true;

        // 落下
        if (fallingAnimal.transform.position.y < 0) return true;

        return false;
    }

    
}
