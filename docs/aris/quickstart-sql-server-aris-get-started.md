---
title: Get started with big data on SQL Server vNext | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 08/06/2018
ms.topic: quickstart
ms.prod: sql
---

# Quickstart: Get started with big data on SQL Server vNext

In this quickstart, you will combine high-volume HDFS data with high-value relational data using SQL Server vNext running on Kubernetes.

## Prerequisites

This quickstart requires that you have already done the following tasks:

- [Configured a Kubernetes cluster](sql-server-aris-deployment-guidance.md#kubernetes).
- [Installed SQL Server vNext on the Kubernetes cluster](sql-server-aris-deployment-guidance.md#deploy).
- [Installed SQL Server big data tools](sql-server-aris-install-big-data-tools.md).

## Initialize a data pool

Connect to the master instance using the IP address / port number (31433) obtained at the end of the deployment script. You can now create a data pool on the master instance. Execute the batch below to create a data pool that consists of 2 instances. If you modified the number of Kubernetes agents above, then change the value of @cluster_node_count variable to match.

```sql
USE high_value_data
GO
DECLARE @data_pool_name NVARCHAR(max) = 'mssql-data-pool'
DECLARE @cluster_node_count INT = 2

EXEC sp_data_pool_create @data_pool_name, @cluster_node_count
GO
```

## Derive table schema from sample file using spark and create sql server table schema

After successful creation of the data pool, you can now configure the data pool instances to receive data from a Spark streaming job and query the data from the master instance. For this example, we will derive the schema of the table in SQL Server from a sample file that resides on an HDFS volume on the newly created cluster. Execute the batch below to create an external table called "airlinedata" on the master instance and tables on the data pool instances to hold data from the Spark streaming job.

```sql
USE high_value_data
GO
DECLARE @data_pool_name NVARCHAR(max) = 'mssql-data-pool'
DECLARE @schema_name NVARCHAR(max) = 'dbo'
DECLARE @table_name NVARCHAR(max) = 'airlinedata'
DECLARE @sample_file_name NVARCHAR(max) = 'hdfs:///airlinedata/airlinedata_sample.csv'
EXEC sp_data_pool_derive_schema_and_create_table @data_pool_name, @schema_name, @table_name, @sample_file_name
GO
```

## Start stop spark stream submit spark job to resource negotiator

Now, you can start streaming data from a Spark streaming job into the data pool instances. Execute the batch below to start a Spark streaming job that will stream data into the table **airlinedata** on each data pool instance.

```sql
USE high_value_data
GO
DECLARE @data_pool_name NVARCHAR(max) = 'mssql-data-pool'
DECLARE @schema_name NVARCHAR(max) = 'dbo'
DECLARE @table_name NVARCHAR(max) = 'airlinedata'
DECLARE @source_folder NVARCHAR(max) = 'hdfs:///airlinedata'
EXEC sp_data_pool_start_import @data_pool_name, @schema_name, @table_name, @source_folder;
GO
```

## Query from the master instance

After the streaming job has been successfully started, you can now query the data from the master instance. Execute the batch below to run some sample queries. If you run the query a few times you should see that the number of rows in the results is increasing as the data is continuously ingested into the data pool instances via the Spark streaming job.

> [!NOTE]
> It may take a few minutes to populate the data in the tables from the source data files.

```sql
USE high_value_data
GO
SELECT count(*) FROM airlinedata
SELECT TOP 10 * FROM airlinedata
GO
```

## Run a simple query against the master instance to join high value and high volume data

Now that you have populated the high-volume streaming data into the data pool instances, you can execute the script **prepare_high_value_db_view.sql** script under the **/CTP2.0/samples-tsql** folder to create some sample high-value data in the master instance.  

The last step is a to execute a query to join the high-volume data in the data pool instances represented by the newly created view **AirlineEngineSensorDataNorm** with the high-value data in **AirlineEngines**, **AircraftRegistration**, and **FlightRoutes**.

```sql
USE high_value_data
GO

SELECT E.AircraftRegistration, F.Origin, F.Destination, A.*
  FROM AirlineEngineSensorDataNorm AS A
  JOIN high_value_data.dbo.AirlineEngines AS E
    ON E.EngineId = A.EngineId
  JOIN high_value_data.dbo.FlightRoutes AS F
    ON F.AircraftRegistration = E.AircraftRegistration AND F.EngineId = E.EngineId
 WHERE A.EngineId IN (9, 48);
GO
```

## Next steps

Explore how to run Jupyter notebooks in Azure Data Studio:

> [!div class="nextstepaction"]
> [Run Jupyter notebooks on SQL Server vNext](quickstart-sql-server-aris-jupyter-notebook.md)