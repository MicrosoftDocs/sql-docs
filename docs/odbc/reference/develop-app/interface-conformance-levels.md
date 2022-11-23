---
description: "Interface Conformance Levels"
title: "Interface Conformance Levels | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "interface conformance levels [ODBC]"
  - "conformance levels [ODBC], interface"
  - "data sources [ODBC], conformance levels"
  - "ODBC drivers [ODBC], conformance levels"
ms.assetid: 2c470e54-0600-4b2b-b1f3-9885cb28a01a
author: David-Engel
ms.author: v-davidengel
---
# Interface Conformance Levels
The purpose of leveling is to inform the application what features are available to it from the driver. A leveling scheme based on functions does not sufficiently achieve this goal. In ODBC 3.*x*, drivers are classified based on the features they possess. Supporting the feature can include supporting the function; it can also include supporting a descriptor field, a statement attribute, a "Y" value for an information type returned by **SQLGetInfo**, and so on.  
  
 To simplify specification of interface conformance, ODBC defines three conformance levels. To meet a particular conformance level, a driver must satisfy all of the requirements of that conformance level. Conformance with a given level implies complete conformance with all lower levels.  
  
 Conformance levels do not always divide neatly into support for a specific list of ODBC functions, but specify supported features as listed in the following sections. To provide support for a feature, a driver must support some or all forms of calls to certain ODBC functions (for more information, see [Function Conformance](../../../odbc/reference/develop-app/function-conformance.md)), setting certain attributes (see [Attribute Conformance](../../../odbc/reference/develop-app/attribute-conformance.md)), and certain descriptor fields (see [Descriptor Field Conformance](../../../odbc/reference/develop-app/descriptor-field-conformance.md)).  
  
 The application discovers a driver's interface conformance level by connecting to a data source and calling **SQLGetInfo** with the SQL_ODBC_INTERFACE_CONFORMANCE option.  
  
 Drivers are free to implement features beyond the level to which they claim complete conformance. Applications discover any such additional capabilities by calling **SQLGetFunctions** (to determine which ODBC functions are present) and **SQLGetInfo** (to query various other ODBC capabilities).  
  
 There are three ODBC interface conformance levels: Core, Level 1, and Level 2.  
  
> [!NOTE]
>  These conformance levels have different requirements than the ODBC API conformance levels of the same name in ODBC 2*.x*. In particular, all the features implied by ODBC 2*.x* API conformance Level 1 are now part of the Core interface conformance level. As a result, many ODBC drivers may report Core-level interface conformance.  
  
 This section contains the following topics.  
  
-   [Core Interface Conformance](../../../odbc/reference/develop-app/core-interface-conformance.md)  
  
-   [Level 1 Interface Conformance](../../../odbc/reference/develop-app/level-1-interface-conformance.md)  
  
-   [Level 2 Interface Conformance](../../../odbc/reference/develop-app/level-2-interface-conformance.md)  
  
-   [Function Conformance](../../../odbc/reference/develop-app/function-conformance.md)  
  
-   [Attribute Conformance](../../../odbc/reference/develop-app/attribute-conformance.md)  
  
-   [Descriptor Field Conformance](../../../odbc/reference/develop-app/descriptor-field-conformance.md)
