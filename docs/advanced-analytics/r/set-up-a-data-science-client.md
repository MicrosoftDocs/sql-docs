---
title: Set up a data science client for R development on SQL Server | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/15/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Set up a data-science client for R development on SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

After you have configured an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] to support machine learning, you should set up a development environment that is capable of connecting to the server for remote execution and deployment.

This article describes some typical client scenarios, including configuration of the free Visual Studio Community edition to run R code in SQL Server.

## Install R libraries on the client

Your client environment must include Microsoft R Open, as well as the additional RevoScaleR packages that support distributed execution of R on SQL Server. Standard distributions of R do not have the packages that support remote compute contexts or parallel execution of R tasks.

To get these libraries, install any of the following, but choose an option that most closely matches your SQL Server installation. Doing so ensures that client libraries match equivalent libraries on the server. Developer editions of the standalone servers are free of charge for development purposes. 

+ [SQL Server 2017 Machine Learning Server (Standalone)](../install/sql-machine-learning-standalone-windows-install.md)
+ [SQL Server 2016 R Server (Standalone)](../install/sql-r-standalone-windows-install.md)

[Microsoft R Client](http://aka.ms/rclient/download) is a free download that gives you access to the RevoScaleR packages for development use. By installing R Client, you can create R solutions that can be run in all supported compute contexts, including SQL Server in-database analytics, and distributed R computing on Hadoop, Spark, or Linux using Machine Learning Server. In production environments where SQL Server is a central hub for in-database analytics, you can install R Client on computer workstations. Libraries installed with R Client execute locally, but can shift computations to the remote SQL Server. For more information, see [What is Microsoft R Client](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client).

## Built-in R tools

When you install R with SQL Server, you get the same R tools that are installed with any **base** installation of R, such as RGui, Rterm, and so forth. Therefore technically, you have all the tools you need to develop and test R code.

The following standard R tools are included in a *base installation* of R, and therefore are installed by default. The path assumes a default SQL Server instance and the latest version.

| Tool | Description | 
|------|-------------|
| **RTerm**: | A command-line terminal for running R scripts | 
| **RGui.exe** | A simple interactive editor for R. The command-line arguments are the same for RGui.exe and RTerm. |
|**RScript** | A command-line tool for running R scripts in batch mode. |

Tools are located in **bin** folder for base R as installed SQL Server or R Client. The following paths are valid locations for the tools, depending on which product version and feature you installed:

+ SQL Server 2016 R Services: `~\Program Files\Microsoft SQL Server\MSSQL13.<instancename>\R_SERVICES\bin\x64`
+ SQL Server 2016 R Server Standalone: `~\Program Files\Microsoft SQL Server\130\R_SERVER\bin\x64`
+ SQL Server 2017 Machine Learning Services: `~\Program Files\Microsoft SQL Server\MSSQL14.<instancename>\R_SERVICES\bin\x64`
+ SQL Server 2017 Machine Learning Server (Standalone): `~\Program Files\Microsoft SQL Server\140\R_SERVER\bin\x64`

## Install an IDE

### RStudio

When using [RStudio](https://www.rstudio.com/), you can configure the environment to use the R libraries and executables that correspond to those on a remote SQL Server.

1. Check R package versions installed on SQL Server. For more information, see [Get R package information](determine-which-packages-are-installed-on-sql-server.md#get-the-r-library-location).

1. Install Microsoft R Client or a standalone server to add RevoScaleR and other R packages, including the base R distribution used by your SQL Server instance. Choose a version at the same level or lower (packages are backward compatible) that provides the same package versions as those on the server.

1. In RStudio, [update your R path](https://support.rstudio.com/hc/articles/200486138-Using-Different-Versions-of-R) to point to the R environment providing RevoScaleR, Microsoft R Open, and other Microsoft packages. Depending on how you obtained RevoScaleR and other libraries, it is most likely one of the following paths:

  + C:\Program Files\Microsoft SQL Server\130\R_SERVER\Library
  + C:\Program Files\Microsoft SQL Server\140\R_SERVER\Library
  + C:\Program Files\Microsoft\R Client\R_SERVER\bin\x64

### R Tools for Visual Studio (RTVS)

If you don't already have a preferred integrated development environment for R, we recommend **R Tools for Visual Studio**.

+ [Download R Tools for Visual Studio (RTVS)](https://visualstudio.microsoft.com/vs/features/rtvs/)
+ [Installation instrructions](https://docs.microsoft.com/visualstudio/rtvs/installing-r-tools-for-visual-studio) - RTVS is available in several versions of Visual Studio.
+ [Get started with R Tools for Visual Studio](https://docs.microsoft.com/visualstudio/rtvs/getting-started-with-r)

### Connect to SQL Server from RTVS

This example uses Visual Studio 2017 Community Edition, with the data science workload installed.

1. From the **File** menu, select **New** and then select **Project**.

2. The -hand pane contains a list of preinstalled templates. Click **R**, and select **R Project**. In the **Name** box, type `dbtest` and click **OK**.

3. Visual Studio creates a new project folder and a default script file, `Script.R`. 

4. Type `.libPaths()` on the first line of the script file, and then press CTRL + ENTER.

5. The current R library path should be displayed in the **R Interactive** window. 

6. Click the **R Tools** menu and select **Windows** to see a list of other R-specific windows that you can display in your workspace.
 
    + View help on packages in the current library by pressing CTRL + 3.
    + See R variables in the **Variable Explorer**, by pressing CTRL + 8.

7. Create a connection string to a SQL Server instance, and use the connection string in the RxInSqlServer constructor to create a SQL Server data source object. 

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

8. Set the compute context to the server, and then run some simple R code on the data.

    ```r
    rxSetComputeContext(cc)
    rxGetVarInfo(data = inDataSource)
    ```

    Results are returned in the **R Interactive** window.
    
    If you want to assure yourself that the code is being executed on the SQL Server instance, you can use Profiler to create a trace.

## See also

+ [What is Microsoft R Client](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client)