---
title: "Use Native Format to Import or Export Data (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: "06/13/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "native data format [SQL Server]"
  - "data formats [SQL Server], native"
ms.assetid: eb279b2f-0f1f-428f-9b8f-2a7fc495b79f
author: douglaslMS
ms.author: douglasl
manager: craigg
---
# Use Native Format to Import or Export Data (SQL Server)
  Native format is recommended when you bulk transfer data between multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using a data file that does not contain any extended/double-byte character set (DBCS) characters.  
  
> [!NOTE]  
>  To bulk transfer data between multiple instances of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] by using a data file that contains extended or DBCS characters, you should use the Unicode native format. For more information, see [Use Unicode Native Format to Import or Export Data &#40;SQL Server&#41;](use-unicode-native-format-to-import-or-export-data-sql-server.md).  
  
 Native format maintains the native data types of a database. Native format is intended for high-speed data transfer of data between [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tables. If you use a format file, the source and target tables do not need to be identical. The data transfer involves two steps:  
  
1.  Bulk exporting the data from a source table into a data file  
  
2.  Bulk importing the data from the data file into the target table  
  
 The use of native format between identical tables avoids unnecessary conversion of data types to and from character format, saving time and space. To achieve the optimum transfer rate, however, few checks are performed regarding data formatting. To prevent problems with the loaded data, see the following restrictions list.  
  
## Restrictions  
 To import data in native format successfully, ensure that:  
  
-   The data file is in native format.  
  
-   Either the target table must be compatible with the data file (having the correct number of columns, data type, length, NULL status, and so forth), or you must use a format file to map each field to its corresponding columns.  
  
    > [!NOTE]  
    >  If you import data from a file that is mismatched with the target table, the import operation might succeed but the data values inserted into the target table are likely to be incorrect. This is because the data from the file is interpreted by using the format of the target table. Therefore, any mismatch results in the insertion of incorrect values. However, under no circumstances can such a mismatch cause logical or physical inconsistencies in the database.  
  
     For information on using format files, see [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](format-files-for-importing-or-exporting-data-sql-server.md).  
  
 A successful import will not corrupt the target table.  
  
## How bcp Handles Data in Native Format  
 This section discusses special considerations for how the **bcp** utility exports and imports data in native format.  
  
-   Noncharacter data  
  
     The bcp utility uses the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] internal binary data format to write noncharacter data from a table to a data file.  
  
-   `char` or `varchar` data  
  
     At the beginning of each `char` or `varchar` field, **bcp** adds the prefix length.  
  
    > [!IMPORTANT]  
    >  When native mode is used, by default, the **bcp** utility converts characters from [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] to OEM characters before it copies them to a data file. The **bcp** utility converts characters from a data file to ANSI characters before it bulk imports them into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. During these conversions, extended character data can be lost. For extended characters, either use Unicode native format or specify a code page.  
  
-   `sql_variant` data  
  
     If `sql_variant` data is stored as a SQLVARIANT in a native-format data file, the data maintains all of its characteristics. The metadata that records the data type of each data value is stored along with the data value. This metadata is used to re-create the data value with the same data type in a destination `sql_variant` column.  
  
     If the data type of the destination column is not `sql_variant`, each data value is converted to the data type of the destination column, following the normal rules of implicit data conversion. If an error occurs during data conversion, the current batch is rolled back. Any `char` and `varchar` values that are transferred between `sql_variant` columns may have code page conversion issues.  
  
     For more information about data conversion, see [Data Type Conversion &#40;Database Engine&#41;](/sql/t-sql/data-types/data-type-conversion-database-engine).  
  
## Command Options for Native Format  
 You can import native format data into a table using **bcp**, BULK INSERT or INSERT ... SELECT \* FROM OPENROWSET(BULK...). For a **bcp** command or BULK INSERT statement, you can specify the data format on the command line. For an INSERT ... SELECT * FROM OPENROWSET(BULK...) statement, you must specify the data format in a format file.  
  
 Native format is supported by the following command line options:  
  
|Command|Option|Description|  
|-------------|------------|-----------------|  
|**bcp**|**-n**|Causes the **bcp** utility to use the native data types of the data.<sup>1</sup>|  
|BULK INSERT|DATAFILETYPE **='**native**'**|Uses the native or wide native data types of the data. Note that DATAFILETYPE is not needed if a format file specifies the data types.|  
  
 <sup>1</sup> To load native (**-n**) data to a format compatible with earlier versions of [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] clients, use the **-V** switch. For more information, see [Import Native and Character Format Data from Earlier Versions of SQL Server](import-native-and-character-format-data-from-earlier-versions-of-sql-server.md).  
  
 For more information, see [bcp Utility](../../tools/bcp-utility.md), [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql), or [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql).  
  
> [!NOTE]  
>  Alternatively, you can specify formatting on a per-field basis in a format file. For more information, see [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](format-files-for-importing-or-exporting-data-sql-server.md).  
  
## Examples  
 The following examples demonstrate how to bulk export native data using **bcp** and bulk import the same data using BULK INSERT.  
  
### Sample Table  
 The examples require that a table named **myTestNativeData** table be created in the **AdventureWorks** sample database under the **dbo** schema. Before you can run the examples, you must create this table. In [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] Query Editor, execute:  
  
```  
USE AdventureWorks;  
GO  
CREATE TABLE myTestNativeData (  
   Col1 smallint,  
   Col2 nvarchar(50),  
   Col3 nvarchar(50)  
   );   
```  
  
 To populate this table and view the resulting contents execute the following statements:  
  
```  
INSERT INTO myTestNativeData(Col1,Col2,Col3)  
   VALUES(1,'DataField2','DataField3');  
INSERT INTO myTestNativeData(Col1,Col2,Col3)  
   VALUES(2,'DataField2','DataField3');  
GO  
SELECT Col1,Col2,Col3 FROM myTestNativeData  
  
```  
  
### Using bcp to Bulk Export Native Data  
 To export data from the table to the data file, use **bcp** with the **out** option and the following qualifiers:  
  
|Qualifiers|Description|  
|----------------|-----------------|  
|**-n**|Specifies native data types.|  
|**-T**|Specifies that the **bcp** utility connects to [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] with a trusted connection using integrated security. If **-T** is not specified, you need to specify **-U** and **-P** to successfully log in.|  
  
 The following example bulk exports data in native format from the `myTestNativeData` table into a new data file named `myTestNativeData-n.Dat` data file. At the [!INCLUDE[msCoName](../../includes/msconame-md.md)] Windows command prompt, enter:  
  
```  
bcp AdventureWorks..myTestNativeData out C:\myTestNativeData-n.Dat -n -T  
  
```  
  
### Using BULK INSERT to Bulk Import Native Data  
 The following example uses BULK INSERT to import the data in the `myTestNativeData-n.Dat` data file into the `myTestNativeData` table. In [!INCLUDE[ssManStudioFull](../../../includes/ssmanstudiofull-md.md)] Query Editor, execute:  
  
```  
USE AdventureWorks;  
GO  
BULK INSERT myTestNativeData   
    FROM 'C:\myTestNativeData-n.Dat'   
   WITH (DATAFILETYPE='native');   
GO  
SELECT Col1,Col2,Col3 FROM myTestNativeData  
GO  
  
```  
  
##  <a name="RelatedTasks"></a> Related Tasks  
 **To use data formats for bulk import or bulk export**  
  
-   [Import Native and Character Format Data from Earlier Versions of SQL Server](import-native-and-character-format-data-from-earlier-versions-of-sql-server.md)  
  
-   [Use Character Format to Import or Export Data &#40;SQL Server&#41;](use-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Character Format to Import or Export Data &#40;SQL Server&#41;](use-unicode-character-format-to-import-or-export-data-sql-server.md)  
  
-   [Use Unicode Native Format to Import or Export Data &#40;SQL Server&#41;](use-unicode-native-format-to-import-or-export-data-sql-server.md)  
  
## See Also  
 [bcp Utility](../../tools/bcp-utility.md)   
 [BULK INSERT &#40;Transact-SQL&#41;](/sql/t-sql/statements/bulk-insert-transact-sql)   
 [Data Types &#40;Transact-SQL&#41;](/sql/t-sql/data-types/data-types-transact-sql)   
 [sql_variant &#40;Transact-SQL&#41;](/sql/t-sql/data-types/sql-variant-transact-sql)   
 [Import Native and Character Format Data from Earlier Versions of SQL Server](import-native-and-character-format-data-from-earlier-versions-of-sql-server.md)   
 [OPENROWSET &#40;Transact-SQL&#41;](/sql/t-sql/functions/openrowset-transact-sql)   
 [Use Unicode Native Format to Import or Export Data &#40;SQL Server&#41;](use-unicode-native-format-to-import-or-export-data-sql-server.md)  
  
  
