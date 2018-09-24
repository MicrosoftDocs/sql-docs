---
title: Install SQL Server Machine Learning Services (R, Python, Java) on Linux | Microsoft Docs
description: This article describes how to install SQL Server Machine Learning Services (R, Python, Java) on Red Hat and Ubuntu.
author: HeidiSteen
ms.author: heidist
manager: cgronlun
ms.date: 09/24/2018
ms.topic: conceptual
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux"
ms.technology: machine-learning
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Install SQL Server 2019 Machine Learning Services (R, Python, Java) on Linux

[SQL Server Machine Learning Services](../advanced-analytics/what-is-sql-server-machine-learning.md) runs on Linux operating systems starting in this preview release of SQL Server 2019. Follow the steps in this article to install the Java programming extension, or the machine learning extensions for R and Python. 

Machine learning and programming extensions are an add-on to the database engine. Although you can [install the database engine and Machine Learning Services concurrently](#install-all), it's a best practice to install and configure the SQL Server database engine first so that you can resolve any issues before adding more components. 

Package location of the R, Python, and Java extensions are in the SQL Server Linux source repositories. If you already configured source repositories for the database engine install, you can run the mssql-mlservices package install commands using the same repo registration.

## Prerequisites

+ Linux operating system must be [supported by SQL Server](sql-server-linux-release-notes-2019.md#supported-platforms), running on premises or in a Docker container.

+ You must have a SQL Server 2019 Database Engine instance on: 

   + [Red Hat Enterprise Linux (RHEL)](quickstart-install-connect-red-hat.md)

   + [SUSE Enterprise Linux Server](quickstart-install-connect-suse.md)

   + [Ubuntu](quickstart-install-connect-ubuntu.md)

+ For R only, [Microsoft R Open](#mro) for mssql-mlsservices R packages. 

<a name="mro"></a>

### Microsoft R Open (MRO)

Microsoft's base distribution of R is a prerequisite for using RevoScaleR, MicrosoftML, and other R packages installed with Machine Learning Services.

The following commands register the repository providing MRO. Post-registration, the commands for installing other R packages will automatically include MRO as a package dependency.

#### On Ubuntu

```bash
# Set the location of the package repo the "prod" directory containing the distribution.
# This example specifies 16.04. Replace with 18.04 if you want that version
wget https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb

# Register the repo
dpkg -i packages-microsoft-prod.deb
```

#### On RHEL

```bash
# Set the location of the package repo at the "prod" directory
rpm -Uvh https://packages.microsoft.com/config/rhel/7/packages-microsoft-prod.rpm
```
#### On SUSE

```bash
# Set the location of the package repo
zypper ar -f https://packages.microsoft.com/sles/12/prod packages-microsoft-com
```

## Package list

On an internet-connected device, packages are downloaded and installed independently of the database engine using the package installer for each operating system. The following table describes all available packages, but for internet-connected installs, you only need *one* R or Python package to get a specific combination of features.

| Package name | Applies-to | Description |
|--------------|----------|-------------|
|mssql-server-extensibility  | All | Extensibility framework used to run R, Python, or Java code. |
|mssql-server-extensibility-java | Java | Java extension for loading a Java execution environment. There are no additional libraries or packages for Java. |
| microsoft-openmpi  | Python, R | Message passing interface used by the Revo* libraries for parallelization on Linux. |
| microsoft-r-open | R | Open-source distribution of R. |
| mssql-mlservices-python | Python | Open-source distribution of Anaconda and Python. |
|mssql-mlservices-mlm-py  | Python | Full install. Provides revoscalepy, microsoftml, pre-trained models for image featurization and text sentiment analysis.| 
|mssql-mlservices-mml-py  | Python | Partial install. Provides revoscalepy, microsoftml. <br/>Excludes pre-trained models. | 
|mssql-mlservices-packages-py  | Python | Partial install. Provides revoscalepy. <br/>Excludes pre-trained models and microsoftml. | 
|mssql-mlservices-mlm-r  | R | Full install. Provides RevoScaleR, MicrosoftML, sqlRUtils, olapR, pre-trained models for image featurization and text sentiment analysis.| 
|mssql-mlservices-mml-r  | R | Partial install. Provides RevoScaleR, MicrosoftML, sqlRUtils, olapR. <br/>Excludes pre-trained models.  |
|mssql-mlservices-packages-r  | R | Partial install. Provides RevoScaleR, sqlRUtils, olapR. <br/>Excludes  pre-trained models and MicrosoftML. | 

<a name="RHEL"></a>

## RHEL commands

Install any *one* R package, plus any *one* Python package, and Java if you want that capability. Each R and Python package includes a bundle of features. Choose the package that provides the feature set you need. Dependent packages are included automatically.

> [!Tip]
> If possible, run `yum clean all` to refresh packages on the system prior to installation.

### Example 1 -  Full installation 

Includes open-source R and Python, extensibility framework, microsoft-openmpi, extensions (R, Python, Java), with machine learning libraries and pre-trained models for R and Python. For R and Python, if you want something in between full and minimum install - such as machine learning libraries but without the pre-trained models - substitute `mssql-mlservices-mml-r-9.4.5*` and `mssql-mlservices-mml-py-9.4.5*` instead.

```bash
# Install as root or sudo
# Add everything (all R, Python, Java)
# Be sure to include -9.4.5* in mlsservices package names
sudo yum install mssql-mlservices-mlm-py-9.4.5*
sudo yum install mssql-mlservices-mlm-r-9.4.5* 
sudo yum install mssql-server-extensibility-java
```

### Example 2 - Minimum installation 

Includes open-source R and Python, extensibility framework, microsoft-openmpi, core Revo* libraries for R and Python, Java extension. Excludes pre-trained models and machine learning libraries for R and Python. 

```bash
# Install as root or sudo
# Minimum install of R, Python, Java extensions
# Be sure to include -9.4.5* in mlsservices package names
sudo yum install mssql-mlservices-packages-py-9.4.5*
sudo yum install mssql-mlservices-packages-r-9.4.5*
sudo yum install mssql-server-extensibility-java
```

<a name="ubuntu"></a>

## Ubuntu commands

Install any *one* R package, plus any *one* Python package, and Java if you want that capability. Each R and Python package includes a bundle of features. Choose the package that provides the feature set you need. Dependent packages are included automatically.

> [!Tip]
> If possible, run `apt-get update` to refresh packages on the system prior to installation. Additionally, some docker images of Ubuntu might not have the https apt transport option. To install it, use `apt-get install apt-transport-https`.

### Prerequisite for 18.04

Running mssql-mlservices R libraries on Ubuntu 18.04 requires **libpng12** from the Linux Kernel archives. This package is no longer included in the standard distribution and must be installed manually. To get this library, run the following commands:

```bash
wget https://mirrors.kernel.org/ubuntu/pool/main/libp/libpng/libpng12-0_1.2.54-1ubuntu1_amd64.deb
dpkg -i libpng12-01_1.2.54-1ubuntu1_amd64.deb
```

### Example 1 -  Full installation 

Includes open-source R and Python, extensibility framework, microsoft-openmpi, extensions (R, Python, Java), with machine learning libraries and pre-trained models for R and Python. For R and Python, if you want something in between full and minimum install - such as machine learning libraries but without the pre-trained models - substitute mssql-mlservices-mml-r and mssql-mlservices-mml-py instead.

```bash
# Install as root or sudo
# Add everything (all R, Python, Java)
# There is no asterisk in this full install
sudo apt-get install mssql-mlservices-mlm-py 
sudo apt-get install mssql-mlservices-mlm-r 
sudo apt-get install mssql-server-extensibility-java
```

### Example 2 - Minimum installation 

Includes open-source R and Python, extensibility framework, microsoft-openmpi, core Revo* libraries for R and Python, Java extension. Excludes pre-trained models and machine learning libraries for R and Python. 

```bash
# Install as root or sudo
# Minimum install of R, Python, Java
# No aasterisk
sudo apt-get install mssql-mlservices-packages-py
sudo apt-get install mssql-mlservices-packages-r
sudo apt-get install mssql-server-extensibility-java
```

<a name="suse"></a>

## SUSE commands

Install any *one* R package, plus any *one* Python package, and Java if you want that capability. Each R and Python package includes a bundle of features. Choose the package that provides the feature set you need. Dependent packages are included automatically. 

### Example 1 -  Full installation 

Includes open-source R and Python, extensibility framework, microsoft-openmpi, extensions (R, Python, Java), with machine learning libraries and pre-trained models for R and Python. For R and Python, if you want something in between full and minimum install - such as machine learning libraries but without the pre-trained models - substitute `mssql-mlservices-mml-r-9.4.5*` and `mssql-mlservices-mml-py-9.4.5*` instead.

```bash
# Install as root or sudo
# Add everything (all R, Python, Java)
# Be sure to include -9.4.5* in mlsservices package names
sudo zypper install mssql-mlservices-mlm-py-9.4.5*
sudo zypper install mssql-mlservices-mlm-r-9.4.5* 
sudo zypper install mssql-server-extensibility-java
```

### Example 2 - Minimum installation 

Includes open-source R and Python, extensibility framework, microsoft-openmpi, core Revo* libraries for R and Python, Java extension. Excludes pre-trained models and machine learning libraries for R and Python. 

```bash
# Install as root or sudo
# Minimum install of R, Python, Java extensions
# Be sure to include -9.4.5* in mlsservices package names
sudo zypper install mssql-mlservices-packages-py-9.4.5*
sudo zypper install mssql-mlservices-packages-r-9.4.5*
sudo zypper install mssql-server-extensibility-java
```

## Post-install config (required)

Additional configuration is primarily through the [mssql-conf tool](sql-server-linux-configure-mssql-conf.md).


1. Add the mssql user account used to run the SQL Server Launchpad service.

  ```bash
  sudo /opt/mssql/bin/mssql-conf setup
  ```

2. Accept the licensing agreements for open-source R and Python. There are several ways to do this. If you previously accepted SQL Server licensing and are now adding the R or Python extensions, the following command is your consent to their terms:

  ```bash
  # Run as SUDO or root
  # Use set + EULA 
    sudo /opt/mssql/bin/mssql-conf set EULA accepteulaml Y
  ```

  An alternative workflow is that if you have not yet accepted the SQL Server database engine licensing agreement, setup detects the mssql-mlservices packages and prompts for EULA acceptance when `mssql-conf setup` is run. For more information about EULA parameters, see [Configure SQL Server with the mssql-conf tool](sql-server-linux-configure-mssql-conf.md#mlservices-eula).

3. Restart the SQL Server Launchpad service and the database engine instance.

  ```bash
  systemctl restart mssql-launchpadd

  systemctl restart mssql-server.service
  ```

4. Enable external script execution in SQL Server Management Studio or another tool that runs Transact-SQL. 

  ```bash
  EXEC sp_configure 'external scripts enabled', 1 
  RECONFIGURE WITH OVERRIDE 
  ```

## Verify installation

R libraries (MicrosoftML, RevoScaleR, and others) can be found at `/opt/mssql/mlservices/libraries/RServer`.

Python libraries (microsoftml and revoscalepy) can be found at `/opt/mssql/mlservices/libraries/PythonServer`.

Using a SQL Server query tool, execute the following SQL command to test R execution in SQL Server. If the script does not run, try a service restart, `sudo systemctl restart mssql-server`.

```r
EXEC sp_execute_external_script   
@language =N'R', 
@script=N' 
OutputDataSet <- InputDataSet', 
@input_data_1 =N'SELECT 1 AS hello' 
WITH RESULT SETS (([hello] int not null)); 
GO 
```
 
Execute the following SQL command to test Python execution in SQL Server. 
 
```python
EXEC sp_execute_external_script  
@language =N'Python', 
@script=N' 
OutputDataSet = InputDataSet; 
', 
@input_data_1 =N'SELECT 1 AS hello' 
WITH RESULT SETS (([hello] int not null)); 
GO 
```

<a name="install-all"></a>

## Chained installation

You can install and configure the database engine and Machine Learning Services in one procedure by appending R, Python, or Java packages and parameters on a command that installs the database engine. 

The following example is a "template" illustration of what a combined package installation looks like using the Yum package manager:

```bash
sudo yum install -y mssql-sqlserver mssql-server-extensibility-java 
```

The example installs the database engine and adds the Java language extension, which pulls in the extensibility framework package as a dependency. All of the packages used in this example are found at the same path. If you were adding R packages, registration for microsoft-r-open package repository would be required.

Post-installation, remember to use the mssql-conf tool to configure the entire installation and accept licensing agreements. Unaccepted EULAs for open-source R and Python components are detected automatically, and you are prompted to accept them, along with the EULA for SQL Server.

```bash
sudo /opt/mssql/bin/mssql-conf setup MSSQL_PID=Developer 
```

## Unattended installation

Using the [unattended install](https://docs.microsoft.com/sql/linux/sql-server-linux-setup?view=sql-server-2017#unattended) for the Database Engine, add the packages for mssql-mlservices and EULAs.

Recall that Setup or the mssql-conf tool prompts for license agreement acceptance. If you already configured SQL Server database engine and accepted its EULA, use one of the mlservices-specific EULA parameters for the open-source R and Python distributions:

```bash
sudo /opt/mssql/bin/mssql-conf setup accept-eula-ml
```

All possible permutations of EULA acceptance are documented in [Configure SQL Server on Linux with the mssql-conf tool](sql-server-linux-configure-mssql-conf.md#mlservices-eula).

## Offline installation

Follow the [Offline installation](sql-server-linux-setup.md#offline) instructions for steps on installing the packages. Find your download site, and then download specific packages using the package list below.

> [!Tip]
> Several of the package management tools provide commands that can help you determine package dependencies. For yum, use `sudo yum deplist [package]`. For Ubuntu, use `sudo apt-get install --reinstall --download-only [package name]`.
dpkg -I [package name].deb


#### Download site

You can download packages from [https://packages.microsoft.com/](https://packages.microsoft.com/). All of the mlservices packages for R, Python, and Java are co-located with database engine package. Base version for the mlservices packages is 9.4.5. The micrososoft-r-open packages are in a different folder.

#### RHEL/7 paths

|||
|--|----|
| mssql/mlservices packages | [https://packages.microsoft.com/rhel/7/mssql-server-preview/](https://packages.microsoft.com/rhel/7/mssql-server-preview/) |
| microsoft-r-open packages | [https://packages.microsoft.com/rhel/7/prod/](https://packages.microsoft.com/rhel/7/prod/) | 


#### Ubuntu/16.04 paths

|||
|--|----|
| mssql/mlservices packages | [https://packages.microsoft.com/ubuntu/16.04/mssql-server-preview/pool/main/m/](https://packages.microsoft.com/ubuntu/16.04/mssql-server-preview/pool/main/m/) |
| microsoft-r-open packages | [https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/](https://packages.microsoft.com/ubuntu/16.04/prod/pool/main/m/) | 

#### SLES/12 paths

|||
|--|----|
| mssql/mlservices packages | [ https://packages.microsoft.com/sles/12/mssql-server-preview/](https://packages.microsoft.com/sles/12/mssql-server-preview/) |
| microsoft-r-open packages | [https://packages.microsoft.com/sles/12/prod/](https://packages.microsoft.com/sles/12/prod/) | 

#### Package list

Depending on which extensions you want to use, download the packages necessary for a specific language. Exact filenames include platform information, but the file names below should be close enough for you to determine which files to get.

```
# Core packages 
mssql-server-15.0.1000
mssql-server-extensibility-15.0.1000

# Java
mssql-server-extensibility-java-15.0.1000

# R
microsoft-openmpi-3.0.0
microsoft-r-open-foreachiterators-3.4.4
microsoft-r-open-mkl-3.4.4
microsoft-r-open-mro-3.4.4
mssql-mlservices-packages-r-9.4.5
mssql-mlservices-mlm-r-9.4.5
mssql-mlservices-mml-r-9.4.5

# Python
microsoft-openmpi-3.0.0
mssql-mlservices-python-9.4.5
mssql-mlservices-packages-py-9.4.5
mssql-mlservices-mlm-py-9.4.5
mssql-mlservices-mml-py-9.4.5 
```

## Add more R/Python packages 
 
You can install other R and Python packages and use them in script that executes on SQL Server 2019.

### R packages 
 
1. Start an R session.

   ```r
   # sudo /opt/mssql/mlservices/bin/R/R 
   ```

2. Install an R package called [glue](https://mran.microsoft.com/package/glue) to test package installation.

   ```r
   # install.packages("glue",lib="/opt/mssql/mlservices/libraries/RServer") 
   ```
   Alternatively, you can install an R package from the command line 

   ```r
   # sudo /opt/mssql/mlservices/bin/R/R CMD INSTALL -l /opt/mssql/mlservices/libraries/RServer glue_1.1.1.tar.gz 
   ```

3. Import the R package in [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).

   ```r
   EXEC sp_execute_external_script  
   @language = N'R', 
   @script = N'library(glue)' 
   ```

### Python packages 
 
1. Install a Python package called [httpie](https://httpie.org/) using pip. 

   ```python
   # sudo /opt/mssql/mlservices/bin/python/python -m pip install httpie 
   ``` 

2. Import the Python package in [sp_execute_external_script](../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md).
 
   ```python
   EXEC sp_execute_external_script  
   @language = N'Python',  
   @script = N'import httpie' 
   ```

## Limitations in CTP 2.0

The following limitations exist in this CTP release.

+ Implied authentication is currently not available in Machine Learning Services on Linux at this time, which means you cannot connect back to the server from an in-progress R or Python script to access data or other resources. 

+ [CREATE EXTERNAL LIBRARY](../t-sql/statements/create-external-library-transact-sql.md) (for storing R packages in the database) is currently not available on Linux and does not support Python.  

### Resource governance

There is parity between Linux and Windows for [Resource governance](../t-sql/statements/create-external-resource-pool-transact-sql.md) for external resource pools, but the statistics for [sys.dm_resource_governor_external_resource_pools](../relational-databases/system-dynamic-management-views/sys-dm-resource-governor-external-resource-pools.md) currently have different units on Linux. Units will align in an upcoming CTP.
 
| Column name   | Description | Value on Linux | 
|---------------|--------------|---------------|
|peak_memory_kb | The maximum amount of memory used for the resource pool. | On Linux, this statistic is sourced from the CGroups memory subsystem, where the value is memory.max_usage_in_bytes |
|write_io_count | The total write IOs issued since the Resource Governor statistics were reset. | On Linux, this statistic is sourced from the CGroups blkio subsystem, where the value on the write row is blkio.throttle.io_serviced | 
|read_io_count | The total read IOs issued since the Resource Governor statistics were reset. | On Linux, this statistic is sourced from the CGroups blkio subsystem, where value on the read row is blkio.throttle.io_serviced | 
|total_cpu_kernel_ms | The cumulative CPU user kernel time in milliseconds since the Resource Governor statistics were reset. | On Linux, this statistic is sourced from the CGroups cpuacct subsystem, where the value on the user row is cpuacct.stat |  
|total_cpu_user_ms | The cumulative CPU user time in milliseconds since the Resource Governor statistics were reset.| On Linux, this statistic is sourced from the CGroups cpuacct subsystem, where the value on the system row value is cpuacct.stat | 
|active_processes_count | The number of external processes running at the moment of the request.| On Linux, this statistic is sourced from the GGroups pids subsystem, where the value is pids.current | 

## Next steps

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

+ [Tutorial: Run R in T-SQL](../advanced-analytics/tutorials/rtsql-using-r-code-in-transact-sql-quickstart.md)
+ [Tutorial: In-database analytics for R developers](../advanced-analytics/tutorials/sqldev-in-database-r-for-sql-developers.md)

Python developers can learn how to use Python with SQL Server by following these tutorials:

+ [Tutorial: Run Python in T-SQL](../advanced-analytics/tutorials/run-python-using-t-sql.md)
+ [Tutorial: In-database analytics for Python developers](../advanced-analytics/tutorials/sqldev-in-database-python-for-sql-developers.md)

To view examples of machine learning that are based on real-world scenarios, see [Machine learning tutorials](../advanced-analytics/tutorials/machine-learning-services-tutorials.md).
