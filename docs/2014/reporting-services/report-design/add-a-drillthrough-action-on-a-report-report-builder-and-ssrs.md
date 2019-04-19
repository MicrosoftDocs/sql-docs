---
title: "Add a Drillthrough Action on a Report (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 153729c4-d01e-4629-b78f-0cfd5a7f83da
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Add a Drillthrough Action on a Report (Report Builder and SSRS)
  The report that opens when you click the link in the main report is known as a *drillthrough report*. This drillthrough link enables a drillthrough action.  
  
 Drillthrough reports must be published to the same report server as the main report, but they can be in different folders. You can add a drillthrough link to any item that has an **Action** property, such as a text box, an image, or data points on a chart.  
  
 To add a drillthrough link to a report, you must first create the drillthrough report that the main report will link to. A drillthrough report commonly contains details about an item that is contained in the original summary report, and often contains parameters that filter the drillthrough report based on parameters passed to it from the main report. For more information on creating the drillthrough report, see [Drillthrough Reports &#40;Report Builder and SSRS&#41;](drillthrough-reports-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
### To add a drillthrough action  
  
1.  In Design view, right-click the text box, image, or chart to which you want to add a link and then click **Properties**.  
  
2.  In the item's **Properties** dialog box, click **Action**.  
  
3.  Select **Go to report**. Additional sections appear in the dialog box for this option.  
  
4.  In **Specify a report**, click **Browse** to locate the report that you want to jump to, or type the name of the report. Alternatively, click the expression (**fx**) button to create an expression for the report name.  
  
     The format of the path to the drillthrough report differs for native and SharePoint integrated mode. If you browse to the report, a path of the correct format is provided. For more information, see [Specifying Paths to External Items &#40;Report Builder and SSRS&#41;](specifying-paths-to-external-items-report-builder-and-ssrs.md).  
  
     If you have to specify parameters for the drillthrough report, follow the next step.  
  
5.  In **Use these parameters to run the report**, click **Add**. A new row is added to the parameter grid.  
  
    -   In the **Name** text box, click the drop-down list or type the name of the report parameter in the drillthrough report.  
  
        > [!NOTE]  
        >  The names in the parameter list must match the expected parameters in the target report exactly. For example, parameter names must match by case. If the names do not match, or if an expected parameter is not listed, the drillthrough report fails.  
  
    -   In **Value**, type or select the value to pass to the parameter in the drillthrough report.  
  
        > [!NOTE]  
        >  Values can contain an expression that evaluates to a value to pass to the report parameter. The expressions in the value list include the field list for the current report.  
  
     For information on how to hide parameters in reports, see [Add, Change, or Delete a Report Parameter &#40;Report Builder and SSRS&#41;](add-change-or-delete-a-report-parameter-report-builder-and-ssrs.md).  
  
6.  (Optional) For text boxes, it is helpful to indicate to the user that the text is a link by changing the color and effect of the text on the **Home** tab of the Ribbon.  
  
7.  To test the link, run the report and click the report item that you set this link on.  
  
## See Also  
 [Action Properties Dialog Box &#40;Report Builder and SSRS&#41;](../action-properties-dialog-box-report-builder-and-ssrs.md)   
 [Formatting Data Points on a Chart &#40;Report Builder and SSRS&#41;](formatting-data-points-on-a-chart-report-builder-and-ssrs.md)   
 [Show ToolTips on a Series &#40;Report Builder and SSRS&#41;](show-tooltips-on-a-series-report-builder-and-ssrs.md)  
  
  
