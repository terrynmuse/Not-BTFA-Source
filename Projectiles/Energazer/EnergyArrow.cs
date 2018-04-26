using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Events;
using Terraria.GameContent.Shaders;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.World.Generation;

namespace ForgottenMemories.Projectiles.Energazer
{
	public class EnergyArrow : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 8;
			projectile.height = 8;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.aiStyle = 1;
			aiType = ProjectileID.WoodenArrowFriendly;
			projectile.ranged = true;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 5; 
			ProjectileID.Sets.TrailingMode[projectile.type] = 0; 
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Energy Arrow");
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			for (int index1 = 0; index1 < 3; ++index1)
			{
				float num8 = target.velocity.X * 0.334f * (float) index1;
				float num9 = (float) -((double) target.velocity.Y * 0.333999991416931) * (float) index1;
				int index2 = Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, 111, 0.0f, 0.0f, 100, new Color(), 1.1f);
				Main.dust[index2].noGravity = true;
				Main.dust[index2].velocity *= 0.0f;
				Main.dust[index2].position.X -= num8;
				Main.dust[index2].position.Y -= num9;
			}
		}
		
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(0, 0));
			int num2 = Main.rand.Next(20, 40);
            for (int index1 = 0; index1 < num2; ++index1)
            {
                int index2 = Dust.NewDust(projectile.Center, 0, 0, 111, 0.0f, 0.0f, 100, new Color(), 0.8f);
                Main.dust[index2].velocity *= 1.2f;
                --Main.dust[index2].velocity.Y;
                Main.dust[index2].velocity += projectile.velocity;
                Main.dust[index2].noGravity = true;
            }
		}

		//public override void AI() {}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
			for (int k = 0; k < projectile.oldPos.Length; k++)
			{
				Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
				Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
				spriteBatch.Draw(mod.GetTexture("Projectiles/Energazer/EnergyTrail"), drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
			}
			return true;
		}
	}
}
