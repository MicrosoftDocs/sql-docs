---
title: Install your own R language extension for Windows
description: Learn how to install a language extension for R for SQL Server.
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 09/20/2020
ms.topic: how-to
author: cawrites
ms.author: chadam
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-ver15||=sqlallproducts-allversions"
---
# Install language extension for R for Windows

[!INCLUDE [SQL Server 2019 and later](../../includes/applies-to-version/sqlserver2019.md)]

>[!Note] 
>R language extension to bring your own runtime runs on SQL Server 2019 CU3 or later
 
This article describes how to install the language extension for running R scripts with [SQL Server for Windows.](../../database-engine/install-windows/install-sql-server.md)

The language extension can be used with the following scenarios:

+ An installation of SQL Server with extensibility framework.

+ An installation of SQL Machine Learning Services for SQL Server 2019. The language extension can be used with [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) after completing some additional configuration steps.

## <a name="bkmk_prereqs"> </a> Pre-install checklist

+ [SQL Server 2019 for Windows.](../../database-engine/install-windows/install-sql-server.md)

+ [SQL Server Language Extensions on Windows with the extensibility framework.](../../language-extensions/install/install-sql-server-language-extensions-on-windows.md)

+ [SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio.](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio)

+ [R Version 3.3 or higher](https://cran.r-project.org/)

+ [R Studio ](https://rstudio.com/products/rstudio/download/) for executing R code.

## Add SQL Server Language Extensions for Windows

>[!Note]
>For Machine Learning Services using SQL Server 2019, **mssql-server-extensibility is already installed.**

Language Extensions use the extensibility framework for executing external code. Code execution is isolated from the core engine processes, but fully integrated with SQL Server query execution.

Complete the setup for SQL Server 2019.

1. Start the setup wizard for SQL Server 2019. 
  
1. On the **Installation** tab, select **New SQL Server stand-alone installation or add features to an existing installation**.
    
    ![SQL Server 2019 installation](../install/media/2019setup-installation-page-mlsvcs.png) 

1. On the **Feature Selection** page, select these options:
  
    - **Database Engine Services**
  
        To use Language Extensions with SQL Server, you must install an instance of the database engine. You can use either a default or a named instance.
  
    - **Machine Learning Services and Language Extensions**
   
       Select **Machine Learning Services and Language Extensions** R isn't selected.

    ![SQL Server 2019 installation features](../install/media/sql-feature-selection.png) 

1. On the **Ready to Install** page, verify that these selections are included, and select **Install**.
  
    + Database Engine Services
    + Machine Learning Services and Language Extensions

    Note of the location of the folder under the path `..\Setup Bootstrap\Log` where the configuration files are stored. When setup is complete, you can review the installed components in the Summary file.

1. After setup is complete, if you're instructed to restart the computer, do so now. It's important to read the message from the Installation Wizard when you've finished with Setup. For more information, see [View and Read SQL Server Setup Log Files](https://docs.microsoft.com/sql/database-engine/install-windows/view-and-read-sql-server-setup-log-files).


## Install R - Do these steps work for any version of R?

[Complete installation of R and add to path.](https://cran.r-project.org/)


## Install Rcpp package

Start RStudio Desktop. Select **Tools** > **Install Packages** > Type **Rcpp**

![RStudio Desktop](../install/media/rcapp.png) rename file

Select **Install**.

## Enable external script execution in SQL Server

An external script is a stored procedure used by R against SQL Server. Use SQL Server Management Studio or Azure Data Studio to connect to SQL Server.

After setup, enable execution of external scripts, execute the following script:

```sql
sp_configure 'external scripts enabled', 1;
RECONFIGURE WITH OVERRIDE;  
```
## Download language extension

**GitHub link goes here**

## Update environment path for Windows

Add R_HOME as an environment variable. Path modified during installation. For example: C:\Program Files\R\R-3.6.3
![Create R_HOME system variable.](../install/media/sys-env-r-home.png)

>[!Note] 
>For SQL Machine Learning Services a new path for the language extension will need to be created.

## Create external language

Use SQL Server Management Studio or Azure Data Studio to connect to SQL Server.
Modify the path to reflect the location of the download.

>[!Note] 
>R is a reserved word. **Is this a true statement for R?**.

```sql
CREATE EXTERNAL LANGUAGE [myR]
FROM (CONTENT = N'D:\Data-SQL-Language-Extensions\build-output\RExtension\target\debug\R-lang-extension.zip', FILE_NAME = 'libRExtension.dll');
GO
```
**Removed environment variable in Create External Language. Confirm?** 

## Verify language extension

This script tests the installed language extension functionality, such as the exchange of different data types for input, output parameters, and datasets.

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

The scripts tests the function of different data types.

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
