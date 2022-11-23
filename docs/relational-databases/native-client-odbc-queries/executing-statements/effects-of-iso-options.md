---
description: "Effects of ISO Options"
title: "Effects of ISO Options | Microsoft Docs"
ms.custom: ""
ms.date: "03/14/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: native-client
ms.topic: "reference"
helpviewer_keywords: 
  - "ISO options (ODBC)"
  - "ODBC applications, ISO options"
  - "ODBC applications, statements"
  - "SQL Server Native Client ODBC driver, ISO options"
  - "statements [ODBC], ISO options"
ms.assetid: 813f1397-fa0b-45ec-a718-e13fe2fb88ac
author: markingmyname
ms.author: maghan
monikerRange: ">=aps-pdw-2016||=azuresqldb-current||=azure-sqldw-latest||>=sql-server-2016||>=sql-server-linux-2017||=azuresqldb-mi-current"
---
# Effects of ISO Options
[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

  The ODBC standard is closely matched to the ISO standard, and ODBC applications expect standard behavior from an ODBC driver. To make its behavior conform more closely with that defined in the ODBC standard, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver always uses any ISO options available in the version of SQL Server with which it connects.  
  
 When the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver connects to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], the server detects that the client is using the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver and sets several options on.  
  
 The driver issues these statements itself; the ODBC application does nothing to request them. Setting these options allows ODBC applications using the driver to be more portable because the server behavior then matches the ISO standard.  
  
 DB-Library-based applications generally do not turn these options on. Sites observing different behavior between ODBC or DB-Library clients when running the same SQL statement should not assume this points to a problem with the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver. They should first rerun the statement in the DB-Library environment with the same SET options as would be used by the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver.  
  
 Because SET options can be turned on and off at any time by users and applications, developers of stored procedures and triggers should also take care to test their procedures and triggers with the SET options listed above turned both on and off. This ensures that the procedures and triggers work correctly regardless of which options a particular connection may have set on when they invoke the procedure or trigger. Triggers or stored procedures that require a particular setting for one of these options should issue a SET statement at the start of the trigger or stored procedure. This SET statement remains in effect only for the execution of the trigger or stored procedure; when the procedure or trigger ends, the original setting is restored.  
  
 When connected to an instance of [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)], a fourth SET option, CONCAT_NULL_YIELDS_NULL, is also set on. The [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver does not set these options on if AnsiNPW=NO is specified in the data source or on either [SQLDriverConnect](../../../relational-databases/native-client-odbc-api/sqldriverconnect.md) or [SQLBrowseConnect](../../../relational-databases/native-client-odbc-api/sqlbrowseconnect.md).  
  
 Like the ISO options noted earlier, the [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client ODBC driver does not turn the QUOTED_IDENTIFIER option on if QuotedID=NO is specified in the data source or on either **SQLDriverConnect** or **SQLBrowseConnect**.  
  
 To allow the driver to know the current state of SET options, ODBC applications should not use the [!INCLUDE[tsql](../../../includes/tsql-md.md)] SET statement to set these options. They should only set these options using either the data source or the connection options. If the application issues SET statements, the driver can generate incorrect SQL statements.  
  
## See Also  
 [Executing Statements &#40;ODBC&#41;](../../../relational-databases/native-client-odbc-queries/executing-statements/executing-statements-odbc.md)   
 [SQLDriverConnect](../../../relational-databases/native-client-odbc-api/sqldriverconnect.md)   
 [SQLBrowseConnect](../../../relational-databases/native-client-odbc-api/sqlbrowseconnect.md)  
  
  
