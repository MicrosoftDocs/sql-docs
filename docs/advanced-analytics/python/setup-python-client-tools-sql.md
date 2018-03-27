---
title: "Set up Python client tools for use with SQL Server | Microsoft Docs"
ms.custom: ""
ms.date: "03/13/2018"
ms.prod: "machine-learning-services"
ms.prod_service: "machine-learning-services"
ms.service: ""
ms.component: python
ms.reviewer: ""
ms.suite: "sql"
ms.technology: 
ms.tgt_pltfrm: ""
ms.topic: "article"
author: "HeidiSteen"
ms.author: "heidist"
manager: "cgronlun"
ms.workload: "Inactive"
---
# Set up Python client tools for use with SQL Server 
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes how to configure a Python environment on a Windows computer to support running Python code in SQL Server. This includes the following scenarios:

+ You test and develop solutions on a remote Python workstation, and send code to SQL Server for execution in a SQL Server compute context. 

    You generally want to install a full-featured Python development environment. 
    
    Your local Python environment should be compatible with the Python environment on the SQL Server instance, and you must install libraries that support remote compute contexts.

+ You develop and test your code using dedicated Python tools, and then migrate the code to SQL Server to run in a stored procedure.

    You use a full-featured Python IDE for development, off server. However, before you deploy your code, you test it in an environment that emulates the SQL Server environment.

+ You primarily run Python code in a stored procedure in SQL Server and you do not develop Python code. You expect that the code has been tested and debugged before you wrap it in a stored procedure. However, occasionally you might need to run the code outside SQL Server, to determine the source of a problem.

    You do not wish to install any Python tools on the server, and will use only the default tools installed with the distribution. You might decide to configure a Python environment that uses the instance library, to verify that code works outside of a stored procedure.

## Requirements

Regardless of which tools you use to develop your Python code, the following requirements apply whenever you run Python code in SQL Server or use SQL Server data:

