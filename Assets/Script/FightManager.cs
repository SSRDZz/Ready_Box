using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class FightManager : MonoBehaviour
{
    public Boxer player1;
    public Boxer player2;

    private Boxer currentPlayer;
    private Boxer opponentPlayer;

    private int actionsThisTurn = 0;
    private const int maxActionsPerTurn = 2;

    private void Start()
    {
        currentPlayer = player1;
        opponentPlayer = player2;
        StartCoroutine(GameLoop());
    }

    private IEnumerator GameLoop()
    {
        while (!IsFightOver())
        {
            yield return StartCoroutine(PlayerTurn());
            SwitchTurn();
        }

        Debug.Log("Fight Ended!");
    }

    private IEnumerator PlayerTurn()
    {
        actionsThisTurn = 0;

        while (actionsThisTurn < maxActionsPerTurn)
        {
            yield return StartCoroutine(AttackPhase());
            actionsThisTurn++;
        }
    }

    private IEnumerator AttackPhase()
    {
        // 1. Player picks card
        SkillData attackCard = currentPlayer.ChooseAttackCard();

        // 2. Range check + range bonus
        ApplyRangeBonus(attackCard);

        // 3. OnActivate triggers
        ProcessTriggers(attackCard, TriggerCondition.OnActivate);

        // 4. Opponent reaction
        SkillData reactionCard = opponentPlayer.ChooseReactionCard(attackCard);
        ProcessTriggers(reactionCard, reactionCard.triggerCondition);

        // 5. Resolve hit/miss
        bool landedClean = ResolveAttack(attackCard, reactionCard);

        // 6. Post-hit triggers
        if (landedClean)
            ProcessTriggers(attackCard, TriggerCondition.OnAttackLandClean);
        else
            ProcessTriggers(attackCard, TriggerCondition.OnAttackLand);

        yield return null;
    }

    private void ApplyRangeBonus(SkillData card)
    {
        // Example: +1 power if in correct range
        if (card.range == currentPlayer.CurrentRange)
            card.power += 1;
    }

    private void ProcessTriggers(SkillData card, TriggerCondition condition)
    {
        if (card != null && card.triggerCondition == condition)
        {
            currentPlayer.ModifyResource(card.gainingType, card.gainingValue);
            currentPlayer.Move(card.movement);
        }
    }

    private bool ResolveAttack(SkillData attack, SkillData reaction)
    {
        // TODO: implement hit/miss logic
        return true;
    }

    private void SwitchTurn()
    {
        var temp = currentPlayer;
        currentPlayer = opponentPlayer;
        opponentPlayer = temp;
    }

    private bool IsFightOver()
    {
        return player1.HP <= 0 || player2.HP <= 0;
    }
}
