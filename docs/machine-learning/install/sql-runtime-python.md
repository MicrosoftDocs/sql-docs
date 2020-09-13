---
title: Install your own runtime for Python in Linux
description: Learn how to install your own runtime for Python  for SQL Server.
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 09/20/2020
ms.topic: how-to
author: cawrites
ms.author: chadam
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Install runtime for Python 

[!INCLUDE [SQL Server 2019 and later](../../includes/applies-to-version/sqlserver2019.md)]

>Note! The Python and runtime extension runs on SQL Server 2019 or later.
 
This article describes how to install the language extension for running Python scripts with SQL Server. You can install SQL Server on Red Hat Enterprise Linux (RHEL), SUSE Linux Enterprise Server (SLES), and Ubuntu. For more information, see [the Supported platforms section in the Installation guidance for SQL Server on Linux](../../linux/sql-server-linux-setup.md).

The runtime can be used with [SQL Server Machine Learning Services](../sql-server-machine-learning-services.md) after completing some additional configuration steps.

The runtime language extension can be used with the following scenarios:

+ An installation of SQL Server with extensibility framework.

+ An existing installation of Machine Learning Services with the mssql-mlservices-packages installed in Linux. Additional steps are needed to use the language extension.

## <a name="bkmk_prereqs"> </a> Pre-install checklist

+ [SQL Server 2019 of Linux.](../../linux/sql-server-linux-setup.md) 

+ [SQL Server Language Extensions on Linux with the extensibility framework.](../../linux/sql-server-linux-setup-language-extensions.md)

+ [SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md) or [Azure Data Studio.](https://docs.microsoft.com/sql/azure-data-studio/download-azure-data-studio)

+ [Python3.7](https://www.python.org/)

## Add SQL Server Language Extensions on Linux

Language Extensions uses the extensibility framework for executing external code. Code execution is isolated from the core engine processes, but fully integrated with SQL Server query execution.

> [!Tip]
> If possible, run `apt-get update` to refresh packages on the system prior to installation. Additionally, some docker images of Ubuntu might not have the https apt transport option. To install it, use `apt-get install apt-transport-https`.

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

## Python 3.7

 Python runs in a separate process from SQL Server.

[Complete installation of Python 3.7](https://www.python.org/)

+ Install [Pandas](https://pandas.pydata.org/) package for Python 3.7

## Enable external script in SQL Server

An external script is a stored procedure used by Python against SQL Server. Use SQL Server Management Studio or Azure Data Studio to connect to SQL Server.

After setup, to enable external scripts, execute the following script:

```sql
sp_configure 'external scripts enabled', 1;
RECONFIGURE WITH OVERRIDE;  
```
## Download runtime extension

**GitHub link goes here**

## Update environment path in Linux

Add PYTHONHOME as an environment variable, and modify the PATH.

>[NOTE!] For SQL Machine Learning Services a new path for the runtime will need to be created. Example

## Create external language

Use SQL Server Management Studio or Azure Data Studio to connect to SQL Server.
Modify the path to reflect the location of the download.

>[Note!] Python is a reserved word. It can't be used as a name for the external Python runtime.

```sql
CREATE EXTERNAL LANGUAGE mypython 
FROM (CONTENT = N'/PATH/TO/python-lang-extension.zip', FILE_NAME = 'libPythonExtension.so.1.0', ENVIRONMENT_VARIABLES = N'{"PYTHONHOME":"/usr/"}' );
GO

EXEC sp_execute_external_script
@language =N'mypython',
@script=N'
import sys
print(sys.path)
print(sys.version)
print(sys.executable)
```

## Verify runtime language extension

The script will test that the runtime extension is installed properly in Linux.
Use SQL Server Management Studio or Azure Data Studio to connect to SQL Server.

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

```sql
**Results shown here**
```

## Next steps

+ [Extensibility framework in SQL Server](../concepts/extensibility-framework.md)
+ [Language Extensions Overview](../../language-extensions/language-extensions-overview.md)
