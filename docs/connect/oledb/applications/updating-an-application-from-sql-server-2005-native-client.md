---
title: "Updating an Application from SQL Server 2005 Native Client"
description: Learn about the breaking changes in OLE DB Driver for SQL Server since SQL Server Native Client in SQL Server 2005 (9.x).
author: David-Engel
ms.author: v-davidengel
ms.date: "06/12/2018"
ms.service: sql
ms.subservice: connectivity
ms.topic: "reference"
helpviewer_keywords:
  - "OLE DB Driver for SQL Server, updating applications"
---
# Updating Applications from SQL Server 2005 Native Client

[!INCLUDE [SQL Server](../../../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

[!INCLUDE[Driver_OLEDB_Download](../../../includes/driver_oledb_download.md)]

This article discusses the breaking changes in OLE DB Driver for SQL Server since [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client in [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)].

When you upgrade from Microsoft Data Access Components (MDAC) to OLE DB Driver for SQL Server, you might also see some behavior differences. For more information, see [Updating an Application to OLE DB Driver for SQL Server from MDAC](updating-an-application-to-oledb-driver-for-sql-server-from-mdac.md).

[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client 9.0 shipped with [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)]. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client 10.0 shipped with [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)].  [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client 10.5 shipped with [!INCLUDE[ssKilimanjaro](../../../includes/sskilimanjaro-md.md)]. [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client 11.0 shipped with [!INCLUDE[ssSQL11](../../../includes/sssql11-md.md)] and [!INCLUDE[ssSQL14](../../../includes/sssql14-md.md)].

|Changed behavior in OLE DB Driver for SQL Server compared to [!INCLUDE[ssVersion2005](../../../includes/ssversion2005-md.md)] Native Client|Description|
|------------------------------------------------------------------------------------|-----------------|
|OLE DB pads only to the defined scale.|For conversions where converted data is sent to the server, OLE DB Driver for SQL Server pads trailing zeros in data only up to the maximum length of **datetime** values. SQL Server Native Client 9.0 padded to 9 digits.|
|Validate DBTYPE_DBTIMESTAMP for ICommandWithParameter::SetParameterInfo.|OLE DB Driver for SQL Server implements the OLE DB requirement for *bScale* in ICommandWithParameter::SetParameterInfo to be set to the fractional seconds' precision for DBTYPE_DBTIMESTAMP.|
|The **sp_columns** stored procedure now returns **"NO"** instead of **"NO "** for the IS_NULLABLE column.|In OLE DB Driver for SQL Server, **sp_columns** stored procedure now returns **"NO"** instead of **"NO "** for an IS_NULLABLE column.|
|Different error returned when date is out of range.|For the **datetime** type, a different error number will be returned by OLE DB Driver for SQL Server for an out-of-range date than was returned in earlier versions.<br /><br /> Specifically, [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client 9.0 returned 22007 for all out of range year values in string conversions to **datetime**, and OLE DB Driver for SQL Server returns 22008 when the date is within the range supported by **datetime2** but outside the range supported by **datetime** or **smalldatetime**.|
|**datetime** value truncates fractional seconds and not round if rounding will change the day.|Prior to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client 10.0, the client behavior for **datetime** values sent to the server is to round them to nearest 1/300th of a second. In OLE DB Driver for SQL Server, this scenario causes a truncation of fractional seconds if rounding changes the day.|
|Possible truncation of seconds for **datetime** value.|An application built with OLE DB Driver for SQL Server that connects to a [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] 2005 server will truncate seconds and fractional seconds for time portion of data sent to the server if you bind to a datetime column with a type identifier of DBTYPE_DBTIMESTAMP (OLE DB) or SQL_TIMESTAMP (ODBC) and a scale of 0.<br /><br /> For example:<br /><br /> Input data: 1994-08-21 21:21:36.000<br /><br /> Inserted data: 1994-08-21 21:21:00.000|
|OLE DB data conversion from DBTYPE_DBTIME to DBTYPE_DATE no longer can cause the day to change.|Prior to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client 10.0, if the time part of a DBTYPE_DATE was within a half second of midnight, OLE DB conversion code caused the day to change. In OLE DB Driver for SQL Server, the day will not change (fractional seconds are truncated and not rounded).|
|IBCPSession::BCColFmt conversion changes.|In OLE DB Driver for SQL Server, when you use IBCPSession::BCOColFmt to convert SQLDATETIME or SQLDATETIME to a string type, a fractional value is exported. For example, when converting type SQLDATETIME to type SQLNVARCHARMAX, versions prior to [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client 10.0 returned<br /> 1989-02-01 00:00:00.<br />OLE DB Driver for SQL Server returns <br />1989-02-01 00:00:00.0000000.|
|Custom applications that use the BCP API can now see a warning.|The BCP API will generate a warning message if data length is greater than the specified length for a field for all types. Previously, this warning was only given for character types, but will not be issued for all types.|
|Inserting an empty string into a **sql_variant** bound as a date/time type generates an error.|In [!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] Native Client 9.0, inserting an empty string into a **sql_variant** bound as a date/time type did not generate an error. OLE DB Driver for SQL Server correctly generates an error in this situation.|
|[!INCLUDE[ssNoVersion](../../../includes/ssnoversion-md.md)] might return different results when a trigger runs.|Changes introduced in [!INCLUDE[ssKatmai](../../../includes/sskatmai-md.md)] might cause an application to have different results returned from a statement that caused a trigger to run when **NOCOUNT OFF** was in effect. In this situation, your application might generate an error. To resolve this error, set **NOCOUNT ON** in the trigger.|

## See also

[OLE DB Driver for SQL Server](../oledb-driver-for-sql-server.md)
