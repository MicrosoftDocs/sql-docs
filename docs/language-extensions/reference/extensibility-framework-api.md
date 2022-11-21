---
title: Extensibility Framework API
titleSuffix: SQL Server Language Extensions
description: You can use the extensibility framework to write programming language extensions for SQL Server. The Extensibility Framework API for Microsoft SQL Server is an API that can be used by a language extension to interact with and exchange data with SQL Server.
author: rothja
ms.author: jroth 
ms.date: 10/09/2020
ms.topic: reference
ms.service: sql
ms.subservice: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
---
# Extensibility Framework API for SQL Server
[!INCLUDE [SQL Server 2019 and later](../../includes/applies-to-version/sqlserver2019.md)]

You can use the extensibility framework to write programming language extensions for SQL Server. The Extensibility Framework API for Microsoft SQL Server is an API that can be used by a language extension to interact with and exchange data with SQL Server.

As a language extension author, you can use this reference together with the open-sourced language extensions to understand how to use the API for writing your own. You can find the source code for the language extensions at [aka.ms/mssql-lang-extensions](https://aka.ms/mssql-lang-extensions).

Find the syntax and arguments information about all API functions below.

## Return value

All functions return a *SQLRETURN* parameter. If the value is anything other than *SQL_SUCCESS*, it is treated as an error and the execution of the script stops.

## Standard output

Any output by the extension to the standard output or error streams will be traced to the session's log file, and will eventually be traced back to SQL Server (Similar to anything displayed in the SSMS messages tab).


## Init

This function is only called once and is used to initialize the runtime for execution. 

### Syntax

```cpp
SQLRETURN Init(
    SQLCHAR *ExtensionParams,
    SQLULEN ExtensionParamsLength,
    SQLCHAR *ExtensionPath,
    SQLULEN ExtensionPathLength,
    SQLCHAR *PublicLibraryPath,
    SQLULEN PublicLibraryPathLength,
    SQLCHAR *PrivateLibraryPath,
    SQLULEN PrivateLibraryPathLength
);
```

### Arguments

*ExtensionParams*  
\[Input\] Null-terminated string containing `PARAMETERS` value provided during [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md) or [ALTER EXTERNAL LANGUAGE](../../t-sql/statements/alter-external-language-transact-sql.md).

*ExtensionParamsLength*  
\[Input\] Length in bytes of *ExtensionParams* (excluding the null termination character).

*ExtensionPath*  
\[Input\] Null-terminated UTF-8 string containing the absolute path to the installation directory of the extension.

*ExtensionPathLength*  
\[Input\] Length in bytes of *ExtensionPath* (excluding the null termination character).

*PublicLibraryPath*  
\[Input\] Null-terminated UTF-8 string containing the absolute path to the public external libraries directory for this external language.

*PublicLibraryPathLength*  
\[Input\] Length in bytes of *PublicLibraryPath* (excluding the null termination character).

*PrivateLibraryPath*  
\[Input\] Null-terminated UTF-8 string containing the absolute path to the private external libraries directory for this user and this external language.

*PrivateLibraryPathLength*  
\[Input\] Length in bytes of *PrivateLibraryPath* (excluding the null termination character).

## InitSession

This function is called once per session and initializing session-specific settings.

### Syntax

```cpp
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

### Arguments

*SessionId*  
\[Input\] GUID uniquely identifying this script session.

*TaskId*  
\[Input\] An integer uniquely identifying this execution process.

When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

*Script*  
\[Input\] Null-terminated UTF-8 string containing the `@script` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

*ScriptLength*  
\[Input\] Length in bytes of *ScriptScript* (excluding the null termination character).

*InputSchemaColumnsNumber*  
\[Input\] Number of columns in the result set from `@input_data_1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

*ParametersNumber*  
\[Input\] Number of input parameters from `@params` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

*InputDataName*  
\[Input\] Null-terminated UTF-8 string containing the `@input_data_1_name` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

*InputDataNameLength*  
\[Input\] Length in bytes of *InputDataName* (excluding the null termination character).

*OutputDataName*  
\[Input\] Null-terminated UTF-8 string containing the `@output_data_1_name` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

*OutputDataNameLength*  
\[Input\] Length in bytes of *OutputDataName* (excluding the null termination character).

## InitColumn

Initialize the information for a given column for a particular session.

This function is called for each column in the result set from `@input_data_1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

The column structure of this result set will be referred to as the *input schema*.

### Syntax

```cpp
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

### Arguments

*SessionId*  
\[Input\] GUID uniquely identifying this script session.

*TaskId*  
\[Input\] An integer uniquely identifying this execution process.

When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

*ColumnNumber*  
\[Input\] An integer identifying the index of this column in the input schema. Columns are numbered sequentially in increasing order starting at 0.

*ColumnName*  
\[Input\] Null-terminated UTF-8 string containing the column's name.

*ColumnNameLength*  
\[Input\] Length in bytes of *ColumnName* (excluding the null termination character).

*DataType*  
\[Input\] The ODBC C type identifying this column's data type.

*ColumnSize*  
\[Input\] The maximum size in bytes of the underlying data in this column.

For SQL_C_CHAR, SQL_C_WCHAR and SQL_C_BINARY data types, values larger than 8000 indicate this column represent LOBs object and with sizes up to 2 GB.

*DecimalDigits*  
\[Input\] The decimal digits of underlying data in this column, as defined by [Decimal Digits](../../odbc/reference/appendixes/decimal-digits.md).

*Nullable*  
\[Input\] A value that indicates whether this column may contain NULL values. Possible values:

- SQL_NO_NULLS: The column cannot contain NULL values.
- SQL_NULLABLE: The column may contain NULL values.

*PartitionByNumber*  
\[Input\] A value that indicates the index of this column in the `@input_data_1_partition_by_columns` sequence in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). Columns are numbered sequentially in increasing order starting at 0. If this column is not included in the sequence, the value is -1.

