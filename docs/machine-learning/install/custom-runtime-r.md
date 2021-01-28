---
title: Install R custom runtime
description: Learn how to install an R custom runtime for SQL Server using Language Extensions. The Python custom runtime can run machine learning scripts.
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 1/13/2021
ms.topic: how-to
author: dphansen
ms.author: davidph
ms.custom: seo-lt-2019
zone_pivot_groups: sqlml-platforms
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15"
---
# Install an R custom runtime for SQL Server

[!INCLUDE [SQL Server 2019 and later](../../includes/applies-to-version/sqlserver2019.md)]

Learn how to install an R custom runtime for running external R scripts with SQL Server on:

+ Windows
+ Ubuntu Linux
+ Red Hat Enterprise Linux (RHEL)
+ SUSE Linux Enterprise Server (SLES)

The custom runtime can run machine learning scripts and uses the [SQL Server Language Extensions](../../language-extensions/language-extensions-overview.md).

Use your own version of the R runtime with SQL Server, instead of the default runtime version installed with [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md).

::: zone pivot="platform-windows"
[!INCLUDE [R custom runtime - Windows](includes/custom-runtime-r-windows.md)]
::: zone-end

::: zone pivot="platform-linux-ubuntu"
[!INCLUDE [R custom runtime - Linux - Prerequisites](includes/custom-runtime-r-linux-prerequisites.md)]

[!INCLUDE [R custom runtime - Linux - Ubuntu specific steps](includes/custom-runtime-r-linux-ubuntu.md)]

[!INCLUDE [R custom runtime on Linux - Common steps](includes/custom-runtime-r-linux-common.md)]
::: zone-end

::: zone pivot="platform-linux-rhel"
[!INCLUDE [R custom runtime - Linux - Prerequisites](includes/custom-runtime-r-linux-prerequisites.md)]

[!INCLUDE [R custom runtime - Linux - RHEL specific steps](includes/custom-runtime-r-linux-rhel.md)]

[!INCLUDE [R custom runtime on Linux - Common steps](includes/custom-runtime-r-linux-common.md)]
::: zone-end

::: zone pivot="platform-linux-sles"
[!INCLUDE [R custom runtime - Linux - Prerequisites](includes/custom-runtime-r-linux-prerequisites.md)]

[!INCLUDE [R custom runtime - Linux - SLES specific steps](includes/custom-runtime-r-linux-sles.md)]

[!INCLUDE [R custom runtime on Linux - Common steps](includes/custom-runtime-r-linux-common.md)]
::: zone-end







## Add SQL Server Language Extensions for Linux

> [!NOTE]
> If you have Machine Learning Services installed on SQL Server 2019, the **mssql-server-extensibility** package for language extensions is already installed and you can skip this step.

Language Extensions use the extensibility framework for executing external code. Code execution is isolated from the core engine processes, but fully integrated with SQL Server query execution.

Use the following commands to install Language Extensions, depending on your version of Linux.

### Ubuntu
> [!Tip]
> If possible, `sudo apt-get update` to refresh packages on the system prior to installation. Ubuntu might not have the https apt transport option. To install it, use `sudo apt-get install apt-transport-https`.

```bash
# Install as root or sudo
sudo apt-get install mssql-server-extensibility
```

### Red Hat
```bash
# Install as root or sudo
sudo yum install mssql-server-extensibility
```

### Suse
```bash
# Install as root or sudo
sudo zypper install mssql-server-extensibility
```

## Install R

>[!NOTE]
>For SQL Machine Learning Services, R is already installed in `/opt/microsoft/ropen/3.5.2/lib64/R`. If you want to keep using this path as your R_HOME, skip to the next step of **installing Rcpp**. 

If you want to use a different runtime of R, you first need to remove `microsoft-r-open-mro` before continuing to install a new version. Example for Ubuntu:

```bash
sudo apt remove microsoft-r-open-mro-3.5.2
```

