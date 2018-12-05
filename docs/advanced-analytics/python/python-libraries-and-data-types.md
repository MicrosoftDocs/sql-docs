---
title: Python libraries and data types in SQL Server Machine Learning | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Python libraries and data types
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes the Python libraries that are included with SQL Server Machine Learning Services (In-Database) and (Standalone) installations.

This article also lists unsupported data types, and lists the data type conversions that might be performed implicitly when data is passed between Python and SQL Server.

## Python Version

SQL Server 2017 Anaconda 4.2 distribution and Python 3.6.

A subset of the RevoScaleR functionality (rxLinMod, rxLogit, rxPredict, rxDTrees, rxBTrees, maybe a few others) is provided using Python APIs, using a new Python package **revoscalepy**. You can use this package to work with data using Pandas data frames, XDF files, or SQL data queries.

For more information, see [revoscalepy module in SQL Server](ref-py-revoscalepy.md) and [revoscalepy function reference](https://docs.microsoft.com/r-server/python-reference/revoscalepy/revoscalepy-package).

Python supports a limited number of data types in comparison to SQL Server. As a result, whenever you use data from SQL Server in Python scripts, data might be implicitly converted to a compatible data type. However, often an exact conversion cannot be performed automatically, and an error is returned.

## Python and SQL Data Types

This table lists the implicit conversions that are provided. Other data types are not supported.

|SQLtype|Python type|
|-------|-----------|
|**bigint**|`numeric`|
|**binary**|`raw`|
|**bit**|`bool`|
|**char**|`str`|
|**float**|`float64`|
|**int**|`int32`|
|**nchar**|`str`|
|**nvarchar**|`str`|
|**nvarchar(max)**|`str`|
|**real**|`float32`|
|**smallint**|`int16`|
|**tinyint**|`uint8`|
|**varbinary**|`bytes`|
|**varbinary(max)**|`bytes`|
|**varchar(n)**|`str`|
|**varchar(max)**|`str`|



