---
title: "sys.dm_server_registry (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dm_server_registry_TSQL"
  - "sys.dm_server_registry"
  - "dm_server_registry"
  - "sys.dm_server_registry_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.dm_server_registry dynamic management view"
ms.assetid: 9b3e0c74-2e99-4996-a383-104d51831e97
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.dm_server_registry (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Returns configuration and installation information that is stored in the Windows registry for the current instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Returns one row per registry key. Use this dynamic management view to return information such as the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] services that are available on the host machine or network configuration values for the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|registry_key|**nvarchar(256)**|Registry key name. Is nullable.|  
|value_name|**nvarchar(256)**|Key value name. This is the item shown in the **Name** column of the Registry Editor. Is nullable.|  
|value_data|**sql_variant**|Value of the key data. This is the value shown in the **Data** column of the Registry Editor for a given entry. Is nullable.|  
  
## Security  
  
### Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## Examples  
  
### A. Display the SQL Server services  
 The following example returns registry key values for the SQL Server and SQL Server Agent services for the current instance of SQL Server.  
  
```  
SELECT registry_key, value_name, value_data  
FROM sys.dm_server_registry  
WHERE registry_key LIKE N'%ControlSet%';  
```  
  
### B. Display the SQL Server Agent registry key values  
 The following example returns the SQL Server Agent registry key values for the current instance of SQL Server.  
  
```  
SELECT registry_key, value_name, value_data  
FROM sys.dm_server_registry  
WHERE registry_key LIKE N'%SQLAgent%';  
```  
  
### C. Display the current version of the instance of SQL Server  
 The following example returns the version of the current instance of SQL Server.  
  
```  
SELECT registry_key, value_name, value_data  
FROM sys.dm_server_registry  
WHERE registry_key = N'CurrentVersion';  
```  
  
### D. Display the parameters passed to the instance of SQL Server during startup  
 The following example returns the parameters that are passed to the instance of SQL Server during startup.  
  
```  
SELECT registry_key, value_name, value_data  
FROM sys.dm_server_registry  
WHERE registry_key LIKE N'%Parameters';  
```  
  
### E. Return network configuration information for the instance of SQL Server  
 The following example returns network configuration values for the current instance of SQL Server.  
  
```  
SELECT registry_key, value_name, value_data  
FROM sys.dm_server_registry  
WHERE registry_key LIKE N'%SuperSocketNetLib%';  
```  
  
## See Also  
 [sys.dm_server_services &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sys-dm-server-services-transact-sql.md)  
  
  
