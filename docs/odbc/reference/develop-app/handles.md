---
description: "Handles"
title: "Handles | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "handles [ODBC]"
  - "driver handles [ODBC]"
  - "driver manager [ODBC], handles"
  - "handles [ODBC], about handles"
ms.assetid: f663101e-a4cc-402b-b9d7-84d5e975be71
author: David-Engel
ms.author: v-davidengel
---
# Handles
Handles are opaque, 32-bit values that identify a particular item; in ODBC, this item can be an environment, connection, statement, or descriptor. When the application calls **SQLAllocHandle**, the Driver Manager or driver creates a new item of the specified type and returns its handle to the application. The application later uses the handle to identify that item when calling ODBC functions. The Driver Manager and driver use the handle to locate information about the item.  
  
 For example, the following code uses two statement handles (*hstmtOrder* and *hstmtLine*) to identify the statements on which to create result sets of sales orders and sales order line numbers. It later uses these handles to identify which result set to fetch data from.  
  
```  
SQLHSTMT      hstmtOrder, hstmtLine; // Statement handles.  
SQLUINTEGER   OrderID;  
SQLINTEGER    OrderIDInd = 0;  
SQLRETURN     rc;  
  
// Prepare the statement that retrieves line number information.  
SQLPrepare(hstmtLine, "SELECT * FROM Lines WHERE OrderID = ?", SQL_NTS);  
  
// Bind OrderID to the parameter in the preceding statement.  
SQLBindParameter(hstmtLine, 1, SQL_PARAM_INPUT, SQL_C_ULONG, SQL_INTEGER, 5, 0,  
               &OrderID, 0, &OrderIDInd);  
  
// Bind the result sets for the Order table and the Lines table. Bind  
// OrderID to the OrderID column in the Orders table. When each row is  
// fetched, OrderID will contain the current order ID, which will then be  
// passed as a parameter to the statement tofetch line number  
// information. Code not shown.  
  
// Create a result set of sales orders.  
SQLExecDirect(hstmtOrder, "SELECT * FROM Orders", SQL_NTS);  
  
// Fetch and display the sales order data. Code to check if rc equals  
// SQL_ERROR or SQL_SUCCESS_WITH_INFO not shown.  
while ((rc = SQLFetch(hstmtOrder)) != SQL_NO_DATA) {  
   // Display the sales order data. Code not shown.  
  
   // Create a result set of line numbers for the current sales order.  
   SQLExecute(hstmtLine);  
  
   // Fetch and display the sales order line number data. Code to check  
   // if rc equals SQL_ERROR or SQL_SUCCESS_WITH_INFO not shown.  
   while ((rc = SQLFetch(hstmtLine)) != SQL_NO_DATA) {  
      // Display the sales order line number data. Code not shown.  
   }  
  
   // Close the sales order line number result set.  
   SQLCloseCursor(hstmtLine);  
}  
  
// Close the sales order result set.  
SQLCloseCursor(hstmtOrder);  
```  
  
 Handles are meaningful only to the ODBC component that created them; that is, only the Driver Manager can interpret Driver Manager handles and only a driver can interpret its own handles.  
  
 For example, suppose the driver in the preceding example allocates a structure to store information about a statement and returns the pointer to this structure as the statement handle. When the application calls **SQLPrepare**, it passes an SQL statement and the handle of the statement used for sales order line numbers. The driver sends the SQL statement to the data source, which prepares it and returns an access plan identifier. The driver uses the handle to find the structure in which to store this identifier.  
  
 Later, when the application calls **SQLExecute** to generate the result set of line numbers for a particular sales order, it passes the same handle. The driver uses the handle to retrieve the access plan identifier from the structure. It sends the identifier to the data source to tell it which plan to execute.  
  
 ODBC has two levels of handles: Driver Manager handles and driver handles. The application uses Driver Manager handles when calling ODBC functions because it calls those functions in the Driver Manager. The Driver Manager uses this handle to find the corresponding driver handle and uses the driver handle when calling the function in the driver. For an example of how driver and Driver Manager handles are used, see [Driver Manager's Role in the Connection Process](../../../odbc/reference/develop-app/driver-manager-s-role-in-the-connection-process.md).  
  
 That there are two levels of handles is an artifact of the ODBC architecture; in most cases, it is not relevant to either the application or driver. Although there is usually no reason to do so, it is possible for the application to determine the driver handles by calling **SQLGetInfo**.  
  
 This section contains the following topics.  
  
-   [Environment Handles](../../../odbc/reference/develop-app/environment-handles.md)  
  
-   [Connection Handles](../../../odbc/reference/develop-app/connection-handles.md)  
  
-   [Statement Handles](../../../odbc/reference/develop-app/statement-handles.md)  
  
-   [Descriptor Handles](../../../odbc/reference/develop-app/descriptor-handles.md)  
  
-   [State Transitions](../../../odbc/reference/develop-app/state-transitions.md)
