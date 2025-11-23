using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour
{

    private int score = 0;

    //ボールが見える可能性のあるz軸の最小値
    private float visiblePosZ = -6.5f;

    //ゲームオーバを表示するテキスト
    private GameObject gameoverText;
    private GameObject scoreText;

    // Start is called before the first frame update
    void Start ()
    {
        //シーン中のTextオブジェクトを取得
        this.gameoverText = GameObject.Find("GameOverText");
        this.scoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update ()
    {
        //ボールが画面外に出た場合
        if (this.transform.position.z < this.visiblePosZ)
                {
            //GameoverTextにゲームオーバを表示
            this.gameoverText.GetComponent<Text> ().text = "Game Over";
            this.score = 0;
        }
    }
    //衝突時に呼ばれる関数    
    void OnCollisionEnter(Collision other)
    {
        //雲や星にあたった場合はスコアを加算する
        switch (other.gameObject.tag)
        {
            case "SmallStarTag":
            this.score += 10;
            Debug.Log("SmallStar");
            break;

            case "LargeStarTag":
            this.score += 20;
            Debug.Log("LargeStar");
            break;

            case "SmallCloudTag":
            this.score += 25;
            Debug.Log("SmallCloud");
            break;

            case "LargeCloudTag":
            this.score += 35;
            Debug.Log("LargeCloud");
            break;

            default:
            break;    
        }
        this.scoreText.GetComponent<Text> ().text = "SCORE:" + this.score.ToString("D5");
    }
}