---
title: Known issues for Python and R
description: This article describes known problems or limitations with the Python and R components that are provided in SQL Server Machine Learning Services and SQL Server 2016 R Services.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.reviewer: randolphwest
ms.date: 10/06/2022
ms.service: sql
ms.subservice: machine-learning-services
ms.topic: troubleshooting
ms.custom: contperf-fy21q3
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# Known issues for Python and R in SQL Server Machine Learning Services

[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

[!INCLUDE [machine-learning-server-retirement](../../includes/machine-learning-server-retirement.md)]

This article describes known problems or limitations with the Python and R components that are provided in [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) and [SQL Server 2016 R Services](../r/sql-server-r-services.md).

## Setup and configuration issues

For a description of processes related to initial setup and configuration, see [Install SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md). It contains information about upgrades, side-by-side installation, and installation of new R or Python components.

### Inconsistent results in MKL computations due to missing environment variable

**Applies to:** R_SERVER binaries 9.0, 9.1, 9.2 or 9.3.

R_SERVER uses the Intel Math Kernel Library (MKL). For computations involving MKL, inconsistent results can occur if your system is missing an environment variable.

Set the environment variable `'MKL_CBWR'=AUTO` to ensure conditional numerical reproducibility in R_SERVER. For more information, see [Introduction to Conditional Numerical Reproducibility (CNR)](https://software.intel.com/articles/introduction-to-the-conditional-numerical-reproducibility-cnr).

#### Workaround

1. In Control Panel, select **System and Security** > **System** > **Advanced System Settings** > **Environment Variables**.

1. Create a new User or System variable.

   - Set Variable to `MKL_CBWR`.
   - Set the Value to `AUTO`.

1. Restart R_SERVER. On SQL Server, you can restart SQL Server Launchpad Service.

> [!NOTE]  
> If you are running the [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] on Linux, edit or create `.bash_profile` in your user home directory, adding the line `export MKL_CBWR="AUTO"`. Execute this file by typing `source .bash_profile` at a bash command prompt. Restart R_SERVER by typing `Sys.getenv()` at the R command prompt.

### R Script runtime error (SQL Server 2017 CU 5 - CU 7 regression)

For [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], in cumulative updates 5 through 7, there is a regression in the **rlauncher.config** file where the temp directory file path includes a space. This regression is corrected in CU 8.

The error you will see when running R script includes the following messages:

> *Unable to communicate with the runtime for 'R' script. Please check the requirements of 'R' runtime.*
>
> STDERR message(s) from external script:
>
> *Fatal error: cannot create 'R_TempDir'*

#### Workaround

Apply CU 8 when it becomes available. Alternatively, you can recreate `rlauncher.config` by running **registerrext** with uninstall/install on an elevated command prompt.

```cmd
<SQLInstancePath>\R_SERVICES\library\RevoScaleR\rxLibs\x64\RegisterRExt.exe /uninstall /sqlbinnpath:<SQLInstanceBinnPath> /userpoolsize:0 /instance:<SQLInstanceName>

<SQLInstancePath>\R_SERVICES\library\RevoScaleR\rxLibs\x64\RegisterRExt.exe /install /sqlbinnpath:<SQLInstanceBinnPath> /userpoolsize:0 /instance:<SQLInstanceName>
```

The following example shows the commands with the default instance "MSSQL14.MSSQLSERVER" installed into `C:\Program Files\Microsoft SQL Server\`:

```cmd
"C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library\RevoScaleR\rxLibs\x64\RegisterRext.exe" /uninstall /sqlbinnpath:"C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\Binn" /userpoolsize:0 /instance:MSSQLSERVER

"C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\R_SERVICES\library\RevoScaleR\rxLibs\x64\RegisterRext.exe" /install /sqlbinnpath:"C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\Binn" /userpoolsize:0 /instance:MSSQLSERVER
```

### Unable to install SQL Server machine learning features on a domain controller

If you try to install [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] R Services or SQL Server Machine Learning Services on a domain controller, setup fails, with these errors:

> *An error occurred during the setup process of the feature*
>
> *Cannot find group with identity*
>
> *Component error code: 0x80131509*

The failure occurs because, on a domain controller, the service can't create the 20 local accounts required to run machine learning. In general, we don't recommend installing SQL Server on a domain controller. For more information, see [Support bulletin 2032911](https://support.microsoft.com/help/2032911/you-may-encounter-problems-when-installing-sql-server-on-a-domain-cont).

### Install the latest service release to ensure compatibility with Microsoft R Client

If you install the latest version of Microsoft R Client and use it to run R on SQL Server in a remote compute context, you might get an error like the following:

> *You are running version 9.x.x of Microsoft R Client on your computer, which is incompatible with Microsoft R Server version 8.x.x. Download and install a compatible version.*

[!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] requires that the R libraries on the client exactly match the R libraries on the server. The restriction has been removed for releases later than R Server 9.0.1. However, if you encounter this error, verify the version of the R libraries that's used by your client and the server and, if necessary, update the client to match the server version.

The version of R that is installed with SQL Server R Services is updated whenever a SQL Server service release is installed. To ensure that you always have the most up-to-date versions of R components, be sure to install all service packs.

To ensure compatibility with Microsoft R Client 9.0.0, install the updates that are described in this archived version of support article [KB3210262](https://web.archive.org/web/20190415073655/https://support.microsoft.com/en-us/help/3210262/fix-version-of-r-client-is-incompatible-with-the-microsoft-r-server-ve).

To avoid problems with R packages, you can also upgrade the version of the R libraries that are installed on the server, by changing your servicing agreement to use the Modern Lifecycle Support policy, as described in [the next section](#sqlbindr). When you do so, the version of R that's installed with SQL Server is updated on the same schedule used for updates of Machine Learning Server (formerly Microsoft R Server).

**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] R Services, with R Server version 9.0.0 or earlier

### R components missing from SQL Server 2017 CU 3 setup

A limited number of Azure virtual machines were provisioned without the R installation files that should be included with SQL Server. The issue applies to virtual machines provisioned in the period from 2018-01-05 to 2018-01-23. This issue might also affect on-premises installations, if you applied the CU 3 update for [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] during the period from 2018-01-05 to 2018-01-23.

A service release has been provided that includes the correct version of the R installation files.

- [Cumulative Update Package 3 for SQL Server 2017 KB4052987](https://www.microsoft.com/download/details.aspx?id=56128).

To install the components and repair [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU 3, you must uninstall CU 3, and reinstall the updated version:

1. Download the updated CU 3 installation file, which includes the R installers.
1. Uninstall CU 3. In Control Panel, search for **Uninstall an update**, and then select "Hotfix 3015 for SQL Server 2017 (KB4052987) (64-bit)". Proceed with uninstall steps.
1. Reinstall the CU 3 update, by double-clicking on the update for KB4052987 that you downloaded: `SQLServer2017-KB4052987-x64.exe`. Follow the installation instructions.

### Unable to install Python components in offline installations of SQL Server 2017 or later

If you install a pre-release version of [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] on a computer without internet access, the installer might fail to display the page that prompts for the location of the downloaded Python components. In such an instance, you can install the Machine Learning Services feature, but not the Python components.

This issue is fixed in the release version. Also, this limitation doesn't apply to R components.

**Applies to:** [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] with Python

### <a id="sqlbindr"></a> Warn of incompatible version when you connect to an older version of SQL Server R Services from a client by using SQL Server 2017

When you run R code in a [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] compute context, you might see the following error:

> *You are running version 9.0.0 of Microsoft R Client on your computer, which is incompatible with the Microsoft R Server version 8.0.3. Download and install a compatible version.*

This message is displayed if either of the following two statements is true:

- You installed R Server (Standalone) on a client computer by using the setup wizard for [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)].
- You installed Microsoft R Server by using the [separate Windows installer](/machine-learning-server/install/r-server-install-windows).

To ensure that the server and client use the same version you might need to use *binding*, supported for Microsoft R Server 9.0 and later releases, to upgrade the R components in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] instances. To determine if support for upgrades is available for your version of R Services, see [Upgrade an instance of R Services using SqlBindR.exe](../install/upgrade-r-and-python.md).

**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] R Services, with R Server version 9.0.0 or earlier

### Setup for SQL Server 2016 service releases might fail to install newer versions of R components

When you install a cumulative update or install a service pack for [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] on a computer that isn't connected to the internet, the setup wizard might fail to display the prompt that lets you update the R components by using downloaded CAB files. This failure typically occurs when multiple components were installed together with the database engine.

As a workaround, you can install the service release by using the command line and specifying the `MRCACHEDIRECTORY` argument as shown in this example, which installs CU 1 updates:

`C:\<path to installation media>\SQLServer2016-KB3164674-x64.exe /Action=Patch /IACCEPTROPENLICENSETERMS /MRCACHEDIRECTORY=<path to CU 1 CAB files>`

To get the latest installers, see [Install machine learning components without internet access](../install/sql-ml-component-install-without-internet-access.md).

**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] R Services, with R Server version 9.0.0 or earlier

### Launchpad services fails to start if the version is different from the R version

If you install SQL Server R Services separately from the database engine, and the build versions are different, you might see the following error in the System Event log:

> *The SQL Server Launchpad service failed to start due to the following error: The service did not respond to the start or control request in a timely fashion.*

For example, this error might occur if you install the database engine by using the release version, apply a patch to upgrade the database engine, and then add the R Services feature by using the release version.

To avoid this problem, use a utility such as File Manager to compare the versions of Launchpad.exe with version of SQL binaries, such as `sqldk.dll`. All components should have the same version number. If you upgrade one component, be sure to apply the same upgrade to all other installed components.

Look for Launchpad in the `Binn` folder for the instance. For example, in a default installation of [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], the path might be `C:\Program Files\Microsoft SQL Server\MSSQL.13.InstanceNameMSSQL\Binn`.

### Remote compute contexts are blocked by a firewall in SQL Server instances that are running on Azure virtual machines

If you have installed [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)] on an Azure virtual machine, you might not be able to use compute contexts that require the use of the virtual machine's workspace. The reason is that, by default, the firewall on Azure virtual machines includes a rule that blocks network access for local R user accounts.

As a workaround, on the Azure VM, open **Windows Firewall with Advanced Security**, select **Outbound Rules**, and disable the following rule: **Block network access for R local user accounts in SQL Server instance MSSQLSERVER**. You can also leave the rule enabled, but change the security property to **Allow if secure**.

### Implied authentication in SQL Server 2016 Express edition

When you run R jobs from a remote data-science workstation by using Integrated Windows authentication, SQL Server uses *implied authentication* to generate any local ODBC calls that might be required by the script. However, this feature didn't work in the RTM build of [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Express edition.

To fix the issue, we recommend that you upgrade to a later service release. If upgrade isn't feasible, as a workaround, use a SQL login to run remote R jobs that might require embedded ODBC calls.

**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] R Services Express edition

### Performance limits when libraries used by SQL Server are called from other tools

It is possible to call the machine learning libraries that are installed for SQL Server from an external application, such as RGui. Doing so might be the most convenient way to accomplish certain tasks, such as installing new packages, or running ad hoc tests on very short code samples. However, outside of SQL Server, performance might be limited.

For example, even if you are using the Enterprise edition of SQL Server, R runs in single-threaded mode when you run your R code by using external tools. To get the benefits of performance in SQL Server, initiate a SQL Server connection and use [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) to call the external script runtime.

In general, avoid calling the machine learning libraries that are used by SQL Server from external tools. If you need to debug R or Python code, it is typically easier to do so outside of SQL Server. To get the same libraries that are in SQL Server, you can install Microsoft R Client or [SQL Server 2017 Machine Learning Server (Standalone)](../install/sql-machine-learning-standalone-windows-install.md).

### SQL Server Data Tools doesn't support permissions required by external scripts

When you use Visual Studio or SQL Server Data Tools to publish a database project, if any principal has permissions specific to external script execution, you might get an error like this one:

> *TSQL Model: Error detected when reverse engineering the database. The permission was not recognized and was not imported.*

Currently the DACPAC model doesn't support the permissions used by R Services or Machine Learning Services, such as `GRANT ANY EXTERNAL SCRIPT`, or `EXECUTE ANY EXTERNAL SCRIPT`. This issue will be fixed in a later release.

As a workaround, run the additional `GRANT` statements in a post-deployment script.

### External script execution is throttled due to resource governance default values

In Enterprise edition, you can use resource pools to manage external script processes. In some early release builds, the maximum memory that could be allocated to the R processes was 20 percent. Therefore, if the server had 32 GB of RAM, the R executables (`RTerm.exe` and `BxlServer.exe`) could use a maximum of 6.4 GB in a single request.

If you encounter resource limitations, check the current default. If 20 percent isn't enough, see the documentation for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on how to change this value.

**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] R Services, Enterprise edition

### Error when using `sp_execute_external_script` without `libc++.so` on Linux

On a clean Linux machine that doesn't have `libc++.so` installed, running a `sp_execute_external_script` (SPEES) query with Java or an external language fails because `commonlauncher.so` fails to load `libc++.so`.

For example:

```sql
EXECUTE sp_execute_external_script @language = N'Java'
    , @script = N'JavaTestPackage.PassThrough'
    , @parallel = 0
    , @input_data_1 = N'select 1'
WITH RESULT SETS((col1 INT NOT NULL));
GO
```

This fails with a message similar to the following:

```text
Msg 39012, Level 16, State 14, Line 0

Unable to communicate with the runtime for 'Java' script for request id: 94257840-1704-45E8-83D2-2F74AEB46CF7. Please check the requirements of 'Java' runtime.
```

The `mssql-launchpadd` logs will show an error message similar to the following:

```text
Oct 18 14:03:21 sqlextmls launchpadd[57471]: [launchpad] 2019/10/18 14:03:21 WARNING: PopulateLauncher failed: Library /opt/mssql-extensibility/lib/commonlauncher.so not loaded. Error: libc++.so.1: cannot open shared object file: No such file or directory
```

#### Workaround

You can perform one of the following workarounds:

1. Copy `libc++*` from `/opt/mssql/lib` to the default system path `/lib64`

1. Add the following entries to `/var/opt/mssql/mssql.conf` to expose the path:

   ```text
   [extensibility]
   readabledirectories = /opt/mssql
   ```

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] on Linux

### Installation or upgrade error on FIPS enabled servers

If you install [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] with the feature **Machine Learning Services and Language Extensions** or upgrade the SQL Server instance on a [Federal Information Processing Standard (FIPS)](/windows/security/threat-protection/security-policy-settings/system-cryptography-use-fips-compliant-algorithms-for-encryption-hashing-and-signing) enabled server, you will receive the following error:

> *An error occurred while installing extensibility feature with error message: AppContainer Creation Failed with error message NONE, state This implementation is not part of the Windows Platform FIPS validated cryptographic algorithms.*

#### Workaround

Disable FIPS before the installation of [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] with the feature **Machine Learning Services and Language Extensions** or upgrade of the SQL Server instance. Once the installation or upgrade is complete, you can reenable FIPS.

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]

### R libraries using specific algorithms, streaming, or partitioning

#### Issue

The following limitations apply on [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] with runtime upgrade. This issue applies to Enterprise edition.

- Parallelism: `RevoScaleR` and `MicrosoftML` algorithm thread parallelism for scenarios are limited to maximum of two threads.
- Streaming & partitioning: Scenarios involving `@r_rowsPerRead` parameter passed to T-SQL `sp_execute_external_script` isn't applied.
- Streaming & partitioning: `RevoScaleR` and `MicrosoftML` data sources (that is, `ODBC`, `XDF`) doesn't support reading rows in chunks for training or scoring scenarios. These scenarios always bring all data to memory for computation and the operations are memory bound

#### Solution

The best solution is to upgrade to [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)]. Alternatively you can continue to use [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)] with runtime upgrade configured using [RegisterRext.exe /configure](../install/change-default-language-runtime-version.md), after you complete the following tasks.

1. Edit registry to create a key `Computer\HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Microsoft SQL Server\150` and add a value `SharedCode` with data `C:\Program Files\Microsoft SQL Server\150\Shared` or the instance shared directory, as configured.
1. Create a folder `C:\Program Files\Microsoft SQL Server\150\Shared and copy instapi140.dll` from the folder `C:\Program Files\Microsoft SQL Server\140\Shared` to the newly created folder.
1. Rename the `instapi140.dll` to `instapi150.dll` in the new folder `C:\Program Files\Microsoft SQL Server\150\Shared`.

> [!IMPORTANT]  
> If you do the steps above, you must manually remove the added key prior to upgrading to a later version of SQL Server.

## R script execution issues

This section contains known issues that are specific to running R on SQL Server, as well as some issues that are related to the R libraries and tools published by Microsoft, including RevoScaleR.

For additional known issues that might affect R solutions, see the [Machine Learning Server](/machine-learning-server/resources-known-issues) site.

### Access denied warning when executing R scripts on SQL Server in a non default location

If the instance of SQL Server has been installed to a non-default location, such as outside the `Program Files` folder, the warning ACCESS_DENIED is raised when you try to run scripts that install a package. For example:

> *In `normalizePath(path.expand(path), winslash, mustWork)` : path[2]="~ExternalLibraries/R/8/1": Access is denied*

The reason is that an R function attempts to read the path, and fails if the built-in users group **SQLRUserGroup**, doesn't have read access. The warning that is raised doesn't block execution of the current R script, but the warning might recur repeatedly whenever the user runs any other R script.

If you have installed SQL Server to the default location, this error doesn't occur, because all Windows users have read permissions on the `Program Files` folder.

This issue is addressed in an upcoming service release. As a workaround, provide the group, **SQLRUserGroup**, with read access for all parent folders of `ExternalLibraries`.

### Serialization error between old and new versions of RevoScaleR

When you pass a model using a serialized format to a remote SQL Server instance, you might get the error:

> *Error in memDecompress(data, type = decompress) internal error -3 in memDecompress(2).*

This error is raised if you saved the model using a recent version of the serialization function, [rxSerializeModel](/machine-learning-server/r-reference/revoscaler/rxserializemodel), but the SQL Server instance where you deserialize the model has an older version of the RevoScaleR APIs, from [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU 2 or earlier.

As a workaround, you can upgrade the [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] instance to CU 3 or later.

The error doesn't appear if the API version is the same, or if you are moving a model saved with an older serialization function to a server that uses a newer version of the serialization API.

In other words, use the same version of RevoScaleR for both serialization and deserialization operations.

### Real-time scoring doesn't correctly handle the *learningRate* parameter in tree and forest models

If you create a model using a decision tree or decision forest method and specify the learning rate, you might see inconsistent results when using `sp_rxpredict` or the SQL `PREDICT` function, as compared to using `rxPredict`.

The cause is an error in the API that processes serialized models, and is limited to the `learningRate` parameter: for example, in [rxBTrees](/machine-learning-server/r-reference/revoscaler/rxbtrees), or

This issue is addressed in an upcoming service release.

### Limitations on processor affinity for R jobs

In the initial release build of [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], you could set processor affinity only for CPUs in the first k-group. For example, if the server is a 2-socket machine with two k-groups, only processors from the first k-group are used for the R processes. The same limitation applies when you configure resource governance for R script jobs.

This issue is fixed in [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] Service Pack 1. We recommend that you upgrade to the latest service release.

**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] R Services (RTM version)

### Changes to column types can't be performed when reading data in a SQL Server compute context

If your compute context is set to the SQL Server instance, you can't use the *colClasses* argument (or other similar arguments) to change the data type of columns in your R code.

For example, the following statement would result in an error if the column CRSDepTimeStr isn't already an integer:

```R
data <- RxSqlServerData(
  sqlQuery = "SELECT CRSDepTimeStr, ArrDelay FROM AirlineDemoSmall",
  connectionString = connectionString,
  colClasses = c(CRSDepTimeStr = "integer"))
```

As a workaround, you can rewrite the SQL query to use `CAST` or `CONVERT` and present the data to R by using the correct data type. In general, performance is better when you work with data by using SQL rather than by changing data in the R code.

**Applies to:** [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] R Services

### Limits on size of serialized models

When you save a model to a SQL Server table, you must serialize the model and save it in a binary format. Theoretically the maximum size of a model that can be stored with this method is 2 GB, which is the maximum size of varbinary columns in SQL Server.

If you need to use larger models, the following workarounds are available:

- Take steps to reduce the size of your model. Some open source R packages include a great deal of information in the model object, and much of this information can be removed for deployment.
- Use feature selection to remove unnecessary columns.
- If you are using an open source algorithm, consider a similar implementation using the corresponding algorithm in MicrosoftML or RevoScaleR. These packages have been optimized for deployment scenarios.
- After the model has been rationalized and the size reduced using the preceding steps, see if the [memCompress](https://www.rdocumentation.org/packages/base/versions/3.4.1/topics/memCompress) function in base R can be used to reduce the size of the model before passing it to SQL Server. This option is best when the model is close to the 2-GB limit.
- For larger models, you can use the SQL Server [FileTable](../../relational-databases/blob/filetables-sql-server.md) feature to store the models, rather than using a varbinary column.

  To use FileTables, you must add a firewall exception, because data stored in FileTables is managed by the Filestream filesystem driver in SQL Server, and default firewall rules block network file access. For more information, see [Enable Prerequisites for FileTable](../../relational-databases/blob/enable-the-prerequisites-for-filetable.md).

  After you have enabled FileTable, to write the model, you get a path from SQL using the FileTable API, and then write the model to that location from your code. When you need to read the model, you get the path from SQL Server, and then call the model using the path from your script. For more information, see [Access FileTables with File Input-Output APIs](../../relational-databases/blob/access-filetables-with-file-input-output-apis.md).

### Avoid clearing workspaces when you execute R code in a SQL Server compute context

If you use an R command to clear your workspace of objects while running R code in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] compute context, or if you clear the workspace as part of an R script called by using [sp_execute_external_script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md), you might get this error: *workspace object revoScriptConnection not found*

`revoScriptConnection` is an object in the R workspace that contains information about an R session that is called from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. However, if your R code includes a command to clear the workspace (such as `rm(list=ls()))`, all information about the session and other objects in the R workspace is cleared as well.

As a workaround, avoid indiscriminate clearing of variables and other objects while you're running R in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Although clearing the workspace is common when working in the R console, it can have unintended consequences.

- To delete specific variables, use the R `remove` function: for example, `remove('name1', 'name2', ...)`
- If there are multiple variables to delete, save the names of temporary variables to a list and perform periodic garbage collection.

### Restrictions on data that can be provided as input to an R script

You can't use in an R script the following types of query results:

- Data from a [!INCLUDE[tsql](../../includes/tsql-md.md)] query that references AlwaysEncrypted columns.

- Data from a [!INCLUDE[tsql](../../includes/tsql-md.md)] query that references masked columns.

  If you need to use masked data in an R script, a possible workaround is to make a copy of the data in a temporary table and use that data instead.

### Use of strings as factors can lead to performance degradation

Using string type variables as factors can greatly increase the amount of memory used for R operations. This is a known issue with R in general, and there are many articles on the subject. For example, see [Factors aren't first-class citizens in R, by John Mount, in R-bloggers)](https://www.r-bloggers.com/factors-are-not-first-class-citizens-in-r/) or [stringsAsFactors: An unauthorized biography](https://simplystats.github.io/2015/07/24/stringsasfactors-an-unauthorized-biography/), by Roger Peng.

Although the issue isn't specific to SQL Server, it can greatly affect performance of R code run in SQL Server. Strings are typically stored as **varchar** or **nvarchar**, and if a column of string data has many unique values, the process of internally converting these to integers and back to strings by R can even lead to memory allocation errors.

If you don't absolutely require a string data type for other operations, mapping the string values to a numeric (integer) data type as part of data preparation would be beneficial from a performance and scale perspective.

For a discussion of this issue, and other tips, see [Performance for R Services - data optimization](../r/r-and-data-optimization-r-services.md).

### Arguments *varsToKeep* and *varsToDrop* aren't supported for SQL Server data sources

When you use the rxDataStep function to write results to a table, using the *varsToKeep* and *varsToDrop* is a handy way of specifying the columns to include or exclude as part of the operation. However, these arguments aren't supported for SQL Server data sources.

### Limited support for SQL data types in `sp_execute_external_script`

Not all data types that are supported in SQL can be used in R. As a workaround, consider casting the unsupported data type to a supported data type before passing the data to `sp_execute_external_script`.

For more information, see [R libraries and data types](../r/r-libraries-and-data-types.md).

### Possible string corruption using Unicode strings in varchar columns

Passing Unicode data in **varchar** columns from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to R/Python can result in string corruption. This is due to the encoding for these Unicode strings in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collations may not match with the default UTF-8 encoding used in R/Python.

To send any non-ASCII string data from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to R/Python, use UTF-8 encoding (available in [!INCLUDE[sql-server-2019](../../includes/sssql19-md.md)]) or use **nvarchar** type for the same.

### Only one value of type `raw` can be returned from `sp_execute_external_script`

When a binary data type (the R **raw** data type) is returned from R, the value must be sent in the output data frame.

With data types other than **raw**, you can return parameter values along with the results of the stored procedure by adding the OUTPUT keyword. For more information, see [Parameters](../../relational-databases/stored-procedures/parameters.md).

If you want to use multiple output sets that include values of type **raw**, one possible workaround is to do multiple calls of the stored procedure, or to send the result sets back to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using ODBC.

### Loss of precision

Because [!INCLUDE[tsql](../../includes/tsql-md.md)] and R support various data types, numeric data types can suffer loss of precision during conversion.

For more information about implicit data-type conversion, see [R libraries and data types](../r/r-libraries-and-data-types.md).

### Variable scoping error when you use the transformFunc parameter

To transform data while you are modeling, you can pass a *transformFunc* argument in a function such as `rxLinmod` or `rxLogit`. However, nested function calls can lead to scoping errors in the SQL Server compute context, even if the calls work correctly in the local compute context.

> *The sample data set for the analysis has no variables*

For example, assume that you have defined two functions, `f` and `g`, in your local global environment, and `g` calls `f`. In distributed or remote calls involving `g`, the call to `g` might fail with this error, because `f` can't be found, even if you have passed both `f` and `g` to the remote call.

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

### Data import and manipulation using RevoScaleR

When **varchar** columns are read from a database, white space is trimmed. To prevent this, enclose strings in non-white-space characters.

When functions such as `rxDataStep` are used to create database tables that have **varchar** columns, the column width is estimated based on a sample of the data. If the width can vary, it might be necessary to pad all strings to a common length.

Using a transform to change a variable's data type isn't supported when repeated calls to `rxImport` or `rxTextToXdf` are used to import and append rows, combining multiple input files into a single .xdf file.

### Limited support for `rxExec`

In [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)], the `rxExec` function that's provided by the RevoScaleR package can be used only in single-threaded mode.

### Increase the maximum parameter size to support rxGetVarInfo

If you use data sets with extremely large numbers of variables (for example, over 40,000), set the `max-ppsize` flag when you start R to use functions such as `rxGetVarInfo`. The `max-ppsize` flag specifies the maximum size of the pointer protection stack.

If you are using the R console (for example, RGui.exe or RTerm.exe), you can set the value of _max-ppsize_ to 500000 by typing:

```R
R --max-ppsize=500000
```

### Issues with the rxDTree function

The `rxDTree` function doesn't currently support in-formula transformations. In particular, using the `F()` syntax for creating factors on the fly isn't supported. However, numeric data is automatically binned.

Ordered factors are treated the same as factors in all RevoScaleR analysis functions except `rxDTree`.

### `data.table` as an OutputDataSet in R

Using `data.table` as an `OutputDataSet` in R isn't supported in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Cumulative Update 13 (CU 13) and earlier. The following message might appear:

``` text
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

`data.table` as an `OutputDataSet` in R is supported in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Cumulative Update 14 (CU 14) and later.

### Running a long script fails while installing a library

Running a long running external script session and having the dbo in parallel trying to install a library on a different database can terminate the script.

For example, running this external script against master:

```sql
USE MASTER
DECLARE @language nvarchar(1) = N'R'
DECLARE @script nvarchar(max) = N'Sys.sleep(100)'
DECLARE @input_data_1 nvarchar(max) = N'select 1'
EXEC sp_execute_external_script @language = @language, @script = @script, @input_data_1 = @input_data_1 with result sets none
go
```

While the dbo in parallel installs a library in LibraryManagementFunctional:

```sql
USE [LibraryManagementFunctional]
go

CREATE EXTERNAL LIBRARY [RODBC] FROM (CONTENT = N'/home/ani/var/opt/mssql/data/RODBC_1.3-16.tar.gz') WITH (LANGUAGE = 'R')
go

DECLARE @language nvarchar(1) = N'R'
DECLARE @script nvarchar(14) = N'library(RODBC)'
DECLARE @input_data_1 nvarchar(8) = N'select 1'
EXEC sp_execute_external_script @language = @language, @script = @script, @input_data_1 = @input_data_1
go
```

The previous long running external script against master will terminate with the following error message:

> *A 'R' script error occurred during execution of 'sp_execute_external_script' with HRESULT 0x800704d4.*

#### Workaround

Don't run the library install in parallel to the long-running query. Or rerun the long running query after the installation is complete.

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] on Linux & Big Data Clusters only.

