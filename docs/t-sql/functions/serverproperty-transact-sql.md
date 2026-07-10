---
title: "SERVERPROPERTY (Transact-SQL)"
description: SERVERPROPERTY returns property information about the server instance.
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest, amitkh-msft
ms.date: 01/08/2026
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
ms.custom:
  - ignite-2025
f1_keywords:
  - "SERVERPROPERTY_TSQL"
  - "SERVERPROPERTY"
  - "sql13.swb.serverpropeties.connections.f1"
helpviewer_keywords:
  - "Availability Groups [SQL Server], monitoring"
  - "SERVERPROPERTY function"
  - "server instance property information [SQL Server]"
  - "IsHadrEnabled server property"
  - "instances of SQL Server, property information"
  - "server properties [SQL Server]"
dev_langs:
  - TSQL
monikerRange: ">=aps-pdw-2016 || =azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current || =fabric || =fabric-sqldb"
---
# SERVERPROPERTY (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw-fabricse-fabricdw-fabricsqldb.md)]

Returns property information about the server instance.

:::image type="icon" source="../../includes/media/topic-link-icon.svg" border="false"::: [Transact-SQL syntax conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

[!INCLUDE [entra-id](../../includes/entra-id.md)]

## Syntax

```syntaxsql
SERVERPROPERTY ( 'propertyname' )
```

> [!IMPORTANT]
> The [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] version numbers for [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [fabric](../../includes/fabric.md)] aren't comparable with each other, and represent internal build numbers for these separate products. The [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] for [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] is based on the same code base as the [!INCLUDE [ssDEnoversion](../../includes/ssdenoversion-md.md)]. Most importantly, the [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] in [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] always has the newest SQL [!INCLUDE [ssDE-md](../../includes/ssde-md.md)] bits. For example, version 12 of [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] is newer than version 16 of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].

## Arguments

#### *propertyname*

An expression that contains the property information to be returned for the server. *propertyname* can be one of the following values. Use of a *propertyname* that is invalid or not supported on that version of the Database Engine returns `NULL`.

| Property | Values returned |
| --- | --- |
| `BuildClrVersion` | Version of the [!INCLUDE [msCoName](../../includes/msconame-md.md)] [!INCLUDE [dnprdnshort](../../includes/dnprdnshort-md.md)] common language runtime (CLR) that was used while building the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **nvarchar(128)** |
| `Collation` | Name of the default collation for the server.<br /><br />NULL = Input isn't valid, or an error.<br /><br />Base data type: **nvarchar(128)** |
| `CollationID` | ID of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] collation.<br /><br />Base data type: **int** |
| `ComparisonStyle` | Windows comparison style of the collation.<br /><br />Base data type: **int** |
| `ComputerNamePhysicalNetBIOS` | NetBIOS name of the local computer on which the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is currently running.<br /><br />For a clustered instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] on a failover cluster, this value changes as the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] fails over to other nodes in the failover cluster.<br /><br />On a stand-alone instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], this value remains constant and returns the same value as the `MachineName` property.<br /><br />**Note:** If the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] is in a failover cluster and you want to obtain the name of the failover clustered instance, use the `MachineName` property.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **nvarchar(128)** |
| `Edition` | Installed product edition of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)]. Use the value of this property to determine the features and the limits, such as [Compute capacity limits by edition of SQL Server](../../sql-server/compute-capacity-limits-by-edition-of-sql-server.md). 64-bit versions of the [!INCLUDE [ssDE](../../includes/ssde-md.md)] append (64-bit) to the version.<br /><br />Use the following [Edition table](#edition) to identify possible values.<br /><br />Base data type: **nvarchar(128)** |
| `EditionID` | Represents the ID of the installed product edition of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] instance. Use the value of this property to determine features and limits, such as [Compute capacity limits by edition of SQL Server](../../sql-server/compute-capacity-limits-by-edition-of-sql-server.md).<br /><br />Use the following [Edition table](#edition) to identify possible values.<br /><br />Base data type: **bigint** |
| `EngineEdition` | [!INCLUDE [ssDE](../../includes/ssde-md.md)] edition of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] installed on the server.<br /><br />1 = Personal or Desktop Engine (Not available in [!INCLUDE [ssVersion2005](../../includes/ssversion2005-md.md)] and later versions.)<br />2 = Standard (For Standard, Standard Developer, Web, and Business Intelligence.)<br />3 = Enterprise (For Enterprise, Enterprise Developer, Developer, and Evaluation editions.)<br />4 = Express (For Express, Express with Tools, and Express with Advanced Services)<br />5 = [!INCLUDE [ssSDS](../../includes/sssds-md.md)]<br />6 = [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)]<br />8 = [!INCLUDE [ssazuremi](../../includes/ssazuremi-md.md)]<br />9 = Azure SQL Edge (For all editions of Azure SQL Edge)<br />11 = Azure Synapse serverless SQL pool, or [!INCLUDE [fabric](../../includes/fabric.md)]<br />12 = [!INCLUDE [fabric](../../includes/fabric.md)] [!INCLUDE [fabric-sqldb](../../includes/fabric-sqldb.md)].<br /><br />Base data type: **int** |
| `FilestreamConfiguredLevel` | The configured level of FILESTREAM access. For more information, see [filestream access level](../../database-engine/configure-windows/filestream-access-level-server-configuration-option.md).<br /><br />0 = FILESTREAM is disabled<br />1 = FILESTREAM is enabled for Transact-SQL access<br />2 = FILESTREAM is enabled for Transact-SQL and local Win32 streaming access<br />3 = FILESTREAM is enabled for Transact-SQL and both local and remote Win32 streaming access.<br /><br />Base data type: **int** |
| `FilestreamEffectiveLevel` | The effective level of FILESTREAM access. This value can be different than the FilestreamConfiguredLevel if the level has changed and either an instance restart or a computer restart is pending. For more information, see [filestream access level](../../database-engine/configure-windows/filestream-access-level-server-configuration-option.md).<br /><br />0 = FILESTREAM is disabled<br />1 = FILESTREAM is enabled for Transact-SQL access<br />2 = FILESTREAM is enabled for Transact-SQL and local Win32 streaming access<br />3 = FILESTREAM is enabled for Transact-SQL and both local and remote Win32 streaming access.<br /><br />Base data type: **int** |
| `FilestreamShareName` | The name of the share used by FILESTREAM.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **nvarchar(128)** |
| `HadrManagerStatus` | Indicates whether the [!INCLUDE [ssHADR](../../includes/sshadr-md.md)] manager has started.<br /><br />0 = Not started, pending communication.<br />1 = Started and running.<br />2 = Not started and failed.<br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **int** |
| `InstanceDefaultBackupPath` | Name of the default path to the instance backup files.<br /><br />**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions.<br /><br />Base data type: **nvarchar(128)** |
| `InstanceDefaultDataPath` | Name of the default path to the instance data files.<br /><br />**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].<br /><br />Base data type: **nvarchar(128)** |
| `InstanceDefaultLogPath` | Name of the default path to the instance transaction log files.<br /><br />**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].<br /><br />Base data type: **nvarchar(128)** |
| `InstanceName` | Name of the instance to which the user is connected.<br /><br />Returns `NULL` if the instance name is the default instance, if the input isn't valid, or error.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **nvarchar(128)** |
| `IsAdvancedAnalyticsInstalled` | Returns 1 if the Advanced Analytics feature was installed during setup; 0 if Advanced Analytics wasn't installed.<br /><br />Base data type: **int** |
| `IsBigDataCluster` | Introduced in [!INCLUDE [ssSQL2019](../../includes/sssql19-md.md)] beginning with CU 4.<br /><br />Returns 1 if the instance is [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] Big Data Cluster; 0 if not.<br /><br />Base data type: **int** |
| `IsClustered` | Server instance is configured in a failover cluster.<br /><br />1 = Clustered.<br />0 = Not Clustered.<br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **int** |
| `IsExternalAuthenticationOnly` | Returns whether [Microsoft Entra-only authentication](/azure/azure-sql/database/authentication-azure-ad-only-authentication) is enabled.<br /><br />1 = Microsoft Entra-only authentication is enabled.<br />0 = Microsoft Entra-only authentication is disabled.<br /><br />**Applies to**: [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].<br /><br />Base data type: **int** |
| `IsExternalAuthenticationOnly` | Returns whether [SQL authentication](../../sql-server/azure-arc/disable-sql-authentication.md) is enabled.<br /><br />1 = SQL authentication is disabled (Microsoft Entra and Windows authentication only). <br />0 = SQL authentication is enabled.<br /><br />**Applies to**: [!INCLUDE [sql-server-2025](../../includes/sssql25-md.md)] <br /><br />Base data type: **int** |
| `IsExternalGovernanceEnabled` | Returns whether [Microsoft Purview access policies](/azure/purview/how-to-policies-data-owner-arc-sql-server) are enabled.<br /><br />1 = External governance is enabled.<br />0 = External governance is disabled.<br /><br />**Applies to**: [!INCLUDE [sql-server-2022](../../includes/sssql22-md.md)] and later versions.<br /><br />Base data type: **int** |
| `IsFullTextInstalled` | The full-text and semantic indexing components are installed on the current instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />1 = Full-text and semantic indexing components are installed.<br />0 = Full-text and semantic indexing components aren't installed.<br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **int** |
| `IsHadrEnabled` | [!INCLUDE [ssHADR](../../includes/sshadr-md.md)] is enabled on this server instance.<br /><br />0 = The [!INCLUDE [ssHADR](../../includes/sshadr-md.md)] feature is disabled.<br />1 = The [!INCLUDE [ssHADR](../../includes/sshadr-md.md)] feature is enabled.<br />NULL = Input isn't valid, an error, or not applicable.<br /><br />For availability replicas to be created and run on an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE [ssHADR](../../includes/sshadr-md.md)] must be enabled on the server instance. For more information, see [Enable or disable Always On availability group feature](../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md).<br /><br />**Note:** The `IsHadrEnabled` property pertains only to [!INCLUDE [ssHADR](../../includes/sshadr-md.md)]. Other high availability or disaster recovery features, such as database mirroring or log shipping, are unaffected by this server property.<br /><br />**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].<br /><br />Base data type: **int** |
| `IsIntegratedSecurityOnly` | Server is in integrated security mode.<br /><br />1 = Integrated security (Windows Authentication)<br />0 = Not integrated security. (Both Windows Authentication and [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.)<br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **int** |
| `IsLocalDB` | Server is an instance of [!INCLUDE [ssExpress](../../includes/ssexpress-md.md)] LocalDB.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)].<br /><br />Base data type: **int** |
| `IsPolyBaseInstalled` | Returns whether the server instance has the PolyBase feature installed.<br /><br />0 = PolyBase isn't installed.<br />1 = PolyBase is installed.<br /><br />**Applies to**: [!INCLUDE [sssql16-md](../../includes/sssql16-md.md)] and later versions.<br /><br />Base data type: **int** |
| `IsServerSuspendedForSnapshotBackup` | Server is in suspend mode and requires server level thaw.<br /><br />1 = Suspended.<br />0 = Not suspended.<br /><br />Base data type: **int** |
| `IsSingleUser` | Server is in single-user mode.<br /><br />1 = Single user.<br />0 = Not single user<br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **int** |
| `IsTempDbMetadataMemoryOptimized` | Returns 1 if `tempdb` has been enabled to use memory-optimized tables for metadata; 0 if `tempdb` is using regular, disk-based tables for metadata. For more information, see [tempdb Database](../../relational-databases/databases/tempdb-database.md#memory-optimized-tempdb-metadata).<br /><br />**Applies to**: [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] and later versions.<br /><br />Base data type: **int** |
| `IsXTPSupported` | Server supports In-Memory OLTP.<br /><br />1 = Server supports In-Memory OLTP.<br />0 = Server doesn't supports In-Memory OLTP.<br />NULL = Input isn't valid, an error, or not applicable.<br /><br />**Applies to**: [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] and later versions, and [!INCLUDE [ssazure-sqldb](../../includes/ssazure-sqldb.md)].<br /><br />Base data type: **int** |
| `LCID` | Windows locale identifier (LCID) of the collation.<br /><br />Base data type: **int** |
| `LicenseType` | Unused. License information isn't preserved or maintained by the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] product. Always returns DISABLED.<br /><br />Base data type: **nvarchar(128)** |
| `MachineName` | Windows computer name on which the server instance is running.<br /><br />For a clustered instance, an instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] running on a virtual server on Microsoft Cluster Service, it returns the name of the virtual server.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **nvarchar(128)** |
| `NumLicenses` | Unused. License information isn't preserved or maintained by the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] product. Always returns `NULL`.<br /><br />Base data type: **int** |
| `PathSeparator` | Returns `\` on Windows and `/` on Linux<br /><br />**Applies to**: [!INCLUDE [sssql17-md](../../includes/sssql17-md.md)] and later versions.<br /><br />Base data type: **nvarchar** |
| `ProcessID` | Process ID of the [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)] service. ProcessID is useful in identifying which Sqlservr.exe belongs to this instance.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **int** |
| `ProductBuild` | The build number.<br /><br />**Applies to**: [!INCLUDE [sssql14-md](../../includes/sssql14-md.md)] and later versions.<br /><br />Base data type: **nvarchar(128)** |
| `ProductBuildType` | Type of build of the current build.<br /><br />Returns one of the following values:<br /><br />OD = On Demand release a specific customer.<br />GDR = General Distribution Release released through Windows Update.<br />NULL = Not applicable.<br /><br />**Applies to**: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />Base data type: **nvarchar(128)** |
| `ProductLevel` | Level of the version of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />Returns one of the following values:<br /><br />'RTM' = Original release version<br />'SP*n*' = Service pack version<br />'CTP*n*', = Community Technology Preview version.<br /><br />Base data type: **nvarchar(128)** |
| `ProductMajorVersion` | The major version.<br /><br />**Applies to**: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />Base data type: **nvarchar(128)** |
| `ProductMinorVersion` | The minor version.<br /><br />**Applies to**: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />Base data type: **nvarchar(128)** |
| `ProductUpdateLevel` | Update level of the current build. CU indicates a cumulative update.<br /><br />Returns one of the following values:<br /><br />CU*n* = Cumulative Update<br />NULL = Not applicable.<br /><br />**Applies to**: [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] and [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].<br /><br />Base data type: **nvarchar(128)** |
| `ProductUpdateReference` | KB article for that release.<br /><br />**Applies to**: [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />Base data type: **nvarchar(128)** |
| `ProductUpdateType` | Update cadence the instance follows. Corresponds to the [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] [update policy](/azure/azure-sql/managed-instance/update-policy).<br /><br />Returns one of the following values:<br /><br />CU = Updates are deployed via cumulative updates (CUs) for the corresponding major SQL Server release (**SQL Server 2022** update policy).<br /><br />Continuous = New features are brought to [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)] as soon as they're available, independent of the SQL Server release cadence (**Always-up-to-date** update policy).<br /><br />**Applies to**: [!INCLUDE [ssazuremi-md](../../includes/ssazuremi-md.md)].<br /><br />Base data type: **nvarchar(128)** |
| `ProductVersion` | Version of the instance of [!INCLUDE [ssNoVersion](../../includes/ssnoversion-md.md)], in the form of *major.minor.build.revision*.<br /><br />Base data type: **nvarchar(128)** |
| `ResourceLastUpdateDateTime` | Returns the date and time that the Resource database was last updated.<br /><br />Base data type: **datetime** |
| `ResourceVersion` | Returns the version Resource database.<br /><br />Base data type: **nvarchar(128)** |
| `ServerName` | Both the Windows server and instance information associated with a specified instance.<br /><br />NULL = Input isn't valid, or an error.<br /><br />Base data type: **nvarchar(128)** |
| `SqlCharSet` | The SQL character set ID from the collation ID.<br /><br />Base data type: **tinyint** |
| `SqlCharSetName` | The SQL character set name from the collation.<br /><br />Base data type: **nvarchar(128)** |
| `SqlSortOrder` | The SQL sort order ID from the collation.<br /><br />Base data type: **tinyint** |
| `SqlSortOrderName` | The SQL sort order name from the collation.<br /><br />Base data type: **nvarchar(128)** |
| `SuspendedDatabaseCount` | The number of suspended databases on the server.<br /><br />Base data type: **int** |

<a id="edition"></a>

The following table lists possible values for `EditionID` and `Edition`.

| EditionID | Edition |
| ---: | --- |
| `1804890536` | Enterprise |
| `1872460670` | Enterprise Edition: Core-based Licensing |
| `610778273` | Enterprise Evaluation |
| `284895786` | Business Intelligence |
| `-2117995310` | Developer <sup>1</sup>, or Developer Enterprise <sup>2</sup> |
| `-1785266663` | Developer Standard <sup>2</sup> |
| `-1592396055` | Express |
| `-133711905` | Express with Advanced Services |
| `-1534726760` | Standard |
| `1293598313` | Web <sup>1</sup> |
| `1674378470` | [!INCLUDE [ssSDS](../../includes/sssds-md.md)] or [!INCLUDE [ssazuresynapse-md](../../includes/ssazuresynapse-md.md)] |
| `-1461570097` | Azure SQL Edge Developer <sup>3</sup> |
| `1994083197` | Azure SQL Edge <sup>4</sup> |

<sup>1</sup> **Applies to**: [!INCLUDE [sssql22-md](../../includes/sssql22-md.md)] and earlier versions.  
<sup>2</sup> **Applies to**: [!INCLUDE [sssql25-md](../../includes/sssql25-md.md)] and later versions.  
<sup>3</sup> **Applies to**: Azure SQL Edge (development edition).  
<sup>4</sup> **Applies to**: Azure SQL Edge (paid edition).

## Return types

**sql_variant**

## Remarks

### ServerName property

The `ServerName` property of the `SERVERPROPERTY` function and [@@SERVERNAME](servername-transact-sql.md) return similar information. The `ServerName` property provides the Windows server and instance name that together make up the unique server instance. [@@SERVERNAME](servername-transact-sql.md) provides the currently configured local server name.

The `ServerName` property and [@@SERVERNAME](servername-transact-sql.md) return the same information if the default server name, at the time of installation, hasn't been changed. The local server name can be configured by executing the following:

```sql
EXECUTE sp_dropserver 'current_server_name';
GO

