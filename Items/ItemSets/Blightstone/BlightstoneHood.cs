using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ForgottenMemories.Items.ItemSets.Blightstone
{
	[AutoloadEquip(EquipType.Head)]
	public class BlightstoneHood : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 18;
			item.height = 18;
			item.value = Item.sellPrice(0, 6, 0, 0);
			item.rare = 7;
			item.defense = 5;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Blightstone Hood");
			Tooltip.SetDefault("15% increased summon and magic damage\nMax minions increased by 2 and increases maximum mana by 80");
		}

		public override bool IsArmorSet(Item head, Item body, Item legs)
		{
			return body.type == mod.ItemType("VeilstoneBreastplate") && legs.type == mod.ItemType("VeilstoneGreaves");
		}
		
		public override bool DrawHead()
		{
			return false;
		}

		public override void UpdateEquip(Player player)
		{
			player.minionDamage += 0.15f;
			player.magicDamage += 0.15f;
			player.maxMinions += 2;
			player.statManaMax2 += 80;
		}

		public override void UpdateArmorSet(Player player)
		{
			player.setBonus = "Summons an orb of blighted energy that damages nearby enemies";
			((BTFAPlayer)player.GetModPlayer(mod, "BTFAPlayer")).BlightOrb = true;
		}
		
		public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "blight_bar", 12);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
