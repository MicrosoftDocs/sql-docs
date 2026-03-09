---
title: "setSendStringParametersAsUnicode Method (SQLServerDataSource)"
description: "setSendStringParametersAsUnicode Method (SQLServerDataSource)"
author: David-Engel
ms.author: davidengel
ms.date: "01/28/2026"
ms.service: sql
ms.subservice: connectivity
ms.topic: reference
apilocation: "sqljdbc.jar"
apiname: "SQLServerDataSource.setSendStringParametersAsUnicode"
apitype: "Assembly"
---
# setSendStringParametersAsUnicode Method (SQLServerDataSource)
[!INCLUDE[Driver_JDBC_Download](../../../includes/driver_jdbc_download.md)]

  Sets a **boolean** value that indicates whether the driver sends string parameters to the server in Unicode format.  
  
## Syntax  
  
```  
  
public void setSendStringParametersAsUnicode(boolean sendStringParametersAsUnicode)  
```  
  
#### Parameters  
 *sendStringParametersAsUnicode*  
  
 **true** if the driver sends string parameters to the server in Unicode format. Otherwise, **false**.  
  
## Remarks  
 When the sendStringParametersAsUnicode property is set to **true**, which is the default value, the driver sends string parameters to the server in **UTF-16LE (UTF-16 Little Endian) encoding**, matching SQL Server's internal NCHAR/NVARCHAR storage format. The driver converts CHAR, VARCHAR, and LONGVARCHAR types to NCHAR, NVARCHAR, and LONGNVARCHAR respectively before sending them to the server.

When sendStringParametersAsUnicode is set to **false**, the driver sends string parameters in the database's collation-specific **MBCS (Multi-Byte Character Set) encoding**. The specific code page used depends on the target database or column collation. This isn't ASCII (which is 7-bit only), but rather the full character encoding defined by the collation.

If you don't set sendStringParametersAsUnicode, getSendStringParametersAsUnicode returns the default value of **true**.  

> [!NOTE]
> Changing this value can affect the sorting of results from the database. The sorting differences are due to different sorting rules for Unicode versus non-Unicode characters. For VARCHAR/CHAR columns, setting this to **false** avoids implicit conversion overhead on the server.

For more information about the sendStringParametersAsUnicode connection property, see [Setting the Connection Properties](../../../connect/jdbc/setting-the-connection-properties.md). For details about SQL Server's Unicode support, see [Collation and Unicode Support](/sql/relational-databases/collations/collation-and-unicode-support).  
  
## Related content

- [SQLServerDataSource Members](../../../connect/jdbc/reference/sqlserverdatasource-members.md)
- [SQLServerDataSource Class](../../../connect/jdbc/reference/sqlserverdatasource-class.md)  
