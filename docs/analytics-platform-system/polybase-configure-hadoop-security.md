---
title: "Configure PolyBase Hadoop security"
description: Provides a reference for various configuration settings that affect APS PolyBase connectivity to Hadoop.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 10/26/2018
ms.service: sql
ms.subservice: data-warehouse
ms.topic: conceptual
ms.custom: seo-dt-2019
---
# Configure PolyBase Hadoop security

This article provides a reference for various configuration settings that affect APS PolyBase connectivity to Hadoop. For a walkthrough on what is PolyBase, see [What is PolyBase](configure-polybase-connectivity-to-external-data.md).

> [!NOTE]
> On APS, changes on XML files are needed on all compute nodes and control node.
> 
> Take special care when modifying XML files in APS. Any missing tags or unwanted characters can invalidate the xml file hindering the usablilty of the feature.
> Hadoop configuration files are located in the following path:  
> ```  
> C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100\Hadoop\conf 
> ``` 
> Any changes to the xml files require a service restart to be effective.

## <a id="rpcprotection"></a> Hadoop.RPC.Protection setting

A common way to secure communication in a hadoop cluster is by changing the hadoop.rpc.protection configuration to 'Privacy' or 'Integrity'. By default, PolyBase assumes the configuration is set to 'Authenticate'. To override this default, add the following property to the core-site.xml file. Changing this configuration will enable secure data transfer among the hadoop nodes and SSL connection to SQL Server.

```xml
<!-- RPC Encryption information, PLEASE FILL THESE IN ACCORDING TO HADOOP CLUSTER CONFIG -->
   <property>
     <name>hadoop.rpc.protection</name>
     <value></value>
   </property> 
```

## <a id="kerberossettings"></a> Kerberos configuration  

Note, when PolyBase authenticates to a Kerberos secured cluster, it expects the hadoop.rpc.protection setting is 'Authenticate' by default. This leaves the data communication between Hadoop nodes unencrypted. To use 'Privacy' or 'Integrity' settings for hadoop.rpc.protection, update the core-site.xml file on the PolyBase server. For more information, see the previous section [Connecting to Hadoop Cluster with Hadoop.rpc.protection](#rpcprotection).

To connect to a Kerberos-secured Hadoop cluster using MIT KDC the following changes are needed on all APS compute nodes and control node:

1. Find the Hadoop configuration directories in the installation path of APS. Typically, the path is:  

   ```  
   C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100\Hadoop\conf  
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

**core-site.xml**
```xml
<property>
  <name>polybase.kerberos.realm</name>
  <value></value>
</property>
<property>
  <name>polybase.kerberos.kdchost</name>
  <value></value>
</property>
<property>
    <name>hadoop.security.authentication</name>
    <value>KERBEROS</value>
</property>
```

**hdfs-site.xml**
```xml
<property>
  <name>dfs.namenode.kerberos.principal</name>
  <value></value> 
</property>
```

**mapred-site.xml**
```xml
<property>
  <name>mapreduce.jobhistory.principal</name>
  <value></value>
</property>
<property>
  <name>mapreduce.jobhistory.address</name>
  <value></value>
</property>
```

**yarn-site.xml**
```xml
<property>
  <name>yarn.resourcemanager.principal</name>
  <value></value>
</property>
```

4. Create a database-scoped credential object to specify the authentication information for each Hadoop user. See [PolyBase T-SQL objects](../relational-databases/polybase/polybase-t-sql-objects.md).

## <a id="encryptionzone"></a> Hadoop Encryption Zone setup
If you are using Hadoop encryption zone modify core-site.xml and hdfs-site.xml as following. Provide the ip address where KMS service is running with the corresponding port number. The default port for KMS on CDH is 16000.

**core-site.xml**
```xml
<property>
  <name>hadoop.security.key.provider.path</name>
  <value>kms://http@<ip address>:16000/kms</value> 
</property>
```

**hdfs-site.xml**
```xml
<property>
  <name>dfs.encryption.key.provider.uri</name>
  <value>kms://http@<ip address>:16000/kms</value>
</property>
<property>
  <name>hadoop.security.key.provider.path</name>
  <value>kms://http@<ip address>:16000/kms</value>
  </property>
```