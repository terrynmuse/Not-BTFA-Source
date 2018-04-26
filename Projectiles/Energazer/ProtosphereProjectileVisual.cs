using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
 
namespace ForgottenMemories.Projectiles.Energazer
{
    public class ProtosphereProjectileVisual: ModProjectile
    {
		public int scaleTimer = 0;
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Protosphere Shadow");
        } 
        public override void SetDefaults()
        {
			projectile.width = 2;
			projectile.height = 2;
			projectile.aiStyle = 1;
			projectile.scale = 2.5f;
			projectile.alpha = 240;
			projectile.tileCollide = false;
			aiType = ProjectileID.Bullet;
        }
		public override Color? GetAlpha(Color lightColor)
        {
			if (!Main.dayTime)
			{
				return new Color(0f, 0.25f, 0.5f);
			}
			else 
			{
				return new Color(0f, 0.15f, 0.3f);
			}
        }
		public override void AI()
        {		
			scaleTimer+= 4;
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
			if (scaleTimer <= 80)
			{
				projectile.scale = projectile.scale - 0.05f;
				projectile.position.X = projectile.position.X - 1f;
			}
			if (scaleTimer > 81 && scaleTimer < 160)
			{
				projectile.scale = projectile.scale + 0.05f;
				projectile.position.X = projectile.position.X + 1.06f;
			}
			if (scaleTimer == 160)
			{
				scaleTimer = 0;
				projectile.scale = 2.5f;
			}
        }
		public override void Kill(int timeLeft)
        {
			Player player = Main.player[projectile.owner];
			//Main.PlaySound(new Terraria.Audio.LegacySoundStyle(42, 31));
		}
    }
}