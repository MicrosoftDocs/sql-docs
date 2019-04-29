---
title: "System Compatibility Views (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "compatibility views [SQL Server]"
  - "system tables [SQL Server], compatibility views"
  - "type IDs [SQL Server]"
  - "system views [SQL Server], compatibility"
  - "metadata [SQL Server], views"
  - "backward compatibility [SQL Server], compatibility views"
  - "compatibility [SQL Server], compatibility views"
  - "backward compatibility [SQL Server], system tables"
  - "compatibility [SQL Server], system tables"
  - "user IDs [SQL Server]"
ms.assetid: 8e4624f5-9d36-4ce7-9c9e-1fe010fa2122
author: "rothja"
ms.author: "jroth"
manager: craigg
---
# System Compatibility Views (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Many of the system tables from earlier releases of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] are now implemented as a set of views. These views are known as compatibility views, and they are meant for backward compatibility only. The compatibility views expose the same metadata that was available in [!INCLUDE[ssVersion2000](../../includes/ssversion2000-md.md)]. However, the compatibility views do not expose any of the metadata related to features that are introduced in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later. Therefore, when you use new features, such as [!INCLUDE[ssSB](../../includes/sssb-md.md)] or partitioning, you must switch to using the catalog views.  
  
 Another reason for upgrading to the catalog views is that compatibility view columns that store user IDs and type IDs may return NULL or trigger arithmetic overflows. This is because you can create more than 32,767 users, groups, and roles, and 32,767 data types. For example, if you were to create 32,768 users, and then run the following query: `SELECT * FROM sys.sysusers`. If ARITHABORT is set to ON, the query fails with an arithmetic overflow error. If ARITHABORT is set to OFF, the **uid** column returns NULL.  
  
 To avoid these problems, we recommend that you use the new catalog views that can handle the increased number of user IDs and type IDs. The following table lists the columns that are subject to this overflow.  
  
|Column name|Compatibility view|SQL Server 2005 view|  
|-----------------|------------------------|--------------------------|  
|**xusertype**|**syscolumns**|**sys.columns**|  
|**usertype**|**syscolumns**|**sys.columns**|  
|**memberuid**|**sysmembers**|**sys.database_role_members**|  
|**groupuid**|**sysmembers**|**sys.database_role_members**|  
|**uid**|**sysobjects**|**sys.objects**|  
|**uid**|**sysprotects**|**sys.database_permissions**<br /><br /> **sys.server_permissions**|  
|**grantor**|**sysprotects**|**sys.database_permissions**<br /><br /> **sys.server_permissions**|  
|**xusertype**|**systypes**|**sys.types**|  
|**uid**|**systypes**|**sys.types**|  
|**uid**|**sysusers**|**sys.database_principals**|  
|**altuid**|**sysusers**|**sys.database_principals**|  
|**gid**|**sysusers**|**sys.database_principals**|  
|**uid**|**syscacheobjects**|**sys.dm_exec_plan_attributes**|  
|**uid**|**sysprocesses**|**sys.dm_exec_requests**|  
  
 When referenced in a user database, system tables which were announced as deprecated in SQL Server 2000 (such as **syslanguages** or **syscacheobjects**), are now bound to the back-compatibility view in the **sys** schema. Since the SQL Server 2000 system tables have been deprecated for multiple versions, this change is not considered a breaking change.  
  
 Example: If a user creates a user-table called **syslanguages** in a user-database, in SQL Server 2008, the statement `SELECT * from dbo.syslanguages;` in that database would return the values from the user table. Beginning in SQL Server 2012, this practice will return data from the system view **sys.syslanguages**.  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Mapping System Tables to System Views &#40;Transact-SQL&#41;](../../relational-databases/system-tables/mapping-system-tables-to-system-views-transact-sql.md)  
  
  
