---
title: "sys.dm_clr_appdomains (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_clr_appdomains"
  - "sys.dm_clr_appdomains"
  - "dm_clr_appdomains_TSQL"
  - "sys.dm_clr_appdomains_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_clr_appdomains dynamic management dynamic management view"
ms.assetid: 9fe0d4fd-950a-4274-a493-85e776278045
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_clr_appdomains (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a row for each application domain in the server. Application domain (**AppDomain**) is a construct in the [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] common language runtime (CLR) that is the unit of isolation for an application. You can use this view to understand and troubleshoot CLR integration objects that are executing in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 There are several types of CLR integration managed database objects. For general information about these objects, see [Building Database Objects with Common Language Runtime (CLR) Integration](../../relational-databases/clr-integration/database-objects/building-database-objects-with-common-language-runtime-clr-integration.md). Whenever these objects are executed, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] creates an **AppDomain** under which it can load and execute the required code. The isolation level for an **AppDomain** is one **AppDomain** per database per owner. That is, all CLR objects owned by a user are always executed in the same **AppDomain** per-database (if a user registers CLR database objects in different databases, the CLR database objects will run in different application domains). An **AppDomain** is not destroyed after the code finishes execution. Instead, it is cached in memory for future executions. This improves performance.  
  
 For more information, see [Application Domains](https://go.microsoft.com/fwlink/p/?LinkId=299658).  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**appdomain_address**|**varbinary(8)**|Address of the **AppDomain**. All managed database objects owned by a user are always loaded in the same **AppDomain**. You can use this column to look up all the assemblies currently loaded in this **AppDomain** in **sys.dm_clr_loaded_assemblies**.|  
|**appdomain_id**|**int**|ID of the **AppDomain**. Each **AppDomain** has a unique ID.|  
|**appdomain_name**|**varchar(386)**|Name of the **AppDomain** as assigned by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|**creation_time**|**datetime**|Time when the **AppDomain** was created. Because **AppDomains** are cached and reused for better performance, **creation_time** is not necessarily the time when the code was executed.|  
|**db_id**|**int**|ID of the database in which this **AppDomain** was created. Code stored in two different databases cannot share one **AppDomain**.|  
|**user_id**|**int**|ID of the user whose objects can execute in this **AppDomain**.|  
|**state**|**nvarchar(128)**|A descriptor for the current state of the **AppDomain**. An AppDomain can be in different states from creation to deletion. See the Remarks section of this topic for more information.|  
|**strong_refcount**|**int**|Number of strong references to this **AppDomain**. This reflects the number of currently executing batches that use this **AppDomain**. Note that execution of this view will create a **strong refcount**; even if is no code currently executing, **strong_refcount** will have a value of 1.|  
|**weak_refcount**|**int**|Number of weak references to this **AppDomain**. This indicates how many objects inside the **AppDomain** are cached. When you execute a managed database object, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] caches it inside the **AppDomain** for future reuse. This improves performance.|  
|**cost**|**int**|Cost of the **AppDomain**. The higher the cost, the more likely this **AppDomain** is to be unloaded under memory pressure. Cost usually depends on how much memory is required to re-create this **AppDomain**.|  
|**value**|**int**|Value of the **AppDomain**. The lower the value, the more likely this **AppDomain** is to be unloaded under memory pressure. Value usually depends on how many connections or batches are using this **AppDomain**.|  
|**total_processor_time_ms**|**bigint**|Total processor time, in milliseconds, used by all threads while executing in the current application domain since the process started. This is equivalent to **System.AppDomain.MonitoringTotalProcessorTime**.|  
|**total_allocated_memory_kb**|**bigint**|Total size, in kilobytes, of all memory allocations that have been made by the application domain since it was created, without subtracting memory that has been collected. This is equivalent to **System.AppDomain.MonitoringTotalAllocatedMemorySize**.|  
|**survived_memory_kb**|**bigint**|Number of kilobytes that survived the last full, blocking collection and that are known to be referenced by the current application domain. This is equivalent to **System.AppDomain.MonitoringSurvivedMemorySize**.|  
  
## Remarks  
 There is a one-to-may relationship between **dm_clr_appdomains.appdomain_address** and **dm_clr_loaded_assemblies.appdomain_address**.  
  
 The following tables list possible **state** values, their descriptions, and when they occur in the **AppDomain** lifecycle. You can use this information to follow the lifecyle of an **AppDomain** and to watch for suspicious or repetitive **AppDomain** instances unloading, without having to parse the Windows Event Log.  
  
## AppDomain Initialization  
  
|State|Description|  
|-----------|-----------------|  
|E_APPDOMAIN_CREATING|The **AppDomain** is being created.|  
  
## AppDomain Usage  
  
|State|Description|  
|-----------|-----------------|  
|E_APPDOMAIN_SHARED|The runtime **AppDomain** is ready for use by multiple users.|  
|E_APPDOMAIN_SINGLEUSER|The **AppDomain** is ready for use in DDL operations. These differ from E_APPDOMAIN_SHARED in that shared AppDomains are used for CLR integration executions as opposed to DDL operations. Such AppDomains are isolated from other concurrent operations.|  
|E_APPDOMAIN_DOOMED|The **AppDomain** is scheduled to be unloaded, but there are currently threads executing in it.|  
  
## AppDomain Cleanup  
  
|State|Description|  
|-----------|-----------------|  
|E_APPDOMAIN_UNLOADING|[!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has requested that the CLR unload the **AppDomain**, usually because the assembly that contains the managed database objects has been altered or dropped.|  
|E_APPDOMAIN_UNLOADED|The CLR has unloaded the **AppDomain**. This is usually the result of an escalation procedure due to **ThreadAbort**, **OutOfMemory**, or an unhandled exception in user code.|  
|E_APPDOMAIN_ENQUEUE_DESTROY|The **AppDomain** has been unloaded in CLR and set to be destroyed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|E_APPDOMAIN_DESTROY|The **AppDomain** is in the process of being destroyed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].|  
|E_APPDOMAIN_ZOMBIE|The **AppDomain** has been destroyed by [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]; however, not all of the references to the **AppDomain** have been cleaned up.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the database.  
  
## Examples  
 The following example shows how to view the details of an **AppDomain** for a given assembly:  
  
```  
select appdomain_id, creation_time, db_id, user_id, state  
from sys.dm_clr_appdomains a  
where appdomain_address =   
(select appdomain_address   
 from sys.dm_clr_loaded_assemblies  
   where assembly_id = 500);  
```  
  
 The following example shows how to view all assemblies in a given **AppDomain**:  
  
```  
select a.name, a.assembly_id, a.permission_set_desc, a.is_visible, a.create_date, l.load_time   
from sys.dm_clr_loaded_assemblies as l   
inner join sys.assemblies as a  
on l.assembly_id = a.assembly_id  
where l.appdomain_address =   
(select appdomain_address   
from sys.dm_clr_appdomains  
where appdomain_id = 15);  
```  
  
## See Also  
 [sys.dm_clr_loaded_assemblies &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-clr-loaded-assemblies-transact-sql.md)   
 [Common Language Runtime Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/common-language-runtime-related-dynamic-management-views-transact-sql.md)  
  
  
