﻿using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace ForgottenMemories.Buffs
{
	public class CosmicGift : ModBuff
	{
		public override void SetDefaults()
		{
			DisplayName.SetDefault("Cosmic Gift");
			Description.SetDefault("Damage and movement speed increased, defensive stats decreased");
			Main.buffNoTimeDisplay[Type] = true;
		}
		
		public override void Update(Player player, ref int buffIndex)
		{
			player.statDefense -= 12;
			player.endurance -= 0.1f;
			player.lifeRegen -= 3;
			
			player.magicDamage += 0.15f;
			player.meleeDamage += 0.15f;
			player.rangedDamage += 0.15f;
			player.minionDamage += 0.15f;
			player.thrownDamage += 0.15f;
			player.moveSpeed += 0.3f;

			player.meleeCrit += 10;
			player.rangedCrit += 10;
			player.magicCrit += 10;
			player.thrownCrit += 10;
			
			if (Main.rand.Next(5) == 0)
			{
				int num5 = Dust.NewDust(player.position, player.width, player.height, 15, 0f, 0f, 200, default(Color), 0.5f);
				Main.dust[num5].noGravity = true;
				Main.dust[num5].velocity *= 0.75f;
				Main.dust[num5].fadeIn = 1.3f;
				Vector2 vector = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
				vector.Normalize();
				vector *= (float)Main.rand.Next(50, 100) * 0.04f;
				Main.dust[num5].velocity = vector;
				//vector.Normalize();
				//vector *= 34f;
				Main.dust[num5].position = player.Center;
			}
		}
	}
}