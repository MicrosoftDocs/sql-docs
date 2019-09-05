## Enabling and controlling ADR
ADR is off by default in <SS2019>, and can be controlled using DDL syntax:
```sql
ALTER DATABASE [DB] SET ACCELERATED_DATABASE_RECOVERY = {ON | OFF}
[(PERSISTENT_VERSION_STORE_FILEGROUP = { filegroup name }) ];

```

Using this syntax, you can control whether the feature is on or off, and optionally designate a specific filegroup for the Persistent Version Store data.  If no filegroup is specified, the PVS will be stored in the PRIMARY filegroup.

## Managing the Persistent Version Store Filegroup
The ADR feature is based on having changes versioned, with different versions of a data element kept in the "Persistent Version Store" (PVS).
There are considerations to locating where the PVS is located and in how to manage the size of the data in the PVS.

### To enable ADR without specifying a filegroup

```sql
ALTER DATABASE [MyDatabase] SET ACCELERATED_DATABASE_RECOVERY = ON;
GO
```

In this case, when the PVS filegroup is not specified, the PRIMARY filegroup is used to hold the PVS.

### To enable ADR and specify that the PVS should be stored in the [VersionStoreFG] filegroup

```sql
ALTER DATABASE [MyDatabase] SET ACCELERATED_DATABASE_RECOVERY = ON
(PERSISTENT_VERSION_STORE_FILEGROUP = [VersionStoreFG])

```

### To disable the ADR feature

```sql
ALTER DATABASE [MyDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF;
GO
```

Even after the ADR feature is disabled, there will be versions stored in the Persistent Version Store that are still needed to restore the database until the versions have been preserved.

### To change the location of the PVS to a different filegroup

You can change from the current location to a different filegroup for a variety of reasons, for example moving to a location with more space, or moving to faster storage, etc.
Changing the location of the PVS is a three step process.

#### Turn the ADR feature off

```sql
ALTER DATABASE [MyDatabase] SET ACCELERATED_DATABASE_RECOVERY = OFF;
GO
```

#### Wait until all of the versions stored in the PVS can be freed

In order to be able to turn on ADR with a new location for the Persistent Version Store, you must first make sure that all of the version information has been purged from the previous PVS location.  In order to force that cleanup to happen, you first run the command 

```sql
EXEC sys.sp_persistent_version_store_cleanup [database name]
```

This stored procedure is synchronous, meaning that it will not complete until all version information is cleaned up from the current PVS.  Once it completes, you can verify that the version information is indeed removed by querying the DMV sys.dm_persistent_version_store_stats and examining the value of <>.

```sql
SELECT DB_Name(database_id), persistent_version_store_size_kb 
FROM sys.dm_tran_persistent_version_store_stats where database_id = [MyDatabaseID]
```

When the value of persistent_version_store_size_kb is 0, you can re-enable the ADR feature, configuring the PVS to be located in the new filegroup.

#### Turn on ADR specifying the new location.

```sql
ALTER DATABASE [MyDatabase] SET ACCELERATED_DATABASE_RECOVERY = ON
(PERSISTENT_VERSION_STORE_FILEGROUP = [VersionStoreFG])

```
