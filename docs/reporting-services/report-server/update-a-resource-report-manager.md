---
title: "Update a Resource (web portal) | Microsoft Docs"
description: Update a resource by using Reporting Services web portal. Replace an existing resource by importing new or different file content into the existing resource.
ms.date: 06/14/2019
ms.service: reporting-services
ms.subservice: report-server


ms.topic: conceptual
helpviewer_keywords: 
  - "updating resources"
  - "resources [Reporting Services], updating"
ms.assetid: d21f7493-bcf7-4e9e-9886-55ebdc1f1037
author: maggiesMSFT
ms.author: maggies
---
# Update a Resource (web portal)
  You can update a resource by replacing it with a newer version. Resources are items stored on a report server that contain content from a file that you upload. You can replace an existing resource by importing new or different file content into the existing resource. Updating a resource provides a way to update content while preserving existing properties and security settings on the resource.  
  
## To update a resource  
  
1.  Start [The web portal of a report server (SSRS Native Mode)](../../reporting-services/web-portal-ssrs-native-mode.md).  
  
2.  Navigate to or search for the resource you want to update.  
  
3.  Right-click the resource and select **Manage** from the drop-down menu.  
  
4.  Select the **Properties** page and then select **Replace**.  
  
5.  From the **Open** dialog box, navigate to the directory containing the file you want as the new resource.  
  
6.  Select the file that you want to use to replace the current resource. You can use an updated version of the resource file, or specify a file with a different name or file type.  
  
7.  Select **Open** to upload the resource file, and save your changes to the report server.  
  
 If the resource you are updating contains an image that is used in a report, you need to refresh the report to see the updated image.  
  
## See also  
 [Report Server Content Management (SSRS Native Mode)](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md)   
 [Upload Files to a Folder](../../reporting-services/report-server/upload-files-to-a-folder.md)   
  
