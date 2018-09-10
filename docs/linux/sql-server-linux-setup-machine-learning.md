---
title: Install SQL Server Machine Learning Services (R, Python, Java) on Linux | Microsoft Docs
description: This article describes how to install SQL Server Machine Learning Services (R, Python, Java) on Red Hat and Ubuntu.
author: HeidiSteen
ms.author: heidist
manager: cgronlun
ms.date: 09/09/2018
ms.topic: conceptual
ms.prod: sql
ms.component: ""
ms.suite: "sql"
ms.custom: "sql-linux"
ms.technology: machine-learning
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Install SQL Server 2019 Machine Learning Services (R, Python, Java) on Linux

[SQL Server Machine Learning Services](../advanced-analytics/what-is-sql-server-machine-learning.md) runs on Linux operating systems starting in this CTP 2.0 release of SQL Server 2019. Follow these steps to install the Java programming extension, or the machine learning extensions for R and Python. 

## Prerequisites

+ Install the database engine on either [Red Hat Enterprise Linux 7.3 or 7.4](quickstart-install-connect-red-hat.md) or [Ubuntu 16.04](quickstart-install-connect-ubuntu.md).

> [!NOTE]
> SUSE (SLES) is not a supported operating system for Java, R, or Python extensions in this release.

After the database engine is installed and configured, you can add the programming language extensions for Java, R, or Python. Packages for these extensions are downloaded and installed independently of the database engine. Download links and nstructions for each operating system are provided in the following sections.

<a name="RHEL"></a>

## Red Hat package install

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

## Ubuntu package install

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
 
For open-source R and Python components, use the environment variable (ACCEPT_ML_EULA) to accept the ML Services EULA supplement for unattended installations. This is a supplement to the SQL Server EULA. 

The following example configures the Developer edition of SQL Server with SQL Server Machine Learning Services. The -n parameter performs an unprompted installation where the configuration values are pulled from the environment variables. 

```bash
sudo MSSQL_PID=Developer ACCEPT_EULA=Y ACCEPT_ML_EULA=Y SSQL_SA_PASSWORD='<YourStrong!Passw0rd>' /opt/mssql/bin/mssql-conf -n setup 
```

For more information, see [Unattended install](https://docs.microsoft.com/sql/linux/sql-server-linux-setup?view=sql-server-2017#unattended).

## Offline installation

1. Locate the MLS-specific package downloads in the [Release notes](sql-server-linux-release-notes-2019.md). 

2. Continue with [Offline installation](sql-server-linux-setup.md#offline) instructions for your next step.

## Post-install configuration

After the database engine and Machine Learning Services packages are installed, configure the server for operations. This task includes acceptance of SQL Server, R, and Python license agreements and provision of the system administrator (SA) password.

Run the configuration script to accept the license agreement and provide the System Administrator (SA) password: 

```bash
$ sudo /opt/mssql/bin/mssql-conf setup 
```

If you already have a SQL Server installation and just want to add the feature machine learning services, then you can install mlservices packages and run the following to accept the mlservices EULA: 

```bash
$ sudo /opt/mssql/bin/mssql-conf set accept-eula ml 
```

## Configure external script execution 

Before running R and Python scripts in SQL Server, enable external script execution: 

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
 
You can install other R and Python packages and use them in script that executes on SQL Server 2019.

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
