---
title: "Marking Business Objects as Safe for Scripting"
description: "Marking Business Objects as Safe for Scripting"
author: rothja
ms.author: jroth
ms.date: 11/09/2018
ms.service: sql
ms.subservice: ado
ms.topic: conceptual
helpviewer_keywords:
  - "business objects in RDS [ADO]"
---
# Marking Business Objects as Safe for Scripting
> [!IMPORTANT]
>  Beginning with Windows 8 and Windows Server 2012, RDS server components are no longer included in the Windows operating system (see Windows 8 and [Windows Server 2012 Compatibility Cookbook](https://www.microsoft.com/download/details.aspx?id=27416) for more detail). RDS client components will be removed in a future version of Windows. Avoid using this feature in new development work, and plan to modify applications that currently use this feature. Applications that use RDS should migrate to [WCF Data Service](/dotnet/framework/wcf/).  
  
 To help ensure a secure Internet environment, you need to mark any business objects instantiated with the [RDS.DataSpace](../../reference/rds-api/dataspace-object-rds.md) object's [CreateObject](../../reference/rds-api/createobject-method-rds.md) method as "safe for scripting." You need to ensure they are marked as such in the License area of the system registry before they can be used in DCOM.  
  
> [!NOTE]
>  Business objects marked as "safe for scripting" or safe for initialization can be instantiated and initialized by anyone over the network. Marking a business object "safe for scripting" does not make it safe. It is vitally important to make sure business objects are coded with the highest security to ensure that such objects do not present an unprotected access point for sensitive data.  
  
 To manually mark your business object as safe for scripting, create a text file with a .reg extension that contains the following text. In this example, \<*MyActiveXGUID*> is the hexadecimal GUID number of your business object. The following two numbers enable the safe-for-scripting feature:  
  
```console
[HKEY_CLASSES_ROOT\CLSID\<MyActiveXGUID>\Implemented   
Categories\{7DD95801-9882-11CF-9FA9-00AA006C42C4}]  
[HKEY_CLASSES_ROOT\CLSID\<MyActiveXGUID>\Implemented   
Categories\{7DD95802-9882-11CF-9FA9-00AA006C42C4}]  
```  
  
 Save the file and merge it into your registry by using the Registry Editor or double-clicking the .reg file in Windows Explorer.  
  
 Business objects created in Microsoft Visual Basic can be automatically marked as "safe for scripting" with the Package and Deployment Wizard. When the wizard asks you to specify safety settings, select **Safe for initialization** and **Safe for scripting**.  
  
 On the last step, the Application Setup Wizard creates an .htm and a .cab file. You can then copy these two files to the target computer and double-click the .htm file to load the page and correctly register the server.  
  
 Because the business object will be installed in the Windows\System32\Occache directory by default, move it to the Windows\System32 directory and change the **HKEY_CLASSES_ROOT\CLSID\\**\<*MyActiveXGUID*>\\**InprocServer32** registry key to match the correct path.