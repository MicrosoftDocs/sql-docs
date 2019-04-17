---
title: "SQL Server, User Settable Object | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: performance
ms.topic: conceptual
helpviewer_keywords: 
  - "User Settable object"
  - "SQLServer:User Settable"
ms.assetid: 633de3ef-533c-4f0c-9c7b-c105129d8e94
author: julieMSFT
ms.author: jrasnick
manager: craigg
---
# SQL Server, User Settable Object
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  The **User Settable** object in Microsoft [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] allows you to create custom counter instances. Use custom counter instances to monitor aspects of the server not monitored by existing counters, such as components unique to your [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database (for example, the number of customer orders logged or the product inventory).  
  
 The **User Settable** object contains 10 instances of the query counter: **User counter 1** through **User counter 10**. These counters map to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] stored procedures **sp_user_counter1** through **sp_user_counter10**. As these stored procedures are executed by user applications, the values set by the stored procedures are displayed in System Monitor. A counter can monitor any single integer value (for example, a stored procedure that counts how many orders for a particular product have occurred in one day).  
  
> [!NOTE]  
>  The user counter stored procedures are not polled automatically by System Monitor. They must be explicitly executed by a user application for the counter values to be updated. Use a trigger to update the value of the counter automatically. For example, to create a counter that monitors the number of rows in a table, create an INSERT and DELETE trigger on the table that executes the following statement: `SELECT COUNT(*) FROM table`. Whenever the trigger is fired because of an INSERT or DELETE operation occurring on the table, the System Monitor counter is automatically updated.  
  
 This table describes the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] **User Settable** object.  
  
|SQL Server User Settable counters|Description|  
|---------------------------------------|-----------------|  
|**Query**|The **User Settable** object contains the query counter. Users configure the **User counters** within the query object.|  
  
 This table describes the **instances** of the **Query** counter.  
  
|Query counter instances|Description|  
|-----------------------------|-----------------|  
|**User counter 1**|Defined using **sp_user_counter1**.|  
|**User counter 2**|Defined using **sp_user_counter2**.|  
|**User counter 3**|Defined using **sp_user_counter3**.|  
|...||  
|**User counter 10**|Defined using **sp_user_counter10**.|  
  
 To make use of the user counter stored procedures, execute them from your own application with a single integer parameter representing the new value for the counter. For example, to set **User counter 1** to the value 10, execute this Transact-SQL statement:  
  
```  
EXECUTE sp_user_counter1 10  
```  
  
 The user counter stored procedures can be called from anywhere other stored procedures can be called, such as your own stored procedures. For example, you can create the following stored procedure to count the number of connections and attempted connections since an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] was started:  
  
```  
DROP PROC My_Proc  
GO  
CREATE PROC My_Proc  
AS   
   EXECUTE sp_user_counter1 @@CONNECTIONS  
GO  
```  
  
 The @@CONNECTIONS function returns the number of connections or attempted connections since an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] started. This value is passed to the **sp_user_counter1** stored procedure as the parameter.  
  
> [!IMPORTANT]  
>  Make the queries defined in the user counter stored procedures as simple as possible. Memory-intensive queries that perform substantial sort or hash operations or queries that perform large amounts of I/O are expensive to execute and can impact performance.  
  
## Permissions  
 **sp_user_counter** is available for all users but can be restricted for any query counter.  
  
## See Also  
 [Monitor Resource Usage &#40;System Monitor&#41;](../../relational-databases/performance-monitor/monitor-resource-usage-system-monitor.md)  
  
  
