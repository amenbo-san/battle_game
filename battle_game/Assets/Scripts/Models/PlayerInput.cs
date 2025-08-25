using UnityEngine;

public enum InputType
{
    Unit,       // ユニット召喚・移動・攻撃
    Object,     // オブジェクト設置
    Command     // 指示カードやスキル
}

[System.Serializable]
public class PlayerInput
{
    public int playerId;       // プレイヤー識別ID
    public InputType type;     // 入力の種類
    public int cardId;         // 使用したカードID（召喚/オブジェクト/指示）
    public Vector3 targetPos;  // 目的地・設置座標
    public int targetUnitId;   // 攻撃や指示対象のユニットID（あれば）
    public float timestamp;    // 入力発生時間（同期用）
}
