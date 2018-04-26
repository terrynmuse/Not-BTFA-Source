using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace ForgottenMemories.Projectiles.Energazer
{
    public class EnergyArmorVisual: ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Energazer Armor Visual");
        } 
        public override void SetDefaults()
        {
			projectile.width = 20;
			projectile.height = 46;
			projectile.aiStyle = 1;
			projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
        }
		public override void AI()
        {		
			Player player = Main.player[projectile.owner];
			projectile.velocity.X = 0f;
			projectile.velocity.Y = 0f;
			projectile.position.X = player.position.X;
			projectile.position.Y = player.position.Y;
			if (player.setBonus == ("Enemies within 10 block range deal 15% decreased damage") || player.setBonus == ("Enemies within 10 block range take 15% increased damage") && player.active)
			{
				projectile.active = true;
			}
			else
			{
				projectile.Kill();
			}
					float num2 = 60f;
					
					++projectile.ai[1];
					
					float num3 = projectile.ai[1] / num2;
					Vector2 spinningpoint = new Vector2(0.0f, -30f);
					spinningpoint = spinningpoint.RotatedBy((double) num3 * 1.5 * 6.28318548202515, new Vector2()) * new Vector2(1f, 0.4f);
					for (int index1 = 0; index1 < 4; ++index1)
					{
						Vector2 vector2 = Vector2.Zero;
						float num4 = 1f;
						if (index1 == 0)
						{
							vector2 = Vector2.UnitY * -15f;
							num4 = 0.6f;
						}
						if (index1 == 1)
						{
							vector2 = Vector2.UnitY * -5f;
							num4 = 0.9f;
						}
						if (index1 == 2)
						{
							vector2 = Vector2.UnitY * 5f;
							num4 = 0.9f;
						}
						if (index1 == 3)
						{
							vector2 = Vector2.UnitY * 20f;
							num4 = 0.6f;
						}
						int index2 = Dust.NewDust(projectile.Center, 0, 0, 111, 0.0f, 0.0f, 100, new Color(), 0.5f);
						Main.dust[index2].noGravity = true;
						Main.dust[index2].position = projectile.Center + spinningpoint * num4 + vector2;
						Main.dust[index2].velocity = Vector2.Zero;
						spinningpoint *= -1f;
						int index3 = Dust.NewDust(projectile.Center, 0, 0, 111, 0.0f, 0.0f, 100, new Color(), 0.5f);
						Main.dust[index3].noGravity = true;
						Main.dust[index3].position = projectile.Center + spinningpoint * num4 + vector2;
						Main.dust[index3].velocity = Vector2.Zero;
					}
			
        }
		public override void Kill(int timeLeft)
        {
			Player player = Main.player[projectile.owner];
			//Main.PlaySound(new Terraria.Audio.LegacySoundStyle(42, 31));
		}
    }
}