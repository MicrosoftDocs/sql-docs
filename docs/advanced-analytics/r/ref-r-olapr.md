---
title: olapR R function library in SQL Server Machine Learning | Microsoft Docs
description: Introduction to the olapR function library in SQL Server 2016 R Services and SQL Server 2017 Machine Learning Services with R.
ms.prod: sql
ms.technology: machine-learning

ms.date: 12/04/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# olapR (R library in SQL Server)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

**olapR** is a Microsoft library of R functions used for MDX queries against a SQL Server Analysis Services OLAP cube. Functions do not support all MDX operations, but you can build queries that slice, dice, drilldown, rollup, and pivot on dimensions. 

This package is not preloaded into an R session. Run the following command to load the library.

```r
library(olapR)
```

## Package version

Current version is 1.0.0 in all Windows-only products and downloads providing the library.

## Availability and location

This package is provided in the following products, as well as on several virtual machine images on Azure. Package location varies accordingly.

Product | Location |
--------|----------|
SQL Server 2017 Machine Learning Services (with R integration) | C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library | 
SQL Server 2016 R Services | C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\library
Microsoft Machine Learning Server (R Server) | C:\Program Files\Microsoft\R_SERVER\library |
Microsoft R Client | C:\Program Files\Microsoft\R Client\R_SERVER\library |
Data Science Virtual Machine (on Azure) | C:\Program Files\Microsoft\R Client\R_SERVER\library |
SQL Server Virtual Machine (on Azure) <sup>1</sup> | C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library |

<sup>1</sup> R integration is optional in SQL Server. The olapR library will be installed when you add the Machine Learning or R feature during VM configuration.

## Prerequisites

You can use this library on connections to an Analysis Services OLAP cube on all supported versions of SQL Server. Connections to a tabular model are not supported at this time.

## Reference documentation

Although this package is distributed in multiple products, the reference documentation is published to one location only, in the R reference section of the Microsoft Machine Learning Server product documentation site: [olapR function reference](https://docs.microsoft.com/machine-learning-server/r-reference/olapr/olapr).


## See Also

[R tutorials](../tutorials/sql-server-r-tutorials.md)