---
title: "WITH ROWS is not supported in CREATE STATISTICS statements in the compatibility mode of 90 or later | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "WITH ROWS in CREATE STATISTICS statement"
ms.assetid: 197b2ecf-a1a3-4a3a-a523-a0ee919c1dde
author: mashamsft
ms.author: mathoma
manager: craigg
---
# WITH ROWS is not supported in CREATE STATISTICS statements in the compatibility mode of 90 or later
  Specifying WITH ROWS in CREATE STATISTICS statements is not supported when you run [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] set to the compatibility mode of 90 or later.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Corrective Action  
 Modify CREATE STATISTICS statements that include WITH ROWS by specifying WITH SAMPLE *number* ROWS, or by specifying other options that comply with the documented syntax. For more information, see the topic "CREATE STATISTICS (Transact-SQL) in SQL Server Books Online.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](sql-server-2014-upgrade-advisor.md)  
  
  
