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

This topic describes the Python components that are installed if you enable the feature **Machine Learning Services (In-Database)** and select Python as the language.

> [!NOTE]
> Support for Python is a pre-release feature and is still under development.

## Python Components

[!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] does not modify the Python executables. The Python runtime is installed independently of SQL tools, and is executed outside of the [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] process.

The distribution that is associated with a specific [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] instance can be found in the folder associated with the instance.

For example, if you installed Machine Learning Services with the Python option on the default instance, look under:

`C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER`

Installation of SQL Server 2017 Machine Learning Services adds the Anaconda distribution of Python. Specifically, the Anaconda 3 installers are used, based on the Anaconda 4.3 branch. The expected Python level for SQL Server 2017 is Python 3.5.

## New in This Release

For a list of packages supported by the Anaconda distribution, see the Continuum analytics site: [Anaconda package list](https://docs.continuum.io/anaconda/pkg-docs)

Machine Learning Services in SQL Server 2017 also includes the new **revoscalepy** library for Python.

This library provides functionality equivalent to that of the **RevoScaleR** package for Microsoft R. In other words, it supports creation of remote compute contexts, as well as a various scalable machine learning models, such as **rxLinMod**. For more information about RevoScaleR, see [Distributed and parallel computing with ScaleR](https://msdn.microsoft.com/microsoft-r/scaler-distributed-computing).

Because support for Python is a pre-release feature and still under development, the **revoscalepy** library currently includes only a subset of the RevoScaleR functionality. 

Future additions might include the [Microsoft Cognitive Toolkit](https://www.microsoft.com/research/product/cognitive-toolkit/). Formerly known as CNTK, this library supports a variety of neural network models, including convolutional networks (CNN), recurrent networks (RNN), and Long Short Term Memory networks (LSTM).

## Using Python in SQL Server

You import the **revoscalepy** module into your Python code, and then call functions from the module, like any other Python functions.

Input data for Python must be tabular. All Python results must be returned in the form of a **pandas** data frame.

You can execute your Python code inside T-SQL, by embedding the script in a stored procedure.

Or, run the code from a local Python IDE and have the script executed on the SQL Server computer, by defining a remote compute context.

You can work with local data, get data from SQL Server or other ODBC data sources, or use the XDF file format to exchange data with other sources, or with R solutions.

**For more information**

+ Supported functions:  [What is revoscalepy](what-is-revoscalepy.md) 
+ Supported Python data types:  [Python Libraries and Data Types](python-libraries-and-data-types.md)
+ Supported data sources: ODBC databases, SQL Server, and XDF files
+ Supported compute contexts: local, or SQL Server

### Licensing

As part of the installation of Machine Learning Services with Python, you must consent to the terms of the GNU Public License.

## See Also

[Python Libraries and Data Types](python-libraries-and-data-types.md)