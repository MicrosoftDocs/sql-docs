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

**PropertyBag:** The property bag defined for the extension when it was registered.
