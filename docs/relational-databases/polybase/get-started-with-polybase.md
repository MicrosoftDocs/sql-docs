---
title: "Get started with PolyBase | Microsoft Docs"
ms.custom: ""
ms.date: "08/15/2017"
ms.prod: sql
ms.reviewer: ""
ms.suite: "sql"
ms.technology: polybase
ms.tgt_pltfrm: ""
ms.topic: quickstart
helpviewer_keywords: 
  - "PolyBase"
  - "PolyBase, getting started"
  - "Hadoop import"
  - "Hadoop export"
  - "Azure blob storage import"
  - "Azure blob storage export"
  - "Hadoop import, PolyBase getting started"
  - "Hadoop export, Polybase getting started"
author: rothja
ms.author: jroth
manager: craigg 
---
# Get started with PolyBase

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

## Scale out PolyBase  

The PolyBase group feature allows you to create a cluster of SQL Server instances to process large data sets from external data sources  in a scale-out fashion for better query performance.  
  
1. Install SQL Server with PolyBase on multiple machines.  
  
2. Select one SQL Server as head node.  
  
3. Add other instances as compute nodes by running [sp_polybase_join_group](../../relational-databases/system-stored-procedures/polybase-stored-procedures-sp-polybase-join-group.md).  

  ```sql  
  -- Enter head node details:   
  -- head node machine name, head node dms control channel port, head node sql server name  
  EXEC sp_polybase_join_group 'PQTH4A-CMP01', 16450, 'MSSQLSERVER';  
  ```  

4. Restart the PolyBase Data Movement Service on the compute nodes.  

For details, see [PolyBase scale-out groups](../../relational-databases/polybase/polybase-scale-out-groups.md).  
  
## Create T-SQL objects  

Create objects depending on the external data source, either Hadoop or Azure Storage.  
  
### Azure Blob Storage  

```sql  
--1: Create a master key on the database.  
-- Required to encrypt the credential secret.  
  
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'S0me!nfo';  
  
-- Create a database scoped credential  for Azure blob storage.  
-- IDENTITY: any string (this is not used for authentication to Azure storage).  
-- SECRET: your Azure storage account key.  
  
CREATE DATABASE SCOPED CREDENTIAL AzureStorageCredential   
WITH IDENTITY = 'user', Secret = '<azure_storage_account_key>';  
  
--2:  Create an external data source.  
-- LOCATION:  Azure account storage account name and blob container name.  
-- CREDENTIAL: The database scoped credential created above.  
  
CREATE EXTERNAL DATA SOURCE AzureStorage with (  
      TYPE = HADOOP,   
      LOCATION ='wasbs://<blob_container_name>@<azure_storage_account_name>.blob.core.windows.net',  
      CREDENTIAL = AzureStorageCredential  
);  
  
--3:  Create an external file format.  
-- FORMAT TYPE: Type of format in Hadoop (DELIMITEDTEXT,  RCFILE, ORC, PARQUET).  
  
CREATE EXTERNAL FILE FORMAT TextFileFormat
WITH (  
      FORMAT_TYPE = DELIMITEDTEXT,   
      FORMAT_OPTIONS (
       FIELD_TERMINATOR ='|',   
       USE_TYPE_DEFAULT = TRUE
      )
);

  
--4: Create an external table.  
-- The external table points to data stored in Azure storage.  
-- LOCATION: path to a file or directory that contains the data (relative to the blob container).  
-- To point to all files under the blob container, use LOCATION='/'   
  
CREATE EXTERNAL TABLE [dbo].[CarSensor_Data] (  
      [SensorKey] int NOT NULL,   
      [CustomerKey] int NOT NULL,   
      [GeographyKey] int NULL,   
      [Speed] float NOT NULL,   
      [YearMeasured] int NOT NULL  
)  
WITH (LOCATION='/Demo/',   
      DATA_SOURCE = AzureStorage,  
      FILE_FORMAT = TextFileFormat  
);  
  
--5: Create statistics on an external table.   
CREATE STATISTICS StatsForSensors on CarSensor_Data(CustomerKey, Speed)  
  
```  

## Managing PolyBase objects in SSMS  

In SSMS, external tables are displayed in a separate folder **External Tables**. External data sources and external file formats are in subfolders under **External Resources**.  
  
![PolyBase objects in SSMS](../../relational-databases/polybase/media/polybase-management.png "PolyBase objects in SSMS")  
  
## Troubleshooting  

Use DMVs to troubleshoot performance and queries. For details, see [PolyBase troubleshooting](../../relational-databases/polybase/polybase-troubleshooting.md).  
  
After upgrading from SQL Server 2016 RC1 to RC2 or RC3, queries may fail. For details and a remedy, see [SQL Server 2016 Release Notes](../../sql-server/sql-server-2016-release-notes.md) and search for "PolyBase."  
  
## Next steps  

To understand the scale-out feature, see [PolyBase scale-out groups](../../relational-databases/polybase/polybase-scale-out-groups.md).  To monitor PolyBase, see [PolyBase troubleshooting](../../relational-databases/polybase/polybase-troubleshooting.md). To troubleshoot PolyBase performance, see [PolyBase troubleshooting with dynamic management views](http://msdn.microsoft.com/library/ce9078b7-a750-4f47-b23e-90b83b783d80).  
  
## See Also  

[PolyBase Guide](../../relational-databases/polybase/polybase-guide.md)   
[PolyBase scale-out groups](../../relational-databases/polybase/polybase-scale-out-groups.md)   
[PolyBase stored procedures](http://msdn.microsoft.com/library/a522b303-bd1b-410b-92d1-29c950a15ede)   
[CREATE EXTERNAL DATA SOURCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-data-source-transact-sql.md)   
[CREATE EXTERNAL FILE FORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-file-format-transact-sql.md)   
[CREATE EXTERNAL TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-transact-sql.md)