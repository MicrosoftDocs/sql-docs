---
title: Quickstart for verifying Python exists in SQL Server
description: Quickstart for verifying that Python and Machine Learning Services exist in SQL Server. 
ms.prod: sql
ms.technology: machine-learning

ms.date: 01/04/2019  
ms.topic: quickstart
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Quickstart: Verify Python exists in SQL Server 
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

SQL Server includes Python language support for data science analytics on resident SQL Server data. Script execution is through stored procedures, using either of the following approaches:

+ Built-in [sp_execute_external_script](https://docs.microsoft.com/sql/relational-databases/system-stored-procedures/sp-execute-external-script-transact-sql) stored procedure, passing Python script in as an input parameter.
+ Wrap Python script in a [custom stored procedure](sqldev-in-database-r-for-sql-developers.md) that you create.

In this quickstart, you will verify that [SQL Server 2017 Machine Learning Services](../what-is-sql-server-machine-learning.md) is installed and configured.

## Prerequisites

This exercise requires access to an instance of SQL Server with [SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) installed.

Your SQL Server instance can be in an Azure virtual machine or on-premises. Just be aware that the external scripting feature is disabled by default, so you might need to [enable external scripting](../install/sql-machine-learning-services-windows-install.md#bkmk_enableFeature) and verify that **SQL Server Launchpad service** is running before you start.

You also need a tool for running SQL queries. You can run the Python scripts using any database management or query tool, as long as it can connect to a SQL Server instance, and run a T-SQL query or stored procedure. This quickstart uses [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/sql/ssms/sql-server-management-studio-ssms).

## Verify Python exists

You can confirm that Machine Learning Services (is enabled for your SQL Server instance and which version of Python is installed. Follow the steps below.

1. Open SQL Server Management Studio and connect to your SQL Server instance.

2. Run the code below. 

    ```SQL
    EXECUTE sp_execute_external_script
    @language =N'Python',
    @script=N'import sys
    print(sys.version)';
    GO
    ```

3. The Python `print` function returns the version to the **Messages** window. In the example output below, you can see that SQL Server in this case have Python version 3.5.2 installed.

    **Results**

    ```text
    STDOUT message(s) from external script: 
    3.5.2 |Continuum Analytics, Inc.| (default, Jul  5 2016, 11:41:13) [MSC v.1900 64 bit (AMD64)]
    ```

If you get errors, there are a variety of things you can do to ensure that the instance and Python can communicate.

First, rule out any installation issues. Post-install configuration is required to enable use of external code libraries. See [Install SQL Server 2017 Machine Learning Services](../install/sql-machine-learning-services-windows-install.md). Likewise, make sure that the Launchpad service is running.

You must also add the Windows user group `SQLRUserGroup` as a login on the instance, to ensure that Launchpad can provide communication between Python and SQL Server. (The same group is used for both R and Python code execution.) For more information, see [Enabled implied authentication](../security/add-sqlrusergroup-to-database.md).

Additionally, you might need to enable network protocols that have been disabled, or open the firewall so that SQL Server can communicate with external clients. For more information, see [Troubleshooting setup](../common-issues-external-script-execution.md).

## Call revoscalepy functions

To verify that **revoscalepy** is available, run an example script that includes [rx_summary](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-summary) that produces a statistical summary data. The script below demonstrates how to retrieve a sample .xdf data file from built-in samples included in revoscalepy. The RxOptions function provides the **sampleDataDir** parameter that returns the location of the sample files.

Because rx_summary returns an object of type `class revoscalepy.functions.RxSummary.RxSummaryResults`, which contains multiple elements, you can use pandas to extract just the data frame in a tabular format.

```sql
EXEC sp_execute_external_script @language = N'Python', 
@script = N'
import os
from pandas import DataFrame
from revoscalepy import rx_summary
from revoscalepy import RxXdfData
from revoscalepy import RxOptions

sample_data_path = RxOptions.get_option("sampleDataDir")
print(sample_data_path)

ds = RxXdfData(os.path.join(sample_data_path, "AirlineDemoSmall.xdf"))
summary = rx_summary("ArrDelay + DayOfWeek", ds)
print(summary)
dfsummary = summary.summary_data_frame
OutputDataSet = dfsummary
'
WITH RESULT SETS  ((ColName nvarchar(25) , ColMean float, ColStdDev  float, ColMin  float,   ColMax  float, Col_ValidObs  float, Col_MissingObs int))
```

## List Python packages

Microsoft provides a number of Python packages pre-installed with Machine Learning Services in your SQL Server instance. To see a list of which Python packages are installed, including version, follow the steps below.

1. Run the script below on your SQL Server instance.

    ```SQL
    EXECUTE sp_execute_external_script
    @language =N'Python',
    @script=N'import pip
    for i in pip.get_installed_distributions():
        print(i)';
    GO
    ```

2. The output is from `pip.get_installed_distributions()` in Python and returned as `STDOUT` messages.

    **Results**

    ```text
    STDOUT message(s) from external script: 
    xlwt 1.2.0
    XlsxWriter 0.9.6
    xlrd 1.0.0
    win-unicode-console 0.5
    widgetsnbextension 2.0.0
    wheel 0.29.0
    Werkzeug 0.12.1
    wcwidth 0.1.7
    unicodecsv 0.14.1
    traitlets 4.3.2
    tornado 4.4.2
    toolz 0.8.2
    testpath 0.3
    tables 3.2.2
    sympy 1.0
    statsmodels 0.8.0
    sqlparse 0.1.19
    SQLAlchemy 1.1.9
    snowballstemmer 1.2.1
    six 1.10.0
    simplegeneric 0.8.1
    seaborn 0.7.1
    scipy 0.19.0
    scikit-learn 0.18.1
    ...
    ```

## Next steps

Now that you have confirmed your instance is ready to work with Python, take a closer look at a basic Python interaction.

> [!div class="nextstepaction"]
> [Quickstart: "Hello world" Python script in SQL Server](quickstart-python-run-using-t-sql.md)
