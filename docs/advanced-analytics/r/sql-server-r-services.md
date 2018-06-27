---
title: SQL Server Machine Learning and R Services (In-Database) | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# SQL Server Machine Learning and R Services (In-Database)
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

An in-database installation of machine learning  operates within the context of a SQL Server database engine instance, providing R and Python external script support for resident data in your SQL Server instance. Because machine learning is integrated with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can keep analytics close to the data and eliminate the costs and security risks associated with data movement.

Because the database engine is multi-instance, you can install more than one instance of in-database analytics, or even older and newer versions side-by-side. Choices include either [SQL Server 2017 Machine Learning Services (In-Database)](../install/sql-machine-learning-standalone-windows-install.md) with R and Python, or [SQL Server 2016 R Services (In-Database)](../install/sql-r-standalone-windows-install.md) with just R. 

Machine learning components can also be installed as instance-agnostic [standalone servers](r-server-standalone.md). Generally, we recommend that you treat (Standalone) and (In-Database) installations as mutually exclusive to avoid resource contention, but if you have sufficient resources, there are no prohibitions against installing them both on the same physical computer.

## Choosing between in-database and standalone analytics

Understanding your development requirements can help you choose between (In-Database) and (Standalone) approaches. A standalone server is simpler to configure and manage if you want maximum flexibility in how it is used, or if you want to also connect to a variety of data sources outside of SQL Server. 