### SQL Server stops responding when executing R scripts containing parallel execution

[!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] contains a regression that affects R scripts that use parallel execution. Examples include using `rxExec` with `RxLocalPar` compute context and scripts that use the parallel package. This problem is caused by errors the parallel package encounters when writing to the null device while executing in SQL Server.

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)].

### Precision loss for money/numeric/decimal/bigint data types

Executing an R script with `sp_execute_external_script` allows money, numeric, decimal, and bigint data types as input data. However, because they are converted to R's numeric type, they suffer a precision loss with values that are very high or have decimal point values.

- **money**: Sometimes cent values would be imprecise and a warning would be issued: *Warning: unable to precisely represent cents values*.
- **numeric/decimal**: `sp_execute_external_script` with an R script doesn't support the full range of those data types and would alter the last few decimal digits especially those with fraction.
- **bigint**: R only support up to 53-bit integers and then it will start to have precision loss.

## Python script execution issues

This section contains known issues that are specific to running Python on SQL Server, as well as issues that are related to the Python packages published by Microsoft, including [revoscalepy](/r-server/python-reference/revoscalepy/revoscalepy-package) and [microsoftml](/r-server/python-reference/microsoftml/microsoftml-package).

### Call to pretrained model fails if path to model is too long

If you installed the pretrained models in an early release of [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)], the complete path to the trained model file might be too long for Python to read. This limitation is fixed in a later service release.

