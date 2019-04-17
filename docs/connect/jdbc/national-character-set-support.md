---
title: "National Character Set Support | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
ms.assetid: 4fceacfd-df4f-40cd-b7a2-5e5e58a5979f
author: MightyPen
ms.author: genemi
manager: craigg
---
# National Character Set Support
[!INCLUDE[Driver_JDBC_Download](../../includes/driver_jdbc_download.md)]

  The JDBC driver provides support for the JDBC 4.0 API, which includes new national character set conversion API methods. This support includes new setter, getter, and updater methods for **NCHAR**, **NVARCHAR**, **LONGNVARCHAR**, and **NCLOB** JDBC types.  
  
 The following is a list of new getter, setter, and updater methods to support the national character set conversion:  
  
-   [SQLServerPreparedStatement](../../connect/jdbc/reference/sqlserverpreparedstatement-class.md): [setNString](../../connect/jdbc/reference/setnstring-method-int-java-lang-string.md), [setNCharacterStream](../../connect/jdbc/reference/setncharacterstream-method-sqlserverpreparedstatement.md), [setNClob](../../connect/jdbc/reference/setnclob-method-sqlserverpreparedstatement.md).  
  
-   [SQLServerCallableStatement](../../connect/jdbc/reference/sqlservercallablestatement-class.md): [getNClob](../../connect/jdbc/reference/getnclob-method-sqlservercallablestatement.md), [getNString](../../connect/jdbc/reference/getnstring-method-sqlservercallablestatement.md), [getNCharacterStream](../../connect/jdbc/reference/getncharacterstream-method-sqlservercallablestatement.md), [setNString](../../connect/jdbc/reference/setnstring-method-sqlservercallablestatement.md), [setNCharacterStream](../../connect/jdbc/reference/setncharacterstream-method-sqlservercallablestatement.md), [setNClob](../../connect/jdbc/reference/setnclob-method-sqlservercallablestatement.md).  
  
-   [SQLServerResultSet](../../connect/jdbc/reference/sqlserverresultset-class.md): [getNClob](../../connect/jdbc/reference/getnclob-method-sqlserverresultset.md), [getNString](../../connect/jdbc/reference/getnstring-method-sqlserverresultset.md), [getNCharacterStream](../../connect/jdbc/reference/getncharacterstream-method-sqlserverresultset.md), [updateNClob](../../connect/jdbc/reference/updatenclob-method-sqlserverresultset.md), [updateNString](../../connect/jdbc/reference/updatenstring-method-sqlserverresultset.md), [updateNCharacterStream](../../connect/jdbc/reference/updatencharacterstream-method-sqlserverresultset.md).  
  
> [!NOTE]  
>  You must set the classpath to include the sqljdbc4.jar file to use these methods in your application.  
  
 In order to send String parameters to the server in Unicode format, the applications should either use the new JDBC 4.0 national character methods; or set the **sendStringParametersAsUnicode** connection property to "**true**" when using the non-national character methods. The recommended way is to use the new JDBC 4.0 national character methods where possible. For more information about the **sendStringParametersAsUnicode** connection property, see [Setting the Connection Properties](../../connect/jdbc/setting-the-connection-properties.md).  
  
## See Also  
 [Understanding the JDBC Driver Data Types](../../connect/jdbc/understanding-the-jdbc-driver-data-types.md)  
  
  
