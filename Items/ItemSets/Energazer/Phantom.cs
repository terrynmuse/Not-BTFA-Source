using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ForgottenMemories.Items.ItemSets.Energazer
{
	public class Phantom : ModItem
	{
		public override void SetDefaults()
		{
			item.width = 2;
			item.height = 2;
			item.maxStack = 999;
			item.rare = 0;
			item.alpha = 255;
			ItemID.Sets.ItemIconPulse[item.type] = true;
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}

		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Phantom");
		}
		public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
		public override void GrabRange (Player player, ref int grabRange)
		{
			grabRange *= 10;
		}
		public override bool ItemSpace (Player player)
		{
			return true;
		}
		public override bool OnPickup (Player player)
		{
			Main.PlaySound(new Terraria.Audio.LegacySoundStyle(42, 50));
			player.AddBuff(mod.BuffType("PhantomReapBuff"), 4*60);
			return false;
		}
		public override void PostDrawInWorld (SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			if (item.velocity.X != 0f && item.velocity.Y != 0f)
			{
				for (int index1 = 0; index1 < 10; ++index1)
				{
                    float x = item.position.X - item.velocity.X / 10f * (float) index1;
                    float y = item.position.Y - item.velocity.Y / 10f * (float) index1;
                    int index2 = Dust.NewDust(new Vector2(x, y), 1, 1, 111, 0.0f, 0.0f, 0, new Color(), 1f);
                    Main.dust[index2].position.X = x;
                    Main.dust[index2].position.Y = y;
			        Main.dust[index2].scale = (float) Main.rand.Next(70, 70) * 0.013f;
                    Main.dust[index2].velocity *= 0.0f;
                    Main.dust[index2].noGravity = true;
				}
			}
			if (item.velocity.X == 0f && item.velocity.Y == 0f && Main.rand.Next(15) == 0)
			{
				int index = Dust.NewDust(item.position, item.width, item.height, 111, 0.0f, 0.0f, 200, new Color(), 0.5f);
				Main.dust[index].noGravity = true;
				Main.dust[index].velocity *= 0.75f;
				Main.dust[index].fadeIn = 1.3f;
				Vector2 vector2_1 = new Vector2((float) Main.rand.Next(-100, 101), (float) Main.rand.Next(-100, 101));
				vector2_1.Normalize();
				Vector2 vector2_2 = vector2_1 * ((float) Main.rand.Next(50, 100) * 0.04f);
				Main.dust[index].velocity = vector2_2;
				vector2_2.Normalize();
				Vector2 vector2_3 = vector2_2 * 34f;
				Main.dust[index].position = item.Center - vector2_3;
			}
		}
	}
}
