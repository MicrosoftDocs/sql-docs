---
title: Install SQL Server 2022 Machine Learning Services on Windows
description: Learn how to install SQL Server 2022 Machine Learning Services on Windows. You can use Machine Learning Services to execute Python, R, or Java scripts in-database.
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 08/23/2022
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.custom:
- seo-lt-2019
- intro-installation
- event-tier1-build-2022
monikerRange: ">=sql-server-ver16"
---
# Install SQL Server 2022 Machine Learning Services (Python and R) on Windows

[!INCLUDE [SQL Server 2022](../../includes/applies-to-version/sqlserver2022.md)]

Learn how to install [SQL Server 2022 Machine Learning Services](../sql-server-machine-learning-services.md) on Windows. You can use Machine Learning Services to execute Python and R scripts in-database.

> [!NOTE]
> These instructions are specific to SQL Server 2022 on Windows. To install SQL Server Machine Learning Services on Windows for SQL Server 2016, SQL Server 2017, or SQL Server 2019, see [Install SQL Server Machine Learning Services (Python and R) on Windows](sql-machine-learning-services-windows-install.md). For Linux, see [Install SQL Server Machine Learning Services (Python and R) on Linux](../../linux/sql-server-linux-setup-machine-learning.md).

## <a name="bkmk_prereqs"> </a> Pre-install checklist

+ A database engine instance is required. You can't install just Python or R features, although you can add them incrementally to an existing instance.

+ For business continuity, [Always On Availability Groups](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) are supported for Machine Learning Services. Install Machine Learning Services, and configure packages, on each node. 

+ Installing Machine Learning Services is also supported on an [Always On Failover Cluster Instance (FCI)](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md) in SQL Server 2019 and later.
 
+ Don't install Machine Learning Services on a domain controller. The Machine Learning Services portion of setup will fail.

+ Side-by-side installation with other versions of Python and R is supported but isn't recommended. It's supported because the SQL Server instance uses its own copies of the open-source R and Anaconda distributions. It isn't recommended because running code that uses Python and R on a SQL Server computer outside SQL Server can lead to various problems:
    
  + Using a different library and executable files will create inconsistent results, than what you are running in SQL Server.
  + R and Python scripts running in external libraries can't be managed by SQL Server, leading to resource contention.

> [!IMPORTANT]
> After setup is complete, be sure to complete the post-configuration steps described in this article. These steps include enabling SQL Server to use external scripts and adding accounts required for SQL Server to run R and Python jobs on your behalf. Configuration changes generally require a restart of the instance or a restart of the Launchpad service.

## Get the installation media

[!INCLUDE[GetInstallationMedia](../../includes/getssmedia.md)]

<!-- For more information on which SQL Server editions support Python and R integration with Machine Learning Services, see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).  -->

## Run setup

For local installations, you must run the setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.

If you encounter any installation errors during setup, check the summary log in the Setup Bootstrap log folder. For example, `%ProgramFiles%\Microsoft SQL Server\160\Setup Bootstrap\Log\Summary.txt`.

1. Start the SQL Setup wizard for SQL Server.
  
2. On the **Installation** tab, select **New SQL Server stand-alone installation or add features to an existing installation**.

3. On the **Feature Selection** page, select these options:
 
  - **Database Engine Services**
     
    To use R or Python with SQL Server, you must install an instance of the database engine. You can use either a default or a named instance.

  - **Machine Learning Services (In-Database)**
     
    This option installs the database services that support R and Python script execution.

    This screenshot shows the minimum **Instance Features** to check when installing SQL Server 2022 Machine Learning Services.

    :::image type="content" source="media/machine-learning-services-windows-install-sql-2022/sql-server-2022-machine-learning-services-feature-selection.png" alt-text="Screenshot of feature selection showing check boxes next to Database Engine Services and Machine Learning Services and Language.":::


