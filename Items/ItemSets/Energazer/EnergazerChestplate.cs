using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ForgottenMemories.Items.ItemSets.Energazer
{
	[AutoloadEquip(EquipType.Body)]

    public class EnergazerChestplate : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 18;
            item.height = 18;

            item.value = 36000;
            item.rare = 2;
            item.defense = 7;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Energazer Armatura");
			Tooltip.SetDefault("Increases maximum mana by 40\n4% increased magic damage");
			//WhirlingWorldsGlowmask.AddGlowMask(item.type, "WhirlingWorlds/Items/Sets/Energazer_Set/Energazer_Armor/Energazer_Chestplate_Glow");
		}
		
        public override void UpdateEquip(Player player)
        {
			player.magicDamage += 0.04f;
			player.statManaMax2 += 40;
        }
		public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
        {
            glowMaskColor = Color.White;
        }
		public override void AddRecipes()
		{
		    ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "EnergyRemnant", 7);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}
