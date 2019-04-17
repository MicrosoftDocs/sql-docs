---
title: "PolyBase configuration and security for Hadoop | Microsoft Docs"
ms.custom: ""
ms.date: 09/24/2018
ms.prod: sql
ms.reviewer: ""
ms.technology: polybase
ms.topic: conceptual
author: rothja
ms.author: jroth
manager: craigg
---

# PolyBase configuration and security for Hadoop

[!INCLUDE[appliesto-ss-xxxx-xxxx-xxx-md-winonly](../../includes/appliesto-ss-xxxx-xxxx-xxx-md-winonly.md)]

This article provides a reference for various configuration settings that affect PolyBase connectivity to Hadoop. For a walkthrough on how to use PolyBase with Hadoop, see [Configure PolyBase to access external data in Hadoop](polybase-configure-hadoop.md).

## <a id="rpcprotection"></a> Hadoop.RPC.Protection setting

A common way to secure communication in a hadoop cluster is by changing the hadoop.rpc.protection configuration to 'Privacy' or 'Integrity'. By default, PolyBase assumes the configuration is set to 'Authenticate'. To override this default, add the following property to the core-site.xml file. Changing this configuration will enable secure data transfer among the hadoop nodes and SSL connection to SQL Server.

```xml
<!-- RPC Encryption information, PLEASE FILL THESE IN ACCORDING TO HADOOP CLUSTER CONFIG -->
   <property>
     <name>hadoop.rpc.protection</name>
     <value></value>
   </property> 
```

To use 'Privacy' or 'Integrity' for hadoop.rpc.protection, SQL Server must be at least SQL Server 2016 SP1 CU7, SQL Server 2016 SP2, or SQL Server 2017 CU3.

## Example XML files for CDH 5.X cluster

Yarn-site.xml with yarn.application.classpath and mapreduce.application.classpath configuration.

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

If you choose to break your two configuration settings into the mapred-site.xml and the yarn-site.xml then the files would be the following:

**yarn-site.xml**

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

**mapred-site.xml**

Note that we added the property mapreduce.application.classpath. In CDH 5.x you will find the configuration values under the same naming convention in Ambari.

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

## Kerberos configuration  

Note, when PolyBase authenticates to a Kerberos secured cluster, it expects the hadoop.rpc.protection setting is 'Authenticate' by default. This leaves the data communication between Hadoop nodes unencrypted. To use 'Privacy' or 'Integrity' settings for hadoop.rpc.protection, update the core-site.xml file on the PolyBase server. For more information, see the previous section [Connecting to Hadoop Cluster with Hadoop.rpc.protection](#rpcprotection).

To connect to a Kerberos-secured Hadoop cluster using MIT KDC:

1. Find the Hadoop configuration directory in the installation path of SQL Server. Typically, the path is:  

   ```  
   C:\Program Files\Microsoft SQL Server\MSSQL13.MSSQLSERVER\MSSQL\Binn\PolyBase\Hadoop\conf  
   ```  

2. Find the Hadoop side configuration value of the configuration keys listed in the table. (On the Hadoop machine, find the files in the Hadoop configuration directory.)  
   
3. Copy the configuration values into the value property in the corresponding files on the SQL Server machine.  
   
   |**#**|**Configuration file**|**Configuration key**|**Action**|  
   |------------|----------------|---------------------|----------|   
   |1|core-site.xml|polybase.kerberos.kdchost|Specify the KDC hostname. For example: kerberos.your-realm.com.|  
   |2|core-site.xml|polybase.kerberos.realm|Specify the Kerberos realm. For example: YOUR-REALM.COM|  
   |3|core-site.xml|hadoop.security.authentication|Find the Hadoop side configuration and copy to SQL Server machine. For example: KERBEROS<br></br>**Security note:** KERBEROS must be written in upper case. If lower case, it might not be on.|   
   |4|hdfs-site.xml|dfs.namenode.kerberos.principal|Find the Hadoop side configuration and copy to SQL Server machine. For example: hdfs/_HOST@YOUR-REALM.COM|  
   |5|mapred-site.xml|mapreduce.jobhistory.principal|Find the Hadoop side configuration and copy to SQL Server machine. For example: mapred/_HOST@YOUR-REALM.COM|  
   |6|mapred-site.xml|mapreduce.jobhistory.address|Find the Hadoop side configuration and copy to SQL Server machine. For example: 10.193.26.174:10020|  
   |7|yarn-site.xml yarn.|yarn.resourcemanager.principal|Find the Hadoop side configuration and copy to SQL Server machine. For example: yarn/_HOST@YOUR-REALM.COM|  

4. Create a database-scoped credential object to specify the authentication information for each Hadoop user. See [PolyBase T-SQL objects](../../relational-databases/polybase/polybase-t-sql-objects.md).  

## Next steps  

For more information, see the following articles:

[Configure PolyBase to access external data in Hadoop](polybase-configure-hadoop.md)
[PolyBase overview](../../relational-databases/polybase/polybase-guide.md)
