---
title: Configure SQL Server Always On Availability Group on Windows and Linux | Microsoft Docs
description: 
author: MikeRayMSFT 
ms.author: mikeray 
manager: craigg
ms.date: 01/31/2018
ms.topic: article
ms.prod: "sql-non-specified"
ms.prod_service: "database-engine"
ms.service: ""
ms.component: sql-linux
ms.suite: "sql"
ms.custom: ""
ms.technology: database-engine
ms.assetid: 
ms.workload: "On Demand"
---
# Configure SQL Server Always On Availability Group on Windows and Linux (cross-platform)

[!INCLUDE[tsql-appliesto-sslinux-only](../includes/tsql-appliesto-sslinux-only.md)]

This article explains the steps to create an Always On Availability Group (AG) with one replica on a Windows server and the other replica on a Linux server. This configuration is cross-platform because the replicas are on different operating systems. Use this configuration for migration from one platform to the other. This configuration does not support high-availability because there is no automatic failover capability. 

## Scenario

In this scenario, two servers are on different operating systems. A Windows Server 2016 named `WinSQLInstance` hosts the primary replica. A Linux server named `LinuxSQLInstance` host the secondary replica.

## Steps 

The steps to create the AG are the same as the steps to create an AG for read-scale workloads. The AG cluster type is none, because there is no cluster manager. 

1. Install SQL Server 2017 on Windows Server 2016 and enable AGs from SQL Server Configuration Manager.

1. Install SQL Server 2017 on Linux. Enable HADR via mssql-conf.

1. Configure hosts on both servers or register the server names with DNS.

1. Open up firewall ports for TPC 1433 and 5022 on both Windows and Linux.

1. Create a database for the AG. The example steps use a database named `<TestDB>`.

1. On the primary replica, create a database login and password.

   ```sql
   CREATE LOGIN dbm_login WITH PASSWORD = '<C0m9L3xP@55w0rd!>';
   CREATE USER dbm_user FOR LOGIN dbm_login;
   GO
   ```

1. On the primary replica, create master key, certificate, and backup the certificate.    
   ```sql
   CREATE MASTER KEY ENCRYPTION BY PASSWORD = '<C0m9L3xP@55w0rd!>';
   CREATE CERTIFICATE dbm_certificate WITH SUBJECT = 'dbm';
   BACKUP CERTIFICATE dbm_certificate
   TO FILE = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\dbm_certificate.cer'
   WITH PRIVATE KEY (
           FILE = 'C:\Program Files\Microsoft SQL Server\MSSQL14.MSSQLSERVER\MSSQL\DATA\dbm_certificate.pvk',
           ENCRYPTION BY PASSWORD = '<C0m9L3xP@55w0rd!>'
       );
   GO
   ```

1. Copy the certificate and private key to the Linux server at `/var/opt/mssql/data`. Set the group and ownership to `mssql:mssql`.

1. On the primary replica, create the endpoint.

   ```sql
   CREATE ENDPOINT [Hadr_endpoint]
       AS TCP (LISTENER_IP = (0.0.0.0), LISTENER_PORT = 5022)
       FOR DATA_MIRRORING (
           ROLE = ALL,
           AUTHENTICATION = CERTIFICATE dbm_certificate,
           ENCRYPTION = REQUIRED ALGORITHM AES
           );
   ALTER ENDPOINT [Hadr_endpoint] STATE = STARTED;
   GRANT CONNECT ON ENDPOINT::[Hadr_endpoint] TO [dbm_login]
   GO
   ```

1. On the primary replica, create the AG with `CLUSTER_TYPE = NONE`.

   ```sql
   CREATE AVAILABILITY GROUP [readscaleag]
       WITH (CLUSTER_TYPE = NONE)
       FOR REPLICA ON
           N'<WinSQLInstance>' 
      	WITH (
	       ENDPOINT_URL = N'tcp://<WinSQLInstance>:5022',
	       AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
      	    SEEDING_MODE = AUTOMATIC,
	      	FAILOVER_MODE = MANUAL,
		   SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL)
      		),
           N'<LinuxSQLInstance>' 
	   WITH (
      	    ENDPOINT_URL = N'tcp://<LinuxSQLInstance>:5022',
	       AVAILABILITY_MODE = ASYNCHRONOUS_COMMIT,
	       SEEDING_MODE = AUTOMATIC,
		   FAILOVER_MODE = MANUAL,
		   SECONDARY_ROLE (ALLOW_CONNECTIONS = ALL)
           )
   GO
   ```

1. On the primary replica, grant the AG permission to create any database.

   ```sql
   ALTER AVAILABILITY GROUP [readscaleag] ADD DATABASE tpcc_workload
   GO
   ```

1. On the secondary replica, create a database login and password.

   ```sql
   CREATE LOGIN dbm_login WITH PASSWORD = '<C0m9L3xP@55w0rd!>';
   CREATE USER dbm_user FOR LOGIN dbm_login;
   GO
   ```

1. On the secondary replica, restore the certificate you copied to `/var/opt/mssql/data`. 

   ```sql
   CREATE CERTIFICATE dbm_certificate   
       AUTHORIZATION dbm_user
       FROM FILE = '/var/opt/mssql/data/dbm_certificate.cer'
       WITH PRIVATE KEY (
       FILE = '/var/opt/mssql/data/dbm_certificate.pvk',
       DECRYPTION BY PASSWORD = '<C0m9L3xP@55w0rd!>'
   )
   GO
   ```

1. On the secondary replica, create the endpoint.

   ```sql
   CREATE ENDPOINT [Hadr_endpoint]
       AS TCP (LISTENER_IP = (0.0.0.0), LISTENER_PORT = 5022)
       FOR DATA_MIRRORING (
           ROLE = ALL,
           AUTHENTICATION = CERTIFICATE dbm_certificate,
           ENCRYPTION = REQUIRED ALGORITHM AES
           );
   ALTER ENDPOINT [Hadr_endpoint] STATE = STARTED;
   GRANT CONNECT ON ENDPOINT::[Hadr_endpoint] TO [dbm_login];
   GO
   ```

1. On the secondary replica, join the AG.

   ```sql
   ALTER AVAILABILITY GROUP [readscaleag] JOIN WITH (CLUSTER_TYPE = NONE)
   ALTER AVAILABILITY GROUP [readscaleag] GRANT CREATE ANY DATABASE
   GO
   ```

1. On the primary replica, run the SQL query to add the db to the AG.

   ```sql
   ALTER AVAILABILITY GROUP [readscaleag] ADD DATABASE <TestDB>
   GO
   ```

1. You should see the db now getting populated on Linux based on what was on the primary replica.

## Next steps

