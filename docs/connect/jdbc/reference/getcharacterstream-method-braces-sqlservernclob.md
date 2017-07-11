---
title: "getCharacterStream Method () (SQLServerNClob) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
ms.assetid: 7641698e-b25c-4bb2-bcc7-9273bdd08bf0
caps.latest.revision: 11
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# getCharacterStream Method () (SQLServerNClob)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Retrieves the **NCLOB** data as a **Reader** object or as a stream of characters.  
  
## Syntax  
  
```  
  
public java.io.Reader getCharacterStream()  
```  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Return Value  
 A Reader object that contains the **NCLOB** data.  
  
## Remarks  
 This getCharacterStream method is specified by the getCharacterStream method in the java.sql.NClob interface.  
  
## See Also  
 [getCharacterStream Method &#40;SQLServerNClob&#41;](../../../connect/jdbc/reference/getcharacterstream-method-sqlservernclob.md)   
 [SQLServerNClob Methods](../../../connect/jdbc/reference/sqlservernclob-methods.md)   
 [SQLServerNClob Members](../../../connect/jdbc/reference/sqlservernclob-members.md)   
 [SQLServerNClob Class](../../../connect/jdbc/reference/sqlservernclob-class.md)  
  
  