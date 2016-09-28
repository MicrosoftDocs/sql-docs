---
title: "DATABASEPROPERTYEX (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: c717487d-62c1-486a-92d9-90a1fe2b4b8a
caps.latest.revision: 9
author: BarbKess
---
# DATABASEPROPERTYEX (SQL Server PDW)
Returns the current setting of the specified database option or property for the specified database. Not all SQL Server properties are supported by SQL Server PDW. For more information, see the [DATABASEPROPERTYEX (Transact-SQL)](http://msdn.microsoft.com/en-us/library/ms186823.aspx) documentation on MSDN.  
  
![Topic link icon](../sqlpdw/media/Topic_Link.gif "Topic_Link")[Syntax Conventions &#40;SQL Server PDW&#41;](../sqlpdw/syntax-conventions-sql-server-pdw.md)  
  
## Syntax  
  
```  
DATABASEPROPERTYEX ( database , property )  
```  
  
## Arguments  
*database*  
The name of the database for which to return the named property information. *database* is **nvarchar(128)**.  
  
*property*  
An expression that represents the name of the database property to return. *property* is **varchar(128)**, and can be one of the following values. The return type is **sql_variant**. The following table shows the base data type for each property value. *property* is not case sensitive.  
  
> [!NOTE]  
> If the database is not started, properties that SQL Server PDW retrieves by accessing the database directly instead of retrieving the value from metadata will return NULL.  
  
|Property|Description|Value returned|  
|------------|---------------|------------------|  
|Collation|Default collation name for the database.|Collation name<br /><br />NULL = Database is not started.<br /><br />Base data type: **nvarchar(128)**|  
|IsQuotedIdentifiersEnabled|Double quotation marks can be used on identifiers. SQL Server PDW returns the value of the control node.|1 = TRUE<br /><br />0 = FALSE<br /><br />NULL = Input not valid<br /><br />Base data type: **int**|  
|IsTornPageDetectionEnabled|The SQL Server Database Engine detects incomplete I/O operations caused by power failures or other system outages. SQL Server PDW returns TRUE.|1 = TRUE<br /><br />0 = FALSE<br /><br />NULL = Input not valid<br /><br />Base data type: **int**|  
|SQLSortOrder|SQL Server sort order ID supported in earlier versions of SQL Server. SQL Server PDW returns 0.|0 = Database is using Windows collation<br /><br />>0 = SQL Server sort order ID<br /><br />NULL = Input not valid or database is not started<br /><br />Base data type: **tinyint**|  
|Status|Database status.|ONLINE = Database is available for query.<br /><br />OFFLINE = Database was explicitly taken offline.<br /><br />RESTORING = Database is being restored.<br /><br />RECOVERING = Database is recovering and not yet ready for queries.<br /><br />SUSPECT = Database did not recover.<br /><br />EMERGENCY = Database is in an emergency, read-only state. Access is restricted to sysadmin members<br /><br />Base data type: **nvarchar(128)**|  
|Updateability|Indicates whether data can be modified. SQL Server PDW returns READ_WRITE.|READ_ONLY = Data can be read but not modified.<br /><br />READ_WRITE = Data can be read and modified.<br /><br />Base data type: **nvarchar(128)**|  
|UserAccess|Indicates which users can access the database. SQL Server PDW returns MULTI_USER>|SINGLE_USER = Only one db_owner, dbcreator, or sysadmin user at a time<br /><br />RESTRICTED_USER = Only members of db_owner, dbcreator, and sysadmin roles<br /><br />MULTI_USER = All users<br /><br />Base data type: **nvarchar(128)**|  
|Version|Internal version number of the SQL Server code with which the database was created. Identified for informational purposes only. Not supported. Future compatibility is not guaranteed.SQL Server PDW returns the value of the control node.|Version number = Database is open.<br /><br />NULL = Database is not started.<br /><br />Base data type: **int**|  
  
## Return Types  
**sql_variant**  
  
## Exceptions  
Returns NULL on error or if a caller does not have permission to view the object.  
  
In SQL Server, a user can only view the metadata of securables that the user owns or on which the user has been granted permission. This means that metadata-emitting, built-in functions such as OBJECT_ID may return NULL if the user does not have any permission on the object.  
  
## Remarks  
DATABASEPROPERTYEX returns only one property setting at a time. To display multiple property settings, use the [sys.databases](../Topic/sys.databases%20(Transact-SQL).md) catalog view.  
  
## Examples  
  
```  
SELECT DATABASEPROPERTYEX('AdventureWorksPDW2012', 'Collation')  
```  
  
