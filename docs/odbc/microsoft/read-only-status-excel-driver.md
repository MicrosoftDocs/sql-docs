---
title: "Read-Only Status (Excel Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.prod: sql
ms.prod_service: connectivity
ms.reviewer: ""
ms.technology: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "read-only status for Excel driver [ODBC]"
  - "Excel driver [ODBC], read-only status"
ms.assetid: ef5d773b-4f8f-4005-b985-84b53d8e9f9b
author: MightyPen
ms.author: genemi
manager: craigg
---
# Read-Only Status (Excel Driver)
When the Microsoft Excel driver is used, data source tables are opened as read-only by default, and can be opened by only one user at a time. Even though tables have read-only status, however, applications can perform insertions and updates for Microsoft Excel tables.  
  
 When an application performs a Save As command on Microsoft Excel data through the Microsoft Excel driver, the application should create a new table and insert the data to be saved into the new table. Inserts result in an append to the table. No other operations can be performed on the table until it is closed and reopened. Once the table is closed, no subsequent insert can be performed because the table is then a read-only table.  
  
 It is possible to update values when using the Microsoft Excel driver, but a row cannot be deleted from a table based on a Microsoft Excel spreadsheet, so updates are not considered officially supported by the Microsoft Excel driver.
