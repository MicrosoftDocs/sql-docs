---
title: "What is a Cursor?"
description: "What is a Cursor?"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
ms.custom: intro-overview
helpviewer_keywords:
  - "cursors [ADO], about cursors"
---
# What is a Cursor?
Operations in a relational database act on a complete set of rows. The set of rows returned by a SELECT statement consists of all the rows that satisfy the conditions in the WHERE clause of the statement. This complete set of rows returned by the statement is known as the result set. Applications, especially those that are interactive and online, cannot always work effectively with the entire result set as a unit. These applications need a mechanism to work with one row or a small block of rows at a time. Cursors are an extension to result sets that provide that mechanism.  
  
 A cursor is implemented by a cursor library. A cursor library is software, often implemented as a part of a database system or a data access API, that is used to manage attributes of data returned from a data source (a result set). These attributes include concurrency management, position in the result set, number of rows returned, and whether you can move forward or backward (or both) through the result set (scrollability).  
  
 A cursor keeps track of the position in the result set, and allows you to perform multiple operations row by row against a result set, with or without returning to the original table. In other words, cursors conceptually return a result set based on tables within the databases. The cursor is so named because it indicates the current position in the result set, just as the cursor on a computer screen indicates current position.  
  
 It is important to become familiar with the concept of cursors before moving on to learn the specifics of their usage in ADO.  
  
 Using cursors, you can:  
  
-   Specify positioning at specific rows in the result set.  
  
-   Retrieve one row or a block of rows based on the current result set position.  
  
-   Modify data in the rows at the current position in the result set.  
  
-   Define different levels of sensitivity to data changes made by other users.  
  
 For example, consider an application that displays a list of available products to a potential buyer. The buyer scrolls through the list to see product details and cost, and finally selects a product for purchase. Additional scrolling and selection occurs for the remainder of the list. As far as the buyer is concerned, the products appear one at a time, but the application uses a scrollable cursor to browse up and down through the result set.  
  
 You can use cursors in a variety of ways:  
  
-   With no rows at all.  
  
-   With some or all of the rows in a single table.  
  
-   With some or all of the rows from logically joined tables.  
  
-   As read-only or updateable at the cursor or field level.  
  
-   As forward-only or fully scrollable.  
  
-   With the cursor keyset located on the server.  
  
-   Sensitive to underlying table changes caused by other applications (such as membership, sort, inserts, updates, and deletes).  
  
-   Existing on either the server or the client.  
  
 Read-only cursors help users browse through the result set, and read/write cursors can implement individual row updates. Complex cursors can be defined with keysets that point back to base table rows. Although some cursors are read-only in a forward direction, others can move back and forth and provide a dynamic refresh of the result set based on changes that other applications are making to the database.  
  
 Not all applications need to use cursors to access or update data. Some queries simply do not require direct row updating by using a cursor. Cursors should be one of the last techniques you choose to retrieve data-and then you should choose the lowest impact cursor possible. When you create a result set by using a stored procedure, the result set is not updateable using cursor edit or update methods.  
  
## Concurrency  
 In some multiuser applications it is very important for the data presented to the end user to be as current as possible. A classic example of such a system is an airline reservation system, where many users might be contending for the same seat on a given flight (and therefore, a single record). In a case like this, the application design must handle concurrent operations on a single record.  
  
 In other applications, concurrency is not as important. In such cases, the expense involved in keeping the data current at all times cannot be justified.  
  
## Position  
 A cursor also keeps track of the current position in a result set. Think of the cursor position as a pointer to the current record, similar to the way an array index points to the value at that particular location in the array.  
  
## Scrollability  
 The type of cursor employed by your application also affects the ability to move forward and backward through the rows in a result set; this is sometimes referred to as scrollability. The ability to move forward *and* backward through a result set adds to the complexity of the cursor, and is therefore more expensive to implement. For this reason, you should ask for a cursor with this functionality only when necessary.
