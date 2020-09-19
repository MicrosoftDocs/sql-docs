---
title: Install R custom runtime
description: Learn how to install an R custom runtime for SQL Server.
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 09/20/2020
ms.topic: how-to
author: cawrites
ms.author: chadam
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Install an R custom runtime for SQL Server

[!INCLUDE [SQL Server 2019 and later](../../includes/applies-to-version/sqlserver2019.md)]

This article describes how to install a custom runtime for running R scripts with SQL Server. The custom runtime for Python can be used with the following scenarios:

+ An installation of SQL Server with extensibility framework.

+ An installation of SQL Machine Learning Services for SQL Server 2019. The language extension can be used with [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) after completing some additional configuration steps.

::: moniker range=">=sql-server-ver15||=sqlallproducts-allversions"
> [!NOTE]
> This article describes how to install a custom runtime for R on Windows. To install on Linux, see the [Install an R custom runtime for SQL Server on Linux](custom-runtime-r.md?view=sql-server-linux-ver15&preserve-view=true)

## <a name="bkmk_prereqs"> </a> Pre-install checklist

+ [SQL Server 2019 for Windows.](../../database-engine/install-windows/install-sql-server.md)

  > [!Note]
  > Python custom runtime requires Cumulative Update (CU) 3 or later for SQL Server 2019.

+ [SQL Server Language Extensions on Windows with the extensibility framework.](../../language-extensions/install/install-sql-server-language-extensions-on-windows.md)

