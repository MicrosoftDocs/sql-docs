---
title: "sys.fulltext_document_types (Transact-SQL)"
description: sys.fulltext_document_types (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.reviewer: mikeray
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.fulltext_document_types_TSQL"
  - "fulltext_document_types"
  - "fulltext_document_types_TSQL"
  - "sys.fulltext_document_types"
helpviewer_keywords:
  - "sys.fulltext_document_types catalog view"
dev_langs:
  - "TSQL"
ms.assetid: 156fcfa4-7304-4a5c-b96f-1c3e061e5df0
monikerRange: "=azuresqldb-current||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sys.fulltext_document_types (Transact-SQL)
[!INCLUDE [SQL Server Azure SQL Database Azure SQL Managed Instance](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

  Returns a row for each document type that is available for full-text indexing operations. Each row represents the IFilter interface that is registered in the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**document_type**|**sysname**|The file extension of the supported document type.<br /><br /> This value can be used to identify the filter that will be used during full-text indexing of columns of type **varbinary(max)** or **image**.|  
|**class_id**|**uniqueidentifier**|GUID of the IFilter class that supports file extension.|  
|**path**|**nvarchar(260)**|The path to the IFilter DLL. The path is only visible to members of the **serveradmin** fixed server role.|  
|**version**|**sysname**|Version of the IFilter DLL.|  
|**manufacturer**|**sysname**|Name of the IFilter manufacturer.<br /><br /> Note: Only documents with the manufacturer as [!INCLUDE[msCoName](../../includes/msconame-md.md)] are supported on [!INCLUDE[ssSDS](../../includes/sssds-md.md)].|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)]  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)  
  
  
