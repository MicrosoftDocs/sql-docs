---
author: rwestMSFT
ms.author: randolphwest
ms.date: 09/15/2022
ms.service: sql
ms.subservice: linux
ms.topic: include
---
## Add a database to the availability group

Ensure that the database you add to the availability group is in full recovery mode and has a valid log backup. If this is a test database or a newly created database, take a database backup. On the primary SQL Server, run the following Transact-SQL script to create and back up a database called `db1`:

```sql
CREATE DATABASE [db1];
ALTER DATABASE [db1] SET RECOVERY FULL;
BACKUP DATABASE [db1]
   TO DISK = N'/var/opt/mssql/data/db1.bak';
```

On the primary SQL Server replica, run the following Transact-SQL script to add a database called `db1` to an availability group called `ag1`:

```sql
ALTER AVAILABILITY GROUP [ag1] ADD DATABASE [db1];
```

### Verify that the database is created on the secondary servers

On each secondary SQL Server replica, run the following query to see if the `db1` database was created and is synchronized:

```sql
SELECT * FROM sys.databases WHERE name = 'db1';
GO
SELECT DB_NAME(database_id) AS 'database', synchronization_state_desc FROM sys.dm_hadr_database_replica_states;
```