+ [R Version 3.3 or higher](https://cran.r-project.org/)

## Add SQL Server Language Extensions for Windows

>[!Note]
>Language Extensions use the extensibility framework for executing external code. Code execution is isolated from the core engine processes, but fully integrated with SQL Server query execution.

Complete the setup for SQL Server 2019.

1. Start the setup wizard for SQL Server 2019. 
  
1. On the **Installation** tab, select **New SQL Server stand-alone installation or add features to an existing installation**.
    
    ![SQL Server 2019 installation CU3 or later](../install/media/2019setup-installation-page-mlsvcs.png) 

1. On the **Feature Selection** page, select these options:
  
    - **Database Engine Services**
  
        To use Language Extensions with SQL Server, you must install an instance of the database engine. You can use either a default or a named instance.
  
    - **Machine Learning Services and Language Extensions**
   
       Select **Machine Learning Services and Language Extensions** R isn't selected.

    ![SQL Server 2019 CU3 or later installation features](../install/media/sql-feature-selection.png) 

1. On the **Ready to Install** page, verify that these selections are included, and select **Install**.
  
    + Database Engine Services
    + Machine Learning Services and Language Extensions

    Note of the location of the folder under the path `..\Setup Bootstrap\Log` where the configuration files are stored. When setup is complete, you can review the installed components in the Summary file.

1. After setup is complete, if you're instructed to restart the computer, do so now. It's important to read the message from the Installation Wizard when you've finished with Setup. For more information, see [View and Read SQL Server Setup Log Files](https://docs.microsoft.com/sql/database-engine/install-windows/view-and-read-sql-server-setup-log-files).


## Install R 

[Complete installation of R and add to path.](https://cran.r-project.org/)

## Install Rcpp package

In R command prompt, run the following command where R_HOME is the path to your R installation.

```bash
install.packages("Rcpp", lib="<R_HOME>/library")
```

>[!Note]
>Optionally [RStudio Desktop.](https://rstudio.com/products/rstudio/download/) Select **Tools** > **Install Packages** > Type **Rcpp**. Select **Install**.


After setup, enable execution of external scripts, execute the following script:

```sql
sp_configure 'external scripts enabled', 1;
RECONFIGURE WITH OVERRIDE;  
```
## Download language extension

**GitHub link goes here**

## Update environment path for Windows

Add R_HOME as an environment variable. 
+ In **Search** type **environment.** Select **Edit system environment variables.**
+ In the section System Variables.
Select **Advanced** tab.
Select **Environment Variables.**

+ Select **New** to create R_HOME.
To modify select **Edit** to change R_HOME.

+ To append to Path select **Path** then **Edit**

```bash
%R_HOME%\bin\x64
```
Select **OK** to close remaining windows.

## Restart SQL launchpad service
Since we aren't using SSMS how did you want to define this step?

![Create R_HOME system variable.](../install/media/sys-env-r-home.png)


>[!Note] 
>For SQL Machine Learning Services, set R_HOME to the location of your R_SERVICES folder. e.g. C:\Program Files\Microsoft SQL Server<InstanceName>\R_SERVICES"

## Enable external script execution in SQL Server

An external script in R can be executed via the stored procedure [sp_execute_external script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) run again SQL Server. Execute the following script using [Azure Data Studio.](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio)

Modify the path to reflect the location of the download.

>[!Note] 
>R is a reserved word.

```sql
CREATE EXTERNAL LANGUAGE [myR]
FROM (CONTENT = N'\Path\to\\downloaded\R-lang-extension.zip', FILE_NAME = 'libRExtension.dll');
GO
```
::: moniker-end

::: moniker range=">=sql-server-linux-ver15||=sqlallproducts-allversions"
You can install SQL Server on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu. For more information, see [the Supported platforms section in the Installation guidance for SQL Server on Linux](../../linux/sql-server-linux-setup.md).

> [!NOTE]
> This article describes how to install a custom runtime for R on Linux. To install on Windows, see the [Install an R custom runtime for SQL Server on Windows](custom-runtime-r.md?view=sql-server-ver15&preserve-view=true)

## <a name="bkmk_prereqs"> </a> Pre-install checklist

+ [SQL Server 2019 CU3 and later for Linux.](../../linux/sql-server-linux-setup.md)
Before you install SQL Server on Linux, you must configure a Microsoft repository. For more information, see [configuring repositories](../../linux/sql-server-linux-change-repo.md)

  > [!Note]
  > Python custom runtime requires Cumulative Update (CU) 3 or later for SQL Server 2019.

+ [SQL Server Language Extensions on Linux with the extensibility framework.](../../linux/sql-server-linux-setup-language-extensions.md)

+ [R Version 3.3 or higher](https://cran.r-project.org/)

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

+ Locate installation directory for R
    /usr/lib/R/bin/

+ Start R
   sudo /usr/lib/R/bin/R

+ Install Rcpp as follows where lib is the path to the library folder under R_HOME."

```bash
install.packages("Rcpp", lib = "/usr/lib/R/library")
```

## Enable external script execution in SQL Server

An external script in R can be executed via the stored procedure [sp_execute_external script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) run again SQL Server. Execute the following script using [Azure Data Studio.](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio)

After setup, to enable execution of external scripts, execute the following script:

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
echo 'export R_HOME="/usr/lib/R"' >> ~/.bash_profile
```

For non-logged sessions:

```bash
echo 'export R_HOME="/usr/lib/R"' >> ~/.bashrc
source ~/.bashrc
```


>[!Note]
>If you have installed R in the default location of /usr/lib/R, you can skip this step.
If you haven't, complete the following steps:

1. Edit configuration file.

```bash
sudo systemctl edit mssql-launchpadd
```

1. Edit file and insert the following text:

```bash
[Service]
Environment="R_HOME=/path/to/your/R/installation/folder"
```

Save and close.


1. Start launchpadd

```bash
sudo systemctl restart mssql-launchpadd
```

>[!Note] 
>To use the R runtime provided with SQL Machine Learning Services, set R_HOME to /opt/microsoft/ropen/3.5.2/lib64/R.

## Create external language

Use Azure Data Studio to connect to SQL Server.
Modify the path to reflect the location of the download.

>[!Note] 
>R is a reserved word. **Is this still true for R**.

```sql
CREATE EXTERNAL LANGUAGE myR
FROM (CONTENT = N'/path/to/R-lang-extension.zip', FILE_NAME = 'libRExtension.so.1.0');
GO
```

::: moniker-end

## Verify language extension

This script tests the functionality of the installed language extension. Use Azure Data Studio to connect to SQL Server.

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
