---
title: "Python interoperability | Microsoft Docs"
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

# Python Interoperability

This topic describes the Python components that are installed if you enable the feature **Machine Learning Services (In-Database)** and select Python as the lanaguage.

For information about how Python interacts with SQL Server, see these topics:

LINK
LINK
LINK

## Python Components

[!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] does not modify the Python executables. The Python runtime is installed independently of SQL tools, and is executed outside of the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] process. 

The distribution that is associated with a specific [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] instance can be found in the folder associated with the instance. 

For example, if you installed Machine Learning Services with the Python option on the default instance, look under:

`C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER`

You  and can be run independently of [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)]. 

[!INCLUDE[rsql_productname_md](../../includes/rsql-productname-md.md)] includes a full Anaconda distribution. Specifically, the Anaconda 3 installers are used, based on the Anaconda 4.3 branch. The expected Python level for SQL Server vNext CTP 2.0 is Python 3.6. 

To work with SQL Server, Python script is expected to intake tabular data and return Pandas data frames.

## RevoScalePy

Machine Learning Services in SQL Server vNext also includes the new RevoScalePy Python package.

RevoScalePy is the Python counterpart to the RevoScaleR package for the R language. It supports creation of machine learning models such as rxLinMod and rxLogit.

The library includes a subset of the RevoScaleR functionality. 

| Version| Supported functions |
| ------ | ------ |
| CTP 2.0 | rxLinMod, rxLogit, rxPredict, rxDTrees, rxBTrees|

The RevoScalePy functions are unique to Machine Learning Services in that they can use multiple data sources: including .XDF files and SQL queries.

When you install Machine Learning Services with SQL Server vNext, you can add either R or Python languages, or both.

You can call functions from RevoScalePy from your Python code like any other Python functions.

### Licensing

As part of the installation of Machine Learning Services with Python, you must consent to the terms of the GNU Public License. 


### Additional Libraries and Tools

For a list of packages supported by the Anaconda distribution, see the Continuum analytics site: [Anaconda package list](https://docs.continuum.io/anaconda/pkg-docs)

Additionally, support is planned for these popular machine learning libraries:

+ [Microsoft Cognitive Toolkit](https://www.microsoft.com/research/product/cognitive-toolkit/). Formerly known as CNTK, this library supports a variety of neural network models, including convolutional networks (CNN), recurrent networks (RNN), and Long Short Term Memory networks (LSTM). 

## See Also

