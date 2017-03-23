---
title: "DISCOVER_LOCATIONS Rowset | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
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
ms.assetid: 6d3a1171-8e4d-4022-ade0-b785cf795d70
caps.latest.revision: 7
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_LOCATIONS Rowset
  Returns information about the contents of a backup file. You must have permission to access the backup file location.  
  
## Rowset Columns  
 The **DISCOVER_LOCATIONS** rowset contains the following columns.  
  
|Column name|Type indicator|Restriction|Description|  
|-----------------|--------------------|-----------------|-----------------|  
|**LOCATION_BACKUP_FILE_PATHNAME**|**DBTYPE_WSTR**|Required, see below.|The location of the backup file.|  
|**LOCATION_PARTITION_OBJECTPATH**|**DBTYPE_WSTR**||The path to the partition relative to the data folder.|  
|**LOCATION_PARTITION_DATASOURCEID**|**DBTYPE_WSTR**||The data source ID used for processing the partition.|  
|**LOCATION_PARTITION_DATASOURCENAME**|**DBTYPE_WSTR**||The name of the data source used for processing.|  
|**LOCATION_PARTITION_NAME**|**DBTYPE_WSTR**||The partition name.|  
|**LOCATION_PARTITION_SIZE**|**DBTYPE_WSTR**||The approximate size of the partition.|  
|**LOCATION_CONNECTION_STRING**|**DBTYPE_WSTR**||The connection string for the data source used in processing.|  
|**LOCATION_PARTITION_FOLDER**|**DBTYPE_WSTR**||The original location of this partition when the backup file was produced.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **DISCOVER_LOCATIONS** rowset can be restricted on the columns listed in the following table.  
  
|Column name|Type indicator|Restriction state|  
|-----------------|--------------------|-----------------------|  
|**LOCATION_BACKUP_FILE_PATHNAME**|**DBTYPE_WSTR**|Required|  
|**LOCATION_PASSWORD PF_DBTYPE**|**DBTYPE_WSTR**|Required if it was specified during backup. This restriction is not used to restrict the rows returned. It is used to provide the password to access the location.|  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-working-with-schema-rowsets.md).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|a07ccd92-8148-11d0-87bb-00c04fc33942|  
|ADOMDNAME|Locations|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
  