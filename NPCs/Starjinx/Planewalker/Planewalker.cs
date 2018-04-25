using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ForgottenMemories.NPCs.Starjinx.Planewalker
{
    public class Planewalker : ModNPC
    {
		protected int guardianTimerRed = 0;
		protected int guardianTimerGreen = 0;
		protected int guardianTimerYellow = 0;
		protected int bloomTimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Planewalker");
			Main.npcFrameCount[npc.type] = 1;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = -1;  
			npc.lifeMax = 140;	 
            npc.defense = 0;
			npc.value = 500f;
            npc.knockBackResist = 3f;
            npc.width = 20;
            npc.height = 34;
			npc.damage = 0;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = 	new Terraria.Audio.LegacySoundStyle(42, 167);
            npc.DeathSound = new Terraria.Audio.LegacySoundStyle(42, 168);;
			NPCID.Sets.TrailCacheLength[npc.type] = 10;
			NPCID.Sets.TrailingMode[npc.type] = 3;
			banner = npc.type;
			bannerItem = mod.ItemType("PlanewalkerBannerItem");
        }
		public override void AI()
        {
			Player player = Main.player[npc.target];
			npc.spriteDirection = npc.direction;
			
			Lighting.AddLight(npc.position, 0.2f, 0.2f, 0f);	
			
			bloomTimer++;
			
			float distance = 1500f;
            if (player == Main.player[npc.target])
            {
				if ((npc.active && player.active && (double) Vector2.Distance(player.Center, npc.Center) <= (double) distance) && bloomTimer == 10)
				{
					int bloom = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + (int)(npc.height/2), mod.NPCType("PlanewalkerBloom"));
					Main.npc[bloom].Center += new Vector2(0, 0);
					Main.npc[bloom].ai[1] = npc.whoAmI;
				}
                if ((npc.active && player.active && (double) Vector2.Distance(player.Center, npc.Center) <= (double) distance) && guardianTimerRed == 10 && (npc.life < npc.lifeMax))
                {
					for (int index1 = 0; index1 < 36; ++index1)
					{
						Vector2 vector2_3 = (new Vector2((float) npc.width, (float) npc.height) * 0.75f * 0.5f).RotatedBy((double) (index1 - (36 / 2 - 1)) * 6.28318548202515 / (double) 36, new Vector2()) + npc.Center;
						Vector2 vector2_4 = vector2_3 - npc.Center;
						int index2 = Dust.NewDust(vector2_3 + vector2_4, 0, 0, 60, vector2_4.X * 2f, vector2_4.Y * 2f, 100, new Color(), 1.4f);
						Main.dust[index2].noGravity = true;
						Main.dust[index2].noLight = false;
						Main.dust[index2].scale = 2f;
						Main.dust[index2].velocity = Vector2.Normalize(vector2_4) * 5f;
					}
					int redGuardian = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + (int)(npc.height/2), mod.NPCType("PlanewalkerGuardianRed"));
					Main.npc[redGuardian].Center += new Vector2(0, 30);
					Main.npc[redGuardian].ai[1] = npc.whoAmI;
                }
				if ((npc.active && player.active && (double) Vector2.Distance(player.Center, npc.Center) <= (double) distance) && guardianTimerGreen == 10 && (npc.life < npc.lifeMax))
                {
					for (int index1 = 0; index1 < 36; ++index1)
					{
						Vector2 vector2_3 = (new Vector2((float) npc.width / 2, (float) npc.height) * 0.75f * 0.5f).RotatedBy((double) (index1 - (36 / 2 - 1)) * 6.28318548202515 / (double) 36, new Vector2()) + npc.Center;
						Vector2 vector2_4 = vector2_3 - npc.Center;
						int index2 = Dust.NewDust(vector2_3 + vector2_4, 0, 0, 61, vector2_4.X * 2f, vector2_4.Y * 2f, 100, new Color(), 1.4f);
						Main.dust[index2].noGravity = true;
						Main.dust[index2].noLight = false;
						Main.dust[index2].scale = 2f;
						Main.dust[index2].velocity = Vector2.Normalize(vector2_4) * 5f;
					}
					int greenGuardian = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + (int)(npc.height/2), mod.NPCType("PlanewalkerGuardianGreen"));
					Main.npc[greenGuardian].Center += new Vector2(0, 30).RotatedBy(MathHelper.ToRadians(120));
					Main.npc[greenGuardian].ai[1] = npc.whoAmI;
                }
				if ((npc.active && player.active && (double) Vector2.Distance(player.Center, npc.Center) <= (double) distance) && guardianTimerYellow == 10 && (npc.life < npc.lifeMax))
                {
					for (int index1 = 0; index1 < 36; ++index1)
					{
						Vector2 vector2_3 = (new Vector2((float) npc.width / 4, (float) npc.height) * 0.75f * 0.5f).RotatedBy((double) (index1 - (36 / 2 - 1)) * 6.28318548202515 / (double) 36, new Vector2()) + npc.Center;
						Vector2 vector2_4 = vector2_3 - npc.Center;
						int index2 = Dust.NewDust(vector2_3 + vector2_4, 0, 0, 64, vector2_4.X * 2f, vector2_4.Y * 2f, 100, new Color(), 1.4f);
						Main.dust[index2].noGravity = true;
						Main.dust[index2].noLight = false;
						Main.dust[index2].scale = 2f;
						Main.dust[index2].velocity = Vector2.Normalize(vector2_4) * 5f;
					}
					int yellowGuardian = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y + (int)(npc.height/2), mod.NPCType("PlanewalkerGuardianYellow"));
					Main.npc[yellowGuardian].Center += new Vector2(0, 30).RotatedBy(MathHelper.ToRadians(240));
					Main.npc[yellowGuardian].ai[1] = npc.whoAmI;
                }
			}
			
			if (npc.life < npc.lifeMax)
			{
				guardianTimerRed++;
				guardianTimerGreen++;
				guardianTimerYellow++;
				npc.damage = 30;
			}
			
			float num1 = 3f;
			float num2 = 11f / 1000f;
			npc.TargetClosest(true);
			Vector2 vector2 = new Vector2(npc.position.X + (float) npc.width * 0.5f, npc.position.Y + (float) npc.height * 0.5f);
			float num3 = Main.player[npc.target].position.X + (float) (Main.player[npc.target].width / 2) - vector2.X;
			float num4 = Main.player[npc.target].position.Y + (float) (Main.player[npc.target].height / 2) - vector2.Y;
			float num5 = (float) Math.Sqrt((double) num3 * (double) num3 + (double) num4 * (double) num4);
			float num6 = num5;
			++npc.ai[1];
			if ((double) npc.ai[1] > 600.0)
			{
				num2 *= 8f;
				num1 = 6f;
				if ((double) npc.ai[1] > 650.0)
					npc.ai[1] = 0.0f;
			}
			else if ((double) num6 < 250.0)
			{
				npc.ai[0] += 0.9f;
				if ((double) npc.ai[0] > 0.0)
					npc.velocity.Y += 0.019f;
				else
					npc.velocity.Y -= 0.019f;
				if ((double) npc.ai[0] < -100.0 || (double) npc.ai[0] > 100.0)
					npc.velocity.X += 0.019f;
				else
					npc.velocity.X -= 0.019f;
				if ((double) npc.ai[0] > 200.0)
					npc.ai[0] = -200f;
			}
			if ((double) num6 > 350.0)
			{
				num1 = 7f;
				num2 = 0.3f;
			}
			else if ((double) num6 > 300.0)
			{
				num1 = 5f;
				num2 = 0.2f;
			}
			else if ((double) num6 > 250.0)
			{
				num1 = 3.5f;
				num2 = 0.1f;
			}
			float num7 = num1 / num5;
			float num8 = num3 * num7;
			float num9 = num4 * num7;
			if (Main.player[npc.target].dead)
			{
				num8 = (float) ((double) npc.direction * (double) num1 / 2.0);
				num9 = (float) (-(double) num1 / 2.0);
			}
			if ((double) npc.velocity.X < (double) num8)
				npc.velocity.X += num2;
			else if ((double) npc.velocity.X > (double) num8)
				npc.velocity.X -= num2;
			if ((double) npc.velocity.Y < (double) num9)
				npc.velocity.Y += num2;
			else if ((double) npc.velocity.Y > (double) num9)
				npc.velocity.Y -= num2;
			if ((double) num8 > 0.0)
			{
				npc.spriteDirection = -1;
			}
			if ((double) num8 < 0.0)
			{
				npc.spriteDirection = 1;
			}
        }
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/PlanewalkerGore"), 1f);
			}
		}
		public override void NPCLoot()
		{		
			if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("AuroraBowl"), Main.rand.Next(1, 5));
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			var effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame,
							 drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
			SpriteEffects spriteEffects = SpriteEffects.None;
			Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)npc.position.X + (double)npc.width * 0.5) / 16, (int)(((double)npc.position.Y + (double)npc.height * 0.5) / 16.0));
			Texture2D texture2D3 = mod.GetTexture("NPCs/Starjinx/Planewalker/PlanewalkerGlow");
			int num156 = Main.npcTexture[npc.type].Height / Main.npcFrameCount[npc.type];
			int y3 = num156 * (int)npc.frameCounter;
			Microsoft.Xna.Framework.Rectangle rectangle = new Microsoft.Xna.Framework.Rectangle(0, y3, texture2D3.Width, num156);
			Vector2 origin2 = rectangle.Size() / 2f;
			int arg_5ADA_0 = npc.type;
			int arg_5AE7_0 = npc.type;
			int arg_5AF4_0 = npc.type;
			int num157 = 10;
			int num158 = 2;
			int num159 = 1;
			float value3 = 1f;
			float num160 = 0f;
			
			
			int num161 = num159;
			while (npc.velocity != Vector2.Zero &&((num158 > 0 && num161 < num157) || (num158 < 0 && num161 > num157)))
			{
				Microsoft.Xna.Framework.Color color26 = color25;
				color26 = npc.GetAlpha(color26);		
				{
					goto IL_6899;
				}
				color26 = Microsoft.Xna.Framework.Color.Lerp(color26, Microsoft.Xna.Framework.Color.Green, 0.5f);
				
				IL_6881:
				num161 += num158;
				continue;
				IL_6899:
				float num164 = (float)(num157 - num161);
				if (num158 < 0)
				{
					num164 = (float)(num159 - num161);
				}
				color26 *= num164 / ((float)NPCID.Sets.TrailCacheLength[npc.type] * 1.5f);
				Vector2 value4 = (npc.oldPos[num161]);
				float num165 = npc.rotation;
				SpriteEffects effectss = spriteEffects;
				Main.spriteBatch.Draw(texture2D3, value4 + npc.Size / 2f - Main.screenPosition + new Vector2(0f, npc.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(rectangle), color26, num165 + npc.rotation * num160 * (float)(num161 - 1) * -(float)spriteEffects.HasFlag(SpriteEffects.FlipHorizontally).ToDirectionInt(), origin2, npc.scale, effectss, 0f);
				goto IL_6881;
			}
			return false;
		}
		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			BTFAUtility.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/Starjinx/Planewalker/PlanewalkerGlow"));
		}
    }
}