---
title: "SQL Server R Tutorials | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/282/2017"
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
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# SQL Server R Tutorials

This article provides a list of tutorials and samples that demonstrate the use of R with SQL Server 2016 or SQL Server 2017. Through these samples and demos, you will learn:

+ How to run R from T-SQL
+ What are remote and local compute contexts, and how you can execute R code using the SQL Server computer
+ How to wrap R code in a stored procedure
+ Optimizing R code for a SQL production environment
+ Real-world scenarios for embedding machine learning in applications

For information about requirements and setup, see [Prerequisites](#bkmk_Prerequisites).

## <a name="bkmk_sqltutorials"></a>R Tutorials

This section lists tutorials that were developed for SQL Server 2016 R Services. Unless otherwise indicated, these tutorials are expected to work without modification in SQL Server 2017. All tutorials make extensive use of features in the RevoScaleR package for SQL Server compute contexts.

+ [Data Science Deep Dive with R and SQL Server](../tutorials/deepdive-data-science-deep-dive-using-the-revoscaler-packages.md)

  Learn how to use the functions in the RevoScaleR packages. Move data between R and SQL Server, and switch compute contexts to suit a particular task. Create models and plots, and move them between your development environment and the database server.

  **Audience:** For data scientists or developers who are already familiar with the R language, and who want to learn about the enhanced R packages and functions in Microsoft R by Revolution Analytics.

  **Requirements:** Some basic R knowledge. Access to a server with SQL Server R Services or Machine Learning Services with R. For setup help, see [Prerequisites](#bkmk_Prerequisites).

+ [In-Database Advanced Analytics for SQL Developers &#40;Tutorial&#41;](../../advanced-analytics/r-services/in-database-advanced-analytics-for-sql-developers-tutorial.md)

  Build and deploy a complete R solution, using only [!INCLUDE[tsql](../../includes/tsql-md.md)] tools.

  Focuses on moving a solution into production. You'll learn how to wrap R code in a stored procedure, save an R model to a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, and make parameterized calls to the R model for prediction.

  **Audience:** For SQL developers, application developers, or SQL professionals who support R solutions and want to learn how to deploy R models to SQL Server.

  **Requirements:** No R environment is needed. All R code is provided and you can build the complete solution using only [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)] and familiar business intelligence and SQL development tools. However, some basic knowledge of R is helpful.

  You must have access to a SQL Server with the R language installed and enabled. For setup help, see [Prerequisites](#bkmk_Prerequisites).

+ [Using R Code in Transact-SQL &#40;SQL Server R Services&#41;](../../advanced-analytics/r-services/using-r-code-in-transact-sql-sql-server-r-services.md)

  This quickstart covers the basic syntax for using R in [!INCLUDE[tsql](../../includes/tsql-md.md)].

  Learn how to call the R run-time from T-SQL, wrap R functions in SQL code, and run a stored procedure that saves R output and R models to a SQL table.

  **Audience:** For people who are new to the feature, and want to learn the basics of calling R from a stored procedure.

  **Requirements:** No knowledge of R or SQL required. However, you need either SQL Server Management Studio or another client that can connect to a database and run T-SQL. We recommend the free [MSSQL extension for Visual Studio Code](https://marketplace.visualstudio.com/items?itemName=ms-mssql.mssql) if you are new to T-SQL queries.

  You must also have access to a server with SQL Server R Services or Machine Learning Services with R already enabled. For setup help, see [Prerequisites](#bkmk_Prerequisites).

+ [Data Science End-to-End Walkthrough](../../advanced-analytics/r-services/data-science-end-to-end-walkthrough.md)
  Demonstrates the data science process from beginning to end, as you acquire data and save it to SQL Server, analyze the data with R and build graphs.

  You'll learn how to move graphics between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and R, and compare feature engineering in T-SQL with R functions. Finally, you'll learn how to use the predictive model in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] for both batch scoring and single-row scoring.

  **Audience:** For people who are familiar with R and with developer tools such as SQL Server Management Studio.

  **Requirements:** You should have access to an R development environment and know how to run R commands. Use of PowerShell is required to download the New York City taxi dataset. You must have access to a server with SQL Server R Services or Machine Learning Services with R already enabled. For setup help, see [Prerequisites](#bkmk_Prerequisites).

## <a name ="bkmk_samples"></a>R Samples

These samples and demos are provided by the SQL Server development team to highlight the many ways that you can use embedded analytics in real-world applications.

+ [Build a predictive model using R and SQL Server](https://microsoft.github.io/sql-ml-tutorials/R/rentalprediction)

  Learn how a ski rental business might use machine learning to predict future rentals, which helps the business plan and staff to meet future demand.

+ [Perform customer clustering using R and SQL Server](https://microsoft.github.io/sql-ml-tutorials/R/customerclustering/)

  Use unsupervised learning to segment customers based on sales data.

## <a name="bkmk_Prerequisites"></a>Prerequisites

To use these tutorials and samples, you must install one of the following server products:

+ SQL Server 2016 R Services (In-Database)
  
  Supports R. Be sure to install the machine learning features, and then enable external scripting.

+ SQL Server 2017 Machine Learning Services (In-Database)
  
  Supports either R or Python. You must select the machine learning feature and the language to install, and then enable external scripting.

After running SQL Server setup, don't forget these important steps:

+ Enable the external script execution feature by running `sp_configure 'enable external script', 1`
+ Restart the server
+ Ensure that the service that calls the external runtime has necessary permissions
+ Ensure that your SQL login or Windows user account has necessary permissions to connect to the server, to read data, and to create any database objects required by the sample

If you run into trouble, see this article for some common issues: [Upgrade and Installation of SQL Server R Services](../../advanced-analytics/r/upgrade-and-installation-faq-sql-server-r-services.md)
