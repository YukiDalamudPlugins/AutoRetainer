using ECommons.ExcelServices;

namespace AutoRetainer.UI.NeoUI.InventoryManagementEntries.InventoryCleanupEntries;
public class FastAddition : InventoryManagemenrBase
{
    public override string Name { get; } = "Inventory Cleanup/Fast Addition and Removal";

    private FastAddition()
    {
        Builder = InventoryCleanupCommon.CreateCleanupHeaderBuilder()
        .Section(Name.Loc())
        .Widget(() =>
        {
            var selectedSettings = InventoryCleanupCommon.SelectedPlan;
            ImGuiEx.TextWrapped(GradientColor.Get(EColor.RedBright, EColor.YellowBright), "While this text is visible, hover over items while holding:".Loc());
            ImGuiEx.Text(!ImGui.GetIO().KeyShift ? ImGuiColors.DalamudGrey : ImGuiColors.DalamudRed, "Shift - add to Quick Venture Sell List".Loc());
            ImGuiEx.Text("* Items that already in Unconditional Sell List WILL NOT BE ADDED to Quick Venture Sell List".Loc());
            ImGuiEx.Text(!ImGui.GetIO().KeyCtrl ? ImGuiColors.DalamudGrey : ImGuiColors.DalamudRed, "Ctrl - add to Unconditional Sell List".Loc());
            ImGuiEx.Text("* Items that already in Quick Venture Sell List WILL BE MOVED to Unconditional Sell List".Loc());
            ImGuiEx.Text(!ImGui.GetIO().KeyAlt ? ImGuiColors.DalamudGrey : ImGuiColors.DalamudRed, "Alt - delete from either list".Loc());
            ImGuiEx.Text("\n" + "Items that are protected are unaffected by these actions".Loc());
            if(Svc.GameGui.HoveredItem > 0)
            {
                var id = (uint)(Svc.GameGui.HoveredItem % 1000000);
                if(ImGui.GetIO().KeyShift)
                {
                    if(!selectedSettings.IMProtectList.Contains(id) && !selectedSettings.IMAutoVendorSoft.Contains(id) && !selectedSettings.IMAutoVendorHard.Contains(id))
                    {
                        selectedSettings.IMAutoVendorSoft.Add(id);
                        Notify.Success("Added ?? to Quick Venture Sell List".Loc(ExcelItemHelper.GetName(id)));
                        selectedSettings.IMAutoVendorHard.Remove(id);
                    }
                }
                if(ImGui.GetIO().KeyCtrl)
                {
                    if(!selectedSettings.IMProtectList.Contains(id) && !selectedSettings.IMAutoVendorHard.Contains(id) && !selectedSettings.IMAutoVendorSoft.Contains(id))
                    {
                        selectedSettings.IMAutoVendorHard.Add(id);
                        Notify.Success("Added ?? to Unconditional Sell List".Loc(ExcelItemHelper.GetName(id)));
                        selectedSettings.IMAutoVendorSoft.Remove(id);
                    }
                }
                if(ImGui.GetIO().KeyAlt)
                {
                    if(selectedSettings.IMAutoVendorSoft.Remove(id)) Notify.Info("Removed ?? from Quick Venture Sell List".Loc(ExcelItemHelper.GetName(id)));
                    if(selectedSettings.IMAutoVendorHard.Remove(id)) Notify.Info("Removed ?? from Unconditional Sell List".Loc(ExcelItemHelper.GetName(id)));
                }
            }
        });
        DisplayPriority = -10;
    }
}
