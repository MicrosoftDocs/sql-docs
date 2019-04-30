---
title: "Raising Events in the Script Task | Microsoft Docs"
ms.custom: ""
ms.date: "03/06/2017"
ms.prod: sql
ms.prod_service: "integration-services"
ms.reviewer: ""
ms.technology: integration-services
ms.topic: "reference"
dev_langs: 
  - "VB"
helpviewer_keywords: 
  - "events [Integration Services], scripts"
  - "warnings [Integration Services]"
  - "SSIS events, scripts"
  - "errors [Integration Services], events"
  - "SSIS Script task, events"
  - "Events property"
  - "Script task [Integration Services], events"
ms.assetid: 21ea07d1-e267-4fb1-a6cc-82c95a39beae
author: janinezhang
ms.author: janinez
manager: craigg
---
# Raising Events in the Script Task

[!INCLUDE[ssis-appliesto](../../../includes/ssis-appliesto-ssvrpluslinux-asdb-asdw-xxx.md)]


  Events provide a way to report errors, warnings, and other information, such as task progress or status, to the containing package. The package provides event handlers for managing event notifications. The Script task can raise events by calling methods on the <xref:Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptObjectModel.Events%2A> property of the **Dts** object. For more information about how [!INCLUDE[ssISnoversion](../../../includes/ssisnoversion-md.md)] packages handle events, see [Integration Services &#40;SSIS&#41; Event Handlers](../../../integration-services/integration-services-ssis-event-handlers.md).  
  
 Events can be logged to any log provider that is enabled in the package. Log providers store information about events in a data store. The Script task can also use the <xref:Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptObjectModel.Log%2A> method to log information to a log provider without raising an event. For more information about how to use the <xref:Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptObjectModel.Log%2A> method, see [Logging in the Script Task](../../../integration-services/extending-packages-scripting/task/logging-in-the-script-task.md).  
  
 To raise an event, the Script task calls one of the methods exposed by the <xref:Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptObjectModel.Events%2A> property. The following table lists the methods exposed by the <xref:Microsoft.SqlServer.Dts.Tasks.ScriptTask.ScriptObjectModel.Events%2A> property.  
  
|Event|Description|  
|-----------|-----------------|  
|<xref:Microsoft.SqlServer.Dts.Runtime.IDTSComponentEvents.FireCustomEvent%2A>|Raises a user-defined custom event in the package.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.IDTSComponentEvents.FireError%2A>|Informs the package of an error condition.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.IDTSComponentEvents.FireInformation%2A>|Provides information to the user.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.IDTSComponentEvents.FireProgress%2A>|Informs the package of the progress of the task.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.IDTSComponentEvents.FireQueryCancel%2A>|Returns a value that indicates whether the package needs the task to shut down prematurely.|  
|<xref:Microsoft.SqlServer.Dts.Runtime.IDTSComponentEvents.FireWarning%2A>|Informs the package that the task is in a state that warrants user notification, but is not an error condition.|  
  
## Events Example  
 The following example demonstrates how to raise events from within the Script task. The example uses a native Windows API function to determine whether an Internet connection is available. If no connection is available, it raises an error. If a potentially volatile modem connection is in use, the example raises a warning. Otherwise, it returns an informational message that an Internet connection has been detected.  
  
```vb  
Private Declare Function InternetGetConnectedState Lib "wininet" _  
    (ByRef dwFlags As Long, ByVal dwReserved As Long) As Long  
  
Private Enum ConnectedStates  
    LAN = &H2  
    Modem = &H1  
    Proxy = &H4  
    Offline = &H20  
    Configured = &H40  
    RasInstalled = &H10  
End Enum  
  
Public Sub Main()  
  
    Dim dwFlags As Long  
    Dim connectedState As Long  
    Dim fireAgain as Boolean  
  
    connectedState = InternetGetConnectedState(dwFlags, 0)  
  
    If connectedState <> 0 Then  
        If (dwFlags And ConnectedStates.Modem) = ConnectedStates.Modem Then  
            Dts.Events.FireWarning(0, "Script Task Example", _  
                "Volatile Internet connection detected.", String.Empty, 0)  
        Else  
            Dts.Events.FireInformation(0, "Script Task Example", _  
                "Internet connection detected.", String.Empty, 0, fireAgain)  
        End If  
    Else  
        ' If not connected to the Internet, raise an error.  
        Dts.Events.FireError(0, "Script Task Example", _  
            "Internet connection not available.", String.Empty, 0)  
    End If  
  
    Dts.TaskResult = ScriptResults.Success  
  
End Sub  
```  
  
```csharp  
using System;  
using System.Data;  
using Microsoft.SqlServer.Dts.Runtime;  
using System.Windows.Forms;  
using System.Runtime.InteropServices;  
  
public class ScriptMain  
{  
  
[DllImport("wininet")]  
        private extern static long InternetGetConnectedState(ref long dwFlags, long dwReserved);  
  
        private enum ConnectedStates  
        {  
            LAN = 0x2,  
            Modem = 0x1,  
            Proxy = 0x4,  
            Offline = 0x20,  
            Configured = 0x40,  
            RasInstalled = 0x10  
        };  
  
        public void Main()  
        {  
            //  
            long dwFlags = 0;  
            long connectedState;  
            bool fireAgain = true;  
            int state;  
  
            connectedState = InternetGetConnectedState(ref dwFlags, 0);  
            state = (int)ConnectedStates.Modem;  
            if (connectedState != 0)  
            {  
                if ((dwFlags & state) == state)  
                {  
                    Dts.Events.FireWarning(0, "Script Task Example", "Volatile Internet connection detected.", String.Empty, 0);  
                }  
                else  
                {  
                    Dts.Events.FireInformation(0, "Script Task Example", "Internet connection detected.", String.Empty, 0, ref fireAgain);  
                }  
            }  
            else  
            {  
                // If not connected to the Internet, raise an error.  
                Dts.Events.FireError(0, "Script Task Example", "Internet connection not available.", String.Empty, 0);  
            }  
  
            Dts.TaskResult = (int)ScriptResults.Success;  
  
        }  
  
```  
  
## See Also  
 [Integration Services &#40;SSIS&#41; Event Handlers](../../../integration-services/integration-services-ssis-event-handlers.md)   
 [Add an Event Handler to a Package](https://msdn.microsoft.com/library/5e56885d-8658-480a-bed9-3f2f8003fd78)  
  
  
