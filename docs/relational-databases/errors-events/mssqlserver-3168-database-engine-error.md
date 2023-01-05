---
description: "MSSQLSERVER_3168"
title: "MSSQLSERVER_3168 | Microsoft Docs"
ms.custom: ""
ms.date: "04/04/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: supportability
ms.topic: "reference"
helpviewer_keywords: 
  - "3168 (Database Engine error)"
ms.assetid: 991111d9-1eb3-43e9-9333-a75a775c3200
author: MashaMSFT
ms.author: mathoma
---
# MSSQLSERVER_3168
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]
  
## Details  
  
| Attribute | Value |  
| :-------- | :---- |  
|Product Name|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]|  
|Event ID|3168|  
|Event Source|MSSQLSERVER|  
|Component|SQLEngine|  
|Symbolic Name|LDDB_SYSTEMWRONGVER|  
|Message Text|The backup of the system database on the device %ls cannot be restored because it was created by a different version of the server (%ls) than this server (%ls).|  
  
## Explanation  
You cannot restore a backup of a system database (**master**, **model**, or **msdb**) on a server build that differs from the build on which the backup was originally performed.  
  
> [!NOTE]  
> Installing a service pack or a hotfix build changes the server build number, and server builds are always incremental.  
  
### Possible Causes  
The database schema for system databases may be changed across server builds. To make sure that a schema change does not cause inconsistencies, the RESTORE statement compares the server build number on the backup file to the build number of the server on which you are trying to restore the backup. If the builds are different, the statement issues the 3168 error message, and the restore operation terminates abnormally.  
  
Some scenarios in which this problem may occur include the following:  
  
-   A user tries to restore a system database on Server A from a backup taken on Server B. Servers A and B are on different server builds. For example, Server A might be on the original release version build and Server B might be on a service pack 1 (SP1) build.  
  
-   A user tries to restore a system database from a backup taken on the same server. However, the server was running a different build when the backup occurred. That is, the server was upgraded since the backup was performed.  
  
## User Action  
The restore process in this situation is fairly involved, and used only as a last resort. For more information, see"[You cannot restore system database backups to a different build of SQL Server](https://support.microsoft.com/kb/264474)".  
  
## See Also  
[Limitations on Restoring System Databases &#40;SQL Server&#41;](~/relational-databases/backup-restore/back-up-and-restore-of-system-databases-sql-server.md#limitations-on-restoring-system-databases)  
  
