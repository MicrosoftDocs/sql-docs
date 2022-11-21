---
description: "Environment Transitions"
title: "Environment Transitions | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: reference
helpviewer_keywords: 
  - "environment transitions [ODBC]"
  - "transitioning states [ODBC], environment"
  - "state transitions [ODBC], environment"
ms.assetid: 9d11b1ab-f4c8-48ca-9812-8c04303f939d
author: David-Engel
ms.author: v-davidengel
---
# Environment Transitions
ODBC environments have the following three states.  
  
|State|Description|  
|-----------|-----------------|  
|E0|Unallocated environment|  
|E1|Allocated environment, unallocated connection|  
|E2|Allocated environment, allocated connection|  
  
 The following tables show how each ODBC function affects the environment state.  
  
## SQLAllocHandle  
  
|E0<br /><br /> Unallocated|E1<br /><br /> Allocated|E2<br /><br /> Connection|  
|------------------------|----------------------|-----------------------|  
|E1[1]|--[4]|--[4]|  
|(IH)[2]|E2[5]<br />(HY010)[6]|--[4]|  
|(IH)[3]|(IH)|--[4]|  
  
 [1]   This row shows transitions when *HandleType* was SQL_HANDLE_ENV.  
  
 [2]   This row shows transitions when *HandleType* was SQL_HANDLE_DBC.  
  
 [3]   This row shows transitions when *HandleType* was SQL_HANDLE_STMT or SQL_HANDLE_DESC.  
  
 [4]   Calling **SQLAllocHandle** with *OutputHandlePtr* pointing to a valid handle overwrites that handle. This might be an application programming error.  
  
 [5]   The SQL_ATTR_ODBC_VERSION environment attribute had been set on the environment.  
  
 [6]   The SQL_ATTR_ODBC_VERSION environment attribute had not been set on the environment.  
  
## SQLDataSources and SQLDrivers  
  
|E0<br /><br /> Unallocated|E1<br /><br /> Allocated|E2<br /><br /> Connection|  
|------------------------|----------------------|-----------------------|  
|(IH)|--[1]<br />(HY010)[2]|--[1]<br />(HY010)[2]|  
  
 [1]   The SQL_ATTR_ODBC_VERSION environment attribute had been set on the environment.  
  
 [2]   The SQL_ATTR_ODBC_VERSION environment attribute had not been set on the environment.  
  
## SQLEndTran  
  
|E0<br /><br /> Unallocated|E1<br /><br /> Allocated|E2<br /><br /> Connection|  
|------------------------|----------------------|-----------------------|  
|(IH)[1]|--[3]<br />(HY010)[4]|--[3]<br />(HY010)[4]|  
|(IH)[2]|(IH)|--|  
  
 [1]   This row shows transitions when *HandleType* was SQL_HANDLE_ENV.  
  
 [2]   This row shows transitions when *HandleType* was SQL_HANDLE_DBC.  
  
 [3]   The SQL_ATTR_ODBC_VERSION environment attribute had been set on the environment.  
  
 [4]   The SQL_ATTR_ODBC_VERSION environment attribute had not been set on the environment.  
  
## SQLFreeHandle  
  
|E0<br /><br /> Unallocated|E1<br /><br /> Allocated|E2<br /><br /> Connection|  
|------------------------|----------------------|-----------------------|  
|(IH)[1]|E0|(HY010)|  
|(IH)[2]|(IH)|--[4]<br />E1[5]|  
|(IH)[3]|(IH)|--|  
  
 [1]   This row shows transitions when *HandleType* was SQL_HANDLE_ENV.  
  
 [2]   This row shows transitions when *HandleType* was SQL_HANDLE_DBC.  
  
 [3]   This row shows transitions when *HandleType* was SQL_HANDLE_STMT or SQL_HANDLE_DESC.  
  
 [4]   There were other allocated connection handles.  
  
 [5]   The connection handle specified in *Handle* was the only allocated connection handle.  
  
## SQLGetDiagField and SQLGetDiagRec  
  
|E0<br /><br /> Unallocated|E1<br /><br /> Allocated|E2<br /><br /> Connection|  
|------------------------|----------------------|-----------------------|  
|(IH)[1]|--|--|  
|(IH)[2]|(IH)|--|  
  
 [1]   This row shows transitions when *HandleType* was SQL_HANDLE_ENV.  
  
 [2]   This row shows transitions when *HandleType* was SQL_HANDLE_DBC, SQL_HANDLE_STMT, or SQL_HANDLE_DESC.  
  
## SQLGetEnvAttr  
  
|E0<br /><br /> Unallocated|E1<br /><br /> Allocated|E2<br /><br /> Connection|  
|------------------------|----------------------|-----------------------|  
|(IH)|--[1]<br />(HY010)[2]|--|  
  
 [1]   The SQL_ATTR_ODBC_VERSION environment attribute had been set on the environment.  
  
 [2]   The SQL_ATTR_ODBC_VERSION environment attribute had not been set on the environment.  
  
## SQLSetEnvAttr  
  
|E0<br /><br /> Unallocated|E1<br /><br /> Allocated|E2<br /><br /> Connection|  
|------------------------|----------------------|-----------------------|  
|(IH)|--[1]<br />(HY010)[2]|(HY011)|  
  
 [1]   The SQL_ATTR_ODBC_VERSION environment attribute had been set on the environment.  
  
 [2]   The *Attribute* argument was not SQL_ATTR_ODBC_VERSION, and the SQL_ATTR_ODBC_VERSION environment attribute had not been set on the environment.  
  
## All Other ODBC Functions  
  
|E0<br /><br /> Unallocated|E1<br /><br /> Allocated|E2<br /><br /> Connection|  
|------------------------|----------------------|-----------------------|  
|(IH)|(IH)|--|
