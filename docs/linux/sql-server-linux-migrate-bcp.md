---
# required metadata

title: Bulk copy data with bcp to SQL Server on Linux - SQL Server vNext CTP1 | Microsoft Docs
description: 
author: sanagama 
ms.author: sanagama 
manager: jhubbard
ms.date: 11/13/2016
ms.topic: article
ms.prod: sql-linux
ms.technology: database-engine
ms.assetid: 7b93d0d7-7946-4b78-b33a-57d6307cdfa9

# optional metadata
# keywords: ""
# ROBOTS: ""
# audience: ""
# ms.devlang: ""
# ms.reviewer: ""
# ms.suite: ""
# ms.tgt_pltfrm: ""
# ms.custom: ""

---
# Bulk copy data with bcp to SQL Server on Linux

This topic shows how to use the [bcp](https://msdn.microsoft.com/en-us/library/ms162802.aspx) command line utility to bulk copy data between an instance of SQL Server vNext CTP1 on Linux and a data file in a user-specified format.

You can use `bcp` to import large numbers of rows into SQL Server tables or to export data from SQL Server tables into data files. Except when used with the queryout option, `bcp` requires no knowledge of Transact-SQL. The `bcp` command line utility works with Microsoft SQL Server running on-premises or in the cloud, on Linux, Windows or Docker and Azure SQL Database and Azure SQL Data Warehouse.

This topic will show you how to:
- Import data into a table using the `bcp in` command
- Export data from a table uisng the `bcp out` command

## Install the SQL Server command-line tools

`bcp` is part of the SQL Server command-line tools package, which is not installed by default with SQL Server vNext CTP1 on Linux. If you have not already installed the SQL Server command-line tools on your Linux machine, you must install them. For more information on how to install the tools, select your Linux distribution from the following drop-down list.

> [!div class="op_single_selector"]
- [Red Hat Enterprise Linux](sql-server-linux-setup-red-hat.md#tools)
- [Ubuntu](sql-server-linux-setup-ubuntu.md#tools)
- [Docker](sql-server-linux-setup-docker.md#tools)

## Import data with bcp

In this tutorial, you will create a sample database and table on the local SQL Server instance (**localhost**) and then use `bcp` to load into the sample table from a text file on disk.

### Create a sample database and table

Let's start by creating a sample database with a simple table that will be used in the rest of this tutorial.

Open a terminal window on your computer and copy and paste the commands below into the terminal window. These commands use the **sqlcmd** command line utility to create a sample database (**BcpSampleDB**) and a table (**TestEmployees**) on the local SQL Server instance (**localhost**). Remember to replace the `username` and `<your_password>` as necessary before running the commands.

Create the database **BcpSampleDB**:
```bash 
sqlcmd -S localhost -U sa -P <your_password> -Q "CREATE DATABASE BcpSampleDB;"
```
Create the table **TestEmployees** in the database **BcpSampleDB**:
```bash 
sqlcmd -S localhost -U sa -P <your_password> -d BcpSampleDB -Q "CREATE TABLE TestEmployees (Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, Name NVARCHAR(50), Location NVARCHAR(50));"
```

### Create the source data file
In your terminal window, copy and paste the commands below. We will use the built-in `printf` command to create a sample data file with 3 records where each field is delimited by a `comma` and save the file in your home directory as **~/test_data.txt**.
```bash 
printf "1,Jared,Australia\n" > ~/test_data.txt
```
```bash 
printf "2,Nikita,India\n" >> ~/test_data.txt
```
```bash 
printf "3,Tom,Germany\n" >> ~/test_data.txt
```

You can verify that the data file **~/test_data.txt** was created correctly by running the command below in your terminal window:
```bash 
cat ~/test_data.txt
```

This should display the following in your terminal window:
```bash
1,Jared,Australia
2,Nikita,India
3,Tom,Germany
```

### Import data from the source data file
In your terminal window, copy and paste the command below. This command uses `bcp` to connect to the local SQL Server instance (**localhost**) and import the data from the data file (**~/test_data.txt**) into the table (**TestEmployees**) in the database (**BcpSampleDB**). Remember to replace the `username` and `<your_password>` as necessary before running the commands.

We are using the following command line parameters with `bcp` in this example:
- `-S`: specifies the instance of SQL Server to which to connect
- `-U`: specifies the login ID used to connect to SQL Server
- `-P`: specifies the password for the login ID
- `-d`: specifies the database to connect to
- `-c`: performs operations using a character data type
- `-t`: specifies the field terminator. We are using `comma` as the field terminator for the records in our data file

> [!NOTE]
> We are not specifying a custom row terminator in this example. We terminated each row with `\r\n` as `bcp` expects when we used `printf` to create the data file earlier.

```bash 
bcp TestEmployees in ~/test_data.txt -S localhost -U sa -P <your_password> -d BcpSampleDB -c -t  ','
```

You can verify that the data was successfully imported by running the command below in your terminal window. Remember to replace the `username` and `<your_password>` as necessary before running the command.
```bash 
sqlcmd -S localhost -d BcpSampleDB -U sa -P <your_password> -I -Q "SELECT * FROM TestEmployees;"
```

This should return the following results:
```bash
Id          Name                Location
----------- ------------------- -------------------
          1 Jared               Australia
          2 Nikita              India
          3 Tom                 Germany

(3 rows affected)
```

## Export data with bcp

In this tutorial, you will use `bcp` to export data from the sample table we created earlier to a new data file.

Open a terminal window on your computer and copy and paste the commands below into the terminal window. These commands use the `bcp` command line utility to export data from the table **TestEmployees** in the in the database **BcpSampleDB** to a new data file called **~/test_export.txt**.  Remember to replace the `username` and `<your_password>` as necessary before running the command.
```bash 
bcp TestEmployees out ~/test_export.txt -S localhost -U sa -P <your_password> -d BcpSampleDB -c -t ','
```

You can verify that the data was exported correctly by running the command below in your terminal window:
```bash 
cat ~/test_export.txt
```

This should display the following in your terminal window:
```
1,Jared,Australia
2,Nikita,India
3,Tom,Germany
```

## See also
- [bcp utility](https://msdn.microsoft.com/en-us/library/ms162802.aspx)
- [Data Formats for Compatibility when Using bcp](https://msdn.microsoft.com/en-us/library/ms190759.aspx)
- [Import Bulk Data by Using BULK INSERT](https://msdn.microsoft.com/en-us/library/ms175915.aspx)
- [BULK INSERT (Transact-SQL)](https://msdn.microsoft.com/en-us/library/ms188365.aspx)
