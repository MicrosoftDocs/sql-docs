---
title: Machine learning lifecycle and the team process - SQL Server Machine Learning Services
ms.prod: sql
ms.technology: machine-learning

ms.date: 03/29/2019
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Machine learning lifecycle and personas
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Machine learning projects can be complex, because they require the skills and collaboration of a disparate set of professionals. This article describes the principal tasks in the machine learning lifecycle, the type of data professionals who are engaged in machine learning, and how SQL Server supports the needs.

> [!TIP]
> 
> Before you get started on a machine learning project, we recommend that you review the tools and best practices provided by the [Microsoft Team Data Science Process](https://docs.microsoft.com/azure/machine-learning/team-data-science-process/overview), or TDSP. This process was created by machine learning consultants at Microsoft to consolidate best practices around planning and iterating on machine learning projects. The TDSP has its roots in industry standards such as CRISP-DM, but incorporates recent practices such as DevOps and visualization.

## Machine learning life cycle

Machine learning is a complex process that touches all aspects of data in the enterprise, and many machine learning projects end up taking longer and being more complex than anticipated. Here are some of the ways that machine learning requires the support of data professionals in the enterprise:

+ Machine learning begins with identification of goals and business rules.
+ Machine learning professionals must be aware of policies for storing, extracting, and auditing data.
+ Collection of potentially applicable data is next.  Data sources must be identified, and the appropriate data extracted from sensors and business applications. 
+ The quality of machine learning efforts is highly dependent on not just the type of data that is available, but the very processes used for extracting, processing, and storing data. 
+ No machine learning project would be complete without a strategy for reporting and analysis, and possibly customer engagement and feedback.

SQL Server helps bridge many of the gaps between enterprise data professionals and machine learning experts:

+ Data can be stored on-premises or in the cloud
+ SQL Server is integrated at every stage of enterprise data processing, including reporting and ETL
+ SQL Server supports data security, data redundancy, and auditing
+ Provides resource governance

## Data scientists

Data scientists use a variety of tools for data analysis and machine learning, ranging from Excel or free open-source platforms, to expensive statistical suites that require deep technical knowledge. However, using R or Python with SQL Server provides some unique benefits in comparison to these traditional tools:

+ You can develop and test a solution by using the development environment of your choice, then deploy your R or Python code as part of T-SQL code.
+ Move complex computations off the data scientist's laptop and onto the server, avoiding data movement to comply with enterprise security policies.
+ Performance and scale are improved through special R packages and APIs. You are no longer restricted by the single-threaded, memory-bound architecture of R, and can work with large datasets and multi-threaded, multi-core, multi-process computations.
+ Code portability: Solutions can run in SQL Server, or in Hadoop or Linux, using [Machine Learning Server](https://docs.microsoft.com/machine-learning-server/what-is-machine-learning-server). Code once, deploy anywhere.

## Application and database developers

Database developers are tasked with integrating multiple technologies and bringing together the results so that they can be shared throughout the enterprise. The database developer works with application developers, SQL developers, and data scientists to design solutions, recommend data management methods, and architect or deploy solutions.

Integration with SQL Server provides many benefits to data developers:

+ The data scientist can work in RStudio, while the data developer deploys the solution using SQL Server Management Studio. No more recoding of R or Python solutions.
+ Optimize your solutions by using the best of T-SQL, R, and Python. Complex operations on large datasets can be run far more efficiently using SQL Server than in R. Leverage the knowledge of your database professionals to improve the performance of machine learning solutions, by using in-memory columnstore indexes, and aggregations using SQL set-based operations. 
+ Effortlessly automate tasks that must run repeatedly on large amounts of data, such as generating prediction scores on production data. 
+ Access parameterized R or Python script from any application that uses [!INCLUDE[tsql](../../includes/tsql-md.md)]. Just call a stored procedure to train a model, generate a plot, or output predictions.
+ APIs can stream large datasets and benefit from multi-threaded, multi-core, multi-process in-database computations.

For information on related tasks, see:
+ [Operationalizing your R code](../../advanced-analytics/r/operationalizing-your-r-code.md)

### Database administrators

Database administrators must integrate competing projects and priorities into a single point of contact: the database server. They must provide data access not just to data scientists but to a variety of report developers, business analysts, and business data consumers, while maintaining the health of operational and reporting data stores. In the enterprise, the DBA is a critical part of building and deploying an effective infrastructure for data science. 

SQL Server provides unique features for the database administrator who must support the data science role:

+ Security by SQL Server: The architecture of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] keeps your databases secure and isolates the execution of external script sessions from the operation of the database instance. You can specify who has permission to execute machine learning scripts, and use database roles to manage packages.

+ R and Python sessions are executed in a separate process to ensure that your server continues to run as usual even if the external script encounters issues.

+ Resource governance using SQL Server lets you control the memory and processes allocated to external runtimes, to prevent massive computations from jeopardizing the overall server performance.

For information on related tasks, see:
+ [Managing and monitoring machine learning solutions](../../advanced-analytics/r/managing-and-monitoring-r-solutions.md)

## Architects and data engineers

Architects design integrated workflows that span all aspects of the machine learning life cycle. Data engineers design and build ETL solutions and determine how to optimize feature engineering tasks for machine learning. The overall data platform must be designed to balance competing business needs.

Because [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] is deeply integrated with other Microsoft tools such as the business intelligence and data warehouse stack, enterprise cloud and mobility tools, and Hadoop, it provides an array of benefits to the data engineer or system architect who wants to promote advanced analytics.

+ Call any Python or R script by using system stored procedures, to populate datasets, generate graphics, or get predictions. No more designing parallel workflows in data science and ETL tools. Support for Azure Data Factory and Azure SQL Database makes it easier to use cloud data sources in machine learning workflows.

+ For scheduling and management of machine learning tasks, use standard automation workflows in SQL Server, based on Integration Services, SQL Agent, or Azure Data Factory. Or, use the [operationalization features](https://docs.microsoft.com/machine-learning-server/operationalize/how-to-deploy-web-service-publish-manage-in-r) in Machine Learning Server.

For information on related tasks, see:

+ [Creating machine learning workflows in SQL Server](../../advanced-analytics/r/creating-workflows-that-use-r-in-sql-server.md)

