using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;

namespace ForgottenMemories.Items.ItemSets.Energazer
{
	public class Accumulator : ModItem
	{
		protected static int arrowAmount = 1;
		public override void SetDefaults()
		{
			item.value = 12000;
			item.useStyle = 5;
			item.useAnimation = 30;
			item.useTime = 30;
			item.rare = 2;
			item.scale = 1f;
			item.width = 20;
			item.height = 20;
			item.UseSound = new Terraria.Audio.LegacySoundStyle(2, 102);
			item.autoReuse = true;
			item.damage = 8;
			item.knockBack = 2.5f;
			item.shoot = mod.ProjectileType("EnergyArrow");
			item.shootSpeed = 7f;
			item.noMelee = true;
			item.ranged = true;
			item.mana = 4;
		}
		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2)
			{
				item.value = 12000;
				item.useStyle = 5;
				item.noUseGraphic = false;
				item.useAnimation = 30;
				item.useTime = 30;
				item.rare = 2;
				item.scale = 1f;
				item.width = 20;
				item.height = 20;
				item.UseSound = new Terraria.Audio.LegacySoundStyle(42, 228);
				item.autoReuse = true;
				item.damage = 12;
				item.knockBack = 2.5f;
				item.shoot = mod.ProjectileType("This_is_a_placeHolder");
				item.shootSpeed = 12f;
				item.noMelee = true;
				item.ranged = true;
				item.mana = 0;   			
			}
			else
			{
				item.noUseGraphic = false;
				item.value = 12000;
				item.useStyle = 5;
				item.useAnimation = 30;
				item.useTime = 30;
				item.rare = 2;
				item.scale = 1f;
				item.width = 20;
				item.height = 20;
				item.UseSound = new Terraria.Audio.LegacySoundStyle(2, 102);
				item.autoReuse = true;
				item.damage = 12;
				item.knockBack = 0f;
				item.shoot = mod.ProjectileType("EnergyArrow");
				item.shootSpeed = 12f;
				item.noMelee = true;
				item.ranged = true;
				item.mana = 4 * arrowAmount;
			}
			return base.CanUseItem(player);
		}
		public override bool AltFunctionUse(Player player)
		{
			arrowAmount+= 1;
			if (arrowAmount >= 6)
			{
				arrowAmount = 1;
			}
			return true;
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Accumulator");
			Tooltip.SetDefault("Right Click to increase amount of arrows you shoot\nMana cost increases as arrows increase");
		}
		public override void ModifyTooltips(List<TooltipLine> tooltips)
		{
			TooltipLine line = new TooltipLine(mod, "Styx", "You will shoot " + arrowAmount + " arrow(s)");
			line.overrideColor = new Color(255, 255, 255);
			tooltips.Add(line);
		}
		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 40f;
			if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
			{
				position += muzzleOffset;
			}
			
			if (arrowAmount == 1)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("EnergyArrow"), item.damage, item.knockBack, player.whoAmI);
				return false;
			}
			else if (arrowAmount == 2)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("EnergyArrow"), item.damage, item.knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX / 1.2f, speedY, mod.ProjectileType("EnergyArrow"), item.damage, item.knockBack, player.whoAmI);
				return false;
			}
			else if (arrowAmount == 3)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("EnergyArrow"), item.damage, item.knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX / 1.2f, speedY, mod.ProjectileType("EnergyArrow"), item.damage, item.knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX / 1.4f, speedY, mod.ProjectileType("EnergyArrow"), item.damage, item.knockBack, player.whoAmI);
				return false;
			}
			else if (arrowAmount == 4)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("EnergyArrow"), item.damage, item.knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX / 1.2f, speedY, mod.ProjectileType("EnergyArrow"), item.damage, item.knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX / 1.4f, speedY, mod.ProjectileType("EnergyArrow"), item.damage, item.knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX / 1.6f, speedY, mod.ProjectileType("EnergyArrow"), item.damage, item.knockBack, player.whoAmI);
				return false;
			}
			else if (arrowAmount == 5)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("EnergyArrow"), item.damage, item.knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX / 1.2f, speedY, mod.ProjectileType("EnergyArrow"), item.damage, item.knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX / 1.4f, speedY, mod.ProjectileType("EnergyArrow"), item.damage, item.knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX / 1.6f, speedY, mod.ProjectileType("EnergyArrow"), item.damage, item.knockBack, player.whoAmI);
				Projectile.NewProjectile(position.X, position.Y, speedX / 1.8f, speedY, mod.ProjectileType("EnergyArrow"), item.damage, item.knockBack, player.whoAmI);
				return false;
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
		
		public override Vector2? HoldoutOffset()
		{
			return new Vector2(-5, 0);
		}
	}
}
