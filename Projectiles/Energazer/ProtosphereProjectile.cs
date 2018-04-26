using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace ForgottenMemories.Projectiles.Energazer
{
    public class ProtosphereProjectile: ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Protosphere Sphere");
        } 
        public override void SetDefaults()
        {
			projectile.width = 18;
			projectile.height = 12;
			projectile.aiStyle = 1;
			aiType = ProjectileID.Bullet;
			projectile.scale = 1f;
			projectile.alpha = 200;
			projectile.tileCollide = false;
			Main.projFrames[projectile.type] = 5;
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
				if (projectile.frame > 4)
				{
					projectile.frame = 0;
				}
			}
			Lighting.AddLight(projectile.Center, 0.153f, 0.204f, 0.255f);
			projectile.velocity.X = 0f;
			projectile.velocity.Y = 0f;
			Player player = Main.player[projectile.owner];
			if (Main.player[projectile.owner].HeldItem.type == mod.ItemType("Protosphere") && player.active)
			{
				projectile.active = true;
			}
			else
			{
				projectile.Kill();
			}
        }
		public override void Kill(int timeLeft)
        {
			Player player = Main.player[projectile.owner];
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(42, 31));
			player.GetModPlayer<BTFAPlayer>().liberMortisCount = 1;
			int num23 = 36;	
				for (int index1 = 0; index1 < num23; ++index1)
				{
					Vector2 vector2_3 = (new Vector2((float) projectile.width, (float) projectile.height) * 0.75f * 0.5f).RotatedBy((double) (index1 - (num23 / 2 - 1)) * 6.28318548202515 / (double) num23, new Vector2()) + projectile.Center;
					Vector2 vector2_4 = vector2_3 - projectile.Center;
					int index2 = Dust.NewDust(vector2_3 + vector2_4, 0, 0, 111, vector2_4.X * 2f, vector2_4.Y * 2f, 100, new Color(), 1.4f);
					Main.dust[index2].noGravity = true;
					Main.dust[index2].noLight = false;
					Main.dust[index2].scale = 2f;
					Main.dust[index2].velocity = Vector2.Normalize(vector2_4) * 5f;
				}
				
				for (int index = 0; index < 3; ++index)
				{
					float SpeedX = (float) (-(double) projectile.velocity.X * (double) Main.rand.Next(40, 70) * 0.00999999977648258 + (double) Main.rand.Next(-20, 21) * 0.400000005960464);
					float SpeedY = (float) (-(double) projectile.velocity.Y * (double) Main.rand.Next(40, 70) * 0.00999999977648258 + (double) Main.rand.Next(-20, 21) * 0.400000005960464);
					Projectile.NewProjectile(projectile.position.X + SpeedX, projectile.position.Y + SpeedY, SpeedX, SpeedY, mod.ProjectileType("ProtosphereHoming"), projectile.damage, 0.0f, projectile.owner, 0.0f, 0.0f);
				}
		}
    }
}