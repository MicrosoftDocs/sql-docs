---
title: "Python interoperability | Microsoft Docs"
ms.custom: ""
ms.date: "04/18/2017"
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
# Python Interoperability

This topic describes the Python components that are installed if you enable the feature **Machine Learning Services (In-Database)** and select Python as the lanaguage.

> [!NOTE]
> Support for Python is a prerelease feature and is still under development.

## Python Components

[!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] does not modify the Python executables. The Python runtime is installed independently of SQL tools, and is executed outside of the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] process.

The distribution that is associated with a specific [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] instance can be found in the folder associated with the instance.

For example, if you installed Machine Learning Services with the Python option on the default instance, look under:

`C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER`

Installation of SQL Server vNext Machine Learning Services adds the Anaconda distribution of Python. Specifically, the Anaconda 3 installers are used, based on the Anaconda 4.3 branch. The expected Python level for SQL Server vNext CTP 2.0 is Python 3.6.

To work with SQL Server, Python script requires that input data be tabular data. All Python results must be returned in the form of a **pandas** data frame.

## New in This Release

For a list of packages supported by the Anaconda distribution, see the Continuum analytics site: [Anaconda package list](https://docs.continuum.io/anaconda/pkg-docs)

Machine Learning Services in SQL Server vNext also includes the new **revoscalepy** library for Python.

This library provides functionality equivalent to that of the **RevoScaleR** package for Microsoft R. In other words, it supports creation of remote compute contexts, as well as a various scalable machine learning models, such as **rxLinMod**. For more information about RevoScaleR, see [Distributed and parallel computing with ScaleR](https://msdn.microsoft.com/microsoft-r/scaler-distributed-computing).

Because support for Python is a prerelease feature and under development, the **revscalepy** library currently includes a subset of the RevoScaleR functionality.

| Version| Supported functions |
| ------ | ------ |
| CTP 2.0 | rxLinMod, rxLogit, rxPredict, rxDTrees, rxBTrees|


You can call functions from **revoscalepy** from your Python code, like any other Python functions.
Use this library to create compute contexts that run in SQL Server, or access data sources such as .XDF files and SQL queries.

Additionally, support is planned for these popular machine learning libraries:

+ [Microsoft Cognitive Toolkit](https://www.microsoft.com/research/product/cognitive-toolkit/). Formerly known as CNTK, this library supports a variety of neural network models, including convolutional networks (CNN), recurrent networks (RNN), and Long Short Term Memory networks (LSTM).


### Licensing

As part of the installation of Machine Learning Services with Python, you must consent to the terms of the GNU Public License.

## See Also

[Python LIbraries and Data Types](python-libraries-and-data-types.md)