There are several potential workarounds:

- When you install the pretrained models, choose a custom location.
- If possible, install the SQL Server instance under a custom installation path with a shorter path, such as `C:\SQL\MSSQL14.MSSQLSERVER`.
- Use the Windows utility [Fsutil](/previous-versions/windows/it-pro/windows-server-2012-R2-and-2012/cc788097(v=ws.11)) to create a hard link that maps the model file to a shorter path.
- Update to the latest service release.

### Error when saving serialized model to SQL Server

When you pass a model to a remote SQL Server instance, and try to read the binary model using the `rx_unserialize` function in [revoscalepy](/machine-learning-server/python-reference/revoscalepy/revoscalepy-package), you might get the error:

> *NameError: name 'rx_unserialize_model' is not defined*

This error is raised if you saved the model using a recent version of the serialization function, but the SQL Server instance where you deserialize the model doesn't recognize the serialization API.

To resolve the issue, upgrade the [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] instance to CU 3 or later.

### Failure to initialize a varbinary variable causes an error in `BxlServer`

If you run Python code in SQL Server using `sp_execute_external_script`, and the code has output variables of type **varbinary(max)**, **varchar(max)** or similar types, the variable must be initialized or set as part of your script. Otherwise, the data exchange component, BxlServer, raises an error and stops working.

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

