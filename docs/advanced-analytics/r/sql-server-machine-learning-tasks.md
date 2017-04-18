---
title: "SQL Server Machine LEarning Tasks | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "04/16/2017"
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
# SQL Server Machine Learning Tasks

[!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] combines the power and flexibility of the open source R language with enterprise-level tools for data storage and management, workflow development, and reporting and visualization. This topic describes the machine learning lifecycle, and how SQL Server supports the needs of four different data professionals who are enagged in machine learning.

## Machine Learning Life Cycle

Machine learning is not a short-term task, but rather a long-term process that touches all aspects of data in the enterprise. Machine learning begins with identification of business goals and rules, and collection of data from sensors and business applications. Machine learning is highly dependent on processes for extracting, processing, and storing data, and is increasingly important when considering policies for storing, extracting, and auditing data. Finally, machine learning is now an important omponent of strategies for reporting and analysis, as well as customer engagement and feedback.



SQL Server is an ideal fit for machine learning, because it bridges many of the gaps in the machine learning process:

+ Works on-premises or in the cloud
+ Integrated at every stage of enterprise data processing, including business intelligence
+ Supports improved data security
+ Provides resource governance and auditing

## Data Professionals And How they Use Machine Learning

### Data Scientists

Data scientists have access to a variety of tools for data analysis and machine learning, ranging from Excel or free open-source platforms, to expensive statistical suites that require deep technical knowledge. However, integration with SQL Server provides unique benefits:

+ Develop and test your solutions by using the R development environment of your choice.
+ Push computations to the database, avoiding data movement while complying with enterprise security policies.
+ Performance and scale are improved through special R packages and APIs. You are no longer restricted by the single-threaded, memory-bound architecture of R, and can work with large datasets and multi-threaded, multi-core, multi-process computations.
+ R code can be easily deployed to production and called by enterprise tools, applications, other databases, and dashboards.
+ Data scientists can deploy and update an analytical solution while meeting standard requirements for enterprise data management, including security and access auditing
+ Code portability: easily re-use your R code against other data sources, such as Hadoop

### Application and Database Developers

Database developers are tasked with integrating multiple technologies and bringing together the results so that they can be shared throughout the enterprise. The database developer works with application developers, SQL developers, and data scientists to design solutions, recommend data management methods, and architect and deploy solutions. 

Integration with SQL Server provides these benefits to data developers who work with machine learning:

+ Use familiar tools to interact with R scripts. Let the data scientist work in RStudio while the data developer deploys the solution using SQL Server Management Studio. No more recoding of R or Python solutions.
+ Optimize by mixing and matching SQL and R, or SQL and Python. Many times, complex operations on large datasets can be run far more efficiently using SQL Server features, such as in-memory columnstoreindexes, or very fast aggregates in T-SQL. Use a machine learning language where it makes sense, and use SQL to move and process data.
+ Effortlessly automate tasks that must run repeatedly on large amounts of data, such as generating prediction scores on production data.
+ Execute R or Python script from any application that uses [!INCLUDE[tsql](../../includes/tsql-md.md)]. Just call a stored procedure to create a parameterized model, generate a complex plot, or output predictions.
+ The **RevoScaleR** and **revoscalepy** APIs can operate on large datasets and benefit from multi-threaded, multi-core, multi-process in-database computations.

For information on related tasks, see:
+ [Operationalizing Your R Code](../../advanced-analytics/r-services/operationalizing-your-r-code.md)

### Database Administrators

Database administrators must integrate competing projects and priorities into a single point of contact: the database server. They must provide data access not just to data scientists but to a variety of report developers, business analysts, and business data consumers, while maintaining the health of operational and reporting data stores. In the enterprise, the DBA is a critical part of building and deploying an effective infrastructure for data science. 

SQL Server provides unique features for the database administrator who must support the data science role:

+ Security by SQL Server: The architecture of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] keeps your databases secure and isolates the execution of external script sessions from the operation of the database instance. You can specify who has permission to execute machine learning scripts, and who can install new R packages, using database roles.

+ R and Python sessions are executed in a separate process to ensure that your server continues to run as usual even if the external script encounters issues.

+ Resource governance using SQL Server lets you control the memory and processes allocated to external runtimes, to prevent massive computations from jeopardizing the overall server performance.

For information on related tasks, see:
+ [Managing and Monitoring R Solutions](../../advanced-analytics/r-services/managing-and-monitoring-r-solutions.md)

### Architects and ETL Designers

Architects design integrated workflows that span all aspects of the machine learning life cycle. Data engineers design and build ETL solutions and determine how to optimize feature engineering tasks tht are part of the machine learning process. Often, the overall data platform must be designed to balance competing and complementary business needs.

Because [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] is deeply integrated with other Microsoft tools such as the business intelligence and data warehouse stack, enterprise cloud and mobility tools, and Hadoop, it provides an array of benefits to the data engineer or system architect who wants to promote advanced analytics.

+ Familiar development tools for developng R and Python solutions. You can call any Python or R script by using system stored procedures, to populate datasets, generate graphics, or get predictions. No more designing parallel workflows in data science and ETL tools. Support for Azure Data Factory and Azure SQL Database makes it easier to transform and manage data, and use cloud data sources in machine learning workflows.

+ Scheduling and management using operationalization features in Microsoft R Server.

For information on related tasks, see:

+ [Creating Workflows that Use R in SQL Server](../../advanced-analytics/r-services/creating-workflows-that-use-r-in-sql-server.md)

