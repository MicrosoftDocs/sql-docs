---
title: "sys.filetable_system_defined_objects (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "06/10/2016"
ms.prod: sql
ms.prod_service: "database-engine"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.filetable_system_defined_objects_TSQL"
  - "filetable_system_defined_objects"
  - "filetable_system_defined_objects_TSQL"
  - "sys.filetable_system_defined_objects"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.filetable_system_defined_objects catalog view"
ms.assetid: 62022e6b-46f6-495f-b14b-53f41e040361
author: stevestein
ms.author: sstein
manager: craigg
---
# sys.filetable_system_defined_objects (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2012-xxxx-xxxx-xxx-md](../../includes/tsql-appliesto-ss2012-xxxx-xxxx-xxx-md.md)]

  Displays a list of the system-defined objects that are related to FileTables. Contains one row for each system-defined object.  
  
 When you create a FileTable, related objects such as constraints and indexes are created at the same time. You cannot alter or drop these objects; they disappear only when the FileTable itself is dropped.  
  
 For more information about FileTables, see [FileTables &#40;SQL Server&#41;](../../relational-databases/blob/filetables-sql-server.md).  
  
|Column|Data type|Description|  
|------------|---------------|-----------------|  
|**object_id**|**int**|Object ID of the system-defined object related to a FileTable.<br /><br /> References the object in **sys.objects**.|  
|**parent_object_id**|**int**|Object ID of the parent FileTable.<br /><br /> References the object in **sys.objects**.|  
  
## See Also  
 [Create, Alter, and Drop FileTables](../../relational-databases/blob/create-alter-and-drop-filetables.md)   
 [Manage FileTables](../../relational-databases/blob/manage-filetables.md)  
  
  
