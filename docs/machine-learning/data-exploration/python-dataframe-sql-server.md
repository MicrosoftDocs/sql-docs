---
title: Insert Python dataframe into SQL table 
titleSuffix: SQL machine learning
description: How to insert data from a dataframe into SQL table.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 09/16/2021
ms.topic: how-to
ms.service: sql
ms.subservice: machine-learning
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current||=azuresqldb-current"
---
# Insert Python dataframe into SQL table
[!INCLUDE[SQL Server SQL DB SQL MI](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to insert a [pandas](https://pandas.pydata.org/) dataframe into a SQL database using the [pyodbc](../../connect/python/pyodbc/python-sql-driver-pyodbc.md) package in Python.

## Prerequisites

::: moniker range=">=sql-server-2017||>=sql-server-linux-ver15"
* [SQL Server for Windows](../../database-engine/install-windows/install-sql-server.md) or [for Linux](../../linux/sql-server-linux-overview.md)
::: moniker-end

::: moniker range="=azuresqldb-current"
* [Azure SQL Database](/azure/sql-database/sql-database-get-started-portal)
::: moniker-end

::: moniker range="=azuresqldb-mi-current"
* [Azure SQL Managed Instance](/azure/azure-sql/managed-instance/instance-create-quickstart)

* [SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md) for restoring the sample database to Azure SQL Managed Instance.
::: moniker-end

* Azure Data Studio. To install, see [Download and install Azure Data Studio](../../azure-data-studio/download-azure-data-studio.md).

* Follow the steps in [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md) to restore the OLTP version of the AdventureWorks sample database for your version of SQL Server.

  You can verify that the database was restored correctly by querying the **HumanResources.Department** table:

  ```sql
  USE AdventureWorks;
  SELECT * FROM HumanResources.Department;
  ```

## Install Python packages

1. In Azure Data Studio, open a new notebook and connect to the Python 3 kernel.

1. Select **Manage Packages**.

   :::image type="content" source="../media/python-dataframe-sql-server/manage-packages.png" alt-text="Manage packages":::

1. In the **Manage Packages** pane, select the **Add new** tab.

1. For each of the following packages, enter the package name, click **Search**, then click **Install**.
   * pyodbc
   * pandas

## Create a sample CSV file

Copy the following text and save it to a file named `department.csv`.

```text
DepartmentID,Name,GroupName,
1,Engineering,Research and Development,
2,Tool Design,Research and Development,
3,Sales,Sales and Marketing,
4,Marketing,Sales and Marketing,
5,Purchasing,Inventory Management,
6,Research and Development,Research and Development,
7,Production,Manufacturing,
8,Production Control,Manufacturing,
9,Human Resources,Executive General and Administration,
10,Finance,Executive General and Administration,
11,Information Services,Executive General and Administration,
12,Document Control,Quality Assurance,
13,Quality Assurance,Quality Assurance,
14,Facilities and Maintenance,Executive General and Administration,
15,Shipping and Receiving,Inventory Management,
16,Executive,Executive General and Administration
```

## Create a new database table

1. Follow the steps in [Connect to a SQL Server](../../azure-data-studio/quickstart-sql-server.md?view=sql-server-ver15&preserve-view=true#connect-to-a-sql-server) to connect to the AdventureWorks database.

1. Create a table named **HumanResources.DepartmentTest**. The SQL table will be used for the dataframe insertion.

   ```sql
   CREATE TABLE [HumanResources].[DepartmentTest](
   [DepartmentID] [smallint] NOT NULL,
   [Name] [dbo].[Name] NOT NULL,
   [GroupName] [dbo].[Name] NOT NULL
   )
   GO
   ```

## Load a dataframe from the CSV file

Use the Python `pandas` package to create a dataframe, load the CSV file, and then load the dataframe into the new SQL table, **HumanResources.DepartmentTest**.

1. Connect to the **Python 3** kernel.

1. Paste the following code into a code cell, updating the code with the correct values for `server`, `database`, `username`, `password`, and the location of the CSV file.

   ```Python
   import pyodbc
   import pandas as pd
   # insert data from csv file into dataframe.
   # working directory for csv file: type "pwd" in Azure Data Studio or Linux
   # working directory in Windows c:\users\username
   df = pd.read_csv("c:\\user\\username\department.csv")
   # Some other example server values are
   # server = 'localhost\sqlexpress' # for a named instance
   # server = 'myserver,port' # to specify an alternate port
   server = 'yourservername' 
   database = 'AdventureWorks' 
   username = 'username' 
   password = 'yourpassword' 
   cnxn = pyodbc.connect('DRIVER={SQL Server};SERVER='+server+';DATABASE='+database+';UID='+username+';PWD='+ password)
   cursor = cnxn.cursor()
   # Insert Dataframe into SQL Server:
   for index, row in df.iterrows():
        cursor.execute("INSERT INTO HumanResources.DepartmentTest (DepartmentID,Name,GroupName) values(?,?,?)", row.DepartmentID, row.Name, row.GroupName)
   cnxn.commit()
   cursor.close()
   ```

1. Run the cell.

## Confirm data in the database

Connect to the SQL kernel and AdventureWorks database and run the following SQL statement to confirm the table was successfully loaded with data from the dataframe.

```sql
SELECT count(*) from HumanResources.DepartmentTest;
```

Results

```bash
(No column name)
16
```

## Next steps

+ [Plot a histogram for data exploration with Python](../data-exploration/python-plot-histogram.md)