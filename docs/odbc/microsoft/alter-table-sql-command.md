---
title: "ALTER TABLE - SQL Command | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "alter table [ODBC]"
ms.assetid: 3a01a291-f4d9-43bc-a725-5a95546ff364
author: MightyPen
ms.author: genemi
manager: craigg
---
# ALTER TABLE - SQL Command
Programmatically modifies the structure of a table.  
  
## Syntax  
  
```  
  
ALTER TABLE TableName1  
   ADD | ALTER [COLUMN] FieldName1  
      FieldType [(nFieldWidth [, nPrecision])]  
      [NULL | NOT NULL]  
      [CHECK lExpression1 [ERROR cMessageText1]]  
      [DEFAULT eExpression1]  
      [PRIMARY KEY | UNIQUE]  
      [REFERENCES TableName2 [TAG TagName1]]  
      [NOCPTRANS]  
 - Or -  
ALTER TABLE TableName1  
   ALTER [COLUMN] FieldName2  
      [NULL | NOT NULL]  
      [SET DEFAULT eExpression2]  
      [SET CHECK lExpression2 [ERROR cMessageText2]]  
      [DROP DEFAULT]  
      [DROP CHECK]  
 - Or -  
ALTER TABLE TableName1  
   [DROP [COLUMN] FieldName3]  
   [SET CHECK lExpression3 [ERROR cMessageText3]]  
   [DROP CHECK]  
   [ADD PRIMARY KEY eExpression3 TAG TagName2]  
   [DROP PRIMARY KEY]  
   [ADD UNIQUE eExpression4 [TAG TagName3]]  
   [DROP UNIQUE TAG TagName4]  
   [ADD FOREIGN KEY [eExpression5] TAG TagName4  
      REFERENCES TableName2 [TAG TagName5]]  
   [DROP FOREIGN KEY TAG TagName6 [SAVE]]  
   [RENAME COLUMN FieldName4 TO FieldName5]  
   [NOVALIDATE]  
```  
  
## Arguments  
 *TableName1*  
 Specifies the name of the table whose structure is modified.  
  
 ADD [COLUMN] *FieldName1*  
 Specifies the name of the field to add.  
  
 ALTER [COLUMN] *FieldName1*  
 Specifies the name of an existing field to modify.  
  
 *FieldType* [( *nFieldWidth* [, *nPrecision*]])  
 Specifies the field type, field width, and field precision (number of decimal places) for a new or modified field.  
  
 *FieldType* is a single letter that indicates the field's [data type](../../odbc/microsoft/visual-foxpro-field-data-types.md). Some field data types require that you specify *nFieldWidth* or *nPrecision* or both.  
  
 *nFieldWidth* and *nPrecision* are ignored for D, G, I, L, M, P, T, and Y types. By default, *nPrecision* is zero (no decimal places) if *nPrecision* is not included for the B, F, or N types.  
  
 NULL &#124; NOT NULL  
 Allows or prevents null values in the field.  
  
 If you omit NULL and NOT NULL, the current setting of SET NULL determines whether null values are allowed in the field. However, if you omit NULL and NOT NULL and include the PRIMARY KEY or UNIQUE clause, the current setting of SET NULL is ignored and the field is NOT NULL by default.  
  
 CHECK *lExpression1*  
 Specifies a validation rule for the field. *lExpression1* must evaluate to a logical expression and can be a user-defined function or a stored procedure. Whenever a blank record is appended, the validation rule is checked. An error is generated if the validation rule does not allow for a blank field value in an appended record.  
  
 ERROR *cMessageText1*  
 Specifies the error message displayed when the field validation rule generates an error.  
  
 DEFAULT *eExpression1*  
 Specifies a default value for the field. The data type of *eExpression1* must be the same as the data type for the field.  
  
 PRIMARY KEY  
 Creates a primary index tag. The index tag has the same name as the field.  
  
 UNIQUE  
 Creates a candidate index tag with the same name as the field.  
  
> [!NOTE]  
>  Candidate indexes (created by including the UNIQUE option, provided for ANSI compatibility in ALTER TABLE or CREATE TABLE) differ from indexes created by using the UNIQUE option in the INDEX command. An index created by using UNIQUE in the INDEX command allows duplicate index keys; candidate indexes do not allow duplicate index keys.  
  
 Null values and duplicate records are not permitted in a field that is used for a primary or candidate index.  
  
 If you are creating a new field by using ADD COLUMN, Visual FoxPro will not generate an error if you create a primary or candidate index for a field that supports null values. However, Visual FoxPro will generate an error if you try to enter a null or duplicate value into a field that is used for a primary or candidate index.  
  
 If you are modifying an existing field and the primary or candidate index expression consists of fields in the table, Visual FoxPro checks the fields to see whether they contain null values or duplicate records. If they do, Visual FoxPro generates an error and the table is not altered.  
  
 REFERENCES *TableName2* TAG *TagName1*  
 Specifies the parent table to which a persistent relationship is established. TAG *TagName1* specifies the parent table's index tag on which the relationship is based. Index tag names can contain up to 10 characters.  
  
 NOCPTRANS  
 Prevents translation to a different code page for character and memo fields. If the table is converted to another code page, the fields for which NOCPTRANS has been specified are not translated. NOCPTRANS can be specified only for character and memo fields.  
  
 The following example creates a table named mytable that contains two character fields and two memo fields. The second character field, char2, and the second memo field, memo2, include NOCPTRANS to prevent translation.  
  
