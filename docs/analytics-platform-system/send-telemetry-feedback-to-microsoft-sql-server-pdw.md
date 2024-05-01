---
title: Telemetry feedback
description: Send telemetry feedback to Microsoft for Analytics Platform System.
author: charlesfeddersen
ms.author: charlesf
ms.reviewer: martinle
ms.date: 12/04/2023
ms.service: sql
ms.subservice: data-warehouse
ms.topic: how-to
---

# Send telemetry feedback to Microsoft for Analytics Platform System
Analytics Platform System has an optional telemetry feature that sends Admin Console data to Microsoft. 
  
> [!NOTE]  
> In this release, Microsoft is not actively monitoring the telemetry data. The data is being collected for analysis purposes only.  
  
## <a id="privacy"></a> Privacy
To provide the maximum privacy protection, APS ships without enabling telemetry. Before enabling this feature, first review the [Microsoft Analytics Platform System Privacy Statement](https://go.microsoft.com/fwlink/?LinkId=400902). To opt in, run the PowerShell script described below.  
  
## <a id="enable"></a> Enable Telemetry
**DNS Forwarding:** Sending telemetry data to Microsoft requires Analytics Platform System to connect to the internet through a DNS forwarder. To enable this feature, you must enable DNS forwarding on all hosts and workload VMs. Invoke the `Enable-RemoteMonitoring` command with the `SetupDnsForwarder` option to properly configure DNS forwarding and enable telemetry. Invoke the `Enable-RemoteMonitoring` command without the `SetupDnsForwarder` option when DNS forwarding is already configured and you only want to enable Heartbeat Monitoring.  
  
> [!IMPORTANT]  
> Enabling DNS forwarding opens the internet connection for all hosts and workload VMs.  
  
#### <a id="to-enable-feedback"></a> Enable feedback
  
1. Using an appliance domain administrator account, connect to the Control node (***appliance_domain*-CTL01**) and open a command prompt using your Windows administrator credentials.  
  
1. Navigate to the following directory: `C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100`.  
  
1. Import the module `Configure-RemoteMonitoring.ps1`  
  
    > [!NOTE]  
    > To import you must use two periods in the command.  
  
    **Example:**  
  
    ```console
    PS C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100> . .\Configure-RemoteMonitoring.ps1  
    ```  
  
1. Invoke the `Enable-RemoteMonitoring` command.  
  
    > [!NOTE]  
    > The script assumes that the internet connection is working properly and does not validate the internet connection.  
  
    1. The first time you enable the telemetry, use the following command to ensure all DNS Forwarders are correctly configured. In this example replace the DNS Forwarded IP address `xx.xx.xx.xx` with the DNS Forwarder IP address in your environment.  
  
        ```console
        PS C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100> Enable-RemoteMonitoring -SetupDnsForwarder -DnsForwarderIp xx.xx.xx.xx  
        ```  
  
    1. When DNS forwarders are already configured, such as re-enabling previously disabled telemetry, use the following command to enable telemetry without configuring DNS Forwarding.  
  
        ```console
        PS C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100> Enable-RemoteMonitoring  
        ```  
  
1. You will be prompted to read and acknowledge that APS will collect information about the appliance. There will be a link to the APS privacy statement which you can copy and paste into any internet browser for review.  
  
1. Enter **Y** to accept and opt in to feedback or enter **N** to not accept.  
  
1. If you entered **Y** there will be a series of commands that will run which will enable the telemetry (and optionally the DNS Forwarder) feature on your appliance. If you see any errors or information that leads you to believe that the command was not successful contact CSS for assistance.  
  
If you entered **N**, no commands will be run and the feature will not be enabled and there is nothing more for you to do.  
  
There is no harm in running the `Enable-RemoteMonitoring` command multiple times. If the DNS forwarder is already set you will get a warning message indicating that is the case.  
  
## <a id="disable"></a> Disable telemetry
Disabling telemetry will stop all operations which communicate information about the state of the appliance to the APS monitoring service in the cloud.  
  
> [!IMPORTANT]  
> Performing this operation will not disable your DNS forwarder or internet connection which might be in use by other features in the appliance.  
  
#### <a id="to-disable-telemetry"></a> Disable telemetry
  
1. Using an appliance domain administrator account, connect to the Control node (***appliance_domain*-CTL01**) and open a PowerShell window with administrator privileges.  
  
1. Navigate to the following directory: `C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100`.  
  
1. Import the module `Configure-RemoteMonitoring.ps1`  
  
    > [!NOTE]  
    > To import you must use two periods in the command.  
  
    **Example:**  
  
    ```console
    PS C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100> . .\Configure-RemoteMonitoring.ps1  
    ```  
  
1. Invoke the `Disable-RemoteMonitoring` command without parameters. This command will stop sending feedback. (This will not affect local monitoring.) However, the command will not disable the DNS Forwarder and/or disable any internet connectivity. This must be done manually after successfully disabling feedback.  
  
    **Example:**  
  
    ```console
    PS C:\Program Files\Microsoft SQL Server Parallel Data Warehouse\100> Disable-RemoteMonitoring  
    ```  

If you see any errors or information that leads you to believe that the command was not successful contact CSS for assistance.  
  
There is no harm in running the `Disable-RemoteMonitoring` command multiple times.  

## Related content

- [Monitor the appliance with the Admin Console - Analytics Platform System](monitor-the-appliance-by-using-the-admin-console.md)
- [Monitor the appliance with system views - Analytics Platform System](monitor-the-appliance-by-using-system-views.md)
- [Monitor with System Center Operations Manager - Analytics Platform System](monitor-the-appliance-by-using-system-center-operations-manager.md)
- [Use a DNS Forwarder to Resolve Non-Appliance DNS Names in Analytics Platform System](use-a-dns-forwarder-to-resolve-non-appliance-dns-names.md)
