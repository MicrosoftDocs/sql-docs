---
title: "SSTRANSTIGHTLYCPLD Field (SQLServerXAResource) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SSTRANSTIGHTLYCPLD Field (SQLServerXAResource)"
apilocation: 
  - "SSTRANSTIGHTLYCPLD Field (SQLServerXAResource)"
apitype: "Assembly"
ms.assetid: 379857c3-9de1-4964-8782-32df317cbfbb
author: MightyPen
ms.author: genemi
manager: craigg
---
# SSTRANSTIGHTLYCPLD Field (SQLServerXAResource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Used to allow the tightly coupled XA transactions, which have different XA branch transaction IDs (XIDs) but have the same global transaction ID (GTRID).  
  
## Syntax  
  
```  
  
public static final int SSTRANSTIGHTLYCPLD  
```  
  
## Field Value  
 An **int** value of 32768.  
  
## Remarks  
 Each transaction is identified by an XA branch transaction ID (XID) and a global transaction ID (GTRID). In order to allow the applications to use tightly coupled XA transactions that have different XIDs but have the same GTRID, you must set the [SSTRANSTIGHTLYCPLD](../../../connect/jdbc/reference/sstranstightlycpld-field-sqlserverxaresource.md) on the flags parameter of the XAResource.start method. For more information about how to use this flag, see [Understanding XA Transactions](../../../connect/jdbc/understanding-xa-transactions.md).  
  
## See Also  
 [SQLServerXAResource Fields](../../../connect/jdbc/reference/sqlserverxaresource-fields.md)   
 [SQLServerXAResource Members](../../../connect/jdbc/reference/sqlserverxaresource-members.md)   
 [SQLServerXAResource Class](../../../connect/jdbc/reference/sqlserverxaresource-class.md)  
  
  
