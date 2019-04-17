---
title: "Specify File Storage Type by Using bcp (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "bcp utility [SQL Server], file storage types"
  - "importing data, file storage types"
  - "native data format [SQL Server]"
  - "file storage types [SQL Server]"
  - "data formats [SQL Server], file storage types"
ms.assetid: 85e12df8-1be7-4bdc-aea9-05aade085c06
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Specify File Storage Type by Using bcp (SQL Server)
  The *file storage type* describes how data is stored in the data file. Data can be exported to a data file as its database table type (native format), in its character representation (character format), or as any data type where implicit conversion is supported; for example, copying a `smallint` as an `int`. User-defined data types are exported as their base types.  
  
## The bcp Prompt for File Storage Type  
 If an interactive **bcp** command contains the **in** or **out** option without either the format file switch (**-f**) or a data-format switch (**-n**, **-c**, **-w**, or **-N**), the command prompts for the file storage type of each data field, as follows:  
  
 `Enter the file storage type of field <field_name> [<default>]:`  
  
 Your response to this prompt depends on the task you perform, as follows:  
  
-   To bulk export data from an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to a data file in the most compact storage possible (native data format), accept the default file storage types that are provided by **bcp**. For a list of the native file storage types, see "Native File Storage Types," later in this topic.  
  
-   To bulk export data from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to a data file in character format, specify `char` as the file storage type for all columns in the table.  
  
-   To bulk import data to an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from a data file, specify the file storage type as `char` for types stored in character format and, for data stored in native data type format, specify one of the file storage types, as appropriate:  
  
    |File storage type|Enter at command prompt|  
    |-----------------------|-----------------------------|  
    |`char` <sup>1</sup>|`c`[`har`]|  
    |`varchar`|`c[har]`|  
    |`nchar`|`w`|  
    |`nvarchar`|`w`|  
    |`text` <sup>2</sup>|`T`[`ext`]|  
    |`ntext2`|`W`|  
    |`binary`|`x`|  
    |`varbinary`|`x`|  
    |`image` <sup>2</sup>|`I`[`mage`]|  
    |`datetime`|**d[ate]**|  
    |`smalldatetime`|`D`|  
    |`time`|`te`|  
    |`date`|`de`|  
    |`datetime2`|`d2`|  
    |`datetimeoffset`|`do`|  
    |`decimal`|`n`|  
    |`numeric`|`n`|  
    |`float`|**f[loat]**|  
    |`real`|`r`|  
    |`Int`|**i[nt]**|  
    |`bigint`|`B[igint]`|  
    |`smallint`|**s[mallint]**|  
    |`tinyint`|**t[inyint]**|  
    |`money`|**m[oney]**|  
    |`smallmoney`|`M`|  
    |`bit`|`b[it]`|  
    |`uniqueidentifier`|`u`|  
    |`sql_variant`|`V[ariant]`|  
    |`timestamp`|`x`|  
    |`UDT` (a user-defined data type)|`U`|  
    |`XML`|`X`|  
  
     <sup>1</sup> The interaction of field length, prefix length, and terminators determines the amount of storage space that is allocated in a data file for noncharacter data that is exported as the `char` file storage type.  
  
     <sup>2</sup> The `ntext`, `text`, and `image` data types will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. In new development work, avoid using these data types, and plan to modify applications that currently use them. Use `nvarchar(max)`, `varchar(max)`, and `varbinary(max)` instead.  
  
## Native File Storage Types  
 Each native file storage type is recorded in the format file as a corresponding host file data type.  
  
|File storage type|Host file data type|  
|-----------------------|-------------------------|  
|`char` <sup>1</sup>|SQLCHAR|  
|`varchar`|SQLCHAR|  
|`nchar`|SQLNCHAR|  
|`nvarchar`|SQLNCHAR|  
|`text` <sup>2</sup>|SQLCHAR|  
|`ntext` <sup>2</sup>|SQLNCHAR|  
|`binary`|SQLBINARY|  
|`varbinary`|SQLBINARY|  
|`image` <sup>2</sup>|SQLBINARY|  
|`datetime`|SQLDATETIME|  
|`smalldatetime`|SQLDATETIM4|  
|`decimal`|SQLDECIMAL|  
|`numeric`|SQLNUMERIC|  
|`float`|SQLFLT8|  
|`real`|SQLFLT4|  
|`int`|SQLINT|  
|`bigint`|SQLBIGINT|  
|`smallint`|SQLSMALLINT|  
|`tinyint`|SQLTINYINT|  
|`money`|SQLMONEY|  
|`smallmoney`|SQLMONEY4|  
|`bit`|SQLBIT|  
|`uniqueidentifier`|SQLUNIQUEID|  
|`sql_variant`|SQLVARIANT|  
|`timestamp`|SQLBINARY|  
|UDT (a user-defined data type)|SQLUDT|  
  
 <sup>1</sup> Data files that are stored in character format use `char` as the file storage type. Therefore, for character data files, SQLCHAR is the only data type that appears in a format file.  
  
 <sup>2</sup> You cannot bulk import data into `text`, `ntext`, and `image` columns that have DEFAULT values.  
  
## Additional Considerations for File Storage Types  
 When you bulk export data from an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to a data file:  
  
-   You can always specify `char` as the file storage type.  
  
-   If you enter a file storage type that represents an invalid implicit conversion, **bcp** fails; for example, though you can specify `int` for `smallint` data, if you specify `smallint` for `int` data, overflow errors result.  
  
-   When noncharacter data types such as `float`, `money`, `datetime`, or `int` are stored as their database types, the data is written to the data file in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] native format.  
  
    > [!NOTE]  
    >  After you interactively specify all of the fields in a **bcp** command, the command prompts you save your responses for each field in a non-XML format file. For more information on non-XML format files, see [Non-XML Format Files &#40;SQL Server&#41;](xml-format-files-sql-server.md).  
  
## See Also  
 [bcp Utility](../../tools/bcp-utility.md)   
 [Data Types &#40;Transact-SQL&#41;](/sql/t-sql/data-types/data-types-transact-sql)   
 [Specify Field Length by Using bcp &#40;SQL Server&#41;](specify-field-length-by-using-bcp-sql-server.md)   
 [Specify Field and Row Terminators &#40;SQL Server&#41;](specify-field-and-row-terminators-sql-server.md)   
 [Specify Prefix Length in Data Files by Using bcp &#40;SQL Server&#41;](specify-prefix-length-in-data-files-by-using-bcp-sql-server.md)  
  
  
