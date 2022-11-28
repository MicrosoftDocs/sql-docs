---
title: "Set parameters on a published paginated report - SharePoint Integrated Mode | Microsoft Docs"
description: Learn how to set parameters and run a parameterized paginated report, in the report definition or after the report is published, in Report Builder.
ms.date: 03/07/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
helpviewer_keywords: 
  - "SharePoint integration [Reporting Services], content management"
  - "report parameters [Reporting Services]"
ms.assetid: dec5d985-a6c1-4dd8-8a66-a848e89a2e18
author: maggiesMSFT
ms.author: maggies
monikerRange: ">=sql-server-2016 <=sql-server-2016"
---
# Set parameters on a published paginated report - SharePoint Integrated Mode (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]


  A parameterized report is a paginated report that accepts input values that are used to filter data when you run the report. Parameters are defined when the report is created. Depending on how a report parameter is defined in the report definition, it can accept a single value, multiple values, or dynamic values, which change in response to a previous selection (for example, when you select product category, your next selection might be a specific product from that category). A parameter can have a default value, which can be used to run a filtered version of the report automatically or possibly be replaced with a different value.  
  
 These properties can be set in the report definition, or after the report is published. Although you can modify some parameter properties in a published report to change the value and display properties, you cannot change a parameter name or the data type. The parameter name and data type can only be changed by the report author in the report definition.  
  
 To run a parameterized report, you typically must know which values to type, although a report might include drop-down lists of valid values from which to choose.  
  
 To set parameter properties on a published report, you must have Edit Items permission for the report. You can modify some or all of the parameter properties on a report that you run from a SharePoint site. You cannot personalize a report by saving a combination of parameter values that you want to use repeatedly. Any default values that you specify are used by all users who open the report.  
  
 If the report is embedded in a Report Viewer Web part that is configured to always show that report, set the properties on the Report Viewer Web Part. For more information, see [Add the Report Viewer Web Part to a Web Page &#40;Reporting Services in SharePoint Integrated Mode&#41;](../../reporting-services/report-server-sharepoint/add-the-report-viewer-web-part-to-a-web-page.md).  
  
### To run a parameterized report  
  
1.  Open the library that contains the report.  
  
2.  Click the report name. You must know in advance which reports have parameters. There is no visual identifier on the report to indicate that it is parameterized.  
  
3.  When the report opens, specify the parameters you want to use. The Parameters area might be visible, collapsed, or hidden depending on how properties are set on the Report Viewer Web Part.  
  
    1.  If the Parameters area is visible, choose a value for each parameter.  
  
    2.  If there is a thin strip of color that runs down the length of the report that has an arrow in the middle of it, the Parameters area is collapsed. You can click the arrow to expand it.  
  
    3.  If the Parameters area is hidden, you cannot specify a value.  
  
4.  Click **Apply** at the bottom of the Parameters area to run the report.  
  
     It is possible to specify a combination of values that do not give you the results you expect. The report might need to be modified by the report author if you are not getting the information you require.  
  
5.  Click **OK**.  
  
### To set parameter properties  
  
1.  Open the library or folder that contains the report.  
  
2.  Point to the report and click the down arrow.  
  
3.  Click **Manage Parameters**. If the report contains parameters, each parameter will be listed on the page. The list shows the parameter name, data type, data value that is used by default, and whether it is visible in the parameter area when you open the report.  
  
4.  Click a parameter in the list to modify its settings.  
  
5.  In Default Value, enter a value for the parameter. The value will not be validated, so be sure that you are providing a value that works for the report.  
  
    1.  Choose **Use value expression specified in report definition** if you want to use the default value that was defined when the report was created. If the report definition does not provide a default value, this option will be unavailable.  
  
    2.  Choose **Override the report default value** if you want to specify a replacement for the report definition default value. Optionally, for report parameters that accept null values, you can select the **Null** check box to prevent filtering based on that parameter.  
  
    3.  Choose **Parameter does not have a default value** if you want each user to specify a value before the report is processed. If you select this option, you should set display settings that prompt the user to specify a value.  
  
     Note that if all parameter values have a default value, the report will automatically run with those values when the user opens the report. However, if the parameter area is displayed, users can override the default value and re-run the report.  
  
6.  Set display options to determine whether the parameter is visible.  
  
    1.  Choose **Prompt User** to show the parameter on the page. You can specify prompt text that appears within a field to provide a brief statement about the format or type of data that the user must provide.  
  
    2.  Choose **Hidden** if you are using a default value and you do not want the parameter to be visible in the Parameters area.  
  
    3.  Choose **Internal** if you are using a default value and you do not want the parameter to be visible in the Parameters area or on subscription pages.  
  
7.  Click **Apply**.  
  
## See Also  
 [SharePoint Site and List Permission Reference for Report Server Items](../../reporting-services/security/sharepoint-site-and-list-permission-reference-for-report-server-items.md)  
  
  
