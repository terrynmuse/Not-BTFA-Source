using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
 
namespace ForgottenMemories.Items.ItemSets.Starjinx
{
    public class Dawnriser : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 14;
            item.melee = true;
            item.width = 36;
            item.height = 36;
            item.useTime = 20;
            item.useAnimation = 20;
            item.useStyle = 1;
            item.knockBack = 3.5f;
            item.value = 12000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Dawnriser");
			Tooltip.SetDefault("Right Click increases the weapon's hitbox and has more damage and knockback but decreases swing speed");
			BTFAGlowmask.AddGlowMask(item.type, "ForgottenMemories/Items/ItemSets/Starjinx/DawnriserGlow");
		}
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				item.damage = 24;
				item.melee = true;
				item.width = 60;
				item.height = 60;
				item.useTime = 31;
				item.useAnimation = 31;
				item.useStyle = 1;
				item.knockBack = 8f;
				item.value = 12000;
				item.rare = 2;
				item.UseSound = SoundID.Item1;
				item.autoReuse = false;
				item.useTurn = true;
			}
			else
			{
				item.damage = 14;
				item.melee = true;
				item.width = 36;
				item.height = 36;
				item.useTime = 20;
				item.useAnimation = 20;
				item.useStyle = 1;
				item.knockBack = 3.5f;
				item.value = 12000;
				item.rare = 2;
				item.UseSound = SoundID.Item1;
				item.autoReuse = true;
				item.useTurn = true;
			}
			return base.CanUseItem(player);
		}
		public override void UseItemHitbox(Player player, ref Rectangle hitbox, ref bool noHitbox) 
		{
			if (player.altFunctionUse == 2)
			{
				hitbox.Width = 120;
                hitbox.Height = 120;
                hitbox.X = (int)player.Center.X - 50 * player.direction;
                if (player.gravDir > 0) { hitbox.Y = (int)player.Bottom.Y + 16 - hitbox.Height; }
                else { hitbox.Y = (int)player.Top.Y - 16; }
                if (player.direction < 0) hitbox.X -= hitbox.Width;
			}
        }
		public override void UseStyle(Player player)
        {
			if (player.altFunctionUse == 2)
			{
				int Type;
				switch (Main.rand.Next(3))
				{
					case 0:
						Type = 60;
						break;
					case 1:
						Type = 61;
						break;
					case 2:
						Type = 64;
						break;
					default:
						Type = 60;
						break;
				}
				float cosRot = (float)Math.Cos(player.itemRotation - 0.78f * player.direction * player.gravDir);
				float sinRot = (float)Math.Sin(player.itemRotation - 0.78f * player.direction * player.gravDir);
				for (int i = 0; i < 8; i++)
				{
					float length = (item.width * 1.2f - i * item.width/9) * item.scale + 32;
					int dust = Dust.NewDust(
						new Vector2(
						(float)(player.itemLocation.X + length * cosRot * player.direction),
						(float)(player.itemLocation.Y + length * sinRot * player.direction)), 
						0, 0, Type,
						player.velocity.X * 0.9f,
						player.velocity.Y * 0.9f, 
						100, 
						Color.Transparent, 
						1.5f);
					Main.dust[dust].velocity *= 0.3f;
					Main.dust[dust].noGravity = true;
				}
			}
        }
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/ItemSets/Starjinx/DawnriserGlow"),
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
			recipe.AddIngredient(null, "AuroraBowl", 7);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}
