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
    public class Protosphere : ModItem
    {
		public override void SetDefaults()
		{
			item.damage = 15;
			item.noMelee = true;
			item.noUseGraphic = false;
			item.magic = true;
			item.width = 30;
			item.height = 30;
			item.useTime = 30;
			item.useAnimation = 30;
			item.useStyle = 5;
			item.shoot = mod.ProjectileType("ProtosphereProjectile");
			item.shootSpeed = 8f;
			item.knockBack = 2f;
			item.autoReuse = true;
			item.rare = 2;
			item.value = 12000;
			item.UseSound = new Terraria.Audio.LegacySoundStyle(42, 19);
			item.useTurn = true;
			item.mana = 8;
		}
		public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)	
        {
			player.GetModPlayer<BTFAPlayer>().liberMortisCount += 1;
            Vector2 POZISYON = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY);
			position = POZISYON;
			Projectile.NewProjectile(Main.screenPosition.X + Main.mouseX + 52, Main.screenPosition.Y + Main.mouseY + 1, 0f, 0f, mod.ProjectileType("ProtosphereProjectileVisual"), item.damage, item.knockBack, player.whoAmI);
            return true;			
        }
		public override bool UseItem(Player player)
        {
			player.GetModPlayer<BTFAPlayer>().liberMortisCount += 1;
		    return true;
		}
		public override bool CanUseItem(Player player)
		{
			if (player.GetModPlayer<BTFAPlayer>().liberMortisCount > 2)
            { 
                return false;
            }
            return true;
		}
		public override bool AltFunctionUse(Player player)
		{
			for (int k = 0; k < Main.projectile.Length; k++)
            { 
		        Projectile proj = Main.projectile[k];
				if (proj.active && proj.type == mod.ProjectileType("ProtosphereProjectile") && proj.owner == player.whoAmI)
                { 
                    proj.Kill();
					player.GetModPlayer<BTFAPlayer>().liberMortisCount = 1;
                }
				if (proj.active && proj.type == mod.ProjectileType("ProtosphereProjectileVisual") && proj.owner == player.whoAmI)
                { 
                    proj.Kill();
					player.GetModPlayer<BTFAPlayer>().liberMortisCount = 1;
                }
            }
            return false;
		}
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Protosphere");
			Tooltip.SetDefault("Creates a static orb on your cursor\nOrbs will explode upon death\nYou can create 2 orbs maximum\nRight Click to destroy orbs");
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
