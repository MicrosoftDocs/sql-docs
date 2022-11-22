---
title: "Specifying paths to external items in a paginated report | Microsoft Docs"
description: Find out how to specify paths in item properties to reference items that are external to the paginated report definition file in Report Builder.
ms.date: 03/01/2017
ms.service: reporting-services
ms.subservice: report-design


ms.topic: conceptual
ms.assetid: 4fe513da-f3c5-479c-9fec-8662b91a0d6d
author: maggiesMSFT
ms.author: maggies
---
# Specifying paths to external items in a paginated report (Report Builder)

[!INCLUDE[ssrs-appliesto](../../includes/ssrs-appliesto.md)] [!INCLUDE [ssrs-appliesto-ssrs-rb](../../includes/ssrs-appliesto-ssrs-rb.md)] [!INCLUDE [ssrs-appliesto-pbi-rb](../../includes/ssrs-appliesto-pbi-rb.md)] [!INCLUDE [ssrb-applies-to-ssdt-yes](../../includes/ssrb-applies-to-ssdt-yes.md)]

  You specify paths in paginated report item properties to reference items such as drillthrough reports, subreports, and image files that are external to the report definition file and are stored on a report server.  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
> [!NOTE]  
>  In Report Builder, paths to items must specify items on a report server. Paths to items on a file system are not supported. You can preview a report that uses these items only if you are connected to the report server where the items are located.  
  
 When you specify a path for an external item in a dialog box for actions, subreports, or images, you can browse directly to the report server and select the item. Browsing to an item and selecting it directly is the recommended way to specify a drillthrough report or subreport. That way the correct parameter names will be available in a drop-down list when you specify report or subreport parameters. When you change an item path to point to a different item, you must manually update the correct parameter names and values as needed.  
  
 On a report server configured in native mode, specify a drillthrough report name without the file extension .rdl.  
  
 On a report server configured in SharePoint integrated mode, you must include the file extension .rdl. The path can be one of the following:  
  
-   **A relative path to the item from the main report.** For example, ../AllSubreports/Subreport1. In this example, the **..** indicates the folder above the folder where the main report is located.  
  
    > [!NOTE]  
    >  Relative paths are not supported when the report is run inside Report Builder. To view a report that uses relative paths to external items, save the report to the report server, and run the report from there  
  
-   **A full path to the item.**  
  
    -   **On a report server:** The path starts from **/**, the Home folder. For example, /Reports/AllSubreports/Subreport1.  
  
    -   **On a SharePoint site:** You must specify the report name in an expression, with the full URL of the item and the file extension .rdl. For example, `="https://server/site/library/folder/Report1.rdl"`.  
  
## See Also  
 [Add an External Image &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-an-external-image-report-builder-and-ssrs.md)   
 [Add a Subreport and Parameters &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-a-subreport-and-parameters-report-builder-and-ssrs.md)   
 [Add a Drillthrough Action on a Report &#40;Report Builder and SSRS&#41;](../../reporting-services/report-design/add-a-drillthrough-action-on-a-report-report-builder-and-ssrs.md)  
  
  
