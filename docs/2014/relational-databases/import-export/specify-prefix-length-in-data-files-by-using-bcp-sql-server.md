---
title: "Specify Prefix Length in Data Files by Using bcp (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "bcp utility [SQL Server], prefix length"
  - "prefix length [SQL Server]"
  - "lengths [SQL Server], prefix characters"
  - "data formats [SQL Server], prefix length"
ms.assetid: ce32dd1a-26f1-4f61-b9fa-3f1feea9992e
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Specify Prefix Length in Data Files by Using bcp (SQL Server)
  To provide the most compact file storage for the bulk export of data in native format to a data file, the **bcp** command precedes each field with one or more characters that indicates the length of the field. These characters are called *length prefix characters*.  
  
## The bcp Prompt for Prefix Length  
 If an interactive **bcp** command contains the **in** or **out** option without either the format file switch (**-f**) or a data-format switch (**-n**, **-c**, **-w**, or **-N**), the command prompts for the prefix length of each data field, as follows:  
  
 `Enter prefix length of field <field_name> [<default>]:`  
  
 If you specify 0, **bcp** prompts you for either the length of the field (for a character data type) or a field terminator (for a native non-character type).  
  
> [!NOTE]  
>  After you interactively specify all of the fields in a **bcp** command, the command prompts you save your responses for each field in a non-XML format file. For more information about non-XML format files, see [Non-XML Format Files &#40;SQL Server&#41;](xml-format-files-sql-server.md).  
  
## Overview of Prefix Length  
 To store the prefix length of a field, you need enough bytes to represent the maximum length of the field. The number of bytes that are required also depends upon the file storage type, the nullability of a column, and whether the data is being stored in the data file in its native or character format. For example, a `text` or `image` data type requires four prefix characters to store the field length, but a `varchar` data type requires two characters. In the data file, these length-prefix characters are stored in the internal binary data format of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!IMPORTANT]  
>  When you use native format, use length prefixes rather than field terminators. Native format data might conflict with terminators because a native-format data file is stored in the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internal binary data format.  
  
##  <a name="PrefixLengthsExport"></a> Prefix Lengths for Bulk Export  
  
> [!NOTE]  
>  The default value that is provided at the prefix-length prompt when you export a field indicates the most efficient prefix length for the field.  
  
 Null values are represented as an empty field. To indicate that the field is empty (represents NULL), the field prefix contains the value -1; that is, it requires at least 1 byte. Note that if a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table column allows null values, the column requires a prefix length of 1 or greater, depending on the file storage type.  
  
 When you bulk export data and store it in either native data types or character format, use the prefix lengths shown in the following table.  
  
|SQL Server<br /><br /> data type|Native format<br /><br /> NOT NULL|Native format<br /><br /> NULL|Character format<br /><br /> NOT NULL|Character format<br /><br /> NULL|  
|------------------------------|--------------------------------|----------------------------|-----------------------------------|-------------------------------|  
|`char`|2|2|2|2|  
|`varchar`|2|2|2|2|  
|`nchar`|2|2|2|2|  
|`nvarchar`|2|2|2|2|  
|`text` <sup>1</sup>|4|4|4|4|  
|`ntext` <sup>1</sup>|4|4|4|4|  
|`binary`|2|2|2|2|  
|`varbinary`|2|2|2|2|  
|`image` <sup>1</sup>|4|4|4|4|  
|`datetime`|0|1|0|1|  
|`smalldatetime`|0|1|0|1|  
|`decimal`|1|1|1|1|  
|`numeric`|1|1|1|1|  
|`float`|0|1|0|1|  
|`real`|0|1|0|1|  
|`int`|0|1|0|1|  
|`bigint`|0|1|0|1|  
|`smallint`|0|1|0|1|  
|`tinyint`|0|1|0|1|  
|`money`|0|1|0|1|  
|`smallmoney`|0|1|0|1|  
|`bit`|0|1|0|1|  
|`uniqueidentifier`|1|1|0|1|  
|`timestamp`|1|1|1|1|  
|`varchar(max)`|8|8|8|8|  
|`varbinary(max)`|8|8|8|8|  
|UDT (a user-defined data type)|8|8|8|8|  
|XML|8|8|8|8|  
  
 <sup>1</sup> The `ntext`, `text`, and `image` data types will be removed in a future version of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. Avoid using these data types in new development work, and plan to modify applications that currently use them. Use `nvarchar(max)`, `varchar(max)`, and `varbinary(max)` instead.  
  
##  <a name="PrefixLengthsImport"></a> Prefix Lengths for Bulk Import  
 When data is bulk imported, the prefix length is the value that was specified when the data file was created originally. If the data file was not created by a **bcp** command, length prefix characters probably do not exist. In this instance, specify 0 for the prefix length.  
  
> [!NOTE]  
>  To specify a prefix length in a data file that was not created by using **bcp**, use the lengths provided in [Prefix Lengths for Bulk Export](#PrefixLengthsExport), earlier in this topic.  
  
## See Also  
 [bcp Utility](../../tools/bcp-utility.md)   
 [Data Types &#40;Transact-SQL&#41;](/sql/t-sql/data-types/data-types-transact-sql)   
 [Specify Field Length by Using bcp &#40;SQL Server&#41;](specify-field-length-by-using-bcp-sql-server.md)   
 [Specify Field and Row Terminators &#40;SQL Server&#41;](specify-field-and-row-terminators-sql-server.md)   
 [Specify File Storage Type by Using bcp &#40;SQL Server&#41;](specify-file-storage-type-by-using-bcp-sql-server.md)  
  
  
