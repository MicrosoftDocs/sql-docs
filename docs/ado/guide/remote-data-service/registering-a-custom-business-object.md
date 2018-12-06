---
title: "Registering a Custom Business Object | Microsoft Docs"
ms.prod: sql
ms.prod_service: connectivity
ms.technology: connectivity
ms.custom: ""
ms.date: 11/09/2018
ms.reviewer: ""
ms.topic: conceptual
helpviewer_keywords: 
  - "custom business object in RDS [ADO]"
  - "registering custom business objects in RDS [ADO]"
  - "business objects in RDS [ADO]"
ms.assetid: e9032ad8-d14c-42e3-ba13-cb5f00084a79
author: MightyPen
ms.author: genemi
manager: craigg
---
# Registering a Custom Business Object
To successfully launch a custom business object (.dll or .exe) through the Web server, the business object's ProgID must be entered into the registry as explained in this procedure. This RDS feature protects the security of your Web server by running only sanctioned executables.  
  
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](https://go.microsoft.com/fwlink/?LinkId=199565).  
  
> [!NOTE]
>  For MDAC 2.0 and later and Windows DAC, the default business object, [RDSServer.DataFactory](../../../ado/reference/rds-api/datafactory-object-rdsserver.md), is not registered by default during the MDAC/Windows DAC installation. However, if **RDSServer.DataFactory** was registered as safe for execution on the computer prior to the installation, the registry entry is maintained for the new installation.  
  
### To register a custom business object:  
  
1.  Click **Start** and then click **Run**.  
  
2.  Type **RegEdit** and click **OK**.  
  
3.  In the Registry Editor, navigate to the **HKEY_LOCAL_MACHINE\System\CurrentControlSet\Services\W3SVC\Parameters\ADCLaunch** registry key.  
  
4.  Select the **ADCLaunch** key, and then from the **Edit**menu, point to **New** and click **Key**.  
  
5.  Type the ProgID of your custom business object and click **Enter**. Leave the **Value** entry blank.