### Telemetry warning on successful execution of Python code

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] CU 2, the following message might appear even if Python code otherwise runs successfully:

> *STDERR message(s) from external script:*
> *~PYTHON_SERVICES\lib\site-packages\revoscalepy\utils\RxTelemetryLogger*
> *SyntaxWarning: telemetry_state is used prior to global declaration*

This issue has been fixed in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Cumulative Update 3 (CU 3).

### Numeric, decimal, and money data types not supported

Beginning with [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Cumulative Update 12 (CU 12), numeric, decimal and money data types in WITH RESULT SETS are unsupported when using Python with `sp_execute_external_script`. The following messages might appear:

> *[Code: 39004, SQL State: S1000]  A 'Python' script error occurred during execution of'sp_execute_external_script' with HRESULT 0x80004004.*
>
> *[Code: 39019, SQL State: S1000]  An external script error occurred:*
>
> *SqlSatelliteCall error: Unsupported type in output schema. Supported types: bit, smallint, int, datetime, smallmoney, real and float. char, varchar are partially supported.*

This has been fixed in [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] Cumulative Update 14 (CU 14).

### Bad interpreter error when installing Python packages with pip on Linux

On [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)], if you try to use **pip**. For example:

```bash
/opt/mssql/mlservices/runtime/python/bin/pip -h
```

