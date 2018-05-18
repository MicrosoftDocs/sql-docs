---
title: "MDSCHEMA_INPUT_DATASOURCES Rowset | Microsoft Docs"
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
# MDSCHEMA_INPUT_DATASOURCES Rowset
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Describes the data sources defined within the database.  
  
## Rowset Columns  
 The **MDSCHEMA_INPUT_DATASOURCES** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**||The name of the catalog to which this data source belongs.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**||Not supported.|  
|**DATASOURCE_NAME**|**DBTYPE_WSTR**||The name of the data source object.|  
|**DATASOURCE_TYPE**|**DBTYPE_WSTR**||The type of the data source. Valid values include:<br /><br /> **Relational**<br /><br /> **Olap**|  
|**CREATED_ON**|**DBTYPE_DBTIMESTAMP**||The date that the data source was created.|  
|**LAST_SCHEMA_UPDATE**|**DBTYPE_DBTIMESTAMP**||The date and time that the data source was last modified.|  
|**DESCRIPTION**|**DBTYPE_WSTR**||A user-friendly description of the action.|  
|**TIMEOUT**|**DBTYPE_UI4**||The timeout of the data source.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **MDSCHEMA_INPUT_DATASOURCES** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**CATALOG_NAME**|**DBTYPE_WSTR**|Optional.|  
|**SCHEMA_NAME**|**DBTYPE_WSTR**|Optional.|  
|**DATASOURCE_NAME**|**DBTYPE_WSTR**|Optional.|  
|**DATASOURCE_TYPE**|**DBTYPE_WSTR**|Optional.|  
  
## See Also  
 [OLE DB for OLAP Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets.md)  
  
  
