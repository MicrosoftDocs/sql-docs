---
title: "SQL Server Destination Custom Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: b736aa6d-c154-44a0-be08-f25733fca1d9
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# SQL Server Destination Custom Properties
  The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination has both custom properties and the properties common to all data flow components.  
  
 The following table describes the custom properties of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination. All properties are read/write.  
  
|Property name|Data Type|Description|  
|-------------------|---------------|-----------------|  
|AlwaysUseDefaultCodePage|Boolean|Forces the use of the DefaultCodePage property value. The default value of this property is `False`.|  
|BulkInsertCheckConstraints|Boolean|A value that specifies whether the bulk insert checks constraints. The default value of this property is `True`.|  
|BulkInsertFireTriggers|Boolean|A value that specifies whether the bulk insert fires triggers on tables. The default value of this property is `False`.|  
|BulkInsertFirstRow|Integer|A value that specifies the first row to insert. The default value of this property is **-1**, which indicates that no value has been assigned|  
|BulkInsertKeepIdentity|Boolean|A value that specifies whether values can be inserted into identity columns. The default value of this property is `False`.|  
|BulkInsertKeepNulls|Boolean|A value that specifies whether the bulk insert keeps Null values. The default value of this property is `False`.|  
|BulkInsertLastRow|Integer|A value that specifies the last row to insert. The default value of this property is **-1**, which indicates that no value has been assigned.|  
|BulkInsertMaxErrors|Integer|A value that specifies the number of errors that can occur before the bulk insert stops. The default value of this property is **-1**, which indicates that no value has been assigned.|  
|BulkInsertOrder|String|The names of the sort columns. Each column can be sorted in ascending or descending order. If multiple sort columns are used, the column names are separated by commas.|  
|BulkInsertTableName|String|The [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table or view in the database to which the data is copied.|  
|BulkInsertTablock|Boolean|A value that specifies whether the table is locked during the bulk insert. The default value of this property is `True`.|  
|DefaultCodePage|Integer|The code page to use when code page information is not available from the data source.|  
|MaxInsertCommitSize|Integer|A value that specifies the maximum number of rows to insert in a batch. When the value is zero, all rows are inserted in a single batch.|  
|Timeout|Integer|A value that specifies the number of seconds the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination waits before termination if there is no data available for insertion. A value of 0 means that the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination will not time out. The default value of this property is 30.|  
  
 The input and the input columns of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] destination have no custom properties.  
  
 For more information, see [SQL Server Destination](sql-server-destination.md).  
  
## See Also  
 [Common Properties](../common-properties.md)  
  
  
