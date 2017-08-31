---
title: "Known issues in Machine Learning Services - SQL Server | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "06/16/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 2b37a63a-5ff5-478e-bcc2-d13da3ac241c
caps.latest.revision: 53
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Known issues in Machine Learning Services

This article describes known problems or limitations with Machine Learning components that are provided as an option in SQL Server 2016 and SQL Server 2017.

The information here applies to all of the following, unless specifically indicated:

* SQL Server 2016

  - Microsoft R Services (In-Database)
  - Microsoft R Server (Standalone)

* SQL Server 2017

  - Machine Learning Services for R (In-Database)
  - Machine Learning Services for Python (In-Database)
  - Machine Learning Server (Standalone)

## Setup and configuration issues

For a description of processes and common questions that are related to initial setup and configuration, see [Upgrade and installation FAQ](r/upgrade-and-installation-faq-sql-server-r-services.md). This FAQ article contains information about upgrades, side-by-side installation, and installation of new R or Python components.

### Unable to install Python components in offline installations of SQL Server 2017

If you install SQL Server 2017 on a computer without internet access, the installer might fail to display the page that prompts for the location of the downloaded Python components. In such an instance, you can install the Machine Learning Services feature, but not the Python components.

This issue will be fixed in an upcoming release. As a workaround, you can temporarily enable internet access for the duration of the setup. This limitation does not apply to R.

**Applies to:** SQL Server 2017 with Python

### Install the latest service release to ensure compatibility with Microsoft R Client

If you install the latest version of Microsoft R Client and use it to run R on SQL Server in a remote compute context, you might get an error like the following:

>*You are running version 9.x.x of Microsoft R Client on your computer, which is incompatible with Microsoft R Server version 8.x.x. Download and install a compatible version.*

SQL Server 2016 requires that the R libraries on the client exactly match the R libraries on the server. The restriction has been removed for releases later than R Server 9.0.1. However, if you encounter this error, verify the version of the R libraries that's used by your client and the server and, if necessary, update the client to match the server version.

The version of R that is installed with SQL Server R Services is updated whenever a SQL Server service release is installed. To ensure that you always have the most up-to-date versions of R components, be sure to install all service packs.

