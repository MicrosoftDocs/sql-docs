---
title: Install new Python language packages
description: Add new Python packages to SQL Server Machine Learning Services.
ms.prod: sql
ms.technology: machine-learning

ms.date: 08/02/2019
ms.topic: conceptual
author: dphansen
ms.author: davidph
monikerRange: ">=sql-server-2017||=sqlallproducts-allversions"
---

# Install new Python packages on SQL Server

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

This article describes how to install new Python packages on an instance of SQL Server Machine Learning Services. In general, the process for installing new packages is similar to that in a standard Python environment. However, some additional steps are required if the server does not have an internet connection.

For more information about package location and installation paths, see [Get R or Python package information](../package-management/installed-package-information.md).

## Prerequisites

+ [SQL Server Machine Learning Services (In-Database)](../install/sql-machine-learning-services-windows-install.md) with the Python language option.

+ Packages must be Python 3.5-compliant and run on Windows.

+ You'll need administrative access to the server to install packages.

Other considerations:

+ Package installation is per instance. If you have multiple instances of Machine Learning Services, you must add the package to each one.

+ Before adding packages, consider whether the package is a good fit for the SQL Server environment. If you add packages that put too much computational pressure on the server, performance will suffer.

+ We recommend that you use Python in-database for tasks that benefit from tight integration with the database engine, such as machine learning, rather than tasks that simply query the database.

## Add a new Python package

1. Download the Windows version of the Python package. If you're installing Python packages on a server with no internet access, download the WHL file to a different computer and then copy it to the server.

1. Open a Python command prompt. Python is located in the PYTHON_SERVICE folder for the instance where you want to add the package.

   For example, if Machine Learning Services has been installed using defaults, and machine learning is enabled on the default instance, the path would be:

   ::: moniker range="=sql-server-2017||=sqlallproducts-allversions"
   `C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\PYTHON_SERVICES\library`
   ::: moniker-end

   ::: moniker range=">sql-server-2017||=sqlallproducts-allversions"
   `C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\PYTHON_SERVICES\library`
   ::: moniker-end

1. Install the package using pip. You can find the **pip** installer in the `Scripts` subfolder of PYTHON_SERVICES.

   > [NOTE!]
   > SQL Server Setup does not add `Scripts` to the system path. If you get an error that `pip` is not recognized as an internal or external command, you can add the `Scripts` folder to the **PATH** environment variable in Windows.

   If you are using Visual Studio with the Python extensions, you can run `pip install` from the **Python Environments** window. Click **Packages**, and in the text box, provide the name or location of the package to install. You don't need to type `pip install`, it is filled in for you automatically.

   If the computer has Internet access, provide the name of the package, or the URL of a specific package and version. If the computer doesn't have internet access, download the WHL file before beginning installation and then specify the local file path and name.

1. Load the package or its functions as part of your script. To use functions from the package in your script, insert the standard `import <package_name>` statement in the initial lines of the script.
