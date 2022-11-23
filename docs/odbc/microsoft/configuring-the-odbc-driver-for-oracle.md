---
description: "Configuring the ODBC Driver for Oracle"
title: "Configuring the ODBC Driver for Oracle | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "configuring ODBC driver for Oracle [ODBC]"
  - "ODBC driver for Oracle [ODBC], configuring"
ms.assetid: 0a5f827c-0b80-4627-85cb-f10292b9fb33
author: David-Engel
ms.author: v-davidengel
---
# Configuring the ODBC Driver for Oracle
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Instead, use the ODBC driver provided by Oracle.  
  
 You can control performance of the ODBC Driver for Oracle by knowing the data environment and correctly setting the parameters of the data source connection through the [ODBC Data Source Administrator](../../odbc/admin/odbc-data-source-administrator.md) dialog box or through connect string parameters. The dialog box provides the following controls for connecting to a data source using the dialog box or using connect strings:  
  
-   **User DSN tab** Lists the Data Source Names that are local to the computer.  
  
-   **System DSN tab** Enables you to add or remove a system data source. System data sources can be accessed by all users on the local computer.  
  
-   **File DSN tab** Enables you to add or remove a file data source from the local computer. File data sources can be shared by all users who have the same driver installed.  
  
-   **Drivers tab** Lists the installed ODBC drivers.  
  
-   **Tracing tab** Enables you to specify how the ODBC Driver Manager traces calls to ODBC functions. You can configure tracing separately for each installed ODBC application.  
  
-   **Connection Pooling tab** Enables you to select connection options for each installed driver.  
  
-   **About tab** Lists the installed ODBC component files.  
  
 After you add a data source, you can use the **ODBC Data Source Administrator** dialog box to configure the access to your data source. Select a data source, and then click one of the tabs to edit or review the information.
