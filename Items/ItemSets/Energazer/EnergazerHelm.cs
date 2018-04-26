using System.Collections.Generic;
using Terraria;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ForgottenMemories.Items.ItemSets.Energazer
{
	[AutoloadEquip(EquipType.Head)]
	
    public class EnergazerHelm : ModItem
    {
		protected bool hasTheVisual = false;

        public override void SetDefaults()
        {
            item.width = 26;
            item.height = 26;
            item.value = 12000;
            item.rare = 2;
            item.defense = 4;
        }

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Energazer Cassis");
			Tooltip.SetDefault("10% increased weapon haste\n5% increased magic damage");
			//WhirlingWorldsGlowmask.AddGlowMask(item.type, "WhirlingWorlds/Items/Sets/Energazer_Set/Energazer_Armor/Energazer_Helm_Glow");
		}
        public override void UpdateEquip(Player player)
        {
			player.GetModPlayer<BTFAPlayer>().energazerHelm = true;
			player.magicDamage += 0.05f;
		}
		public override bool IsArmorSet(Item head, Item body, Item legs)
        {
            return body.type == mod.ItemType("EnergazerChestplate") && legs.type == mod.ItemType("EnergazerLeggings");
        }
		public override void DrawArmorColor(Player drawPlayer, float shadow, ref Color color, ref int glowMask, ref Color glowMaskColor)
        {
            glowMaskColor = Color.White;
        }
		public override void UpdateArmorSet(Player player)
        {
			player.setBonus = ("Enemies within 10 block range take 15% increased damage");
			/*if ((double) player.velocity.X != 0.0 && (double) player.velocity.Y == 0.0 && player.miscCounter % 2 == 0)
			{
				for (int index1 = 0; index1 < 2; ++index1)
				{
					int index2 = index1 != 0 ? Dust.NewDust(new Vector2(player.position.X + (float) (player.width / 2), player.position.Y + (float) player.height + player.gfxOffY), player.width / 2, 6, 111, 0.0f, 0.0f, 0, new Color(), 1.35f) : Dust.NewDust(new Vector2(player.position.X, player.position.Y + (float) player.height + player.gfxOffY), player.width / 2, 6, 111, 0.0f, 0.0f, 0, new Color(), 1.35f);
					Main.dust[index2].scale = 1f;
					Main.dust[index2].noGravity = true;
					Main.dust[index2].noLight = true;
					Main.dust[index2].velocity *= 1f / 1000f;
					Main.dust[index2].velocity.Y -= 3f / 1000f;
				}
			}*/
			for(int k = 0; k < Main.projectile.Length; k++)
			{
				int type2 = Main.projectile[k].type;
				if(type2 != mod.ProjectileType("EnergyArmorVisual") && !Main.projectile[k].active)
				{
					hasTheVisual = false;
				}
				else if(type2 == mod.ProjectileType("EnergyArmorVisual") && Main.projectile[k].active)
				{
					hasTheVisual = true;
					break;
				}
			}
			
			if(!hasTheVisual)
			{
				Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("EnergyArmorVisual"), 0, 0, player.whoAmI);
			}
        }
		public override void DrawHair(ref bool drawHair, ref bool drawAltHair)
        {
            drawHair = drawAltHair = false;  
        }
		public override bool DrawHead()
        {
            return false;   
        }
		public override void AddRecipes()
		{
		    ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(null, "EnergyRemnant", 5);
            recipe.AddTile(TileID.Anvils);
			recipe.SetResult(this);
			recipe.AddRecipe();
        }
    }
}
