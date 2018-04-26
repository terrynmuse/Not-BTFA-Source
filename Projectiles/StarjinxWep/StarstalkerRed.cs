using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace ForgottenMemories.Projectiles.StarjinxWep
{
    public class StarstalkerRed: ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Red Starstalker Shard");
        } 
        public override void SetDefaults()
        {
			projectile.width = 6;
			projectile.height = 6;
			projectile.aiStyle = 24;
			projectile.friendly = true;
			projectile.scale = 1.2f;
			projectile.timeLeft = 600;
			projectile.penetrate = -1;
			projectile.extraUpdates = 1;
			projectile.tileCollide = false;
        }
		public override void AI()
        {
			for (int index1 = 0; index1 < 5; ++index1)
            {
                float x = projectile.position.X - projectile.velocity.X / 10f * (float) index1;
                float y = projectile.position.Y - projectile.velocity.Y / 10f * (float) index1;
                int index2 = Dust.NewDust(new Vector2(x, y), 1, 1, 60, 0.0f, 0.0f, 0, new Color(), 1f);
                Main.dust[index2].alpha = projectile.alpha;
                Main.dust[index2].position.X = x;
                Main.dust[index2].position.Y = y;
			    Main.dust[index2].scale = (float) Main.rand.Next(100, 100) * 0.013f;
                Main.dust[index2].velocity *= 0.1f;
                Main.dust[index2].noGravity = true;
            }
        }
    }
}