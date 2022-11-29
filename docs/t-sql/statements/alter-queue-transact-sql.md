---
title: "ALTER QUEUE (Transact-SQL)"
description: ALTER QUEUE (Transact-SQL)
author: markingmyname
ms.author: maghan
ms.date: "05/01/2016"
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
f1_keywords:
  - "ALTER_QUEUE_TSQL"
  - "ALTER QUEUE"
helpviewer_keywords:
  - "number of queue readers"
  - "modifying queues"
  - "ALTER QUEUE statement"
  - "queue readers [SQL Server]"
  - "queues [Service Broker], modifying"
  - "unavailable queues [SQL Server]"
  - "activation stored procedures [Service Broker]"
dev_langs:
  - "TSQL"
---
# ALTER QUEUE (Transact-SQL)
[!INCLUDE [SQL Server - ASDBMI](../../includes/applies-to-version/sql-asdbmi.md)]

  Changes the properties of a queue.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```syntaxsql
ALTER QUEUE <object>   
   queue_settings  
   | queue_action  
[ ; ]  
  
<object> : :=  
{ database_name.schema_name.queue_name | schema_name.queue_name | queue_name }
  
<queue_settings> : :=  
WITH  
   [ STATUS = { ON | OFF } [ , ] ]  
   [ RETENTION = { ON | OFF } [ , ] ]  
   [ ACTIVATION (  
       { [ STATUS = { ON | OFF } [ , ] ]   
         [ PROCEDURE_NAME = <procedure> [ , ] ]  
         [ MAX_QUEUE_READERS = max_readers [ , ] ]  
         [ EXECUTE AS { SELF | 'user_name'  | OWNER } ]  
       |  DROP }  
          ) [ , ]]  
         [ POISON_MESSAGE_HANDLING (  
          STATUS = { ON | OFF } )  
         ]   
  
<queue_action> : :=  
   REBUILD [ WITH <query_rebuild_options> ]  
   | REORGANIZE [ WITH (LOB_COMPACTION = { ON | OFF } ) ]  
   | MOVE TO { file_group | "default" }  
  
<procedure> : :=  
{ database_name.schema_name.stored_procedure_name | schema_name.stored_procedure_name | stored_procedure_name }
  
<queue_rebuild_options> : :=  
{  
   ( MAXDOP = max_degree_of_parallelism )  
}  
```  
  

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments
 *database_name* (object)  
 Is the name of the database that contains the queue to be changed. When no *database_name* is provided, this defaults to the current database.  
  
 *schema_name* (object)  
 Is the name of the schema to which the new queue belongs. When no *schema_name* is provided, this defaults to the default schema for the current user.  
  
 *queue_name*  
 Is the name of the queue to be changed.  
  
 STATUS (Queue)  
 Specifies whether the queue is available (ON) or unavailable (OFF). When the queue is unavailable, no messages can be added to the queue or removed from the queue.  
  
 RETENTION  
 Specifies the retention setting for the queue. If RETENTION = ON, all messages sent or received on conversations using this queue are retained in the queue until the conversations have ended. This allows you to retain messages for auditing purposes, or to perform compensating transactions if an error occurs  
  
> [!NOTE]  
>  Setting RETENTION = ON can reduce performance. This setting should only be used if required to meet the service level agreement for the application.  
  
 ACTIVATION  
 Specifies information about the stored procedure that is activated to process messages that arrive in this queue.  
  
 STATUS (Activation)  
 Specifies whether or not the queue activates the stored procedure. When STATUS = ON, the queue starts the stored procedure specified with PROCEDURE_NAME when the number of procedures currently running is less than MAX_QUEUE_READERS and when messages arrive on the queue faster than the stored procedures receive messages. When STATUS = OFF, the queue does not activate the stored procedure.  
  
 REBUILD [ WITH \<queue_rebuild_options> ]  
 **Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later.  
  
 Rebuilds all indexes on the queue internal table. Use this capability when you are experiencing fragmentation problems due to high load. MAXDOP is the only supported queue rebuild option. REBUILD is always an offline operation.  
  
 REORGANIZE [ WITH ( LOB_COMPACTION = { ON | OFF } ) ]  
 **Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later.  
  
 Reorganize all indexes on the queue internal table.   
Unlike REORGANIZE on user tables, REORGANIZE on a queue is always performed as an offline operation because page level locks are explicitly disabled on queues.  
  
> [!TIP]  
>  For general guidance  regarding index fragmentation, when fragmentation is between 5% and 30%, reorganize the index. When fragmentation is above 30%, rebuild the index. However, these numbers are only for general guidance as a starting point for your environment. To determine the amount of index  fragmentation, use [sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md) - see example G in that article for examples.  
  
 MOVE TO { *file_group* | "default" }  
 **Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later.  
  
 Moves the queue internal table (with its indexes) to a user-specified filegroup.  The new filegroup  must not be read-only.  
  
 PROCEDURE_NAME = \<procedure>  
 Specifies the name of the stored procedure to activate when the queue contains messages to be processed. This value must be a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifier.  
  
 *database_name* (procedure)  
 Is the name of the database that contains the stored procedure.  
  
 *schema_name* (procedure)  
 Is the name of the schema that owns the stored procedure.  
  
 *stored_procedure_name*  
 Is the name of the stored procedure.  
  
 MAX_QUEUE_READERS =*max_reader*  
 Specifies the maximum number of instances of the activation stored procedure that the queue starts simultaneously. The value of *max_readers* must be a number between 0 and 32767.  
  
 EXECUTE AS  
 Specifies the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database user account under which the activation stored procedure runs. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be able to check the permissions for this user at the time that the queue activates the stored procedure. For Windows domain user, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] must be connected to the domain and able to validate the permissions of the specified user when the procedure is activated or activation fails. For a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user, the server can always check permissions.  
  
 SELF  
 Specifies that the stored procedure executes as the current user. (The database principal executing this ALTER QUEUE statement.)  
  
 '*user_name*'  
 Is the name of the user that the stored procedure executes as. *user_name* must be a valid [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] user specified as a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] identifier. The current user must have IMPERSONATE permission for the *user_name* specified.  
  
 OWNER  
 Specifies that the stored procedure executes as the owner of the queue.  
  
 DROP  
 Deletes all of the activation information associated with the queue.  
  
 POISON_MESSAGE_HANDLING  
 Specifies whether poison message handling is enabled. The default is ON.  
  
 A queue that has poison message handling set to OFF will not be disabled after five consecutive transaction rollbacks. This allows for a custom poison message handing system to be defined by the application.  
  
