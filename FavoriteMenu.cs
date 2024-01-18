using GTA;
using LemonUI;
using LemonUI.Menus;
using System;

/// <summary>
/// Class representing a menu for configuring favorite weapons and settings.
/// </summary>
internal class FavoriteMenu
{
    private readonly ObjectPool pool;
    private readonly NativeMenu menu;
    private NativeCheckboxItem modEnabledCheckbox;
    private NativeListItem<WeaponHash> meleeSelector;
    private NativeListItem<WeaponHash> pistolSelector;
    private NativeListItem<WeaponHash> shotgunSelector;
    private NativeListItem<WeaponHash> machineGunSelector;
    private NativeListItem<WeaponHash> assultRifleSelector;
    private NativeListItem<WeaponHash> sniperRifleSelector;
    private NativeListItem<WeaponHash> heavyFirearmsSelector;
    private NativeListItem<WeaponHash> explosiveSelector;
    private NativeListItem<WeaponHash> specialSelector;
    private bool enabledByDefault;

    private PedHash currentPed;

    // Event handler for applying settings
    public delegate void ApplyEventHandler(FavoriteSetting setting);
    public event ApplyEventHandler SaveConfig;

    private FavoriteSetting settingForMichael;
    private FavoriteSetting settingForTrevor;
    private FavoriteSetting settingForFranklin;

    public FavoriteMenu(bool _enabledByDefault, FavoriteSetting _settingForMichael, FavoriteSetting _settingForTrevor, FavoriteSetting _settingForFranklin)
    {
        pool = new ObjectPool();
        menu = new NativeMenu("OnlyFavorites", "Choose your favorites only");
        currentPed = (PedHash)Game.Player.Character.Model.Hash;
        enabledByDefault = _enabledByDefault;
        settingForMichael = _settingForMichael;
        settingForTrevor = _settingForTrevor;
        settingForFranklin = _settingForFranklin;
        SetMenuItem(menu);
    }

    /// <summary>
    /// Gets the current favorite settings based on the player's character.
    /// </summary>
    /// <returns>The current favorite settings.</returns>
    private FavoriteSetting GetCurrentPedSetting()
    {
        switch ((PedHash)Game.Player.Character.Model.Hash)
        {
            case PedHash.Michael:
                return settingForMichael;
            case PedHash.Trevor:
                return settingForTrevor;
            default:
                return settingForFranklin;
        }
    }

    /// <summary>
    /// Sets menu items based on the provided settings.
    /// </summary>
    /// <param name="setting">The settings to be displayed in the menu.</param>
    private void SetMenuFromSettings(FavoriteSetting setting)
    {
        meleeSelector.SelectedItem = setting.Melee;
        pistolSelector.SelectedItem = setting.Pistol;
        shotgunSelector.SelectedItem = setting.Shotgun;
        machineGunSelector.SelectedItem = setting.MG;
        assultRifleSelector.SelectedItem = setting.Rifle;
        sniperRifleSelector.SelectedItem = setting.Sniper;
        heavyFirearmsSelector.SelectedItem = setting.Heavy;
        explosiveSelector.SelectedItem = setting.Explosive;
        specialSelector.SelectedItem = setting.Special;
    }

    /// <summary>
    /// Applies menu-selected settings to the provided settings object.
    /// </summary>
    /// <param name="setting">The settings to be updated from the menu.</param>
    private void ApplyMenuToSettings(FavoriteSetting setting)
    {
        setting.Melee = meleeSelector.SelectedItem;
        setting.Pistol = pistolSelector.SelectedItem;
        setting.Shotgun = shotgunSelector.SelectedItem;
        setting.MG = machineGunSelector.SelectedItem;
        setting.Rifle = assultRifleSelector.SelectedItem;
        setting.Sniper = sniperRifleSelector.SelectedItem;
        setting.Heavy = heavyFirearmsSelector.SelectedItem;
        setting.Explosive = explosiveSelector.SelectedItem;
        setting.Special = specialSelector.SelectedItem;
    }

