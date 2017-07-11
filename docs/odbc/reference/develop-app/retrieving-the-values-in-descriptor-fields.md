---
title: "Retrieving the Values in Descriptor Fields | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: "sql-non-specified"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "drivers"
ms.tgt_pltfrm: ""
ms.topic: "article"
helpviewer_keywords: 
  - "descriptors [ODBC], retrieving or setting field values"
ms.assetid: c05b180f-c2b0-437b-8d1c-ce7f4da93287
caps.latest.revision: 5
author: "MightyPen"
ms.author: "genemi"
manager: "jhubbard"
---
# Retrieving the Values in Descriptor Fields
An application can call **SQLGetDescField** to obtain a single field of a descriptor record. **SQLGetDescField** gives the application access to all the descriptor fields defined in ODBC, and to driver-defined fields as well.  
  
 **SQLGetDescRec** can be called to retrieve the settings of multiple descriptor fields that affect the data type and storage of column or parameter data.