using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ForgottenMemories.NPCs.Energy.Arachnergy
{
    public class StingingEnergy : ModNPC
    {
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Stinging Energy");
			Main.npcFrameCount[npc.type] = 3;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = 0;  
			npc.lifeMax = 10;	 
            npc.defense = 0;  
            npc.knockBackResist = 2f;
            npc.width = 22;
            npc.height = 18;
			npc.damage = 10;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.DeathSound = SoundID.NPCHit53;
        }
		public override void AI()
        {
			Player player = Main.player[npc.target];
			Vector2 vector2 = new Vector2(npc.position.X + (float) npc.width * 0.5f, npc.position.Y + (float) npc.height * 0.5f);
                float num2 = Main.player[npc.target].position.X + (float) (Main.player[npc.target].width / 2) - vector2.X;
                float num3 = Main.player[npc.target].position.Y + (float) (Main.player[npc.target].height / 2) - vector2.Y;
                float num4 = 8f / (float) Math.Sqrt((double) num2 * (double) num2 + (double) num3 * (double) num3);
				npc.velocity.X = num2 * num4 * 0.4f;
                npc.velocity.Y = num3 * num4 * 0.4f;
        }
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Stinging_Energy_Gore"), 1f);
			}
		}
		public override void FindFrame(int frameHeight)
		{
			const int Frame_1 = 0;
			const int Frame_2 = 1;
			const int Frame_3 = 2;
			
			{
				npc.frameCounter++;
				if (npc.frameCounter < 6)
				{
					npc.frame.Y = Frame_1 * frameHeight;
				}
				else if (npc.frameCounter < 12)
				{
					npc.frame.Y = Frame_2 * frameHeight;
				}
				else if (npc.frameCounter < 18)
				{
					npc.frame.Y = Frame_3 * frameHeight;
				}
				else
				{
					npc.frameCounter = 0;
				}
			}
		}
		public override bool PreDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			var effects = npc.direction == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally;
			spriteBatch.Draw(Main.npcTexture[npc.type], npc.Center - Main.screenPosition + new Vector2(0, npc.gfxOffY), npc.frame,
							 drawColor, npc.rotation, npc.frame.Size() / 2, npc.scale, effects, 0);
			return false;
		}
		public override void PostDraw(SpriteBatch spriteBatch, Color drawColor)
		{
			BTFAUtility.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/Energy/Arachnergy/StingingEnergyGlow"));
		}
    }
}