4. **Next steps vary from previous versions:** Beginning with SQL Server 2022, runtimes for R, Python, and Java, are no longer shipped or installed within SQL Setup. Instead, install your desired R and/or Python custom runtime(s) and packages. See the next sections and continue to step 5:

    - [Install R](#install-r)
    - [Install Python](#install-python)
    - [Install Java](#install-java)

**Continue to the instructions to [install R](#install-r) or [install Python](#install-python).**

## Install R

5. Download the most recent version of [R 4.2 for Windows](https://cran.r-project.org/bin/windows/base/) for Windows, and install.

6. Install CompatibilityAPI and RevoScaleR dependencies. From the R terminal of the version you have installed, run the following:

    ```r
    # R Terminal
    install.packages("iterators")
    install.packages("foreach")
    install.packages("R6")
    install.packages("jsonlite")
    ```
    
7. Install the latest version of RevoScaleR package and its dependencies. Download links available here:

   - [CompatibilityAPI Windows](https://go.microsoft.com/fwlink/?LinkID=2193827)
   - [RevoScaleR package for Windows](https://go.microsoft.com/fwlink/?LinkID=2193828)

    Assuming you have installed version 4.2.0 of R in its default location, for example `C:\Program Files\R\R-4.2.0`, and downloaded the required packages in `C:\temp` directory, the following sample scripts can be adapted for the installation:

    ```cmd
    cd C:\Program Files\R\R-4.2.0\bin
    R.exe CMD INSTALL -l "C:\Program Files\R\R-4.2.0\library" "C:\temp\CompatibilityAPI.zip"
    R.exe CMD INSTALL -l "C:\Program Files\R\R-4.2.0\library" "C:\temp\RevoScaleR.zip"
    ```

8. Configure the installed R runtime with SQL Server. You can change the default version by using the `RegisterRext.exe` command-line utility. The utility is in an R application folder depending on the installation, usually in `%ProgramFiles%\R\R-4.2.0\library\RevoScaleR\rxLibs\x64`.

   The following script can be used to configure the installed R runtime from the installation folder location of `RegisterRext.exe`. The instance name is "MSSQLSERVER" for a default instance of SQL Server, or the instance name for a named instance of SQL Server.

  ```cmd
  .\RegisterRext.exe /configure /rhome:"%ProgramFiles%\R\R-4.2.0" /instance:"MSSQLSERVER"
  ```

9. Using [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md), connect to the instance where you installed SQL Server Machine Learning Services. Select **New Query** to open a query window, and **Execute*** the following command to enable the external scripting feature:

    ```sql
    EXEC sp_configure  'external scripts enabled', 1;
    RECONFIGURE WITH OVERRIDE
    ```

    If you have already enabled the feature for another language, you don't need run reconfigure a second time for R. The underlying extensibility platform supports both languages. To verify, confirm that the following output returns a `config_value` and `run_value` of `1`:

    ```sql
    EXEC sp_configure  'external scripts enabled';
    ```

10. Restart the SQL Server service. Restarting the service also automatically restarts the related [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service. You can restart the service using the right-click **Restart** command for the instance in the SSMS Object Explorer, or by using the **Services** panel in Control Panel, or by using [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

11. Verify the installation by executing a simple T-SQL command to return the version of R:

    ```sql
    EXEC sp_execute_external_script @script=N'print(R.version)',@language=N'R';
    GO
    ```


## Install Python

5. Download the most recent version of [Python 3.10 for Windows](https://www.python.org/downloads/) for Windows. Install using the following options:
    1. Launch the Python Setup application and choose **Customize installation**. 
    1. Verify the box is checked next to the option to **Install launcher for all users (recommended)**.
    1. Select all **Optional Features** options, or as desired.
    1. On the **Advanced Options** page, check **Install for all users**, accept other default options, and select **Install**. It is recommended that the Python installation path is accessible by all users such as `C:\Program Files\Python310 `and it is not specific to a single user.

6. Download the latest version of RevoScalePY package and its dependencies: [revoscalepy Python Windows](https://go.microsoft.com/fwlink/?LinkID=2193924) and install revoscalepy from the Python custom install location. For example: 

    ```cmd
    cd C:\Program Files\Python310\
    python -m pip install C:\Users\%username%\Downloads\revoscalepy-10.0.0-py3-none-any.whl
    ```

7. Configure the installed Python runtime with SQL Server. You can change the default version by using the **RegisterRext.exe** command-line utility. The utility is in the custom install location, for example: `C:\Program Files\Python310\Lib\site-packages\revoscalepy\rxLibs`.

    The following script can be used to configure the installed Python runtime from the installation folder location of **RegisterRext.exe**. The instance name is "MSSQLSERVER" for a default instance of SQL Server, or the instance name for a named instance of SQL Server.
    
    ```cmd
    cd C:\Program Files\Python310\Lib\site-packages\revoscalepy\rxLibs
    .\RegisterRext.exe /configure /pythonhome:"C:\Program Files\Python310" /instance:"MSSQLSERVER"
    ```

8. Use [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md) to connect to the instance where you installed SQL Server Machine Learning Services. Select **New Query** to open a query window, and **Execute*** the following command to enable the external scripting feature:

    ```sql
    EXEC sp_configure  'external scripts enabled', 1;
    RECONFIGURE WITH OVERRIDE
    ```

    If you have already enabled the feature for another language, you don't need run reconfigure a second time for R. The underlying extensibility platform supports both languages. To verify, confirm that the following output returns a `config_value` and `run_value` of `1`:

    ```sql
    EXEC sp_configure  'external scripts enabled';
    ```

9. Restart the SQL Server service. Restarting the service also automatically restarts the related [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service. You can restart the service using the right-click **Restart** command for the instance in the SSMS Object Explorer, or by using the **Services** panel in Control Panel, or by using [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

10. Verify the installation by executing a simple command to return the version of Python:

    ```sql
    EXEC sp_execute_external_script @script=N'import sys;print(sys.version)',@language=N'Python'
    GO
    ```

## Install Java

For information on installing and using Java, see [Install SQL Server Language Extensions on Windows](../../language-extensions/install/windows-java.md).

## Additional configuration

If the external script verification step was successful, you can run R or Python commands from SQL Server Management Studio, Visual Studio Code, or any other client that can send T-SQL statements to the server.

Whether the additional configuration is required depends on your security schema, where you installed SQL Server, and how you expect users to connect to the database and run external scripts.

If you got an error when running the command, review the additional configuration steps in this section. You might need to make additional appropriate configurations to the service or database.

At the instance level, additional configuration might include:

* [Firewall configuration for SQL Server Machine Learning Services](../../machine-learning/security/firewall-configuration.md)
* [Enable additional network protocols](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md)
* [Enable remote connections](../../database-engine/configure-windows/configure-the-remote-access-server-configuration-option.md)
* [Create a login for SQLRUserGroup](../../machine-learning/security/create-a-login-for-sqlrusergroup.md)
* [Manage disk quotas](/windows/desktop/fileio/managing-disk-quotas) to avoid external scripts running tasks that exhaust disk space

Starting with SQL Server 2019 on Windows, the isolation mechanism has changed. This mechanism affects **SQLRUserGroup**, firewall rules, file permission, and implied authentication. For more information, see [Isolation changes for Machine Learning Services](sql-server-machine-learning-services-2019.md).

<a name="bkmk_configureAccounts"></a> 
<a name="permissions-external-script"></a> 

On the database, you might need the following configuration updates: [Give users permission to SQL Server Machine Learning Services](../../machine-learning/security/user-permission.md).

## Suggested optimizations

Now that you have everything working, you might also want to optimize the server to support machine learning or install a pre-trained machine learning model.

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
