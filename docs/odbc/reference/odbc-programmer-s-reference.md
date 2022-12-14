---
description: "ODBC Programmer's Reference"
title: "ODBC Programmer's Reference | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC [ODBC], reference"
ms.assetid: b33c3c43-ae66-44a3-be17-9cd82624dd96
author: David-Engel
ms.author: v-davidengel
---
# ODBC Programmer's Reference
The *ODBC Programmer's Reference* contains the following sections.  
  
-   [What's New in ODBC 3.8](../../odbc/reference/what-s-new-in-odbc-3-8.md) lists the new ODBC features that were added in the Windows 8 SDK.  
  
-   [Sample ODBC Program](../../odbc/reference/sample-odbc-program.md) presents a sample ODBC program.  
  
-   [Introduction to ODBC](../../odbc/reference/introduction-to-odbc.md) provides a brief history of Structured Query Language and ODBC, and conceptual information about the ODBC interface.  
  
-   [Developing Applications](../../odbc/reference/develop-app/developing-applications.md) contains information about developing applications that use the ODBC interface and drivers that implement it.  
  
-   [Installing and Configuring ODBC Software](../../odbc/reference/install/installing-and-configuring-the-odbc-software.md) provides information about installation and a setup DLL function reference.  
  
-   [Developing an ODBC Driver](../../odbc/reference/develop-driver/developing-an-odbc-driver.md) contains information on writing a driver.  
  
-   [API Reference](../../odbc/reference/syntax/odbc-reference.md) contains syntax and semantic information for all ODBC functions.  
  
-   [ODBC Appendixes](../../odbc/reference/appendixes/odbc-appendixes.md) contain technical details and reference tables for ODBC error codes, data types, and SQL grammar.  
  
## Working with the ODBC Documentation  
 The ODBC interface is designed for use with the C programming language. Use of the ODBC interface spans three areas: SQL statements, ODBC function calls, and C programming. This documentation assumes the following:  
  
-   A working knowledge of the C programming language.  
  
-   General DBMS knowledge and a familiarity with SQL.  
  
 The following typographic conventions are used.  
  
|Format|Used for|  
|------------|--------------|  
|SELECT * FROM|Uppercase letters indicate SQL statements, macro names, and terms used at the operating-system command level.|  
|`RETCODE SQLFetch(hdbc)`|The monospace font is used for sample command lines and program code.|  
|*argument*|Italicized words indicate programmatic arguments, information that the user or the application must provide, or word emphasis.|  
|**SQLEndTran**|Bold type indicates that syntax must be typed exactly as shown, including function names.|  
|&#124;|A vertical bar separates two mutually exclusive choices in a syntax line.|  
|...|An ellipsis indicates that arguments can be repeated several times.|  
|. . .|A column of three dots indicates continuation of previous lines of code.|  
  
## About the Code Examples  
 The code examples in this guide are designed for illustration purposes only. Because they are written primarily to demonstrate ODBC principles, efficiency has sometimes been set aside in the interest of clarity. In addition, whole sections of code have sometimes been omitted for clarity. These include the definitions of non-ODBC functions (those functions whose names do not start with "SQL") and most error handling.  
  
 All code examples use ANSI strings and the same database schema, which is shown at the start of [Catalog Functions](../../odbc/reference/develop-app/catalog-functions.md).  
  
## Recommended Reading  
 For more information about SQL, the following standards are available:  
  
-   Database Language - SQL with Integrity Enhancement, ANSI, 1989 ANSI X3.135-1989.  
  
-   Database Language - SQL: ANSI X3H2 and ISO/IEC JTC1/SC21/WG3 9075:1992 (SQL-92).  
  
-   Open Group, Data Management: Structured Query Language (SQL), Version 2 (The Open Group, 1996).  
  
 In addition to standards and vendor-specific SQL guides, many books describe SQL, including:  
  
-   Date, C. J., with Darwen, Hugh: *A Guide to the SQL Standard* (Addison-Wesley, 1993).  
  
-   Emerson, Sandra L., Darnovsky, Marcy, and Bowman, Judith S.: *The Practical SQL Handbook* (Addison-Wesley, 1989).  
  
-   Groff, James R. and Weinberg, Paul N.: *Using SQL* (Osborne McGraw-Hill, 1990).  
  
-   Gruber, Martin: *Understanding SQL* (Sybex, 1990).  
  
-   Hursch, Jack L. and Carolyn J.: *SQL, The Structured Query Language* (TAB Books, 1988).  
  
-   Melton, Jim, and Simon, Alan R.: *Understanding the New SQL: A Complete Guide* (Morgan Kaufmann Publishers, 1993).  
  
-   Pascal, Fabian: *SQL and Relational Basics* (M & T Books, 1990).  
  
-   Trimble, J. Harvey, Jr. and Chappell, David: *A Visual Introduction to SQL* (Wiley, 1989).  
  
-   Van der Lans, Rick F.: *Introduction to SQL* (Addison-Wesley, 1988).  
  
-   Vang, Soren: *SQL and Relational Databases* (Microtrend Books, 1990).  
  
-   Viescas, John: *Quick Reference Guide to SQL* (Microsoft Corp., 1989).  
  
 For additional information about transaction processing, see:  
  
-   Gray, J. N. and Reuter, Andreas: *Transaction Processing: Concepts and Techniques* (Morgan Kaufmann Publishers, 1993).  
  
-   Hackathorn, Richard D.: *Enterprise Database Connectivity* (Wiley & Sons, 1993).  
  
 For more information about Call-Level Interfaces, the following standards are available:  
  
-   Open Group, *Data Management: SQL Call Level Interface (CLI), C451* (Open Group, 1995).  
  
-   ISO/IEC 9075-3:1995, Call-Level Interface (SQL/CLI).  
  
 For additional information about ODBC, a number of books are available, including:  
  
-   Geiger, Kyle: *Inside ODBC* (Microsoft Press®, 1995).  
  
-   Gryphon, Robert, Charpentier, Luc, Oelschlager, Jon, Shoemaker, Andrew, Cross, Jim, and Lilley, Albert W.: *Using ODBC 2* (Que, 1994).  
  
-   Johnston, Tom and Osborne, Mark: *ODBC Developers Guide* (Howard W. Sams & Company, 1994).  
  
-   North, Ken: *Windows Multi-DBMS Programming: Using C++, Visual Basic, ODBC, OLE 2 and Tools for DBMS Projects* (John Wiley & Sons, Inc., 1995).  
  
-   Stegman, Michael O., Signore, Robert, and Creamer, John: *The ODBC Solution, Open Database Connectivity in Distributed Environments* (McGraw-Hill, 1995).  
  
-   Welch, Keith: *Using ODBC 2* (Que, 1994).  
  
-   Whiting, Bill: *Teach Yourself ODBC in Twenty-One Days* (Howard W. Sams & Company, 1994).
