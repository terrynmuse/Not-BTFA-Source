using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using System;

namespace ForgottenMemories.Projectiles
{
	public class LaserbeamStaff : ModProjectile
	{
		int kek;
		public override void SetDefaults()
		{
			projectile.width = 44;
			projectile.height = 44;
			//projectile.aiStyle = 75;
			projectile.friendly = false;
			projectile.penetrate = -1;
			projectile.tileCollide = false;
			projectile.hide = true;
			projectile.magic = true;
			projectile.ignoreWater = true;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Laserbeam Staff");
		}
		
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			//float num = 1.57079637f;
			Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
			projectile.ai[0] += 1f;
			int num16 = 0;
			float move = 0;
			if (projectile.ai[0] >= 120f)
			{
				num16++;
			}
			if (projectile.ai[0] >= 360f)
			{
				num16++;
			}
			bool flag4 = false;
			if (projectile.ai[0] == 60f || projectile.ai[0] == 180f || (projectile.ai[0] > 180f && projectile.ai[0] % 20f == 0f))
			{
				flag4 = true;
			}
			bool flag5 = projectile.ai[0] >= 360f;
			int num17 = 10;
			//if (!flag5)
			//{
					projectile.ai[1] += 1f;
			//}
			bool flag6 = false;
			if (flag5 && projectile.ai[0] % 20f == 0f)
			{
				flag6 = true;
			}
			if (projectile.ai[1] >= 1)
			{
				flag6 = true;
				float scaleFactor5 = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
				Vector2 vector17 = vector;
				Vector2 value7 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector17;
				if (player.gravDir == -1f)
				{
					value7.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector17.Y;
				}
				Vector2 vector18 = Vector2.Normalize(value7);
				if (float.IsNaN(vector18.X) || float.IsNaN(vector18.Y))
				{
					vector18 = -Vector2.UnitY;
				}
				vector18 = Vector2.Normalize(Vector2.Lerp(vector18, Vector2.Normalize(projectile.velocity), 0.92f));
				vector18 *= scaleFactor5;
				if (vector18.X != projectile.velocity.X || vector18.Y != projectile.velocity.Y)
				{
					projectile.netUpdate = true;
				}
				projectile.velocity = vector18;
				
				projectile.ai[1] = 0f;
			}
			if (projectile.soundDelay <= 0 && !flag5)
			{
				projectile.soundDelay = num17 - num16;
				projectile.soundDelay *= 2;
				if (projectile.ai[0] != 1f)
				{
					Main.PlaySound(SoundID.Item15, projectile.position);
				}
			}
			if (projectile.ai[0] > 10f && !flag5)
			{
				Vector2 vector13 = Vector2.UnitX * 18f;
				vector13 = vector13.RotatedBy((double)(projectile.rotation - 1.57079637f), default(Vector2));
				Vector2 value6 = projectile.Center + vector13;
				for (int k = 0; k < num16 + 1; k++)
				{
					int num18 = 130;
					float num19 = 0.4f;
					if (k % 2 == 1)
					{
						num18 = 130;
						num19 = 0.65f;
					}
					Vector2 vector14 = value6 + ((float)Main.rand.NextDouble() * 6.28318548f).ToRotationVector2() * (12f - (float)(num16 * 2));
					int num20 = Dust.NewDust(projectile.Center, 16, 16, num18, projectile.velocity.X / 2f, projectile.velocity.Y / 2f, 0, default(Color), 1f);
					Main.dust[num20].velocity = Vector2.Normalize(value6 - vector14) * 1.5f * (10f - (float)num16 * 2f) / 10f;
					Main.dust[num20].noGravity = true;
					Main.dust[num20].scale = num19;
					Main.dust[num20].customData = player;
				}
			}
			if (flag6 && Main.myPlayer == projectile.owner)
			{
				bool flag7 = !flag4 || player.CheckMana(player.inventory[player.selectedItem].mana, true, false);
				bool flag8 = (player.channel & flag7) && !player.noItems && !player.CCed;
				if (flag8)
				{
					if (projectile.ai[0] == 360f)
					{
						Vector2 center = projectile.Center;
						Vector2 vector15 = Vector2.Normalize(projectile.velocity);
						if (float.IsNaN(vector15.X) || float.IsNaN(vector15.Y))
						{
							vector15 = -Vector2.UnitY;
						}
						int num21 = (int)((float)projectile.damage * 4f);
						int num22 = Projectile.NewProjectile(center.X, center.Y, vector15.X, vector15.Y, mod.ProjectileType("LaserbeamBig"), num21, projectile.knockBack, projectile.owner, 0f, (float)projectile.whoAmI);
						kek = num22;
						projectile.netUpdate = true;
					}
					else if (flag5)
					{
						Projectile projectile1 = Main.projectile[kek];
						if (!projectile1.active || projectile1.type != mod.ProjectileType("LaserbeamBig"))
						{
							projectile.Kill();
							return;
						}
					}
				}
				else
				{
					if (!flag5)
					{
						int num23 = mod.ProjectileType("laserbeam2");
						float scaleFactor4 = 10f;
						Vector2 center2 = projectile.Center;
						Vector2 vector16 = Vector2.Normalize(projectile.velocity) * scaleFactor4;
						if (float.IsNaN(vector16.X) || float.IsNaN(vector16.Y))
						{
							vector16 = -Vector2.UnitY;
						}
						float num24 = 0.7f + (float)num16 * 0.3f;
						int num25 = (num24 < 1f) ? projectile.damage : ((int)((float)projectile.damage * 3f));
						Projectile.NewProjectile(center2.X, center2.Y, vector16.X, vector16.Y, num23, num25, projectile.knockBack, projectile.owner, num16, 0);
					}
					projectile.Kill();
				}
			}
			
			projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
			projectile.rotation = projectile.velocity.ToRotation() + MathHelper.Pi/4;
			//projectile.spriteDirection = projectile.direction;
			projectile.timeLeft = 2;
			player.ChangeDir(projectile.direction);
			player.heldProj = projectile.whoAmI;
			player.itemTime = 2;
			player.itemAnimation = 2;
			player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
			Vector2 vector38 = Main.OffsetsPlayerOnhand[player.bodyFrame.Y / 56] * 2f;
			if (player.direction != 1)
			{
				vector38.X = (float)player.bodyFrame.Width - vector38.X;
			}
			if (player.gravDir != 1f)
			{
				vector38.Y = (float)player.bodyFrame.Height - vector38.Y;
			}
			vector38 -= new Vector2((float)(player.bodyFrame.Width - player.width), (float)(player.bodyFrame.Height - 42)) / 2f;
			projectile.Center = player.RotatedRelativePoint(player.position + vector38, true) - projectile.velocity;
		}
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			if (projectile.ai[0] > 360f && Main.projectile[kek].type == mod.ProjectileType("LaserbeamBig"))
			{
				Main.instance.DrawProj(kek);
			}
			return true;
		}
	}
}