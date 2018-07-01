---
title: "DBSCHEMA_TABLES Rowset | Microsoft Docs"
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
# DBSCHEMA_TABLES Rowset
[!INCLUDE[ssas-appliesto-sqlas](../../../includes/ssas-appliesto-sqlas.md)]
  Identifies the measure groups and dimensions exposed as tables within [!INCLUDE[msCoName](../../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] [!INCLUDE[ssASnoversion](../../../includes/ssasnoversion-md.md)].  
  
## Rowset Columns  
 The **DBSCHEMA_TABLES** rowset contains the following columns.  
  
|Column name|Type indicator|Length|Description|  
|-----------------|--------------------|------------|-----------------|  
|**TABLE_CATALOG**|**DBTYPE_WSTR**|255|The name of the catalog to which this object belongs.|  
|**TABLE_SCHEMA**|**DBTYPE_WSTR**|255|The name of the cube to which this object belongs.|  
|**TABLE_NAME**|**DBTYPE_WSTR**|255|The name of the object, if **TABLE_TYPE** is **TABLE**.|  
|**TABLE_TYPE**|**DBTYPE_WSTR**||The type of the table.<br /><br /> **TABLE** indicates the object is a measure group.<br /><br /> **SYSTEM TABLE** indicates the object is a dimension.|  
|**TABLE_GUID**|**DBTYPE_GUID**||Not supported.|  
|**DESCRIPTION**|**DBTYPE_WSTR**||A human-readable description of the object.|  
|**TABLE_PROPID**|**DBTYPE_UI4**||Not supported.|  
|**DATE_CREATED**|**DBTYPE_DBTIMESTAMP**||Not supported.|  
|**DATE_MODIFIED**|**DBTYPE_DBTIMESTAMP**||The date the object was last modified.|  
|**TABLE_OLAP_TYPE**|**DBTYPE_WSTR**||The OLAP type of the object.<br /><br /> **MEASURE_GROUP** indicates the object is a measure group.<br /><br /> **CUBE_DIMENSION** indicated the object is a dimension.|  
  
 The rowset is sorted on **TABLE_TYPE**, **TABLE_CATALOG**, **TABLE_SCHEMA**, and **TABLE_NAME**.  
  
## Restriction Columns  
 The **DBSCHEMA_TABLES** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction State|  
|-----------------|--------------------|-----------------------|  
|**TABLE_CATALOG**|**DBTYPE_WSTR**|Optional|  
|**TABLE_SCHEMA**|**DBTYPE_WSTR**|Optional|  
|**TABLE_NAME**|**DBTYPE_WSTR**|Optional|  
|**TABLE_TYPE**|**DBTYPE_WSTR**|Optional|  
|**TABLE_OLAP_TYPE**|**DBTYPE_WSTR**|Optional|  
  
## See Also  
 [OLE DB Schema Rowsets](../../../analysis-services/schema-rowsets/ole-db/ole-db-schema-rowsets.md)  
  
  
