using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace ForgottenMemories.Projectiles.Starjinx
{
    public class GreenBlast: ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Green Blast");
        } 
        public override void SetDefaults()
        {
			projectile.width = 4;
			projectile.height = 4;
			projectile.aiStyle = 1;
			aiType = ProjectileID.Bullet;
			projectile.hostile = true;
			projectile.friendly = false;
			projectile.scale = 0f;
			projectile.timeLeft = 60;
			projectile.tileCollide = false;
        }
		
		public override void AI()
        {
			for (int index1 = 0; index1 < 10; ++index1)
            {
                float x = projectile.position.X - projectile.velocity.X / 10f * (float) index1;
                float y = projectile.position.Y - projectile.velocity.Y / 10f * (float) index1;
                int index2 = Dust.NewDust(new Vector2(x, y), 1, 1, 61, 0.0f, 0.0f, 0, new Color(), 1f);
                Main.dust[index2].alpha = projectile.alpha;
                Main.dust[index2].position.X = x;
                Main.dust[index2].position.Y = y;
			    Main.dust[index2].scale = (float) Main.rand.Next(60, 60) * 0.013f;
                Main.dust[index2].velocity *= 0.0f;
                Main.dust[index2].noGravity = true;
                Main.dust[index2].noLight = true;
            }
        }

		public override void OnHitPlayer (Player target, int damage, bool crit)
		{
			int num2 = Main.rand.Next(20, 40);
            for (int index1 = 0; index1 < num2; ++index1)
            {
                int index2 = Dust.NewDust(projectile.Center, 0, 0, 61, 0.0f, 0.0f, 100, new Color(), 0.8f);
                Main.dust[index2].velocity *= 1.2f;
                --Main.dust[index2].velocity.Y;
                Main.dust[index2].velocity += projectile.velocity;
                Main.dust[index2].noGravity = true;
            }
			projectile.Kill();
		}

		public override void Kill(int timeLeft)
		{
			int num2 = Main.rand.Next(20, 40);
            for (int index1 = 0; index1 < num2; ++index1)
            {
                int index2 = Dust.NewDust(projectile.Center, 0, 0, 61, 0.0f, 0.0f, 100, new Color(), 0.8f);
                Main.dust[index2].velocity *= 1.2f;
                --Main.dust[index2].velocity.Y;
                Main.dust[index2].velocity += projectile.velocity;
                Main.dust[index2].noGravity = true;
                Main.dust[index2].noLight = true;
				Main.PlaySound(SoundID.Item94, projectile.position);
            }
		}
    }
}