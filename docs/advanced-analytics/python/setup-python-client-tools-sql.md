---
title: Set up Python client tools for use with SQL Server Machine Learning | Microsoft Docs
ms.prod: sql
ms.technology: machine-learning

ms.date: 10/21/2018  
ms.topic: conceptual
author: HeidiSteen
ms.author: heidist
manager: cgronlun
---
# Set up Python client tools for use with SQL Server Machine Learning
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

Python integration is available starting in SQL Server 2017 or later, when you include the Python option in a Machine Learning Services (In-Database) installation. For more information, see [Install SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md).

In this article, learn how to configure a client development workstation so that you can connect to a remote SQL Server enabled for machine learning and Python integration. By completing the steps below, you will have the same Python libraries as those on the remote SQL Server instance.

> [!Tip]
> For a demonstration and video walkthrough, see [Run R and Python Remotely in SQL Server from Jupyter Notebooks or any IDE](https://blogs.msdn.microsoft.com/mlserver/2018/07/10/run-r-and-python-remotely-in-sql-server-from-jupyter-notebooks-or-any-ide/) or this [YouTube video](https://youtu.be/D5erljpJDjE).

## 1 - Install Python packages

Local workstations must have the same Python package versions as those on SQL Server, including the base distribution, and Microsoft-specific packages [revoscalepy](https://docs.microsoft.com//machine-learning-server/python-reference/revoscalepy/revoscalepy-package) and [microsftml](https://docs.microsoft.com/machine-learning-server/python-reference/microsoftml/microsoftml-package). 

The [azureml-model-management](https://docs.microsoft.com/en-us/machine-learning-server/python-reference/azureml-model-management-sdk/azureml-model-management-sdk) package is also installed, but it applies to operationalization tasks associated with a standalone (non-instance) Machine Learning Server context. For in-database analystics on a SQL Server instance, operationalization is through stored procedures.

1. Download the installation shell script:

  + [https://aka.ms/mls-py](https://aka.ms/mls-py) if SQL Server 2017 is not bound (common case). Choose this script if you aren't sure.

  + [https://aka.ms/mls93-py](https://aka.ms/mls93-py) if the remote SQL Server instance is [bound to Machine Learning Server 9.3](../r/use-sqlbindr-exe-to-upgrade-an-instance-of-sql-server).

  The script installs Anaconda 4.2.0, which includes Python 3.5.2, along with the three previously listed packages.

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

### On a standalone Python server

If you chose the [standalone server](../install/sql-machine-learning-standalone-windows-install) option in SQL Server setup, you have a Python server that is fully decoupled from a SQL Server database engine instance. Using a standalone server as a rich client is an option that some customers prefer. If you have a standalone server, SQL Server Setup added standard Python tools and resources, which you can find at this location:

1. Go to `C:\Program Files\Microsoft SQL Server\140\PYTHON_SERVER`.

2. Right-click **Python.exe** and select **Run as administrator** to open an interactive command-line window.

> [!Note]
> Generally, to avoid resource contention, we recommend that you **do not** run a standalone server and an instance-bound service on the same computer.

## 3 - Permissions

To connect to an instance of SQL Server to run scripts and upload data, you must have a valid login on the database server. You can use either a SQL login or integrated Windows authentication. We generally recommend that you use Windows integrated authentication, but using the SQL login is simpler for some scenarios, particularly when your script contains connection strings to external data.

At a minimum, the account used to run code must have permission to read from the databases you are working with, plus the special permission EXECUTE ANY EXTERNAL SCRIPT. Most developers also require permissions to create new objects in the form of stored procedures containing your script, and write data into tables containing training data or scored data. 

Ask the database administrator to configure the following permissions for the account, in the database where you use Python:

+ **EXECUTE ANY EXTERNAL SCRIPT** to run Python on the server.
+ **db_datareader** privileges to run the queries used for training the model.
+ **db_datawriter** to write training data or scored data.
+ **db_owner** to create objects such as stored procedures, tables, functions. 
  You also need **db_owner** to create sample and test databases. 

If your code requires packages that are not installed by default with SQL Server, arrange with the database administrator to have the packages installed with the instance. SQL Server is a secured environment and there are restrictions on where packages can be installed. Ad hoc installation of packages as part of your code is not recommended, even if you have rights. Also, always carefully consider the security implications before installing new packages in the server library.

## 4 - Test connections

After installing all tools and libraries, you should connect to the server and verify that you can load the libraries locally and remotely.

### Verify that revoscalepy works from the Python command line

To verify that the **revoscalepy** module can be loaded on the local client, run the following sample code from a Python interactive command prompt. The code generates a data summary using the Python sample data and [rx_summary](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-summary) function from the revoscalepy library.

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

### Verify Python execution in SQL Server as remote compute context

If you have installed **revoscalepy** in your local Python development environment, you should be able to connect to a remote instance of SQL Server 2017 where Python has been enabled, and run a similar code sample using the server as the compute context. 

The mechanism for connecting to a remote instance is the [rx_set_compute_context](https://docs.microsoft.com/machine-learning-server/python-reference/revoscalepy/rx-set-compute-context) in **revoscalepy**. Start by loading revoscalepy locally, and then define and set the remote compute context. The remaining code runs remotely on SQL Server.

For this script to run, specify a valid server name and an existing database. This particular script doesn't use the database, but the connection string requires it.

```Python
# Load libraries and functions locally
import os
from revoscalepy import rx_summary, rx_set_compute_context, RxOptions, RxXdfData, RxSqlServerData, RxInSqlServer

# Define connection string and remote compute context for SQL Server
sql_conn_string="Driver=SQL Server;Server=<server-name>;Database=<database-name>;Trusted_Connection=True"
sqlcc = RxInSqlServer(
    connection_string = sql_conn_string,
    num_tasks = 1,
    auto_cleanup = False,
    console_output = True,
    execution_timeout_seconds = 0,
    wait = True
    )
rx_set_compute_context(sqlcc)

# Remaining code now runs reomotely. This command gets sample data and path
sample_data_path = RxOptions.get_option("sampleDataDir")
print(sample_data_path)

# Generate a statistical summary of built-in sample data
ds = RxXdfData(os.path.join(sample_data_path, "AirlineDemoSmall.xdf"))
summary = rx_summary("ArrDelay+DayOfWeek", ds)
print(summary)
```
In this sample, the summary object is returned to the console. 

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