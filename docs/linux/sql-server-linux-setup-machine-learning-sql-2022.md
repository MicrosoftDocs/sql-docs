---
title: Install on SQL Server 2022 for Linux
titleSuffix: SQL Server Machine Learning Services
description: "Learn how to install SQL Server 2022 Machine Learning Services on Linux: Red Hat, Ubuntu, and SUSE."
author: WilliamDAssafMSFT
ms.author: wiassaf
manager: rothja
ms.date: 05/24/2022
ms.topic: how-to
ms.prod: sql
ms.technology: machine-learning-services
monikerRange: ">=sql-server-ver16||>=sql-server-linux-ver16"
ms.custom:
- intro-installation
- event-tier1-build-2022
---
# Install SQL Server 2022 Machine Learning Services (Python and R) on Linux

[!INCLUDE [SQL Server 2022 - Linux](../includes/applies-to-version/sqlserver2022-linux.md)]

This article guides you in the installation of [SQL Server Machine Learning Services](../machine-learning//sql-server-machine-learning-services.md) on Linux. Python and R scripts can be executed in-database using Machine Learning Services.

You can install Machine Learning Services on Ubuntu. Currently, Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES) are unsupported. 

You can install ML Services on a Docker container running a Linux distribution. Inside the Docker container, the steps would be the same as below.

