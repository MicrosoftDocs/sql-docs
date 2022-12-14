---
title: "SET REMOTE_PROC_TRANSACTIONS (Transact-SQL)"
description: SET REMOTE_PROC_TRANSACTIONS (Transact-SQL)
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: "07/26/2017"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "REMOTE_PROC_TRANSACTIONS_TSQL"
  - "SET REMOTE_PROC_TRANSACTIONS"
  - "REMOTE_PROC_TRANSACTIONS"
  - "SET_REMOTE_PROC_TRANSACTIONS_TSQL"
helpviewer_keywords:
  - "remote stored procedures [SQL Server]"
  - "SET REMOTE_PROC_TRANSACTIONS statement"
  - "distributed transactions [SQL Server], starting"
  - "REMOTE_PROC_TRANSACTIONS option"
  - "active transactions"
dev_langs:
  - "TSQL"
---
# SET REMOTE_PROC_TRANSACTIONS (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Specifies that when a local transaction is active, executing a remote stored procedure starts a [!INCLUDE[tsql](../../includes/tsql-md.md)] distributed transaction managed by [!INCLUDE[msCoName](../../includes/msconame-md.md)] Distributed Transaction Coordinator (MS DTC).  
  
> [!IMPORTANT]  
>  [!INCLUDE[ssNoteDepFutureAvoid](../../includes/ssnotedepfutureavoid-md.md)] This option is provided for backward compatibility for applications that use remote stored procedures. Instead of issuing remote stored procedure calls, use distributed queries that reference linked servers. These are defined by using [sp_addlinkedserver](../../relational-databases/system-stored-procedures/sp-addlinkedserver-transact-sql.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
  
SET REMOTE_PROC_TRANSACTIONS { ON | OFF }   
```  
  
[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 ON | OFF  
 When ON, a [!INCLUDE[tsql](../../includes/tsql-md.md)] distributed transaction is started when a remote stored procedure is executed from a local transaction. When OFF, calling remote stored procedures from a local transaction does not start a [!INCLUDE[tsql](../../includes/tsql-md.md)] distributed transaction.  
  
## Remarks  
 When REMOTE_PROC_TRANSACTIONS is ON, calling a remote stored procedure starts a distributed transaction and enlists the transaction with MS DTC. The instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] making the remote stored procedure call is the transaction originator and controls the completion of the transaction. When a subsequent COMMIT TRANSACTION or ROLLBACK TRANSACTION statement is issued for the connection, the controlling instance requests that MS DTC manage the completion of the distributed transaction across the computers involved.  
  
 After a [!INCLUDE[tsql](../../includes/tsql-md.md)] distributed transaction has been started, remote stored procedure calls can be made to other instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that have been defined as remote servers. The remote servers are all enlisted in the [!INCLUDE[tsql](../../includes/tsql-md.md)] distributed transaction, and MS DTC ensures that the transaction is completed against each remote server.  
  
 REMOTE_PROC_TRANSACTIONS is a connection-level setting that can be used to override the instance-level **sp_configure remote proc trans** option.  
  
 When REMOTE_PROC_TRANSACTIONS is OFF, remote stored procedure calls are not made part of a local transaction. The modifications made by the remote stored procedure are committed or rolled back at the time the stored procedure completes. Subsequent COMMIT TRANSACTION or ROLLBACK TRANSACTION statements issued by the connection that called the remote stored procedure have no effect on the processing done by the procedure.  
  
 The REMOTE_PROC_TRANSACTIONS option is a compatibility option that affects only remote stored procedure calls made to instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] defined as remote servers using **sp_addserver**. The option does not apply to distributed queries that execute a stored procedure on an instance defined as a linked server using **sp_addlinkedserver**.  
  
 The setting of SET REMOTE_PROC_TRANSACTIONS is set at execute or run time and not at parse time.  
  
## Permissions  
 Requires membership in the **public** role.  
  
## See Also  
 [BEGIN DISTRIBUTED TRANSACTION &#40;Transact-SQL&#41;](../../t-sql/language-elements/begin-distributed-transaction-transact-sql.md)   
 [SET Statements &#40;Transact-SQL&#41;](../../t-sql/statements/set-statements-transact-sql.md)  
  
  
