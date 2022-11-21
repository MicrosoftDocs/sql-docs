---
title: Install SQL Server 2022 Machine Learning Services on Windows
description: Learn how to install SQL Server 2022 Machine Learning Services on Windows to run Python, R, or Java scripts in-database.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 09/26/2022
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

This article shows you how to install [SQL Server 2022 Machine Learning Services](../sql-server-machine-learning-services.md) on Windows. You can use Machine Learning Services to run Python and R scripts in-database.

> [!NOTE]
> These instructions are specific to [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] on Windows. To install SQL Server Machine Learning Services on Windows for [!INCLUDE[sssql16-md](../../includes/sssql16-md.md)], [!INCLUDE[sssql17-md](../../includes/sssql17-md.md)], or [!INCLUDE[sssql19-md](../../includes/sssql19-md.md)], see [Install SQL Server Machine Learning Services (Python and R) on Windows](sql-machine-learning-services-windows-install.md). 
>
> For Linux, see [Install SQL Server Machine Learning Services (Python and R) on Linux](../../linux/sql-server-linux-setup-machine-learning.md).

## <a name="bkmk_prereqs"> </a> Pre-installation checklist

+ A database engine instance is required. You can't install just Python or R features, although you can add them incrementally to an existing instance.

+ For business continuity, [Always On availability groups](../../database-engine/availability-groups/windows/overview-of-always-on-availability-groups-sql-server.md) are supported for Machine Learning Services. Install Machine Learning Services, and configure packages, on each node. 

+ Installing Machine Learning Services is also supported on an [Always On failover cluster instance](../../sql-server/failover-clusters/windows/always-on-failover-cluster-instances-sql-server.md) in SQL Server 2019 and later.
 
+ Don't install Machine Learning Services on a domain controller. The Machine Learning Services portion of setup will fail.

+ Side-by-side installation with other versions of Python and R is supported, but we don't recommend it. It's supported because the SQL Server instance uses its own copies of the open-source R and Anaconda distributions. We don't recommend it because running code that uses Python and R on a computer outside SQL Server can lead to problems:
    
  + Using a different library and different executable files will create results that are inconsistent with what you're running in SQL Server.
  + SQL Server can't manage R and Python scripts that run in external libraries, leading to resource contention.

> [!IMPORTANT]
> After you finish setup, be sure to complete the post-configuration steps described in this article. These steps might include enabling SQL Server to use external scripts. Configuration changes generally require a restart of the instance or a restart of the Launchpad service.

## Get the installation media

[!INCLUDE[GetInstallationMedia](../../includes/getssmedia.md)]

<!-- For more information on which SQL Server editions support Python and R integration with Machine Learning Services, see [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md).  -->

## Run setup

For local installations, you must run the setup as an administrator. If you install [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a remote share, you must use a domain account that has read and execute permissions on the remote share.

If you encounter any installation errors during setup, check the summary log in the Setup Bootstrap log folder (for example, `%ProgramFiles%\Microsoft SQL Server\160\Setup Bootstrap\Log\Summary.txt`).

1. Start the SQL Server 2022 Setup wizard.
  
1. On the **Installation** tab, select **New SQL Server stand-alone installation or add features to an existing installation**.

1. On the **Feature Selection** page, select these options:
 
   - **Database Engine Services**
     
     To use R or Python with SQL Server, you must install an instance of the database engine. You can use either a default or a named instance.

   - **Machine Learning Services and Language**
     
      This option installs the database services that support R and Python script execution.

   This screenshot shows the minimum instance features to check when you're installing [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] Machine Learning Services.

   :::image type="content" source="media/machine-learning-services-windows-install-sql-2022/sql-server-2022-machine-learning-services-feature-selection.png" alt-text="Screenshot of feature selection showing check boxes next to Database Engine Services and Machine Learning Services and Language.":::

## Install runtimes and packages

Beginning with [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)], runtimes for R, Python, and Java are no longer shipped or installed with SQL Server setup. Instead, use the following sections to install your custom runtimes and packages. 

### Setup R support

#### Install R runtime

