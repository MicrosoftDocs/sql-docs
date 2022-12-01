---
description: "Translation DLLs"
title: "Translation DLLs | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "translation DLLs [ODBC]"
ms.assetid: 38975059-b346-410f-bb27-326f3f7bbf39
author: David-Engel
ms.author: v-davidengel
---
# Translation DLLs
The application and data source often store data in different character sets. ODBC provides a generic mechanism that allows the driver to translate data from one character set to another. It consists of a DLL that implements the translation functions **SQLDriverToDataSource** and **SQLDataSourceToDriver**, which are called by the driver to translate all data flowing between the data source and driver. This DLL can be written by the application developer, the driver developer, or a third party.  
  
 The translation DLL for a particular data source can be specified in the system information for that data source; for more information, see [Data Source Specification Subkeys](../../../odbc/reference/install/data-source-specification-subkeys.md). It can also be set at run time with the SQL_ATTR_TRANSLATE_DLL and SQL_ATTR_TRANSLATE_OPTION connection attributes.  
  
 The translation option is a value that can be interpreted only by a particular translation DLL. For example, if the translation DLL translates between different code pages, the option might give the numbers of the code pages used by the application and the data source. There is no requirement for a translation DLL to use a translation option.  
  
 After a translation DLL has been specified, the driver loads it and calls it to translate all data flowing between the application and data source. This includes all SQL statements and character parameters being sent to the data source, and all character results, character metadata such as column names, and error messages retrieved from the data source. Connection data is not translated, because the translation DLL is not loaded until after the application has connected to the data source.
