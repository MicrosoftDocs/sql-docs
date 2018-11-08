---
title: "0xFFFF character is not valid as an object identifier | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: 
  - "database-engine"
ms.topic: conceptual
helpviewer_keywords: 
  - "0xFFFF character [SQL Server]"
ms.assetid: b2c9c8cf-9194-45e0-be6b-2d5ec52e8153
author: mashamsft
ms.author: mathoma
manager: craigg
---
# 0xFFFF character is not valid as an object identifier
  Upgrade Advisor has detected the 0xFFFF character in an object identifier. In [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later, objects such as databases, tables, and columns that contain this character in their identifiers cannot be referenced or renamed when the database compatibility mode is set to 90 or later. When you upgrade to [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)], user databases maintain their compatibility mode. Before you change the database compatibility mode to 90 or later, rename the object that contains the 0xFFFF character.  
  
 For more information about identifiers, see "Identifiers" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online. For more information about database compatibility modes, see "sp_dbcmptlevel" in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Books Online.  
  
## Component  
 [!INCLUDE[ssDE](../../includes/ssde-md.md)]  
  
## See Also  
 [Database Engine Upgrade Issues](../../../2014/sql-server/install/database-engine-upgrade-issues.md)   
 [SQL Server 2014 Upgrade Advisor &#91;new&#93;](/sql/2014/sql-server/install/sql-server-2014-upgrade-advisor)  
  
  
