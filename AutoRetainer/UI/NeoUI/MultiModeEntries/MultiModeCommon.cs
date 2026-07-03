namespace AutoRetainer.UI.NeoUI.MultiModeEntries;
public class MultiModeCommon : NeoUIEntry
{
    public override string Path => "Multi Mode/Common Settings".Loc();

    public override NuiBuilder Builder { get; init; } = new NuiBuilder()
        .Section("Common Settings".Loc())
        .Checkbox("Enforce Full Character Rotation".Loc(), () => ref C.CharEqualize, "Recommended for users with > 15 characters, forces multi mode to make sure ventures are processed on all characters in order before returning to the beginning of the cycle.".Loc())
        .Checkbox("Wait on login screen".Loc(), () => ref C.MultiWaitOnLoginScreen, "If no character is available for ventures, you will be logged off until any character is available again. Title screen movie will be disabled while this option and MultiMode are enabled.".Loc())
        .Checkbox("Synchronise Retainers (one time)".Loc(), () => ref MultiMode.Synchronize, "AutoRetainer will wait until all enabled retainers have completed their ventures. After that this setting will be disabled automatically and all characters will be processed.".Loc())
        .Checkbox("Disable Multi Mode on Manual Login".Loc(), () => ref C.MultiDisableOnRelog)
        .Checkbox("Do not reset Preferred Character on Manual Login".Loc(), () => ref C.MultiNoPreferredReset)
        .Checkbox("Enable Manual relogs character postprocess".Loc(), () => ref C.AllowManualPostprocess)
        .Checkbox("Allow entering shared houses".Loc(), () => ref C.SharedHET)
        .Checkbox("Attempt to enter house on login even when Multi Mode is disabled".Loc(), () => ref C.HETWhenDisabled)
        .Checkbox("Do not teleport or enter house for retainers when already next to bell".Loc(), () => ref C.NoTeleportHetWhenNextToBell)

        .Section("Game startup".Loc())
        .Checkbox("Enable Multi Mode on Game Boot".Loc(), () => ref C.MultiAutoStart)
        .Widget("Auto-login on Game Boot".Loc(), (x) =>
        {
            ImGui.SetNextItemWidth(150f);
            var names = C.OfflineData.Where(s => !s.Name.IsNullOrEmpty()).Select(s => $"{s.Name}@{s.World}");
            var dict = names.ToDictionary(s => s, s => Censor.Character(s));
            dict.Add("", "Disabled".Loc());
            dict.Add("~", "Last logged in character".Loc());
            ImGuiEx.Combo(x, ref C.AutoLogin, ["", "~", .. names], names: dict);
        })
        .SliderInt(150f, "Delay".Loc(), () => ref C.AutoLoginDelay.ValidateRange(0, 60), 0, 20, "Set appropriate delay to let plugins fully load before logging in and to allow yourself some time to cancel login if needed".Loc())

        .Section("Inventory warnings".Loc())
        .InputInt(100f, "Retainer list: remaining inventory slots warning".Loc(), () => ref C.UIWarningRetSlotNum.ValidateRange(2, 1000))
        .InputInt(100f, "Retainer list: remaining ventures warning".Loc(), () => ref C.UIWarningRetVentureNum.ValidateRange(2, 1000))
        .InputInt(100f, "Deployables list: remaining inventory slots warning".Loc(), () => ref C.UIWarningDepSlotNum.ValidateRange(2, 1000))
        .InputInt(100f, "Deployables list: remaining fuel warning".Loc(), () => ref C.UIWarningDepTanksNum.ValidateRange(20, 1000))
        .InputInt(100f, "Deployables list: remaining repair kit warning".Loc(), () => ref C.UIWarningDepRepairNum.ValidateRange(5, 1000))

        .Section("Teleportation".Loc())
        .Widget(() => ImGuiEx.Text("Lifestream plugin is required".Loc()))
        .Widget(() => ImGuiEx.PluginAvailabilityIndicator([new("Lifestream", new Version("2.2.1.1"))]))
        .TextWrapped("You must register houses in Lifestream plugin for every character you want this option to work or enable Simple Teleport.".Loc())
        .TextWrapped("You can customize these settings per character in character configuration menu.".Loc())
        .Widget(() =>
        {
            if(Data != null && Data.GetAreTeleportSettingsOverriden())
            {
                ImGuiEx.TextWrapped(ImGuiColors.DalamudRed, "For current character teleport options are customized.".Loc());
            }
        })
        .Checkbox("Enabled".Loc(), () => ref C.GlobalTeleportOptions.Enabled)
        .Indent()
        .Checkbox("Teleport for retainers...".Loc(), () => ref C.GlobalTeleportOptions.Retainers)
        .Indent()
        .Checkbox("...to private house".Loc(), () => ref C.GlobalTeleportOptions.RetainersPrivate)
        .Checkbox("...to free company house".Loc(), () => ref C.GlobalTeleportOptions.RetainersFC)
        .Checkbox("...to apartment".Loc(), () => ref C.GlobalTeleportOptions.RetainersApartment)
        .TextWrapped("If all above are disabled or fail, will be teleported to inn.".Loc())
        .Unindent()
        .Checkbox("Teleport to free company house for deployables".Loc(), () => ref C.GlobalTeleportOptions.Deployables)
        .Checkbox("Enable Simple Teleport".Loc(), () => ref C.AllowSimpleTeleport)
        .Unindent()
        .Widget(() => ImGuiEx.HelpMarker("Allows teleporting to houses without registering them in Lifestream. You still need Lifestream plugin for teleportation to work.\n\nWarning! This option is less reliable than registering your houses in Lifestream. Avoid it if you can.".Loc(), EColor.RedBright, FontAwesomeIcon.ExclamationTriangle.ToIconString()))

        .Section("Bailout Module".Loc())
        .Checkbox("Auto-close and retry logging in on connection errors".Loc(), () => ref C.ResolveConnectionErrors)
        .Widget(() => ImGuiEx.PluginAvailabilityIndicator([new("NoKillPlugin")]));
}
