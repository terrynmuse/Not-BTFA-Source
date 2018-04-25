using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ForgottenMemories.NPCs.Starjinx.NightResident
{
    public class NightResident : ModNPC
    {
		protected int reflectorTimer = 0;
		protected int DashTimer = 0;
		protected int movementTimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Night Resident");
			Main.npcFrameCount[npc.type] = 1;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = 0;  
			npc.lifeMax = 160;	 
            npc.defense = 5;  
			npc.value = 500f;
            npc.knockBackResist = 1.5f;
            npc.width = 26;
            npc.height = 30;
			npc.damage = 10;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = 	new Terraria.Audio.LegacySoundStyle(29, 48);
            npc.DeathSound = SoundID.NPCDeath43;
			NPCID.Sets.TrailCacheLength[npc.type] = 10;
			NPCID.Sets.TrailingMode[npc.type] = 3;
			banner = npc.type;
			bannerItem = mod.ItemType("NightResidentBannerItem");
        }
		public override void AI()
        {
			Player player = Main.player[npc.target];
			npc.spriteDirection = npc.direction;
			
			reflectorTimer++;
			movementTimer++;
			
			if (reflectorTimer >= 180 && reflectorTimer <= 300)
			{
				npc.reflectingProjectiles = true;
			}
			if (reflectorTimer > 300)
			{
				reflectorTimer = 0;
				npc.reflectingProjectiles = false;
			}
			
            if (player == Main.player[npc.target])
            {
                if (npc.active && player.active && (double) Vector2.Distance(player.Center, npc.Center) >= (double) 1f)
                {
					Vector2 vector2 = new Vector2(npc.position.X + (float) npc.width * 0.5f, npc.position.Y + (float) npc.height * 0.5f);
					float num2 = Main.player[npc.target].position.X + (float) (Main.player[npc.target].width / 2) - vector2.X;
					float num3 = (Main.player[npc.target].position.Y) + (float) (Main.player[npc.target].height / 2) - vector2.Y;
					if (movementTimer < 61)
					{
						num3 = (Main.player[npc.target].position.Y - 100) + (float) (Main.player[npc.target].height / 2) - vector2.Y;
					}
					if (movementTimer > 60)
					{
						num3 = (Main.player[npc.target].position.Y + 100) + (float) (Main.player[npc.target].height / 2) - vector2.Y;
					}
					float num4 = 8f / (float) Math.Sqrt((double) num2 * (double) num2 + (double) num3 * (double) num3);
					npc.velocity.X = num2 * num4 * 0.45f;
					npc.velocity.Y = num3 * num4 * 0.45f;
					
					if (movementTimer > 120)
					{
						movementTimer = 0;
					}
				}
				/*DashTimer++;
				if (DashTimer == 180)
				{
					npc.aiStyle = 0;
					DashTimer = 0;
					npc.velocity.X *= 3f;
					npc.velocity.Y *= 3f;
					Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
					{
						float rotation = (float)Math.Atan2((vector8.Y) - (Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)), (vector8.X) - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
						npc.velocity.X = (float)(Math.Cos(rotation) * 36) * -1;
						npc.velocity.Y = (float)(Math.Sin(rotation) * 36) * -1;
					}
				}*/
			}
		}
		public override void HitEffect(int hitDirection, double damage)
		{
			/*if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Energy_Humanoid_Gore"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Energy_Humanoid_Gore"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Energy_Humanoid_Gore_2"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Energy_Humanoid_Gore_2"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Energy_Humanoid_Gore_3"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Energy_Humanoid_Gore_4"), 1f);
			}*/
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			var effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame,
							 drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
			SpriteEffects spriteEffects = SpriteEffects.None;
			Microsoft.Xna.Framework.Color color25 = Lighting.GetColor((int)((double)npc.position.X + (double)npc.width * 0.5) / 16, (int)(((double)npc.position.Y + (double)npc.height * 0.5) / 16.0));
			Texture2D texture2D3 = mod.GetTexture("NPCs/Starjinx/NightResident/NightResidentGlow");
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
			BTFAUtility.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/Starjinx/NightResident/NightResidentGlow"));
		}
    }
}