You will then get this error:

> *bash: /opt/mssql/mlservices/runtime/python/bin/pip: /opt/microsoft/mlserver/9.4.7/bin/python/python: bad interpreter: No such file or directory*

#### Workaround

Install **pip** from the [Python Package Authority (PyPA)](https://www.pypa.io):

```bash
wget 'https://bootstrap.pypa.io/get-pip.py'
/opt/mssql/mlservices/bin/python/python ./get-pip.py
```

#### Recommendation

See [Install Python packages with sqlmlutils](../package-management/install-additional-python-packages-on-sql-server.md).

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] on Linux

### <a id="python-pip"></a> Unable to install Python packages using `pip` after installing SQL Server 2019 on Windows

After installing [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] on Windows, attempting to install a python package via **pip** from a DOS command line will fail. For example:

```bash
pip install quantfolio
```

This will return the following error:

> *pip is configured with locations that require TLS/SSL, however the ssl module in Python is not available.*

This is a problem specific to the Anaconda package. It will be fixed in an upcoming service release.

#### Workaround

Copy the following files:

- `libssl-1_1-x64.dll`
- `libcrypto-1_1-x64.dll`

from the folder <br>
`C:\Program Files\Microsoft SQL Server\MSSSQL15.MSSQLSERVER\PYTHON_SERVICES\Library\bin`

