---
title: "Configure PolyBase to access external data in Hadoop | Microsoft Docs"
description: Explains how to configure PolyBase in Parallel Data Warehouse to connect to external Hadoop. 
author: mzaman1 
manager: craigg
ms.prod: sql
ms.technology: data-warehouse
ms.topic: conceptual
ms.date: 04/17/2018
ms.author: murshedz
ms.reviewer: martinle
---
# Configure PolyBase to access external data in Hadoop

The article explains how to use PolyBase on an APS appliance to query external data in Hadoop.

## Prerequisites

PolyBase supports two Hadoop providers, Hortonworks Data Platform (HDP) and Cloudera Distributed Hadoop (CDH). Hadoop follows the "Major.Minor.Version" pattern for its new releases, and all versions within a supported Major and Minor release are supported. The following Hadoop providers are supported:
 - Hortonworks HDP 1.3 on Linux/Windows Server  
 - Hortonworks HDP 2.1 - 2.6 on Linux
 - Hortonworks HDP 2.1 - 2.3 on Windows Server  
 - Cloudera CDH 4.3 on Linux  
 - Cloudera CDH 5.1 - 5.5, 5.9 - 5.13 on Linux

### Configure Hadoop connectivity

First, configure APS to use your specific Hadoop provider.

1. Run [sp_configure](../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) with 'hadoop connectivity' and set an appropriate value for your provider. To find the value for your provider, see [PolyBase Connectivity Configuration](../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md). 

   ```sql  
   -- Values map to various external data sources.  
   -- Example: value 7 stands for Hortonworks HDP 2.1 to 2.6 on Linux,
   -- 2.1 to 2.3 on Windows Server, and Azure blob storage  
   sp_configure @configname = 'hadoop connectivity', @configvalue = 7;
   GO

   RECONFIGURE
   GO
   ```  

2. Restart APS Region using Service Status page on [Appliance Configuration Manager](launch-the-configuration-manager.md).
  
## <a id="pushdown"></a> Enable pushdown computation  

To improve query performance, enable pushdown computation to your Hadoop cluster:  
  
1. Open a remote desktop connection to PDW Control node.

2. Find the file **yarn-site.xml** on the Control node. Typically, the path is:  

   ```xml  
   C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100\Hadoop\conf\  
   ```  

3. On the Hadoop machine, find the analogous file in the Hadoop configuration directory. In the file, find and copy the value of the configuration key yarn.application.classpath.  
  
4. On the Control node, in the **yarn.site.xml file,** find the **yarn.application.classpath** property. Paste the value from the Hadoop machine into the value element.  
  
5. For all CDH 5.X versions, you will need to add the mapreduce.application.classpath configuration parameters either to the end of your yarn.site.xml file or into the mapred-site.xml file. HortonWorks includes these configurations within the yarn.application.classpath configurations. See [PolyBase configuration](../relational-databases/polybase/polybase-configuration.md) for examples.

## Configure an external table

To query the data in your Hadoop data source, you must define an external table to use in Transact-SQL queries. The following steps describe how to configure the external table.

1. Create a master key on the database. It is required to encrypt the credential secret.

   ```sql
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'S0me!nfo';  
   ```

2. Create a database scoped credential for Kerberos-secured Hadoop clusters.

   ```sql
   -- IDENTITY: the Kerberos user name.  
   -- SECRET: the Kerberos password  
   CREATE DATABASE SCOPED CREDENTIAL HadoopUser1
   WITH IDENTITY = '<hadoop_user_name>', Secret = '<hadoop_password>';  
   ```

3. Create an external data source with [CREATE EXTERNAL DATA SOURCE](../t-sql/statements/create-external-data-source-transact-sql.md).

   ```sql
   -- LOCATION (Required) : Hadoop Name Node IP address and port.  
   -- RESOURCE MANAGER LOCATION (Optional): Hadoop Resource Manager location to enable pushdown computation.  
   -- CREDENTIAL (Optional):  the database scoped credential, created above.  
   CREATE EXTERNAL DATA SOURCE MyHadoopCluster WITH (  
         TYPE = HADOOP,
         LOCATION ='hdfs://10.xxx.xx.xxx:xxxx',
         RESOURCE_MANAGER_LOCATION = '10.xxx.xx.xxx:xxxx',
         CREDENTIAL = HadoopUser1
   );  
   ```