In-database analytics are designed for deep integration with data within SQL Server. You can write T-SQL queries that call R or Python functions and execute the script in SQL Server Management Studio or any tool or app used for external or embedded T-SQL. If you need to run R or Python code in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], either by using stored procedures or by using the SQL Server instance as the [compute context](https://docs.microsoft.com/machine-learning-server/r/concept-what-is-compute-context), you must install in-database analytics. This option provides maximum data security and integration with SQL Server tools.

Both in-database and standalone servers can alleviate the memory and processing constraints of open-source R and Python. Both options include the same packages and tools, with the ability to load and process large amounts of data on multiple cores and aggregate the results into a single consolidated output. The functions and algorithms are engineered for both scale and utility: delivering predictive analytics, statistical modeling, data visualizations, and leading-edge machine learning algorithms in a commercial server product engineered and supported by Microsoft. 

## Components of an in-database installation

SQL Server 2016 is R only. SQL Server 2017 supports R and Python. The following table describes the features in each version. With the exception of the SQL Server Launchpad service, this table is identical to the one provided in the [standalone server article](r-server-standalone.md).

| Component | Description |
|-----------|-------------|
| SQL Server Launchpad service | A service that manages communications between the external R and Python runtimes and the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance. |
| R packages | [RevoScaleR](revoscaler-overview.md) is the primary library for scaleable R with functions for data manipulation, transformation, visualzation, and analysis.  <br/>[MicrosoftML (R)](https://docs.microsoft.com/machine-learning-server/r-reference/microsoftml/microsoftml-package) adds machine learning algorithms to create custom models for text analysis, image analysis, and sentiment analysis. <br/>[mrsdeploy](operationalization-with-mrsdeploy.md) offers web service deployment (in SQL Server 2017 only). <br/>[olapR](how-to-create-mdx-queries-using-olapr.md) is for specifying MDX queries in R.|
| Microsoft R Open (MRO) | [MRO](https://mran.microsoft.com/open) is Microsoft's open-source distribution of R. The package and interpreter are included. Always use the version of MRO bundled in setup. |
| R tools | R console windows and command prompts are standard tools in an R distribution. Find them at \Program files\Microsoft SQL Server\140\R_SERVER\bin\x64. |
| R Samples and scripts |  Open-source R and RevoScaleR packages include built-in data sets so that you can create and run script using pre-installed data. Look for them at \Program files\Microsoft SQL Server\140\R_SERVER\library\datasets and \library\RevoScaleR. |
| Python packages | [revoscalepy](../python/what-is-revoscalepy.md) is the primary library for scaleable Python with functions for data manipulation, transformation, visualzation, and analysis. <br/>[microsoftml (Python)](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package) adds machine learning algorithms to create custom models for text analysis, image analysis, and sentiment analysis.  |
| Python tools | The built-in Python command line tool is useful for ad hoc testing and tasks. Find the tool at \Program files\Microsoft SQL Server\140\PYTHON_SERVER\python.exe. |
| Anaconda | Anaconda is an open-source distribution of Python and essential packages. |
| Python samples and scripts | As with R, Python includes built-in data sets  and scripts. Find the revoscalepy data at \Program files\Microsoft SQL Server\140\PYTHON_SERVER\lib\site-packages\revoscalepy\data\sample-data. |
| Pre-trained models in R and Python | Pre-trained models are supported and usable on a standalone server, but you cannot install them through SQL Server Setup. The setup program for Microsoft Machine Learning Server provides the models, which you can install free of charge. For more information, see [Install pretrained machine learning models on SQL Server](install-pretrained-models-sql-server.md). |

## Get started step-by-step

Start with setup, attach the binaries to your favorite development tool, and write your first script.

### Step 1: Install the software

Install either one of these versions:

+ [SQL Server 2017 Machine Learning Services (In-Database)](../install/sql-machine-learning-services-windows-install.md)

+ [SQL Server 2016 R Services (In-Database) - R only](../install/sql-r-services-windows-install.md)
 
### Step 2: Configure a development tool

Configure your development tools to use the Machine Learning Server binaries. For more information about Python, see [Link Python binaries](https://docs.microsoft.com/machine-learning-server/python/quickstart-python-tools). For instructions on how to connect in R Studio, see [Using Different Versions of R](https://support.rstudio.com/hc/en-us/articles/200486138-Using-Different-Versions-of-R) and point the tool to C:\Program Files\Microsoft SQL Server\140\R_SERVER\bin\x64. You could also try [R Tools for Visual Studio](https://docs.microsoft.com/visualstudio/rtvs/installation). 

Data scientists typically use R or Python on their own laptop or development workstation, to explore data, and build and tune predictive models until a good predictive model is achieved. 

With in-database analytics in SQL Server, there is no need to change this process. After installation is complete, you can run R or Python code on [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] either locally or remotely:

![rsql_keyscenario2](media/rsql-keyscenario2.png) 

+ **Use the IDE you prefer**. [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] client components provide the data scientist with all the tools needed to experiment and develop. These tools include the R runtime, the Intel math kernel library to boost the performance of standard R operations, and a set of enhanced R packages that support executing R code in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  

+ **Work remotely or locally**. Data scientists can connect to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and bring the data to the client for local analysis, as usual. However, a better solution is to use the **RevoScaleR** or **revoscalepy** APIs to push computations to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer, avoiding costly and insecure data movement.

+ **Embed R or Python scripts in [!INCLUDE[tsql](../../includes/tsql-md.md)] stored procedures**. When your code is fully optimized, wrap it in a stored procedure to avoid unnecessary data movement and optimize data processing tasks.

### Step 3: Write your first script

Call R or Python functions from within T-SQL script:
  
  + [R: Use R code in Transact-SQL](../tutorials/rtsql-using-r-code-in-transact-sql-quickstart.md) 
  + [R: In-database analytics for SQL developers](../tutorials/sqldev-in-database-r-for-sql-developers.md)
  + [Python: Run Python using T-SQL](../tutorials/run-python-using-t-sql.md)
  + [Python: In-database analytics for SQL developers](../tutorials/sqldev-in-database-python-for-sql-developers.md)

Choose the best language for the task. R is best for statistical computations that are difficult to implement using SQL. For set-based operations over data, leverage the power of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to achieve maximum performance. Use the in-memory database engine for very fast computations over columns.

### Step 4: Optimize your solution

When the model is ready to scale on enterprise data, the data scientist often works with the DBA or SQL developer to optimize processes such as:

+ Feature engineering
+ Data ingestion and data transformation
+ Scoring

Traditionally data scientists using R have had problems with both performance and scale, especially when using large dataset. That is because the common runtime implementation is single-threaded and can accommodate only those data sets that fit into the available memory on the local computer. Integration with SQl Server Machine Learning Services provides multiple features for better performance, with more data:

+ **RevoScaleR**: This R package contains implementations of some of the most popular R functions, redesigned to provide parallelism and scale. The package also includes functions that further boost  performance and scale by pushing computations to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] computer, which typically has far greater memory and computational power.

+ **revoscalepy**. This Python library, available in SQL Server 2017, implements the most popular functions in RevoScaleR, such as remote compute contexts, and many algorithms that support distributed processing.

**Resources**

+ [Performance Case Study](../../advanced-analytics/r/performance-case-study-r-services.md)
+ [R and Data Optimization](../../advanced-analytics/r/r-and-data-optimization-r-services.md)

### Step 5: Deploy and Consume

After the script or model is ready for production use, a database developer might embed the code or model in a stored procedure, so that the saved R or Python code can be called from an application. Storing and running R code from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] has many benefits: you can use the convenient [!INCLUDE[tsql](../../includes/tsql-md.md)] interface, and all computations take place in the database, avoiding unnecessary data movement.

![rsql_keyscenario1](media/rsql-keyscenario1.png)

+ **Secure and extensible**. [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] uses a new extensibility architecture that keeps your database engine secure and isolates R and Python sessions. You also have control over the users who can execute scripts, and you can specify which databases can be accessed by code. You can control the amount of resources allocated to the runtime, to prevent massive computations from jeopardizing the overall server performance.

+ **Scheduling and auditing**. When external script jobs are run in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], you can control and audit the data used by data scientists. You can also schedule jobs and author workflows containing external R or Python scripts, just like you would schedule any other T-SQL job or stored procedure.

To take advantages of the resource management and securty features in SQL Server, the deployment process might include these tasks:

+ Converting yourcode to a function that can run optimally in a stored procedure
+ Setting up security and locking down packages used by a particular task
+ Enabling resource governance (requires the Enterprise edition)

**Resources**

+ [Resource Governance for R](resource-governance-for-r-services.md)
+ [R Package Management for SQL Server](install-additional-r-packages-on-sql-server.md)

## See also

 [SQL Server Machine Learning and R Server (Standalone)](sql-server-r-services.md)
