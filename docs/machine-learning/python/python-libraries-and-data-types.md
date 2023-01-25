---
title: Convert Python and SQL data types
description: Review the implicit and explicit data type conversions between Python and SQL Server in data science and machine learning solutions.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 10/06/2020 
ms.topic: conceptual
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current"
---
# Data type mappings between Python and SQL Server
[!INCLUDE [SQL Server 2017 SQL MI](../../includes/applies-to-version/sqlserver2017-asdbmi.md)]

This article lists the supported data types, and the data type conversions performed, when using the Python integration feature in SQL Server Machine Learning Services.

Python supports a limited number of data types in comparison to SQL Server. As a result, whenever you use data from SQL Server in Python scripts, SQL data might be implicitly converted to a compatible Python data type. However, often an exact conversion cannot be performed automatically and an error is returned.

## Python and SQL Data Types

This table lists the implicit conversions that are provided. Other data types are not supported.

| SQL type             | Python type | Description |
|----------------------|-------------|-------------|
| **bigint**           | `float64`   |
| **binary**           | `bytes`     |
| **bit**              | `bool`      |
| **char**             | `str`       |
| **date**             | `datetime`  |
| **datetime**         |`datetime`   | Supported with SQL Server 2017 CU6 and above (with **NumPy** arrays of type `datetime.datetime` or **Pandas** `pandas.Timestamp`). `sp_execute_external_script` now supports `datetime` types with fractional seconds.|
| **float**            | `float64`   |
| **nchar**            | `str`       |
| **nvarchar**         | `str`       |
| **nvarchar(max)**    | `str`       |
| **real**             | `float64`   |
| **smalldatetime**    | `datetime`  |
| **smallint**         | `int32`     |
| **tinyint**          | `int32`     |
| **uniqueidentifier** | `str`       |
| **varbinary**        | `bytes`     |
| **varbinary(max)**   | `bytes`     |
| **varchar(n)**       | `str`       |
| **varchar(max)**     | `str`       |

## See also

+ [Data type mappings between R and SQL Server](../r/r-libraries-and-data-types.md)
