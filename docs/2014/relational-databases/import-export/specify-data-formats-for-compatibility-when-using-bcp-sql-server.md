---
title: "Specify Data Formats for Compatibility when Using bcp (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "bulk exporting [SQL Server], compatibility"
  - "bulk importing [SQL Server], compatibility"
  - "compatibility [SQL Server], data formats"
  - "data formats [SQL Server], compatibility"
  - "bcp utility [SQL Server], compatibility"
ms.assetid: cd5fc8c8-eab1-4165-9468-384f31e53f0a
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Specify Data Formats for Compatibility when Using bcp (SQL Server)
  This topic describes the data-format attributes, field-specific prompts, and storing field-by-field data in a non-xml format file of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]`bcp` command. Understanding these can be helpful when you bulk export [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data for bulk import into another program, such as another database program. The default data formats (native, character, or Unicode) in the source table might be incompatible with the data layout expected by the other program If an incompatibility exists, when you export the data, you must describe the data layout.  
  
> [!NOTE]  
>  If you are unfamiliar with data formats for importing or exporting data, see [Data Formats for Bulk Import or Bulk Export &#40;SQL Server&#41;](data-formats-for-bulk-import-or-bulk-export-sql-server.md).  
  
 **In This Topic:**  
  
-   [bcp Data-Format Attributes](#bcpDataFormatAttr)  
  
-   [Overview of the Field-Specific Prompts](#FieldSpecificPrompts)  
  
-   [Storing Field-by-Field Data in a Non-XML Format File](#FieldByFieldNonXmlFF)  
  
-   [Related Tasks](#RelatedTasks)  
  
##  <a name="bcpDataFormatAttr"></a> bcp Data-Format Attributes  
 The `bcp` command allows you to specify the structure of each field in a data file in terms of the following data-format attributes:  
  
-   File storage type  
  
     The *file storage type* describes how data is stored in the data file. Data can be exported to a data file as its database table type (native format), in its character representation (character format), or as any data type where implicit conversion is supported; for example, copying a `smallint` as an `int`. User-defined data types are exported as their base types. For more information, see [Specify File Storage Type by Using bcp &#40;SQL Server&#41;](specify-file-storage-type-by-using-bcp-sql-server.md).  
  
-   Prefix length  
  
     To provide the most compact file storage for the bulk export of data in native format to a data file, the `bcp` command precedes each field with one or more characters that indicates the length of the field. These characters are called *length prefix characters*. For more information, see [Specify Prefix Length in Data Files by Using bcp &#40;SQL Server&#41;](specify-prefix-length-in-data-files-by-using-bcp-sql-server.md).  
  
-   Field length  
  
     The field length indicates the maximum number of characters that are required to represent data in character format. The field length is already known if the data is stored in the native format. For more information, see [Specify Field Length by Using bcp &#40;SQL Server&#41;](specify-field-length-by-using-bcp-sql-server.md).  
  
-   Field terminator  
  
     For character data fields, optional terminating characters allow you to mark the end of each field in a data file (using a *field terminator*) and the end of each row (using a *row terminator*). Terminating characters are one way to indicate to programs reading the data file where one field or row ends and another begins. For more information, see [Specify Field and Row Terminators &#40;SQL Server&#41;](specify-field-and-row-terminators-sql-server.md).  
  
##  <a name="FieldSpecificPrompts"></a> Overview of the Field-Specific Prompts  
 If an interactive `bcp` command contains the **in** or **out** option but does not also contain either the format file switch (**-f**) or a data-format switch (**-n**, **-c**, **-w**, or **-N**),  each column in the source or target table, the command prompts for each of the preceding attributes, in turn. In each prompt, the `bcp` command provides a default value based on the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data type of the table column. Accepting the default value for all of the prompts produces the same result as specifying native format (**-n**) on the command line. Each prompt displays a default value in brackets: [*default*]. Pressing ENTER accepts the displayed default. To specify a value other than the default, enter the new value at the prompt.  
  
### Example  
 The following example uses the `bcp` command to bulk export data from the `HumanResources.myTeam` table interactively to the `myTeam.txt` file. Before you can run the example, you must create this table. For information about the table and how to create it, see [HumanResources.myTeam Sample Table &#40;SQL Server&#41;](humanresources-myteam-sample-table-sql-server.md).  
  
 The command specifies neither a format file nor a data type, causing `bcp` to prompt for data-format information. At the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows command prompt, enter:  
  
```  
bcp AdventureWorks.HumanResources.myTeam out myTeam.txt -T  
```  
  
 For each column, bcp prompts for field-specific values. The following example shows the field-specific prompts for the `EmployeeID` and `Name` columns of the table, and suggests the default file storage type (the native format) for each column. The prefix lengths of the `EmployeeID` and `Name` column are 0 and 2, respectively. The user specifies a comma (`,`) as the terminator of each field.  
  
 `Enter the file storage type of field EmployeeID [smallint]:`  
  
 `Enter prefix-length of field EmployeeID [0]:`  
  
 `Enter field terminator [none]:,`  
  
 `Enter the file storage type of field Name [nvarchar]:`  
  
 `Enter prefix length of field Name [2]:`  
  
 `Enter field terminator [none]:,`  
  
 `.`  
  
 `.`  
  
 `.`  
  
 Equivalent prompts (as needed) are displayed for each of the table columns in order.  
  
##  <a name="FieldByFieldNonXmlFF"></a> Storing Field-by-Field Data in a Non-XML Format File  
 After all of the table columns are specified, the `bcp` command prompts you to optionally generate a non-XML format file that stores the field-by-field information just supplied (see the preceding example). If you choose to generate a format file, you can whenever you export data out of that table or import like-structured data into [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)].  
  
> [!NOTE]  
>  You can use the format file to bulk import data from the data file into an instance of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] or to bulk export data from the table, without needing to respecify the format. For more information, see [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](format-files-for-importing-or-exporting-data-sql-server.md).  
  
 The following example creates a non-XML format file named `myFormatFile.fmt`:  
  
 `Do you want to save this format information in a file? [Y/n] y`  
  
 `Host filename: [bcp.fmt]myFormatFile.fmt`  
  
 The default name for the format file is bcp.fmt, but you can specify a different file name if you choose.  
  
> [!NOTE]  
>  For a data file that uses a single data format for its file-storage type, such as character or native format, you can quickly create a format file without exporting or importing data by using the **format** option. This approach has the advantages of being easy and of allowing you to create either an XML format file or a non-XML format file. For more information, see [Create a Format File &#40;SQL Server&#41;](create-a-format-file-sql-server.md).  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Specify File Storage Type by Using bcp &#40;SQL Server&#41;](specify-file-storage-type-by-using-bcp-sql-server.md)  
  
-   [Specify Prefix Length in Data Files by Using bcp &#40;SQL Server&#41;](specify-prefix-length-in-data-files-by-using-bcp-sql-server.md)  
  
-   [Specify Field Length by Using bcp &#40;SQL Server&#41;](specify-field-length-by-using-bcp-sql-server.md)  
  
-   [Specify Field and Row Terminators &#40;SQL Server&#41;](specify-field-and-row-terminators-sql-server.md)  
  
## Related Content  
 None.  
  
## See Also  
 [Bulk Import and Export of Data &#40;SQL Server&#41;](bulk-import-and-export-of-data-sql-server.md)   
 [Data Formats for Bulk Import or Bulk Export &#40;SQL Server&#41;](data-formats-for-bulk-import-or-bulk-export-sql-server.md)   
 [bcp Utility](../../tools/bcp-utility.md)   
 [Data Types &#40;Transact-SQL&#41;](/sql/t-sql/data-types/data-types-transact-sql)  
  
  
