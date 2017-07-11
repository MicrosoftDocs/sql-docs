---
title: "DISCOVER_STORAGE_TABLE_COLUMNS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/03/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
  - "docset-sql-devref"
ms.tgt_pltfrm: ""
ms.topic: "reference"
applies_to: 
  - "SQL Server 2016 Preview"
ms.assetid: 24abb88e-33a9-4ae2-829d-cdef0ff22ec1
caps.latest.revision: 14
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_STORAGE_TABLE_COLUMNS Rowset
  Provides information at the column level about storage tables used by an Analysis Services database running in SharePoint or Tabular mode.  
  
 **Applies to:** tabular models  
  
## Rowset Columns  
 The **DISCOVER_STORAGE_TABLE_COLUMNS** rowset contains the following columns.  
  
|**Column name**|**Type indicator**|**Restriction**|**Description**|  
|---------------------|------------------------|---------------------|---------------------|  
|**DATABASE_NAME**|**DBTYPE_WSTR**|Yes|Specifies the database name that contains the tables. If omitted, the current database is used.<br /><br /> The **DISCOVER_STORAGE_TABLE_COLUMNS** rowset can be restricted by using this column.|  
|**CUBE_NAME**|**DBTYPE_WSTR**|Yes|Specifies the cube or model that contains the tables.<br /><br /> The **DISCOVER_STORAGE_TABLES** rowset can be restricted by using this column.|  
|**MEASURE_GROUP_NAME**|**DBTYPE_WSTR**|Yes|The name of the measure group.|  
|**DIMENSION_NAME**|**DBTYPE_WSTR**||The name of the dimension.|  
|**ATTRIBUTE_NAME**|**DBTYPE_WSTR**||The name of the attribute.|  
|**TABLE_ID**|**DBTYPE_WSTR**||The ID of the table.|  
|**COLUMN_ID**|**DBTYPE_ WSTR**||The ID of the column. The column ID is internal to the xVelocity in-memory analytics engine (VertiPaq) and is for information only.|  
|**COLUMN_TYPE**|**DBTYPE_WSTR**||The type of column. The column type is internal to the xVelocity in-memory analytics engine (VertiPaq) and is for information only.<br /><br /> BASIC_DATA<br /><br /> HIERARCHY_DATAID_TO_POSITION<br /><br /> HIERARCHY_POSITION_TO_DATAID<br /><br /> RELATIONSHIP|  
|**COLUMN_ENCODING**|**DBTYPE_UI8**||An integer that represents the type of encoding used for column data.<br /><br /> **0**, used with **COLUMN_TYPE**: HIERARCHY_DATAID_TO_POSITION, HIERARCHY_POSITION_TO_DATAID, RELATIONSHIP<br /><br /> **1**, used with **COLUMN_TYPE**: BASIC_DATA<br /><br /> **2**, used with **COLUMN_TYPE**: BASIC_DATA|  
|**DATATYPE**|**DBTYPE_WSTR**||The data type of the column. Has the following possible values:<br /><br /> DBTYPE_BOOL<br /><br /> DBTYPE_CY<br /><br /> DBTYPE_DATE<br /><br /> DBTYPE_I4<br /><br /> DBTYPE_I8<br /><br /> DBTYPE_R8<br /><br /> DBTYPE_WSTR<br /><br /> N/A|  
|**ISKEY**|**DBTYPE_BOOL**||**True** if the column is used as a primary or foreign key; otherwise **false**.|  
|**ISUNIQUE**|**DBTYPE_BOOL**||**True** if the values in the column are unique; otherwise **false**.|  
|**ISNULLABLE**|**DBTYPE_BOOL**||**True** if the column is nullable; otherwise **false**.|  
|**ISROWNUMBER**|**DBTYPE_BOOL**||**True** if the column is a row number column. Row number columns for internal use by the xVelocity in-memory analytics engine.|  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-working-with-schema-rowsets.md).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|a07ccd44-8148-11d0-87bb-00c04fc33942|  
|ADOMDNAME|StorageTableColumns|  
  
## Example  
 The following code sample uses a DMV query to return the result set.  
  
```  
SELECT *  
FROM $System.DISCOVER_STORAGE_TABLE_COLUMNS  
ORDER BY TABLE_ID DESC  
  
```  
  
## See Also  
 [Analysis Services Schema Rowsets](../../../analysis-services/schema-rowsets/analysis-services-schema-rowsets.md)  
  
  