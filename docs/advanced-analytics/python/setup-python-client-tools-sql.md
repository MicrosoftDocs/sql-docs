---
title: Set up Python client tools for use with SQL Server Machine Learning | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/21/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Set up Python client tools for use with SQL Server Machine Learning
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Python integration is available starting in SQL Server 2017 or later, when you add Python support to Machine Learning Services (In-Database). For more information, see [Install SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md).

In this article, learn how to configure development workstations so that you can connect to a remote SQL Server enabled for Python.

### Evaluation and independent development
 
If you have the developer edition and plan to work locally on Python script you plan to move to SQL Server, you can skip ahead to [Install an IDE](#install-ide) and point the tool to local Python libraries used by SQL Server.

> [!Tip]
> For a demonstration and video walkthrough, see [Run R and Python Remotely in SQL Server from Jupyter Notebooks or any IDE](https://blogs.msdn.microsoft.com/mlserver/2018/07/10/run-r-and-python-remotely-in-sql-server-from-jupyter-notebooks-or-any-ide/) or this [YouTube video](https://youtu.be/D5erljpJDjE).

## 1 - Install Python packages

Local workstations must have the same Python package versions as those on SQL Server: revoscalepy and microsftml. Additional Python packages are available, but are more commonly used with other scenarios in a standalone (non-instance) Machine Learning Server context. 

1. Download the installation shell script from [https://aka.ms/mls93-py](https://aka.ms/mls93-py) (or use [https://aka.ms/mls-py](https://aka.ms/mls-py) for the 9.2. release). 

  The script installs Anaconda 4.2.0, which includes Python 3.5.2, along with all packages listed previously.

  Python components are provided through [SPO_9.3.0.0_1033.cab](https://go.microsoft.com/fwlink/?LinkId=859054&clcid=1033). If you need a different version, see [CAB downloads](../install/sql-ml-cab-downloads.md)

2. Open PowerShell window with elevated administrator permissions (right-click **Run as administrator**).

3. Go to the folder in which you downloaded the installer and run the script. Add the `-InstallFolder` command-line argument to specify a folder location for the libraries. For example: 

   ```python
   cd {{download-directory}}
   .\Install-PyForMLS.ps1 -InstallFolder "C:\path-to-python-for-mls"
   ```

   Installation takes some time to complete. You can monitor progress in the PowerShell window. When setup is finished, you have a complete set of packages. For example, if you specified `C:\mspythonlibs` as the folder name, you would find the packages at `C:\mspythonlibs\Lib\site-packages`. Otherwise, the default folder is `C:\Program Files\Microsoft\PyForMLS1`.

The installation script does not modify the PATH environment variable on your computer so the new python interpreter and modules you just installed are not automatically available to your tools. For help on linking the Python interpreter and libraries to tools, see [5 - Install an IDE](#install-ide).

<a name="python-tool"></a>
 
## 2 - Open a Python prompt

Python integration in Microsoft includes built-in tools and data in addition to the product-specific libraries like revoscalepy and microsoftml. The following items are available on both client and server instances when setup is complete:

+ Python sample data
+ Anaconda 4.2 distribution 
+ Python executables python.exe and pythonw.exe

> [!Tip] 
> We recommend the [Python for Windows FAQ](https://docs.python.org/3/faq/windows.html) for general purppose information on running Python programs on Windows.

### On client workstations

To use the Python executable installed by the setup script:

1. Go to `C:\Program Files\Microsoft\PyForMLS\python.exe` or whatever location you chose for installation path.

2. Right-click **Python.exe** and select **Run as administrator** to open an interactive command-line window.

### On SQL Server

SQL Server Setup adds standard Python tools and resources to a server instance. If you are using the developer edition and want to check Python version or run ad hoc commands:

1. Go to `C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES`.

2. Right-click **Python.exe** and select **Run as administrator** to open an interactive command-line window.

> [!Note]
> Generally, to avoid resource contention, we recommend that you **do not** run Python from the instance library on the server, if you think it is possible the SQL Server instance is running Python code. However, using the tools in the instance library might be valuable if you are trying to debug an issue that occurs only when running in SQL Server and want to view more detailed error messages, or make sure that all required packages are installed.

## 3 - Permissions

To connect to an instance of SQL Server to run scripts and upload data, you must have a valid login on the database server. You can use either a SQL login or integrated Windows authentication. We generally recommend that you use Windows integrated authentication, but using the SQL login is simpler for some scenarios.

At a minimum, the account used to run code must have permission to read from the databases you are working with, plus the special permission EXECUTE ANY EXTERNAL SCRIPT. Most developers also require permissions to create new objects in the form of stored procedures containing your script, and write data into tables containing training data or scored data. 

Ask the database administrator to configure the following permissions for the account, in the database where you use Python:

+ **EXECUTE ANY EXTERNAL SCRIPT** to run Python on the server.
+ **db_datareader** privileges to run the queries used for training the model.
+ **db_datawriter** to write training data or scored data.
+ **db_owner** to create objects such as stored procedures, tables, functions. 
  You also need **db_owner** to create sample and test databases. 

If your code requires packages that are not installed by default with SQL Server, arrange with the database administrator to have the packages installed with the instance. SQL Server is a secured environment and there are restrictions on where packages can be installed. Ad hoc installation of packages as part of your code is not recommended, even if you have rights. Also, always carefully consider the security implications before installing new packages in the server library.

## 4 - Test connections

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

### Verify that Python can be called from a local SQL Server

On a local development environment, verify that Python is communicating with local [SQL Server instance configured for external scripting](../install/sql-machine-learning-services-windows-install.md). Use SQL Server Management Studio to open a new **Query** window and run any simple Python command in the context of a stored procedure:

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

If you have installed **revoscalepy** in your local Python development environment, you should be able to connect to a remote instance of SQL Server 2017 where Python has been enabled, and run a similar code sample using the server as the compute context. 

For this script to run, specify a valid server name and an existing database. This particular script doesn't use the database, but the connection string requires it.

```Python
import os
from revoscalepy import rx_summary, RxOptions, RxXdfData, RxSqlServerData, RxInSqlServer

# define connection string and compute context
sql_conn_string="Driver=SQL Server;Server=<server-name>;Database=TestDB;Trusted_Connection=True"
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


<a name="install-ide"></a>

## 5 - Install an IDE

If you are simply debugging scripts from the command line, you can get by with the standard Python tools. However, if you are developing new solutions, or working from a remote client, we recommend use of a full-featured Python IDE. Popular options are:

+ [Visual Studio 2017 Community Edition](https://www.visualstudio.com/vs/features/python/) with Python
+ [AI tools for Visual Studio](https://docs.microsoft.com/visualstudio/ai/installation)
+ [Python in Visual Studio Code](https://code.visualstudio.com/docs/languages/python)
+ Popular third-party tools such as PyCharm, Spyder, and Eclipse

We recommend Visual Studio because it supports database projects as well as machine learning projects. For help configuring a Python environment, see [Managing Python environments in Visual Studio](https://docs.microsoft.com/visualstudio/python/managing-python-environments-in-visual-studio).

Because developers frequently work with multiple versions of Python, setup does not add Python to your PATH. To use the Python executable and libraries installed by setup, link your IDE to **Python.exe** at the path that also provides revoscalepy and microsoftml. For example, for a Python project in Visual Studio, your custom environment would specify `C:\Program Files\Microsoft\PyForMLS`, `C:\Program Files\Microsoft\PyForMLS\python.exe` and `C:\Program Files\Microsoft\PyForMLS\pythonw.exe` for **Prefix path**, **Interpreter path**, and **Windowed interpreter**, respectively.

For additional guidance, see [Link Python tools and IDEs](https://docs.microsoft.com/machine-learning-server/python/quickstart-python-tools). The article is written for Microsoft Machine Learning Server so the Python paths are different, but it shows you how to link to Python libraries from various tools.

## Next steps

Now that you have tools and a working connection to SQL Server, step through a tutorial to get a closer look at revoscalepy functions and switching compute contexts.

> [!div class="nextstepaction"]
> [Create a model using revoscalepy and a remote compute context](../tutorials/use-python-revoscalepy-to-create-model.md)