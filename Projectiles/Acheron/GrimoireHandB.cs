using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System.Collections.Generic;
using System;

namespace ForgottenMemories.Projectiles.Acheron
{
	public class GrimoireHandB : ModProjectile
	{
		int timer = 0;
		int gropedID = -1;
		int previousDamage = 0;

		public override void SetDefaults()
		{
			projectile.width = 24;
			projectile.height = 24;
			projectile.aiStyle = -1;
			projectile.friendly = true;
			projectile.magic = true;
			projectile.penetrate = -1;
			projectile.alpha = 255;
			projectile.tileCollide = true;
			ProjectileID.Sets.TrailCacheLength[projectile.type] = 10;
			ProjectileID.Sets.TrailingMode[projectile.type] = 1;
			projectile.usesLocalNPCImmunity = true;
			projectile.localNPCHitCooldown = 10;
			projectile.timeLeft = 360;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Hand of the Dead");
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
			SpriteEffects spriteEffects = SpriteEffects.None;
			if (projectile.velocity.X < 0)
				spriteEffects = SpriteEffects.FlipHorizontally;
			Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)projectile.position.X + (double)projectile.width * 0.5) / 16, (int)(((double)projectile.position.Y + (double)projectile.height * 0.5) / 16.0));
			Texture2D texture2D3 = Main.projectileTexture[projectile.type];
			int num156 = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
			int y3 = num156 * projectile.frame;
			Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(0, y3, texture2D3.Width, num156);
			Vector2 origin2 = rectangle.Size() / 2f;
			int arg_5ADA_0 = projectile.type;
			int arg_5AE7_0 = projectile.type;
			int arg_5AF4_0 = projectile.type;
			int num157 = 10;
			int num158 = 2;
			int num159 = 1;
			float value3 = 1f;
			float num160 = 0f;
			
			
			int num161 = num159;
			while ((num158 > 0 && num161 < num157) || (num158 < 0 && num161 > num157))
			{
				Microsoft.Xna.Framework.Color color26 = color25;
				color26 = projectile.GetAlpha(color26);		
				{
					goto IL_6899;
				}
				color26 = Microsoft.Xna.Framework.Color.Lerp(color26, Microsoft.Xna.Framework.Color.Blue, 0.5f);
				
				IL_6881:
				num161 += num158;
				continue;
				IL_6899:
				float num164 = (float)(num157 - num161);
				if (num158 < 0)
				{
					num164 = (float)(num159 - num161);
				}
				color26 *= num164 / ((float)ProjectileID.Sets.TrailCacheLength[projectile.type] * 1.5f);
				Vector2 value4 = projectile.oldPos[num161];
				float num165 = projectile.rotation;
				SpriteEffects effects = spriteEffects;
				if (ProjectileID.Sets.TrailingMode[projectile.type] == 2)
				{
					num165 = projectile.oldRot[num161];
					effects = ((projectile.oldSpriteDirection[num161] == -1) ? SpriteEffects.FlipHorizontally : SpriteEffects.None);
				}
				Main.spriteBatch.Draw(texture2D3, value4 + projectile.Size / 2f - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26, num165 + projectile.rotation * num160 * (float)(num161 - 1) * -(float)spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin2, projectile.scale, effects, 0f);
				goto IL_6881;
			}
					
			Microsoft.Xna.Framework.Color color29 = projectile.GetAlpha(color25);
			Main.spriteBatch.Draw(texture2D3, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color29, projectile.rotation, origin2, projectile.scale, spriteEffects, 0f);
			return false;
		}

		public override void AI()
		{
			if (gropedID == -1)
			{
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X);
				if (projectile.velocity.X < 0)
				{
					projectile.rotation += (float) Math.PI;
				}

				if ((float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) > MathHelper.PiOver2 && (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) < (3 * MathHelper.PiOver2))
				{
					projectile.spriteDirection = -1;
				}
				else
				{
					projectile.spriteDirection = 1;
				}

				if (Main.rand.Next(5) == 0)
				{
					int dust2 = Dust.NewDust(projectile.position + projectile.velocity, projectile.height, projectile.width, 20, 0f, 0f);
				}
			
				if (projectile.ai[0] > 0)
				{
					projectile.alpha += 10;
			
					if (projectile.alpha >= 255)
						projectile.Kill();
				}
				else if (projectile.alpha > 0)
				{
					projectile.alpha -= 15;
				}
			}
			else
			{
				NPC npc = Main.npc[gropedID];

				if (npc.active && !npc.dontTakeDamage)
				{
					projectile.Center = npc.Center - projectile.velocity * 2f;
					projectile.gfxOffY = npc.gfxOffY;
				}
				else
				{
					projectile.Kill();
				}
			}
		}

		public override void Kill (int timeLeft)
		{
			if (gropedID != -1)
			{
				Main.PlaySound(SoundID.Item14, projectile.position);

				projectile.position.X += projectile.width / 2;
				projectile.position.Y += projectile.height / 2;

				float modifier = 100f;
			
				projectile.width = (int) (modifier * projectile.scale);
				projectile.height = (int) (modifier * projectile.scale);

				projectile.position.X -= projectile.width / 2;
				projectile.position.Y -= projectile.height / 2;

				for(int i = 0; i < 40; i++) //PLS SPRAY DUST JUICE ON THIS PLS PLS MEW
				{
					int dust = Dust.NewDust(projectile.position, projectile.width, projectile.height, 20, 0, 0, 0, default(Color), 3.4f);
					
					Main.dust[dust].noGravity = true;
					Main.dust[dust].velocity *= 4.4f;
					Main.dust[dust].noLight = true;

					int rng = Main.rand.Next(3);

					if (rng == 0)
					{
						Main.dust[dust].velocity *= 1.5f;
						Main.dust[dust].scale *= 0.7f;
					}
					else if (rng == 1)
					{
						Main.dust[dust].velocity *= 1.2f;
						Main.dust[dust].scale *= 0.9f;
					}
				}
			
				if (projectile.owner == Main.myPlayer)
				{
					projectile.damage = previousDamage;
					projectile.Damage();
				}
			}
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if (gropedID == -1)
			{
				projectile.ai[0] += 1;
				gropedID = target.whoAmI;

				projectile.velocity = (target.Center - projectile.Center) * 0.75f;
				projectile.netUpdate = true;
				projectile.ignoreWater = true;
				projectile.tileCollide = false;

				previousDamage = projectile.damage;
				projectile.damage = 0;

				target.AddBuff(mod.BuffType("Groped"), projectile.timeLeft);
			}
		}
		
		public override bool OnTileCollide (Vector2 velocity1)
		{
			projectile.ai[0]++;
			projectile.velocity = velocity1;
			projectile.tileCollide = false;
			return false;
		}
	}
}