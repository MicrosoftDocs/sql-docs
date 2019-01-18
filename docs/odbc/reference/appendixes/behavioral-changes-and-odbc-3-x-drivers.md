---
title: "Behavioral Changes and ODBC 3.x Drivers | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "sql_attr_odbc_version [ODBC]"
  - "backward compatibility [ODBC], behavioral changes"
  - "compatibility [ODBC], behavioral changes"
ms.assetid: 88a503cc-bff7-42d9-83ff-8e232109ed06
author: MightyPen
ms.author: genemi
manager: craigg
---
# Behavioral Changes and ODBC 3.x Drivers
The environment attribute SQL_ATTR_ODBC_VERSION indicates to the driver whether it needs to exhibit ODBC 2.*x* behavior or ODBC 3*.x* behavior. How the SQL_ATTR_ODBC_VERSION environment attribute is set depends on the application. ODBC 3*.x* applications must call **SQLSetEnvAttr** to set this attribute after they call **SQLAllocHandle** to allocate an environment handle and before they call **SQLAllocHandle** to allocate a connection handle. If they fail to do this, the Driver Manager returns SQLSTATE HY010 (Function sequence error) on the latter call to **SQLAllocHandle**.  
  
> [!NOTE]  
>  For more information on behavioral changes and how an application acts, see [Behavioral Changes](../../../odbc/reference/develop-app/behavioral-changes.md).  
  
 ODBC 2.*x* applications and ODBC 2.*x* applications recompiled with the ODBC 3*.x* header files do not call **SQLSetEnvAttr**. However, they call **SQLAllocEnv** instead of **SQLAllocHandle** to allocate an environment handle. Therefore, when the application calls **SQLAllocEnv** in the Driver Manager, the Driver Manager calls **SQLAllocHandle** and **SQLSetEnvAttr** in the driver. Thus, ODBC 3*.x* drivers can always count on this attribute being set.  
  
 If a standards-compliant application compiled with the ODBC_STD compile flag calls **SQLAllocEnv** (which may occur because **SQLAllocEnv** is not deprecated in ISO), the call is mapped to **SQLAllocHandleStd** at compile time. At run time, the application calls **SQLAllocHandleStd**. The Driver Manager sets the SQL_ATTR_ODBC_VERSION environment attribute to SQL_OV_ODBC3. A call to **SQLAllocHandleStd** is equivalent to a call to **SQLAllocHandle** with a *HandleType* of SQL_HANDLE_ENV and a call to **SQLSetEnvAttr** to set SQL_ATTR_ODBC_VERSION to SQL_OV_ODBC3.  
  
 In certain driver architectures, there is a need for the driver to appear as either an ODBC 2.*x* driver or an ODBC 3*.x* driver, depending on the connection. The driver in this case might not actually be a driver but a layer that resides between the Driver Manager and another driver. For example, it might mimic a driver, like ODBC Spy. In another example, it might act as a gateway, like EDA/SQL. To appear as an ODBC 3*.x* driver, such a driver must be able to export **SQLAllocHandle**, and to appear as an ODBC 2.*x* driver, must be able to export **SQLAllocConnect**, **SQLAllocEnv**, and **SQLAllocStmt**. When an environment, connection, or statement is to be allocated, the Driver Manager checks to see if this driver exports **SQLAllocHandle**. Since the driver does, the Driver Manager calls **SQLAllocHandle** in the driver. If the driver is working with an ODBC 2.*x* driver, the driver must map the call to **SQLAllocHandle** to **SQLAllocConnect**, **SQLAllocEnv**, or **SQLAllocStmt**, as appropriate. It must also do nothing with the **SQLSetEnvAttr** call when behaving as an ODBC 2.*x* driver.  
  
 This section contains the following topics.  
  
-   [Datetime Data Types](../../../odbc/reference/appendixes/datetime-data-types.md)  
  
-   [Backward Compatibility of C Data Types](../../../odbc/reference/appendixes/backward-compatibility-of-c-data-types.md)  
  
-   [Fixed-Length Bookmarks](../../../odbc/reference/appendixes/fixed-length-bookmarks.md)  
  
-   [SQLGetInfo Support](../../../odbc/reference/appendixes/sqlgetinfo-support.md)  
  
-   [Returning SQL_NO_DATA](../../../odbc/reference/appendixes/returning-sql-no-data.md)  
  
-   [Calling SQLSetPos to Insert Data](../../../odbc/reference/appendixes/calling-sqlsetpos-to-insert-data.md)  
  
-   [Loading by Ordinal](../../../odbc/reference/appendixes/loading-by-ordinal.md)
