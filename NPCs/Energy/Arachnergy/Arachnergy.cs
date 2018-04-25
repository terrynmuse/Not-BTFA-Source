using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ForgottenMemories.NPCs.Energy.Arachnergy
{
    public class Arachnergy : ModNPC
    {
		protected int spawnerTimer = 0;
		protected int DashTimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Arachnergy");
			Main.npcFrameCount[npc.type] = 4;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = 3;  
			npc.lifeMax = 100;	 
            npc.defense = 5;  
			npc.value = 500f;
			aiType = 73;
            npc.knockBackResist = 0.1f;
            npc.width = 44;
            npc.height = 46;
			npc.damage = 30;
            npc.lavaImmune = true;
            npc.noTileCollide = false;
            npc.HitSound = 	new Terraria.Audio.LegacySoundStyle(29, 48);
			npc.dontTakeDamage = false;
            npc.DeathSound = SoundID.NPCDeath43;
			banner = npc.type;
			bannerItem = mod.ItemType("ArachnergyBannerItem");
        }
		public override void AI()
        {
			Player player = Main.player[npc.target];
			npc.spriteDirection = npc.direction;
			
			spawnerTimer++;
			
			float distance = 1500f;
            if (player == Main.player[npc.target])
            {
                if ((npc.active && player.active && (double) Vector2.Distance(player.Center, npc.Center) <= (double) distance) && spawnerTimer == 300 || spawnerTimer == 360 || spawnerTimer == 420)
                {
					npc.dontTakeDamage = true;
					npc.aiStyle = 0;  
					NPC.NewNPC((int)(npc.position.X - Main.rand.Next(-160, 160)), (int)(npc.position.Y - Main.rand.Next(-160, 160)), (mod.NPCType("StingingEnergy")));
					Main.PlaySound(SoundID.Item17, npc.position);
					int num23 = 36;	
					for (int index1 = 0; index1 < num23; ++index1)
					{
						Vector2 vector2_3 = (new Vector2((float) npc.width, (float) npc.height) * 0.75f * 0.5f).RotatedBy((double) (index1 - (num23 / 2 - 1)) * 6.28318548202515 / (double) num23, new Vector2()) + npc.Center;
						Vector2 vector2_4 = vector2_3 - npc.Center;
						int index2 = Dust.NewDust(vector2_3 + vector2_4, 0, 0, 111, vector2_4.X * 2f, vector2_4.Y * 2f, 100, new Color(), 1.4f);
						Main.dust[index2].noGravity = true;
						Main.dust[index2].noLight = false;
						Main.dust[index2].scale = 2f;
						Main.dust[index2].velocity = Vector2.Normalize(vector2_4) * 5f;
					}
                }
				if (npc.active && player.active)
				{
					if (spawnerTimer >= 300 && spawnerTimer <= 480)
					{ 
						npc.aiStyle = 0;				
						npc.velocity.Y = 0;								
						npc.velocity.X = 0;
						npc.dontTakeDamage = true;
						npc.defense = 9999;
					}
					else
						npc.defense = 5;
						npc.aiStyle = 3;
						npc.dontTakeDamage = false;
					if (spawnerTimer >= 480)
					{
						spawnerTimer = 0;
					}
				}
			}		
        }
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Arachnergy_Gore"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Arachnergy_Gore_2"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Arachnergy_Gore_3"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Arachnergy_Gore_4"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Arachnergy_Gore_5"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Arachnergy_Gore_6"), 1f);
			}
		}
		public override void NPCLoot ()
		{
			spawnerTimer = 0;
			if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EnergyRemnant"), Main.rand.Next(1, 5));
			}
		}
		public override void FindFrame(int frameHeight)
		{
			const int Frame_1 = 0;
			const int Frame_2 = 1;
			const int Frame_3 = 2;
			const int Frame_4 = 3;
			
			if (spawnerTimer >= 300 && spawnerTimer <= 480)
			{
				npc.frame.Y = Frame_2 * frameHeight;
			}
			else 
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
				else if (npc.frameCounter < 24)
				{
					npc.frame.Y = Frame_4 * frameHeight;
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
			BTFAUtility.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/Energy/Arachnergy/ArachnergyGlow"));
		}
    }
}