using System.Collections.Generic;
using Terraria;
using System.Collections.Generic;
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;

namespace ForgottenMemories.Buffs
{
    public class PhantomReapBuff : ModBuff
    {
        public override void SetDefaults()
        {
            Main.buffNoTimeDisplay[Type] = false;
			Main.debuff[Type] = false; 
            Main.pvpBuff[Type] = false; 
            Main.buffNoSave[Type] = true;
			DisplayName.SetDefault("Soul Absorber");
			Description.SetDefault("Increased attack and movement speed");
        }
		public override void Update(Player player, ref int buffIndex)
        {
			player.GetModPlayer<BTFAPlayer>().phantomReapBuff = true;
			player.moveSpeed = player.moveSpeed * 2f;

			if (Main.rand.NextFloat() < 1f)
			{
				Dust dust = Main.dust[Terraria.Dust.NewDust(player.position, player.width, player.height, 111, 0f, -10f, 150, new Color(255,255,255), 1.052632f)];
				dust.noGravity = true;
				dust.noLight = true;
			}
        }
    }
}