---
title: "Pattern Value Arguments | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "catalog functions [ODBC], arguments"
  - "arguments in catalog functions [ODBC], pattern value"
  - "pattern value arguments [ODBC]"
ms.assetid: 1d3f0ea6-87af-4836-807f-955e7df2b5df
author: MightyPen
ms.author: genemi
manager: craigg
---
# Pattern Value Arguments
Some arguments in the catalog functions, such as the *TableName* argument in **SQLTables**, accept search patterns. These arguments accept search patterns if the SQL_ATTR_METADATA_ID statement attribute is set to SQL_FALSE; they are identifier arguments that do not accept a search pattern if this attribute is set to SQL_TRUE.  
  
 The search pattern characters are:  
  
-   An underscore (_), which represents any single character.  
  
-   A percent sign (%), which represents any sequence of zero or more characters.  
  
-   An escape character, which is driver-specific and is used to include underscores, percent signs, and the escape character as literals. If the escape character precedes a non-special character, the escape character has no special meaning. If the escape character precedes a special character, it escapes the special character. For example, "\a" would be treated as two characters, "\\" and "a", but "\\%" would be treated as the non-special single character "%".  
  
 The escape character is retrieved with the SQL_SEARCH_PATTERN_ESCAPE option in **SQLGetInfo**. It must precede any underscore, percent sign, or escape character in an argument that accepts search patterns to include that character as a literal. Examples are shown in the following table.  
  
|Search pattern|Description|  
|--------------------|-----------------|  
|%A%|All identifiers containing the letter A|  
|ABC_|All four character identifiers starting with ABC|  
|ABC\\_|The identifier ABC_, assuming the escape character is a backslash (\\)|  
|\\\\%|All identifiers starting with a backslash (\\), assuming the escape character is a backslash|  
  
 Special care must be taken to escape search pattern characters in arguments that accept search patterns. This is particularly true for the underscore character, which is commonly used in identifiers. A common mistake in applications is to retrieve a value from one catalog function and pass that value to a search pattern argument in another catalog function. For example, suppose an application retrieves the table name MY_TABLE from the result set for **SQLTables** and passes this to **SQLColumns** to retrieve a list of columns in MY_TABLE. Instead of getting the columns for MY_TABLE, the application will get the columns for all the tables that match the search pattern MY_TABLE, such as MY_TABLE, MY1TABLE, MY2TABLE, and so on.  
  
> [!NOTE]
>  ODBC 2.*x* drivers do not support search patterns in the *CatalogName* argument in **SQLTables**. ODBC 3*.x* drivers accept search patterns in this argument if the SQL_ATTR_ ODBC_VERSION environment attribute is set to SQL_OV_ODBC3; they do not accept search patterns in this argument if it is set to SQL_OV_ODBC2.  
  
 Passing a null pointer to a search pattern argument does not constrain the search for that argument; that is, a null pointer and the search pattern % (any characters) are equivalent. However, a zero-length search pattern - that is, a valid pointer to a string of length zero - matches only the empty string ("").
