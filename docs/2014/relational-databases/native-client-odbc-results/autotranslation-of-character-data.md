---
title: "Autotranslation of Character Data | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2014"
ms.reviewer: ""
ms.technology: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "result sets [ODBC], autotranslating character data"
  - "data types [ODBC], autotranslating character data"
  - "ACPs"
  - "SQL Server Native Client ODBC driver, result sets"
  - "ODBC applications, result sets"
  - "AutoTranslate feature"
  - "ANSI code pages"
  - "character data autotranslation [ODBC]"
  - "autotranslating character data"
  - "SQL Server Native Client ODBC driver, data types"
  - "ODBC data types, autotranslating character data"
ms.assetid: 86a8adda-c5ad-477f-870f-cb370c39ee13
author: MightyPen
ms.author: genemi
manager: craigg
---
# Autotranslation of Character Data
  Character data, such as ANSI character variables declared with SQL_C_CHAR or data stored in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] using the **char**, **varchar**, or **text** data types, can represent only a limited number of characters. Character data stored using one byte per character can only represent 256 characters. The values stored in SQL_C_CHAR variables are interpreted using the ANSI code page (ACP) of the client computer. The values stored using **char**, **varchar**, or **text** data types on the server are evaluated using the ACP of the server.  
  
 If both the server and the client have the same ACP, then they have no problems in interpreting the values stored in SQL_C_CHAR, **char**, **varchar**, or **text** objects. If the server and client have different ACPs, then SQL_C_CHAR data from the client may be interpreted as a different character on the server if it is used in **char**, **varchar**, or **text** columns, variables, or parameters. For example, a character byte containing the value 0xA5 is interpreted as the character ?? on a computer using code page 437 and is interpreted as the yen sign (??) on a computer running code page 1252.  
  
 Unicode data is stored using two bytes per character. All extended characters are covered by the Unicode specification, so all Unicode characters are interpreted the same by all computers.  
  
 The AutoTranslate feature of the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver attempts to minimize the problems in moving character data between a client and a server that have different code pages. AutoTranslate can be set in the connect string of [SQLDriverConnect](../native-client-odbc-api/sqldriverconnect.md), in the configuration string of [SQLConfigDataSource](../native-client-odbc-api/sqlconfigdatasource.md), or when configuring data sources for the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver using ODBC Administrator.  
  
 When AutoTranslate is set to "no", no conversions are done on data moved between SQL_C_CHAR variables on the client and **char**, **varchar**, or **text** columns, variables, or parameters in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database. The bit patterns may be interpreted differently on the client and server computers if the data contains extended characters and the two computers have different code pages. The data will be interpreted the same if both computers have the same code page.  
  
 When AutoTranslate is set to "yes", the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver uses Unicode to convert data moved between SQL_C_CHAR variables on the client and **char**, **varchar**, or **text** columns, variables, or parameters in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database:  
  
-   When data is sent from an SQL_C_CHAR variable on the client to a **char**, **varchar**, or **text** column, variable, or parameter in an [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database, the ODBC driver first converts from SQL_C_CHAR to Unicode using the ACP of the client, then from Unicode back to character using the ACP of the server.  
  
-   When data is sent from a **char**, **varchar**, or **text** column, variable, or parameter in a [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] database to a SQL_C_CHAR variable on the client, the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver first converts from character to Unicode using the ACP of the server, then from Unicode back to SQL_C_CHAR using the ACP of the client.  
  
 Because all of these conversions are done by the [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] Native Client ODBC driver executing on the client, the server ACP must be one of the code pages installed on the client computer.  
  
 Making the character conversions through Unicode ensures the proper conversion of all characters that exist in both code pages. If a character exists in one code page but not another, however, then the character cannot be represented in the target code page. For example, code page 1252 has the registered trademark symbol (??), while code page 437 does not.  
  
 The AutoTranslate setting has no effect on these conversions:  
  
-   Moving data between character SQL_C_CHAR client variables and Unicode **nchar**, **nvarchar**, or **ntext** columns, variables, or parameters in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.  
  
-   Moving data between Unicode SQL_C_WCHAR client variables and character **char**, **varchar**, or **text** columns, variables, or parameters in [!INCLUDE[ssNoVersion](../../includes/ssnoversion-md.md)] databases.  
  
 Data always must be converted when moved from character to Unicode.  
  
## See Also  
 [Processing Results &#40;ODBC&#41;](processing-results-odbc.md)   
 [Collation and Unicode Support](../collations/collation-and-unicode-support.md)  
  
  
