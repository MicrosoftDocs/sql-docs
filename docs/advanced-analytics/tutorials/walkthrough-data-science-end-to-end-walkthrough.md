---
title: "Data Science End-to-End Walkthrough | Microsoft Docs"
ms.custom: ""
ms.date: "03/17/2017"
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
caps.latest.revision: 16
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Data Science End-to-End Walkthrough
In this walkthrough, you'll develop an end-to-end solution for predictive modeling using [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)].  
  
This walkthrough is based on a popular set of public data, the New York City taxi dataset. You will use a combination of R code, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data, and custom SQL functions to build a classification model that indicates the probability that the driver will get a tip on a particular taxi trip. You'll also deploy your R model to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and use server data to generate scores based on the model.  
  
This example can easily be extended to all kinds of real-life problems, such as predicting customer responses to sales campaigns, or predicting spending by visitors to events. Because the model can be invoked from a stored procedure, you can also easily embed it in an application.  
  
**Intended Audience**  
  
This walkthrough is intended for R or SQL developers. It provides an introduction into how R can be integrated into enterprise workflows using [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)].  You should be familiar with database operations, such as creating databases and tables, importing data, and creating queries using [!INCLUDE[tsql](../../includes/tsql-md.md)].  

+ You will be given the SQL and R scripts to execute.  
+ No R coding is required; all scripts are provided. 
  
**Prerequisites**  
  
+ You must have access to an instance of SQL Server 2016, or an evaluation version of SQL Server 2017. 
+ At least one instance on the SQL Server computer must have [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] installed. 
+ To run R commands, you'll need a separate computer that has an R IDE and the Microsoft R Open libraries. It can be a laptop or other networked computer, but it must be able to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.  
  
For details about how to set up these server and client environments, see [Prerequisites for Data Science Walkthroughs &#40;SQL Server R Services&#41;](../../advanced-analytics/r-services/prerequisites-for-data-science-walkthroughs-sql-server-r-services.md).  
  
## What the Walkthrough Covers  

Note that the estimated times do not include setup.
  
|Lesson list|Estimated time|  
|-|------------------------------|  
|[Lesson 1: Prepare the Data &#40;Data Science End-to-End Walkthrough&#41;](../../advanced-analytics/r-services/lesson-1-prepare-the-data-data-science-end-to-end-walkthrough.md)<br /><br />Obtain the data used for building a model. Download a public dataset and load it into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|30 minutes|  
|[Lesson 2: View and Explore the Data &#40;Data Science End-to-End Walkthrough&#41;](../../advanced-analytics/r-services/lesson-2-view-and-explore-the-data-data-science-end-to-end-walkthrough.md)<br /><br />Explore the data, prepare it for modeling, and create new features.  You'll use both SQL and R to explore the data and generate summaries.|20 minutes|  
|[Lesson 3: Create Data Features &#40;Data Science End-to-End Walkthrough&#41;](../../advanced-analytics/r-services/lesson-3-create-data-features-data-science-end-to-end-walkthrough.md)<br /><br />Perform feature engineering using custom functions in R and [!INCLUDE[tsql](../../includes/tsql-md.md)]. Compare the performance of R and T-SQL for featurization tasks. |10 minutes|  
|[Lesson 4: Build and Save the Model &#40;Data Science End-to-End Walkthrough&#41;](../../advanced-analytics/r-services/lesson-4-build-and-save-the-model-data-science-end-to-end-walkthrough.md)<br /><br />Train and tune a predictive model. Assess model performance. This walkthrough creates a classification model. Plot the model's accuracy using R.|15 minutes|  
|[Lesson 5: Deploy and Use the Model &#40;Data Science End-to-End Walkthrough&#41;](../../advanced-analytics/r-services/lesson-5-deploy-and-use-the-model-data-science-end-to-end-walkthrough.md)<br /><br />Deploy the model in production by saving the model to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. Call the model from a stored procedure to generate predictions.|10 minutes|  
  
### Notes

+ The walkthrough is designed to introduce R developers to [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], so R is used whever possible. This does not mean that R is necessarily the best tool for each task. In many cases, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might provide better performance, particularly for tasks such as data aggregation and feature engineering.  Such tasks can particularly benefit from new features in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], such as memory optimized columnstore indexes. We'll point out possible optimizations along the way. 
+ The walkthrough was originally developed for and tested on SQL Server 2016. However, screenshots and procedures have been updated to use the latest version of SQL Server Management Studio, which works with SQL Server 2017.
  
## Next Step  
[Lesson 1: Prepare the Data &#40;Data Science End-to-End Walkthrough&#41;](../../advanced-analytics/r-services/lesson-1-prepare-the-data-data-science-end-to-end-walkthrough.md)  
  
  
  

