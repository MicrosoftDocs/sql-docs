---
title: "sys.dm_io_backup_tapes (Transact-SQL)"
description: sys.dm_io_backup_tapes (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "06/10/2016"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_io_backup_tapes"
  - "dm_io_backup_tapes_TSQL"
  - "sys.dm_io_backup_tapes_TSQL"
  - "dm_io_backup_tapes"
helpviewer_keywords:
  - "sys.dm_io_backup_tapes dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 2e27489e-cf69-4a89-9036-77723ac3de66
---
# sys.dm_io_backup_tapes (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  Returns the list of tape devices and the status of mount requests for backups.   
 
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**physical_device_name**|**nvarchar(520)**|Name of the actual physical device on which a backup can be taken. Is not nullable.|  
|**logical_device_name**|**nvarchar(256)**|User-specified name for the drive (from **sys.backup_devices**). NULL if no user-specified name is available. Is nullable.|  
|**status**|**int**|Status of the tape:<br /><br /> 1 = Open, available for use<br /><br /> 2 = Mount pending<br /><br /> 3 = In use<br /><br /> 4 = Loading<br /><br /> **Note:** While a tape is being loaded (**status = 4**), the media label is not read yet. Columns that copy media-label values, such as **media_sequence_number**, show anticipated values, which may differ from the actual values on the tape. After the label has been read, **status** changes to **3** (in use), and the media-label columns then reflect the actual tape that is loaded.<br /><br /> Is not nullable.|  
|**status_desc**|**nvarchar(520)**|Description of the tape status:<br /><br /> AVAILABLE<br /><br /> MOUNT PENDING<br /><br /> IN USE<br /><br /> LOADING MEDIA<br /><br /> Is not nullable.|  
|**mount_request_time**|**datetime**|Time at which mount was requested. NULL if no mount is pending (**status!=2**). Is nullable.|  
|**mount_expiration_time**|**datetime**|Time at which mount request will expire (time-out). NULL if no mount is pending (**status!=2**). Is nullable.|  
|**database_name**|**nvarchar(256)**|Database that is to be backed up onto this device. Is nullable.|  
|**spid**|**int**|Session ID. This identifies the user of the tape. Is nullable.|  
|**command**|**int**|Command that performs the backup. Is nullable.|  
|**command_desc**|**nvarchar(120)**|Description of the command. Is nullable.|  
|**media_family_id**|**int**|Index of media family (1...*n*), *n* is the number of media families in the media set. Is nullable.|  
|**media_set_name**|**nvarchar(256)**|Name of the media set (if any) as specified by the MEDIANAME option when the media set was created). Is nullable.|  
|**media_set_guid**|**uniqueidentifier**|Identifier that uniquely identifies the media set. Is nullable.|  
|**media_sequence_number**|**int**|Index of volume within a media family (1...*n*). Is nullable.|  
|**tape_operation**|**int**|Tape operation that is being performed:<br /><br /> 1 = Read<br /><br /> 2 = Format<br /><br /> 3 = Init<br /><br /> 4 = Append<br /><br /> Is nullable.|  
|**tape_operation_desc**|**nvarchar(120)**|Tape operation that is  being performed:<br /><br /> READ<br /><br /> FORMAT<br /><br /> INIT<br /><br /> APPEND<br /><br /> Is nullable.|  
|**mount_request_type**|**int**|Type of the mount request:<br /><br /> 1 = Specific tape. The tape identified by the **media_\*** fields is required.<br /><br /> 2 = Next media family. The next media family not yet restored is requested. This is used when restoring from fewer devices than there are media families.<br /><br /> 3 = Continuation tape. The media family is being extended, and a continuation tape is requested.<br /><br /> Is nullable.|  
|**mount_request_type_desc**|**nvarchar(120)**|Type of the mount request:<br /><br /> SPECIFIC TAPE<br /><br /> NEXT MEDIA FAMILY<br /><br /> CONTINUATION VOLUME<br /><br /> Is nullable.|  
  
## Permissions  
 The user must have VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [I/O Related Dynamic Management Views and Functions &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/i-o-related-dynamic-management-views-and-functions-transact-sql.md)  
  
  

