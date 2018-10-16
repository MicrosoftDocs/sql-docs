---
title: "Embedded SQL Example | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "SQL [ODBC], embedded SQL"
  - "SQL statements [ODBC], embedded SQL"
  - "embedded SQL [ODBC]"
ms.assetid: b8a26e05-3c82-4c5f-8f01-9de0edb645e9
author: MightyPen
ms.author: genemi
manager: craigg
---
# Embedded SQL Example
The following code is a simple embedded SQL program, written in C. The program illustrates many, but not all, of the embedded SQL techniques. The program prompts the user for an order number, retrieves the customer number, salesperson, and status of the order, and displays the retrieved information on the screen.  
  
```  
int main() {  
   EXEC SQL INCLUDE SQLCA;  
   EXEC SQL BEGIN DECLARE SECTION;  
      int OrderID;         /* Employee ID (from user)         */  
      int CustID;            /* Retrieved customer ID         */  
      char SalesPerson[10]   /* Retrieved salesperson name      */  
      char Status[6]         /* Retrieved order status        */  
   EXEC SQL END DECLARE SECTION;  
  
   /* Set up error processing */  
   EXEC SQL WHENEVER SQLERROR GOTO query_error;  
   EXEC SQL WHENEVER NOT FOUND GOTO bad_number;  
  
   /* Prompt the user for order number */  
   printf ("Enter order number: ");  
   scanf_s("%d", &OrderID);  
  
   /* Execute the SQL query */  
   EXEC SQL SELECT CustID, SalesPerson, Status  
      FROM Orders  
      WHERE OrderID = :OrderID  
      INTO :CustID, :SalesPerson, :Status;  
  
   /* Display the results */  
   printf ("Customer number:  %d\n", CustID);  
   printf ("Salesperson: %s\n", SalesPerson);  
   printf ("Status: %s\n", Status);  
   exit();  
  
query_error:  
   printf ("SQL error: %ld\n", sqlca->sqlcode);  
   exit();  
  
bad_number:  
   printf ("Invalid order number.\n");  
   exit();  
}  
```  
  
 Note the following about this program:  
  
-   **Host Variables** The host variables are declared in a section enclosed by the **BEGIN DECLARE SECTION** and **END DECLARE SECTION** keywords. Each host variable name is prefixed by a colon (:) when it appears in an embedded SQL statement. The colon allows the precompiler to distinguish between host variables and database objects, such as tables and columns, that have the same name.  
  
-   **Data Types** The data types supported by a DBMS and a host language can be quite different. This affects host variables because they play a dual role. On one hand, host variables are program variables, declared and manipulated by host language statements. On the other hand, they are used in embedded SQL statements to retrieve database data. If there is no host language type that corresponds to a DBMS data type, the DBMS automatically converts the data. However, because each DBMS has its own rules and idiosyncrasies associated with the conversion process, the host variable types must be chosen carefully.  
  
-   **Error Handling** The DBMS reports run-time errors to the applications program through an SQL Communications Area, or SQLCA. In the preceding code example, the first embedded SQL statement is INCLUDE SQLCA. This tells the precompiler to include the SQLCA structure in the program. This is required whenever the program will process errors returned by the DBMS. The WHENEVER...GOTO statement tells the precompiler to generate error-handling code that branches to a specific label when an error occurs.  
  
-   **Singleton SELECT** The statement used to return the data is a singleton SELECT statement; that is, it returns only a single row of data. Therefore, the code example does not declare or use cursors.
