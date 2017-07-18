---
title: "PolyBase configuration | Microsoft Docs"
ms.custom: ""
ms.date: "07/11/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "database-engine"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 80ff73c1-2861-438b-a13f-309155f3d6e1
caps.latest.revision: 17
author: "barbkess"
ms.author: "barbkess"
manager: "jhubbard"
---
# PolyBase configuration
[!INCLUDE[tsql-appliesto-ss2016-xxxx-xxxx-xxx_md](../../includes/tsql-appliesto-ss2016-xxxx-xxxx-xxx-md.md)]

  Use the procedures below to configure PolyBase.  
  
## External data source configuration  
 You must ensure connectivity to the external data source from SQL Server. The type of connectivity strongly influences query performance. For example, a 10Gbit Ethernet link will result in a faster query response time for PolyBase queries than a 1Gbit Ethernet link.  
  
 You must configure SQL Server to connect to  either your Hadoop version or Azure Blob storage using **sp_configure**. PolyBase supports two Hadoop distributions: Hortonworks Data Platform (HDP) and Cloudera Distributed Hadoop (CDH).  For a complete list of supported external data sources, see [PolyBase Connectivity Configuration &#40;Transact-SQL&#41;](../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md).  
 
 Please note: PolyBase does not support Cloudera Encrypted Zones. 
  
### Run sp_configure  
  
1.  Run sp_configure ‘hadoop connectivity’ and set an appropriate value.  To find the value, see [PolyBase Connectivity Configuration &#40;Transact-SQL&#41;](../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md).  
  
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
  
## Pushdown configuration  
 To improve query performance, enable pushdown computation to a Hadoop cluster you will need to provide SQL Server some configuration parameters specific to your Hadoop environment:  
  
1.  Find the file **yarn-site.xml** in the installation path of SQL Server. Typically, the path is:  
  
    ```  
    C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn\Polybase\Hadoop\conf  
    ```  
  
2.  On the Hadoop machine, find the analogous file in the Hadoop configuration directory. In the file, find and copy the value of the configuration key yarn.application.classpath.  
  
3.  On the SQL Server machine, in the **yarn.site.xml file,** find the **yarn.application.classpath** property. Paste the value from the Hadoop machine into the value element.  

4. For all CDH 5.X versions, you will need to add the **mapreduce.application.classpath** configuration parameters either to the end of your **yarn.site.xml file** or into the **mapred-site.xml file**. HortonWorks includes these configurations within the **yarn.application.classpath** configurations.

## Example yarn-site.xml and mapred-site.xml files for CDH 5.X cluster.



Yarn-site.xml with yarn.application.classpath and mapreduce.application.classpath configuration.
```
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
If you choose to break your two configuration settings into the mapred-site.xml and the yarn-site.xml then the files would be the following:

**yarn-site.xml**
```
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

**mapred-site.xml**

Note that we added the property mapreduce.application.classpath. In CDH 5.x you will find the configuration values under the same naming convention in Ambari.

```
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
  
## Kerberos configuration  
Please note, that when PolyBase authenticates to a Kerberos secured cluster, we require the hadoop.rpc.protection setting to be set to authentication. This will leave the data communication between Hadoop nodes unencrypted. 

 To connect to a Kerberos-secured Hadoop cluster [using MIT KDC] :
   
  
1.  Find the Hadoop configuration directory in the installation path of SQL Server. Typically, the path is:  
  
    ```  
    C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn\Polybase\Hadoop\conf  
    ```  
  
2.  Find the Hadoop side configuration value of the configuration keys listed in the table. (On the Hadoop machine, find the files in the Hadoop configuration directory.)  
  
3.  Copy the configuration values into the value property in the corresponding files on the SQL Server machine.  
  
    |**#**|**Configuration file**|**Configuration key**|**Action**|  
    |------------|----------------|---------------------|----------|   
    |1|core-site.xml|polybase.kerberos.kdchost|Specify the KDC hostname. For example: kerberos.your-realm.com.|  
    |2|core-site.xml|polybase.kerberos.realm|Specify the Kerberos realm. For example: YOUR-REALM.COM|  
    |3|core-site.xml|hadoop.security.authentication|Find the Hadoop side configuration and copy to SQL Server machine. For example: KERBEROS<br></br>**Security note:** KERBEROS must be written in upper case. If lower case, it might not be on.|   
    |4|hdfs-site.xml|dfs.namenode.kerberos.principal|Find the Hadoop side configuration and copy to SQL Server machine. For example: hdfs/_HOST@YOUR-REALM.COM|  
    |5|mapred-site.xml|mapreduce.jobhistory.principal|Find the Hadoop side configuration and copy to SQL Server machine. For example: mapred/_HOST@YOUR-REALM.COM|  
    |6|mapred-site.xml|mapreduce.jobhistory.address|Find the Hadoop side configuration and copy to SQL Server machine. For example: 10.193.26.174:10020|  
    |7|yarn-site.xml yarn.|yarn.resourcemanager.principal|Find the Hadoop side configuration and copy to SQL Server machine. For example: yarn/_HOST@YOUR-REALM.COM|  
  
4.  Create a database-scoped credential object to specify the authentication information for each Hadoop user. See [PolyBase T-SQL objects](../../relational-databases/polybase/polybase-t-sql-objects.md).  
  
## Next steps  
 [PolyBase T-SQL objects](../../relational-databases/polybase/polybase-t-sql-objects.md)  
  
 [Get started with PolyBase](../../relational-databases/polybase/get-started-with-polybase.md)  
  
## See Also  
 [PolyBase Connectivity Configuration &#40;Transact-SQL&#41;](../../database-engine/configure-windows/polybase-connectivity-configuration-transact-sql.md)   
 [PolyBase Guide](../../relational-databases/polybase/polybase-guide.md)  
  
  
