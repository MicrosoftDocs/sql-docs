---
title: "SERVERPROPERTY (Transact-SQL)"
description: "SERVERPROPERTY returns property information about the server instance"
author: VanMSFT
ms.author: vanto
ms.reviewer: randolphwest, amitkh-msft
ms.date: 11/22/2022
ms.service: sql
ms.subservice: t-sql
ms.topic: reference
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
  - "TSQL"
monikerRange: ">= aps-pdw-2016 || = azuresqldb-current || = azure-sqldw-latest || >= sql-server-2016 || >= sql-server-linux-2017 || = azuresqldb-mi-current"
---
# SERVERPROPERTY (Transact-SQL)

[!INCLUDE [sql-asdb-asdbmi-asa-pdw](../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

Returns property information about the server instance.

:::image type="icon" source="../../database-engine/configure-windows/media/topic-link.gif" border="false"::: [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)

## Syntax

```syntaxsql
SERVERPROPERTY ( 'propertyname' )
```

> [!IMPORTANT]  
> The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] version numbers for [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] are not comparable with each other, and represent internal build numbers for these separate products. The [!INCLUDE[ssde_md](../../includes/ssde_md.md)] for [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] is based on the same code base as the [!INCLUDE[ssDEnoversion](../../includes/ssdenoversion-md.md)]. Most importantly, the [!INCLUDE[ssde_md](../../includes/ssde_md.md)] in [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] always has the newest SQL [!INCLUDE[ssde_md](../../includes/ssde_md.md)] bits. Version 12 of [!INCLUDE[ssSDSfull](../../includes/sssdsfull-md.md)] is newer than version 15 of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].

[!INCLUDE[sql-server-tsql-previous-offline-documentation](../../includes/sql-server-tsql-previous-offline-documentation.md)]

## Arguments

#### *propertyname*

An expression that contains the property information to be returned for the server. *propertyname* can be one of the values below. Use of a *propertyname* that is invalid or not supported on that version of the Database Engine will return `NULL`.

