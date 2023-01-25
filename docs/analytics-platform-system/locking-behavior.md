---
title: Locking behavior
description: Learn how Parallel Data Warehouse uses locking to ensure the integrity of transactions and to maintain the consistency of databases when multiple users are accessing data at the same time.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 04/17/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---

# Locking behavior in Parallel Data Warehouse
Learn how Parallel Data Warehouse uses locking to ensure the integrity of transactions and to maintain the consistency of databases when multiple users are accessing data at the same time.  
  
## <a name="Basics"></a>Locking Basics  
**Modes**  
  
SQL Server PDW supports four locking modes:  
  
Exclusive  
The Exclusive lock prohibits writing to or reading from the locked object until the transaction holding the Exclusive lock completes. No other locks of any mode are permitted while the Exclusive lock is in effect. For example, DROP TABLE and CREATE DATABASE use an Exclusive lock.  
  
Shared  
The Shared lock prohibits initiation of an Exclusive lock on the affected object, but allows all other lock modes. For example, the SELECT statement initiates a Shared lock, and therefore allows multiple queries to access the selected data concurrently, but prevents updates to the records being read, until the SELECT statement completes.  
  
ExclusiveUpdate  
The ExclusiveUpdate lock prohibits writing to the locked object, but does allow reading via the Shared lock. No other locks are permitted while the ExclusiveUpdate lock is in effect. For example, BACKUP DATABASE and RESTORE DATABASE use an Exclusive Update lock.  
  
SharedUpdate  
The SharedUpdate lock prohibits Exclusive and ExclusiveUpdate lock modes and allows Shared and SharedUpdate lock modes on the object. SharedUpdate modifies an object, but does not restrict read access to it during the modification. For example, INSERT and UPDATE use a SharedUpdate lock.  
  
**Resource Classes**  
  
Locks are held on the following classes of objects: DATABASE, SCHEMA, OBJECT (a table, view, or procedure), APPLICATION (used internally), EXTERNALDATASOURCE, EXTERNALFILEFORMAT AND SCHEMARESOLUTION (a database level lock taken while creating, altering, or dropping schema objects or database users). These object classes can appear in the object_type column of [sys.dm_pdw_waits](../relational-databases/system-dynamic-management-views/sys-dm-pdw-waits-transact-sql.md).  
  
## <a name="Remarks"></a>General Remarks  
Locks can be applied to databases, tables, or views.  
  
SQL Server PDW does not implement any configurable isolation levels. It supports the  READ_UNCOMMITTED isolation level as defined by the ANSI standard. However, since read operations are run under READ_UNCOMMITTED, very few blocking operations actually occur or lead to contention in the system.  
  
SQL Server PDW relies on the underlying SQL Server engine to implement locking and concurrency control. If operations lead to an underlying SQL Server deadlock within the same node, SQL Server PDW leverages the SQL Server deadlock detection capability and terminates one of the blocking statements.  
  
> [!NOTE]  
> SQL Server does not allow statements that are waiting for locks to be blocked by newer lock requests. SQL Server PDW has not fully implemented this process. In SQL Server PDW, continuous requests for new shared locks can sometimes block a previous (but waiting) request for an exclusive lock. For example, an **UPDATE** statement (requiring an exclusive lock) can be blocked by shared locks that are granted for series of **SELECT** statements. To resolve a blocked process (identified by reviewing the [sys.dm_pdw_waits](../relational-databases/system-dynamic-management-views/sys-dm-pdw-waits-transact-sql.md) DVM), stop submitting new requests until the exclusive lock has been satisfied.  
  
## Lock Definition Table  
SQL Server supports the following types of locks. Not all lock types are available on the control node, but could occur on the compute nodes.  
  
-   Sch-S (Schema stability). Ensures that a schema element, such as a table or index, is not dropped while any session holds a schema stability lock on the schema element.  
  
-   Sch-M (Schema modification). Must be held by any session that wants to change the schema of the specified resource. Ensures that no other sessions are referencing the indicated object.  
  
-   S (Shared). The holding session is granted shared access to the resource.  
  
-   U (Update). Indicates an update lock acquired on resources that may eventually be updated. It is used to prevent a common form of deadlock that occurs when multiple sessions lock resources for potential update in the future.  
  
-   X (Exclusive). The holding session is granted exclusive access to the resource.  
  
-   IS (Intent Shared). Indicates the intention to place S locks on some subordinate resource in the lock hierarchy.  
  
-   IU (Intent Update). Indicates the intention to place U locks on some subordinate resource in the lock hierarchy.  
  
-   IX (Intent Exclusive). Indicates the intention to place X locks on some subordinate resource in the lock hierarchy.  
  
-   SIU (Shared Intent Update). Indicates shared access to a resource with the intent of acquiring update locks on subordinate resources in the lock hierarchy.  
  
-   SIX (Shared Intent Exclusive). Indicates shared access to a resource with the intent of acquiring exclusive locks on subordinate resources in the lock hierarchy.  
  
-   UIX (Update Intent Exclusive). Indicates an update lock hold on a resource with the intent of acquiring exclusive locks on subordinate resources in the lock hierarchy.  
  
-   BU. Used by bulk operations.  
  
-   RangeS_S (Shared Key-Range and Shared Resource lock). Indicates serializable range scan.  
  
-   RangeS_U (Shared Key-Range and Update Resource lock). Indicates serializable update scan.  
  
-   RangeI_N (Insert Key-Range and Null Resource lock). Used to test ranges before inserting a new key into an index.  
  
-   RangeI_S. Key-Range Conversion lock, created by an overlap of RangeI_N and S locks.  
  
-   RangeI_U. Key-Range Conversion lock, created by an overlap of RangeI_N and U locks.  
  
-   RangeI_X. Key-Range Conversion lock, created by an overlap of RangeI_N and X locks.  
  
-   RangeX_S. Key-Range Conversion lock, created by an overlap of RangeI_N and RangeS_S. locks.  
  
-   RangeX_U. Key-Range Conversion lock, created by an overlap of RangeI_N and RangeS_U locks.  
  
-   RangeX_X (Exclusive Key-Range and Exclusive Resource lock). This is a conversion lock used when updating a key in a range.  
  
## See Also  
<!-- MISSING LINKS 
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
-->
[sys.dm_pdw_waits](../relational-databases/system-dynamic-management-views/sys-dm-pdw-waits-transact-sql.md)  
  
