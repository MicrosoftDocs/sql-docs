---
description: "Translator Setup DLLs"
title: "Translator Setup DLLs | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "translator setup DLL [ODBC]"
ms.assetid: b3ca79e9-01b9-4541-81de-bbbad24ca736
author: David-Engel
ms.author: v-davidengel
---
# Translator Setup DLLs
> [!NOTE]  
>  Starting with Windows XP and Windows Server 2003, ODBC is included in the Windows operation system. You should only explicitly install ODBC on earlier versions of Windows.  
  
 The translator setup DLL contains the **ConfigTranslator** function, which returns the default option for a translator. If necessary, it prompts the user for this information. For a complete description of this function, see [Setup DLL API Reference](../../../odbc/reference/syntax/setup-dll-api-reference.md).  
  
 The translator setup DLL is written by the translator developer. It can be part of the translator DLL or a separate DLL.
