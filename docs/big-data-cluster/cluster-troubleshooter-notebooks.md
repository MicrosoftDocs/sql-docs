---
title: Troubleshooting Big Data Clusters with Jupyter notebooks and Azure Data Studio
titleSuffix: SQL Server Big Data Clusters
description: Troubleshooting Big Data Clusters with Jupyter notebooks and Azure Data Studio on SQL Server 2019 Big Data Clusters.
author: HugoMSFT
ms.author: hudequei
ms.reviewer: wiassaf
ms.date: 07/16/2021
ms.service: sql
ms.subservice: big-data-cluster
ms.topic: troubleshooting
ms.metadata: seo-lt-2019
---

# Troubleshoot Big Data Clusters by using Jupyter Notebooks and Azure Data Studio

This page is an index of notebooks for SQL Server Big Data Clusters. Those executable notebooks (.ipynb) are designed for SQL Server 2019 to help troubleshooting big data clusters.

[!INCLUDE[big-data-clusters-banner-retirement](../includes/bdc-banner-retirement.md)]

Each notebook is designed to check for its own dependencies. The **Run all cells** option either completes successfully or raises an exception with a hyperlinked *hint* to another notebook to resolve the missing dependency. Follow the hint hyperlink to the target notebook, click **Run all cells**. Upon success return back to the original notebook, then click **Run all cells**.

Once all dependencies are installed, but **Run all cells** fails, each notebook will analyze results and where possible, produce a hyperlinked hint to another notebook to further aid in resolving the issue.

