---
title: Loopback connection to SQL Server from Python and R scripts
description: How to create a loopback connection from Machine Learning Services to SQL Server
ms.prod: sql
ms.technology: machine-learning
ms.date: 08/15/2019  
ms.topic: conceptual
author: dphansen
ms.author: davidph
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Loopback connection to SQL Server
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Learn how to connect back to SQL Server over ODBC to read or write data from a Python or R script executed via `sp_execute_external_script`

This article talks about how to make an R or Python process launched through the execution of sp_execute_external_script on SQL Server connect back to SQL over ODBC to retrieve or write additional datasets more than those possible just by the InputDataSet and OutputDataSet arguments of sp_execute_external_script. Such a connection is called a loopback connection.

## Connection String

To make a loopback connection, the most important aspect is the formation of the correct connection string. The common mandatory arguments on both SQL server platforms are the name of the ODBC driver, the server address and the name of database. 

For authentication on Windows SQL Server, external scripts can use the "Trusted_Connection" connection string attribute to authenticate as the same user that ran the sp_execute_external_script. Refer here 
On this platform, the loopback connection string for external scripts looks like : 
"Driver=SQL Server;Server=.;Database=nameOfDatabase;Trusted_Connection=Yes;"

For Linux SQL Server, external scripts need to use ClientCertificate and ClientKey attributes of the ODBC driver to authenticate as the same user that ran the sp_execute_external_script. This requires the use of latest odbc driver 17.4.1.1. The loopback connection string on linux looks like :
"Driver=ODBC Driver 17 for SQL Server;Server=fe80::8012:3df5:0:5db1%eth0;Database=nameOfDatabase;ClientCertificate=file:/var/opt/mssql-extensibility/data/baeaac72-60b3-4fae-acfd-c50eff5d34a2/sqlsatellitecert.pem;ClientKey=file:/var/opt/mssql-extensibility/data/baeaac72-60b3-4fae-acfd-c50eff5d34a2/sqlsatellitekey.pem;TrustServerCertificate=Yes;Trusted_Connection=no;Encrypt=Yes"
The server address, clientcertificate file location and client key file location are unique to every sp_execute_external_script and can be obtained by the use of the api - rxGetSqlLoopbackConnectionString() for R or rx_get_sql_loopback_connection_string() for Python as described below. 

Refer here for more information on connection string attributes: https://docs.microsoft.com/en-us/sql/connect/odbc/dsn-connection-string-attribute?view=sql-server-linux-ver15#new-connection-string-keywords-and-connection-attributes


## rxGetSqlLoopbackConnectionString() in RevoScaleR for R

This is a convenience API useful to generate the correct connection string for such loopback connections. It accepts following arguments:
nameOfDatabase 
    name of the database to which the connection is to be made.
odbcDriver
    name of the odbc driver.

Example for Windows SQL Server:
execute sp_execute_external_script
@language = N'R',
@script = N'
    loopbackConnectionString <-  rxGetSqlLoopbackConnectionString(nameOfDatabase="DBName", odbcDriver ="SQL Server")
    print(paste("Connection String:", loopbackConnectionString))
    dataSet <- RxSqlServerData(sqlQuery = "select col1, col2 from tableName", connectionString = loopbackConnectionString)
    OutputDataSet <- rxDataStep(dataSet)
'
WITH RESULT SETS ((col1 int, col2 int))
GO

Example for Linux SQL Server:
execute sp_execute_external_script
@language = N'R',
@script = N'
    loopbackConnectionString <-  rxGetSqlLoopbackConnectionString(nameOfDatabase="DBName", odbcDriver ="ODBC Driver 17 for SQL Server")
    print(paste("Connection String:", loopbackConnectionString))
    dataSet <- RxSqlServerData(sqlQuery = "select col1, col2 from tableName", connectionString = loopbackConnectionString)
    OutputDataSet <- rxDataStep(dataSet)
'
WITH RESULT SETS ((col1 int, col2 int))
GO

## rx_get_sql_loopback_connection_string() in revoscalepy for Python

This is a convenience API useful to generate the correct connection string for such loopback connections when external script is Python. It accepts following arguments:
name_of_database
    name of the database to which the connection is to be made.
odbc_driver
    name of the odbc driver.

Example for Windows SQL Server:
execute sp_execute_external_script
@language = N'Python',
@script = N'
from revoscalepy import rx_get_sql_loopback_connection_string, RxSqlServerData, rx_data_step
loopback_connection_string = rx_get_sql_loopback_connection_string(odbc_driver="SQL Server", name_of_database="DBName")
print("Connection String:{0}".format(loopback_connection_string))
data_set = RxSqlServerData(sql_query = "select col1, col2 from tableName", connection_string = loopback_connection_string)
OutputDataSet = rx_data_step(data_set)
'
WITH RESULT SETS ((col1 int, col2 int))
GO

Example for Linux SQL Server:
execute sp_execute_external_script
@language = N'Python',
@script = N'
from revoscalepy import rx_get_sql_loopback_connection_string, RxSqlServerData, rx_data_step
loopback_connection_string = rx_get_sql_loopback_connection_string(odbc_driver="ODBC Driver 17 for SQL Server", name_of_database="DBName")
print("Loopback Connection String:{0}".format(loopback_connection_string))
data_set = RxSqlServerData(sql_query = "select col1, col2 from tableName", connection_string = loopback_connection_string)
OutputDataSet = rx_data_step(data_set)
'
WITH RESULT SETS ((col1 int, col2 int))
GO

## Next steps

+ [Some relevant article](../advanced-analytics/link-to-relevant-article.md)
+ [Some other relevant article](../advanced-analytics/link-to-other-relevant-article.md)