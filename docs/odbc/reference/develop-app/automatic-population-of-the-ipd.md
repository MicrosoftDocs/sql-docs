---
title: "Automatic Population of the IPD | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "automatically populating ipd [ODBC]"
  - "descriptors [ODBC], automatically populating ipd"
  - "descriptors [ODBC], allocating and freeing"
  - "ipd [ODBC]"
  - "allocating and freeing descriptors [ODBC]"
ms.assetid: 1184a7d8-d557-4140-843b-6633ae6deacc
author: MightyPen
ms.author: genemi
manager: craigg
---
# Automatic Population of the IPD
Some drivers are capable of setting the fields of the IPD after a parameterized query has been prepared. The descriptor fields are automatically populated with information about the parameter, including the data type, precision, scale, and other characteristics. This is equivalent to supporting **SQLDescribeParam**. This information can be particularly valuable to an application when it has no other way to discover it, such as when an ad hoc query is performed with parameters that the application does not know about.  
  
 An application determines whether the driver supports automatic population by calling **SQLGetConnectAttr** with an *Attribute* of SQL_ATTR_AUTO_IPD. If SQL_TRUE is returned, the driver supports it and the application can enable it by setting the SQL_ATTR_ENABLE_AUTO_IPD statement attribute to SQL_TRUE.  
  
 When automatic population is supported and enabled, the driver populates the fields of the IPD after an SQL statement containing parameter markers has been prepared by a call to **SQLPrepare**. An application can retrieve this information by calling **SQLGetDescField** or **SQLGetDescRec**, or **SQLDescribeParam**. The application can use the information to bind the most appropriate application buffer for a parameter or to specify a data conversion for it.  
  
 Automatic population of the IPD might produce a performance penalty. An application can turn it off by resetting the SQL_ATTR_ENABLE_AUTO_IPD statement attribute to SQL_FALSE (the default value).