To ensure compatibility with Microsoft R Client 9.0.0, install the updates that are described in this [support article](https://support.microsoft.com/kb/3210262).

To avoid problems with R packages, you can also upgrade the version of the R libraries that are installed on the server, by changing to the Modern Lifecycle policy that's described in [the next section](#bkmk_sqlbindr). When you do so, the version of R that's installed with SQL Server is updated on the same schedule that updates are published for Microsoft R Server, which ensures that both server and client always have the latest releases of Microsoft R.

**Applies to:** SQL Server 2016 R Services, with R Server version 9.0.0 or earlier

### <a name="bkmk_sqlbindr"></a> Warning of incompatible version when you connect to an older version of SQL Server R Services from a client by using [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)]

If you installed Microsoft R Server on a client computer by using the setup wizard for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)] or the new Standalone installer for [Microsoft R Server](https://msdn.microsoft.com/microsoft-r/rserver-install-windows), and run R code in a compute context that uses an earlier version of SQL Server R Services, you might see an error like the following:

>*You are running version 9.0.0 of Microsoft R Client on your computer, which is incompatible with the Microsoft R Server version 8.0.3. Download and install a compatible version.*

The SqlBindR.exe tool is provided in the Microsoft R Server 9.0 release to support the upgrade of SQL Server instances to a compatible 9.0 version. Support for the upgrade of R Services instances to 9.0 will be added in SQL Server as part of an upcoming service release. Versions that are candidates for future upgrade include SQL Server 2016 RTM CU3* and SP1+, and SQL Server 2017 CTP 1.1.

**Applies to:** SQL Server 2016 R Services, with R Server version 9.0.0 or earlier

### Setup for SQL Server 2016 service releases might fail to install newer versions of R components

When you install a cumulative update or install a service pack for SQL Server 2016 on a computer that is not connected to the internet, the setup wizard might fail to display the prompt that lets you update the R components by using downloaded CAB files. This failure typically occurs when multiple components are installed together with the database engine.

As a workaround, you can install the service release by using the command line and specifying the */MRCACHEDIRECTORY* argument as shown in this example, which installs CU1 updates:

`C:\<path to installation media>\SQLServer2016-KB3164674-x64.exe /Action=Patch /IACCEPTROPENLICENSETERMS /MRCACHEDIRECTORY=<path to CU1 CAB files>`

To get the latest installers, see [Installing Machine Learning components without internet access](r/installing-ml-components-without-internet-access.md).

**Applies to:** SQL Server 2016 R Services, with R Server version 9.0.0 or earlier

### The Launchpad service fails to start if the version is different from the R version

If you install R Services separately from the database engine, and the build versions are different, you might see the following error in the system log in Event Viewer: 

>_The SQL Server Launchpad service failed to start due to the following error: The service did not respond to the start or control request in a timely fashion._

For example, this error might occur if you install the database engine by using the release version, apply a patch to upgrade the database engine, and then add R Services by using the release version.

To avoid this problem, make sure that all components have the same version number. If you upgrade one component, be sure to apply the same upgrade to all other installed components.

To view a list of the R version numbers that are required for each release of SQL Server 2016, see [Installing R components without internet access](r/installing-ml-components-without-internet-access.md).

### Remote compute contexts are blocked by a firewall in SQL Server instances that are running on Azure virtual machines

If you have installed [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] on a Windows Azure virtual machine, you might not be able to use compute contexts that require the use of the virtual machine's workspace. The reason is that, by default, the Azure VM firewall includes a rule that blocks network access for local R user accounts.

As a workaround, on the Azure VM, open **Windows Firewall with Advanced Security**, select **Outbound Rules**, and disable the following rule: **Block network access for R local user accounts in SQL Server instance MSSQLSERVER**.

### Implied authentication in SQLEXPRESS

When you run R jobs from a remote data-science workstation by using Windows integrated authentication, SQL Server uses *implied authentication* to generate any local Microsoft Open Database Connectivity (ODBC) calls that might be required by the script. However, this feature did not work in the RTM build of SQL Server Express Edition.

To fix the issue, we recommend that you upgrade to a later service release.

If you cannot upgrade, you can use a SQL logon to run remote R jobs that might require embedded ODBC calls.

**Applies to:** SQL Server 2016 R Services Express Edition

### Performance limits when R libraries are called from Standalone R tools

It is possible to call the R tools and libraries that are installed for SQL Server R Services from an external R application such as RGui. This call might be handy when you install new packages, or when you run ad hoc tests on very short code samples.

However, be aware that outside of SQL Server, performance will be limited. For example, even if you have purchased the Enterprise Edition of SQL Server, R will run in single-threaded mode when you run your R code by using external tools. Performance will be superior if you run your R code by initiating a SQL Server connection and using [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), which will call the R libraries for you.

* Avoid calling the R libraries that are used by SQL Server from external R tools.
* If you need to run extensive R code on the SQL Server computer without using SQL Server, install a separate instance of R, such as Microsoft R Client, and then ensure that your R development tools point to the new library.

For more information, see [Create a Standalone R Server](r/create-a-standalone-r-server.md).

### The R script is throttled due to resource governance default values

In Enterprise Edition, you can use resource pools to manage external script processes. In some early release builds, the maximum memory that could be allocated to the R processes was 20 percent. Therefore, if the server had 32 GB of RAM, the R executables (RTerm.exe and BxlServer.exe) could use a maximum of 6.4 GB in a single request.

If you encounter resource limitations, check the current default. If 20 percent is not enough, see the documentation for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on how to change this value.

**Applies to:** SQL Server 2016 R Services, Enterprise Edition

## R code execution and package or function issues

This section contains known issues that are specific to running R on SQL Server, as well as some issues that are related to the R libraries and tools published by Microsoft, including RevoScaleR.

For additional known issues that might affect R solutions, go to the [Microsoft R Server site](https://msdn.microsoft.com/microsoft-r/rserver-known-issues).

### Limitations on processor affinity for R jobs

In the initial release build of SQL Server 2016, you could set processor affinity only for CPUs in the first k-group. For example, if the server is a two-socket machine with two k-groups, only processors from the first k-group are used for the R processes. The same limitation applies when you configure resource governance for R script jobs.

This issue is fixed in SQL Server 2016 Service Pack 1.

**Applies to:** SQL Server 2016 R Services RTM version

### Changes to column types cannot be performed when reading data in a SQL Server compute context

If your compute context is set to the SQL Server instance, you cannot use the _colClasses_ argument (or other similar arguments) to change the data type of columns in your R code.

For example, the following statement would result in an error if the column CRSDepTimeStr is not already an integer:

~~~~
data <- RxSqlServerData(sqlQuery = "SELECT CRSDepTimeStr, ArrDelay  FROM AirlineDemoSmall",
                                connectionString = connectionString,
                                colClasses = c(CRSDepTimeStr = "integer"))
~~~~

This issue will be fixed in a later release.

As a workaround, you can rewrite the SQL query to use CAST or CONVERT and present the data to R by using the correct data type. In general, performance is better when you work with data by using SQL rather than by changing data in the R code.

**Applies to:** SQL Server 2016 R Services

### Avoid clearing workspaces when you execute R code in a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] compute context

If you use the R command to clear your workspace of objects while you're running R code in a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] compute context, or if you clear the workspace as part of an R script that's called by using [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), you might get this error: 

>*workspace object 'revoScriptConnection' not found*

`revoScriptConnection` is an object in the R workspace that contains information about an R session that's called from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. However, if your R code includes a command to clear the workspace (such as `rm(list=ls()))`), all information about the session and other objects in the R workspace is cleared as well.

As a workaround, avoid indiscriminate clearing of variables and other objects while you're running R in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Although clearing the workspace is common when you're working in the R console, doing so can have unintended consequences.

* To delete specific variables, use the R *remove* function: `remove('name1', 'name2', ...)`.
* If there are multiple variables to delete, save the names of temporary variables to a list, and then perform periodic garbage collection.

### Restrictions on data that can be provided as input to an R script

You cannot use in an R script the following types of query results:

- Data from a [!INCLUDE[tsql](../includes/tsql-md.md)] query that references AlwaysEncrypted columns.
  
- Data from a [!INCLUDE[tsql](../includes/tsql-md.md)] query that references masked columns.
  
     If you need to use masked data in an R script, a possible workaround is to make a copy of the data in a temporary table and use that data instead.

### Arguments *varsToKeep* and *varsToDrop* are not supported for SQL Server data sources

When you use the rxDataStep function to write results to a table, using the *varsToKeep* and *varsToDrop* is a handy way of specifying the columns to include or exclude as part of the operation. Currently, these arguments are not supported for SQL Server data sources.

This limitation will be removed in a later release.

### Limited support for SQL data types in `sp_execute_external_script`

Not all data types that are supported in SQL can be used in R. As a workaround, consider casting the unsupported data type to a supported data type before you pass the data to sp_execute_external_script.

For more information, see [R Libraries and Data Types](r/r-libraries-and-data-types.md).

### Possible string corruption

Any round trip of string data from [!INCLUDE[tsql](../includes/tsql-md.md)] to R and then to [!INCLUDE[tsql](../includes/tsql-md.md)] again can result in corruption. This corruption results from the various encodings that are used in R and in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)], as well as from the various collations and languages that are supported in R and [!INCLUDE[tsql](../includes/tsql-md.md)]. Any string in a non-ASCII encoding can potentially be handled incorrectly.

