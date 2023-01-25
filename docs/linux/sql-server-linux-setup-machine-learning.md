---
title: Install on Linux
titleSuffix: SQL Server 2019 Machine Learning Services
description: "Learn how to install SQL Server 2019 Machine Learning Services on Linux: Red Hat, Ubuntu, and SUSE."
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 05/24/2022
ms.topic: how-to
ms.service: sql
ms.subservice: machine-learning-services
monikerRange: "=sql-server-ver15||=sql-server-linux-ver15"
ms.custom:
- intro-installation
- event-tier1-build-2022
---
# Install SQL Server 2019 Machine Learning Services (Python and R) on Linux

[!INCLUDE [SQL Server 2019 - Linux](../includes/applies-to-version/sqlserver2019-linux.md)]

This article guides you in the installation of [SQL Server Machine Learning Services](../machine-learning//sql-server-machine-learning-services.md) on Linux. Python and R scripts can be executed in-database using Machine Learning Services.

You can install Machine Learning Services on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu. For more information, see [the Supported platforms section in the Installation guidance for SQL Server on Linux](sql-server-linux-setup.md#supportedplatforms).

> [!IMPORTANT]
> These instructions are specific to [!INCLUDE [sssql19-md](../includes/sssql19-md.md)]. For [!INCLUDE [sssql22-md](../includes/sssql22-md.md)], where installation steps are different, refer to [Install SQL Server 2022 Machine Learning Services (Python and R) on Linux](sql-server-linux-setup-machine-learning-sql-2022.md).


<a name="mro"></a>

## Pre-install checklist

* [Install SQL Server on Linux](sql-server-linux-setup.md) and verify the installation.

* Check the SQL Server Linux repositories for the Python and R extensions. 
  If you already configured source repositories for the database engine install, you can run the **mssql-mlservices** package install commands using the same repo registration.

* (R only) Microsoft R Open (MRO) provides the base R distribution for the R feature in SQL Server and is a prerequisite for using RevoScaleR, MicrosoftML, and other R packages installed with Machine Learning Services.
    * The required version is MRO 3.5.2.
    * Choose from the following two approaches to install MRO:
        * Download the MRO tarball from MRAN, unpack it, and run its install.sh script. You can follow the [installation instructions on MRAN](https://mran.microsoft.com/releases/3.5.2) if you want this approach.
        * Register the **packages.microsoft.com** repo as described below to install the MRO distribution: microsoft-r-open-mro and microsoft-r-open-mkl. 
    * See the installation sections below for how to install MRO.

* You should have a tool for running T-SQL commands. 

  * You can use [Azure Data Studio](../azure-data-studio/download-azure-data-studio.md), a free database tool that runs on Linux, Windows, and macOS.

## Package list

On an internet-connected device, packages are downloaded and installed independently of the database engine using the package installer for each operating system. The following table describes all available packages, but for R and Python, you specify packages that provide either the full feature installation or the minimum feature installation.

Available installation packages:

| Package name | Applies-to | Description |
|--------------|----------|-------------|
|mssql-server-extensibility  | All | Extensibility framework used to run Python and R. |
| microsoft-openmpi  | Python, R | Message passing interface used by the Rev* libraries for parallelization on Linux. |
| mssql-mlservices-python | Python | Open-source distribution of Anaconda and Python. |
|mssql-mlservices-mlm-py  | Python | *Full install*. Provides revoscalepy, microsoftml, pre-trained models for image featurization and text sentiment analysis.| 
|mssql-mlservices-packages-py  | Python | *Minimum install*. Provides revoscalepy and microsoftml. <br/>Excludes pre-trained models. | 
| [microsoft-r-open*](#mro) | R | Open-source distribution of R, composed of three packages. |
|mssql-mlservices-mlm-r  | R | *Full install*. Provides: RevoScaleR, MicrosoftML, sqlRUtils, olapR, pre-trained models for image featurization and text sentiment analysis.| 
|mssql-mlservices-packages-r  | R | *Minimum install*. Provides RevoScaleR, sqlRUtils, MicrosoftML, olapR. <br/>Excludes pre-trained models. |

<a name="RHEL"></a>

## Install on RHEL

Follow the steps below to install SQL Server Machine Learning Services on Red Hat Enterprise Linux (RHEL).

### Install MRO on RHEL

The following commands register the repository providing MRO. Post-registration, the commands for installing other R packages, such as mssql-mlservices-mml-r, will automatically include MRO as a package dependency.
```bash
# Import the Microsoft repository key

sudo rpm --import https://packages.microsoft.com/keys/microsoft.asc

# Set the location of the package repo at the "prod" directory
# The following command is for version 8.x
# To get the version for 6.x or 7.x, replace 8 with 6 or 7, respectively.
rpm -Uvh https://packages.microsoft.com/config/rhel/8/packages-microsoft-prod.rpm

# Update packages on your system (optional)
yum update
```

Installation Options for Python and R:

*  Install language support based on your requirements (single or multiple languages).
*  The *full installation* provides all available features including pre-trained machine learning models.
*  The *minimal installation* excludes the models but still has all of the functionality.

> [!Tip]
> If possible, run `yum clean all` to refresh packages on the system prior to installation.

### Full installation

Includes:
*  Open-source Python
*  Open-source R
*  Extensibility framework
*  Microsoft-openmpi
*  Extensions (Python, R)
*  Machine learning libraries
*  Pre-Trained models for Python and R

```bash
# Install as root or sudo
# Add everything (all R, Python)
# Be sure to include -9.4.7* in mlsservices package names
sudo yum install mssql-mlservices-mlm-py-9.4.7*
sudo yum install mssql-mlservices-mlm-r-9.4.7*
```

### Minimum installation

Includes:
*  Open-source Python
*  Open-source R
*  Extensibility framework
*  Microsoft-openmpi
*  Core Revo* libraries
*  Machine learning libraries

```bash
# Install as root or sudo
# Minimum install of R, Python extensions
# Be sure to include -9.4.6* in mlsservices package names
sudo yum install mssql-mlservices-packages-py-9.4.7*
sudo yum install mssql-mlservices-packages-r-9.4.7*
```
<a name="ubuntu"></a>

## Install on Ubuntu

Follow the steps below to install SQL Server Machine Learning Services on Ubuntu.

### Install MRO on Ubuntu

The following commands register the repository providing MRO. Post-registration, the commands for installing other R packages, such as mssql-mlservices-mml-r, will automatically include MRO as a package dependency.

```bash
# Install as root
sudo su

# Optionally, if your system does not have the https apt transport option
apt-get install apt-transport-https

# Set the location of the package repo the "prod" directory containing the distribution.
# This example specifies 20.04. Replace with 16.04 or 14.04 if you want those versions.
wget https://packages.microsoft.com/config/ubuntu/20.04/packages-microsoft-prod.deb

# Register the repo
dpkg -i packages-microsoft-prod.deb

# Update packages on your system (required), including MRO installation
sudo apt-get update
```

Installation Options for Python and R:

*  Install language support based on your requirements (single or multiple languages).
*  The *full installation* provides all available features the including pre-trained machine learning models.
*  The *minimal installation* excludes the models but still has all of the functionality.

> [!Tip]
> If possible, run `apt-get update` to refresh packages on the system prior to installation. 

### Full installation 

Includes:
*  Open-source Python
*  Open-source R
*  Extensibility framework
*  Microsoft-openmpi
*  Python extensions
*  R extensions
*  Machine learning libraries
*  Pre-trained models for Python and R

```bash
# Install as root or sudo
# Add everything (all R, Python)
# There is no asterisk in this full install
sudo apt-get install mssql-mlservices-mlm-py 
sudo apt-get install mssql-mlservices-mlm-r 
```

### Minimum installation 

Includes:
*  Open-source Python
*  Open-source R
*  Extensibility framework
*  Microsoft-openmpi
*  Core Revo* libraries
*  Machine learning libraries

```bash
# Install as root or sudo
# Minimum install of R, Python
# No asterisk
sudo apt-get install mssql-mlservices-packages-py
sudo apt-get install mssql-mlservices-packages-r
```

<a name="SLES"></a>

## Install on SLES

Follow the steps below to install SQL Server Machine Learning Services on SUSE Linux Enterprise Server (SLES).

### Install MRO on SLES

The following commands register the repository providing MRO. Post-registration, the commands for installing other R packages, such as mssql-mlservices-mml-r, will automatically include MRO as a package dependency.

```bash
# Install as root
sudo su

# Set the location of the package repo at the "prod" directory containing the distribution
# This example is for SLES12
zypper ar -f https://packages.microsoft.com/sles/12/prod packages-microsoft-com

# Update packages on your system (optional)
zypper update
```

Installation Options for Python and R:

*  Install language support based on your requirements (single or multiple languages).
*  The *full installation* provides all available features the including pre-trained machine learning models.
*  The *minimal installation* excludes the models but still has all of the functionality.

### Full installation 

Includes:
*  Open-source Python
*  Open-source R
*  Extensibility framework
*  Microsoft-openmpi
*  Extensions for Python and R
*  Machine learning libraries
*  Pre-trained models for Python and R

```bash
# Install as root or sudo
# Add everything (all R, Python)
sudo zypper install mssql-mlservices-mlm-py
sudo zypper install mssql-mlservices-mlm-r
```

### Minimum installation 

Includes:
*  Open-source Python
*  Open-source R
*  Extensibility framework
*  Microsoft-openmpi
*  Core Revo* libraries
*  Machine learning libraries 

```bash
# Install as root or sudo
# Minimum install of R, Python extensions
sudo zypper install mssql-mlservices-packages-py
sudo zypper install mssql-mlservices-packages-r
```

## Post-install config (required)

Additional configuration is primarily through the [mssql-conf tool](sql-server-linux-configure-mssql-conf.md).

1. After the package installation finishes, run mssql-conf setup and follow the prompts to set the SA password and choose your edition. Perform this step only if you have not configured SQL Server on Linux yet. 

   ```bash
   sudo /opt/mssql/bin/mssql-conf setup
   ```

2. Accept the licensing agreements for open-source Python and R extensions. Use the following command:

   ```bash
   # Run as SUDO or root
   # Use set + EULA 
   sudo /opt/mssql/bin/mssql-conf set EULA accepteulaml Y
   ```
   Setup detects the mssql-mlservices packages and prompts for EULA acceptance (if not previously accepted) when `mssql-conf setup` is run. For more information about EULA parameters, see [Configure SQL Server with the mssql-conf tool](sql-server-linux-configure-mssql-conf.md#mlservices-eula).

3. Enable outbound network access. Outbound network access is disabled by default. To enable outbound requests, set the "outboundnetworkaccess" Boolean property using the mssql-conf tool. For more information, see [Configure SQL Server on Linux with mssql-conf](sql-server-linux-configure-mssql-conf.md#mlservices-outbound-access).

   ```bash
   # Run as SUDO or root
   # Enable outbound requests over the network
   sudo /opt/mssql/bin/mssql-conf set extensibility outboundnetworkaccess 1
   ```

4. For R feature integration only, set the **MKL_CBWR** environment variable to [ensure consistent output](https://software.intel.com/articles/introduction-to-the-conditional-numerical-reproducibility-cnr) from Intel Math Kernel Library (MKL) calculations.

   + Edit or create a file `.bash_profile` in your user home directory, adding the line `export MKL_CBWR="AUTO"` to the file.

   + Execute this file by typing `source .bash_profile` at a bash command prompt.

5. Restart the SQL Server Launchpad service and the database engine instance to read the updated values from the INI file. A notification message is displayed when an extensibility-related setting is modified.  

   ```bash
   systemctl restart mssql-launchpadd

   systemctl restart mssql-server.service
   ```

6. Enable external script execution using Azure Data Studio or another tool like SQL Server Management Studio (Windows only) that runs Transact-SQL.

   ```sql
   EXEC sp_configure 'external scripts enabled', 1 
   RECONFIGURE WITH OVERRIDE 
   ```

7. Restart the Launchpad service again.

## Verify installation

R libraries (MicrosoftML, RevoScaleR, and others) can be found at `/opt/mssql/mlservices/libraries/RServer`.

Python libraries (microsoftml and revoscalepy) can be found at `/opt/mssql/mlservices/libraries/PythonServer`.

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

<a name="install-all"></a>

## Unattended installation

Using the [unattended install](sql-server-linux-setup.md#unattended) for the Database Engine, add the packages for mssql-mlservices and EULAs.

 Use one of the mlservices-specific EULA parameters for the open-source R and Python distributions:

```bash
sudo /opt/mssql/bin/mssql-conf setup accept-eula-ml
```

The complete EULA is documented at [Configure SQL Server on Linux with the mssql-conf tool](sql-server-linux-configure-mssql-conf.md#mlservices-eula).

## Offline installation

Follow the [Offline installation](sql-server-linux-setup.md#offline) instructions for steps on installing the packages. Find your download site, and then download specific packages using the package list below.

> [!Tip]
> Several of the package management tools provide commands that can help you determine package dependencies. For yum, use `sudo yum deplist [package]`. For Ubuntu, use `sudo apt-get install --reinstall --download-only [package name]` followed by `dpkg -I [package name].deb`.

 
### Download site

Download packages from [https://packages.microsoft.com/](https://packages.microsoft.com/). All of the mlservices packages for Python and R are colocated with database engine package. Base version for the mlservices packages is 9.4.6. Recall that the microsoft-r-open packages are in a [different repository](#mro).

### RHEL/8 paths

|Package|Download location|
|--|----|
| mssql/mlservices packages | [https://packages.microsoft.com/rhel/8/mssql-server-2019/](https://packages.microsoft.com/rhel/8/mssql-server-2019/) |
| microsoft-r-open packages | [https://packages.microsoft.com/rhel/8/prod/](https://packages.microsoft.com/rhel/8/prod/) | 

### Ubuntu/20.04 paths

|Package|Download location|
|--|----|
| mssql/mlservices packages | [https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/](https://packages.microsoft.com/ubuntu/20.04/mssql-server-2019/pool/main/m/) |
| microsoft-r-open packages | [https://packages.microsoft.com/ubuntu/20.04/prod/pool/main/m/](https://packages.microsoft.com/ubuntu/20.04/prod/pool/main/m/) | 

### SLES/12 paths

|Package|Download location|
|--|----|
| mssql/mlservices packages | [https://packages.microsoft.com/sles/12/mssql-server-2019/](https://packages.microsoft.com/sles/12/mssql-server-2019/) |
| microsoft-r-open packages | [https://packages.microsoft.com/sles/12/prod/](https://packages.microsoft.com/sles/12/prod/) | 

Select extensions you want to use and download the packages necessary for a specific language. The filenames include platform information in the suffix.

### Package list

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

## Next steps

Python developers can learn how to use Python with SQL Server by following these tutorials:

+ [Python tutorial: Predict ski rental with linear regression in SQL Server Machine Learning Services](../machine-learning/tutorials/python-ski-rental-linear-regression-deploy-model.md)
+ [Python tutorial: Categorizing customers using k-means clustering with SQL Server Machine Learning Services](../machine-learning/tutorials/python-clustering-model.md)

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

+ [Quickstart: Run R in T-SQL](../machine-learning/tutorials/quickstart-r-create-script.md)
+ [Tutorial: In-database analytics for R developers](../machine-learning/tutorials/r-taxi-classification-introduction.md)
