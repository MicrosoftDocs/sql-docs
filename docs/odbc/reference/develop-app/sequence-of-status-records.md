---
description: "Sequence of Status Records"
title: "Sequence of Status Records | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "diagnostic information [ODBC], diagnostic records"
  - "status records [ODBC]"
  - "diagnostic records [ODBC]"
ms.assetid: 0e0436cc-230f-44b0-b373-04a57e83ee76
author: David-Engel
ms.author: v-davidengel
---
# Sequence of Status Records
If two or more status records are returned, the Driver Manager and driver rank them according to the following rules. The record with the highest rank is the first record. The source of a record (Driver Manager, driver, gateway, and so on) is not considered when ranking records.  
  
-   **Errors** Status records that describe errors have the highest rank. Among error records, records that indicate a transaction failure or possible transaction failure outrank all other records. If two or more records describe the same error condition, SQLSTATEs defined by the Open Group CLI specification (classes 03 through HZ) outrank ODBC-defined and driver-defined SQLSTATEs.  
  
-   **Implementation-defined No Data Values** Status records that describe driver-defined No Data values (class 02) have the second highest rank.  
  
-   **Warnings** Status records that describe warnings (class 01) have the lowest rank. If two or more records describe the same warning condition, warning SQLSTATEs defined by the Open Group CLI specification outrank ODBC-defined and driver-defined SQLSTATEs.  
  
 If there are two or more records with the highest rank, it is undefined which record is the first record. The order of all other records is undefined. In particular, because warnings may appear before errors, applications should check all status records when a function returns a value other than SQL_SUCCESS.
