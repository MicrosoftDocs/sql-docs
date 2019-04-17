---
title: "Keep Identity Values When Bulk Importing Data (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "identity values [SQL Server], bulk imports"
  - "data formats [SQL Server], identity values"
  - "bulk importing [SQL Server], identity values"
ms.assetid: 45894a3f-2d8a-4edd-9568-afa7d0d3061f
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Keep Identity Values When Bulk Importing Data (SQL Server)
  Data files that contain identity values can be bulk imported into an instance of [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)]. By default, the values for the identity column in the data file that is imported are ignored and [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] assigns unique values automatically. The unique values are based on the seed and increment values that are specified during table creation.  
  
 If the data file does not contain values for the identifier column in the table, use a format file to specify that the identifier column in the table should be skipped when importing data. [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] assigns unique values for the column automatically.  
  
 To prevent [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] from assigning identity values while bulk importing data rows into a table, use the appropriate keep-identity command qualifier. When you specify a keep-identity qualifier, [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] uses the identity values in the data file. These qualifiers are as follows:  
  
|Command|Keep-identity qualifier|Qualifier type|  
|-------------|------------------------------|--------------------|  
|`bcp`|**-E**|Switch|  
|BULK INSERT|KEEPIDENTITY|Argument|  
|INSERT ... SELECT * FROM OPENROWSET(BULK...)|KEEPIDENTITY|Table hint|  
  
 For more information, see [bcp Utility](../../tools/bcp-utility.md), [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql), [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql), [INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/insert-transact-sql), [SELECT &#40;Transact-SQL&#41;](/sql/t-sql/queries/select-transact-sql), and [Table Hints &#40;Transact-SQL&#41;](/sql/t-sql/queries/hints-transact-sql-table).  
  
> [!NOTE]  
>  To create an automatically incrementing number that can be used in multiple tables or that can be called from applications without referencing any table, see [Sequence Numbers](../sequence-numbers/sequence-numbers.md).  
  
## Examples  
 The examples in this topic bulk import data using INSERT ... SELECT * FROM OPENROWSET(BULK...) and keeping default values.  
  
### Sample Table  
 The bulk-import examples require that a table named **myTestKeepNulls** table be created in the **AdventureWorks** sample database under the **dbo** schema. To create this table. in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] Query Editor, execute:  
  
```  
USE AdventureWorks;  
GO  
SELECT * INTO HumanResources.myDepartment   
   FROM HumanResources.Department  
      WHERE 1=0;  
GO  
SELECT * FROM HumanResources.myDepartment;  
```  
  
 The **Department** table on which `myDepartment` is based has IDENTITY_INSERT is set to OFF. Therefore, to import data into an identity column you must specify KEEPIDENTITY or **-E**.  
  
### Sample Data File  
 The data file used in the bulk-import examples contains data bulk exported from the `HumanResources.Department` table in native format. To create the data file, at the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows command prompt, enter:  
  
```  
bcp AdventureWorks.HumanResources.Department out myDepartment-n.Dat -n -T  
```  
  
### Sample Format File  
 This bulk-import examples use an XML format file, `myDepartment-f-x-n.Xml`, which uses native data formats. This example uses `bcp` to create to generate this format file from the `HumanResources.Department` table of the `AdventureWorks` database. At the Windows command prompt, enter:  
  
