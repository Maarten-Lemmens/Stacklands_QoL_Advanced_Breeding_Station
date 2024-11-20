using HarmonyLib;
using System;
using System.Collections;
using UnityEngine;

namespace AdvancedBreedingStationModNS
{
    public class AdvancedBreedingStationMod : Mod
    {
        public override void Ready()
        {
            WorldManager.instance.GameDataLoader.AddCardToSetCardBag(SetCardBagType.AdvancedBuildingIdea, "blueprint_advanced_breeding_station", 1);
        }
    }

    public class AdvancedBreedingStation : BreedingPen
	{
		// Overwrite required to put timer to 60 seconds (instead of the default hard-coded 120 seconds). 
        public override void UpdateCard()
        {
            if (MyGameCard.GetChildCount() == 2)
            {
                MyGameCard.StartTimer(60f, BreedAnimals, SokLoc.Translate("card_advanced_breeding_station_status"), GetActionId("BreedAnimals"));
            }
            else if (MyGameCard.GetChildCount() > 2)
            {
                GameCard gameCard = MyGameCard.TryGetNthChild(3);
                if (gameCard != null)
                {
                    gameCard.RemoveFromParent();
                }
                MyGameCard.CancelTimer(GetActionId("BreedAnimals"));
            }
            else
            {
                MyGameCard.CancelTimer(GetActionId("BreedAnimals"));
            }
        }
        
        [TimedAction("breed_animals")]
        public void BreedAnimals()
        {
            CardData cardData = WorldManager.instance.CreateCard(base.transform.position, MyGameCard.Child.CardData.Id);
            WorldManager.instance.StackSendCheckTarget(MyGameCard, cardData.MyGameCard, OutputDir);
            QuestManager.instance.SpecialActionComplete("breed_" + cardData.Id);        
        }
	}
}