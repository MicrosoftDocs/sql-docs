---
title: "SQL Server R Services Features and Tasks | Microsoft Docs"
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
ms.assetid: 52ad3f10-6d24-477a-aeb6-110456b2ed1c
caps.latest.revision: 13
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# SQL Server R Services Features and Tasks
  [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] combines the power and flexibility of the open source R language with enterprise-level tools for data storage and management, workflow development, and reporting and visualization. It supports the needs of four different data professionals and scenarios.  
  
## Data Scientists: Analyze, Model, and Score using R and SQL Server  
 Data scientists have access to a variety of tools for data analysis and machine learning, ranging from the familiar Excel and open source platforms, to expensive statistical suites that require deep technical knowledge. Among these, [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] provides unique benefits because  it allows the data scientist to push computations to the database, avoiding data movement and complying with enterprise security policies. Moreover,  the R code created by the data scientist can easily be deployed to production and called by familiar enterprise tools and solutions:  applications based on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases, BI reporting tools, and dashboards. Using [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], the data scientist can deploy and update an analytical solution while meeting standard requirements for enterprise data management, including security, ease of deployment, management and monitoring, and access auditing.  
  
-   **Familiar user interface.**  Develop and test your solutions by using the R development environment of your choice.  
  
-   **In-database processing.**  Execute R code and have the computations take place on the computer that hosts the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. This eliminates the need to move data.  
  
-   **Performance and scale.**  Includes  scalable R packages and APIs, so you are no longer restricted by the single-threaded, memory-bound architecture of R. You can work with large datasets and multi-threaded, multi-core, multi-process computations.  
    
-   **Code portability.**  The same R code that you run against [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data can be easily re-used against other data sources, such as Hadoop.  
  
 For  detailed information on related tasks and concepts, see [Data Exploration and Predictive Modeling with R](../../advanced-analytics/r-services/data-exploration-and-predictive-modeling-with-r.md).  
  
## Application and Database Developers: Deploy R Solutions  
 Database developers are tasked with integrating multiple technologies and bringing together the results so that they can be shared throughout the enterprise. The database developer works with application developers, SQL developers, and data scientists to design solutions, recommend data management methods, and architect and deploy solutions. [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] provides many benefits to developers who work with data scientists:  
  
-   **Interact with R scripts using [!INCLUDE[tsql](../../includes/tsql-md.md)].**  Your report developers, analysts, and database developers can invoke R scripts by calling system stored procedures. If your solution uses complex aggregations or involves large datasets, you can have computations execute in-database, or use a mix of R and [!INCLUDE[tsql](../../includes/tsql-md.md)], depending on which provides the best performance. The effortless integration with  [!INCLUDE[tsql](../../includes/tsql-md.md)] is especially welcome when you need to run tasks repeatedly on large amounts of data, such as generating prediction scores on production data.  
  
     Integration with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] also means that you can execute R scripts from any application that uses [!INCLUDE[tsql](../../includes/tsql-md.md)]. For example, you can easily call a stored procedure from a [!INCLUDE[ssRSnoversion](../../includes/ssrsnoversion-md.md)] report to invoke R script, and generate a plot along with the predictions in the report.  
  
-   **Performance and scale.**  Although the open source R language is known to have limitations, the RevoScaleR package APIs provided by [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] can operate on large datasets and benefit from multi-threaded, multi-core, multi-process in-database computations.  
  
-   **Standard development and management tools.**  No additional tools for administration or deployment required; all R jobs can be called by invoking a stored procedure. Moreover, the same R code that you run against [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data can be used against other data sources, such as Hadoop.  
  
 For  detailed information on related tasks and concepts, see [Operationalizing Your R Code](../../advanced-analytics/r-services/operationalizing-your-r-code.md).  
  
## Database Administrators: Manage Advanced Analytics Solutions  
 Database administrators must integrate competing projects and priorities into a single point of contact: the database server. They must provide data access not just to data scientists but to a variety of report developers, business analysts, and business data consumers, while maintaining the health of operational and reporting data stores. In the enterprise, the DBA is a critical part of building and deploying an effective infrastructure for data science. [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] provides many benefits to the database administrator who supports the data science role:  
  
-   **Security.**  The architecture of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] keeps your databases secure and isolates the execution of R sessions from the operation of the database instance.  
  
     You can specify who has permission to execute R scripts and ensure that the data used in R jobs is managed using the same security roles that are defined in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
-   **Reliability.**  R sessions are executed in a separate process to ensure that your server continues to run as usual even if the R session encounters issues.  
  
-   **Resource governance.**  You can control the amount of resources allocated to the R runtime, to prevent massive computations from jeopardizing the overall server performance.  
  
 For  detailed information on related tasks and concepts, see [Managing and Monitoring R Solutions](../../advanced-analytics/r-services/managing-and-monitoring-r-solutions.md).  
  
## Architects and ETL Designers: Create Integrated Workflows that Span R and SQL Server  
 Data engineers design and build ETL solutions. Architects design data platforms that serve competing and complementary business needs. Because [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] is deeply integrated with other Microsoft tools such as the business intelligence and data warehouse stack, enterprise cloud and mobility tools, and Hadoop, it provides an array of benefits to the data engineer or system architect who wants to promote advanced analytics.  
  
-   **Broad set of familiar development tools.**  When developing R solutions, use [!INCLUDE[tsql](../../includes/tsql-md.md)] and system stored procedures to populate datasets, run R jobs, or get predictions. No more designing parallel workflows in data science tools; build your data pipelines in the familiar environment of [!INCLUDE[ssISnoversion](../../includes/ssisnoversion-md.md)].  
  
     Need to use cloud data? Support for Azure Data Factory and Azure SQL Database makes it easier to transform and manage data, and use cloud data sources in workflows just like those for on-premise [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data.  
  
-   **Author and manage workflows.**  Schedule jobs and author workflows containing R scripts, using system stored procedures.  
  
 For  detailed information on related tasks and concepts, see [Creating Workflows that Use R in SQL Server](../../advanced-analytics/r-services/creating-workflows-that-use-r-in-sql-server.md).  
  
## See Also  
 [Getting Started with SQL Server R Services](../../advanced-analytics/r-services/getting-started-with-sql-server-r-services.md)  
  
  
