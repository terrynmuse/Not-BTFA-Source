using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ForgottenMemories.Items.Boss
{
	public class MegaTreeBag : ModItem
	{
		public override void SetDefaults()
		{

			item.maxStack = 999;
			item.consumable = true;
			item.width = 24;
			item.height = 24;

			item.expert = true;
			item.rare = 3;
			bossBagNPC = mod.NPCType("GhastlyEnt");
		}

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Treasure Bag");
      Tooltip.SetDefault("Right click to open");
    }


		public override bool CanRightClick()
		{
			return true;
		}

		public override void OpenBossBag(Player player)
		{
			player.TryGettingDevArmor();
            player.QuickSpawnItem(mod.ItemType("AmberCrystal"), 1); 
			player.QuickSpawnItem(mod.ItemType("ForestEnergy"), Main.rand.Next(22, 35));
			
			switch (Main.rand.Next(5))
			{
				case 0: 
					player.QuickSpawnItem(mod.ItemType("Fist_of_the_Hallow_Ent"), 1);
					break;
				case 1: 
					player.QuickSpawnItem(mod.ItemType("ForestBlast"), 1);
					break;
				case 2:
					player.QuickSpawnItem(mod.ItemType("GhastlyKnife"), Main.rand.Next(403, 508));
					break;
				case 3:
					player.QuickSpawnItem(mod.ItemType("LeafScythe"), 1);
					break;
				case 4:
					player.QuickSpawnItem(mod.ItemType("LivingTreeSword"), 1);
					break;
				default:
					break;
			}
			
			if (Main.rand.Next(7) == 0)
			{
				player.QuickSpawnItem(mod.ItemType("GhastlyMask"), 1);
			}
			
				
		}
	}
}