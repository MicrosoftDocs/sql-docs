---
title: "Database States | Microsoft Docs"
description: "Find out about various database states, such as ONLINE, OFFLINE, or SUSPECT. Learn how to verify the current state of a database."
ms.custom: ""
ms.date: "07/14/2016"
ms.service: sql
ms.reviewer: ""
ms.subservice: configuration
ms.topic: conceptual
f1_keywords: 
  - "SQL13.SWB.DATABASESTATES.F1"
helpviewer_keywords: 
  - "emergency database state [SQL Server]"
  - "verifying database states"
  - "viewing database states"
  - "displaying database states"
  - "database states [SQL Server]"
  - "current database state"
  - "offline database state [SQL Server]"
  - "suspect database state"
  - "recovery pending database state [SQL Server]"
  - "states [SQL Server], databases"
  - "online database state"
  - "states [SQL Server]"
  - "restoring database state [SQL Server]"
ms.assetid: b7f1f111-ca73-4a89-b567-a98d64d6ecb3
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Database States
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]
  A database is always in one specific state. For example, these states include ONLINE, OFFLINE, or SUSPECT. To verify the current state of a database, select the **state_desc** column in the [sys.databases](../../relational-databases/system-catalog-views/sys-databases-transact-sql.md) catalog view or the **Status** property in the [DATABASEPROPERTYEX](../../t-sql/functions/databasepropertyex-transact-sql.md) function.  
  
## Database State Definitions  
 The following table defines the database states.  
  
|State|Definition|  
|-----------|----------------|  
|ONLINE|Database is available for access. The primary filegroup is online, although the undo phase of recovery may not have been completed.|  
|OFFLINE|Database is unavailable. A database becomes offline by explicit user action and remains offline until additional user action is taken. For example, the database may be taken offline in order to move a file to a new disk. The database is then brought back online after the move has been completed.|  
|RESTORING|One or more files of the primary filegroup are being restored, or one or more secondary files are being restored offline. The database is unavailable.|  
|RECOVERING|Database is being recovered. The recovering process is a transient state; the database will automatically become online if the recovery succeeds. If the recovery fails, the database will become suspect. The database is unavailable.|  
|RECOVERY PENDING|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has encountered a resource-related error during recovery. The database is not damaged, but files may be missing or system resource limitations may be preventing it from starting. The database is unavailable. Additional action by the user is required to resolve the error and let the recovery process be completed.|  
|SUSPECT|At least the primary filegroup is suspect and may be damaged. The database cannot be recovered during startup of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. The database is unavailable. Additional action by the user is required to resolve the problem.|  
|EMERGENCY|User has changed the database and set the status to EMERGENCY. The database is in single-user mode and may be repaired or restored. The database is marked READ_ONLY, logging is disabled, and access is limited to members of the **sysadmin** fixed server role. EMERGENCY is primarily used for troubleshooting purposes. For example, a database marked as suspect can be set to the EMERGENCY state. This could permit the system administrator read-only access to the database. Only members of the **sysadmin** fixed server role can set a database to the EMERGENCY state.|  
  
## Related Content  
 [ALTER DATABASE &#40;Transact-SQL&#41;](../../t-sql/statements/alter-database-transact-sql.md)  
  
 [Mirroring States &#40;SQL Server&#41;](../../database-engine/database-mirroring/mirroring-states-sql-server.md)  
  
 [File States](../../relational-databases/databases/file-states.md)  
  
  
