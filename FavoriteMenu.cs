using GTA;
using LemonUI;
using LemonUI.Menus;
using System.Windows.Forms;

namespace OnlyFavorites
{
    internal class FavoriteMenu
    {
        private ObjectPool pool;
        public readonly NativeMenu Menu;
        private NativeCheckboxItem modEnabledCheckbox;
        private NativeListItem<WeaponHash> meleeSelector;
        private NativeListItem<WeaponHash> pistolSelector;
        private NativeListItem<WeaponHash> shotgunSelector;
        private NativeListItem<WeaponHash> machineGunSelector;
        private NativeListItem<WeaponHash> assultRifleSelector;
        private NativeListItem<WeaponHash> sniperRifleSelector;
        private NativeListItem<WeaponHash> heavyFirearmsSelector;
        private NativeListItem<WeaponHash> explosionSelector;
        // private NativeListItem<WeaponHash> specialSelector;
        private NativeItem saveButton;
        private ScriptSettings _config;

        public FavoriteMenu(ScriptSettings config)
        {
            pool = new ObjectPool();
            Menu = new NativeMenu("OnlyFavorites", "Choose your favorites only");
            this._config = config;
            SetMenuItem(Menu);
            pool.Add(Menu);
            saveButton.Selected += save;
        }

        private void SetMenuItem(NativeMenu menu)
        {
            modEnabledCheckbox = new NativeCheckboxItem("Enabled");
            menu.Add(modEnabledCheckbox);

            // Melee
            meleeSelector = new NativeListItem<WeaponHash>("Melee", WeaponHash.Unarmed, WeaponHash.Knife, WeaponHash.Nightstick, WeaponHash.Hammer, WeaponHash.Bat, WeaponHash.Crowbar, WeaponHash.GolfClub, WeaponHash.Bottle, WeaponHash.Dagger, WeaponHash.BattleAxe, WeaponHash.KnuckleDuster, WeaponHash.Flashlight, WeaponHash.Machete, WeaponHash.SwitchBlade, WeaponHash.PoolCue, WeaponHash.Wrench, WeaponHash.BattleAxe, WeaponHash.StoneHatchet);
            meleeSelector.Description = $"{meleeSelector.Items.Count} Options";
            meleeSelector.SelectedItem = _config.GetValue("FAVORITES", "Melee", WeaponHash.Unarmed);
            menu.Add(meleeSelector);

            // Pistol
            pistolSelector = new NativeListItem<WeaponHash>("Pistol", WeaponHash.Unarmed, WeaponHash.Pistol, WeaponHash.CombatPistol, WeaponHash.APPistol, WeaponHash.Pistol50, WeaponHash.SNSPistol, WeaponHash.HeavyPistol, WeaponHash.VintagePistol, WeaponHash.MarksmanPistol, WeaponHash.StunGun, WeaponHash.Revolver, WeaponHash.PistolMk2, WeaponHash.SNSPistolMk2, WeaponHash.RevolverMk2, WeaponHash.DoubleActionRevolver, WeaponHash.FlareGun, WeaponHash.UpNAtomizer, WeaponHash.CeramicPistol, WeaponHash.NavyRevolver, WeaponHash.PericoPistol, WeaponHash.WM29Pistol);
            pistolSelector.Description = $"{pistolSelector.Items.Count} Options";
            pistolSelector.SelectedItem = _config.GetValue("FAVORITES", "Pistol", WeaponHash.Unarmed);
            menu.Add(pistolSelector);

            // Shotgun
            shotgunSelector = new NativeListItem<WeaponHash>("Shotgun", WeaponHash.Unarmed,WeaponHash.SawnOffShotgun, WeaponHash.PumpShotgun, WeaponHash.AssaultShotgun, WeaponHash.BullpupShotgun, WeaponHash.Musket, WeaponHash.HeavyShotgun, WeaponHash.DoubleBarrelShotgun, WeaponHash.SweeperShotgun, WeaponHash.PumpShotgunMk2, WeaponHash.CombatShotgun);
            shotgunSelector.Description = $"{shotgunSelector.Items.Count} Options";
            shotgunSelector.SelectedItem = _config.GetValue("FAVORITES", "Shotgun", WeaponHash.Unarmed);
            menu.Add(shotgunSelector);

            // Machine Gun
            machineGunSelector = new NativeListItem<WeaponHash>("MG", WeaponHash.Unarmed, WeaponHash.MicroSMG, WeaponHash.SMG, WeaponHash.AssaultSMG, WeaponHash.CombatPDW, WeaponHash.MachinePistol, WeaponHash.MiniSMG, WeaponHash.SMGMk2, WeaponHash.MG, WeaponHash.CombatMG, WeaponHash.Gusenberg, WeaponHash.CombatMGMk2, WeaponHash.UnholyHellbringer);
            machineGunSelector.Description = $"{machineGunSelector.Items.Count} Options";
            machineGunSelector.SelectedItem = _config.GetValue("FAVORITES", "MG", WeaponHash.Unarmed);
            menu.Add(machineGunSelector);

            // Assult Rifle
            assultRifleSelector = new NativeListItem<WeaponHash>("Rifle", WeaponHash.Unarmed, WeaponHash.AssaultRifle, WeaponHash.CarbineRifle, WeaponHash.AdvancedRifle, WeaponHash.BullpupRifle, WeaponHash.SpecialCarbine, WeaponHash.CompactRifle, WeaponHash.AssaultrifleMk2, WeaponHash.CarbineRifleMk2, WeaponHash.SpecialCarbineMk2, WeaponHash.BullpupRifleMk2, WeaponHash.MilitaryRifle, WeaponHash.HeavyRifle, WeaponHash.ServiceCarbine);
            assultRifleSelector.Description = $"{assultRifleSelector.Items.Count} Options";
            assultRifleSelector.SelectedItem = _config.GetValue("FAVORITES", "Rifle", WeaponHash.Unarmed);
            menu.Add(assultRifleSelector);

            // Sniper Rifle
            sniperRifleSelector = new NativeListItem<WeaponHash>("Sniper", WeaponHash.Unarmed, WeaponHash.SniperRifle, WeaponHash.HeavySniper, WeaponHash.MarksmanRifle, WeaponHash.HeavySniperMk2, WeaponHash.MarksmanRifleMk2, WeaponHash.PrecisionRifle);
            sniperRifleSelector.Description = $"{sniperRifleSelector.Items.Count} Options";
            sniperRifleSelector.SelectedItem = _config.GetValue("FAVORITES", "Sniper", WeaponHash.Unarmed);
            menu.Add(sniperRifleSelector);

            // Heavy Firearms
            heavyFirearmsSelector = new NativeListItem<WeaponHash>("Heavy", WeaponHash.Unarmed, WeaponHash.GrenadeLauncher, WeaponHash.RPG, WeaponHash.Firework,WeaponHash.HomingLauncher, WeaponHash.Minigun, WeaponHash.Railgun, WeaponHash.CompactGrenadeLauncher, WeaponHash.Widowmaker, WeaponHash.CompactEMPLauncher);
            heavyFirearmsSelector.Description = $"{heavyFirearmsSelector.Items.Count} Options";
            heavyFirearmsSelector.SelectedItem = _config.GetValue("FAVORITES", "Heavy", WeaponHash.Unarmed);
            menu.Add(heavyFirearmsSelector);

            // Explosion
            explosionSelector = new NativeListItem<WeaponHash>("Explosion", WeaponHash.Unarmed, WeaponHash.Grenade, WeaponHash.StickyBomb, WeaponHash.ProximityMine, WeaponHash.BZGas, WeaponHash.Molotov, WeaponHash.PetrolCan, WeaponHash.PipeBomb, WeaponHash.Flare, WeaponHash.Ball, WeaponHash.Snowball, WeaponHash.HazardousJerryCan, WeaponHash.FertilizerCan);
            explosionSelector.Description = $"{explosionSelector.Items.Count} Options";
            explosionSelector.SelectedItem = _config.GetValue("FAVORITES", "Explosion", WeaponHash.Unarmed);
            menu.Add(explosionSelector);

            // Special
            //specialSelector = new NativeListItem<WeaponHash>("Special", WeaponHash.Unarmed, WeaponHash.Parachute, WeaponHash.FireExtinguisher, WeaponHash.NightVision);
            //specialSelector.Description = $"{specialSelector.Items.Count} Options";
            //specialSelector.SelectedItem = _config.GetValue("FAVORITES", "Special", WeaponHash.Unarmed);
            //menu.Add(specialSelector);

            // Save Button
            saveButton = new NativeItem("Save", "Save current settings to ini.");
            menu.Add(saveButton);
        }

