---
title: Extensibility architecture in SQL Server Machine Learning Services | Microsoft Docs
description: External code support for the SQL Server database engine, with dual architecture for running R and Python script on relational data.
ms.prod: sql
ms.technology: machine-learning

ms.date: 09/05/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---

# Extensibility architecture in SQL Server Machine Learning Services 
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

SQL Server has an extensibility framework for loading external language runtime environments as an *extension process*. 

The architecture is designed such that external scripts run in a separate process from SQL Server, but with components for seamlessly managing a chain of requests for data and operations on SQL Server. Depending on the version of SQL Server, supported language extensions include R and Python. 

  ![Component architecture](../media/generic-architecture.png "Component architecture")

Components include a **Launchpad** service used to invoke language-specific launchers (R or Python), language and library-specific logic for loading interpreters and libraries. The Launcher loads a language run time, plus any proprietary modules. For example, if your code includes RevoScaleR functions, a RevoScaleR interpreter would load. **BxlServer** and **SQL Satellite** manage communication and data transfer with SQL Server.

Language runtimes are built on open-source distributions, which means you can port existing code and execute it in SQL Server with relatively minor modifications, assuming your script is compatible with the language versions installed by SQL Server Setup.

## Core concepts

The extensibility framework was introduced in SQL Server 2016 to support the R runtime. SQL Server 2017 adds support for Python

The purpose of the extensibility framework is to provide an interface between SQL Server and data science languages such as R and Python, reducing friction when moving data science solutions into production, and protecting data exposed during the development process. By executing a trusted scripting language within a secure framework managed by SQL Server, database administrators can maintain security while allowing data scientists access to enterprise data.

Benefits of language integration include data proximity and security, and enterprise-class tools and infrastructure.

  ![Goals of integration with SQL Server](../media/ml-service-value-add.png "Machine Learning Services Value Add")

### Security integration and data proximity

Script execution occurs within the boundaries of the SQL Server data security model. A guiding principle of the security model is that a user running external script should not be able to use any data that could not be accessed by that user in a SQL query. From a permissions perspective, this translates to standard database read permissions, plus an additional permission to run external script. Users who need to create and save stored procedures, or persist models, also need write permission. 

All tasks are secured by Windows job objects or SQL Server security. Data is kept within the compliance boundary by enforcing SQL Server security at the table, database, and instance level. At the database level, an administrator can manage resources used by external scripts, manage users, and manage and monitor external code libraries.  

### Scale and performance benefits

Integration with SQL Server is key to improving the usefulness of R and Python in the enterprise. Any R or Python script can be run by calling a stored procedure, and the results are returned as tabular results directly to SQL Server, making it easy to generate or consume machine learning from any application that can send a SQL query and handle the results.

Performance optimization relies on two equally powerful aspects of the platform: 

+ Resource governance and parallel processing using SQL Server. 

+ Distributed computing provided by the algorithms in RevoScaleR and revoscalepy. Whereas R is single-threaded, RevoScaleR and revoscalepy are multi-threaded, using available processing power to distribute a workload over multiple cores.

#### Resource governance

In SQL Server Enterprise Edition, you can use Resource Governor to manage and monitor resource use of external script operations, including R script and Python scripts. For more information, see [Resource Governance for R](../../advanced-analytics/r/resource-governance-for-r-services.md).

#### Microsoft libraries

Parallel and distributed computing requires the functions in Microsoft product libraries, such as RevoScaleR and revoscalepy. With few exceptions, you cannot make open-source or third-party functions more capable than they already are, although proximity to data offers tangible benefits when script can execute quickly, with no wait time for data transfer.

If you do not use the Revo functions, you can still get some performance improvements by running your R code in the context of SQL Server by co-location of data and script execution.

### Development and deployment

Integration with SQL Server simplifies development if you are using Visual Studio or other IDEs used ofr database development.

For deployment, code runnning in the extensibility framework is available through standard SQL Server data access methodologies. Models and code operating over relational data are wrapped in stored procedures, or serialized to a binary format and stored in a table, or loaded from disk if you serialize the raw byte stream to a file. You can query and run commands interactively using T-SQL. Or, you can write embedded SQL in other programming languages that make calls to R and Python functions running on SQL Server.

## Launchpad

The SQL Server Trusted Launchpad is a service that manages and executes external scripts, similar to the way that the full-text indexing and query service launches a separate host for processing full-text queries. The Launchpad service can start only trusted launchers that are published by Microsoft, or that have been certified by Microsoft as meeting requirements for performance and resource management.

| Trusted launchers | SQL Server versions |
|-------------------|---------------------|
| RLauncher.dll for the R language | SQL Server 2016, SQL Server 2017 |
| Pythonlauncher.dll for Python 3.5 | SQL Server 2017 |

The [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] service runs under its own user account. If you change the account that runs Launchpad, be sure to do so using SQL Server Configuration Manager, to ensure that changes are written to related files.

To execute tasks in a specific supported language, the Launchpad gets a secured worker account from the pool, and starts a satellite process to manage the external runtime. Each satellite process inherits the user account of the Launchpad and uses that worker account for the duration of script execution. If script uses parallel processes, they are created under the same, single worker account.

