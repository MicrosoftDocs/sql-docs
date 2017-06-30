---
title: "Data Science End-to-End Walkthrough | Microsoft Docs"
ms.custom: ""
ms.date: "06/28/2017"
ms.prod: "sql-server-2016"
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
# Data Science End-to-End Walkthrough

In this walkthrough, you'll develop an end-to-end solution for predictive modeling using [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)].

This walkthrough is based on a popular set of public data, the New York City taxi dataset. You will use a combination of R code, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data, and custom SQL functions to build a classification model that indicates the probability that the driver will get a tip on a particular taxi trip. You'll also deploy your R model to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and use server data to generate scores based on the model.

This example can easily be extended to all kinds of real-life problems, such as predicting customer responses to sales campaigns, or predicting spending by visitors to events. Because the model can be invoked from a stored procedure, you can also easily embed it in an application.

**Intended audience**

This walkthrough is intended for R or SQL developers. It provides an introduction into how R can be integrated into enterprise workflows using [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)].  You should be familiar with database operations, such as creating databases and tables, importing data, and creating queries using [!INCLUDE[tsql](../../includes/tsql-md.md)].

+ All Transact-SQL are provided. However, you might need to modify scripts to change table or database name.
+ No R coding is required; all scripts are provided.

**Prerequisites**

+ You must have access to an instance of SQL Server 2016, or an evaluation version of SQL Server 2017.
+ At least one instance on the SQL Server computer must have [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] installed.
+ To run R commands, you'll need a separate computer that has an R IDE and the Microsoft R Open libraries. It can be a laptop or other networked computer, but it must be able to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.

For details about how to set up these server and client environments, see [Prerequisites for Data Science Walkthrough](/walkthrough-prerequisites-for-data-science-walkthroughs.md).

## What the walkthrough covers

Note that the estimated times do not include setup.

|Lesson list|Description and estimated time|
|-|------------------------------|
|[Prerequisites](/walkthrough-prerequisites-for-data-science-walkthroughs.md)|Resources to help you set up the environment. Setup requires about 1 hr.|
|[Prepare the Data](/walkthrough-prepare-the-data.md)|Obtain the data used for building a model. Download a public dataset and load it into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.<br />30 minutes|
|[Explore the Data](/walkthrough-view-and-explore-the-data.md)|Explore the data, prepare it for modeling, and create new features.  You'll use both SQL and R to explore the data and generate summaries.<br />20 minutes|
|[Summarize Data using R](/walkthrough-view-and-summarize-data-using-r.md)|Create data summaries using RevoScaleR functions.<br />20 minutes|
|[Create Graphs and Plots](/walkthrough-create-graphs-and-plots-using-r.md)|Create scatterplots and other charts.<br />10 minutes|
|[Create Data Features using SQL and R](/walkthrough-create-data-features.md) |Perform feature engineering using custom functions in R and [!INCLUDE[tsql](../../includes/tsql-md.md)]. Compare the performance of R and Transact-SQL for feature extraction.<br />10 minutes|
|[Build and Save the Model](/walkthrough-build-and-save-the-model.md)|Train and tune a a classification model, then assess model performance. Plot the model's accuracy using R.<br />15 minutes|
|[Deploy and Use the Model](/walkthrough-deploy-and-use-the-model.md)|Deploy the model in production by saving the model to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Call the model from a stored procedure to generate predictions.<br />10 minutes|

The walkthrough is designed to introduce R developers to [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], so R is used wherever possible. This does not mean that R is necessarily the best tool for each task. In many cases, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might provide better performance, particularly for tasks such as data aggregation and feature engineering.  Such tasks can particularly benefit from new features in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], such as memory-optimized columnstore indexes. We'll point out possible optimizations along the way.

> [!NOTE]
> The walkthrough was originally developed for and tested on SQL Server 2016. However, screenshots and procedures have been updated to use the latest version of SQL Server Management Studio, which works with SQL Server 2017.

## Next step

[Prepare the Data](/walkthrough-prepare-the-data.md)