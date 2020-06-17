---
title: Active Directory objects
titleSuffix: SQL Server Big Data Cluster
description: Learn about SQL Server Big Data Cluster deployment in Active Directory Domain.
author: mihaelablendea
ms.author: mihaelab
ms.reviewer: mikeray
ms.date: 06/16/2020
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
---

# Auto generated Active Directory objects

This article describes what are the Active Directory (AD) accounts and groups that SQL Server creates during a big data cluster deployment.

## Accounts

The following user accounts are generated in the provided OU during cluster deployment. Each of them represents a service in BDC. The accounts own the Service Principal Names (SPNs) required by each service. The SPNs owned by each account can be listed using [setspn](https://social.technet.microsoft.com/wiki/contents/articles/717.service-principal-names-spn-setspn-syntax.aspx) command.

The pod suffix (-x) is used to denote a variable pod ID below. The account names below do not include a variable prefix that is user provided during deployment.

The classic account name applies to deployments using versions before 2019 CU5 as well as deployments done with "useSubdomain" option set to false in security configuration.

Account name prefix is determined by the namespace for the cluster. In the table below `bdc` represents the namespace.

|Scale Set Name|Pod Name|Container Name|Service Name|Account Name (Without prefix)<br><br>Account Name (with prefix=`bdc`)<br><br>Classic Account Name|Description|
|---|---|---|---|---|---|
|`control`|`control-x`|`controller`|`controller`|`ctrl`<br><br>`bdc-ctrl`<br><br>`cntrl-controller`|Controller Service account|
|`mgmtproxy`|`mgmtproxy-x`|`service-proxy`|`nginx`|`ngxm`<br><br>`bdc-ngxm`<br><br>`nginx-mgmtproxy`|Monitoring service proxy service account|
|`metricsui`|`metricsui-x`|`grafana`|`grafana`|`ldap`<br><br>`bdc-ldap`<br><br>`ldap-user`|LDAP lookup user - this is used by grafana and hadoop services for LDAP lookup of users|
|`master`|`master-x`|`mssql-server`|`mssql`|`sqmp-x/sqmp`<br><br>`bdc-sqmp-x/bdc-sqmp`<br><br>`mssql-master-x`|Master pool SQL Server user|
|`master`|`master-x`|`mssql-server`|`dwdms`|`dmmp-x`<br><br>`bdc-dmmp-x`<br><br>`dwdms-master-x`|Master pool Data Warehouse DMS user|
|`master`|`master-x`|`mssql-server`|`dweng`|`demp-x`<br><br>`bdc-demp-x`<br><br>`dweng-master-x`|Master pool Data Warehouse Engine user|
|`compute-0`|`compute-0-x`|`mssql-server`|`mssql`|`sqc0-x/sqlc0`<br><br>`bdc-sqc0-x/bdc-sqc0`<br><br>`mssql-compute-0-x`|Compute pool SQL Server user|
|`compute-0`|`compute-0-x`|`mssql-server`|`dwdms`|`dmc0-x`<br><br>`bdc-dmc0-x`<br><br>`dwdms-compute-0-x`|Compute pool Data Warehouse DMS user|
|`compute-0`|`compute-0-x`|`mssql-server`|`dweng`|`dec0-x`<br><br>`bdc-dec0-x`<br><br>`dweng-compute-0-x`|Compute pool Data Warehouse Engine user|
|`data-0`|`data-0-x`|`mssql-server`|`mssql`|`sqd0`<br><br>`bdc-sqd0`<br><br>`mssql-data-0`|Data pool SQL Server user|
|`storage-0`|`storage-0-x`|`mssql-server`|`mssql`|`sqs0`<br><br>`bdc-sqs0`<br><br>`mssql-storage-0`|Storage pool SQL Server user|
|`storage-0`|`storage-0-x`|`hadoop`|`Yarn Node Manager`|`ynt0-x`<br><br>`bdc-ynt0-x`<br><br>`yarnnm-storage-0-x`|Storage pool Yarn node manager service user|
|`storage-0`|`storage-0-x`|`hadoop`|`HDFS Datanode`|`hdt0`<br><br>`bdc-hdt0`<br><br>`hdfsdn-storage-0`|Storage pool HDFS datanode service user|
|`storage-0`|`storage-0-x`|`hadoop`|`HDFS Datanode`|`htt0`<br><br>`bdc-htt0`<br><br>`http-storage-0`|Storage pool HTTP service user|
|`nmnode-0`|`nmnode-0-x`|`hadoop`|`HDFS Namenode`|`hdnn`<br><br>`bdc-hdnn`<br><br>`hdfsnn-nmnode`|HDFS Name node service user|
|`nmnode-0`|`nmnode-0-x`|`hadoop`|`HDFS Namenode`|`htnn`<br><br>`bdc-htnn`<br><br>`http-nmnode`|HDFS Name node HTTP service user|
|`nmnode-0`|`nmnode-0-x`|`hadoop`|`KMS`|`kmnn-x`<br><br>`bdc-kmnn-x`<br><br>`kms-nmnode-x`|Name node KMS service user|
|`zookeeper`|`zookeeper-x`|`zookeeper`|`Journal node`<br><br>`jnzk-x`<br><br>`bdc-jnzk-x`|`jn-zookeeper-x`|Zookeeper JournalNode service user|
|`zookeeper`|`zookeeper-x`|`zookeeper`|`Zookeeper`|`htzk`<br><br>`bdc-htzk`<br><br>`http-zookeeper`|Zookeeper HTTP service user|
|`sparkhead`|`sparkhead-x`|`hadoop-yarn-jobhistory`|`Yarn Resource Manager`<br><br>`yrsh-x`<br><br>`bdc-yrsh-x`|`yarnrm-sparkhead-x`|Spark head Yarn resource manager service user|
|`sparkhead`|`sparkhead-x`|`*`|`*`|`htsh`<br><br>`bdc-htsh`<br><br>`http-sparkhead`|Spark head HTTP user|
|`sparkhead`|`sparkhead-x`|`hadoop-livy-sparkhistory`|`Spark History Server`|`shsh-x`<br><br>`bdc-shsh-x`<br><br>`sph-sparkhead-x`|Spark head Spark history service user|
|`sparkhead`|`sparkhead-x`|`hadoop-livy-sparkhistory`|`Livy`|`lvsh-x`<br><br>`bdc-lvsh-x`<br><br>`livy-sparkhead-x`|Spark head Livy service user|
|`sparkhead`|`sparkhead-x`|`hadoop-hivemetastore`|`Hive Metastore`|`hvsh-x`<br><br>`bdc-hvsh-x`<br><br>`hive-sparkhead-x`|Spark head Hive service user|
|`spark-0`|`spark-0-x`|`hadoop`|`Yarn Node Manager`|`yns0-x`<br><br>`bdc-yns0-x`<br><br>`yarnnm-spark-0-x`|Spark pool Yarn node manager service user|
|`spark-0`|`spark-0-x`|`hadoop`|`Yarn Node Manager`|`hts0`<br><br>`bdc-hts0`<br><br>`http-spark-0`|Spark pool Yarn node manager HTTP user|
|`gateway`|`gateway-x`|`knox`|`Knox`|`knox-x`<br><br>`bdc-knox-x`<br><br>`knox-gateway-x`|Knox Gateway user|
|`gateway`|`gateway-x`|`knox`|`Knox`|`htgw`<br><br>`bdc-htgw`<br><br>`http-gateway`|Knox Gateway HTTP user|
|`appproxy`|`appproxy-x`|`App Service Proxy`|`nginx`|`apst`<br><br>`bdc-apst`<br><br>`app-setup`|App setup user|

## Groups

The following groups are created in the OU provided by the user. The members of the groups are the users created above for the corresponding services.

Group name prefix is determined by the namespace for the cluster. In the table below `bdc` represents the namespace.

|Scale Set Name|Pod Name|Container Name|Service Name|Group Name (Without prefix)|Group Name (with prefix=`bdc`)|Classic Group Name|Description|
|---|---|---|---|---|---|---|---|
|`master/compute-0`|`master-x/compute-0-x`|`mssql-server`|`dwdms`|`dmsvc`|`bdc-dmsvc`|`dwdms-service`|Data Warehouse DMS Service group|
|`master/compute-0`|`master-x/compute-0-x`|`mssql-server`|`dweng`|`desvc`|`bdc-desvc`|`dweng-service`|Data Warehouse Engine Service group|

## Next steps

[Deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in Active Directory mode](deploy-active-directory.md)

[Deploy multiple [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in the same Active Directory domain](active-directory-deployment-background.md)