---
title: Add a subreport and parameters to a Report Builder paginated report 
description: Learn how to add a subreport to a paginated report when you want to create a main report as a container for multiple related reports in Report Builder.
author: maggiesMSFT
ms.author: maggies
ms.date: 07/16/2024
ms.service: reporting-services
ms.subservice: report-design
ms.topic: how-to
ms.custom: updatefrequency5
f1_keywords:
  - "10093"
  - "sql13.rtp.rptdesigner.subreportproperties.general.f1"

#customer intent: As a Report Builder user, I want to learn how to create subreports so that I can create multiple related reports inside of a single paginated report. 
---
# Add a subreport and parameters to a Report Builder paginated report 

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

Add subreports to a paginated report when you want to create a main report that's a container for multiple related reports. A subreport is a reference to another report. To relate the reports through data values, you must design a parameterized report as the subreport. For example, to have multiple reports show data for the same customer you could create a report that shows the details for a specific customer. When you add a subreport to the main report, you can specify parameters to pass to the subreport.  
  
You can also add subreports to dynamic rows or columns in a table or matrix. When the main report is processed, the subreport is processed for each row. In this case, consider whether you can achieve the desired effect by using data regions or nested data regions.

> [!NOTE]  
> [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  

## Prerequisites

- You must first create the report that acts as the subreport. For more information on creating the subreport, see [Subreports in paginated reports (Report Builder)](../../reporting-services/report-design/subreports-report-builder-and-ssrs.md).  

## Add a subreport  
  
1. On the **Insert** tab, select **Subreport**.  
  
1. On the **Design** surface, choose a location on the report and then drag a box to the desired size of the subreport. Alternatively, select the **Design** surface to create a subreport of default size.  
  
1. Right-click the subreport, and then select **Subreport Properties**.  
  
1. In the **Subreport Properties** dialog box, enter a name in the **Name** text box or accept the default. The name must be unique within the report. By default, a general name such as `Subreport1` or `Subreport2` is assigned.

    :::image type="content" source="../../reporting-services/report-design/media/subreport-properties-name.png" alt-text="Screenshot of the Subreport Properties dialog box highlighting the Name field on the General tab.":::
  
1. In the **Use this report as a subreport** box, select **Browse**, or enter the name of the report. Selecting **Browse** is preferred because the path to the subreport is specified automatically. You can specify the report in several ways. For more information, see [Specify paths to external items in a paginated report (Report Builder)](../../reporting-services/report-design/specifying-paths-to-external-items-report-builder-and-ssrs.md).  
  
1. (Optional) Choose **Yes** for **Omit border on page break** to prevent a border from being rendered in the middle of the subreport if the subreport spans more than one page.  
  
1. Select **OK**.
  
## Specify parameters you want to pass to a subreport  
  
1. In **Design** view, right-click the subreport and then select **Subreport Properties**.  
  
1. In the **Subreport Properties** dialog, select the **Parameters** tab.  
  
1. Select **Add**. A new row is added to the parameter grid.  
  
1. In the **Name** text box, type the name of a parameter in the subreport or choose it from the list box. This name must match a report parameter, not a query parameter, in the subreport.  
  
1. In the **Value** list box, type or select a value to pass to the subreport. This value can be static text or an expression that references a field or other object in the main report.

    :::image type="content" source="../../reporting-services/report-design/media/subreport-properties-parameter.png" alt-text="Screenshot of the Subreport Properties dialog box showing one example parameter on the Parameters tab.":::
  
    > [!NOTE]  
    > In Report Builder, if a parameter is missing from the **Parameters** list and the subreport has a default value defined, the subreport is processed correctly.  
    >
    > In Report Designer, all parameters that are required by the subreport must be included in the **Parameters** list. If a required parameter is missing, the subreport isn't displayed correctly in the main report.  
  
1. Repeat steps 3-5 to specify a name and value for each subreport parameter.  
  
1. To delete a subreport parameter, select the parameter in the parameter grid, and then choose **Delete**.  
  
1. To change the order of a subreport parameter, select the parameter, and then choose the up or down button.  
  
     Changing the order of a subreport parameter doesn't affect the processing of the subreport.  
  
## Related content

- [Subreports in paginated reports (Report Builder)](../../reporting-services/report-design/subreports-report-builder-and-ssrs.md)
- [Rendering behaviors in a paginated report (Report Builder)](../../reporting-services/report-design/rendering-behaviors-report-builder-and-ssrs.md)  
  