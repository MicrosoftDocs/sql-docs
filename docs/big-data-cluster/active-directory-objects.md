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

The following user accounts are generated in the provided [organizational unit (OU)](/windows-server/identity/ad-ds/plan/reviewing-ou-design-concepts) during cluster deployment. Each of them represents a service in BDC. The accounts own the Service Principal Names (SPNs) required by each service. The SPNs owned by each account can be listed using [setspn](https://social.technet.microsoft.com/wiki/contents/articles/717.service-principal-names-spn-setspn-syntax.aspx) command.

The pod suffix (-x) is used to denote a variable pod ID below. The account names below do not include a variable prefix that is user provided during deployment.

The classic account name applies to deployments using versions before 2019 CU5 as well as deployments done with "useSubdomain" option set to false in security configuration.

Account name prefix is determined by the namespace for the cluster. If the namespace is `bdc` for the items below replace `<namespace>` with `bdc`.

## Controller service account

|Object ||
|---|---|
|Scale set name|`control`|
|Pod name|`control-x`|
|Container name|`controller`|
|Service name|`controller`|
|Account name (without prefix)| `ctrl`|
|Account (with namespace prefix)|`<namespace>-ctrl`|
|Classic account name|`ctrl-controller`|

## Monitoring service proxy service account

|Object ||
|---|---|
|Scale set name|`mgmtproxy`|
|Pod name|`mgmtproxy-x`|
|Container name|`service-proxy`|
|Service name|`nginx`|
|Account (without prefix)| `ngxm`|
|Account (with namespace prefix)|`bdc-ngxm`|
|Classic account name|`nginx-mgmtproxy`|


## LDAP lookup user

Used by grafana and hadoop services to look up users through LDAP.

|Object ||
|---|---|
|Scale set name|`metricsui`|
|Pod name|`metricsui-x`|
|Container name|`grafana`|
|Service name|`grafana`|
|Account name (without prefix)| `ldap`|
|Account name (with namespace prefix)|`<namespace>-ldap`|
|Classic account name|`ldap-user`|

## Master pool SQL Server user

|Object ||
|---|---|
|Scale set name|`master`|
|Pod name|`master-x`|
|Container name|`mssql-server`|
|Service name|`mssql`|
|Account name (without prefix)| `sqmp-x/sqmp`|
|Account name (with namespace prefix)|`<namespace>-sqmp-x/<namespace>-sqmp`|
|Classic account name|`mssql-master-x`|

## Master pool Data Warehouse DMS user

|Object ||
|---|---|
|Scale set name|`master`|
|Pod name|`master-x`|
|Container name|`mssql-server`|
|Service name|`dwdms`|
|Account (without prefix)| `dmmp-x`|
|Account (with namespace prefix)|`<namespace>-dmmp-x`|
|Classic account name|`dwdms-master-x`|

## Master pool Data Warehouse Engine user

|Object ||
|---|---|
|Scale set name|`master`|
|Pod name|`master-x`|
|Container name|`mssql-server`|
|Service name|`dweng`|
|Account (without prefix)| `demp`|
|Account (with namespace prefix)|`<namespace>-demp-x`|
|Classic account name|`dweng-master-x`|

## Compute pool SQL Server user

|Object ||
|---|---|
|Scale set name|`compute-0`|
|Pod name|`compute-0-x`|
|Container name|`mssql-server`|
|Service name|`mssql`|
|Account (without prefix)| `sqc0-x/sqlc0`|
|Account (with namespace prefix)|`<namespace>-sqc0-x/<namespace>-sqc0`|
|Classic account name|`mssql-compute-0-x`|

## Compute pool Data Warehouse DMS user

|Object ||
|---|---|
|Scale set name|`compute-0`|
|Pod name|`compute-0-x`|
|Container name|`mssql-server`|
|Service name|`dwdms`|
|Account (without prefix)| `dmc0-x`|
|Account (with namespace prefix)|`<namespace>-dmc0-x`|
|Classic account name|`dwdms-compute-0-x`|

## Compute pool Data Warehouse Engine user

|Object ||
|---|---|
|Scale set name|`compute-0`|
|Pod name|`compute-0-x`|
|Container name|`mssql-server`|
|Service name|`dweng`|
|Account (without prefix)| `dec0-x`|
|Account (with namespace prefix)|`<namespace>-dec0-x`|
|Classic account name|`dweng-compute-0-x`|

## Data pool SQL Server user

|Object ||
|---|---|
|Scale set name|`data-0`|
|Pod name|`data-0-x`|
|Container name|`mssql-server`|
|Service name|`mssql`|
|Account (without prefix)| `sqd0`|
|Account (with namespace prefix)|`<namespace>-sqd0`|
|Classic account name|`mssql-data-0`|

## Storage pool SQL Server user

|Object ||
|---|---|
|Scale set name|`storage-0`|
|Pod name|`storage-0-x`|
|Container name|`mssql-server`|
|Service name|`mssql`|
|Account (without prefix)| `sqs0`|
|Account (with namespace prefix)|`<namespace>-sqs0`|
|Classic account name|`mssql-storage-0`|

## Storage pool Yarn node manager service user

|Object ||
|---|---|
|Scale set name|`storage-0`|
|Pod name|`storage-0-x`|
|Container name|`hadoop`|
|Service name|`Yarn Node Manager`|
|Account (without prefix)| `ynt0-x`|
|Account (with namespace prefix)|`<namespace>-ynt0-x`|
|Classic account name|`yarnnm-storage-0-x`|

## Storage pool HTTP service user

|Object ||
|---|---|
|Scale set name|`storage-0`|
|Pod name|`storage-0-x`|
|Container name|`hadoop`|
|Service name|`HDFS Datanode`|
|Account (without prefix)| `hdt0`|
|Account (with namespace prefix)|`<namespace>-hdt0`|
|Classic account name|`http-storage-0`|

## Storage pool HDFS datanode service user

|Object ||
|---|---|
|Scale set name|`storage-0`|
|Pod name|`storage-0-x`|
|Container name|`hadoop`|
|Service name|`HDFS Datanode`|
|Account (without prefix)| `hdt0`|
|Account (with namespace prefix)|`<namespace>-hdt0`|
|Classic account name|`hdfsdn-storage-0`|

## Storage pool HTTP service user

|Object ||
|---|---|
|Scale set name|`storage-0`|
|Pod name|`storage-0-x`|
|Container name|`hadoop`|
|Service name|`HDFS Datanode`|
|Account (without prefix)| `htt0`|
|Account (with namespace prefix)|`<namespace>-htt0`|
|Classic account name|`http-storage-0`|

## HDFS Name node service user

|Object ||
|---|---|
|Scale set name|`nmnode-0`|
|Pod name|`nmnode-0-x`|
|Container name|`hadoop`|
|Service name|`HDFS Namenode`|
|Account (without prefix)| `hdnn`|
|Account (with namespace prefix)|`<namespace>-hdnn`|
|Classic account name|`hdfsnn-nmnode`|

## HDFS Name node HTTP service user

|Object ||
|---|---|
|Scale set name|`nmnode-0`|
|Pod name|`nmnode-0-x`|
|Container name|`hadoop`|
|Service name|`HDFS Namenode`|
|Account (without prefix)| `htnn`|
|Account (with namespace prefix)|`<namespace>-htnn`|
|Classic account name|`http-nmnode`|

## Name node KMS service user

|Object ||
|---|---|
|Scale set name|`nmnode-0`|
|Pod name|`nmnode-0-x`|
|Container name|`hadoop`|
|Service name|`KMS`|
|Account (without prefix)| `kmnn-x`|
|Account (with namespace prefix)|`<namespace>-kmnn-x`|
|Classic account name|`kms-nmnode-x`|

## Zookeeper JournalNode service users

|Object ||
|---|---|
|Scale set name|`zookeeper`|
|Pod name|`zookeeper-x`|
|Container name|`zookeeper`|
|Service name|`Journal node`|
|Account (without prefix)| `jnzk-x`|
|Account (with namespace prefix)|`<namespace>-jnzk-x`|
|Classic account name|`jn-zookeeper-x`|

## Zookeeper HTTP service user

|Object ||
|---|---|
|Scale set name|`zookeeper`|
|Pod name|`zookeeper-x`|
|Container name|`zookeeper`|
|Service name|`Zookeeper`|
|Account (without prefix)| `htzk`|
|Account (with namespace prefix)|`<namespace>-htzk`|
|Classic account name|`http-zookeeper`|

## Sparkhead Yarn resource manager service user

|Object ||
|---|---|
|Scale set name|`sparkhead`|
|Pod name|`sparkhead-x`|
|Container name|`hadoop-yarn-jobhistory`|
|Service name|`Yarn Resource Manager`|
|Account (without prefix)| `yrsh-x`|
|Account (with namespace prefix)|`<namespace>-yrsh-x`|
|Classic account name|`yarnrm-sparkhead-x`|

## Sparkhead HTTP user

|Object ||
|---|---|
|Scale set name|`sparkhead`|
|Pod name|`sparkhead-x`|
|Container name|`*`|
|Service name|`*`|
|Account (without prefix)| `htsh`|
|Account (with namespace prefix)|`<namespace>-htsh`|
|Classic account name|`http-sparkhead`|

## Sparkhead Spark history service user

|Object ||
|---|---|
|Scale set name|`sparkhead`|
|Pod name|`sparkhead-x`|
|Container name|`hadoop-livy-sparkhistory`|
|Service name|`Spark History Server`|
|Account (without prefix)| `shsh-x`|
|Account (with namespace prefix)|`<namespace>-shsh-x`|
|Classic account name|`sph-sparkhead-x`|

## Sparkhead Livy service user

|Object ||
|---|---|
|Scale set name|`sparkhead`|
|Pod name|`sparkhead-x`|
|Container name|`hadoop-livy-sparkhistory`|
|Service name|`Livy`|
|Account (without prefix)| `lvsh-x`|
|Account (with namespace prefix)|`<namespace>-lvsh-x`|
|Classic account name|`livy-sparkhead-x`|

## Sparkhead Hive service user

|Object ||
|---|---|
|Scale set name|`sparkhead`|
|Pod name|`sparkhead-x`|
|Container name|`hadoop-hivemetastore`|
|Service name|`Hive Metastore`|
|Account (without prefix)| `hvsh-x`|
|Account (with namespace prefix)|`<namespace>-hvsh-x`|
|Classic account name|`hive-sparkhead-x`|

## Spark pool Yarn node manager service user

|Object ||
|---|---|
|Scale set name|`spark-0`|
|Pod name|`spark-0-x`|
|Container name|`hadoop`|
|Service name|`Yarn Node Manager`|
|Account (without prefix)| `yns0-x`|
|Account (with namespace prefix)|`<namespace>-yns0-x`|
|Classic account name|`yarnnm-spark-0-x`|

## Spark pool Yarn node manager HTTP user

|Object ||
|---|---|
|Scale set name|`spark-0`|
|Pod name|`spark-0-x`|
|Container name|`hadoop`|
|Service name|`Yarn Node Manager`|
|Account (without prefix)| `hts0`|
|Account (with namespace prefix)|`<namespace>-hts0`|
|Classic account name|`http-spark-0`|

## Knox Gateway user

|Object ||
|---|---|
|Scale set name|`gateway`|
|Pod name|`gateway-x`|
|Container name|`knox`|
|Service name|`Knox`|
|Account (without prefix)| `knox-x`|
|Account (with namespace prefix)|`<namespace>-knox-x`|
|Classic account name|`knox-gateway-x`|

## Knox Gateway HTTP user

|Object ||
|---|---|
|Scale set name|`gateway`|
|Pod name|`gateway-x`|
|Container name|`knox`|
|Service name|`Knox`|
|Account (without prefix)| `htgw`|
|Account (with namespace prefix)|`<namespace>-htgw`|
|Classic account name|`http-gateway`|

## App setup user

|Object ||
|---|---|
|Scale set name|`appproxy`|
|Pod name|`appproxy-x`|
|Container name|`App Service Proxy`|
|Service name|`nginx`|
|Account (without prefix)| `apst`|
|Account (with namespace prefix)|`<namespace>-apst`|
|Classic account name|`app-setup`|

## Groups

The following groups are created in the OU provided by the user. The members of the groups are the users created above for the corresponding services.

Group name prefix is determined by the namespace for the cluster. If the namespace is `bdc` for the items below replace `<namespace>` with `bdc`.

## Data Warehouse DMS Service group

|Object ||
|---|---|
|Scale set name|`master/compute-0`|
|Pod name|`master-x/compute-0-x`|
|Container name|`mssql-server`|
|Service name|`dwdms`|
|Group (without prefix)| `dmsvc`|
|Account (with namespace prefix)|`<namespace>-dmsvc`|
|Classic account name|`dwdms-service`|

## Data Warehouse Engine Service group

|Object ||
|---|---|
|Scale set name|`master/compute-0`|
|Pod name|`master-x/compute-0-x`|
|Container name|`mssql-server`|
|Service name|`dweng`|
|Group (without prefix)| `desvc`|
|Account (with namespace prefix)|`<namespace>-desvc`|
|Classic account name|`desvc`|

## Next steps

[Deploy [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in Active Directory mode](deploy-active-directory.md)

[Deploy multiple [!INCLUDE[big-data-clusters-2019](../includes/ssbigdataclusters-ss-nover.md)] in the same Active Directory domain](active-directory-deployment-background.md)