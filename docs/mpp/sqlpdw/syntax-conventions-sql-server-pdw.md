---
title: "Syntax Conventions (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 23bf3377-1b93-42e1-baeb-1156138ccd3d
caps.latest.revision: 18
author: BarbKess
---
# Syntax Conventions (SQL Server PDW)
Describes syntax conventions and multipart name rules that are used in the syntax diagrams in the SQL Reference topics.  
  
## Contents  
  
-   [Syntax Conventions](#SyntaxConventions)  
  
-   [Multipart Names](#MultipartNames)  
  
## <a name="SyntaxConventions"></a>Syntax Conventions  
The following table lists and describes conventions that are used in the syntax diagrams in the SQL Reference.  
  
|Convention|Used For|  
|--------------|------------|  
|UPPERCASE|SQL keywords.|  
|*italic*|User-supplied parameters of SQL syntax.|  
|**bold**|Database names, table names, column names, index names, data type names, and text that must be typed exactly as shown.|  
|**underline**|Indicates the default value applied when the clause that contains the underlined value is omitted from the statement.|  
|&#124; (vertical bar)|Separates alternative syntax items enclosed in brackets or braces. You can use only one of the items.|  
|[ ] (brackets)|Optional syntax items. Do not type the brackets.|  
|{ } (braces)|Required syntax items. Do not type the braces.|  
|[**,**...*n*]|Indicates that the preceding item can be repeated *n* number of times. The occurrences are separated by commas.|  
|[...*n*]|Indicates the preceding item can be repeated *n* number of times. The occurrences are separated by blanks.|  
|;|SQL statement terminator. The semicolon is optional except to separate statements within multi-statement transactions.|  
|<label> ::=|The name for a block of syntax. This convention is used to group and label sections of lengthy syntax or a unit of syntax that can be used in more than one location within a statement. Each location in which the block of syntax can be used is indicated with the label enclosed in angle brackets: <label>.<br /><br />A set is a collection of expressions, for example <grouping set>; and a list is a collection of sets, for example <composite element list>.|  
  
## <a name="MultipartNames"></a>Multipart Names  
Unless specified otherwise, all references to the name of a SQL Server PDW database object can be a three-part name:  
  
*database_name***.***schema_name***.***object_name*  
  
The name can be referenced using any of the following:  
  
*object_name | database_name..object_name | schema_name.object_name | database_name.schema_name.object_name*  
  
*database_name*  
Specifies the name of a SQL Server PDW database.  
  
*schema_name*  
Refers to the name of the current schema.  
  
*object_name*  
Refers to the name of the object.  
  
## See Also  
[Common Metadata Query Examples &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/common-metadata-query-examples-sql-server-pdw.md)  
  
