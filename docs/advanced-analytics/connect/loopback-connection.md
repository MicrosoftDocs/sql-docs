---
title: Loopback connection to SQL Server from a Python or R script
description: Learn how to use a loopback connection to connect back to SQL Server over ODBC to read or write data from a Python or R script executed from sp_execute_external_script. You can use this when you can't use the InputDataSet and OutputDataSet arguments of sp_execute_external_script.
ms.prod: sql
ms.technology: machine-learning
ms.date: 08/15/2019  
ms.topic: conceptual
author: dphansen
ms.author: anmunde
ms.reviewer: dphansen
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Loopback connection to SQL Server from a Python or R script
[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md](../../includes/appliesto-ss-xxxx-xxxx-xxx-md.md)]

Learn how to use a loopback connection to connect back to SQL Server over [ODBC](../../connect/odbc/microsoft-odbc-driver-for-sql-server.md) to read or write data from a Python or R script executed from `sp_execute_external_script`. You can use this when you can't use the **InputDataSet** and **OutputDataSet** arguments of `sp_execute_external_script`.

## Connection string

To make a loopback you connection, you need to use a correct connection string. The common mandatory arguments are the name of the [ODBC driver](../../connect/odbc/microsoft-odbc-driver-for-sql-server.md), the server address and the name of database.

### Connection string on Windows

For authentication on SQL Server on Windows, the Python or R script can use the **Trusted_Connection** connection string attribute to authenticate as the same user that ran the sp_execute_external_script.

Here is an example of the loopback connection string on Windows:

``` 
"Driver=SQL Server;Server=.;Database=nameOfDatabase;Trusted_Connection=Yes;"
```

### Connection string on Linux

For authentication on SQL Server on Linux, the Python or R script needs to use **ClientCertificate** and **ClientKey** attributes of the ODBC driver to authenticate as the same user that executed `sp_execute_external_script`. This requires the use of latest odbc driver 17.4.1.1.

Here is an example of the loopback connection string on Linux:

```
"Driver=ODBC Driver 17 for SQL Server;Server=fe80::8012:3df5:0:5db1%eth0;Database=nameOfDatabase;ClientCertificate=file:/var/opt/mssql-extensibility/data/baeaac72-60b3-4fae-acfd-c50eff5d34a2/sqlsatellitecert.pem;ClientKey=file:/var/opt/mssql-extensibility/data/baeaac72-60b3-4fae-acfd-c50eff5d34a2/sqlsatellitekey.pem;TrustServerCertificate=Yes;Trusted_Connection=no;Encrypt=Yes"
```

The server address, clientcertificate file location, and client key file location are unique to every `sp_execute_external_script` and can be obtained by the use of the API **rx_get_sql_loopback_connection_string()** for Python or **rxGetSqlLoopbackConnectionString()** for R.

For more information on the connection string attributes, see the [DSN and Connection String Keywords and Attributes](https://docs.microsoft.com/en-us/sql/connect/odbc/dsn-connection-string-attribute?view=sql-server-linux-ver15#new-connection-string-keywords-and-connection-attributes) for Microsoft ODBC Driver for SQL Server.


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