        private void save(object sender, SelectedEventArgs args)
        {
            if(this._config != null)
            {
                this._config.SetValue("KEY", "OpenMenu", _config.GetValue("KEY", "OpenMenu", Keys.F8));

                this._config.SetValue("FAVORITES", "Melee", meleeSelector.SelectedItem);
                this._config.SetValue("FAVORITES", "Pistol", pistolSelector.SelectedItem);
                this._config.SetValue("FAVORITES", "Shotgun", shotgunSelector.SelectedItem);
                this._config.SetValue("FAVORITES", "MG", machineGunSelector.SelectedItem);
                this._config.SetValue("FAVORITES", "Rifle", assultRifleSelector.SelectedItem);
                this._config.SetValue("FAVORITES", "Sniper", sniperRifleSelector.SelectedItem);
                this._config.SetValue("FAVORITES", "Heavy", heavyFirearmsSelector.SelectedItem);
                this._config.SetValue("FAVORITES", "Explosion", explosionSelector.SelectedItem);
                //this._config.SetValue("FAVORITES", "Special", specialSelector.SelectedItem);

                this._config.Save();
                GTA.UI.Screen.ShowSubtitle("Setting ~g~saved ~s~successfully.");
            }
        }

        public void OnTick()
        {
            pool.Process();
        }

        public void ToggleMenu()
        {
            Menu.Visible = !Menu.Visible;
        }

        /// <summary>
        /// Get selected weapon hashes.
        /// </summary>
        /// <returns>An array of selected WeaponHash values.</returns>
        public WeaponHash[] GetHashes()
        {
            if (modEnabledCheckbox.Checked)
            {
                var result = new WeaponHash[8];
                result[0] = meleeSelector.SelectedItem;
                result[1] = pistolSelector.SelectedItem;
                result[2] = shotgunSelector.SelectedItem;
                result[3] = machineGunSelector.SelectedItem;
                result[4] = assultRifleSelector.SelectedItem;
                result[5] = sniperRifleSelector.SelectedItem;
                result[6] = heavyFirearmsSelector.SelectedItem;
                result[7] = explosionSelector.SelectedItem;
                //result[8] = specialSelector.SelectedItem;

                return result;
            }
            return null;
        }
    }
}
