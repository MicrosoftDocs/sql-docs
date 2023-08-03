---
title: "Data Shaping Example"
description: "Data Shaping Example"
author: rothja
ms.author: jroth
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "data shaping [ADO], about data shaping"
---
# Data Shaping Example
The following data shaping command demonstrates how to build a hierarchical **Recordset** from the **Customers** and **Orders** tables in the Northwind database.  
  
```  
SHAPE {SELECT CustomerID, ContactName FROM Customers}   
APPEND ({SELECT OrderID, OrderDate, CustomerID FROM Orders} AS chapOrders   
RELATE customerID TO customerID)   
```  
  
 When this command is used to open a **Recordset** object (as shown in [Visual Basic Example of Data Shaping](./visual-basic-example-of-data-shaping.md)), it creates a chapter (**chapOrders**) for each record returned from the **Customers** table. This chapter consists of a subset of the **Recordset** returned from the **Orders** table. The **chapOrders** chapter contains all the requested information about the orders placed by the given customer. In this example, the chapter consists of three columns: **OrderID**, **OrderDate**, and **CustomerID**.  
  
 The first two entries of the resultant shaped **Recordset** are as follows:  
  
|CustomerID|ContactName|OrderID|OrderDate|CustomerID|  
|----------------|-----------------|-------------|---------------|----------------|  
|ALFKI|Maria Ander|10643<br /><br /> 10692<br /><br /> 10702<br /><br /> 10835<br /><br /> 10952<br /><br /> 11011|1997-08-25<br /><br /> 1997-10-03<br /><br /> 1997-10-13<br /><br /> 1998-01-15<br /><br /> 1998-03-16<br /><br /> 1998-04-09|ALFKI<br /><br /> ALFKI<br /><br /> ALFKI<br /><br /> ALFKI<br /><br /> ALFKI<br /><br /> ALFKI|  
|ANATR|Ana Trujillo|10308<br /><br /> 10625<br /><br /> 10759<br /><br /> 10926|1996-09-18<br /><br /> 1997-08-08<br /><br /> 1997-11-28<br /><br /> 1998-03-04|ANATR<br /><br /> ANATR<br /><br /> ANATR<br /><br /> ANATR|  
  
 In a SHAPE command, APPEND is used to create a child **Recordset** related to the parent **Recordset** (as returned from the provider-specific command immediately after the SHAPE keyword that was discussed earlier) by the RELATE clause. The parent and child typically have at least one column in common: The value of the column in a row of the parent is the same as the value of the column in all rows of the child.  
  
 There is a second way to use SHAPE commands: namely, to generate a parent **Recordset** from a child **Recordset**. The records in the child **Recordset** are grouped, typically by using the BY clause, and one row is added to the parent **Recordset** for each resulting group in the child. If the BY clause is omitted, the child **Recordset** will form a single group and the parent **Recordset** will contain exactly one row. This is useful for computing "grand total" aggregates over the entire child **Recordset**.  
  
 The SHAPE command construct also enables you to programmatically create a shaped **Recordset**. You can then access the components of the **Recordset** programmatically or through an appropriate visual control. A shape command is issued like any other ADO command text. For more information, see [Shape Commands in General](./shape-commands-in-general.md).  
  
 Regardless of which way the parent **Recordset** is formed, it will contain a chapter column that is used to relate it to a child **Recordset**. If you want, the parent **Recordset** can also have columns that contain aggregates (SUM, MIN, MAX, and so on) over the child rows. Both the parent and the child **Recordset** can have columns that contain an expression on the row in the **Recordset**, as well as columns which are new and initially empty.  
  
 This section continues with the following topic.  
  
-   [Visual Basic Example of Data Shaping](./visual-basic-example-of-data-shaping.md)