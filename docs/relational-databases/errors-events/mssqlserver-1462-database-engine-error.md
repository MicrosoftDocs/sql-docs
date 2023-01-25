---
description: "MSSQLSERVER_1462"
title: "MSSQLSERVER_1462 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "1462 (Database Engine error)"
ms.assetid: 680e9c1c-a9d6-4765-b601-956d0a83324c
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_1462
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|SQL Server|  
|Event ID|1462|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|DBM_DISABLED_DUE_TO_FAILED_REDO|  
|Message Text|Database mirroring is disabled due to a failed redo operation. Unable to resume.|  
  
## Explanation  
Database mirroring failed to redo a log record on the mirror.  
  
### Possible Causes  
The most likely cause is that an add-file operation completed on the principal database but then failed on the mirror database because file names or directory structures differ on the principal server and mirror server.  
  
## User Action  
Look in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] error log for the cause of this error. Try to resolve the cause and resume mirroring on the database.  
  
## See Also  
[Troubleshoot Database Mirroring Configuration &#40;SQL Server&#41;](~/database-engine/database-mirroring/troubleshoot-database-mirroring-configuration-sql-server.md)  
  
