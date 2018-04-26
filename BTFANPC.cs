using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using System;
using Terraria.ModLoader;
using ForgottenMemories;

namespace ForgottenMemories
{
	public class BTFANPC : GlobalNPC
	{
		int DagNum = 0;
		public bool BlightCelled = false;
		public bool FilicidCelled = false;
		public bool BloodLeech = false;
		public bool MarbleArrow = false;
		public bool Necro = false;
		public bool boilingBlood = false;
		public int boilingBloodCounter = 7;
		public bool boilingBlood2 = false;
		public bool groped = false;
		public bool titanCrush = false;
		public bool bleeding = false;
		public bool blightFlame = false;
		public int blightChakramHits = 0;
		
		public override void ResetEffects(NPC npc)
        {
            DagNum = 0;
			BlightCelled = false;
			FilicidCelled = true;
			BloodLeech = false;
			MarbleArrow = false;
			Necro = false;
			boilingBlood = false;
			boilingBlood2 = false;
			groped = false;
			titanCrush = false;
			bleeding = false;
			blightFlame = false;
        } 
		
		public override bool InstancePerEntity {get{return true;}}
		
		public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
		{
			if (player.GetModPlayer<BTFAPlayer>(mod).isGlitch)
			{
				spawnRate = (int)(spawnRate * 50f);
				maxSpawns = (int)(maxSpawns * 50f);
			}
			
			if(TGEMWorld.forestInvasionUp && (Main.invasionX == (double)Main.spawnTileX))
            {
                spawnRate = 50; 
                maxSpawns = 50; 
            }
		}
		
		public override void PostAI(NPC npc)
        {
            if(TGEMWorld.forestInvasionUp && (Main.invasionX == (double)Main.spawnTileX))
            {
                npc.timeLeft = 1000;
            }
        }

		public override bool StrikeNPC (NPC npc, ref double damage, int defense, ref float knockback, int hitDirection, ref bool crit)
		{
			if (boilingBlood2)
				damage *= 1.1;
			
			return true;
		}
		
