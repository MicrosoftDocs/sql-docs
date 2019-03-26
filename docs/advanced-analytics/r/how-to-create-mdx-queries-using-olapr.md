---
title: How to create MDX queries in R using olapR - SQL Server Machine Learning Services
description: Use the olapR package library in SQL Server to write MDX queries in R language script.
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# How to create MDX queries in R using olapR
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

The [olapR](https://docs.microsoft.com/machine-learning-server/r-reference/olapr/olapr) package supports MDX queries against cubes hosted in SQL Server Analysis Services. You can build a query against an existing cube, explore dimensions and other cube objects, and paste in existing MDX queries to retrieve data.

This article describes the two main uses of the **olapR** package:

+ [Build an MDX query from R, using the constructors provided in the olapR package](#buildMDX)
+ [Execute an existing, valid MDX query using olapR and an OLAP provider](#executeMDX)

The following operations are not supported:

+ DAX queries against a tabular model
+ Creation of new OLAP objects
+ Writeback to partitions, including measures or sums

## <a name="buildMDX"></a> Build an MDX query from R

1. Define a connection string that specifies the OLAP data source (SSAS instance), and the MSOLAP provider.

2. Use the function `OlapConnection(connectionString)` to create a handle for the MDX query and pass the connection string.

3. Use the `Query()` constructor to instantiate a query object.

4. Use the following helper functions to provide more details about the dimensions and measures to include in the MDX query:

     + `cube()` Specify the name of the SSAS database. If connecting to a named instance, provide the machine name and instance name. 
     + `columns()` Provide the names of the measures to use in the **ON COLUMNS** argument.
     + `rows()` Provide the names of the measures to use in the **ON ROWS** argument.
     + `slicers()` Specify a field or members to use as a slicer. A slicer is like a filter that is applied to all MDX query data.
     
     + `axis()` Specify the name of an additional axis to use in the query. 
     
         An OLAP cube can contain up to 128 query axes. Generally, the first four axes are referred to as **Columns**, **Rows**, **Pages**, and **Chapters**. 
         
         If your query is relatively simple, you can use the functions `columns`, `rows`, etc. to build your query. However, you can also use the `axis()` function with a non-zero index value to build an MDX query with many qualifiers, or to add extra dimensions as qualifiers.

5. Pass the handle, and the completed MDX query, into one of the following functions, depending on the shape of the results: 

  + `executeMD` Returns a multi-dimensional array
  + `execute2D` Returns a two-dimensional (tabular) data frame

## <a name="executeMDX"></a> Execute a valid MDX query from R

1. Define a connection string that specifies the OLAP data source (SSAS instance), and the MSOLAP provider.

2. Use the function `OlapConnection(connectionString)` to create a handle for the MDX query and pass the connection string.

3. Define an R variable to store the text of the MDX query.

4. Pass the handle and the variable containing the MDX query into the functions `executeMD` or `execute2D`, depending on the shape of the results.

    + `executeMD` Returns a multi-dimensional array
    + `execute2D` Returns a two-dimensional (tabular) data frame

## Examples

The following examples are based on the AdventureWorks data mart and cube project, because that project is widely available, in multiple versions, including backup files that can easily be restored to Analysis Services. If you don't have an existing cube, get a sample cube using either of these options:

+ Create the cube that is used in these examples by following the Analysis Services tutorial up to Lesson 4:
[Creating an OLAP cube](../../analysis-services/multidimensional-modeling-adventure-works-tutorial.md)

+ Download an existing cube as a backup, and restore it to an instance of Analysis Services. For example, this site provides a fully processed cube in zipped format: [Adventure Works Multidimensional Model SQL 2014](https://msftdbprodsamples.codeplex.com/downloads/get/882334). Extract the file, and then restore it to your SSAS instance. For more information, see [Backup and restore](../../analysis-services/multidimensional-models/backup-and-restore-of-analysis-services-databases.md), or [Restore-ASDatabase Cmdlet](../../analysis-services/powershell/restore-asdatabase-cmdlet.md).

### 1. Basic MDX with slicer

This MDX query selects the _measures_ for count and amount of Internet sales count and sales amount, and places them on the Column axis. It adds a member of the SalesTerritory dimension as a *slicer*, to filter the query so that only the sales from Australia are used in calculations.

```MDX
SELECT {[Measures].[Internet Sales Count], [Measures].[InternetSales-Sales Amount]} ON COLUMNS, 
{[Product].[Product Line].[Product Line].MEMBERS} ON ROWS 
FROM [Analysis Services Tutorial] 
WHERE [Sales Territory].[Sales Territory Country].[Australia]
```

+ On columns, you can specify multiple measures as elements of a comma-separated string.
+ The Row axis uses all possible values (all MEMBERS) of the "Product Line" dimension. 
+ This query would return a table with three columns, containing a _rollup_ summary of Internet sales from all countries.
+ The WHERE clause specifies the _slicer axis_. In this example, the slicer uses a member of the **SalesTerritory** dimension to filter the query so that only the sales from Australia are used in calculations.

#### To build this query using the functions provided in olapR

```R
cnnstr <- "Data Source=localhost; Provider=MSOLAP;"
ocs <- OlapConnection(cnnstr)

qry <- Query()
cube(qry) <- "[Analysis Services Tutorial]"
columns(qry) <- c("[Measures].[Internet Sales Count]", "[Measures].[Internet Sales-Sales Amount]")
rows(qry) <- c("[Product].[Product Line].[Product Line].MEMBERS") 
slicers(qry) <- c("[Sales Territory].[Sales Territory Country].[Australia]")

result1 <- executeMD(ocs, qry)

```

For a named instance, be sure to escape any characters that could be considered control characters in R.  For example, the following connection string references an instance OLAP01, on a server named ContosoHQ:

```R
cnnstr <- "Data Source=ContosoHQ\\OLAP01; Provider=MSOLAP;"
```

#### To run this query as a predefined MDX string

```R
cnnstr <- "Data Source=localhost; Provider=MSOLAP;"
ocs <- OlapConnection(cnnstr)

mdx <- "SELECT {[Measures].[Internet Sales Count], [Measures].[InternetSales-Sales Amount]} ON COLUMNS, {[Product].[Product Line].[Product Line].MEMBERS} ON ROWS FROM [Analysis Services Tutorial] WHERE [Sales Territory].[Sales Territory Country].[Australia]"

result2 <- execute2D(ocs, mdx)
```

If you define a query by using the MDX builder in SQL Server Management Studio and then save the MDX string, it will number the axes starting at 0, as shown here: 

```MDX
SELECT {[Measures].[Internet Sales Count], [Measures].[Internet Sales-Sales Amount]} ON AXIS(0), 
   {[Product].[Product Line].[Product Line].MEMBERS} ON AXIS(1) 
   FROM [Analysis Services Tutorial] 
   WHERE [Sales Territory].[Sales Territory Countr,y].[Australia]
```

You can still run this query as a predefined MDX string. However, to build the same query using R using the `axis()` function, you must renumber the axes starting at 1.

### 2. Explore cubes and their fields on an SSAS instance

You can use the `explore` function to return a list of cubes, dimensions, or members to use in constructing your query. This is handy if you don't have access to other OLAP browsing tools, or if you want to programmatically manipulate or construct the MDX query.

#### To list the cubes available on the specified connection

To view all cubes or perspectives on the instance that you have permission to view, provide the handle as an argument to `explore`.

> [!IMPORTANT]
> The final result is **not** a cube; TRUE merely indicates that the metadata operation was successful. An error is thrown if arguments are invalid.

```R
cnnstr <- "Data Source=localhost; Provider=MSOLAP;"
ocs <- OlapConnection(cnnstr)
explore(ocs)
```

| Results  |
| ----|
| _Analysis Services Tutorial_|
|_Internet Sales_|
|_Reseller Sales_|
|_Sales Summary_|
|_[1] TRUE_|

#### To get a list of cube dimensions

To view all dimensions in the cube or perspective, specify the cube or perspective name.

```R
cnnstr <- "Data Source=localhost; Provider=MSOLAP;"
ocs \<- OlapConnection(cnnstr)
explore(ocs, "Sales")
```

| Results  |
| ----|
| _Customer_|
|_Date_|
|_Region_|


#### To return all members of the specified dimension and hierarchy

After defining the source and creating the handle, specify the cube, dimension, and hierarchy to return. In the return results, items that are prefixed with **->** represent children of the previous member.

```R
cnnstr <- "Data Source=localhost; Provider=MSOLAP;"
ocs \<- OlapConnection(cnnstr)
explore(ocs, "Analysis Services Tutorial", "Product", "Product Categories", "Category")
```

| Results  |
| ----|
| _Accessories_|
|_Bikes_|
|_Clothing_|
|_Components_|
|-> Assembly Components|
|-> Assembly Components|


## See also

[Using data from OLAP cubes in R](../../advanced-analytics/r/using-data-from-olap-cubes-in-r.md)
