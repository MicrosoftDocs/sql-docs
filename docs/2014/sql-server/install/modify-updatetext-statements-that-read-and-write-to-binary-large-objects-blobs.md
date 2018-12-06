---
title: "Modify UPDATETEXT statements that read and write to binary large objects (BLOBs) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "UPDATETEXT statement"
  - "text [SQL Server], UPDATETEXT statements"
ms.assetid: b85da6a7-42f6-4707-a25e-3ded8958b94f
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Modify UPDATETEXT statements that read and write to binary large objects (BLOBs)
  Upgrade Advisor detected UPDATETEXT statements that read and write to the same binary large objects (BLOB) by using the same text pointer. [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] does not support the use of text pointers in this manner.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Corrective Action  
 Copy the BLOB to a temporary table or to a table variable, and then assign the value back to the original column.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](/sql/2014/sql-server/install/sql-server-2014-upgrade-advisor)  
  
  
