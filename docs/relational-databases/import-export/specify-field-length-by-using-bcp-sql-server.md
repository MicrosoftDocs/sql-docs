---
title: "Specify Field Length by Using bcp (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "native data format [SQL Server]"
  - "default field lengths"
  - "field length [SQL Server]"
  - "data formats [SQL Server], field length"
  - "bcp utility [SQL Server], field length"
ms.assetid: 240f33ca-ef4a-413a-a4de-831885cb505b
author: "douglaslMS"
ms.author: "douglasl"
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Specify Field Length by Using bcp (SQL Server)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  The field length indicates the maximum number of characters that are required to represent data in character format. The field length is already known if the data is stored in the native format; for example, the **int** data type takes 4 bytes. If you have indicated 0 for the prefix length, the **bcp** command prompts you for field length, the default field lengths, and the impact of field-length on data storage in data files that contain **char** data.  
  
## The bcp Prompt for Field Length  
 If an interactive **bcp** command contains the **in** or **out** option without either the format file switch (**-f**) or a data-format switch (**-n**, **-c**, **-w**, or **-N**), the command prompts for the field length of each data field, as follows:  
  
 `Enter length of field <field_name> [<default>]:`  
  
 For an example that shows this prompt in context, see [Specify Data Formats for Compatibility when Using bcp &#40;SQL Server&#41;](../../relational-databases/import-export/specify-data-formats-for-compatibility-when-using-bcp-sql-server.md).  
  
> [!NOTE]  
>  After you interactively specify all of the fields in a **bcp** command, the command prompts you save your responses for each field in a non-XML format file. For more information on non-XML format files, see [Non-XML Format Files &#40;SQL Server&#41;](../../relational-databases/import-export/non-xml-format-files-sql-server.md).  
  
 Whether a **bcp** command prompts for field length depends on several factors, as follows:  
  
-   When you copy data types that are not of fixed length and you specify a prefix length of 0, **bcp** prompts for a field length.  
  
-   When converting noncharacter data to character data, **bcp** suggests a default field length large enough to store the data.  
  
-   If the file storage type is noncharacter, the **bcp** command does not prompt for a field length. The data is stored in the [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] native data representation (native format).  
  
## Using Default Field Lengths  
 Generally, [!INCLUDE[msCoName](../../includes/msconame-md.md)] recommends that you accept the **bcp**-suggested default values for the field length. When a character mode data file is created, using the default field length ensures that data is not truncated and that numeric overflow errors do not occur.  
  
 If you specify a field length that is incorrect, problems can occur. For instance, if you copy numeric data and you specify a field length that is too short for the data, the **bcp** utility prints an overflow message and does not copy the data. Also, if you export **datetime** data and specify a field length of less than 26 bytes for the character string, the **bcp** utility truncates the data without an error message.  
  
> [!IMPORTANT]  
>  When the default size option is used, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] expects to read an entire string. In some situations, use of a default field length can lead to an "unexpected end of file" error. Typically, this error occurs with the **money** and **datetime** data types when only part of the expected field occurs in the data file; for example, when a **datetime** value of *mm*/*dd*/*yy* is specified without the time component and is, therefore, shorter than the expected 24 character length of a **datetime** value in **char** format. To avoid this type of error, use field terminators or fixed-length data fields, or change the default field length by specifying another value.  
  
### Default Field Lengths for Character File Storage  
 The following table lists the default field lengths for data to be stored as a character-file storage type. Nullable data is the same length as nonnull data.  
  
|Data type|Default length (characters)|  
|---------------|-----------------------------------|  
|**char**|Length defined for the column|  
|**varchar**|Length defined for the column|  
|**nchar**|Twice the length defined for the column|  
|**nvarchar**|Twice the length defined for the column|  
|**Text**|0|  
|**ntext**|0|  
|**bit**|1|  
|**binary**|Twice the length defined for the column + 1|  
|**varbinary**|Twice the length defined for the column + 1|  
|**image**|0|  
|**datetime**|24|  
|**smalldatetime**|24|  
|**float**|30|  
|**real**|30|  
|**int**|12|  
|**bigint**|19|  
|**smallint**|7|  
|**tinyint**|5|  
|**money**|30|  
|**smallmoney**|30|  
|**decimal**|41*|  
|**numeric**|41*|  
|**uniqueidentifier**|37|  
|**timestamp**|17|  
|**varchar(max)**|0|  
|**varbinary(max)**|0|  
|**nvarchar(max)**|0|  
|UDT|Length of the user-defined term (UDT) column|  
|XML|0|  
  
 \*For more information about the **decimal** and **numeric** data types, see [decimal and numeric &#40;Transact-SQL&#41;](../../t-sql/data-types/decimal-and-numeric-transact-sql.md).  
  
> [!NOTE]  
>  A column of type **tinyint** can have values from 0 through 255; the maximum number of characters that are needed to represent any number in that range is three (representing values 100 through 255).  
  
### Default Field Lengths for Native File Storage  
 The following table lists the default field lengths for data to be stored as native file storage type. Nullable data is the same length as nonnull data, and character data is always stored in character format.  
  
|Data type|Default length (characters)|  
|---------------|-----------------------------------|  
|**bit**|1|  
|**binary**|Length defined for the column|  
|**varbinary**|Length defined for the column|  
|**image**|0|  
|**datetime**|8|  
|**smalldatetime**|4|  
|**float**|8|  
|**real**|4|  
|**int**|4|  
|**bigint**|8|  
|**smallint**|2|  
|**tinyint**|1|  
|**money**|8|  
|**smallmoney**|4|  
|**decimal**|*|  
|**numeric**|*|  
|**uniqueidentifier**|16|  
|**timestamp**|8|  
  
 \*For more information about the **decimal** and **numeric** data types, see [decimal and numeric &#40;Transact-SQL&#41;](../../t-sql/data-types/decimal-and-numeric-transact-sql.md).  
  
 In all of the preceding cases, to create a data file for later reloading into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] that keeps the storage space to a minimum, use a length prefix with the default file storage type and the default field length.  
  
## See Also  
 [bcp Utility](../../tools/bcp-utility.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Specify Field and Row Terminators &#40;SQL Server&#41;](../../relational-databases/import-export/specify-field-and-row-terminators-sql-server.md)   
 [Specify Prefix Length in Data Files by Using bcp &#40;SQL Server&#41;](../../relational-databases/import-export/specify-prefix-length-in-data-files-by-using-bcp-sql-server.md)   
 [Specify File Storage Type by Using bcp &#40;SQL Server&#41;](../../relational-databases/import-export/specify-file-storage-type-by-using-bcp-sql-server.md)   
 [Keep Nulls or Use Default Values During Bulk Import &#40;SQL Server&#41;](../../relational-databases/import-export/keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)  
  
  
