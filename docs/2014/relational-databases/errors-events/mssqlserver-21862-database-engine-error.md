---
title: "MSSQLSERVER_21862 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "21862 (Database Engine error)"
ms.assetid: a1d393dd-453b-4d45-9aa5-7d371213e32b
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_21862
    
## Details  
  
|||  
|-|-|  
|Product Name|MSSQLSERVER|  
|Event ID|21862|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|SQLErrorNum21862|  
|Message Text|FILESTREAM columns cannot be published in a publication by using a synchronization method of either 'database snapshot' or 'database snapshot character'.|  
  
## Explanation  
 Because FILESTREAM data cannot be accessed through a database snapshot, the snapshot agent will be unable to read FILESTREAM data when either the *database snapshot* or *database_snapshot_character* parameter is specified for the synchronization method of the publication.  
  
## User Action  
 Change the publication synchronization method to something other than *database snapshot* or *database_snapshot_character*, or just exclude the FILESTREAM column from the publication.  
  
  
