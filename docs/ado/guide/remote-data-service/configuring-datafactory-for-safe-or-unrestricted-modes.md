---
title: "Configuring DataFactory for Safe or Unrestricted Modes"
description: "Configuring DataFactory for Safe or Unrestricted Modes"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "DataFactory configuration in RDS [ADO]"
---
# Configuring DataFactory for Safe or Unrestricted Modes
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
 By default, ADO is installed with a "safe" [RDSServer.DataFactory](../../reference/rds-api/datafactory-object-rdsserver.md) configuration. Safe mode for RDS Server components means that the following are true:  
  
1.  Handler is required with the RDSServer.DataFactory (this is mandated by a registry key setting).  
  
2.  The default handler, msdfmap.handler, is registered, present in the safe-handler list, and marked as the default handler.  
  
3.  Msdfmap.ini file is installed in the Windows directory. You must configure this file according to your needs, before you use RDS in three-tier mode.  
  
 Optionally, you can configure an unrestricted **DataFactory** installation. **DataFactory** can be used directly without the custom handler. Users can still use a custom handler by modifying the connection strings, but it is not required. For more information about the implications of using the **RDSServer.DataFactory** object, see [Securing RDS Applications](./securing-rds-applications.md).  
  
 The registry file handsafe.reg has been provided to set up the handler registry entries for a safe configuration. To run in safe mode, run handsafe.reg.  
  
 After running handsafe.reg, you must stop and restart the World Wide Web Publishing Service on the Web server by typing the following commands in a Command Prompt window: "NET STOP W3SVC" and "NET START W3SVC".  
  
## See Also  
 [DataFactory Customization](./datafactory-customization.md)   
 [RDS Fundamentals](./rds-fundamentals.md)