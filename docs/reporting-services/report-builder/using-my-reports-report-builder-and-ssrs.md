---
title: "Using My Reports (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "reporting-services-sharepoint"
  - "reporting-services-native"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 49c3c1da-b106-41f6-9173-16ff225bade8
caps.latest.revision: 8
author: "guyinacube"
ms.author: "asaxton"
manager: "erikre"
---
# Using My Reports (Report Builder and SSRS)
  On a report server configured in native mode, the My Reports folder is a personal workspace that you can use to store and work with reports that you own. Other report server folders are public and typically require users to have advanced permissions to add to or modify folder contents. In contrast, the My Reports folder is a user-managed workspace. You can add or remove reports and folders and save linked reports with personalized settings.  
  
 Conceptually, the My Reports folder is similar to the My Documents folder in the Windows file system. Although each user has a folder called My Reports, the folder that each accesses is different from all other users. Except for report server administrators, other users cannot access the contents of the My Reports folder that belongs to you.  
  
 The My Reports feature is optional and can be disabled by a report server administrator. If it is enabled, you will see a My Reports folder in the Home folder, which you can access using the Report Manager or a Web browser. For more information, see [Finding and Viewing Reports in Report Manager &#40;Report Builder and SSRS&#41;](finding-and-viewing-reports-with-a-browser-report-builder-and-ssrs.md).  
  
 On a report server configured in SharePoint integrated mode, there is no equivalent to the My Reports folder. For more information, see [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md).  
  
> [!NOTE]  
>  [!INCLUDE[ssRBRDDup](../../includes/ssrbrddup-md.md)]  
  
## Ways to Use My Reports  
 My Reports is empty until you add reports, folders, and other items. Here are some ways to add content to My Reports.  
  
-   Create a personal linked report and store it in My Reports. Not all reports are available for linking. For more information, see [Create a Linked Report](../../reporting-services/reports/create-a-linked-report.md).  
  
-   Upload a report definition (.rdl) file, report model (.smdl) file, or other files from the file system. You can upload any file, but the report server only processes report files that have an .rdl or .smdl file extension. For more information, see Report Definitions" in the [Reporting Services documentation](http://go.microsoft.com/fwlink/?linkid=121312) in SQL Server Books Online and [Upload a File or Report &#40;Report Manager&#41;](../../reporting-services/reports/upload-a-file-or-report-report-manager.md).  
  
-   Create and publish your own reports to My Reports. For more information, see [Report Design View &#40;Report Builder&#41;](../../reporting-services/report-builder/report-design-view-report-builder.md).  
  
 Usually, permissions on My Reports allow you to manage the folder yourself. However, the report server administrator ultimately determines which tasks users can perform. If insufficient permissions prevent you from working with My Reports, see your report server administrator.  
  
## Searching My Reports  
 When you search a report server database, the contents of your My Reports folder are included in the search, while the contents of other user's My Reports folders are excluded. Search results list only the reports to which you have access.  
  
## See Also  
 [Finding, Viewing, and Managing Reports &#40;Report Builder and SSRS &#41;](../../reporting-services/report-builder/finding-viewing-and-managing-reports-report-builder-and-ssrs.md)  
  
  