using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace ForgottenMemories.Items.ItemSets.Energazer 
{
    public class StaticShiv : ModItem
    {
		public override void SetDefaults()
		{
			item.damage = 11;
			item.noMelee = true;
			item.noUseGraphic = true;
			item.thrown = true;
			item.value = 12000;
			item.width = 20;
			item.height = 20;
			item.useTime = 22;
			item.useAnimation = 22;
			item.useStyle = 1;
			item.knockBack = 2.15f;
			item.rare = 2;
			item.UseSound = SoundID.Item7;
			item.autoReuse = true;
			item.shootSpeed = 7f;
			item.shoot = mod.ProjectileType("StaticShivProjectile");
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Static Shiv");
			Tooltip.SetDefault("Every third dagger will deal double damage\nYour damage will get boosted depending on your movement speed");
		}

		public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
		{
			player.GetModPlayer<BTFAPlayer>().StaticShivProjectileCount += 1;
			if (player.GetModPlayer<BTFAPlayer>().StaticShivProjectileCount >= 3)
			{
				Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("StaticShivProjectile"), item.damage * 2, 2f, player.whoAmI);
				player.GetModPlayer<BTFAPlayer>().StaticShivProjectileCount = 0;
				return false;
			}
			return true;
		}

		public override void GetWeaponDamage(Player player, ref int damage)
		{
			damage = Convert.ToInt32(damage + (double) Main.player[Main.myPlayer].velocity.Length() / 2);
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
