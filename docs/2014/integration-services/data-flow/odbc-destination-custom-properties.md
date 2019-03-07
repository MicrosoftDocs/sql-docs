---
title: "ODBC Destination Custom Properties | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: conceptual
ms.assetid: 07508c40-6c08-4359-96cd-8ff17671244d
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# ODBC Destination Custom Properties
  The following table describes the custom properties of the ODBC destination. All properties can be set from SSIS property expressions.  
  
|Property name|Data Type|Description|  
|-------------------|---------------|-----------------|  
|Connection|ODBC Connection|An ODBC connection to access the destination database.|  
|BatchSize|Integer|The size of the batch for bulk loading. This is the number of rows loaded as a batch. This is valid only if the row-wise parameter binding is supported. If the row-wise parameter binding is not supported, the batch size is 1.|  
|BindCharColumnAs|Integer (enumeration)|This property determines how the ODBC destination binds columns with multiple-byte string types such as SQL_CHAR, SQL_VARCHAR, or SQL_LONGVARCHAR.<br /><br /> The possible values are Unicode (0), which binds the columns as SQL_C_WCHAR, and ANSI (1), which binds the columns as SQL_C_CHAR). The default is Unicode (0).<br /><br /> Unicode is the best option for most ODBC 3.x providers and ODBC 2.x providers that support binding CHAR parameters as wide strings. When you select Unicode and ExposeCharColumnsAsUnicode is True, the user does not need to specify the code page used by the source database.<br /><br /> **Note:** This property is not available in the **ODBC Destination Editor**, but can be set by using the **Advanced Editor**.|  
|BindNumericAs|Integer (enumeration)|This property determines how the ODBC destination binds columns with numeric data with data types SQL_TYPE_NUMERIC and SQL_TYPE_DECIMAL.<br /><br /> The possible values are Char (0), which binds the columns as SQL_C_CHAR and Numeric (1), which binds the columns as SQL_C_NUMERIC. The default value is Char (0).<br /><br /> **Note**: This property is not available in the **ODBC Destination Editor**, but can be set by using the **Advanced Editor**.|  
|DefaultCodePage|Integer|The code page to use for string columns.<br /><br /> **Note**: This property is not available in the **ODBC Destination Editor**, but can be set by using the **Advanced Editor**.|  
|InsertMethod|Integer (enumeration)|The method used for inserting the data. The possible values are Row by row (0) and Batch (1). The default value is Batch (1).<br /><br /> For more information about these options, see "Load Options" in [ODBC Destination](odbc-destination.md).|  
|StatementTimeout|Integer|The number of seconds to wait for an SQL statement to execute before returning, with an error, to the application. The default value is 120.|  
|TableName|String|The name of the destination table where the data that is being inserted.|  
|TransactionSize|Integer|The number of inserts in a single transaction. The default value is 0, which means that the ODBC destination works in auto commit mode.<br /><br /> Because the ODBC connection manager does not support distributed transactions, it is possible to set this property with a value other than 0. However, if the connection manager **RetainSameConnection** property is set to **true** then this property must be set to 0.<br /><br /> **Note**: This property is not available in the **ODBC Destination Editor**, but can be set by using the **Advanced Editor**.|  
|LobChunkSize|Integer|The chunk size allocation for LOB columns.|  
  
  
