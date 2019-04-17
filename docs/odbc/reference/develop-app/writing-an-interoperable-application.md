---
title: "Writing an Interoperable Application | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "interoperability [ODBC], feature support and variability"
  - "interoperability [ODBC], writing interoperable applications"
  - "feature support in interoperable applications [ODBC]"
  - "feature variability in interoperable applications [ODBC]"
ms.assetid: 8b42b8ae-7862-4b63-a0b3-2a204e0c43a5
author: MightyPen
ms.author: genemi
manager: craigg
---
# Writing an Interoperable Application
Whenever an application uses the same code against more than one driver, that code must be interoperable among those drivers. In most cases, this is an easy task. For example, the code to fetch rows with a forward-only cursor is the same for all drivers. In some cases, this can be more difficult. For example, the code to construct identifiers for use in SQL statements needs to consider identifier case, quoting, and one-part, two-part, and three-part naming conventions.  
  
 In general, interoperable code must cope with problems of feature support and feature variability. *Feature support* refers to whether or not a particular feature is supported. For example, not all DBMSs support transactions, and interoperable code must work correctly regardless of transaction support. *Feature variability* refers to variation in the manner in which a particular feature is supported. For example, catalog names are placed at the start of identifiers in some DBMSs and at the end of identifiers in others.  
  
 Applications can deal with feature support and feature variability at design time or at run time. To deal with feature support and variability at design time, a developer looks at the target DBMSs and drivers and makes sure that the same code will be interoperable among them. This is generally the way in which applications with low or limited interoperability deal with these problems.  
  
 For example, if the developer guarantees that a vertical application will work only with four particular DBMSs and if each of those DBMSs supports transactions, the application does not need code to check for transaction support at run time. It can always assume transactions are available because of the design-time decision to use only four DBMSs, each of which supports transactions.  
  
 To deal with feature support and variability at run time, the application must test for different capabilities at run time and act accordingly. This is generally the way in which highly interoperable applications deal with these problems. For feature support problems, this means writing code that makes the feature optional or writing code that emulates the feature when it is not available. For feature variability problems, this means writing code that supports all possible variations.  
  
 This section contains the following topics.  
  
-   [Checking Feature Support and Variability](../../../odbc/reference/develop-app/checking-feature-support-and-variability.md)  
  
-   [Features to Watch For](../../../odbc/reference/develop-app/features-to-watch-for.md)