```  
CREATE TABLE mytable (char1 C(10), char2 C(10) NOCPTRANS,;  
   memo1 M, memo2 M NOCPTRANS)  
```  
  
 ALTER [COLUMN] *FieldName2*  
 Specifies the name of an existing field to modify.  
  
 SET DEFAULT *eExpression2*  
 Specifies a new default value for an existing field. The data type of *eExpression2* must be the same as the data type for the field.  
  
 SET CHECK *lExpression2*  
 Specifies a new validation rule for an existing field. *lExpression2* must evaluate to a logical expression and may be a user-defined function or a stored procedure.  
  
 ERROR *cMessageText2*  
 Specifies the error message displayed when the field validation rule generates an error. The message is displayed only when data is changed within a Browse or Edit window.  
  
 DROP DEFAULT  
 Removes the default value for an existing field.  
  
 DROP CHECK  
 Removes the validation rule for an existing field.  
  
 DROP [COLUMN] *FieldName3*  
 Specifies a field to remove from the table. Removing a field from the table also removes the field's default value setting and field validation rule.  
  
 If index key or trigger expressions reference the field, the expressions become invalid when the field is removed. In this case, an error is not generated when the field is removed but the invalid index key or trigger expressions will generate errors at run time.  
  
 SET CHECK *lExpression3*  
 Specifies the table validation rule. *lExpression3* must evaluate to a logical expression and may be a user-defined function or a stored procedure.  
  
 ERROR *cMessageText3*  
 Specifies the error message displayed when the table validation rule generates an error. The message is displayed only when data is changed within a Browse or Edit window.  
  
 DROP CHECK  
 Removes the table's validation rule.  
  
 ADD PRIMARY KEY *eExpression3*TAG *TagName2*  
 Adds a primary index to the table. *eExpression3* specifies the primary index key expression, and *TagName2* specifies the name of the primary index tag. Index tag names can contain up to 10 characters. If TAG *TagName2* is omitted and *eExpression3* is a single field, the primary index tag has the same name as the field specified in *eExpression3*.  
  
 DROP PRIMARY KEY  
 Removes the primary index and its index tag. Because a table can have only one primary key, it is not necessary to specify the name of the primary key. Removing the primary index also deletes any persistent relations based on the primary key.  
  
 ADD UNIQUE *eExpression4*[TAG *TagName3*]  
 Adds a candidate index to the table. *eExpression4* specifies the candidate index key expression, and *TagName3* specifies the name of the candidate index tag. Index tag names can contain up to 10 characters. If you omit TAG *TagName3* and if *eExpression4* is a single field, the candidate index tag has the same name as the field specified in *eExpression4*.  
  
 DROP UNIQUE TAG *TagName4*  
 Removes the candidate index and its index tag. Because a table can have multiple candidate keys, you must specify the name of the candidate index tag.  
  
 ADD FOREIGN KEY [ *eExpression5*]TAG *TagName4*  
 Adds a foreign (nonprimary) index to the table. *eExpression5* specifies the foreign index key expression, and *TagName4* specifies the name of the foreign index tag. Index tag names can contain up to 10 characters.  
  
 REFERENCES *TableName2*[TAG *TagName5*]  
 Specifies the parent table to which a persistent relationship is established. Include TAG *TagName5* to establish a relation based on an existing index tag for the parent table. Index tag names can contain up to 10 characters. If you omit TAG *TagName5*, the relationship is established using the parent table's primary index tag.  
  
 DROP FOREIGN KEY TAG *TagName6*[SAVE]  
 Deletes a foreign key whose index tag is *TagName6*. If you omit SAVE, the index tag is deleted from the structural index. Include SAVE to prevent deletion of the index tag from the structural index.  
  
 RENAME COLUMN *FieldName4*TO *FieldName5*  
 Allows you to change the name of a field in the table. *FieldName4* specifies the name of the field that is renamed. *FieldName5* specifies the new name of the field.  
  
> [!CAUTION]  
>  Exercise care when renaming table fields because index expressions, field and table validation rules, commands, and functions may reference the original field names.  
  
 NOVALIDATE  
 Specifies that Visual FoxPro allows changes to be made to the structure of the table; these changes might violate the integrity of the data in the table. By default, Visual FoxPro prevents ALTER TABLE from making changes that violate the integrity of the data in the table. Include NOVALIDATE to override this default behavior.  
  
## Remarks  
 ALTER TABLE can be used to modify the structure of a table that has not been added to a database. However, Visual FoxPro generates an error if you include the DEFAULT, FOREIGN KEY, PRIMARY KEY, REFERENCES, or SET clauses when modifying a free table.  
  
 ALTER TABLE may rebuild the table by creating a new table header and appending records to the table header. For example, changing a field's type or width might cause the table to be rebuilt.  
  
 After a table is rebuilt, field validation rules are executed for any fields whose type or width is changed. If you change the type or width of any field in the table, the table rule is executed.  
  
 If you modify field or table validation rules for a table that has records, Visual FoxPro tests the new field or table validation rules against the existing data and issues a warning on the first occurrence of a field or table validation rule or of a trigger violation.  
  
 If the table you modify is in a database, ALTER TABLE - SQL requires exclusive use of the database. To open a database for exclusive use, include EXCLUSIVE in OPEN DATABASE.  
  
## See Also  
 [CREATE TABLE - SQL Command](../../odbc/microsoft/create-table-sql-command.md)   
 [INDEX Command](../../odbc/microsoft/index-command.md)