## Remarks  
 When a queue with a specified activation stored procedure contains messages, changing the activation status from OFF to ON immediately activates the activation stored procedure. Altering the activation status from ON to OFF stops the broker from activating instances of the stored procedure, but does not stop instances of the stored procedure that are currently running.  
  
 Altering a queue to add an activation stored procedure does not change the activation status of the queue. Changing the activation stored procedure for the queue does not affect instances of the activation stored procedure that are currently running.  
  
 [!INCLUDE[ssSB](../../includes/sssb-md.md)] checks the maximum number of queue readers for a queue as part of the activation process. Therefore, altering a queue to increase the maximum number of queue readers allows [!INCLUDE[ssSB](../../includes/sssb-md.md)] to immediately start more instances of the activation stored procedure. Altering a queue to decrease the maximum number of queue readers does not affect instances of the activation stored procedure currently running. However, [!INCLUDE[ssSB](../../includes/sssb-md.md)] does not start a new instance of the stored procedure until the number of instances for the activation stored procedure falls below the configured maximum number.  
  
 When a queue is unavailable, [!INCLUDE[ssSB](../../includes/sssb-md.md)] holds messages for services that use the queue in the transmission queue for the database. The [sys.transmission_queue](../../relational-databases/system-catalog-views/sys-transmission-queue-transact-sql.md) catalog view provides a view of the transmission queue.  
  
 If a RECEIVE statement or a GET CONVERSATION GROUP statement specifies an unavailable queue, that statement fails with a [!INCLUDE[tsql](../../includes/tsql-md.md)] error.  
  
## Permissions  
 Permission for altering a queue defaults to the owner of the queue, members of the db_ddladmin or db_owner fixed database roles, and members of the sysadmin fixed server role.  
  
## Examples  
  
### A. Making a queue unavailable  
 The following example makes the `ExpenseQueue` queue unavailable to receive messages.  
  
```sql  
ALTER QUEUE ExpenseQueue WITH STATUS = OFF ;  
```  
  
### B. Changing the activation stored procedure  
 The following example changes the stored procedure that the queue starts. The stored procedure executes as the user who ran the `ALTER QUEUE` statement.  
  
```sql  
ALTER QUEUE ExpenseQueue  
    WITH ACTIVATION (  
        PROCEDURE_NAME = new_stored_proc,  
        EXECUTE AS SELF) ;  
```  
  
### C. Changing the number of queue readers  
 The following example sets to `7` the maximum number of stored procedure instances that [!INCLUDE[ssSB](../../includes/sssb-md.md)] starts for this queue.  
  
```sql  
ALTER QUEUE ExpenseQueue WITH ACTIVATION (MAX_QUEUE_READERS = 7) ;  
```  
  
### D. Changing the activation stored procedure and the EXECUTE AS account  
 The following example changes the stored procedure that [!INCLUDE[ssSB](../../includes/sssb-md.md)] starts. The stored procedure executes as the user `SecurityAccount`.  
  
```sql  
ALTER QUEUE ExpenseQueue  
    WITH ACTIVATION (  
        PROCEDURE_NAME = AdventureWorks2012.dbo.new_stored_proc ,  
        EXECUTE AS 'SecurityAccount') ;  
```  
  
### E. Setting the queue to retain messages  
 The following example sets the queue to retain messages. The queue retains all messages sent to or from services that use this queue until the conversation that contains the message ends.  
  
```sql  
ALTER QUEUE ExpenseQueue WITH RETENTION = ON ;  
```  
  
### F. Removing activation from a queue  
 The following example removes all activation information from the queue.  
  
```  
ALTER QUEUE ExpenseQueue WITH ACTIVATION (DROP) ;  
```  
  
### G. Rebuilding queue indexes  
  
**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later.  
  
 The following example rebuilds queue indexes'  
  
```sql  
ALTER QUEUE ExpenseQueue REBUILD WITH (MAXDOP = 2)   
```  
  
### H. Reorganizing queue indexes  
  
**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later.  
  
 The following example reorganizes queue indexes  
  
```sql  
ALTER QUEUE ExpenseQueue REORGANIZE   
```  
  
### I: Moving queue internal table to another filegroup  
  
**Applies to**: [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)] and later.  
  
```sql  
ALTER QUEUE ExpenseQueue MOVE TO [NewFilegroup]   
```  
  
## See Also  
 [CREATE QUEUE &#40;Transact-SQL&#41;](../../t-sql/statements/create-queue-transact-sql.md)   
 [DROP QUEUE &#40;Transact-SQL&#41;](../../t-sql/statements/drop-queue-transact-sql.md)   
 [EVENTDATA &#40;Transact-SQL&#41;](../../t-sql/functions/eventdata-transact-sql.md)   
 [sys.dm_db_index_physical_stats &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-db-index-physical-stats-transact-sql.md)  
  
  
