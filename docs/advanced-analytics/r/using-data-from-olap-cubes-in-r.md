---
title: "Using data from OLAP cubes in R | Microsoft Docs"
ms.custom: ""
ms.date: "11/03/2017"
ms.prod: sql-non-specified
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

OLAP is short for Online Analytical Processing. OLAP solutions are widely used for capturing and storing critical business data over time. OLAP data is consumed for business analytics by a variety of tools, dashboards, and visualizations. For more information, see [Online analytical processing](https://en.wikipedia.org/wiki/Online_analytical_processing).

Microsoft provides [Analysis Services](https://docs.microsoft.com/sql/analysis-services/analysis-services), which lets you design, deploy, and query OLAP data in the form of _cubes_ or _tabular models_. A cube is a multi-dimensional database. _Dimensions_ are like facets of the data, or factors in R: you use dimensions to identify some particular subset of data that you want to summarize or analyze. For example, time is an important dimension, so much so that many OLAP solutions include multiple calendars defined by default, to use when slicing and summarizing data. 

For performance reasons, an OLAP database often calculates summaries (or _aggregations_) in advance, and then stores them for faster retrieval. Summaries are based on  *measures*, which represent formulas that can be applied to numerical data. You use the dimensions to define a subset of data, and then compute the measure over that data. For example, you would use a measure to compute the total sales for a certain product line over multiple quarters minus taxes, to report the average shipping costs for a particular supplier, year-to-date cumulative wages paid, and so forth.

MDX, short for multidimensional expressions, is the language used for querying cubes. An MDX query typically contains a data definition that includes one or more dimensions, and at least one measure, thogh MDX queries can get considerably more complex, and include rolling windows, cumulative averages or sums, percentiles. 

Here are some other terms that might be helpful when you start building MDX queries:

+ *Slicing* takes a subset of the cube by using values from a single dimension.

+ *Dicing* creates a subcube by specifying a range of values on multiple dimensions.

+ *Drill-down* navigates from a summary to details.

+ *Drill-up* moves from details to a higher level of aggregation.

+ *Roll-up* summarizes the data on a dimension.

+ *Pivot* rotate the cube or the data selection.

This topic provides more examples of the basic syntax for queries on a cube: 

+ [How to create MDX queries using R](../../advanced-analytics/r-services/how-to-create-mdx-queries-using-olapr.md)

## olapR API

The **olapR** package supports two methods of creating MDX queries:

- **Use the MDX builder.** Use the R functions in the package to generate a simple MDX query, by choosing a cube, and setting axes and slicers. This is an easy way to build a valid MDX query if you do not have access to traditional OLAP tools, or don't have deep knowledge of the MDX language.

    Not all possible MDX queries can be created by using this method, because MDX can be complex. However, this API supports most of the most common and useful operations, including slice, dice, drilldown, rollup, and pivot in N dimensions.

+ **Copy-paste well-formed MDX.** Manually create and then paste in any MDX query. This option is the best if you have existing MDX queries that you want to reuse, or if the query you want to build is too complex for **olapR** to handle. 

    Build your MDX using any client utility, such as SSMS or Excel, and then save the string that defines the MDX query. YOu provide this MDX string as an argument to the *SSAS query handler* in the **olapR** package. The function sends the query to the specified Analysis Services server, and passes back the results to R, assuming you have permissions to query the cube of course.

For examples of how to build an MDX query or run an existing MDX query, see [How to create MDX queries using R](../../advanced-analytics/r/how-to-create-mdx-queries-using-olapr.md).

## Known Issues

### Tabular models not supported

+ If you connect to a tabular instance of Analysis Services, the `explore` function reports success with a return value of TRUE. However, tabular model objects are not a compatible type and cannot be explored.

+ Tabular models can be queried using either DAX or MDX. If you design a valid MDX query against a tabular model using an external tool and then paste the query into this API, the query returns a NULL result and does not report an error.

## Resources

If you are new to OLAP or to MDX queries, see these Wikipedia articles: 
[OLAP Cubes](https://en.wikipedia.org/wiki/OLAP_cube)
[MDX queries](https://en.wikipedia.org/wiki/MultiDimensional_eXpressions)

### Samples

If you want to learn more about cubes, you can create the cube that is used in these examples by following the Analysis Services tutorial up to Lesson 4:
[Creating an OLAP Cube](../../analysis-services/multidimensional-modeling-adventure-works-tutorial.md)

You can also download an existing cube as a backup, and restore it to an instance of Analysis Services. For example, you can download a fully processed cube for [Adventure Works Multidimensional Model SQL 2014](http://msftdbprodsamples.codeplex.com/downloads/get/882334), in zipped format, and restore it to your SSAS instance. For more information, see [Backup and Restore](../../analysis-services/multidimensional-models/backup-and-restore-of-analysis-services-databases.md), or [Restore-ASDatabase Cmdlet](../../analysis-services/powershell/restore-asdatabase-cmdlet.md).
