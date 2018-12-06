---
title: "Simulating Positioned Update and Delete Statements | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "positioned deletes [ODBC]"
  - "data updates [ODBC], positioned update or delete"
  - "row identifiers [ODBC]"
  - "positioned updates [ODBC]"
  - "updating data [ODBC], positioned update or delete"
ms.assetid: b24ed59f-f25b-4646-a135-5f3596abc1a4
author: MightyPen
ms.author: genemi
manager: craigg
---
# Simulating Positioned Update and Delete Statements
If the data source does not support positioned update and delete statements, the driver can simulate these. For example, the ODBC cursor library simulates positioned update and delete statements. The general strategy for simulating positioned update and delete statements is to convert positioned statements to searched ones. This is done by replacing the **WHERE CURRENT OF** clause with a searched **WHERE** clause that identifies the current row.  
  
 For example, because the CustID column uniquely identifies each row in the Customers table, the positioned delete statement  
  
```  
DELETE FROM Customers WHERE CURRENT OF CustCursor  
```  
  
 might be converted to  
  
```  
DELETE FROM Customers WHERE (CustID = ?)  
```  
  
 The driver may use one of the following *row identifiers* in the **WHERE** clause:  
  
-   Columns whose values serve to identify uniquely every row in the table. For example, calling **SQLSpecialColumns** with SQL_BEST_ROWID returns the optimal column or set of columns that serve this purpose.  
  
-   Pseudo-columns, provided by some data sources, for the purpose of uniquely identifying every row. These may also be retrievable by calling **SQLSpecialColumns**.  
  
-   A unique index, if available.  
  
-   All the columns in the result set.  
  
 Exactly which columns a driver should use in the **WHERE** clause it constructs depends on the driver. On some data sources, determining a row identifier can be costly. However, it is faster to execute and guarantees that a simulated statement updates or deletes at most one row. Depending on the capabilities of the underlying DBMS, using a row identifier can be expensive to set up. However, it is faster to execute and guarantees that a simulated statement will update or delete only one row. The option of using all the columns in the result set is usually much easier to set up. However, it is slower to execute and, if the columns do not uniquely identify a row, can result in rows being unintentionally updated or deleted, especially when the select list for the result set does not contain all the columns that exist in the underlying table.  
  
 Depending upon which of the preceding strategies the driver supports, an application can choose which strategy it wants the driver to use with the SQL_ATTR_SIMULATE_CURSOR statement attribute. Although it might seem odd for an application to risk unintentionally updating or deleting a row, the application can remove this risk by ensuring that the columns in the result set uniquely identify each row in the result set. This saves the driver the effort of having to do this.  
  
 If the driver chooses to use a row identifier, it intercepts the **SELECT FOR UPDATE** statement that creates the result set. If the columns in the select list do not effectively identify a row, the driver adds the necessary columns to the end of the select list. Some data sources have a single column that always uniquely identifies a row, such as the ROWID column in Oracle; if such a column is available, the driver uses this. Otherwise, the driver calls **SQLSpecialColumns** for each table in the **FROM** clause to retrieve a list of the columns that uniquely identify each row. A common restriction that results from this technique is that cursor simulation fails if there is more than one table in the **FROM** clause.  
  
 No matter how the driver identifies rows, it usually strips the **FOR UPDATE OF** clause off the **SELECT FOR UPDATE** statement before sending it to the data source. The **FOR UPDATE OF** clause is used only with positioned update and delete statements. Data sources that do not support positioned update and delete statements generally do not support it.  
  
 When the application submits a positioned update or delete statement for execution, the driver replaces the **WHERE CURRENT OF** clause with a **WHERE** clause containing the row identifier. The values of these columns are retrieved from a cache maintained by the driver for each column it uses in the **WHERE** clause. After the driver has replaced the **WHERE** clause, it sends the statement to the data source for execution.  
  
 For example, suppose that the application submits the following statement to create a result set:  
  
```  
SELECT Name, Address, Phone FROM Customers FOR UPDATE OF Phone, Address  
```  
  
 If the application has set SQL_ATTR_SIMULATE_CURSOR to request a guarantee of uniqueness and if the data source does not provide a pseudo-column that always uniquely identifies a row, the driver calls **SQLSpecialColumns** for the Customers table, discovers that CustID is the key to the Customers table and adds this to the select list, and strips the **FOR UPDATE OF** clause:  
  
```  
SELECT Name, Address, Phone, CustID FROM Customers  
```  
  
 If the application has not requested a guarantee of uniqueness, the driver strips only the **FOR UPDATE OF** clause:  
  
```  
SELECT Name, Address, Phone FROM Customers  
```  
  
 Suppose the application scrolls through the result set and submits the following positioned update statement for execution, where Cust is the name of the cursor over the result set:  
  
```  
UPDATE Customers SET Address = ?, Phone = ? WHERE CURRENT OF Cust  
```  
  
 If the application has not requested a guarantee of uniqueness, the driver replaces the **WHERE** clause and binds the CustID parameter to the variable in its cache:  
  
```  
UPDATE Customers SET Address = ?, Phone = ? WHERE (CustID = ?)  
```  
  
 If the application has not requested a guarantee of uniqueness, the driver replaces the **WHERE** clause and binds the Name, Address, and Phone parameters in this clause to the variables in its cache:  
  
```  
UPDATE Customers SET Address = ?, Phone = ?  
   WHERE (Name = ?) AND (Address = ?) AND (Phone = ?)  
```