```  
bcp AdventureWorks.HumanResources.Department format nul -n -x -f myDepartment-f-n-x.Xml -T  
```  
  
 For more information about creating a format file, see [Create a Format File &#40;SQL Server&#41;](create-a-format-file-sql-server.md).  
  
### A. Using bcp and Keeping Identity Values  
 The following example demonstrates how to keep identity values when using `bcp` to bulk import data. The `bcp` command uses the format file, `myDepartment-f-n-x.Xml`, and contains the following switches:  
  
|Qualifiers|Description|  
|----------------|-----------------|  
|**-E**|Specifies that identity value or values in the data file are to be used for the identity column.|  
|**-T**|Specifies that the `bcp` utility connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with a trusted connection.|  
  
 At the Windows command prompt, enter.  
  
```  
bcp AdventureWorks.HumanResources.myDepartment in C:\myDepartment-n.Dat -f C:\myDepartment-f-n-x.Xml -E -T  
  
```  
  
### B. Using BULK INSERT and Keeping Identity Values  
 The following example uses BULK INSERT to bulk import data from the `myDepartment-c.Dat` file into the `AdventureWorks.HumanResources.myDepartment` table. The statement uses the `myDepartment-f-n-x.Xml` format file and includes the KEEPIDENTITY option to ensure that any identity values in the data file are retained.  
  
 In the [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] Query Editor, execute:  
  
```  
USE AdventureWorks;  
GO  
DELETE HumanResources.myDepartment;  
GO  
BULK INSERT HumanResources.myDepartment  
   FROM 'C:\myDepartment-n.Dat'  
   WITH (  
      KEEPIDENTITY,  
      FORMATFILE='C:\myDepartment-f-n-x.Xml'  
   );  
GO  
SELECT * FROM HumanResources.myDepartment;  
  
```  
  
### C. Using OPENROWSET and Keeping Identity Values  
 The following example uses the OPENROWSET bulk rowset provider to bulk import data from the `myDepartment-c.Dat` file into the `AdventureWorks.HumanResources.myDepartment` table. The statement uses the `myDepartment-f-n-x.Xml` format file and includes the KEEPIDENTITY hint to ensure that any identity values in the data file are retained.  
  
 In the [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] Query Editor, execute:  
  
```  
USE AdventureWorks;  
GO  
DELETE HumanResources.myDepartment;  
GO  
  
INSERT INTO HumanResources.myDepartment  
   with (KEEPIDENTITY)  
   (DepartmentID, Name, GroupName, ModifiedDate)  
   SELECT *  
      FROM  OPENROWSET(BULK 'C:\myDepartment-n.Dat',  
      FORMATFILE='C:\myDepartment-f-n-x.Xml') as t1;  
GO  
  
```  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Keep Nulls or Use Default Values During Bulk Import &#40;SQL Server&#41;](keep-nulls-or-use-default-values-during-bulk-import-sql-server.md)  
  
-   [Prepare Data for Bulk Export or Import &#40;SQL Server&#41;](prepare-data-for-bulk-export-or-import-sql-server.md)  
  
 **To use a format file**  
  
-   [Create a Format File &#40;SQL Server&#41;](create-a-format-file-sql-server.md)  
  
-   [Use a Format File to Bulk Import Data &#40;SQL Server&#41;](use-a-format-file-to-bulk-import-data-sql-server.md)  
  
-   [Use a Format File to Map Table Columns to Data-File Fields &#40;SQL Server&#41;](use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)  
  
-   [Use a Format File to Skip a Data Field &#40;SQL Server&#41;](use-a-format-file-to-skip-a-data-field-sql-server.md)  
  
-   [Use a Format File to Skip a Table Column &#40;SQL Server&#41;](use-a-format-file-to-skip-a-table-column-sql-server.md)  
  
 **To use data formats for bulk import or bulk export**  
  
-   [Import Native and Character Format Data from Earlier Versions of SQL Server](import-native-and-character-format-data-from-earlier-versions-of-sql-server.md)  
  
-   [Use Character Format to Import or Export Data &#40;SQL Server&#41;](use-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Native Format to Import or Export Data &#40;SQL Server&#41;](use-native-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Character Format to Import or Export Data &#40;SQL Server&#41;](use-unicode-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Native Format to Import or Export Data &#40;SQL Server&#41;](use-unicode-native-format-to-import-or-export-data-sql-server.md)  
  
 **To specify data formats for compatibility when using bcp**  
  
1.  [Specify Field and Row Terminators &#40;SQL Server&#41;](specify-field-and-row-terminators-sql-server.md)  
  
2.  [Specify Prefix Length in Data Files by Using bcp &#40;SQL Server&#41;](specify-prefix-length-in-data-files-by-using-bcp-sql-server.md)  
  
3.  [Specify File Storage Type by Using bcp &#40;SQL Server&#41;](specify-file-storage-type-by-using-bcp-sql-server.md)  
  
## See Also  
 [BACKUP &#40;Transact-SQL&#41;](/sql/t-sql/statements/backup-transact-sql)   
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql)   
 [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql)   
 [Table Hints &#40;Transact-SQL&#41;](/sql/t-sql/queries/hints-transact-sql-table)  
  
  