1. Download and install the most recent version of [R 4.2 for Windows](https://cran.r-project.org/bin/windows/base/).

1. Install dependencies for `CompatibilityAPI` and `RevoScaleR`. From the R terminal of the version that you installed, run the following commands:

    ```r
    # R Terminal
    install.packages("iterators")
    install.packages("foreach")
    install.packages("R6")
    install.packages("jsonlite")
    ```
    
1. Download and install the latest version of `CompatibilityAPI` and `RevoScaleR` packages:

    ```r
    install.packages("https://aka.ms/sqlml/r4.2/windows/CompatibilityAPI_1.1.0.zip", repos=NULL)

    install.packages("https://aka.ms/sqlml/r4.2/windows/RevoScaleR_10.0.1.zip", repos=NULL)
    ```

#### Configure R runtime with SQL Server

1. Configure the installed R runtime with SQL Server. You can change the default version by using the `RegisterRext.exe` command-line utility. The utility is in an R application folder that depends on the installation. Usually, it's in `%ProgramFiles%\R\R-4.2.0\library\RevoScaleR\rxLibs\x64`.

   You can use the following script to configure the installed R runtime from the installation folder location of `RegisterRext.exe`. The instance name is `MSSQLSERVER` for a default instance of SQL Server, or the instance name for a named instance of SQL Server.

    ```cmd
    .\RegisterRext.exe /configure /rhome:"%ProgramFiles%\R\R-4.2.0" /instance:"MSSQLSERVER"
    ```

1. By using [SQL Server Management Studio (SSMS)](../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md), connect to the instance where you installed SQL Server Machine Learning Services. Select **New Query** to open a query window, and then run the following command to enable the external scripting feature:

    ```sql
    EXEC sp_configure  'external scripts enabled', 1;
    RECONFIGURE WITH OVERRIDE
    ```

    If you've already enabled the feature for another language, you don't need to run `RECONFIGURE` a second time for R. The underlying extensibility platform supports both languages. To verify, confirm that the following command returns `1` for `config_value` and `run_value`:

    ```sql
    EXEC sp_configure  'external scripts enabled';
    ```

1. Restart the SQL Server service. Restarting the service also automatically restarts the related [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service. 

   You can restart the service by using the right-click **Restart** command for the instance in SSMS Object Explorer, or by using the **Services** item in Control Panel, or by using [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).

1. Verify the installation by running a simple T-SQL command to return the version of R:

    ```sql
    EXEC sp_execute_external_script @script=N'print(R.version)',@language=N'R';
    GO
    ```

### Setup Python support

#### Install Python runtime

1. Download the most recent version of [Python 3.10 for Windows](https://www.python.org/downloads/). Install it by using the following options:
    
    1. Open the Python Setup application and select **Customize installation**. 
    1. Verify that the **Install launcher for all users (recommended)** checkbox is selected.
    1. For **Optional Features**, select the features that you want (or select them all).
    1. On the **Advanced Options** page, select **Install for all users**, accept other default options, and then select **Install**. 
    
       We recommend using a Python installation path that all users can access (such as `C:\Program Files\Python310`), and not one that's specific to a single user.

1. Download and install the latest version of the `revoscalepy` package and its dependencies:

    ```cmd
    cd "C:\Program Files\Python310\"
    python -m pip install https://aka.ms/sqlml/python3.10/windows/revoscalepy-10.0.1-py3-none-any.whl
    ```

#### Configure Python runtime with SQL Server

1. Configure the installed Python runtime with SQL Server. You can change the default version by using the `RegisterRext.exe` command-line utility. The utility is in the custom installation location (for example, `C:\Program Files\Python310\Lib\site-packages\revoscalepy\rxLibs`).

    You can use the following script to configure the installed Python runtime from the installation folder location of `RegisterRext.exe`. The instance name is `MSSQLSERVER` for a default instance of SQL Server, or the instance name for a named instance of SQL Server.
    
    ```cmd
    cd "C:\Program Files\Python310\Lib\site-packages\revoscalepy\rxLibs"
    .\RegisterRext.exe /configure /pythonhome:"C:\Program Files\Python310" /instance:"MSSQLSERVER"
    ```

1. Use [SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md) to connect to the instance where you installed SQL Server Machine Learning Services. Select **New Query** to open a query window, and then run the following command to enable the external scripting feature:

    ```sql
    EXEC sp_configure  'external scripts enabled', 1;
    RECONFIGURE WITH OVERRIDE
    ```

    If you've already enabled the feature for another language, you don't need to run `RECONFIGURE` a second time for R. The underlying extensibility platform supports both languages. To verify, confirm that the following command returns `1` for `config_value` and `run_value`:

    ```sql
    EXEC sp_configure  'external scripts enabled';
    ```

1. Restart the SQL Server service. Restarting the service also automatically restarts the related [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service. 

   You can restart the service by using the right-click **Restart** command for the instance in SSMS Object Explorer, or by using the **Services** item in Control Panel, or by using [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md).
   
1. Verify the installation by running a simple command to return the version of Python:

    ```sql
    EXEC sp_execute_external_script @script=N'import sys;print(sys.version)',@language=N'Python'
    GO
    ```

### Install Java

For information on installing and using Java, see [Install SQL Server Java Language Extension on Windows](../../language-extensions/install/windows-java.md).

## Additional configuration

If the external script verification step was successful, you can run R or Python commands from SQL Server Management Studio, Visual Studio Code, or any other client that can send T-SQL statements to the server.

Whether the additional configuration is required depends on your security schema, where you installed SQL Server, and how you expect users to connect to the database and run external scripts.

If you got an error when you ran the command, you might need to make additional configurations to the service or database. At the instance level, additional configurations might include:

* [Configure a firewall for SQL Server Machine Learning Services](../../machine-learning/security/firewall-configuration.md)
* [Enable additional network protocols](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md)
* [Enable remote connections](../../database-engine/configure-windows/configure-the-remote-access-server-configuration-option.md)
* [Create a login for SQLRUserGroup](../../machine-learning/security/create-a-login-for-sqlrusergroup.md)
* [Manage disk quotas](/windows/desktop/fileio/managing-disk-quotas) to prevent external scripts from running tasks that exhaust disk space

Starting with SQL Server 2019 on Windows, the isolation mechanism has changed. This mechanism affects *SQLRUserGroup*, firewall rules, file permission, and implied authentication. For more information, see [Isolation changes for Machine Learning Services](sql-server-machine-learning-services-2019.md).

<a name="bkmk_configureAccounts"></a> 
<a name="permissions-external-script"></a> 

On the database, you might need configuration updates. For more information, see [Give users permission to SQL Server Machine Learning Services](../../machine-learning/security/user-permission.md).

## Suggested optimizations

Now that you have everything working, you might also want to optimize the server to support machine learning or install a pre-trained machine learning model.

### Optimize the server for script execution

The default settings for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] setup are intended to optimize the balance of the server for a variety of other services and applications.

Under the default settings, resources for machine learning are sometimes restricted or throttled, particularly in memory-intensive operations.

To ensure that machine learning jobs are prioritized and resourced appropriately, we recommend that you use SQL Server Resource Governor to configure an external resource pool. You might also want to change the amount of memory that's allocated to the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database engine, or increase the number of accounts that run under the [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] service.

- To configure a resource pool for managing external resources, see [Create an external resource pool](../../t-sql/statements/create-external-resource-pool-transact-sql.md).
  
- To change the amount of memory reserved for the database, see [Server memory configuration options](../../database-engine/configure-windows/server-memory-server-configuration-options.md).
  
- To change the number of R accounts that [!INCLUDE[rsql_launchpad](../../includes/rsql-launchpad-md.md)] can start, see [Scale concurrent execution of external scripts in SQL Server Machine Learning Services](../administration/scale-concurrent-execution-external-scripts.md).

If you're using Standard Edition and don't have Resource Governor, you can use dynamic management views, SQL Server Extended Events, and Windows event monitoring to help manage the server resources.

### Install additional Python and R packages

The Python and R solutions that you create for SQL Server can call:

- Basic functions.
- Functions from the proprietary packages installed with SQL Server.
- Third-party packages that are compatible with the version of open-source Python and R that SQL Server installs.

Packages that you want to use from SQL Server must be installed in the default library that the instance uses. If you have a separate installation of Python or R on the computer, or if you installed packages to user libraries, you can't use those packages from T-SQL.

To install and manage additional packages, you can set up user groups to share packages on a per-database level. Or you can configure database roles to enable users to install their own packages. For more information, see [Install Python packages](../package-management/install-additional-python-packages-on-sql-server.md) and [Install new R packages](../package-management/install-additional-r-packages-on-sql-server.md).

### Standalone RevoScale packages for Python and R runtime

RevoScale packages are also supported as a standalone package with Python and R runtimes. In order to setup Python or R runtime for the standalone scenario, follow the instructions in the [Install Python runtime](#install-python-runtime) and [Install R runtime](#install-r-runtime) sections respectively.

## Next steps

Python developers can learn how to use Python with SQL Server by following these tutorials:

- [Python tutorial: Predict ski rental with linear regression in SQL Server Machine Learning Services](../tutorials/python-ski-rental-linear-regression-deploy-model.md)
- [Python tutorial: Categorize customers by using k-means clustering with SQL Server Machine Learning Services](../tutorials/python-clustering-model.md)

R developers can get started with some simple examples and learn the basics of how R works with SQL Server. For your next step, see the following links:

- [Quickstart: Run R in T-SQL](../tutorials/quickstart-r-create-script.md)
- [Tutorial: In-database analytics for R developers](../tutorials/r-taxi-classification-introduction.md)
