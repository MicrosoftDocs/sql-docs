---
description: "CREATE TABLE - SQL Command"
title: "CREATE TABLE - SQL Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "CREATE TABLE [ODBC]"
ms.assetid: be2143ba-fc16-42c9-84f7-8985cd924860
author: David-Engel
ms.author: v-davidengel
---
# CREATE TABLE - SQL Command
Creates a table having the specified fields.  
  
 The Visual FoxPro ODBC Driver supports the native Visual FoxPro language syntax for this command. For driver-specific information, see **Driver Remarks**.  
  
## Syntax  
  
```  
  
CREATE TABLE | DBF TableName1 [NAME LongTableName] [FREE]  
   (FieldName1FieldType [(nFieldWidth [, nPrecision])]  
      [NULL | NOT NULL]   
      [CHECK lExpression1 [ERROR cMessageText1]]  
      [DEFAULT eExpression1]  
      [PRIMARY KEY | UNIQUE]  
      [REFERENCES TableName2 [TAG TagName1]]  
      [NOCPTRANS]  
   [, FieldName2 ...]  
      [, PRIMARY KEY eExpression2 TAG TagName2  
      |, UNIQUE eExpression3 TAG TagName3]  
      [, FOREIGN KEY eExpression4 TAG TagName4 [NODUP]  
            REFERENCES TableName3 [TAG TagName5]]  
      [, CHECK lExpression2 [ERROR cMessageText2]])  
| FROM ARRAY ArrayName  
```  
  
