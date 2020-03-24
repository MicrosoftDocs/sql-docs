---
title: Extensibility Framework API for Microsoft SQL Server
titleSuffix: SQL Server Language Extensions
description:  
author: dphansen
ms.author: davidph 
ms.date: 03/18/2020
ms.topic: reference
ms.prod: sql
ms.technology: language-extensions
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Extensibility Framework API for Microsoft SQL Server

## What is the Extensibility Framework API?

You can use the extensibility framework to write programming language extensions for SQL Server. The Extensibility Framework API for Microsoft SQL Server is an API that can be used by a language extension to interact with and exchange data with SQL Server. As a language extension author, you can use this reference together with the open sourced [Java language extension for SQL Server](../how-to/extensibility-sdk-java-sql-server.md) to understand how to use the API for writing your own language extensions.

You can find the source code for the Java Language Extension at [aka.ms/mssql-lang-extensions](https://aka.ms/mssql-lang-extensions).

## API Calls

### Return value for all the calls

All the functions return a **SQLRETURN** parameter. If the value is anything other than **SQL_SUCCESS**, the execution of the script stops.

### Init

This function is only called once and is used to initialize the runtime for execution. For example, the Java Extension initializes the JVM.

#### Syntax

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

#### Arguments

 *ExtensionParams*
 Null-terminated string containing *PARAMETERS* value provided during [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md) or [ALTER EXTERNAL LANGUAGE](../../t-sql/statements/alter-external-language-transact-sql.md).

 *ExtensionParamsLength*
 Length in bytes of *ExtensionParams* (excluding the null termination character).

 *ExtensionPath*
 Null-terminated UTF-8 string containing the absolute path to the installation directory of the extension.

 *ExtensionPathLength*
 Length in bytes of *ExtensionPath* (excluding the null termination character).

 *PublicLibraryPath*
 Null-terminated UTF-8 string containing the absolute path to the public external libraries directory for this external language.

 *PublicLibraryPathLength*
 Length in bytes of *PublicLibraryPath* (excluding the null termination character).

 *PrivateLibraryPath*
 Null-terminated UTF-8 string containing the absolute path to the private external libraries directory for this user and this external language.

 *PrivateLibraryPathLength*
 Length in bytes of *PrivateLibraryPath* (excluding the null termination character).

### InitSession

This function is called once per session and initializing session specific settings.

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

- **SessionId:** GUID uniquely identifying this script session.
- **TaskId:** An integer uniquely identifying this execution process. 

    When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

- **Script:** Null-terminated UTF-8 string containing the `@script` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).
- **ScriptLength:** Length in bytes of **ScriptScript** (excluding the null termination character).
- **InputSchemaColumnsNumber:** Number of columns in the result set from `@input_data_1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).
- **ParametersNumber:** Number of input parameters from `@params` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).
- **InputDataName:** Null-terminated UTF-8 string containing the `@input_data_1_name` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).
- **InputDataNameLength:** Length in bytes of **InputDataName** (excluding the null termination character).
- **OutputDataName:** Null-terminated UTF-8 string containing the `@output_data_1_name` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).
- **OutputDataNameLength:** Length in bytes of **OutputDataName** (excluding the null termination character).

### InitColumn

Initialize the information for a given column for a particular session.

This function is called for each column in the result set from `@input_data_1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).
The column structure of this result set will be referred to as the 'input schema'.

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
- **SessionId:** GUID uniquely identifying this script session.
- **TaskId:** An integer uniquely identifying this execution process. 

    When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

