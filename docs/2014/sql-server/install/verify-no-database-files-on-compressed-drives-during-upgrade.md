---
title: "Verify that no database files are on compressed drives during the upgrade process | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "compressed drives [SQL Server]"
ms.assetid: 63be6853-c54a-42b2-ae1a-db2175f1d28e
author: mashamsft
ms.author: mathoma
manager: craigg
---
# Verify that no database files are on compressed drives during the upgrade process
  Upgrade Advisor detected database files on a compressed drive. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] cannot create or upgrade databases on compressed drives.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## Corrective Action  
 When you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], select an uncompressed drive for system databases and verify that databases to be upgraded are not on compressed drives. However, note that after the database has been upgraded, you can put read-only databases and read-only secondary filegroups on an NTFS compressed file system.  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](sql-server-2014-upgrade-advisor.md)  
  
  
