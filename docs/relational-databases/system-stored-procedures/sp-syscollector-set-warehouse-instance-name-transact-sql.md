---
title: "sp_syscollector_set_warehouse_instance_name (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "08/09/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_syscollector_set_warehouse_instance_name_TSQL"
  - "sp_syscollector_set_warehouse_instance_name"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "data collector [SQL Server], stored procedures"
  - "sp_syscollector_set_warehouse_instance_name"
ms.assetid: 5320fcd4-bed1-468f-b784-a5e10fcfaeb6
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sp_syscollector_set_warehouse_instance_name (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Specifies the instance name for the connection string used to connect to the management data warehouse.  
  
 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_syscollector_set_warehouse_instance_name [ @instance_name = ] 'instance_name'  
```  
  
## Arguments  
 [ @instance_name = ] '*instance_name*'  
 Is the instance name. *instance_name* is **sysname** and defaults to the local instance if NULL.  
  
> **NOTE:**  _instance_name_ must be the fully qualified instance name, which consists of the computer name and the instance name in the form *computerName*\\*instanceName*.  
  
## Return Code Values  
 **0** (success) or **1** (failure)  
  
## Remarks  
 You must disable the data collector before changing this data collector-wide configuration. This procedure fails if the data collector is enabled.  
  
 To view the current instance name, query the [syscollector_config_store](../../relational-databases/system-catalog-views/syscollector-config-store-transact-sql.md) system view.  
  
## Permissions  
 Requires membership in the dc_admin (with EXECUTE permission) fixed database role to execute this procedure.  
  
## Examples  
 The following example illustrates how to configure the data collector to use a management data warehouse instance on a remote server. In this example the remote server is named `RemoteSERVER` and the database is installed on the default instance.  
  
```  
USE msdb;  
GO  
EXEC sp_syscollector_set_warehouse_instance_name N'RemoteSERVER'; -- the default instance is assumed on the remote server  
GO  
```  
  
## See Also  
 [Data Collector Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/data-collector-stored-procedures-transact-sql.md)   
 [syscollector_config_store &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/syscollector-config-store-transact-sql.md)  
  
  
