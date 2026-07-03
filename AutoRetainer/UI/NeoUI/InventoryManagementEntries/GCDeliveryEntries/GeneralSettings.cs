using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoRetainer.UI.NeoUI.InventoryManagementEntries.GCDeliveryEntries;
public unsafe sealed class GeneralSettings : InventoryManagemenrBase
{
    public override string Name { get; } = "Grand Company Delivery/General Settings";

    public override void Draw()
    {
        ImGui.Checkbox("Enable Expert Delivery continuation".Loc(), ref C.AutoGCContinuation);
        ImGui.Indent();
        ImGuiEx.TextWrapped("When Expert Delivery Continuation is enabled:".Loc() + "\n"
            + "- The plugin will automatically spend available Grand Company Seals to purchase items from the configured Exchange List.".Loc() + "\n"
            + "- If the Exchange List is empty, only Ventures will be purchased.".Loc() + "\n\n"
            + "After seals have been spent:".Loc() + "\n"
            + "- Expert Delivery will resume automatically.".Loc() + "\n"
            + "- The process will repeat until there are no eligible items left to deliver or no seals remaining.".Loc());
        ImGui.Unindent();
    }
}