*OrderByNumber*  
\[Input\] A value that indicates the index of this column in the `@input_data_1_order_by_columns` sequence in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md). Columns are numbered sequentially in increasing order starting at 0. If this column is not included in the sequence, the value is -1.

## InitParam

Initialize the information regarding a given input parameter for a particular session.

This function is called for each parameter from `@params` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

### Syntax

```cpp
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

### Arguments

*SessionId*  
\[Input\] GUID uniquely identifying this script session.

*TaskId*  
\[Input\] n integer uniquely identifying this execution process.

When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

*ParamNumber*  
\[Input\] An integer identifying the index of this parameter. Parameters are numbered sequentially in increasing order starting at 0.

*ParamName*  
\[Input\] Null-terminated UTF-8 string containing the parameter's name.

*ParamNameLength*  
\[Input\] Length in bytes of *ParamName* (excluding the null termination character).

*DataType*  
\[Input\] The ODBC C type identifying this parameter's data type.

*ParamSize*  
\[Input\] The maximum size in bytes of the underlying data in this parameter.

For SQL_C_CHAR, SQL_C_WCHAR and SQL_C_BINARY data types, values larger than 8000 indicate this parameter represent LOBs object and with sizes up to 2 GB.

*DecimalDigits*  
\[Input\] The decimal digits of underlying data in this parameter, as defined by [Decimal Digits](../../odbc/reference/appendixes/decimal-digits.md).

*ParamValue*  
\[Input\] A pointer to a buffer containing the parameter's value.

*StrLen_or_Ind*  
\[Input\] An integer value indicating the length in bytes of *ParamValue*, or SQL_NULL_DATA to indicate that the data is NULL.

StrLen_or_Ind\[col\] can be ignored if a column is not nullable and doesn't represent one of the following data types: SQL_C_CHAR, SQL_C_WCHAR and SQL_C_BINARY, SQL_C_NUMERIC or SQL_C_TYPE_TIMESTAMP. Otherwise it points to a valid array with \[RowsNumber\] elements, where each element contains its length or null indicator data.

*InputOutputType*  
\[Input\] The type of the parameter. The *InputOutputType* argument is one of the following values:

- SQL_PARAM_INPUT
- SQL_PARAM_INPUT_OUTPUT

## Execute

Execute the `@script` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

This function may be called multiple times. Once for each steam chunk and for each partition in the `@input_data_1_partition_by_columns` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).


### Syntax

```cpp
SQLRETURN Execute(
    SQLGUID         SessionId,
    SQLUSMALLINT    TaskId,
    SQLULEN         RowsNumber,
    SQLPOINTER*     Data,
    SQLINTEGER**    StrLen_or_Ind,
    SQLUSMALLINT*   OutputSchemaColumnsNumber
);
```

### Arguments

*SessionId*  
\[Input\] GUID uniquely identifying this script session.

*TaskId*  
\[Input\] An integer uniquely identifying this execution process.

When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

*RowsNumber*  
\[Input\] The number of rows in the *Data*.

*Data*  
\[Input\] A two-dimensional array that contains the result set of `@input_data_1` n [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

The total number of columns is *InputSchemaColumnsNumber* that was received in the [InitSession](#initsession) call. Each column contains *RowsNumber* elements that should be interpreted according to the column type from [InitColumn](#initcolumn).

Elements indicated to be NULL in *StrLen_or_Ind* are not guaranteed to be valid and should be ignored.

*StrLen_or_Ind*  
\[Input\] A two-dimensional array that contains the length/NULL indicator for each value in *Data*. Possible values of each cell:

- n, where n > 0. Indicating the length of the data in bytes
- SQL_NULL_DATA, indicating a NULL value.

The total number of columns is *InputSchemaColumnsNumber* that was received in the [InitSession](#initsession) call. Each column contains *RowsNumber* elements that should be interpreted according to the column type from [InitColumn](#initcolumn).

StrLen_or_Ind\[col\] can be ignored, if one column is not nullable and doesn't represent one of the following data types: SQL_C_CHAR, SQL_C_WCHAR and SQL_C_BINARY, SQL_C_NUMERIC or SQL_C_TYPE_TIMESTAMP. Otherwise it points to a valid array with *RowsNumber* elements, each element contains its length or null indicator data.

*OutputSchemaColumnsNumber*  
\[Output\] Pointer to a buffer in which to return the number of columns in the expected result set of the `@script` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

## GetResultColumn

Retrieve the information regarding a given output column for a particular session.

This function is called for each column in the result set from `@script` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).
The column structure of this result set will be referred to as the 'output schema'.

### Syntax

```cpp
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

