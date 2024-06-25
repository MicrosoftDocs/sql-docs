---
title: "Record Count"
description: "Record Count"
author: David-Engel
ms.author: davidengel
ms.date: "01/19/2017"
ms.service: sql
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords:
  - "record count [ODBC]"
  - "descriptors [ODBC], record count"
---
# Record Count
The SQL_DESC_COUNT header field of a descriptor is the one-based index of the highest-numbered record that contains data. This field is not a count of all columns or parameters that are bound. When a descriptor is allocated, the initial value of SQL_DESC_COUNT is 0.  
  
 The driver takes any action necessary to allocate and maintain whatever storage it requires to hold descriptor information. The application does not explicitly specify the size of a descriptor nor allocate new records. When the application provides information for a descriptor record whose number is higher than the value of SQL_DESC_COUNT, the driver automatically increases SQL_DESC_COUNT. When the application unbinds the highest-numbered descriptor record, the driver automatically decreases SQL_DESC_COUNT to contain the number of the highest remaining bound record.
