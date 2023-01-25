---
title: olapR R package
description: olapR is an R package from Microsoft used for MDX queries against a SQL Server Analysis Services OLAP cube. Functions do not support all MDX operations, but you can build queries that slice, dice, drilldown, rollup, and pivot on dimensions. The package is included in SQL Server Machine Learning Services and SQL Server 2016 R Services.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 09/30/2021
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# olapR (R package in SQL Server Machine Learning Services)
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

**olapR** is an R package from Microsoft used for MDX queries against a SQL Server Analysis Services OLAP cube. Functions do not support all MDX operations, but you can build queries that slice, dice, drilldown, rollup, and pivot on dimensions. The package is included in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) and [SQL Server 2016 R Services](sql-server-r-services.md).

You can use this package on connections to an Analysis Services OLAP cube on all supported versions of SQL Server. Connections to a tabular model are not supported at this time.

## Load package

The **olapR** package is not preloaded into an R session. Run the following command to load the package.

```R
library(olapR)
```

## Package version

Current version is 1.0.0 in all Windows-only products and downloads providing the package.

## Availability and location

This package is provided in the following products, as well as on several virtual machine images on Azure. Package location varies accordingly.

| Product                                                   | Location                                                                     |
|-----------------------------------------------------------|------------------------------------------------------------------------------|
| SQL Server Machine Learning Services (with R integration) | C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library |
| SQL Server 2016 R Services                                | C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\R_SERVICES\library |
| Microsoft Machine Learning Server (R Server)              | C:\Program Files\Microsoft\R_SERVER\library                                  |
| Microsoft R Client                                        | C:\Program Files\Microsoft\R Client\R_SERVER\library                         |
| Data Science Virtual Machine (on Azure)                   | C:\Program Files\Microsoft\R Client\R_SERVER\library                         |
| SQL Server Virtual Machine (on Azure) <sup>1</sup>        | C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library |

<sup>1</sup> R integration is optional in SQL Server. The olapR package will be installed when you add the Machine Learning or R feature during VM configuration.

## How to use olapR

The **olapR** library provides a simple R style API for generating and validating MDX queries against an Analysis Services cube. **olapR** does not provide APIs for all MDX scenarios, but it does cover the most use cases including slice, dice, drilldown, rollup, and pivot scenarios in N dimensions. You can also input a direct MDX query to Analysis Services for queries that cannot be constructed using the olapR APIs.  

**Workflow for using olapR**

1. Load the library.
1. Create a connection string pointing to a MOLAP cube on Analysis Services. 
1. Verify you have read access on the cube
1. Use the connection string on a connection.
1. Verify the connection using the explore function.
1. Set up a query by submitting an MDX query string or by building a query structure.
1. Execute the query and verify the result.

To execute an MDX query on an OLAP Cube, you need to first create a connection string (`olapCnn`) and validate using the function `OlapConnection(connectionString)`. The connection string must have a Data Source (such as localhost) and a Provider (MSOLAP). 

After the connection is established, you can either pass in a fully defined MDX query, or you can construct the query using the `Query()` object, setting the query details using cube(), axis(), columns(), slicers(), and so forth. 

Finally, pass the `olapCnn` and query into either `executeMD` or `execute2D` to get a multidimensional array or a data frame back.

