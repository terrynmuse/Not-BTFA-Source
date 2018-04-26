using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;
using Terraria.ID;

namespace ForgottenMemories.Projectiles.StarjinxWep  
{
	public class StarshineRed : ModProjectile
	{
		public override void SetDefaults()
		{
			projectile.width = 150;
			projectile.height = 150;
			projectile.penetrate = -1;
			projectile.aiStyle = 1;
			aiType = ProjectileID.Bullet;
			projectile.timeLeft = 10;
			projectile.magic = true;
			projectile.friendly = true;
			projectile.alpha = 255;
			projectile.tileCollide = false;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Red Starshine");
		}
		
		public override void AI()
		{
			Player player = Main.player[projectile.owner];
			int amountOfDust = (projectile.width/35);			
			for (int index2 = 0; index2 < Main.npc.Length; ++index2)
            {
                NPC npc = Main.npc[index2];
				if ((npc.active && !npc.friendly && (npc.damage > 0 && !npc.dontTakeDamage) && (double) Vector2.Distance(player.Center, npc.Center) <= (double) 300f))
					for (int index1 = 0; index1 < 1; ++index1)
					if (Main.rand.Next(3) == 0)
					{
						int dust;
						Vector2 newVect = new Vector2 (8, 0).RotatedBy(MathHelper.ToRadians(Main.rand.Next(45)));
						Vector2 newVect2 = newVect.RotatedBy(MathHelper.ToRadians(45));
						Vector2 newVect3 = newVect.RotatedBy(MathHelper.ToRadians(90));
						Vector2 newVect4 = newVect.RotatedBy(MathHelper.ToRadians(135));
						Vector2 newVect5 = newVect.RotatedBy(MathHelper.ToRadians(180));
						Vector2 newVect6 = newVect.RotatedBy(MathHelper.ToRadians(225));
						Vector2 newVect7 = newVect.RotatedBy(MathHelper.ToRadians(270));
						Vector2 newVect8 = newVect.RotatedBy(MathHelper.ToRadians(315));
						dust = Dust.NewDust(npc.Center, 0, 0, 90, newVect.X, newVect.Y);
						int dust2 = Dust.NewDust(npc.Center, 0, 0, 90, newVect2.X, newVect2.Y);//73
						int dust3 = Dust.NewDust(npc.Center, 0, 0, 90, newVect3.X, newVect3.Y);
						int dust4 = Dust.NewDust(npc.Center, 0, 0, 90, newVect4.X, newVect4.Y);
						int dust5 = Dust.NewDust(npc.Center, 0, 0, 90, newVect5.X, newVect5.Y);
						int dust6 = Dust.NewDust(npc.Center, 0, 0, 90, newVect6.X, newVect6.Y);
						int dust7 = Dust.NewDust(npc.Center, 0, 0, 90, newVect7.X, newVect7.Y);
						int dust8 = Dust.NewDust(npc.Center, 0, 0, 90, newVect8.X, newVect8.Y);
						Main.dust[dust].noGravity = true;
						Main.dust[dust2].noGravity = true;
						Main.dust[dust3].noGravity = true;
						Main.dust[dust4].noGravity = true;
						Main.dust[dust5].noGravity = true;
						Main.dust[dust6].noGravity = true;
						Main.dust[dust7].noGravity = true;
						Main.dust[dust8].noGravity = true;
						Main.dust[dust].scale = 2;
						Main.dust[dust2].scale = 2;
						Main.dust[dust3].scale = 2;
						Main.dust[dust4].scale = 2;
						Main.dust[dust5].scale = 2;
						Main.dust[dust6].scale = 2;
						Main.dust[dust7].scale = 2;
						Main.dust[dust8].scale = 2;
					}
			}
			float distance = 300f;
            if (player == Main.player[projectile.owner])
            {
                for (int index2 = 0; index2 < Main.npc.Length; ++index2)
                {
                    NPC npc = Main.npc[index2];
                    if ((npc.active && !npc.friendly && (npc.damage > 0 && !npc.dontTakeDamage) && (double) Vector2.Distance(player.Center, npc.Center) <= (double) distance))
                    {
						projectile.position.X = npc.position.X;
						projectile.position.Y = npc.position.Y;
						projectile.Damage();
                    }
                }
			}
		}
	}
}	