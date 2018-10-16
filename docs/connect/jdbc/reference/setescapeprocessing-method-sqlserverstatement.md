---
title: "setEscapeProcessing Method (SQLServerStatement) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
apiname: 
  - "SQLServerStatement.setEscapeProcessing"
apilocation: 
  - "sqljdbc.jar"
apitype: "Assembly"
ms.assetid: 6ac0682e-e04c-4fdb-893b-92408d42051e
author: MightyPen
ms.author: genemi
manager: craigg
---
# setEscapeProcessing Method (SQLServerStatement)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets the escape processing mode.  
  
> [!NOTE]  
>  Escape processing for [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)] is always enabled. Setting this method to false has no effect.  
  
## Syntax  
  
```  
  
public final void setEscapeProcessing(boolean enable)  
```  
  
#### Parameters  
 *enable*  
  
 **true** to enable escape processing. Otherwise, **false**.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This setEscapeProcessing method is specified by the setEscapeProcessing method in the java.sql.Statement interface.  
  
## See Also  
 [SQLServerStatement Members](../../../connect/jdbc/reference/sqlserverstatement-members.md)   
 [SQLServerStatement Class](../../../connect/jdbc/reference/sqlserverstatement-class.md)  
  
  
