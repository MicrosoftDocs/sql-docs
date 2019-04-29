---
title: "FileTable Schema | Microsoft Docs"
ms.custom: ""
ms.date: "03/01/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: filestream
ms.topic: conceptual
helpviewer_keywords: 
  - "FileTables [SQL Server], table schema"
ms.assetid: e1cb3880-cfda-40ac-91fc-d08998287f44
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
---
# FileTable Schema
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]
  Describes the pre-defined and fixed schema of a FileTable.  
  
|File attribute name|type|Size|Default|Description|File system accessibility|  
|-------------------------|----------|----------|-------------|-----------------|-------------------------------|  
|**path_locator**|**hierarchyid**|variable|A **hierarchyid** that identifies the position of this item.|The position of this node in the hierarchical FileNamespace.<br /><br /> Primary key for the table.|Can be created and modified by setting the Windows path values.|  
|**stream_id**|**[uniqueidentifier] rowguidcol**||A value returned by the **NEWID()** function.|A unique ID for the FILESTREAM data.|Not applicable.|  
|**file_stream**|**varbinary(max)**<br /><br /> **filestream**|variable|NULL|Contains the FILESTREAM data.|Not applicable.|  
|**file_type**|**nvarchar(255)**|variable|NULL.<br /><br /> A create or rename operation in the file system populates the file extension value from the name.|Represents the type of the file.<br /><br /> This column can be used as the **TYPE COLUMN** when you create a full-text index.<br /><br /> **file_type** is a persisted computed column.|Calculated automatically. Cannot be set.|  
|**Name**|**nvarchar(255)**|variable|GUID value.|The file or directory name.|Can be created or modified by using Windows APIs.|  
|**parent_path_locator**|**hierarchyid**|variable|A **hierarchyid** that identifies the directory that contains this item.|The **hierarchyid** of the containing directory.<br /><br /> **parent_path_locator** is a persisted computed column.|Calculated automatically. Cannot be set.|  
|**cached_file_size**|**bigint**|||The size in bytes of the FILESTREAM data.<br /><br /> **cached_file_size** is a persisted computed column.|Although the cached file size is automatically kept up to date, it can go out of sync in unusual circumstances. To calculate the exact size, use the **DATALENGTH()** function.|  
|**creation_time**|**datetime2(4)**<br /><br /> **not null**|8 bytes|Current time.|The date and time that the file was created.|Calculated automatically. Can also be set by using Windows APIs.|  
|**last_write_time**|**datetime2(4)**<br /><br /> **not null**|8 bytes|Current time.|The date and time that the file was last updated.|Calculated automatically. Can also be set by using Windows APIs.|  
|**last_access_time**|**datetime2(4)**<br /><br /> **not null**|8 bytes|Current time.|The date and time that the file was last accessed.|Calculated automatically. Can also be set by using Windows APIs.|  
|**is_directory**|**bit**<br /><br /> **not null**|1 byte|FALSE|Indicates whether the row represents a directory. This value is calculated automatically, and cannot be set.|Calculated automatically. Cannot be set.|  
|**is_offline**|**bit**<br /><br /> **not null**|1 byte|FALSE|Offline file attribute.|Calculated automatically. Can also be set by using Windows APIs.|  
|**is_hidden**|**bit**<br /><br /> **not null**|1 byte|FALSE|Hidden file attribute.|Calculated automatically. Can also be set by using Windows APIs.|  
|**is_readonly**|**bit**<br /><br /> **not null**|1 byte|FALSE|Read-only  file attribute.|Calculated automatically. Can also be set by using Windows APIs.|  
|**is_archive**|**bit**<br /><br /> **not null**|1 byte|FALSE|Archive attribute.|Calculated automatically. Can also be set by using Windows APIs.|  
|**is_system**|**bit**<br /><br /> **not null**|1 byte|FALSE|System file attribute.|Calculated automatically. Can also be set by using Windows APIs.|  
|**is_temporary**|**bit**<br /><br /> **not null**|1 byte|FALSE|Temporary file attribute.|Calculated automatically. Can also be set by using Windows APIs.|  
  
## See Also  
 [Create, Alter, and Drop FileTables](../../relational-databases/blob/create-alter-and-drop-filetables.md)  
  
  
