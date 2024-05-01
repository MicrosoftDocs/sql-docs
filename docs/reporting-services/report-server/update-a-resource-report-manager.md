---
title: "Update a resource (web portal)"
description: Update a resource by using Reporting Services web portal. Replace an existing resource by importing new or different file content into the existing resource.
author: maggiesMSFT
ms.author: maggies
ms.date: 06/14/2019
ms.service: reporting-services
ms.subservice: report-server
ms.topic: conceptual
ms.custom: updatefrequency5
helpviewer_keywords:
  - "updating resources"
  - "resources [Reporting Services], updating"
---
# Update a resource (web portal)
  You can update a resource by replacing it with a newer version. Resources are items stored on a report server that contain content from a file that you upload. You can replace an existing resource by importing new or different file content into the existing resource. Updating a resource provides a way to update content while preserving existing properties and security settings on the resource.  
  
## Update a resource  
  
1.  Start [The web portal of a report server (SSRS native mode)](../../reporting-services/web-portal-ssrs-native-mode.md).  
  
1.  Navigate to or search for the resource you want to update.  
  
1.  Right-click the resource and select **Manage** from the menu.  
  
1.  Select the **Properties** page, and then choose **Replace**.  
  
1.  From the **Open** dialog, navigate to the directory containing the file you want as the new resource.  
  
1.  Select the file that you want to use to replace the current resource. You can use an updated version of the resource file, or specify a file with a different name or file type.  
  
7.  Select **Open** to upload the resource file, and save your changes to the report server.  
  
 If the resource you're updating contains an image that is used in a report, you need to refresh the report to see the updated image.  
  
## Related content
 [Report Server content management (SSRS native mode)](../../reporting-services/report-server/report-server-content-management-ssrs-native-mode.md)   
 [Upload files to a folder](../../reporting-services/report-server/upload-files-to-a-folder.md)   
  
