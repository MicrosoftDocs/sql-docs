---
title: Extensibility Framework API for Microsoft SQL Server
titleSuffix: SQL Server Language Extensions
description:  
author: dphansen
ms.author: davidph 
ms.date: 11/04/2019
ms.topic: reference
ms.prod: sql
ms.technology: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Extensibility Framework API for Microsoft SQL Server

## What is the Extensibility Framework API?

*We need something here...*

## API Calls

### Return value for all the calls

All the functions return a **SQLRETURN** parameter. If the value is anything other than **SQL_SUCCESS**, the execution of the script stops.

### Init

Initialize global, shared information. For example, jvm. 

```C++
SQLRETURN Init(
    SQLPOINTER		PropertyBag
);
```

- **PropertyBag:** The property bag defined for the extension when it was registered.

### InitSession

Initialize per-session information.

```C++
SQLRETURN InitSession(
    SQLGUID		    SessionId,
    SQLUSMALLINT    TaskId,
    SQLCHAR*       	Script,
    SQLULEN         ScriptLength,
    SQLUSMALLINT    InputSchemaColumnsNumber,
    SQLUSMALLINT    ParametersNumber
    SQLCHAR*       	InputDataName,
    SQLUSMALLINT	InputDataNameLength,
    SQLCHAR*       	OutputDataName,
    SQLUSMALLINT	OutputDataNameLength
);
```

- **Script:** Pointer to a null-terminated buffer that contains the external language script.
- **ScriptLength:** Length of the *Script buffer in bytes ([excluding the null-termination character](https://docs.microsoft.com/sql/odbc/reference/syntax/sqldescribecol-function)).
- **InputSchemaColumnsNumber:** Number of columns in the input schema.
- **ParametersNumber:** Number of input parameters.
- **InputDataName & OutputDataName:** Pointer to a null-terminated buffer that contains the name of the input/output datasets. These parameters are N/A for pre-compiled languages.
- **InputDataNameLength & OutputDataNameLength:** Length of *InputDataName and *OutputDataName in bytes, respectively ([excluding the null-termination character](https://docs.microsoft.com/sql/odbc/reference/syntax/sqldescribecol-function)). These parameters are N/A for pre-compiled languages.

### InitColumn

Initialize the information regarding a given column in the input dataset for a particular session.

```C++
SQLRETURN InitColumn(
    SQLGUID		    SessionId,
    SQLUSMALLINT 	TaskId,
    SQLUSMALLINT	ColumnNumber,
    SQLCHAR*       	ColumnName,
    SQLSMALLINT     ColumnNameLength,
    SQLSMALLINT     DataType,
    SQLULEN         ColumnSize,
    SQLSMALLINT     DecimalDigits,
    SQLSMALLINT     Nullable,
    SQLSMALLINT     PartitionByNumber,
    SQLSMALLINT     OrderByNumber
);
```

- **ColumnNumber:** Number of the input schema column. Columns are numbered sequentially in increasing order starting at 0.
- **ColumnName:** Pointer to a null-terminated buffer that contains the column name.
- **ColumnNameLength:** The length (excluding the null termination character) of *ColumnName in bytes.
- **DataType:** The C type identifier of the column.
- **ColumnSize:** The size of the column in the data source in bytes.
- **DecimalDigits:** The number of decimal digits of the column on the data source. If the number of decimal digits cannot be determined or is not applicable, contains 0.
- **Nullable:** A value that indicates whether the column allows NULL values. Possible values:
    - SQL_NO_NULLS: The column does not allow NULL values.
    - SQL_NULLABLE: The column allows NULL values.
- **PartitionByNumber:** Number of the column in the @partition_by sequence. Columns are numbered sequentially in increasing order starting at 0. If the column is not included in the partition by sequence, the value of this argument is -1.
- **OrderByNumber:** Number of the column in the @order_by sequence. Columns are numbered sequentially in increasing order starting at 0. If the column is not included in the order by sequence, the value of this argument is -1.
