---
title: "sp_polybase_leave_group (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.technology: polybase
ms.topic: conceptual
f1_keywords: 
  - "sp_polybase_leave_group"
  - "sp_polybase_leave_group_TSQL"
helpviewer_keywords: 
  - "sp_polybase_leave_group"
ms.assetid: ef87a8f1-5407-47b5-b8bf-bd7d08c0f0fe
author: rothja
ms.author: jroth
manager: craigg
---
# sp_polybase_leave_group (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Removes a SQL Server instance from a PolyBase group for scale-out computation. 
 
 The SQL Server instance must have the  [PolyBase Guide](../../relational-databases/polybase/polybase-guide.md) feature installed.  PolyBase enables the integration of non-SQL Server data sources, such as Hadoop and Azure blob storage. See also [sp_polybase_join_group](../../relational-databases/system-stored-procedures/polybase-stored-procedures-sp-polybase-join-group.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_polybase_leave_group;  
  
```  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Permissions  
 Requires CONTROL SERVER  permission.  
  
## Remarks  
 You can only remove a compute node from a group.  
  
 After running the stored procedure, restart the PolyBase engine and PolyBase Data Movement Service on the machine. To verify run the following DMV on the head node: **sys.dm_exec_compute_nodes**.  
  
## Example  
 The example removes  the current machine from a PolyBase group.  
  
```sql  
EXEC sp_polybase_leave_group ;  
```  
  
## See Also  
 [Get started with PolyBase](../../relational-databases/polybase/get-started-with-polybase.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
