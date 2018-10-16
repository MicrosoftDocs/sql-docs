---
title: "Considering Database Features to Use | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "interoperability [ODBC], database features"
ms.assetid: 59760114-508e-46c5-81d2-8f2498c0d778
author: MightyPen
ms.author: genemi
manager: craigg
---
# Considering Database Features to Use
After the basic level of interoperability is known, the database features used by the application must be considered. For example, what SQL statements will the application execute? Will the application use scrollable cursors? Transactions? Procedures? Long data? For ideas about what features might not be supported by all DBMSs, see the [SQLGetInfo](../../../odbc/reference/syntax/sqlgetinfo-function.md), [SQLSetConnectAttr](../../../odbc/reference/syntax/sqlsetconnectattr-function.md), and [SQLSetStmtAttr](../../../odbc/reference/syntax/sqlsetstmtattr-function.md) function descriptions, and [Appendix C: SQL Grammar](../../../odbc/reference/appendixes/appendix-c-sql-grammar.md). The features required by an application might eliminate some DBMSs from the list of target DBMSs. They might also show that the application can easily target many DBMSs.  
  
 For example, if the required features are simple, they can usually be implemented with a high degree of interoperability. An application that executes a simple **SELECT** statement and retrieves results with a forward-only cursor is likely to be highly interoperable by virtue of its simplicity: Almost all drivers and DBMSs support the functionality it needs.  
  
 However, if the required features are more complex, such as scrollable cursors, positioned update and delete statements, and procedures, trade-offs must often be made. There are several possibilities:  
  
-   **Lower interoperability, more features.** The application includes the features but works only with DBMSs that support them.  
  
-   **Higher interoperability, fewer features.** The application drops the features but works with more DBMSs.  
  
-   **Higher interoperability, optional features.** The application includes the features but makes them available only with those DBMSs that support them.  
  
-   **Higher interoperability, more features.** The application uses the features with DBMSs that support them and emulates them for DBMSs that do not.  
  
 The first two cases are relatively simple to implement, because the features are used either with all supported DBMSs or with none. The latter two cases, on the other hand, are more complex. It is necessary in both cases to check whether the DBMS supports the features and in the last case to write a potentially large amount of code to emulate these features. Therefore, these schemes are likely to require more development time and may be slower at run time.  
  
 Consider a generic query application that can connect to a single data source. The application accepts a query from the user and displays the results in a window. Now suppose this application has one feature that allows users to display the results of multiple queries simultaneously. That is, they can execute a query and look at some of the results, execute a different query and look at some of its results, and then return to the first query. This presents an interoperability problem because some drivers support only a single active statement.  
  
 The application has a number of choices, based on what the driver returns for the SQL_MAX_CONCURRENT_ACTIVITIES option in **SQLGetInfo**:  
  
-   **Always support multiple queries.** After connecting to a driver, the application checks the number of active statements. If the driver supports only one active statement, the application closes the connection and informs the user that the driver does not support required functionality. The application is easy to implement and has full functionality but has lower interoperability.  
  
-   **Never support multiple queries.** The application drops the feature altogether. It is easy to implement and has high interoperability but has less functionality.  
  
-   **Support multiple queries only if the driver does.** After connecting to a driver, the application checks the number of active statements. The application allows the user to start a new statement when one is already active only if the driver supports multiple active statements. The application has higher functionality and interoperability but is harder to implement.  
  
-   **Always support multiple queries and emulate them when necessary.** After connecting to a driver, the application checks the number of active statements. The application always allows the user to start a new statement when one is already active. If the driver supports only one active statement, the application opens an additional connection to that driver and executes the new statement on that connection. The application has full functionality and high interoperability but is harder to implement.