to the folder <br>
`C:\Program Files\Microsoft SQL Server\MSSSQL15.MSSQLSERVER\PYTHON_SERVICES\DLLs`

Then open a new DOS command shell prompt.

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] on Windows

### Error when using `sp_execute_external_script` without `libc++abo.so` on Linux

On a clean Linux machine that doesn't have `libc++abi.so` installed, running a `sp_execute_external_script` (SPEES) query fails with a "No such file or directory" error.

For example:

```sql
EXEC sp_execute_external_script
    @language = N'Python'
    , @script = N'
OutputDataSet = InputDataSet'
    , @input_data_1 = N'select 1'
    , @input_data_1_name = N'InputDataSet'
    , @output_data_1_name = N'OutputDataSet'
    WITH RESULT SETS (([output] int not null));
```

```output
Msg 39012, Level 16, State 14, Line 0
Unable to communicate with the runtime for 'Python' script for request id: 94257840-1704-45E8-83D2-2F74AEB46CF7. Please check the requirements of 'Python' runtime.
STDERR message(s) from external script:

Failed to load library /opt/mssql-extensibility/lib/sqlsatellite.so with error libc++abi.so.1: cannot open shared object file: No such file or directory.

SqlSatelliteCall error: Failed to load library /opt/mssql-extensibility/lib/sqlsatellite.so with error libc++abi.so.1: cannot open shared object file: No such file or directory.
STDOUT message(s) from external script:
SqlSatelliteCall function failed. Please see the console output for more information.
Traceback (most recent call last):
  File "/opt/mssql/mlservices/libraries/PythonServer/revoscalepy/computecontext/RxInSqlServer.py", line 605, in rx_sql_satellite_call
    rx_native_call("SqlSatelliteCall", params)
  File "/opt/mssql/mlservices/libraries/PythonServer/revoscalepy/RxSerializable.py", line 375, in rx_native_call
    ret = px_call(functionname, params)
RuntimeError: revoscalepy function failed.
Total execution time: 00:01:00.387
```

