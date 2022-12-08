---
title: Load the SMO Assemblies in Windows PowerShell
description: Learn how to load the SQL Server Management Object (SMO) assemblies in Windows PowerShell scripts that don't use the SQL Server PowerShell provider.
ms.service: sql
ms.subservice: sql-server-powershell
ms.topic: conceptual
author: markingmyname
ms.author: maghan
ms.reviewer: matteot, drskwier
ms.custom: ""
ms.date: 10/14/2020
---

# Load the SMO Assemblies in Windows PowerShell

[!INCLUDE[SQL Server Azure SQL Database Synapse Analytics PDW ](../includes/applies-to-version/sql-asdb-asdbmi-asa-pdw.md)]

This article describes how to load the SQL Server Management Object (SMO) assemblies in Windows PowerShell scripts that don't use the SQL Server PowerShell provider.  

[!INCLUDE [sql-server-powershell-version](../includes/sql-server-powershell-version.md)]

The preferred mechanism for loading the SMO assemblies is to load the **SqlServer** module. The [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] provider included in the module automatically loads the SMO assemblies, and also implements features that extend the usefulness of the SMO objects in PowerShell scripts.

There are two cases where you may need to load the SMO assemblies directly:  

- If your script references a SMO object before the first command that references the provider or cmdlets from the [!INCLUDE[ssNoVersion](../includes/ssnoversion-md.md)] snap-ins.  

- You want to port SMO code from another language, such as C# or Visual Basic, which doesn't use the provider or cmdlets.  

## Example: Loading the SQL Server Management Objects

The following code loads the SMO assemblies:  

```powershell
# Loads the SQL Server Management Objects (SMO)  

$ErrorActionPreference = "Stop"
  
$sqlpsreg="HKLM:\SOFTWARE\Microsoft\PowerShell\1\ShellIds\Microsoft.SqlServer.Management.PowerShell.sqlps"  
  
if (Get-ChildItem $sqlpsreg -ErrorAction "SilentlyContinue")  
{  
    throw "SQL Server Provider for Windows PowerShell is not installed."  
}  
else  
{  
    $item = Get-ItemProperty $sqlpsreg  
    $sqlpsPath = [System.IO.Path]::GetDirectoryName($item.Path)  
}  
  
$assemblylist =
"Microsoft.SqlServer.Management.Common",  
"Microsoft.SqlServer.Smo",  
"Microsoft.SqlServer.Dmf ",  
"Microsoft.SqlServer.Instapi ",  
"Microsoft.SqlServer.SqlWmiManagement ",  
"Microsoft.SqlServer.ConnectionInfo ",  
"Microsoft.SqlServer.SmoExtended ",  
"Microsoft.SqlServer.SqlTDiagM ",  
"Microsoft.SqlServer.SString ",  
"Microsoft.SqlServer.Management.RegisteredServers ",  
"Microsoft.SqlServer.Management.Sdk.Sfc ",  
"Microsoft.SqlServer.SqlEnum ",  
"Microsoft.SqlServer.RegSvrEnum ",  
"Microsoft.SqlServer.WmiEnum ",  
"Microsoft.SqlServer.ServiceBrokerEnum ",  
"Microsoft.SqlServer.ConnectionInfoExtended ",  
"Microsoft.SqlServer.Management.Collector ",  
"Microsoft.SqlServer.Management.CollectorEnum",  
"Microsoft.SqlServer.Management.Dac",  
"Microsoft.SqlServer.Management.DacEnum",  
"Microsoft.SqlServer.Management.Utility"  
  
foreach ($asm in $assemblylist)  
{  
    $asm = [Reflection.Assembly]::LoadWithPartialName($asm)  
}  
  
Push-Location  
cd $sqlpsPath  
update-FormatData -prependpath SQLProvider.Format.ps1xml
Pop-Location  
```

## See Also

- [SQL Server PowerShell](sql-server-powershell.md)