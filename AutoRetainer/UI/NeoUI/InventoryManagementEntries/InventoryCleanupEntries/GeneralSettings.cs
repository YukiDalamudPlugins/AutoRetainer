using AutoRetainer.Internal.InventoryManagement;
using ECommons.GameHelpers;

namespace AutoRetainer.UI.NeoUI.InventoryManagementEntries.InventoryCleanupEntries;
public class GeneralSettings : InventoryManagemenrBase
{
    public override string Name { get; } = "Inventory Cleanup/General Settings";

    private GeneralSettings()
    {
        Builder = InventoryCleanupCommon.CreateCleanupHeaderBuilder()
            .Section(Name.Loc())
            .Checkbox("Auto-open venture coffers".Loc(), () => ref InventoryCleanupCommon.SelectedPlan.IMEnableCofferAutoOpen, "Multi Mode only. Before logging out, all coffers will be opened unless your inventory space is too low.".Loc())
            .Checkbox("Enable selling items to retainer".Loc(), () => ref InventoryCleanupCommon.SelectedPlan.IMEnableAutoVendor, "When AutoRetainer checks resents retainers to ventures, items will be sold according to Inventory Cleanup plan.".Loc())
            .Checkbox("Enable selling items to housing NPC".Loc(), () => ref InventoryCleanupCommon.SelectedPlan.IMEnableNpcSell, "When AutoRetainer enters a house, items will be sold according to the Inventory Cleanup plan. A housing vendor that supports item selling must be placed near the house entrance (not the workshop entrance)—you should be able to interact with the NPC immediately after entering.".Loc())
            .Indent()
            .Checkbox("Ignore NPC if retainer is available".Loc(), () => ref InventoryCleanupCommon.SelectedPlan.IMSkipVendorIfRetainer)
            .Widget("Sell now".Loc(), (x) =>
            {
                if(ImGuiEx.Button(x, Player.Interactable && InventoryCleanupCommon.SelectedPlan.IMEnableNpcSell && NpcSaleManager.GetValidNPC() != null && !IsOccupied() && !P.TaskManager.IsBusy))
                {
                    NpcSaleManager.EnqueueIfItemsPresent(true);
                }
            })
            .Unindent()
            .Checkbox("Auto-desynth items".Loc(), () => ref InventoryCleanupCommon.SelectedPlan.IMEnableItemDesynthesis)
            .Checkbox("Enable context menu integration".Loc(), () => ref InventoryCleanupCommon.SelectedPlan.IMEnableContextMenu)
            .Checkbox("Allow selling items from Armory Chest".Loc(), () => ref InventoryCleanupCommon.SelectedPlan.AllowSellFromArmory)
            .Checkbox("Demo mode".Loc(), () => ref InventoryCleanupCommon.SelectedPlan.IMDry, "Do not sell items, instead print in chat what would be sold".Loc())
            ;
    }
}
