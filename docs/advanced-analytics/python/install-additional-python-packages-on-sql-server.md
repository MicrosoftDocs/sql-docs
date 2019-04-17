---
title: Install new Python language packages -  SQL Server Machine Learning
description: Add new Python packages to SQL Server 2017 Machine Learning Services (In-Database), and Machine Learning Server (Standalone).
ms.prod: sql
ms.technology: machine-learning

ms.date: 05/10/2018  
ms.topic: conceptual
author: dphansen
ms.author: davidph
manager: cgronlun
---
# Install new Python packages on SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article describes how to install new Python packages on an instance of SQL Server 2017 Machine Learning Services. In general, the process for installing new packages is similar to that in a standard Python environment. However, some additional steps are required if the server does not have an internet connection.

For more information about package location and installation paths, see [Get R or Python package information](../r/determine-which-packages-are-installed-on-sql-server.md).

## Prerequisites

+ [SQL Server 2017 Machine Learning Services (In-Database)](../install/sql-machine-learning-services-windows-install.md) with the Python language option. 

+ Packages must be Python 3.5-compliant and run on Windows. 

+ Administrative access to the server is required to install packages.

## Considerations

Before adding packages, consider whether the package is a good fit for the SQL Server environment. Typically a database server is a shared asset accommodating multiple workloads. If you add packages that put too much computational pressure on the server, performance will suffer. 

Additionally, Some popular Python packages (like Flask) perform tasks, such as web development, that are better suited for a standalone environment. We recommend that you use Python in-database for tasks that benefit from tight integration with the database engine, such as machine learning,  rather than tasks that simply query the database.

Database servers are frequently locked down. In many cases, Internet access is blocked entirely. For packages with a long list of dependencies, you will need to identify these dependencies in advance and be willing to install each one manually.

## Add a new Python package

For this example, we assume that you want to install a new package directly on the SQL Server computer.

Package installation is per instance. If you have multiple instances of Machine Learning Services, you must add the package to each one.

The package installed in this example is [CNTK](https://docs.microsoft.com/cognitive-toolkit/), a framework for deep learning from Microsoft that supports customization, training, and sharing of different types of neural networks.

### Step 1. Download the Windows version of the Python package

+ If you are installing Python packages on a server with no internet access, you must download the WHL file to a different computer and then copy it to the server.

    For example, on a separate computer, you can download the WHL file from this site [https://cntk.ai/PythonWheel/CPU-Only](https://cntk.ai/PythonWheel/CPU-Only/cntk-2.1-cp35-cp35m-win_amd64.whl), and then copy the file `cntk-2.1-cp35-cp35m-win_amd64.whl` to a local folder on the SQL Server computer.

+ SQL Server 2017 uses Python 3.5. 

> [!IMPORTANT]
> Make sure that you get the Windows version of the package. If the file ends in .gz, it's probably not the right version.

This page contains downloads for multiple platforms and for multiple Python versions: [Set up CNTK](https://docs.microsoft.com/cognitive-toolkit/Setup-CNTK-on-your-machine)

### Step 2. Open a Python command prompt

Locate the default Python library location used by SQL Server. If you have installed multiple instances, locate the PYTHON_SERVICE folder for the instance where you want to add the package.

For example, if Machine Learning Services has been installed using defaults, and machine learning is enabled on the default instance, the path would be as follows:

    `C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES`

Open the Python command prompt associated with the instance.

> [!TIP]
> For future debugging and testing, you might want to set up a Python environment specific to the instance library.

### Step 3. Install the package using pip

+ If you are accustomed to using the Python command line, use PIP.exe to install new packages. You can find the **pip** installer in the `Scripts` subfolder. 

  SQL Server Setup does not add Scripts to the system path. If you get an error that `pip` is not recognized as an internal or external command, you can add the Scripts folder to the PATH variable in Windows.

  The full path of the **Scripts** folder in a default installation is as follows:

    C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\Scripts

+ If you are using Visual Studio 2017, or Visual Studio 2015 with the Python extensions, you can run `pip install` from the **Python Environments** window. Click **Packages**, and in the text box, provide the name or location of the package to install. You don't need to type `pip install`; it is filled in for you automatically. 

    - If the computer has Internet access, provide the name of the package, or the URL of a specific package and version. 
    
    For example, to install the version of CNTK that is supported for Windows and Python 3.5, specify the download URL: `https://cntk.ai/PythonWheel/CPU-Only/cntk-2.1-cp35-cp35m-win_amd64.whl`

    - If the computer does not have internet access, you must download the WHL file before beginning installation. Then, specify the local file path and name. For example, paste the following path and file to install the WHL file downloaded from the site: 
    `"C:\Downloads\CNTK\cntk-2.1-cp35-cp35m-win_amd64.whl"`

You might be prompted to elevate permissions to complete the install.

As installation progresses, you can see status messages in the command prompt window:

```python
pip install https://cntk.ai/PythonWheel/CPU-Only/cntk-2.1-cp35-cp35m-win_amd64.whl
Collecting cntk==2.1 from https://cntk.ai/PythonWheel/CPU-Only/cntk-2.1-cp35-cp35m-win_amd64.whl
  Downloading https://cntk.ai/PythonWheel/CPU-Only/cntk-2.1-cp35-cp35m-win_amd64.whl (34.1MB)
    100% |################################| 34.1MB 13kB/s
...
Installing collected packages: cntk
Successfully installed cntk-2.1
```


### Step 4. Load the package or its functions as part of your script

When installation is complete, you can immediately begin using the package as described in the next step.

For examples of deep learning using CNTK, see these tutorials: [Python API for CNTK](https://cntk.ai/pythondocs/tutorials.html)

To use functions from the package in your script, insert the standard `import <package_name>` statement in the initial lines of the script:

```python
import numpy as np
import cntk as cntk
cntk._version_
```

## List installed packages using conda

There are different ways that you can get a list of installed packages. For example, you can view the installed packages in the **Python Environments** windows of Visual Studio.

If you are using the Python command line, you can use either **Pip** or the **conda** package manager, included with the Anaconda Python environment added by SQL Server setup.

1. Go to C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\Scripts

1. Right-click **conda.exe** > **Run as administrator**, and enter `conda list` to return a list of packages installed in the current environment.

1. Similarly, right-click **pip.exe** > **Run as administrator**, and enter `pip list` to return the same information. 

For more information about **conda** and how you can use it to create and manage multiple Python environments, see [Managing environments with conda](https://conda.io/docs/user-guide/tasks/manage-environments.html).

> [!Note]
> SQL Server Setup does not add Pip or Conda to the system path and on a production SQL Server instance, keeping non-essential executables out of the path is a best practice. However, for development and test environments, you could add the Scripts folder to the system PATH environment variable to run both Pip and Conda on command from any location.
