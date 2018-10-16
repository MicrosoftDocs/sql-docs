---
title: "sp_tableoption (Transact-SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "09/11/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database"
ms.reviewer: ""
ms.technology: system-objects
ms.topic: "language-reference"
f1_keywords: 
  - "sp_tableoption_TSQL"
  - "sp_tableoption"
dev_langs: 
  - "TSQL"
helpviewer_keywords: 
  - "sp_tableoption"
ms.assetid: 0a57462c-1057-4c7d-bce3-852cc898341d
author: stevestein
ms.author: sstein
manager: craigg
monikerRange: "=azuresqldb-current||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# sp_tableoption (Transact-SQL)
[!INCLUDE[tsql-appliesto-ss2008-asdb-xxxx-xxx-md](../../includes/tsql-appliesto-ss2008-asdb-xxxx-xxx-md.md)]

  Sets option values for user-defined tables. sp_tableoption can be used to control the in-row behavior of tables with **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml**, **text**, **ntext**, **image**, or large user-defined type columns.  
  
> [!IMPORTANT]  
>  The text in row feature will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. To store large value data, we recommend that you use of the **varchar(max)**, **nvarchar(max)** and **varbinary(max)** data types.  
  

 ![Topic link icon](../../database-engine/configure-windows/media/topic-link.gif "Topic link icon") [Transact-SQL Syntax Conventions](../../t-sql/language-elements/transact-sql-syntax-conventions-transact-sql.md)  
  
## Syntax  
  
```  
  
sp_tableoption [ @TableNamePattern = ] 'table'   
     , [ @OptionName = ] 'option_name'   
     ,[ @OptionValue =] 'value'  
```  
  
## Arguments  
 [ @TableNamePattern =] '*table*'  
 Is the qualified or nonqualified name of a user-defined database table. If a fully qualified table name, including a database name, is provided, the database name must be the name of the current database. Table options for multiple tables can not be set at the same time. *table* is **nvarchar(776)**, with no default.  
  
 [ @OptionName = ] '*option_name*'  
 Is a table option name. *option_name* is **varchar(35)**, with no default of NULL. *option_name* can be one of the following values.  
  
|Value|Description|  
|-----------|-----------------|  
|table lock on bulk load|When disabled (the default), it causes the bulk load process on user-defined tables to obtain row locks. When enabled, it causes the bulk load processes on user-defined tables to obtain a bulk update lock.|  
|insert row lock|No longer supported.<br /><br /> This option has no effect on the locking behavior of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] and is included only for compatibility of existing scripts and procedures.|  
|text in row|When OFF or 0 (disabled, the default), it does not change current behavior, and there is no BLOB in row.<br /><br /> When specified and @OptionValue is ON (enabled) or an integer value from 24 through 7000, new **text**, **ntext**, or **image** strings are stored directly in the data row. All existing BLOB (binary large object: **text**, **ntext**, or **image** data) will be changed to text in row format when the BLOB value is updated. For more information, see Remarks.|  
|large value types out of row|1 = **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml** and large user-defined type (UDT) columns in the table are stored out of row, with a 16-byte pointer to the root.<br /><br /> 0 = **varchar(max)**, **nvarchar(max)**, **varbinary(max)**, **xml** and large UDT values are stored directly in the data row, up to a limit of 8000 bytes and as long as the value can fit in the record. If the value does not fit in the record, a pointer is stored in-row and the rest is stored out of row in the LOB storage space. 0 is the default value.<br /><br /> Large user-defined type (UDT) applies to: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)]. <br /><br /> Use the TEXTIMAGE_ON option of [CREATE TABLE](../../t-sql/statements/create-table-transact-sql.md) to specify a location for storage of large data types. |  
|vardecimal storage format|**Applies to**: [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] through [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)].<br /><br /> When TRUE, ON, or 1, the designated table is enabled for vardecimal storage format. When FALSE, OFF, or 0, the table is not enabled for vardecimal storage format. Vardecimal storage format can be enabled only when the database has been enabled for vardecimal storage format by using [sp_db_vardecimal_storage_format](../../relational-databases/system-stored-procedures/sp-db-vardecimal-storage-format-transact-sql.md). In [!INCLUDE[ssKatmai](../../includes/sskatmai-md.md)] and later, **vardecimal** storage format is deprecated. Use ROW compression instead. For more information, see [Data Compression](../../relational-databases/data-compression/data-compression.md). 0 is the default value.|  
  
 [ @OptionValue =] '*value*'  
 Is whether the *option_name* is enabled (TRUE, ON, or 1) or disabled (FALSE, OFF, or 0). *value* is **varchar(12)**, with no default. *value* is case insensitive.  
  
 For the text in row option, valid option values are 0, ON, OFF, or an integer from 24 through 7000. When *value* is ON, the limit defaults to 256 bytes.  
  
