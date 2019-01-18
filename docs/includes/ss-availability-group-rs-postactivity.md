
## Add a database to the availability group

Ensure that the database you add to the availability group is in full recovery mode and has a valid log backup. If the database is a test database or a newly created database, take a database backup. To create and back up a database called `db1`, run the following Transact-SQL script on the primary SQL Server instance:

```sql
CREATE DATABASE [db1];
ALTER DATABASE [db1] SET RECOVERY FULL;
BACKUP DATABASE [db1]
   TO DISK = N'/var/opt/mssql/data/db1.bak';
```

To add a database called `db1` to an availability group called `ag1`, run the following Transact-SQL script on the primary SQL Server replica:

```sql
ALTER AVAILABILITY GROUP [ag1] ADD DATABASE [db1];
```

### Verify that the database is created on the secondary servers

To see whether the `db1` database was created and is synchronized, run the following query on each secondary SQL Server replica:

```sql
SELECT * FROM sys.databases WHERE name = 'db1';
GO
SELECT DB_NAME(database_id) AS 'database', synchronization_state_desc FROM sys.dm_hadr_database_replica_states;
```
