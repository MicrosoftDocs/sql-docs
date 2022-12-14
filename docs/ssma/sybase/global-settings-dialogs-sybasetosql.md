---
description: "Global Settings (Dialogs)  (SybaseToSQL)"
title: "Global Settings (Dialogs)  (SybaseToSQL) | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: ssma
ms.topic: conceptual
ms.assetid: e11452b7-ba94-4367-a745-5ccf1764acec
author: cpichuka 
ms.author: cpichuka 
---
# Global Settings (Dialogs)  (SybaseToSQL)
Use the Dialogs page of the **Global Settings** dialog box to specify the default user action and warning settings for SSMA.  
  
To access the dialog settings on the **Tools** menu, select **Global Settings**, click **GUI** at the bottom of the left pane, and then select **Dialogs**.  
  
## Options  
**Warn before overwriting objects**  
When SSMA converts objects to SQL Server, some objects may already exist in the project's SQL Server metadata. These objects may have already been converted, or the objects may simply have the same name within the target schema as objects you are going to convert.  
  
Use this option to specify whether SSMA should prompt you for overwriting duplicate object definitions:  
  
-   If you select **True**, SSMA will display a warning dialog box when it encounters a duplicate object. In this dialog, you can specify to overwrite individual objects or all duplicate objects, or to skip individual objects or all duplicate objects.  
  
-   If you select **False**, the **Object overwrite default action** option appears where you specify the default action.  
  
**Object overwrite default action**  
This option appears if you select **False** for the **Warn before overwriting objects** option.  
  
Use this option to specify the default object overwrite behavior:  
  
-   If you select **True**, SSMA will automatically overwrite objects in the SQL Server project metadata that have the same name and are in the same target schema as the object to be converted.  
  
-   If you select **False**, SSMA does not overwrite object metadata during conversion.  
  
