---
title: "Setup DLL API Reference | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "ODBC drivers [ODBC], driver setup DLL"
  - "driver setup DLL [ODBC]"
ms.assetid: f9d03f17-1c0d-4e7c-9c04-8c316e07ef25
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Setup DLL API Reference
This section describes the syntax of the driver setup DLL API,which consists of two functions (**ConfigDriver** and **ConfigDSN**). **ConfigDriver** and **ConfigDSN** can be either in the driver DLL or in a separate setup DLL.  
  
 In addition, this section describes the syntax of the translator setup DLL API, which consists of a single function (**ConfigTranslator**). **ConfigTranslator** can be either in the translator DLL or in a separate setup DLL.  
  
 Each function is labeled with the version of ODBC in which it was introduced.  
  
 This section contains the following topics.  
  
-   [ConfigDriver Function](../../../odbc/reference/syntax/configdriver-function.md)  
  
-   [ConfigDSN Function](../../../odbc/reference/syntax/configdsn-function.md)  
  
-   [ConfigTranslator Function](../../../odbc/reference/syntax/configtranslator-function.md)