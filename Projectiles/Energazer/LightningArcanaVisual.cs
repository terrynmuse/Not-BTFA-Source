using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace ForgottenMemories.Projectiles.Energazer
{
    public class LightningArcanaVisual: ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Arcana");
        } 

        public override void SetDefaults()
        {
			projectile.width = 4;
			projectile.height = 4;
			projectile.aiStyle = 1;
			aiType = ProjectileID.Bullet;
			projectile.scale = 1f;
			projectile.tileCollide = false;
			Main.projFrames[projectile.type] = 15;
        }

		public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }

		public override void AI()
        {
			{         	
				projectile.frameCounter++;
			
				if (projectile.frameCounter > 4)
				{
					projectile.frame++;
					projectile.frameCounter = 1;
				}
				if (projectile.frame > 14)
				{
					projectile.frame = 0;
				}
			}
			
			Lighting.AddLight(projectile.Center, 0.153f, 0.204f, 0.255f);
			
			Player player = Main.player[projectile.owner];
			if (Main.player[projectile.owner].HeldItem.type == mod.ItemType("LightningArcana") && player.active)
			{
				projectile.active = true;
			}
			else
			{
				projectile.active = false;
			}
			
			for(int i = 0; i < Main.npc.Length; i++)
			{
				NPC npc = Main.npc[i];
				if (npc.active && player.active && !npc.friendly && npc.damage >= 1 && (double) Vector2.Distance(player.Center, npc.Center) <= (double) 250f)
				{
					if (Main.rand.Next(150) == 0)
					{
						int manaAmount = Main.rand.Next(1, 5);
						player.statMana += manaAmount;
						if (player.statMana > player.statManaMax2)
							player.statMana = player.statManaMax2;
						player.ManaEffect(manaAmount);
					}
					for (int index1 = 0; index1 < 3; ++index1)
					{
						float num8 = npc.velocity.X * 0.334f * (float) index1;
						float num9 = (float) -((double) npc.velocity.Y * 0.333999991416931) * (float) index1;
						int index2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 111, 0.0f, 0.0f, 100, new Color(), 1.1f);
						Main.dust[index2].noGravity = true;
						Main.dust[index2].velocity *= 0.0f;
						Main.dust[index2].position.X -= num8;
						Main.dust[index2].position.Y -= num9;
					}
				}
			}
			
			if (player.direction == -1)
				projectile.position.X = player.position.X - 20;
			else if (player.direction == 1)
				projectile.position.X = player.position.X + 36;
			projectile.position.Y = player.MountedCenter.Y;
        }
    }
}