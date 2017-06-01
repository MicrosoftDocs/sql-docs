---
title: "Get started with PolyBase | Microsoft Docs"
ms.custom: 
  - "SQL2016_New_Updated"
ms.date: "5/30/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine-polybase"
ms.tgt_pltfrm: ""
ms.topic: "get-started-article"
helpviewer_keywords: 
  - "PolyBase"
  - "PolyBase, getting started"
  - "Hadoop import"
  - "Hadoop export"
  - "Azure blob storage import"
  - "Azure blob storage export"
  - "Hadoop import, PolyBase getting started"
  - "Hadoop export, Polybase getting started"
ms.assetid: c71ddc50-b4c7-416c-9789-264671bd9ecb
caps.latest.revision: 78
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard" 
---
# Get started with PolyBase
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  This topic contains the basics about running PolyBase on a SQL Server instance.
  
 After running the steps below, you will have:  
  
-   PolyBase installed and runnable on your server  
  
-   Examples of statements that create PolyBase objects  
  
-   An understanding of how to manage PolyBase objects in SQL Server Management Studio (SSMS)  
  
-   Examples of queries using PolyBase objects  
  
## Prerequisites  
 An instance of  [SQL Server (64-bit)](https://www.microsoft.com/evalcenter/evaluate-sql-server-2016) with the following:  
  
-   Microsoft .NET Framework 4.5.  
  
-   Oracle Java SE RunTime Environment (JRE) version 7.51 or higher (64-bit). (Either [JRE](http://www.oracle.com/technetwork/java/javase/downloads/jre8-downloads-2133155.html) or [Server JRE](http://www.oracle.com/technetwork/java/javase/downloads/server-jre8-downloads-2133154.html) will work). Go to [Java SE downloads](http://www.oracle.com/technetwork/java/javase/downloads/index.html). The installer will fail if JRE is not present.   
  
-   Minimum memory: 4GB  
  
-   Minimum hard disk space: 2GB    
-   TCP/IP connectivity must be enabled. (See [Enable or Disable a Server Network Protocol](../../database-engine/configure-windows/enable-or-disable-a-server-network-protocol.md).)  
  
 
 An external data source, one of the following:  
  
-   Hadoop cluster. For supported versions see [Configure PolyBase](#supported).  

-   Azure Blob storage

> [!NOTE]
>   If you are going to use the computation pushdown functionality against Hadoop, you will need to ensure that the target Hadoop cluster has core components of HDFS, Yarn/MapReduce with Jobhistory server enabled. PolyBase submits the pushdown query via MapReduce and pulls status from the JobHistory Server. Without either component the query will fail. 

## Install PolyBase  
 If you haven't installed PolyBase, see  [PolyBase installation](../../relational-databases/polybase/polybase-installation.md).  
  
### How to confirm installation  
 After installation, run the following command to confirm that PolyBase has been successfully installed. If PolyBase is installed, returns 1; otherwise, 0.  
  
```tsql  
SELECT SERVERPROPERTY ('IsPolybaseInstalled') AS IsPolybaseInstalled;  
```  
  
##  <a name="supported"></a> Configure PolyBase  
 After installing, you must configure SQL Server to use either your Hadoop version or Azure Blob Storage. PolyBase supports two Hadoop providers, Hortonworks Data Platform (HDP) and Cloudera Distributed Hadoop (CDH).  The supported external data sources include:  
  
-   Hortonworks HDP 1.3 on Linux/Windows Server  
  
-   Hortonworks HDP 2.1 – 2.6 on Linux

-   Hortonworks HDP 2.1 - 2.3 on Windows Server  
  
-   Cloudera CDH 4.3 on Linux  
  
-   Cloudera CDH 5.1 – 5.5, 5.9 - 5.11 on Linux  
  
-   Azure Blob Storage  
  
>  [!NOTE]
> Azure Data Lake Store connectivity is only supported in Azure SQL Data Warehouse.
  
### External data source configuration  
  
1.  Run [sp_configure &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) ‘hadoop connectivity’ and set an appropriate value. By Default, the hadoop connectivity is set to 7. To find the value, see [PolyBase Connectivity Configuration &#40;Transact-SQL&#41;](../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md).  
      ```tsql  
    -- Values map to various external data sources.  
    -- Example: value 7 stands for Azure blob storage and Hortonworks HDP 2.3 on Linux.  
    sp_configure @configname = 'hadoop connectivity', @configvalue = 7;   
    GO   
  
    RECONFIGURE   
    GO   
    ```  
  
2.  You must restart  SQL Server using **services.msc**. Restarting SQL Server restarts these services:  
  
    -   SQL Server PolyBase Data Movement Service  
  
    -   SQL Server PolyBase Engine  
  
 ![stop and start PolyBase services in services.msc](../../relational-databases/polybase/media/polybase-stop-start.png "stop and start PolyBase services in services.msc")  
  
### Pushdown configuration  
 To improve query performance,  enable pushdown computation to a Hadoop cluster:  
  
1.  Find the file **yarn-site.xml** in the installation path of SQL Server. Typically, the path is:  
  
    ```  
  
    C:Program FilesMicrosoft SQL ServerMSSQL13.MSSQLSERVERMSSQLBinnPolybaseHadoopconf  
  
    ```  
  
2.  On the Hadoop machine, find the analogous file in the Hadoop configuration directory. In the file, find and copy the value of the configuration key yarn.application.classpath.  
  
3.  On the SQL Server machine, in the **yarn.site.xml file,** find the **yarn.application.classpath** property. Paste the value from the Hadoop machine into the value element.  
  
4. For all CDH 5.X versions, you will need to add the mapreduce.application.classpath configuration parameters either to the end of your yarn.site.xml file or into the mapred-site.xml file. HortonWorks includes these configurations within the yarn.application.classpath configurations. See [PolyBase configuration](../../relational-databases/polybase/polybase-configuration.md) for examples.

 
## Scale out PolyBase  
 The PolyBase group feature allows you to create a cluster of SQL Server instances to process large data sets from external data sources  in a scale-out fashion for better query performance.  
  
1.  Install SQL Server with PolyBase on multiple machines.  
  
2.  Select one SQL Server as head node.  
  
3.  Add other instances as compute nodes by running [sp_polybase_join_group](../../relational-databases/system-stored-procedures/polybase-stored-procedures-sp-polybase-join-group.md).  
  
    ```  
    -- Enter head node details:   
    -- head node machine name, head node dms control channel port, head node sql server name  
    EXEC sp_polybase_join_group 'PQTH4A-CMP01', 16450, 'MSSQLSERVER';  
  
    ```  
  
4.  Restart the PolyBase Data Movement Service on the compute nodes.  
  
 For details, see [PolyBase scale-out groups](../../relational-databases/polybase/polybase-scale-out-groups.md).  
  
## Create T-SQL objects  
 Create objects depending on the external data source, either Hadoop or Azure Storage.  
  
### Hadoop  
  
```sql  
-- 1: Create a database scoped credential.  
-- Create a master key on the database. This is required to encrypt the credential secret.  
  
CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'S0me!nfo';  
  
-- 2: Create a database scoped credential  for Kerberos-secured Hadoop clusters.  
-- IDENTITY: the Kerberos user name.  
-- SECRET: the Kerberos password  
  
CREATE DATABASE SCOPED CREDENTIAL HadoopUser1   
WITH IDENTITY = '<hadoop_user_name>', Secret = '<hadoop_password>';  
  
-- 3:  Create an external data source.  
-- LOCATION (Required) : Hadoop Name Node IP address and port.  
-- RESOURCE MANAGER LOCATION (Optional): Hadoop Resource Manager location to enable pushdown computation.  
-- CREDENTIAL (Optional):  the database scoped credential, created above.  
  
CREATE EXTERNAL DATA SOURCE MyHadoopCluster WITH (  
        TYPE = HADOOP,   
        LOCATION ='hdfs://10.xxx.xx.xxx:xxxx',   
        RESOURCE_MANAGER_LOCATION = '10.xxx.xx.xxx:xxxx',   
        CREDENTIAL = HadoopUser1        
);  
  
-- 4: Create an external file format.  
-- FORMAT TYPE: Type of format in Hadoop (DELIMITEDTEXT,  RCFILE, ORC, PARQUET).    
CREATE EXTERNAL FILE FORMAT TextFileFormat WITH (  
        FORMAT_TYPE = DELIMITEDTEXT,   
        FORMAT_OPTIONS (FIELD_TERMINATOR ='|',   
                USE_TYPE_DEFAULT = TRUE)  
  
-- 5:  Create an external table pointing to data stored in Hadoop.  
-- LOCATION: path to file or directory that contains the data (relative to HDFS root).  
  
CREATE EXTERNAL TABLE [dbo].[CarSensor_Data] (  
        [SensorKey] int NOT NULL,   
        [CustomerKey] int NOT NULL,   
        [GeographyKey] int NULL,   
        [Speed] float NOT NULL,   
        [YearMeasured] int NOT NULL  
)  
WITH (LOCATION='/Demo/',   
        DATA_SOURCE = MyHadoopCluster,  
        FILE_FORMAT = TextFileFormat  
);  
  
-- 6:  Create statistics on an external table.   
CREATE STATISTICS StatsForSensors on CarSensor_Data(CustomerKey, Speed)  
  
```  
  
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
  
CREATE EXTERNAL FILE FORMAT TextFileFormat WITH (  
        FORMAT_TYPE = DELIMITEDTEXT,   
        FORMAT_OPTIONS (FIELD_TERMINATOR ='|',   
                USE_TYPE_DEFAULT = TRUE)  
  
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
  
## PolyBase queries  
 There are three functions that PolyBase is suited for:  
  
-   ad-hoc queries against external tables.  
  
-   importing data.  
  
-   exporting data.  
  
### Query examples  
  
-   Ad-hoc queries  
  
    ```tsql  
    -- PolyBase Scenario 1: Ad-Hoc Query joining relational with Hadoop data   
    -- Select customers who drive faster than 35 mph: joining structured customer data stored   
    -- in SQL Server with car sensor data stored in Hadoop.  
    SELECT DISTINCT Insured_Customers.FirstName,Insured_Customers.LastName,   
            Insured_Customers. YearlyIncome, CarSensor_Data.Speed  
    FROM Insured_Customers, CarSensor_Data  
    WHERE Insured_Customers.CustomerKey = CarSensor_Data.CustomerKey and CarSensor_Data.Speed > 35   
    ORDER BY CarSensor_Data.Speed DESC  
    OPTION (FORCE EXTERNALPUSHDOWN);    -- or OPTION (DISABLE EXTERNALPUSHDOWN)  
    ```  
  
-   Importing data  
  
    ```tsql  
    -- PolyBase Scenario 2: Import external data into SQL Server.  
    -- Import data for fast drivers into SQL Server to do more in-depth analysis and  
    -- leverage Columnstore technology.  
  
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
  
-   Exporting data  
  
    ```  
    -- PolyBase Scenario 3: Export data from SQL Server to Hadoop.  
  
    -- Enable INSERT into external table  
    sp_configure ‘allow polybase export’, 1;  
    reconfigure  
  
    -- Create an external table.   
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
    INSERT INTO dbo.FastCustomer2009  
    SELECT T.* FROM Insured_Customers T1 JOIN CarSensor_Data T2  
    ON (T1.CustomerKey = T2.CustomerKey)  
    WHERE T2.YearMeasured = 2009 and T2.Speed > 40;  
    ```  
  
## Managing PolyBase objects in SSMS  
 In SSMS, external tables are displayed in a separate folder **External Tables**. External data sources and external file formats are in subfolders under **External Resources**.  
  
 ![PolyBase objects in SSMS](../../relational-databases/polybase/media/polybase-management.png "PolyBase objects in SSMS")  
  
## Troubleshooting  
 Use DMVs to troubleshoot performance and queries. For details, see [PolyBase troubleshooting](../../relational-databases/polybase/polybase-troubleshooting.md).  
  
 After upgrading from SQL Server 2016 RC1 to RC2 or RC3, queries may fail. For details and a remedy, see [SQL Server 2016 Release Notes](../../sql-server/sql-server-2016-release-notes.md) and search for "PolyBase."  
  
## Next steps  
 To understand the scale-out feature, see [PolyBase scale-out groups](../../relational-databases/polybase/polybase-scale-out-groups.md).  To monitor PolyBase, see [PolyBase troubleshooting](../../relational-databases/polybase/polybase-troubleshooting.md). To trouble shoot PolyBase perfomance, see [PolyBase troubleshooting with dynamic management views](http://msdn.microsoft.com/library/ce9078b7-a750-4f47-b23e-90b83b783d80).  
  
## See Also  
 [PolyBase Guide](../../relational-databases/polybase/polybase-guide.md)   
 [PolyBase scale-out groups](../../relational-databases/polybase/polybase-scale-out-groups.md)   
 [PolyBase stored procedures](http://msdn.microsoft.com/library/a522b303-bd1b-410b-92d1-29c950a15ede)   
 [CREATE EXTERNAL DATA SOURCE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-data-source-transact-sql.md)   
 [CREATE EXTERNAL FILE FORMAT &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-file-format-transact-sql.md)   
 [CREATE EXTERNAL TABLE &#40;Transact-SQL&#41;](../../t-sql/statements/create-external-table-transact-sql.md)  
  
  
