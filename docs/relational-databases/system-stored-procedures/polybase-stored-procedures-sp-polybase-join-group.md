---
title: "sp_polybase_join_group | Microsoft Docs"
ms.custom: ""
ms.date: "05/24/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: conceptual
f1_keywords: 
  - "sp_polybase_join_group"
helpviewer_keywords: 
  - "PolyBase"
ms.assetid: 48066431-fed2-4a8a-85af-ac704689e183
author: rothja
ms.author: jroth
manager: craigg
---
# sp_polybase_join_group (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Adds a SQL Server instance as a compute node to a PolyBase group for scale-out computation.  
  
 The SQL Server instance must have the  [PolyBase](../../relational-databases/polybase/polybase-guide.md) feature installed.  PolyBase enables the integration of non-SQL Server data sources, such as Hadoop and Azure blob storage. See also [sp_polybase_leave_group &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/polybase-stored-procedures-sp-polybase-leave-group.md).  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
sp_polybase_join_group (@head_node_address = N'head_node_address',  
    @dms_control_channel_port = dms_control_channel_port,  
    @head_node_sql_server_instance_name = head_node_sql_server_instance_name)  
[ ; ]          
```  
  
## Arguments  
 *@head_node_address* = N'*head_node_address*'  
 The name of the machine that hosts the SQL Server head node of the PolyBase scale-out group. *@head_node_address* is nvarchar(255).  
  
 *@dms_control_channel_port* = dms_control_channel_port  
 The port where the control channel for the head node PolyBase Data Movement Service is running. *@dms_control_channel_port* is an unsigned __int16. The default is **16450**.  
  
 *@head_node_sql_server_instance_name* = head_node_sql_server_instance_name  
 The name of the head node SQL Server instance in the PolyBase scale-out group. *@head_node_sql_server_instance_name* is nvarchar(16).  
  
## Return Code Values  
 0 (success) or 1 (failure)  
  
## Permissions  
 Requires CONTROL SERVER permission.  
  
## Remarks  
 After running the stored procedure, shut down the PolyBase engine, and restart the PolyBase Data Movement Service on the machine. To verify run the following DMV on the head node: **sys.dm_exec_compute_nodes**.  
  
## Example  
 The example joins the current machine as a compute node to a PolyBase group.  The name of the head node is **HST01** and the name of the SQL Server instance on the head node is **MSSQLSERVER**.  
  
```  
EXEC sp_polybase_join_group N'HST01', 16450, N'MSSQLSERVER'   
```  
  
## See Also  
 [Get started with PolyBase](../../relational-databases/polybase/get-started-with-polybase.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)  
  
  