For more information, see [the Supported platforms section in the installation guidance for SQL Server on Linux](sql-server-linux-setup.md#supportedplatforms).

> [!IMPORTANT]
> This article refers to [!INCLUDE[sssql22-md](../includes/sssql22-md.md)]. For SQL Server 2019 on Linux, see to [Install SQL Server 2019 Machine Learning Services (Python and R) on Linux](sql-server-linux-setup-machine-learning.md).

## Pre-install checklist

* [Install SQL Server on Linux](sql-server-linux-setup.md) and verify the installation.

* Check the SQL Server Linux repositories for the Python and R extensions. 
  If you already configured source repositories for the database engine install, you can run the **mssql-server-extensibility** package install commands using the same repo registration.

* You should have a tool for running T-SQL commands. 

  * You can use [Azure Data Studio](../azure-data-studio/download-azure-data-studio.md), a free database tool that runs on Linux, Windows, and macOS.

## Package list

On an internet-connected device, packages are downloaded and installed independently of the database engine using the package installer for each operating system. The following table describes all available packages, but for R and Python, you specify packages that provide either the full feature installation or the minimum feature installation.

Available installation packages for [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] on Linux:

| Package name | Applies-to | Description |
|--------------|----------|-------------|
|mssql-server-extensibility  | All | Extensibility framework used to run Python and R. |


<a name="ubuntu"></a>

## Install on Ubuntu

Refer to [Quickstart: Install SQL Server and create a database on Ubuntu](quickstart-install-connect-ubuntu.md) for [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] on Linux installation. Then, use the following steps to install SQL Server Machine Learning Services on Ubuntu:

 - [Install R on Ubuntu](#install-r-on-ubuntu)
 - [Install Python on Ubuntu](#install-python-on-ubuntu)

### Install R on Ubuntu

The following commands register the repository providing the R language platform. 

> [!Tip]
> If possible, run `apt-get update` to refresh packages on the system prior to installation. 

1. Begin installation as root.

    ```bash
    sudo su
    ```

2. Optionally, if your system does not have the `https apt transport` option:

    ```bash
    apt-get install apt-transport-https
    ```

3. Set the location of the package repo the "prod" directory containing the distribution. This example specifies 20.04. Replace with 16.04 or 14.04 if you want those versions.

    ```bash
    wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb
    ```

4. Register the repository.

    ```bash
    dpkg -i packages-microsoft-prod.deb
    ```

5. Update packages on your system (required).

    ```bash
    apt-get update
    ```

6. Install the SQL Server extensibility feature.

    ```bash
    apt-get install mssql-server-extensibility
    ```

7. Download and install the version of R that is desired. Choose a version of R 4.2 or higher, [available for download directly from cran.r-project.org](https://cran.r-project.org/). Follow the instructions for the desired runtime.

8. Install CompatibilityAPI and RevoScaleR dependencies. From the R terminal of the version you have installed, run the following:

    ```r
    # R Terminal
    install.packages("iterators")
    install.packages("foreach")
    install.packages("R6")
    install.packages("jsonlite")
    ```

11. Download the [CompatibilityAPI for Linux](https://go.microsoft.com/fwlink/?LinkID=2193925).

12. Install CompatibilityAPI for Linux, specifying the absolute file path to the `.tar.gz` file.

    ```r
    install.packages('/path/to/CompatibilityAPI.tar.gz', repos = NULL)
    ```

13. Download [RevoScaleR Linux](https://go.microsoft.com/fwlink/?LinkID=2193829).

14. Install RevoScaleR for Linux, specifying the absolute file path to the `.tar.gz` file.

    ```r
    install.packages('/path/to/RevoScaleR.tar.gz', repos = NULL)
    ```

15. Verify RevoScaleR installation from the R terminal.

    ```r
    library("RevoScaleR")
    ```

16. Configure the installed R runtime with SQL Server for Linux, where `path/to/` is the file path to the R binary, and `RFolderVersion` is the version-specific folder name for your installation of R runtime, for example, `R4.2`.


    ```bash  
    /opt/mssql/bin/mssql-conf set extensibility rbinpath /path/to/RFolderVersion/lib/R/bin/R
    /opt/mssql/bin/mssql-conf set extensibility datadirectories /path/to/RFolderVersion/
    ```

17. Restart the Launchpad service.

    ```bash
    systemctl restart mssql-launchpadd.service
    ```

18. Configure SQL Server for Linux to allow external scripts using the `sp_configure` system stored procedure.

    ```sql
    EXEC sp_configure 'external scripts enabled', 1;
    GO
    RECONFIGURE
    GO
    ```

19. Verify the installation by executing a simple T-SQL command to return the version of R:

    ```sql
    EXEC sp_execute_external_script @script=N'print(R.version)',@language=N'R';
    GO
    ```

### Install Python on Ubuntu

1. Begin installation as root.

    ```bash
    sudo su
    ```
    
2. Update packages on your system (required).

    ```bash
    apt-get update
    ```

3. Install the SQL Server extensibility feature.

    ```bash
    apt-get install mssql-server-extensibility
    ```

4. Download and install the version of Python that is desired. Choose a version of Python 3.10 or higher, [available for download directly from python.org](https://docs.python.org/3/using/unix.html). Follow the instructions for the desired runtime.

5. Download [revoscalepy for Linux](https://go.microsoft.com/fwlink/?LinkID=2193830).

6. Install revoscalepy for the root user, specifying the absolute file path to the `.whl` file.

    ```bash  
    pip install /path/to/revoscalepy.whl
    ```

7. Verify the revoscalepy installation from the python terminal. Verify the library can be imported.

    ```python
    import revoscalepy
    ```

8. Configure the installed Python runtime with SQL Server, where `/path/to` is the path to the python installation binary, and `pythonFolderVersion` is the desired version of Python installed, for example, `python3.10`. Use the following script with your actual installation path:

    ```bash
    /opt/mssql/bin/mssql-conf set extensibility pythonbinpath /path/to/pythonFolderVersion
    /opt/mssql/bin/mssql-conf set extensibility datadirectories /path/to/:/path/to/lib/pythonFolderVersion/dist-packages/
    ```

9. Restart the Launchpad service.

    ```bash
    systemctl restart mssql-launchpadd.service
    ```
    
10. Configure SQL Server for Linux to allow external scripts using the `sp_configure` system stored procedure.

    ```sql
    EXEC sp_configure 'external scripts enabled', 1;
    GO
    RECONFIGURE
    GO
    ```

11. Verify the installation by executing a simple T-SQL command to return the version of python:

    ```sql
    EXEC sp_execute_external_script @script=N'import sys;print(sys.version)',@language=N'Python';
    GO
    ```

<a name="RHEL"></a>

## Install on RHEL

Installation for [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] Machine Learning Services for Red Hat Enterprise Linux (RHEL) is currently not supported.

<a name="SLES"></a>

## Install on SUSE Linux

Installation for [!INCLUDE [sssql22-md](../includes/sssql22-md.md)] Machine Learning Services for SUSE Linux Enterprise Server (SLES) v15 is currently not supported.

## Install Java

To install the Java language extension, see [Install SQL Server Java Language Extension on Linux](sql-server-linux-setup-language-extensions-java.md).


## Verify installation

To validate installation:

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

## Next steps

Python developers can learn how to use Python with SQL Server by following these tutorials:

+ [Python tutorial: Predict ski rental with linear regression in SQL Server Machine Learning Services](../machine-learning/tutorials/python-ski-rental-linear-regression-deploy-model.md)
+ [Python tutorial: Categorizing customers using k-means clustering with SQL Server Machine Learning Services](../machine-learning/tutorials/python-clustering-model.md)

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

+ [Quickstart: Run R in T-SQL](../machine-learning/tutorials/quickstart-r-create-script.md)
+ [Tutorial: In-database analytics for R developers](../machine-learning/tutorials/r-taxi-classification-introduction.md)
