---
title: "Set up a data science client for use with SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "10/31/2017"
ms.reviewer: 
ms.suite: sql
ms.prod: machine-learning-services
ms.prod_service: machine-learning-services
ms.component: r
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: d15ee956-918f-40e0-b986-2bf929ef303a
caps.latest.revision: 14
author: "jeannt"
ms.author: "jeannt"
manager: "cgronlund"
---
# Set up a data science client for use with SQL Server

After you have configured an instance of [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] to support machine learning, you should set up a development environment that is capable of connecting to the server for remote execution and deployment.

This article describes some typical client scenarios, including configuration of the free Visual Studio Community edition to run R code in SQL Server.

## Install R libraries on the client

Your client environment must include Microsoft R Open, as well as the additional RevoScaleR packages that support distributed execution of R on SQL Server. Standard distributions of R do not have the packages that support remote compute contexts or parallel execution of R tasks.

To get these libraries, install any of the following:
  
+ [Microsoft R Client](http://aka.ms/rclient/download)

+ Microsoft R Server (for SQL Server 2016)

    - To install from SQL Server setup, see [Create a standalone R Server](../../advanced-analytics/r/create-a-standalone-r-server.md)

    - To use the separate Windows-based installer, see [Install Machine Learning Server for Windows](https://docs.microsoft.com/machine-learning-server/install/machine-learning-server-windows-install)

+ Machine Learning Server (for SQL Server 2017)

    - To install from SQL Server setup, see [Create a standalone R Server](../../advanced-analytics/r/create-a-standalone-r-server.md)

    - To use the separate Windows-based installer, see [Install R Server 9.1 for Windows](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows)

## Install a development environment

If you don't already have a preferred R development environment, we recommend one of the following:

+ R Tools for Visual Studio

    Works with Visual Studio 2015.

    For setup information, see [How to install R Tools for Visual Studio](https://docs.microsoft.com/visualstudio/rtvs/installation).
 
    To configure RTVS to use your Microsoft R client libraries, see [About Microsoft R Client](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client)

+ Visual Studio 2017

    Even the free Community Edition includes the data science workload, which installs project templates for R, Python, and F#.

    Download Visual Studio from [this site](https://www.visualstudio.com/vs/). 

+ RStudio

    If you prefer to use RStudio, some additional steps are required to use the RevoScaleR libraries:

    - Install Microsoft R Client to get the required packages and libraries.
    - Update your R path to use the Microsoft R runtime.

    For more information, see [R Client - configure your IDE](https://docs.microsoft.com/machine-learning-server/r-client/what-is-microsoft-r-client#step-2-configure-your-ide).

## Configure your IDE

+ R Tools for Visual Studio

    See [this site](https://docs.microsoft.com/visualstudio/rtvs/getting-started-with-r) for some examples of how to build and debug R projects using R Tools for Visual Studio. 

+ Visual Studio 2017

    If you install Microsoft R Client or R Server **before** you install Visual Studio, the R Server libraries are automatically detected and used for your library path. If you have not installed the RevoScaleR libraries, from the **R Tools** menu, select **Install R Client**.

## Run R in SQL Server

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