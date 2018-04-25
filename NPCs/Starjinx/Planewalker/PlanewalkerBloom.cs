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
    public class PlanewalkerBloom : ModNPC
    {
		public int scaleTimer = 0;
		Vector2 Location;
		Vector2 Location2;
        public override void SetDefaults()
        {
            npc.aiStyle = -1;
            npc.lifeMax = 10;
            npc.damage = 0;
            npc.defense = 0;
            npc.knockBackResist = 0f;
            npc.width = 30;
			npc.scale = 2.5f;
            npc.height = 2;
            npc.value = 0;
            npc.lavaImmune = true;
			npc.alpha = 240;
			npc.dontTakeDamage = true;
            npc.noTileCollide = true;
            npc.noGravity = true;
        }
		public override Color? GetAlpha(Color lightColor)
        {
			if (!Main.dayTime)
			{
				return new Color(0.25f, 0.25f, 0f);
			}
			else 
			{
				return new Color(0.15f, 0.15f, 0f);
			}
        }
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("");
		}
		
		public override bool? DrawHealthBar(byte hbPos, ref float scale, ref Vector2 Pos)
		{
			return false;
		}
		
        public override void AI()
        {
			npc.TargetClosest(true);
            Player player = Main.player[npc.target];
			npc.ai[2]++;
			Lighting.AddLight(npc.position, 0.2f, 0.2f, 0f);	
			if (npc.ai[0] == 0)
			{
				Location = npc.Center - Main.npc[(int)npc.ai[1]].Center;
				npc.position.Y = Main.npc[(int)npc.ai[1]].position.Y + 80;
				npc.ai[0]++;
			}
			else
			{
				npc.Center = Location + Main.npc[(int)npc.ai[1]].Center;
				npc.position.Y = Main.npc[(int)npc.ai[1]].position.Y + 80;
			}
			
			if (!NPC.AnyNPCs(mod.NPCType("Planewalker")))
			{
				npc.life = 0;
				npc.active = false;
			}
			
			scaleTimer+= 4;
			if (scaleTimer <= 80)
			{
				npc.scale = npc.scale - 0.05f;
			}
			if (scaleTimer > 81 && scaleTimer < 160)
			{
				npc.scale = npc.scale + 0.05f;
			}
			if (scaleTimer == 160)
			{
				scaleTimer = 0;
				npc.scale = 2.5f;
			}
		}
	}
}
