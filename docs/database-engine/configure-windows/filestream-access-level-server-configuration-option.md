---
title: "filestream access level Server Configuration Option"
description: "Become familiar with the filestream_access_level option. See how it changes the FILESTREAM access level for an instance of SQL Server."
author: rwestMSFT
ms.author: randolphwest
ms.date: "03/02/2017"
ms.service: sql
ms.subservice: configuration
ms.topic: conceptual
helpviewer_keywords:
  - "FILESTREAM [SQL Server], access level"
  - "filestream access level"
---
# filestream access level Server Configuration Option
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Use the filestream_access_level option to change the FILESTREAM access level for this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  Before this option has any effect, the Windows administration settings for FILESTREAM must be enabled. You can enable these settings when you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
|Value|Definition|  
|-----------|----------------|  
|0|Disables FILESTREAM support for this instance.|  
|1|Enables FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] access.|  
|2|Enables FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] and Win32 streaming access.|  
  
## See Also  
 [Database Engine Configuration - Filestream](../install-windows/install-sql-server.md)   
 [Enable and Configure FILESTREAM](../../relational-databases/blob/enable-and-configure-filestream.md)  
  
