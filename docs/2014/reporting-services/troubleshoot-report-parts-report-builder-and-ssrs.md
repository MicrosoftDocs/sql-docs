---
title: "Troubleshoot Report Parts (Report Builder and SSRS) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "reporting-services-native"
ms.topic: conceptual
ms.assetid: d9fe1932-46e7-421b-a8a9-4c54d9576e94
author: markingmyname
ms.author: maghan
manager: kfile
---
# Troubleshoot Report Parts (Report Builder and SSRS)
  These tips can help when working with report parts.  
  
## Why do my co-worker and I see different instances of the same report part when we search for it?  
 There can be multiple instances of a single report part on a report server or SharePoint site integrated with a report server, and all instances will have the same globally unique identifier (GUID). Only the most recent instance is displayed in the list of search results. In some situations, different instances of a single report part can have different permissions. If your co-worker and you have different permissions on the server, you will not see the same instance. For example, say that multiple copies of a report part, all with the same GUID, are saved in different folders on a report server in SharePoint integrated mode. If the folders have different permissions, then the report parts in those folders will also have different permissions.  
  
 To see what permissions you and your co-worker have, ask your report server administrator.  
  
## When I search for report parts that I uploaded to a SharePoint server, I do not see them. Why not?  
 Report Parts that you have manually uploaded to a SharePoint document library, instead of published by using Report Builder, might not appear in the Report Part Gallery. The report server used for the gallery search might need to be synchronized with the contents of the SharePoint document library. For more information, see [Activate the Report Server File Sync Feature in SharePoint Central Administration](../../2014/reporting-services/activate-report-server-file-sync-feature-sharepoint-central-administration.md) in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] [Books Online](https://go.microsoft.com/fwlink/?LinkId=154888) on msdn.microsoft.com.  
  
## Why can't others see the image in their reports?  
 If you publish a report part that is a link to an image file, the report part is really just a link. If others cannot see the image when they add the image report part to their reports, they might not have permissions for the image that you are linking to.  
  
 There are a few possible solutions to this:  
  
-   Make the image file a report part, instead of making the link to the image file the report part.  
  
-   Move the image file to a location for which others have permissions.  
  
-   Ask the report server administrator to change permissions.  
  
## Why do I get a "circular reference" error message when I try to publish my report part?  
 If report items have a circular reference, you won't be able to publish them as report parts. For example, a report item points to a dataset, which in turn points to a parameter. The parameter, in turn, points to the dataset¸ too. You'll need to delete one of the references first before you can publish the report part.  
  
## See Also  
 [Report Parts &#40;Report Builder and SSRS&#41;](report-parts-report-builder-and-ssrs.md)  
  
  
