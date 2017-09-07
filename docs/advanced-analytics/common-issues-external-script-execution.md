---
title: "Common issues with external script execution in SQL Server| Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "08/20/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "r-services"
ms.tgt_pltfrm: ""
ms.topic: "article"
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Common issues with external script execution in SQL Server

This article contains a list of known issues and common problems with running R or Python code in SQL Server.

Before you get started, we recommend that you collect some information about your system. To learn how, see [Data collection for troubleshooting](data-collection-ml-troubleshooting-process.md).

Also, review a list of common issues that are specific to initial setup or configuration: [Setup and upgrade FAQ](r/upgrade-and-installation-faq-sql-server-r-services.md).

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services

## Launchpad issues

The SQL Server Trusted Launchpad service manages the execution of external scripts and communication with R, Python, or other external runtimes. Multiple issues can prevent Launchpad from starting, including configuration problems or changes, or missing network protocols.

As part of the troubleshooting process, begin by answering the following questions:

- Did Launchpad always fail to run, or did it stop working?
- What service account is Launchpad running under?
- What user rights does the Launchpad service account have?

### Determine whether Launchpad is running

1. Open the **Services** panel (Services.msc). Or, from the command line, type **SQLServerManager13.msc** or **SQLServerManager14.msc** to open [SQL Server Configuration Manager](https://docs.microsoft.com/sql/relational-databases/sql-server-configuration-manager).

2. Make a note of the service account that Launchpad is running under. Each instance where R or Python is enabled should have its own instance of the Launchpad service. For example, the service for a named instance might be something like _MSSQLLaunchpad$InstanceName_.

3. If the service is stopped, restart it. On restarting, if there are any issues with configuration, a message is published in the system event log, and the service is stopped again. Check the system event log for details about why the service stopped.

4. Review the contents of RSetup.log, and make sure that there are no errors in the setup. For example, the message *Exiting with code 0* indicates failure of the service to start.

5. To look for other errors, review the contents of rlauncher.log.

### Check the Launchpad service account

The default service account might be "NT Service\$SQL2016" or "NT Service\$SQL2017". The final part might vary, depending on your SQL instance name.

The Launchpad service (Launchpad.exe) runs by using a low-privilege service account. However, to start R and Python and communicate with the database instance, the Launchpad service account requires the following user rights:

- Log on as a service (SeServiceLogonRight)
- Replace a process-level token (SeAssignPrimaryTokenPrivilege)
- Bypass traverse checking (SeChangeNotifyPrivilege)
- Adjust memory quotas for a process (SeIncreaseQuotaSizePrivilege)

For information about these user rights, see the "Windows privileges and rights" section in [Configure Windows service accounts and permissions](https://msdn.microsoft.com/library/ms143504.aspx).

> [!TIP]
> If you are familiar with the use of the Support Diagnostics Platform (SDP) tool for SQL Server diagnostics, you can use SDP to review the output file with the name MachineName_UserRights.txt.

### Review Launchpad requirements

Any of several issues can prevent Launchpad from starting. The Launchpad service might start and then stop or crash, or the service might stop with a connection timeout. In these cases, the system has usually been changed or configured such that Launchpad cannot run.

#### Determine whether 8dot3 notation is enabled

For compatibility with R, SQL Server 2016 R Services (In-Database) required the drive where the feature is installed to support the creation of short file names by using *8dot3 notation*. An 8.3 file name is also called a *short file name*, and it's used for compatibility with earlier versions of Microsoft Windows or as an alternative to long file names.

If the volume where you are installing R does not support short file names, the processes that launch R from SQL Server might not be able to locate the correct executable, and Launchpad will not start.

As a workaround, you can enable the 8dot3 notation on the volume where SQL Server is installed and where R Services is installed. You must then provide the short name for the working directory in the R Services configuration file.

1. To enable 8dot3 notation, run the fsutil utility with the *8dot3name* argument as described here: [fsutil 8dot3name](https://technet.microsoft.com/library/ff621566(v=ws.11).aspx).

2. After the 8dot3 notation is enabled, open the RLauncher.config file and note the property of `WORKING_DIRECTORY`. For information about how to find this file, see [Data collection for Machine Learning troubleshooting](data-collection-ml-troubleshooting-process.md).

3. Use the fsutil utility with the *file* argument to specify a short file path for the folder that's specified in WORKING_DIRECTORY.

4. Edit the configuration file to specify the same working directory that you entered in the WORKING_DIRECTORY property. Alternatively, you can specify a different working directory and choose an existing path that's already compatible with the 8dot3 notation.

> [!NOTE] 
> This restriction has been removed in later releases. If you experience this issue, install one of the following:
> * SQL Server 2016 SP1 and CU1: [Cumulative Update 1 for SQL Server](https://support.microsoft.com/help/3208177/cumulative-update-1-for-sql-server-2016-sp1).
> * SQL Server 2016 RTM, Service Pack 3, and this [hotfix](https://support.microsoft.com/help/3210110/on-demand-hotfix-update-package-for-sql-server-2016-cu3), which is available on demand.

#### The user group for Launchpad cannot log on locally

During setup of Machine Learning services, SQL Server creates the Windows user group **SQLRUserGroup** and then provisions it with all rights necessary for Launchpad to connect to SQL Server and run external script jobs. If this user group is enabled, it is also used to execute Python scripts.

However, in organizations where more restrictive security policies are enforced, the rights that are required by this group might have been manually removed, or they might be automatically revoked by policy. If the rights have been removed, Launchpad can no longer connect to SQL Server, and SQL Server cannot call the external runtime.

To correct the problem, ensure that the group **SQLRUserGroup** has the system right **Allow log on locally**.

For more information, see [Configure Windows Service Accounts and Permissions](https://msdn.microsoft.com/library/ms143504.aspx#Windows).

#### Improper setup leading to mismatched DLLs

If you install the database engine with other features, patch the server, and then later add the Machine Learning feature by using the original media, the wrong version of the Machine Learning components might be installed. When Launchpad detects a version mismatch, it shuts down and creates a dump file.

To avoid this problem, be sure to install any new features at the same patch level as the server instance.

**The wrong way to upgrade**:

1. Install SQL Server 2016 without R Services.
2. Upgrade SQL Server 2016 Cumulative Update 2.
3. Install R Services (In-Database) by using the RTM media.

**The correct way to upgrade**:

1. Install SQL Server 2016 without R Services.
2. Upgrade SQL Server 2016 to the desired patch level. For example, install Service Pack 1 and then Cumulative Update 2.
3. To add the feature at the correct patch level, run SP1 and CU2 setup again, and then choose R Services (In-Database). 

#### Check to see whether a user has rights to run external scripts

Even if Launchpad is configured correctly, it returns an error if the user does not have permission to run R or Python scripts.

If you installed SQL Server as a database administrator or you are a database owner, you are automatically granted this permission. However, other users usually have more limited permissions. If they try to run an R script, they get a Launchpad error.

To correct the problem, in SQL Server Management Studio, a security administrator can modify the SQL login or Windows user account by running the following script:

```SQL
GRANT EXECUTE ANY EXTERNAL SCRIPT
```

### Common Launchpad errors

This section lists the most common error messages that Launchpad returns.

#### Error: *Unable to launch runtime for R script*

If the Windows group for R users (also used for Python) cannot log on to the instance that is running R Services, you might see the following errors:

- Errors generated when you try to run R scripts:

    * *Unable to launch runtime for 'R' script. Please check the configuration of the 'R' runtime.*

    * *An external script error occurred. Unable to launch the runtime.*

- Errors generated by the
    [!INCLUDE[rsql_launchpad](../includes/rsql-launchpad-md.md)] service:

    * *Failed to initialize the launcher RLauncher.dll*

    * *No launcher dlls were registered!*

    * *Security logs indicate that the account NT SERVICE was unable to log on*

For information about how to grant this user group the necessary permissions, see [Set up SQL Server R Services](r/set-up-sql-server-r-services-in-database.md).

> [!NOTE]
> This limitation does not apply if you use SQL logins to run R scripts from a remote workstation.

#### Error: *Logon failure: the user has not been granted the requested logon type*

By default, [!INCLUDE[rsql_launchpad_md](../includes/rsql-launchpad-md.md)] uses the following account on startup: `NT Service\MSSQLLaunchpad`. The account is configured by [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)] setup to have all necessary permissions.

If you assign a different account to Launchpad, or the right is removed by a policy on the SQL Server machine, the account might not have the necessary permissions, and you might see this error:

>*ERROR_LOGON_TYPE_NOT_GRANTED 1385 (0x569) Logon failure: the user has not been granted the requested logon type at this computer.*

To grant the necessary permissions to the new service account, use the Local Security Policy application, and update the permissions on the account to include the following permissions:

+ Adjust memory quotas for a process (SeIncreaseQuotaPrivilege)
+ Bypass traverse checking (SeChangeNotifyPrivilege)
+ Log on as a service (SeServiceLogonRight)
+ Replace a process-level token (SeAssignPrimaryTokenPrivilege)

#### Error: *Unable to communicate with the Launchpad service*

If you have installed and then enabled machine learning, but you get this error when you try to run an R or Python script, the Launchpad service for the instance might have stopped running.

1. From a Windows command prompt, open the SQL Server Configuration Manager. For more information, see [SQL Server Configuration Manager](https://docs.microsoft.com/sql/relational-databases/sql-server-configuration-manager).

2. Right-click SQL Server Launchpad for the instance, and then select **Properties**.

3. Select the **Service** tab, and then verify that the service is running. If it is not running, change the **Start Mode** to **Automatic**, and then select **Apply**.

4. Restarting the service usually fixes the problem so that machine learning scripts can run. If the restart does not fix the issue, note the path and the arguments in the **Binary Path** property, and do the following:

    a. Review the launcher's .config file and ensure that the working directory is valid.

    b. Ensure that the Windows group that's used by Launchpad can connect to the SQL Server instance, as described in the [previous section](#bkmk_LaunchpadTS).

    c. If you change any of the service properties, restart the Launchpad service.

#### Error: *Fatal error creation of tmpFile failed*

In this scenario, you have successfully installed machine learning features, and Launchpad is running. You try to run some simple R or Python code, but Launchpad fails with an error like the following: 

>*Unable to communicate with the runtime for R script. Please check the requirements of R runtime.*

At the same time, the external script runtime writes the following message as part of the STDERR message: 

>*Fatal error: creation of tmpfile failed.*

This error indicates that the account that Launchpad is attempting to use does not have permission to log on to the database. This situation can happen when strict security policies are implemented. To determine whether this is the case, review the SQL Server logs, and check to see whether the MSSQLSERVER01 account was denied at login. The same information is provided in the logs that are specific to R\_SERVICES or PYTHON\_SERVICES. Look for ExtLaunchError.log.

By default, 20 accounts are set up and associated with the Launchpad.exe process, with the names MSSQLSERVER01 through MSSQLSERVER20. If you make heavy use of R or Python, you can increase the number of accounts.

To resolve the issue, ensure that the group has *Allow Log on Locally* permissions to the local instance where machine learning features have been installed and enabled. In some environments, this permission level might require a GPO exception from the network administrator.

## R script issues

This section contains some common issues that are specific to R script execution and R script errors. The list is not comprehensive, because there are many R packages, and errors might differ between versions of the same R package. We recommend that you post R script errors on the [Microsoft R Server forum](https://social.msdn.microsoft.com/Forums/home?forum=MicrosoftR), which supports all related products: R Services (In-Database), Machine Learning Services with Python, Microsoft R Client, and Microsoft R Server.

### Multiple R instances on the same computer

It is easy to install multiple distributions of R on the same computer, or to install multiple copies of the same R package in different versions. For example, if you install both Machine Learning Server (Standalone) and Machine Learning Services (In-Database), the installers create separate versions of the R libraries. 

The duplication can become confusing when you try to run a script from a command line and you're not sure which libraries you are using. It can also be confusing if you install a package to the wrong library and cannot find the package when you try to run it from SQL Server.

+ Avoid direct use of the R libraries and tools that are installed for the use of the SQL Server instance, except in limited cases such as troubleshooting or installation of new packages. 
+ If you need to use an R command-line tool, you can install [Microsoft R Client](https://docs.microsoft.com/r-server/r-client/what-is-microsoft-r-client).
+ SQL Server provides in-database management of R packages. This is the easiest way to create R package libraries that can be shared among users. For more information, see  [Installing and Managing R Packages](r/installing-and-managing-r-packages.md).

### Avoid clearing the workspace while you're running R in a SQL compute context

Although clearing the workspace is common when you work in the R console, it can have unintended consequences in a SQL compute context.

`revoScriptConnection` is an object in the R workspace that contains information about an R session that's called from SQL Server. However, if your R code includes a command to clear the workspace (such as `rm(list=ls())`), all information about the session and other objects in the R workspace is cleared as well.

As a workaround, avoid indiscriminate clearing of variables and other objects while you're running R in SQL Server. You can delete specific variables by using the **remove** function:

```R
remove('name1', 'name2', ...)
```

If there are multiple variables to delete, we suggest that you save the names of temporary variables to a list and then perform periodic garbage collections on the list.

### Implied authentication for remote execution via ODBC

If you connect to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] computer to run R commands by using the **RevoScaleR** functions, you might get an error when you use ODBC calls that write data to the server. This error happens only when you're using Windows authentication.

The reason is that the worker accounts that are created for R Services do not have permission to connect to the server. Therefore, ODBC calls cannot be executed on your behalf. The problem does not occur with SQL logins because, with SQL logins, the credentials are passed explicitly from the R client to the SQL Server instance and then to ODBC. However, using SQL logins is also less secure than using Windows authentication.

To enable your Windows credentials to be passed securely from a script that's initiated remotely, SQL Server must emulate your credentials. This process is termed _implied authentication_. To make this work, the worker accounts that run R or Python scripts on the SQL Server computer must have the correct permissions.

1. Open [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] as an administrator on the instance where you want to run R code.

2. Run the following script. Be sure to edit the user group name, if you changed the default, and the computer and instance names.

    ```SQL
    USE [master]
    GO
    
    CREATE LOGIN [computername\\SQLRUserGroup] FROM WINDOWS WITH
    DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[language]
    GO
    ```

### The R script works outside of T-SQL but not in a stored procedure

Before wrapping your R code in a stored procedure, it is a good idea to run your R code in an external IDE, or in one of the R tools such as RTerm or RGui. By using these methods, you can test and debug the code by using the detailed error messages that are returned by R.

However, sometimes code that works perfectly in an external IDE or utility might fail to run in a stored procedure or in a SQL Server compute context. If this happens, there are a variety of issues to look for before you can assume that the package doesn't work in SQL Server.

1. Check to see whether Launchpad is running.

2. Review messages to see whether either the input data or output data contains columns with incompatible or unsupported data types. For example, queries on a SQL database often return GUIDs or RowGUIDs, both of which are unsupported. For more information, see [R libraries and data types](r/r-libraries-and-data-types.md).

3. Review the help pages for individual R functions to determine whether all parameters are supported for the SQL Server compute context. For ScaleR help, use the inline R help commands, or see [Package Reference](https://docs.microsoft.com/r-server/r-reference/revoscaler/revoscaler).

### You get different results from the same script when running in SQL compared to other environments

R scripts can return different values in a SQL Server context, for several reasons:

- Implicit type conversion is automatically performed on some data types, when the data is passed between SQL Server and R. For more information, see [R libraries and data types](r/r-libraries-and-data-types.md).

- Determine whether bitness is a factor. For example, there are often differences in the results of math operations for 32-bit and 64-bit floating point libraries.

- Determine whether NaNs were produced in any operation. This can invalidate results.

- Small differences can be amplified when you take a reciprocal of a number near zero.

- Accumulated rounding errors can cause such things as values that are less than zero instead of zero.

### Error: *Not enough quota to process this command*

This error can mean one of several things:

- Launchpad might have insufficient external users to run the external query. For example, if you are running more than 20 external queries concurrently, and there are only 20 default users, one or more queries might fail.

- Insufficient memory is available to process the R task. This error happens most often in a default environment, where SQL Server might be using up to 70 percent of the computer’s resources. For information about how to modify the server configuration to support greater use of resources by R, see [Operationalizing your R code](r/operationalizing-your-r-code.md).

### Error: *Can't find package*

If you run R code in SQL Server and get this message, but did not get the message when you ran the same code outside SQL Server, it means that the package was not installed to the default library location used by SQL Server.

This error can happen in many ways:

- You installed a new package on the server, but access was denied, so R installed the package to a user library.

- You installed R Services and then installed another R tool or set of libraries, including Microsoft R Server (Standalone), Microsoft R Client, RStudio, and so forth.

To determine the location of the R package library that's used by the instance, open SQL Server Management Studio (or any other database query tool), connect to the instance, and then run the following stored procedure:

```SQL
EXEC sp_execute_external_script @language = N'R',  
@script = N' print(normalizePath(R.home())); print(.libPaths());'; 
```

#### Sample results

*STDOUT message(s) from external script:*

*[1] "C:\\Program Files\\Microsoft SQL Server\\MSSQL13.SQL2016\\R_SERVICES"*

*[1] "C:/Program Files/Microsoft SQL Server/MSSQL13.SQL2016/R_SERVICES/library"*

To resolve the issue, you must reinstall the package to the SQL Server instance library.

>[!NOTE]
>If you have upgraded an instance of SQL Server 2016 to use the latest version of Microsoft R, the default library location is different. For more information, see [Use SqlBindR to upgrade an instance of R Services](r/use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

## Next steps

[Machine Learning troubleshooting and known issues](machine-learning-troubleshooting-faq.md)

[Data collection for troubleshooting Machine Learning](data-collection-ml-troubleshooting-process.md)

[Upgrade and installation FAQ](r/upgrade-and-installation-faq-sql-server-r-services.md)

[Troubleshoot database engine connections](../database-engine/configure-windows/troubleshoot-connecting-to-the-sql-server-database-engine.md)
