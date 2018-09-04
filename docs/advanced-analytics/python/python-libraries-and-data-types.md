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

For more information, see [What Is revoscalepy?](what-is-revoscalepy.md).





