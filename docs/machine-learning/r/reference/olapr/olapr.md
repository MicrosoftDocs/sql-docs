--- 

# required metadata 
title: "OlapR package for R (Microsoft R Server) " 
description: "Function help reference for the olapR R package of Microsoft R Server, used to import data from OLAP cubes stored in SQL Server Analysis Services into R." 
keywords: "olapR" 
author: "dphansen"
ms.author: "davidph" 
manager: "cgronlun" 
ms.date: 07/15/2019
ms.topic: "reference" 
ms.prod: "mlserver" 
ms.service: "" 
ms.assetid: "" 

# optional metadata 
#ROBOTS: "" 
#audience: "" 
#ms.devlang: "" 
#ms.reviewer: "" 
#ms.suite: "" 
#ms.tgt_pltfrm: "" 
#ms.technology: "" 
#ms.custom: "" 

--- 

# olapR package

The **olapR** library provides R functions for importing data from OLAP cubes stored in SQL Server Analysis Services into a data frame. This package is available on premises, on Windows only.

| Package details | Description |
|--------|-|
| Current version: |  1.0.0 |
| Built on: | R 3.4.3 |
| Package distribution: | [Machine Learning Server for Windows](/machine-learning-server/what-is-machine-learning-server.md) </br>[R Client (Windows)](/machine-learning-server/r-client/what-is-microsoft-r-client.md) <br/>[R Server 9.1](/machine-learning-server/what-is-microsoft-r-server.md)   <br/>[SQL Server 2017 Machine Learning Services (Windows only) and SQL Server 2016 R Services](/sql/machine-learning/sql-server-machine-learning-services) |


## How to use olapR

The **olapR** library provides a simple R style API for generating and validating MDX queries against an Analysis Services cube. **olapR** does not provide APIs for all MDX scenarios, but it does cover the most use cases including slice, dice, drilldown, rollup, and pivot scenarios in N dimensions. You can also input a direct MDX query to Analysis Services for queries that cannot be constructed using the olapR APIs.  

**Workflow for using olapR**

1. Load the library.
1. Create a connection string pointing to a MOLAP cube on Analysis Services. 
2. Verify you have read access on the cube
3. Use the connection string on a connection.
4. Verify the connection using the explore function.
5. Set up a query by submitting an MDX query string or by building a query structure.
6. Execute the query and verify the result.

To execute an MDX query on an OLAP Cube, you need to first create a connection string (`olapCnn`) and validate using the function `OlapConnection(connectionString)`. The connection string must have a Data Source (such as localhost) and a Provider (MSOLAP). 

After the connection is established, you can either pass in a fully defined MDX query, or you can construct the query using the `Query()` object, setting the query details using cube(), axis(), columns(), slicers(), and so forth. 

Finally, pass the `olapCnn` and query into either `executeMD` or `execute2D` to get a multidimensional array or a data frame back.

> [!Important]
> **olapR** requires the Analysis Services OLE DB provider. If you do not have SQL Server Analysis Services installed on your computer, download the provider from Microsoft:
>[Data providers used for Analysis Services connections](https://docs.microsoft.com/sql/analysis-services/instances/data-providers-used-for-analysis-services-connections)
>
>The exact version you should install for SQL Server 2016 is [here](https://download.microsoft.com/download/8/7/2/872BCECA-C849-4B40-8EBE-21D48CDF1456/ENU/x64/SQL_AS_OLEDB.msi).
>

## Function list

|Function | Description |
|---------|-------------|
|[`OlapConnection`](olapconnection.md) |Create the connection string to access the Analysis Services Database. |
|[`Query`](query.md) |Construct a Query object to use on the Analysis Services Database. Use cube, axis, columns, rows, pages, chapters, slicers to add details to the query.|
|[`executeMD`](executemd.md) |Takes a Query object or an MDX string, and returns the result as a multi-dimensional array. |
|[`execute2D`](execute2d.md)|Takes a Query object or an MDX string, and returns the result as a 2D data frame. |
|[`explore`](explore.md)|Allows for exploration of cube metadata. |

## MDX concepts

MDX is the query language for multidimensional OLAP (MOLAP) cubes containing processed and aggregated data stored in structures optimized for data analysis and exploration. Cubes are used in business and scientific applications to draw insights about relationships in historical data. Internally, cubes consist of mostly quantifiable numeric data, which is sliced along dimensions like date and time, geography, or other entities. A typical query might roll up sales for a given region and time period, sliced by product category, promotion, sales channel, and so forth.

 Cube data can be accessed using various operations:

* Slicing - Taking a subset of the cube by picking a value for one dimension, resulting in a cube that is one dimension smaller.

* Dicing - Creating a subcube by specifying a range of values on multiple dimensions.

* Drill-Down/Up - Navigate from more general to more detailed data ranges, or vice versa.

* Roll-up - Summarize the data on a dimension.

* Pivot - Rotate the cube.

MDX queries are similar to SQL queries but, because of the flexibility of OLAP databases, can contain up to 128 query axes. The first four axes are named for convenience: Columns, Rows, Pages, and Chapters. It's also common to just use two (Rows and Columns), as shown in the following example:

~~~~
SELECT {[Measures].[Internet Sales Count], [Measures].[Internet Sales-Sales Amount]} ON COLUMNS, 
{[Product].[Product Line].[Product Line].MEMBERS} ON ROWS
FROM [Analysis Services Tutorial]
WHERE [Sales Territory].[Sales Territory Country].[Australia]
~~~~

Using an AdventureWorks OLAP cube from the [multidimensional cube tutorial](https://docs.microsoft.com/sql/analysis-services/multidimensional-modeling-adventure-works-tutorial), this MDX query selects the internet sales count and sales amount and places them on the Column axis. On the Row axis it places all possible values of the "Product Line" dimension. Then, using the WHERE clause (which is the slicer axis in MDX queries), it filters the query so that only the sales from Australia matter. Without the slicer axis, we would roll up and summarize the sales from all countries.

 ## olapR examples

 ```
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

## Next steps

Learn more about MDX concepts:

+ OLAP Cubes: [https://en.wikipedia.org/wiki/OLAP_cube](https://en.wikipedia.org/wiki/OLAP_cube)

+ MDX queries: [https://en.wikipedia.org/wiki/MultiDimensional_eXpressions](https://en.wikipedia.org/wiki/MultiDimensional_eXpressions)

+ Create a demo OLAP Cube (identical to examples): [multidimensional cube tutorial](https://docs.microsoft.com/sql/analysis-services/multidimensional-modeling-adventure-works-tutorial)  

## See also

+ [Using data from OLAP cubes in R (SQL Server)](https://docs.microsoft.com/sql/advanced-analytics/r/using-data-from-olap-cubes-in-r)
+ [R Package Reference](../introducing-r-server-r-package-reference.md) 
+ [R Client](/machine-learning-server/r-client/what-is-microsoft-r-client.md) 
+ [R Server](/machine-learning-server/what-is-microsoft-r-server.md)

