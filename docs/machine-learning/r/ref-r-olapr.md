---
title: olapR R package
description: olapR is an R package from Microsoft used for MDX queries against a SQL Server Analysis Services OLAP cube. Functions do not support all MDX operations, but you can build queries that slice, dice, drilldown, rollup, and pivot on dimensions. The package is included in SQL Server Machine Learning Services and SQL Server 2016 R Services.
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 07/14/2020
ms.topic: how-to
author: dphansen
ms.author: davidph
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# olapR (R package in SQL Server Machine Learning Services)
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

**olapR** is an R package from Microsoft used for MDX queries against a SQL Server Analysis Services OLAP cube. Functions do not support all MDX operations, but you can build queries that slice, dice, drilldown, rollup, and pivot on dimensions. The package is included in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) and [SQL Server 2016 R Services](sql-server-r-services.md).

You can use this package on connections to an Analysis Services OLAP cube on all supported versions of SQL Server. Connections to a tabular model are not supported at this time.

## Load package

The **olapR** package is not preloaded into an R session. Run the following command to load the package.

```R
library(olapR)
```

## Package version

Current version is 1.0.0 in all Windows-only products and downloads providing the package.

## Full reference documentation

The **olapr** package is distributed in multiple Microsoft products, but usage is the same whether you get the package in SQL Server or another product. Because the functions are the same, [documentation for individual sqlrutils functions](https://docs.microsoft.com/machine-learning-server/r-reference/olapr/olapr) is published to just one location under the [R reference](https://docs.microsoft.com/machine-learning-server/r-reference/introducing-r-server-r-package-reference) for Microsoft Machine Learning Server. Should any product-specific behaviors exist, discrepancies will be noted in the function help page.

## Availability and location

This package is provided in the following products, as well as on several virtual machine images on Azure. Package location varies accordingly.

Product | Location |
--------|----------|
SQL Server Machine Learning Services (with R integration) | C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library | 
SQL Server 2016 R Services | C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\library
Microsoft Machine Learning Server (R Server) | C:\Program Files\Microsoft\R_SERVER\library |
Microsoft R Client | C:\Program Files\Microsoft\R Client\R_SERVER\library |
Data Science Virtual Machine (on Azure) | C:\Program Files\Microsoft\R Client\R_SERVER\library |
SQL Server Virtual Machine (on Azure) <sup>1</sup> | C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library |

<sup>1</sup> R integration is optional in SQL Server. The olapR package will be installed when you add the Machine Learning or R feature during VM configuration.


## See also

[How to create MDX queries using olapR](how-to-create-mdx-queries-using-olapr.md)
