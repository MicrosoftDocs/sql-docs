---
title: Troubleshoot Launchpad for Python and R scripts
description: This article provides troubleshooting guidance for many issues that prevent the SQL Server Launchpad service from starting, including configuration problems or changes, or missing network protocols. The Launchpad service supports external script execution for R and Python.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 04/08/2021
ms.topic: troubleshooting
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom: contperf-fy21q3
monikerRange: ">=sql-server-2016||>=sql-server-linux-ver15"
---
# Troubleshoot issues with Launchpad service executing Python and R scripts in SQL Server Machine Learning Services
[!INCLUDE [SQL Server 2016 and later](../../includes/applies-to-version/sqlserver2016.md)]

This article provides troubleshooting guidance for issues involving the [SQL Server Launchpad service](../security/sql-server-launchpad-service-account.md) used with [Machine Learning Services](../sql-server-machine-learning-services.md). The Launchpad service supports external script execution for R and Python. Multiple issues can prevent Launchpad from starting, including configuration problems or changes, or missing network protocols.  

## Determine whether Launchpad is running

1. Open [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md). From the command line, type **SQLServerManager13.msc**, **SQLServerManager14.msc**, or **SQLServerManager15.msc**.

2. Make a note of the service account that Launchpad is running under. Each instance where R or Python is enabled should have its own instance of the Launchpad service. For example, the service for a named instance might be something like _MSSQLLaunchpad$InstanceName_.

3. If the service is stopped, restart it. On restarting, if there are any issues with configuration, a message is published in the system event log, and the service is stopped again. Check the system event log for details about why the service stopped.

4. Review the contents of RSetup.log, and make sure that there are no errors in the setup. For example, the message *Exiting with code 0* indicates failure of the service to start.

5. To look for other errors, review the contents of rlauncher.log.

## Check the Launchpad service account

The default service account might be "NT Service\$SQL2016", "NT Service\$SQL2017", or "NT Service\$SQL2019". The final part might vary, depending on your SQL instance name.

The Launchpad service (Launchpad.exe) runs by using a low-privilege service account. However, to start R and Python and communicate with the database instance, the Launchpad service account requires the following user rights:

- Log on as a service (SeServiceLogonRight)
- Replace a process-level token (SeAssignPrimaryTokenPrivilege)
- Bypass traverse checking (SeChangeNotifyPrivilege)
- Adjust memory quotas for a process (SeIncreaseQuotaSizePrivilege)

