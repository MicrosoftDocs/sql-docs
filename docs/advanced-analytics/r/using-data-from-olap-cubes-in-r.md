---
title: "Using Data from OLAP Cubes in R | Microsoft Docs"
ms.custom: ""
ms.date: "12/16/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
dev_langs: 
  - "R"
ms.assetid: 8093599c-8307-4237-983b-0908d0f8ab77
caps.latest.revision: 12
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Using Data from OLAP Cubes in R

The **olapR** package is a new R package, provided in Microsoft R Server and SQL Server R Services, that lets you run MDX queries and use the data from OLAP cubes in your R solution.

With this package, you don't need to create linked servers or clean up flattened rowsets; you can use OLAP data directly in R,

## Overview

An OLAP cube is a multi-dimensional database that contains precalculated aggregations over *measures*, which typically capture important business metrics such as sales amount, sales count, or other numeric values. OLAP cubes are widely used for capturing and storing critical business data over time. OLAP data is consumed for business analytics by a variety of tools, dashboards, and visualizations. For more information, see [Online analytical processing](https://en.wikipedia.org/wiki/Online_analytical_processing)

The **olapR** package supports two methods of creating MDX queries: 

- You can generate a simple MDX query by using an R-style API to choose a cube, axes, and slicers. Using these function, you can build valid MDX queries even if you do not have access to traditional OLAP tools, or don't have deep knowledge of the MDX language.

  Note that not all possible MDX queries can be created by using this method in the **olapR** package, as MDX can be very complex. However, it supports all of the most common and useful operations, including slice, dice, drilldown, rollup, and pivot in N dimensions.

+ You can manually create and then paste in any MDX query. This option is useful if you have existing MDX queries that you want to reuse, or if the query you want to build is too complex for **olapR** to handle. 

  With this approach, you build your MDX using any client utility, such as SSMS or Excel, and then use it as a string argument to the *SSAS query handler* that is provided by this package. The **olapR** function will send the query to the specified Analysis Services server, and pass back the results to R.

For examples of how to build an MDX query or run an existing MDX query, see [How to Create MDX Queries using R](../../advanced-analytics/r-services/how-to-create-mdx-queries-using-olapr.md).


## MDX Basics

Data in a cube can be retrieved using the MDX (MultiDimensional Expression) query language. Because the data in an OLAP cube (or Analysis Services database) is multidimensional rather than tabular, MDX supports a complex syntax and a variety of operations for filtering and slicing data:

+ *Slicing* takes a subset of the cube by picking a value for one dimension, resulting in a cube that is one dimension smaller. 

+ *Dicing* creates a subcube by specifying a range of values on multiple dimensions.

+ *Drill-down* navigates from a summary to details.

+ *Drill-up* moves from details to a higher level of aggregation.

+ *Roll-up* summarizes the data on a dimension.

+ *Pivot* rotate the cube or the data selection.

This topic provides more examples following examples show the basic syntax for querying a cube.
[How to Create MDX Queries using R](../../advanced-analytics/r-services/how-to-create-mdx-queries-using-olapr.md)


## Known Issues

### Tabular models not supported currently

You can connect to a tabular instance of Analysis Services and the `explore` function will report success with a return value of TRUE, but the tabular model objects are not a compatible type and cannot be explored. 

Tabular models support MDX queries, but a valid MDX query against a tabular model will return a NULL result and not report an error.

## Resources

If you are new to OLAP or to MDX queries, see these Wikipedia articles: 
[OLAP Cubes](https://en.wikipedia.org/wiki/OLAP_cube)
[MDX queries](https://en.wikipedia.org/wiki/MultiDimensional_eXpressions)

### Samples

If you want to learn more about cubes, you can create the cube that is used in these examples by following the Analysis Services tutorial up to Lesson 4:
[Creating an OLAP Cube](/sql-docs/docs/analysis-services/multidimensional-modeling-adventure-works-tutorial)

You can also download an existing cube as a backup, and restore it to an instance of Analysis Services. For example, you can download a fully processed cube for [Adventure Works Multidimensional Model SQL 2014](http://msftdbprodsamples.codeplex.com/downloads/get/882334), in zipped format, and restore it to your SSAS instance. For more information, see [Backup and Restore](../../analysis-services/multidimensional-models/backup-and-restore-of-analysis-services-databases.md), or [Restore-ASDatabase Cmdlet](../../analysis-services/powershell/restore-asdatabase-cmdlet.md).

## See Also
[How to Create MDX Queries using R](../../advanced-analytics/r-services/how-to-create-mdx-queries-using-olapr.md)

[MDX Query Designer for Analysis Services](http://msdn.microsoft.com/library/7e288eee-2d37-485e-a6a0-dbba5e041e26)


