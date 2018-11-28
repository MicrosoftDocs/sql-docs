---
title: "The ODBC Solution | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC [ODBC], database access"
  - "SQL [ODBC], database access"
  - "database access [ODBC]"
  - "standardizing database access [ODBC], using ODBC"
ms.assetid: 34b80790-e010-4b90-8eaa-03189f5d8986
author: MightyPen
ms.author: genemi
manager: craigg
---
# The ODBC Solution
The question, then, is how does ODBC standardize database access? There are two architectural requirements:  
  
-   Applications must be able to access multiple DBMSs using the same source code without recompiling or relinking.  
  
-   Applications must be able to access multiple DBMSs simultaneously.  
  
 And there is one more question, due to marketplace reality:  
  
-   Which DBMS features should ODBC expose? Only features that are common to all DBMSs or any feature that is available in any DBMS?  
  
 ODBC solves these problems in the following manner:  
  
-   **ODBC is a call-level interface.** To solve the problem of how applications access multiple DBMSs using the same source code, ODBC defines a standard CLI. This contains all of the functions in the CLI specifications from Open Group and ISO/IEC and provides additional functions commonly required by applications.  
  
     A different library, or driver, is required for each DBMS that supports ODBC. The driver implements the functions in the ODBC API. To use a different driver, the application does not need to be recompiled or relinked. Instead, the application simply loads the new driver and calls the functions in it. To access multiple DBMSs simultaneously, the application loads multiple drivers. How drivers are supported is operating system-specific. For example, on the Microsoft® Windows® operating system, drivers are dynamic-link libraries (DLLs).  
  
-   **ODBC defines a standard SQL grammar.** In addition to a standard call-level interface, ODBC defines a standard SQL grammar. This grammar is based on the Open Group SQL CAE specification. Differences between the two grammars are minor and primarily due to the differences between the SQL grammar required by embedded SQL (Open Group) and a CLI (ODBC). There are also some extensions to the grammar to expose commonly available language features not covered by the Open Group grammar.  
  
     Applications can submit statements using ODBC or DBMS-specific grammar. If a statement uses ODBC grammar that is different from DBMS-specific grammar, the driver converts it before sending it to the data source. However, such conversions are rare because most DBMSs already use standard SQL grammar.  
  
-   **ODBC provides a Driver Manager to manage simultaneous access to multiple DBMSs.** Although the use of drivers solves the problem of accessing multiple DBMSs simultaneously, the code to do this can be complex. Applications that are designed to work with all drivers cannot be statically linked to any drivers. Instead, they must load drivers at run time and call the functions in them through a table of function pointers. The situation becomes more complex if the application uses multiple drivers simultaneously.  
  
     Rather than forcing each application to do this, ODBC provides a Driver Manager. The Driver Manager implements all of the ODBC functions - mostly as pass-through calls to ODBC functions in drivers - and is statically linked to the application or loaded by the application at run time. Thus, the application calls ODBC functions by name in the Driver Manager, rather than by pointer in each driver.  
  
     When an application needs a particular driver, it first requests a connection handle with which to identify the driver and then requests that the Driver Manager load the driver. The Driver Manager loads the driver and stores the address of each function in the driver. To call an ODBC function in the driver, the application calls that function in the Driver Manager and passes the connection handle for the driver. The Driver Manager then calls the function by using the address it stored earlier.  
  
-   **ODBC exposes a significant number of DBMS features but does not require drivers to support all of them.** If ODBC exposed only features that are common to all DBMSs, it would be of little use; after all, the reason so many different DBMSs exist today is that they have different features. If ODBC exposed every feature that is available in any DBMS, it would be impossible for drivers to implement.  
  
     Instead, ODBC exposes a significant number of features - more than are supported by most DBMSs - but requires drivers to implement only a subset of those features. Drivers implement the remaining features only if they are supported by the underlying DBMS or if they choose to emulate them. Thus, applications can be written to exploit the features of a single DBMS as exposed by the driver for that DBMS, to use only those features used by all DBMSs, or to check for support of a particular feature and react accordingly.  
  
     So that an application can determine what features a driver and DBMS support, ODBC provides two functions (**SQLGetInfo** and **SQLGetFunctions**) that return general information about the driver and DBMS capabilities and a list of functions the driver supports. ODBC also defines API and SQL grammar conformance levels, which specify broad ranges of features supported by the driver. For more information, see [Conformance Levels](../../odbc/reference/develop-app/conformance-levels.md).  
  
     It is important to remember that ODBC defines a common interface for all of the features it exposes. Because of this, applications contain feature-specific code, not DBMS-specific code, and can use any drivers that expose those features. One advantage of this is that applications do not need to be updated when the features supported by a DBMS are enhanced; instead, when an updated driver is installed, the application automatically uses the features because its code is feature-specific, not driver-specific or DBMS-specific.
