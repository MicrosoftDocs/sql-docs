---
title: "updateBytes Method (int, byte)"
description: "updateBytes Method (int, byte)"
author: David-Engel
ms.author: v-davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerResultSet.updateBytes (int, byte[])"
apitype: "Assembly"
---
# updateBytes Method (int, byte)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Updates the designated column with an array of **byte** values given the column index.  
  
## Syntax  
  
```  
  
public void updateBytes(int index,  
                        byte[] x)  
```  
  
#### Parameters  
 *index*  
  
 An **int** that indicates the column index.  
  
 *x*  
  
 An array of **byte** values.  
  
## Exceptions  
 [SQLServerException](../../../connect/jdbc/reference/sqlserverexception-class.md)  
  
## Remarks  
 This updateBytes method is specified by the updateBytes method in the java.sql.ResultSet interface.  
  
 In a previous version of [!INCLUDE[jdbcNoVersion](../../../includes/jdbcnoversion_md.md)], you could use SQLServerResultSet.updateBytes to convert values between byte arrays and [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] data type **date**, **time**, **datetime2**, or **datetimeoffset**. Now, using this method with those data types will cause an exception indicating that the conversion is not supported.  
  
## See Also  
 [updateBytes Method &#40;SQLServerResultSet&#41;](../../../connect/jdbc/reference/updatebytes-method-sqlserverresultset.md)   
 [SQLServerResultSet Members](../../../connect/jdbc/reference/sqlserverresultset-members.md)   
 [SQLServerResultSet Class](../../../connect/jdbc/reference/sqlserverresultset-class.md)  
  
  
