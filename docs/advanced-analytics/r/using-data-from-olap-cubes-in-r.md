---
title: "Using data from OLAP cubes in R | Microsoft Docs"
ms.custom: ""
ms.date: "11/03/2017"
ms.prod: "sql-server-2017"
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
manager: "cgronlund"
ms.workload: "On Demand"
---
# Using data from OLAP cubes in R

The **olapR** package is an R package, provided by Microsoft for use with Machine Learning Server and SQL Server R Services, that lets you run MDX queries to get data from OLAP cubes. With this package, you don't need to create linked servers or clean up flattened rowsets; you can use OLAP data directly in R.

This article describes the API, along with an overview fo OLAP and MDX for R users who might be new to multidimensional cube databases.

## What is an OLAP cube?

OLAP is short for Online Analytical Processing. It's a generic term that covers all kinds of cubes, not just those fromm Microsoft. OLAP cubes are widely used for capturing and storing critical business data over time. OLAP data is consumed for business analytics by a variety of tools, dashboards, and visualizations. For more information, see [Online analytical processing](https://en.wikipedia.org/wiki/Online_analytical_processing).

An OLAP _cube_ is a multi-dimensional database, meaning the data is carefully structured by dimensions. A dimension is similar to a factor in R: a variable that you can use to perform summaries or other calculations over groups. For example, time is an important dimenson, and many cubes contain multiple calendars to use as time dimensions when slicing and summarizing data. For performance reasons, an OLAP database often calculates these summaries (or _aggregations_) in advance, and then stores them for faster retrieval. 

Numerical values are usually captured in form of *measures*. A measure is a formula that can be applied over any set of dimensions. Typically the formula for a measure represents some core business metric, and can be as detailed as the business needs. For example, you would include a measure in your query to get the sum of sales amount, average taxes paid per quarter, year-to-date cumulative wages paid, and so forth.

MDX, short for multidimensional expressions, is the language used for querying cubes. Typically, you define a slice of data by specifying its dimensions, and specify at least one measure. .

here are some more terms to help you understand MDX and get started with query building:

+ *Slicing* takes a subset of the cube by picking a value for one dimension, resulting in a cube that is one dimension smaller. 

+ *Dicing* creates a subcube by specifying a range of values on multiple dimensions.

+ *Drill-down* navigates from a summary to details.

+ *Drill-up* moves from details to a higher level of aggregation.

+ *Roll-up* summarizes the data on a dimension.

+ *Pivot* rotate the cube or the data selection.

This topic provides more examples following examples show the basic syntax for querying a cube.
[How to create MDX queries using R](../../advanced-analytics/r-services/how-to-create-mdx-queries-using-olapr.md)

## olapR API

The **olapR** package supports two methods of creating MDX queries:

- **Use the MDX builder.** Use the R functions in the package to generate a simple MDX query, by choosing a cube, and setting axes and slicers. This is an easy way to build a valid MDX query if you do not have access to traditional OLAP tools, or don't have deep knowledge of the MDX language.

    Not all possible MDX queries can be created by using this method, because MDX can be very complex. However, this API supports most of the most common and useful operations, including slice, dice, drilldown, rollup, and pivot in N dimensions.

+ **Copy-paste well-formed MDX.** Manually create and then paste in any MDX query. This option is the best if you have existing MDX queries that you want to reuse, or if the query you want to build is too complex for **olapR** to handle. 

    Build your MDX using any client utility, such as SSMS or Excel, and then save the string that defines the MDX query. YOu provide this MDX string as an argument to the *SSAS query handler* in the **olapR** package. The function sends the query to the specified Analysis Services server, and passes back the results to R, assuming you have permissions to query the cube of course.

For examples of how to build an MDX query or run an existing MDX query, see [How to create MDX queries using R](../../advanced-analytics/r/how-to-create-mdx-queries-using-olapr.md).

## Known Issues

### Tabular models not supported

You can connect to a tabular instance of Analysis Services and the `explore` function will report success with a return value of TRUE, but the tabular model objects are not a compatible type and cannot be explored.

Tabular models support MDX queries, but a valid MDX query against a tabular model will return a NULL result and not report an error.

## Resources

If you are new to OLAP or to MDX queries, see these Wikipedia articles: 
[OLAP Cubes](https://en.wikipedia.org/wiki/OLAP_cube)
[MDX queries](https://en.wikipedia.org/wiki/MultiDimensional_eXpressions)

### Samples

If you want to learn more about cubes, you can create the cube that is used in these examples by following the Analysis Services tutorial up to Lesson 4:
[Creating an OLAP Cube](../../analysis-services/multidimensional-modeling-adventure-works-tutorial.md)

You can also download an existing cube as a backup, and restore it to an instance of Analysis Services. For example, you can download a fully processed cube for [Adventure Works Multidimensional Model SQL 2014](http://msftdbprodsamples.codeplex.com/downloads/get/882334), in zipped format, and restore it to your SSAS instance. For more information, see [Backup and Restore](../../analysis-services/multidimensional-models/backup-and-restore-of-analysis-services-databases.md), or [Restore-ASDatabase Cmdlet](../../analysis-services/powershell/restore-asdatabase-cmdlet.md).

