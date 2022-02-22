---
description: "sys.dm_database_backups"
title: "sys.dm_database_backups | Microsoft Docs"
ms.custom: ""
ms.date: "02/22/2022"
ms.service: sql-database
ms.reviewer: ""
ms.topic: "reference"
f1_keywords: 
  - "dm_database_backups_TSQL"
  - "dm_database_backups"
  - "sys.dm_database_backups"
  - "sys.dm_database_backups_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "dm_database_backups dynamic management view"
  - "sys.dm_database_backups dynamic management view"
ms.assetid: 
author: SudhirRaparla
ms.author: nvraparl
monikerRange: "= azuresqldb-current"
---
# sys.dm_database_backups

[!INCLUDE [Azure SQL Database](../../includes/applies-to-version/asdb.md)]

    Returns information about all active backups ofa  database in a [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] server.


> [!NOTE]
> sys.dm_database_backups is introduced as part of Backup History for Azure SQL Database which is currently in preview.

|Column Name|Data Type|Description|  
|-----------------|---------------|-----------------|  
|backup_file_id|**uniqueidentifier**|ID of the generated backup file. Not null|
|database_guid|**sql_variant**|Logical Database ID of the [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] on which the operation is performed. Not Null.|
|physical_database_name|**sql_variant**|Name of the Physical [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] on which the operation is performed.|
|server_name|**sql_variant**|Name of the Physical server on which the [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] which is being backed up is present. Not Null.|
|backup_start_date|**datetime**|Timestamp when the Backup operation started|
|backup_finish_date|**datetime**|Timestamp when the Backup operation finished|
|backup_type|**nvarchar(60)**|Type of Backup<br /><br /> D = Full Database Backup<br />I = Incremental Backup<br />L = Log Backup|
|in_retention|**tinyint**|Backup Retention Status. Tells whether backup is within retention period<br /><br />1 = In Retention <br />0 = Out of Retention|

## Permissions  
 Requires VIEW DATABASE STATE permission on the database.

## Remarks
Backups retained and shown in Backup history view depend on configured backup retention. Some backups older than the retention period, in_retention=0, are also shown in dm_database_backups view. They're needed to do point in restore within the configured retention. 