---
title: "Table-Valued Parameter data conversion"
description: "Table-Valued Parameter Data Conversion and Other Errors and Warnings"
author: markingmyname
ms.author: maghan
ms.date: "03/14/2017"
ms.service: sql
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords:
  - "table-valued parameters (ODBC), data conversion"
  - "table-valued parameters (ODBC), error messages"
---
# Table-Valued Parameter Data Conversion and Other Errors and Warnings
[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  Table-valued parameter column values can be converted between client and server data types in the same way as other column and parameter values. But because a table-valued parameter can contain multiple columns and multiple rows, it is important to be able to identify the actual value where the error occurred.  
  
 When an error or warning is detected in a table-valued parameter column, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client will generate a diagnostic record. The error message will contain the parameter number of the table-valued parameter, plus the column ordinal and row number. An application can also use the diagnostic fields SQL_DIAG_SS_TABLE_COLUMN_NUMBER and SQL_DIAG_SS_TABLE_ROW_NUMBER within diagnostic records to determine which values are associated with errors and warnings. These diagnostic fields are available in [!INCLUDE[sql2008-md](../../includes/sql2008-md.md)] and later versions.  
  
 The SQLSTATE and message components of diagnostic records will conform to existing ODBC behavior in all other respects. That is, except for the parameter, row, and column identification information, error messages have the same values for table-valued parameters as they would for non-table-valued parameters.  
  
## See Also  
 [Table-Valued Parameters &#40;ODBC&#41;](../../relational-databases/native-client-odbc-table-valued-parameters/table-valued-parameters-odbc.md)  
  
  
