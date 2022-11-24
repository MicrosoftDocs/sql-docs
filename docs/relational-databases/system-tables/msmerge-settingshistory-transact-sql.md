---
title: "MSmerge_settingshistory (Transact-SQL)"
description: MSmerge_settingshistory (Transact-SQL)
author: VanMSFT
ms.author: vanto
ms.date: "03/04/2017"
ms.service: sql
ms.subservice: replication
ms.topic: "reference"
f1_keywords:
  - "MSmerge_settingshistory"
  - "MSmerge_settingshistory_TSQL"
helpviewer_keywords:
  - "MSmerge_settingshistory system table"
dev_langs:
  - "TSQL"
ms.assetid: 0bdf2d5f-5502-44cd-aa9d-2d5006ad20ce
---
# MSmerge_settingshistory (Transact-SQL)
[!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

  The **MSmerge_settingshistory** table is used to maintain a history of changes made to article and publication properties for merge replication, with one row for each change made to a merge replication topology. This table also stores information about when the initial property settings were made. This table is stored in the publication and subscription databases.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**eventtime**|**datetime**|The datetime that the event occurred.|  
|**pubid**|**uniqueidentifier**|The unique identification number for a given publication.|  
|**artid**|**uniqueidentifier**|The unique identification number for the given article.|  
|**eventtype**|**tinyint**|Specifies the type of event being recorded, which can be one of the following:<br /><br /> **1** - initial publication level property setting.<br /><br /> **2** - change in a publication property.<br /><br /> **101** - initial article property setting.<br /><br /> **102** - change in an article property.|  
|**propertyname**|**sysname**|The name of the property set or changed|  
|**previousvalue**|**sysname**|The previous property value if a property was changed.|  
|**newvalue**|**sysname**|The value that the property was changed to or created at.|  
|**eventtext**|**nvarchar(2000)**|The character string describing the event.|  
  
## See Also  
 [Replication Tables &#40;Transact-SQL&#41;](../../relational-databases/system-tables/replication-tables-transact-sql.md)   
 [Replication Views &#40;Transact-SQL&#41;](../../relational-databases/system-views/replication-views-transact-sql.md)  
  
  
