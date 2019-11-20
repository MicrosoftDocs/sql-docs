---
title: Install SQL Server Machine Learning Services (Python, R) on Linux
description: 'Learn how to install SQL Server Machine Learning Services (Python and R) on Linux: Red Hat, Ubuntu, and SUSE.'
author: dphansen
ms.author: davidph
ms.reviewer: vanto
manager: cgronlun
ms.date: 11/04/2019
ms.topic: conceptual
ms.prod: sql
ms.technology: machine-learning
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Install SQL Server Machine Learning Services (Python and R) on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article explains how to install [SQL Server Machine Learning Services](../advanced-analytics/index.yml) on Linux. You can use Machine Learning Services to execute Python and R scripts in-database.

The following Linux distributions are supported:

- Red Hat Enterprise Linux (RHEL)
- SUSE Linux Enterprise Server (SLES)
- Ubuntu

Machine Learning Services are a feature add-on to the database engine. Although you can [install the database engine and Machine Learning Services concurrently](#install-all), it's a best practice to install and configure the SQL Server database engine first so that you can resolve any issues before adding more components. 

Package location for the Python and R extensions is in the SQL Server Linux source repositories. If you already configured source repositories for the database engine install, you can run the **mssql-mlservices** package install commands using the same repo registration.

Machine Learning Services is also supported on Linux containers. We do not provide pre-built containers with Machine Learning Services, but you can create one from the SQL Server containers using [an example template available on GitHub](https://github.com/Microsoft/mssql-docker/tree/master/linux/preview/examples/mssql-mlservices).

Machine Learning Services is installed by default on SQL Server Big Data Clusters, and you do not need to follow the steps in this case. For more information, see [Use Machine Learning Services (Python and R) on Big Data Clusters](../big-data-cluster/machine-learning-services.md).

## Uninstall preview release

If you have installed a preview release (Community Technical Preview (CTP) or Release Candidate), we recommend uninstalling this version to remove all previous packages before installing SQL Server 2019. Side-by-side installation of multiple versions is not supported, and the package list has changed over the last several preview (CTP/RC) releases.

### 1. Confirm package installation

You might want to check for the existence of a previous installation as a first step. The following files indicate an existing installation: checkinstallextensibility.sh, exthost, launchpad.

```bash
ls /opt/microsoft/mssql/bin
```

### 2. Uninstall CTP/RC packages

Uninstall at the lowest package level. Any upstream package dependent on a lower-level package is automatically uninstalled.

  + For R integration, remove **microsoft-r-open***
  + For Python integration, remove **mssql-mlservices-python**

Commands for removing packages appear in the following table.

| Platform	| Package removal command(s) | 
|-----------|----------------------------|
| Red Hat	| `sudo yum remove microsoft-r-open-mro-3.4.4`<br/>`sudo yum remove msssql-mlservices-python` |
| SUSE	| `sudo zypper remove microsoft-r-open-mro-3.4.4`<br/>`sudo zypper remove msssql-mlservices-python` |
| Ubuntu	| `sudo apt-get remove microsoft-r-open-mro-3.4.4`<br/>`sudo apt-get remove msssql-mlservices-python`|

> [!Note]
> Microsoft R Open 3.4.4 is composed of two packages, depending on which CTP release you previously installed. (The foreachiterators package was combined into the main mro package in CTP 2.2.) If any of these packages remain after removing microsoft-r-open-mro-3.4.4, you should remove them individually.
> ```
> microsoft-r-open-foreachiterators-3.4.4
> microsoft-r-open-mkl-3.4.4
> microsoft-r-open-mro-3.4.4
> ```

### 3. Proceed with install

Install at the highest package level using the instructions in this article for your operating system.

For each OS-specific set of installation instructions, *highest package level* is either **Example 1 - Full installation** for the full set of packages, or **Example 2 - Minimal installation** for the least number of packages required for a viable installation.

1. For R integration, start with [MRO](#mro) because it is a prerequisite. R integration will not install without it.

2. Run install commands using the package managers and syntax for your operating system: 

   + [Red Hat](#RHEL)
   + [Ubuntu](#ubuntu)
   + [SUSE](#suse)

## Prerequisites

+ The Linux version must be [supported by SQL Server](sql-server-linux-release-notes-2019.md#supported-platforms), but does not include the Docker Engine. Supported versions include:

   + [Red Hat Enterprise Linux (RHEL)](quickstart-install-connect-red-hat.md)

   + [SUSE Enterprise Linux Server](quickstart-install-connect-suse.md)

   + [Ubuntu](quickstart-install-connect-ubuntu.md)

+ (R only) [Microsoft R Open](#mro) provides the base R distribution for the R feature in SQL Server

+ You should have a tool for running T-SQL commands. A query editor is necessary for post-install configuration and validation. We recommend [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/download?view=sql-server-2017#get-azure-data-studio-for-linux), a free download that runs on Linux.

<a name="mro"></a>

### Microsoft R Open (MRO) installation

Microsoft's base distribution of R is a prerequisite for using RevoScaleR, MicrosoftML, and other R packages installed with Machine Learning Services.

The required version is MRO 3.5.2.

Choose from the following two approaches to install MRO:

+ Download the MRO tarball from MRAN, unpack it, and run its install.sh script. You can follow the [installation instructions on MRAN](https://mran.microsoft.com/releases/3.5.2) if you want this approach.

+ Alternatively, register the **packages.microsoft.com** repo as described below to install the two packages comprising the MRO distribution: microsoft-r-open-mro and microsoft-r-open-mkl. 

The following commands register the repository providing MRO. Post-registration, the commands for installing other R packages, such as mssql-mlservices-mml-r, will automatically include MRO as a package dependency.

#### MRO on Ubuntu

```bash
# Install as root
sudo su

# Optionally, if your system does not have the https apt transport option
apt-get install apt-transport-https

# Set the location of the package repo the "prod" directory containing the distribution.
# This example specifies 16.04. Replace with 14.04 if you want that version
wget https://packages.microsoft.com/config/ubuntu/16.04/packages-microsoft-prod.deb

# Register the repo
dpkg -i packages-microsoft-prod.deb

# Update packages on your system (required), including MRO installation
sudo apt-get update
```

#### MRO on Red Hat

```bash
# Import the Microsoft repository key
sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc


# Set the location of the package repo at the "prod" directory
# The following command is for version 7.x
# For 6.x, replace 7 with 6 to get that version
rpm -Uvh https://packages.microsoft.com/config/rhel/7/packages-microsoft-prod.rpm

# Update packages on your system (optional)
yum update
```

#### MRO on SUSE

```bash
# Install as root
sudo su

# Set the location of the package repo at the "prod" directory containing the distribution
# This example is for SLES12, the only supported version of SUSE in Machine Learning Server
zypper ar -f https://packages.microsoft.com/sles/12/prod packages-microsoft-com

# Update packages on your system (optional)
zypper update
```

## Package list

On an internet-connected device, packages are downloaded and installed independently of the database engine using the package installer for each operating system. The following table describes all available packages, but for R and Python, you specify packages that provide either the full feature installation or the minimum feature installation.

| Package name | Applies-to | Description |
|--------------|----------|-------------|
|mssql-server-extensibility  | All | Extensibility framework used to run R and Python code. |
| microsoft-openmpi  | Python, R | Message passing interface used by the Revo* libraries for parallelization on Linux. |
| mssql-mlservices-python | Python | Open-source distribution of Anaconda and Python. |
|mssql-mlservices-mlm-py  | Python | *Full install*. Provides revoscalepy, microsoftml, pre-trained models for image featurization and text sentiment analysis.| 
|mssql-mlservices-packages-py  | Python | *Minimum install*. Provides revoscalepy and microsoftml. <br/>Excludes pre-trained models. | 
| [microsoft-r-open*](#mro) | R | Open-source distribution of R, composed of three packages. |
|mssql-mlservices-mlm-r  | R | *Full install*. Provides RevoScaleR, MicrosoftML, sqlRUtils, olapR, pre-trained models for image featurization and text sentiment analysis.| 
|mssql-mlservices-packages-r  | R | *Minimum install*. Provides RevoScaleR, sqlRUtils, MicrosoftML, olapR. <br/>Excludes pre-trained models. | 

<a name="RHEL"></a>

## RedHat commands

You can install language support in whatever combination you require (single or multiple languages). For R and Python, there are two packages to choose from. One provides all available features, characterized as the *full installation*. The alternative choice excludes the pretrained machine learning models and is considered the *minimal installation*.

> [!Tip]
> If possible, run `yum clean all` to refresh packages on the system prior to installation.

### Example 1 -  Full installation 

Includes open-source R and Python, extensibility framework, microsoft-openmpi, extensions (R, Python), with machine learning libraries and pre-trained models for R and Python. 

```bash
# Install as root or sudo
# Add everything (all R, Python)
# Be sure to include -9.4.7* in mlsservices package names
sudo yum install mssql-mlservices-mlm-py-9.4.7*
sudo yum install mssql-mlservices-mlm-r-9.4.7* 
```

### Example 2 - Minimum installation 

Includes open-source R and Python, extensibility framework, microsoft-openmpi, core Revo* libraries, and machine learning libraries for R and Python. Excludes the pre-trained models.

```bash
# Install as root or sudo
# Minimum install of R, Python extensions
# Be sure to include -9.4.6* in mlsservices package names
sudo yum install mssql-mlservices-packages-py-9.4.7*
sudo yum install mssql-mlservices-packages-r-9.4.7*
```

<a name="ubuntu"></a>

## Ubuntu commands

You can install language support in whatever combination you require (single or multiple languages). For R and Python, there are two packages to choose from. One provides all available features, characterized as the *full installation*. The alternative choice excludes the pretrained machine learning models and is considered the *minimal installation*.

> [!Tip]
> If possible, run `apt-get update` to refresh packages on the system prior to installation. Additionally, some docker images of Ubuntu might not have the https apt transport option. To install it, use `apt-get install apt-transport-https`.

### Example 1 -  Full installation 

Includes open-source R and Python, extensibility framework, microsoft-openmpi, extensions (R, Python), with machine learning libraries and pre-trained models for R and Python. 

```bash
# Install as root or sudo
# Add everything (all R, Python)
# There is no asterisk in this full install
sudo apt-get install mssql-mlservices-mlm-py 
sudo apt-get install mssql-mlservices-mlm-r 
```

### Example 2 - Minimum installation 

Includes open-source R and Python, extensibility framework, microsoft-openmpi, core Revo* libraries, and machine learning libraries for R and Python. Excludes the pre-trained models. 

```bash
# Install as root or sudo
# Minimum install of R, Python
# No aasterisk
sudo apt-get install mssql-mlservices-packages-py
sudo apt-get install mssql-mlservices-packages-r
```

<a name="suse"></a>

## SUSE commands

You can install language support in whatever combination you require (single or multiple languages). For R and Python, there are two packages to choose from. One provides all available features, characterized as the *full installation*. The alternative choice excludes the pretrained machine learning models and is considered the *minimal installation*.

### Example 1 -  Full installation 

Includes open-source R and Python, extensibility framework, microsoft-openmpi, extensions (R, Python), with machine learning libraries and pre-trained models for R and Python. 

```bash
# Install as root or sudo
# Add everything (all R, Python)
# Be sure to include -9.4.7* in mlsservices package names
sudo zypper install mssql-mlservices-mlm-py-9.4.7*
sudo zypper install mssql-mlservices-mlm-r-9.4.7* 
```

### Example 2 - Minimum installation 

Includes open-source R and Python, extensibility framework, microsoft-openmpi, core Revo* libraries, and machine learning libraries for R and Python. Excludes the pre-trained models. 

```bash
# Install as root or sudo
# Minimum install of R, Python extensions
# Be sure to include -9.4.6* in mlsservices package names
sudo zypper install mssql-mlservices-packages-py-9.4.7*
sudo zypper install mssql-mlservices-packages-r-9.4.7*
```

## Post-install config (required)

Additional configuration is primarily through the [mssql-conf tool](sql-server-linux-configure-mssql-conf.md).


1. Add the mssql user account used to run the SQL Server service. This is required if you haven't run the setup previously.

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

3. Enable outbound network access. Outbound network access is disabled by default. To enable outbound requests, set the "outboundnetworkaccess" Boolean property using the mssql-conf tool. For more information, see [Configure SQL Server on Linux with mssql-conf](sql-server-linux-configure-mssql-conf.md#mlservices-outbound-access).

   ```bash
   # Run as SUDO or root
   # Enable outbound requests over the network
   sudo /opt/mssql/bin/mssql-conf set extensibility outboundnetworkaccess 1
   ```

4. For R feature integration only, set the **MKL_CBWR** environment variable to [ensure consistent output](https://software.intel.com/articles/introduction-to-the-conditional-numerical-reproducibility-cnr) from Intel Math Kernel Library (MKL) calculations.

   + Edit or create a file named **.bash_profile** in your user home directory, adding the line `export MKL_CBWR="AUTO"` to the file.

   + Execute this file by typing `source .bash_profile` at a bash command prompt.

5. Restart the SQL Server Launchpad service and the database engine instance to read the updated values from the INI file. A restart message reminds you whenever an extensibility-related setting is modified.  

   ```bash
   systemctl restart mssql-launchpadd

   systemctl restart mssql-server.service
   ```

6. Enable external script execution using Azure Data Studio or another tool like SQL Server Management Studio (Windows only) that runs Transact-SQL. 

   ```bash
   EXEC sp_configure 'external scripts enabled', 1 
   RECONFIGURE WITH OVERRIDE 
   ```

7. Restart the Launchpad service again.

## Verify installation

R libraries (MicrosoftML, RevoScaleR, and others) can be found at `/opt/mssql/mlservices/libraries/RServer`.

Python libraries (microsoftml and revoscalepy) can be found at `/opt/mssql/mlservices/libraries/PythonServer`.

To validate installation, run a T-SQL script that executes a system stored procedure invoking R or Python. You will need a query tool for this task. Azure Data Studio is a good choice. Other commonly used tools such as SQL Server Management Studio or PowerShell are Windows-only. If you have a Windows computer with these tools, use it to connect to your Linux installation of the database engine.

Execute the following SQL command to test R execution in SQL Server. If the script does not run, try a service restart, `sudo systemctl restart mssql-server.service`.

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

## Chained "combo" install

You can install and configure the database engine and Machine Learning Services in one procedure by appending R or Python packages and parameters on a command that installs the database engine. 

1. For R integration, install [Microsoft R Open](#mro) as a prerequisite. Skip this step if you are not installing the R feature.

2. Provide a command line that includes the database engine, plus language extension features.

  You can add a single feature, such as Python integration, to a database engine install.

  ```bash
  sudo yum install -y mssql-server mssql-mlservices-packages-r-9.4.7* 
  ```

  Or, add both extensions (R, Python).

  ```bash
  sudo yum install -y mssql-server mssql-mlservices-packages-r-9.4.7* mssql-mlservices-packages-py-9.4.7*
  ```

3. Accept license agreements and complete the post-install configuration. Use the **mssql-conf** tool for this task.

  ```bash
  sudo /opt/mssql/bin/mssql-conf setup
  ```

  You will be prompted to accept the license agreement for the database engine, choose an edition, and set the administrator password. You are also prompted to accept the license agreement for Machine Learning Services.

4. Restart the service, if prompted to do so.

  ```bash
  sudo systemctl restart mssql-server.service
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
> Several of the package management tools provide commands that can help you determine package dependencies. For yum, use `sudo yum deplist [package]`. For Ubuntu, use `sudo apt-get install --reinstall --download-only [package name]` followed by `dpkg -I [package name].deb`.


#### Download site

You can download packages from [https://packages.microsoft.com/](https://packages.microsoft.com/). All of the mlservices packages for R and Python are colocated with database engine package. Base version for the mlservices packages is 9.4.6. Recall that the microsoft-r-open packages are in a [different repository](#mro).

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
| mssql/mlservices packages | [https://packages.microsoft.com/sles/12/mssql-server-preview/](https://packages.microsoft.com/sles/12/mssql-server-preview/) |
| microsoft-r-open packages | [https://packages.microsoft.com/sles/12/prod/](https://packages.microsoft.com/sles/12/prod/) | 

#### Package list

Depending on which extensions you want to use, download the packages necessary for a specific language. Exact filenames include platform information in the suffix, but the file names below should be close enough for you to determine which files to get.

```
# Core packages 
mssql-server-15.0.1000
mssql-server-extensibility-15.0.1000

# R
microsoft-openmpi-3.0.0
microsoft-r-open-mkl-3.5.2
microsoft-r-open-mro-3.5.2
mssql-mlservices-packages-r-9.4.7.64
mssql-mlservices-mlm-r-9.4.7.64


# Python
microsoft-openmpi-3.0.0
mssql-mlservices-python-9.4.7.64
mssql-mlservices-packages-py-9.4.7.64
mssql-mlservices-mlm-py-9.4.7.64
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

## Run in a container

Follow the steps below to build and run SQL Server Machine Learning Services in a Docker container. For more information, see [Configure SQL Server container images on Docker](sql-server-linux-configure-docker.md).

### Prerequisites

- Git command-line interface.
- Docker Engine 1.8+ on any supported Linux distribution, or Docker for Mac/Windows. For more information, see [Install Docker](https://docs.docker.com/engine/installation/).
- Minimum of 2 gigabytes (GB) of disk space.
- Minimum of 2 GB of RAM.
- [System requirements for SQL Server on Linux](sql-server-linux-setup.md#system).

### Clone the mssql-docker repository

1. Open a Bash terminal on Linux or Mac, or open a WSL terminal on Windows.

1. Create a local directory to hold a local copy of the mssql-docker repository.

1. Run the git clone command to clone the mssql-docker repository:

    ```bash
    git clone https://github.com/microsoft/mssql-docker mssql-docker
    ```

### Build a SQL Server Linux container image with Machine Learning Services

1. Change the directory to the mssql-mlservices directory:

    ```bash
    cd mssql-docker/linux/preview/examples/mssql-mlservices
    ```

1. Run the build.sh script:

   ```bash
   ./build.sh
   ```

   > [!NOTE]
   > To build the Docker image, you must install packages that are several GBs in size. The script may take up to 20 minutes to finish running, depending on network bandwidth.

### Run the SQL Server Linux container image with Machine Learning Services

1. Set your environment variables before running the container. Set the PATH_TO_MSSQL environment variable to a host directory:

   ```bash
    export MSSQL_PID='Developer'
    export ACCEPT_EULA='Y'
    export ACCEPT_EULA_ML='Y'
    export PATH_TO_MSSQL='/home/mssql/'
   ```

1. Run the run.sh script:

   ```bash
   ./run.sh
   ```

   This command creates a SQL Server container with Machine Learning Services, using the Developer edition (default). SQL Server port **1433** is exposed on the host as port **1401**.

   > [!NOTE]
   > The process for running production SQL Server editions in containers is slightly different. For more information, see [Configure SQL Server container images on Docker](sql-server-linux-configure-docker.md). If you use the same container names and ports, the rest of this walk-through still works with production containers.

1. To view your Docker containers, run the `docker ps` command:

   ```bash
   sudo docker ps -a
   ```

1. If the **STATUS** column shows a status of **Up**, SQL Server is running in the container and listening on the port specified in the **PORTS** column. If the **STATUS** column for your SQL Server container shows **Exited**, see the [Troubleshooting section of the configuration guide](sql-server-linux-configure-docker.md#troubleshooting).

   ```bash
   $ sudo docker ps -a
   ```

    Output: 
    
    ```
    CONTAINER ID        IMAGE                          COMMAND                  CREATED             STATUS              PORTS                    NAMES
    941e1bdf8e1d        mcr.microsoft.com/mssql/server/mssql-server-linux   "/bin/sh -c /opt/m..."   About an hour ago   Up About an hour     0.0.0.0:1401->1433/tcp   sql1
    ```

## Next steps

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

+ [Tutorial: Run R in T-SQL](../advanced-analytics/tutorials/quickstart-r-create-script.md)
+ [Tutorial: In-database analytics for R developers](../advanced-analytics/tutorials/sqldev-in-database-r-for-sql-developers.md)

Python developers can learn how to use Python with SQL Server by following these tutorials:

+ [Tutorial: Run Python in T-SQL](../advanced-analytics/tutorials/run-python-using-t-sql.md)
+ [Tutorial: In-database analytics for Python developers](../advanced-analytics/tutorials/sqldev-in-database-python-for-sql-developers.md)

To view examples of machine learning that are based on real-world scenarios, see [Machine learning tutorials](../advanced-analytics/tutorials/machine-learning-services-tutorials.md).
