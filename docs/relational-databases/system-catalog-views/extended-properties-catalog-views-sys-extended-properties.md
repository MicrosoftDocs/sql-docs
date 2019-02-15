---
title: "sys.extended_properties (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "03/15/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sys.extended_properties"
  - "sys.extended_properties_TSQL"
  - "extended_properties"
  - "extended_properties_TSQL"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sys.extended_properties catalog view"
ms.assetid: 439b7299-dce3-4d26-b1c7-61be5e0df82a
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Extended Properties Catalog Views - sys.extended_properties
[!INCLUDE[tsql-appliesto-ss2008-xxxx-asdw-pdw-md](../../includes/tsql-appliesto-ss2008-xxxx-asdw-pdw-md.md)]

  Returns a row for each extended property in the current database.  
  
|Column name|Data type|Description|  
|-----------------|---------------|-----------------|  
|class|**tinyint**|Identifies the class of item on which the property exists. Can be one of the following:<br /><br /> 0 = Database<br /><br /> 1 = Object or column<br /><br /> 2 = Parameter<br /><br /> 3 = Schema<br /><br /> 4 = Database principal<br /><br /> 5 = Assembly<br /><br /> 6 = Type<br /><br /> 7 = Index<br /><br /> 10 = XML schema collection<br /><br /> 15 = Message type<br /><br /> 16 = Service contract<br /><br /> 17 = Service<br /><br /> 18 = Remote service binding<br /><br /> 19 = Route<br /><br /> 20 = Dataspace (filegroup or partition scheme)<br /><br /> 21 = Partition function<br /><br /> 22 = Database file<br /><br /> 27 = Plan guide|  
|class_desc|**nvarchar(60)**|Description of the class on which the extended property exists. Can be one of the following:<br /><br /> DATABASE<br /><br /> OBJECT_OR_COLUMN<br /><br /> PARAMETER<br /><br /> SCHEMA<br /><br /> DATABASE_PRINCIPAL<br /><br /> ASSEMBLY<br /><br /> TYPE<br /><br /> INDEX<br /><br /> XML_SCHEMA_COLLECTION<br /><br /> MESSAGE_TYPE<br /><br /> SERVICE_CONTRACT<br /><br /> SERVICE<br /><br /> REMOTE_SERVICE_BINDING<br /><br /> ROUTE<br /><br /> DATASPACE<br /><br /> PARTITION_FUNCTION<br /><br /> DATABASE_FILE<br /><br /> PLAN_GUIDE|  
|major_id|**int**|ID of the item on which the extended property exists, interpreted according to its class. For most items, this is the ID that applies to what the class represents. Interpretation for nonstandard major IDs is as follows:<br /><br /> If class is 0, major_id is always 0.<br /><br /> If class is 1, 2, or 7 major_id is object_id.|  
|minor_id|**int**|Secondary ID of the item on which the extended property exists, interpreted according to its class. For most items this is 0; otherwise, the ID is as follows:<br /><br /> If class = 1, minor_id is the column_id if column, else 0 if object.<br /><br /> If class = 2, minor_id is the parameter_id.<br /><br /> If class 7 = minor_id is the index_id.|  
|name|**sysname**|Property name, unique with class, major_id, and minor_id.|  
|value|**sql_variant**|Value of the extended property.|  
  
## Permissions  
 [!INCLUDE[ssCatViewPerm](../../includes/sscatviewperm-md.md)] For more information, see [Metadata Visibility Configuration](../../relational-databases/security/metadata-visibility-configuration.md).  
  
## See Also  
 [Catalog Views &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/catalog-views-transact-sql.md)   
 [Extended Properties Catalog Views &#40;Transact-SQL&#41;](https://msdn.microsoft.com/library/f39fd324-efd4-4468-884c-bf77ed1a026f)   
 [sys.fn_listextendedproperty &#40;Transact-SQL&#41;](../../relational-databases/system-functions/sys-fn-listextendedproperty-transact-sql.md)   
 [sp_addextendedproperty &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-addextendedproperty-transact-sql.md)   
 [sp_dropextendedproperty &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-dropextendedproperty-transact-sql.md)   
 [sp_updateextendedproperty &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-updateextendedproperty-transact-sql.md)  
  
  
