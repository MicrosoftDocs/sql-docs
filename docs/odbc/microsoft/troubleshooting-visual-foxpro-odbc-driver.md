---
description: "Troubleshooting (Visual FoxPro ODBC Driver)"
title: "Troubleshooting (Visual FoxPro ODBC Driver) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "set ANSI [ODBC]"
  - "troubleshooting Visual FoxPro ODBC driver [ODBC]"
  - "remote views [ODBC]"
  - "multitiered views [ODBC]"
  - "parameterized views [ODBC], Visual FoxPro ODBC driver"
  - "fetches [ODBC], Visual FoxPro ODBC driver"
  - "positioned updates [ODBC]"
  - "background fetching [ODBC]"
ms.assetid: fd478dd8-666a-4f0a-a2d6-b94e81cbbe4b
author: David-Engel
ms.author: v-davidengel
---
# Troubleshooting (Visual FoxPro ODBC Driver)
The following sections discuss how to improve performance and solve problems you might encounter while using the Visual FoxPro ODBC Driver.  
  
## Accessing Parameterized Views  
 You can't access parameterized views in a Visual FoxPro database using the driver. A parameterized view creates a WHERE clause in the view's SQL **SELECT** statement that limits the records downloaded to those records that meet the conditions of the WHERE clause built using the value supplied for the parameter. Because the driver doesn't support passing parameters to the view, attempts to access a parameterized view by using the driver will fail.  
  
 The parameter value can be supplied at run time or passed programmatically to the view.  
  
## Accessing Remote Views  
 You can't access remote views in a Visual FoxPro database using the driver. Remote views are views that access either non-FoxPro data or a combination of FoxPro and non-FoxPro data. To access remote views, use Visual FoxPro.  
  
## Deleting Records  
 You can mark records for deletion using the driver, but you can't permanently remove records from the database. To permanently remove records from a table, use Visual FoxPro.  
  
## Increasing Performance Using Background Fetching  
 You can improve performance on large fetches by using the background fetching feature of the driver. Background fetching uses a separate thread to fetch data requested from a specific data source.  
  
 You can employ background fetching for a data source in one of the following ways:  
  
-   Check the **Fetch data in background** checkbox on the [ODBC Visual FoxPro Setup dialog box](../../odbc/microsoft/odbc-visual-foxpro-setup-dialog-box.md).  
  
-   Use the BackgroundFetch attribute keyword in your connection string.  
  
 For information on connection string attribute keywords, see [Using Connection Strings](../../odbc/microsoft/using-connection-strings.md).  
  
## Updating Multitiered Views  
 A multitiered view is a view based on one or more views rather than on a base table. When you update data in a multitiered view, the updates go down only one level, to the view on which the top-level view is based; base tables are not updated.  
  
## Using Data Definition Language (DDL) in Stored Procedures  
 You can't use DDL, such as CREATE TABLE or ALTER TABLE, in Visual FoxPro stored procedures.  
  
 For information on language you can use in stored procedures, see [Support for Rules, Triggers, Default Values, and Stored Procedures](../../odbc/microsoft/support-rules-triggers-defaults-stored-procedures-visual-foxpro-odbc-driver.md).  
  
## Using Positioned Updates  
 The driver doesn't support positioned updates. Use the SQL WHERE clause to identify the rows you want to update.  
  
## Using the SET ANSI Command  
 If you're a Visual FoxPro developer, you should be aware that the default setting for SET ANSI is ON for the driver, in contrast to a default setting of OFF for Visual FoxPro. The default ON setting for SET ANSI allows Visual FoxPro data sources to behave consistently with other ODBC data sources that typically perform exact comparisons. You can change the default setting. For more information, see [SET ANSI](../../odbc/microsoft/set-ansi-command.md).
