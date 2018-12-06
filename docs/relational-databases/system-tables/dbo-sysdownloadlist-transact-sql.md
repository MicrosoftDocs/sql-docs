---
title: "dbo.sysdownloadlist (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "dbo.sysdownloadlist"
  - "sysdownloadlist_TSQL"
  - "dbo.sysdownloadlist_TSQL"
  - "sysdownloadlist"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sysdownloadlist system table"
ms.assetid: 71087a4c-e829-488e-aa7d-a9476e2b4779
author: "stevestein"
ms.author: "sstein"
manager: craigg
---
# dbo.sysdownloadlist (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-xxxx-xxxx-xxx-md.md)]

  Holds the queue of download instructions for all target servers.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|**instance_id**|**int**|Identity column that provides the natural insertion sequence of rows.|  
|**source_server**|**sysname**|Name of the source server.|  
|**operation_code**|**tinyint**|Operation code for the job:<br /><br /> **1** = INS (INSERT)<br /><br /> **2** = UPD (UPDATE)<br /><br /> **3** = DEL (DELETE)<br /><br /> **4** = START<br /><br /> **5** = STOP|  
|**object_type**|**tinyint**|Object type code.|  
|**object_id** <sup>1</sup>|**uniqueidentifier**|Object identification number.|  
|**target_server**|**sysname**|Name of the target server.|  
|**error_message**|**nvarchar(1024)**|Error message if the target server encounters an error when processing the particular row.|  
|**date_posted**|**datetime**|Date and time the job was posted to the target server.|  
|**date_downloaded**|**datetime**|Date and time job was last downloaded.|  
|**status**|**tinyint**|Status of the job:<br /><br /> **0** = Not yet downloaded<br /><br /> **1** = Successfully downloaded|  
|**deleted_object_name**|**sysname**|Name of deleted object.|  
  
 <sup>1</sup> The **object_id** column can be a value of **-1**, which corresponds to a value of ALL if the **operation_code** column is a value of DELETE.  
  
  
