---
title: Install on Windows
description: Learn how to install SQL Server Machine Learning Services on Windows. You can use Machine Learning Services to execute Python and R scripts in-database.
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 02/29/2020
ms.topic: how-to
author: cawrites
ms.author: chadam
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions"
---
# Install SQL Server Machine Learning Services (Python and R) on Windows

[!INCLUDE [SQL Server 2017 and later](../../includes/applies-to-version/sqlserver2017.md)]

Learn how to install SQL Server Machine Learning Services on Windows. You can use Machine Learning Services to execute Python and R scripts in-database.

## <a name="bkmk_prereqs"> </a> Pre-install checklist

+ A database engine instance is required. You can't install just Python or R features, although you can add them incrementally to an existing instance.

+ For business continuity, [Always On Availability Groups](https://docs.microsoft.com/sql/database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server) are supported for Machine Learning Services. Install Machine Learning Services, and configure packages, on each node.

+ Installing Machine Learning Services *isn't supported* on an [Always On Failover Cluster Instance (FCI)](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md) in SQL Server 2017. It's supported with SQL Server 2019 and later.
 
+ Don't install Machine Learning Services on a domain controller. The Machine Learning Services portion of setup will fail.

+ Don't install **Shared Features** > **Machine Learning Server (Standalone)** on the same computer running a database instance. A stand-alone server will compete for the same resources, diminishes the performance of both installations.

+ Side-by-side installation with other versions of Python and R is supported but isn't recommended. It's supported because the SQL Server instance uses its own copies of the open-source R and Anaconda distributions. It isn't recommended because running code that uses Python and R on a SQL Server computer outside SQL Server can lead to various problems:
    
  + Using a different library and executable files will create inconsistent results, than what you are running in SQL Server.
  + R and Python scripts running in external libraries can't be managed by SQL Server, leading to resource contention.

::: moniker range=">=sql-server-ver15||=sqlallproducts-allversions"
> [!NOTE]
> Machine Learning Services is installed by default on **SQL Server Big Data Clusters**. You don't need to follow the steps in this article if you use a **Big Data Cluster**. For more information, see [Use Machine Learning Services (Python and R) on Big Data Clusters](../../big-data-cluster/machine-learning-services.md).
::: moniker-end

> [!IMPORTANT]
> After setup is complete, be sure to complete the post-configuration steps described in this article. These steps include enabling SQL Server to use external scripts and adding accounts required for SQL Server to run R and Python jobs on your behalf. Configuration changes generally require a restart of the instance or a restart of the Launchpad service.

## Get the installation media

[!INCLUDE[GetInstallationMedia](../../includes/getssmedia.md)]

::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
For more information on which SQL Server editions support Python and R integration with Machine Learning Services, see [Editions and supported features of SQL Server 2017](https://docs.microsoft.com/sql/sql-server/editions-and-components-of-sql-server-2017).
::: moniker-end

::: moniker range="=sql-server-ver15||=sqlallproducts-allversions"
For more information on which SQL Server editions support Python and R integration with Machine Learning Services, see [Editions and supported features of SQL Server 2019 (15.x)](https://docs.microsoft.com/sql/sql-server/editions-and-components-of-sql-server-version-15).
::: moniker-end

## Run setup

For local installations, you must run the setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.

1. Start the setup wizard for SQL Server.
  
1. On the **Installation** tab, select **New SQL Server stand-alone installation or add features to an existing installation**.

   ::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
   ![New SQL Server stand-alone installation](media/2017setup-installation-page-mlsvcs.png)
   ::: moniker-end

   ::: moniker range="=sql-server-ver15||=sqlallproducts-allversions"
   ![New SQL Server stand-alone installation](media/2019setup-installation-page-mlsvcs.png)
   ::: moniker-end

1. On the **Feature Selection** page, select these options:

   ::: moniker range="=sql-server-2017||=sqlallproducts-allversions"

   - **Database Engine Services**
     
     To use R and Python with SQL Server, you must install an instance of the database engine. You can use either a default or a named instance.

   - **Machine Learning Services (In-Database)**
     
     This option installs the database services that support R and Python script execution.

   ::: moniker-end

   ::: moniker range="=sql-server-ver15||=sqlallproducts-allversions"

   - **Database Engine Services**
     
     To use R or Python with SQL Server, you must install an instance of the database engine. You can use either a default or a named instance.

   - **Machine Learning Services (In-Database)**
     
     This option installs the database services that support R and Python script execution.

   ::: moniker-end

   - **R**
     
     Check this option to add the Microsoft R packages, interpreter, and open-source R. 
     
   - **Python**
     
     Check this option to add the Microsoft Python packages, the Python 3.5 executable, and select libraries from the Anaconda distribution.
     
   ::: moniker range="=sql-server-ver15||=sqlallproducts-allversions"
   For information on installing and using Java, see [Install SQL Server Language Extensions on Windows](../../language-extensions/install/install-sql-server-language-extensions-on-windows.md).
   ::: moniker-end
   
   ::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
   ![Feature options for R and Python](media/2017setup-features-page-mls-rpy.PNG "Setup options for R and Python")
   ::: moniker-end
   
   ::: moniker range="=sql-server-ver15||=sqlallproducts-allversions"
   ![Feature options for R and Python](media/2019setup-features-page-mls-rpy.png "Setup options for R and Python")
   ::: moniker-end
   
   > [!NOTE]
   > 
   > Don't select the option for **Machine Learning Server (Standalone)**. The option to install Machine Learning Server under **Shared Features** is intended for use on a separate computer.

::: moniker range="=sql-server-2017||=sqlallproducts-allversions"

4. On the **Consent to Install Microsoft R Open** page, select **Accept** and then **Next**. 

The license agreement covers:
+ Microsoft R Open
+ Open-source R base packages and tools
+ Enhanced R packages and connectivity providers from the Microsoft development team.

1. On the **Consent to Install Python** page, select **Accept** and then **Next**. The Python open-source licensing agreement also covers Anaconda and related tools, plus some new Python libraries from the Microsoft development team.

   > [!NOTE]
   >  If the computer you are using doesn't have internet access, you can pause setup at this point to download the installers separately. For more information, see [Install machine learning components without internet access](../install/sql-ml-component-install-without-internet-access.md).

1. On the **Ready to Install** page, verify that these selections are included, and select **Install**.
  
   + Database Engine Services
   + Machine Learning Services (In-Database)
   + R or Python, or both

   Note of the location of the folder under the path `..\Setup Bootstrap\Log` where the configuration files are stored. When setup is complete, you can review the installed components in the Summary file.

1. After setup is complete, if you are instructed to restart the computer, do so now. It's important to read the message from the Installation Wizard when you have finished with setup. For more information, see [View and Read SQL Server Setup Log Files](https://docs.microsoft.com/sql/database-engine/install-windows/view-and-read-sql-server-setup-log-files).

::: moniker-end

::: moniker range="=sql-server-ver15||=sqlallproducts-allversions"

1. On the **Consent to Install Microsoft R Open** page, select **Accept** and then **Next**. This license agreement covers Microsoft R Open, which includes a distribution of the open-source R base packages and tools, together with enhanced R packages and connectivity providers from the Microsoft development team.

2. On the **Consent to Install Python** page, select **Accept** and then **Next**. The Python open-source licensing agreement also covers Anaconda and related tools, plus some new Python libraries from the Microsoft development team.

3. On the **Ready to Install** page, verify that these selections are included, and select **Install**.
  
   + Database Engine Services
   + Machine Learning Services (In-Database)
   + R and/or Python

   Note the location of the folder under the path `..\Setup Bootstrap\Log` where the configuration files are stored. When setup is complete, you can review the installed components in the Summary file.

4. After setup is complete, if you're instructed to restart the computer, do so now. It is important to read the message from the Installation Wizard when you've finished with setup. For more information, see [View and Read SQL Server Setup Log Files](https://docs.microsoft.com/sql/database-engine/install-windows/view-and-read-sql-server-setup-log-files).

::: moniker-end

## Set environment variables

For R feature integration only, you should set the **MKL_CBWR** environment variable to [ensure consistent output](https://software.intel.com/articles/introduction-to-the-conditional-numerical-reproducibility-cnr) from Intel Math Kernel Library (MKL) calculations.

1. In Control Panel, click **System and Security** > **System** > **Advanced System Settings** > **Environment Variables**.

2. Create a new User or System variable. 

   + Set variable name to `MKL_CBWR`
   + Set the variable value to `AUTO`

This step requires a server restart. If you are about to enable script execution, you can hold off on the restart until all of the configuration work is done.

<a name="bkmk_enableFeature"></a>

## Enable script execution

1. Open [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)]. 

    > [!TIP]
    > You can download and install the appropriate version from this page: [Download SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/download-sql-server-management-studio-ssms).
    > 
    > You can also use [Azure Data Studio](../../azure-data-studio/what-is.md), which supports administrative tasks and queries against SQL Server.
  
2. Connect to the instance where you installed Machine Learning Services, click **New Query** to open a query window, and run the following command:

    ```sql
    sp_configure
    ```

    The value for the property, `external scripts enabled`, should be **0** at this point. The feature is turned off by default. The feature must be explicitly enabled by an administrator before you can run R or Python scripts.
    
3.  To enable the external scripting feature, run the following statement:
    
    ```sql
    EXEC sp_configure  'external scripts enabled', 1
    RECONFIGURE WITH OVERRIDE
    ```
    
    If you have already enabled the feature for the R language, don't run reconfigure a second time for Python. The underlying extensibility platform supports both languages.

## Restart the service

When the installation is complete, restart the database engine.

Restarting the service also automatically restarts the related [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service.

You can restart the service using the right-click **Restart** command for the instance in SSMS, or by using the **Services** panel in Control Panel, or by using [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

## Verify installation

Use the following steps to verify that all components used to launch external script are running.

1. In SQL Server Management Studio, open a new query window, and run the following command:
    
   ```sql
   EXECUTE sp_configure  'external scripts enabled'
   ```

   The **run_value** is set to 1.
    
2. Open the **Services** panel or SQL Server Configuration Manager, and verify **SQL Server Launchpad service** is running. You should have one service for every database engine instance that has R or Python installed. For more information about the service, see [Extensibility framework](../concepts/extensibility-framework.md). 
   
3. If Launchpad is running, you can run simple Python and R scripts to verify that external scripting runtimes can communicate with SQL Server.

   Open a new **Query** window in [!INCLUDE[ssManStudioFull](../../includes/ssmanstudiofull-md.md)], and then run a script such as:
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

> [!NOTE]
> Columns or headings used in the Python script aren't returned automatically. To add column names for your output, you must specify the schema for the return data set. Do this by using the WITH RESULTS parameter of the stored procedure, naming the columns, and specifying the SQL data type.
>
> For example, you can add the following line to generate an arbitrary column name: `WITH RESULT SETS ((Col1 AS int))`

::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
<!-- There are no updates yet available for 2019, and there's no 2019 update list site. When updates become available, add 2019 information to this section. -->

<a name="apply-cu"></a>

## Apply updates

We recommend that you apply the latest cumulative update to both the database engine and machine learning components.

On internet-connected devices, cumulative updates are typically applied through Windows Update, but you can also use the steps below for controlled updates. When you apply the update for the database engine, setup pulls cumulative updates for any Python or R features you installed on the same instance. 

Disconnected servers require extra steps. For more information, see [Install on computers with no internet access > Apply cumulative updates](sql-ml-component-install-without-internet-access.md#apply-cu).

1. Start with a baseline instance already installed: SQL Server 2017 initial release

2. Go to the cumulative update list: [Latest updates for Microsoft SQL Server](https://docs.microsoft.com/sql/database-engine/install-windows/latest-updates-for-microsoft-sql-server)

3. Select the latest cumulative update. An executable is downloaded and extracted automatically.

4. Run Setup. Accept the licensing terms, and on the Feature selection page, review the features for which cumulative updates are applied. You should see every feature installed for the current instance, including machine learning features. Setup downloads the CAB files necessary to update all features.

   ![Summary of installed features](media/cumulative-update-feature-selection.png)

5. Continue through the wizard, accepting the licensing terms for R and Python distributions. 

::: moniker-end

## Additional configuration

If the external script verification step was successful, you can run R or Python commands from SQL Server Management Studio, Visual Studio Code, or any other client that can send T-SQL statements to the server.

If you got an error when running the command, review the additional configuration steps in this section. You might need to make additional appropriate configurations to the service or database.

At the instance level, additional configuration might include:

* [Firewall configuration for SQL Server Machine Learning Services](../../machine-learning/security/firewall-configuration.md)
* [Enable additional network protocols](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md)
* [Enable remote connections](../../database-engine/configure-windows/configure-the-remote-access-server-configuration-option.md)
* [Create a login for SQLRUserGroup](../../machine-learning/security/create-a-login-for-sqlrusergroup.md)
* [Manage disk quotas](https://docs.microsoft.com/windows/desktop/fileio/managing-disk-quotas) to avoid external scripts running tasks that exhaust disk space

::: moniker range=">=sql-server-ver15||=sqlallproducts-allversions"
In SQL Server 2019 on Windows, the isolation mechanism has changed. This mechanism affects **SQLRUserGroup**, firewall rules, file permission, and implied authentication. For more information, see [Isolation changes for Machine Learning Services](sql-server-machine-learning-services-2019.md).
::: moniker-end

<a name="bkmk_configureAccounts"></a> 
<a name="permissions-external-script"></a> 

On the database, you might need the following configuration updates:

* [Give users permission to SQL Server Machine Learning Services](../../machine-learning/security/user-permission.md)

> [!NOTE]
> Whether the additional configuration is required depends on your security schema, where you installed SQL Server, and how you expect users to connect to the database and run external scripts.

## Suggested optimizations

Now that you have everything working, you might also want to optimize the server to support machine learning or install a pre-trained machine learning model.

::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
### Add more worker accounts

If you expect many users to be running scripts concurrently, you can increase the number of worker accounts that are assigned to the Launchpad service. For more information, see [Scale concurrent execution of external scripts in SQL Server Machine Learning Services](../administration/scale-concurrent-execution-external-scripts.md).
::: moniker-end

### Optimize the server for script execution

The default settings for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup are intended to optimize the balance of the server for a variety of services that are supported by the database engine, which might include extract, transform, and load (ETL) processes, reporting, auditing, and applications that use [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data. Under the default settings, resources for machine learning are sometimes restricted or throttled, particularly in memory-intensive operations.

To ensure that machine learning jobs are prioritized and resourced appropriately, we recommend that you use SQL Server Resource Governor to configure an external resource pool. You might also want to change the amount of memory that's allocated to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database engine, or increase the number of accounts that run under the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service.

- To configure a resource pool for managing external resources, see [Create an external resource pool](../../t-sql/statements/create-external-resource-pool-transact-sql.md).
  
- To change the amount of memory reserved for the database, see [Server memory configuration options](../../database-engine/configure-windows/server-memory-server-configuration-options.md).
  
- To change the number of R accounts that can be started by [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)], see [Scale concurrent execution of external scripts in SQL Server Machine Learning Services](../administration/scale-concurrent-execution-external-scripts.md).

If you are using Standard Edition and don't have Resource Governor, you can use Dynamic Management Views (DMVs) and Extended Events, as well as Windows event monitoring, to help manage the server resources.

### Install additional Python and R packages

The Python and R solutions you create for SQL Server can call basic functions, functions from the proprietary packages installed with SQL Server, and third-party packages compatible with the version of open-source Python and R installed by SQL Server.

Packages that you want to use from SQL Server must be installed in the default library used by the instance. If you have a separate installation of Python or R on the computer, or if you installed packages to user libraries, you can't use those packages from T-SQL.

To install and manage additional packages, you can set up user groups to share packages on a per-database level, or configure database roles to enable users to install their own packages. For more information, see [Install Python packages](../package-management/install-additional-python-packages-on-sql-server.md) and [Install new R packages](../package-management/install-additional-r-packages-on-sql-server.md).

## Next steps

Python developers can learn how to use Python with SQL Server by following these tutorials:

+ [Python tutorial: Predict ski rental with linear regression in SQL Server Machine Learning Services](../tutorials/python-ski-rental-linear-regression-deploy-model.md)
+ [Python tutorial: Categorizing customers using k-means clustering with SQL Server Machine Learning Services](../tutorials/python-clustering-model.md)

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

+ [Quickstart: Run R in T-SQL](../tutorials/quickstart-r-create-script.md)
+ [Tutorial: In-database analytics for R developers](../tutorials/r-taxi-classification-introduction.md)
