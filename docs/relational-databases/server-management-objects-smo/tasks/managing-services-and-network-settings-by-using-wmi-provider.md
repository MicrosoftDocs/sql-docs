---
title: Manage services and network settings with the WMI provider
description: Manage SQL Server services and network settings with the WMI provider in MMC.
author: "markingmyname"
ms.author: "maghan"
ms.reviewer: randolphwest
ms.date: 11/30/2023
ms.service: sql
ms.topic: "reference"
helpviewer_keywords:
  - "WMI provider [SMO]"
  - "services [SQL Server], SMO"
  - "network settings [SMO]"
  - "monitoring [SMO]"
monikerRange: "=azuresqldb-current || =azure-sqldw-latest || >=sql-server-2016 || >=sql-server-linux-2017 || =azuresqldb-mi-current"
---
# Manage services and network settings with the WMI provider

[!INCLUDE [sqlserver2022](../../../includes/applies-to-version/sqlserver2022.md)]

The WMI provider is a published interface that is used by [!INCLUDE [msCoName](../../../includes/msconame-md.md)] Management Console (MMC) to manage the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] services and network protocols. In SMO, the <xref:Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer> object represents the WMI provider.

The <xref:Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer> object operates independently of the connection established with the <xref:Microsoft.SqlServer.Management.Smo.Server> object to an instance of [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)], and uses Windows credentials to connect to the WMI service.

## Examples

To use any code example that is provided, choose the programming environment, template, and language in which to create your application. For more information, see [How to Create a Visual C# SMO Project in Visual Studio .NET](../how-to-create-a-visual-csharp-smo-project-in-visual-studio-net.md).

For programs that use the [!INCLUDE [ssNoVersion](../../../includes/ssnoversion-md.md)] WMI provider, you must include the `Imports` statement to qualify the WMI namespace. Insert the statement after the other `Imports` statements, before any declarations in the application, such as:

`Imports Microsoft.SqlServer.Management.Smo`  
`Imports Microsoft.SqlServer.Management.Common`  
`Imports Microsoft.SqlServer.Management.Smo.Wmi`


## Stop and restart the SQL Server service in Visual Basic

This code example shows how to stop and start services by using the SMO <xref:Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer> object. This provides an interface to the WMI provider for Configuration Management.

```vbnet
'Declare and create an instance of the ManagedComputer object that represents the WMI provider services.
Dim mc As ManagedComputer
mc = New ManagedComputer()
'Iterate through each service registered with the WMI provider.
Dim svc As Service
For Each svc In mc.Services
    Console.WriteLine(svc.Name)
Next
'Reference the SQL Server service.
svc = mc.Services("MSSQLSERVER")
'Stop the service if it is running and report on the status continuously until it has stopped.
If svc.ServiceState = ServiceState.Running Then
    svc.Stop()

    Console.WriteLine(String.Format("{0} service state is {1}", svc.Name, svc.ServiceState))
    Do Until String.Format("{0}", svc.ServiceState) = "Stopped"
        Console.WriteLine(String.Format("{0}", svc.ServiceState))
        svc.Refresh()
    Loop
    Console.WriteLine(String.Format("{0} service state is {1}", svc.Name, svc.ServiceState))
    'Start the service and report on the status continuously until it has started.
    svc.Start()
    Do Until String.Format("{0}", svc.ServiceState) = "Running"
        Console.WriteLine(String.Format("{0}", svc.ServiceState))
        svc.Refresh()
    Loop
    Console.WriteLine(String.Format("{0} service state is {1}", svc.Name, svc.ServiceState))

Else
    Console.WriteLine("SQL Server service is not running.")
End If
```

## Enable a server protocol using a URN string in Visual Basic

The code example shows how to identify a server protocol using a URN object, and then enables the protocol.

```vbnet
'This program must run with administrator privileges.
'Declare the ManagedComputer WMI interface.
Dim mc As New ManagedComputer()

'Create a URN object that represents the TCP server protocol.
Dim u As New Urn("ManagedComputer[@Name='V-ROBMA3']/ServerInstance[@Name='MSSQLSERVER']/ServerProtocol[@Name='Tcp']")

'Declare the serverProtocol variable and return the ServerProtocol object.
Dim sp As ServerProtocol
sp = mc.GetSmoObject(u)

'Enable the protocol.
sp.IsEnabled = True

'propagate back to the service
sp.Alter()
```

## Enable a server protocol using a URN string in PowerShell

The code example shows how to identify a server protocol using a URN object, and then enables the protocol.

```powershell
#This example shows how to identify a server protocol using a URN object, and then enable the protocol
#This program must run with administrator privileges.

#Load the assembly containing the classes used in this example
[reflection.assembly]::LoadWithPartialName("Microsoft.SqlServer.SqlWmiManagement")

#Get a managed computer instance
$mc = New-Object -TypeName Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer

#Create a URN object that represents the TCP server protocol
#Change 'MyPC' to the name of the your computer
$urn = New-Object -TypeName Microsoft.SqlServer.Management.Sdk.Sfc.Urn -argumentlist "ManagedComputer[@Name='MyPC']/ServerInstance[@Name='MSSQLSERVER']/ServerProtocol[@Name='Tcp']"

#Get the protocol object
$sp = $mc.GetSmoObject($urn)

#enable the protocol on the object
$sp.IsEnabled = $true

#propagate back to actual service
$sp.Alter()
```

## Start and stop a service in C#

The code example shows how to stop and start an instance of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].

