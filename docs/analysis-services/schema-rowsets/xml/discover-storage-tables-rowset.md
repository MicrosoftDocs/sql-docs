---
title: "DISCOVER_STORAGE_TABLES Rowset | Microsoft Docs"
ms.date: 05/03/2018
ms.prod: sql
ms.technology: analysis-services
ms.custom: schema-rowsets
ms.topic: reference
ms.author: owend
ms.reviewer: owend
author: minewiskan
manager: kfile
---
# DISCOVER_STORAGE_TABLES Rowset
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Allows the client to determine the tables that are included in an Analysis Services database running in Tabular or SharePoint mode.  
  
## Rowset Columns  
 The **DISCOVER_STORAGE_TABLES** rowset contains the following columns.  
  
|**Column name**|**Type indicator**|**Length**|**Description**|  
|---------------------|------------------------|----------------|---------------------|  
|**DATABASE_NAME**|**DBTYPE_WSTR**||Specifies the database name that contains the tables.<br /><br /> The **DISCOVER_STORAGE_TABLES** rowset can be restricted by using this column. If this column is not used to restrict the rowset, the current database is used.|  
|**CUBE_NAME**|**DBTYPE_WSTR**||Specifies the cube or model that contains the tables.<br /><br /> The **DISCOVER_STORAGE_TABLES** rowset can be restricted by using this column.|  
|**MEASURE_GROUP_NAME**|**DBTYPE_WSTR**||The name of the measure group.|  
|**PARTITION_NAME**|**DBTYPE_WSTR**||The name of the partition.|  
|**DIMENSION_NAME**|**DBTYPE_WSTR**||The name of the dimension.|  
|**TABLE_ID**|**DBTYPE_WSTR**||The ID of the table that is used to store the table attributes.|  
|**TABLE_PARTITIONS_COUNT**|**DBTYPE_ WSTR**||The table partition count.|  
|**HINT_TABLE_TYPE**|**DBTYPE_ WSTR**||The hint of the table type.|  
|**ROWS_COUNT**|**DBTYPE_UI4**||The number of rows in the partition.|  
|**RIVIOLATION_COUNT**|**DBTYPE_UI4**||The number of rows with referential integrity violations.|  
  
## Restriction Columns  
 The **DISCOVER_STORAGE_TABLES** rowset can be restricted on the columns listed in the following table.  
  
|**Column name**|**Type indicator**|**Restriction State**|  
|---------------------|------------------------|---------------------------|  
|**DATABASE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**CUBE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**MEASURE_GROUP_NAME**|**DBTYPE_WSTR**|Optional|  
|**PARTITION_NAME**|**DBTYPE_WSTR**|Optional|  
  
## Example  
 The following code sample returns a list of the storage tables and the number of rows in each, from the default database on the current connection.  
  
```  
SELECT TABLE_ID, ROWS_COUNT  
FROM $system.DISCOVER_STORAGE_TABLES  
ORDER BY TABLE_ID DESC  
  
```  
  
## See Also  
 [Analysis Services Schema Rowsets](../../../analysis-services/schema-rowsets/analysis-services-schema-rowsets.md)  
  
  
