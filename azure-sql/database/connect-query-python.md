---
title: Use Python to query a database
titleSuffix: Azure SQL Database & SQL Managed Instance
description: This article shows you how to use Python to create a program that connects to a database in Azure SQL Database and query it using Transact-SQL statements.
author: dzsquared
ms.author: drskwier
ms.reviewer: wiassaf, mathoma
ms.date: 11/11/2022
ms.service: sql-database
ms.subservice: connect
ms.topic: quickstart
ms.custom:
  - "seo-python-october2019"
  - "sqldbrb=2"
  - "devx-track-python"
  - "mode-api"
  - "py-fresh-zinc"
ms.devlang: python
monikerRange: "= azuresql || = azuresql-db || = azuresql-mi"
---
# Quickstart: Use Python to query a database

[!INCLUDE[appliesto-sqldb-sqlmi](../includes/appliesto-sqldb-sqlmi-asa.md)]

In this quickstart, you use Python to connect to Azure SQL Database, Azure SQL Managed Instance, or Synapse SQL database and use T-SQL statements to query data.

## Prerequisites

To complete this quickstart, you need:

- An Azure account with an active subscription. [Create an account for free](https://azure.microsoft.com/free/?ref=microsoft.com&utm_source=microsoft.com&utm_medium=docs&utm_campaign=visualstudio).

- A database where you'll run a query.

  [!INCLUDE[create-configure-database](../includes/create-configure-database.md)]

- [Python](https://python.org/downloads) 3 and related software

    |**Action**|**macOS**|**Ubuntu**|**Windows**|
    |----------|-----------|------------|---------|
    |Install the ODBC driver, SQLCMD, and the Python driver for SQL Server|Use steps **1.2**, **1.3**, and **2.1** in [create Python apps using SQL Server on macOS](https://www.microsoft.com/sql-server/developer-get-started/python/mac/). These steps will also install Homebrew and Python.<br/><br/>Although the linked article references SQL Server, these steps are also applicable to Azure SQL Database, Azure SQL Managed Instance, and Azure Synapse Analytics. |[Configure an environment for pyodbc Python development](/sql/connect/python/pyodbc/step-1-configure-development-environment-for-pyodbc-python-development#linux)|[Configure an environment for pyodbc Python development](/sql/connect/python/pyodbc/step-1-configure-development-environment-for-pyodbc-python-development#windows).|
    |Install Python and other required packages|    |Use `sudo apt-get install python python-pip gcc g++ build-essential`.|    |
    |Further information|[Microsoft ODBC driver on macOS](/sql/connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server)  |[Microsoft ODBC driver on Linux](/sql/connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server)|[Microsoft ODBC driver on Linux](/sql/connect/odbc/linux-mac/installing-the-microsoft-odbc-driver-for-sql-server)|

To further explore Python and the database in Azure SQL Database, see [Azure SQL Database libraries for Python](/python/api/overview/azure/sql), the [pyodbc repository](https://github.com/mkleehammer/pyodbc/wiki/), and a [pyodbc sample](https://github.com/mkleehammer/pyodbc/wiki/Getting-started).

## Create code to query your database 

1. In a text editor, create a new file named *sqltest.py*.

1. Add the following code. Get the connection information from the prerequisites section and substitute your own values for \<server>, \<database>, \<username>, and \<password>.

   ```python
   import pyodbc
   server = '<server>.database.windows.net'
   database = '<database>'
   username = '<username>'
   password = '{<password>}'   
   driver= '{ODBC Driver 17 for SQL Server}'
   
   with pyodbc.connect('DRIVER='+driver+';SERVER=tcp:'+server+';PORT=1433;DATABASE='+database+';UID='+username+';PWD='+ password) as conn:
       with conn.cursor() as cursor:
           cursor.execute("SELECT TOP 3 name, collation_name FROM sys.databases")
           row = cursor.fetchone()
           while row:
               print (str(row[0]) + " " + str(row[1]))
               row = cursor.fetchone()
   ```

## Run the code

1. At a command prompt, run the following command:

   ```cmd
   python sqltest.py
   ```

1. Verify that the databases and their collations are returned, and then close the command window.

    If you receive an error:
    - Verify that the server name, database name, username, and password you're using are correct.
    - Verify that the ODBC driver you installed is the same version as the `driver` variable in the code above. For example, the code shows 17, but you may have installed a different version.
    - If you're running the code from a local environment, verify that the firewall of the Azure resource you're trying to access is configured to allow access from your environment's IP address.

## Next steps

- [Design your first database in Azure SQL Database](design-first-database-tutorial.md)
- [Microsoft Python drivers for SQL Server](/sql/connect/python/python-driver-for-sql-server/)
- [Python developer center](https://azure.microsoft.com/develop/python/?v=17.23h)
