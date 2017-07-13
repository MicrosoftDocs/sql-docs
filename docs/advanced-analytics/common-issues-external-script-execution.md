---
title: "Common Issues with External Script Execution | Microsoft Docs"
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
caps.latest.revision: 1
author: "jeannt"
ms.author: "jeannt"
manager: "jhubbard"
---
# Common Issues with External Script Execution

This section contains a list of known issues and common problems when running R
or Python code in SQL Server.

Before you get started, we recommend that you collect some information about your system: [Data Collection for Troubleshooting](data-collection-ml-troubleshooting-process.md)

Also, review this list of common issues specific to initial setup or configuration: [Setup and Upgrade FAQ](r/upgrade-and-installation-faq-sql-server-r-services.md)

## Launchpad Issues

The **SQL Server Trusted Launchpad** is the service that manages execution of external scripts and communication with R, Python, or other external runtimes. Multiple issues can prevent Launchpad from starting, including configuration problems or changes, or missing network protocols.

As part of the troubleshooting process, begin by asking and answering these questions:

- Did Launchpad always fail to run, or did it stop working?
- What service account is Launchpad running under?
- What user rights does the Launchpad service account have?

### Determine if Launchpad is running

1.  Open the **Services** panel (Services.msc). Or, from the command line, type SQLServerManager13.msc or SQLServerManager14.msc to open [SQL Server Configuration Manager](https://docs.microsoft.comsql/relational-databases/sql-server-configuration-manager).

2.  Make a note of the service account that the Launchpad is running under. Each instance where R or Python is enabled should have its own instance of the Launchpad service. For example, the service for a named instance might be something like _MSSQLLaunchpad$InstanceName_.

3.  If the service is stopped, restart it. On restarting, if there are any issues with configuration, a message should be entered in the system event log before the service stops again.

4. Review the contents of RSetup.log and make sure there are no errors in the setup: for example, the message "Exiting with code 0" indicates failure of the service to start.

5. Review the contents of rlauncher.log to look for other errors.

### Check the Launchpad service account

The default service account is something like this: "NT Service\$SQL2016" where the final part varies, based on your SQL instance name.

The Launchpad service (Launchpad.exe) runs using a low-privilege service account. However to start R and Python and communicate with the database instance, the Launchpad service account requires the following user rights:

-   Log on as a Service (SeServiceLogonRight)

-   Replace Process Level Token (SeAssignPrimaryTokenPrivilege) and Bypass Traverse Checking (SeChangeNotifyPrivilege)

-   Adjust Memory Quotas for a process (SeIncreaseQuotaSizePrivilege)

For more information about these user rights, see the section, "List of user rights and file
level permissions", in this article: [Configure Windows Service Accounts and Permissions](https://msdn.microsoft.com/library/ms143504.aspx)

> [!TIP]
> If you are an advanced user who is already familiar with use of the SDP tool for SQL Server diagnostics, you can check the current permissions on the account by using this tool. Review the output file with the name MachineName_UserRights.txt file.

### Review Launchpad requirements

There are several issues that can prevent Launchpad from starting. The Launchpad service might start and then stop or crash, or the service might stop with a connection timeout. In these cases, most often the system has been changed or configured such that Launchpad cannot run.

#### Determine if 8dot3 notation is enabled

For compatibility with R, SQL Server 2016 R Services (In-Database) required that the drive where the feature is installed support creation of short file names using the **8dot3** notation. An 8.3 filename is also called a short filename and is used for compatibility with older versions of Microsoft Windows prior or as an alternate filename to the long filename.

If the volume where you are installing R does not support the short filenames, the processes that launch R from SQL Server might not be able to locate the correct executable and the Launchpad will not start.

As a workaround, you should enable the 8dot3 notation on the volume where SQL Server is installed and R Services is installed. You must then provide the short name for the working directory in the R Services configuration file.

1.  To enable 8dot3 notation, run the **fsutil** utility with the *8dot3name* argument as described here: [fsutil
    8dot3name](https://technet.microsoft.com/library/ff621566(v=ws.11).aspx).

2.  After the 8dot3 notation is enabled, open the file RLauncher.config and make a note of the property for `WORKING_DIRECTORY`. For information on how to find this file, see [Data Collection for Machine Learning Troubleshooting](data-collection-ml-troubleshooting-process.md).

4.  Use the fsutil utility with the *file* argument to specify a short file path for the folder specified in WORKING_DIRECTORY.

5.  Edit the configuration file to use the short name for `WORKING_DIRECTORY`. Alternatively, you can specify a different directory for `WORKING_DIRECTORY` that has a path compatible with the 8dot3 notation.

>   [!NOTE] 
> This restriction has been removed in later releases. If you experience this issue, install one of the following:
> 
>   SQL Server 2016 SP1 and CU1: [Cumulative Update 1 for SQL
>   Server](https://support.microsoft.com/help/3208177/cumulative-update-1-for-sql-server-2016-sp1)
> 
>   SQL Server 2016 RTM, Service Pack 3, and this
>   [hotfix](https://support.microsoft.com/help/3210110/on-demand-hotfix-update-package-for-sql-server-2016-cu3),
>   available on demand

#### User group for Launchpad cannot log in locally

During setup of R Services, SQL Server creates the Windows user group, **SQLRUserGroup**, and provisions it with all rights necessary for Launchpad to connect to SQL Server and run external script jobs.

However, in organizations where more restrictive security policies are enforced, the rights required by this group might have been manually removed, or might be automatically revoked by policy. If this happens, Launchpad can no longer connect to SQL Server, and R Services will be unable to function.

To correct the problem, ensure that the group **SQLRUserGroup** has the system right **Allow log on locally**.

For more information, see [Configure Windows Service Accounts and Permissions](https://msdn.microsoft.com/library/ms143504.aspx#Windows).

#### Improper setup leading to mismatched DLLs

If you install the database engine with other features, patch the server, and then later add the R Services feature using the original media, the wrong version of the R components might be installed. When Launchpad detects a version mismatch, it will shut down and create a dump file.

To avoid this problem, be sure to install any new features at the same patch level as the server instance.

**Wrong way to upgrade**

1. Install SQL Server 2016 without R Services
2. Upgrade SQL Server 2016 Cumulative Update 2
3. Install R Services (in-database) using the RTM media

**Correct way to upgrade**

1. Install SQL Server 2016 without R Services
2. Upgrade SQL Server 2016 to the desired patch level, for example, install SP1 and then Cumulative Update 2
3. Run SP1 and CU2 setup again and choose R Services (In-Database) to add the feature at the correct patch level

#### Check whether user has rights to run external scripts

Even if Launchpad is configured correctly, if a user does not have permission to run R or Python scripts, the Launchpad will return an error.

If you installed SQL Server as a database administrator or are a database owner, you are automatically granted this permission. However, other users typically have more limited permissions; therefore, if they try to run R script, they would get a Launchpad error.

To correct the problem, in SQL Server Management Studio, a security administrator can modify the SQL login or Windows user account, by running:

```SQL
GRANT EXECUTE ANY EXTERNAL SCRIPT
```

### Common Launchpad errors

This section lists the most common error messages returned by Launchpad.

#### Error: Unable to launch runtime for R script

If the Windows group for R users (which is also used for Python) does not have the ability to log into the instance that is running R Services, you might see the following errors:

- When trying to run R scripts:

- *Unable to launch runtime for 'R' script. Please check the configuration of
    the 'R' runtime.*

    *An external script error occurred. Unable to launch the runtime.*

- Errors generated by the
    [!INCLUDE[rsql_launchpad](../includes/rsql-launchpad-md.md)] service:

-   *Failed to initialize the launcher RLauncher.dll*

    *No launcher dlls were registered!*

-   Security logs indicate that the account NT SERVICE was unable to log in.

For information about how to give this user group the necessary permissions, see [Set up SQL Server R
Services](r/set-up-sql-server-r-services-in-database.md).

> [!NOTE]
> This limitation does not apply if you use SQL logins to run R scripts from a remote workstation.

#### Error: *Logon failure: the user has not been granted the requested logon type*

By default, [!INCLUDE[rsql_launchpad_md](../includes/rsql-launchpad-md.md)] uses the following account on startup, which is configured by [!INCLUDE[ssNoVersion_md](../includes/ssnoversion-md.md)] setup to have all necessary permissions: `NT Service\MSSQLLaunchpad`

If you assign a different account to the Launchpad or the right is removed by a policy on the SQL Server machine, the account might not have necessary permissions, and you might see this error:

*ERROR_LOGON_TYPE_NOT_GRANTED 1385 (0x569) Logon failure: the user has not been
granted the requested logon type at this computer.*

To give the necessary permissions to the new service account, use the **Local Security Policy** application and update the permissions on the account to include these permissions:

+ Adjust memory quotas for a process (SeIncreaseQuotaPrivilege)
+ Bypass traverse checking (SeChangeNotifyPrivilege)
+ Log on as a service (SeServiceLogonRight)
+ Replace a process-level token (SeAssignPrimaryTokenPrivilege)

#### Error: Unable to communicate with the Launchpad service

If you install R Services and enable the feature, but get this error when you try to run an R script, it might be that the Launchpad service for the instance has stopped running.

1.  From a Windows command prompt, open the SQL Server Configuration Manager. For more information, see [SQL Server Configuration Manager](../relational-databases/sql-server-configuration-manager.md).

2.  Right-click SQL Server Launchpad for the instance where R Services is not working, and select **Properties**.

3.  Click the **Service** tab and verify that the service is running. If it is not, change the **Start Mode** to **Automatic** and click **Apply**.

4.  Typically, restarting the service fixes the problem and R scripts can run. If it does not, make a note of the path and arguments in the **Binary Path** property.

    - Review the rlauncher.config file and ensure that the working directory is valid.

    - Ensure that the Windows group used by Launchpad has the ability to connect to the SQL Server instance, as described in the [previous section](#bkmk_LaunchpadTS).

    - Restart the Launchpad service if you make any changes to service properties.

#### Error: Fatal error creation of tmpFile failed

In this scenario, you have successfully installed SQL Server 2016 with R Services (In-Database), and Launchpad is running. You try to run some simple R or Python code, but Launchpad fails with this error: “Unable to communicate with the runtime for R script. Please check the requirements of R runtime.”

At the same time, the R runtime outputs the following message as part of the STDERR message: “Fatal error: creation of tmpfile failed”.

This error indicates that the account the R script is attempting to use does not have permission to log into the database. This can happen when strict security policies are implemented. To determine if this is the case, review the SQL Server logs, and check whether the account MSSQLSERVER01 was denied at login. (The same information is provided in the logs specific to R Services, ExtLaunchError.log).

By default, 20 accounts are provisioned and associated with the Launchpad.exe process, with the names MSSQLSERVER01 – MSSQLSERVER20. The number of accounts might have been increased, if you make heavy use of R or Python.

To resolve the issue, ensure that the group has the permission “Allow Log on Locally” rights to the local instance where R Services is used. In some environments, this might require a GPO exception from the network administrator.

## R Script Issues

This section contains some common issues that are specific to R script execution and R script errors. The list is not intended to be comprehensive, as there are many R packages and errors may differ between versions of the same R package. We recommend that you post R script errors on the [Microsoft R Server forum](https://social.msdn.microsoft.com/Forums/home?forum=MicrosoftR), which supports all related products: R Services (In-Database), Machine Learning Services with Python, Microsoft R Client, and Microsoft R Server.

### Multiple R instances on the same computer

You can easily get multiple installation of R on the same computer, or multiple copies of the same R package, in different versions.

If you install both R Server (standalone) and R Services (In-Database), be aware that these install separate versions of the R libraries, and it is possible to have different versions of R for each. This can become very confusing when you are running R script and see errors in one environment but not in the other.

+ Avoid direct use of the R libraries and tools that are installed for the use of the SQL Server instance, except in limited cases such as troubleshooting. If you need to use an R command line tool, you can install [Microsoft R Client](https://msdn.microsoft.com/microsoft-r/r-client).
+ SQL Server provides in-database management of R packages. This is the easiest way to create R package libraries that can be shared among users. For more information, see  [Installing and Managing R Packages](r/installing-and-managing-r-packages.md).

### Avoid clearing the workspace while running R in SQL compute context

Although clearing the workspace is common when working in the R console, it can have unintended consequences in a SQL compute context.

`revoScriptConnection` is an object in the R workspace that contains information about an R session that is called from SQL Server. However, if your R code includes a command to clear the workspace (such as `rm(list=ls())`), all information about the session and other objects in the R workspace is cleared as well.

As a workaround, avoid indiscriminate clearing of variables and other objects while running R in SQL Server. You can delete specific variables by using the **remove** function:

```R
remove('name1', 'name2', ...)
```

If there are multiple variables to delete, we suggest saving the names of temporary variables to a list and then performing periodic garbage collection on the list.

### Implied authentication for remote execution via ODBC

If you connect to the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] computer to run R commands using the **RevoScaleR** functions, you might get an error when using ODBC calls that write data to the server. This error happens only when using Windows authentication.

The reason is that the worker accounts created for R Services do not have permission to connect to the server; therefore, ODBC calls cannot be executed on your behalf. The problem does not occur with SQL logins because, with SQL logins, the credentials are explicitly passed from the R client to the SQL Server instance and then to ODBC. However, using SQL logins is also less secure than using Windows authentication.

To enable your Windows credentials to be passed securely from a script initiated remotely, SQL Server must emulate your credentials, in a process termed _implied authentication_. To make this work, the worker accounts that run R or Python scripts on the SQL Server computer must have the correct permissions.

1.  Open [!INCLUDE[ssManStudioFull](../includes/ssmanstudiofull-md.md)] as an administrator on the instance where you will run R code.

2.  Run the following script. Be sure to edit the user group name, if you changed the default, and the computer and instance name.

    ```SQL
    USE [master]
    GO
    
    CREATE LOGIN [computername\\SQLRUserGroup] FROM WINDOWS WITH
    DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[language]
    GO
    ```

### R script works outside of T-SQL but not in a stored procedure

Before wrapping your R code in a stored procedure, it is a good idea to run your R code in an external IDE, or in one of the R tools such as RTerm or RGui, so that you can test and debug the code using the detailed error messages returned by R.

However, sometimes code that works perfectly in an external IDE or utility will fail to run in a stored procedure or in SQL Server compute context. If this happens, there are a variety of issues to look for before assuming the package doesn't work in SQL Server.

1. Check whether Launchpad is running.

2. Review messages to see if either the input data or output data contains columns with incompatible or unsupported data types. For example, queries on a SQl database will often return GUIDs or rowguids, both of which are unsupported. For more information, see [R Libraries and Data Types](r/r-libraries-and-data-types.md).

3. Review the help pages for individual R functions to determine if all parameters are supported for the SQL Server compute context. For ScaleR help, use the inline R help commands, or see [Package Reference](https://msdn.microsoft.com/microsoft-r/package-reference).

### Different results running in SQL and in other environments

This can happen for different reasons:

-   Some type conversion might be implicitly performed when passing data between SQL Server and R. For more information, see [R Libraries and Data Types](r/r-libraries-and-data-types.md)

-   Determine whether bitness is a factor. For example, there are often differences in the results of math operations 32-bit and 64-bit floating point libraries.

-   Determine whether NaNs were produced in any operation. This can invalidate results.

-   Small differences can be amplified when you take a reciprocal of a number near zero.

-   Accumulated rounding errors can cause things like a value which is less than zero instead of zero.

### Error: Not enough quota to process this command

This error can mean one of several things:

- The Launchpad might have insufficient external users to run the external query. For example, if you are running more than 20 external queries concurrently, and there are only 20 default users, one or more queries might fail.

- Insufficient memory is available to process the R task. This happens most often in a default environment, where SQL Server might be allocated up to 70% of the computer’s resources. For information about how to modify the server configuration to support greater use of resources by R, see [Operationalizing Your R Code](r/operationalizing-your-r-code.md).

### Error: Can't find package

If you run R code in SQL Server and get this message, but did not get the message when you ran the same code outside SQL Server, it means that the package was not installed to the default library location used by SQL Server.

This can happen in many ways:

- You installed a new package on the server, but access was denied, so R installed the package to a user library

- You installed R Services, and then installed another R tool or set of libraries, including Microsoft R Server (Standalone), Microsoft R Client, RStudio, and so forth.

To determine the location of the R package library used by the instance, open SQL Server Management Studio (or any other database query tool), connect to the instance in question, and run the following stored procedure:

```SQL
EXEC sp_execute_external_script @language = N'R',  
@script = N' print(normalizePath(R.home())); print(.libPaths());'; 
```

**Sample Results**

*STDOUT message(s) from external script:*

*[1] "C:\\Program Files\\Microsoft SQL Server\\MSSQL13.SQL2016\\R_SERVICES"*

*[1] "C:/Program Files/Microsoft SQL Server/MSSQL13.SQL2016/R_SERVICES/library"*

To resolve the issue, you must reinstall the package to the SQL Server instance library.

Note that, if you have upgraded an instance of SQL Server 2016 to use the latest version of Microsoft R, the default library location will be different. For more information, see [Use SqlBindR to Upgrade an Instance of R Services](r/use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server.md).

## See Also

[Machine Learning Troubleshooting and Known Issues](machine-learning-troubleshooting-faq.md)
[Data Collection for Troubleshooting Machine Learning](data-collection-ml-troubleshooting-process.md)
[Upgrade and Installation FAQ](r/upgrade-and-installation-faq-sql-server-r-services.md)
[Troubleshoot Database Engine Connections](../database-engine/configure-windows/troubleshoot-connecting-to-the-sql-server-database-engine.md)
