---
title: Monitoring and troubleshooting queries for SQL Server Big Data Cluster | Microsoft Docs
description:
author: rothja 
ms.author: jroth 
manager: craigg
ms.date: 08/06/2018
ms.topic: conceptual
ms.prod: sql
---

# Monitoring and troubleshooting queries for SQL Server Big Data Cluster

This article describes the DMVs and stored procedures available to help troubleshoot SQL Server Big Data Cluster. It also provides troubleshooting recommendations for known issues.

## Dynamic management views

The sample database (**high_value_data**) created by the deployment script already contains the new dynamic management views for troubleshooting data pools.

## DM_DATA_POOLS

This DMV shows the number of nodes in the data pool

| Column | Description |
|---|---|
| Name | Data pool name |
| Nodes | Number of data pool nodes in a data pool |

Example usage (make sure you are running this against the high_value_data database):

```sql
SELECT * FROM dm_data_pools ()
```

## DM_DATA_POOL_NODE_STATUS

This DMV returns the status of all data pool nodes in the data pool.

| Column | Description |
|---|---|
| Node_id | Data pool node id |
| State | State of the data pool node |
| Connection_time | | 
| version | Version of SQL Server running on the compute node |

Example usage:

```sql
DECLARE @data_pool_name NVARCHAR(max) = 'mssql-data-pool'
SELECT * FROM dm_data_pool_node_status (@data_pool_name)
```

## DM_DATA_POOL_JOBS

This DMV returns information about the spark job that was submitted by T-SQL command.

| Column | Description |
|---|---|
| App_name | Name of the job |
| App_id | ID of the job |
| Started_time | Spark job execution start time |
| Elapsed_time | Time elapsed in milliseconds |
| State | State of the job – running, finished, etc. |
| Progress | Percent complete |
| Finished_status | Finished status output if any |
| Finished_time | Spark job execution finish time |
| Diagnostics | Diagnostic info if any |

Example usage:

```sql
DECLARE @data_pool_name NVARCHAR(max) = 'mssql-data-pool'
SELECT * FROM dm_data_pool_jobs (@data_pool_name)
```

## DM_DATA_POOL_TABLE_STATUS

This DMV returns number of rows in data pool external tables.

| Column | Description |
|---|---|
| Node_id | Compute node id |
| Rows | Number of row in an external table in the compute node |
| Exception_message | Exception message if any |
| Exception_stack | Exception stack if any |
| Exception_source | Exception source if any |

Example usage:

```sql
DECLARE @data_pool_name NVARCHAR(max) = 'mssql-data-pool'
DECLARE @schema_name NVARCHAR(max) = 'dbo'
DECLARE @table_name NVARCHAR(max) = 'airlinedata'
SELECT * FROM dm_data_pool_table_status (@data_pool_name, @schema_name, @table_name)
```

## SP_DATA_POOL_LOG

This store procedure shows the log of all the data pool operations. You can use this stored procedure to troubleshoot the commands between master instance & compute nodes and spark job server.

Example:

```sql
exec sp_data_pool_log
```

## Known issues

### Unable to connect to sql server master instance in acs cluster using external ip

You may receive an error saying that the certificate authority cannot be trusted while trying to connect to the SQL Server master instance in ACS cluster using SQL Server Management Studio (SSMS). The error message will look like below:

```
A connection was successfully established with the server, but then an error occurred during the login process. (provider: SSL Provider, error: 0 - The certificate chain was issued by an authority that is not trusted.) (.Net SqlClient Data Provider)
```

You can work around this by enabling the **Trust server certificate** option in the SSMS Connection Dialog box. (Click Options & option is on the Connection Properties tab)

### Latest versions of k8 s utilities

Ensure that you have the latest version of minikube & kubectl utilities.

### Clearning minikube state

If you encounter issues with minikube related to stale docker images or are getting **unable to start the VM** errors, then run the following commands:

```bash
minikube stop
minikube delete
```

Also, remove the `%userprofile%\.minikube` or `$home/.minikube` directory before trying to start the VM again. This will ensure that you have a clean environment.

### Windows only error the systemctl restart crio failed while trying to start minikube

Depending on the resources on your machine, you might get error about some service restart "the systemctl restart crio failed" while trying to start a minikube cluster. This error can happen on Windows 10 since VMs by default will use dynamic memory and system may not be responsive enough. To switch off dynamic memory, use the following steps:

```bash
minikube stop
set-vm -Name minikube -StaticMemory
minikube start
```

### Sql operations studio macos error failed to install jupyter could not find jupyter prerequisite pip

Depending on the environment, you might get pip not found error when trying to invoke a notebook session. Use the following step to install pip from the Mac Terminal window and restart Operations Studio:

```python
sudo python -m ensurepip --default-pip
```

Due to the large size of the images for Big Data Cluster, in some cases you might have a timeout error happen when pulling the images.  You may need to "pre-pull" the images for now.  Below are steps to use to SSH into each ACS agent VM and pre-pull the images.  Use this as an example of what to do if you are using other container hosting services like AWS EKS or GCP GKE.

> [!IMPORTANT]
> Before starting this series of steps, you need the credentials to the private docker registry containing SQL Server 2019CTP 2.0 images.  This should be provided to you by your PM buddy via email when you joined the Early Adoption Program.  If you have not already received them, please contact your PM buddy.

First, copy your id_rsa SSH key file to the ACS master VM.  You will need the scp utility if you don't have it already installed.  You will also need to login to the Azure portal and find the public IP address of the ACS master VM.

```bash
scp -i <path to your key file created earlier>/id_rsa <path to your key file created earlier>/id_rsa azureuser@<public IP>:/tmp
```

Example:

```bash
scp -i /Users/travis/clusteracskeys/id_rsa /Users/travis/clusteracskeys/id_rsa azureuser@13.66.229.6:/tmp
```

Then, SSH into the ACS master VM:

```bash
ssh -i <path to your key file created earlier>/id_rsa azureuser@<public IP>
```

Example:

```
ssh -i /Users/travis/clusteracskeys/id_rsa azureuser@13.66.229.6
```

From the ACS master VM SSH into EACH of the agent VMs as follows:

```bash
ssh -i /tmp/id_rsa azureuser@10.240.0.4
```

Note: the agent VMs always have internal IP addresses that start with 10.240.0.4 and go up – e.g. 10.240.0.5, 10.240.0.6, ...

In each agent VM run the following commands:

```bash
docker login private-repo.microsoft.com
```

You will be prompted for a login and password.  Please use the login and password provided to you via email when you joined the Early Adoption Program.

```bash
docker pull private-repo.microsoft.com/mssql-private-preview/mssql-controller:ctp-1.8 && docker pull private-repo.microsoft.com/mssql-private-preview/mssql-agent:ctp-1.8 && docker pull private-repo.microsoft.com/mssql-private-preview/mssql-hadoop:ctp-1.8 && docker pull private-repo.microsoft.com/mssql-private-preview/mssql-server-data:ctp-1.8 && docker pull private-repo.microsoft.com/mssql-private-preview/mssql-server-security:ctp-1.8 && docker pull private-repo.microsoft.com/mssql-private-preview/mssql-security-ranger:ctp-1.8 && docker pull private-repo.microsoft.com/mssql-private-preview/mssql-security-knox:ctp-1.8
```

Wait for all the images to be pulled.

```bash
exit
```

## Next steps

TBD
