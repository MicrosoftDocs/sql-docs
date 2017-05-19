---
title: "Python Libraries | Microsoft Docs"
ms.custom: ""
ms.date: "03/30/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Python Libraries and Data Types

This topic describes the Python libraries that are included with the following products:

+ SQL Server Machine Learning Services (In-Database)
+ Microsoft Machine Learning Server (Standalone)

This topic also lists unsupported data types, and lists the data type conversions that might be performed implicitly when data is passed between Python and SQL Server.

## Python Version

SQL Server 2017 CTP 2.0 includes a portion of the Anaconda distribution and Python 3.6.

A subset of the RevoScaleR functionality (rxLinMod, rxLogit, rxPredict, rxDTrees, rxBTrees, maybe a few others) is provided using Python APIS, using a new Python package **RevoScalePy**. You can use this packages to work with data using Pandas data frames, .XDF files, or SQL data queries.

For more information, see [What Is revoscalepy?](what-is-revoscalepy.md).

## Python and SQL Data Types

Python supports a limited number of data types in comparison to SQL Server.

As a result, whenever you use data from  [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] in Python scripts, data might be implicitly converted to a compatible data type. However, often an exact conversion cannot be performed automatically, and an error is returned.

This table lists the implicit conversions that are provided. Other data types are not supported.

|SQLtype|Python type|
|-|-|
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



