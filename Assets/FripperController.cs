using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;


public class FripperController : MonoBehaviour
{
    //HingeJointコンポーネントを入れる
    private HingeJoint myHingeJoint;

    //初期の傾き
    private float defaultAngle = 20;
    //弾いた時の傾き
    private float flickAngle = -20;
    //タップの状態を保存しておく
    private int rightTap = -1;
    private int leftTap = -1;

    // Start is called before the first frame update
    void Start ()
    {
        //HingeJointコンポーネント取得
        this.myHingeJoint = GetComponent<HingeJoint>();

        //フリッパーの傾きを設定
        SetAngle(this.defaultAngle);
    }

    // Update is called once per frame
    void Update ()
    {

        //対象キー(←↓AS)が押された時左フリッパーを動かす
        if ((Keyboard.current.leftArrowKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current[Key.A].wasPressedThisFrame || Keyboard.current[Key.S].wasPressedThisFrame) && this.gameObject.CompareTag("LeftFripperTag"))
        {
            SetAngle (this.flickAngle);
        }
        //対象キー(→↓DS)を押した時右フリッパーを動かす
        if ((Keyboard.current.rightArrowKey.wasPressedThisFrame || Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current[Key.D].wasPressedThisFrame || Keyboard.current[Key.S].wasPressedThisFrame) && this.gameObject.CompareTag("RightFripperTag"))
        {
            SetAngle (this.flickAngle);
        }

        //対象キー(←↓AS)を離された時左フリッパーを元に戻す
        if ((Keyboard.current.leftArrowKey.wasReleasedThisFrame || Keyboard.current.downArrowKey.wasReleasedThisFrame || Keyboard.current[Key.A].wasReleasedThisFrame || Keyboard.current[Key.S].wasReleasedThisFrame) && this.gameObject.CompareTag("LeftFripperTag"))
        {
            SetAngle (this.defaultAngle);
        }
        //対象キー(→↓DS)を離された時右フリッパーを元に戻す
        if ((Keyboard.current.rightArrowKey.wasReleasedThisFrame || Keyboard.current.downArrowKey.wasReleasedThisFrame || Keyboard.current[Key.D].wasReleasedThisFrame || Keyboard.current[Key.S].wasReleasedThisFrame) && this.gameObject.CompareTag("RightFripperTag"))
        {
            SetAngle (this.defaultAngle);
        }

        //タッチを取る
        //タッチを取ったあと逆側に移動して離した場合はfingerIdで判定して戻す
        foreach (Touch touch in Input.touches)
        {

            switch (touch.phase)
            {

                //タップされた
                case UnityEngine.TouchPhase.Began:
                    if(touch.position.x >= 540 && rightTap == -1 && this.gameObject.CompareTag("RightFripperTag"))
                    {
                        rightTap = touch.fingerId;
                        SetAngle (this.flickAngle);                        
                    }else if (touch.position.x < 540 && leftTap == -1 && this.gameObject.CompareTag("LeftFripperTag"))
                    {
                        leftTap = touch.fingerId;
                        SetAngle (this.flickAngle);                         
                    }
                    break;

                //タップが離された
                case UnityEngine.TouchPhase.Ended:
                    if(touch.fingerId == rightTap && this.gameObject.CompareTag("RightFripperTag"))
                    {
                        rightTap = -1;
                        SetAngle (this.defaultAngle);                        
                    }else if (touch.fingerId == leftTap && this.gameObject.CompareTag("LeftFripperTag"))
                    {
                        leftTap = -1;
                        SetAngle (this.defaultAngle);                            
                    }
                    break;
                
            }
        }


    }

    //フリッパーの傾きを設定
    public void SetAngle (float angle)
    {
        JointSpring jointSpr = this.myHingeJoint.spring;
        jointSpr.targetPosition = angle;
        this.myHingeJoint.spring = jointSpr;
    }
}
