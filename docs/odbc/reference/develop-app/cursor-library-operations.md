---
description: "Cursor Library Operations"
title: "Cursor Library Operations | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "ODBC cursor library [ODBC], backward compatibility"
  - "compatibility [ODBC], cursor library"
  - "upgrading applications [ODBC], cursor library"
  - "application upgrades [ODBC], cursor library"
  - "backward compatibility [ODBC], cursor library"
  - "cursor library [ODBC], backward compatibility"
ms.assetid: 04d514b1-dc4d-4b84-bf35-60f4657ef1f6
author: David-Engel
ms.author: v-davidengel
---
# Cursor Library Operations
> [!IMPORTANT]  
>  This feature will be removed in a future version of Windows. Avoid using this feature in new development work and plan to modify applications that currently use this feature. Microsoft recommends using the driver's cursor functionality.  
  
 If an application working with an ODBC *2.x* driver makes calls to the ODBC *3.x* cursor library, the application might be able to use ODBC *3.x* features that are not supported by the ODBC *2.x* driver. An application writer should be careful how these features are used, however. Use of the ODBC *3.x* cursor library does not make an ODBC *2.x* driver into an ODBC *3.x* driver.
