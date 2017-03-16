---
title: "Data Science End-to-End Walkthrough | Microsoft Docs"
ms.custom: ""
ms.date: "2016-11-22"
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
caps.latest.revision: 15
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Data Science End-to-End Walkthrough
In this walkthrough, you'll develop an end-to-end solution for predictive modeling using [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)].  
  
This walkthrough is based on a well-known public data set, the New York City taxi dataset. You will use a combination of R code, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data, and custom SQL functions to build a classification model that indicates the probability that the driver will get a tip on a particular taxi trip. You'll also deploy your R model to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and use server data to generate scores based on the model.  
  
This example can easily be extended to all kinds of real-life problems, such as predicting customer responses to sales campaigns, or predicting spending by visitors to events. Because the model can be invoked from a stored procedure, you can also easily embed it in an application.  
  
**Intended Audience**  
  
This walkthrough is intended for R developers. You should be familiar with fundamental database operations, such as creating databases, creating tables, importing data into tables, and querying the tables using [!INCLUDE[tsql](../../includes/tsql-md.md)].  You will be given the SQL and R scripts to execute.  
  
If you are new to R but know databases well, this tutorial provides an introduction into how R can be integrated into enterprise workflows using [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)]. No R coding is required; all scripts are provided. You do need to have some kind of R IDE installed to run the commands.  
  
**Prerequisites**  
  
To do this tutorial, you must have access to an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] installed. For your local environment, prepare a data science workstation that can connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance and then install the required R libraries.  
  
For more information, see [Prerequisites for Data Science Walkthroughs &#40;SQL Server R Services&#41;](../../advanced-analytics/r-services/prerequisites-for-data-science-walkthroughs-sql-server-r-services.md).  
  
## Overview  
This walkthrough illustrates a typical end-to-end solution for advanced analytics. You need to complete the lessons in order.  
  
||Estimated time to complete|  
|-|------------------------------|  
|[Lesson 1: Prepare the Data &#40;Data Science End-to-End Walkthrough&#41;](../../advanced-analytics/r-services/lesson-1-prepare-the-data-data-science-end-to-end-walkthrough.md)<br /><br />The analytical process begins with obtaining the data used for building a model. You will download a public dataset and save it in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database.|30 minutes|  
|[Lesson 2: View and Explore the Data &#40;Data Science End-to-End Walkthrough&#41;](../../advanced-analytics/r-services/lesson-2-view-and-explore-the-data-data-science-end-to-end-walkthrough.md)<br /><br />A data scientist often spends considerable time  exploring the data and preparing it for modeling, creating new features, or transforming the data as needed.  You'll use both SQL and R to explore the data and generate summaries.|20 minutes|  
|[Lesson 3: Create Data Features &#40;Data Science End-to-End Walkthrough&#41;](../../advanced-analytics/r-services/lesson-3-create-data-features-data-science-end-to-end-walkthrough.md)<br /><br />You'll create new data features using custom functions in R and [!INCLUDE[tsql](../../includes/tsql-md.md)].|10 minutes|  
|[Lesson 4: Build and Save the Model &#40;Data Science End-to-End Walkthrough&#41;](../../advanced-analytics/r-services/lesson-4-build-and-save-the-model-data-science-end-to-end-walkthrough.md)<br /><br />When the data is ready, the data scientist trains and tunes the model, assessing its performance and trying different parameters. In this walkthrough, you will create a classification model, and use data in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to generate predictions. You will also  plot the model's accuracy using R.|15 minutes|  
|[Lesson 5: Deploy and Use the Model &#40;Data Science End-to-End Walkthrough&#41;](../../advanced-analytics/r-services/lesson-5-deploy-and-use-the-model-data-science-end-to-end-walkthrough.md)<br /><br />Finally, you will deploy the model in production by saving the model to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, and call the model from a stored procedure to generate predictions.|10 minutes|  
  
## Notes  
Because the walkthrough is designed to introduce R developers to [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], as many actions as possible are done using R. This does not mean that R is necessarily the best tool for each task. In many cases, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] might provide better performance, particularly for tasks such as data aggregation and feature engineering. Such tasks can particularly benefit from new features in [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)], such as memory optimized columnstore indexes.  
  
## Next Step  
[Lesson 1: Prepare the Data &#40;Data Science End-to-End Walkthrough&#41;](../../advanced-analytics/r-services/lesson-1-prepare-the-data-data-science-end-to-end-walkthrough.md)  
  
  
  
