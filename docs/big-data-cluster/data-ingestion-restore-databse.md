---
title: Restore a database into SQL Server big data cluster | Microsoft Docs
description:
author: rothja
ms.author: jroth
manager: craigg
ms.date: 10/01/2018
ms.topic: conceptual
ms.prod: sql
---

# Restore a database into the SQL Server big data cluster master instance

To bring an existing SQL Server database into the master instance, we recommend using a backup, copy, and restore approach.  This article shows how to restore the AdventureWorks database. You can use any database backup. You can download the AdventureWorks backup [here](https://www.microsoft.com/en-us/download/details.aspx?id=49502).

First, backup your existing SQL Server database from either SQL Server on Windows or Linux. Use any of the usual database backup methods.

Copy the backup file to the SQL Server container in the master instance pod of the Kubernetes cluster.

```bash
kubectl cp <path to .bak file> mssql-data-pool-master-0:/tmp/ -c mssql-data-pool-data -n <name of your cluster>
```

Example:

```bash
kubectl cp ~/Downloads/AdventureWorks2016CTP3.bak mssql-data-pool-master-0:/tmp/ -c mssql-data-pool-data -n clustertest
```

Then, verify that the backup file was copied to the pod container.

```bash
kubectl exec -it mssql-data-pool-master-0 -n <name of your cluster> -c mssql-data-pool-data -- bin/bash
root@mssql-data-pool-master-0:/# ls /tmp
root@mssql-data-pool-master-0:/# exit
```

Example:

```bash
kubectl exec -it mssql-data-pool-master-0 -n clustertest -c mssql-data-pool-data -- bin/bash
root@mssql-data-pool-master-0:/# ls /tmp
```

Next, restore the database backup to master instance SQL Server.  If you are restoring a database backup that was created on Windows, you will need to get the names of the files.  In Ops Studio connected to the master instance run this SQL script:

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

Now, for the SQL Server master instance to access data pools and HDFS, setup the data pool and storage pool stored procedure. Run the following Transact-SQL scripts against your newly restored database:

```sql
USE AdventureWorks2016CTP3
GO 
IF NOT EXISTS(SELECT * FROM sys.external_data_sources WHERE name = 'SqlDataPool')
    CREATE EXTERNAL DATA SOURCE SqlDataPool
    WITH (LOCATION = 'sqldatapool://service-mssql-controller:8080/datapools/default');

IF NOT EXISTS(SELECT * FROM sys.external_data_sources WHERE name = 'SqlStoragePool')
    CREATE EXTERNAL DATA SOURCE SqlStoragePool
    WITH (LOCATION = 'sqlhdfs://service-mssql-controller:8080');
GO
```

> [!NOTE]
> You will have to run through these setup scripts only for databases restored from older versions of SQL Server. If you create a new database in SQL Server master instance, data pool and storage pool store procedures are already setup!
