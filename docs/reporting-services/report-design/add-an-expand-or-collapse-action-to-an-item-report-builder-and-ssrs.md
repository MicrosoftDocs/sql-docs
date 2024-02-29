---
title: "Add an expand or collapse action to a paginated report"
description: Enable a user to expand or collapse items, rows, and columns associated with a group for a table or matrix in a paginated report in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 12/19/2019
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom: updatefrequency5
---
# Add an expand or collapse action to a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-not-pbi-rb](../../includes/ssrs-appliesto-not-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  You can enable a user to interactively expand or collapse items in a paginated report, or expand or collapse rows and columns associated with a group for a table or matrix. To allow users to expand or collapse an item, you set the visibility properties for that item. Setting visibility works in an HTML report viewer, and is sometimes called a *drilldown* action.  
  
 In report design view, you specify the name of the text box where you want to display the expand and collapse toggle icons. In the rendered report, the text box displays a plus (+) or minus (-) sign in addition to its contents. When the user selects the toggle, the report display is refreshed to show or hide the report item, based on the current visibility settings for items in the report.  
  
 Typically, the expand and collapse action is used to initially display only summary data and to enable the user to select the plus sign to show detail data. For example, you can initially hide a table that displays values for a chart, or hide child groups for a table with nested row or column groups, as in a drilldown report.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### Add expand and collapse action to a group  
  
1.  In report design view, select the table or matrix to select it. The Grouping pane displays the row and column groups.  
  
     :::image type="content" source="../../reporting-services/report-design/media/groupingpane.png" alt-text="Screenshot of the Grouping pane with the Product ID cell highlighted.":::
  
  
     If the **Grouping** pane doesn't appear, select the **View** menu and then choose **Grouping**.  
  
1.  Right-click anywhere in the title bar of the **Grouping** pane, and then select **Advanced**. The **Grouping** pane mode toggles to show the underlying display structure for rows and columns on the design surface.  
  
     :::image type="content" source="../../reporting-services/report-design/media/groupingpane-advancedmode.png" alt-text="Screenshot of the Grouping Pane with Advanced Mode menu."::: 
  
1.  In the appropriate group pane, select the name of the row group or column group for which you want to hide the associated rows or columns. The group is selected and the **Properties** pane shows the **Tablix Member** properties.  
  
    > [!NOTE]  
    >  If you don't see the **Properties** pane, select **View** on the Ribbon and then choose **Properties**.  
  
1.  In **Hidden**, choose one of the following options to set the visibility of this report item the first time you run a report:  
  
    -   Select **False** to display the report item.  
  
    -   Select **True** to hide the report item.  
  
    -   Select **\<Expression>** to open the **Expression** dialog to create an expression that is evaluated at run time to determine the visibility.  
  
1.  In **ToggleItem**, from the list, select the name of a text box to which to add the toggle image.  
  
     In the following image, the Color row group is configured enable users to expand and collapse associated rows.  
  
     :::image type="content" source="../../reporting-services/report-design/media/expandcollapse-confighiddentoggleitemwithnumbers.png" alt-text="Screenshot of the steps to configure a row group to be expanded.":::
  
    > [!NOTE]  
    >  The text box with the toggle image can't be the row or column group for which you want to hide the associated rows or columns. It must either be in the same group as the item that is being hidden or in an ancestor group. For example, to toggle visibility of rows associated with a child group, select a text box in a row associated with the parent group.  
  
1.  To test the toggle, run the report and select the text box with the toggle image. The report display refreshes to show row groups and column groups with their toggled visibility.  
  
     :::image type="content" source="../../reporting-services/report-design/media/expandcollapse-runreport-rowgroup.png" alt-text="Screenshot of running a report with an expandable row group.":::  
  
### Add expand and collapse action to a report item  
  
1.  In report design view, right-click the report item to show or hide, and then select `<report item>` **Properties**. The `<report item>` **Properties** dialog for the report item opens.  
  
1.  Select **Visibility**.  
  
1.  In **When the report is initially run**, choose one of the following options to set the visibility of this report item the first time you run a report:  
  
    -   Select **Show** to display the report item.  
  
    -   Select **Hide** to hide the report item.  
  
    -   Select **Show or hide based on an expression** to use an expression evaluated at run time to determine the visibility. Select (**fx**) to open the **Expression** dialog to create an expression.  
  
        > [!NOTE]  
        >  When you specify an expression for visibility, you set the Hidden property of the report item. The expression evaluates to a **Boolean** value of **True** to hide the item and **False** to show the item.  
  
1.  In **Display can be toggled by this report item**, from the list, enter or select the name of a text box in the report in which to display a toggle image. For example, enter `Textbox1`.  
  
     In the following image, the table is configured to enable users to expand and collapse it. The display of the table is toggled by the **Products Table** text box.  
  
     :::image type="content" source="../../reporting-services/report-design/media/expandcollapse-reporttable.png" alt-text="Screenshot of configuring a report table to be expanded.":::  
  
    > [!NOTE]  
    >  The text box that you choose must be in the current or containing scope for this report item (up to and including the report body). To toggle visibility of a chart, select a text box that is in the same containing scope as the chart. For example, select the report body or a rectangle. The text box must be in the same container hierarchy or higher.  
  
1.  To test the toggle, run the report and select the text box with the toggle image. The report display refreshes to show report items with their toggled visibility.  
  
     :::image type="content" source="../../reporting-services/report-design/media/expandcollapse-runreport-reporttable.png" alt-text="Screenshot of running the report with an expanding table.":::  
  
## Related content  
 [Drilldown action &#40;Report Builder&#41;](../../reporting-services/report-design/drilldown-action-report-builder-and-ssrs.md)   
 [Hide an item &#40;Report Builder&#41;](../../reporting-services/report-builder/hide-an-item-report-builder-and-ssrs.md)  
  
  
