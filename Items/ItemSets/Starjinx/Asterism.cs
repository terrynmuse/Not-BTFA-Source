using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;

namespace ForgottenMemories.Items.ItemSets.Starjinx
{
	public class Asterism : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Asterism");
			Tooltip.SetDefault("Uses starjinx pebbles as ammunition\nThree round burst\nAll shots consume ammo");
			BTFAGlowmask.AddGlowMask(item.type, "ForgottenMemories/Items/ItemSets/Starjinx/AsterismGlow");
		}

		public override void SetDefaults()
		{
			item.damage = 14;
			item.ranged = true;
			item.width = 40;
			item.height = 20;
			item.useTime = 12;
			item.reuseDelay = 20;
			item.useAnimation = 36;
			item.useStyle = 5;
			item.noMelee = true; 
			item.knockBack = 4;
			item.value = 12000;
			item.rare = 2;
			item.UseSound = SoundID.Item41;
			item.autoReuse = true;
			item.shoot = 10;
			item.shootSpeed = 13f;
			item.useAmmo = mod.ItemType("StarjinxPebble");
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/ItemSets/Starjinx/AsterismGlow"),
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
			int Type;
				switch (Main.rand.Next(3))
				{
					case 0:
						Type = mod.ProjectileType("AsterismYellow");
						break;
					case 1:
						Type = mod.ProjectileType("AsterismRed");
						break;
					case 2:
						Type = mod.ProjectileType("AsterismGreen");
						break;
					default:
						return true;
						break;
				}
				type = Type;
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 40f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			return true;
		}
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(3, -4);
		}
		public override void AddRecipes()
		{
		    ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "AuroraBowl", 5);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
	}
}
