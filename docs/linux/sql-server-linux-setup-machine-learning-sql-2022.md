---
title: Install on SQL Server 2022 for Linux
titleSuffix: SQL Server Machine Learning Services
description: "Learn how to install SQL Server 2022 Machine Learning Services on Linux: Red Hat, Ubuntu, and SUSE."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 09/26/2022
ms.topic: how-to
ms.service: sql
ms.subservice: machine-learning-services
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16"
ms.custom:
- intro-installation
- event-tier1-build-2022
---
# Install SQL Server 2022 Machine Learning Services (Python and R) on Linux

[!INCLUDE [SQL Server 2022 - Linux](../includes/applies-to-version/sqlserver2022-linux.md)]

This article guides you in the installation of [SQL Server Machine Learning Services](../machine-learning/sql-server-machine-learning-services.md) on Linux. Python and R scripts can be executed in-database using Machine Learning Services.

You can install Machine Learning Services on Ubuntu and Red Hat Enterprise Linux (RHEL). Currently, SUSE Linux Enterprise Server (SLES) is unsupported. 

You can install ML Services on a Docker container running a Linux distribution. Inside the Docker container, the steps would be the same as below.

For more information, see [the Supported platforms section in the installation guidance for SQL Server on Linux](sql-server-linux-setup.md#supportedplatforms).

> [!IMPORTANT]
> This article refers to [!INCLUDE[sssql22-md](../includes/sssql22-md.md)]. For SQL Server 2019 on Linux, see to [Install SQL Server 2019 Machine Learning Services (Python and R) on Linux](sql-server-linux-setup-machine-learning.md). For SQL Server on Windows, see [Install SQL Server 2022 Machine Learning Services (Python and R) on Windows](../machine-learning/install/sql-machine-learning-services-windows-install-sql-2022.md).

## Pre-install checklist

* [Install SQL Server on Linux](sql-server-linux-setup.md) and verify the installation.

* Check the SQL Server Linux repositories for the Python and R extensions. 
  If you already configured source repositories for the database engine install, you can run the **mssql-server-extensibility** package install commands using the same repo registration.

* You should have a tool for running T-SQL commands. 

  * You can use [Azure Data Studio](../azure-data-studio/download-azure-data-studio.md), a free database tool that runs on Linux, Windows, and macOS.

* Restarting the SQL Server instance during this installation process will be required.

## Package list

On an internet-connected device, packages are downloaded and installed independently of the database engine using the package installer for each operating system.

Available installation packages for [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] on Linux:

| Package name | Applies-to | Description |
|--------------|----------|-------------|
|mssql-server-extensibility  | All | Extensibility framework used to run Python and R. |

## Install mssql-server-extensibility package

1. [Configure Linux Repositories](sql-server-linux-change-repo.md) corresponding to the Linux distribution. Install the SQL Server extensibility feature with the package `mssql-server-extensibility`.

    **Ubuntu**
    ```bash
    apt-get install mssql-server-extensibility
    ```

    **RHEL**
    ```bash
    yum install mssql-server-extensibility
    ```

1. Review and accept the End User License Agreement (EULA) for SQL Server ML Services.

    ```bash
    sudo /opt/mssql/bin/mssql-conf set EULA accepteulaml Y
    ```

    To complete acceptance of the EULA, the SQL Server instance must be restarted. 

    ```bash
    sudo systemctl restart mssql-server
    ```

## Setup R support

### Install R runtime

1. Download and install the version of R that is desired. Choose a version of R 4.2 or higher, [available for download directly from cran.r-project.org](https://cran.r-project.org/). Follow the instructions for the desired runtime.

1. Install dependencies for `CompatibilityAPI` and `RevoScaleR`. From an admin R terminal of the version you have installed, run the following:

    ```r
    # R Terminal
    install.packages("iterators", lib="/usr/lib/R/library")
    install.packages("foreach", lib="/usr/lib/R/library")
    install.packages("R6", lib="/usr/lib/R/library")
    install.packages("jsonlite", lib="/usr/lib/R/library")
    ```

1. Download and install `CompatibilityAPI` and `RevoScaleR` for Linux.

    ```r
    install.packages("https://aka.ms/sqlml/r4.2/linux/CompatibilityAPI_1.1.0_R_x86_64-pc-linux-gnu.tar.gz", repos=NULL, lib="/usr/lib/R/library")
    install.packages("https://aka.ms/sqlml/r4.2/linux/RevoScaleR_10.0.1_R_x86_64-pc-linux-gnu.tar.gz", repos=NULL, lib="/usr/lib/R/library")
    ```

1. Verify `RevoScaleR` installation from the R terminal.

    ```r
    library("RevoScaleR")
    ```

### Configure R runtime with SQL Server

1. Configure the installed R runtime with SQL Server for Linux, where `path/to/` is the file path to the R binary, and `RFolderVersion` is the version-specific folder name for your installation of R runtime, for example, `R4.2`.

    ```bash  
    /opt/mssql/bin/mssql-conf set extensibility rbinpath /usr/lib/R/bin/R
    /opt/mssql/bin/mssql-conf set extensibility datadirectories /usr/lib/R
    ```

1. Restart the `Launchpadd` service.

    ```bash
    systemctl restart mssql-launchpadd.service
    ```

1. Configure SQL Server for Linux to allow external scripts using the `sp_configure` system stored procedure.

    ```sql
    EXEC sp_configure 'external scripts enabled', 1;
    GO
    RECONFIGURE
    GO
    ```

1. Verify the installation by executing a simple T-SQL command to return the version of R:

    ```sql
    EXEC sp_execute_external_script @script=N'print(R.version)',@language=N'R';
    GO
    ```

## Setup Python support

### Install Python runtime

1. Download and install the version of Python that is desired. Choose a version of Python 3.10 or higher, [available for download directly from python.org](https://docs.python.org/3/using/unix.html). Follow the instructions for the desired runtime. Also, install the shared python runtime library for the desired runtime version. For example, to install `libpython3.10` for Ubuntu: `sudo apt-get install libpython3.10`.

1. Download and install `revoscalepy` for the root user.

    ```bash  
    sudo pip install https://aka.ms/sqlml/python3.10/linux/revoscalepy-10.0.1-py3-none-any.whl --target=/usr/lib/python3.10/dist-packages
    ```

1. Verify the `revoscalepy` installation from the Python terminal. Verify the library can be imported.

    ```python
    import revoscalepy
    ```

### Configure Python runtime with SQL Server

1. Configure the installed Python runtime with SQL Server, where `pythonbinbath` is set to the path of the installed python binary, and `datadirectories` includes the path where the packages are installed for the desired version of python, for example, `/usr/lib/python3.10/dist-packages`. Use the following script with your actual installation path:

    ```bash
    /opt/mssql/bin/mssql-conf set extensibility pythonbinpath /usr/bin/python3.10
    /opt/mssql/bin/mssql-conf set extensibility datadirectories /usr/lib:/usr/lib/python3.10/dist-packages
    ```

1. Restart the `Launchpadd` service.

    ```bash
    systemctl restart mssql-launchpadd.service
    ```
   
1. Configure SQL Server for Linux to allow external scripts using the `sp_configure` system stored procedure.

    ```sql
    EXEC sp_configure 'external scripts enabled', 1;
    GO
    RECONFIGURE
    GO
    ```

1. Verify the installation by executing a simple T-SQL command to return the version of python:

    ```sql
    EXEC sp_execute_external_script @script=N'import sys;print(sys.version)',@language=N'Python';
    GO
    ```

## Install Java

To install the Java language extension, see [Install SQL Server Java Language Extension on Linux](sql-server-linux-setup-language-extensions-java.md).


## Verify installation

To validate installation, use any of the following methods:

* Run a T-SQL script that executes a system stored procedure invoking Python or R using a query tool. 

* Execute the following SQL command to test R execution in SQL Server. Errors? Try a service restart, `sudo systemctl restart mssql-server.service`.
  ```sql
  EXEC sp_execute_external_script   
  @language =N'R', 
  @script=N' 
  OutputDataSet <- InputDataSet', 
  @input_data_1 =N'SELECT 1 AS hello' 
  WITH RESULT SETS (([hello] int not null)); 
  GO 
  ```
 
* Execute the following SQL command to test Python execution in SQL Server. 
 
  ```sql
  EXEC sp_execute_external_script  
  @language =N'Python', 
  @script=N' 
  OutputDataSet = InputDataSet; 
  ', 
  @input_data_1 =N'SELECT 1 AS hello' 
  WITH RESULT SETS (([hello] int not null)); 
  GO 
  ```

## Offline installation

Follow the [Offline installation](sql-server-linux-setup.md#offline) instructions for steps on installing the packages. Find your download site, and then download specific packages using the package list below.

> [!Tip]
> Several of the package management tools provide commands that can help you determine package dependencies. For yum, use `sudo yum deplist [package]`. For Ubuntu, use `sudo apt-get install --reinstall --download-only [package name]` followed by `dpkg -I [package name].deb`.

## Standalone RevoScale packages for Python and R runtime

RevoScale packages are also supported as a standalone package with Python and R runtimes. In order to setup Python or R runtime for the standalone scenario, follow the instructions in the [Install Python runtime](#install-python-runtime) and [Install R runtime](#install-r-runtime) sections respectively.

## Next steps

Python developers can learn how to use Python with SQL Server by following these tutorials:

- [Python tutorial: Predict ski rental with linear regression in SQL Server Machine Learning Services](../machine-learning/tutorials/python-ski-rental-linear-regression-deploy-model.md)
- [Python tutorial: Categorizing customers using k-means clustering with SQL Server Machine Learning Services](../machine-learning/tutorials/python-clustering-model.md)

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

- [Quickstart: Run R in T-SQL](../machine-learning/tutorials/quickstart-r-create-script.md)
- [Tutorial: In-database analytics for R developers](../machine-learning/tutorials/r-taxi-classification-introduction.md)
