using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;

namespace ForgottenMemories.Items.Ammo
{
	public class StarjinxPebble : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starjinx Pebble");
			Tooltip.SetDefault("Ammo required by Asterism");
			BTFAGlowmask.AddGlowMask(item.type, "ForgottenMemories/Items/Ammo/StarjinxPebbleGlow");
		}

		public override void SetDefaults()
		{
			item.damage = 5;
			item.ranged = true;
			item.width = 14;
			item.height = 14;
			item.maxStack = 999;
			item.consumable = true;
			item.knockBack = 1f;
			item.value = 15;
			item.rare = 2;
			item.ammo = item.type;
		}
		
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/Ammo/StarjinxPebbleGlow"),
				new Vector2
				(
					item.position.X - Main.screenPosition.X + item.width * 0.5f,
					item.position.Y - Main.screenPosition.Y + item.height - texture.Height * 0.5f + 2f
				),
				new Rectangle(0, 0, texture.Width, texture.Height),
				Color.White,
				rotation,
				texture.Size() * 0.5f,
				scale, 
				SpriteEffects.None, 
				0f
			);
		}
		
		public override void AddRecipes()
		{
		    ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "AuroraBowl", 1);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this, 150);
			recipe.AddRecipe();
        }
	}
}
