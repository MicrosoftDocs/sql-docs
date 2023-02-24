---
title: Install packages with Python tools
description: Learn how to use standard Python tools to install new Python packages to an instance of SQL Server Machine Learning Services.
ms.service: sql
ms.subservice: machine-learning-services
ms.date: 10/05/2021
ms.topic: how-to
author: WilliamDAssafMSFT
ms.author: wiassaf
monikerRange: "=sql-server-2017"
ms.custom:
  - intro-installation
---
# Install packages with Python tools on SQL Server
[!INCLUDE [SQL Server 2017 only](../../includes/applies-to-version/sqlserver2017-only.md)]

This article describes how to use standard Python tools to install new Python packages on an instance of SQL Server Machine Learning Services. In general, the process for installing new packages is similar to that in a standard Python environment. However, some additional steps are required if the server does not have an Internet connection.

For more information about package location and installation paths, see [Get Python package information](python-package-information.md).

## Prerequisites

+ You must have [SQL Server Machine Learning Services](../install/sql-machine-learning-services-windows-install.md) installed with the Python language option.

### Other considerations

+ Packages must be Python 3.5-compliant and run on Windows.

+ The Python package library is located in the Program Files folder of your SQL Server instance and, by default, installing in this folder requires administrator permissions. For more information, see [Package library location](../package-management/python-package-information.md#default-python-library-location).

+ Package installation is per instance. If you have multiple instances of Machine Learning Services, you must add the package to each one.

+ Database servers are frequently locked down. In many cases, Internet access is blocked entirely. For packages with a long list of dependencies, you will need to identify these dependencies in advance and be ready to install each one manually.

+ Before adding a package, consider whether the package is a good fit for the SQL Server environment.

  + We recommend that you use Python in-database for tasks that benefit from tight integration with the database engine, such as machine learning, rather than tasks that simply query the database.

  + If you add packages that put too much computational pressure on the server, performance will suffer.

  + On a hardened SQL Server environment, you might want to avoid the following:
    + Packages that require network access
    + Packages that require elevated file system access
    + Packages used for web development or other tasks that don't benefit by running inside SQL Server

## Add a Python package on SQL Server

To install a new Python package that can be used in a script on SQL Server, you install the package in the instance of Machine Learning Services. If you have multiple instances of Machine Learning Services, you must add the package to each one.

The package installed in the following examples is [CNTK](/cognitive-toolkit/), a framework for deep learning from Microsoft that supports customization, training, and sharing of different types of neural networks.

### For offline install, download the Python package

If you are installing Python packages on a server with no Internet access, you must download the WHL file from a computer with Internet access and then copy the file to the server.

For example, on an Internet-connected computer you can download a `.whl` file for CNTK and then copy the file to a local folder on the SQL Server computer. See [Install CNTK from Wheel Files](/cognitive-toolkit/setup-windows-python?tabs=cntkpy26#2-install-from-wheel-files) for a list of available `.whl` files for CNTK.

> [!IMPORTANT]
> Make sure that you get the Windows version of the package. If the file ends in .gz, it's probably not the right version.

For more information about downloads of the CNTK framework for multiple platforms and for multiple versions of Python, see [Setup CNTK on your machine](/cognitive-toolkit/Setup-CNTK-on-your-machine).

### Locate the Python library

Locate the default Python library location used by SQL Server. If you have installed multiple instances, locate the `PYTHON_SERVICES` folder for the instance where you want to add the package.

For example, if Machine Learning Services was installed using defaults, and machine learning was enabled on the default instance, the path is:

```console
cd "C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES"
```

> [!TIP]
> For future debugging and testing, you might want to set up a Python environment specific to the instance library.

### Install the package using pip

Use the **pip** installer to install new packages. You can find `pip.exe` in the `Scripts` subfolder of the `PYTHON_SERVICES` folder. SQL Server Setup does not add the `Scripts` subfolder to the system path, so you must specify the full path, or you can add the Scripts folder to the PATH variable in Windows.

> [!NOTE]
> If you're using Visual Studio 2017, or Visual Studio 2015 with the Python extensions, you can run `pip install` from the **Python Environments** window. Click **Packages**, and in the text box, provide the name or location of the package to install. You don't need to type `pip install`; it is filled in for you automatically.

+ If the computer has Internet access, provide the name of the package:

  ```console
  scripts\pip.exe install cntk
  ```
  You can also specify the URL of a specific package and version, for example:

  ```console
  scripts\pip.exe install https://cntk.ai/PythonWheel/CPU-Only/cntk-2.1-cp35-cp35m-win_amd64.whl
  ```

+ If the computer does not have Internet access, specify the WHL file you downloaded earlier. For example:

  ```console
  scripts\pip.exe install C:\Downloads\cntk-2.1-cp35-cp35m-win_amd64.whl
  ```

You might be prompted to elevate permissions to complete the install.
As the installation progresses, you can see status messages in the command prompt window.

### Load the package or its functions as part of your script

When installation is complete, you can immediately begin using the package in Python scripts in SQL Server.

To use functions from the package in your script, insert the standard `import <package_name>` statement in the initial lines of the script:

```sql
EXECUTE sp_execute_external_script 
  @language = N'Python', 
  @script = N'
import cntk
# Python statements ...
'
```

## See also

+ [Get Python package information](python-package-information.md)
+ [Python tutorials for SQL Server Machine Learning Services](../tutorials/python-tutorials.md)
+ [Python API for CNTK](https://cntk.ai/pythondocs/tutorials.html).
