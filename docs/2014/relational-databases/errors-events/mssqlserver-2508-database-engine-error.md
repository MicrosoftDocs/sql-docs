---
title: "MSSQLSERVER_2508 | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: supportability
ms.topic: conceptual
helpviewer_keywords: 
  - "2508 (Database Engine error)"
ms.assetid: c37d40e5-c665-4d66-a727-5cb845634fcc
author: MashaMSFT
ms.author: mathoma
manager: craigg
---
# MSSQLSERVER_2508
    
## Details  
  
|||  
|-|-|  
|Product Name|SQL Server|  
|Event ID|2508|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBCC_OUT_OF_DATE_PAGE_OR_ROW_COUNT|  
|Message Text|The %.*ls count for object "%.\*ls", index ID %d, partition ID %I64d, alloc unit ID %I64d (type %.\*ls) is incorrect. Run DBCC UPDATEUSAGE.|  
  
## Explanation  
 In versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] before [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)], the values for the table and index row counts and page counts can become incorrect. Databases that were created on versions prior to [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] may contain incorrect counts. DBCC CHECKDB has been enhanced to detect these errors and returns this warning message when the error encountered.  
  
## User Action  
 Run DBCC UPDATEUSAGE against the specified object or index, or against the database in which the object is contained to correct the invalid counts.  
  
  
