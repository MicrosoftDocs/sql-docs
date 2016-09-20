---
title: "Installing the Informatica Connector (SQL Server PDW)"
ms.custom: na
ms.date: 07/27/2016
ms.reviewer: na
ms.suite: na
ms.tgt_pltfrm: na
ms.topic: article
ms.assetid: 3a9cf8fe-86a5-4b75-9dc5-95cbf6354cfd
caps.latest.revision: 8
author: BarbKess
---
# Installing the Informatica Connector (SQL Server PDW)
The Informatica Connector server should be configured on the Informatica Server 64 bit computer. The Informatica Connector client should be configured on the 64-bit Informatica Client computer.  
  
## Installation Steps  
  
#### Client and Server Installation Steps  
  
1.  Run the installer msi. Installer will check the registry. On failure the installer will abort. On success it will show the **Welcome** dialog.  
  
2.  Click **Next** to proceed to the license agreement.  
  
3.  Accept to enable the **Next** button. Click **Next** to proceed.  
  
4.  Complete the Information dialog box, ask for confirmation, and click on **Install** to proceed with installation process.  
  
5.  On successful installation, the installer will show the installation is complete screen. Click on **Finish** to complete the process.  
  
See the related topic in this guide for additional post installation configuration tasks.  
  
-   [Using SQL Server PDW tables as Informatica Sources and Targets &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/using-sql-server-pdw-tables-as-informatica-sources-and-targets-sql-server-pdw.md)  
  
-   [Using Informatica to create SQL Server PDW connections in workflow manager &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/using-informatica-to-create-sql-server-pdw-connections-in-workflow-manager-sql-server-pdw.md)  
  
-   [Creating Sessions and Workflows in Informatica &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/creating-sessions-and-workflows-in-informatica-sql-server-pdw.md)  
  
## Registering and Verifying a Plug-in  
  
#### Register a Plug-in  
  
1.  Open the Informatica Administrator at  **HYPERLINK "https://infa_server_host_name:port_number" https://infa_server_host_name:port_number**. The default https port is 8443.  
  
2.  Connect to the desired repository.  
  
3.  In the Domain Navigator pane, right-click the repository.  
  
4.  On the **Properties** tab, click **Edit**.  
  
5.  In the **Edit Repository Properties** section, in the **Operating Mode** box, select **Exclusive**.  
  
6.  On the **Plug-Ins** tab, browse and select the **SQLServerPDW _Plugin.xml**. (The path of this xml file is normally where the Informatica client is installed. The default path is **C:\Informatica\\<version>\clients\PowerCenterClient\client\Microsoft**.)  
  
7.  Click **Register**, and complete the dialog box.  
  
8.  After registration, change the operating mode to **normal**.  
  
#### Verifying the Create SQL Server PDW Source and Target in Menu Item  
  
1.  In the PowerCenter Designer, open the appropriate repository.  
  
2.  On the **Tools** menu, click **Source Analyzer**. This opens the Source Analyzer window, and changes some of the menu options.  
  
3.  On **Sources** menu, confirm that the menu item **Import from SQL Server PDW** is now available.  
  
## See Also  
[Uninstalling the Informatica Connector Server &#40;SQL Server PDW&#41;](../../mpp/sqlpdw/uninstalling-the-informatica-connector-server-sql-server-pdw.md)  
  
