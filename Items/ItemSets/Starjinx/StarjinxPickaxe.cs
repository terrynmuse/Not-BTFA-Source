using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;

namespace ForgottenMemories.Items.ItemSets.Starjinx
{
    public class StarjinxPickaxe : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 8;
            item.melee = true;
            item.width = 20;
            item.height = 12;
			item.hammer = 0;
            item.useTime = 16;
            item.useAnimation = 16;
            item.pick = 59;
			item.axe = 14;
            item.useStyle = 1;
            item.knockBack = 2.3f;
            item.value = 12000;
            item.rare = 2;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.useTurn = true;
        }
		public override bool AltFunctionUse(Player player)
		{
			return true;
		}
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				item.damage = 8;
				item.melee = true;
				item.width = 36;
				item.height = 36;
				item.useTime = 8;
				item.useAnimation = 16;
				item.pick = 0;
				item.axe = 0;
				item.hammer = 55; 
				item.useStyle = 1;
				item.knockBack = 6.3f;
				item.value = 12000;
				item.rare = 2;
				item.UseSound = SoundID.Item1;
				item.autoReuse = true;
				item.useTurn = true;
			}
			else
			{
				item.damage = 8;
				item.melee = true;
				item.width = 20;
				item.height = 12;
				item.hammer = 0;
				item.useTime = 16;
				item.useAnimation = 16;
				item.pick = 59;
				item.axe = 14;
				item.useStyle = 1;
				item.knockBack = 2.3f;
				item.value = 12000;
				item.rare = 2;
				item.UseSound = SoundID.Item1;
				item.autoReuse = true;
				item.useTurn = true;
			}
			for (int i = 0; i < 1000; ++i)
            {
                if (Main.projectile[i].active && Main.projectile[i].owner == Main.myPlayer && Main.projectile[i].type == mod.ProjectileType("PikeTotem"))
                {
                    return false;
                }
            }		
			return base.CanUseItem(player);
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starjinx Pickaxe");
			Tooltip.SetDefault("Can mine Meteorite\nRight Click functions as a hammer");
			BTFAGlowmask.AddGlowMask(item.type, "ForgottenMemories/Items/ItemSets/Starjinx/StarjinxPickaxeGlow");
		}
		
		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float  scale, int whoAmI) 	
		{
			Texture2D texture;
			texture = Main.itemTexture[item.type];
			spriteBatch.Draw
			(
				mod.GetTexture("Items/ItemSets/Starjinx/StarjinxPickaxeGlow"),
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
			recipe.AddIngredient(null, "AuroraBowl", 10);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}
