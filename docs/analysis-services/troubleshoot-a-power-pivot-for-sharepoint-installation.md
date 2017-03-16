---
title: "Troubleshoot a Power Pivot for SharePoint Installation | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 97bc2ce7-af04-4372-ad79-c96b8c3417ab
caps.latest.revision: 10
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# Troubleshoot a Power Pivot for SharePoint Installation
  If you get errors instead of the pages and features you expect, do the following.  
  
-   Review release notes for both SharePoint and [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] to get workarounds for known installation problems. Release notes are provided with the installation media or on the Microsoft site from which you downloaded the software.  
  
    -   [SQL Server 2016 Release Notes](../sql-server/sql-server-2016-release-notes.md)  
  
-   See the TechNet wiki topic, [Troubleshooting Installations of Power Pivot (and other add-ins)](http://social.technet.microsoft.com/wiki/contents/articles/13737.troubleshooting-installations-of-powerpivot-and-other-add-ins.aspx).  
  
## Issues  
  
### Power Pivot Gallery Thumbnail images show as a red X  
 One Possible cause is the **[!INCLUDE[ssGemini](../includes/ssgemini-md.md)] features Integration for Site Collections** is not active. Complete the following:  
  
1.  In the [!INCLUDE[ssGemini](../includes/ssgemini-md.md)] Gallery library, click **Site Settings** from either the gear icon ![SharePoint Settings](../analysis-services/media/as-sharepoint2013-settings-gear.gif "SharePoint Settings") or the **Home** list.  
  
2.  In the **Site Collection Administration** section, click **Site Collection Features**.  
  
3.  Click **Site Collection Features**.  
  
4.  Verify **[!INCLUDE[ssGemini](../includes/ssgemini-md.md)] features Integration for Site Collections** is **Active**.  
  
 For additional causes of this issue, see [Power Pivot Gallery shows Red X's for Icons](http://support.microsoft.com/kb/2361559) (http://support.microsoft.com/kb/2361559).  
  
  