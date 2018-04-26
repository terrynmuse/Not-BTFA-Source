using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace ForgottenMemories.Projectiles.StarjinxWep
{
    public class AsterismGreen: ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Asterism Heatray");
        } 

        public override void SetDefaults()
        {
			projectile.width = 4;
			projectile.height = 4;
			projectile.aiStyle = 1;
			aiType = ProjectileID.Bullet;
			projectile.friendly = true;
			projectile.scale = 0f;
			projectile.timeLeft = 900;
			projectile.ranged = true;
			projectile.tileCollide = true;
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
			    Main.dust[index2].scale = (float) Main.rand.Next(50, 50) * 0.013f;
                Main.dust[index2].velocity *= 0.0f;
                Main.dust[index2].noGravity = true;
            }
        }
		
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item98, projectile.position);
		}
    }
}