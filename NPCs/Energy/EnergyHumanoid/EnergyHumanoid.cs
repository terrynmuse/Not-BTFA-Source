using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ForgottenMemories.NPCs.Energy.EnergyHumanoid
{
    public class EnergyHumanoid : ModNPC
    {
		protected int shooterTimer = 0;
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Energy Humanoid");
			Main.npcFrameCount[npc.type] = 1;
		}
        public override void SetDefaults()
        {
            npc.aiStyle = 2;  
			npc.lifeMax = 100;	 
            npc.defense = 5;  
			npc.value = 500f;
            npc.knockBackResist = 0.1f;
            npc.width = 60;
            npc.height = 60;
			npc.damage = 40;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.noTileCollide = true;
            npc.HitSound = 	new Terraria.Audio.LegacySoundStyle(29, 48);
            npc.DeathSound = SoundID.NPCDeath43;
			NPCID.Sets.TrailCacheLength[npc.type] = 10;
			NPCID.Sets.TrailingMode[npc.type] = 3;
			banner = npc.type;
			bannerItem = mod.ItemType("EnergyHumanoidBannerItem");
        }
		public override void AI()
        {
			Player player = Main.player[npc.target];
			npc.spriteDirection = npc.direction;
			
			shooterTimer++;
			
			float distance = 1500f;
            if (player == Main.player[npc.target])
            {
                if ((npc.active && player.active && (double) Vector2.Distance(player.Center, npc.Center) <= (double) distance) && shooterTimer >= 180)
                {
					Main.PlaySound(SoundID.Item93, npc.position);
					shooterTimer = 0;
					Vector2 baklava = new Vector2(npc.Center.X - 11, npc.Center.Y - 6); 
					Vector2 baklava2 = new Vector2(npc.Center.X + 11, npc.Center.Y - 6); 
					float rotation = (float)Math.Atan2(baklava.Y - (player.position.Y + (player.height * 0.5f)), baklava.X - (player.position.X + (player.width * 0.5f)));
					float rotation2 = (float)Math.Atan2(baklava2.Y - (player.position.Y + (player.height * 0.5f)), baklava2.X - (player.position.X + (player.width * 0.5f)));
					if (npc.direction == -1)
					{
						Projectile.NewProjectile(baklava.X, baklava.Y, (float)((Math.Cos(rotation) * 16f) * -1), (float)((Math.Sin(rotation) * 16f) * -1), mod.ProjectileType("EnergyBlast"), 10, 0f, 0);
					}
					else if (npc.direction == 1)
					{
						Projectile.NewProjectile(baklava2.X, baklava2.Y, (float)((Math.Cos(rotation2) * 16f) * -1), (float)((Math.Sin(rotation) * 16f) * -1), mod.ProjectileType("EnergyBlast"), 10, 0f, 0);
					}
                }
			}
			
			for (int index1 = 0; index1 < 10; ++index1)
                {
					float x = (npc.Center.X - 11);
					float xnum2 = (npc.Center.X + 11);
					float y = (npc.Center.Y - 6);
					float ynum2 = (npc.Center.Y - 6);
					if (npc.direction == -1)
					{
						int index2 = Dust.NewDust(new Vector2(x, y), 1, 1, 111, 0.0f, 0.0f, 0, new Color(), 1f);
						Main.dust[index2].position.X = x;
						Main.dust[index2].position.Y = y;
						Main.dust[index2].scale = shooterTimer * 0.006f;
						Main.dust[index2].velocity *= 0.0f;
						Main.dust[index2].noGravity = true;
						Main.dust[index2].noLight = true;
					}
					else if (npc.direction == 1)
					{
						int index2 = Dust.NewDust(new Vector2(xnum2, ynum2), 1, 1, 111, 0.0f, 0.0f, 0, new Color(), 1f);
						Main.dust[index2].position.X = xnum2;
						Main.dust[index2].position.Y = ynum2;
						Main.dust[index2].scale = shooterTimer * 0.006f;
						Main.dust[index2].velocity *= 0.0f;
						Main.dust[index2].noGravity = true;
						Main.dust[index2].noLight = true;
					}
                }
        }
		public override void HitEffect(int hitDirection, double damage)
		{
			if (npc.life <= 0)
			{
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Energy_Humanoid_Gore"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Energy_Humanoid_Gore"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Energy_Humanoid_Gore_2"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Energy_Humanoid_Gore_2"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Energy_Humanoid_Gore_3"), 1f);
				Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/Energy_Humanoid_Gore_4"), 1f);
			}
		}
		public override void NPCLoot ()
		{		
			if (Main.rand.Next(2) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("EnergyRemnant"), Main.rand.Next(1, 5));
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
			BTFAUtility.DrawNPCGlowMask(spriteBatch, npc, mod.GetTexture("NPCs/Energy/EnergyHumanoid/EnergyHumanoidGlow"));
		}
    }
}