		public override void DrawEffects(NPC npc, ref Color drawColor)
		{
			if (boilingBlood) //PLACEHOLDER DUST, PLEASE SPRAY YOUR MAGIC DUST JUICE ON THIS
			{
				if (Main.rand.Next(7) < 6)
                {
                    int dust = Dust.NewDust(npc.position - new Vector2(2f, 2f), npc.width + 4, npc.height + 4, mod.DustType("BloodDust2"), npc.velocity.X * 0.4f, npc.velocity.Y * 0.4f, 44, default(Color), 3.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 1.2f;
                    Main.dust[dust].velocity.Y -= 0.15f;
                }
				Lighting.AddLight(npc.position, 0.5f, 0, 0);
			}
		}

		public override void UpdateLifeRegen(NPC npc, ref int damage)
        {
			if (blightFlame)
			{
				npc.lifeRegen -= 40;
				
				if (damage < 10)
				{
					damage = 10;
				}
			}
			
			if (titanCrush)
			{
				npc.lifeRegen -= 20;
				
				if (damage < 20)
				{
					damage = 20;
				}
			}
			
			/*if (npc.FindBuffIndex(mod.BuffType("Frostburn2")) >= 0) //THIS DOESNT EVEN EXIST
			{
				if (damage < 10)
				{
					damage = 10;
				}
			}*/
			
			if (bleeding)
			{
				npc.lifeRegen -= 8;
				
				if (damage < 4)
				{
					damage = 4;
				}
			}
			
			if (BlightCelled)
			{
				if (npc.lifeRegen > 0)
					npc.lifeRegen = 0;
				int num = 0;
				for (int index = 0; index < 1000; ++index)
				{
				  if (Main.projectile[index].active && Main.projectile[index].type == mod.ProjectileType("BlightOrbShoot") && ((double) Main.projectile[index].ai[0] == 1.0 && (double) Main.projectile[index].ai[1] == (double) npc.whoAmI))
					++num;
				}
				npc.lifeRegen -= num * 4 * 5;
				if (damage < num * 5)
					damage = num * 5;
			}
			
			if (FilicidCelled)
			{
				int num = 0;
				for (int index = 0; index < 1000; ++index)
				{
				  if (Main.projectile[index].active && Main.projectile[index].type == mod.ProjectileType("FilicidCellRanged") && ((double) Main.projectile[index].ai[0] == 1.0 && (double) Main.projectile[index].ai[1] == (double) npc.whoAmI))
					++num;
				}
				npc.defense -= num * 2;
			}
			
			if (BloodLeech)
			{
				if (npc.lifeRegen > 0)
					npc.lifeRegen = 0;
				int num = 0;
				DagNum = num;
				for (int index = 0; index < 1000; ++index)
				{
				  if (Main.projectile[index].active && Main.projectile[index].type == mod.ProjectileType("BloodLeech") && ((double) Main.projectile[index].ai[0] == 1.0 && (double) Main.projectile[index].ai[1] == (double) npc.whoAmI))
					++num;
				}
				npc.lifeRegen -= num * 10 * 2;
				if (damage < num)
					damage = num;
			}
			
			if (MarbleArrow)
			{
				if (npc.lifeRegen > 0)
					npc.lifeRegen = 0;
				int num = 0;
				for (int index = 0; index < 1000; ++index)
				{
				  if (Main.projectile[index].active && Main.projectile[index].type == mod.ProjectileType("MarbleArrow") && ((double) Main.projectile[index].ai[0] == 1.0 && (double) Main.projectile[index].ai[1] == (double) npc.whoAmI))
					++num;
				}
				npc.lifeRegen -= num * 4 * 5;
				if (damage < num * 5)
					damage = num * 5;
			}
			
			if (Necro)
			{
				if (npc.lifeRegen > 0)
					npc.lifeRegen = 0;
				int num = 0;
				for (int index = 0; index < 1000; ++index)
				{
				  if (Main.projectile[index].active && Main.projectile[index].type == mod.ProjectileType("NecroDagger") && ((double) Main.projectile[index].ai[0] == 1.0 && (double) Main.projectile[index].ai[1] == (double) npc.whoAmI))
					++num;
					npc.AddBuff(mod.BuffType("NecroDagger"), 180, false);
				}
				npc.lifeRegen -= num * 4 * 3;
				if (damage < num * 3)
					damage = num * 3;
			}

			if (boilingBlood)
			{
				if (npc.lifeRegen > 0)
				{
					npc.lifeRegen = 0;
				}
				npc.lifeRegen -= boilingBloodCounter;
				if (damage < 14)
				{
					damage = 14;
				}
			}
			else
			{
				boilingBloodCounter = 7;
			}

			if (groped)
			{
				if (npc.lifeRegen > 0)
					npc.lifeRegen = 0;
				npc.lifeRegen -= 40;
				if (damage < 4)
					damage = 4;
			}
		}
		
		public override void SetupShop(int type, Chest shop, ref int nextSlot) // add items to npc shops
		{
			if (type == 19) // arms dealer
			{
				if (NPC.downedBoss2) // check if EoW/BoC is defeated
				{
					shop.item[nextSlot].SetDefaults(mod.ItemType("psychic_pistol")); // sell psychic pistol
					nextSlot++;
				}
				if (NPC.downedAncientCultist)
				{
					shop.item[nextSlot].SetDefaults(mod.ItemType("VengeanceBullet"));
					nextSlot++;
				}
				
				for (int i = 0; i < 200; i++)
				{
					Player player = Main.player[i];
					if (player.HasItem(mod.ItemType("AncientLauncher")))
					{
						shop.item[nextSlot].SetDefaults(771);
						nextSlot++;
					}
				}
			}
			
			if (type == 124)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("MechanicsHammer"));
                nextSlot++;
            }
			
			switch (type)
            {	
				case NPCID.WitchDoctor:
                {				
			        if (NPC.downedBoss2)
                    {
                        shop.item[nextSlot].SetDefaults(ItemID.WormholePotion);
                        nextSlot++;
                    }
				    break;
				}
				
				case NPCID.SkeletonMerchant:
                {				
			        if (NPC.downedBoss3)
                    {
                        shop.item[nextSlot].SetDefaults(mod.ItemType("BoneFungus"));
                        nextSlot++;
                    }
				    break;
                }
            }
		}
		
		public override void NPCLoot(NPC npc)
		{
			if (BloodLeech == true)
			{
				for (int i = 0; i <= DagNum; i++)
				{
					int p = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("BloodBoom"), 50, 0f, Main.player[npc.target].whoAmI, 0f, 0f);
					Main.projectile[p].melee = false;
					Main.projectile[p].thrown = true;
					Main.projectile[p].usesLocalNPCImmunity = true;
					Main.projectile[p].localNPCHitCooldown = 10;
				}
			}

