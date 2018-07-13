---
title: Install SQL Server Machine Learning Services (R and Python) on Linux | Microsoft Docs
description: This article describes how to install SQL Server Machine Learning Services (R and Python) on Red Hat and Ubuntu.
author: HeidiSteen
ms.author: heidist
manager: cgronlun
ms.date: 07/12/2018
ms.topic: conceptual
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux"
ms.technology: machine-learning
---
# Install SQL Server vNext Machine Learning Services R and Python support on Linux

[SQL Server Machine Learning Services (R and Python)](../advanced-analytics/what-is-sql-server-machine-learning.md) runs on Linux operating systems starting in this CTP 2.0 release of SQL Server vNext. Follow these steps to install in-database analytics on either of these Linux operating systems:

- [Red Hat Enterprise Linux 7.3 or 7.4](#RHEL)
- [Ubuntu 16.04](#ubuntu)

> [!NOTE]
> SUSE (SLES) is not a supported operating system in this release.

## Prerequisites

First [install SQL Server vNext](sql-server-linux-setup.md#platforms). This configures the keys and repositories used when installing the R and Python packages.

Hardware requirements include:

+ Minimum of 2 GB of RAM, maximum 256 GB of RAM 
+ Minimum of 4 GB of disk space 
+ XFS (default on RHEL) or EXT4 file system 
+ Network connectivity to the Internet to download the package 
+ A hostname with a maximum of 15 characters 
+ wget is a required package to run the configuration script 
+ Python3 is required to run the configuration script 

<a name="RHEL"></a>

## Install on RHEL

Download the Microsoft SQL Server Red Hat repository configuration file: 
(copied from: https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-red-hat?view=sql-server-linux-2017) 

```bash
sudo curl -o /etc/yum.repos.d/mssql-server.repo https://packages.microsoft.com/config/rhel/7/mssql-server-2017.repo 
```
Run the following commands to install SQL Server. 

```bash
sudo yum install -y mssql-server 
 ```

### Add both R and Python support

Run the following commands to install SQL Server with Machine Learning Services with both R and Python. 

```bash
# sudo yum install mssql-mlservices-all-9.4.1   
```

### Add R only
 
Run the following commands to install SQL Server with Machine Learning Services with only R.

```bash 
# sudo yum install mssql-mlservices-R-9.4.1   
```

### Add Python only

Run the following commands to install SQL Server with Machine Learning Services with only Python. 

```bash
# sudo yum install mssql-mlservices-python-9.4.1   
```

<a name="ubuntu"></a>

## Install on Ubuntu

Import the public repository GPG keys: 

```bash
(copied from: https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-ubuntu?view=sql-server-linux-2017) 

wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add - 
```

Register the Microsoft SQL Server Ubuntu repository: 

```bash
sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/16.04/mssql-server-2017.list)" 
```

Run the following commands to install SQL Server vNext.

```bash 
sudo apt-get update 
sudo apt-get install -y mssql-server 
```

### Add both R and Python support
Run the following commands to install SQL Server with Machine Learning Services with both R and Python: 

```bash
# sudo apt-get install mssql-mlservices-all-9.4.1   
```
### Add R only

Run the following commands to install SQL Server with Machine Learning Services with only R: 

```bash
# sudo apt-get install mssql-mlservices-R-9.4.1   
```
### Add Python only

Run the following commands to install SQL Server with Machine Learning Services with only Python: 

```bash
# sudo apt-get install mssql-mlservices-python-9.4.1   
```

## Unattended installation 
 
For Machine Learning Services, we have added a new environment variable (ACCEPT_ML_EULA) that you can use to accept the ML Services EULA supplement for unattended installations. This is a supplement to the SQL Server EULA. 

The following example configures the Developer edition of SQL Server with SQL Server Machine Learning Services. The -n parameter performs an unprompted installation where the configuration values are pulled from the environment variables. 

```bash
sudo MSSQL_PID=Developer ACCEPT_EULA=Y ACCEPT_ML_EULA=Y SSQL_SA_PASSWORD='<YourStrong!Passw0rd>' /opt/mssql/bin/mssql-conf -n setup 
```

You can read more about unattended SQL Server installations and scripts here: https://docs.microsoft.com/en-us/sql/linux/sql-server-linux-setup?view=sql-server-linux-2017#unattended 

## Offline installation

If you need an offline installation, locate the package downloads in the [Release notes](sql-server-linux-release-notes.md). Then use the same offline installation steps described in the article [Install SQL Server](sql-server-linux-setup.md#offline).

## Post-install configuration

After installation of SQL Server on Linux you will need to do the following to accept the license agreement and provide the system administrator (SA) password. This license agreement also contains a supplement for Machine Learning Services if you have installed any of the mlservices packages. 

Run the configuration script to accept the license agreement and provide the System Administrator (SA) password: 

```bash
$ sudo /opt/mssql/bin/mssql-conf setup 
```

If you already have a SQL Server installation and just want to add the feature machine learning services, then you can install one of the mlservices packages as described above, and run the following to accept the mlservices EULA: 

```bash
$ sudo /opt/mssql/bin/mssql-conf set accept-eula ml 
```

## Configure external script execution 

Before running R and Python scripts in SQL Server, you need to enable external script execution. 
Configure external script execution by running the following SQL command: 

```bash
EXEC sp_configure 'external scripts enabled', 1 
RECONFIGURE WITH OVERRIDE 
```

Restart SQL Server for the changes to take effect: 

```bash
# sudo systemctl restart mssql-server 
```
 
## Verify external script execution
  
Execute the following SQL command to test R execution in SQL Server: 

```r
EXEC sp_execute_external_script   
@language =N'R', 
@script=N' 
OutputDataSet <- InputDataSet', 
@input_data_1 =N'SELECT 1 AS hello' 
WITH RESULT SETS (([hello] int not null)); 
GO 
```
 
Execute the following SQL command to test Python execution in SQL Server: 
 
```python
EXEC sp_execute_external_script  
@language =N'Python', 
@script=N' 
OutputDataSet = InputDataSet; 
', 
@input_data_1 =N'SELECT 1 AS hello' 
WITH RESULT SETS (([hello] int not null)); 
GO 
```

## Add other R and Python packages 
 
You can install other R and Python packages and use them in script that executes on SQL Server vNext.

### R packages 
 
1. Start an R session.

   ```r
   # sudo /opt/mssql/mlservices/bin/R/R 
   ```

2. Install an R package called “glue” to test package installation.

   ```r
   # install.packages("glue",lib="/opt/mssql/mlservices/libraries/RServer") 
   ```
   Alternatively, you can install an R package from the command line 

   ```r
   # sudo /opt/mssql/mlservices/bin/R/R CMD INSTALL -l /opt/mssql/mlservices/libraries/RServer glue_1.1.1.tar.gz 
   ```
  
3. Import the R package in sp_execute_external_script 

   ```r
   EXEC sp_execute_external_script  
   @language = N'R', 
   @script = N'library(glue)' 
   ```

### Python packages 
 
1. Install a Python package called “httpie” using pip. 

   ```python
   # sudo /opt/mssql/mlservices/bin/python/python -m pip install httpie 
   ``` 
     
2. Import the Python package in sp_execute_external_script
 
   ```python
   EXEC sp_execute_external_script  
   @language = N'Python',  
   @script = N'import httpie' 
   ```

## Limitations in CTP 2.0

The following limitations exist in this CTP release.

+ Implied authentication currently not available in Machine Learning Services on Linux, which means connecting back to the server from within an R or Python script is not supported on Linux at this time. 

## Next steps

R developers can get started with some simple examples, and learn the basics of how R works with SQL Server. For your next step, see the following links:

+ [Tutorial: Run R in T-SQL](../advanced-analytics/tutorials/rtsql-using-r-code-in-transact-sql-quickstart.md)
+ [Tutorial: In-database analytics for R developers](../advanced-analytics/tutorials/sqldev-in-database-r-for-sql-developers.md)

Python developers can learn how to use Python with SQL Server by following these tutorials:

+ [Tutorial: Run Python in T-SQL](../advanced-analytics/tutorials/run-python-using-t-sql.md)
+ [Tutorial: In-database analytics for Python developers](../advanced-analytics/tutorials/sqldev-in-database-python-for-sql-developers.md)

To view examples of machine learning that are based on real-world scenarios, see [Machine learning tutorials](../advanced-analytics/tutorials/machine-learning-services-tutorials.md).
