using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace ForgottenMemories.Projectiles.StarjinxWep
{
	public class StarstalkerArrow : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 4;
			projectile.height = 4;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.aiStyle = 1;
			projectile.ranged = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Starstalker Arrow");
		}
		
		public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
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
			int num2 = Main.rand.Next(20, 40);
            for (int index1 = 0; index1 < num2; ++index1)
            {
                int index2 = Dust.NewDust(projectile.Center, 0, 0, Type, 0.0f, 0.0f, 100, new Color(), 0.8f);
                Main.dust[index2].velocity *= 1.2f;
                --Main.dust[index2].velocity.Y;
                Main.dust[index2].velocity += projectile.velocity;
                Main.dust[index2].noGravity = true;
            }
		}
		
		public override void Kill(int timeLeft)
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
			Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y);
			int num2 = Main.rand.Next(20, 40);
            for (int index1 = 0; index1 < num2; ++index1)
            {
                int index2 = Dust.NewDust(projectile.Center, 0, 0, Type, 0.0f, 0.0f, 100, new Color(), 1.2f);
                Main.dust[index2].velocity *= 1.5f;
                --Main.dust[index2].velocity.Y;
                Main.dust[index2].velocity += projectile.velocity;
                Main.dust[index2].noGravity = true;
            }
			int num = Main.rand.Next(1, 1);
			int num1 = Main.rand.Next(1, 1);
			int number2 = Main.rand.Next(1, 1);
			for (int index = 0; index < num; ++index)
			{
				Vector2 vector2 = new Vector2((float) Main.rand.Next(-200, 200), (float) Main.rand.Next(-200, 200));
				vector2.Normalize();
				vector2 *= (float) Main.rand.Next(80, 201) * 0.01f;
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector2.X, vector2.Y, mod.ProjectileType("StarstalkerGreen"), 6, 1f, projectile.owner, 0.0f, (float) Main.rand.Next(-45, 1));
			}
			for (int index = 0; index < num1; ++index)
			{
				Vector2 vector2 = new Vector2((float) Main.rand.Next(-200, 200), (float) Main.rand.Next(-200, 200));
				vector2.Normalize();
				vector2 *= (float) Main.rand.Next(80, 201) * 0.01f;
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector2.X, vector2.Y, mod.ProjectileType("StarstalkerRed"), 6, 1f, projectile.owner, 0.0f, (float) Main.rand.Next(-45, 1));
			}
			for (int index = 0; index < number2; ++index)
			{
				Vector2 vector2 = new Vector2((float) Main.rand.Next(-200, 200), (float) Main.rand.Next(-200, 200));
				vector2.Normalize();
				vector2 *= (float) Main.rand.Next(80, 201) * 0.01f;
				Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector2.X, vector2.Y, mod.ProjectileType("StarstalkerYellow"), 6, 1f, projectile.owner, 0.0f, (float) Main.rand.Next(-45, 1));
			}
		}

		//public override void AI() {}
	}
}
