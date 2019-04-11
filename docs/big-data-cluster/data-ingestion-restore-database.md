---
title: Restore a database
titleSuffix: SQL Server big data clusters
description: This article shows how to restore a database into the master instance of a SQL Server 2019 big data cluster (preview).
author: rothja
ms.author: jroth
manager: craigg
ms.date: 12/06/2018
ms.topic: conceptual
ms.prod: sql
ms.technology: big-data-cluster
ms.custom: seodec18
---

# Restore a database into the SQL Server big data cluster master instance

[!INCLUDE[tsql-appliesto-ssver15-xxxx-xxxx-xxx](../includes/tsql-appliesto-ssver15-xxxx-xxxx-xxx.md)]

This article describes how to restore an existing database into the master instance of a SQL Server 2019 big data cluster (preview). The recommended method is to use a backup, copy, and restore approach.

## Backup your existing database

First, backup your existing SQL Server database from either SQL Server on Windows or Linux. Use standard backup techniques with Transact-SQL or with a tool like SQL Server Management Studio (SSMS).

This article shows how to restore the AdventureWorks database, but you can use any database backup. 

> [!TIP]
> You can download the AdventureWorks backup [here](https://www.microsoft.com/download/details.aspx?id=49502).

## Copy the backup file

Copy the backup file to the SQL Server container in the master instance pod of the Kubernetes cluster.

```bash
kubectl cp <path to .bak file> mssql-master-pool-0:/tmp -c mssql-server -n <name of your cluster>
```

Example:

```bash
kubectl cp ~/Downloads/AdventureWorks2016CTP3.bak mssql-master-pool-0:/tmp -c mssql-server -n clustertest
```

Then, verify that the backup file was copied to the pod container.

```bash
kubectl exec -it mssql-master-pool-0 -n <name of your cluster> -c mssql-server -- bin/bash
cd /var/
ls /tmp
exit
```

Example:

```bash
kubectl exec -it mssql-master-pool-0 -n clustertest -c mssql-server -- bin/bash
ls /tmp
exit
```

## Restore the backup file

Next, restore the database backup to master instance SQL Server.  If you are restoring a database backup that was created on Windows, you will need to get the names of the files.  In Azure Data Studio, connect to the master instance and run this SQL script:

```sql
RESTORE FILELISTONLY FROM DISK='/tmp/<db file name>.bak'
```

Example:

```sql
RESTORE FILELISTONLY FROM DISK='/tmp/AdventureWorks2016CTP3.bak'
```

![Backup file list](media/restore-database/database-restore-file-list.png)

Now, restore the database. The following script is an example. Replace the names/paths as needed depending on your database backup.

```sql
RESTORE DATABASE AdventureWorks2016CTP3
FROM DISK='/tmp/AdventureWorks2016CTP3.bak'
WITH MOVE 'AdventureWorks2016CTP3_Data' TO '/var/opt/mssql/data/AdventureWorks2016CTP3_Data.mdf',
        MOVE 'AdventureWorks2016CTP3_Log' TO '/var/opt/mssql/data/AdventureWorks2016CTP3_Log.ldf',
        MOVE 'AdventureWorks2016CTP3_mod' TO '/var/opt/mssql/data/AdventureWorks2016CTP3_mod'
```

## Configure data pool and HDFS access

Now, for the SQL Server master instance to access data pools and HDFS, run the data pool and storage pool stored procedures. Run the following Transact-SQL scripts against your newly restored database:

```sql
USE AdventureWorks2016CTP3
GO
-- Create the SqlDataPool data source:
IF NOT EXISTS(SELECT * FROM sys.external_data_sources WHERE name = 'SqlDataPool')
  CREATE EXTERNAL DATA SOURCE SqlDataPool
  WITH (LOCATION = 'sqldatapool://service-mssql-controller:8080/datapools/default');

-- Create the SqlStoragePool data source:
IF NOT EXISTS(SELECT * FROM sys.external_data_sources WHERE name = 'SqlStoragePool')
BEGIN
    CREATE EXTERNAL DATA SOURCE SqlStoragePool
    WITH (LOCATION = 'sqlhdfs://service-master-pool:50070');
END
GO
```

> [!NOTE]
> You will have to run through these setup scripts only for databases restored from older versions of SQL Server. If you create a new database in SQL Server master instance, data pool and storage pool store procedures are already configured for you.

## Next steps

To learn more about the SQL Server big data clusters, see the following overview:

- [What are SQL Server 2019 big data clusters?](big-data-cluster-overview.md)
