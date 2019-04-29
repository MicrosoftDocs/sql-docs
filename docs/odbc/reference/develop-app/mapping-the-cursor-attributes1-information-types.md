---
title: "Mapping the Cursor Attributes1 Information Types | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "compatibility [ODBC], mapping cursor attributes1 information types"
  - "application upgrades [ODBC], mapping cursor attributes1 information types"
  - "mapping cursor attributes1 information types [ODBC]"
  - "backward compatibility [ODBC], mapping cursor attributes1 information types"
  - "upgrading applications [ODBC], mapping cursor attributes1 information types"
ms.assetid: 9f112449-ca86-45ac-a865-e6174d67f91b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Mapping the Cursor Attributes1 Information Types
When an ODBC 3.*x* application calls **SQLGetInfo** in an ODBC 2*.x* driver with the SQL_XXXX_CURSOR_ATTRIBUTES1 information type (for dynamic, forward-only, keyset-driver, or static cursors), the setting of the bits returned by Driver Manager depends on what the ODBC 2.*x* driver returns for the corresponding ODBC 2.*x* information types. The bits are set as shown in the following table.  
  
|Bit in<br /><br /> SQL_XXXX_CURSOR_ATTRIBUTES1|Cursor type|ODBC 2.*x* information<br /><br /> type|  
|-----------------------------------------------|-----------------|-------------------------------------|  
|SQL_CA1_NEXT|All|SQL_FETCH_DIRECTION|  
|SQL_CA1_ABSOLUTE SQL_CA1_RELATIVE SQL_CA1_BOOKMARK|Dynamic, keyset-driver, static|SQL_FETCH_DIRECTION|  
|SQL_CA1_LOCK_NO_CHANGE SQL_CA1_LOCK_UNLOCK SQL_CA1_LOCK_EXCLUSIVE|Dynamic, keyset-driver, static|SQL_LOCK_TYPES|  
|SQL_CA1_POSITIONED_UPDATE SQL_CA1_POSITIONED_DELETE SQL_CA1_SELECT_FOR_UPDATE|All|SQL_POSITIONED_STATEMENTS|  
|SQL_CA1_POS_POSITION SQL_CA1_POS_DELETE SQL_CA1_POS_REFRESH SQL_CA1_POS_BULK_ADD|Dynamic, keyset-driver, static|SQL_POS_OPERATIONS|