## Arguments  
 CREATE TABLE &#124; DBF *TableName1*  
 Specifies the name of the table to create. The TABLE and DBF options are identical.  
  
 NAME *LongTableName*  
 Specifies a long name for the table. A long table name can be specified only when a database is open, because long table names are stored in databases.  
  
 Long names can contain up to 128 characters and can be used in place of short file names in the database.  
  
 FREE  
 Specifies that the table will not be added to an open database. FREE isn't required if a database isn't open.  
  
 *(FieldName1 FieldType* [( *nFieldWidth* [, *nPrecision*])]  
 Specifies the field name, field type, field width, and field precision (number of decimal places), respectively.  
  
 *FieldType* is a single letter indicating the field's [data type](../../odbc/microsoft/visual-foxpro-field-data-types.md). Some field data types require that you specify *nFieldWidth* or *nPrecision* or both.  
  
 *nFieldWidth* and *nPrecision* are ignored for D, G, I, L, M, P, T, and Y types. *nPrecision* defaults to zero (no decimal places) if *nPrecision* isn't included for the B, F, or N types.  
  
 NULL  
 Allows null values in the field.  
  
 NOT NULL  
 Prevents null values in the field.  
  
 If you omit NULL and NOT NULL, the current setting of SET NULL determines whether null values are allowed in the field. However, if you omit NULL and NOT NULL and include the PRIMARY KEY or UNIQUE clause, the current setting of SET NULL is ignored and the field defaults to NOT NULL.  
  
 CHECK *lExpression1*  
 Specifies a validation rule for the field. *lExpression1* can be a user-defined function. Whenever a blank record is appended, the validation rule is checked. An error is generated if the validation rule doesn't allow for a blank field value in an appended record.  
  
 ERROR *cMessageText1*  
 Specifies the error message Visual FoxPro displays when the field rule generates an error. The message is displayed only when data is changed within a Browse window or an Edit window.  
  
 DEFAULT *eExpression1*  
 Specifies a default value for the field. The data type of *eExpression1* must be the same as the field's data type.  
  
 PRIMARY KEY  
 Creates a primary index for the field. The primary index tag has the same name as the field.  
  
 UNIQUE  
 Creates a candidate index for the field. The candidate index tag has the same name as the field.  
  
> [!NOTE]  
>  Candidate indexes (created by including the UNIQUE option in CREATE TABLE or ALTER TABLE - SQL) are not the same as indexes created with the UNIQUE option in the INDEX command. An index created with the UNIQUE option in the INDEX command allows duplicate index keys; candidate indexes do not allow duplicate index keys. See [INDEX](../../odbc/microsoft/index-command.md) for additional information on its UNIQUE option.  
  
 Null values and duplicate records are not permitted in a field used for a primary or candidate index. However, Visual FoxPro will not generate an error if you create a primary or candidate index for a field that supports null values. Visual FoxPro will generate an error if you attempt to enter a null or duplicate value into a field used for a primary or candidate index.  
  
 REFERENCES *TableName2*[TAG *TagName1*]  
 Specifies the parent table to which a persistent relationship is established. If you omit TAG *TagName1*, the relationship is established using the primary index key of the parent table. If the parent table does not have a primary index, Visual FoxPro generates an error.  
  
 Include TAG *TagName1* to establish a relation based on an existing index tag for the parent table. Index tag names can contain up to 10 characters.  
  
 The parent table cannot be a free table.  
  
 NOCPTRANS  
 Prevents translation to a different code page for character and memo fields. If the table is converted to another code page, the fields for which NOCPTRANS has been specified are not translated. NOCPTRANS can be specified only for character and memo fields.  
  
 The following example creates a table named mytable containing two character fields and two memo fields. The second character field, char2, and the second memo field, memo2, include NOCPTRANS to prevent translation.  
  
```  
CREATE TABLE mytable (char1 C(10), char2 C(10) NOCPTRANS,;  
   memo1 M, memo2 M NOCPTRANS)  
```  
  
 PRIMARY KEY *eExpression2* TAG *TagName2*  
 Specifies a primary index to create. *eExpression2* specifies any field or combination of fields in the table. TAG *TagName2* specifies the name for the primary index tag that is created. Index tag names can contain up to 10 characters.  
  
 Because a table can have only one primary index, you cannot include this clause if you have already created a primary index for a field. Visual FoxPro generates an error if you include more than one PRIMARY KEY clause in CREATE TABLE.  
  
 UNIQUE *eExpression3*TAG *TagName3*  
 Creates a candidate index. *eExpression3* specifies any field or combination of fields in the table. However, if you have created a primary index with one of the PRIMARY KEY options, you cannot include the field that was specified for the primary index. TAG *TagName3* specifies a tag name for the candidate index tag that is created. Index tag names can contain up to 10 characters.  
  
 A table can have multiple candidate indexes.  
  
 FOREIGN KEY *eExpression4*TAG *TagName4*[NODUP]  
 Creates a foreign (nonprimary) index and establishes a relationship to a parent table. *eExpression4* specifies the foreign index key expression, and *TagName4* specifies the name of the foreign index key tag that is created. Index tag names can contain up to 10 characters. Include NODUP to create a candidate foreign index.  
  
 You can create multiple foreign indexes for the table, but the foreign index expressions must specify different fields in the table.  
  
 REFERENCES *TableName3*[TAG *TagName5*]  
 Specifies the parent table to which a persistent relationship is established. Include TAG *TagName5* to establish a relation based on an index tag for the parent table. Index tag names can contain up to 10 characters. By default, if you omit TAG *TagName5,* the relationship is established using the parent table's primary index key.  
  
 CHECK *eExpression2*[ERROR *cMessageText2*]  
 Specifies the table validation rule. ERROR *cMessageText2* specifies the error message Visual FoxPro displays when the table validation rule is executed. The message is displayed only when data is changed within a Browse window or Edit window.  
  
 FROM ARRAY *ArrayName*  
 Specifies the name of an existing array whose contents are the name, type, precision, and scale for each field in the table. The contents of the array can be defined with the **AFIELDS**( ) function.  
  
## Remarks  
 The new table is opened in the lowest available work area and can be accessed by its alias. The new table is opened exclusively, regardless of the current setting of SET EXCLUSIVE.  
  
 If a database is open and you don't include the FREE clause, the new table is added to the database. You cannot create a new table with the same name as a table in the database.  
  
 If a database is open, CREATE TABLE - SQL requires exclusive use of the database. To open a database for exclusive use, include EXCLUSIVE in OPEN DATABASE.  
  
 If a database isn't open when you create the new table, including the NAME, CHECK, DEFAULT, FOREIGN KEY, PRIMARY KEY, or REFERENCES clauses generates an error.  
  
> [!NOTE]  
>  CREATE TABLE syntax uses commas to separate certain CREATE TABLE options. Also, the NULL, NOT NULL, CHECK, DEFAULT, PRIMARY KEY, and UNIQUE clause must be placed within the parentheses containing the column definitions.  
  
## Driver Remarks  
 When your application sends the ODBC SQL statement CREATE TABLE to the data source, the Visual FoxPro ODBC Driver translates the command into the Visual FoxProCREATE TABLE command using the syntax shown in the following table.  
  
|ODBC syntax|Visual FoxPro syntax|  
|-----------------|--------------------------|  
|CREATE TABLE *base-table-name*<br /><br /> (*column-identifier data type*<br /><br /> [NOT NULL]<br /><br /> [,*column-identifier data type*<br /><br /> [NOT NULL] ...)|CREATE TABLE *TableName1* [NAME *LongTableName*]<br /><br /> (*FieldName1* *FieldType*<br /><br /> [(*nFieldWidth* [, *nPrecision*])]<br /><br /> [NOT NULL])|  
  
 When you create a table using the driver, the driver closes the table immediately after creation to allow access to the table by other users. This differs from Visual FoxPro, which leaves the table open exclusively upon creation. However, if a stored procedure on your data source containing a CREATE TABLE statement executes, the table is left open.  
  
 If the data source is a database (.dbc file), the Visual FoxPro ODBC Driver creates a table named *LongTableName* with the same name as the *base-table-name*.  
  
### Using Data Definition Language (DDL)  
 You cannot include DDL in the following places:  
  
-   In a batch SQL statement that requires a transaction  
  
-   In manual-commit mode, after a statement that required a transaction, unless your application first calls **SQLTransact**.  
  
 For example, if you want to create a temporary table, you should create the table before you begin the statement requiring a transaction. If you include the CREATE TABLE statement in a batch SQL statement that requires a transaction, the driver returns an error message.  
  
## See Also  
 [ALTER TABLE - SQL Command](../../odbc/microsoft/alter-table-sql-command.md)   
 [Supported Data Types (Visual FoxPro ODBC Driver)](../../odbc/microsoft/supported-data-types-visual-foxpro-odbc-driver.md)   
 [INSERT - SQL Command](../../odbc/microsoft/insert-sql-command.md)   
 [SELECT - SQL Command](../../odbc/microsoft/select-sql-command.md)