```csharp
//Declare and create an instance of the ManagedComputer
//object that represents the WMI provider services.
ManagedComputer mc;
mc = new ManagedComputer();

//Iterate through each service registered with the WMI provider.
foreach (Service svc in mc.Services)
{
   Console.WriteLine(svc.Name);
}

//Reference the SQL Server service.
Service mySvc = mc.Services["MSSQLSERVER"];

//Stop the service if it is running and report on the status
// continuously until it has stopped.
if (mySvc.ServiceState == ServiceState.Running)
{
   mySvc.Stop();
   Console.WriteLine(string.Format("{0} service state is {1}", mySvc.Name, mySvc.ServiceState));
   while (!(string.Format("{0}", mySvc.ServiceState) == "Stopped"))
   {
         Console.WriteLine(string.Format("{0}", mySvc.ServiceState));
         mySvc.Refresh();
   }

   Console.WriteLine(string.Format("{0} service state is {1}", mySvc.Name, mySvc.ServiceState));
   //Start the service and report on the status continuously
   //until it has started.
   mySvc.Start();
   while (!(string.Format("{0}", mySvc.ServiceState) == "Running"))
   {
         Console.WriteLine(string.Format("{0}", mySvc.ServiceState));
         mySvc.Refresh();
   }

   Console.WriteLine(string.Format("{0} service state is {1}", mySvc.Name, mySvc.ServiceState));
   Console.ReadLine();
}
else
{
   Console.WriteLine("SQL Server service is not running.");
   Console.ReadLine();
}
```

## Start and stop a service in PowerShell

The code example shows how to stop and start an instance of [!INCLUDE [ssnoversion-md](../../../includes/ssnoversion-md.md)].

```powershell
#Load the assembly containing the objects used in this example
[reflection.assembly]::LoadWithPartialName("Microsoft.SqlServer.SqlWmiManagement")

#Get a managed computer instance
$mc = New-Object -TypeName Microsoft.SqlServer.Management.Smo.Wmi.ManagedComputer

#List out all SQL Server instances running on this mc
foreach ($Item in $mc.Services) { $Item.Name }

#Get the default SQL Server database engine service
$svc = $mc.Services["MSSQLSERVER"]

# for stopping and starting services PowerShell must run as administrator

#Stop this service
$svc.Stop()
$svc.Refresh()
while ($svc.ServiceState -ne "Stopped") {
    $svc.Refresh()
    $svc.ServiceState
}
"Service" + $svc.Name + " is now stopped"
"Starting " + $svc.Name
$svc.Start()
$svc.Refresh()
while ($svc.ServiceState -ne "Running") {
    $svc.Refresh()
    $svc.ServiceState
}
$svc.ServiceState
"Service" + $svc.Name + "is now started"
```

## Related content

- [WMI Provider for Configuration Management](../../wmi-provider-configuration/wmi-provider-for-configuration-management.md)
