---
title: "XML Format Files (SQL Server) | Microsoft Docs"
ms.custom: ""
ms.date: 01/11/2019
ms.prod: sql
ms.prod_service: "database-engine, sql-database, sql-data-warehouse, pdw"
ms.reviewer: ""
ms.technology: data-movement
ms.topic: conceptual
helpviewer_keywords: 
  - "format files [SQL Server], XML format files"
  - "bulk importing [SQL Server], format files"
  - "XML format files [SQL Server]"
ms.assetid: 69024aad-eeea-4187-8fea-b49bc2359849
author: MashaMSFT
ms.author: mathoma
manager: craigg
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||=sqlallproducts-allversions||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# XML Format Files (SQL Server)
[!INCLUDE[appliesto-ss-asdb-asdw-pdw-md](../../includes/appliesto-ss-asdb-asdw-pdw-md.md)]
  [!INCLUDE[ssCurrent](../../includes/sscurrent-md.md)] provides an XML schema that defines syntax for writing *XML format files* to use for bulk importing data into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table. XML format files must adhere to this schema, which is defined in the XML Schema Definition Language (XSDL). XML format files are only supported when [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] tools are installed together with [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client.  
  
 You can use an XML format file with a **bcp** command, BULK INSERT statement, or INSERT ... SELECT \* FROM OPENROWSET(BULK...) statement. The **bcp** command allows you to automatically generate an XML format file for a table; for more information, see [bcp Utility](../../tools/bcp-utility.md).  
  
> [!NOTE]  
>  Two types of format files are supported for bulk exporting and importing: *non-XML format files* and *XML format files*. XML format files provide a flexible and powerful alternative to non-XML format files. For information about non-XML format files, see [Non-XML Format Files &#40;SQL Server&#41;](../../relational-databases/import-export/non-xml-format-files-sql-server.md).  
  
 **In This Topic:**  
  
-   [Benefits of XML Format Files](#BenefitsOfXmlFFs)  
  
-   [Structure of XML Format Files](#StructureOfXmlFFs)  
  
-   [Schema Syntax for XML Format Files](#SchemaSyntax)  
  
-   [Sample XML Format Files](#SampleXmlFFs)  
  
-   [Related Tasks](#RelatedTasks)  
  
-   [Related Content](#RelatedContent)  
  
##  <a name="BenefitsOfXmlFFs"></a> Benefits of XML Format Files  
  
-   XML format files are self-describing, making them easy to read, create, and extend. They are human readable, making it easy to understand how data is interpreted during bulk operations.  
  
-   XML format files contain the data types of target columns.  The XML encoding clearly describes the data types and data elements of the data file and also the mapping between data elements and table columns.  
  
     This enables separation between how data is represented in the data file and what data type is associated with each field in the file. For example, if a data file contains a character representation of the data, the corresponding SQL column type is lost.  
  
-   An XML format file allows for loading of a field that contains a single large object (LOB) data type from a data file.  
  
-   An XML format file can be enhanced yet remain compatible with its earlier versions. Furthermore, the clarity of XML encoding facilitates the creation of multiple format files for a given data file. This is useful if you have to map all or some of the data fields to columns in different tables or views.  
  
-   The XML syntax is independent of the direction of the operation; that is, the syntax is the same for bulk export and bulk import.  
  
-   You can use XML format files to bulk import data into tables or non-partitioned views and to bulk export data.  
  
-   For the OPENROWSET(BULK...) function specifying a target table is optional. This is because the function relies on the XML format file to read data from a data file.  
  
    > [!NOTE]  
    >  A target table is necessary with the **bcp** command and the BULK INSERT statement, which uses the target table columns to do the type conversion.  
  
##  <a name="StructureOfXmlFFs"></a> Structure of XML Format Files  
 Like a non-XML format file, an XML format file defines the format and structure of the data fields in a data file and maps those data fields to columns in a single target table.  
  
 An XML format file possesses two main components, \<RECORD> and \<ROW>:  
  
-   \<RECORD> describes the data as it is stored in the data file.  
  
     Each \<RECORD> element contains a set of one or more \<FIELD> elements. These elements correspond to fields in the data file. The basic syntax is as follows:  
  
     \<RECORD>  
  
     \<FIELD .../> [ ...*n* ]  
  
     \</RECORD>  
  
     Each \<FIELD> element describes the contents of a specific data field. A field can only be mapped to one column in the table. Not all fields need to be mapped to columns.  
  
     A field in a data file can be either of fixed/variable length or character terminated. A *field value* can be represented as: a character (using single-byte representation), a wide character (using Unicode two-byte representation), native database format, or a file name. If a field value is represented as a file name, the file name points to the file that contains the value of a BLOB column in the target table.  
  
-   \<ROW> describes how to construct data rows from a data file when the data from the file is imported into a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] table.  
  
     A \<ROW> element contains a set of \<COLUMN> elements. These elements correspond to table columns. The basic syntax is as follows:  
  
     \<ROW>  
  
     \<COLUMN .../> [ ...*n* ]  
  
     \</ROW>  
  
     Each \<COLUMN> element can be mapped to only one field in the data file. The order of the \<COLUMN> elements in the \<ROW> element defines the order in which they are returned by the bulk operation. The XML format file assigns each \<COLUMN> element a local name that has no relationship to the column in the target table of a bulk import operation.  
  
##  <a name="SchemaSyntax"></a> Schema Syntax for XML Format Files  
 This section contains a summary of the elements and attributes of the XML schema for XML format files. The syntax of a format file is independent of the direction of the operation; that is, the syntax is the same for bulk export and bulk import. This section also considers how bulk import uses the \<ROW> and \<COLUMN> elements and how to put the xsi:type value of an element into a data set.  
  
 To see how the syntax corresponds to actual XML format files, see [Sample XML Format Files](#SampleXmlFFs), later in this topic.  
  
> [!NOTE]  
>  You can modify a format file to let you bulk import from a data file in which the number and/or order of the fields differ from the number and/or order of table columns. For more information, see [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md).  
  
 **In This Section:**  
  
-   [Basic Syntax of the XML Schema](#BasicSyntax)  
  
-   [How Bulk Import Uses the \<ROW> Element](#HowUsesROW)  
  
-   [How Bulk Import Uses the \<COLUMN> Element](#HowUsesColumn)  
  
-   [Putting the xsi:type Value into a Data Set](#PutXsiTypeValueIntoDataSet)  
  
###  <a name="BasicSyntax"></a> Basic Syntax of the XML Schema  
 This syntax statements show only the elements (\<BCPFORMAT>, \<RECORD>, \<FIELD>, \<ROW>, and \<COLUMN>) and their basic attributes.  
  
 \<BCPFORMAT ...>  
  
 \<RECORD>  
  
 \<FIELD ID = "*fieldID*" xsi:type = "*fieldType*" [...]  
  
 />  
  
 \</RECORD>  
  
 \<ROW>  
  
 \<COLUMN SOURCE = "*fieldID*" NAME = "*columnName*" xsi:type = "*columnType*" [...]  
  
 />  
  
 \</ROW>  
  
 \</BCPFORMAT>  
  
> [!NOTE]  
>  Additional attributes that are associated with the value of the xsi:type in a \<FIELD> or \<COLUMN> element are described later in this topic.  
  
 **In This Section:**  
  
-   [Schema Elements](#SchemaElements)  
  
-   [Attributes of the \<FIELD> Element](#AttrOfFieldElement) (and [Xsi:type values of the \<FIELD> Element](#XsiTypeValuesOfFIELD))  
  
-   [Attributes of the \<COLUMN> Element](#AttrOfColumnElement) and ([Xsi:type values of the \<COLUMN> Element](#XsiTypeValuesOfCOLUMN))  
  
####  <a name="SchemaElements"></a> Schema Elements  
 This section summarizes the purpose of each element that the XML schema defines for XML format files. The attributes are described in separate sections later in this topic.  
  
 \<BCPFORMAT>  
 Is the format-file element that defines the record structure of a given data file and its correspondence to the columns of a table row in the table.  
  
 \<RECORD .../>  
 Defines a complex element containing one or more \<FIELD> elements. The order in which the fields are declared in the format file is the order in which those fields appear in the data file.  
  
 \<FIELD .../>  
 Defines a field in data file, which contains data.  
  
 The attributes of this element are discussed in [Attributes of the \<FIELD> Element](#AttrOfFieldElement), later in this topic.  
  
 \<ROW .../>  
 Defines a complex element containing one or more \<COLUMN> elements. The order of the \<COLUMN> elements is independent of the order of \<FIELD> elements in a RECORD definition. Rather, the order of the \<COLUMN> elements in a format file determines the column order of the resultant rowset. Data fields are loaded in the order in which the corresponding \<COLUMN> elements are declared in the \<COLUMN> element.  
  
 For more information, see [How Bulk Import Uses the \<ROW> Element](#HowUsesROW), later in this topic.  
  
 \<COLUMN>  
 Defines a column as an element (\<COLUMN>). Each \<COLUMN> element corresponds to a \<FIELD> element (whose ID is specified in the SOURCE attribute of the \<COLUMN> element).  
  
 The attributes of this element are discussed in [Attributes of the \<COLUMN> Element](#AttrOfColumnElement), later in this topic. Also see, [How Bulk Import Uses the \<COLUMN> Element](#HowUsesColumn), later in this topic.  
  
 \</BCPFORMAT>  
 Required to end the format file.  
  
####  <a name="AttrOfFieldElement"></a> Attributes of the \<FIELD> Element  
 This section describes the attributes of the \<FIELD> element, which are summarized in the following schema syntax:  
  
 <FIELD  
  
 ID **="**_fieldID_**"**  
  
 xsi**:**type **="**_fieldType_**"**  
  
 [ LENGTH **="**_n_**"** ]  
  
 [ PREFIX_LENGTH **="**_p_**"** ]  
  
 [ MAX_LENGTH **="**_m_**"** ]  
  
 [ COLLATION **="**_collationName_**"** ]  
  
 [ TERMINATOR **="**_terminator_**"** ]  
  
 />  
  
 Each \<FIELD> element is independent of the others. A field is described in terms of the following attributes:  
  
|FIELD Attribute|Description|Optional /<br /><br /> Required|  
|---------------------|-----------------|------------------------------|  
|ID **="**_fieldID_**"**|Specifies the logical name of the field in the data file. The ID of a field is the key used to refer to the field.<br /><br /> \<FIELD ID**="**_fieldID_**"**/> maps to \<COLUMN SOURCE**="**_fieldID_**"**/>|Required|  
|xsi:type **="**_fieldType_**"**|This is an XML construct (used like an attribute) that identifies the type of the instance of the element. The value of *fieldType* determines which of the optional attributes (below) you need in a given instance.|Required (depending on the data type)|  
|LENGTH **="**_n_**"**|This attribute defines the length for an instance of a fixed-length data type.<br /><br /> The value of *n* must be a positive integer.|Optional unless required by the xsi:type value|  
|PREFIX_LENGTH **="**_p_**"**|This attribute defines the prefix length for a binary data representation. The PREFIX_LENGTH value, *p*, must be one of the following: 1, 2, 4, or 8.|Optional unless required by the xsi:type value|  
|MAX_LENGTH **="**_m_**"**|This attribute is the maximum number of bytes that can be stored in a given field. Without a target table, the column max-length is not known. The MAX_LENGTH attribute restricts the maximum length of an output character column, limiting the storage allocated for the column value. This is especially convenient when using the OPENROWSET function's BULK option in a SELECT FROM clause.<br /><br /> The value of *m* must be a positive integer. By default, the maximum length is 8000 characters for a **char** column and 4000 characters for an **nchar** column.|Optional|  
|COLLATION **="**_collationName_**"**|COLLATION is only allowed for character fields. For a list of the SQL collation names, see [SQL Server Collation Name &#40;Transact-SQL&#41;](../../t-sql/statements/sql-server-collation-name-transact-sql.md).|Optional|  
|TERMINATOR **= "**_terminator_**"**|This attribute specifies the terminator of a data field. The terminator can be any character. The terminator must be a unique character that is not part of the data.<br /><br /> By default, the field terminator is the tab character (represented as \t). To represent a paragraph mark, use \r\n.|Used only with an xsi:type of character data, which requires this attribute|  
  
#####  <a name="XsiTypeValuesOfFIELD"></a> Xsi:type values of the \<FIELD> Element  
 The xsi:type value is an XML construct (used like an attribute) that identifies the data type of an instance of an element. For information on using the "Putting the xsi:type Value into a Data Set," later in this section.  
  
 The xsi:type value of the \<FIELD> element supports the following data types.  
  
|\<FIELD> xsi:type values|Required XML Attribute(s)<br /><br /> for Data Type|Optional XML Attribute(s)<br /><br /> for Data Type|  
|-------------------------------|---------------------------------------------------|---------------------------------------------------|  
|**NativeFixed**|**LENGTH**|None.|  
|**NativePrefix**|**PREFIX_LENGTH**|MAX_LENGTH|  
|**CharFixed**|**LENGTH**|COLLATION|  
|**NCharFixed**|**LENGTH**|COLLATION|  
|**CharPrefix**|**PREFIX_LENGTH**|MAX_LENGTH, COLLATION|  
|**NCharPrefix**|**PREFIX_LENGTH**|MAX_LENGTH, COLLATION|  
|**CharTerm**|**TERMINATOR**|MAX_LENGTH, COLLATION|  
|**NCharTerm**|**TERMINATOR**|MAX_LENGTH, COLLATION|  
  
 For more information about [!INCLUDE[msCoName](../../includes/msconame-md.md)] [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md).  
  
####  <a name="AttrOfColumnElement"></a> Attributes of the \<COLUMN> Element  
 This section describes the attributes of the \<COLUMN> element, which are summarized in the following schema syntax:  
  
 <COLUMN  
  
 SOURCE = "*fieldID*"  
  
 NAME = "*columnName*"  
  
 xsi:type = "*columnType*"  
  
 [ LENGTH = "*n*" ]  
  
 [ PRECISION = "*n*" ]  
  
 [ SCALE = "*value*" ]  
  
 [ NULLABLE = { "YES"  
  
 "NO" } ]  
  
 />  
  
 A field is mapped to a column in the target table using the following attributes:  
  
|COLUMN Attribute|Description|Optional /<br /><br /> Required|  
|----------------------|-----------------|------------------------------|  
|SOURCE **="**_fieldID_**"**|Specifies the ID of the field being mapped to the column.<br /><br /> \<COLUMN SOURCE**="**_fieldID_**"**/> maps to \<FIELD ID**="**_fieldID_**"**/>|Required|  
|NAME = "*columnName*"|Specifies the name of the column in the row set represented by the format file. This column name is used to identify the column in the result set, and it need not correspond to the column name used in the target table.|Required|  
|xsi**:**type **="**_ColumnType_**"**|This is an XML construct (used like an attribute) that identifies the data type of the instance of the element. The value of *ColumnType* determines which of the optional attributes (below) you need in a given instance.<br /><br /> Note: The possible values of *ColumnType* and their associated attributes are listed in the \<COLUMN> element table in the [Xsi:type values of the &lt;COLUMN&gt; Element](#XsiTypeValuesOfCOLUMN) section.|Optional|  
|LENGTH **="**_n_**"**|Defines the length for an instance of a fixed-length data type. LENGTH is used only when the xsi:type is a string data type.<br /><br /> The value of *n* must be a positive integer.|Optional (available only if the xsi:type is a string data type)|  
|PRECISION **="**_n_**"**|Indicates the number of digits in a number. For example, the number 123.45 has a precision of 5.<br /><br /> The value must be a positive integer.|Optional (available only if the xsi:type is a variable-number data type)|  
|SCALE **="**_int_**"**|Indicates the number of digits to the right of the decimal point in a number. For example, the number 123.45 has a scale of 2.<br /><br /> The value must be an integer.|Optional (available only if the xsi:type is a variable-number data type)|  
|NULLABLE **=** { **"**YES**"**<br /><br /> **"**NO**"** }|Indicates whether a column can assume NULL values. This attribute is completely independent of FIELDS. However, if a column is not NULLABLE and field specifies NULL (by not specifying any value), a run-time error results.<br /><br /> The NULLABLE attribute is used only if you do a plain SELECT FROM OPENROWSET(BULK...) statement.|Optional (available for any data type)|  
  
#####  <a name="XsiTypeValuesOfCOLUMN"></a> Xsi:type values of the \<COLUMN> Element  
 The xsi:type value is an XML construct (used like an attribute) that identifies the data type of an instance of an element. For information on using the "Putting the xsi:type Value into a Data Set," later in this section.  
  
 The \<COLUMN> element supports native SQL data types, as follows:  
  
|Type Category|\<COLUMN> Data Types|Required XML Attribute(s)<br /><br /> for Data Type|Optional XML Attribute(s)<br /><br /> for Data Type|  
|-------------------|---------------------------|---------------------------------------------------|---------------------------------------------------|  
|Fixed|**SQLBIT**, **SQLTINYINT**, **SQLSMALLINT**, **SQLINT**, **SQLBIGINT**, **SQLFLT4**, **SQLFLT8**, **SQLDATETIME**, **SQLDATETIM4**, **SQLDATETIM8**, **SQLMONEY**, **SQLMONEY4**, **SQLVARIANT**, and **SQLUNIQUEID**|None.|NULLABLE|  
|Variable Number|**SQLDECIMAL** and **SQLNUMERIC**|None.|NULLABLE, PRECISION, SCALE|  
|LOB|**SQLIMAGE**, **CharLOB**, **SQLTEXT**, and **SQLUDT**|None.|NULLABLE|  
|Character LOB|**SQLNTEXT**|None.|NULLABLE|  
|Binary string|**SQLBINARY** and **SQLVARYBIN**|None.|NULLABLE, LENGTH|  
|Character string|**SQLCHAR**, **SQLVARYCHAR**, **SQLNCHAR**, and **SQLNVARCHAR**|None.|NULLABLE, LENGTH|  
  
> [!IMPORTANT]  
>  To bulk export or import SQLXML data, use one of the following data types in your format file: SQLCHAR or SQLVARYCHAR (the data is sent in the client code page or in the code page implied by the collation), SQLNCHAR or SQLNVARCHAR (the data is sent as Unicode), or SQLBINARY or SQLVARYBIN (the data is sent without any conversion).  
  
 For more information about [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] data types, see [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md).  
  
###  <a name="HowUsesROW"></a> How Bulk Import Uses the \<ROW> Element  
 The \<ROW> element is ignored in some contexts. Whether the \<ROW> element affects a bulk-import operation depends on how the operation is performed:  
  
-   the **bcp** command  
  
     When data is loaded into a target table, **bcp** ignores the \<ROW> component. Instead, **bcp** loads the data based on the column types of the target table.  
  
-   [!INCLUDE[tsql](../../includes/tsql-md.md)] statements (BULK INSERT and OPENROWSET's Bulk rowset provider)  
  
     When bulk importing data into a table, [!INCLUDE[tsql](../../includes/tsql-md.md)] statements use the \<ROW> component to generate the input rowset. Also, [!INCLUDE[tsql](../../includes/tsql-md.md)] statements perform appropriate type conversions based on the column types specified under \<ROW> and the corresponding column in the target table. If a mismatch exists between column types as specified in the format file and in the target table, an extra type conversion occurs. This extra type conversion may lead to some discrepancy (that is, a loss of precision) in behavior in BULK INSERT or OPENROWSET's Bulk rowset provider as compared to **bcp**.  
  
     The information in the \<ROW> element allows a row to be constructed without requiring any additional information. For this reason, you can generate a rowset using a SELECT statement (SELECT \* FROM OPENROWSET(BULK *datafile* FORMATFILE=*xmlformatfile*).  
  
    > [!NOTE]  
    >  The OPENROWSET BULK clause requires a format file (note that converting from the data type of the field to the data type of a column is available only with an XML format file).  
  
###  <a name="HowUsesColumn"></a> How Bulk Import Uses the \<COLUMN> Element  
 For bulk importing data into a table, the \<COLUMN> elements in a format file map a data-file field to table columns by specifying:  
  
-   The position of each field within a row in the data file.  
  
-   The column type, which is used to convert the field data type to the desired column data type.  
  
 If no column is mapped to a field, the field is not copied into the generated row(s). This behavior allows a data file to generate rows with different columns (in different tables).  
  
 Similarly, for bulk exporting data from a table, each \<COLUMN> in the format file maps the column from the input table row to its corresponding field in the output data file.  
  
###  <a name="PutXsiTypeValueIntoDataSet"></a> Putting the xsi:type Value into a Data Set  
 When an XML document is validated through the XML Schema Definition (XSD) language, the xsi:type value is not put into the data set. However, you can put the xsi:type information into the data set by loading the XML format file into an XML document (for example, `myDoc`), as illustrated in the following code snippet:  
  
```cs
...;  
myDoc.LoadXml(xmlFormat);  
XmlNodeList ColumnList = myDoc.GetElementsByTagName("COLUMN");  
for(int i=0;i<ColumnList.Count;i++)  
{  
   Console.Write("COLUMN: xsi:type=" +ColumnList[i].Attributes["type",  
      "http://www.w3.org/2001/XMLSchema-instance"].Value+"\n");  
}  
```  
  
##  <a name="SampleXmlFFs"></a> Sample XML Format Files  
 This section contains information on using XML format files in a variety of cases, including an [!INCLUDE[ssSampleDBCoShort](../../includes/sssampledbcoshort-md.md)] example.  
  
> [!NOTE]  
>  In the data files shown in the following examples, `<tab>` indicates a tab character in a data file, and `<return>` indicates a carriage return.  
  
 The examples illustrate key aspects of using XML format files, as follows:  
  
-   [Ordering character-data fields the same as table columns](#OrderCharFieldsSameAsCols)  
  
-   [Ordering data fields and table columns differently](#OrderFieldsAndColsDifferently)  
  
-   [Omitting a data field](#OmitField)  
  
-   [Mapping different types of fields to columns](#MapXSItype)  
  
-   [Mapping XML data to a table](#MapXMLDataToTbl)  
  
-   [Importing fixed-length or fixed-width fields](#ImportFixedFields)  
  
-   [Additional Example](#AdditionalExamples)  
  
> [!NOTE]  
>  For information about how to create format files, see [Create a Format File &#40;SQL Server&#41;](../../relational-databases/import-export/create-a-format-file-sql-server.md).  
  
###  <a name="OrderCharFieldsSameAsCols"></a> A. Ordering character-data fields the same as table columns  
 The following example shows an XML format file that describes a data file containing three fields of character data. The format file maps the data file to a table that contains three columns. The data fields correspond one-to-one with the columns of the table.  
  
 **Table (row):** Person (Age int, FirstName varchar(20), LastName varchar(30))  
  
 **Data file (record):** Age\<tab>Firstname\<tab>Lastname\<return>  
  
 The following XML format file reads from the data file to the table.  
  
 In the `<RECORD>` element, the format file represents the data values in all three fields as character data. For each field, the `TERMINATOR` attribute indicates the terminator that follows the data value.  
  
 The data fields correspond one-to-one with the columns of the table. In the `<ROW>` element, the format file maps the column `Age` to the first field, the column `FirstName` to the second field, and the column `LastName` to the third field.  
  
```xml
<?xml version="1.0"?>  
<BCPFORMAT   
xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format"   
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
  <RECORD>  
    <FIELD ID="1" xsi:type="CharTerm" TERMINATOR="\t"   
      MAX_LENGTH="12"/>   
    <FIELD ID="2" xsi:type="CharTerm" TERMINATOR="\t"   
      MAX_LENGTH="20" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>  
    <FIELD ID="3" xsi:type="CharTerm" TERMINATOR="\r\n"   
      MAX_LENGTH="30"   
      COLLATION="SQL_Latin1_General_CP1_CI_AS"/>  
  </RECORD>  
  <ROW>  
    <COLUMN SOURCE="1" NAME="age" xsi:type="SQLINT"/>  
    <COLUMN SOURCE="2" NAME="firstname" xsi:type="SQLVARYCHAR"/>  
    <COLUMN SOURCE="3" NAME="lastname" xsi:type="SQLVARYCHAR"/>  
  </ROW>  
</BCPFORMAT>  
```  
  
> [!NOTE]  
>  For an equivalent [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] example, see [Create a Format File &#40;SQL Server&#41;](../../relational-databases/import-export/create-a-format-file-sql-server.md).  
  
###  <a name="OrderFieldsAndColsDifferently"></a> B. Ordering data fields and table columns differently  
 The following example shows an XML format file that describes a data file containing three fields of character data. The format file maps the data file to a table that contains three columns that are ordered differently from the fields of the data file.  
  
 **Table (row):** Person (Age int, FirstName varchar(20), LastName varchar(30))  
  
 **Data file** (record): Age\<tab>Lastname\<tab>Firstname\<return>  
  
 In the `<RECORD>` element, the format file represents the data values in all three fields as character data.  
  
 In the `<ROW>` element, the format file maps the column `Age` to the first field, the column `FirstName` to the third field, and the column `LastName` to the second field.  
  
```xml
<?xml version="1.0"?>  
<BCPFORMAT   
xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format"   
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
  <RECORD>  
    <FIELD ID="1" xsi:type="CharTerm" TERMINATOR="\t"   
      MAX_LENGTH="12"/>  
    <FIELD ID="2" xsi:type="CharTerm" TERMINATOR="\t" MAX_LENGTH="20"   
      COLLATION="SQL_Latin1_General_CP1_CI_AS"/>  
    <FIELD ID="3" xsi:type="CharTerm" TERMINATOR="\r\n"   
      MAX_LENGTH="30" COLLATION="SQL_Latin1_General_CP1_CI_AS"/>  
  </RECORD>  
  <ROW>  
    <COLUMN SOURCE="1" NAME="age" xsi:type="SQLINT"/>  
    <COLUMN SOURCE="3" NAME="firstname" xsi:type="SQLVARYCHAR"/>  
    <COLUMN SOURCE="2" NAME="lastname" xsi:type="SQLVARYCHAR"/>  
  </ROW>  
</BCPFORMAT>  
```  
  
> [!NOTE]  
>  For an equivalent [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] example, see [Use a Format File to Map Table Columns to Data-File Fields &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md).  
  
###  <a name="OmitField"></a> C. Omitting a data field  
 The following example shows an XML format file that describes a data file containing four fields of character data. The format file maps the data file to a table that contains three columns. The second data field does not correspond to any table column.  
  
 **Table (row):** Person (Age int, FirstName Varchar(20), LastName Varchar(30))  
  
 **Data file (record):** Age\<tab>employeeID\<tab>Firstname\<tab>Lastname\<return>  
  
 In the `<RECORD>` element, the format file represents the data values in all four fields as character data. For each field, the TERMINATOR attribute indicates the terminator that follows the data value.  
  
 In the `<ROW>` element, the format file maps the column `Age` to the first field, the column `FirstName` to the third field, and the column `LastName` to the fourth field.  
  
```xml
<?xml version = "1.0"?>  
<BCPFORMAT   
xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format"   
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
  <RECORD>  
    <FIELD ID="1" xsi:type="CharTerm" TERMINATOR="\t"   
      MAX_LENGTH="12"/>  
    <FIELD ID="2" xsi:type="CharTerm" TERMINATOR="\t"   
      MAX_LENGTH="10"   
      COLLATION="SQL_Latin1_General_CP1_CI_AS"/>  
    <FIELD ID="3" xsi:type="CharTerm" TERMINATOR="\t"   
      MAX_LENGTH="20"   
      COLLATION="SQL_Latin1_General_CP1_CI_AS"/>  
    <FIELD ID="4" xsi:type="CharTerm" TERMINATOR="\r\n"   
      MAX_LENGTH="30"   
      COLLATION="SQL_Latin1_General_CP1_CI_AS"/>  
  </RECORD>  
  <ROW>  
    <COLUMN SOURCE="1" NAME="age" xsi:type="SQLINT"/>  
    <COLUMN SOURCE="3" NAME="firstname" xsi:type="SQLVARYCHAR"/>  
    <COLUMN SOURCE="4" NAME="lastname" xsi:type="SQLVARYCHAR"/>  
  </ROW>  
</BCPFORMAT>  
```  
  
> [!NOTE]  
>  For an equivalent [!INCLUDE[ssSampleDBobject](../../includes/sssampledbobject-md.md)] example, see [Use a Format File to Skip a Data Field &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-data-field-sql-server.md).  
  
###  <a name="MapXSItype"></a> D. Mapping \<FIELD> xsi:type to \<COLUMN> xsi:type  
 The following example shows different types of fields and their mappings to columns.  
  
```xml
<?xml version = "1.0"?>  
<BCPFORMAT  
xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format"   
   xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
   <RECORD>  
      <FIELD xsi:type="CharTerm" ID="C1" TERMINATOR="\t"   
            MAX_LENGTH="4"/>  
      <FIELD xsi:type="CharFixed" ID="C2" LENGTH="10"   
         COLLATION="SQL_LATIN1_GENERAL_CP1_CI_AS"/>  
      <FIELD xsi:type="CharPrefix" ID="C3" PREFIX_LENGTH="2"   
         MAX_LENGTH="32" COLLATION="SQL_LATIN1_GENERAL_CP1_CI_AS"/>  
      <FIELD xsi:type="NCharTerm" ID="C4" TERMINATOR="\t"   
         MAX_LENGTH="4"/>  
      <FIELD xsi:type="NCharFixed" ID="C5" LENGTH="10"   
         COLLATION="SQL_LATIN1_GENERAL_CP1_CI_AS"/>  
      <FIELD xsi:type="NCharPrefix" ID="C6" PREFIX_LENGTH="2"   
         MAX_LENGTH="32" COLLATION="SQL_LATIN1_GENERAL_CP1_CI_AS"/>  
      <FIELD xsi:type="NativeFixed" ID="C7" LENGTH="4"/>  
   </RECORD>  
   <ROW>  
      <COLUMN SOURCE="C1" NAME="Age" xsi:type="SQLTINYINT"/>  
      <COLUMN SOURCE="C2" NAME="FirstName" xsi:type="SQLVARYCHAR"   
      LENGTH="16" NULLABLE="NO"/>  
      <COLUMN SOURCE="C3" NAME="LastName" />  
      <COLUMN SOURCE="C4" NAME="Salary" xsi:type="SQLMONEY"/>  
      <COLUMN SOURCE="C5" NAME="Picture" xsi:type="SQLIMAGE"/>  
      <COLUMN SOURCE="C6" NAME="Bio" xsi:type="SQLTEXT"/>  
      <COLUMN SOURCE="C7" NAME="Interest"xsi:type="SQLDECIMAL"   
      PRECISION="5" SCALE="3"/>  
   </ROW>  
</BCPFORMAT>  
```  
  
###  <a name="MapXMLDataToTbl"></a> E. Mapping XML data to a table  
 The following example creates an empty two-column table (`t_xml`), in which the first column maps to the `int` data type and the second column maps to the `xml` data type.  
  
```sql
CREATE TABLE t_xml (c1 int, c2 xml)  
```  
  
 The following XML format file would load a data file into table `t_xml`.  
  
```xml
<?xml version="1.0"?>  
<BCPFORMAT xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format"   
xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
 <RECORD>  
  <FIELD ID="1" xsi:type="NativePrefix" PREFIX_LENGTH="1"/>  
  <FIELD ID="2" xsi:type="NCharPrefix" PREFIX_LENGTH="8"/>  
 </RECORD>  
 <ROW>  
  <COLUMN SOURCE="1" NAME="c1" xsi:type="SQLINT"/>  
  <COLUMN SOURCE="2" NAME="c2" xsi:type="SQLNCHAR"/>  
 </ROW>  
</BCPFORMAT>  
```  
  
###  <a name="ImportFixedFields"></a> F. Importing fixed-length or fixed-width fields  
 The following example describes fixed fields of `10` or `6` characters each. The format file represents these field lengths/widths as `LENGTH="10"` and `LENGTH="6"`, respectively. Every row of the data files ends with a carriage return-line feed combination, {CR}{LF}, which the format file represents as `TERMINATOR="\r\n"`.  
  
```xml
<?xml version="1.0"?>  
<BCPFORMAT  
       xmlns="https://schemas.microsoft.com/sqlserver/2004/bulkload/format"  
       xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance">  
  <RECORD>  
    <FIELD ID="1" xsi:type="CharFixed" LENGTH="10"/>  
    <FIELD ID="2" xsi:type="CharFixed" LENGTH="6"/>  
    <FIELD ID="3" xsi:type="CharTerm" TERMINATOR="\r\n"/>  
  </RECORD>  
  <ROW>  
    <COLUMN SOURCE="1" NAME="C1" xsi:type="SQLINT" />  
    <COLUMN SOURCE="2" NAME="C2" xsi:type="SQLINT" />  
  </ROW>  
</BCPFORMAT>  
```  
  
###  <a name="AdditionalExamples"></a> Additional Examples  
 For additional examples of both non-XML format files and XML format files, see the following topics:  
  
-   [Use a Format File to Skip a Table Column &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)  
  
-   [Use a Format File to Skip a Data Field &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-data-field-sql-server.md)  
  
-   [Use a Format File to Map Table Columns to Data-File Fields &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)  
  
##  <a name="RelatedTasks"></a> Related Tasks  
  
-   [Create a Format File &#40;SQL Server&#41;](../../relational-databases/import-export/create-a-format-file-sql-server.md)  
  
-   [Use a Format File to Bulk Import Data &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-bulk-import-data-sql-server.md)  
  
-   [Use a Format File to Skip a Table Column &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-table-column-sql-server.md)  
  
-   [Use a Format File to Skip a Data Field &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-skip-a-data-field-sql-server.md)  
  
-   [Use a Format File to Map Table Columns to Data-File Fields &#40;SQL Server&#41;](../../relational-databases/import-export/use-a-format-file-to-map-table-columns-to-data-file-fields-sql-server.md)  
  
##  <a name="RelatedContent"></a> Related Content  
 None.  
  
## See Also  
 [Bulk Import and Export of Data &#40;SQL Server&#41;](../../relational-databases/import-export/bulk-import-and-export-of-data-sql-server.md)   
 [Data Types &#40;Transact-SQL&#41;](../../t-sql/data-types/data-types-transact-sql.md)   
 [Non-XML Format Files &#40;SQL Server&#41;](../../relational-databases/import-export/non-xml-format-files-sql-server.md)   
 [Format Files for Importing or Exporting Data &#40;SQL Server&#41;](../../relational-databases/import-export/format-files-for-importing-or-exporting-data-sql-server.md)  
  
  
