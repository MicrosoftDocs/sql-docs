---
title: "DISCOVER_TRANSACTIONS Rowset | Microsoft Docs"
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
ms.assetid: 85789177-c5df-4336-a90c-c20d69277ab4
caps.latest.revision: 6
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# DISCOVER_TRANSACTIONS Rowset
  Returns the current set of pending transactions on the system.  
  
 **Applies to:** tabular models, multidimensional models  
  
## Rowset Columns  
 The **DISCOVER_TRANSACTIONS** rowset contains the following columns.  
  
|Column name|Type indicator|Description|  
|-----------------|--------------------|-----------------|  
|**TRANSACTION_ID**|**DBTYPE_WSTR**|The transaction unique identifier, as a GUID.|  
|**TRANSACTION_SESSION_ID**|**DBTYPE_WSTR**|The transaction session unique identifier, as a GUID.|  
|**TRANSACTION_START_TIME**|**DBTYPE_DBTIMESTAMP**|The server UTC date and time when the transaction was started.|  
|**TRANSACTION_ELAPSED_TIME_MS**|**DBTYPE_I8**|The elapsed time, in milliseconds, since the start of the transaction.|  
|**TRANSACTION_CPU_TIME_MS**|**DBTYPE_I8**|The CPU time, in milliseconds, consumed by all requests since the beginning of the transaction.|  
  
 This schema rowset is not sorted.  
  
## Restriction Columns  
 The **DISCOVER_TRANSACTIONS** rowset can be restricted on the columns listed in the following table.  
  
|**Column name**|**Type indicator**|**Restriction State**|  
|---------------------|------------------------|---------------------------|  
|**ID**|**DBTYPE_WSTR**|Optional.|  
|**SESSION_ID**|**DBTYPE_WSTR**|Optional.|  
  
## Using ADOMD.NET to return the rowset  
 When using ADOMD.NET and the schema rowset to retrieve metadata, you can use either the GUID or string to reference a schema rowset object in the GetSchemaDataSet method. For more information, see [Working with Schema Rowsets in ADOMD.NET](../../../analysis-services/multidimensional-models-adomd-net-client/retrieving-metadata-working-with-schema-rowsets.md).  
  
 The following table provides the GUID and string values that identify this rowset.  
  
|Argument|Value|  
|--------------|-----------|  
|GUID|a07ccd28-8148-11d0-87bb-00c04fc33942|  
|String|DISCOVER_TRANSACTIONS|  
  
## See Also  
 [XML for Analysis Schema Rowsets](../../../analysis-services/schema-rowsets/xml/xml-for-analysis-schema-rowsets.md)  
  
  