For information about these user rights, see the "Windows privileges and rights" section in [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

> [!TIP]
> If you are familiar with the use of the Support Diagnostics Platform (SDP) tool for SQL Server diagnostics, you can use SDP to review the output file with the name MachineName_UserRights.txt.

## User group for Launchpad cannot log on locally

During setup of Machine Learning Services, SQL Server creates the Windows user group **SQLRUserGroup** and then provisions it with all rights necessary for Launchpad to connect to SQL Server and run external script jobs. If this user group is enabled, it is also used to execute Python scripts.

However, in organizations where more restrictive security policies are enforced, the rights that are required by this group might have been manually removed, or they might be automatically revoked by policy. If the rights have been removed, Launchpad can no longer connect to SQL Server, and SQL Server cannot call the external runtime.

To correct the problem, ensure that the group **SQLRUserGroup** has the system right **Allow log on locally**.

For more information, see [Configure Windows service accounts and permissions](../../database-engine/configure-windows/configure-windows-service-accounts-and-permissions.md).

## Permissions to run external scripts

Even if Launchpad is configured correctly, it returns an error if the user does not have permission to run R or Python scripts.

If you installed SQL Server as a database administrator or you are a database owner, you are automatically granted this permission. However, other users usually have more limited permissions. If they try to run an R script, they get a Launchpad error.

To correct the problem, in SQL Server Management Studio, a security administrator can modify the SQL login or Windows user account by running the following script:

```sql
GRANT EXECUTE ANY EXTERNAL SCRIPT TO <username>
```

For more information, see [GRANT (Transact-SQL](../../t-sql/statements/grant-transact-sql.md).

## Common Launchpad errors

This section lists the most common error messages that Launchpad returns.

::: moniker range=">=sql-server-2016"
## "Unable to launch runtime for R script"

If the Windows group for R users (also used for Python) cannot log on to the instance that is running R Services, you might see the following errors:

- Errors generated when you try to run R scripts:

    * *Unable to launch runtime for 'R' script. Please check the configuration of the 'R' runtime.*

    * *An external script error occurred. Unable to launch the runtime.*

- Errors generated by the
    [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service:

    * *Failed to initialize the launcher RLauncher.dll*

    * *No launcher dlls were registered!*

    * *Security logs indicate that the account NT SERVICE was unable to log on*

For information about how to grant this user group the necessary permissions, see [Install SQL Server R Services](../install/sql-r-services-windows-install.md).

> [!NOTE]
> This limitation does not apply if you use SQL logins to run R scripts from a remote workstation.
::: moniker-end

## "Logon failure: the user has not been granted the requested logon type"

By default, [!INCLUDE[rsql_launchpad_md](../../includes/rsql-launchpad-md.md)] uses the following account on startup: `NT Service\MSSQLLaunchpad`. The account is configured by [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)] setup to have all necessary permissions.

If you assign a different account to Launchpad, or the right is removed by a policy on the SQL Server machine, the account might not have the necessary permissions, and you might see this error:

>*ERROR_LOGON_TYPE_NOT_GRANTED 1385 (0x569) Logon failure: the user has not been granted the requested logon type at this computer.*

To grant the necessary permissions to the new service account, use the Local Security Policy application, and update the permissions on the account to include the following permissions:

+ Adjust memory quotas for a process (SeIncreaseQuotaPrivilege)
+ Bypass traverse checking (SeChangeNotifyPrivilege)
+ Log on as a service (SeServiceLogonRight)
+ Replace a process-level token (SeAssignPrimaryTokenPrivilege)

## "Unable to communicate with the Launchpad service"

If you have installed and then enabled machine learning, but you get this error when you try to run an R or Python script, the Launchpad service for the instance might have stopped running.

1. From a Windows command prompt, open the SQL Server Configuration Manager. For more information, see [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

2. Right-click SQL Server Launchpad for the instance, and then select **Properties**.

3. Select the **Service** tab, and then verify that the service is running. If it is not running, change the **Start Mode** to **Automatic**, and then select **Apply**.

4. Restarting the service usually fixes the problem so that machine learning scripts can run. If the restart does not fix the issue, note the path and the arguments in the **Binary Path** property, and do the following:

    a. Review the launcher's .config file and ensure that the working directory is valid.

    b. Ensure that the Windows group that's used by Launchpad can connect to the SQL Server instance.

    c. If you change any of the service properties, restart the Launchpad service.

## "Fatal error creation of tmpFile failed"

In this scenario, you have successfully installed machine learning features, and Launchpad is running. You try to run some simple R or Python code, but Launchpad fails with an error like the following: 

>*Unable to communicate with the runtime for R script. Please check the requirements of R runtime.*

At the same time, the external script runtime writes the following message as part of the STDERR message: 

>*Fatal error: creation of tmpfile failed.*

This error indicates that the account that Launchpad is attempting to use does not have permission to log on to the database. This situation can happen when strict security policies are implemented. To determine whether this is the case, review the SQL Server logs, and check to see whether the MSSQLSERVER01 account was denied at login. The same information is provided in the logs that are specific to R\_SERVICES or PYTHON\_SERVICES. Look for ExtLaunchError.log.

By default, 20 accounts are set up and associated with the Launchpad.exe process, with the names MSSQLSERVER01 through MSSQLSERVER20. If you make heavy use of R or Python, you can increase the number of accounts.

To resolve the issue, ensure that the group has *Allow Log on Locally* permissions to the local instance where machine learning features have been installed and enabled. In some environments, this permission level might require a GPO exception from the network administrator.

## "Not enough quota to process this command"

This error can mean one of several things:

- Launchpad might have insufficient external users to run the external query. For example, if you are running more than 20 external queries concurrently, and there are only 20 default users, one or more queries might fail.

- Insufficient memory is available to process the R task. This error happens most often in a default environment, where SQL Server might be using up to 70 percent of the computer's resources. For information about how to modify the server configuration to support greater use of resources by R, see [Operationalizing your R code](../tutorials/python-ski-rental-linear-regression-deploy-model.md).

## "Can't find package"

If you run R code in SQL Server and get this message, but did not get the message when you ran the same code outside SQL Server, it means that the package was not installed to the default library location used by SQL Server.

This error can happen in many ways:

- You installed a new package on the server, but access was denied, so R installed the package to a user library.

- You installed R Services and then installed another R tool or set of libraries, such as RStudio.

To determine the location of the R package library that's used by the instance, open SQL Server Management Studio (or any other database query tool), connect to the instance, and then run the following stored procedure:

```sql
EXEC sp_execute_external_script @language = N'R',  
@script = N' print(normalizePath(R.home())); print(.libPaths());'; 
```

#### Sample results

*STDOUT message(s) from external script:*

*[1] "C:\\Program Files\\Microsoft SQL Server\\MSSQL13.SQL2016\\R_SERVICES"*

*[1] "C:/Program Files/Microsoft SQL Server/MSSQL13.SQL2016/R_SERVICES/library"*

To resolve the issue, you must reinstall the package to the SQL Server instance library.

::: moniker range=">=sql-server-2016"
>[!NOTE]
>If you have upgraded an instance of SQL Server 2016 to use the latest version of Microsoft R, the default library location is different. For more information, see [Default R library location](../package-management/r-package-information.md#default-r-library-location).
::: moniker-end

::: moniker range=">=sql-server-2016"
## Launchpad shuts down due to mismatched DLLs

If you install the database engine with other features, patch the server, and then later add the Machine Learning feature by using the original media, the wrong version of the Machine Learning components might be installed. When Launchpad detects a version mismatch, it shuts down and creates a dump file.

To avoid this problem, be sure to install any new features at the same patch level as the server instance.

**The wrong way to upgrade:**

1. Install SQL Server 2016 without R Services.
2. Upgrade SQL Server 2016 Cumulative Update 2.
3. Install R Services (In-Database) by using the RTM media.

**The correct way to upgrade:**

1. Install SQL Server 2016 without R Services.
2. Upgrade SQL Server 2016 to the desired patch level. For example, install Service Pack 1 and then Cumulative Update 2.
3. To add the feature at the correct patch level, run SP1 and CU2 setup again, and then choose R Services (In-Database). 

## Launchpad fails to start if 8dot3 notation is required

> [!NOTE] 
> On older systems, Launchpad can fail to start if there is an 8dot3 notation requirement. This requirement has been removed in later releases. SQL Server 2016 R Services customers should install one of the following:
> * SQL Server 2016 SP1 and CU1: [Cumulative Update 1 for SQL Server](https://support.microsoft.com/help/3208177/cumulative-update-1-for-sql-server-2016-sp1).
> * SQL Server 2016 RTM, Cumulative Update 3, and this [hotfix](https://support.microsoft.com/help/3210110/on-demand-hotfix-update-package-for-sql-server-2016-cu3), which is available on demand.

For compatibility with R, SQL Server 2016 R Services (In-Database) required the drive where the feature is installed to support the creation of short file names by using *8dot3 notation*. An 8.3 file name is also called a *short file name*, and it's used for compatibility with earlier versions of Microsoft Windows or as an alternative to long file names.

If the volume where you are installing R does not support short file names, the processes that launch R from SQL Server might not be able to locate the correct executable, and Launchpad will not start.

As a workaround, you can enable the 8dot3 notation on the volume where SQL Server is installed and where R Services is installed. You must then provide the short name for the working directory in the R Services configuration file.

1. To enable 8dot3 notation, run the fsutil utility with the *8dot3name* argument as described here: [fsutil 8dot3name](/previous-versions/windows/it-pro/windows-server-2012-R2-and-2012/ff621566(v=ws.11)).

2. After the 8dot3 notation is enabled, open the RLauncher.config file and note the property of `WORKING_DIRECTORY`. For information about how to find this file, see [Data collection for Machine Learning troubleshooting](data-collection-ml-troubleshooting-process.md).

3. Use the fsutil utility with the *file* argument to specify a short file path for the folder that's specified in WORKING_DIRECTORY.

4. Edit the configuration file to specify the same working directory that you entered in the WORKING_DIRECTORY property. Alternatively, you can specify a different working directory and choose an existing path that's already compatible with the 8dot3 notation.
::: moniker-end

## Next steps

[Data collection for troubleshooting machine learning](data-collection-ml-troubleshooting-process.md)

[Install SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md)

[Troubleshoot database engine connections](/troubleshoot/sql/connect/network-related-or-instance-specific-error-occurred-while-establishing-connection)