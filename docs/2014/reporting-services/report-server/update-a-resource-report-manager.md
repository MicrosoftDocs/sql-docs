---
title: "Update a Resource (Report Manager) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
helpviewer_keywords: 
  - "updating resources"
  - "resources [Reporting Services], updating"
ms.assetid: d21f7493-bcf7-4e9e-9886-55ebdc1f1037
author: markingmyname
ms.author: maghan
manager: craigg
---
# Update a Resource (Report Manager)
  You can update a resource by replacing it with a newer version. Resources are items stored on a report server that contain content from a file that you upload. You can replace an existing resource by importing new or different file content into the existing resource. Updating a resource provides a way to update content while preserving existing properties and security settings on the resource.  
  
### To update a resource  
  
1.  Start [Report Manager  &#40;SSRS Native Mode&#41;](../report-manager-ssrs-native-mode.md).  
  
2.  In Report Manager, navigate to or search for the resource you want to update.  
  
3.  Click the resource to open it in the **View** page.  
  
4.  Click **Properties** to open the **General** properties page.  
  
5.  Click **Replace** to open the **Import Resource** page.  
  
6.  Click **Browse**.  
  
7.  Select the file that you want to use to replace the current resource. You can use an updated version of the resource file, or specify a file with a different name or file type.  
  
8.  Click **OK** to upload the resource file, close the **Import Resource** page, and save your changes to the report server.  
  
 If the resource you are updating contains an image that is used in a report, you need to refresh the report to see the updated image.  
  
## See Also  
 [Contents Page &#40;Report Manager&#41;](../contents-page-report-manager.md)   
 [Upload File Page &#40;Report Manager&#41;](../upload-file-page-report-manager.md)   
 [Upload Files to a Folder](upload-files-to-a-folder.md)   
 [Report Manager F1 Help](../report-manager-f1-help.md)  
  
  
