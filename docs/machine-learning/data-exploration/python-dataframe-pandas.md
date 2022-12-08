---
title: Insert data from a SQL table into a Python pandas dataframe
titleSuffix: SQL machine learning
description: Learn how to read data from a SQL table and insert into a pandas dataframe using Python.
author: WilliamDAssafMSFT
ms.author: wiassaf
ms.date: 07/23/2020
ms.topic: how-to
ms.service: sql
ms.subservice: machine-learning
monikerRange: ">=sql-server-2017||>=sql-server-linux-ver15||=azuresqldb-mi-current||=azuresqldb-current"
---
# Insert data from a SQL table into a Python pandas dataframe
[!INCLUDE[SQL Server SQL DB SQL MI](../../includes/applies-to-version/sql-asdb-asdbmi.md)]

This article describes how to insert SQL data into a [pandas](https://pandas.pydata.org/) dataframe using the [pyodbc](../../connect/python/pyodbc/python-sql-driver-pyodbc.md) package in Python. The rows and columns of data contained within the dataframe can be used for further data exploration.

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

* Azure Data Studio. To install, see [Azure Data Studio](../../azure-data-studio/what-is-azure-data-studio.md).

* [Restore sample database](../../samples/adventureworks-install-configure.md) to get sample data used in this article.

## Verify restored database

You can verify that the restored database exists by querying the **Person.CountryRegion** table:

```sql
USE AdventureWorks;
SELECT * FROM Person.CountryRegion;
```

## Install Python packages

[Download and Install Azure Data Studio](../../azure-data-studio/download-azure-data-studio.md).

Install the following Python packages:
  * pyodbc
  * pandas

  To install these packages:

  1. In your Azure Data Studio notebook, select **Manage Packages**.
  2. In the **Manage Packages** pane, select the **Add new** tab.
  3. For each of the following packages, enter the package name, click **Search**, then click **Install**.

## Insert data

Use the following script to select data from Person.CountryRegion table and insert into a dataframe. Edit the connection string variables: 'server', 'database', 'username', and 'password' to connect to SQL.

To create a new notebook:

1. In Azure Data Studio, select **File**, select **New Notebook**.
2. In the notebook, select kernel **Python3**, select the **+code**.
3. Paste code in notebook, select **Run All**.

```python
import pyodbc
import pandas as pd
# Some other example server values are
# server = 'localhost\sqlexpress' # for a named instance
# server = 'myserver,port' # to specify an alternate port
server = 'servername' 
database = 'AdventureWorks' 
username = 'yourusername' 
password = 'databasename'  
cnxn = pyodbc.connect('DRIVER={SQL Server};SERVER='+server+';DATABASE='+database+';UID='+username+';PWD='+ password)
cursor = cnxn.cursor()
# select 26 rows from SQL table to insert in dataframe.
query = "SELECT [CountryRegionCode], [Name] FROM Person.CountryRegion;"
df = pd.read_sql(query, cnxn)
print(df.head(26))
```

**Output**

The `print` command in the preceding script displays the rows of data from the `pandas` dataframe `df`.

```text
CountryRegionCode                 Name
0                 AF          Afghanistan
1                 AL              Albania
2                 DZ              Algeria
3                 AS       American Samoa
4                 AD              Andorra
5                 AO               Angola
6                 AI             Anguilla
7                 AQ           Antarctica
8                 AG  Antigua and Barbuda
9                 AR            Argentina
10                AM              Armenia
11                AW                Aruba
12                AU            Australia
13                AT              Austria
14                AZ           Azerbaijan
15                BS         Bahamas, The
16                BH              Bahrain
17                BD           Bangladesh
18                BB             Barbados
19                BY              Belarus
20                BE              Belgium
21                BZ               Belize
22                BJ                Benin
23                BM              Bermuda
24                BT               Bhutan
25                BO              Bolivia
```

## Next steps

+ [Insert Python dataframe into SQL](../data-exploration/python-dataframe-sql-server.md)