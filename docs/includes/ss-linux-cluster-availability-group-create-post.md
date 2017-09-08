
## Add a database to the availability group

Ensure the database you are adding to the Availability group is in full recovery mode and has a valid log backup. If this is a test database or a new database created, take a database backup. On the primary SQL Server, run the following Transact-SQL to create and back up a database called `db1`.

```Transact-SQL
CREATE DATABASE [db1];
ALTER DATABASE [db1] SET RECOVERY FULL;
BACKUP DATABASE [db1] 
   TO DISK = N'/var/opt/mssql/data/db1.bak';
```

On the primary SQL Server replica, run the following Transact-SQL to add a database called `db1` to an availability group called `ag1`.

```Transact-SQL
ALTER AVAILABILITY GROUP [ag1] ADD DATABASE [db1];
```

### Verify that the database is created on the secondary servers

On each secondary SQL Server replica, run the following query to see if the `db1` database has been created and is synchronized.

```Transact-SQL
SELECT * FROM sys.databases WHERE name = 'db1';
GO
SELECT DB_NAME(database_id) AS 'database', synchronization_state_desc FROM sys.dm_hadr_database_replica_states;
```