+ To use Python, SQL Server 2017 or later is required. You must also install the feature that supports Machine Learning Services (In-Database), and explicitly enable the feature. For more information, see [Set up SQL Server 2017](../r/set-up-sql-server-r-services-in-database.md).

    If you installed an early release of SQL Server 2017, you might get errors if you try to run Python commands from this command line utility. We recommend that you [upgrade to the latest version](#bkmk_update) if possible. 

+ You must ensure that the account you use to run the code has permission to connect to the database and to run Python code. The special permission `EXECUTE ANY EXTERNAL SCRIPT` is required for all accounts that use R or Python script. 

+ You must ensure that the account has any database permissions that might be required to  read data or create new objects. 

+ If your code requires packages that are not installed by default with SQL Server, arrange with the database administrator to have the packages installed in the Python environment that is used by the instance. SQL Server is a secured environment and there are restrictions on where packages can be installed. 

    Ad hoc installation of packages as part of your code is not recommended, even if you have rights. Also, always carefully consider the security implications before installing new packages in the server library.

> [!NOTE]
> If you intend to use Python with Machine Learning Server, which supports additional compute contexts, such as Linux or Spark clusters, see these articles:
> 
> + [How to install custom Python packages and interpreter locally on Windows](https://docs.microsoft.com/machine-learning-server/install/python-libraries-interpreter)
> + [Link Python tools and IDEs to the Python interpreter installed with Machine Learning Server](https://docs.microsoft.com/machine-learning-server/python/quickstart-python-tools)

## Default tools included with standard install

A default installation of SQL Server 2017 with Machine Learning Services (In-database) and Python adds the following standard Python tools and resources:

+ Python sample data
+ Anaconda 4.2 distribution 
+ Python executables python.exe and pythonw.exe

By default, installation is to this folder: `~\Program Files\Microsoft SQL Server\<instance_name>\PYTHON_SERVICES`. 

## Open Python from the command line 

To use the Python runtime that is installed with the instance, you can start the Python executable from the installation path. Basic instructions are available on the [Python for Windows FAQ](https://docs.python.org/3/faq/windows.html).

> [!IMPORTANT]
> Generally, to avoid resource contention, we recommend that you **do not** run Python from the instance library on the server, if you think it is possible the SQL Server instance is running Python code. However, using the tools in the instance library might be valuable if you are trying to debug an issue that occurs only when running in SQL Server and want to view more detailed error messages, or make sure that all required packages are installed.

1. Navigate to the folder where the instance library is installed. For example, in a default installation, the folder is `C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES`.

2. Locate Python.exe. 

3. Right-click and select **Run as administrator** to open an interactive command-line window.

## <a name="bkmk_update"></a> Update Python components

You can update the Python components by downloading and installing a more recent version, using the script described here: [Install Python client on Windows](https://docs.microsoft.com/machine-learning-server/install/python-libraries-interpreter#install-on-windows)
> 
> The installer downloaded by the script is [SPO_9.3.0.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=859054&clcid=1033). If you need a different version, see [Installing machine learning components without internet access](http://bing.com)

## Set up a Python development environment

If you are simply debugging scripts from the command line, you can get by with the standard Python tools installed with Machine Learning Services, or use a text editor. However, if you are developing new solutions, or working from a remote client, we recommend use of a full-featured Python IDE. Popular options are:

+ [Visual Studio 2017 Community Edition](https://www.visualstudio.com/vs/features/python/) with Python
+ [AI tools for Visual Studio](https://docs.microsoft.com/visualstudio/ai/installation)
+ [Python in Visual Studio Code](https://code.visualstudio.com/docs/languages/python)
+ Popular third party tools such as PyCharm, Spyder, and Eclipse

We recommend Visual Studio because it supports database projects as well as machine learning projects. For help configuring a Python environment, see [Managing Python environments in Visual Studio](https://docs.microsoft.com/visualstudio/python/managing-python-environments-in-visual-studio)

## Install revoscalepy

Even if you have successfully installed Machine Learning Services, you might get an error when attempting to load **revoscalepy** functions from a Python command line. These steps describe how you can install an update to enable use of **revoscalepy**.

1. Download the installation shell script from https://aka.ms/mls93-py (or use https://aka.ms/mls-py for the 9.2. release). The script installs Anaconda 4.2.0, which includes Python 3.5.2, along with all packages listed previously.

2. Open a new PowerShell window with elevated permissions (as administrator).

3. Open the folder in which you downloaded the installer and run the script:

```ps1
cd {{download-directory}}
.\Install-PyForMLS.ps1
```

You can also run the `-InstallFolder` command-line argument and specify the new path as part of the command. For example: 

```ps1
.\Install-PyForMLS.ps1 -InstallFolder "<installation_path>")
```

If you get an error, you might need to suspend execution policies for a specific file, the duration of the session, as follows: 

```ps1
powershell -ExecutionPolicy Bypass -File "C:\<installation_path>\Install-PyForMLS.ps1"
```

You can also suspend execution policies for the duration of the session. With this statement, the execution policy  is set to `Unrestricted` for the duration of the session, and does not change the configuration or write the change to disk.

```ps1
Set-ExecutionPolicy Bypass -Scope Process
```

## Verify that Python and revoscalepy are working

After installing all tools and libraries, you should connect to the server and verify that you can create a compute context, or that Python can communicate with SQL Server.

### Verify that revoscalepy works from the Python command line

To verify that the **revoscalepy** module can be loaded, run the following sample code from a Python interactive command prompt. The code generates a data summary using the Python sample data and [rx_summary](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-summary). 

```Python
import os
from revoscalepy import rx_summary
from revoscalepy import RxXdfData
from revoscalepy import RxOptions
sample_data_path = RxOptions.get_option("sampleDataDir")
print(sample_data_path)
ds = RxXdfData(os.path.join(sample_data_path, "AirlineDemoSmall.xdf"))
summary = rx_summary("ArrDelay+DayOfWeek", ds)
print(summary)
```

The sample data path is printed so that you can determine which instance of Python is being called.

### Verify that Python can be called from SQL Server

To verify that Python is communicating with SQL Server, open SQL Server Management Studio. (You can use another similar tool, such as [SQL Operations Studio](https://docs.microsoft.com/sql/sql-operations-studio/what-is).) Open a new **Query** window and run any simple Python command in the context of a stored procedure:

```SQL
EXEC sp_execute_external_script @language = N'Python', 
@script = N'print(3+4)'
```

It can take a while to launch the Python run-time for the first time, but if there are no errors, you know that the SQL Server Launchpad is working, and Python can be started from SQL Server.

To verify that **revoscalepy** is available in the SQL Server instance library, run the script based on [rx_summary](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-summary), with some slight modifications to generate results compatible with SQL Server. 

```SQL
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

Because rx_summary returns an object of type `class revoscalepy.functions.RxSummary.RxSummaryResults`, which contains multiple elements, to handle the results in SQL Server, you can extract just the data frame in a tabular format.

### Verify Python execution in SQL Server as remote compute context

If you have installed **revoscalepy** in your local Python development environment, you should be able to connect to an instance of SQL Server 2017 where Python has been enabled, and run a similar code sample using the server as the compute context. 


```Python
import os
from revoscalepy import rx_summary, RxOptions, RxXdfData, RxSqlServerData, RxInSqlServer

# define connection string and compute context
sql_conn_string="Driver=SQL Server;Server=;Database=TestDB;Trusted_Connection=True"
sqlcc = RxInSqlServer(
    connection_string = sql_conn_string,
    num_tasks = 1,
    auto_cleanup = False,
    console_output = True,
    execution_timeout_seconds = 0,
    wait = True
    )
rx_set_compute_context(sqlcc)

# get sample data and path
sample_data_path = RxOptions.get_option("sampleDataDir")
print(sample_data_path)

# generate summary
ds = RxXdfData(os.path.join(sample_data_path, "AirlineDemoSmall.xdf"))
summary = rx_summary("ArrDelay+DayOfWeek", ds)
print(summary)
```

In this sample, the summary object is returned to the console, rather than returning well-formed tabular data to SQL Server. 

Also, because [rx_set_compute_context](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-set-compute-context) has been invoked, the sample data is loaded from the samples folder on the SQL Server computer, and not from your local samples folder.
