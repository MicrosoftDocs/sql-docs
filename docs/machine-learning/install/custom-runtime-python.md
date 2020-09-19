---
title: Install Python custom runtime
description: Learn how to install a Python custom runtime for SQL Server.
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 09/20/2020
ms.topic: how-to
author: cawrites
ms.author: chadam
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Install a Python custom runtime for SQL Server
[!INCLUDE [SQL Server 2019 and later](../../includes/applies-to-version/sqlserver2019.md)]

This article describes how to install a custom runtime for running Python scripts with SQL Server. The custom runtime for Python can be used with the following scenarios:

+ An installation of SQL Server with extensibility framework.

+ An installation of SQL Machine Learning Services for SQL Server 2019. The language extension can be used with [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) after completing some additional configuration steps.

::: moniker range=">=sql-server-ver15||=sqlallproducts-allversions"
> [!NOTE]
> This article describes how to install a custom runtime for Python on Windows. To install on Linux, see the [Install a custom Python runtime for SQL Server on Linux](custom-runtime-python.md?view=sql-server-linux-ver15&preserve-view=true)

## <a name="bkmk_prereqs"> </a> Pre-install checklist

+ [SQL Server 2019 for Windows CU3 or later.](../../database-engine/install-windows/install-sql-server.md)

  > [!Note]
  > Python custom runtime requires Cumulative Update (CU) 3 or later for SQL Server 2019.

+ [SQL Server Language Extensions on Windows with the extensibility framework.](../../language-extensions/install/install-sql-server-language-extensions-on-windows.md)