> [!Important]
> **olapR** requires the Analysis Services OLE DB provider. If you do not have SQL Server Analysis Services installed on your computer, download the provider from Microsoft:
>[Data providers used for Analysis Services connections](/analysis-services/client-libraries)
>
>The exact version you should install for SQL Server 2016 is [here](https://download.microsoft.com/download/9/2/B/92BAD988-00C5-4F68-811E-B7FFBE009B00/SQLServer2016SP2-KB4052908-x64-ENU.exe).
>

## Function list

|Function | Description |
|---------|-------------|
|[`OlapConnection`](reference/olapr/olapconnection.md) |Create the connection string to access the Analysis Services Database. |
|[`Query`](reference/olapr/query.md) |Construct a Query object to use on the Analysis Services Database. Use cube, axis, columns, rows, pages, chapters, slicers to add details to the query.|
|[`executeMD`](reference/olapr/executemd.md) |Takes a Query object or an MDX string, and returns the result as a multi-dimensional array. |
|[`execute2D`](reference/olapr/execute2d.md)|Takes a Query object or an MDX string, and returns the result as a 2D data frame. |
|[`explore`](reference/olapr/explore.md)|Allows for exploration of cube metadata. |

## MDX concepts

MDX is the query language for multidimensional OLAP (MOLAP) cubes containing processed and aggregated data stored in structures optimized for data analysis and exploration. Cubes are used in business and scientific applications to draw insights about relationships in historical data. Internally, cubes consist of mostly quantifiable numeric data, which is sliced along dimensions like date and time, geography, or other entities. A typical query might roll up sales for a given region and time period, sliced by product category, promotion, sales channel, and so forth.

Cube data can be accessed using various operations:

* Slicing - Taking a subset of the cube by picking a value for one dimension, resulting in a cube that is one dimension smaller.

* Dicing - Creating a subcube by specifying a range of values on multiple dimensions.

* Drill-Down/Up - Navigate from more general to more detailed data ranges, or vice versa.

* Roll-up - Summarize the data on a dimension.

* Pivot - Rotate the cube.

MDX queries are similar to SQL queries but, because of the flexibility of OLAP databases, can contain up to 128 query axes. The first four axes are named for convenience: Columns, Rows, Pages, and Chapters. It's also common to just use two (Rows and Columns), as shown in the following example:

```sql
SELECT {[Measures].[Internet Sales Count], [Measures].[Internet Sales-Sales Amount]} ON COLUMNS, 
{[Product].[Product Line].[Product Line].MEMBERS} ON ROWS
FROM [Analysis Services Tutorial]
WHERE [Sales Territory].[Sales Territory Country].[Australia]
```

Using an AdventureWorks OLAP cube from the [multidimensional cube tutorial](/analysis-services/multidimensional-tutorial/multidimensional-modeling-adventure-works-tutorial), this MDX query selects the internet sales count and sales amount and places them on the Column axis. On the Row axis it places all possible values of the "Product Line" dimension. Then, using the WHERE clause (which is the slicer axis in MDX queries), it filters the query so that only the sales from Australia matter. Without the slicer axis, we would roll up and summarize the sales from all countries/regions.

 ## olapR examples

 ```R
# load the library
library(olapR)

# Connect to a local SSAS default instance and the Analysis Services Tutorial database.
# For named instances, use server-name\\instancename, escaping the instance name delimiter.
# For databases containing multiple cubes, use the cube= parameter to specify which one to use.
cnnstr <- "Data Source=localhost; Provider=MSOLAP; initial catalog=Analysis Services Tutorial"
olapCnn <- OlapConnection(cnnstr)

# Approach 1 - build the mdx query in R
qry <- Query()

cube(qry) <- "[Analysis Services Tutorial]"
columns(qry) <- c("[Measures].[Internet Sales Count]", "[Measures].[Internet Sales-Sales Amount]")
rows(qry) <- c("[Product].[Product Line].[Product Line].MEMBERS") 
slicers(qry) <- c("[Sales Territory].[Sales Territory Country].[Australia]")

result1 <- executeMD(olapCnn, qry)

# Approach 2 - Submit a fully formed MDX query
mdx <- "SELECT {[Measures].[Internet Sales Count], [Measures].[Internet Sales-Sales Amount]} ON AXIS(0), {[Product].[Product Line].[Product Line].MEMBERS} ON AXIS(1) FROM [Analysis Services Tutorial] WHERE [Sales Territory].[Sales Territory Country].[Australia]"

result2 <- execute2D(olapCnn, mdx)
```

## See also

[How to create MDX queries using olapR](how-to-create-mdx-queries-using-olapr.md)
