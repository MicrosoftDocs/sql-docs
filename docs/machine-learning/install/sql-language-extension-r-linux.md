---
title: Install your own R language extension for Linux
description: Learn how to install your own runtime for R for SQL Server.
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 09/20/2020
ms.topic: how-to
author: cawrites
ms.author: chadam
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Install custom runtime for R

[!INCLUDE [SQL Server 2019 and later](../../includes/applies-to-version/sqlserver2019.md)]

>[!Note] 
>R language extension to bring your own runtime runs on SQL Server 2019 CU3 or later.
 
This article describes how to install the language extension for running R scripts with SQL Server. You can install SQL Server on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu. For more information, see [the Supported platforms section in the Installation guidance for SQL Server on Linux](../../linux/sql-server-linux-setup.md).

The runtime language extension can be used with the following scenarios:

+ An installation of SQL Server with extensibility framework.

+ An existing installation of Machine Learning Services with  mssql-mlservices-packages-py installed in Linux. The runtime can be used with [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) after completing some additional configuration steps.

## <a name="bkmk_prereqs"> </a> Pre-install checklist

+ [SQL Server 2019 CU3 and later for Linux.](../../linux/sql-server-linux-setup.md)
When you install SQL Server on Linux, you must configure a Microsoft repository. For more information, see [configuring repositories](../../linux/sql-server-linux-change-repo.md)

+ [SQL Server Language Extensions on Linux with the extensibility framework.](../../linux/sql-server-linux-setup-language-extensions.md)

+ [Azure Data Studio ](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio) for T-SQL commands.

+ [R Version 3.3 or higher](https://cran.r-project.org/)

+ [R Studio ](https://rstudio.com/products/rstudio/download/) for executing R code.

## Add SQL Server Language Extensions for Linux

Language Extensions use the extensibility framework for executing external code. Code execution is isolated from the core engine processes, but fully integrated with SQL Server query execution.

> [!Tip]
> If possible, `update` to refresh packages on the system prior to installation. Ubuntu might not have the https apt transport option. To install it, use `apt-get install apt-transport-https`.

## Ubuntu
```bash
# Install as root or sudo
sudo apt-get install mssql-server-extensibility
```

## Red Hat
```bash
# Install as root or sudo
sudo yum install mssql-server-extensibility
```

## Suse
```bash
# Install as root or sudo
sudo zypper install mssql-server-extensibility
```

>[!Note]
>For Machine Learning Services using SQL Server 2019 mssql-server-extensibility is already installed.

## Install R - Do these steps work for any version of R?

[Complete installation of R and add to path.](https://cran.r-project.org/)

```bash
export DEBIAN_FRONTEND=noninteractive
sudo apt-get update
sudo apt-get --no-install-recommends -y install curl zip unzip apt-transport-https libstdc++6

# Add R CRAN repository.
#
sudo apt-key adv --keyserver keyserver.ubuntu.com --recv-keys E298A3A825C0D65DFD57CBB651716619E084DAB9
sudo add-apt-repository 'deb https://cloud.r-project.org/bin/linux/ubuntu xenial-cran35/'
sudo apt-get update

# Install R runtime.
#
sudo apt-get --no-install-recommends -y install r-base-core
```
## Install Rcpp package

 *** Is RStudio needed for this? Can be typed at prompt. ***

+ Locate installation directory for R
    /usr/lib/R/bin/

+ Start R
    /usr/lib/R/bin/R
        install.packages("Rcpp")

*** (there is a prompt that states "would you like to use a personable library", does that need to be included, or does that appear normally? are there permissions that need to be set in advance) ***


## Enable external script execution in SQL Server

An external script in R can be executed via the stored procedure [sp_execute_external script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) run again SQL Server. 
After setup, enable execution of external scripts, execute the following script using [Azure Data Studio.](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio)

After setup, enable execution of external scripts, execute the following script:

```sql
sp_configure 'external scripts enabled', 1;
RECONFIGURE WITH OVERRIDE;  
```
## Download runtime extension

**GitHub link goes here**

## Add R_HOME environment variable

Create the environment variable called R_HOME to point to the R installation location.

For logged sessions:


```bash
echo 'export R_HOME="/usr"' >> ~/.bash_profile
```

For non-logged sessions:

```bash
echo 'export R_HOME="/usr"' >> ~/.bashrc
source ~/.bashrc
```

>[!Note] 
>To use the R runtime provided with SQL Machine Learning Services, set R_HOME to /opt/mssql/mlservices/runtime/r.

## Create external language

Use SQL Server Management Studio or Azure Data Studio to connect to SQL Server.
Modify the path to reflect the location of the download.

>[!Note] 
>R is a reserved word. **Is this still true for R**.

```sql
CREATE EXTERNAL LANGUAGE myR
FROM (CONTENT = N'/users/username/R-lang-extension.zip', FILE_NAME = 'libRExtension.so.1.0',
ENVIRONMENT_VARIABLES = N'{"R_HOME": "/usr/lib/R"}');
GO
```

## Verify language extension

This script tests the functionality of the installed language extension. Use SQL Server Management Studio or Azure Data Studio to connect to SQL Server.

```sql
EXEC sp_execute_external_script
@language =N'myR',
@script=N'
import sys
print(sys.path)
print(sys.version)
print(sys.executable)
```

## Verify parameters and datasets of different data types

This script tests the installed language extension functionality, such as the exchange of different data types for input, output parameters, and datasets.

```sql
exec sp_execute_external_script 
@language = N'myR',
@script = N'print(''Hello RExtension!'');
OutputDataSet <- InputDataSet;
print(InputDataSet);
print(OutputDataSet);
print(R.version)',
@input_data_1 = N'select 1, cast(1.4 as real), ''Hi'', cast(''1'' as bit)'
WITH RESULT SETS ((intCol int, doubleCol real, charCol char(2), logicalCol bit))
```

## Next steps

+ [Extensibility framework in SQL Server](../concepts/extensibility-framework.md)
+ [Language Extensions Overview](../../language-extensions/language-extensions-overview.md)
