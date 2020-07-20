---
title: SQL loopback connection
description: Learn how t use a loopback connection to connect back to SQL Server over ODBC to read or write data from a Python or R script executed from sp_execute_external_script. 
ms.prod: sql
ms.technology: machine-learning-services
ms.date: 08/21/2019
ms.topic: conceptual
author: Aniruddh25
ms.author: anmunde
ms.reviewer: dphansen
ms.custom: seo-lt-2019
monikerRange: ">=sql-server-ver15||>=sql-server-linux-ver15||=sqlallproducts-allversions"
---
# Loopback connection to SQL Server from a Python or R script
 [!INCLUDE [SQL Server](../../includes/applies-to-version/sqlserver.md)]

Learn how to use a loopback connection to connect back to SQL Server over [ODBC](../../connect/odbc/microsoft-odbc-driver-for-sql-server.md) to read or write data from a Python or R script executed from `sp_execute_external_script`. You can use this when using the **InputDataSet** and **OutputDataSet** arguments of `sp_execute_external_script` are not possible.

## Connection string

To make a loopback connection, you need to use a correct connection string. The common mandatory arguments are the name of the [ODBC driver](../../connect/odbc/microsoft-odbc-driver-for-sql-server.md), the server address, and the name of database.

### Connection string on Windows

For authentication on SQL Server on Windows, the Python or R script can use the **Trusted_Connection** connection string attribute to authenticate as the same user that ran the sp_execute_external_script.

Here is an example of the loopback connection string on Windows:

``` 
"Driver=SQL Server;Server=.;Database=nameOfDatabase;Trusted_Connection=Yes;"
```

### Connection string on Linux

For authentication on SQL Server on Linux, the Python or R script needs to use **ClientCertificate** and **ClientKey** attributes of the ODBC driver to authenticate as the same user that executed `sp_execute_external_script`. This requires the use of [latest ODBC driver](../../connect/odbc/download-odbc-driver-for-sql-server.md) version 17.4.1.1.

Here is an example of the loopback connection string on Linux:

```
"Driver=ODBC Driver 17 for SQL Server;Server=fe80::8012:3df5:0:5db1%eth0;Database=nameOfDatabase;ClientCertificate=file:/var/opt/mssql-extensibility/data/baeaac72-60b3-4fae-acfd-c50eff5d34a2/sqlsatellitecert.pem;ClientKey=file:/var/opt/mssql-extensibility/data/baeaac72-60b3-4fae-acfd-c50eff5d34a2/sqlsatellitekey.pem;TrustServerCertificate=Yes;Trusted_Connection=no;Encrypt=Yes"
```

The server address, client certificate file location, and client key file location are unique to every `sp_execute_external_script` and can be obtained by the use of the API **rx_get_sql_loopback_connection_string()** for Python or **rxGetSqlLoopbackConnectionString()** for R.

For more information on the connection string attributes, see the [DSN and Connection String Keywords and Attributes](https://docs.microsoft.com/sql/connect/odbc/dsn-connection-string-attribute?view=sql-server-linux-ver15#new-connection-string-keywords-and-connection-attributes) for Microsoft ODBC Driver for SQL Server.

## Generate connection string with revoscalepy for Python

You can use the API **rx_get_sql_loopback_connection_string()** in [revoscalepy](../python/ref-py-revoscalepy.md) to generate a correct connection string for a loopback connection in a Python script.

It accepts the following arguments:

| Argument | Description |
|-|-|
| name_of_database | Name of the database to which the connection is to be made |
| odbc_driver | Name of the odbc driver |

### Examples

Example for SQL Server on Windows:

```sql
EXECUTE sp_execute_external_script
@language = N'Python',
@script = N'
from revoscalepy import rx_get_sql_loopback_connection_string, RxSqlServerData, rx_data_step
loopback_connection_string = rx_get_sql_loopback_connection_string(odbc_driver="SQL Server", name_of_database="DBName")
print("Connection String:{0}".format(loopback_connection_string))
data_set = RxSqlServerData(sql_query = "select col1, col2 from tableName",
                           connection_string = loopback_connection_string)
OutputDataSet = rx_data_step(data_set)
'
WITH RESULT SETS ((col1 int, col2 int))
GO
```

Example for SQL Server on Linux:

```sql
EXECUTE sp_execute_external_script
@language = N'Python',
@script = N'
from revoscalepy import rx_get_sql_loopback_connection_string, RxSqlServerData, rx_data_step
loopback_connection_string = rx_get_sql_loopback_connection_string(odbc_driver="ODBC Driver 17 for SQL Server",
                                                                   name_of_database="DBName")
print("Loopback Connection String:{0}".format(loopback_connection_string))
data_set = RxSqlServerData(sql_query = "select col1, col2 from tableName",
                           connection_string = loopback_connection_string)
OutputDataSet = rx_data_step(data_set)
'
WITH RESULT SETS ((col1 int, col2 int))
GO
```

## Generate connection string with RevoScaleR for R

You can use the API **rxGetSqlLoopbackConnectionString()** in [RevoScaleR](../r/ref-r-revoscaler.md) to generate a correct connection string for a loopback connection in an R script.

It accepts the following arguments:

| Argument | Description |
|-|-|
| nameOfDatabase | Name of the database to which the connection is to be made |
| odbcDriver | Name of the odbc driver |

### Examples

Example for SQL Server on Windows:

```sql
EXECUTE sp_execute_external_script
@language = N'R',
@script = N'
    loopbackConnectionString <- rxGetSqlLoopbackConnectionString(nameOfDatabase="DBName", odbcDriver ="SQL Server")
    print(paste("Connection String:", loopbackConnectionString))
    dataSet <- RxSqlServerData(sqlQuery = "select col1, col2 from tableName",
                               connectionString = loopbackConnectionString)
    OutputDataSet <- rxDataStep(dataSet)
'
WITH RESULT SETS ((col1 int, col2 int))
GO
```

Example for SQL Server on Linux:

```sql
EXECUTE sp_execute_external_script
@language = N'R',
@script = N'
    loopbackConnectionString <-  rxGetSqlLoopbackConnectionString(nameOfDatabase="DBName", 
                                                                  odbcDriver ="ODBC Driver 17 for SQL Server")
    print(paste("Connection String:", loopbackConnectionString))
    dataSet <- RxSqlServerData(sqlQuery = "select col1, col2 from tableName", 
                               connectionString = loopbackConnectionString)
    OutputDataSet <- rxDataStep(dataSet)
'
WITH RESULT SETS ((col1 int, col2 int))
GO
```

## Next steps

+ [Microsoft ODBC driver for SQL Server](../../connect/odbc/microsoft-odbc-driver-for-sql-server.md)
+ [revoscalepy](../python/ref-py-revoscalepy.md)
+ [RevoScaleR](../r/ref-r-revoscaler.md)
