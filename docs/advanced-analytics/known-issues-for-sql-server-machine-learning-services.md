---
title: Known issues for R language and Python integration - SQL Server Machine Learning Services
ms.prod: sql
ms.technology: machine-learning

ms.date: 04/29/2019
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Known issues in Machine Learning Services
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes known problems or limitations with machine learning components that are provided as an option in [SQL Server 2016 R Services](install/sql-r-services-windows-install.md) and [SQL Server 2017 Machine Learning Services with R and Python](install/sql-machine-learning-services-windows-install.md).

## Setup and configuration issues

For a description of processes and common questions that are related to initial setup and configuration, see [Upgrade and installation FAQ](r/upgrade-and-installation-faq-sql-server-r-services.md). It contains information about upgrades, side-by-side installation, and installation of new R or Python components.

### 1. Inconsistent results in MKL computations due to missing environment variable

**Applies to:** R_SERVER binaries 9.0, 9.1, 9.2 or 9.3.

R_SERVER uses the Intel Math Kernel Library (MKL). For computations involving MKL, inconsistent results can occur if your system is missing an environment variable. 

Set the environment variable `'MKL_CBWR'=AUTO` to ensure conditional numerical reproducibility in R_SERVER. For more information, see [Introduction to Conditional Numerical Reproducibility (CNR)](https://software.intel.com/articles/introduction-to-the-conditional-numerical-reproducibility-cnr).

**Workaround**

1. In Control Panel, click **System and Security** > **System** > **Advanced System Settings** > **Environment Variables**.

2. Create a new User or System variable. 

  + Set variable name to 'MKL_CBWR'.
  + Set the 'Variable value' to 'AUTO'.

3. Restart R_SERVER. On SQL Server, you can restart SQL Server Launchpad Service.

> [!NOTE]
> If you are running the SQL Server 2019 Preview on Linux, edit or create *.bash_profile* in your user home directory, adding the line `export MKL_CBWR="AUTO"`. Execute this file by typing `source .bash_profile` at a bash command prompt. Restart R_SERVER by typing `Sys.getenv()` at the R command prompt.

### 2. R Script runtime error (SQL Server 2017 CU5-CU7 Regression)

For SQL Server 2017, in cumulative updates 5 through 7, there is a regression in the **rlauncher.config** file where the temp directory file path includes a space. This regression is corrected in CU8.

The error you will see when running R script includes the following messages:

> *Unable to communicate with the runtime for 'R' script. Please check the requirements of 'R' runtime.*
>
> STDERR message(s) from external script: 
>
> *Fatal error: cannot create 'R_TempDir'*

**Workaround**

Apply CU8 when it becomes available. Alternatively, you can recreate **rlauncher.config** by running **registerrext** with uninstall/install on an elevated command prompt. 

```cmd
<SQLInstancePath>\R_SERVICES\library\RevoScaleR\rxLibs\x64\RegisterRExt.exe /uninstall /sqlbinnpath:<SQLInstanceBinnPath> /userpoolsize:0 /instance:<SQLInstanceName>

<SQLInstancePath>\R_SERVICES\library\RevoScaleR\rxLibs\x64\RegisterRExt.exe /install /sqlbinnpath:<SQLInstanceBinnPath> /userpoolsize:0 /instance:<SQLInstanceName>
```

The following example shows the commands with the default instance "MSSQL14.MSSQLSERVER" installed into "C:\Program Files\Microsoft SQL Server\":

```cmd
"C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library\RevoScaleR\rxLibs\x64\RegisterRext.exe" /uninstall /sqlbinnpath:"C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\Binn" /userpoolsize:0 /instance:MSSQLSERVER

"C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library\RevoScaleR\rxLibs\x64\RegisterRext.exe" /install /sqlbinnpath:"C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\Binn" /userpoolsize:0 /instance:MSSQLSERVER
```

### 3. Unable to install SQL Server machine learning features on a domain controller

If you try to install SQL Server 2016 R Services or SQL Server 2017 Machine Learning Services on a domain controller, setup fails, with these errors:

> *An error occurred during the setup process of the feature*
> 
> *Cannot find group with identity*
> 
> *Component error code: 0x80131509*

The failure occurs because, on a domain controller, the service cannot create the 20 local accounts required to run machine learning. In general, we do not recommend installing SQL Server on a domain controller. For more information, see [Support bulletin 2032911](https://support.microsoft.com/help/2032911/you-may-encounter-problems-when-installing-sql-server-on-a-domain-cont).

### 4. Install the latest service release to ensure compatibility with Microsoft R Client

If you install the latest version of Microsoft R Client and use it to run R on SQL Server in a remote compute context, you might get an error like the following:

> *You are running version 9.x.x of Microsoft R Client on your computer, which is incompatible with Microsoft R Server version 8.x.x. Download and install a compatible version.*

SQL Server 2016 requires that the R libraries on the client exactly match the R libraries on the server. The restriction has been removed for releases later than R Server 9.0.1. However, if you encounter this error, verify the version of the R libraries that's used by your client and the server and, if necessary, update the client to match the server version.

The version of R that is installed with SQL Server R Services is updated whenever a SQL Server service release is installed. To ensure that you always have the most up-to-date versions of R components, be sure to install all service packs.

To ensure compatibility with Microsoft R Client 9.0.0, install the updates that are described in this [support article](https://support.microsoft.com/kb/3210262).

To avoid problems with R packages, you can also upgrade the version of the R libraries that are installed on the server, by changing your servicing agreement to use the Modern Lifecycle Support policy, as described in [the next section](#bkmk_sqlbindr). When you do so, the version of R that's installed with SQL Server is updated on the same schedule used for updates of machine Learning Server (formerly Microsoft R Server).

**Applies to:** SQL Server 2016 R Services, with R Server version 9.0.0 or earlier

### 5. R components missing from CU3 setup

A limited number of Azure virtual machines were provisioned without the R installation files that should be included with SQL Server. The issue applies to virtual machines provisioned in the period from 2018-01-05 to 2018-01-23. This issue might also affect on-premises installations, if you applied the CU3 update for SQL Server 2017 during the period from 2018-01-05 to 2018-01-23.

A service release has been provided that includes the correct version of the R installation files.

+ [Cumulative Update Package 3 for SQL Server 2017 KB4052987](https://www.microsoft.com/download/details.aspx?id=56128).

To install the components and repair SQL Server 2017 CU3, you must uninstall CU3, and reinstall the updated version:

1. Download the updated CU3 installation file, which includes the R installers.
2. Uninstall CU3. In Control Panel, search for **Uninstall an update**, and then select "Hotfix 3015 for SQL Server 2017 (KB4052987) (64-bit)". Proceed with uninstall steps.
3. Reinstall the CU3 update, by double-clicking on the update for KB4052987 that you just downloaded: `SQLServer2017-KB4052987-x64.exe`. Follow the installation instructions.

### 6. Unable to install Python components in offline installations of SQL Server 2017 CTP 2.0 or later

If you install a pre-release version of SQL Server 2017 on a computer without internet access, the installer might fail to display the page that prompts for the location of the downloaded Python components. In such an instance, you can install the Machine Learning Services feature, but not the Python components.

This issue is fixed in the release version. Also, this limitation does not apply to R components.

**Applies to:** SQL Server 2017 with Python

### <a name="bkmk_sqlbindr"></a> Warning of incompatible version when you connect to an older version of SQL Server R Services from a client by using [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)]

When you run R code in a SQL Server 2016 compute context, you might see the following error:

> *You are running version 9.0.0 of Microsoft R Client on your computer, which is incompatible with the Microsoft R Server version 8.0.3. Download and install a compatible version.*

This message is displayed if either of the following two statements is true,

+ You installed R Server (Standalone) on a client computer by using the setup wizard for [!INCLUDE[ssSQLv14_md](../includes/sssqlv14-md.md)].
+ You installed Microsoft R Server by using the [separate Windows installer](https://docs.microsoft.com/machine-learning-server/install/r-server-install-windows).

To ensure that the server and client use the same version you might need to use _binding_, supported for Microsoft R Server 9.0 and later releases, to upgrade the R components in SQL Server 2016 instances. To determine if support for upgrades is available for your version of R Services, see [Upgrade an instance of R Services using SqlBindR.exe](r/use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

**Applies to:** SQL Server 2016 R Services, with R Server version 9.0.0 or earlier

### 7. Setup for SQL Server 2016 service releases might fail to install newer versions of R components

When you install a cumulative update or install a service pack for SQL Server 2016 on a computer that is not connected to the internet, the setup wizard might fail to display the prompt that lets you update the R components by using downloaded CAB files. This failure typically occurs when multiple components were installed together with the database engine.

As a workaround, you can install the service release by using the command line and specifying the `MRCACHEDIRECTORY` argument as shown in this example, which installs CU1 updates:

`C:\<path to installation media>\SQLServer2016-KB3164674-x64.exe /Action=Patch /IACCEPTROPENLICENSETERMS /MRCACHEDIRECTORY=<path to CU1 CAB files>`

To get the latest installers, see [Install machine learning components without internet access](install/sql-ml-component-install-without-internet-access.md).

**Applies to:** SQL Server 2016 R Services, with R Server version 9.0.0 or earlier

### 8. Launchpad services fails to start if the version is different from the R version

If you install SQL Server R Services separately from the database engine, and the build versions are different, you might see the following error in the System Event log:

> *The SQL Server Launchpad service failed to start due to the following error: The service did not respond to the start or control request in a timely fashion.*

For example, this error might occur if you install the database engine by using the release version, apply a patch to upgrade the database engine, and then add the R Services feature by using the release version.

To avoid this problem, use a utility such as File Manager to compare the versions of Launchpad.exe with version of SQL binaries, such as sqldk.dll. All components should have the same version number. If you upgrade one component, be sure to apply the same upgrade to all other installed components.

Look for Launchpad in the `Binn` folder for the instance. For example, in a default installation of SQL Server 2016, the path might be `C:\Program Files\Microsoft SQL Server\MSSQL.13.InstanceNameMSSQL\Binn`. 

### 9. Remote compute contexts are blocked by a firewall in SQL Server instances that are running on Azure virtual machines

If you have installed [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)] on a Windows Azure virtual machine, you might not be able to use compute contexts that require the use of the virtual machine's workspace. The reason is that, by default, the firewall on Azure virtual machines includes a rule that blocks network access for local R user accounts.

As a workaround, on the Azure VM, open **Windows Firewall with Advanced Security**, select **Outbound Rules**, and disable the following rule: **Block network access for R local user accounts in SQL Server instance MSSQLSERVER**. You can also leave the rule enabled, but change the security property to **Allow if secure**.

### 10. Implied authentication in SQLEXPRESS

When you run R jobs from a remote data-science workstation by using Integrated Windows authentication, SQL Server uses *implied authentication* to generate any local ODBC calls that might be required by the script. However, this feature did not work in the RTM build of SQL Server Express Edition.

To fix the issue, we recommend that you upgrade to a later service release.

If upgrade is not feasible, as a workaround, use  a SQL login to run remote R jobs that might require embedded ODBC calls.

**Applies to:** SQL Server 2016 R Services Express Edition

### 11. Performance limits when libraries used by SQL Server are called from other tools

It is possible to call the machine learning libraries that are installed for SQL Server from an external application, such as RGui. Doing so might be the most convenient way to accomplish certain tasks, such as installing new packages, or running ad hoc tests on very short code samples. However, outside of SQL Server, performance might be limited. 

For example, even if you are using the Enterprise Edition of SQL Server, R runs in single-threaded mode when you run your R code by using external tools. To get the benefits of performance in SQL Server, initiate a SQL Server connection and use [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) to call the external script runtime.

In general, avoid calling the machine learning libraries that are used by SQL Server from external tools. If you need to debug R or Python code, it is typically easier to do so outside of SQL Server. To get the same  libraries that are in SQL Server, you can install Microsoft R Client, [SQL Server 2017 Machine Learning Server (Standalone)](install/sql-machine-learning-standalone-windows-install.md), or [SQL Server 2016 R Server (Standalone)](install/sql-r-standalone-windows-install.md).

### 12. SQL Server Data Tools does not support permissions required by external scripts

When you use Visual Studio or SQL Server Data Tools to publish a database project, if any principal has permissions specific to external script execution, you might get an error like this one:

> *TSQL Model: Error detected when reverse engineering the database. The permission was not recognized and was not imported.*

Currently the DACPAC model does not support the permissions used by R Services or Machine Learning Services, such as GRANT ANY EXTERNAL SCRIPT, or EXECUTE ANY EXTERNAL SCRIPT. This issue will be fixed in a later release.

As a workaround, run the additional GRANT statements in a post-deployment script.

### 13. External script execution is throttled due to resource governance default values

In Enterprise Edition, you can use resource pools to manage external script processes. In some early release builds, the maximum memory that could be allocated to the R processes was 20 percent. Therefore, if the server had 32 GB of RAM, the R executables (RTerm.exe and BxlServer.exe) could use a maximum of 6.4 GB in a single request.

If you encounter resource limitations, check the current default. If 20 percent is not enough, see the documentation for [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] on how to change this value.

**Applies to:** SQL Server 2016 R Services, Enterprise Edition

## R script execution issues

This section contains known issues that are specific to running R on SQL Server, as well as some issues that are related to the R libraries and tools published by Microsoft, including RevoScaleR.

For additional known issues that might affect R solutions, see the [Machine Learning Server](https://docs.microsoft.com/machine-learning-server/resources-known-issues) site.

### 1. Access denied warning when executing R scripts on SQL Server in a non default location

If the instance of SQL Server has been installed to a non-default location, such as outside the `Program Files` folder, the warning ACCESS_DENIED is raised when you try to run scripts that install a package. For example:

> *In `normalizePath(path.expand(path), winslash, mustWork)` : path[2]="~ExternalLibraries/R/8/1": Access is denied*

The reason is that an R function attempts to read the path, and fails if the built-in users group **SQLRUserGroup**, does not have read access. The warning that is raised does not block execution of the current R script, but the warning might recur repeatedly whenever the user runs any other R script.

If you have installed SQL Server to the default location, this error does not occur, because all Windows users have read permissions on the `Program Files` folder.

This issue ia addressed in an upcoming service release. As a workaround, provide the group, **SQLRUserGroup**, with read access for all parent folders of `ExternalLibraries`.

### 2. Serialization error between old and new versions of RevoScaleR

When you pass a model using a serialized format to a remote SQL Server instance, you might get the error: 

> *Error in memDecompress(data, type = decompress) internal error -3 in memDecompress(2).*

This error is raised if you saved the model using a recent version of the serialization function, [rxSerializeModel](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxserializemodel), but the SQL Server instance where you deserialize the model has an older version of the RevoScaleR APIs, from SQL Server 2017 CU2 or earlier.

As a workaround, you can upgrade the SQL Server 2017 instance to CU3 or later.

The error does not appear if the API version is the same, or if you are moving a model saved with an older serialization function to a server that uses a newer version of the serialization API.

In other words, use the same version of RevoScaleR for both serialization and deserialization operations.

### 3. Real-time scoring does not correctly handle the _learningRate_ parameter in tree and forest models

If you create a model using a decision tree or decision forest method and specify the learning rate, you might see inconsistent results when using `sp_rxpredict` or the SQL `PREDICT` function, as compared to using `rxPredict`.

The cause is an error in the API that processes serialized models, and is limited to the `learningRate` parameter: for example, in [rxBTrees](https://docs.microsoft.com/machine-learning-server/r-reference/revoscaler/rxbtrees), or

This issue is addressed in an upcoming service release.

### 4. Limitations on processor affinity for R jobs

In the initial release build of SQL Server 2016, you could set processor affinity only for CPUs in the first k-group. For example, if the server is a 2-socket machine with two k-groups, only processors from the first k-group are used for the R processes. The same limitation applies when you configure resource governance for R script jobs.

This issue is fixed in SQL Server 2016 Service Pack 1. We recommend that you upgrade to the latest service release.

**Applies to:** SQL Server 2016 R Services RTM version

### 5. Changes to column types cannot be performed when reading data in a SQL Server compute context

If your compute context is set to the SQL Server instance, you cannot use the _colClasses_ argument (or other similar arguments) to change the data type of columns in your R code.

For example, the following statement would result in an error if the column CRSDepTimeStr is not already an integer:

```R
data <- RxSqlServerData(
  sqlQuery = "SELECT CRSDepTimeStr, ArrDelay  FROM AirlineDemoSmall", 
  connectionString = connectionString, 
  colClasses = c(CRSDepTimeStr = "integer"))
```

As a workaround, you can rewrite the SQL query to use CAST or CONVERT and present the data to R by using the correct data type. In general, performance is better when you work with data by using SQL rather than by changing data in the R code.

**Applies to:** SQL Server 2016 R Services

### 6. Limits on size of serialized models

When you save a model to a SQL Server table, you must serialize the model and save it in a binary format. Theoretically the maximum size of a model that can be stored with this method is 2 GB, which is the maximum size of varbinary columns in SQL Server.

If you need to use larger models, the following workarounds are available:

+ Take steps to reduce the size of your model. Some open source R packages include a great deal of information in the model object, and much of this information can be removed for deployment. 
+ Use feature selection to remove unnecessary columns.
+ If you are using an open source algorithm, consider a similar implementation using the corresponding algorithm in MicrosoftML or RevoScaleR. These packages have been optimized for deployment scenarios.
+ After the model has been rationalized and the size reduced using the preceding steps, see if the [memCompress](https://www.rdocumentation.org/packages/base/versions/3.4.1/topics/memCompress) function in base R can be used to reduce the size of the model before passing it to SQL Server. This option is best when the model is close to the 2 GB limit.
+ For larger models, you can use the SQL Server [FileTable](../relational-databases/blob/filetables-sql-server.md) feature to store the models, rather than using a varbinary column.

    To use FileTables, you must add a firewall exception, because data stored in FileTables is managed by the Filestream filesystem driver in SQL Server, and default firewall rules block network file access. For more information, see [Enable Prerequisites for FileTable](../relational-databases/blob/enable-the-prerequisites-for-filetable.md).

    After you have enabled FileTable, to write the model, you get a path from SQL using the FileTable API, and then write the model to that location from your code. When you need to read the model, you get the path from SQL and then call the model using the path from your script. For more information, see [Access FileTables with File Input-Output APIs](../relational-databases/blob/access-filetables-with-file-input-output-apis.md).

### 7. Avoid clearing workspaces when you execute R code in a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] compute context

If you use an R command to clear your workspace of objects while running R code in a [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] compute context, or if you clear the workspace as part of an R script called by using [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), you might get this error: *workspace object revoScriptConnection not found*

`revoScriptConnection` is an object in the R workspace that contains information about an R session that is called from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. However, if your R code includes a command to clear the workspace (such as `rm(list=ls()))`, all information about the session and other objects in the R workspace is cleared as well.

As a workaround, avoid indiscriminate clearing of variables and other objects while you're running R in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)]. Although clearing the workspace is common when working in the R console, it can have unintended consequences.

* To delete specific variables, use the R `remove` function: for example, `remove('name1', 'name2', ...)`
* If there are multiple variables to delete, save the names of temporary variables to a list and perform periodic garbage collection.

### 8. Restrictions on data that can be provided as input to an R script

You cannot use in an R script the following types of query results:

- Data from a [!INCLUDE[tsql](../includes/tsql-md.md)] query that references AlwaysEncrypted columns.
  
- Data from a [!INCLUDE[tsql](../includes/tsql-md.md)] query that references masked columns.
  
     If you need to use masked data in an R script, a possible workaround is to make a copy of the data in a temporary table and use that data instead.

### 9. Use of strings as factors can lead to performance degradation

Using string type variables as factors can greatly increase the amount of memory used for R operations. This is a known issue with R in general, and there are many articles on the subject. For example, see [Factors are not first-class citizens in R, by John Mount, in R-bloggers)](https://www.r-bloggers.com/factors-are-not-first-class-citizens-in-r/) or [stringsAsFactors: An unauthorized biography, by Roger Peng](https://simplystatistics.org/2015/07/24/stringsasfactors-an-unauthorized-biography/). 

Although the issue is not specific to SQL Server, it can greatly affect performance of R code run in SQl Server. Strings are typically stored as varchar or nvarchar, and if a column of string data has many unique values, the process of internally converting these to integers and back to strings by R can even lead to memory allocation errors.

If you do not absolutely require a string data type for other operations, mapping the string values to a numeric (integer) data type as part of data preparation would be beneficial from a performance and scale perspective.

For a discussion of this issue, and other tips, see [Performance for R Services - data optimization](r/r-and-data-optimization-r-services.md).

### 10. Arguments *varsToKeep* and *varsToDrop* are not supported for SQL Server data sources

When you use the rxDataStep function to write results to a table, using the *varsToKeep* and *varsToDrop* is a handy way of specifying the columns to include or exclude as part of the operation. However, these arguments are not supported for SQL Server data sources.

### 11. Limited support for SQL data types in sp\_execute\_external\_script

Not all data types that are supported in SQL can be used in R. As a workaround, consider casting the unsupported data type to a supported data type before passing the data to sp\_execute\_external\_script.

For more information, see [R libraries and data types](r/r-libraries-and-data-types.md).

### 12. Possible string corruption using unicode strings in varchar columns

Passing unicode data in varchar columns from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to R/Python can result in string corruption. This is due to the encoding for these unicode string in [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] collations   may not match with the default UTF-8 encoding used in R/Python. 

To send any non-ASCII string data from [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] to R/Python, use UTF-8 encoding (available in [!INCLUDE[sql-server-2019](../includes/sssqlv15-md.md)]) or use nvarchar type for the same.

### 13. Only one value of type `raw` can be returned from `sp_execute_external_script`

When a binary data type (the R **raw** data type) is returned from R, the value must be sent in the output data frame.

With data types other than **raw**, you can return parameter values along with the results of the stored procedure by adding the OUTPUT keyword. For more information, see [Parameters](https://docs.microsoft.com/sql/relational-databases/stored-procedures/parameters).

If you want to use multiple output sets that include values of type **raw**, one possible workaround is to do multiple calls of the stored procedure, or to send the result sets back to [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] by using ODBC.

### 14. Loss of precision

Because [!INCLUDE[tsql](../includes/tsql-md.md)] and R support various data types, numeric data types can suffer loss of precision during conversion.

For more information about implicit data-type conversion, see [R libraries and data types](r/r-libraries-and-data-types.md).

### 15. Variable scoping error when you use the transformFunc parameter

To transform data while you are modeling, you can pass a *transformFunc* argument in a function such as `rxLinmod` or `rxLogit`. However, nested function calls can lead to scoping errors in the SQL Server compute context, even if the calls work correctly in the local compute context.

> *The sample data set for the analysis has no variables*

For example, assume that you have defined two functions, `f` and `g`, in your local global environment, and `g` calls `f`. In distributed or remote calls involving `g`, the call to `g` might fail with this error, because `f` cannot be found, even if you have passed both `f` and `g` to the remote call.

If you encounter this problem, you can work around the issue by embedding the definition of `f` inside your definition of `g`, anywhere before `g` would ordinarily call `f`.

For example:

```R
f <- function(x) { 2*x * 3 }
g <- function(y) {
              a <- 10 * y
               f(a)
}
```

To avoid the error, rewrite the definition as follows:

```R
g <- function(y){
              f <- function(x) { 2*x +3}
              a <- 10 * y
              f(a)
}
```

### 16. Data import and manipulation using RevoScaleR

When **varchar** columns are read from a database, white space is trimmed. To prevent this, enclose strings in non-white-space characters.

When functions such as `rxDataStep` are used to create database tables that have **varchar** columns, the column width is estimated based on a sample of the data. If the width can vary, it might be necessary to pad all strings to a common length.

Using a transform to change a variable's data type is not supported when repeated calls to `rxImport` or `rxTextToXdf` are used to import and append rows, combining multiple input files into a single .xdf file.

### 17. Limited support for rxExec

In SQL Server 2016, the `rxExec` function that's provided by the RevoScaleR package can be used only in single-threaded mode.

### 18. Increase the maximum parameter size to support rxGetVarInfo

If you use data sets with extremely large numbers of variables (for example, over 40,000), set the `max-ppsize` flag when you start R to use functions such as `rxGetVarInfo`. The `max-ppsize` flag specifies the maximum size of the pointer protection stack.

If you are using the R console (for example, RGui.exe or RTerm.exe), you can set the value of _max-ppsize_ to 500000 by typing:

```R
R --max-ppsize=500000
```

### 19. Issues with the rxDTree function

The `rxDTree` function does not currently support in-formula transformations. In particular, using the `F()` syntax for creating factors on the fly is not supported. However, numeric data is automatically binned.

Ordered factors are treated the same as factors in all RevoScaleR analysis functions except `rxDTree`.

### 20. Data.table as an OutputDataSet in R

Using `data.table` as an `OutputDataSet` in R is not supported in SQL Server 2017 Cumulative Update 13 (CU13) and earlier. The following message might appear:

```
Msg 39004, Level 16, State 20, Line 2
A 'R' script error occurred during execution of 
'sp_execute_external_script' with HRESULT 0x80004004.
Msg 39019, Level 16, State 2, Line 2
An external script error occurred: 
Error in alloc.col(newx) : 
  Internal error: length of names (0) is not length of dt (11)
Calls: data.frame ... as.data.frame -> as.data.frame.data.table -> copy -> alloc.col

Error in execution.  Check the output for more information.
Error in eval(expr, envir, enclos) : 
  Error in execution.  Check the output for more information.
Calls: source -> withVisible -> eval -> eval -> .Call
Execution halted
```

`data.table` as an `OutputDataSet` in R is supported in SQL Server 2017 Cumulative Update 14 (CU14) and later.

## Python script execution issues

This section contains known issues that are specific to running Python on SQL Server, as well as issues that are related to the Python packages published by Microsoft, including [revoscalepy](https://docs.microsoft.com/r-server/python-reference/revoscalepy/revoscalepy-package) and [microsoftml](https://docs.microsoft.com/r-server/python-reference/microsoftml/microsoftml-package).

### 1. Call to pretrained model fails if path to model is too long

If you installed the pretrained models in an early release of SQL Server 2017, the complete path to the trained model file might be too long for Python to read. This limitation is fixed in a later service release.

There are several potential workarounds: 

+ When you install the pretrained models, choose a custom location.
+ If possible, install the SQL Server instance under a custom installation path with a shorter path, such as C:\SQL\MSSQL14.MSSQLSERVER.
+ Use the Windows utility [Fsutil](https://technet.microsoft.com/library/cc788097(v=ws.11).aspx) to create a hard link that maps the model file to a shorter path.
+ Update to the latest service release.

### 2. Error when saving serialized model to SQL Server

When you pass a model to a remote SQL Server instance, and try to read the binary model using the `rx_unserialize` function in [revoscalepy](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/revoscalepy-package), you might get the error: 

> *NameError: name 'rx_unserialize_model' is not defined*

This error is raised if you saved the model using a recent version of the serialization function, but the SQL Server instance where you deserialize the model does not recognize the  serialization API.

To resolve the issue, upgrade the SQL Server 2017 instance to CU3 or later.

### 3. Failure to initialize a varbinary variable causes an error in BxlServer

If you run Python code in SQL Server using `sp_execute_external_script`, and the code has output variables of type varbinary(max), varchar(max) or similar types, the variable must be initialized or set as part of your script. Otherwise, the data exchange component, BxlServer, raises an error and stops working.

This limitation will be fixed in an upcoming service release. As a workaround, make sure that the variable is initialized within the Python script. Any valid value can be used, as in the following examples:

```sql
declare @b varbinary(max);
exec sp_execute_external_script
  @language = N'Python'
  , @script = N'b = 0x0'
  , @params = N'@b varbinary(max) OUTPUT'
  , @b = @b OUTPUT;
go
```

```sql
declare @b varchar(30);
exec sp_execute_external_script
  @language = N'Python'
  , @script = N' b = ""  '
  , @params = N'@b varchar(30) OUTPUT'
  , @b = @b OUTPUT;
go
```

### 4. Telemetry warning on successful execution of Python code

Beginning with SQL Server 2017 CU2, the following message might appear even if Python code otherwise runs successfully:

> *STDERR message(s) from external script:*
> *~PYTHON_SERVICES\lib\site-packages\revoscalepy\utils\RxTelemetryLogger*
> *SyntaxWarning: telemetry_state is used prior to global declaration*

This issue has been fixed in SQL Server 2017 Cumulative Update 3 (CU3). 

### 5. Numeric, decimal and money data types not supported

Beginning with SQL Server 2017 Cumulative Update 12 (CU12), numeric, decimal and money data types in WITH RESULT SETS are unsupported when using Python with `sp_execute_external_script`. The following messages might appear:

> *[Code: 39004, SQL State: S1000]  A 'Python' script error occurred during execution of'sp_execute_external_script' with HRESULT 0x80004004.*

> *[Code: 39019, SQL State: S1000]  An external script error occurred:*
> 
> *SqlSatelliteCall error: Unsupported type in output schema. Supported types: bit, smallint, int, datetime, smallmoney, real and float. char, varchar are partially supported.*

This has been fixed in SQL Server 2017 Cumulative Update 14 (CU14).

## Revolution R Enterprise and Microsoft R Open

This section lists issues specific to R connectivity, development, and performance tools that are provided by Revolution Analytics. These tools were provided in earlier pre-release versions of [!INCLUDE[ssCurrent](../includes/sscurrent-md.md)].

In general, we recommend that you uninstall these previous versions and install the latest version of SQL Server or Microsoft R Server.

### 1. Revolution R Enterprise is not supported

Installing Revolution R Enterprise side by side with any version of [!INCLUDE[rsql_productname_md](../includes/rsql-productname-md.md)] is not supported.

If you have an existing license for Revolution R Enterprise, you must put it on a separate computer from both the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance and any workstation that you want to use to connect to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] instance.

Some pre-release versions of [!INCLUDE[rsql_productname](../includes/rsql-productname-md.md)] included an R development environment for Windows that was created by Revolution Analytics. This tool is no longer provided, and is not supported.

For compatibility with [!INCLUDE[rsql_productname](../includes/rsql-productname-md.md)], we recommend that you install Microsoft R Client instead. [R Tools for Visual Studio](https://www.visualstudio.com/vs/rtvs/) and [Visual Studio Code](https://code.visualstudio.com/) also supports Microsoft R solutions.

### 2. Compatibility issues with SQLite ODBC driver and RevoScaleR

Revision 0.92 of the SQLite ODBC driver is incompatible with RevoScaleR. Revisions 0.88-0.91 and 0.93 and later are known to be compatible.

## See also

[What's new in SQL Server 2016](../sql-server/what-s-new-in-sql-server-2016.md)

[Troubleshooting machine learning in SQL Server](machine-learning-troubleshooting-faq.md)
