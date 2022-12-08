---
description: "Table-Valued Parameters (ODBC)"
title: "Table-Valued Parameters (ODBC) | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "table-valued parameters (ODBC)"
  - "ODBC, table-valued parameters"
ms.assetid: ef06cd13-18e2-4c65-8ede-c3955d820e54
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Table-Valued Parameters (ODBC)
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  ODBC support for table-valued parameters lets a client application send parameterized data to the server more efficiently, by sending multiple rows to the server with one call.  
  
 For information about table-valued parameters on the server, see [Use Table-Valued Parameters &#40;Database Engine&#41;](../../relational-databases/tables/use-table-valued-parameters-database-engine.md).  
  
 In ODBC, there are two ways that you can send table-valued parameters to the server:  
  
-   All the table-valued parameter data can be in memory at the time SQLExecDirect or SQLExecute is called. This data is stored in arrays if there are multiple rows in the table-value.  
  
-   An application can specify data-at-execution for a table-valued parameter when SQLExecDirect or SQLExecute is called. In this case, rows of data for the table-value can be provided in batches, or one at a time to reduce memory requirements.  
  
 The first option enables stored procedures to encapsulate more business logic. For example, a single stored procedure could encapsulate a whole order entry transaction when the order items are passed as a table-valued parameter. This option is very efficient, because only a single round trip to the server is required. Alternatively, you could use different procedures to handle the order header and order items separately, which would require more code and a more complex contract between the client and server.  
  
 The second method provides an efficient mechanism for bulk operations with very large amounts of data. This enables an application to stream rows of data to the server without having to buffer them all in memory first.  
  
 You can create constraints and primary keys when you create the table variable. Constraints are a good way to ensure that the data in a table meets specific requirements.  
  
## In This Section  
 [Uses of ODBC Table-Valued Parameters](../../relational-databases/native-client-odbc-table-valued-parameters/uses-of-odbc-table-valued-parameters.md)  
 Describes the primary user scenarios for table-valued parameters and ODBC.  
  
 [ODBC SQL Type for Table-Valued Parameters](../../relational-databases/native-client-odbc-table-valued-parameters/odbc-sql-type-for-table-valued-parameters.md)  
 Describes the SQL_SS_TABLE type. This is a new ODBC SQL type that supports table-valued parameters.  
  
 [Table-Valued Parameter Descriptor Fields](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameter-descriptor-fields.md)  
 Describes descriptor fields that support table-valued parameters.  
  
 [Descriptor Fields for Table-Valued Parameter Constituent Columns](../../relational-databases/native-client-odbc-table-valued-parameters/descriptor-fields-for-table-valued-parameter-constituent-columns.md)  
 Describes descriptor fields that have meaning for table-valued parameters.  
  
 [Table-Valued Parameter Diagnostic Record Fields](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameter-diagnostic-record-fields.md)  
 Describes two diagnostic fields that have been added to diagnostic records to support table-valued parameters.  
  
 [Statement Attributes that Affect Table-Valued Parameters](../../relational-databases/native-client-odbc-table-valued-parameters/statement-attributes-that-affect-table-valued-parameters.md)  
 Describes a new descriptor header field that enables table-valued parameters columns to be addressed.  
  
 [Binding and Data Transfer of Table-Valued Parameters and Column Values](../../relational-databases/native-client-odbc-table-valued-parameters/binding-and-data-transfer-of-table-valued-parameters-and-column-values.md)  
 Describes parameter binding and how to pass a table-valued parameter to the server.  
  
 [Table-Valued Parameter Metadata for Prepared Statements](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameter-metadata-for-prepared-statements.md)  
 Describes how an application can obtain metadata for a prepared procedure call.  
  
 [Additional Table-Valued Parameter Metadata](../../relational-databases/native-client-odbc-table-valued-parameters/additional-table-valued-parameter-metadata.md)  
 Describes how to use SQLProcedureColumns, SQLTables, and SQLColumns to retrieve metadata for a table-valued parameter.  
  
 [Table-Valued Parameter Data Conversion and Other Errors and Warnings](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameter-data-conversion-and-other-errors-and-warnings.md)  
 Describes how to process errors on table-valued parameter column values.  
  
 [Cross-Version Compatibility](../../relational-databases/native-client-odbc-table-valued-parameters/cross-version-compatibility.md)  
 Describes conflicts that can occur when table-valued parameters are used by a client or server of a version earlier than [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)].  
  
 [ODBC Table-Valued Parameter API Summary](../../relational-databases/native-client-odbc-table-valued-parameters/odbc-table-valued-parameter-api-summary.md)  
 Lists the ODBC functions that support table-valued parameters.  

## See Also  
 [SQL Server Native Client &#40;ODBC&#41;](../../relational-databases/native-client/odbc/sql-server-native-client-odbc.md)   
 [Table-Valued Parameters &#40;SQL Server Native Client&#41;](../../relational-databases/native-client/features/table-valued-parameters-sql-server-native-client.md)  
  
