---
title: Insert Python dataframe into SQL table 
description: How to insert data from a dataframe into SQL table
author: cawrites
ms.author: chadam
ms.date: 07/14/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: machine-learning
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current||=azuresqldb-current||=sqlallproducts-allversions"
---
# Insert Python dataframe into SQL table
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/applies-to-version/sql-asdb-asdbmi-asa.md)]

This article describes how to insert data into a SQL database from a `pandas` dataframe using the `pyodbc` package in Python. For more information, see the [pyodbc documentation](../../connect/python/pyodbc/python-sql-driver-pyodbc.md). By establishing a connection with SQL using Python `pandas`, data can be sent directly to a SQL table.

## Prerequisites:

::: moniker range=">=sql-server-2017||>=sql-server-linux-ver15||=sqlallproducts-allversions"
* SQL Server. For how to install, see [SQL Server for Windows](../../database-engine/install-windows/install-sql-server.md) or [for Linux](../../linux/sql-server-linux-overview.md).
::: moniker-end

::: moniker range="=azuresqldb-current||=sqlallproducts-allversions"
* Azure SQL Database. For how to sign up, see [Azure SQL Database](https://docs.microsoft.com/azure/sql-database/sql-database-get-started-portal)
::: moniker-end

::: moniker range="=azuresqldb-mi-current||=sqlallproducts-allversions"
* Azure SQL Managed Instance. For how to sign up, see [Azure SQL Managed Instance](https://docs.microsoft.com/azure/azure-sql/managed-instance/instance-create-quickstart).

* [SQL Server Management Studio](../../ssms/download-sql-server-management-studio-ssms.md) for restoring the sample database to Azure SQL Managed Instance.
::: moniker-end

* Azure Data Studio. For how to install, see [Azure Data Studio](../../azure-data-studio/what-is.md).


## Restore the sample database

The sample database used in this article has been saved to a **.bak** database backup file for you to download and use.
::: moniker range=">=sql-server-ver15||>=sql-server-linux-ver15||=azuresqldb-current||=sqlallproducts-allversions"
1. Follow the instructions in [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md#download-bak-files) to download the correct OLTP version of the AdventureWorks file and restore it as a database. This database will be used as a datasource.
1. Follow the directions in [Restore a database from a backup file](../../azure-data-studio/tutorial-backup-restore-sql-server.md#restore-a-database-from-a-backup-file) in Azure Data Studio, using these details:
   - Import from the **AdventureWorks.bak** file - you downloaded.
   - Name the target database "AdventureWorks".
::: moniker-end   
::: moniker range="=azuresqldb-mi-current||=sqlallproducts-allversions"
1. Follow the instructions in [AdventureWorks sample databases](../../samples/adventureworks-install-configure.md#download-bak-files) to download the correct OLTP version of the AdventureWorks file and restore it as a database. This database will be used as a datasource.
1. Follow the directions in [Restore a database to a Managed Instance](/azure/sql-database/sql-database-managed-instance-get-started-restore) in SQL Server Management Studio, using these details:
   - Import from the **AdventureWorks.bak** file - you downloaded.
   - Name the target database "AdventureWorks".
::: moniker-end

You can verify that the restored database exists by querying the **HumanResources.Department** table:

```sql
USE AdventureWorks;
SELECT * FROM HumanResources.Department;
```

## Install Python packages

Install the following Python packages using [Azure Data Studio notebook with a Python kernel](../../azure-data-studio/notebooks-tutorial-python-kernel.md)

  * pyodbc
  * pandas

  To install these packages:
  1. In your Azure Data Studio notebook, select **Manage Packages**.
  2. In the **Manage Packages** pane, select the **Add new** tab.
  3. For each of the following packages, enter the package name, click **Search**, then click **Install**.

  As an alternative, you can open a **Command Prompt**, change to the installation path for the version of Python you use in Azure Data Studio (for example, `cd %LocalAppData%\Programs\Python\Python37-32`), then run `pip install` for each package.

## Connect to SQL Server using Azure Data Studio

[Connect using Azure Data Studio](../../azure-data-studio/quickstart-sql-server.md).

1. Connect to Adventureworks database to create the new table, HumanResources.DepartmentTest. The SQL table will be used for the dataframe insertion.

```sql
CREATE TABLE [HumanResources].[DepartmentTest](
[DepartmentID] [smallint] NOT NULL,
[Name] [dbo].[Name] NOT NULL,
[GroupName] [dbo].[Name] NOT NULL
)
GO
```

## Create CVS file
Copy text and save file as department.cvs for dataframe.

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

## Connect to SQL using Python

1. Edit the connection string variables 'server','database','username' and 'password' to connect to SQL database.
 
2. Edit path for CSV file.
 
## Load dataframe from CSV file

Use the Python `pandas` package to create a dataframe and load the CSV file. Connect to SQL to load dataframe into the new SQL table, HumanResources.DepartmentTest.

To create a new notebook:
1. In Azure Data Studio, select **File**, select **New Notebook**.
2. In the notebook, select kernel **Python3**, select the **+code**.
3. Paste code in notebook, select **Run All**.

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

## Confirm row count in SQL

Execute the SQL statement to confirm the table was successfully loaded with data from the dataframe.

```sql
SELECT count(*) from HumanResources.DepartmentTest;
```

Results

```bash
(No column name)
16
```
