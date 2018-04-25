using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Events;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.Utilities;
using Terraria.World.Generation;
using Terraria.ModLoader;

namespace ForgottenMemories.NPCs.Starjinx.Planewalker
{
    public class PlanewalkerGuardianGreen : ModNPC
    {
		Vector2 Location;
		Vector2 Location2;
		protected int shootTimer = 0;
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 10;
            npc.damage = 0;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 14;
            npc.height = 14;
            npc.value = 0;
            npc.lavaImmune = true;
			npc.alpha = 255;
            npc.noTileCollide = true;
            npc.noGravity = true;
            npc.HitSound = new Terraria.Audio.LegacySoundStyle(42, 166);
        }
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Planewalker Guardian");
		}
		
		public override void ModifyHitByProjectile(Projectile projectile, ref int damage, ref float knockback, ref bool crit, ref int hitDirection )		
		{
			if (!projectile.minion)
				projectile.penetrate = 0;
			
			damage = 0;
		}
		
		public override bool? DrawHealthBar(byte hbPos, ref float scale, ref Vector2 Pos)
		{
			return false;
		}
		
        public override void AI()
        {
			shootTimer++;
			npc.TargetClosest(true);
            Player player = Main.player[npc.target];
			npc.ai[2]++;
			
			if (npc.ai[0] == 0)
			{
				Location = npc.Center - Main.npc[(int)npc.ai[1]].Center;
				Location2 = npc.Center - Main.npc[(int)npc.ai[1]].Center;
				npc.ai[0]++;
			}
			else
			{
				Location2 = Location.RotatedBy((MathHelper.Pi / 180));
				Location = Location2;
				npc.Center = Location + Main.npc[(int)npc.ai[1]].Center;
			}
			
			if (!NPC.AnyNPCs(mod.NPCType("Planewalker")))
			{
				npc.life = 0;
			}
			
			for (int index1 = 0; index1 < 10; ++index1)
                {
                    float x = npc.position.X - npc.velocity.X / 100f * (float) index1;
                    float y = npc.position.Y - npc.velocity.Y / 100f * (float) index1;
                    int index2 = Dust.NewDust(new Vector2(x, y), 1, 1, 61, 0.0f, 0.0f, 0, new Color(), 1f);
                    Main.dust[index2].position.X = x;
                    Main.dust[index2].position.Y = y;
			        Main.dust[index2].scale = (float) Main.rand.Next(80, 80) * 0.013f;
                    Main.dust[index2].velocity *= 0.0f;
                    Main.dust[index2].noGravity = true;
                    Main.dust[index2].noLight = false;
                }
				
			float distance = 1500f;
            if (player == Main.player[npc.target])
            {
                if ((npc.active && player.active && (double) Vector2.Distance(player.Center, npc.Center) <= (double) distance) && shootTimer >= 120)
                {
					Main.PlaySound(new Terraria.Audio.LegacySoundStyle(42, 133));
					shootTimer = 0;
					float Speed = 8f; 
					Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 4), npc.position.Y + (npc.height / 4));
					int damage = 10; 
					int type = mod.ProjectileType("GreenBlast"); 
					float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
					int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                }
			}
		}
	}
}