- **ColumnNumber:** An integer identifying the index of this column in the input schema. Columns are numbered sequentially in increasing order starting at 0.
- **ColumnName:** Null-terminated UTF-8 string containing the column's name.
- **ColumnNameLength:** Length in bytes of **ColumnName** (excluding the null termination character).
- **DataType:** The ODBC C type identifying this column's data type.
- **ColumnSize:** The maximum size in bytes of the underlying data in this column.
For SQL_C_CHAR, SQL_C_WCHAR and SQL_C_BINARY data types, values larger than 8000 indicate this column represent LOBs object and with sizes up to 2GB.
- **DecimalDigits:** The decimal digits of underlying data in this column, as defined by [https://docs.microsoft.com/en-us/sql/odbc/reference/appendixes/decimal-digits].
- **Nullable:** A value that indicates whether this column may contain NULL values. Possible values:
    - SQL_NO_NULLS: The column cannot contain NULL values.
    - SQL_NULLABLE: The column may contain NULL values.
- **PartitionByNumber:** A value that indicates the index of this column in the `@input_data_1_partition_by_columns` [todo: David add link] sequence. Columns are numbered sequentially in increasing order starting at 0. If this column is not included in the sequence, the value is -1.
- **OrderByNumber:** A value that indicates the index of this column in the `@input_data_1_order_by_columns` [todo: David add link] sequence. Columns are numbered sequentially in increasing order starting at 0. If this column is not included in the sequence, the value is -1.

### InitParam

Initialize the information regarding a given input parameter for a particular session.

This function is called for each parameter from `@params` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

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
- **SessionId:** GUID uniquely identifying this script session.
- **TaskId:** An integer uniquely identifying this execution process. 

    When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

- **ParamNumber:** An integer identifying the index of this parameter. Parameters are numbered sequentially in increasing order starting at 0.
- **ParamName:** Null-terminated UTF-8 string containing the parameter's name.
- **ParamNameLength:** Length in bytes of **ParamName** (excluding the null termination character).
- **DataType:** The ODBC C type identifying this parameter's data type.
- **ParamSize:** The maximum size in bytes of the underlying data in this parameter.
For SQL_C_CHAR, SQL_C_WCHAR and SQL_C_BINARY data types, values larger than 8000 indicate this parameter represent LOBs object and with sizes up to 2GB.
- **DecimalDigits:** The decimal digits of underlying data in this parameter, as defined by [https://docs.microsoft.com/en-us/sql/odbc/reference/appendixes/decimal-digits].
- **ParamValue:** A pointer to a buffer containing the parameter's value.
- **StrLen_or_Ind:** An integer value indicating the length in bytes of **ParamValue**, or SQL_NULL_DATA which indicates that the data is NULL.
- **InputOutputType:** The type of the parameter. The **InputOutputType** argument is one of the following values:
    - SQL_PARAM_INPUT
    - SQL_PARAM_INPUT_OUTPUT

### Execute

Execute the `@script` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

This function may be called multiple times. Once for each steam chunk and for each partition in the `@input_data_1_partition_by_columns` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).
[todo: UC to expand]

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

- **SessionId:** GUID uniquely identifying this script session.
- **TaskId:** An integer uniquely identifying this execution process. 

    When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

- **RowsNumber:** Number of rows in the input dataset passes in Data.
- **Data:** A 2D array of the input dataset. The length of the array is \[InputSchemaColumnsNumber\] (received in the InitSession call). Each column's array has \[RowsNumber\] elements that should be interpreted according to the column type (from InitColumn). Each such element is the value of row \[i\] of column\[j\].
- **StrLen_or_Ind:** A 2D array the size of the input data that represents the length or null indicator value. Possible values of each cell:
    - n, where n > 0. Indicating the length of the data in bytes
    - SQL_NULL_DATA

    The length of the array is \[InputSchemaColumnsNumber\] (received in the InitSession call). Each column's array has \[RowsNumber\] elements that should be interpreted according to the column type (from InitColumn).

    If column col is not nullable and represents a data type of fixed size, StrLen_or_Ind\[col\] is a null pointer. Otherwise it points to a valid array with \[RowsNumber\] elements, and for each element it contains its length or null indicator data.

### GetResultColumn

Get the information regarding a given column in the output dataset for a particular session:

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
- **SessionId:** GUID uniquely identifying this script session.
- **TaskId:** An integer uniquely identifying this execution process. 

    When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

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

```cpp
SQLRETURN GetResults(
    SQLGUID         SessionId,
    SQLUSMALLINT    TaskId,
    SQLULEN*        RowsNumber,
    SQLPOINTER**    Data,
    SQLINTEGER***   StrLen_or_Ind
);
```
- **SessionId:** GUID uniquely identifying this script session.
- **TaskId:** An integer uniquely identifying this execution process. 

    When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

- **RowsNumber:** \[Output\] Number of rows in the output dataset being passes in Data.
- **Data:** \[Output\] A 2D array of the output dataset allocated by the extension. The length of the array is \[OutputSchemaColumnsNumber\] (known from the `Execute()` call). Each column's array should have \[RowsNumber\] elements that should be interpreted according to the column type (known from `GetResultColumn()`). Each such element is the value of row \[i\] of column\[j\]. 
- **StrLen_or_Ind:** \[Output\] A 2D array the size of the output data that represents the length or null indicator value. Possible values of each cell: 
    - n, where n > 0. Indicating the length of the data in bytes 
    - SQL_NULL_DATA 
 
    The length of the array is \[OutputSchemaColumnsNumber\] (known from the `Execute()` call). Each column's array has \[RowsNumber\] elements that should be interpreted according to the column type (known from GetResultColumn()). 

    If column col is not nullable and represents a data type of fixed size, StrLen_or_Ind\[col\] is ignored.

### GetOutputParam

Get output parameters at the end of the script.

```cpp
SQLRETURN GetOutputParam(
    SQLGUID        SessionId,
    SQLUSMALLINT   ParamNumber,
    SQLPOINTER*    ParamValue,
    SQLINTEGER*    StrLen_or_Ind
);
```
- **SessionId:** GUID uniquely identifying this script session.
- **ParamValue:** \[Output\] The value of the output parameter

- **StrLen_or_Ind:** \[Output\] The length or indicator value. Possible values:
    - n, where n > 0. Indicating the length of the data in bytes.
    - SQL_NULL_DATA 

### CleanupSession

Clean up per-session information.

```cpp
SQLRETURN CleanupSession(
    SQLGUID        SessionId,
    SQLUSMALLINT   TaskId
);
```
- **SessionId:** GUID uniquely identifying this script session.
- **TaskId:** An integer uniquely identifying this execution process. 

    When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

### Cleanup

Clean up global, shared information (e.g. jvm).

```cpp
SQLRETURN Cleanup();
```

### GetTelemetryResults

Retrieves telemetry (key-value pairs) data from the extension. The function is optional and doesn't require implementation. The telemetry is exposed by the `dm_db_external_script_execution_stats` dynamic management view (DMV).

There is a counter named script_executions which is sent by the framework. The extension should not use this name.

Each telemetry entry is a key-value pair. The keys are strings, the values are 64-bit integers - counters. Thus, the output comprises for two logical arrays: the names and their corresponding counters. Each array is output.

The length of each array is **RowsNumber**, which is an output. The first logical output contains pointers to strings, thus, it's represented by two arrays: **CounterNames** (the actual string data) and **CounterNamesLength** (the length of each string). The second logical output is stored in the **CounterValues** pointer. 

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
- **SessionId:** GUID uniquely identifying this script session.
- **TaskId:** An integer uniquely identifying this execution process. 

    When `@parallel = 1` in [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), this value ranges from 0 to the degree of parallelism of the query.

- **RowsNumber:** \[Output\] The number of key-value pairs.
- **CounterNames:** \[Output\] The string data containing the keys.
- **CounterNamesLength:** \[Output\] The length of each key string.
- **CounterValues:** \[Output\] The 64-bit integer data containing the values.

### InstallExternalLibrary

Installs a library. The function is optional and doesn't require implementation. The default implementation is to copy the content of the library (see [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql)) to a file at the proper location. The file name is library name.

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
- **SetupSessionId:** GUID uniquely identifying this script session.
- **LibraryName:** \[Input\] The library name.
- **LibraryNameLength:** \[Input\] The length of the library name.
- **LibraryFile:** \[Input\] The path (as a string) to the library file containing the binary content specified by [CREATE EXTERNAL LIBRARY](https://docs.microsoft.com/sql/t-sql/statements/create-external-library-transact-sql).
- **LibraryFileLength:** \[Input\] The length of the LibraryFile string.
- **LibraryInstallDirectory:** \[Input\] The root directory to install the library.
- **LibraryInstallDirectoryLength:** \[Input\] The length of the LibraryInstallDirectory string.
- **LibraryError:** \[Output\] An optional output parameter. In case there was an error during the installation of the library, LibraryError would point to a string describing the error.
- **LibraryErrorLength:** \[Output\] The length of the LibraryError string.

### UninstallLibrary

Uninstalls a library. The function is optional and doesn't require implementation. The default implementation is to undo the work done by the default Implementation of InstallExternalLibrary. The default implementation deletes the content of the **LibraryName** file under **LibraryInstallDirectory**.

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
- **SetupSessionId:** GUID uniquely identifying this script session.
- **LibraryName:** \[Input\] The library name
- **LibraryNameLength:** \[Input\] The length of the library name
- **LibraryFile:** \[Input\] The path (as a string) to the library file containing the binary content specified by CREATE EXTERNAL LIBRARY
- **LibraryFileLength:** \[Input\] The length of the LibraryFile string
- **LibraryInstallDirectory:** \[Input\] The root directory to install the library
- **LibraryInstallDirectoryLength:** \[Input\] The length of the LibraryInstallDirectory string.
- **LibraryError:** \[Output\] The library error string.
- **LibraryErrorLength:** \[Output\] The length of the LibraryError string.

## Next steps

- [Microsoft Extensibility SDK for Java for SQL Server](../how-to/extensibility-sdk-java-sql-server.md) 
