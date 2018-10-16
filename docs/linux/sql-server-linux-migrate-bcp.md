---
title: Bulk copy data to SQL Server on Linux | Microsoft Docs
description: 
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 01/30/2018
ms.topic: conceptual
ms.prod: sql
ms.custom: "sql-linux"
ms.technology: linux
ms.assetid: 7b93d0d7-7946-4b78-b33a-57d6307cdfa9
---
# Bulk copy data with bcp to SQL Server on Linux

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-linuxonly](../includes/appliesto-ss-xxxx-xxxx-xxx-md-linuxonly.md)]

This article shows how to use the [bcp](../tools/bcp-utility.md) command-line utility to bulk copy data between an instance of SQL Server on Linux and a data file in a user-specified format.

You can use `bcp` to import large numbers of rows into SQL Server tables or to export data from SQL Server tables into data files. Except when used with the queryout option, `bcp` requires no knowledge of Transact-SQL. The `bcp` command-line utility works with Microsoft SQL Server running on-premises or in the cloud, on Linux, Windows or Docker and Azure SQL Database and Azure SQL Data Warehouse.

This article shows you how to:
- Import data into a table using the `bcp in` command
- Export data from a table using the `bcp out` command

## Install the SQL Server command-line tools

`bcp` is part of the SQL Server command-line tools, which are not installed automatically with SQL Server on Linux. If you have not already installed the SQL Server command-line tools on your Linux machine, you must install them. For more information on how to install the tools, select your Linux distribution from the following list:

- [Red Hat Enterprise Linux (RHEL)](sql-server-linux-setup-tools.md#RHEL)
- [Ubuntu](sql-server-linux-setup-tools.md#ubuntu)
- [SUSE Linux Enterprise Server (SLES)](sql-server-linux-setup-tools.md#SLES)

## Import data with bcp

In this tutorial, you create a sample database and table on the local SQL Server instance (**localhost**) and then use `bcp` to load into the sample table from a text file on disk.

### Create a sample database and table

Let's start by creating a sample database with a simple table that is used in the rest of this tutorial.

1. On your Linux box, open a command terminal.

2. Copy and paste the following commands into the terminal window. These commands use the **sqlcmd** command-line utility to create a sample database (**BcpSampleDB**) and a table (**TestEmployees**) on the local SQL Server instance (**localhost**). Remember to replace the `username` and `<your_password>` as necessary before running the commands.

Create the database **BcpSampleDB**:
```bash 
sqlcmd -S localhost -U sa -P <your_password> -Q "CREATE DATABASE BcpSampleDB;"
```
Create the table **TestEmployees** in the database **BcpSampleDB**:
```bash 
sqlcmd -S localhost -U sa -P <your_password> -d BcpSampleDB -Q "CREATE TABLE TestEmployees (Id INT IDENTITY(1,1) NOT NULL PRIMARY KEY, Name NVARCHAR(50), Location NVARCHAR(50));"
```
### Create the source data file
Copy and paste the following command into your terminal window. We use the built-in `cat` command to create a sample text data file with three records save the file in your home directory as **~/test_data.txt**. The fields in the records are delimited by a comma.

```bash
cat > ~/test_data.txt << EOF
1,Jared,Australia
2,Nikita,India
3,Tom,Germany
EOF
```

You can verify that the data file was created correctly by running the following command in your terminal window:
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
Copy and paste the following commands into the terminal window. This command uses `bcp` to connect to the local SQL Server instance (**localhost**) and import the data from the data file (**~/test_data.txt**) into the table (**TestEmployees**) in the database (**BcpSampleDB**). Remember to replace the username and `<your_password>` as necessary before running the commands.

```bash 
bcp TestEmployees in ~/test_data.txt -S localhost -U sa -P <your_password> -d BcpSampleDB -c -t  ','
```

Here's a brief overview of the command-line parameters we used with `bcp` in this example:
- `-S`: specifies the instance of SQL Server to which to connect
- `-U`: specifies the login ID used to connect to SQL Server
- `-P`: specifies the password for the login ID
- `-d`: specifies the database to connect to
- `-c`: performs operations using a character data type
- `-t`: specifies the field terminator. We are using `comma` as the field terminator for the records in our data file

> [!NOTE]
> We are not specifying a custom row terminator in this example. Rows in the text data file were correctly terminated with `newline` when we used the `cat` command to create the data file earlier.

You can verify that the data was successfully imported by running the following command in your terminal window. Remember to replace the `username` and `<your_password>` as necessary before running the command.
```bash 
sqlcmd -S localhost -d BcpSampleDB -U sa -P <your_password> -I -Q "SELECT * FROM TestEmployees;"
```

This should display the following results:
```bash
Id          Name                Location
----------- ------------------- -------------------
          1 Jared               Australia
          2 Nikita              India
          3 Tom                 Germany

(3 rows affected)
```

## Export data with bcp

In this tutorial, you use `bcp` to export data from the sample table we created earlier to a new data file.

Copy and paste the following commands into the terminal window. These commands use the `bcp` command-line utility to export data from the table **TestEmployees** in the database **BcpSampleDB** to a new data file called **~/test_export.txt**.  Remember to replace the username and `<your_password>` as necessary before running the command.

```bash 
bcp TestEmployees out ~/test_export.txt -S localhost -U sa -P <your_password> -d BcpSampleDB -c -t ','
```

You can verify that the data was exported correctly by running the following command in your terminal window:
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
- [bcp utility](../tools/bcp-utility.md)
- [Data Formats for Compatibility when Using bcp](../relational-databases/import-export/specify-data-formats-for-compatibility-when-using-bcp-sql-server.md)
- [Import Bulk Data by Using BULK INSERT](../relational-databases/import-export/import-bulk-data-by-using-bulk-insert-or-openrowset-bulk-sql-server.md)
- [BULK INSERT (Transact-SQL)](../t-sql/statements/bulk-insert-transact-sql.md)
