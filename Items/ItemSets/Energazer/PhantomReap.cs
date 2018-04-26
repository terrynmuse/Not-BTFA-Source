using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
 
namespace ForgottenMemories.Items.ItemSets.Energazer
{
    public class PhantomReap : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 12;
            item.melee = true;
            item.width = 36;
            item.height = 36;
            item.useTime = 34;
            item.useAnimation = 34;
            item.useStyle = 1;
			item.scale = 1f;
            item.knockBack = 5f;
            item.value = 12000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phantom Reap");
			Tooltip.SetDefault("Swinging speed will increase over time\nKilling enemies might cause them to drop their soul\nAbsorbing their souls increases your attack and movement speed slightly");
		}
		public override bool UseItem(Player player)
        {
			if (item.useTime >= 10 && item.useAnimation >= 10 )
			{
				item.useTime -= 2;
				item.useAnimation -= 2;
			}
			else
			{
				item.useTime = 34;
				item.useAnimation = 34;
			}
		    return true;
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
