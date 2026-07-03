namespace AutoRetainer.UI.NeoUI.MultiModeEntries;
public class MultiModeDeployables : NeoUIEntry
{
    public override string Path => "Multi Mode/Deployables".Loc();

    public override NuiBuilder Builder { get; init; } = new NuiBuilder()
        .Section("Multi Mode - Deployables".Loc())
        .Checkbox("Wait For Voyage Completion".Loc(), () => ref C.MultiModeWorkshopConfiguration.MultiWaitForAll, """When enabled, AutoRetainer will wait for all deployables to return before logging into the character. If you're already logged in for another reason, it will still resend completed submarines—unless the global setting "Wait even when already logged in" is also turned on.""".Loc())
        .Indent()
        .Checkbox("Wait even when already logged in".Loc(), () => ref C.MultiModeWorkshopConfiguration.WaitForAllLoggedIn, """Changes the behavior of "Wait for Voyage Completion" (both global and per-character) so that AutoRetainer no longer resends individual submarines while already logged in. Instead, it will wait until all submarines have returned before taking action.""".Loc())
        .InputInt(120f, "Maximum Wait, minutes".Loc(), () => ref C.MultiModeWorkshopConfiguration.MaxMinutesOfWaiting.ValidateRange(0, 9999), 10, 60, """If waiting for other deployables to return would exceed this number of minutes, AutoRetainer will ignore both the "Wait for Voyage Completion" and "Wait even when already logged in" settings.""".Loc())
        .Unindent()
        .DragInt(60f, "Advance Relog Threshold, seconds".Loc(), () => ref C.MultiModeWorkshopConfiguration.AdvanceTimer.ValidateRange(0, 300), 0.1f, 0, 300, "The number of seconds AutoRetainer should log in early before submarines on this character are ready to be resent.".Loc())
        .DragInt(120f, "Retainer venture processing cutoff, minutes".Loc(), () => ref C.DisableRetainerVesselReturn.ValidateRange(0, 60), "If set to a value greater than 0, AutoRetainer will stop processing any retainers this number of minutes before any character is scheduled to redeploy submarines, taking all previous settings into account.".Loc())
        .Checkbox("Periodically check FC chest for gil upon entering workshop".Loc(), () => ref C.FCChestGilCheck, "Periodically checks the Free Company chest when entering the Workshop to keep the gil counter up to date.".Loc())
        .Indent()
        .SliderInt(150f, "Check frequency, hours".Loc(), () => ref C.FCChestGilCheckCd, 0, 24 * 5)
        .Widget("Reset cooldowns".Loc(), (x) =>
        {
            if(ImGuiEx.Button(x, C.FCChestGilCheckTimes.Count > 0)) C.FCChestGilCheckTimes.Clear();
        })
        .Unindent();
}
