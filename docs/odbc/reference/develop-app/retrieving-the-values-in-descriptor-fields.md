---
title: "Retrieving the Values in Descriptor Fields"
description: "Retrieving the Values in Descriptor Fields"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "descriptors [ODBC], retrieving or setting field values"
---
# Retrieving the Values in Descriptor Fields
An application can call **SQLGetDescField** to obtain a single field of a descriptor record. **SQLGetDescField** gives the application access to all the descriptor fields defined in ODBC, and to driver-defined fields as well.  
  
 **SQLGetDescRec** can be called to retrieve the settings of multiple descriptor fields that affect the data type and storage of column or parameter data.