When you send string data to R, convert it to an ASCII representation, if possible.

### Only one value of type `raw` can be returned from `sp_execute_external_script`

When a binary data type (the R **raw** data type) is returned from R, the value must be the value in the output data frame.

Support for multiple **raw** outputs will be added in subsequent releases.

If you want to use multiple output sets, one possible workaround is to do multiple calls of the stored procedure and then send the result sets back to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] by using ODBC.

You can return parameter values along with the results of the stored procedure simply by adding the OUTPUT keyword. For more information, see [Returning Data by Using OUTPUT Parameters](https://technet.microsoft.com/library/ms187004.aspx).

### Loss of precision

Because [!INCLUDE[tsql](../includes/tsql-md.md)] and R support a variety of data types, numeric data types can suffer a loss of precision during the conversion.

For more information about implicit data-type conversion, see [Working with R data types](r/r-libraries-and-data-types.md).

### Variable scoping error (*The sample data set for the analysis has no variables*) when you use the transformFunc parameter

To transform the data while you are modeling it, you can pass a *transformFunc* argument in a function such as `rxLinmod` or `rxLogit`. However, nested function calls can lead to scoping errors in the SQL Server compute context, even if the calls work correctly in the local compute context.

For example, assume that you have defined two functions, `f` and `g`, in your local global environment, and `g` calls `f`. In distributed or remote calls involving `g`, the call to `g` might fail because `f` cannot be found, even if you have passed both `f` and `g` to the remote call.

If you encounter this problem, you can work around the issue by embedding the definition of `f` inside your definition of `g`, anywhere that `g` would ordinarily call `f`.

For example:

```  
f <- function(x) { 2*x * 3 }  
g <- function(y) {   
              a <- 10 * y  
               f(a)  
}  
  
```  


To avoid the error, rewrite the functions as follows:

```  
g <- function(y){  
              f <- function(x) { 2*x +3}  
              a <- 10 * y  
              f(a)  
}  
  
```  

### Data import and manipulation using RevoScaleR

When **varchar** columns are read from a database, white space is trimmed. To prevent this trimming, enclose strings in non-white-space characters.

When you use functions such as `rxDataStep` to create database tables with **varchar** columns, the column width is estimated based on a sample of the data. If the width can vary, it might be necessary to pad all strings to a common length.

Using a transform to change a variable's data type is not supported when you use repeated calls to `rxImport` or `rxTextToXdf` to import and append rows, combining multiple input files into a single .xdf file.

### Limited support for rxExec

In SQL Server 2016, the `rxExec` function that's provided by the RevoScaleR package can be used only in single-threaded mode.

Parallelism for `rxExec` across multiple processes will be added in an upcoming release.

### Increase the maximum parameter size to support rxGetVarInfo

If you use data sets with extremely large numbers of variables (for example, over 40,000), set the `max-ppsize` flag when you start R to use functions such as `rxGetVarInfo`. The `max-ppsize` flag specifies the maximum size of the pointer protection stack.

If you are using the R console (for example, in rgui.exe or rterm.exe), you can set the value of max-ppsize to 500000 by entering the following:

```  
R --max-ppsize=500000  
```  
  
If you are using the [!INCLUDE[rsql_developr](../includes/rsql-developr-md.md)] environment, you can set the `max-ppsize` flag by making the following call to the RevoIDE executable:

```  
RevoIDE.exe /RCommandLine --max-ppsize=500000  
```  

### Issues with the rxDTree function

The `rxDTree` function does not currently support in-formula transformations. In particular, using the `F()` syntax for creating factors on the fly is not supported. However, the numeric data will be automatically binned.

Ordered factors are treated the same as factors in all RevoScaleR analysis functions except `rxDTree`.

## Revolution R Enterprise and Microsoft R Open

This section lists issues specific to R connectivity, development, and performance tools that are provided by Revolution Analytics. These tools were provided in earlier pre-release versions of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)]. 

