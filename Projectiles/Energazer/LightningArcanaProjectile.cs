using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace ForgottenMemories.Projectiles.Energazer
{
    public class LightningArcanaProjectile : ModProjectile
    {
		protected int randomTimer = 0;

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning");
        } 

        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 30;
            projectile.friendly = true;
			projectile.scale = 0f;
            projectile.aiStyle = 1;
            projectile.magic = true;
			aiType = ProjectileID.Bullet;
			projectile.timeLeft = 300;
			projectile.tileCollide = true;
        }

		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) //When you hit an NPC
        {
			if (target.damage >= 1)
			{
				Player player = Main.player[projectile.owner];
				if (player.GetModPlayer<BTFAPlayer>().navitasOrbisCounter < 4)
				{
					player.GetModPlayer<BTFAPlayer>().navitasOrbisCounter += 1;
				}
				else
				{
					player.GetModPlayer<BTFAPlayer>().navitasOrbisCounter = 1;
				}
			}
        }
        public override void AI()
        { 
			Player player = Main.player[projectile.owner];		
			if (Main.player[projectile.owner].HeldItem.type == mod.ItemType("LightningArcana") && player.active)
			{
				projectile.active = true;
			}
			else
			{
				projectile.active = false;
			}
			
			if ((double) Vector2.Distance(player.Center, projectile.Center) >= (double) 40f)
			{
				for (int index1 = 0; index1 < 10; ++index1)
                {
                    float x = projectile.position.X - projectile.velocity.X / 10f * (float) index1;
                    float y = projectile.position.Y - projectile.velocity.Y / 10f * (float) index1;
                    int index2 = Dust.NewDust(new Vector2(x, y), 1, 1, 111, 0.0f, 0.0f, 0, new Color(), 1f);
                    Main.dust[index2].alpha = projectile.alpha;
                    Main.dust[index2].position.X = x;
                    Main.dust[index2].position.Y = y;
			        Main.dust[index2].scale = (float) Main.rand.Next(70, 70) * 0.013f;
                    Main.dust[index2].velocity *= 0.0f;
                    Main.dust[index2].noGravity = true;
                }
			}
			if (projectile.timeLeft == 299)
            {	
				projectile.ai[0] = -10;
				projectile.ai[1] = -10;
			}
			if (projectile.ai[1] == 0)
            {	
				projectile.ai[1] = -5;
			}	
            if (projectile.ai[0] == 0)
            {	
				projectile.ai[0] = -5;
			}
			if (projectile.timeLeft%2 == 0)
			{				
				projectile.velocity.Y = projectile.velocity.Y - projectile.ai[0]*2;
				projectile.velocity.X = projectile.velocity.X - projectile.ai[1]*2;	
			}
			else
			{
				projectile.velocity.Y = projectile.velocity.Y + projectile.ai[0]*2;
				projectile.velocity.X = projectile.velocity.X + projectile.ai[1]*2;
			}
		}
    }
}