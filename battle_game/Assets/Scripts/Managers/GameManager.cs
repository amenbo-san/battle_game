using System.Collections;
using System.Collections.Generic;
using System.Resources;
using System.Runtime.Serialization;
using UnityEngine;

/// <summary>
/// ゲームのベースとなる動作を行う、メインルーチン
/// </summary>
public class GameManager : MonoBehaviour
{
    //外部マネージャー
    [SerializeField] UnitManager unitManager;
    [SerializeField] GameObjectManager objectManager;
    [SerializeField] CommandManager commandManager;
    [SerializeField] GameResourceManager resourceManager;

    /// <summary>
    /// バトル継続フラグ
    /// </summary>
    private bool isBattleActive;
    /// <summary>
    /// プレイヤーの入力バッファ
    /// </summary>
    private List<PlayerInput> receivedInputs;


    void FixedUpdateNetwork()
    {
        if (!isBattleActive) return;

        // 1. プレイヤー入力処理
        ProcessPlayerInput();

        // 2. 各 Manager の更新
        unitManager.UpdateUnits();
        objectManager.UpdateObjects();
        commandManager.UpdateCommands();
        resourceManager.UpdateResources();

        // 3. サーバ authoritative 確定
        SyncToClients();

        // 4. 勝敗判定
        CheckBattleEnd();
    }

    void ProcessPlayerInput()
    {
        foreach (var input in receivedInputs)
        {
            if (ValidateInput(input))
                DistributeInput(input);
            // 不正入力は破棄
        }
    }

    bool ValidateInput(PlayerInput input)
    {
        // コストチェック / 対象有効性 / タイミングなど
        return true; // 問題なければtrue
    }

    void DistributeInput(PlayerInput input)
    {
        if (input.type == InputType.Unit)
            unitManager.HandleInput(input);
        else if (input.type == InputType.Object)
            objectManager.HandleInput(input);
        else if (input.type == InputType.Command)
            commandManager.HandleInput(input);
    }

    /// <summary>
    /// クライアントとの同期
    /// </summary>
    void SyncToClients()
    {
        // authoritative 状態を各クライアントに送信
    }

    /// <summary>
    /// バトル継続フラグの更新を行う
    /// </summary>
    void CheckBattleEnd()
    {
        // 拠点破壊 or 時間切れで勝敗判定
    }


}