    /// <summary>
    /// Sets up menu items and UI components.
    /// </summary>
    /// <param name="menu">The menu to be configured.</param>
    private void SetMenuItem(NativeMenu menu)
    {
        modEnabledCheckbox = new NativeCheckboxItem("Enabled")
        {
            Checked = enabledByDefault
        };
        menu.Add(modEnabledCheckbox);

        // Separator
        //menu.Add(new NativeSeparatorItem());

        // Melee
        meleeSelector = new NativeListItem<WeaponHash>("Melee", WeaponHash.Unarmed, WeaponHash.Knife, WeaponHash.Nightstick, WeaponHash.Hammer, WeaponHash.Bat, WeaponHash.Crowbar, WeaponHash.GolfClub, WeaponHash.Bottle, WeaponHash.Dagger, WeaponHash.BattleAxe, WeaponHash.KnuckleDuster, WeaponHash.Flashlight, WeaponHash.Machete, WeaponHash.SwitchBlade, WeaponHash.PoolCue, WeaponHash.Wrench, WeaponHash.BattleAxe, WeaponHash.StoneHatchet);
        meleeSelector.Description = $"{meleeSelector.Items.Count} Options";
        menu.Add(meleeSelector);

        // Pistol
        pistolSelector = new NativeListItem<WeaponHash>("Pistol", WeaponHash.Unarmed, WeaponHash.Pistol, WeaponHash.CombatPistol, WeaponHash.APPistol, WeaponHash.Pistol50, WeaponHash.SNSPistol, WeaponHash.HeavyPistol, WeaponHash.VintagePistol, WeaponHash.MarksmanPistol, WeaponHash.StunGun, WeaponHash.Revolver, WeaponHash.PistolMk2, WeaponHash.SNSPistolMk2, WeaponHash.RevolverMk2, WeaponHash.DoubleActionRevolver, WeaponHash.FlareGun, WeaponHash.UpNAtomizer, WeaponHash.CeramicPistol, WeaponHash.NavyRevolver, WeaponHash.PericoPistol, WeaponHash.WM29Pistol);
        pistolSelector.Description = $"{pistolSelector.Items.Count} Options";
        menu.Add(pistolSelector);

        // Shotgun
        shotgunSelector = new NativeListItem<WeaponHash>("Shotgun", WeaponHash.Unarmed, WeaponHash.SawnOffShotgun, WeaponHash.PumpShotgun, WeaponHash.AssaultShotgun, WeaponHash.BullpupShotgun, WeaponHash.Musket, WeaponHash.HeavyShotgun, WeaponHash.DoubleBarrelShotgun, WeaponHash.SweeperShotgun, WeaponHash.PumpShotgunMk2, WeaponHash.CombatShotgun);
        shotgunSelector.Description = $"{shotgunSelector.Items.Count} Options";
        menu.Add(shotgunSelector);

        // Machine Gun
        machineGunSelector = new NativeListItem<WeaponHash>("MG", WeaponHash.Unarmed, WeaponHash.MicroSMG, WeaponHash.SMG, WeaponHash.AssaultSMG, WeaponHash.CombatPDW, WeaponHash.MachinePistol, WeaponHash.MiniSMG, WeaponHash.SMGMk2, WeaponHash.MG, WeaponHash.CombatMG, WeaponHash.Gusenberg, WeaponHash.CombatMGMk2, WeaponHash.UnholyHellbringer);
        machineGunSelector.Description = $"{machineGunSelector.Items.Count} Options";
        menu.Add(machineGunSelector);

        // Assult Rifle
        assultRifleSelector = new NativeListItem<WeaponHash>("Rifle", WeaponHash.Unarmed, WeaponHash.AssaultRifle, WeaponHash.CarbineRifle, WeaponHash.AdvancedRifle, WeaponHash.BullpupRifle, WeaponHash.SpecialCarbine, WeaponHash.CompactRifle, WeaponHash.AssaultrifleMk2, WeaponHash.CarbineRifleMk2, WeaponHash.SpecialCarbineMk2, WeaponHash.BullpupRifleMk2, WeaponHash.MilitaryRifle, WeaponHash.HeavyRifle, WeaponHash.ServiceCarbine);
        assultRifleSelector.Description = $"{assultRifleSelector.Items.Count} Options";
        menu.Add(assultRifleSelector);

        // Sniper Rifle
        sniperRifleSelector = new NativeListItem<WeaponHash>("Sniper", WeaponHash.Unarmed, WeaponHash.SniperRifle, WeaponHash.HeavySniper, WeaponHash.MarksmanRifle, WeaponHash.HeavySniperMk2, WeaponHash.MarksmanRifleMk2, WeaponHash.PrecisionRifle);
        sniperRifleSelector.Description = $"{sniperRifleSelector.Items.Count} Options";
        menu.Add(sniperRifleSelector);

        // Heavy Firearms
        heavyFirearmsSelector = new NativeListItem<WeaponHash>("Heavy", WeaponHash.Unarmed, WeaponHash.GrenadeLauncher, WeaponHash.RPG, WeaponHash.Firework, WeaponHash.HomingLauncher, WeaponHash.Minigun, WeaponHash.Railgun, WeaponHash.CompactGrenadeLauncher, WeaponHash.Widowmaker, WeaponHash.CompactEMPLauncher);
        heavyFirearmsSelector.Description = $"{heavyFirearmsSelector.Items.Count} Options";
        menu.Add(heavyFirearmsSelector);

        // Explosive
        explosiveSelector = new NativeListItem<WeaponHash>("Explosive", WeaponHash.Unarmed, WeaponHash.Grenade, WeaponHash.StickyBomb, WeaponHash.ProximityMine, WeaponHash.SmokeGrenade, WeaponHash.BZGas, WeaponHash.Molotov, WeaponHash.PetrolCan, WeaponHash.PipeBomb, WeaponHash.Flare, WeaponHash.Ball, WeaponHash.Snowball, WeaponHash.HazardousJerryCan, WeaponHash.FertilizerCan);
        explosiveSelector.Description = $"{explosiveSelector.Items.Count} Options";
        menu.Add(explosiveSelector);

        // Special
        specialSelector = new NativeListItem<WeaponHash>("Special", WeaponHash.Unarmed, WeaponHash.Parachute);
        specialSelector.Description = $"{specialSelector.Items.Count} Options";
        menu.Add(specialSelector);

        SetMenuFromSettings(GetCurrentPedSetting());

        // Apply & Save
        menu.Closed += HandleApply;

        pool.Add(menu);
    }

    /// <summary>
    /// Event handler for applying settings.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    /// <param name="e">Event arguments.</param>
    private void HandleApply(object sender, EventArgs e)
    {
        ApplyMenuToSettings(GetCurrentPedSetting());
        SaveConfig?.Invoke(GetCurrentPedSetting());
    }

    /// <summary>
    /// Updates the menu state on each game tick.
    /// </summary>
    public void OnTick()
    {
        pool.Process();
        if(currentPed != (PedHash)Game.Player.Character.Model.Hash)
        {
            currentPed = (PedHash)Game.Player.Character.Model.Hash;
            if(menu != null)
            {
                pool.Remove(menu);
                menu.Clear();
                SetMenuItem(menu);
            }
        }
    }

    /// <summary>
    /// Toggles the visibility of the menu.
    /// </summary>
    public void ToggleMenu()
    {
        menu.Visible = !menu.Visible;
    }

    /// <summary>
    /// Gets the current settings for the active player character.
    /// </summary>
    /// <returns>The current favorite settings.</returns>
    public FavoriteSetting GetMenuSettings()
    {
        if(modEnabledCheckbox != null && modEnabledCheckbox.Checked)
        {
            return GetCurrentPedSetting();
        }
        return null;
    }
}

