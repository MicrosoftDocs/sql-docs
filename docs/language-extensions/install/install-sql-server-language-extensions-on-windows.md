---
title: Install SQL Server Language Extensions on Windows
titleSuffix: SQL Server Language Extensions
description: Language extensions installation steps for SQL Server 2019 in Windows.
author: dphansen
ms.author: davidph 
manager: cgronlun
ms.date: 05/22/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: language-extensions
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---
# Install SQL Server Machine Learning Services on Windows

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

Starting in SQL Server 2019, Language Extensions and Java support is provided. This article explains how to install the Language Extensions component by running the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup wizard.

> [!NOTE]
> This article is for installation of SQL Server Language Extensions on Windows. For Linux, see [Install SQL Server 2019 Language Extensions (Java) on Linux](https://docs.microsoft.com/sql//linux/sql-server-linux-setup-language-extensions.md)

<a name="prerequisites"></a> 

## Pre-install checklist

+ SQL Server 2019 Setup is required if you want to install support for Language Extensions.

+ A database engine instance is required. You cannot install just the Language Extensions features, although you can add them incrementally to an existing instance.

+ For business continuity, [Always On Availability Groups](https://docs.microsoft.com/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server) are supported for Language Extensions. You have to install language extensions, and configure packages, on each node.

+ Installing Language Extensions is supported on a failover cluster in SQL Server 2019.

+ Do not install SQL Server Language Extensions on a domain controller. The Language Extensions portion of setup will fail.

> [!IMPORTANT]
> After setup is complete, be sure to complete the post-configuration steps described in this article. These steps include enabling SQL Server to use external code, and adding accounts required for SQL Server to run Java code on your behalf. Configuration changes generally require a restart of the instance, or a restart of the Launchpad service.

<a name="java-prerequisite"></a>

## Java JRE or JDK prerequisite

Java 8 is currently the supported version. Newer versions, like Java 11, should with the language extension but is currently not supported. The Java Runtime Environment (JRE) is the minimum requirement, but JDK is useful if you need the Java compiler and development packages. Because the JDK is all inclusive, if you install the JDK, the JRE is not necessary.

You can use your preferred Java 8 distribution. Below are two suggested distributions:

| Distribution | Java version | Operating systems | JDK | JRE |
|-|-|-|-|-|
| [Zulu OpenJDK](https://www.azul.com/downloads/zulu/) | 8 | Windows and Linux | Yes | No |
| [Oracle Java SE](https://www.oracle.com/technetwork/java/javase/downloads/index.html) | 8 | Windows and Linux | Yes | Yes |

On Windows, we recommend installing the JDK under the default `/Program Files/` folder if possible. Otherwise, extra configuration is required to grant permissions to executables. For more information, see the [grant permissions (Windows)](#perms-nonwindows) section in this document.

> [!Note]
> Given that Java is backwards compatible, earlier versions might work, but the supported and tested version for this early CTP release is Java 8. 

## Get the installation media

The preview version of SQL Server 2019 is available at the [SQL Server 2019 install site](https://www.microsoft.com/sql-server/sql-server-2019#Install).

<!-- We can use this include statement, once SQL Server 2019 is in GA
[!INCLUDE[GetInstallationMedia](../../includes/getssmedia.md)]
-->
## Run Setup

For local installations, you must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.

1. Ensure a supported version of Java is installed. For more information, see the [Java prerequisites](#java-prerequisite).

3. Start the setup wizard for SQL Server 2019. 
  
4. On the **Installation** tab, select **New SQL Server stand-alone installation or add features to an existing installation**.

4. On the **Feature Selection** page, select these options:
  
    - **Database Engine Services**
  
        To use Language Extensions with SQL Server, you must install an instance of the database engine. You can use either a default or a named instance.
  
    - **Machine Learning Services and Language Extensions**
  
        This option installs the Language Extensions component that support Java code execution.

        You can omit R and Python if you wish.

        ![Feature options for Language Extensions](../media/sql2019-install-language-extensions.png) 

5. On the **Ready to Install** page, verify that these selections are included, and select **Install**.
  
    + Database Engine Services
    + Machine Learning Services (in-database)

    Note of the location of the folder under the path `..\Setup Bootstrap\Log` where the configuration files are stored. When setup is complete, you can review the installed components in the Summary file.

6. After setup is complete, if you are instructed to restart the computer, do so now. It is important to read the message from the Installation Wizard when you have finished with Setup. For more information, see [View and Read SQL Server Setup Log Files](https://docs.microsoft.com/sql/database-engine/install-windows/view-and-read-sql-server-setup-log-files).

## Add the JRE_HOME variable

`JRE_HOME` is a system environment variable that specifies the location of the Java interpreter. In this step, create a system environment variable for it on Windows.

1. Find and copy the JRE home path (for example, `C:\Program Files\Zulu\zulu-8\jre\`).

    Depending on your preferred Java distribution, your location of the JDK or JRE might be different than the example path above. Even if you have a JDK installed, you often times will get a JRE sub folder as part of that installation, so point to the JRE folder in that case. The Java extension will attempt to load the `jvm.dll` from the path `%JRE_HOME%\bin\server`.

2. In Control Panel, open **System and Security**, open **System**, and click **Advanced System Properties**.

3. Click **Environment Variables**.

4. Create a new system variable for `JRE_HOME` with the value of the JDK/JRE path (found in step 1).

5. Restart [Launchpad](../concepts/extensibility-framework.md#launchpad).

    1. Open [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

    2. Under SQL Server Services, right-click SQL Server Launchpad and select **Restart**.

<a name="perms-nonwindows"></a>

## Grant access to non-default JRE folder (Windows only)

If you did not install the JDK or JRE under program files, you need to perform the following steps. Run the **icacls** commands from an *elevated* line to grant access to the **SQLRUsergroup** and SQL Server service accounts (in **ALL_APPLICATION_PACKAGES**) for accessing the JRE. The commands will recursively grant access to all files and folders under the given directory path.

1. Give SQLRUserGroup permissions

    For a named instance,  append the instance name to SQLRUsergroup (for example, `SQLRUsergroupINSTANCENAME`).

    ```cmd
    icacls "<PATH to JRE>" /grant "SQLRUsergroup":(OI)(CI)RX /T
    ```
    
    You can skip this step if you installed the JDK/JRE in the default folder under program files on Windows.

2. Give AppContainer permissions

    ```cmd
    icacls "<PATH to JRE>" /grant "ALL APPLICATION PACKAGES":(OI)(CI)RX /T
    ```
    
## Enable script execution

1. Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. 

    > [!TIP]
    > You can download and install the appropriate version from this page: [Download SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms).
    > 
    > You can also use [Azure Data Studio](../../azure-data-studio/what-is.md), which supports administrative tasks and queries against SQL Server.
  
2. Connect to the instance where you installed Language Extensions, click **New Query** to open a query window, and run the following command:

    ```sql
    sp_configure
    ```

    The value for the property, `external scripts enabled`, should be **0** at this point. That is because the feature is turned off by default. The feature must be explicitly enabled by an administrator before you can run Java code.
    
3.  To enable the external scripting feature, run the following statement:
    
    ```sql
    EXEC sp_configure  'external scripts enabled', 1
    RECONFIGURE WITH OVERRIDE
    ```
    
    If you have already enabled the feature for Machine Learning Services, don't run reconfigure a second time for Language Extensions. The underlying extensibility platform supports both.

## Restart the service

When the installation is complete, restart the database engine before continuing to the next, enabling script execution.

Restarting the service also automatically restarts the related SQL Server Launchpad service.

You can restart the service using the right-click **Restart** command for the instance in SSMS, or by using the **Services** panel in Control Panel, or by using [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

## Verify installation

Check the installation status of the instance in the setup logs.

Use the following steps to verify that all components used to launch external script are running.

1. In SQL Server Management Studio or Azure Data Studio, open a new query window, and run the following statement:
    
    ```sql
    EXEC sp_configure  'external scripts enabled'
    ```

    The **run_value** should now be set to 1.
    
2. Open the **Services** panel or SQL Server Configuration Manager, and verify **SQL Server Launchpad service** is running. You should have one service for every database engine instance that has language extensions installed. For more information about the service, see [Extensibility framework](../concepts/extensibility-framework.md). 
   
## Additional configuration

If the verification step was successful, you can run Java Code from SQL Server Management Studio, Azure Data Studio, Visual Studio Code, or any other client that can send T-SQL statements to the server.

If you got an error when running the command, review the additional configuration steps in this section. You might need to make additional appropriate configurations to the service or database.

At the instance level, additional configuration might include:

* [Firewall configuration for SQL Server Machine Learning Services](../../advanced-analytics/security/firewall-configuration.md)
* [Enable additional network protocols](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md)
* [Enable remote connections](../../database-engine/configure-windows/configure-the-remote-access-server-configuration-option.md)
* [Create a login for SQLRUserGroup](../../advanced-analytics/security/create-a-login-for-sqlrusergroup.md)

<a name="bkmk_configureAccounts"></a> 
<a name="permissions-external-script"></a> 

On the database, you might need the following configuration updates:

* [Give users permission to SQL Server Machine Learning Services](../../advanced-analytics/security/user-permission.md)

> [!NOTE]
> Whether additional configuration is required depends on your security schema, where you installed SQL Server, and how you expect users to connect to the database and run external scripts.

## Suggested optimizations

Now that you have everything working, you might also want to optimize the server to support language extensions.

### Optimize the server for language extensions

The default settings for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup are intended to optimize the balance of the server for a variety of services that are supported by the database engine, which might include extract, transform, and load (ETL) processes, reporting, auditing, and applications that use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data. Therefore, under the default settings, you might find that resources for language extensions are sometimes restricted or throttled, particularly in memory-intensive operations.

To ensure that language extensions jobs are prioritized and resourced appropriately, we recommend that you use SQL Server Resource Governor to configure an external resource pool. You might also want to change the amount of memory that's allocated to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database engine, or increase the number of accounts that run under the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service.

- To configure a resource pool for managing external resources, see [Create an external resource pool](../../t-sql/statements/create-external-resource-pool-transact-sql.md).
  
- To change the amount of memory reserved for the database, see [Server memory configuration options](../../database-engine/configure-windows/server-memory-server-configuration-options.md).
  
If you are using Standard Edition and do not have Resource Governor, you can use Dynamic Management Views (DMVs) and Extended Events, as well as Windows event monitoring, to help manage the server resources. 

## Limitations in CTP 3.0

* The number of values in input and output buffers cannot exceed `MAX_INT (2^31-1)` since that is the maximum number of elements that can be allocated in an array in Java.

* Output parameters in [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) are not supported in this version.

* Streaming using the sp_execute_external_script parameter @r_rowsPerRead is not supported in this CTP.

* Partitioning using the sp_execute_external_script parameter @input_data_1_partition_by_columns is not supported in this CTP.

## Next steps

Java developers can get started with some simple examples, and learn the basics of how Java works with SQL Server. For your next step, see the following link:

+ [Tutorial: Regular expressions with Java](../tutorials/search-for-string-using-regular-expressions-in-java.md)