### Arguments

*SessionId*  
\[Input\] GUID uniquely identifying this script session.

*TaskId*  
\[Input\] An integer uniquely identifying this execution process.

When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

*ColumnNumber*  
\[Input\] An integer identifying the index of this column in the output schema. Columns are numbered sequentially in increasing order starting at 0.

*DataType*  
\[Output\] A pointer to the buffer that contains the ODBC C type identifying this column's data type.

*ColumnSize*  
\[Output\] A pointer to a buffer that contains the maximum size in bytes of the underlying data in this column.

*DecimalDigits*  
\[Output\] A pointer to a buffer that contains the decimal digits of underlying data in this column, as defined by [Decimal Digits](../../odbc/reference/appendixes/decimal-digits.md). If the number of decimal digits cannot be determined or is not applicable, the value is discarded.

*Nullable*  
\[Output\] A pointer to a buffer that contains a value, which indicates whether this column may contain NULL values. Possible values:

- SQL_NO_NULLS: The column cannot contain NULL values.
- SQL_NULLABLE: The column may contain NULL values.

If other values are passed, then execution stops.

## GetResults

Retrieve the result set from executing the `@script` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

This function may be called multiple times. Once for each steam chunk and for each partition in the `@input_data_1_partition_by_columns` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).


### Syntax

```cpp
SQLRETURN GetResults(
    SQLGUID         SessionId,
    SQLUSMALLINT    TaskId,
    SQLULEN*        RowsNumber,
    SQLPOINTER**    Data,
    SQLINTEGER***   StrLen_or_Ind
);
```

### Arguments

*SessionId*  
\[Input\] GUID uniquely identifying this script session.

*TaskId*  
\[Input\] An integer uniquely identifying this execution process.

When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

*RowsNumber*  
\[Output\] A pointer to a buffer that contains the number of rows in the *Data*.

