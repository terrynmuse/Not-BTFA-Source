using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;

namespace ForgottenMemories.Items.ItemSets.Starjinx
{
	public class Starstalker : ModItem
	{
		public override void SetDefaults()
		{
			item.value = 12000;
			item.useStyle = 5;
			item.useAnimation = 28;
			item.useTime = 28;
			item.rare = 2;
			item.scale = 1f;
			item.width = 20;
			item.height = 20;
			item.crit = 20;
			item.UseSound = SoundID.Item5;
			item.autoReuse = true;
			item.damage = 10;
			item.knockBack = 3f;
			item.shoot = 1;
			item.shootSpeed = 10f;
			item.noMelee = true;
			item.ranged = true;
			item.useAmmo = AmmoID.Arrow;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starstalker");
			Tooltip.SetDefault("Arrows turn into detonating starstalker arrows");
			BTFAGlowmask.AddGlowMask(item.type, "ForgottenMemories/Items/ItemSets/Starjinx/StarstalkerGlow");
		}
		
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/ItemSets/Starjinx/StarstalkerGlow"),
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
			if (type == ProjectileID.WoodenArrowFriendly)
			{
				type = mod.ProjectileType("StarstalkerArrow");
			}
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 40f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
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
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-1, 0	);
		}
	}
}
