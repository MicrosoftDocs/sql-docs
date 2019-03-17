---
title: "Use Unicode Character Format to Import or Export Data (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "data formats [SQL Server], Unicode character"
  - "Unicode [SQL Server], bulk importing and exporting"
ms.assetid: 74342a11-c1c0-4746-b482-7f3537744a70
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Use Unicode Character Format to Import or Export Data (SQL Server)
  Unicode character format is recommended for bulk transfer of data between multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using a data file that contains extended/DBCS characters. The Unicode character data format allows data to be exported from a server by using a code page that differs from the code page used by the client that is performing the operation. In such cases, use of Unicode character format has the following advantages:  
  
-   If the source and destination data are Unicode data types, use of Unicode character format preserves all of the character data.  
  
-   if the source and destination data are not Unicode data types, use of Unicode character format minimizes the loss of extended characters in the source data that cannot be represented at the destination.  
  
 Unicode character format data files follow the conventions for Unicode files. The first two bytes of the file are hexadecimal numbers, 0xFFFE. These bytes serve as byte-order marks, specifying whether the high-order byte is stored first or last in the file.  
  
> [!IMPORTANT]  
>  For a format file to work with a Unicode character data file, all the input fields must be Unicode text strings (that is, either fixed-size or character-terminated Unicode strings).  
  
 The `sql_variant` data that is stored in a Unicode character-format data file operates in the same way it operates in a character-format data file, except that the data is stored as `nchar` instead of `char` data. For more information about character format, see [Collation and Unicode Support](../collations/collation-and-unicode-support.md).  
  
 To use a field or row terminator other than the default that is provided with Unicode character format, see [Specify Field and Row Terminators &#40;SQL Server&#41;](specify-field-and-row-terminators-sql-server.md).  
  
## Command Options for Unicode Character Format  
 You can import Unicode character format data into a table using **bcp**, BULK INSERT or INSERT ... SELECT \* FROM OPENROWSET(BULK...). For a **bcp** command or BULK INSERT statement, you can specify the data format on the command line. For an INSERT ... SELECT * FROM OPENROWSET(BULK...) statement, you must specify the data format in a format file.  
  
 Unicode character format is supported by the following command line options:  
  
|Command|Option|Description|  
|-------------|------------|-----------------|  
|**bcp**|**-w**|Uses the Unicode character format.|  
|BULK INSERT|DATAFILETYPE **='**widechar**'**|Uses Unicode character format when bulk importing data.|  
  
 For more information, see [bcp Utility](../../tools/bcp-utility.md), [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql), or [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql).  
  
> [!NOTE]  
>  Alternatively, you can specify formatting on a per-field basis in a format file. For more information, see [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](format-files-for-importing-or-exporting-data-sql-server.md).  
  
## Examples  
 The following examples demonstrate how to bulk export Unicode character data using **bcp** and to bulk import the same data using BULK INSERT.  
  
### Sample Table  
 The examples require that a table named `myTestUniCharData` table be created in the [!INCLUDE[ssSampleDBnormal](../../includes/sssampledbnormal-md.md)] sample database under the `dbo` schema. Before you can run the examples, you must create this table. To create this table, in [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] Query Editor, execute:  
  
```  
USE AdventureWorks2012;  
GO  
CREATE TABLE myTestUniCharData (  
   Col1 smallint,  
   Col2 nvarchar(50),  
   Col3 nvarchar(50)  
   );   
```  
  
 To populate this table and view the resulting contents execute the following statements:  
  
```  
INSERT INTO myTestUniCharData(Col1,Col2,Col3)  
   VALUES(1,'DataField2','DataField3')   
        ,(2,'DataField2','DataField3');  
GO  
SELECT Col1,Col2,Col3 FROM myTestUniCharData;  
  
```  
  
### Using bcp to Bulk Export Unicode Character Data  
 To export data from the table to the data file, use **bcp** with the **out** option and the following qualifiers:  
  
|Qualifiers|Description|  
|----------------|-----------------|  
|**-w**|Specifies Unicode character format.|  
|**-t** `,`|Specifies a comma (`,`) as the field terminator.<br /><br /> Note: The default field terminator is the tab Unicode character (\t). For more information, see [Specify Field and Row Terminators &#40;SQL Server&#41;](specify-field-and-row-terminators-sql-server.md).|  
|**-T**|Specifies that the **bcp** utility connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with a trusted connection using integrated security. If **-T** is not specified, you need to specify **-U** and **-P** to successfully log in.|  
  
 The following example bulk exports data in Unicode character format from the `myTestUniCharData` table into a new data file named `myTestUniCharData-w.Dat` data file that uses the comma (`,`) as the field terminator. At the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows command prompt, enter:  
  
```  
bcp AdventureWorks2012..myTestUniCharData out C:\myTestUniCharData-w.Dat -w -t, -T  
  
```  
  
### Using BULK INSERT to Bulk Import Unicode Character Data  
 The following example uses `BULK INSERT` to import the data in the `myTestUniCharData-w.Dat` data file into the `myTestUniCharData` table. The nondefault field terminator (`,`) must be declared in the statement. In [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] Query Editor, execute:  
  
```  
USE AdventureWorks2012;  
GO  
BULK INSERT myTestUniCharData   
   FROM 'C:\myTestUniCharData-w.Dat'   
   WITH (  
      DATAFILETYPE='widechar',  
      FIELDTERMINATOR=','  
   );   
GO  
SELECT Col1,Col2,Col3 FROM myTestUniCharData;  
GO  
  
```  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To use data formats for bulk import or bulk export**  
  
-   [Import Native and Character Format Data from Earlier Versions of SQL Server](import-native-and-character-format-data-from-earlier-versions-of-sql-server.md)  
  
-   [Use Character Format to Import or Export Data &#40;SQL Server&#41;](use-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Native Format to Import or Export Data &#40;SQL Server&#41;](use-native-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Native Format to Import or Export Data &#40;SQL Server&#41;](use-unicode-native-format-to-import-or-export-data-sql-server.md)  
  
## See Also  
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql)   
 [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql)   
 [Data Types &#40;Transact-SQL&#41;](/sql/t-sql/data-types/data-types-transact-sql)   
 [Collation and Unicode Support](../collations/collation-and-unicode-support.md)  
  
  
