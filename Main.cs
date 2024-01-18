using GTA;
using GTA.Native;
using System;
using System.Windows.Forms;

namespace OnlyFavorites
{
    public class Main : Script
    {
        private readonly FavoriteMenu FavoriteMenu;
        private ScriptSettings scriptSetting;
        private readonly Keys MenuKey;

        public Main()
        {
            scriptSetting = ScriptSettings.Load("scripts\\OnlyFavorites.ini");
            MenuKey = scriptSetting.GetValue("MENU", "OpenKey", Keys.F10);
            var EnableByDefault = scriptSetting.GetValue("MENU", "EnabledByDefault", false);
            FavoriteMenu = new FavoriteMenu(EnableByDefault, new FavoriteSetting(scriptSetting, PedHash.Michael), new FavoriteSetting(scriptSetting, PedHash.Trevor), new FavoriteSetting(scriptSetting, PedHash.Franklin));
            FavoriteMenu.SaveConfig += HandleSave;
            Tick += OnTick;
            KeyDown += OnKeyDown;
        }

        private void HandleSave(FavoriteSetting favoriteSetting)
        {
            favoriteSetting.UpdateScriptSettings(scriptSetting, (PedHash)Game.Player.Character.Model.Hash);
            scriptSetting.Save();
        }

        private void OnTick(object sender, EventArgs e)
        {
            FavoriteMenu.OnTick();

            var currentSetting = FavoriteMenu.GetMenuSettings();
            if (currentSetting != null)
            {
                foreach (WeaponHash weaponHash in Enum.GetValues(typeof(WeaponHash)))
                {
                    if (weaponHash == WeaponHash.Unarmed)
                    {
                        continue;
                    }
                    if (Function.Call<bool>(Hash.HAS_PED_GOT_WEAPON, Game.Player.Character, weaponHash, false) && !currentSetting.Contains(weaponHash))
                    {
                        if (weaponHash == WeaponHash.Parachute)
                        {
                            Function.Call(Hash.REMOVE_WEAPON_FROM_PED, Game.Player.Character, weaponHash);
                        }
                        else
                        {
                            Function.Call(Hash.SET_PED_DROPS_INVENTORY_WEAPON, Game.Player.Character, weaponHash, 0, 0, 0, 0);
                        }
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
