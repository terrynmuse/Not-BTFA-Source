using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ForgottenMemories.Items.ItemSets.Energazer
{
	[AutoloadEquip(EquipType.Legs)]
    public class EnergazerLeggings : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;
            item.value = 24000;
            item.rare = 2;
            item.defense = 5;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Energazer Ocrearum");
			Tooltip.SetDefault("Increases maximum mana by 30\n5% increased magic damage");
		}
        public override void UpdateEquip(Player player)
        {
			player.magicDamage += 0.05f;
			player.statManaMax2 += 30;
        }
		public override void AddRecipes()
		{
		    ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "EnergyRemnant", 6);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}
