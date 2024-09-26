---
title: "Add a drillthrough action on a paginated report"
description: Improve query performance with the addition of a drillthrough action link in a text box, an image, or data points on a chart.
author: maggiesMSFT
ms.author: maggies
ms.date: 09/25/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: conceptual
ms.custom:
  - updatefrequency5
---
# Add a drillthrough action on a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-not-pbi-rb](../../includes/ssrs-appliesto-not-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  The paginated report that opens when you select the link in the main paginated report is known as a *drillthrough report*. This drillthrough link enables a drillthrough action.  
  
 Drillthrough reports must be published to the same report server as the main report, but they can be in different folders. You can add a drillthrough link to any item that has an **Action** property, such as a text box, an image, or data points on a chart.  
  
 To add a drillthrough link to a report, you must first create the drillthrough report that the main report links to. A drillthrough report commonly contains details about an item that is contained in the original summary report. The report often contains parameters that filter the drillthrough report based on parameters passed to it from the main report. For more information on creating the drillthrough report, see [Drillthrough reports &#40;Report Builder&#41;](../../reporting-services/report-design/drillthrough-reports-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### Add a drillthrough action  
  
1.  In Design view, right-click the text box, image, or chart to which you want to add a link, and then select **Properties**.  
  
1.  In the item's **Properties** dialog, select **Action**.  
  
1.  Select **Go to report**. Other sections appear in the dialog for this option.  
  
1.  In **Specify a report**, select **Browse** to locate the report that you want to jump to, or type the name of the report. Alternatively, select the expression (**fx**) button to create an expression for the report name.  
  
     The format of the path to the drillthrough report differs for native and SharePoint integrated mode. If you browse to the report, a path of the correct format is provided. For more information, see [Specify paths to external items &#40;Report Builder&#41;](../../reporting-services/report-design/specifying-paths-to-external-items-report-builder-and-ssrs.md).  
  
     If you have to specify parameters for the drillthrough report, follow the next step.  
  
1.  In **Use these parameters to run the report**, select **Add**. A new row is added to the parameter grid.  
  
    -   In the **Name** text box, select the list or enter the name of the report parameter in the drillthrough report.  
  
        > [!NOTE]  
        >  The names in the parameter list must match the expected parameters in the target report exactly. For example, parameter names must match by case. If the names don't match, or if an expected parameter isn't listed, the drillthrough report fails.  
  
    -   In **Value**, enter or select the value to pass to the parameter in the drillthrough report.  
  
        > [!NOTE]  
        >  Values can contain an expression that evaluates to a value to pass to the report parameter. The expressions in the value list include the field list for the current report.  
  
     For information on how to hide parameters in reports, see [Add, change, or delete a report parameter &#40;Report Builder&#41;](../../reporting-services/report-design/add-change-or-delete-a-report-parameter-report-builder-and-ssrs.md).  
  
1.  (Optional) For text boxes, it's helpful to indicate to the user that the text is a link by changing the color and effect of the text on the **Home** tab of the Ribbon.  
  
1.  To test the link, run the report and select the report item that you set this link on.  
  
## Related content

- [Action Properties dialog&#40;Report Builder&#41;](./add-a-hyperlink-to-a-url-report-builder-and-ssrs.md)
- [Format data points on a chart &#40;Report Builder&#41;](../../reporting-services/report-design/formatting-data-points-on-a-chart-report-builder-and-ssrs.md)
- [Show ToolTips on a series &#40;Report Builder&#41;](../../reporting-services/report-design/show-tooltips-on-a-series-report-builder-and-ssrs.md)
