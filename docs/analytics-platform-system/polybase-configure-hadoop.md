---
title: "Access external data: Hadoop - PolyBase"
description: Explains how to configure PolyBase in Analytics Platform System (PDW) to connect to external Hadoop.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 10/04/2022
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom:
  - seo-dt-2019
  - seo-lt-2019
---
# Configure PolyBase in Analytics Platform System (PDW) to access external data in Hadoop

The article explains how to use PolyBase on an [!INCLUDE[sspdw-md](../includes/sspdw-md.md)] or APS appliance to query external data in Hadoop.

## Prerequisites

PolyBase supports two Hadoop providers, Hortonworks Data Platform (HDP) and Cloudera Distributed Hadoop (CDH). Hadoop follows the "Major.Minor.Version" pattern for its new releases, and all versions within a supported Major and Minor release are supported. The following Hadoop providers are supported:
 - Hortonworks HDP 1.3 on Linux/Windows Server
 - Hortonworks HDP 2.1 - 2.6 on Linux
 - Hortonworks HDP 3.0 - 3.1 on Linux
 - Hortonworks HDP 2.1 - 2.3 on Windows Server  
 - Cloudera CDH 4.3 on Linux  
 - Cloudera CDH 5.1 - 5.5, 5.9 - 5.13, 5.15 & 5.16 on Linux

### Configure Hadoop connectivity

First, configure APS to use your specific Hadoop provider.

1. Run [sp_configure](../relational-databases/system-stored-procedures/sp-configure-transact-sql.md) with 'hadoop connectivity' and set an appropriate value for your provider. To find the value for your provider, see [PolyBase Connectivity Configuration](../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md).

   ```sql
   -- Values map to various external data sources.
   -- Example: value 7 stands for Hortonworks HDP 2.1 to 2.6 and 3.0 - 3.1 on Linux,
   -- 2.1 to 2.3 on Windows Server, and Azure Blob Storage
   sp_configure @configname = 'hadoop connectivity', @configvalue = 7;
   GO

   RECONFIGURE
   GO
   ```

2. Restart APS Region using Service Status page on [Appliance Configuration Manager](launch-the-configuration-manager.md).

## <a id="pushdown"></a> Enable pushdown computation

To improve query performance, enable pushdown computation to your Hadoop cluster:

1. Open a remote desktop connection to APS PDW Control node.

1. Find the file `yarn-site.xml` on the Control node. Typically, the path is: `C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100\Hadoop\conf\`.

1. On the Hadoop machine, find the analogous file in the Hadoop configuration directory. In the file, find and copy the value of the configuration key `yarn.application.classpath`.

1. On the Control node, in the `yarn.site.xml` file, find the `yarn.application.classpath` property. Paste the value from the Hadoop machine into the value element.

1. For all CDH 5.X versions, you will need to add the `mapreduce.application.classpath` configuration parameters either to the end of your `yarn.site.xml` file or into the `mapred-site.xml` file. HortonWorks includes these configurations within the `yarn.application.classpath` configurations. For examples, see [PolyBase configuration](../relational-databases/polybase/polybase-configuration.md).

## Example XML files for CDH 5.X cluster default values

`Yarn-site.xml` with `yarn.application.classpath` and `mapreduce.application.classpath` configuration.

```xml
<?xml version="1.0" encoding="utf-8"?>
<?xml-stylesheet type="text/xsl" href="configuration.xsl"?>
<!-- Put site-specific property overrides in this file. -->
 <configuration>
   <property>
      <name>yarn.resourcemanager.connect.max-wait.ms</name>
      <value>40000</value>
   </property>
   <property>
      <name>yarn.resourcemanager.connect.retry-interval.ms</name>
      <value>30000</value>
   </property>