#### Workaround

Run the following command:

```bash
sudo cp /opt/mssql/lib/libc++abi.so.1 /opt/mssql-extensibility/lib/
```

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] on Linux

### <a id="modprobe"></a> Firewall rule creation error in `modprobe` when running `mssql-launchpadd` on Linux

When viewing the logs of `mssql-launchpadd` using `sudo journalctl -a -u mssql-launchpadd`, you might see a firewall rule creation error similar to the following output.

```output
-- Logs begin at Sun 2021-03-28 12:03:30 PDT, end at Wed 2022-10-12 13:20:17 PDT. --
Mar 22 16:57:51 sqlVm systemd[1]: Started Microsoft SQL Server Extensibility Launchpad Daemon.
Mar 22 16:57:51 sqlVm launchpadd[195658]: 2022/03/22 16:57:51 [launchpadd] INFO: Extensibility Log Header: <timestamp> <process> <sandboxId> <sessionId> <message>
Mar 22 16:57:51 sqlVm launchpadd[195658]: 2022/03/22 16:57:51 [launchpadd] INFO: No extensibility section in /var/opt/mssql/mssql.conf file. Using default settings.
Mar 22 16:57:51 sqlVm launchpadd[195658]: 2022/03/22 16:57:51 [launchpadd] INFO: DataDirectories =  /bin:/etc:/lib:/lib32:/lib64:/sbin:/usr/bin:/usr/include:/usr/lib:/usr/lib32:/usr/lib64:/usr/libexec/gcc:/usr/sbin:/usr/share:/var/lib:/opt/microsoft:/opt/mssql-extensibility:/opt/mssql/mlservices:/opt/mssql/lib/zulu-jre-11:/opt/mssql-tools
Mar 22 16:57:51 sqlVm launchpadd[195658]: 2022/03/22 16:57:51 [launchpadd] INFO: [RG] SQL Extensibility Cgroup initialization is done.
Mar 22 16:57:51 sqlVm launchpadd[195658]: 2022/03/22 16:57:51 [launchpadd] INFO: Found 1 IP address(es) from the bridge.
Mar 22 16:57:51 sqlVm launchpadd[195676]: modprobe: ERROR: could not insert 'ip6_tables': Operation not permitted
Mar 22 16:57:51 sqlVm launchpadd[195673]: ip6tables v1.8.4 (legacy): can't initialize ip6tables table `filter': Table does not exist (do you need to insmod?)
Mar 22 16:57:51 sqlVm launchpadd[195673]: Perhaps ip6tables or your kernel needs to be upgraded.
Mar 22 16:57:51 sqlVm launchpadd[195678]: modprobe: ERROR: could not insert 'ip6_tables': Operation not permitted
Mar 22 16:57:51 sqlVm launchpadd[195677]: ip6tables v1.8.4 (legacy): can't initialize ip6tables table `filter': Table does not exist (do you need to insmod?)
Mar 22 16:57:51 sqlVm launchpadd[195677]: Perhaps ip6tables or your kernel needs to be upgraded.
Mar 22 16:57:51 sqlVm launchpadd[195670]: 2022/03/22 16:57:51 [setnetbr] ERROR: Failed to set firewall rules: exit status 3
```

#### Workaround

Run the following commands to configure `modprobe`, and restart the SQL Server Launchpad service:

```bash
sudo modprobe ip6_tables
sudo systemctl restart mssql-launchpadd
```

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later on Linux

### <a id="tensorflow"></a> Can't install `tensorflow` package using `sqlmlutils`

The [sqlmlutils package](../package-management/install-additional-python-packages-on-sql-server.md) is used to install Python packages in [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]. You need to download, install, and update the [Microsoft Visual C++ 2015-2019 Redistributable (x64)](https://visualstudio.microsoft.com/downloads/). However, the `tensorflow` package can't be installed using sqlmlutils. The `tensorflow` package depends on a newer version of `numpy` than the version installed in SQL Server. However, `numpy` is a preinstalled system package that `sqlmlutils` can't update when trying to install `tensorflow`.

#### Workaround

Using a command prompt in administrator mode, run the following command, replacing "MSSQLSERVER" with the name of your SQL instance:

```cmd
"C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\PYTHON_SERVICES\python.exe" -m pip install --upgrade tensorflow
```

If you get a "TLS/SSL" error, see [7. Unable to install Python packages using pip](#python-pip) earlier in this article.

**Applies to:** [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] on Windows

## Revolution R Enterprise and Microsoft R Open

This section lists issues specific to R connectivity, development, and performance tools that are provided by Revolution Analytics. These tools were provided in earlier pre-release versions of [!INCLUDE[ssnoversion](../../includes/ssnoversion-md.md)].

In general, we recommend that you uninstall these previous versions and install the latest version of SQL Server or Microsoft R Server.

### Revolution R Enterprise isn't supported

Installing Revolution R Enterprise side by side with any version of [!INCLUDE[rsql_productname_md](../../includes/rsql-productname-md.md)] isn't supported.

If you have an existing license for Revolution R Enterprise, you must put it on a separate computer from both the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance and any workstation that you want to use to connect to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] instance.

Some pre-release versions of [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)] included an R development environment for Windows that was created by Revolution Analytics. This tool is no longer provided, and isn't supported.

For compatibility with [!INCLUDE[rsql_productname](../../includes/rsql-productname-md.md)], we recommend that you install Microsoft R Client instead. [R Tools for Visual Studio](https://marketplace.visualstudio.com/items?itemName=MikhailArkhipov007.RTVS2019) and [Visual Studio Code](https://code.visualstudio.com/) also supports Microsoft R solutions.

### Compatibility issues with SQLite ODBC driver and RevoScaleR

Revision 0.92 of the SQLite ODBC driver is incompatible with RevoScaleR. Revisions 0.88-0.91 and 0.93 and later are known to be compatible.

## Next steps

- [Collect data to troubleshoot SQL Server Machine Learning Services](data-collection-ml-troubleshooting-process.md)