+ [Python3.7]( https://www.python.org/downloads/release/python-379/)

## Add custom runtime language extension

>[!Note]
>For Machine Learning Services using SQL Server 2019, the extensibility framework with launchpad service is already installed.

Language Extensions use the extensibility framework for executing external code. Code execution is isolated from the core engine processes, but fully integrated with SQL Server query execution.

Complete the setup for SQL Server 2019.

1. Start the setup wizard for SQL Server 2019. 
  
1. On the **Installation** tab, select **New SQL Server stand-alone installation or add features to an existing installation**.
    
    ![SQL Server 2019 CU3 or later installation](../install/media/2019setup-installation-page-mlsvcs.png) 

1. On the **Feature Selection** page, select these options:
  
    - **Database Engine Services**
  
        To use Language Extensions with SQL Server, you must install an instance of the database engine. You can use either a default or a named instance.
  
    - **Machine Learning Services and Language Extensions**
   
       Select **Machine Learning Services and Language Extensions** Python isn't selected.

    ![SQL Server 2019 CU3 or later installation features](../install/media/sql-feature-selection.png) 

1. On the **Ready to Install** page, verify that these selections are included, and select **Install**.
  
    + Database Engine Services
    + Machine Learning Services and Language Extensions

    Note of the location of the folder under the path `..\Setup Bootstrap\Log` where the configuration files are stored. When setup is complete, you can review the installed components in the Summary file.

1. After setup is complete, if you're instructed to restart the computer, do so now. It's important to read the message from the Installation Wizard when you've finished with Setup. For more information, see [View and Read SQL Server Setup Log Files](https://docs.microsoft.com/sql/database-engine/install-windows/view-and-read-sql-server-setup-log-files).


## Install Python 3.7 

[Complete installation of Python 3.7 and add to path.]( https://www.python.org/downloads/release/python-379/)

![Add Python 3.7 to path.](../install/media/python-379.png) update image - note

+ Install [Pandas](https://pandas.pydata.org/) package for Python 3.7

```bash
python.exe -m pip install pandas
```

## Enable external script execution in SQL Server

An external script in R can be executed via the stored procedure [sp_execute_external script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) run again SQL Server. Execute the following script using [Azure Data Studio.](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio)


```sql
sp_configure 'external scripts enabled', 1;
RECONFIGURE WITH OVERRIDE;  
```

## Download language extension

**GitHub link goes here**

## Update environment path for Windows

Add or modify PYTHONHOME as an environment variable.

+ In **Search** type **environment.** Select **Edit system environment variables.**
+ In the section System Variables.
Select **Advanced** tab.
Select **Environment Variables.**

+ Select **New** to create PYTHONHOME.
To modify select **Edit** to change PYTHONHOME.
Select **OK** to close remaining windows.

![Create PYTHONHOME system variable.](../install/media/sys-pythonhome.png)



>[!Note] 
>For existing SQL Machine Learning Services installations, modify the variable PYTHONHOME.

## Create external language

Use Azure Data Studio to connect to SQL Server.
Modify the path to reflect the location of the download.

>[!Note] 
>Python is a reserved word. It can't be used as a name for the external Python language extension.

```sql
CREATE EXTERNAL LANGUAGE mypython 
FROM (CONTENT = N'C:\Users\username\pythonextension.zip', FILE_NAME = 'pythonextension.dll');
GO
```
::: moniker-end

::: moniker range=">=sql-server-linux-ver15||=sqlallproducts-allversions"
You can install SQL Server on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu. For more information, see [the Supported platforms section in the Installation guidance for SQL Server on Linux](../../linux/sql-server-linux-setup.md).

> [!NOTE]
> This article describes how to install a custom runtime for Python on Linux. To install on Windows, see the [Install a custom Python runtime for SQL Server on Windows](custom-runtime-python.md?view=sql-server-ver15&preserve-view=true)

## <a name="bkmk_prereqs"> </a> Pre-install checklist

+ [SQL Server 2019 CU3 and later for Linux.](../../linux/sql-server-linux-setup.md)
When you install SQL Server on Linux, you must configure a Microsoft repository. For more information, see [configuring repositories](../../linux/sql-server-linux-change-repo.md)

  > [!Note]
  > Python custom runtime requires Cumulative Update (CU) 3 or later for SQL Server 2019.

+ [SQL Server Language Extensions on Linux with the extensibility framework.](../../linux/sql-server-linux-setup-language-extensions.md)

+ [Python3.7](https://www.python.org/downloads/release/python-379/)

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

## Install Python 3.7 and pandas

 Python runs in a separate process from SQL Server.

[Complete installation for Python 3.7](https://www.python.org/downloads/release/python-379/)

```bash
$ sudo add-apt-repository ppa:deadsnakes/ppa
$ sudo apt-get update
$ sudo apt-get install python3.7 python3-pip
```

```bash
# Install pandas to /usr/lib:
$ sudo python3.7 -m pip install pandas -t /usr/lib/python3.7/dist-packages
```

## Enable external script execution in SQL Server

An external script in Python can be executed via the stored procedure [sp_execute_external script](../../relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql.md) run again SQL Server. Execute the following script using [Azure Data Studio.](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio)

```sql
sp_configure 'external scripts enabled', 1;
RECONFIGURE WITH OVERRIDE;  
```
## Download runtime extension

**GitHub link goes here**

## Add PYTHONHOME environment variable

Create the environment variable called PYTHONHOME to point to the Python installation location.

For login sessions:


```bash
echo 'export PYTHONHOME="/usr"' >> ~/.bash_profile
```

For non-login sessions:

```bash
echo 'export PYTHONHOME="/usr"' >> ~/.bashrc
source ~/.bashrc
```

>[!Note] 
>To use the Python runtime provided with SQL Machine Learning Services, set PYTHONHOME to /opt/mssql/mlservices/runtime/python.

## Create external language

Use [Azure Data Studio](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio) to connect to SQL Server.
Modify the path to reflect the location of the download.

>[!Note]
>Python is a reserved word. It can't be used as a name for the external Python language extension.

```sql
CREATE EXTERNAL LANGUAGE mypython 
FROM (CONTENT = N'/PATH/TO/python-lang-extension.zip', FILE_NAME = 'libPythonExtension.so.1.0');
GO
```
::: moniker-end

## Verify language extension

This script tests the functionality of the installed language extension. Use SQL Azure Data Studio to connect to SQL Server.

```sql
EXEC sp_execute_external_script
@language =N'mypython',
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
@language = N'myPython',
@script = N'
import sys
print(''Hello PythonExtension!'');
OutputDataSet = InputDataSet;
print(InputDataSet);
print(OutputDataSet);
print(sys.version)',
@input_data_1 = N'select 1, cast(1.4 as real), ''Hi'', cast(''1'' as bit)'
WITH RESULT SETS ((intCol int, doubleCol real, charCol char(2), logicalCol bit))
```

## Next steps

+ [Extensibility framework in SQL Server](../concepts/extensibility-framework.md)
+ [Language Extensions Overview](../../language-extensions/language-extensions-overview.md)
