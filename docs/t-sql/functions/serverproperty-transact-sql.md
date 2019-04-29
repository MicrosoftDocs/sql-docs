---
title: "SERVERPROPERTY (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/07/2018"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: t-sql
ms.topic: "language-reference"
f1_keywords: 
  - "SERVERPROPERTY_TSQL"
  - "SERVERPROPERTY"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "Availability Groups [SQL Server], monitoring"
  - "SERVERPROPERTY function"
  - "server instance property information [SQL Server]"
  - "IsHadrEnabled server property"
  - "instances of SQL Server, property information"
  - "server properties [SQL Server]"
ms.assetid: 11e166fa-3dd2-42d8-ac4b-04f18c612c4a
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# SERVERPROPERTY (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-all-md](../../includes/tsql-appliesto-ss2008-all-md.md)]

  Returns property information about the server instance.  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
SERVERPROPERTY ( 'propertyname' )  
```  
  
## Arguments  
 *propertyname*  
 Is an expression that contains the property information to be returned for the server. *propertyname* can be one of the following values.  
  
|Property|Values returned|  
|--------------|---------------------|  
|BuildClrVersion|Version of the [!INCLUDE[msCoName](../../includes/msconame-md.md)][!INCLUDE[dnprdnshort](../../includes/dnprdnshort-md.md)] common language runtime (CLR) that was used while building the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> NULL = Input is not valid, an error, or not applicable.<br /><br /> Base data type: **nvarchar(128)**|  
|Collation|Name of the default collation for the server.<br /><br /> NULL = Input is not valid, or an error.<br /><br /> Base data type: **nvarchar(128)**|  
|CollationID|ID of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] collation.<br /><br /> Base data type: **int**|  
|ComparisonStyle|Windows comparison style of the collation.<br /><br /> Base data type: **int**|  
|ComputerNamePhysicalNetBIOS|NetBIOS name of the local computer on which the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is currently running.<br /><br /> For a clustered instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] on a failover cluster, this value changes as the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] fails over to other nodes in the failover cluster.<br /><br /> On a stand-alone instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], this value remains constant and returns the same value as the MachineName property.<br /><br /> **Note:** If the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] is in a failover cluster and you want to obtain the name of the failover clustered instance, use the MachineName property.<br /><br /> NULL = Input is not valid, an error, or not applicable.<br /><br /> Base data type: **nvarchar(128)**|  
|Edition|Installed product edition of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use the value of this property to determine the features and the limits, such as [Compute Capacity Limits by Edition of SQL Server](../../sql-server/compute-capacity-limits-by-edition-of-sql-server.md). 64-bit versions of the [!INCLUDE[ssDE](../../includes/ssde-md.md)] append (64-bit) to the version.<br /><br /> Returns:<br /><br /> 'Enterprise Edition'<br /><br /> 'Enterprise Edition: Core-based Licensing'<br /><br /> 'Enterprise Evaluation Edition'<br /><br /> 'Business Intelligence Edition'<br /><br /> 'Developer Edition'<br /><br /> 'Express Edition'<br /><br /> 'Express Edition with Advanced Services'<br /><br /> 'Standard Edition'<br /><br /> 'Web Edition'<br /><br /> 'SQL Azure' indicates [!INCLUDE[ssSDS](../../includes/sssds-md.md)] or [!INCLUDE[ssDW](../../includes/ssdw-md.md)]<br /><br /> Base data type: **nvarchar(128)**|  
|EditionID|EditionID represents the installed product edition of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Use the value of this property to determine features and limits, such as [Compute Capacity Limits by Edition of SQL Server](../../sql-server/compute-capacity-limits-by-edition-of-sql-server.md).<br /><br /> 1804890536 = Enterprise<br /><br /> 1872460670 = Enterprise Edition: Core-based Licensing<br /><br /> 610778273= Enterprise Evaluation<br /><br /> 284895786 = Business Intelligence<br /><br /> -2117995310 = Developer<br /><br /> -1592396055 = Express<br /><br /> -133711905= Express with Advanced Services<br /><br /> -1534726760 = Standard<br /><br /> 1293598313 = Web<br /><br /> 1674378470 = SQL Database or SQL Data Warehouse<br /><br /> Base data type: **bigint**|  
|EngineEdition|[!INCLUDE[ssDE](../../includes/ssde-md.md)] edition of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] installed on the server.<br /><br /> 1 = Personal or Desktop Engine (Not available in [!INCLUDE[ssVersion2005](../../includes/ssversion2005-md.md)] and later versions.)<br /><br /> 2 = Standard (This is returned for Standard, Web, and Business Intelligence.)<br /><br /> 3 = Enterprise (This is returned for Evaluation, Developer, and both Enterprise editions.)<br /><br /> 4 = Express (This is returned for Express, Express with Tools and Express with Advanced Services)<br /><br /> 5 = [!INCLUDE[ssSDS](../../includes/sssds-md.md)]<br /><br /> 6 - [!INCLUDE[ssDW](../../includes/ssdw-md.md)]<br /><br /> 8 = managed instance<br /><br /> Base data type: **int**|  
|HadrManagerStatus|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Indicates whether the [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] manager has started.<br /><br /> 0 = Not started, pending communication.<br /><br /> 1 = Started and running.<br /><br /> 2 = Not started and failed.<br /><br /> NULL = Input is not valid, an error, or not applicable.|  
|InstanceDefaultDataPath|**Applies to**:  [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through current version in updates beginning in late 2015.<br /><br /> Name of the default path to the instance data files.|  
|InstanceDefaultLogPath|**Applies to**:  [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through current version in updates beginning in late 2015.<br /><br /> Name of the default path to the instance log files.|  
|InstanceName|Name of the instance to which the user is connected.<br /><br /> Returns NULL if the instance name is the default instance, if the input is not valid, or error.<br /><br /> NULL = Input is not valid, an error, or not applicable.<br /><br /> Base data type: **nvarchar(128)**|  
|IsAdvancedAnalyticsInstalled|Returns 1 if the Advanced Analytics feature was installed during setup; 0 if Advanced Analytics was not installed.|  
|IsClustered|Server instance is configured in a failover cluster.<br /><br /> 1 = Clustered.<br /><br /> 0 = Not Clustered.<br /><br /> NULL = Input is not valid, an error, or not applicable.<br /><br /> Base data type: **int**|  
|IsFullTextInstalled|The full-text and semantic indexing components are installed on the current instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> 1 = Full-text and semantic indexing components are installed.<br /><br /> 0 = Full-text and semantic indexing components are not installed.<br /><br /> NULL = Input is not valid, an error, or not applicable.<br /><br /> Base data type: **int**|  
|IsHadrEnabled|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] is enabled on this server instance.<br /><br /> 0 = The [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] feature is disabled.<br /><br /> 1 = The [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] feature is enabled.<br /><br /> NULL = Input is not valid, an error, or not applicable.<br /><br /> Base data type: **int**<br /><br /> For availability replicas to be created and run on an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], [!INCLUDE[ssHADR](../../includes/sshadr-md.md)] must be enabled on the server instance. For more information, see [Enable and Disable AlwaysOn Availability Groups (SQL Server)](../../database-engine/availability-groups/windows/enable-and-disable-always-on-availability-groups-sql-server.md).<br /><br /> **Note:** The IsHadrEnabled property pertains only to [!INCLUDE[ssHADR](../../includes/sshadr-md.md)]. Other high availability or disaster recovery features, such as database mirroring or log shipping, are unaffected by this server property.|  
|IsIntegratedSecurityOnly|Server is in integrated security mode.<br /><br /> 1 = Integrated security (Windows Authentication)<br /><br /> 0 = Not integrated security. (Both Windows Authentication and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Authentication.)<br /><br /> NULL = Input is not valid, an error, or not applicable.<br /><br /> Base data type: **int**|  
|IsLocalDB|**Applies to**: [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Server is an instance of [!INCLUDE[ssExpress](../../includes/ssexpress-md.md)] LocalDB.<br /><br /> NULL = Input is not valid, an error, or not applicable.|  
|IsPolyBaseInstalled|**Applies to**: [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> Returns whether the server instance has the PolyBase feature installed.<br /><br /> 0 = PolyBase is not installed.<br /><br /> 1 = PolyBase is installed.<br /><br /> Base data type: **int**|  
|IsSingleUser|Server is in single-user mode.<br /><br /> 1 = Single user.<br /><br /> 0 = Not single user<br /><br /> NULL = Input is not valid, an error, or not applicable.<br /><br /> Base data type: **int**|  
|IsXTPSupported|**Applies to**: SQL Server ([!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]), [!INCLUDE[ssSDS](../../includes/sssds-md.md)].<br /><br /> Server supports In-Memory OLTP.<br /><br /> 1= Server supports In-Memory OLTP.<br /><br /> 0= Server does not supports In-Memory OLTP.<br /><br /> NULL = Input is not valid, an error, or not applicable.<br /><br /> Base data type: **int**|  
|LCID|Windows locale identifier (LCID) of the collation.<br /><br /> Base data type: **int**|  
|LicenseType|Unused. License information is not preserved or maintained by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product. Always returns DISABLED.<br /><br /> Base data type: **nvarchar(128)**|  
|MachineName|Windows computer name on which the server instance is running.<br /><br /> For a clustered instance, an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] running on a virtual server on Microsoft Cluster Service, it returns the name of the virtual server.<br /><br /> NULL = Input is not valid, an error, or not applicable.<br /><br /> Base data type: **nvarchar(128)**|  
|NumLicenses|Unused. License information is not preserved or maintained by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] product. Always returns NULL.<br /><br /> Base data type: **int**|  
|ProcessID|Process ID of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] service. ProcessID is useful in identifying which Sqlservr.exe belongs to this instance.<br /><br /> NULL = Input is not valid, an error, or not applicable.<br /><br /> Base data type: **int**|  
|ProductBuild|**Applies to**:  [!INCLUDE[ssSQL14](../../includes/sssql14-md.md)] beginning October, 2015.<br /><br /> The build number.|  
|ProductBuildType|**Applies to**:  [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through current version in updates beginning in late 2015.<br /><br /> Type of build of the current build.<br /><br /> Returns one of the following:<br /><br /> OD = On Demand release a specific customer.<br /><br /> GDR  =  General Distribution Release released through windows update.<br /><br /> NULL<br />= Not applicable.|  
|ProductLevel|Level of the version of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> Returns one of the following:<br /><br /> 'RTM' = Original release version<br /><br /> 'SP*n*' = Service pack version<br /><br /> 'CTP*n*', = Community Technology Preview version<br /><br /> Base data type: **nvarchar(128)**|  
|ProductMajorVersion|**Applies to**:  [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through current version in updates beginning in late 2015.<br /><br /> The major version.|  
|ProductMinorVersion|**Applies to**:  [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through current version in updates beginning in late 2015.<br /><br /> The minor version.|  
|ProductUpdateLevel|**Applies to**:  [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through current version in updates beginning in late 2015.<br /><br /> Update level of the current build. CU indicates a cumulative update.<br /><br /> Returns one of the following:<br /><br /> CU*n* = Cumulative Update<br /><br /> NULL<br />= Not applicable.|  
|ProductUpdateReference|**Applies to**:  [!INCLUDE[ssSQL11](../../includes/sssql11-md.md)] through current version in updates beginning in late 2015.<br /><br /> KB article for that release.|  
|ProductVersion|Version of the instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)], in the form of '*major.minor.build.revision*'.<br /><br /> Base data type: **nvarchar(128)**|  
|ResourceLastUpdateDateTime|Returns the date and time that the Resource database was last updated.<br /><br /> Base data type: **datetime**|  
|ResourceVersion|Returns the version Resource database.<br /><br /> Base data type: **nvarchar(128)**|  
|ServerName|Both the Windows server and instance information associated with a specified instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].<br /><br /> NULL = Input is not valid, or an error.<br /><br /> Base data type: **nvarchar(128)**|  
|SqlCharSet|The SQL character set ID from the collation ID.<br /><br /> Base data type: **tinyint**|  
|SqlCharSetName|The SQL character set name from the collation.<br /><br /> Base data type: **nvarchar(128)**|  
|SqlSortOrder|The SQL sort order ID from the collation<br /><br /> Base data type: **tinyint**|  
|SqlSortOrderName|The SQL sort order name from the collation.<br /><br /> Base data type: **nvarchar(128)**|  
|FilestreamShareName|The name of the share used by FILESTREAM.<br /><br /> NULL = Input is not valid, an error, or not applicable.|  
|FilestreamConfiguredLevel|The configured level of FILESTREAM access. For more information, see [filestream access level](../../database-engine/configure-windows/filestream-access-level-server-configuration-option.md).|  
|FilestreamEffectiveLevel|The effective level of FILESTREAM access. This value can be different than the FilestreamConfiguredLevel if the level has changed and either an instance restart or a computer restart is pending. For more information, see [filestream access level](../../database-engine/configure-windows/filestream-access-level-server-configuration-option.md).|  
  
## Return Types  
 **sql_variant**  
  
## Remarks  
  
### ServerName Property  
 The `ServerName` property of the `SERVERPROPERTY` function and [@@SERVERNAME](../../t-sql/functions/servername-transact-sql.md) return similar information. The `ServerName` property provides the Windows server and instance name that together make up the unique server instance. [@@SERVERNAME](../../t-sql/functions/servername-transact-sql.md) provides the currently configured local server name.  
  
 The `ServerName` property and [@@SERVERNAME](../../t-sql/functions/servername-transact-sql.md) return the same information if the default server name at the time of installation has not been changed. The local server name can be configured by executing the following:  
  
```  
EXEC sp_dropserver 'current_server_name';  
GO  
EXEC sp_addserver 'new_server_name', 'local';  
GO  
```  
  
 If the local server name has been changed from the default server name at installation time, [@@SERVERNAME](../../t-sql/functions/servername-transact-sql.md) returns the new name.  
  
### Version Properties  
 The `SERVERPROPERTY` function returns individual properties that relate to the version information whereas the [@@VERSION](../../t-sql/functions/version-transact-sql-configuration-functions.md) function combines the output into one string. If your application requires individual property strings, you can use the `SERVERPROPERTY` function to return them instead of parsing the [@@VERSION](../../t-sql/functions/version-transact-sql-configuration-functions.md) results.  

> [!NOTE]  
> We are aware of an issue where the version properties reported by SERVERPROPERTY are incorrect for Azure SQL Database. 
> The version of the SQL Server database engine run by Azure SQL Database is always ahead of the on-premises version of SQL Server, and includes the latest security fixes. This means that the patch level is always on par with or ahead of the on-premises version of SQL Server, and that the latest features available in SQL Server are available in Azure SQL Database.
>
> To programmatically determine the engine edition, use SELECT SERVERPROPERTY('EngineEdition'). This query will return '5' for single databases/elastic pools and '8' for managed instances in Azure SQL Database. 
>
> We will update the documentation once this issue is resolved.

## Permissions

All users can query the server properties. 
  
## Examples  
 The following example uses the `SERVERPROPERTY` function in a `SELECT` statement to return information about the current instance of [!INCLUDE[ssNoVersion_md](../../includes/ssnoversion-md.md)].   
  
```  
SELECT  
  SERVERPROPERTY('MachineName') AS ComputerName,
  SERVERPROPERTY('ServerName') AS InstanceName,  
  SERVERPROPERTY('Edition') AS Edition,
  SERVERPROPERTY('ProductVersion') AS ProductVersion,  
  SERVERPROPERTY('ProductLevel') AS ProductLevel;  
GO  
```  
  
## See Also  
 [Editions and Components of SQL Server 2016](../../sql-server/editions-and-components-of-sql-server-2016.md)  
  
  
