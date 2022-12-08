---
title: "sys.dm_os_loaded_modules (Transact-SQL)"
description: sys.dm_os_loaded_modules (Transact-SQL)
author: rwestMSFT
ms.author: randolphwest
ms.date: "08/18/2017"
ms.service: sql
ms.subservice: system-objects
ms.topic: "reference"
f1_keywords:
  - "sys.dm_os_loaded_modules"
  - "dm_os_loaded_modules"
  - "sys.dm_os_loaded_modules_TSQL"
  - "dm_os_loaded_modules_TSQL"
helpviewer_keywords:
  - "sys.dm_os_loaded_modules dynamic management view"
dev_langs:
  - "TSQL"
ms.assetid: 56c7743a-b568-4943-bd3b-73c57d9d641c
monikerRange: ">=sql-server-2016||>=sql-server-linux-2017||>=aps-pdw-2016"
---
# sys.dm_os_loaded_modules (Transact-SQL)
[!INCLUDE [sql-pdw](../../includes/applies-to-version/sql-pdw.md)]

  Returns a row for each module loaded into the server address space.  
  
> [!NOTE]  
>  To call this from [!INCLUDE[ssPDW](../../includes/sspdw-md.md)], use the name **sys.dm_pdw_nodes_os_loaded_modules**.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**base_address**|**varbinary(8)**|Address of the module in the process.|  
|**file_version**|**varchar(23)**|Version of the file. Appears in the following format:<br /><br /> x.x:x.x|  
|**product_version**|**varchar(23)**|Version of the product. Appears in the following format:<br /><br /> x.x:x.x|  
|**debug**|**bit**|1 = Module is a debug version of the loaded module.|  
|**patched**|**bit**|1 = Module has been patched.|  
|**prerelease**|**bit**|1 = Module is a pre-release version of the loaded module.|  
|**private_build**|**bit**|1 = Module is a private build of the loaded module.|  
|**special_build**|**bit**|1 = Module is a special build of the loaded module.|  
|**language**|**int**|Language of version information of the module.|  
|**company**|**nvarchar(256)**|Name of company that created the module.|  
|**description**|**nvarchar(256)**|Description of the module.|  
|**name**|**nvarchar(255)**|Name of module. Includes the full path of the module.|  
|**pdw_node_id**|**int**|**Applies to**: [!INCLUDE[ssPDW](../../includes/sspdw-md.md)]<br /><br /> The identifier for the node that this distribution is on.|  
  
## Permissions  
 Requires VIEW SERVER STATE permission on the server.  
  
## See Also  
 [Dynamic Management Views and Functions &#40;Transact-SQL&#41;](~/relational-databases/system-dynamic-management-views/system-dynamic-management-views.md)   
 [SQL Server Operating System Related Dynamic Management Views &#40;Transact-SQL&#41;](../../relational-databases/system-dynamic-management-views/sql-server-operating-system-related-dynamic-management-views-transact-sql.md)  
  
  
