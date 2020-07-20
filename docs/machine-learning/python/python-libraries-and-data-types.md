---
title: Convert Python and SQL data types
description: Review the implicit and explicit data type conversions between Python and SQL Server in data science and machine learning solutions.
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 06/30/2020 
ms.topic: conceptual
author: dphansen
ms.author: davidph
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Data type mappings between Python and SQL Server
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

For Python solutions that run on the Python integration feature in SQL Server Machine Learning Services, review the list of unsupported data types, and data type conversions that might be performed implicitly when data is passed between Python and SQL Server.

## Python Version

A subset of the RevoScaleR functionality (rxLinMod, rxLogit, rxPredict, rxDTrees, rxBTrees, maybe a few others) is provided using Python APIs, using a new Python package **revoscalepy**. You can use this package to work with data using Pandas data frames, XDF files, or SQL data queries.

For more information, see [revoscalepy module in SQL Server](ref-py-revoscalepy.md) and [revoscalepy function reference](https://docs.microsoft.com/r-server/python-reference/revoscalepy/revoscalepy-package).

Python supports a limited number of data types in comparison to SQL Server. As a result, whenever you use data from SQL Server in Python scripts, data might be implicitly converted to a compatible data type. However, often an exact conversion cannot be performed automatically, and an error is returned.

## Python and SQL Data Types

This table lists the implicit conversions that are provided. Other data types are not supported.

|SQLtype|Python type|Description
|-------|-----------|---------------------------------------------------------------------------------------------|
|**bigint**|`float64`|
|**binary**|`bytes`|
|**bit**|`bool`|
|**char**|`str`|
|**date**|`datetime`|
|**datetime**|`datetime`|Supported with SQL Server 2017 CU6 and above (with **NumPy** arrays of type `datetime.datetime` or **Pandas** `pandas.Timestamp`). `sp_execute_external_script` now supports `datetime` types with fractional seconds.|
|**float**|`float64`|
|**int**|`int32`|
|**nchar**|`str`|
|**nvarchar**|`str`|
|**nvarchar(max)**|`str`|
|**real**|`float64`|
|**smalldatetime**|`datetime`|
|**smallint**|`int32`|
|**tinyint**|`int32`|
|**uniqueidentifier**|`str`|
|**varbinary**|`bytes`|
|**varbinary(max)**|`bytes`|
|**varchar(n)**|`str`|
|**varchar(max)**|`str`|
