---
title: "Creating Workflows that Use R in SQL Server | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "05/31/2016"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 34c3b1c2-97db-4cea-b287-c7f4fe4ecc1b
caps.latest.revision: 11
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Creating Workflows that Use R in SQL Server
  A relational database is a highly optimized technology for delivering scalable solutions for transaction processing, storage, and querying of data. However, traditionally R solutions have generally relied on importing data from various sources, often in CSV format, to perform further data exploration and modeling. Such practices are not only inefficient but insecure.  
  
 Using [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] provides multiple advantages:  
  
-   Data security. The power of R is brought into the database, closer to the source of data. Avoids wasteful or insecure data movement.  
  
-   Speed. Databases are optimized for set-based operations. Moreover, recent innovations in databases such as in-memory tables and columnar data storage further improve data science by making aggregations lightning fast.  
  
-   Integration. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is the central point of operations for many other data management tasks and applications that consume enterprise. Using data already in the database ensures that data is consistent and up-to-date. Rather than process data in R, you can rely on enterprise data pipelines including [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and Azure Data Factory. Reporting of results or analyses is easy via Power BI or [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)].  
  
 By using the right combination of SQL and R for different data processing and analytical tasks, both data scientists and developers can be more productive. This section describes how you can integrate R with other enterprise solutions for data transformation, analysis, and reporting.  
  
##  <a name="bkmk_ssis"></a> Create Efficient Workflows that Span R and the Database  
 Data science workflows are highly iterative and involve much transformation of data, including scaling, aggregations, computation of probabilities, and renaming and merging of attributes. Data scientists are accustomed to doing many of these tasks in R, Python, or another language; however, executing such workflows on enterprise data requires seamless integration with ETL tools and processes.  
  
 Because [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] enables you to run complex operations in R via Transact-SQL and stored procedures, you can integrate R-specific tasks with existing ETL processes without minimal re-development work. Rather than perform a chain of memory-ntensive tasks in R, data preparation can be optimized using the most efficient tools, including [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] and [!INCLUDE[tsql](../../includes/tsql-md.md)].  
  
 For example, you can combine R with [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] in scenarios such as these:  
  
-   Use [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)] tasks to create necessary objects in the SQL database  
  
-   Use conditional branching to switch compute context for R jobs  
  
-   Run R jobs that generate their own data  
  
-   Use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to call an R script saved in a text variable  
  
### Samples and Resources  
 [Operationalize your machine learning project using SQL Server 2016 SSIS and R Services](https://blogs.msdn.microsoft.com/ssis/2016/01/11/operationalize-your-machine-learning-project-using-sql-server-2016-ssis-and-r-services/)  
  
 In this blog post, you'll learn how to:  
  
-   Use R in the Execute SQL Task to generate data and save it into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]  
  
-   Use  a stored procedure to train an R model and store it in the database  
  
-   Perform scoring on the model using the Script Task and the Execute SQL Task  
  
##  <a name="bkmk_ssrs"></a> Create Visualizations that Span R and Enterprise Reporting Tools  
 Although R can create charts and interesting visualization, it is not well-integrated with external data sources, meaning that each chart or graph has to be individually produced. Sharing also can be difficult.  
  
 Using [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], you can run complex operations in R via [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures, which can easily be consumed by a variety of enterprise reporting tools, including [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] and Power BI.  
  
-   Visualize graphics objects returned from an R script   
    using [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)]  
  
-   Use the table in Power BI  
  
## Samples and Resources  
 [R Graphics Device for Microsoft Reporting Services (SSRS)](https://rgraphicsdevice.codeplex.com/)  
  
 This CodePlex project provides the code to help you create a custom report item that renders the graphics output of  R as an image that can be used in [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] reports.  By using the custom report item, you can:  
  
-   Publish charts and plots created using the R Graphics Device to [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] dashboards  
  
-   Pass [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] parameters to R plots  
  
> [!NOTE]  
>  Note that the code that supports the R Graphics Device for Reporting Services must be installed on the Reporting Services server, as well as in Visual Studio. Manual compilation and configuration is also required.  
  
  
