---
title: "New-RestoreFolder cmdlet | Microsoft Docs"
ms.custom: ""
ms.date: "03/07/2017"
ms.prod: "sql-server-2016"
ms.reviewer: ""
ms.suite: ""
ms.technology: 
  - "analysis-services"
ms.tgt_pltfrm: ""
ms.topic: "reference"
ms.assetid: 5938b3a9-6412-45fc-86f8-264651d01598
caps.latest.revision: 10
author: "Minewiskan"
ms.author: "owend"
manager: "erikre"
---
# New-RestoreFolder cmdlet

[!INCLUDE[ssas-appliesto-sqlas-all](../../includes/ssas-appliesto-sqlas-all.md)]

  Restores an original folder to a new folder.  

  >[!NOTE] This article may contain outdated information and examples.  
>
  
## Syntax  
 `New-RestoreFolder [-OriginalFolder] <String> [-NewFolder] <String> [-AsTemplate] [-Server <String>] [-Credential <PSCredential>] [-Verbose] [-Debug] [-ErrorAction <ActionPreference>] [-WarningAction <ActionPreference>] [-ErrorVariable <String>] [-WarningVariable <String>] [-OutVariable <String>] [-OutBuffer <Int32>] [-WhatIf] [-Confirm]`  
  
 `New-RestoreFolder [-Server <String>] [-Credential <PSCredential>] [-Verbose] [-Debug] [-ErrorAction <ActionPreference>] [-WarningAction <ActionPreference>] [-ErrorVariable <String>] [-WarningVariable <String>] [-OutVariable <String>] [-OutBuffer <Int32>] [-WhatIf] [-Confirm]`  
  
 Common parameters, such as –Verbose, -Debug, error and warning parameters, -Whatif, and –Confirm are documented in the Windows PowerShell reference. For more information, see [about_CommonParameters](http://technet.microsoft.com/library/dd315352.aspx).  
  
## Description  
 The New-RestoreFolder cmdlet is used to create a new folder based on the name of the original folder.  
  
## Parameters  
  
### -OriginalFolder \<string>  
 Gets the original folder location.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|0|  
|Default value||  
|Accept pipeline input?|true|  
|Accept wildcard characters?|false|  
  
### -NewFolder \<string>  
 Sets the location of a new folder.  
  
|||  
|-|-|  
|Required?|true|  
|Position?|1|  
|Default value||  
|Accept pipeline input?|true|  
|Accept wildcard characters?|false|  
  
### -AsTemplate \<SwitchParameter>  
 Specifies whether the object should be created in memory and returned.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Server \<string>  
 Specifies the Analysis Services instance to which the cmdlet will connect and execute. If no server name is provided, a connection will be made to localhost. For default instances, specify just the server name. For named instances, use the format servername\instancename. For HTTP connections, use the format http[s]://server[:port]/virtualdirectory/msmdpump.dll.  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value|localhost|  
|Accept pipeline input?|false|  
|Accept wildcard characters?|false|  
  
### -Credential \<PSCredential>  
 This parameter is used to pass in a username and password when using an HTTP connection to an Analysis Service instance, for an instance that you have configured for HTTP access. For more information, see [Configure HTTP Access to Analysis Services on Internet Information Services &#40;IIS&#41; 8.0](../../analysis-services/instances/configure-http-access-to-analysis-services-on-iis-8-0.md) for HTTP connections.  
  
 If this parameter is specified, the username and password will be used to connect to the specified Analysis Server instance. If no credentials are specified default windows account of the user who is running the tool will be used.  
  
 To use this parameter, first create a PSCredential object using Get-Credential to specify the username and password (for example, `$Cred=Get-Credential “adventure-works\bobh”`. You can then pipe this object to the –Credential parameter `(-Credential:$Cred`).  
  
|||  
|-|-|  
|Required?|false|  
|Position?|named|  
|Default value||  
|Accept pipeline input?|True (ByValue)|  
|Accept wildcard characters?|false|  
  
## Inputs and Outputs  
 The input type is the type of the objects that you can pipe to the cmdlet. The return type is the type of the objects that the cmdlet returns.  
  
|||  
|-|-|  
|Inputs||  
|Outputs|None|  
  