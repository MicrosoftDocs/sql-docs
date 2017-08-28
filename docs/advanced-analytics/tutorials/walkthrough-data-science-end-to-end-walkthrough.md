---
title: "End-to-end data science walkthrough for R and SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "08/22/2017"
ms.prod: "sql-server-2017"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
applies_to: 
  - "SQL Server 2016"
dev_langs: 
  - "R"
ms.assetid: edd76ae9-4125-45a8-bf42-47a85b9d9a32
caps.latest.revision: 17
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# End-to-end data science walkthrough for R and SQL Server

In this walkthrough, you develop an end-to-end solution for predictive modeling based on Microsoft R with SQL Server 2016 or SQL Server 2017.

This walkthrough is based on a popular set of public data, the New York City taxi dataset. You use a combination of R code, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data, and custom SQL functions to build a classification model that indicates the probability that the driver might get a tip on a particular taxi trip. You also deploy your R model to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and use server data to generate scores based on the model.

This example can be extended to all kinds of real-life problems, such as predicting customer responses to sales campaigns, or predicting spending or attendance at events. Because the model can be invoked from a stored procedure, you can easily embed it in an application.

Because the walkthrough is designed to introduce R developers to [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], R is used wherever possible. However, this does not mean that R is necessarily the best tool for each task. In many cases, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might provide better performance, particularly for tasks such as data aggregation and feature engineering.  Such tasks can particularly benefit from new features in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], such as memory optimized columnstore indexes. We try to point out possible optimizations along the way.

> [!NOTE]
> The walkthrough was originally developed for and tested on SQL Server 2016. However, screenshots and procedures have been updated to use the latest version of SQL Server Management Studio, which works with SQL Server 2017.

## Overview

The estimated times do not include setup. For more information, see [Prerequisites for the walkthrough](../tutorials/walkthrough-prerequisites-for-data-science-walkthroughs.md).

|Topic list|Estimated time|
|-|------------------------------|
|[Prepare the R walkthrough data](../tutorials/walkthrough-prepare-the-data.md) <br /><br />Obtain the data used for building a model. Download a public dataset and load it into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|30 minutes|
|[Explore the data using SQL](../tutorials/walkthrough-view-and-explore-the-data.md) <br /><br />Understand your data using SQL tools and summaries.|10 minutes|
|[Summarize the data using R](../tutorials/walkthrough-view-and-summarize-data-using-r.md) <br /><br />Use R to explore the data and generate summaries.|10 minutes|
|[Create plots using R in SQL Server](../tutorials/walkthrough-create-graphs-and-plots-using-r.md) <br /><br />Create plots in local and remote compute contexts by mixing R and SQL.|10 minutes|
|[Create data features using R and T-SQL)](../tutorials/walkthrough-create-data-features.md) <br /><br />Perform feature engineering using custom functions in R and [!INCLUDE[tsql](../../includes/tsql-md.md)]. Compare the performance of R and T-SQL for featurization tasks. |10 minutes|
|[Build an R model and save it in SQL Server](../tutorials/walkthrough-build-and-save-the-model.md) <br /><br />Train and tune a predictive model. Assess model performance. This walkthrough creates a classification model. Plot the model's accuracy using R.|15 minutes|
|[Deploy the R model using SQL Server](../tutorials/walkthrough-deploy-and-use-the-model.md) <br /><br />Deploy the model in production by saving the model to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Call the model from a stored procedure to generate predictions.|10 minutes|

### Intended audience

This walkthrough is intended for R or SQL developers. It provides an introduction into how R can be integrated into enterprise workflows using [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)].  You should be familiar with database operations, such as creating databases and tables, importing data, and running queries.

+ All SQL and R scripts are included.
+ You might need to modify strings in the scripts, to run in your environment. You can do this with any code editor, such as [Visual Studio Code](https://code.visualstudio.com/Download).

### Prerequisites

+ You must have access to an instance of SQL Server 2016, or an evaluation version of SQL Server 2017.
+ At least one instance on the SQL Server computer must have [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] installed.
+ If you want to run R commands from a remote computer, such as a laptop or other networked computer, you must install the Microsoft R Open libraries. You can install either Microsoft R Client or Microsoft R Server. The remote computer must be able to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.
+ If you need to put client and server on the same computer, be sure to install a separate set of Microsoft R libraries for use in sending R script from a "remote" client. Do not use the R libraries that are installed for use by the SQL Server instance for this purpose.

For details about how to set up these server and client environments, see [Prerequisites for R and SQL Server data science walkthrough](../tutorials/walkthrough-prerequisites-for-data-science-walkthroughs.md).

## Next lesson

[Prepare the R walkthrough data](../tutorials/walkthrough-prepare-the-data.md)
