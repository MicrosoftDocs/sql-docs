---
title: "Creating Natively Compiled Stored Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: in-memory-oltp
ms.topic: conceptual
ms.assetid: e6b34010-cf62-4f65-bbdf-117f291cde7b
author: CarlRabeler
ms.author: carlrab
manager: craigg
---
# Creating Natively Compiled Stored Procedures
  Natively compiled stored procedures do not implement the full [!INCLUDE[tsql](../../includes/tsql-md.md)] programmability and query surface area. There are certain [!INCLUDE[tsql](../../includes/tsql-md.md)] constructs that cannot be used inside natively compiled stored procedures. For more information, see [Supported Constructs in Natively Compiled Stored Procedures](../in-memory-oltp/supported-features-for-natively-compiled-t-sql-modules.md).  
  
 There are, however, several [!INCLUDE[tsql](../../includes/tsql-md.md)] features that are only supported for natively compiled stored procedures:  
  
-   Atomic blocks. For more information, see [Atomic Blocks](atomic-blocks-in-native-procedures.md).  
  
-   `NOT NULL` constraints on parameters of and variables in natively compiled stored procedures. You cannot assign `NULL` values to parameters or variables declared as `NOT NULL`. For more information, see [DECLARE @local_variable &#40;Transact-SQL&#41;](/sql/t-sql/language-elements/declare-local-variable-transact-sql).  
  
-   Schema binding of natively compiled stored procedures.  
  
 Natively compiled stored procedures are created using [CREATE PROCEDURE &#40;Transact-SQL&#41;](/sql/t-sql/statements/create-procedure-transact-sql). The following example shows a memory-optimized table and a natively compiled stored procedure used for inserting rows into the table.  
  
```tsql  
create table dbo.Ord  
(OrdNo integer not null primary key nonclustered,   
 OrdDate datetime not null,   
 CustCode nvarchar(5) not null)   
 with (memory_optimized=on)  
go  
  
create procedure dbo.OrderInsert(@OrdNo integer, @CustCode nvarchar(5))  
with native_compilation, schemabinding, execute as owner  
as   
begin atomic with  
(transaction isolation level = snapshot,  
language = N'English')  
  
  declare @OrdDate datetime = getdate();  
  insert into dbo.Ord (OrdNo, CustCode, OrdDate) values (@OrdNo, @CustCode, @OrdDate);  
end  
go  
```  
  
 In the code sample, `NATIVE_COMPILATION` indicates that this [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedure is a natively compiled stored procedure. The following options are required:  
  
|Option|Description|  
|------------|-----------------|  
|`SCHEMABINDING`|Natively compiled stored procedures must be bound to the schema of the objects it references. This means that table references by the procedure cannot be dropped. Tables referenced in the procedure must include their schema name, and wildcards (\*) are not allowed in queries. `SCHEMABINDING` is only supported for natively compiled stored procedures in this version of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)].|  
|`EXECUTE AS`|Natively compiled stored procedures do not support `EXECUTE AS CALLER`, which is the default execution context. Therefore, specifying the execution context is required. The options `EXECUTE AS OWNER`, `EXECUTE AS`*user*, and `EXECUTE AS SELF` are supported.|  
|`BEGIN ATOMIC`|The natively compiled stored procedure body must consist of exactly one atomic block. Atomic blocks guarantee atomic execution of the stored procedure. If the procedure is invoked outside the context of an active transaction, it will start a new transaction, which commits at the end of the atomic block. Atomic blocks in natively compiled stored procedures have two required options:<br /><br /> `TRANSACTION ISOLATION LEVEL`. See [Transaction Isolation Levels](../../database-engine/transaction-isolation-levels.md) for supported isolation levels.<br /><br /> `LANGUAGE`. The language for the stored procedure must be set to one of the available languages or language aliases.|  
  
 Regarding `EXECUTE AS` and Windows logins, an error can occur because of the impersonation done through `EXECUTE AS`. If a user account uses Windows Authentication, there must be full trust between the service account used for the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] instance and the domain of the Windows login. If there is not full trust, the following error message is returned when creating a natively compiled stored procedure: Msg 15404, Could not obtain information about Windows NT group/user 'username', error code 0x5.  
  
 To resolve this error, use one of the following:  
  
-   Use an account from the same domain as the Windows user for the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] service.  
  
-   If [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] is using a machine account such as Network Service or Local System, the machine must be trusted by the domain containing the Windows user.  
  
-   Use [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Authentication.  
  
 You may also see error 15517 when creating a natively compiled stored procedure. For more information, see [MSSQLSERVER_15517](../errors-events/mssqlserver-15517-database-engine-error.md).  
  
## Updating a Natively Compiled Stored Procedure  
 Performing alter operations on natively compiled stored procedures is not supported. One way to modify a natively compiled stored procedure is to drop and recreate the stored procedure:  
  
1.  Generate script for the permissions on the stored procedure.  
  
2.  Optional, generate script for the stored procedure and save as a backup.  
  
3.  Drop the stored procedure.  
  
4.  Create the altered stored procedure.  
  
5.  Re-apply the scripted permissions to the stored procedure.  
  
 The disadvantage to this procedure is the application will be offline from the start of step 3 through the completion of step 5. This may take a few seconds and the client using the application may see error messages.  
  
 Another way to (effectively) modify a natively compiled stored procedure is to first create a new version of the stored procedure. Here, the natively compiled stored procedure has an associated version number. We will call the old version SP_Vold and the new version SP_Vnew.  
  
1.  Generate script for the permissions on SP_Vold.  
  
2.  Create SP_Vnew.  
  
3.  Apply the permissions of SP_Vold to SP_Vnew.  
  
4.  Update references to SP_Vold to point to SP_Vnew. This can be accomplished in different of ways, for example:  
  
     Use a wrapper (disk-based) stored procedure, and alter that procedure to point to SP_Vnew. The disadvantage of this approach is the performance impact of the indirection.  
  
    ```tsql  
    ALTER PROCEDURE dbo.SP p1,...,pn  
    AS  
      EXEC dbo.SP_Vnew p1,...,pn  
    GO  
    ```  
  
5.  Optional, drop SP_Vold.  
  
 The advantage of this approach is that the application does not go offline. However, more work is required to maintain the references and make sure they always point to the latest version of the stored procedure.  
  
## See Also  
 [Natively Compiled Stored Procedures](natively-compiled-stored-procedures.md)  
  
  
