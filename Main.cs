using GTA;
using GTA.Native;
using System;
using System.Windows.Forms;

namespace OnlyFavorites
{
    public class Main : Script
    {
        private readonly FavoriteMenu FavoriteMenu;
        private Keys MenuKey;

        public Main()
        {
            var Config = ScriptSettings.Load("scripts\\OnlyFavorites.ini");
            MenuKey = Config.GetValue("KEY", "OpenMenu", Keys.F8);
            FavoriteMenu = new FavoriteMenu(Config);
            Tick += OnTick;
            KeyDown += OnKeyDown;
        }

        private void OnTick(object sender, EventArgs e)
        {
            FavoriteMenu.OnTick();

            var selectedHashes = FavoriteMenu.GetHashes();
            if (selectedHashes != null && selectedHashes.Length > 0)
            {
                foreach (uint weaponHash in Enum.GetValues(typeof(WeaponHash)))
                {
                    if(weaponHash == (uint)WeaponHash.Unarmed)
                    {
                        continue;
                    }
                    if (Function.Call<bool>(Hash.HAS_PED_GOT_WEAPON, Game.Player.Character, weaponHash, false) && Array.IndexOf(selectedHashes, (WeaponHash)weaponHash) == -1)
                    {
                        Function.Call(Hash.SET_PED_DROPS_INVENTORY_WEAPON, Game.Player.Character, weaponHash, 0, 0, 0, 0);
                    }
                }
            }
        }

        private void OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == MenuKey)
            {
                FavoriteMenu.ToggleMenu();
            }
        }
    }
}
