---
title: "filestream access level Server Configuration Option | Microsoft Docs"
ms.custom: ""
ms.date: "03/02/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
helpviewer_keywords: 
  - "FILESTREAM [SQL Server], access level"
  - "filestream access level"
ms.assetid: b88f6ff2-795e-4730-bfb8-dbc6a958f2ad
author: MikeRayMSFT
ms.author: mikeray
manager: craigg
---
# filestream access level Server Configuration Option
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

  Use the filestream_access_level option to change the FILESTREAM access level for this instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  Before this option has any effect, the Windows administration settings for FILESTREAM must be enabled. You can enable these settings when you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or by using [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Configuration Manager.  
  
|Value|Definition|  
|-----------|----------------|  
|0|Disables FILESTREAM support for this instance.|  
|1|Enables FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] access.|  
|2|Enables FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] and Win32 streaming access.|  
  
## See Also  
 [Database Engine Configuration - Filestream](https://msdn.microsoft.com/library/641a10a1-ae52-4d26-8f1c-a032a4aeff02)   
 [Enable and Configure FILESTREAM](../../relational-databases/blob/enable-and-configure-filestream.md)  
  
  