			if (boilingBlood)
			{
				int p = Projectile.NewProjectile(npc.position.X, npc.position.Y, 0, 0, mod.ProjectileType("BloodBall"), 84, 4.4f, Main.myPlayer, 0f, 1f);
				Main.projectile[p].Kill();
			}

			if (Main.player[npc.target].active && Main.player[npc.target].HeldItem.type == mod.ItemType("PhantomReap") && Main.rand.Next(2) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Phantom"));
			}
			
			if(TGEMWorld.forestInvasionUp)
            {
                foreach(int invader in CustomInvasion.invaders)
                {
                    if(npc.type == invader)
                    {
                        Main.invasionSize -= 2;
						if (Main.rand.Next(3) == 0)
						{
							Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ForestEnergy"), 1); 
						}
                    }
					if(npc.type == mod.NPCType("MagnoliacSecondStage") && !Main.hardMode)
					{
						Player player = Main.player[npc.target];
						Main.invasionSize -= 1000;
						player.GetModPlayer<BTFAPlayer>().MagnoliacBool = false;
					}
					if(npc.type == mod.NPCType("MagnoliacSecondStage") && Main.hardMode)
					{
						Player player = Main.player[npc.target];
						player.GetModPlayer<BTFAPlayer>().MagnoliacBool = false;
					}
					if(npc.type == mod.NPCType("GhastlyEnt") && Main.hardMode)
					{
						Player player = Main.player[npc.target];
						player.GetModPlayer<BTFAPlayer>().GentBool = false;
						Main.invasionSize -= 1000;
					}
                }
            }
			
			if (npc.type == NPCID.KingSlime && !Main.expertMode && Main.rand.Next(8) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SlimeRod"), 1); 
			}
			
			if (npc.type == 32 && Main.rand.Next(3) == 0)
			{
				int amountToDrop = Main.rand.Next(1,2);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WaterShard"), amountToDrop); 
			}
			