In general, we recommend that you uninstall these previous versions and install the latest version of SQL Server or Microsoft R Server.

### Running side-by-side versions of Revolution R Enterprise

Installing Revolution R Enterprise side by side with any version of [!INCLUDE[rsql_productname_md](../includes/rsql-productname-md.md)] is not supported.

If you have a license to use a different version of Revolution R Enterprise, you must put it on a separate computer from both the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance and any workstation that you want to use to connect to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance.

### The use of an R productivity environment is not supported

Some pre-release versions of [!INCLUDE[rsql_productname](../includes/rsql-productname-md.md)] included an R development environment for Windows that was created by Revolution Analytics. This tool is no longer provided, and it is not supported.

For compatibility with [!INCLUDE[rsql_productname](../includes/rsql-productname-md.md)], we strongly recommend that you install Microsoft R Client or Microsoft R Server instead of the Revolution Analytics tools. [R Tools for Visual Studio](https://www.visualstudio.com/vs/rtvs/) is another recommended client that supports Microsoft R solutions.

### Compatibility issues with SQLite ODBC driver and RevoScaleR

Revision 0.92 of the SQLite ODBC driver is incompatible with RevoScaleR. Revisions 0.88-0.91 and 0.93 and later are known to be compatible.

## See also

[What's new in SQL Server 2016](../sql-server/what-s-new-in-sql-server-2016.md)