4. Create an external file format with [CREATE EXTERNAL FILE FORMAT](../t-sql/statements/create-external-file-format-transact-sql.md).

   ```sql
   -- FORMAT TYPE: Type of format in Hadoop (DELIMITEDTEXT,  RCFILE, ORC, PARQUET).
   CREATE EXTERNAL FILE FORMAT TextFileFormat WITH (  
         FORMAT_TYPE = DELIMITEDTEXT,
         FORMAT_OPTIONS (FIELD_TERMINATOR ='|',
               USE_TYPE_DEFAULT = TRUE)  
   ```

5. Create an external table pointing to data stored in Hadoop with [CREATE EXTERNAL TABLE](../t-sql/statements/create-external-table-transact-sql.md). In this example, the external data contains car sensor data.

   ```sql
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
   ```

6. Create statistics on an external table.

   ```sql
   CREATE STATISTICS StatsForSensors on CarSensor_Data(CustomerKey, Speed)  
   ```

## PolyBase queries

There are three functions that PolyBase is suited for:  
  
- Ad hoc queries against external tables.  
- Importing data.  
- Exporting data.  

The following queries provide example with fictional car sensor data.

### Ad hoc queries  

The following ad hoc query joins relational with Hadoop data. It selects customers who drive faster than 35 mph, joining structured customer data stored in APS with car sensor data stored in Hadoop.  

```sql  
SELECT DISTINCT Insured_Customers.FirstName,Insured_Customers.LastName,
       Insured_Customers. YearlyIncome, CarSensor_Data.Speed  
FROM Insured_Customers, CarSensor_Data  
WHERE Insured_Customers.CustomerKey = CarSensor_Data.CustomerKey and CarSensor_Data.Speed > 35
ORDER BY CarSensor_Data.Speed DESC  
OPTION (FORCE EXTERNALPUSHDOWN);   -- or OPTION (DISABLE EXTERNALPUSHDOWN)  
```  

### Importing data  

The following query imports external data into APS. This example imports data for fast drivers into APS to do more in-depth analysis. To improve performance, it leverages Columnstore technology in APS.  

```sql
CREATE TABLE Fast_Customers
WITH
(CLUSTERED COLUMNSTORE INDEX, DISTRIBUTION = HASH (CustomerKey))
AS
SELECT DISTINCT
      Insured_Customers.CustomerKey, Insured_Customers.FirstName, Insured_Customers.LastName,   
      Insured_Customers.YearlyIncome, Insured_Customers.MaritalStatus  
from Insured_Customers INNER JOIN   
(  
      SELECT * FROM CarSensor_Data where Speed > 35   
) AS SensorD  
ON Insured_Customers.CustomerKey = SensorD.CustomerKey  
```  

### Exporting data  

The following query exports data from APS to Hadoop. It can be used to archive relational data to Hadoop while still be able to query it.

```sql
-- Export data: Move old data to Hadoop while keeping it query-able via an external table.  
CREATE EXTERNAL TABLE [dbo].[FastCustomers2009] 
WITH (  
      LOCATION='/archive/customer/2009',  
      DATA_SOURCE = HadoopHDP2,  
      FILE_FORMAT = TextFileFormat
)  
AS
SELECT T.* FROM Insured_Customers T1 JOIN CarSensor_Data T2  
ON (T1.CustomerKey = T2.CustomerKey)  
WHERE T2.YearMeasured = 2009 and T2.Speed > 40;  
```  

## View PolyBase objects in SSDT  

In SQL Server Data Tools, external tables are displayed in a separate folder **External Tables**. External data sources and external file formats are in subfolders under **External Resources**.  
  
![PolyBase objects in SSDT](media/polybase/external-tables-datasource.png)  

## Next steps

For Hadoop security settings see [configure Hadoop security](polybase-configure-hadoop-security.md).<br>
For more information about PolyBase, see the [What is PolyBase?](../relational-databases/polybase/polybase-guide.md). 
 
