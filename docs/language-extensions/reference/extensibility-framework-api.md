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
- **InputDataNameLength & OutputDataNameLength:** Length of *InputDataName and *OutputDataName in bytes, respectively ([excluding the null-termination character](https://docs.microsoft.com/en-us/sql/odbc/reference/syntax/sqldescribecol-function)). These parameters are N/A for pre-compiled languages.

