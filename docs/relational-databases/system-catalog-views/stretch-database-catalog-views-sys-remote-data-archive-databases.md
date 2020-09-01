---
title: "sys.remote_data_archive_databases (Transact-SQL) | Microsoft Docs"
description: Learn how sys.remote_data_archive_databases contains one row for each remote database that stores data from a Stretch-enabled local database.
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.technology: stored-procedures
ms.topic: "language-reference"
f1_keywords: 
  - "sys.remote_data_archive_databases"
  - "sys.remote_data_archive_databases_TSQL"
  - "remote_data_archive_databases"
  - "remote_data_archive_databases_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.remote_data_archive_databases catalog view"
ms.assetid: 25bffb0c-9821-40b4-88cf-75f854891a09
author: pmasl
ms.author: pelopes
ms.reviewer: mikeray
---
# Stretch Database Catalog Views - sys.remote_data_archive_databases
[!INCLUDE [sqlserver2016](../../includes/applies-to-version/sqlserver2016.md)]

  Contains one row for each remote database that stores data from a Stretch-enabled local database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**remote_database_id**|**int**|The auto-generated local identifier of the remote database.|  
|**remote_database_name**|**sysname**|The name of the remote database.|  
|**data_source_id**|**int**|The data source used to connect to the remote server|  
  
## See Also  
 [Stretch Database](../../sql-server/stretch-database/stretch-database.md)  
  
  
