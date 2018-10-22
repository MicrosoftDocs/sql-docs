---
title: Set up a data science client for R development on SQL Server | Microsoft Docs
description: Install local R libraries and tools on a development workstation for remote connections to SQL Server.
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Set up a data-science client for R development on SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

After you have configured an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] to support machine learning, you should set up an R development environment that is capable of connecting to the server for remote execution and deployment.

### Evaluation and independent development
 
If you have the developer edition and plan to work locally on R script you plan to move to SQL Server, you can skip ahead to [Install an IDE](#install-ide) and point the tool to local R libraries used by SQL Server.

> [!Tip]
> For a demonstration and video walkthrough, see [Run R and Python Remotely in SQL Server from Jupyter Notebooks or any IDE](https://blogs.msdn.microsoft.com/mlserver/2018/07/10/run-r-and-python-remotely-in-sql-server-from-jupyter-notebooks-or-any-ide/) or this [YouTube video](https://youtu.be/D5erljpJDjE).

## 1 - Install R packages

R functionality in Microsoft products is multi-layered. It starts with Microsoft's open-source base R distribution, but is then extended with product-specific packages, such as [RevoScaleR](revoscaler-overview.md), that enable remote compute contexts and parallel execution of R tasks.

Coordinated operations between a client and remote server require both systems having the same packages. To get the full complement of libraries on a client workstation, install one of the software packages in the following table. 

| Library provider | Usage  |
|------------------|--------------------------------|
| [Microsoft R Client](http://aka.ms/rclient/download) |  This free download provides RevoScaleR, MicrosoftML, and other R packages, but is capped at two threads and in-memory data. However, you can still create R solutions that start locally, and shift execution (referred to as *compute context*) to access data and the computational power of a remote SQL Server instance. This is the recommended approach for client integration with a production SQL Server instance. For more information about this tool, see [What is Microsoft R Client](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client).|
| Standalone servers | Under **Shared features**, SQL Server Setup includes standalone server install options for SQL Server 2016 R Services and SQL Server 2017 machine learning. These are full-featured servers, fully decoupled from SQL Server, with the ability to connect to and consume data from multiple data platforms. But you could potentially use the software in a client capacity to access SQL Server database engine instance running R and Python tasks. [SQL Server 2017 Machine Learning Server (Standalone)](../install/sql-machine-learning-standalone-windows-install.md) has the same libraries as a SQL Server 2017 machine learning instance. [SQL Server 2016 R Server (Standalone)](../install/sql-r-standalone-windows-install.md) has the same libraries as SQL Server 2016 R Services. |


<a name="r-tool"></a>
 
## 2 - Open an R prompt

When you install R with SQL Server, you get the same R tools that are standard to any base installation of R, such as RGui, Rterm, and so forth. These tools are lightweight, useful for checking package and library information, running ad hoc commands or script, or stepping through tutorials. You can use these tools to get R version information and confirm connectivity.

To use the version of R installed with SQL Server or R Client, open an R prompt from the SQL Server or R Client program folder. The following steps are for R Client and RGui.exe.

1. For R Client, go to `~\Program Files\Microsoft\R Client\R_SERVER\bin\x64`.
2. Double-click **RGui.exe** to start an R session with an R command prompt.

When you start an R session from a Microsoft program folder, several packages, including RevoScaleR, load automatically. Enter **search()** at the R prompt for confirmation.

   ![Version information when loading R](../install/media/rclient-rgui-r-prompt.png "Open an R prompt")

### Tool list and location

| Tool | Description | 
|------|-------------|
| **RTerm**: | A command-line terminal for running R scripts | 
| **RGui.exe** | A simple interactive editor for R. The command-line arguments are the same for RGui.exe and RTerm. |
| **RScript** | A command-line tool for running R scripts in batch mode. |

Tools are located in **bin** folder for base R as installed SQL Server or R Client. The following paths are valid locations for the tools, depending on which product version and feature you installed:

| Microsoft product | R tool location |
|-------------------|-----------------|
| Microsoft R Client | `~\Program Files\Microsoft\R Client\R_SERVER\bin\x64` |
| Microsoft Machine Learning (R) Server | `~\Program Files\Microsoft\R_SERVER\bin\x64`
| SQL Server 2016 R Services | `~\Program Files\Microsoft SQL Server\MSSQL13.<instancename>\R_SERVICES\bin\x64`|
| SQL Server 2016 R Standalone Server | `~\Program Files\Microsoft SQL Server\130\R_SERVER\bin\x64` 
| SQL Server 2017 Machine Learning (R) Services | `~\Program Files\Microsoft SQL Server\MSSQL14.<instancename>\R_SERVICES\bin\x64`|
| SQL Server 2017 Machine Learning (R) Standalone Server | `~\Program Files\Microsoft SQL Server\140\R_SERVER\bin\x64` |

## 3 - Permissions

To connect to an instance of SQL Server to run scripts and upload data, you must have a valid login on the database server. You can use either a SQL login or integrated Windows authentication. We generally recommend that you use Windows integrated authentication, but using the SQL login is simpler for some scenarios.

At a minimum, the account used to run code must have permission to read from the databases you are working with, plus the special permission EXECUTE ANY EXTERNAL SCRIPT. Most developers also require permissions to create new objects in the form of stored procedures containing your script, and write data into tables containing training data or scored data. 

Ask the database administrator to configure the following permissions for the account, in the database where you use R:

+ **EXECUTE ANY EXTERNAL SCRIPT** to run R on the server.
+ **db_datareader** privileges to run the queries used for training the model.
+ **db_datawriter** to write training data or scored data.
+ **db_owner** to create objects such as stored procedures, tables, functions. 
  You also need **db_owner** to create sample and test databases. 

If your code requires packages that are not installed by default with SQL Server, arrange with the database administrator to have the packages installed with the instance. SQL Server is a secured environment and there are restrictions on where packages can be installed. Ad hoc installation of packages as part of your code is not recommended, even if you have rights. Also, always carefully consider the security implications before installing new packages in the server library.

> [!Tip]
> If you are unfamiliar with SQL Server and working in a local development environment, you can step through this tutorial to learn about logins and setting permissions: [Deep-dive into RevoScaleR](../tutorials/deepdive-data-science-deep-dive-using-the-revoscaler-packages.md)

## 4 - Test connections

SQL Server must be enabled for [remote connections](https://docs.microsoft.com/sql/database-engine/configure-windows/view-or-configure-remote-server-connection-options-sql-server.md) and you must have permissions, including a user login and a database to connect to. The following steps assume the demo database, [NYCTaxi_Sample](../tutorials/demo-data-nyctaxi-in-sql.md) and Windows authentication.

 As a verification step, use a built-in tool and RevoScaleR to confirm connectivity to the remote server.

1. Start by [opening an R tool](#r-tool) on the client workstation. RevoScaleR loads automatically. For example, go to `~\Program Files\Microsoft SQL Server\140\R_SERVER\bin\x64` and double-click **RGui.exe** to start it.

2. Confirm RevoScaleR is operational by running this command, which returns statistical summary on a demo dataset. Make sure you provide a valid server name for the SQL Server database engine instance.

```r
connStr <- "Driver=SQL Server;Server=<your-server-name>;Database=NYCTaxi_Sample;Trusted_Connection=true"
testQuery <-"SELECT DISTINCT TOP(100) tip_amount FROM [dbo].nyctaxi_sample ORDER BY tip_amount DESC;"
cc <-RxInSqlServer(connectionString=connStr)
rxSummary(formula = ~ ., data = RxSqlServerData(sqlQuery=testQuery, connectionString=connStr), computeContext=cc)
```
This script connects to a database on the remote server, provides a query, creates a compute context `cc` instruction for remote code execution, then provides the RevoScaleR function **rxSummary** to return a statistical summary of the query results.

<a name="install-ide"></a>

## 5 - Install an IDE

For sustained and serious development projects, you should install an integrated development environment (IDE). SQL Server tools and the built-in R tools are not equipped for heavy R development. Once you have working code, you can deploy it as a stored procedure for execution on SQL Server.

You should point your IDE to the local R libraries: base R, RevoScaleR, and so forth. Running workloads on a remote SQL Server occurs during script execution, when your script invokes a remote compute context on SQL Server, accessing data and operations on that server.

### RStudio

When using [RStudio](https://www.rstudio.com/), you can configure the environment to use the R libraries and executables that correspond to those on a remote SQL Server.

1. Check R package versions installed on SQL Server. For more information, see [Get R package information](determine-which-packages-are-installed-on-sql-server.md#get-the-r-library-location).

1. Install Microsoft R Client or one of the standalone server options to add RevoScaleR and other R packages, including the base R distribution used by your SQL Server instance. Choose a version at the same level or lower (packages are backward compatible) that provides the same package versions as those on the server. For version information, see the version map in this article: [Upgrade R and Python components](use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

1. In RStudio, [update your R path](https://support.rstudio.com/hc/articles/200486138-Using-Different-Versions-of-R) to point to the R environment providing RevoScaleR, Microsoft R Open, and other Microsoft packages. Depending on how you obtained RevoScaleR and other libraries, it is most likely one of the following paths:

  + C:\Program Files\Microsoft SQL Server\130\R_SERVER\Library
  + C:\Program Files\Microsoft SQL Server\140\R_SERVER\Library
  + C:\Program Files\Microsoft\R Client\R_SERVER\bin\x64

### R Tools for Visual Studio (RTVS)

If you don't already have a preferred IDE for R, we recommend **R Tools for Visual Studio**.

+ [Download R Tools for Visual Studio (RTVS)](https://visualstudio.microsoft.com/vs/features/rtvs/)
+ [Installation instructions](https://docs.microsoft.com/visualstudio/rtvs/installing-r-tools-for-visual-studio) - RTVS is available in several versions of Visual Studio.
+ [Get started with R Tools for Visual Studio](https://docs.microsoft.com/visualstudio/rtvs/getting-started-with-r)

### Connect to SQL Server from RTVS

This example uses Visual Studio 2017 Community Edition, with the data science workload installed.

1. From the **File** menu, select **New** and then select **Project**.

2. The left-hand pane contains a list of preinstalled templates. Click **R**, and select **R Project**. In the **Name** box, type `dbtest` and click **OK**. 

  Visual Studio creates a new project folder and a default script file, `Script.R`. 

3. Type `.libPaths()` on the first line of the script file, and then press CTRL + ENTER.

  The current R library path should be displayed in the **R Interactive** window. 

4. Click the **R Tools** menu and select **Windows** to see a list of other R-specific windows that you can display in your workspace.
 
    + View help on packages in the current library by pressing CTRL + 3.
    + See R variables in the **Variable Explorer**, by pressing CTRL + 8.

5. Create a connection string to a SQL Server instance, and use the connection string in the RxInSqlServer constructor to create a SQL Server data source object. 

    ```r
    connStr <- "Driver=SQL Server;Server=MyServer;Database=MyTestDB;Uid=;Pwd="
    sqlShareDir <- paste("C:\\AllShare\\", Sys.getenv("USERNAME"), sep = "")
    sqlWait <- TRUE
    sqlConsoleOutput <- FALSE
    cc <- RxInSqlServer(connectionString = connStr, shareDir = sqlShareDir, wait = sqlWait, consoleOutput = sqlConsoleOutput)
    sampleDataQuery <- "SELECT TOP 100 * from [dbo].[MyTestTable]"
    inDataSource <- RxSqlServerData(sqlQuery = sampleDataQuery, connectionString = connStr, rowsPerRead = 500)
    ```
    > [!TIP]
    > To run a batch, select the lines you want to run and press CTRL + ENTER.

6. Set the compute context to the server, and then run some simple R code on the data.

    ```r
    rxSetComputeContext(cc)
    rxGetVarInfo(data = inDataSource)
    ```

    Results are returned in the **R Interactive** window.
    
    If you want to assure yourself that the code is being executed on the SQL Server instance, you can use Profiler to create a trace.

## Next steps

Two different tutorials include exercises so that you can practice switching the compute context from local to a remote SQL Server instance.

+ [Tutorial: Use RevoScaleR R functions with SQL Server data](../tutorials/deepdive-data-science-deep-dive-using-the-revoscaler-packages.md)
+ [Data Science End-to-End Walkthrough](../tutorials/walkthrough-data-science-end-to-end-walkthrough.md)