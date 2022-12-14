---
description: "Usage Counting"
title: "Usage Counting | Microsoft Docs"
ms.custom: ""
ms.date: "01/19/2017"
ms.service: sql
ms.reviewer: ""
ms.subservice: connectivity
ms.topic: conceptual
helpviewer_keywords: 
  - "usage counts [ODBC]"
  - "file usage counts [ODBC]"
  - "installing ODBC components [ODBC], usage counts"
  - "subkeys [ODBC], usage counts"
ms.assetid: 0678aee9-8256-463c-89dd-77b1a0dfdd60
author: David-Engel
ms.author: v-davidengel
---
# Usage Counting
> [!NOTE]  
>  Starting with Windows XP and Windows Server 2003, ODBC is included in the Windows operation system. You should only explicitly install ODBC on earlier versions of Windows.  
  
 Two types of usage counts are maintained in the registry for each component: a component usage count and one or more optional file usage counts. The component usage count helps the installer DLL maintain registry entries. It is stored in the UsageCount value under the ODBC Core, driver, and translator subkeys. For the format of the UsageCount value and more information about these subkeys, see [Registry Entries for ODBC Components](../../../odbc/reference/install/registry-entries-for-odbc-components.md).  
  
 When a component is first installed, the installer DLL creates a subkey for it and sets the data for the UsageCount value in that subkey to 1. When the component is installed again, the installer DLL increments the usage count. When the component is removed, the installer DLL decrements the usage count. If the usage count falls to 0, the installer DLL removes the subkey for the component.  
  
> [!CAUTION]  
>  An application should not physically remove Driver Manager files when the component usage count and the file usage count reach zero.  
  
 File usage counts help determine when a file must actually be copied or deleted as opposed to incrementing or decrementing the usage count. This is important because ODBC components, and therefore the files in ODBC components, are shared and can be installed or removed by a variety of applications. The application can delete driver and translator files if the component usage count and the file usage count reach zero. Driver Manager files should not, however, be deleted when both the component usage count and the file usage count have reached zero, because these files can be used by other applications that have not incremented the file usage count.  
  
> [!NOTE]  
>  File usage counts are optional in Microsoft® WindowsNT®/Windows2000.  
  
 File usage counts are maintained by the setup program after it calls **SQLInstallDriverManager**, **SQLInstallDriverEx**, **SQLInstallTranslatorEx**, **SQLRemoveDriverManager**, **SQLRemoveDriver**, or **SQLRemoveTranslator**.  
  
 When a component is first installed, the setup program or installer DLL creates a value under the following key for each file in that component that is not already on the system:  
  
> [!NOTE]  
>  HKEY_LOCAL_MACHINE  
>   
>  SOFTWARE  
>   
>  Microsoft  
>   
>  Windows  
>   
>  CurrentVersion  
>   
>  SharedDlls  
  
 It sets the data for those values to 1 and copies the file to the system. When the component is installed again, the setup program or installer DLL increments the usage counts. When the component is removed, the setup program or installer DLL decrements the usage counts. If any usage count falls to 0, the setup program or installer DLL removes the value for the file and, if the component is a driver or a translator, deletes the file. Driver Manager files should not be deleted.  
  
 The format of the file usage count value is shown in the following table.  
  
|Name|Data type|Data|  
|----------|---------------|----------|  
|*full-path*|REG_DWORD|*count*|  
  
 For example, suppose a driver for Informix uses the Infrmx32.dll and Infrmx32.hlp files, and suppose that this driver has been installed twice. The values under the SharedDlls subkey for the Informix driver would be as follows:  
  
```  
C:\WINDOWS\SYSTEM32\INFRMX32.DLL : REG_DWORD : 0x2  
C:\WINDOWS\SYSTEM32\INFRMX32.HLP : REG_DWORD : 0x2  
```