Follow the [instructions](https://cran.r-project.org/bin/linux/) to complete the installation of R (3.3 or later) for your respective linux platform. By default, R is installed in **/usr/lib/R**. This path is your **R_HOME**. If you install R in a different location, take note of that path as your R_HOME.

Example instructions for Ubuntu. Change the repository URL below for your version of R.

```bash
export DEBIAN_FRONTEND=noninteractive
sudo apt-get update
sudo apt-get --no-install-recommends -y install curl zip unzip apt-transport-https libstdc++6

# Add R CRAN repository. This repository works for R 4.0.x.
#
sudo apt-key adv --keyserver keyserver.ubuntu.com --recv-keys E298A3A825C0D65DFD57CBB651716619E084DAB9
sudo add-apt-repository 'deb https://cloud.r-project.org/bin/linux/ubuntu xenial-cran40/'
sudo apt-get update

# Install R runtime.
#
sudo apt-get -y install r-base-core
```

## Install Rcpp package

In the following instructions, ${R_HOME} is the path to your R installation. 

+ Locate the R binary in ${R_HOME}/bin. By default, it is in **/usr/lib/R/bin**.

+ Start R

  ```bash
  sudo ${R_HOME}/bin/R
  ```

+ In this *elevated* R prompt (${R_HOME}/bin/R), run the following script to install the **Rcpp** package in the ${R_HOME}/library folder.

  ```R
  install.packages("Rcpp", lib = "${R_HOME}/library");
  ```

## Using a custom installation of R

> [!NOTE]
>If you have installed R in the default location of **/usr/lib/R**, you can skip this section.

### Update the environment variables

1. Edit the mssql-launchpadd service to add the R_HOME environment variable to the file `/etc/systemd/system/mssql-launchpadd.service.d/override.conf`

    ```bash
    sudo systemctl edit mssql-launchpadd
    ```

    + Insert the following text in the `/etc/systemd/system/mssql-launchpadd.service.d/override.conf` file that opens. Set value of R_HOME to the custom R installation path.

    ```text
    [Service]
    Environment="R_HOME=/path/to/installation/of/R"
    ```

    + Save and close.

2. Make sure **libR.so** can be loaded.

    + Create a custom-r.conf file in **/etc/ld.so.conf.d**.

    ```bash
    sudo vi /etc/ld.so.conf.d/custom-r.conf
    ```

    + In the file that opens, add path to **libR.so** from the custom R installation.

    ```vi editor
    /path/to/installation/of/R/lib
    ```

    + Save and close the new file.

    + Run `ldconfig` and verify **libR.so** can be loaded by running the following command and checking that all dependent libraries can be found.

    ```bash
    sudo ldconfig
    ldd /path/to/installation/of/R/lib/libR.so
    ```

### Grant access to the custom R installation folder

Set the `datadirectories` option in the extensibility section of /var/opt/mssql/mssql.conf file to the custom R installation.

```bash
sudo /opt/mssql/bin/mssql-conf set extensibility.datadirectories /path/to/installation/of/R
```

### Restart mssql-launchpadd service

> [!NOTE]
>If you have installed R in the default location of **/usr/lib/R**, you can skip this step.

```bash
sudo systemctl restart mssql-launchpadd
```

## Download R language extension

Download the [zip file containing the R language extension for Linux](https://github.com/microsoft/sql-server-language-extensions/releases). Recommended to use the release version in production. Use the debug version in development or test since it provides verbose logging information to investigate any errors.

## Register external language

Register this R language extension with [CREATE EXTERNAL LANGUAGE](../../t-sql/statements/create-external-language-transact-sql.md) for each database you want to use it in. Use [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio) to connect to SQL Server and run the following T-SQL command. 
Modify the path in this statement to reflect the location of the downloaded language extension zip file (r-lang-extension.zip).


> [!NOTE]
>**R** is a reserved word. Use a different name for the external language, for example, "myR".

```sql
CREATE EXTERNAL LANGUAGE [myR]
FROM (CONTENT = N'/path/to/R-lang-extension.zip', FILE_NAME = 'libRExtension.so.1.0');
GO
```

::: zone-end

## Enable external script execution in SQL Server

An external script in R can be executed via the stored procedure [sp_execute_external script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) run against SQL Server. 

To enable external scripts, execute the following SQL commands using [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio), connected to SQL Server.

```sql
sp_configure 'external scripts enabled', 1;
RECONFIGURE WITH OVERRIDE;
```

## Verify language extension installation

This SQL script verifies the successful installation of the custom R language extension. Output of this script should display the R_HOME, path to R and the version of the custom R runtime. It confirms the script is using your custom runtime.

```sql
EXEC sp_execute_external_script
	@language =N'myR',
	@script=N'
print(R.home());
print(file.path(R.home("bin"), "R"));
print(R.version);
print("Hello RExtension!");'
```

## Verify parameters and datasets of different data types

This script tests different data types for input/output parameters and datasets.

```sql
DECLARE @sumVal INT = 12;
DECLARE @charVal VARCHAR(30) = N'Hello';
EXEC sp_execute_external_script
	@language =N'myR',
	@script=N'
print(sumVal);
print(charVal);
sumVal <- sumVal + 300;
OutputDataSet <- InputDataSet;'
	,@input_data_1 = N'select 1, cast(1.4 as real), ''Hi'', cast(''1'' as bit)'
	,@params = N'@sumVal int OUTPUT, @charVal VARCHAR(30)'
	,@sumVal = @sumVal OUTPUT
	,@charVal =  @charVal
WITH RESULT SETS ((intCol INT, doubleCol REAL, charCol CHAR(2), logicalCol BIT));
PRINT @sumVal;
```

## See also

+ [Extensibility framework in SQL Server](../concepts/extensibility-framework.md)
+ [Language Extensions Overview](../../language-extensions/language-extensions-overview.md)