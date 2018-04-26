using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ForgottenMemories.Projectiles.Energazer  
{
    public class ProtosphereHoming : ModProjectile
    {
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Protosphere Shard");
        }
        public override void SetDefaults()
        {
            projectile.width = 4; 
            projectile.height = 4;  
            projectile.aiStyle = 1; 
            projectile.friendly = true;  
            projectile.hostile = false;
            projectile.tileCollide = true; 
            projectile.magic = true;
            projectile.timeLeft = 400;
			projectile.scale = 0f;
            aiType = ProjectileID.Bullet;		
			ProjectileID.Sets.Homing[projectile.type] = true;
        }
        public override void AI()
        {
            for (int index1 = 0; index1 < 10; ++index1)
                {
                    float x = projectile.position.X - projectile.velocity.X / 10f * (float) index1;
                    float y = projectile.position.Y - projectile.velocity.Y / 10f * (float) index1;
                    int index2 = Dust.NewDust(new Vector2(x, y), 1, 1, 111, 0.0f, 0.0f, 0, new Color(), 1f);
                    Main.dust[index2].alpha = projectile.alpha;
                    Main.dust[index2].position.X = x;
                    Main.dust[index2].position.Y = y;
			        Main.dust[index2].scale = (float) Main.rand.Next(30, 80) * 0.013f;
                    Main.dust[index2].velocity *= 0.0f;
                    Main.dust[index2].noGravity = true;
                }

			
			if (projectile.alpha > 70)
			{
				projectile.alpha -= 15;
				if (projectile.alpha < 70)
				{
					projectile.alpha = 70;
				}
			}
			if (projectile.localAI[0] == 0f)
			{
				AdjustMagnitude(ref projectile.velocity);
				projectile.localAI[0] = 1f;
			}
			Vector2 move = Vector2.Zero;
			float distance = 800f;
			if (distance > 800f)
			{
				projectile.active = false;
			}
			else
			{
				projectile.active = true;
			}
			bool target = false;
			for (int k = 0; k < 200; k++)
			{
				if (Main.npc[k].active && !Main.npc[k].dontTakeDamage && !Main.npc[k].friendly && Main.npc[k].lifeMax > 5 && Main.npc[k].type != 488)
				{
					Vector2 newMove = Main.npc[k].Center - projectile.Center;
					float distanceTo = (float)Math.Sqrt(newMove.X * newMove.X + newMove.Y * newMove.Y);
					if (distanceTo < distance)
					{
						move = newMove;
						distance = distanceTo;
						target = true;
					}
				}
			}
			if (target)
			{
				AdjustMagnitude(ref move);
				projectile.velocity = (10 * projectile.velocity + move) / 3f;
				AdjustMagnitude(ref projectile.velocity);
			}
        }
		private void AdjustMagnitude(ref Vector2 vector)
		{
			float magnitude = (float)Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y);
			if (magnitude > 6f)
			{
				vector *= 6f / magnitude;
			}
		}
    }
}