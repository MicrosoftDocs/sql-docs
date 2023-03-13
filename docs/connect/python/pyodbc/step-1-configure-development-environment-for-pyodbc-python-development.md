---
title: "Step 1: Configure pyodbc Python environment"
description: "Step 1 of this getting started guide involves installing Python, the Microsoft ODBC Driver for SQL Server, and pyODBC into your development environment."
author: David-Engel
ms.author: v-davidengel
ms.date: 08/29/2022
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
---
# Step 1: Configure development environment for pyodbc Python development

This article explains how to configure your development environment for pyodbc Python development.

## Windows

1. **Download Python installer**. If your machine doesn't have Python, install it. Go to the [**Python download page**](https://www.python.org/downloads/windows/) and download the appropriate installer. For example, if you are on a 64-bit machine, download the Python 3.10 (x64) installer.  
  
2. **Install Python**. Once the installer is downloaded, do the following steps:

   1. Double-click the file to start the installer.
   1. Select your language, and agree to the terms.
   1. Follow the instructions on the screen to install Python on your computer.
   1. You can verify that Python is installed by going to a command prompt and running `python -V`. You can also search for Python in the start menu.

3. [**Install the Microsoft ODBC Driver for SQL Server on Windows**.](../../odbc/windows/system-requirements-installation-and-driver-files.md#installing-microsoft-odbc-driver-for-sql-server)
  
4. **Open cmd.exe as an administrator**.

5. **Install pyodbc using pip - Python package manager**.

   ```cmd
   pip install pyodbc  
   ```

## Linux

Installing on Linux is similar. If the instructions below don't work, see the [pyODBC Install instructions](https://github.com/mkleehammer/pyodbc/wiki/Install), which have more details for different Linux distributions.

1. **Open terminal**.

2. [**Install Microsoft ODBC Driver for SQL Server on Linux**.](../../odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server.md)

3. **Install pyodbc**.  

   ```bash  
   sudo -H pip install pyodbc
   ```

## Next steps

[Create an SQL database for pyodbc](step-2-create-a-sql-database-for-pyodbc-python-development.md).
