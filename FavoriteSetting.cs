using GTA;

/// <summary>
/// Class to store favorite weapon settings for each character.
/// </summary>
internal class FavoriteSetting
{
    // WeaponHash properties for each weapon
    public WeaponHash Melee { get; set; }
    public WeaponHash Pistol { get; set; }
    public WeaponHash Shotgun { get; set; }
    public WeaponHash MG { get; set; }
    public WeaponHash Rifle { get; set; }
    public WeaponHash Sniper { get; set; }
    public WeaponHash Heavy { get; set; }
    public WeaponHash Explosive { get; set; }
    public WeaponHash Special { get; set; }

    // Method to get the section name corresponding to PedHash
    private string GetSectionName(PedHash hash)
    {
        switch (hash)
        {
            case PedHash.Michael:
                return "Michael";
            case PedHash.Trevor:
                return "Trevor";
            default:
                return "Franklin";
        }
    }

    /// <summary>
    /// Constructor to load favorite weapon settings based on the specified ScriptSettings and PedHash.
    /// </summary>
    /// <param name="setting">ScriptSettings obtained from the INI file.</param>
    /// <param name="hash">PedHash representing a character.</param>
    public FavoriteSetting(ScriptSettings setting, PedHash hash)
    {
        var section = GetSectionName(hash);

        Melee = setting.GetValue(section, "Melee", WeaponHash.Unarmed);
        Pistol = setting.GetValue(section, "Pistol", WeaponHash.Unarmed);
        Shotgun = setting.GetValue(section, "Shotgun", WeaponHash.Unarmed);
        MG = setting.GetValue(section, "MG", WeaponHash.Unarmed);
        Rifle = setting.GetValue(section, "Rifle", WeaponHash.Unarmed);
        Sniper = setting.GetValue(section, "Sniper", WeaponHash.Unarmed);
        Heavy = setting.GetValue(section, "Heavy", WeaponHash.Unarmed);
        Explosive = setting.GetValue(section, "Explosive", WeaponHash.Unarmed);
        Special = setting.GetValue(section, "Special", WeaponHash.Unarmed);
    }

    /// <summary>
    /// Update script settings with the current weapon preferences.
    /// </summary>
    /// <param name="setting">Script settings obtained from the INI file.</param>
    /// <param name="hash">PedHash representing a character.</param>
    /// <exception cref="NotImplementedException">Thrown when the section name is not a valid character name.</exception>
    public void UpdateScriptSettings(ScriptSettings setting, PedHash hash)
    {
        var section = GetSectionName(hash);

        setting.SetValue(section, "Melee", Melee);
        setting.SetValue(section, "Pistol", Pistol);
        setting.SetValue(section, "Shotgun", Shotgun);
        setting.SetValue(section, "MG", MG);
        setting.SetValue(section, "Rifle", Rifle);
        setting.SetValue(section, "Sniper", Sniper);
        setting.SetValue(section, "Heavy", Heavy);
        setting.SetValue(section, "Explosive", Explosive);
        setting.SetValue(section, "Special", Special);
    }

    /// <summary>
    /// Check if the specified WeaponHash is included in the favorite weapons.
    /// </summary>
    /// <param name="hash">WeaponHash to check.</param>
    /// <returns>True if the specified weapon is included; otherwise, false.</returns>
    public bool Contains(WeaponHash hash)
    {
        return hash == Melee ||
               hash == Pistol ||
               hash == Shotgun ||
               hash == MG ||
               hash == Rifle ||
               hash == Sniper ||
               hash == Heavy ||
               hash == Explosive ||
               hash == Special;
    }
}
