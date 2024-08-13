---
title: "Initialization of Descriptor Fields"
description: "Initialization of Descriptor Fields"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "descriptors [ODBC], allocating and freeing"
  - "initializing descriptor fields [ODBC]"
  - "allocating and freeing descriptors [ODBC]"
---
# Initialization of Descriptor Fields
When an application row descriptor is allocated, its fields receive initial values as indicated in [SQLSetDescField](../../../odbc/reference/syntax/sqlsetdescfield-function.md). The initial value of the SQL_DESC_TYPE field is SQL_DEFAULT. This provides for a standard treatment of database data for presentation to the application. The application may specify different treatment of the data by setting fields of the descriptor record.  
  
 The initial value of SQL_DESC_ARRAY_SIZE in the descriptor header is 1. The application can modify this field to enable multirow fetch.  
  
 The concept of a default value is not valid for the fields of an IRD. An application can gain access to the fields of an IRD only when there is a prepared or executed statement associated with it.  
  
 Certain fields of an IPD are defined only after the IPD has been automatically populated by the driver. If not, they are undefined. These fields are SQL_DESC_CASE_SENSITIVE, SQL_DESC_FIXED_PREC_SCALE, SQL_DESC_TYPE_NAME, SQL_DESC_UNSIGNED, and SQL_DESC_LOCAL_TYPE_NAME.
