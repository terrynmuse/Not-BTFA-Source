using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ForgottenMemories.NPCs.Energy.EnergyDweller
{
    public class EnergyDweller : ModNPC
    {
		protected int DashTimer = 0;
		protected int miscCounter = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Energy Dweller");
			Main.npcFrameCount[npc.type] = 6;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = 3;  
			npc.lifeMax = 50;	 
            npc.defense = 5;  
			npc.value = 500f;
			aiType = 73;
            npc.knockBackResist = 1.5f;
            npc.width = 28;
            npc.height = 36;
			npc.damage = 25;
            npc.lavaImmune = true;
            npc.noTileCollide = false;
            npc.HitSound = 	new Terraria.Audio.LegacySoundStyle(29, 48);
			npc.dontTakeDamage = false;
            npc.DeathSound = SoundID.NPCDeath43;
			banner = npc.type;
			bannerItem = mod.ItemType("EnergyDwellerBannerItem");
        }
		public override void AI()
        {
			Player player = Main.player[npc.target];
			npc.spriteDirection = npc.direction;
			
			DashTimer++;
			miscCounter++;
			Vector2 vector2 = new Vector2(npc.position.X + (float) npc.width * 0.5f, npc.position.Y + (float) npc.height * 0.5f);
			float num2 = Main.player[npc.target].position.X + (float) (Main.player[npc.target].width / 2) - vector2.X;
			float num3 = Main.player[npc.target].position.Y + (float) (Main.player[npc.target].height / 2) - vector2.Y;
			float num4 = 8f / (float) Math.Sqrt((double) num2 * (double) num2 + (double) num3 * (double) num3);
			npc.velocity.X = num2 * num4 * 0.5f;
			float distance = 1500f;
            if (player == Main.player[npc.target])
            {
                if ((npc.active && player.active && (double) Vector2.Distance(player.Center, npc.Center) <= (double) distance) && DashTimer >= 120)
                {
					DashTimer = 0;
					npc.velocity.X *= 3f;
					npc.velocity.Y *= 3f;
					Vector2 vector8 = new Vector2(npc.position.X + (npc.width * 0.5f), npc.position.Y + (npc.height * 0.5f));
					{
						float rotation = (float)Math.Atan2((vector8.Y) - (Main.player[npc.target].position.Y + (Main.player[npc.target].height * 0.5f)), (vector8.X) - (Main.player[npc.target].position.X + (Main.player[npc.target].width * 0.5f)));
						npc.velocity.X = (float)(Math.Cos(rotation) * 100) * -1;
					}
					for (int index1 = 0; index1 < 20; ++index1)
					{
						int index2 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 111, 0.0f, 0.0f, 100, new Color(), 2f);
						Main.dust[index2].position.X += (float) Main.rand.Next(-5, 6);
						Main.dust[index2].position.Y += (float) Main.rand.Next(-5, 6);
						Main.dust[index2].velocity *= 0.2f;
						Main.dust[index2].scale *= (float) (1.0 + (double) Main.rand.Next(20) * 0.00999999977648258);
						Main.dust[index2].noGravity = true;
						Main.dust[index2].fadeIn = 0.5f;
					}
				}
			}
			if ((double) npc.velocity.X != 0.0 && (double) npc.velocity.Y == 0.0 && miscCounter % 2 == 0)
			{
				for (int index1 = 0; index1 < 2; ++index1)
				{
					int index2 = index1 != 0 ? Dust.NewDust(new Vector2(npc.position.X + (float) (npc.width / 2), npc.position.Y + (float) npc.height + npc.gfxOffY), npc.width / 2, 6, 111, 0.0f, 0.0f, 0, new Color(), 1.35f) : Dust.NewDust(new Vector2(npc.position.X, npc.position.Y + (float) npc.height + npc.gfxOffY), npc.width / 2, 6, 111, 0.0f, 0.0f, 0, new Color(), 1.35f);
					Main.dust[index2].scale = 1f;
					Main.dust[index2].noGravity = true;
					Main.dust[index2].noLight = true;
					Main.dust[index2].velocity *= 1f / 1000f;
					Main.dust[index2].velocity.Y -= 3f / 1000f;
				}
			}			
        }
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Arachnergy_Gore_2"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Arachnergy_Gore_3"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Arachnergy_Gore_4"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Arachnergy_Gore_5"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Arachnergy_Gore_6"), 1f);
			}
		}
		public override void NPCLoot ()
		{
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
			const int Frame_5 = 4;
			const int Frame_6 = 5;
			
			if (DashTimer >= 120)
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
				else if (npc.frameCounter < 28)
				{
					npc.frame.Y = Frame_5 * frameHeight;
				}
				else if (npc.frameCounter < 32)
				{
					npc.frame.Y = Frame_6 * frameHeight;
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
			BTFAUtility.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/Energy/EnergyDweller/EnergyDwellerGlow"));
		}
    }
}