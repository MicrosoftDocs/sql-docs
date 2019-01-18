---
title: "sys.dm_clr_loaded_assemblies (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_clr_loaded_assemblies"
  - "sys.dm_clr_loaded_assemblies_TSQL"
  - "dm_clr_loaded_assemblies_TSQL"
  - "sys.dm_clr_loaded_assemblies"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_clr_loaded_assemblies dynamic management view"
ms.assetid: 8523d8db-d8a0-4b1f-ae19-6705d633e0a6
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_clr_loaded_assemblies (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns a row for each managed user assembly loaded into the server address space. Use this view to understand and troubleshoot CLR integration managed database objects that are executing in [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 Assemblies are managed code DLL files that are used to define and deploy managed database objects in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Whenever a user executes one of these managed database objects, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and the CLR load the assembly (and its references) in which the managed database object is defined. The assembly remains loaded in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to increase performance, so that the managed database objects contained in the assembly can be called in the future with out having to reload the assembly. The assembly is not unloaded until [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] comes under memory pressure. For more information about assemblies and CLR integration, see [CLR Hosted Environment](../../relational-databases/clr-integration/clr-integration-architecture-clr-hosted-environment.md). For more information about managed database objects, see [Building Database Objects with Common Language Runtime &#40;CLR&#41; Integration](../../relational-databases/clr-integration/database-objects/building-database-objects-with-common-language-runtime-clr-integration.md).  

  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**assembly_id**|**int**|ID of the loaded assembly. The **assembly_id** can be used to look up more information about the assembly in the [sys.assemblies &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-assemblies-transact-sql.md) catalog view. Note that the [!INCLUDE[tsql](../../includes/tsql-md.md)] [sys.assemblies](../../relational-databases/system-catalog-views/sys-assemblies-transact-sql.md) catalog shows assemblies in the current database only. The **sqs.dm_clr_loaded_assemblies** view shows all loaded assemblies on the server.|  
|**appdomain_address**|**int**|Address of the application domain (**AppDomain**) in which the assembly is loaded. All the assemblies owned by a single user are always loaded in the same **AppDomain**. The **appdomain_address** can be used to lookup more information about the **AppDomain** in the [sys.dm_clr_appdomains](../../relational-databases/system-dynamic-management-views/sys-dm-clr-appdomains-transact-sql.md) view.|  
|**load_time**|**datetime**|Time when the assembly was loaded. Note that the assembly remains loaded until [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is under memory pressure and unloads the **AppDomain**. You can monitor **load_time** to understand how frequently [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] comes under memory pressure and unloads the **AppDomain**.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Remarks  
 The **dm_clr_loaded_assemblies.appdomain_address** view has a many-to-one relationship with  **dm_clr_appdomains.appdomain_address**. The **dm_clr_loaded_assemblies.assembly_id** view has a one-to-many relationship with **sys.assemblies.assembly_id**.  
  
## Examples  
 The following example shows how to view details of all assemblies in the current database that are currently loaded.  
  
```  
 SELECT a.name, a.assembly_id, a.permission_set_desc, a.is_visible, a.create_date, l.load_time   
FROM sys.dm_clr_loaded_assemblies AS l   
INNER JOIN sys.assemblies AS a  
ON l.assembly_id = a.assembly_id;  
```  
  
 The following example shows how to view details of the **AppDomain** in which a given assembly is loaded.  
  
```  
SELECT appdomain_id, creation_time, db_id, user_id, state  
FROM sys.dm_clr_appdomains AS a  
WHERE appdomain_address =   
(SELECT appdomain_address   
 FROM sys.dm_clr_loaded_assemblies  
 WHERE assembly_id = 555);  
```  
  
## See Also  
 [Common Language Runtime Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/common-language-runtime-related-dynamic-management-views-transact-sql.md)  
  
  
