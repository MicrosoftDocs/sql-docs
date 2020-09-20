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

This article describes how to install a custom runtime for running R scripts with SQL Server. The custom runtime for R can be used with the following scenarios:

+ An installation of SQL Server with extensibility framework.

+ An installation of SQL Machine Learning Services for SQL Server 2019. The language extension can be used with [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) after completing some additional configuration steps.

::: moniker range=">=sql-server-ver15||=sqlallproducts-allversions"
> [!NOTE]
> This article describes how to install a custom runtime for R on Windows. To install on Linux, see the [Install an R custom runtime for SQL Server on Linux](custom-runtime-r.md?view=sql-server-linux-ver15&preserve-view=true)

<a name="pre_install_checklist"> </a>

## Pre-install checklist

+ [SQL Server 2019 for Windows (Cumulative Update 3 onwards).](../../database-engine/install-windows/install-sql-server.md)

  > [!Note]
  > R custom runtime requires Cumulative Update (CU) 3 or later for SQL Server 2019.

+ [SQL Server Language Extensions on Windows with the extensibility framework.](../../language-extensions/install/install-sql-server-language-extensions-on-windows.md)

+ [R Version 3.3 or higher](https://cran.r-project.org/)

## Add SQL Server Language Extensions for Windows

>[!Note]
>For Machine Learning Services using SQL Server 2019, the extensibility framework for language extensions is already installed.

Language Extensions use the extensibility framework for executing external code. Code execution is isolated from the core engine processes, but fully integrated with SQL Server query execution.

Complete the setup for SQL Server 2019.

1. Start the setup wizard for SQL Server 2019.

1. On the **Installation** tab, select **New SQL Server stand-alone installation or add features to an existing installation**.

    ![SQL Server 2019 installation CU3 or later](../install/media/2019setup-installation-page-mlsvcs.png)

1. On the **Feature Selection** page, select these options:

    - **Database Engine Services**

        To use Language Extensions with SQL Server, you must install an instance of the database engine. You can use either a default or a named instance.

    - **Machine Learning Services and Language Extensions**

       Select **Machine Learning Services and Language Extensions**. There's no need to select R.

    ![SQL Server 2019 CU3 or later installation features](../install/media/sql-feature-selection.png)

1. On the **Ready to Install** page, verify that these selections are included, and select **Install**.

    + Database Engine Services
    + Machine Learning Services and Language Extensions

1. After setup is complete, if you're instructed to restart the computer, do so now. It's important to read the message from the Installation Wizard when you've finished with Setup. For more information, see [View and Read SQL Server Setup Log Files](https://docs.microsoft.com/sql/database-engine/install-windows/view-and-read-sql-server-setup-log-files).


## Install R

>[!Note]
>For SQL Machine Learning Services, R is already installed in the R_SERVICES folder of your SQL Server instance e.g. "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\R_SERVICES". If you want to keep using this as your R_HOME, skip to the next step of installing Rcpp. Otherwise, if you want to use a different runtime of R, continue below to install it.

[Complete installation of R (>= 3.3)](https://cran.r-project.org/bin/windows/base/) and note the path where it's installed. This path is your **R_HOME**. For example, as shown below, R_HOME is "C:\Program Files\R\R-4.0.2"

![Install Custom R](../install/media/install-custom-r.png)

## Install Rcpp package

+ Locate the R executable in <R_HOME>\bin.

+ Start R from an *elevated* command prompt:

```CMD
<R_HOME>\bin\R.exe

e.g.
>C:\Program Files\R\R-4.0.2\bin\R.exe
```

In this *elevated* R prompt (<R_HOME>\bin\R.exe), run the following script where R_HOME is the path to your R installation as noted above. This script will install the Rcpp package in the <R_HOME>\library folder.

```R
install.packages("Rcpp", lib="<R_HOME>/library");

e.g.
>install.packages("Rcpp", lib="C:/Program Files/R/R-4.0.2/library");
```

## Update the system environment variables for Windows

Add **R_HOME** as an environment variable.
+ In **Search** type **environment.** Select **Edit the system environment variables.**
+ In the section System Variables.
Select **Advanced** tab.
Select **Environment Variables.**

+ Select **New** to create R_HOME.
To modify, select **Edit** to change it. Set its value to the R installation location you want to use.

![Create R_HOME system environment variable.](../install/media/sys-env-r-home.png)

Update the **PATH** environment variable.
+ Append the path to **R.dll** to the system **PATH** environment variable. To do that, select **PATH** then **Edit**
and add %R_HOME%\bin\x64 to the list of paths.

![Append to PATH system environment variable.](../install/media/sys-env-path-r.png)

Select **OK** to close remaining windows.

Alternatively, to set these environment variables from an *elevated* command prompt, run the following commands. Make sure to use the path to your installation of R.

```CMD
setx R_HOME "path\to\installation\of\R"
setx PATH "path\to\installation\of\R\bin\x64;%PATH%"

e.g.
setx R_HOME "C:\Program Files\R\R-4.0.2"
setx PATH "C:\Program Files\R\R-4.0.2\bin\x64;%PATH%"
```

## Grant access to non-default R_HOME folder

If you didn't install R in the default location "C:\Program Files\R\R-<version>", you need to do the following steps. Run the **icacls** commands from a new *elevated* command prompt to grant READ & EXECUTE access to the **SQL Server Launchpad Service user name** and SID **S-1-15-2-1** (**ALL_APPLICATION_PACKAGES**) for accessing the R_HOME. The launchpad service user name is of the form *NT Service\MSSQLLAUCHPAD$INSTANCENAME* where INSTANCENAME is the instance name of your SQL Server.
The commands will recursively grant access to all files and folders under the given directory path.

1. Give permissions to SQL Server Launchpad Service user name

    For a named instance, append the instance name to MSSQLLAUNCHPAD (for example, `MSSQLLAUNCHPAD$INSTANCENAME`).

    ```cmd
    icacls "%R_HOME%" /grant "NT Service\MSSQLLAUCHPAD$INSTANCENAME":(OI)(CI)RX /T

    e.g.
    icacls "C:\Program Files\R\R-4.0.2" /grant "NT Service\MSSQLLAUCHPAD$MSSQLSERVER":(OI)(CI)RX /T
    ```

    You can skip this step if you installed the R_HOME in the default folder under Program Files on Windows.

2. Give AppContainer permissions

    ```cmd
    icacls "%R_HOME%" /grant *S-1-15-2-1:(OI)(CI)RX /T

    e.g.
    icacls "C:\Program Files\R\R-4.0.2" /grant *S-1-15-2-1:(OI)(CI)RX /T
    ```

>Note
>The above command grants permissions to the computer SID S-1-15-2-1, which is equivalent to ALL APPLICATION PACKAGES on an English version of Windows. Alternatively, you can use icacls "%R_HOME%" /grant "ALL APPLICATION PACKAGES":(OI)(CI)RX /T on an English version of Windows.

## Restart SQL Server Launchpad service

Find the name of the SQL Server Launchpad Service. It is of the form MSSQLLAUCHPAD$INSTANCENAME where INSTANCENAME is the instance name of your SQL Server. From an *elevated* command prompt, run the following commands.

```CMD
net stop <Name of the SQL Server Launchpad Service>
net start <Name of the SQL Server Launchpad Service>

e.g.
net stop MSSQLLAUNCHPAD$MSSQLSERVER
net start MSSQLLAUNCHPAD$MSSQLSERVER
```

Alternatively, right-click the SQL Server Launchpad service in the **Services** app of the system and click the **Restart** command. Or use [SQL Server Configuration Manager](../../relational-databases/sql-server-configuration-manager.md) to restart the service.

## Download language extension

Download the zip file containing the R language extension (R-lang-extension.zip) from [here.](**GitHub link goes here**)

## Register external language

For each database you want to use this R language extension in, you need to register it with CREATE EXTERNAL LANGUAGE.
Use [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio) to connect to SQL Server and run the following T-SQL. Modify the path in this statement to reflect the location of the downloaded language extension zip file (R-lang-extension.zip) from above.

>[!Note]
>R is a reserved word. So use a different name for the external language e.g. myR.

```sql
CREATE EXTERNAL LANGUAGE [myR]
FROM (CONTENT = N'/path/to/R-lang-extension.zip', FILE_NAME = 'libRExtension.dll');
GO
```

::: moniker-end

::: moniker range=">=sql-server-linux-ver15||=sqlallproducts-allversions"
You can install SQL Server on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu. For more information, see [the Supported platforms section in the Installation guidance for SQL Server on Linux](../../linux/sql-server-linux-setup.md).

> [!NOTE]
> This article describes how to install a custom runtime for R on Linux. To install on Windows, see the [Install an R custom runtime for SQL Server on Windows](custom-runtime-r.md?view=sql-server-ver15&preserve-view=true)

<a name="pre_install_checklist"> </a>

## Pre-install checklist

+ [SQL Server 2019 for Linux (Cumulative Update 3 onwards).](../../linux/sql-server-linux-setup.md)
Before you install SQL Server on Linux, you must configure a Microsoft repository. For more information, see [configuring repositories](../../linux/sql-server-linux-change-repo.md)

 > [!Note]
  > R custom runtime requires Cumulative Update (CU) 3 or later for SQL Server 2019.

+ [SQL Server Language Extensions on Linux with the extensibility framework.](../../linux/sql-server-linux-setup-language-extensions.md)

+ [R Version 3.3 or higher](https://cran.r-project.org/)

## Add SQL Server Language Extensions for Linux

>[!Note]
>For Machine Learning Services using SQL Server 2019, the mssql-server-extensibility package for language-extensions is already installed.

Language Extensions use the extensibility framework for executing external code. Code execution is isolated from the core engine processes, but fully integrated with SQL Server query execution.

## Ubuntu
> [!Tip]
> If possible, `sudo apt-get update` to refresh packages on the system prior to installation. Ubuntu might not have the https apt transport option. To install it, use `sudo apt-get install apt-transport-https`.
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

## Install R

>[!Note]
>For SQL Machine Learning Services, R is already installed in /opt/microsoft/ropen/3.5.2/lib64/R. If you want to keep using this as your R_HOME, skip to the next step of installing Rcpp. Otherwise, if you want to use a different runtime of R, you first need to remove microsoft-r-open-mro before continuing to install a new version:
Example for Ubuntu:
>```bash
>sudo apt remove microsoft-r-open-mro-3.5.2
>```

[Complete installation of R (>= 3.3)](https://cran.r-project.org/bin/linux/) by following the instructions for your respective linux platform. By default, R is installed in **/usr/lib/R**. This path is your **R_HOME**. If you install R in a different location, take note of that path as your R_HOME.

Example instructions for Ubuntu. Change the repository URL below for your version of R.

```bash
export DEBIAN_FRONTEND=noninteractive
sudo apt-get update
sudo apt-get --no-install-recommends -y install curl zip unzip apt-transport-https libstdc++6

# Add R CRAN repository. This repository works for R 4.0.x
#
sudo apt-key adv --keyserver keyserver.ubuntu.com --recv-keys E298A3A825C0D65DFD57CBB651716619E084DAB9
sudo add-apt-repository 'deb https://cloud.r-project.org/bin/linux/ubuntu xenial-cran40/'
sudo apt-get update

# Install R runtime.
#
sudo apt-get --no-install-recommends -y install r-base-core
```

## Install Rcpp package

+ Locate the R binary in <R_HOME>/bin. By default, it is in /usr/lib/R/bin.

+ Start R

```bash
sudo <R_HOME>/bin/R

e.g.
sudo /usr/lib/R/bin/R
```

+ In this *elevated* R prompt (<R_HOME>/bin/R), run the following script where R_HOME is the path to your R installation as noted above. This script will install the Rcpp package in the <R_HOME>/library folder.

```R
install.packages("Rcpp", lib = "<R_HOME>/library");

e.g.
install.packages("Rcpp", lib = "/usr/lib/R/library");
```

## Add R_HOME environment variable to mssql-launchpadd service config

>[!Note]
>If you have installed R in the default location of /usr/lib/R, you can skip this step.

1. Edit mssql-launchpadd service.

```bash
sudo systemctl edit mssql-launchpadd
```

2. Insert the following text in the file that opens. Set value of R_HOME to the R installation you want to use.

```vi editor
[Service]
Environment="R_HOME=/path/to/your/R/installation/folder"
```

Save and close.

>[!Note]
>To keep using the R runtime provided with SQL Machine Learning Services, set R_HOME to /opt/microsoft/ropen/3.5.2/lib64/R.

## Grant access to non-default R_HOME folder

>[!Note]
>If you have installed R in the default location of /usr/lib/R, you can skip this step.

Set the datadirectories option in the extensibility section of /var/opt/mssql/mssql.conf file. Replace R_HOME with the R installation you want to use.

```bash
sudo /opt/mssql/bin/mssql-conf set extensibility.datadirectories <R_HOME>
```

## Restart mssql-launchpadd service

```bash
sudo systemctl restart mssql-launchpadd
```

## Download runtime extension

Download the zip file containing the R language extension (R-lang-extension.zip) from [here.](**GitHub link goes here**)

## Register external language

For each database you want to use this R language extension in, you need to register it with CREATE EXTERNAL LANGUAGE.
Use [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio) to connect to SQL Server and run the following T-SQL. Modify the path in this statement to reflect the location of the downloaded language extension zip file (R-lang-extension.zip) from above.

>[!Note]
>R is a reserved word. So use a different name for the external language e.g. myR.

```sql
CREATE EXTERNAL LANGUAGE [myR]
FROM (CONTENT = N'/path/to/R-lang-extension.zip', FILE_NAME = 'libRExtension.so.1.0');
GO
```

::: moniker-end

## Enable external script execution in SQL Server

An external script in R can be executed via the stored procedure [sp_execute_external script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) run against SQL Server. Run the following T-SQL using [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio) to enable execution of this stored procedure.

```sql
sp_configure 'external scripts enabled', 1;
RECONFIGURE WITH OVERRIDE;
```

## Verify language extension

This script verifies the successful installation of the custom language extension. Output of this script should display the R_HOME, path to R and the version of the custom R runtime confirming that the script is using that.

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

This script tests different data types for input/output parameters, and datasets.

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
