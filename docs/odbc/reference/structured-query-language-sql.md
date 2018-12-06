---
title: "Structured Query Language (SQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL [ODBC]"
  - "SQL [ODBC], about SQL"
  - "ODBC [ODBC], SQL"
ms.assetid: bebfd93e-0dc0-46b3-a531-518beb7ea976
author: MightyPen
ms.author: genemi
manager: craigg
---
# Structured Query Language (SQL)
A typical DBMS allows users to store, access, and modify data in an organized, efficient way. Originally, the users of DBMSs were programmers. Accessing the stored data required writing a program in a programming language such as COBOL. While these programs were often written to present a friendly interface to a nontechnical user, access to the data itself required the services of a knowledgeable programmer. Casual access to the data was not practical.  
  
 Users were not entirely happy with this situation. While they could access data, it often required convincing a DBMS programmer to write special software. For example, if a sales department wanted to see the total sales in the previous month by each of its salespeople and wanted this information ranked in order by each salesperson's length of service in the company, it had two choices: Either a program already existed that allowed the information to be accessed in exactly this way, or the department had to ask a programmer to write such a program. In many cases, this was more work than it was worth, and it was always an expensive solution for one-time, or ad hoc, inquiries. As more and more users wanted easy access, this problem grew larger and larger.  
  
 Allowing users to access data on an ad hoc basis required giving them a language in which to express their requests. A single request to a database is defined as a query; such a language is called a query language. Many query languages were developed for this purpose, but one of these became the most popular: Structured Query Language, invented at IBM in the 1970s. It is more commonly known by its acronym, SQL, and is pronounced both as "ess-cue-ell" and as "sequel". SQL became an ANSI standard in 1986 and an ISO standard in 1987; it is used today in a great many database management systems.  
  
 Although SQL solved the ad hoc needs of users, the need for data access by computer programs did not go away. In fact, most database access still was (and is) programmatic, in the form of regularly scheduled reports and statistical analyses, data entry programs such as those used for order entry, and data manipulation programs, such as those used to reconcile accounts and generate work orders.  
  
 These programs also use SQL, using one of the following three techniques:  
  
-   **Embedded SQL**, in which SQL statements are embedded in a host language such as C or COBOL.  
  
-   **SQL modules**, in which SQL statements are compiled on the DBMS and called from a host language.  
  
-   **Call-level interface**, or CLI, which consists of functions called to pass SQL statements to the DBMS and to retrieve results from the DBMS.  
  
> [!NOTE]  
>  It is a historical accident that the term call-level interface is used instead of application programming interface (API), another term for the same thing. In the database world, API is used to describe SQL itself: SQL is the API to a DBMS.  
  
 Of these choices, embedded SQL is the most commonly used, although most major DBMSs support proprietary CLIs.  
  
 This section contains the following topics.  
  
-   [Processing an SQL Statement](../../odbc/reference/processing-a-sql-statement.md)  
  
-   [Embedded SQL](../../odbc/reference/embedded-sql.md)  
  
-   [SQL Modules](../../odbc/reference/sql-modules.md)  
  
-   [Call-Level Interfaces](../../odbc/reference/call-level-interfaces.md)