| Property | Values returned |
| -------------- | -------------- |
| `BuildClrVersion` | Version of the [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] common language runtime (CLR) that was used while building the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **nvarchar(128)** |
| `Collation` | Name of the default collation for the server.<br /><br />NULL = Input isn't valid, or an error.<br /><br />Base data type: **nvarchar(128)** |
| `CollationID` | ID of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collation.<br /><br />Base data type: **int** |
| `ComparisonStyle` | Windows comparison style of the collation.<br /><br />Base data type: **int** |
| `ComputerNamePhysicalNetBIOS` | NetBIOS name of the local computer on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is currently running.<br /><br />For a clustered instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a failover cluster, this value changes as the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] fails over to other nodes in the failover cluster.<br /><br />On a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this value remains constant and returns the same value as the MachineName property.<br /><br />**Note:** If the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is in a failover cluster and you want to obtain the name of the failover clustered instance, use the MachineName property.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **nvarchar(128)** |
| `Edition` | Installed product edition of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use the value of this property to determine the features and the limits, such as [Compute Capacity Limits by Edition of SQL Server](../../sql-server/compute-capacity-limits-by-edition-of-sql-server.md). 64-bit versions of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] append (64-bit) to the version.<br /><br />Returns:<br /><br />'Enterprise Edition'<br /><br />'Enterprise Edition: Core-based Licensing'<br /><br />'Enterprise Evaluation Edition'<br /><br />'Business Intelligence Edition'<br /><br />'Developer Edition'<br /><br />'Express Edition'<br /><br />'Express Edition with Advanced Services'<br /><br />'Standard Edition'<br /><br />'Web Edition'<br /><br />'SQL Azure' indicates [!INCLUDE[ssSDS](../../includes/sssds-md.md)] or [!INCLUDE[ssDW](../../includes/ssdw-md.md)]<br /><br />'Azure SQL Edge Developer' indicates the development only edition for Azure SQL Edge<br /><br />'Azure SQL Edge' indicates the paid edition for Azure SQL Edge<br /><br />Base data type: **nvarchar(128)** |
| `EditionID` | EditionID represents the installed product edition of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use the value of this property to determine features and limits, such as [Compute Capacity Limits by Edition of SQL Server](../../sql-server/compute-capacity-limits-by-edition-of-sql-server.md).<br /><br />1804890536 = Enterprise<br /><br />1872460670 = Enterprise Edition: Core-based Licensing<br /><br />610778273 = Enterprise Evaluation<br /><br />284895786 = Business Intelligence<br /><br />-2117995310 = Developer<br /><br />-1592396055 = Express<br /><br />-133711905 = Express with Advanced Services<br /><br />-1534726760 = Standard<br /><br />1293598313 = Web<br /><br />1674378470 = [!INCLUDE[ssSDS](../../includes/sssds-md.md)] or [!INCLUDE[ssSDW](../../includes/sssdwfull-md.md)]<br /><br />-1461570097 = Azure SQL Edge Developer<br /><br />1994083197 = Azure SQL Edge<br /><br />Base data type: **bigint** |
| `EngineEdition` | [!INCLUDE[ssDE](../../includes/ssde-md.md)] edition of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed on the server.<br /><br />1 = Personal or Desktop Engine (Not available in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions.)<br /><br />2 = Standard (For Standard, Web, and Business Intelligence.)<br /><br />3 = Enterprise (For Evaluation, Developer, and Enterprise editions.)<br /><br />4 = Express (For Express, Express with Tools, and Express with Advanced Services)<br /><br />5 = [!INCLUDE[ssSDS](../../includes/sssds-md.md)]<br /><br />6 = [!INCLUDE[ssDW](../../includes/ssdw-md.md)]<br /><br />8 = [!INCLUDE[ssSDSMIfull](../../includes/sssdsmifull-md.md)]<br /><br />9 = Azure SQL Edge (For all editions of Azure SQL Edge)<br /><br />11 = Azure Synapse serverless SQL pool<br /><br />Base data type: **int** |
| `FilestreamConfiguredLevel` | The configured level of FILESTREAM access. For more information, see [filestream access level](../../database-engine/configure-windows/filestream-access-level-server-configuration-option.md).<br /><br />Base data type: **int** |
| `FilestreamEffectiveLevel` | The effective level of FILESTREAM access. This value can be different than the FilestreamConfiguredLevel if the level has changed and either an instance restart or a computer restart is pending. For more information, see [filestream access level](../../database-engine/configure-windows/filestream-access-level-server-configuration-option.md).<br /><br />Base data type: **int** |
| `FilestreamShareName` | The name of the share used by FILESTREAM.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **nvarchar(128)** |
| `HadrManagerStatus` | **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br />Indicates whether the [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] manager has started.<br /><br />0 = Not started, pending communication.<br /><br />1 = Started and running.<br /><br />2 = Not started and failed.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **int** |
| `InstanceDefaultBackupPath` | **Applies to**: [!INCLUDE[ssSQL2019](../../includes/sssql19-md.md)] and later.<br /><br />Name of the default path to the instance backup files. |
| `InstanceDefaultDataPath` | **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through current version in updates beginning in late 2015.<br /><br />Name of the default path to the instance data files.<br /><br />Base data type: **nvarchar(128)** |
| `InstanceDefaultLogPath` | **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through current version in updates beginning in late 2015.<br /><br />Name of the default path to the instance log files.<br /><br />Base data type: **nvarchar(128)** |
| `InstanceName` | Name of the instance to which the user is connected.<br /><br />Returns NULL if the instance name is the default instance, if the input isn't valid, or error.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **nvarchar(128)** |
| `IsAdvancedAnalyticsInstalled` | Returns 1 if the Advanced Analytics feature was installed during setup; 0 if Advanced Analytics wasn't installed.<br /><br />Base data type: **int** |
| `IsBigDataCluster` | Introduced in [!INCLUDE[ssSQL2019](../../includes/sssql19-md.md)] beginning with CU4.<br /><br />Returns 1 if the instance is SQL Server Big Data Cluster; 0 if not.<br /><br />Base data type: **int** |
| `IsClustered` | Server instance is configured in a failover cluster.<br /><br />1 = Clustered.<br /><br />0 = Not Clustered.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **int** |
| `IsExternalAuthenticationOnly` | **Applies to**: Azure SQL Database and Azure SQL Managed Instance.<br /><br />Returns whether [Azure AD-only authentication](/azure/azure-sql/database/authentication-azure-ad-only-authentication) is enabled.<br /><br />1 = Azure AD-only authentication is enabled.<br /><br />0 = Azure AD-only authentication is disabled.<br /><br />Base data type: **int** |
| `IsExternalGovernanceEnabled` | **Applies to**: [!INCLUDE[sql-server-2022](../../includes/sssql22-md.md)] and later.<br /><br />Returns whether [Microsoft Purview access policies](/azure/purview/how-to-policies-data-owner-arc-sql-server) are enabled.<br /><br />1 = External governance is enabled.<br /><br />0 = External governance is disabled.<br /><br />Base data type: **int** |
| `IsFullTextInstalled` | The full-text and semantic indexing components are installed on the current instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />1 = Full-text and semantic indexing components are installed.<br /><br />0 = Full-text and semantic indexing components aren't installed.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **int** |
| `IsHadrEnabled` | **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br />[!INCLUDE[ssHADR](../../includes/sshadr-md.md)] is enabled on this server instance.<br /><br />0 = The [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] feature is disabled.<br /><br />1 = The [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] feature is enabled.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **int**<br /><br />For availability replicas to be created and run on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] must be enabled on the server instance. For more information, see [Enable and Disable Always On Availability Groups (SQL Server)](../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md).<br /><br />**Note:** The IsHadrEnabled property pertains only to [!INCLUDE[ssHADR](../../includes/sshadr-md.md)]. Other high availability or disaster recovery features, such as database mirroring or log shipping, are unaffected by this server property. |
| `IsIntegratedSecurityOnly` | Server is in integrated security mode.<br /><br />1 = Integrated security (Windows Authentication)<br /><br />0 = Not integrated security. (Both Windows Authentication and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.)<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **int** |
| `IsLocalDB` | **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] and later.<br /><br />Server is an instance of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] LocalDB.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **int** |
| `IsPolyBaseInstalled` | **Applies to**: [!INCLUDE[ssSQL2016](../../includes/sssql16-md.md)].<br /><br />Returns whether the server instance has the PolyBase feature installed.<br /><br />0 = PolyBase isn't installed.<br /><br />1 = PolyBase is installed.<br /><br />Base data type: **int** |
| `IsServerSuspendedForSnapshotBackup` | Server is in suspend mode and requires server level thaw.<br /><br />1 = Suspended.<br /><br />0 = Not suspended<br /><br />Base data type: **int** |
| `IsSingleUser` | Server is in single-user mode.<br /><br />1 = Single user.<br /><br />0 = Not single user<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **int** |
| `IsTempDbMetadataMemoryOptimized` | **Applies to**: [!INCLUDE[ssSQL2019](../../includes/sssql19-md.md)] and later.<br /><br />Returns 1 if tempdb has been enabled to use memory-optimized tables for metadata; 0 if tempdb is using regular, disk-based tables for metadata. For more information, see [tempdb Database](../../relational-databases/databases/tempdb-database.md#memory-optimized-tempdb-metadata).<br /><br />Base data type: **int** |
| `IsXTPSupported` | **Applies to**: SQL Server ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] and later), [!INCLUDE[ssSDS](../../includes/sssds-md.md)].<br /><br />Server supports In-Memory OLTP.<br /><br />1 = Server supports In-Memory OLTP.<br /><br />0 = Server doesn't supports In-Memory OLTP.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **int** |
| `LCID` | Windows locale identifier (LCID) of the collation.<br /><br />Base data type: **int** |
| `LicenseType` | Unused. License information isn't preserved or maintained by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product. Always returns DISABLED.<br /><br />Base data type: **nvarchar(128)** |
| `MachineName` | Windows computer name on which the server instance is running.<br /><br />For a clustered instance, an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on a virtual server on Microsoft Cluster Service, it returns the name of the virtual server.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **nvarchar(128)** |
| `NumLicenses` | Unused. License information isn't preserved or maintained by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product. Always returns NULL.<br /><br />Base data type: **int** |
| `PathSeparator` | **Applies to**: [!INCLUDE[ssSQL2017](../../includes/sssql17-md.md)] and later.<br /><br />Returns `\` on Windows and `/` on Linux<br /><br />Base data type: **nvarchar** |
| `ProcessID` | Process ID of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service. ProcessID is useful in identifying which Sqlservr.exe belongs to this instance.<br /><br />NULL = Input isn't valid, an error, or not applicable.<br /><br />Base data type: **int** |
| `ProductBuild` | **Applies to**: [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] beginning October 2015.<br /><br />The build number.<br /><br />Base data type: **nvarchar(128)** |
| `ProductBuildType` | **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through current version in updates beginning in late 2015.<br /><br />Type of build of the current build.<br /><br />Returns one of the following values:<br /><br />OD = On Demand release a specific customer.<br /><br />GDR = General Distribution Release released through Windows Update.<br /><br />NULL = Not applicable.<br /><br />Base data type: **nvarchar(128)** |
| `ProductLevel` | Level of the version of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />Returns one of the following values:<br /><br />'RTM' = Original release version<br /><br />'SP*n*' = Service pack version<br /><br />'CTP*n*', = Community Technology Preview version<br /><br />Base data type: **nvarchar(128)** |
| `ProductMajorVersion` | **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through current version in updates beginning in late 2015.<br /><br />The major version.<br /><br />Base data type: **nvarchar(128)** |
| `ProductMinorVersion` | **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through current version in updates beginning in late 2015.<br /><br />The minor version.<br /><br />Base data type: **nvarchar(128)** |
| `ProductUpdateLevel` | **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through current version in updates beginning in late 2015.<br /><br />Update level of the current build. CU indicates a cumulative update.<br /><br />Returns one of the following values:<br /><br />CU*n* = Cumulative Update<br /><br />NULL = Not applicable.<br /><br />Base data type: **nvarchar(128)** |
| `ProductUpdateReference` | **Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through current version in updates beginning in late 2015.<br /><br />KB article for that release.<br /><br />Base data type: **nvarchar(128)** |
| `ProductVersion` | Version of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], in the form of *major.minor.build.revision*.<br /><br />Base data type: **nvarchar(128)** |
| `ResourceLastUpdateDateTime` | Returns the date and time that the Resource database was last updated.<br /><br />Base data type: **datetime** |
| `ResourceVersion` | Returns the version Resource database.<br /><br />Base data type: **nvarchar(128)** |
| `ServerName` | Both the Windows server and instance information associated with a specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br />NULL = Input isn't valid, or an error.<br /><br />Base data type: **nvarchar(128)** |
| `SqlCharSet` | The SQL character set ID from the collation ID.<br /><br />Base data type: **tinyint** |
| `SqlCharSetName` | The SQL character set name from the collation.<br /><br />Base data type: **nvarchar(128)** |
| `SqlSortOrder` | The SQL sort order ID from the collation<br /><br />Base data type: **tinyint** |
| `SqlSortOrderName` | The SQL sort order name from the collation.<br /><br />Base data type: **nvarchar(128)** |
| `SuspendedDatabaseCount` | The number of suspended databases on the server.<br /><br />Base data type: **int** |


## Return types

**sql_variant**

## Remarks

### ServerName property

The `ServerName` property of the `SERVERPROPERTY` function and [@@SERVERNAME](../../t-sql/functions/servername-transact-sql.md) return similar information. The `ServerName` property provides the Windows server and instance name that together make up the unique server instance. [@@SERVERNAME](../../t-sql/functions/servername-transact-sql.md) provides the currently configured local server name.

The `ServerName` property and [@@SERVERNAME](../../t-sql/functions/servername-transact-sql.md) return the same information if the default server name, at the time of installation, hasn't been changed. The local server name can be configured by executing the following:

```sql
EXEC sp_dropserver 'current_server_name';
GO
EXEC sp_addserver 'new_server_name', 'local';
GO
```

If the local server name has been changed from the default server name at installation time, [@@SERVERNAME](../../t-sql/functions/servername-transact-sql.md) returns the new name.

The `ServerName` property of the `SERVERPROPERTY` function returns the Windows server name as it is saved. In previous major versions it returned uppercase. This behavior changed back to uppercase between [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU9 and CU12, but starting from [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] CU13 the server name returns as it is saved.

If the Windows server name contains any lowercase characters, this change of behavior may cause differences between the `ServerName` property of the `SERVERPROPERTY` function, and [@@SERVERNAME](../../t-sql/functions/servername-transact-sql.md) (uppercase versus lowercase), even if there is no name change for the server.

Consider you have a server named as `server01`, with a SQL Server instance named `INST1`. The following table summarizes the change of behavior between different builds of SQL Server 2019:

| [!INCLUDE [sssql19-md](../../includes/sssql19-md.md)] release | SERVERPROPERTY('ServerName') | Additional information |
| -------------- | -------------- | -------------- |
| RTM | `SERVER01\INST1` | Returns the `ServerName` property in uppercase |
| CU1 – CU8 | `server01\INST1` | Returns the `ServerName` property as is, without changing to uppercase |
| CU9 – CU12 | `SERVER01\INST1` | Returns the `ServerName` property in uppercase |
| CU 13 and later | `server01\INST1` | Returns the `ServerName` property as-is, without changing to uppercase |

### Version properties

The `SERVERPROPERTY` function returns individual properties that relate to the version information whereas the [@@VERSION](../../t-sql/functions/version-transact-sql-configuration-functions.md) function combines the output into one string. If your application requires individual property strings, you can use the `SERVERPROPERTY` function to return them instead of parsing the [@@VERSION](../../t-sql/functions/version-transact-sql-configuration-functions.md) results.

## Permissions

All users can query the server properties.

## Examples

The following example uses the `SERVERPROPERTY` function in a `SELECT` statement to return information about the current instance of [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)].

```sql
SELECT
 SERVERPROPERTY('MachineName') AS ComputerName,
 SERVERPROPERTY('ServerName') AS InstanceName,
 SERVERPROPERTY('Edition') AS Edition,
 SERVERPROPERTY('ProductVersion') AS ProductVersion,
 SERVERPROPERTY('ProductLevel') AS ProductLevel;
GO
```

## See also

- [Editions and supported features of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md)
- [Editions and supported features of SQL Server 2017](../../sql-server/editions-and-components-of-sql-server-2017.md)
- [Editions and supported features of SQL Server 2019](../../sql-server/editions-and-components-of-sql-server-2019.md)
