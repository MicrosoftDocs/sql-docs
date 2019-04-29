---
title: "Upload Files to a Folder | Microsoft Docs"
ms.date: 03/01/2017
ms.prod: reporting-services
ms.prod_service: "reporting-services-sharepoint, reporting-services-native"
ms.technology: report-server


ms.topic: conceptual
helpviewer_keywords: 
  - "publishing reports [Reporting Services], uploading files"
  - "reports [Reporting Services], publishing"
  - "uploading reports [Reporting Services]"
  - "uploading files [Reporting Services]"
  - "files [Reporting Services], uploading"
  - "files [Reporting Services]"
  - "folders [Reporting Services], uploading files to"
ms.assetid: 2f99a288-d4aa-4c64-b310-e457a2aef2c5
author: maggiesMSFT
ms.author: maggies
---
# Upload Files to a Folder
  You can upload files from the file system and store them as managed items in a report server database. What happens when you upload a file depends on the file type.  
  
-   Uploading an .rdl file is equivalent to publishing a report.  
  
-   Uploading any other file adds it to the report server database as a single binary object. These files are published to a report server as a resource. Resources can be any file type. If the file extension matches a known MIME type, an icon for that MIME type is used to identify the resource type. Otherwise, a generic file icon indicates a resource.  
  
> [!NOTE]  
>  You cannot upload a report data source (.rds) file to create a shared data source. An .rds file is used only in Report Designer. It cannot provide the content for a shared data source item that you define and manage through Report Manager. As an alternative to uploading, you can write a script that creates a shared data source based on a .rds file.  
  
 The maximum file size for uploaded items is determined by [!INCLUDE[vstecasp](../../includes/vstecasp-md.md)]. By default, the maximum size is 4 megabytes (MB).  
  
 Visually, files that you upload to a report server database are represented in the folder hierarchy with the following icons.  
  
 ![Report icon](../../reporting-services/report-server/media/hlp-16doc.gif "Report icon")  
report icon  
  
 ![Model icon](../../reporting-services/report-server/media/model-icon.gif "Model icon")  
report model icon  
  
 ![generic resource icon](../../reporting-services/report-server/media/hlp-16file.gif "generic resource icon")  
generic resource icon  
  
 When you upload a file, it is always placed in the folder that is currently selected. You can navigate to the folder that you want to contain the item first, or you can upload a file and then move it to a final location later.  
  
 To upload a file, use Report Manager. Whether you can upload files to a report server depends on tasks that are part of your role assignment. If you are using default security, local administrators can add items to a report server. If My Reports is enabled, any user who has a My Reports folder has permission to upload items to that folder. If you use custom role assignments, the role assignment must include tasks that support folder management.  
  
|To do this|Include these tasks|  
|----------------|-------------------------|  
|Upload an .rdl file to a folder|Manage reports|  
|Upload any file as a binary object|Manage resources|  
|View the contents of a folder|View resources, View reports|  
  
## See Also  
 [Report Manager  &#40;SSRS Native Mode&#41;](https://msdn.microsoft.com/library/80949f9d-58f5-48e3-9342-9e9bf4e57896)   
 [Granting Permissions on a Native Mode Report Server](../../reporting-services/security/granting-permissions-on-a-native-mode-report-server.md)   
 [Tasks and Permissions](../../reporting-services/security/tasks-and-permissions.md)   
 [Upload a File or Report &#40;Report Manager&#41;](../../reporting-services/reports/upload-a-file-or-report-report-manager.md)  
  
  
