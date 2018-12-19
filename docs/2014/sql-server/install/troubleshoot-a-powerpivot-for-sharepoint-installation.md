---
title: "Troubleshoot a PowerPivot for SharePoint Installation | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
ms.assetid: 97bc2ce7-af04-4372-ad79-c96b8c3417ab
author: markingmyname
ms.author: maghan
manager: craigg
---
# Troubleshoot a PowerPivot for SharePoint Installation
  If you get errors instead of the pages and features you expect, do the following.  
  
-   Review release notes for both SharePoint and [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] to get workarounds for known installation problems. Release notes are provided with the installation media or on the Microsoft site from which you downloaded the software.  
  
    -   [SQL Server 2014 Release Notes](https://technet.microsoft.com/library/dn169381\(v=sql.15\).aspx).  
  
-   See the Technet wiki topic, [Troubleshooting Installations of PowerPivot (and other add-ins)](https://social.technet.microsoft.com/wiki/contents/articles/13737.troubleshooting-installations-of-powerpivot-and-other-add-ins.aspx).  
  
## Issues  
  
### PowerPivot Gallery Thumbnail images show as a red X  
 One Possible cause is the **PowerPivot features Integration for Site Collections** is not active. Complete the following:  
  
1.  In the PowerPivot Gallery library, click **Site Settings** from either the gear icon ![SharePoint Settings](../../../2014/analysis-services/media/as-sharepoint2013-settings-gear.gif "SharePoint Settings") or the **Home** list.  
  
2.  In the **Site Collection Administration** section, click **Site Collection Features**.  
  
3.  Click **Site Collection Features**.  
  
4.  Verify **PowerPivot features Integration for Site Collections** is **Active**.  
  
 For additional causes of this issue, see [PowerPivot Gallery shows Red X's for Icons](https://support.microsoft.com/kb/2361559) (https://support.microsoft.com/kb/2361559).  
  
  