<!-- Applications' Configuration-->
   <property>
     <description>CLASSPATH for YARN applications. A comma-separated list of CLASSPATH entries</description>
      <!-- Please set this value to the correct yarn.application.classpath that matches your server side configuration -->
      <!-- For example: $HADOOP_CONF_DIR,$HADOOP_COMMON_HOME/share/hadoop/common/*,$HADOOP_COMMON_HOME/share/hadoop/common/lib/*,$HADOOP_HDFS_HOME/share/hadoop/hdfs/*,$HADOOP_HDFS_HOME/share/hadoop/hdfs/lib/*,$HADOOP_YARN_HOME/share/hadoop/yarn/*,$HADOOP_YARN_HOME/share/hadoop/yarn/lib/* -->
      <name>yarn.application.classpath</name>
      <value>$HADOOP_CLIENT_CONF_DIR,$HADOOP_CONF_DIR,$HADOOP_COMMON_HOME/*,$HADOOP_COMMON_HOME/lib/*,$HADOOP_HDFS_HOME/*,$HADOOP_HDFS_HOME/lib/*,$HADOOP_YARN_HOME/*,$HADOOP_YARN_HOME/lib/,$HADOOP_MAPRED_HOME/*,$HADOOP_MAPRED_HOME/lib/*,$MR2_CLASSPATH*</value>
   </property>

<!-- kerberos security information, PLEASE FILL THESE IN ACCORDING TO HADOOP CLUSTER CONFIG
   <property>
      <name>yarn.resourcemanager.principal</name>
      <value></value>
   </property>
-->
</configuration>
```

If you choose to break your two configuration settings into the `mapred-site.xml` and the `yarn-site.xml`, then the files would be the following:

For `yarn-site.xml`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<?xml-stylesheet type="text/xsl" href="configuration.xsl"?>
<!-- Put site-specific property overrides in this file. -->
 <configuration>
   <property>
      <name>yarn.resourcemanager.connect.max-wait.ms</name>
      <value>40000</value>
   </property>
   <property>
      <name>yarn.resourcemanager.connect.retry-interval.ms</name>
      <value>30000</value>
   </property>
<!-- Applications' Configuration-->
   <property>
     <description>CLASSPATH for YARN applications. A comma-separated list of CLASSPATH entries</description>
      <!-- Please set this value to the correct yarn.application.classpath that matches your server side configuration -->
      <!-- For example: $HADOOP_CONF_DIR,$HADOOP_COMMON_HOME/share/hadoop/common/*,$HADOOP_COMMON_HOME/share/hadoop/common/lib/*,$HADOOP_HDFS_HOME/share/hadoop/hdfs/*,$HADOOP_HDFS_HOME/share/hadoop/hdfs/lib/*,$HADOOP_YARN_HOME/share/hadoop/yarn/*,$HADOOP_YARN_HOME/share/hadoop/yarn/lib/* -->
      <name>yarn.application.classpath</name>
      <value>$HADOOP_CLIENT_CONF_DIR,$HADOOP_CONF_DIR,$HADOOP_COMMON_HOME/*,$HADOOP_COMMON_HOME/lib/*,$HADOOP_HDFS_HOME/*,$HADOOP_HDFS_HOME/lib/*,$HADOOP_YARN_HOME/*,$HADOOP_YARN_HOME/lib/*</value>
   </property>

<!-- kerberos security information, PLEASE FILL THESE IN ACCORDING TO HADOOP CLUSTER CONFIG
   <property>
      <name>yarn.resourcemanager.principal</name>
      <value></value>
   </property>
-->
</configuration>
```

For `mapred-site.xml`:

Note the property `mapreduce.application.classpath`. In CDH 5.x, you will find the configuration values under the same naming convention in Ambari.

```xml
<?xml version="1.0"?>
<?xml-stylesheet type="text/xsl" href="configuration.xsl"?>
<!-- Put site-specific property overrides in this file. -->
<configuration xmlns:xi="http://www.w3.org/2001/XInclude">
   <property>
     <name>mapred.min.split.size</name>
       <value>1073741824</value>
   </property>
   <property>
     <name>mapreduce.app-submission.cross-platform</name>
     <value>true</value>
   </property>
<property>
     <name>mapreduce.application.classpath</name>
     <value>$HADOOP_MAPRED_HOME/*,$HADOOP_MAPRED_HOME/lib/*,$MR2_CLASSPATH</value>
   </property>


<!--kerberos security information, PLEASE FILL THESE IN ACCORDING TO HADOOP CLUSTER CONFIG
   <property>
     <name>mapreduce.jobhistory.principal</name>
     <value></value>
   </property>
   <property>
     <name>mapreduce.jobhistory.address</name>
     <value></value>
   </property>
-->
</configuration>
```

## Example XML files for HDP 3.X cluster default values

For `yarn-site.xml`:

```xml
<?xml version="1.0" encoding="utf-8"?>
<?xml-stylesheet type="text/xsl" href="configuration.xsl"?>
<!-- Put site-specific property overrides in this file. -->
 <configuration>
  <property>
     <name>yarn.resourcemanager.connect.max-wait.ms</name>
     <value>40000</value>
  </property>
  <property>
     <name>yarn.resourcemanager.connect.retry-interval.ms</name>
     <value>30000</value>
  </property>
<!-- Applications' Configuration-->
  <property>
    <description>CLASSPATH for YARN applications. A comma-separated list of CLASSPATH entries</description>
     <!-- Please set this value to the correct yarn.application.classpath that matches your server side configuration -->
     <!-- For example: $HADOOP_CONF_DIR,$HADOOP_COMMON_HOME/share/hadoop/common/*,$HADOOP_COMMON_HOME/share/hadoop/common/lib/*,$HADOOP_HDFS_HOME/share/hadoop/hdfs/*,$HADOOP_HDFS_HOME/share/hadoop/hdfs/lib/*,$HADOOP_YARN_HOME/share/hadoop/yarn/*,$HADOOP_YARN_HOME/share/hadoop/yarn/lib/* -->
     <name>yarn.application.classpath</name>
     <value>$HADOOP_CONF_DIR,/usr/hdp/3.1.0.0-78/hadoop/*,/usr/hdp/3.1.0.0-78/hadoop/lib/*,/usr/hdp/current/hadoop-hdfs-client/*,/usr/hdp/current/hadoop-hdfs-client/lib/*,/usr/hdp/current/hadoop-yarn-client/*,/usr/hdp/current/hadoop-yarn-client/lib/*,/usr/hdp/3.1.0.0-78/hadoop-mapreduce/*,/usr/hdp/3.1.0.0-78/hadoop-yarn/*,/usr/hdp/3.1.0.0-78/hadoop-yarn/lib/*,/usr/hdp/3.1.0.0-78/hadoop-mapreduce/lib/*,/usr/hdp/share/hadoop/common/*,/usr/hdp/share/hadoop/common/lib/*,/usr/hdp/share/hadoop/tools/lib/*</value>
  </property>

<!-- kerberos security information, PLEASE FILL THESE IN ACCORDING TO HADOOP CLUSTER CONFIG
  <property>
     <name>yarn.resourcemanager.principal</name>
     <value></value>
  </property>
-->
</configuration>
```

## Configure an external table

To query the data in your Hadoop data source, you must define an external table to use in Transact-SQL queries. The following steps describe how to configure the external table.

1. Create a master key on the database. It is required to encrypt the credential secret.

   ```sql
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = 'S0me!nfo';
   ```

1. Create a database scoped credential for Kerberos-secured Hadoop clusters.

   ```sql
   -- IDENTITY: the Kerberos user name.
   -- SECRET: the Kerberos password
   CREATE DATABASE SCOPED CREDENTIAL HadoopUser1
   WITH IDENTITY = '<hadoop_user_name>', Secret = '<hadoop_password>';
   ```

1. Create an external data source with [CREATE EXTERNAL DATA SOURCE](../t-sql/statements/create-external-data-source-transact-sql.md).

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

1. Create an external file format with [CREATE EXTERNAL FILE FORMAT](../t-sql/statements/create-external-file-format-transact-sql.md).

   ```sql
   -- FORMAT TYPE: Type of format in Hadoop (DELIMITEDTEXT,  RCFILE, ORC, PARQUET).
   CREATE EXTERNAL FILE FORMAT TextFileFormat WITH (
         FORMAT_TYPE = DELIMITEDTEXT,
         FORMAT_OPTIONS (FIELD_TERMINATOR ='|',
               USE_TYPE_DEFAULT = TRUE)
   ```

1. Create an external table pointing to data stored in Hadoop with [CREATE EXTERNAL TABLE](../t-sql/statements/create-external-table-transact-sql.md). In this example, the external data contains car sensor data.

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

1. Create statistics on an external table.

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

### Import data

The following query imports external data into APS. This example imports data for fast drivers into APS to do more in-depth analysis. To improve performance, it leverages columnstore technology in APS.

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

### Export data

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

:::image type="content" source="media/polybase/external-tables-datasource.png" alt-text="A screenshot of PolyBase objects in SQL Server Data Tools (SSDT).":::

## Next steps

- For Hadoop security settings see [configure Hadoop security](polybase-configure-hadoop-security.md).<br>
- For more information about PolyBase, see the [What is PolyBase?](../relational-databases/polybase/polybase-guide.md).
