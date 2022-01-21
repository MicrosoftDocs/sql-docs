---
description: "sys.dm_tran_aborted_transactions (Transact-SQL)"
title: "sys.dm_tran_aborted_transactions (Transact-SQL)"
ms.custom: ""
ms.date: "01/21/2022"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, synapse-analytics, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "reference"
f1_keywords: 
  - "dm_tran_aborted_transactions"
  - "sys.dm_tran_aborted_transactions"
  - "sys.dm_tran_aborted_transactions_TSQL"
  - "dm_tran_aborted_transactions_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_tran_aborted_transactions dynamic management view"
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-mi-current||=azuresqldb-current"
---
# sys.dm_tran_aborted_transactions (Transact-SQL)
[!INCLUDE [SQL Server 2019, ASDB, ASDBMI, ASDW ](../includes/applies-to-version/sqlserver2019-asdb-asdbmi-asa.md)]

  Returns information about unresolved, aborted transactions on the SQL Server instance.
    
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|transaction_id  |  int|  The transaction id of this row.  |
|database_id  |  int|  The database id of this row.  |
|begin_xact_lsn |   numeric(25,0) | The starting LSN of the aborted transaction. |
|end_xact_lsn |  numeric(25,0) | The ending LSN of the aborted transaction. |
|begin_time |   datetime  | The begin time of the aborted transaction. |
|nest_aborted  |  bit | When 1, indicates that the transaction committed but there are portions that aborted. |
|||
  
## Permissions

On [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] and SQL Managed Instance, requires `VIEW SERVER STATE` permission.

On SQL Database **Basic**, **S0**, and **S1** service objectives, and for databases in **elastic pools**, the [server admin](/azure/azure-sql/database/logins-create-manage#existing-logins-and-user-accounts-after-creating-a-new-database) account, the [Azure Active Directory admin](/azure/azure-sql/database/authentication-aad-overview#administrator-structure) account, or membership in the `##MS_ServerStateReader##` [server role](/azure/azure-sql/database/security-server-roles) is required. On all other SQL Database service objectives, either the `VIEW DATABASE STATE` permission on the database, or membership in the `##MS_ServerStateReader##` server role is required.   

## Remarks   

Unresolved, aborted transactions will be removed by the persistent version store (PVS) cleanup process. See [Troubleshoot accelerated database recovery](accelerated-database-recovery-troubleshooting.md).

## See also

- [Accelerated database recovery](accelerated-database-recovery-concepts.md)
- [Manage accelerated database recovery](accelerated-database-recovery-management.md)
