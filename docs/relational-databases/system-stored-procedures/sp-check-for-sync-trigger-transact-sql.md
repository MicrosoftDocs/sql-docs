---
title: "sp_check_for_sync_trigger (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: replication
ms.topic: "language-reference"
f1_keywords: 
  - "filter_TSQL"
  - "sp_check_for_sync_trigger"
helpviewer_keywords: 
  - "sp_check_for_sync_trigger"
ms.assetid: 54a1e2fd-c40a-43d4-ac64-baed28ae4637
author: stevestein
ms.author: sstein
manager: craigg
---
# sp_check_for_sync_trigger (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Determines whether a user-defined trigger or stored procedure is being called in the context of a replication trigger that is used for immediate updating subscriptions. This stored procedure is executed at the Publisher on the publication database or at the Subscriber on the subscription database.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_check_for_sync_trigger [ @tabid = ] 'tabid'   
    [ , [ @trigger_op = ] 'trigger_output_parameters' OUTPUT ]  
    [ , [ @fonpublisher = ] fonpublisher ]  
```  
  
## Arguments  
 [**@tabid =** ] '*tabid*'  
 Is the object ID of the table being checked for immediate updating triggers. *tabid* is **int** with no default.  
  
 [**@trigger_op =** ] '*trigger_output_parameters*' OUTPUT  
 Specifies if the output parameter is to return the type of trigger it is being called from. *trigger_output_parameters* is **char(10)** and can be one of these values.  
  
|Value|Description|  
|-----------|-----------------|  
|**Ins**|INSERT trigger|  
|**Upd**|UPDATE trigger|  
|**Del**|DELETE trigger|  
|NULL (default)||  
  
`[ @fonpublisher = ] fonpublisher`
 Specifies the location where the stored procedure is executed. *fonpublisher* is **bit**, with a default value of 0. If 0, the execution is at the Subscriber, and if 1, the execution is at the Publisher.  
  
## Return Code Values  
 0 indicates that the stored procedure is not being called within the context of an immediate-updating trigger. 1 indicates that it is being called within the context of an immediate-updating trigger and is the type of trigger being returned in *@trigger_op*.  
  
## Remarks  
 **sp_check_for_sync_trigger** is used in snapshot replication and transactional replication.  
  
 **sp_check_for_sync_trigger** is used to coordinate between replication and user-defined triggers. This stored procedure determines if it is being called within the context of a replication trigger. For example, you can call the procedure **sp_check_for_sync_trigger** in the body of a user-defined trigger. If **sp_check_for_sync_trigger** returns **0**, the user-defined trigger continues processing. If **sp_check_for_sync_trigger** returns **1**, the user-defined trigger exits. This ensures that the user-defined trigger does not fire when the replication trigger updates the table.  
  
## Example  
 The following example shows code that could be used in a trigger on a Subscriber table.  
  
```  
DECLARE @retcode int, @trigger_op char(10), @table_id int  
SELECT @table_id = object_id('tablename')  
EXEC @retcode = sp_check_for_sync_trigger @table_id, @trigger_op OUTPUT  
IF @retcode = 1  
RETURN  
```  
  
## Example  
 The code can also be added to a trigger on a table at the Publisher; the code is similar, but the call to **sp_check_for_sync_trigger** includes an additional parameter.  
  
```  
DECLARE @retcode int, @trigger_op char(10), @table_id int, @fonpublisher int  
SELECT @table_id = object_id('tablename')  
SELECT @fonpublisher = 1  
EXEC @retcode = sp_check_for_sync_trigger @table_id, @trigger_op OUTPUT, @fonpublisher  
IF @retcode = 1  
RETURN  
```  
  
## Permissions  
 **sp_check_for_sync_trigger** stored procedure can be executed by any user with SELECT permissions in the [sys.objects](../../relational-databases/system-catalog-views/sys-objects-transact-sql.md) system view.  
  
## See Also  
 [Updatable Subscriptions for Transactional Replication](../../relational-databases/replication/transactional/updatable-subscriptions-for-transactional-replication.md)  
  
  
