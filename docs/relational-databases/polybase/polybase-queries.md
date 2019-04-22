---
title: "PolyBase query scenarios | Microsoft Docs"
ms.custom: ""
ms.date: 09/24/2018
ms.prod: sql
ms.reviewer: ""
ms.technology: polybase
ms.topic: conceptual
keywords: 
  - "PolyBase"
helpviewer_keywords: 
  - "PolyBase, import and export"
  - "Hadoop, import with PolyBase"
  - "Hadoop, export with PolyBase"
  - "Azure blob storage, import with PolyBase"
  - "Azure blob storage, export with PolyBase"
ms.assetid: 2c5aa2bd-af7d-4f57-9a28-9673c2a4c07e
author: rothja
ms.author: jroth
manager: craigg
monikerRange: ">= sql-server-2016 || =sqlallproducts-allversions"
---
# PolyBase query scenarios

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article provides examples of queries using the [PolyBase](../../relational-databases/polybase/polybase-guide.md) feature of SQL Server (starting with 2016). Before using these examples, you must first install and configure PolyBase. For more information, see the [PolyBase overview](polybase-guide.md).
  
Run Transact-SQL statements against external tables or use BI tools to query external tables.
  
## SELECT from external table  

A simple query that returns data from a defined external table.  

```sql  
SELECT TOP 10 * FROM [dbo].[SensorData];   
```

A simple query that includes a predicate.

```sql
SELECT * FROM [dbo].[SensorData]
WHERE Speed > 65;
```

## JOIN external tables with local tables

```sql
SELECT InsuranceCustomers.FirstName,   
   InsuranceCustomers.LastName,   
   SensorData.Speed  
FROM InsuranceCustomers INNER JOIN SensorData    
ON InsuranceCustomers.CustomerKey = SensorData.CustomerKey   
WHERE SensorData.Speed > 65   
ORDER BY SensorData.Speed DESC  
  
```  

## Import data

Import data from Hadoop or Azure Storage into SQL Server for persistent storage. Use SELECT INTO to import data referenced by an external table, for persistent storage in SQL Server. Create a relational table on-the-fly, and then create a column-store index on top of the table in a second step.

```sql
-- PolyBase scenario - import external data into SQL Server
-- Import data for fast drivers into SQL Server to do more in-depth analysis
-- Leverage columnstore technology
  
SELECT DISTINCT   
        Insured_Customers.FirstName, Insured_Customers.LastName,   
        Insured_Customers.YearlyIncome, Insured_Customers.MaritalStatus  
INTO Fast_Customers from Insured_Customers INNER JOIN   
(  
        SELECT * FROM CarSensor_Data where Speed > 35   
) AS SensorD  
ON Insured_Customers.CustomerKey = SensorD.CustomerKey  
ORDER BY YearlyIncome  
  
CREATE CLUSTERED COLUMNSTORE INDEX CCI_FastCustomers ON Fast_Customers;  
```

## Export data

Export data from SQL Server to Hadoop or Azure Storage. 

First, enable export functionality by setting the `sp_configure` value of 'allow polybase export' to 1. Next, create an external table that points to the destination directory. The CREATE EXTERNAL TABLE statement creates the destination directory, if it doesn't already exist. Then, use INSERT INTO to export data from a local SQL Server table to the external data source. 

The results of the SELECT statement are exported to the specified location in the specified file format. The external files are named *QueryID_date_time_ID.format*, where *ID* is an incremental identifier and *format* is the exported data format. For example, one file name might be QID776_20160130_182739_0.orc.

> [!NOTE]
> When exporting data to Hadoop or Azure Blob Storage via PolyBase, only the data is exported, not the column names (metadata) as defined in the CREATE EXTERNAL TABLE command.

```sql  
-- PolyBase scenario  - export data from SQL Server to Hadoop
-- Create an external table
CREATE EXTERNAL TABLE [dbo].[FastCustomers2009] (  
        [FirstName] char(25) NOT NULL,   
        [LastName] char(25) NOT NULL,   
        [YearlyIncome] float NULL,   
        [MaritalStatus] char(1) NOT NULL  
)  
WITH (  
        LOCATION='/old_data/2009/customerdata',  
        DATA_SOURCE = HadoopHDP2,  
        FILE_FORMAT = TextFileFormat,  
        REJECT_TYPE = VALUE,  
        REJECT_VALUE = 0  
);  
  
-- Export data: Move old data to Hadoop while keeping it query-able via an external table.  
INSERT INTO dbo.FastCustomers2009  
SELECT T.* FROM Insured_Customers T1 JOIN CarSensor_Data T2  
ON (T1.CustomerKey = T2.CustomerKey)  
WHERE T2.YearMeasured = 2009 and T2.Speed > 40;  
```

## New catalog views

The following new catalog views show external resources.

```sql
SELECT * FROM sys.external_data_sources;   
SELECT * FROM sys.external_file_formats;  
SELECT * FROM sys.external_tables;  
```

 Determine if a table is an external table by using `is_external`  

```sql  
SELECT name, type, is_external FROM sys.tables WHERE name='myTableName'   
```  

## Next steps  

To learn more about troubleshooting, see [PolyBase troubleshooting](../../relational-databases/polybase/polybase-troubleshooting.md).
