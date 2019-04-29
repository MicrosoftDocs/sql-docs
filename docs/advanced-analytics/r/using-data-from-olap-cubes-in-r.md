---
title: Using data from OLAP cubes in R - SQL Server Machine Learning Services
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Using data from OLAP cubes in R
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

The **olapR** package is an R package, provided by Microsoft for use with Machine Learning Server and SQL Server, that lets you run MDX queries to get data from OLAP cubes. With this package, you don't need to create linked servers or clean up flattened rowsets; you can get OLAP data directly from R.

This article describes the API, along with an overview of OLAP and MDX for R users who might be new to multidimensional cube databases.

> [!IMPORTANT]
> An instance of Analysis Services can support either conventional multidimensional cubes, or tabular models, but an instance cannot support both types of models. Therefore, before you try to build an MDX query against a cube, verify that the Analysis Services instance contains multidimensional models.

## What is an OLAP cube?

OLAP is short for Online Analytical Processing. OLAP solutions are widely used for capturing and storing critical business data over time. OLAP data is consumed for business analytics by a variety of tools, dashboards, and visualizations. For more information, see [Online analytical processing](https://en.wikipedia.org/wiki/Online_analytical_processing).

Microsoft provides [Analysis Services](https://docs.microsoft.com/sql/analysis-services/analysis-services), which lets you design, deploy, and query OLAP data in the form of _cubes_ or _tabular models_. A cube is a multi-dimensional database. _Dimensions_ are like facets of the data, or factors in R: you use dimensions to identify some particular subset of data that you want to summarize or analyze. For example, time is an important dimension, so much so that many OLAP solutions include multiple calendars defined by default, to use when slicing and summarizing data. 

For performance reasons, an OLAP database often calculates summaries (or _aggregations_) in advance, and then stores them for faster retrieval. Summaries are based on  *measures*, which represent formulas that can be applied to numerical data. You use the dimensions to define a subset of data, and then compute the measure over that data. For example, you would use a measure to compute the total sales for a certain product line over multiple quarters minus taxes, to report the average shipping costs for a particular supplier, year-to-date cumulative wages paid, and so forth.

MDX, short for multidimensional expressions, is the language used for querying cubes. An MDX query typically contains a data definition that includes one or more dimensions, and at least one measure, though MDX queries can get considerably more complex, and include rolling windows, cumulative averages, sums, ranks, or percentiles. 

Here are some other terms that might be helpful when you start building MDX queries:

+ *Slicing* takes a subset of the cube by using values from a single dimension.

+ *Dicing* creates a subcube by specifying a range of values on multiple dimensions.

+ *Drill-down* navigates from a summary to details.

+ *Drill-up* moves from details to a higher level of aggregation.

+ *Roll-up* summarizes the data on a dimension.

+ *Pivot* rotate the cube or the data selection.

## How to use olapR to create MDX queries

The following article provides detailed examples of the syntax for creating or executing queries against a cube:

+ [How to create MDX queries using R](../../advanced-analytics/r/how-to-create-mdx-queries-using-olapr.md)

## olapR API

The **olapR** package supports two methods of creating MDX queries:

- **Use the MDX builder.** Use the R functions in the package to generate a simple MDX query, by choosing a cube, and then setting axes and slicers. This is an easy way to build a valid MDX query if you do not have access to traditional OLAP tools, or don't have deep knowledge of the MDX language.

    Not all MDX queries can be created by using this method, because MDX can be complex. However, this API supports most of the most common and useful operations, including slice, dice, drilldown, rollup, and pivot in N dimensions.

+ **Copy-paste well-formed MDX.** Manually create and then paste in any MDX query. This option is the best if you have existing MDX queries that you want to reuse, or if the query you want to build is too complex for **olapR** to handle.

    After building your MDX using any client utility, such as SSMS or Excel, save the query string. Provide this MDX string as an argument to the *SSAS query handler* in the **olapR** package. The provider sends the query to the specified Analysis Services server, and passes back the results to R. 

For examples of how to build an MDX query or run an existing MDX query, see [How to create MDX queries using R](../../advanced-analytics/r/how-to-create-mdx-queries-using-olapr.md).

## Known issues

This section lists some known issues and common questions about  the **olapR** package.

### Tabular model support

If you connect to an instance of Analysis Services that contains a tabular model, the `explore` function reports success with a return value of TRUE. However, tabular model objects are different from multidimensional objects, and the structure of a multidimensional database is different from that of a tabular model.

Although DAX (Data analysis Expressions) is the language typically used with tabular models, you can design valid MDX queries against a tabular model, if you are already familiar with MDX. You cannot use the olapR constructors to build valid MDX queries against a tabular model.

However, MDX queries are an inefficient way to retrieve data from a tabular model. If you need to get data from a tabular model for use in R, consider these methods instead:

+ Enable DirectQuery on the model and add the server as a linked server in SQL Server. 
+ If the tabular model was built on a relational data mart, obtain the data directly from the source.

### How to determine whether an instance contains tabular or multidimensional models

A single Analysis Services instance can contain only one type of model, though it can contain multiple models. The reason is that there are fundamental differences between tabular models and multidimensional models that control the way data is stored and processed. For example, tabular models are stored in memory and leverage columnstore indexes to perform very fast calculations. In multidimensional models, data is stored on disk and aggregations are defined in advance and retrieved by using MDX queries.

If you connect to Analysis Services using a client such as SQL Server Management Studio, you can tell at a glance which model type is supported, by looking at the icon for the database.

You can also view and query the server properties to determine which type of model the instance supports. The **Server mode** property supports two values: _multidimensional_ or _tabular_.

See the following article for general information about the two types of models:

+ [Comparing multidimensional and tabular models](https://docs.microsoft.com/sql/analysis-services/comparing-tabular-and-multidimensional-solutions-ssas)

See the following article for information about querying server properties:

+ [OLE DB for OLAP Schema Rowsets](https://docs.microsoft.com/bi-reference/schema-rowsets/ole-db-olap/ole-db-for-olap-schema-rowsets)

### Writeback is not supported

It is not possible to write the results of custom R calculations back to the cube.

In general, even when a cube is enabled for writeback, only limited operations are supported, and additional configuration might be required. We recommend that you use MDX for such operations.

+ [Write-enabled dimensions](https://docs.microsoft.com/sql/analysis-services/multidimensional-models-olap-logical-dimension-objects/write-enabled-dimensions)
+ [Write-enabled partitions](https://docs.microsoft.com/sql/analysis-services/multidimensional-models-olap-logical-cube-objects/partitions-write-enabled-partitions)
+ [Set custom access to cell data](https://docs.microsoft.com/sql/analysis-services/multidimensional-models/grant-custom-access-to-cell-data-analysis-services)

### Long-running MDX queries block cube processing

Although the **olapR** package performs only read operations, long-running MDX queries can create locks that prevent the cube from being processed. Always test your MDX queries in advance so that you know how much data should be returned.

If you try to connect to a cube that is locked, you might get an error that the SQL Server data warehouse cannot be reached. Suggested resolutions include enabling remote connections, checking the server or instance name, and so forth; however, consider the possibility of a prior open connection.

An SSAS administrator can prevent locking issues by identifying and terminating open sessions. A timeout property can also be applied to MDX queries at the server level to force termination of all long-running queries.

## Resources

If you are new to OLAP or to MDX queries, see these Wikipedia articles: 

+ [OLAP cubes](https://en.wikipedia.org/wiki/OLAP_cube)
+ [MDX queries](https://en.wikipedia.org/wiki/MultiDimensional_eXpressions)