## BxlServer and SQL Satellite

**BxlServer** is an executable provided by Microsoft that manages communication between SQL Server and Python or R. It creates the Windows job objects that are used to contain external script sessions, provisions secure working folders for each external script job, and uses SQL Satellite to manage data transfer between the external runtime and SQL Server. If you run [Process Explorer](https://technet.microsoft.com/sysinternals/processexplorer.aspx) while a job is running, you might see one or multiple instances of BxlServer.

In effect, BxlServer is a companion to a language run time environment that works with SQL Server to transfer data and manage tasks. BXL stands for Binary Exchange language and refers to the data format used to move data efficiently between SQL Server and external processes. BxlServer is also an important part of related products such as Microsoft R Client and Microsoft R Server.

**SQL Satellite** is an extensibility API, included in the database engine starting with SQL Server 2016, that supports external code or external runtimes implemented using C or C++.

BxlServer uses SQL Satellite for these tasks:

+ Reading input data
+ Writing output data
+ Getting input arguments
+ Writing output arguments
+ Error handling
+ Writing STDOUT and STDERR back to client

SQL Satellite uses a custom data format that is optimized for fast data transfer between SQL Server and external script languages. It performs type conversions and defines the schemas of the input and output datasets during communications between SQL Server and the external script runtime.

The SQL Satellite can be monitored by using windows extended events (xEvents). For more information, see [Extended Events for R](../../advanced-analytics/r/extended-events-for-sql-server-r-services.md) and [Extended Events for Python](../../advanced-analytics/python/extended-events-for-python.md).

## Communication channels between components

Communication protocols among components and data platforms are described in this section.

+ **TCP/IP**

  By default, internal communications between SQL Server and the SQL Satellite use TCP/IP.

+ **Named Pipes**

  Internal data transport between the BxlServer and SQL Server through SQL Satellite uses a proprietary, compressed data format to enhance performance. Data is exchanged between language run times and BxlServer in BXL format, using Named Pipes.

+ **ODBC**

  Communications between external data science clients and a remote SQL Server instance use ODBC. The account that sends the script jobs to SQL Server must have both permissions to connect to the instance and to run external scripts.

  Additionally, depending on the task, the account might need these permissions:

  + Read data used by the job
  + Write data to tables: for example, when saving results to a table
  + Create database objects: for example, if saving external script as part of a new stored procedure.

  When SQL Server is used as the compute context for script executed from a remote client, and the executable must retrieve data from an external source, ODBC is used for writeback. SQL Server maps the identity of the user issuing the remote command to the identity of the user on the current instance, and runs the ODBC command using that user's credentials. The connection string needed to perform this ODBC call is obtained from the client code.

+ **RODBC (R only)** 

  Additional ODBC calls can be made inside the script by using **RODBC**. RODBC is a popular R package used to access data in relational databases; however, its performance is generally slower than comparable providers used by SQL Server. Many R scripts use embedded calls to RODBC as a way of retrieving "secondary" datasets for use in analysis. For example, the stored procedure that trains a model might define a SQL query to get the data for training a model, but use an embedded RODBC call to get additional factors, to perform lookups, or to get new data from external sources such as text files or Excel.

  The following code illustrates an RODBC call embedded in an R script:

    ```R
    library(RODBC);
    connStr <- paste("Driver=SQL Server;Server=", instance_name, ";Database=", database_name, ";Trusted_Connection=true;", sep="");
    dbhandle <- odbcDriverConnect(connStr)
    OutputDataSet <- sqlQuery(dbhandle, "select * from table_name");
    ```

+ **Other protocols**

  Processes that might need to work in "chunks" or transfer data back to a remote client can also use the [XDF file format](https://docs.microsoft.com/machine-learning-server/r/concept-what-is-xdf). Actual data transfer is via encoded blobs.

## Development and deployment

Programming or language extensions integrate with traditional RDBMS monitoring tools, development tools, and data access modalities in SQL Server.

  ![ML solution development process](media/ml-solution-development-process.png "Develop and deploy using Machine Learning Services")

+ Data stays within the compliance boundary and use of data can be managed and monitored by SQL Server database administrators. Meanwhile, the DBA has full control over who can install packages or run scripts on the server. If so desired, the DBA can also delegate permissions on a database level to data scientists or managers.

+ For development, data scientists can build and test solutions in preferred R or Python environments, disconnected from the server, and then port code to SQL Server. A SQL developer can use familiar tools such as Management Studio or Visual Studio to integrate the R or Python code with SQL Server. The tight integration means that the savvy developer can choose the best tool to optimize each task. For example, you might use SQL for some feature engineering tasks and R for others. You might embed Python script in an Integration Services task to perform sophisticated text analytics.

+ Deploying predictive solutions to the BI stack or external applications is typically through stored procedures. R and Python tutorials can help you learn the steps.

## See Also

+ [R extension in SQL Server](extension-r.md)
+ [Python extension in SQL Server](extension-py.md)