*Data*  
\[Output\] A pointer to a two-dimensional array allocated by the extension that contains the result set of `@script` n [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

The total number of columns should be *OutputSchemaColumnsNumber* that was retrieved in the [Execute](#execute) call. Each column should contain *RowsNumber* elements that should be interpreted according to the column type from [GetResultColumn](#getresultcolumn).

*StrLen_or_Ind*  
\[Output\] A pointer to a two-dimensional array allocated by the extension that contains the length/NULL indicator for each value in *Data*. Possible values of each cell:

- n, where n > 0. Indicating the length of the data in bytes
- SQL_NULL_DATA, indicating a NULL value.

The total number of columns should be *OutputSchemaColumnsNumber* that was received in the [Execute](#execute) call. Each column contains *RowsNumber* elements that should be interpreted according to the column type from [GetResultColumn](#getresultcolumn).

StrLen_or_Ind\[col\] will be ignored, if one column is not nullable and doesn't represent one of the following data types: SQL_C_CHAR, SQL_C_WCHAR and SQL_C_BINARY [add dates]. Otherwise it points to a valid array with *RowsNumber* elements, each element contains its length or null indicator data.

## GetOutputParam

Retrieve the information regarding a given output parameter for a particular session.

This function is called for each parameter from `@params` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) marked with OUTPUT.

### Syntax

```cpp
SQLRETURN GetOutputParam(
    SQLGUID        SessionId,
    SQLUSMALLINT   ParamNumber,
    SQLPOINTER*    ParamValue,
    SQLINTEGER*    StrLen_or_Ind
);
```

### Arguments

*SessionId*  
\[Input\] GUID uniquely identifying this script session.

*ParamValue*  
\[Output\] A pointer to a buffer containing the parameter's value.

*StrLen_or_Ind* 
\[Output\] A pointer to a buffer that contains an integer value indicating the length in bytes of *ParamValue*, or SQL_NULL_DATA to indicate that the data is NULL.

## GetInterfaceVersion

Retrieve the interface version.
This function returns an integer representing the extension's interface version. 
Supported values:
1. Version 1 is the  initial API version. Supported at SQL Server 2019 RTM.
1. Version 2 has added support for InstallExternalLibrary and UninstallExternalLibrary API and is supported from SQL Server 2019 CU3.                            

### Syntax

```cpp
SQLUSMALLINT GetInterfaceVersion();
```

## CleanupSession

Clean up per-session information.

### Syntax

```cpp
SQLRETURN CleanupSession(
    SQLGUID        SessionId,
    SQLUSMALLINT   TaskId
);
```

### Arguments

*SessionId*  
\[Input\] GUID uniquely identifying this script session.

*TaskId*  
\[Input\] An integer uniquely identifying this execution process.

When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

## Cleanup

Clean up global, shared information (for example, JVM).

### Syntax

```cpp
SQLRETURN Cleanup();
```

## GetTelemetryResults

Retrieves telemetry (key-value pairs) data from the extension. The function is optional and doesn't require implementation. The telemetry is exposed by the `dm_db_external_script_execution_stats` dynamic management view (DMV).

There is a counter named script_executions, which is sent by the framework. The extension should not use this name.

Each telemetry entry is a key-value pair. The keys are strings, the values are 64-bit integers - counters. Thus, the output comprises for two logical arrays: the names and their corresponding counters. Each array is output.

The length of each array is *RowsNumber*, which is an output. The first logical output contains pointers to strings, thus, it's represented by two arrays: *CounterNames* (the actual string data) and *CounterNamesLength* (the length of each string). The second logical output is stored in the *CounterValues* pointer.

### Syntax

```cpp
SQLRETURN GetTelemetryResults(
    SQLGUID        SessionId,
    SQLUSMALLINT   TaskId,
    SQLUINTEGER    *RowsNumber,
    SQLCHAR        ***CounterNames,
    SQLINTEGER      **CounterNamesLength,
    SQLBIGINT       **CounterValues
);
```

### Arguments

*SessionId*  
\[Input\] GUID uniquely identifying this script session.

*TaskId*  
\[Input\] An integer uniquely identifying this execution process.

When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

*RowsNumber*  
\[Output\] The number of key-value pairs.

*CounterNames*  
\[Output\] The string data containing the keys.

*CounterNamesLength*  
\[Output\] The length of each key string.

*CounterValues*  
\[Output\] The 64-bit integer data containing the values.

## InstallExternalLibrary

Installs a library. The function is optional and doesn't require implementation. The default implementation is to copy the content of the library (see [CREATE EXTERNAL LIBRARY](../../t-sql/statements/create-external-library-transact-sql.md)) to a file at the proper location. The file name is library name.

### Syntax

```cpp
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

### Arguments

*SetupSessionId*  
\[Input\] GUID uniquely identifying this script session.

*LibraryName*  
\[Input\] The library name.

*LibraryNameLength*  
\[Input\] The length of the library name.

*LibraryFile*  
\[Input\] The path (as a string) to the library file containing the binary content specified by [CREATE EXTERNAL LIBRARY](../../t-sql/statements/create-external-library-transact-sql.md).

*LibraryFileLength*  
\[Input\] The length of the LibraryFile string.

*LibraryInstallDirectory:*  
\[Input\] The root directory to install the library.

*LibraryInstallDirectoryLength*  
\[Input\] The length of the LibraryInstallDirectory string.

*LibraryError*  
\[Output\] An optional output parameter. In case, there was an error during the installation of the library, LibraryError would point to a string describing the error.

*LibraryErrorLength*  
\[Output\] The length of the LibraryError string.

## UninstallExternalLibrary

Uninstalls a library. The function is optional and doesn't require implementation. The default implementation is to undo the work done by the default Implementation of InstallExternalLibrary. The default implementation deletes the content of the *LibraryName* file under *LibraryInstallDirectory*.

### Syntax

```cpp
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

### Arguments

*SetupSessionId*  
\[Input\] GUID uniquely identifying this script session.

*LibraryName*  
\[Input\] The library name

*LibraryNameLength*  
\[Input\] The length of the library name

*LibraryFile*  
\[Input\] The path (as a string) to the library file containing the binary content specified by CREATE EXTERNAL LIBRARY

*LibraryFileLength*  
\[Input\] The length of the LibraryFile string

*LibraryInstallDirectory*  
\[Input\] The root directory to install the library

*LibraryInstallDirectoryLength*  
\[Input\] The length of the LibraryInstallDirectory string.

*LibraryError*  
\[Output\] The library error string.

*LibraryErrorLength*  
\[Output\] The length of the LibraryError string.

## Next steps

- [Microsoft Extensibility SDK for Java for SQL Server](../how-to/extensibility-sdk-java-sql-server.md)
- [Python custom runtime](../../machine-learning/install/custom-runtime-python.md)
- [R custom runtime](../../machine-learning/install/custom-runtime-r.md).