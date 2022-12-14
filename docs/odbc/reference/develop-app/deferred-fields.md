---
description: "Deferred Fields"
title: "Deferred Fields | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "descriptors [ODBC], deferred fields"
  - "deferred fields [ODBC]"
ms.assetid: 5abeb9cc-4070-4f43-a80d-ad6a2004e5f3
author: David-Engel
ms.author: v-davidengel
---
# Deferred Fields
The values of *deferred fields* are not used when they are set, but the driver saves the addresses of the variables for a deferred effect. For an application parameter descriptor, the driver uses the contents of the variables at the time of the call to **SQLExecDirect** or **SQLExecute**. For an application row descriptor, the driver uses the contents of the variables at the time of the fetch.  
  
 The following are deferred fields:  
  
-   The SQL_DESC_DATA_PTR and SQL_DESC_INDICATOR_PTR fields of a descriptor record.  
  
-   The SQL_DESC_OCTET_LENGTH_PTR field of an application descriptor record.  
  
-   In the case of a multirow fetch, the SQL_DESC_ARRAY_STATUS_PTR and SQL_DESC_ROWS_PROCESSED_PTR fields of a descriptor header.  
  
 When a descriptor is allocated, the deferred fields of each descriptor record initially have a null value. The meaning of the null value is as follows:  
  
-   If SQL_DESC_ARRAY_STATUS_PTR has a null value, a multirow fetch fails to return this component of the per-row diagnostic information.  
  
-   If SQL_DESC_DATA_PTR has a null value, the record is unbound.  
  
-   If the SQL_DESC_OCTET_LENGTH_PTR field of an ARD has a null value, the driver does not return length information for that column.  
  
-   If the SQL_DESC_OCTET_LENGTH_PTR field of an APD has a null value and the parameter is a character string, the driver assumes that string is null-terminated. For output dynamic parameters, a null value in this field prevents the driver from returning length information. (If the SQL_DESC_TYPE field does not indicate a character-string parameter, the SQL_DESC_OCTET_LENGTH_PTR field is ignored.)  
  
 The application must not deallocate or discard variables used for deferred fields between the time it associates them with the fields and the time the driver reads or writes them.
