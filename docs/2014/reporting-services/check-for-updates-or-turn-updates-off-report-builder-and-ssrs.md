---
title: "Check for Updates or Turn Updates Off (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: 9c69792d-d7c4-453b-ae2f-6d2d071d8606
author: maggiesMSFT
ms.author: maggies
manager: kfile
---
# Check for Updates or Turn Updates Off (Report Builder and SSRS)
  Every time you open a report, Report Builder checks to see if the published instances of report parts in that report have been updated on the report server or SharePoint site integrated with a report server. It also checks for changes in the report parts' dependent items, such as the dataset and parameters. If any report parts or their dependencies have been updated on the site or server, an information bar in your report displays the number of updated parts. You can choose to view and accept or reject the updates, or dismiss the information bar.  
  
 You can also disable the information bar and not be informed if published instances of report parts have changed. This is a user setting; it will be disabled for all reports that you open. Even if you have disabled the information bar, you can still check for updates.  
  
### To turn on and off report part updates  
  
1.  Click the Report Builder button, and then click **Options**.  
  
2.  In the **Options** dialog box, on the **Resources** tab, select or clear the **Show updates to report parts in my reports** check box.  
  
> [!NOTE]  
>  This is a user setting. It will be disabled for all reports that you open.  
  
### To check for updates  
  
-   Right-click the design surface outside the report, or in the report body, and click **Check for Updates**.  
  
## See Also  
 [Report Parts &#40;Report Builder and SSRS&#41;](report-parts-report-builder-and-ssrs.md)   
 [Publish and Republish Report Parts &#40;Report Builder and SSRS&#41;](report-design/publish-and-republish-report-parts-report-builder-and-ssrs.md)   
 [Browse for Report Parts and Set a Default Folder &#40;Report Builder and SSRS&#41;](report-design/browse-for-report-parts-and-set-a-default-folder-report-builder-and-ssrs.md)   
 [Troubleshoot Report Parts &#40;Report Builder and SSRS&#41;](../../2014/reporting-services/troubleshoot-report-parts-report-builder-and-ssrs.md)   
 [Report Parts and Datasets in Report Builder](report-data/report-parts-and-datasets-in-report-builder.md)  
  
  