* For more information on using notebooks to manage SQL Server Big Data Clusters, see [Manage SQL Server Big Data Clusters with Azure Data Studio notebooks](notebooks-manage-bdc.md).
* For the location of big data cluster administration notebooks, see [Where to find SQL Server Big Data Clusters administration notebooks](view-cluster-status.md#where-to-find--administration-notebooks).

## Troubleshooting Big Data Clusters

This section contains a set of notebooks for getting logs from a SQL Server big data cluster.

|Name |Description |
|---|---|---|---|
|TSG100 - The Big Data Clusters troubleshooter|Overview of all available notebooks on troubleshooting Big Data Clusters issues and when to use them  |
|TSG101 - SQL Server troubleshooter|Overview of all available notebooks on troubleshooting SQL Server issues and when to use them  |
|TSG102 - HDFS troubleshooter|Overview of all available notebooks on troubleshooting HDFS issues and when to use them  |
|TSG103 - Spark troubleshooter|Overview of all available notebooks on troubleshooting Spark issues and when to use them  |
|TSG104 - Control troubleshooter|Overview of all available notebooks on troubleshooting controller issues and when to use them  |
|TSG105 - Gateway troubleshooter|Overview of all available notebooks on troubleshooting Knox Gateway issues and when to use them  |
|TSG106 - App troubleshooter|Overview of all available notebooks on troubleshooting App-Deploy issues and when to use them  |



## Diagnose issues from Big Data Clusters

A set of notebooks for diagnosing situations and states with a big data cluster.

|Name |Description |
|---|---|---|---|
|TSG002 - CrashLoopBackoff|This TSG will connect to each container whose last attempt to get into a 'Running' state failed, and get the current and previous container logs. This is useful for debugging CrashLoopBackOff issues reported in kubectl get pods.|
|TSG025 - FSM Browser - Query Controller FSM state|Use this notebook to connect to the controller database and browse the Finite State Machine (FSM) state. Use this notebook to list active state machines and identify stuck workflows.|
|TSG026 - Connect to data pool node (to run T-SQL)|Use this notebook to connect to data pool node (to run T-SQL)|
|TSG027 - Observe cluster deployment|Use this notebook to observe cluster deployment, it provides guidance to troubleshoot SQL Server Big Data Clusters create issues the following commands are often useful for pinpointing underlying causes. |
|TSG029 - Find dumps in the cluster|Use this notebook to look for coredumps and minidumps from processes like SQL Server or controller in a big data cluster.|
|TSG032 - CPU and Memory usage for all containers|Use this notebook to check CPU and Memory usage for all containers.|
|TSG037 - Determine master pool pod hosting primary replica|Use this notebook to determine master pool pod hosting primary replica for the big data cluster when master pool high availability is enabled.|
|TSG044 - Run sqlcmd in master pool container|Use this notebook to connect to a master pool node directly via T-SQL.|
|TSG055 - Time Curl to Sparkhead|Use this notebook to diagnose step to understand what the Curl response time is from the controller pod to the sparkhead pod.|
|TSG060 - Persistent Volume disk space for all big data cluster PVCs|Use this notebook to connect to each container and get the disk space used/available for each Persisted Volume (PV) mapped to each Persisted Volume Claim (PVC) of a big data cluster.|
|TSG078 - Is cluster healthy|Use this notebook to check if your big data cluster is healthy.|
|TSG079 - Generate controller core dump|Use this notebook to generate controller core dump.|
|TSG086 - Run top in all containers|Use this notebook to Run top in all containers.|
|TSG087 - Use hadoop fs CLI on namenode pod|Use this notebook to use hadoop fs CLI on namenode pod.|
|TSG108 - View the controller upgrade config map|Use this notebook to troubleshoot the failure when running a big data cluster upgrade using **azdata bdc upgrade**.|
|TSG112 - Active Directory Pre-Deployment Checks|Use this notebook to validate a big data cluster configuration is valid for an Active Directory deployment.|
|TSG115 - SQL Server on Linux security log translator|Use this notebook to parse the logs generated by the secuirty.ldap and security.kerberos loggers for SQL Server on Linux. To enable these loggers, place the lines below in /var/opt/mssql/logger.ini on the machine running SQL Server on Linux. Note: this file is case sensitive.|
|TSG116 - SQL BDC security support log translator|Use this notebook to parse the logs generated by the security support service in SQL BDC. To get the logs, we'll copy the debug logs from the cluster and extract them. Follow the steps below - run "azdata bdc debug copy-logs -n \<namespace\>*". This will create several .tar.gz files - Extract the contents of debuglogs-*\<namespace\>-\<date\>-\<time\>.tar.gz - Locate the security support log stored at ./\<namespace\>/control-<…>/security-support/supervisol/log/secsupp-stderr---<…>.log.|
|TSG119 - Active Directory Post-Deployment Checks|This notebook is designed to validate your BDC configuration after an AD deployment. It will check the existence of DNS entries for all endpoints with a dnsName attribute and these DNS entries should be host records, not aliases (i.e. A records not CNAME records).Also the existence of well-known AD accounts and whether they're enabled and the existence of the expected SPNs|






## Repair issues from Big Data Clusters 

A set of notebooks for repairing known situations and states of a SQL Server big data cluster.

|Name |Description |
|---|---|---|---|
|TSG005 - Forwarding loop detected|Use this notebook to dealing with forwarding loop detected since the utility dnsmasq can put a local loopback in resolv.conf, which can cause the controller pods to go into a CrashLoopBackOff during initial cluster deployment: https://askubuntu.com/questions/627899/nameserver-127-0-1-1-in-resolv-conf-wont-go-away|
|TSG011 - Restart sparkhistory server|Use this notebook to restart sparkhistory server since the sparkhistory java process can stop responding during startup. Restarting the sparkhistory server (supervisorctl restart sparkhistory) can resolve this issue.|
|TSG018 - Kill sqlservr process on the master pool| Use this notebook when T-SQL SHUTDOWN doesn't successfully re-cycle the ./sqlservr process. Use this notebook to kill the main sqlservr process which will get automatically restarted by the ./sqlservr front-end process.|
|TSG024 - Namenode is in safe mode| Use this notebook when HDFS gets itself into Safe mode. For example if too many Pods are re-cycled too quickly in the Storage Pool then Safe mode may be automatically enabled.|
|TSG028 - Restart node manager on all storage pool nodes| Use this notebook when needs to restart node manager on all storage pool nodes.|
|TSG038 - BDC create failures due to - doc is missing key| Use this notebook when BDC create failures due to - doc is missing key.|
|TSG039 - Invalid object name 'role_permissions'| Use this notebook when having invalid object issue due to the role permission in Knox gateway.log|
|TSG040 - Failed to get file names from controller with Error| Use this notebook when having 504 Gateway Time-out while getting file names from controller.|
|TSG041 - Unable to create a new asynchronous I/O context (increase sysctl fs.aio-max-nr)| Use this notebook when unable to create a new asynchronous I/O context (increase sysctl fs.aio-max-nr).|
|TSG045 - The maximum number of data disks allowed to be attached to a VM of this size (AKS)| Use this notebook when a maximum number of data disks allowed to be attached to a VM of this size (AKS).|
|TSG047 - ConfigException - Expected only one object with name| Use this notebook when having ConfigException which is expecting only one object with name.|
|TSG048 - Deployment stuck at "Waiting for controller pod to be up"| Use this notebook when the deployment stuck at "Waiting for controller pod to be up".|
|TSG050 - Cluster create hangs with "timeout expired waiting for volumes to attach or mount for pod"| Use this notebook when the cluster create hangs with "timeout expired waiting for volumes to attach or mount for pod".|
|TSG052 - Tried to get master-svc DNS failed and will try again| Use this notebook when the cluster create hangs with "timeout expired waiting for volumes to attach or mount for pod".|
|TSG057 - Failed when starting controller service .System.TimeoutException| Use this notebook when getting when starting controller service and getting System.TimeoutException.|
|TSG067 - Failed to complete kube config setup| Use this notebook when to complete kube config setup is failing.|
|TSG074 - Delete App-Deploys| Use this notebook when having issue to delete apps in big data cluster.|
|TSG075 - FailedCreatePodSandBox due to NetworkPlugin cni failed to set up pod| Use this notebook when getting FailedCreatePodSandBox exception due to NetworkPlugin cni failed to set up pod.|
|TSG080 - Delete Spark Sessions using azdata| Use this notebook when getting issue while deleting Spark sessions.|
|TSG109 - Set upgrade timeouts| Use this notebook when getting issue having BDC upgrade issue.|
|TSG110 - Azdata returns ApiError| Use this notebook when Azdata returns ApiError.|

## Next steps

For more information about big data clusters, see [What are SQL Server Big Data Clusters?](big-data-cluster-overview.md).
