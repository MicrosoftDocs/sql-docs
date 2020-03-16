---
title: Extensibility Framework API for Microsoft SQL Server
titleSuffix: SQL Server Language Extensions
description:  
author: dphansen
ms.author: davidph 
ms.date: 03/16/2020
ms.topic: reference
ms.prod: sql
ms.technology: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Extensibility Framework API for Microsoft SQL Server

## What is the Extensibility Framework API?

You can use the extensibility framework to write programming language extensions for SQL Server. The Extensibility Framework API for Microsoft SQL Server is an API that can be used by a language extension to interact with and exchange data with SQL Server. As a language extension author, you can use this reference together with the open sourced [Java language extension for SQL Server](../how-to/extensibility-sdk-java-sql-server.md) to understand how to use the API for writing your own language extensions.

## API Calls

### Return value for all the calls

All the functions return a **SQLRETURN** parameter. If the value is anything other than **SQL_SUCCESS**, the execution of the script stops.

### Init

Initialize global, shared information. For example, jvm.

```C++
SQLRETURN Init(
    SQLPOINTER    PropertyBag
);
```

- **PropertyBag:** The property bag defined for the extension when it was registered.

### InitSession

Initialize per-session information.

```C++
SQLRETURN InitSession(
    SQLGUID         SessionId,
    SQLUSMALLINT    TaskId,
    SQLCHAR*        Script,
    SQLULEN         ScriptLength,
    SQLUSMALLINT    InputSchemaColumnsNumber,
    SQLUSMALLINT    ParametersNumber
    SQLCHAR*        InputDataName,
    SQLUSMALLINT    InputDataNameLength,
    SQLCHAR*        OutputDataName,
    SQLUSMALLINT    OutputDataNameLength
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
    SQLGUID       SessionId,
    SQLUSMALLINT  TaskId,
    SQLUSMALLINT  ColumnNumber,
    SQLCHAR*      ColumnName,
    SQLSMALLINT   ColumnNameLength,
    SQLSMALLINT   DataType,
    SQLULEN       ColumnSize,
    SQLSMALLINT   DecimalDigits,
    SQLSMALLINT   Nullable,
    SQLSMALLINT   PartitionByNumber,
    SQLSMALLINT   OrderByNumber
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

### InitParam

Initialize the information regarding a given input parameter for a particular session.

```C++
SQLRETURN InitParam(
    SQLGUID      SessionId,
    SQLUSMALLINT TaskId,
    SQLUSMALLINT ParamNumber,
    SQLCHAR*     ParamName,
    SQLSMALLINT  ParamNameLength,
    SQLSMALLINT  DataType,
    SQLULEN      ParamSize,
    SQLSMALLINT  DecimalDigits,
    SQLPOINTER   ParamValue,
    SQLINTEGER   StrLen_or_Ind,
    SQLSMALLINT  InputOutputType
);
```

- **ParamNumber:** Number of the parameter. Parameters are numbered sequentially in increasing order starting at 0.
- **ParamName:** Pointer to a null-terminated buffer that contains the parameter's name.
- **ParamNameLength:** The length (excluding the null termination character) of ParamName in bytes.
- **DataType:** The C type identifier of the parameter.
- **ParamSize:** The size of the parameter type in bytes.
- **DecimalDigits:** The number of decimal digits of the parameter on the data source. If the number of decimal digits cannot be determined or is not applicable, contains 0.
- **StrLen_or_Ind:** The length or indicator value. Possible values:
    - n, where n > 0. Indicating the length of the data in bytes.
    - SQL_NULL_DATA
- **InputOutputType:** Possible values:
    - SQL_PARAM_INPUT
    - SQL_PARAM_INPUT_OUTPUT

### Execute

Execute the script.

```C++
SQLRETURN Execute(
    SQLGUID         SessionId,
    SQLUSMALLINT    TaskId,
    SQLULEN         RowsNumber,
    SQLPOINTER*     Data,
    SQLINTEGER**    StrLen_or_Ind,
    SQLUSMALLINT*   OutputSchemaColumnsNumber
);
```

- **RowsNumber:** Number of rows in the input dataset passes in Data.
- **Data:** A 2D array of the input dataset. The length of the array is \[InputSchemaColumnsNumber\] (received in the InitSession call). Each column's array has \[RowsNumber\] elements that should be interpreted according to the column type (from InitColumn). Each such element is the value of row \[i\] of column\[j\].
- **StrLen_or_Ind:** A 2D array the size of the input data that represents the length or null indicator value. Possible values of each cell:
    - n, where n > 0. Indicating the length of the data in bytes
    - SQL_NULL_DATA

    The length of the array is \[InputSchemaColumnsNumber\] (received in the InitSession call). Each column's array has \[RowsNumber\] elements that should be interpreted according to the column type (from InitColumn).

    If column col is not nullable and represents a data type of fixed size, StrLen_or_Ind\[col\] is a null pointer. Otherwise it points to a valid array with \[RowsNumber\] elements, and for each element it contains its length or null indicator data.

### GetResultColumn

Get the information regarding a given column in the output dataset for a particular session:

```C++
SQLRETURN GetResultColumn(
    SQLGUID         SessionId,
    SQLUSMALLINT    TaskId,
    SQLUSMALLINT    ColumnNumber,
    SQLSMALLINT*    DataType,
    SQLINTEGER*     ColumnSize,
    SQLSMALLINT*    DecimalDigits,
    SQLSMALLINT*    Nullable
);
```

- **ColumnNumber:** Number of the requested output schema column. Columns are numbered sequentially in increasing order starting at 0. 
- **DataType:** \[Output\] A pointer to the buffer that contains the C type identifier of the column.
- **ColumnSize:** \[Output\] The size of the column on the data source in bytes.
- **DecimalDigits:** \[Output\] The number of decimal digits of the column on the data source. If the number of decimal digits cannot be determined or is not applicable, contains 0. 
- **Nullable:** \[Output\] A value that indicates whether the column allows NULL values. Possible values: 
    - SQL_NO_NULLS: The column does not allow NULL values. 
    - SQL_NULLABLE: The column allows NULL values. 
    - SQL_NULLABLE_UNKNOWN

    If other values are passed, execution stops and errors out.

### GetResults

Get output data

```C++
SQLRETURN GetResults(
    SQLGUID         SessionId,
    SQLUSMALLINT    TaskId,
    SQLULEN*        RowsNumber,
    SQLPOINTER**    Data,
    SQLINTEGER***   StrLen_or_Ind
);
```

- **RowsNumber:** \[Output\] Number of rows in the output dataset being passes in Data.

- **Data:** \[Output\] A 2D array of the output dataset allocated by the extension. The length of the array is \[OutputSchemaColumnsNumber\] (known from the `Execute()` call). Each column's array should have \[RowsNumber\] elements that should be interpreted according to the column type (known from `GetResultColumn()`). Each such element is the value of row \[i\] of column\[j\]. 

- **StrLen_or_Ind:** \[Output\] A 2D array the size of the output data that represents the length or null indicator value. Possible values of each cell: 
    - n, where n > 0. Indicating the length of the data in bytes 
    - SQL_NULL_DATA 
 
    The length of the array is \[OutputSchemaColumnsNumber\] (known from the `Execute()` call). Each column's array has \[RowsNumber\] elements that should be interpreted according to the column type (known from GetResultColumn()). 

    If column col is not nullable and represents a data type of fixed size, StrLen_or_Ind\[col\] is ignored.

### GetOutputParam

Get output parameters at the end of the script.

```C++
SQLRETURN GetOutputParam(
    SQLGUID        SessionId,
    SQLUSMALLINT   ParamNumber,
    SQLPOINTER*    ParamValue,
    SQLINTEGER*    StrLen_or_Ind
);
```

- **ParamValue:** \[Output\] The value of the output parameter

- **StrLen_or_Ind:** \[Output\] The length or indicator value. Possible values:
    - n, where n > 0. Indicating the length of the data in bytes.
    - SQL_NULL_DATA 

### CleanupSession

Clean up per-session information.

```C++
SQLRETURN CleanupSession(
    SQLGUID        SessionId,
    SQLUSMALLINT   TaskId
);
```

### Cleanup

Clean up global, shared information (e.g. jvm).

```C++
SQLRETURN Cleanup();
```

### GetTelemetryResults

Retrieve telemtry collected by the extension.

```C++
SQLRETURN GetTelemetryResults(
    SQLGUID        SessionId,
    SQLUSMALLINT   TaskId,
    SQLUINTEGER    *RowsNumber,
    SQLCHAR        ***CounterNames,
    SQLINTEGER      **CounterNamesLength,
    SQLBIGINT       **CounterValues
);
```

### InstallExternalLibrary

Installs a library.

```C++
SQLRETURN InstallExternalLibrary(
    SQLGUID    SetupSessionId,
    SQLCHAR    *LibraryName,
    SQLINTEGER LibraryNameLength,
    SQLCHAR    *LibraryFile,
    SQLINTEGER LibraryFileLength,
    SQLCHAR    *LibraryInstallDirectory,
    SQLINTEGER LibraryInstallDirectoryLength,
    SQLCHAR    **LibraryError,
    SQLINTEGER *LibraryErrorLength
);
```

### UninstallLibrary

Uninstalls a library.

```C++
SQLRETURN UninstallExternalLibrary(
    SQLGUID    SetupSessionId,
    SQLCHAR    *LibraryName,
    SQLINTEGER LibraryNameLength,
    SQLCHAR    *LibraryInstallDirectory,
    SQLINTEGER LibraryInstallDirectoryLength,
    SQLCHAR    **LibraryError,
    SQLINTEGER *LibraryErrorLength
);
```

## Next steps

- [Microsoft Extensibility SDK for Java for SQL Server](../how-to/extensibility-sdk-java-sql-server.md)