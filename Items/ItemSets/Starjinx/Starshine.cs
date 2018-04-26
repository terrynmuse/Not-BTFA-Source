using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
 
namespace ForgottenMemories.Items.ItemSets.Starjinx
{
    public class Starshine : ModItem
    {
		public override void SetDefaults()
		{
			item.damage = 12;
			item.noMelee = true;
			item.noUseGraphic = false;
			item.magic = true;
			item.width = 30;
			item.height = 30;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 5;
			item.shoot = mod.ProjectileType("StarshineYellow");
			item.knockBack = 0f;
			Item.staff[item.type] = true;
			item.autoReuse = true;
			item.rare = 2;
			item.value = 12000;
			item.UseSound = SoundID.Item43;
			item.useTurn = true;
			item.mana = 8;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starshine");
			Tooltip.SetDefault("Nearby enemies will explode with starshards");
			BTFAGlowmask.AddGlowMask(item.type, "ForgottenMemories/Items/ItemSets/Starjinx/StarshineGlow");
		}
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/ItemSets/Starjinx/StarshineGlow"),
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

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			switch (Main.rand.Next(3))
			{
				case 0:
					item.shoot = mod.ProjectileType("StarshineYellow");
					break;
				case 1:
					item.shoot = mod.ProjectileType("StarshineGreen");
					break;
				case 2:
					item.shoot = mod.ProjectileType("StarshineRed");
					break;
				default:
					return true;
					break;
			}
            return true;
        }
		public override void AddRecipes()
		{
		    ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "AuroraBowl", 7);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}