			if (npc.type == NPCID.AngryNimbus && Main.rand.Next(25) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("LightningDagger"), 1); 
			}
			
			if (npc.type == 204 && Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JungleSlimePendant"), 1); 
			}
			
			if (npc.type == 147 && Main.rand.Next(50) == 0 || npc.type == 184 && Main.rand.Next(40) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("IceSlimeNecklace"), 1); 
			}
			
			if (npc.type == 533 && Main.rand.Next(20) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DesertStaff"), 1); 
			}
			
			if (npc.type == 471)
			{
				int amountToDrop = Main.rand.Next(9,14);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ShadowflameEmber"), amountToDrop); 
			}
			
			if (npc.type == NPCID.WallofFlesh && !Main.expertMode)
			{
				int amountToDrop = Main.rand.Next(10,15);
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BrassAlloy"), amountToDrop); 
				
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MinerEmblem"), 1); 
				}
				
				if (Main.rand.Next(4) == 1)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ShinobiEmblem"), 1); 
				}
				
				if (Main.rand.Next(4) == 2)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BerserkerEmblem"), 1); 
				}
				
				if (Main.rand.Next(4) == 3)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PaladinEmblem"), 1); 
				}
			}
			
			if (npc.type == NPCID.WallofFlesh && !Main.expertMode)
			{
				if (Main.rand.Next(4) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ClubMeat"), 1); 
				}
			}
			
			if (npc.type == 4 && !Main.expertMode)
			{
				switch (Main.rand.Next(2))
				{
				case 0: Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ServantStaff"), 1); 
					break;
				case 1: Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ThirdEye"), 1); 
					break;
				}
			}
			
			if (npc.type == 439)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("VortexSphere"), 1); 
			}
			
			if (npc.type == 483 && Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Tourmaline"), Main.rand.Next(1, 3)); 
			}
			
			if (npc.type == 481 && Main.rand.Next(10) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Citrine"), Main.rand.Next(1, 3)); 
			}
			
			if (npc.type == NPCID.KingSlime/* && NPC.downedSlimeKing && Main.rand.Next(10) == 0*/)
			{
				if (!TGEMWorld.Gelatine || Main.rand.Next(10) == 0)
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SlimeCrystal"), 1);
			}
			
			if ((npc.type == 13 || npc.type == 14 || npc.type == 15) && npc.boss == true || npc.type == 266)
			{
				if (!TGEMWorld.Cryotine || Main.rand.Next(10) == 0)
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CryoCrystal"), 1);
			}
			
			if (npc.type == 262)
			{
				if (!TGEMWorld.Blight || Main.rand.Next(10) == 0)
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("BlightCrystal"), 1); 
			}

			if (Main.invasionType == 4 && (npc.type == 381 || npc.type == 382 || npc.type == 383 || npc.type == 385 || npc.type == 386 || npc.type == 389 || npc.type == 390) && Main.rand.Next(50) == 0)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("MartianYoyo"), 1); 
			}

			if (npc.value > 0.0 && Main.rand.Next(5) == 0 && Main.player[(int) Player.FindClosest(npc.position, npc.width, npc.height)].ZoneUnderworldHeight)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("DevilFlame"), 1); 
			}
			
			if (npc.value > 0.0 && Main.rand.Next(25) == 0 && Main.player[(int) Player.FindClosest(npc.position, npc.width, npc.height)].ZoneUnderworldHeight)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Spinel"), 1); 
			}
			
			if (npc.value > 0.0 && Main.rand.Next(60) == 0 && Main.player[(int) Player.FindClosest(npc.position, npc.width, npc.height)].ZoneBeach)
			{
				Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Pearl"), 1); 
			}
			
			if (Main.hardMode && (double) npc.value > 0.0)
			{
				if (Main.rand.Next(4) == 0 && Main.player[(int) Player.FindClosest(npc.position, npc.width, npc.height)].ZoneDesert)
					Item.NewItem((int) npc.position.X, (int) npc.position.Y, npc.width, npc.height, mod.ItemType("SpiritflameChunk"), 1, false, 0, false, false);
			}
			
			if (npc.type == NPCID.ManEater)
			{
				if (Main.rand.Next(30) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ExoticBoquet"), Main.rand.Next(1, 1));
				}
			}
			
			if (npc.type == NPCID.Snatcher)
			{
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ExoticBoquet"), Main.rand.Next(1, 1));
				}
			}
			if (npc.type == NPCID.Reaper)
			{
				if (Main.rand.Next(17) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SubmergedAshes"), Main.rand.Next(1, 1));
				}
			}
			if (npc.type == NPCID.Skeleton)
			{
				if (Main.rand.Next(20) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GooRattle"), Main.rand.Next(1, 1));
				}
			}
			if (npc.type == NPCID.CaveBat)
			{
				if (Main.rand.Next(30) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WhackTree"), Main.rand.Next(1, 1));
				}
			}
			if (npc.type == NPCID.GiantWormHead)
			{
				if (Main.rand.Next(10) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Axle"), Main.rand.Next(1, 1));
				}
			}
			if (npc.type == NPCID.DarkCaster)
			{
				if (Main.rand.Next(18) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Atlantean"), Main.rand.Next(1, 1));
				}
			}
			if (npc.type == NPCID.Piranha)
			{
				if (Main.rand.Next(50) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ExoticBouquet"), Main.rand.Next(1, 1));
				}
			}
			if (npc.type == NPCID.DiggerHead)
			{
				if (Main.rand.Next(30) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Axle"), Main.rand.Next(1, 1));
				}
			}
			if (npc.type == NPCID.GiantBat)
			{
				if (Main.rand.Next(30) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("WhackTree"), Main.rand.Next(1, 1));
				}
			}
			if (npc.type == NPCID.BloodCrawler)
			{
				if (Main.rand.Next(12) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("Spiderbook"), Main.rand.Next(1, 1));
				}
			}
			if (npc.type == NPCID.Crab)
			{
				if (Main.rand.Next(36) == 0)
				{
					Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ClubFish"), Main.rand.Next(1, 1));
				}
			}
		}
		
		public override void EditSpawnPool(IDictionary< int, float > pool, NPCSpawnInfo spawnInfo)
        {
            if(TGEMWorld.forestInvasionUp && (Main.invasionX == (double)Main.spawnTileX))
            {
                pool.Clear();
				
                foreach(int i in CustomInvasion.invaders)
                {
                    pool.Add(i, 1f);
                }
				
				if (Main.hardMode)
				{
					foreach(int i in CustomInvasion.hmInvaders)
					{
						pool.Add(i, 1f);
					}
				}
            }
        }
	}
}
