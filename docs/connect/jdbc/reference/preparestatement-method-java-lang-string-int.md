---
title: "prepareStatement Method (java.lang.String) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
apiname: 
  - "SQLServerConnection.prepareStatement (java.lang.String)"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: e825765c-eb55-4800-951b-f3495da36641
caps.latest.revision: 9
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# prepareStatement Method (java.lang.String)

  Creates a [SQLServerPreparedStatement](../Topic/SQLServerPreparedStatement%20Class.md) object for sending parameterized SQL statements to the database.  
  
## Syntax  
  
```  
  
public java.sql.PreparedStatement prepareStatement(java.lang.String sql)  
```  
  
#### Parameters  
 *sql*  
  
 A **String** containing an SQL statement.  
  
## Return Value  
 A PreparedStatement object.  
  
## Exceptions  
 [SQLServerException](../Topic/SQLServerException%20Class.md)  
  
## Remarks  
 This prepareStatement method is specified by the prepareStatement method in the java.sql.Connection interface.  
  
## See Also  
 [prepareStatement Method &#40;SQLServerConnection&#41;](../Topic/prepareStatement%20Method%20(SQLServerConnection).md)   
 [SQLServerConnection Members](../Topic/SQLServerConnection%20Members.md)   
 [SQLServerConnection Class](../Topic/SQLServerConnection%20Class.md)  
  
  