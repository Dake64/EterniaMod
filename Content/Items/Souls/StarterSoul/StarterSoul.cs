using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Eternia.Content.Items.Souls.StarterSoul
{
    public class StarterSoul : ModItem
    {
        public override void SetDefaults()
        {
            Item.width = 32;
            Item.height = 32;
            Item.rare = ItemRarityID.Green;
            Item.value = Item.sellPrice(gold: 1);
            Item.accessory = true;
        }

        public override void AddRecipes()
        {
            Recipe.Create(ModContent.ItemType<MeleeSoul.MeleeSoul>())
                .AddIngredient(this)
                .AddTile(TileID.WorkBenches)
                .Register();

            Recipe.Create(ModContent.ItemType<SummonerSoul.SummonerSoul>())
                .AddIngredient(this)
                .AddTile(TileID.WorkBenches)
                .Register();

            Recipe.Create(ModContent.ItemType<RangedSoul.RangedSoul>())
                .AddIngredient(this)
                .AddTile(TileID.WorkBenches)
                .Register();

            Recipe.Create(ModContent.ItemType<MageSoul.MageSoul>())
                .AddIngredient(this)
                .AddTile(TileID.WorkBenches)
                .Register();
        }
    }
}
