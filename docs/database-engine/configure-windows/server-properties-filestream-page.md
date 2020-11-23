---
title: "SQL Server Properties (FILESTREAM Page) | Microsoft Docs"
description: Get acquainted with FILESTREAM settings in SQL Server. Learn how to turn on FILESTREAM, and see how to configure remote client access and other properties.
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: high-availability
ms.reviewer: ""
ms.technology: configuration
ms.topic: conceptual
f1_keywords: 
  - "sql13.swb.serverproperties.filestream.f1"
helpviewer_keywords: 
  - "FILESTREAM [SQL Server], properties page"
ms.assetid: 8a8d38d3-e97a-4b09-a40b-659b2e3a5c47
author: markingmyname
ms.author: maghan
---
# Server Properties - FILESTREAM Page
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  Use this page to enable FILESTREAM for this installation of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].  
  
## UI element list  
 **Enable FILESTREAM for Transact-SQL access**  
 Select to enable FILESTREAM for [!INCLUDE[tsql](../../includes/tsql-md.md)] access. This control must be checked before the other control options will be available.  
  
 **Enable FILESTREAM for file I/O streaming access**  
 Select to enable Win32 streaming access for FILESTREAM.  
  
 **Windows share name**  
 Use this control to enter the name of the Windows share in which the FILESTREAM data will be stored.  
  
 **Allow remote clients to have streaming access to FILESTREAM data**  
 Select this control to allow remote clients to access this FILESTREAM data on this server.  
  
## See Also  
 [FILESTREAM &#40;SQL Server&#41;](../../relational-databases/blob/filestream-sql-server.md)   
 [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md)  
  
  
