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
using Terraria;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
 
namespace ForgottenMemories.Items.ItemSets.Energazer
{
    public class LightningArcana : ModItem
    {
		public override void SetDefaults()
		{
			item.damage = 15;
			item.holdStyle = 1;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.magic = true;
			item.width = 30;
			item.height = 30;
			item.useTime = 24;
			item.useAnimation = 24;
			item.useStyle = 5;
			item.shoot = mod.ProjectileType("LightningArcanaProjectile");
			item.shootSpeed = 8f;
			item.knockBack = 2f;
			Item.staff[item.type] = true;
			item.autoReuse = true;
			item.rare = 2;
			item.value = 12000;
			item.UseSound = SoundID.Item125;
			item.useTurn = true;
			item.mana = 5;
		}
		public override void HoldItem (Player player)	
		{
			for(int i = 0; i < Main.projectile.Length; i++)
			{
				int type = Main.projectile[i].type;
				
				if(type != mod.ProjectileType("LightningArcanaVisual") && !Main.projectile[i].active)
				{
					player.GetModPlayer<BTFAPlayer>().hasLightningArcanaProjectile1 = false;
				}
				else if(type == mod.ProjectileType("LightningArcanaVisual") && Main.projectile[i].active)
				{
					player.GetModPlayer<BTFAPlayer>().hasLightningArcanaProjectile1 = true;
					break;
				}
			}
			
			for(int k = 0; k < Main.projectile.Length; k++)
			{
				int type2 = Main.projectile[k].type;
				if(type2 != mod.ProjectileType("LightningArcanaVisualDust") && !Main.projectile[k].active && player.GetModPlayer<BTFAPlayer>().navitasOrbisCounter == 1)
				{
					player.GetModPlayer<BTFAPlayer>().hasLightningArcanaProjectile2 = false;
				}
				else if(type2 == mod.ProjectileType("LightningArcanaVisualDust") && Main.projectile[k].active)
				{
					player.GetModPlayer<BTFAPlayer>().hasLightningArcanaProjectile2 = true;
					break;
				}
			}
			
			if(!player.GetModPlayer<BTFAPlayer>().hasLightningArcanaProjectile1)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("LightningArcanaVisual"), 0, 0, player.whoAmI);
			}
			if(!player.GetModPlayer<BTFAPlayer>().hasLightningArcanaProjectile2)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("LightningArcanaVisualDust"), 0, 0, player.whoAmI);
			}
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			int numberProjectiles = player.GetModPlayer<BTFAPlayer>().navitasOrbisCounter; 
			for (int i = 0; i < numberProjectiles; i++)
			{
				Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(6)); 
				float scale = 1.5f - (Main.rand.NextFloat() * .3f);
				perturbedSpeed = perturbedSpeed * scale; 
				Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("LightningArcanaProjectile"), item.damage, item.knockBack, player.whoAmI);
			}
			return false;
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Lightning Arcana");
			Tooltip.SetDefault("Holding the weapon will steal mana from nearby enemies\nHitting enemies will charge your weapon\nCharge effect can be stacked 4 times, every charge increases your maximum amount of projectiles by 1");
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