## Return Code Values  
 0 (success) or error number (failure)  
  
## Remarks  
 sp_tableoption can be used only to set option values for user-defined tables. To display table properties, use OBJECTPROPERTY.  
  
 The text in row option in sp_tableoption can be enabled or disabled only on tables that contain text columns. If the table does not have a text column, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] raises an error.  
  
 When the text in row option is enabled, the @OptionValue parameter allows users to specify the maximum size to be stored in a row for a BLOB. The default is 256 bytes, but values can range from 24 through 7000 bytes.  
  
 **text**, **ntext**, or **image** strings are stored in the data row if the following conditions apply:  
  
-   text in row is enabled.  
  
-   The length of the string is shorter than the limit specified in @OptionValue  
  
-   There is enough space available in the data row.  
  
 When BLOB strings are stored in the data row, reading and writing the **text**, **ntext**, or **image** strings can be as fast as reading or writing character and binary strings. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] does not have to access separate pages to read or write the BLOB string.  
  
 If a **text**, **ntext**, or **image** string is larger than the specified limit or the available space in the row, pointers are stored in the row instead. The conditions for storing the BLOB strings in the row nonetheless apply: There must be enough space in the data row to hold the pointers.  
  
 BLOB strings and pointers stored in the row of a table are treated similarly to variable-length strings. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses only the number of bytes required to store the string or the pointer.  
  
 Existing BLOB strings are not converted immediately when text in row is first enabled. The strings are converted only when they are updated. Likewise, when the text in row option limit is increased, the **text**, **ntext**, or **image** strings already in the data row will not be converted to adhere to the new limit until the time they are updated.  
  
> [!NOTE]  
>  Disabling the text in row option or reducing the limit of the option will require the conversion of all BLOBs; therefore, the process can be long, depending on the number of BLOB strings that must be converted. The table is locked during the conversion process.  
  
 A table variable, including a function that returns a table variable, automatically has the text in row option enabled with a default inline limit of 256. This option cannot be changed.  
  
 The text in row option supports the TEXTPTR, WRITETEXT, UPDATETEXT, and READTEXT functions. Users can read parts of a BLOB with the SUBSTRING() function, but must remember that in-row text pointers have different duration and number limits from other text pointers.  
  
 To change a table from vardecimal storage format back to the normal decimal storage format, the database must be in SIMPLE recovery mode. Changing the recovery mode will break the log chain for backup purposes, therefore you should create a full database backup after removing the vardecimal storage format from a table.  
  
 If you are converting an existing LOB data type column (text, ntext, or image) to small-to-medium large value types (varchar(max), nvarchar(max), or varbinary(max)) , and most statements do not reference the large value type columns in your environment, consider changing **large_value_types_out_of_row** to **1** to gain optimal performance. When the **large_value_types_out_of_row** option value is changed, existing varchar(max), nvarchar(max), varbinary(max), and xml values are not immediately converted. The storage of the strings is changed as they are subsequently updated. Any new values inserted into a table are stored according to the table option in effect. For immediate results, either make a copy of the data and then repopulate the table after changing the **large_value_types_out_of_row** setting or update each small-to-medium large value types column to itself so that the storage of the strings is changed with the table option in effect. Consider rebuilding the indexes on the table after the update or repopulation to condense the table. 
    
  
## Permissions  
 To execute sp_tableoption requires ALTER permission on the table.  
  
## Examples  
  
### A. Storing xml data out of the row  
 The following example specifies that the **xml** data in the `HumanResources.JobCandidate` table be stored out of row.  
  
```sql  
USE AdventureWorks2012;  
GO  
EXEC sp_tableoption 'HumanResources.JobCandidate', 'large value types out of row', 1;  
```  
  
### B. Enabling vardecimal storage format on a table  
 The following example modifies the `Production.WorkOrderRouting` table to store the `decimal` data type in the `vardecimal` storage format.  

```sql  
USE master;  
GO  
-- The database must be enabled for vardecimal storage format  
-- before a table can be enabled for vardecimal storage format  
EXEC sp_db_vardecimal_storage_format 'AdventureWorks2012', 'ON';  
GO  
USE AdventureWorks2012;  
GO  
EXEC sp_tableoption 'Production.WorkOrderRouting',   
   'vardecimal storage format', 'ON';  
```  
  
## See Also  
 [sys.tables &#40;Transact-SQL&#41;](../../relational-databases/system-catalog-views/sys-tables-transact-sql.md)   
 [OBJECTPROPERTY &#40;Transact-SQL&#41;](../../t-sql/functions/objectproperty-transact-sql.md)   
 [System Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/system-stored-procedures-transact-sql.md)   
 [Database Engine Stored Procedures &#40;Transact-SQL&#41;](../../relational-databases/system-stored-procedures/database-engine-stored-procedures-transact-sql.md)  
  
  