EXECUTE sp_addserver 'new_server_name', 'local';
GO
```

If the local server name has been changed from the default server name at installation time, [@@SERVERNAME](servername-transact-sql.md) returns the new name.

The `ServerName` property of the `SERVERPROPERTY` function returns the Windows server name as it's saved. In previous major versions, it returned uppercase. This behavior changed back to uppercase between [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 9 and CU 12, but starting from [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU 13, the server name returns as it's saved.

If the Windows server name contains any lowercase characters, this change of behavior might cause differences between the `ServerName` property of the `SERVERPROPERTY` function, and [@@SERVERNAME](servername-transact-sql.md) (uppercase versus lowercase), even if there's no name change for the server.

Consider you have a server named as `server01`, with a [!INCLUDE [ssnoversion-md](../../includes/ssnoversion-md.md)] instance named `INST1`. The following table summarizes the change of behavior between different builds of [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)]:

| [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] release | SERVERPROPERTY('ServerName') | Additional information |
| --- | --- | --- |
| RTM | `SERVER01\INST1` | Returns the `ServerName` property in uppercase |
| CU 1 – CU 8 | `server01\INST1` | Returns the `ServerName` property as is, without changing to uppercase |
| CU 9 – CU 12 | `SERVER01\INST1` | Returns the `ServerName` property in uppercase |
| CU 13 and later versions | `server01\INST1` | Returns the `ServerName` property as-is, without changing to uppercase |

### Version properties

The `SERVERPROPERTY` function returns individual properties that relate to the version information whereas the [@@VERSION](version-transact-sql-configuration-functions.md) function combines the output into one string. If your application requires individual property strings, you can use the `SERVERPROPERTY` function to return them instead of parsing the [@@VERSION](version-transact-sql-configuration-functions.md) results.

## Permissions

All users can query the server properties.

## Examples

The following example uses the `SERVERPROPERTY` function in a `SELECT` statement to return information about the current instance of [!INCLUDE [ssNoVersion_md](../../includes/ssnoversion-md.md)].

```sql
SELECT SERVERPROPERTY('MachineName') AS ComputerName,
       SERVERPROPERTY('ServerName') AS InstanceName,
       SERVERPROPERTY('Edition') AS Edition,
       SERVERPROPERTY('ProductVersion') AS ProductVersion,
       SERVERPROPERTY('ProductLevel') AS ProductLevel;
GO
```

## Related content

- [Editions and supported features of SQL Server 2025](../../sql-server/editions-and-components-of-sql-server-2025.md)
- [Editions and supported features of SQL Server 2022](../../sql-server/editions-and-components-of-sql-server-2022.md)
- [Editions and supported features of SQL Server 2019](../../sql-server/editions-and-components-of-sql-server-2019.md)
- [Editions and supported features of SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md)
- [Editions and supported features of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md)
