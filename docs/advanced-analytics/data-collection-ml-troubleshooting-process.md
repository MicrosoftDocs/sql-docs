---
title: Troubleshoot data collection for machine learning - SQL Server Machine Learning Services
ms.prod: sql
ms.technology: machine-learning

ms.date: 02/28/2019
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Troubleshoot data collection for machine learning

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes data collection methods you should use when attempting to resolve problems on your own or with the help of Microsoft customer support.

**Applies to:** SQL Server 2016 R Services, SQL Server 2017 Machine Learning Services (R and Python)

## SQL Server version and edition

SQL Server 2016 R Services is the first release of SQL Server to include integrated R support. SQL Server 2016 Service Pack 1 (SP1) includes several major improvements, including the ability to run external scripts. If you are a SQL Server 2016 customer, you should consider installing SP1 or later.

SQL Server 2017 added Python language integration. You cannot get Python feature integration in earlier releases.

For assistance getting edition and versions, see this article, which lists the build numbers for each of the
[SQL Server versions](https://social.technet.microsoft.com/wiki/contents/articles/783.sql-server-versions.aspx#Service_Pack_editions).

Depending on the edition of SQL Server you're using, some machine learning functionality might be unavailable, or limited. The following articles list of machine learning features in Enterprise, Developer, Standard, and Express editions.

* [Editions and supported features of SQL Server](https://docs.microsoft.com/sql/sql-server/editions-and-components-of-sql-server-2016)
* [R and Python features by editions of SQL Server](r/differences-in-r-features-between-editions-of-sql-server.md)

## R language and tool versions

In general, the version of Microsoft R that is installed when you select the R Services feature or the Machine Learning Services feature is determined by the SQL Server build number. If you upgrade or patch SQL Server, you must also upgrade or patch its R components.

For a list of releases and links to R component downloads, see [Install machine learning components without internet access](https://docs.microsoft.com/sql/advanced-analytics/r/installing-ml-components-without-internet-access). On computers with internet access, the required version of R is identified and installed automatically.

It's possible to upgrade the R Server components separately from the SQL Server database engine, in a process known as binding. Therefore, the version of R that you use when you run R code in SQL Server might differ depending on both the installed version of SQL Server and whether you have migrated the server to the latest R version.

### Determine the R version

The easiest way to determine the R version is to get the runtime properties by running a statement such as the following:

```sql
exec sp_execute_external_script
       @language = N'R'
       , @script = N'
# Transform R version properties to data.frame
OutputDataSet <- data.frame(
  property_name = c("R.version", "Revo.version"),
  property_value = c(R.Version()$version.string, Revo.version$version.string),
  stringsAsFactors = FALSE)
# Retrieve properties like R.home, libPath & default packages
OutputDataSet <- rbind(OutputDataSet, data.frame(
  property_name = c("R.home", "libPaths", "defaultPackages"),
  property_value = c(R.home(), .libPaths(), paste(getOption("defaultPackages"), collapse=", ")),
  stringsAsFactors = FALSE)
)
'
WITH RESULT SETS ((PropertyName nvarchar(100), PropertyValue nvarchar(4000)));

```

> [!TIP]
> If R Services is not working, try running only the R script portion from RGui.

As a last resort, you can open files on the server to determine the installed version. To do so, locate the rlauncher.config file to get the location of the R runtime and the current working directory. We recommend that you make and open a copy of the file so that you don't accidentally change any properties.

* SQL Server 2016
  
  `C:\Program Files\Microsoft SQL Server\MSSQL13.<instance_name\MSSQL\Binn\rlauncher.config`

* SQL Server 2017
  
  `C:\Program Files\Microsoft SQL Server\MSSQL14.<instance_name>\MSSQL\Binn\rlauncher.config`

To get the R version and RevoScaleR versions, open an R command prompt, or open the RGui that's associated with the instance.

* SQL Server 2016
  
  `C:\Program Files\Microsoft SQL Server\MSSQL13.<instancename>\R_SERVICES\bin\x64\RGui.exe`

* SQL Server 2017
  
  `C:\Program Files\Microsoft SQL Server\MSSQL14.<instance_name>\R_SERVICES\bin\x64\RGui.exe`

The R console displays the version information on startup. For example, the following version represents the default configuration for SQL Server 2017:

    *Microsoft R Open 3.3.3*

    *The enhanced R distribution from Microsoft*

    *Microsoft packages Copyright (C) 2017 Microsoft*

    *Loading Microsoft R Server packages, version 9.1.0.*

## Python versions

There are several ways to get the Python version. The easiest way is to run this statement from Management Studio or any other SQL query tool:

```sql
-- Get Python runtime properties:
exec sp_execute_external_script
       @language = N'Python'
       , @script = N'
import sys
import pkg_resources
OutputDataSet = pandas.DataFrame(
                    {"property_name": ["Python.home", "Python.version", "Revo.version", "libpaths"],
                    "property_value": [sys.executable[:-10], sys.version, pkg_resources.get_distribution("revoscalepy").version, str(sys.path)]}
)
'
with WITH RESULT SETS (SQL keywords) ((PropertyName nvarchar(100), PropertyValue nvarchar(4000)));
```

If Machine Learning Services is not running, you can determine the installed Python version by looking at the pythonlauncher.config file. We recommend that you make and open a copy of the file so that you don't accidentally change any properties.

1. For SQL Server 2017 only: `C:\Program Files\Microsoft SQL Server\MSSQL14.<instance_name>\MSSQL\Log\ExtensibilityLog\pythonlauncher.config`
2. Get the value for **PYTHONHOME**.
3. Get the value of the current working directory.

> [!NOTE]
> If you have installed both Python and R in SQL Server 2017, the working directory and the pool of worker accounts are shared for the R and Python languages.

## Are multiple instances of R or Python installed?

Check to see whether more than one copy of the R libraries is installed on the computer. This duplication can happen if:

* During setup you select both R Services (In-Database) and R Server (Standalone).
* You install Microsoft R Client in addition to SQL Server.
* A different set of R libraries was installed by using R Tools for Visual Studio, R Studio, Microsoft R Client, or another R IDE.
* The computer hosts multiple instances of SQL Server, and more than one instance uses machine learning.

The same conditions apply to Python.

If you find that multiple libraries or runtimes are installed, make sure that you get only the errors associated with the Python or R runtimes that are used by the SQL Server instance.

## Origin of errors

The errors that you see when you attempt to run R code can come from any of the following sources:

* SQL Server database engine, including the stored procedure sp_execute_external_script
* The SQL Server Trusted Launchpad
* Other components of the extensibility framework, including R and Python launchers and satellite processes
* Providers, such as Microsoft Open Database Connectivity (ODBC)
* R language

When you work with the service for the first time, it can be difficult to tell which messages originate from which services. We recommend that you capture not only the exact message text, but the context in which you saw the message. Note the client software that you're using to run machine learning code:

* Are you using Management Studio? An external application?
* Are you running R code in a remote client, or directly in a stored procedure?

## SQL Server log files

Get the most recent SQL Server ERRORLOG. The complete set of error logs consists of the files from the following default log directory:

* SQL Server 2016
  
  `C:\Program Files\Microsoft SQL Server\MSSQL13.SQL2016\MSSQL\Log\ExtensibilityLog`

* SQL Server 2017
  
  `C:\Program Files\Microsoft SQL Server\MSSQL14.SQL2016\MSSQL\Log\ExtensibilityLog`

> [!NOTE]
> The exact folder name differs depending on the instance name.

## Errors returned by sp_execute_external_script

Get the complete text of errors that are returned, if any, when you run the sp_execute_external_script command.

To remove R or Python problems from consideration, you can run this script, which starts the R or Python runtime and passes data back and forth.

**For R**

```sql
exec sp_execute_external_script @language =N'R',  
@script=N'OutputDataSet<-InputDataSet',  
@input_data_1 =N'select 1 as hello'  
with result sets (([hello] int not null));  
go
```

**For Python**

```sql
exec sp_execute_external_script @language =N'Python',  
@script=N'OutputDataSet= InputDataSet',  
@input_data_1 =N'select 1 as hello'  
with result sets (([hello] int not null));  
go
```

## Errors generated by the extensibility framework

SQL Server generates separate logs for the external script language runtimes. These errors are not generated by the Python or R language. They're generated from the extensibility components in SQL Server, including language-specific launchers and their satellite processes.

You can get these logs from the following default locations:

* SQL Server 2016
  
  `C:\Program Files\Microsoft SQL Server\MSSQL13.<instance_name>\MSSQL\Log\ExtensibilityLog`

* SQL Server 2017
  
  `C:\Program Files\Microsoft SQL Server\MSSQL14.<instance_name>\MSSQL\Log\ExtensibilityLog`

> [!NOTE]
> The exact folder name differs based on the instance name. Depending on your configuration, the folder might be on a different drive.

For example, the following log messages are related to the extensibility framework:

* *LogonUser Failed for user MSSQLSERVER01*
  
  This might indicate that the worker accounts that run external scripts cannot access the instance.

* *InitializePhysicalUsersPool Failed*
  
  This message might mean that your security settings are preventing setup from creating the pool of worker accounts that are needed to run external scripts.

* *Security Context Manager initialization failed*

* *Satellite Session Manager initialization failed*

## System events

1. Open Windows Event Viewer, and search the **System Event** log for messages that include the string *Launchpad*.
2. Open the ExtLaunchErrorlog file, and look for the string *ErrorCode*. Review the message that's associated with the ErrorCode.

For example, the following messages are common system errors that are related to the SQL Server extensibility framework:

* *The SQL Server Launchpad (MSSQLSERVER) service failed to start due to the following error:  <text>*

* *The service did not respond to the start or control request in a timely fashion.*

* *A timeout was reached (120000 milliseconds) while waiting for the SQL Server Launchpad (MSSQLSERVER) service to connect.*

## Dump files

If you are knowledgeable about debugging, you can use the dump files to analyze a failure in Launchpad.

1. Locate the folder that contains the setup bootstrap logs for SQL Server. For example, in SQL Server 2016, the default path was C:\Program Files\Microsoft SQL Server\130\Setup Bootstrap\Log.
2. Open the bootstrap log subfolder that is specific to extensibility.
3. If you need to submit a support request, add the entire contents of this folder to a zipped file. For example, C:\Program Files\Microsoft SQL Server\130\Setup Bootstrap\Log\LOG\ExtensibilityLog.
  
The exact location might differ on your system, and it might be on a drive other than your C drive. Be sure to get the logs for the instance where machine learning is installed.

## Configuration settings

This section lists additional components or providers that can be a source of errors when you run R or Python scripts.

### What network protocols are available?

Machine Learning Services requires the following network protocols for internal communication among extensibility components, and for communication with external R or Python clients.

* Named pipes
* TCP/IP

Open SQL Server Configuration Manager to determine whether a protocol is installed and, if it is installed, to determine whether it is enabled.

### Security configuration and permissions

For worker accounts:

1. In Control Panel, open **Users and Groups**, and locate the group used to run external script jobs. By default, the group is **SQLRUserGroup**.
2. Verify that the group exists and that it contains at least one worker account.
3. In SQL Server Management Studio, select the instance where R or Python jobs will be run, select **Security**, and then determine whether there is a logon for SQLRUserGroup.
4. Review permissions for the user group.

For individual user accounts:

1. Determine whether the instance supports Mixed Mode authentication, SQL logins only, or Windows authentication only. This setting affects your R or Python code requirements.
2. For each user who needs to run R code, determine the required level of permissions on each database where objects will be written from R, data will be accessed, or objects will be created.
3. To enable script execution, create roles or add users to the following roles, as necessary:

   - All but *db_owner*: Require EXECUTE ANY EXTERNAL SCRIPT.
   - *db_datawriter*: To write results from R or Python.
   - *db_ddladmin*: To create new objects.
   - *db_datareader*: To read data that's used by R or Python code.
4. Note whether you changed any default startup accounts when you installed SQL Server 2016.
5. If a user needs to install new R packages or use R packages that were installed by other users, you might need to enable package management on the instance and then assign additional permissions. For more information, see [Enable or disable R package management](r/r-package-how-to-enable-or-disable.md).

### What folders are subject to locking by antivirus software?

Antivirus software can lock folders, which prevents both the setup of the machine learning features and successful script execution. Determine whether any folders in the SQL Server tree are subject to virus scanning.

However, when multiple services or features are installed on an instance, it can be difficult to enumerate all possible folders that are used by the instance. For example, when new features are added, the new folders must be identified and excluded.

Moreover, some features create new folders dynamically at runtime. For example, in-memory OLTP tables, stored procedures, and functions all create new directories at runtime. These folder names often contain GUIDs and cannot be predicted. The SQL Server Trusted Launchpad creates new working directories for R and Python script jobs.

Because it might not be possible to exclude all folders that are needed by the SQL Server process and its features, we recommend that you exclude the entire SQL Server instance directory tree.

### Is the firewall open for SQL Server? Does the instance support remote connections?

1. To determine whether SQL Server supports remote connections, see [Configure remote server connections](../database-engine/configure-windows/view-or-configure-remote-server-connection-options-sql-server.md).

2. Determine whether a firewall rule has been created for SQL Server. For security reasons, in a default installation, it might not be possible for remote R or Python client to connect to the instance. For more information, see [Troubleshooting connecting to SQL Server](../database-engine/configure-windows/troubleshoot-connecting-to-the-sql-server-database-engine.md).

## See also

[Troubleshoot machine learning in SQL Server](machine-learning-troubleshooting-faq.md)
