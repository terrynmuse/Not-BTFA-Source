using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace ForgottenMemories.Projectiles.Energazer
{
    public class StaticShivProjectile : ModProjectile
    {	
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.aiStyle = 1;
			aiType = ProjectileID.Bullet;
            projectile.friendly = true;
			projectile.thrown = true;
			projectile.scale = 1f;
            projectile.timeLeft = 800;
			projectile.extraUpdates = 1;
			projectile.tileCollide = true;
        }
		
		public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Static Shiv");
        } 
		
		/*public override void AI()
		{
			Player player = Main.player[projectile.owner];
		}*/
		
		public override void Kill(int timeLeft)
		{
			Player player = Main.player[projectile.owner];
			
			Vector2 usePos = projectile.position;	
			Vector2 rotVector = (projectile.rotation - MathHelper.ToRadians(90f)).ToRotationVector2(); 
			usePos += rotVector;

			for (int i = 0; i < 20; i++)
			{	
				int dustIndex = Dust.NewDust(usePos, projectile.width, projectile.height, 111, 0f, 0f, 0, default(Color), 1f);
				Dust currentDust = Main.dust[dustIndex];
				currentDust.position = (currentDust.position + projectile.Center) / 2f;
				currentDust.velocity += rotVector * 2f;
				currentDust.velocity *= 0.5f;
				currentDust.noGravity = true;
				currentDust.noLight = true;
				usePos -= rotVector * 8f;
			}
		}
    }
}