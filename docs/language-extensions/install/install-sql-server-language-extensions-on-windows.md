---
title: Install SQL Server Language Extensions on Windows
titleSuffix: SQL Server Language Extensions
description: Language extensions installation steps for SQL Server 2019 in Windows.
author: dphansen
ms.author: davidph 
manager: cgronlun
ms.date: 05/06/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: language-extensions
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---
# Install SQL Server Machine Learning Services on Windows
[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

Starting in SQL Server 2019, Language Extensions and Java support is provided. This article explains how to install the Language Extensions component by running the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup wizard.

## <a name="bkmk_prereqs"> </a> Pre-install checklist

+ SQL Server 2019 Setup is required if you want to install support for Language Extensions.

+ A database engine instance is required. You cannot install just the Language Extensions features, although you can add them incrementally to an existing instance.

+ For business continuity, [Always On Availabilty Groups](https://docs.microsoft.com/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server) are supported for Language Extensions. You have to install language extensions, and configure packages, on each node.

+ Installing Language Extensions is supported on a failover cluster in SQL Server 2019.

+ Do not install SQL Server Language Extensions on a domain controller. The Language Extensions portion of setup will fail.

> [!IMPORTANT]
> After setup is complete, be sure to complete the post-configuration steps described in this article. These steps include enabling SQL Server to use external code, and adding accounts required for SQL Server to run Java code on your behalf. Configuration changes generally require a restart of the instance, or a restart of the Launchpad service.

## Get the installation media

[!INCLUDE[GetInstallationMedia](../../includes/getssmedia.md)]

## Run Setup

For local installations, you must run Setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.

1. Start the setup wizard for SQL Server 2019. 
  
2. On the **Installation** tab, select **New SQL Server stand-alone installation or add features to an existing installation**.

<!--   ![New SQL Server stand-alone installation](media/2019setup-installation-page-mlsvcs.PNG) -->
   
3. On the **Feature Selection** page, select these options:
  
    -   **Database Engine Services**
  
         To use Language Extensions with SQL Server, you must install an instance of the database engine. You can use either a default or a named instance.
  
    -   **Machine Learning Services and Language Extensions**
  
         This option installs the Language Extensions component that support Java code execution.

    -   **Java**

        Check this option to add the Microsoft R packages, interpreter, and open-source R. 

    -   **Python**

        Check this option to add the Microsoft Python packages, the Python 3.5 executable, and select libraries from the Anaconda distribution.
        
<!--        ![Feature options for Language Extensions](media/2019setup-features-page-language-extensions.png "Setup options for Python") -->

<!-- Not sure if we have a concent page yet...

4. On the **Consent to Install R** page, select **Accept**. This license agreement covers Microsoft R Open, which includes a distribution of the open-source R base packages and tools, together with enhanced R packages and connectivity providers from the Microsoft development team.

5. On the **Consent to Install Python** page, select **Accept**. The Python open-source licensing agreement also covers Anaconda and related tools, plus some new Python libraries from the Microsoft development team.
     
     ![Agreement to Python license](media/2017setup-python-license.png "License agreement for Python")
  
    > [!NOTE]
    >  If the computer you are using does not have internet access, you can pause setup at this point to download the installers separately. For more information, see [Install machine learning components without internet access](../install/sql-ml-component-install-without-internet-access.md).
  
     Select **Accept**, wait until the **Next** button becomes active, and then select **Next**.
-->
  
6. On the **Ready to Install** page, verify that these selections are included, and select **Install**.
  
    + Database Engine Services
    + Machine Learning Services and Language Extensions
    + Java

    Note of the location of the folder under the path `..\Setup Bootstrap\Log` where the configuration files are stored. When setup is complete, you can review the installed components in the Summary file.

7. After setup is complete, if you are instructed to restart the computer, do so now. It is important to read the message from the Installation Wizard when you have finished with Setup. For more information, see [View and Read SQL Server Setup Log Files](https://docs.microsoft.com/sql/database-engine/install-windows/view-and-read-sql-server-setup-log-files).

## Add the JRE_HOME variable

JRE_HOME is a system environment variable that specifies the location of the Java interpreter. In this step, create a system environment variable for it on Windows.

1. Find and copy the JRE home path (for example, `C:\Program Files\Zulu\zulu-8\jre\`).

    Depending on your preferred Java distribution, your location of the JDK or JRE might be different than the example path above. Even if you have a JDK installed, you often times will get a JRE sub folder as part of that installation, so point to the jre folder in that case. The Java extension will attempt to load the `jvm.dll` from the path `%JRE_HOME%\bin\server`.

2. In Control Panel, open **System and Security**, open **System**, and click **Advanced System Properties**.

3. Click **Environment Variables**.

4. Create a new system variable for `JRE_HOME` with the value of the JDK/JRE path (found in step 1).

5. Restart [Launchpad](../concepts/extensibility-framework.md#launchpad).

    1. Open [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

    2. Under SQL Server Services, right-click SQL Server Launchpad and select **Restart**.

<a name="perms-nonwindows"></a>

## Grant access to non-default JRE folder (Windows only)

If you did not install the JDK or JRE under program files, you need to perform the following steps. Run the **icacls** commands from an *elevated* line to grant access to the **SQLRUsergroup** and SQL Server service accounts (in **ALL_APPLICATION_PACKAGES**) for accessing the JRE. The commands will recursively grant access to all files and folders under the given directory path.

### SQLRUserGroup permissions

For a named instance,  append the instance name to SQLRUsergroup (for example, `SQLRUsergroupINSTANCENAME`).

```cmd
icacls "<PATH to JRE>" /grant "SQLRUsergroup":(OI)(CI)RX /T
```

You can skip this step if you installed the JDK/JRE in the default folder under program files on Windows.

### AppContainer permissions

```cmd
icacls "PATH to JRE" /grant "ALL APPLICATION PACKAGES":(OI)(CI)RX /T
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

1. In SQL Server Management Studio, open a new query window, and run the following command:
    
    ```sql
    EXEC sp_configure  'external scripts enabled'
    ```

    The **run_value** should now be set to 1.
    
2. Open the **Services** panel or SQL Server Configuration Manager, and verify **SQL Server Launchpad service** is running. You should have one service for every database engine instance that has R or Python installed. For more information about the service, see [Extensibility framework](../concepts/extensibility-framework.md). 
   
3. If Launchpad is running, you should be able to run simple R and Python scripts to verify that external scripting runtimes can communicate with SQL Server.

   Open a new **Query** window in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then run a script such as the following:
    
    + For R
    
    ```sql
    EXEC sp_execute_external_script  @language =N'R',
    @script=N'
    OutputDataSet <- InputDataSet;
    ',
    @input_data_1 =N'SELECT 1 AS hello'
    WITH RESULT SETS (([hello] int not null));
    GO
    ```

    + For Python
    
    ```sql
    EXEC sp_execute_external_script  @language =N'Python',
    @script=N'
    OutputDataSet = InputDataSet;
    ',
    @input_data_1 =N'SELECT 1 AS hello'
    WITH RESULT SETS (([hello] int not null));
    GO
    ```

 **Results**

    The script can take a little while to run, the first time the external script runtime is loaded. The results should be something like this:

    | hello |
    |----|
    | 1|


<!--  The preceding 'hello' table is NOT rendering properly on live Docs.
Instead, the RAW markdown for the table is being displayed.  Probable bug in this markdown source,
due to stricter rules imposed by 'markdig' engine (replaced 'DFM').
I will inform HeidiSteen  [GeneMi, 2019/01/17]
-->


> [!NOTE]
> Columns or headings used in the Python script are not returned, by design. To add column names for your output, you must specify the schema for the return data set. Do this by using the WITH RESULTS parameter of the stored procedure, naming the columns and specifying the SQL data type.
> 
> For example, you can add the following line to generate an arbitrary column name: `WITH RESULT SETS ((Col1 AS int))`

<a name="apply-cu"></a>

## Apply updates

We recommend that you apply the latest cumulative update to both the database engine and machine learning components.

On internet-connected devices, cumulative updates are typically applied through Windows Update, but you can also use the steps below for controlled updates. When you apply the update for the database engine, Setup pulls cumulative updates for any R or Python features you installed on the same instance. 

On disconnected servers, extra steps are required. For more information, see [Install on computers with no internet access > Apply cumulative updates](sql-ml-component-install-without-internet-access.md#apply-cu).

1. Start with a baseline instance already installed: SQL Server 2017 initial release

2. Go to the cumulative update list: [SQL Server 2017 updates](https://sqlserverupdates.com/sql-server-2017-updates/)

3. Select the latest cumulative update. An executable is downloaded and extracted automatically.

4. Run Setup. Accept the licensing terms, and on the Feature selection page, review the features for which cumulative updates are applied. You should see every feature installed for the current instance, including machine learning features. Setup downloads the CAB files necessary to update all features.

  ![Summary of installed features](media/cumulative-update-feature-selection.png)

5. Continue through the wizard, accepting the licensing terms for R and Python distributions. 

## Additional configuration

If the external script verification step was successful, you can run R or Python commands from SQL Server Management Studio, Visual Studio Code, or any other client that can send T-SQL statements to the server.

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

Now that you have everything working, you might also want to optimize the server to support machine learning, or install pretrained models.

### Add more worker accounts

If you expect many users to be running scripts concurrently, you can increase the number of worker accounts that are assigned to the Launchpad service. For more information, see [Modify the user account pool for SQL Server Machine Learning Services](../administration/modify-user-account-pool.md).

### Optimize the server for script execution

The default settings for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup are intended to optimize the balance of the server for a variety of services that are supported by the database engine, which might include extract, transform, and load (ETL) processes, reporting, auditing, and applications that use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data. Therefore, under the default settings, you might find that resources for machine learning are sometimes restricted or throttled, particularly in memory-intensive operations.

To ensure that machine learning jobs are prioritized and resourced appropriately, we recommend that you use SQL Server Resource Governor to configure an external resource pool. You might also want to change the amount of memory that's allocated to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database engine, or increase the number of accounts that run under the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service.

- To configure a resource pool for managing external resources, see [Create an external resource pool](../../t-sql/statements/create-external-resource-pool-transact-sql.md).
  
- To change the amount of memory reserved for the database, see [Server memory configuration options](../../database-engine/configure-windows/server-memory-server-configuration-options.md).
  
- To change the number of R accounts that can be started by [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)], see [Modify the user account pool for machine learning](../administration/modify-user-account-pool.md).

If you are using Standard Edition and do not have Resource Governor, you can use Dynamic Management Views (DMVs) and Extended Events, as well as Windows event monitoring, to help manage the server resources. For more information, see [Monitoring and managing R Services](../r/managing-and-monitoring-r-solutions.md) and [Monitoring and managing Python Services](../python/managing-and-monitoring-python-solutions.md).

### Install additional R packages

The R solutions you create for SQL Server can call basic R functions, functions from the proprietary packages installed with SQL Server, and third-party R packages compatible with the version of open-source R installed by SQL Server.

Packages that you want to use from SQL Server must be installed in the default library that is used by the instance. If you have a separate installation of R on the computer, or if you installed packages to user libraries, you won't be able to use those packages from T-SQL.

The process for installing and managing R packages is different in SQL Server 2016 and SQL Server 2017. In SQL Server 2016, a database administrator must install R packages that users need. In SQL Server 2017, you can set up user groups to share packages on a per-database level, or configure database roles to enable users to install their own packages. For more information, see [Install new R packages in SQL Server](../r/install-additional-r-packages-on-sql-server.md).


## Next steps

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

+ [Tutorial: Run R in T-SQL](../tutorials/rtsql-using-r-code-in-transact-sql-quickstart.md)
+ [Tutorial: In-database analytics for R developers](../tutorials/sqldev-in-database-r-for-sql-developers.md)

Python developers can learn how to use Python with SQL Server by following these tutorials:

+ [Tutorial: Run Python in T-SQL](../tutorials/run-python-using-t-sql.md)
+ [Tutorial: In-database analytics for Python developers](../tutorials/sqldev-in-database-python-for-sql-developers.md)

To view examples of machine learning that are based on real-world scenarios, see [Machine learning tutorials](../tutorials/machine-learning-services-tutorials.md).
