---
title: "When to Use Procedures | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL statements [ODBC], procedures"
  - "procedures [ODBC], about procedures"
ms.assetid: 7dc9e327-dd54-4b10-9f66-9ef5c074f122
author: MightyPen
ms.author: genemi
manager: craigg
---
# When to Use Procedures
There are a number of advantages to using procedures, all based on the fact that using procedures moves SQL statements from the application to the data source. What is left in the application is an interoperable procedure call. These advantages include:  
  
-   **Performance** Procedures are usually the fastest way to execute SQL statements. Like prepared execution, the statement is compiled and executed in two separate steps. Unlike prepared execution, procedures are executed only at run time. They are compiled at a different time.  
  
-   **Business Rules** A *business rule* is a rule about the way in which a company does business. For example, only someone with the title Sales Person might be allowed to add new sales orders. Placing these rules in procedures allows individual companies to customize vertical applications by rewriting the procedures called by the application without having to modify the application code. For example, an order entry application might call the procedure **InsertOrder** with a fixed number of parameters; exactly how **InsertOrder** is implemented can vary from company to company.  
  
-   **Replaceability** Closely related to placing business rules in procedures is the fact that procedures can be replaced without recompiling the application. If a business rule changes after a company has bought and installed an application, the company can change the procedure containing that rule. From the application's standpoint, nothing has changed; it still calls a particular procedure to accomplish a particular task.  
  
-   **DBMS-specific SQL** Procedures provide a way for applications to exploit DBMS-specific SQL and still remain interoperable. For example, a procedure on a DBMS that supports control-of-flow statements in SQL might trap and recover from errors, while a procedure on a DBMS that does not support control-of-flow statements might simply return an error.  
  
-   **Procedures survive transactions** On some data sources, the access plans for all prepared statements on a connection are deleted when a transaction is committed or rolled back. By placing SQL statements in procedures, which are permanently stored in the data source, the statements survive the transaction. Whether the procedures survive in a prepared, partially prepared, or unprepared state is DBMS-specific.  
  
-   **Separate development** Procedures can be developed separately from the rest of the application. In large corporations, this might provide a way to further exploit the skills of highly specialized programmers. In other words, application programmers can write user-interface code and database programmers can write procedures.  
  
 Procedures are generally used by vertical and custom applications. These applications tend to perform fixed tasks, and it is possible to hard-code procedure calls in them. For example, an order entry application might call the procedures **InsertOrder**, **DeleteOrder**, **UpdateOrder**, and **GetOrders**.  
  
 There is little reason to call procedures from generic applications. Procedures are usually written to perform a task in the context of a particular application and so have no use to generic applications. For example, a spreadsheet has no reason to call the **InsertOrder** procedure just mentioned. Furthermore, generic applications should not construct procedures at run time in hopes of providing faster statement execution; not only is this likely to be slower than prepared or direct execution, it also requires DBMS-specific SQL statements.  
  
 An exception to this is application-development environments, which often provide a way for programmers to build SQL statements that execute procedures and may provide a way for programmers to test procedures. Such environments call **SQLProcedures** to list available procedures and **SQLProcedureColumns** to list the input, input/output, and output parameters, the procedure return value, and the columns of any result sets created by a procedure. However, such procedures must be developed beforehand on each data source; doing so requires DBMS-specific SQL statements.  
  
 There are three major disadvantages to using procedures. The first is that procedures must be written and compiled for each DBMS with which the application is to run. While this is not a problem for custom applications, it can significantly increase development and maintenance time for vertical applications designed to run with a number of DBMSs.  
  
 The second disadvantage is that many DBMSs do not support procedures. Again, this is most likely to be a problem for vertical applications designed to run with a number of DBMSs. To determine whether procedures are supported, an application calls **SQLGetInfo** with the SQL_PROCEDURES option.  
  
 The third disadvantage, which is particularly applicable to application development environments, is that ODBC does not define a standard grammar for creating procedures. That is, although applications can call procedures interoperably, they cannot create them interoperably.
