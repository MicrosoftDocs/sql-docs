---
title: "sys.database_filestream_options (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "database_filestream_options"
  - "sys.database_filestream_options_TSQL"
  - "database_filestream_options_TSQL"
  - "sys.database_filestream_options"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.database_filestream_options catalog view"
ms.assetid: 3383c607-0bbc-456a-ab37-7230f4cbf0e9
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# sys.database_filestream_options (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Displays information about the level of non-transactional access to FILESTREAM data in FileTables that is enabled. Contains one row for each database in the SQL Server instance.  
  
 For more information about FileTables, see [FileTables &#40;SQL Server&#41;](../../relational-databases/blob/filetables-sql-server.md).  
  
  
|Column|Type|Description|  
|------------|----------|-----------------|  
|**database_id**|**int**|The ID of the database. This value is unique within the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.|  
|**directory_name**|**nvarchar(255)**|The database-level directory for all FileTable namespaces.|  
|**non_transacted_access**|**tinyint**|The level of non-transactional access to FILESTREAM data that is enabled. The level of access is set by the NON_TRANSACTED_ACCESS option of the **CREATE DATABASE** or **ALTER DATABASE** statement.<br /><br /> This setting has one of the following values:<br /><br /> 0 - Not enabled. This is the default value. This level is set by providing the value **OFF** for the **NON_TRANSACTED_ACCESS** option.<br /><br /> 1 - Read-only access. This level is set by providing the value **READ_ONLY** for the **NON_TRANSACTED_ACCESS** option.<br /><br /> 3 - Full access. This level is set by providing the value **FULL** for the **NON_TRANSACTED_ACCESS** option.<br /><br /> 5 - In transition to READONLY<br /><br /> 6 - In transition to OFF|  
|**non_transacted_access_desc**|**nvarchar(60)**|The description of the level of non-transactional access identified in non_transacted_access.<br /><br /> This setting has one of the following values:<br /><br /> NONE - This is the default value.<br /><br /> READ_ONLY<br /><br /> FULL<br /><br /> IN_TRANSITION_TO_READ_ONLY<br /><br /> IN_TRANSITION_TO_OFF|  
  
## See Also  
 [Enable the Prerequisites for FileTable](../../relational-databases/blob/enable-the-prerequisites-for-filetable.md)  
  
  
