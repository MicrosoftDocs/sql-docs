---
title: microsoftml Python package in SQL Server Machine Learning | Microsoft Docs
description: Introduces the Microsoft machine learning algorithms and models for Python, as related to SQL Server machine learning workloads.
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/04/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# microsoftml Python module in SQL Server Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

**microsoftml** is a Python35-compatible module from Microsoft that provides machine learning algorithms. It is included in several products, including SQL Server 2017 Machine Learning Services (with Python), Microsoft Machine Learning Server, the Python client libraries, and several virtual machine images on Azure. 

The machine learning APIs were developed by Microsoft for internal machine learning applications, and have been refined over the years to support high performance on big data, using multicore processing and fast data streaming. MicrosoftML also includes numerous transformations for text and image processing.

There is an R-equivalent version of this package, [MicrosoftML](../r/ref-r-microsoftml.md), containing equivalent functions. 

## Package dependencies

Algorithms in **microsoftml** are used with data source objects based on **revoscalepy** functions. Whenever you use **microsoftml**, you almost always have to make **revoscalepy** in the same script to load the data.

+ [revoscalepy module in SQL Server](python/ref-py-revoscalepy.md)
+ [revoscalepy function reference](https://docs.microsoft.com/r-server/python-reference/revoscalepy/revoscalepy-package) 

## How to call microsoftml in SQL Server

The **microsoftml** package for Python is installed by default, but not loaded by default when you start a Python session using the Python executables installed with SQL Server. 

As a first step, import the **microsoftml** package, and import **revoscalepy** if you need to use remote compute contexts or related connectivity or data source objects. Then, reference the individual functions you need.

```Python
from microsoftml.modules.logistic_regression.rx_logistic_regression import rx_logistic_regression
from revoscalepy.functions.RxSummary import rx_summary
from revoscalepy.etl.RxImport import rx_import_datasource
```

## See Also

[Python tutorials](../tutorials/sql-server-python-tutorials.md)
