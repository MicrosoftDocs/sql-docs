---
title: "SERVERPROPERTY (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 67fb5164-a164-4b3a-931a-2812ecc9e1e0
caps.latest.revision: 8
author: BarbKess
---
# SERVERPROPERTY (SQL Server PDW)
Returns property information about the server instance.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
SERVERPROPERTY ( propertyname )  
```  
  
## Arguments  
*propertyname*  
An expression that contains the property information to be returned for the server. *propertyname* can be one of the following values.  
  
|Property|Values returned|  
|------------|-------------------|  
|BuildClrVersion|Version of the Microsoft .NET Framework common language runtime (CLR) that was used while building the instance of SQL Server PDW.<br /><br />Base data type: **nvarchar(128)**|  
|Collation|Name of the default collation for the server.<br /><br />NULL = Input is not valid, or an error.<br /><br />Base data type: **nvarchar(128)**|  
|CollationID|ID of the SQL Server PDW collation.<br /><br />Base data type: **int**|  
|ComparisonStyle|Windows comparison style of the collation.<br /><br />Base data type: **int**|  
|ComputerNamePhysicalNetBIOS|NetBIOS name of the local computer on which the instance of SQL Server PDW is currently running.<br /><br />For a clustered instance of SQL Server PDW on a failover cluster, this value changes as the instance of SQL Server PDW fails over to other nodes in the failover cluster.<br /><br />On a stand-alone instance of SQL Server PDW, this value remains constant and returns the same value as the MachineName property.<br /><br />**Note:** If the instance of SQL Server PDW is in a failover cluster and you want to obtain the name of the failover clustered instance, use the MachineName property.<br /><br />NULL = Input is not valid, or an error.<br /><br />Base data type: **nvarchar(128)**|  
|Edition|Installed product edition of the instance of SQL Server PDW. Use the value of this property to determine the features and the limits, such as maximum number of CPUs that are supported by the installed product. 64-bit versions of the Database Engine append (64-bit) to the version.  SQL Server PDW returns **Enterprise Edition (64-bit)**.)<br /><br />Returns:<br /><br />'Desktop Engine'<br /><br />'Developer Edition'<br /><br />'Enterprise Edition'<br /><br />'Enterprise Evaluation Edition'<br /><br />'Personal Edition'(Not available for SQL Server PDW.)<br /><br />'Standard Edition'<br /><br />'Express Edition'<br /><br />'Express Edition with Advanced Services'<br /><br />'Workgroup Edition'<br /><br />'Windows Embedded SQL'<br /><br />Base data type: **nvarchar(128)**|  
|EditionID|Is an identification number that represents the installed product edition of the instance of SQL Server PDW. Use the value of this property to determine features and limits, such as maximum number of CPUs that are supported by the installed product. SQL Server PDW returns **1804890536**.)<br /><br />-1253826760 = Desktop<br /><br />-1592396055 = Express<br /><br />-1534726760 = Standard<br /><br />1333529388 = Workgroup<br /><br />1804890536 = Enterprise<br /><br />-323382091 = Personal<br /><br />-2117995310 = Developer<br /><br />610778273 = Enterprise Evaluation<br /><br />1044790755 = Windows Embedded SQL<br /><br />4161255391 = Express with Advanced Services<br /><br />Base data type: **int**|  
|EngineEdition|Database Engine edition of the instance of SQL Server PDW installed on the server. SQL Server PDW returns 6.<br /><br />1 = Personal or Desktop Engine (Not available for SQL Server PDW.)<br /><br />2 = Standard (This is returned for Standard and Workgroup.)<br /><br />3 = Enterprise (This is returned for Enterprise, Enterprise Evaluation, and Developer.)<br /><br />4 = Express (This is returned for Express, Express with Advanced Services, and Windows Embedded SQL.)<br /><br />6 = SQL Server PDW<br /><br />Base data type: **int**|  
|InstanceName|Name of the instance to which the user is connected.<br /><br />SQL Server PDW returns NULL.<br /><br />Base data type: **nvarchar(128)**|  
|IsClustered|Server instance is configured in a failover cluster. SQL Server PDW returns 1.<br /><br />1 = Clustered.<br /><br />0 = Not Clustered.<br /><br />NULL = Input is not valid, or an error.<br /><br />Base data type: **int**|  
|IsFullTextInstalled|The full-text component is installed with the current instance of SQL Server PDW.<br /><br />1 = Full-text is installed.<br /><br />0 = Full-text is not installed.<br /><br />NULL = Input is not valid, or an error.<br /><br />Base data type: **int**|  
|IsIntegratedSecurityOnly|Server is in integrated security mode. SQL Server PDW returns NULL.<br /><br />1 = Integrated security.<br /><br />0 = Not integrated security.<br /><br />NULL = Input is not valid, or an error.<br /><br />Base data type: **int**|  
|IsSingleUser|Server is in single-user mode. SQL Server PDW returns 0.<br /><br />1 = Single user.<br /><br />0 = Not single user<br /><br />NULL = Input is not valid, or an error.<br /><br />Base data type: **int**|  
|LCID|Windows locale identifier (LCID) of the collation.<br /><br />Base data type: **int**|  
|LicenseType|Mode of this instance of SQL Server PDW.<br /><br />PER_SEAT = Per Seat mode<br /><br />PER_PROCESSOR = Per-processor mode<br /><br />DISABLED = Licensing is disabled.<br /><br />Base data type: **nvarchar(128)**|  
|MachineName|Returns the appliance name, same as ServerName below.<br /><br />NULL = Input is not valid, or an error.<br /><br />Base data type: **nvarchar(128)**|  
|ProductVersion|Version of the instance of SQL Server PDW, in the form of '*major.minor.build*'.<br /><br />Base data type: **nvarchar(128)**|  
|ProductLevel|Level of the version of the instance of SQL Server PDW.<br /><br />Returns one of the following:<br /><br />'RTM' = Original release version<br /><br />'SP*n*' = Service pack version<br /><br />'CTP' = Community Technology Preview version<br /><br />'AUn' = Appliance Update<br /><br />Base data type: **nvarchar(128)**|  
|ResourceLastUpdateDateTime|Returns the date and time that the Resource database was last updated.<br /><br />Base data type: **datetime**|  
|ResourceVersion|Returns the version Resource database.<br /><br />Base data type: **nvarchar(128)**|  
|ServerName|Returns the appliance name.<br /><br />NULL = Input is not valid, or an error.<br /><br />Base data type: **nvarchar(128)**|  
|SqlCharSet|The SQL character set ID from the collation ID. Always the value of the control node.<br /><br />Base data type: **tinyint**|  
|SqlCharSetName|The SQL character set name from the collation. Always the value of the control node.<br /><br />Base data type: **nvarchar(128)**|  
|SqlSortOrder|The SQL sort order ID from the collation. Always the value of the control node.<br /><br />Base data type: **tinyint**|  
|SqlSortOrderName|The SQL sort order name from the collation. Always the value of the control node.<br /><br />Base data type: **nvarchar(128)**|  
  
## Return Types  
**sql_variant**  
  
## Remarks  
  
## ServerName Property  
The ServerName property provides the Windows server and instance name that together make up the unique server instance  
  
## Version Properties  
The SERVERPROPERTY function returns individual properties that relate to the version  
  
## Examples  
The following example uses the `SERVERPROPERTY` function in a `SELECT` statement to return information about the current server. This scenario is useful when there are multiple instances of SQL Server installed on a Windows server, and the client must open another connection to the same instance used by the current connection.  
  
```  
SELECT CONVERT(sysname, SERVERPROPERTY('servername'));  
```  
  
The following example uses the SERVERPROPERTY function in a SELECT statement to return version information about the product.  
  
```  
SELECT  
SERVERPROPERTY('ProductVersion') AS ProductVersion,  
SERVERPROPERTY('ProductLevel') AS ProductLevel,  
SERVERPROPERTY('Edition') AS Edition,  
SERVERPROPERTY('EngineEdition') AS EngineEdition;  
```  
  
